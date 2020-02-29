using EstusShots.Client;
using EstusShots.Gtk.Controls;
using EstusShots.Shared.Dto;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace EstusShots.Gtk
{
    internal partial class MainWindow : Window
    {
        private const string ApiUrl = "http://localhost:5000/api/";
        private const string ApplicationName = "Estus Shots";
        private const string Version = "0.1";
        private const int EpisodesPageNumber = 1;

        [UI] public readonly Label InfoLabel = null;
        [UI] public readonly Box LoadingSpinner = null;
        [UI] public readonly Notebook Navigation = null;

        private EstusShotsClient Client { get; }
        private BindableListControl<Episode> EpisodesControl { get; set; }

        public MainWindow() : this(new Builder("MainWindow.glade"))
        {
        }

        private MainWindow(Builder builder) : base(builder.GetObject("MainWindow").Handle)
        {
            builder.Autoconnect(this);
            Client = new EstusShotsClient(ApiUrl);

            DeleteEvent += Window_DeleteEvent;

            // Call initialization code of each page
            InitSeasonsPage();
            InitEpisodesPage();
            InitPlayersPage();

            CreateEpisodesControl();

            // The episodes page is not shown, as long as no season is selected 
            EpisodesPage.Hide();

            Info("Application Started");
            UpdateTitle();
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }

        private void Info(string message)
        {
            InfoLabel.Text = message;
        }

        private void UpdateTitle()
        {
            Title = SeasonsControl.SelectedItem == null
                ? $"{ApplicationName} v{Version}"
                : $"{SeasonsControl.SelectedItem.DisplayName} - {ApplicationName} v{Version}";
        }
    }
}