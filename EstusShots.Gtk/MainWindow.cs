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
        [UI] public readonly Overlay SeasonsOverlay = null;
        [UI] public readonly TreeView SeasonsView = null;

        public MainWindow() : this(new Builder("MainWindow.glade"))
        {
        }

        private MainWindow(Builder builder) : base(builder.GetObject("MainWindow").Handle)
        {
            builder.Autoconnect(this);
            Client = new EstusShotsClient("http://localhost:5000/api/");

            DeleteEvent += Window_DeleteEvent;
            LoadButton.Clicked += LoadButtonClicked;
            NewSeasonButton.Clicked += NewSeasonButtonOnClicked;

            var seasonsColumns = new List<DataColumn>
            {
                new DataColumn(nameof(Season.DisplayName)) {Title = "Name"},
                new DataColumn(nameof(Season.Description)) {Title = "Description"},
                new DataColumn(nameof(Season.Start))
                {
                    Title = "Start",
                    Format = date => (date as DateTime?)?.ToString("dd.MM.yyyy hh:mm")
                }
            };
            SeasonsControl = new BindableListControl<Season>(seasonsColumns, nameof(Season.SeasonId), SeasonsView);
            SeasonsControl.OnSelectionChanged += SeasonsViewOnOnSelectionChanged;
            Info("Application Started");

            // No need to wait for the loading to finnish
            var _ = ReloadSeasons();
        }

        private EstusShotsClient Client { get; }
        private BindableListControl<Season> SeasonsControl { get; }

        private void SeasonsViewOnOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(e.Selection is Season season)) return;
            Info($"Season '{season.DisplayName}' selected");
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
            Info("List Refreshed");
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