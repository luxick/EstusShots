using EstusShots.Shared.Dto;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace EstusShots.Gtk.Dialogs
{
    public class DrinkEditor : DialogBase<Drink>
    {
        [UI] private readonly Entry _nameEntry = null;
        [UI] private readonly Adjustment _volAdjustment = null;

        public DrinkEditor(Window parent, Drink drink) : base(parent, new Builder("DrinkEditor.glade"))
        {
            EditObject = drink;
            var builder = new Builder("Dialogs.glade");
            builder.Autoconnect(this);
        }
        
        protected override void LoadToModel()
        {
            EditObject.Name = _nameEntry.Text;
            EditObject.Vol = _volAdjustment.Value;
        }

        protected override void LoadFromModel()
        {
            _nameEntry.Text = EditObject.Name;
            _volAdjustment.Value = EditObject.Vol;
        }
    }
}