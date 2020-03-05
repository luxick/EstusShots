using System;
using EstusShots.Shared.Dto;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace EstusShots.Gtk.Dialogs
{
    public class DrinkEditor
    {
        [UI] private readonly Dialog DrinkEditorDialog = null;
        [UI] private readonly Entry DrinkNameEntry = null;
        [UI] private readonly Adjustment DrinkVolAdjustment = null;
        [UI] private readonly Button SaveDrinkButton = null;
        [UI] private readonly Button CancelDrinkEditorButton = null;
        
        private readonly Drink _drink;
        
        public event DialogClosedEventHandler OnDialogClosed;

        public DrinkEditor(Window parent, Drink drink)
        {
            _drink = drink;
            
            var builder = new Builder("Dialogs.glade");
            builder.Autoconnect(this);
            
            SaveDrinkButton.Clicked += SaveDrinkButtonOnClicked;
            CancelDrinkEditorButton.Clicked += (sender, args) =>
            {
                OnDialogClosed?.Invoke(this, new DialogClosedEventArgs(false, null));
                DrinkEditorDialog.Dispose();
            };
            
            ReadFromModel();
            
            DrinkEditorDialog.TransientFor = parent;
            DrinkEditorDialog.Show();
        }

        // Events

        private void SaveDrinkButtonOnClicked(object sender, EventArgs e)
        {
            ReadToModel();
            OnDialogClosed?.Invoke(this, new DialogClosedEventArgs(true, _drink));
            DrinkEditorDialog.Dispose();
        }
        
        // Private Methods

        private void ReadToModel()
        {
            _drink.Name = DrinkNameEntry.Text;
            _drink.Vol = DrinkVolAdjustment.Value;
        }

        private void ReadFromModel()
        {
            DrinkNameEntry.Text = _drink.Name;
            DrinkVolAdjustment.Value = _drink.Vol;
        }
    }
}