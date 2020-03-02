using System;
using EstusShots.Shared.Dto;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace EstusShots.Gtk.Dialogs
{
    public class DialogClosedEventArgs : EventArgs
    {
        public bool Ok { get; }
        public object Model { get; }
        
        public DialogClosedEventArgs(bool ok, object model)
        {
            Ok = ok;
            Model = model;
        }
    }
    public delegate void DialogClosedEventHandler(object o, DialogClosedEventArgs args);
    
    public class PlayerEditor
    {
        private Builder _builder;
        private Player _player;

        [UI] private readonly Dialog PlayerEditorDialog = null;
        [UI] private readonly Overlay PlayerEditorOverlay = null;
        [UI] private readonly Entry PlayerNameEntry = null;
        [UI] private readonly Entry PlayerAliasEntry = null;
        [UI] private readonly Entry PlayerHexIdEntry = null;
        [UI] private readonly CheckButton PlayerAnonymousCheckButton = null;
        [UI] private readonly Button CancelPlayerEditorButton = null;
        [UI] private readonly Button SavePlayerButton = null;

        public event DialogClosedEventHandler OnDialogClosed;
        
        public PlayerEditor(Window parent, Player player)
        {
            _player = player;
            
            _builder = new Builder("Dialogs.glade");
            _builder.Autoconnect(this);
            
            SavePlayerButton.Clicked += SavePlayerButtonOnClicked;
            CancelPlayerEditorButton.Clicked += (sender, args) =>
            {
                OnDialogClosed?.Invoke(this, new DialogClosedEventArgs(false, null));
                PlayerEditorDialog.Dispose();
            };

            PlayerEditorDialog.TransientFor = parent;
            PlayerEditorDialog.Show();

            ReadFromModel();
        }
        
        // Events

        private void SavePlayerButtonOnClicked(object sender, EventArgs e)
        {
            ReadToModel();
            OnDialogClosed?.Invoke(this, new DialogClosedEventArgs(true, _player));
            PlayerEditorDialog.Dispose();
        }
        
        // Private Methods

        private void ReadToModel()
        {
            _player.Name = PlayerNameEntry.Text;
            _player.Alias = PlayerAliasEntry.Text;
            _player.HexId = PlayerHexIdEntry.Text;
            _player.Anonymous = PlayerAnonymousCheckButton.Active;
        }

        private void ReadFromModel()
        {
            PlayerNameEntry.Text = _player.Name;
            PlayerAliasEntry.Text = _player.Alias;
            PlayerHexIdEntry.Text = _player.HexId;
            PlayerAnonymousCheckButton.Active = _player.Anonymous;
        }
    }
}