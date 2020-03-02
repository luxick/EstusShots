using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EstusShots.Gtk.Controls;
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

        private BindableListControl<Player> PlayersControl;

        private void InitPlayersPage()
        {
            NewPlayerButton.Clicked += NewPlayerButtonOnClicked;

            var columns = new List<DataColumn>
            {
                new DataColumn(nameof(Player.Name)),
                new DataColumn(nameof(Player.Alias)),
                new DataColumn(nameof(Player.HexId)) {Title = "Hex ID"},
                new DataColumn(nameof(Player.Anonymous)) {Title = "Is Anonymous?", FixedWidth = 30}
            };
            PlayersControl = new BindableListControl<Player>(columns, nameof(Player.PlayerId), PlayersTreeView);
            PlayersControl.OnSelectionChanged += PlayersControlOnOnSelectionChanged;
            
            Task.Factory.StartNew(ReloadPlayers);
        }

        // Events 

        private void PlayersControlOnOnSelectionChanged(object o, SelectionChangedEventArgs args)
        {
            if (!(args.Selection is Player player)) return;
            var dialog = new PlayerEditor(this, player);
            dialog.OnDialogClosed += PlayerEditorClosed;
        }
        
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

            await ReloadPlayers();
        }

        // Private Methods

        private async Task ReloadPlayers()
        {
            var res = await Task.Factory.StartNew(()
                => Client.Players.GetPlayers(new GetPlayersParameter()).Result);
            if (!res.OperationResult.Success)
            {
                InfoLabel.Text = $"Refresh failed: {res.OperationResult.ShortMessage}";
                ErrorDialog.Show(res.OperationResult);
                return;
            }

            PlayersControl.Items = res.Data.Players;
            PlayersControl.DataBind();
            Info("Player list refreshed");
        }
    }
}