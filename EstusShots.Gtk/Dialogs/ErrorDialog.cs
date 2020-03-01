using EstusShots.Shared.Models;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace EstusShots.Gtk.Dialogs
{
    public class ErrorHandler
    {
        [UI] private readonly Dialog ShowErrorDialog = null;
        [UI] private readonly Label ErrorShortTextLabel = null;
        [UI] private readonly Label ErrorDetailTextLabel = null;
        [UI] private readonly TextView StackTraceTextView = null;
        [UI] private readonly Button ErrorDialogCloseButton = null;

        public void ShowFor(Window window, OperationResult error)
        {
            ShowErrorDialog.TransientFor = window;
            ErrorDialogCloseButton.Clicked += (sender, args) => { ShowErrorDialog.Dispose();};
            ErrorShortTextLabel.Text = error.ShortMessage;
            ErrorDetailTextLabel.Visible = error.DetailedMessage != null;
            ErrorDetailTextLabel.Text = error.DetailedMessage;
            StackTraceTextView.Visible = error.StackTrace != null;
            var buff = new TextBuffer(new TextTagTable()) {Text = error.StackTrace};
            StackTraceTextView.Buffer = buff;
            ShowErrorDialog.Show();
        }
    }
    
    public static class ErrorDialog
    {
        public static Window MainWindow;
        public static void Show(OperationResult error)
        {
            var handler = new ErrorHandler();
            var builder = new Builder("MainWindow.glade");
            builder.Autoconnect(handler);
            handler.ShowFor(MainWindow, error);
        }
    }
}