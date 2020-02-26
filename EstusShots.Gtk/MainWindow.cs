using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using EstusShots.Client;
using EstusShots.Gtk.DataStores;
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

        private SeasonsDataStore SeasonsView { get; set; }
        
        public MainWindow() : this(new Builder("MainWindow.glade")) { }

        private MainWindow(Builder builder) : base(builder.GetObject("MainWindow").Handle)
        {
            builder.Autoconnect(this);
            Client = new EstusShotsClient("http://localhost:5000/api/");

            DeleteEvent += Window_DeleteEvent;
            _loadButton.Clicked += LoadButtonClicked;
            _newSeasonButton.Clicked += NewSeasonButtonOnClicked;

            SeasonsView = new SeasonsDataStore(_seasonsView);
            
            Info("Application Started");
        }

        private void NewSeasonButtonOnClicked(object sender, EventArgs e)
        {
            var season = new Season
            {
                Game = "Test Game",
                Number = 1,
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
                
                Info($"Created new Season");
            }
            catch (Exception ex)
            {
                _infoLabel.Text = $"Exception Occured: {ex.Message}";
                Console.WriteLine(ex.Message);
            }
        }

        private void SeasonsViewOnShown(object sender, EventArgs e)
        {
            Info("Loading Data");
            // var seasons = Client.GetSeasons().Result;
            // SeasonsView.Data = seasons;
            // SeasonsView.DataBind();
            // Info("Data Loaded");
        }
        
        private void LoadButtonClicked(object sender, EventArgs a)
        {
            var seasons = Client.GetSeasons().Result;
            SeasonsView.Data = seasons;
            SeasonsView.DataBind();
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