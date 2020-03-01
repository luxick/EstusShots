using System;
using System.Threading.Tasks;
using EstusShots.Client;
using EstusShots.Gtk.Controls;
using EstusShots.Gtk.Dialogs;
using EstusShots.Shared.Dto;
using EstusShots.Shared.Models;
using Gdk;
using GLib;
using Gtk;
using Application = Gtk.Application;
using UI = Gtk.Builder.ObjectAttribute;
using Window = Gtk.Window;

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

            ErrorDialog.MainWindow = this;
            ExceptionManager.UnhandledException += ExceptionManagerOnUnhandledException;
            DeleteEvent += Window_DeleteEvent;
            
            Icon = Pixbuf.LoadFromResource("icon.png");
            
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

        private void ExceptionManagerOnUnhandledException(UnhandledExceptionArgs args)
        {
            Console.WriteLine(args.ExceptionObject);
            args.ExitApplication = false;
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