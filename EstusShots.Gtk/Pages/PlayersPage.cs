using System;
using EstusShots.Gtk.Dialogs;
using EstusShots.Shared.Dto;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace EstusShots.Gtk
{
    internal partial class MainWindow
    {
        [UI] public readonly Box PlayersPage = null;
        [UI] public readonly Overlay PlayersOverlay = null;
        [UI] public readonly TreeView PlayersTreeView = null;
        [UI] public readonly Button NewPlayerButton = null;
        [UI] public readonly Box PlayerEditorContainer = null;
        

        private void InitPlayersPage()
        {
            NewPlayerButton.Clicked += NewPlayerButtonOnClicked;
        }

        // Events 
        
        private void NewPlayerButtonOnClicked(object sender, EventArgs e)
        {
            var dialog = new PlayerEditor(this, new Player());
            dialog.OnDialogClosed += PlayerEditorClosed;
        }

        private void PlayerEditorClosed(object o, DialogClosedEventArgs args)
        {
            if (!args.Ok || !(args.Model is Player player)) return;
            // TODO
            // Client.Players.SavePlayer();
        }
    }
}