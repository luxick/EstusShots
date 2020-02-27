using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using EstusShots.Client;
using EstusShots.Gtk.Controls;
using EstusShots.Shared.Models;
using Gtk;
using Application = Gtk.Application;
using DateTime = System.DateTime;
using UI = Gtk.Builder.ObjectAttribute;

namespace EstusShots.Gtk
{
    class MainWindow : Window
    {
        private const string ApiUrl = "http://localhost:5000/api/";
        
        private EstusShotsClient Client { get; }
            
        [UI] private readonly TreeView _seasonsView = null;
        [UI] private readonly Button _loadButton = null;
        [UI] private readonly Button _newSeasonButton = null;
        [UI] private readonly Label _infoLabel = null;

        private BindableListView<Season> SeasonsView { get; set; }
        
        public MainWindow() : this(new Builder("MainWindow.glade")) { }

        private MainWindow(Builder builder) : base(builder.GetObject("MainWindow").Handle)
        {
            builder.Autoconnect(this);
            Client = new EstusShotsClient("http://localhost:5000/api/");

            DeleteEvent += Window_DeleteEvent;
            _loadButton.Clicked += LoadButtonClicked;
            _newSeasonButton.Clicked += NewSeasonButtonOnClicked;

            var seasonsColumns = new List<DataColumn>
            {
                new DataColumn(nameof(Season.DisplayName)){Title = "Name"}
            };
            SeasonsView = new BindableListView<Season>(seasonsColumns, nameof(Season.SeasonId) ,_seasonsView);
            SeasonsView.OnSelectionChanged += SeasonsViewOnOnSelectionChanged;
            
            Info("Application Started");
        }

        private void SeasonsViewOnOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(e.Selection is Season season)) return;
            Info($"Season '{season.DisplayName}' selected");
        }

        private void NewSeasonButtonOnClicked(object sender, EventArgs e)
        {
            if (!SeasonsView.Items.Any())
            {
                Info("Cannot add Season (Not Loaded)");
                return;
            }
            var nextNum = SeasonsView.Items.Max(x => x.Number) + 1 ;
            var season = new Season
            {
                Game = "Test Game",
                Number = nextNum,
                Start = DateTime.Now
            };           
            var content = new StringContent(JsonSerializer.Serialize(season), Encoding.UTF8, "application/json");
            var client = new HttpClient();
            try
            {
                var response = client.PostAsync(ApiUrl + "season", content).Result;

                if (!response.IsSuccessStatusCode)
                {
                    _infoLabel.Text = $"Error while creating Season: {response.ReasonPhrase}";
                    return;
                }

                ReloadSeasons();
                Info($"Created new Season");
            }
            catch (Exception ex)
            {
                _infoLabel.Text = $"Exception Occured: {ex.Message}";
                Console.WriteLine(ex.Message);
            }
        }
        
        private void LoadButtonClicked(object sender, EventArgs a)
        {
            ReloadSeasons();
            Info("List Refreshed");
        }

        private void ReloadSeasons()
        {
            var seasons = Client.GetSeasons().Result;
            SeasonsView.Items = seasons;
            SeasonsView.DataBind();
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