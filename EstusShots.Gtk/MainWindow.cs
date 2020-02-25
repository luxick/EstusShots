using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using EstusShots.Shared.Models;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace EstusShots.Gtk
{
    class MainWindow : Window
    {
        private const string ApiUrl = "http://localhost:5000/api/";
            
        [UI] private TreeView _seasonsView = null;
        [UI] private Button _loadButton = null;
        [UI] private Label _infoLabel = null;
        
        public MainWindow() : this(new Builder("MainWindow.glade")) { }

        private MainWindow(Builder builder) : base(builder.GetObject("MainWindow").Handle)
        {
            builder.Autoconnect(this);

            DeleteEvent += Window_DeleteEvent;
            _loadButton.Clicked += _loadButton_Clicked;
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }

        private void _loadButton_Clicked(object sender, EventArgs a)
        {
            var season = new Season()
            {
                Game = "Test Game",
                Start = DateTime.Now
            };

            var content = new StringContent(JsonSerializer.Serialize(season), Encoding.UTF8, "application/json");
            var client = new HttpClient();
            try
            {
                var response = client.PostAsync(ApiUrl + "seasons", content).Result;

                if (response.Headers.Location == null)
                {
                    _infoLabel.Text = $"Error while creating Season: {response.ReasonPhrase}";
                    return;
                }
                
                var data = client.GetAsync(response.Headers.Location).Result;
                var jsonData = data.Content.ReadAsStringAsync().Result;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                var s = JsonSerializer.Deserialize<Season>(jsonData, options);
                _infoLabel.Text = $"Created new Season: {s.Game} ({s.SeasonId})";
            }
            catch (Exception e)
            {
                _infoLabel.Text = $"Exception Occured: {e.Message}";
                Console.WriteLine(e.Message);
            }
        }
    }
}