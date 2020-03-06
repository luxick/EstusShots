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
    
    // TODO remove non-generic version
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
    
    public abstract class DialogBase<T> where T: class, new()
    {
        protected T EditObject { get; set; }

        [UI] private readonly Dialog _editorDialog = null;
        [UI] private readonly Button _saveButton = null;
        [UI] private readonly Button _cancelButton = null;
        
        public event DialogClosedEventHandler<T> OnDialogClosed;
        
        protected DialogBase(Window parent, Builder builder)
        {
            builder.Autoconnect(this);

            _saveButton.Clicked += OnSaveButtonClicked;
            _cancelButton.Clicked += (sender, args) =>
            {
                OnDialogClosed?.Invoke(this, new DialogClosedEventArgs<T>(false, new T()));
                _editorDialog.Dispose();
            };
            _editorDialog.TransientFor = parent;
        }

        public void Show()
        {
            LoadFromModel();
            _editorDialog.Show();
        }
        
        private void OnSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                LoadToModel();
                OnDialogClosed?.Invoke(this, new DialogClosedEventArgs<T>(true, EditObject));
                _editorDialog.Dispose();
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