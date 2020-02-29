using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstusShots.Client;
using EstusShots.Gtk.Controls;
using EstusShots.Shared.Dto;
using EstusShots.Shared.Models.Parameters;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace EstusShots.Gtk
{
    internal class MainWindow : Window
    {
        private const string ApiUrl = "http://localhost:5000/api/";
        
        [UI] private readonly Label _infoLabel = null;
        [UI] public readonly Button LoadButton = null;
        [UI] public readonly Box LoadingSpinner = null;
        [UI] public readonly Button NewSeasonButton = null;
        [UI] public readonly Button AddEpisodeButton = null;
        [UI] public readonly Overlay SeasonsOverlay = null;
        [UI] public readonly Overlay EpisodesOverlay = null;
        [UI] public readonly TreeView SeasonsView = null;
        [UI] public readonly TreeView EpisodesTreeView = null;
        [UI] public readonly Notebook Navigation = null;
        [UI] public readonly Box EpisodesPage = null;

        private EstusShotsClient Client { get; }
        private BindableListControl<Season> SeasonsControl { get; set; }
        private BindableListControl<Episode> EpisodesControl { get; set; }

        public MainWindow() : this(new Builder("MainWindow.glade"))
        {
        }

        private MainWindow(Builder builder) : base(builder.GetObject("MainWindow").Handle)
        {
            builder.Autoconnect(this);
            Client = new EstusShotsClient(ApiUrl);

            DeleteEvent += Window_DeleteEvent;
            LoadButton.Clicked += LoadButtonClicked;
            NewSeasonButton.Clicked += NewSeasonButtonOnClicked;
            AddEpisodeButton.Clicked += AddEpisodeButtonOnClicked;

            InitSeasonsControl();
            InitEpisodesControl();
            
            // The episodes page is not shown, as long as no season is selected 
            EpisodesPage.Hide();
            
            Info("Application Started");

            // No need to wait for the loading to finnish
            var _ = ReloadSeasons();
        }

        private void InitSeasonsControl()
        {
            var columns = new List<DataColumn>
            {
                new DataColumn(nameof(Season.DisplayName)) {Title = "Name"},
                new DataColumn(nameof(Season.Description)) {Title = "Description"},
                new DataColumn(nameof(Season.Start))
                {
                    Title = "Start",
                    Format = date => (date as DateTime?)?.ToString("dd.MM.yyyy")
                },
                new DataColumn(nameof(Season.End))
                {
                    Title = "End",
                    Format = date => (date as DateTime?)?.ToString("dd.MM.yyyy") ?? "Ongoing"
                }
            };
            SeasonsControl = new BindableListControl<Season>(columns, nameof(Season.SeasonId), SeasonsView);
            SeasonsControl.OnSelectionChanged += SeasonsViewOnOnSelectionChanged;
        }

        private void InitEpisodesControl()
        {
            var columns = new List<DataColumn>
            {
                new DataColumn(nameof(Episode.DisplayName)) {Title = "Name"},
                new DataColumn(nameof(Episode.Title)) {Title = "Title"},
                new DataColumn(nameof(Episode.Date))
                {
                    Title = "Date",
                    Format = d => (d as DateTime?)?.ToString("dd.MM.yyyy")
                },
                new DataColumn(nameof(Episode.Start))
                {
                    Title = "Start",
                    Format = d => (d as DateTime?)?.ToString("HH:mm")
                    
                },
                new DataColumn(nameof(Episode.End))
                {
                    Title = "End",
                    Format = d => (d as DateTime?)?.ToString("HH:mm") ?? "Ongoing"
                }
            };
            EpisodesControl = new BindableListControl<Episode>(columns, nameof(Episode.EpisodeId), EpisodesTreeView);
        }

        private async void SeasonsViewOnOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(e.Selection is Season season)) return;
            using var _ = new LoadingMode(this);
            
            EpisodesPage.Show();
            var parameter = new GetEpisodesParameter(season.SeasonId);
            var res = await Client.Episodes.GetEpisodes(parameter);
            EpisodesControl.Items = res.Data.Episodes;
            EpisodesControl.DataBind();
            Info($"{season.DisplayName}: {res.Data.Episodes.Count} episodes");
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
                _infoLabel.Text = $"Error while creating Season: {res.OperationResult.ShortMessage}";
                return;
            }
            await ReloadSeasons();
            Info("Created new Season");
        }
        
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

        private async void LoadButtonClicked(object sender, EventArgs a)
        {
            using var _ = new LoadingMode(this);
            Info("Loading Seasons...");
            await ReloadSeasons();
        }

        private async Task ReloadSeasons()
        {
            var res = await Task.Factory.StartNew(
                () => Client.Seasons.GetSeasons(new GetSeasonsParameter()).Result);
            if (!res.OperationResult.Success)
            {
                _infoLabel.Text = $"Refresh Failed: {res.OperationResult.ShortMessage}";
                return;
            }

            SeasonsControl.Items = res.Data.Seasons;
            SeasonsControl.DataBind();
            Info("Seasons Refreshed");
        }

        private async Task ReloadEpisodes()
        {
            var seasonId = SeasonsControl.SelectedItem.SeasonId;
            var res = await Task.Factory.StartNew(
                () => Client.Episodes.GetEpisodes(new GetEpisodesParameter(seasonId)).Result);
            if (!res.OperationResult.Success)
            {
                _infoLabel.Text = $"Refresh Failed: {res.OperationResult.ShortMessage}";
                return;
            }

            EpisodesControl.Items = res.Data.Episodes;
            EpisodesControl.DataBind();
            Info("Episodes Refreshed");
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }

        private void Info(string message)
        {
            _infoLabel.Text = message;
        }
    }
}