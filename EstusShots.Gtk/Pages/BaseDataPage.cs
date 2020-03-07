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
        [UI] public readonly Box BaseDataPage = null;
        [UI] public readonly TreeView PlayersTreeView = null;
        [UI] public readonly Button NewPlayerButton = null;
        [UI] public readonly Button NewDrinkButton = null;
        [UI] public readonly TreeView DrinksTreeView = null;

        private BindableListControl<Player> _playersControl;
        private BindableListControl<Drink> _drinksControl;

        private void InitBaseDataPage()
        {
            NewPlayerButton.Clicked += NewPlayerButtonOnClicked;
            NewDrinkButton.Clicked += NewDrinkButtonOnClicked;

            var playerColumns = new List<DataColumn>
            {
                new DataColumnText(nameof(Player.Name)),
                new DataColumnText(nameof(Player.Alias)),
                new DataColumnBool(nameof(Player.Anonymous)) {Title = "Is Anonymous?", FixedWidth = 120},
                new DataColumnText(nameof(Player.HexId)) {Title = "Hex ID"},
            };
            _playersControl = new BindableListControl<Player>(playerColumns, nameof(Player.PlayerId), PlayersTreeView);
            _playersControl.ItemActivated += PlayersControlActivated;


            var drinkColumns = new List<DataColumn>
            {
                new DataColumnText(nameof(Drink.Name)),
                new DataColumnDouble(nameof(Drink.Vol)) {Title = "%"}
            };
            _drinksControl = new BindableListControl<Drink>(drinkColumns, nameof(Drink.DrinkId), DrinksTreeView);
            _drinksControl.ItemActivated += item =>
            {
                var drinkEditor = new DrinkEditor(this, item);
                drinkEditor.DialogClosed += DrinkEditorClosed;
                drinkEditor.Show();
            };

            // TODO Only Load when navigated to
            Task _;
            _ = ReloadPlayers();
            _ = ReloadDrinks();
        }



        // Events 

        private void PlayersControlActivated(Player player)
        {
            var dialog = new PlayerEditor(this, player);
            dialog.DialogClosed += PlayerEditorClosed;
            dialog.Show();
        }

        private void NewPlayerButtonOnClicked(object sender, EventArgs e)
        {
            var dialog = new PlayerEditor(this, new Player());
            dialog.DialogClosed += PlayerEditorClosed;
            dialog.Show();
        }

        private void NewDrinkButtonOnClicked(object sender, EventArgs e)
        {
            var dialog = new DrinkEditor(this, new Drink());
            dialog.DialogClosed += DrinkEditorClosed;
            dialog.Show();
        }

        private async void DrinkEditorClosed(object o, DialogClosedEventArgs<Drink> args)
        {
            if (!args.Ok) return;
            var res = await Task.Factory.StartNew(()
                => Client.Drinks.SaveDrink(new SaveDrinkParameter(args.Model)).Result);

            if (!res.OperationResult.Success)
            {
                Info($"Unable to save: {res.OperationResult.ShortMessage}");
                ErrorDialog.Show(res.OperationResult);
                return;
            }

            await ReloadDrinks();
        }

        private async void PlayerEditorClosed(object o, DialogClosedEventArgs<Player> args)
        {
            if (!args.Ok) return;
            var res = await Task.Factory.StartNew(()
                => Client.Players.SavePlayer(new SavePlayerParameter(args.Model)).Result);
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

            _playersControl.Items = res.Data.Players;
            _playersControl.DataBind();
            Info("Player list refreshed");
        }

        private async Task ReloadDrinks()
        {
            var res = await Task.Factory.StartNew(()
                => Client.Drinks.GetDrinks(new GetDrinksParameter()).Result);
            if (!res.OperationResult.Success)
            {
                InfoLabel.Text = $"Refresh failed: {res.OperationResult.ShortMessage}";
                ErrorDialog.Show(res.OperationResult);
                return;
            }

            _drinksControl.Items = res.Data.Drinks;
            _drinksControl.DataBind();
            Info("Drink list refreshed");
        }
    }
}