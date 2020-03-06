using System;
using EstusShots.Shared.Models;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace EstusShots.Gtk.Dialogs
{
    public class DialogClosedEventArgs<T> : EventArgs where T: class, new()
    {
        public bool Ok { get; }
        public T Model { get; }
        
        public DialogClosedEventArgs(bool ok, T model)
        {
            Ok = ok;
            Model = model;
        }
    }
    public delegate void DialogClosedEventHandler<T>(object o, DialogClosedEventArgs<T> args) where T: class, new();
    
    public abstract class DialogBase<T> : Dialog where T: class, new()
    {
        protected readonly Builder Builder;
        protected T EditObject { get; set; }

        [UI] private readonly Button _saveButton = null;
        [UI] private readonly Button _cancelButton = null;
        
        public event DialogClosedEventHandler<T> DialogClosed;
        
        protected DialogBase(Window parent, Builder builder) : base(builder.GetObject("_editorDialog").Handle)
        {
            Builder = builder;
            Builder.Autoconnect(this);
            _saveButton.Clicked += OnSaveButtonClicked;
            _cancelButton.Clicked += (sender, args) =>
            {
                DialogClosed?.Invoke(this, new DialogClosedEventArgs<T>(false, new T()));
                Dispose();
            };
            TransientFor = parent;
        }

        public new void Show()
        {
            LoadFromModel();
            base.Show();
        }
        
        private void OnSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                LoadToModel();
                DialogClosed?.Invoke(this, new DialogClosedEventArgs<T>(true, EditObject)); 
                Dispose();
            }
            catch (Exception exception)
            {
                ErrorDialog.Show(new OperationResult(exception));
            }
        }

        protected abstract void LoadToModel();
        protected abstract void LoadFromModel();
    }
}