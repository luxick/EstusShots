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

        private async void NewSeasonButtonOnClicked(object sender, EventArgs e)
        {
            using var _ = new LoadingMode(this);
            // TODO real season edit control
            var season = new Season
            {
                Game = "Test Game",
                Number = SeasonsControl.Items.Any() ? SeasonsControl.Items.Max(x => x.Number) + 1 : 1,
                Start = DateTime.Now,
                Description = "This is a demo description!"
            };
            var parameter = new SaveSeasonParameter(season);
            var res = await Client.Seasons.SaveSeason(parameter);
            if (!res.OperationResult.Success)
            {
                InfoLabel.Text = $"Error while creating Season: {res.OperationResult.ShortMessage}";
                ErrorDialog.Show(res.OperationResult);
                return;
            }

            await ReloadSeasons();
            Info("Created new Season");
        }

        private async void SeasonsControlOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(e.Selection is Season season)) return;
            using var _ = new LoadingMode(this);

            EpisodesPage.Show();
            var parameter = new GetEpisodesParameter(season.SeasonId);
            var res = await Client.Episodes.GetEpisodes(parameter);
            EpisodesControl.Items = res.Data.Episodes;
            EpisodesControl.DataBind();

            UpdateTitle();
            Navigation.Page = EpisodesPageNumber;

            Info($"{season.DisplayName}: {res.Data.Episodes.Count} episodes");
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

            SeasonsControl.Items = res.Data.Seasons;
            SeasonsControl.DataBind();
            Info("Seasons Refreshed");
        }

        private void CreateSeasonsControl()
        {
            var columns = new List<DataColumn>
            {
                new DataColumnText(nameof(Season.DisplayName)) {Title = "Name"},
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
            SeasonsControl.OnSelectionChanged += SeasonsControlOnSelectionChanged;
        }
    }
}