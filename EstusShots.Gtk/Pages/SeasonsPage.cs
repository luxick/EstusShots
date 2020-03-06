using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstusShots.Gtk.Controls;
using EstusShots.Gtk.Dialogs;
using EstusShots.Shared.Dto;
using EstusShots.Shared.Models.Parameters;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace EstusShots.Gtk
{
    internal partial class MainWindow
    {
        [UI] public readonly Button LoadButton = null;
        [UI] public readonly Button NewSeasonButton = null;
        [UI] public readonly TreeView SeasonsView = null;
        [UI] public readonly Overlay SeasonsOverlay = null;

        private BindableListControl<Season> SeasonsControl { get; set; }

        private void InitSeasonsPage()
        {
            CreateSeasonsControl();

            LoadButton.Clicked += LoadButtonClicked;
            NewSeasonButton.Clicked += NewSeasonButtonOnClicked;

            // No need to wait for the loading to finnish
            var _ = ReloadSeasons();
        }

        // Events 

        private async void LoadButtonClicked(object sender, EventArgs a)
        {
            using var _ = new LoadingMode(this);
            Info("Loading Seasons...");
            await ReloadSeasons();
        }

        private void NewSeasonButtonOnClicked(object sender, EventArgs e)
        {
            var dialog = new SeasonEditor(this, new Season());
            dialog.OnDialogClosed += SeasonEditorClosed;
            dialog.Show();
        }

        private async void SeasonEditorClosed(object o, DialogClosedEventArgs<Season> args)
        {
            if (!args.Ok) return;
            
            using var _ = new LoadingMode(this);
            
            var parameter = new SaveSeasonParameter(args.Model);
            var res = await Client.Seasons.SaveSeason(parameter);
            if (!res.OperationResult.Success)
            {
                InfoLabel.Text = $"Error while creating Season: {res.OperationResult.ShortMessage}";
                ErrorDialog.Show(res.OperationResult);
                return;
            }

            await ReloadSeasons();
            Info($"Season {args.Model.DisplayName}");
        }

        private async void SeasonsControlSelectionChanged(object sender, SelectionChangedEventArgs<Season> e)
        {
            EpisodesPage.Show();
            var parameter = new GetEpisodesParameter(e.Selection.SeasonId);
            var res = await Client.Episodes.GetEpisodes(parameter);
            EpisodesControl.Items = res.Data.Episodes;
            EpisodesControl.DataBind();
            UpdateTitle();
            Info($"{e.Selection.DisplayName}: {res.Data.Episodes.Count} episodes");
        }
        
        private void SeasonsControlItemActivated(Season item)
        {
            var dialog = new SeasonEditor(this, item);
            dialog.OnDialogClosed += SeasonEditorClosed;
            dialog.Show();
        }

        // Private Methods

        private async Task ReloadSeasons()
        {
            var res = await Task.Factory.StartNew(
                () => Client.Seasons.GetSeasons(new GetSeasonsParameter()).Result);
            if (!res.OperationResult.Success)
            {
                InfoLabel.Text = $"Refresh Failed: {res.OperationResult.ShortMessage}";
                ErrorDialog.Show(res.OperationResult);
                return;
            }
            
            // TODO Initial ordering should be done by the control
            SeasonsControl.Items = res.Data.Seasons.OrderBy(x => x.DisplayName).ToList();
            SeasonsControl.DataBind();
            Info("Seasons Refreshed");
        }

        private void CreateSeasonsControl()
        {
            var columns = new List<DataColumn>
            {
                new DataColumnText(nameof(Season.DisplayName)) {Title = "Name", SortOrder = SortType.Ascending},
                new DataColumnText(nameof(Season.Description)),
                new DataColumnText(nameof(Season.Start))
                {
                    DisplayConverter = date => (date as DateTime?)?.ToString("dd.MM.yyyy")
                },
                new DataColumnText(nameof(Season.End))
                {
                    DisplayConverter = date => (date as DateTime?)?.ToString("dd.MM.yyyy") ?? "Ongoing"
                }
            };
            SeasonsControl = new BindableListControl<Season>(columns, nameof(Season.SeasonId), SeasonsView);
            SeasonsControl.SelectionChanged += SeasonsControlSelectionChanged;
            SeasonsControl.ItemActivated += SeasonsControlItemActivated;
        }
    }
}