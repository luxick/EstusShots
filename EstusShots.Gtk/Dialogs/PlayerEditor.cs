using EstusShots.Shared.Dto;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace EstusShots.Gtk.Dialogs
{
    public class PlayerEditor : DialogBase<Player>
    {
        [UI] private readonly Entry _nameEntry = null;
        [UI] private readonly Entry _aliasEntry = null;
        [UI] private readonly Entry _hexIdEntry = null;
        [UI] private readonly CheckButton _anonCheckButton = null;
        
        public PlayerEditor(Window parent, Player player) : base(parent, new Builder("PlayerEditor.glade"))
        {
            EditObject = player;
        }

        protected override void LoadToModel()
        {
            EditObject.Name = _nameEntry.Text;
            EditObject.Alias = _aliasEntry.Text;
            EditObject.HexId = _hexIdEntry.Text;
            EditObject.Anonymous = _anonCheckButton.Active;
        }

        protected override void LoadFromModel()
        {
            _nameEntry.Text = EditObject.Name;
            _aliasEntry.Text = EditObject.Alias;
            _hexIdEntry.Text = EditObject.HexId;
            _anonCheckButton.Active = EditObject.Anonymous;
        }
    }
}