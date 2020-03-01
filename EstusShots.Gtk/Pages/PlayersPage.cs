using System;
using System.Threading.Tasks;
using EstusShots.Gtk.Dialogs;
using EstusShots.Shared.Dto;
using EstusShots.Shared.Models.Parameters;
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

        private async void PlayerEditorClosed(object o, DialogClosedEventArgs args)
        {
            if (!args.Ok || !(args.Model is Player player)) return;
            var res = await Task.Factory.StartNew(() 
                => Client.Players.SavePlayer(new SavePlayerParameter(player)).Result);
            if (!res.OperationResult.Success)
            {
                Info($"Unable to save: {res.OperationResult.ShortMessage}");
                ErrorDialog.Show(res.OperationResult);
                return;
            }

            // ReloadPlayers();
        }
        
        // Private Methods

        private async void ReloadPlayers()
        {
            var res = await Task.Factory.StartNew(()
                => Client.Players.GetPlayers(new GetPlayersParameter()).Result);
            if (!res.OperationResult.Success)
            {
                InfoLabel.Text = $"Refresh failed: {res.OperationResult.ShortMessage}";
                ErrorDialog.Show(res.OperationResult);
                return;
            }

            // TODO 
            // SeasonsControl.Items = res.Data.Seasons;
            // SeasonsControl.DataBind();
            // Info("Player list refreshed");
        }
    }
}