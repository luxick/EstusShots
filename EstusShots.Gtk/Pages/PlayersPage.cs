using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace EstusShots.Gtk
{
    internal partial class MainWindow
    {
        [UI] public readonly Box PlayersPage = null;
        [UI] public readonly Overlay PlayersOverlay = null;
        [UI] public readonly TreeView PlayersTreeView = null;
        [UI] public readonly Button SavePlayerButton = null;
        [UI] public readonly Button DeletePlayerButton = null;
        [UI] public readonly Box PlayerEditorContainer = null;
        

        private void InitPlayersPage()
        {
            
        }
    }
}