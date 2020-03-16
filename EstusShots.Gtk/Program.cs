using System;
using System.Runtime.InteropServices;
using Gtk;

namespace EstusShots.Gtk
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.Init();

            // Load a custom theme only when running on windows
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var provider = new CssProvider();
                provider.LoadFromPath("Theme/gtk-3.20/gtk.css");
                StyleContext.AddProviderForScreen(Gdk.Screen.Default, provider, 800);
                
                // TODO ????
                //IconTheme.Default.PrependSearchPath("Theme/icons/48x48");
            }
            
            var app = new Application("EstusShots.Gtk", GLib.ApplicationFlags.None);
            app.Register(GLib.Cancellable.Current);

            var win = new MainWindow();
            app.AddWindow(win);

            win.Show();
            Application.Run();
        }
    }
}