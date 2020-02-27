using System;

namespace EstusShots.Gtk.Controls
{
    internal class LoadingMode : IDisposable
    {
        private MainWindow Window { get; set; }

        public LoadingMode(MainWindow window)
        {
            Window = window;
            Window.LoadButton.Sensitive = false;
            Window.NewSeasonButton.Sensitive = false;
            Window.SeasonsView.Sensitive = false;
            Window.SeasonsOverlay.AddOverlay(Window.LoadingSpinner);
        }

        public void Dispose()
        {
            Window.LoadButton.Sensitive = true;
            Window.NewSeasonButton.Sensitive = true;
            Window.SeasonsView.Sensitive = true;
            Window.SeasonsOverlay.Remove(Window.LoadingSpinner);
        }
    }
}