using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstusShots.Gtk.Controls;
using EstusShots.Shared.Dto;
using EstusShots.Shared.Models.Parameters;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace EstusShots.Gtk
{
    internal partial class MainWindow
    {
        [UI] public readonly Box EpisodesPage = null;
        [UI] public readonly Button AddEpisodeButton = null;
        [UI] public readonly TreeView EpisodesTreeView = null;

        private BindableListControl<Episode> EpisodesControl { get; set; }


        private void InitEpisodesPage()
        {
            AddEpisodeButton.Clicked += AddEpisodeButtonOnClicked;

            CreateEpisodesControl();
        }

        // Events

        private async void AddEpisodeButtonOnClicked(object sender, EventArgs e)
        {
            if (SeasonsControl.SelectedItem == null) return;
            using var _ = new LoadingMode(this);

            var season = SeasonsControl.SelectedItem;

            // Some random test data
            var rand = new Random();
            var start = new DateTime(
                DateTime.Today.Year,
                DateTime.Today.Month,
                DateTime.Today.Day,
                DateTime.Today.Hour + rand.Next(0, 12),
                DateTime.Today.Minute + rand.Next(0, 59),
                DateTime.Today.Second
            );
            var end = start.AddHours(rand.Next(2, 4));

            var ep = new Episode
            {
                SeasonId = season.SeasonId,
                Number = EpisodesControl.Items.Any() ? EpisodesControl.Items.Max(x => x.Number) + 1 : 1,
                Title = $"An Episode in season '{season.Game}'",
                Date = DateTime.Today,
                Start = start,
                End = end
            };

            var parameter = new SaveEpisodeParameter(ep);
            var res = await Client.Episodes.SaveEpisode(parameter);

            if (!res.OperationResult.Success)
            {
                Info($"Cannot add episode: {res.OperationResult.ShortMessage}");
                return;
            }

            await ReloadEpisodes();
        }

        // Private Methods

        private async Task ReloadEpisodes()
        {
            var seasonId = SeasonsControl.SelectedItem.SeasonId;
            var res = await Task.Factory.StartNew(
                () => Client.Episodes.GetEpisodes(new GetEpisodesParameter(seasonId)).Result);
            if (!res.OperationResult.Success)
            {
                InfoLabel.Text = $"Refresh Failed: {res.OperationResult.ShortMessage}";
                return;
            }

            EpisodesControl.Items = res.Data.Episodes;
            EpisodesControl.DataBind();
            Info("Episodes Refreshed");
        }

        private void CreateEpisodesControl()
        {
            var columns = new List<DataColumn>
            {
                new DataColumnText(nameof(Episode.DisplayName)) {Title = "Name"},
                new DataColumnText(nameof(Episode.Title)) {Title = "Title"},
                new DataColumnText(nameof(Episode.Date))
                {
                    Title = "Date",
                    DisplayConverter = d => (d as DateTime?)?.ToString("dd.MM.yyyy")
                },
                new DataColumnText(nameof(Episode.Start))
                {
                    Title = "Start",
                    DisplayConverter = d => (d as DateTime?)?.ToString("HH:mm")
                },
                new DataColumnText(nameof(Episode.End))
                {
                    Title = "End",
                    DisplayConverter = d => (d as DateTime?)?.ToString("HH:mm") ?? "Ongoing"
                }
            };
            EpisodesControl = new BindableListControl<Episode>(columns, nameof(Episode.EpisodeId), EpisodesTreeView);
        }
    }
}