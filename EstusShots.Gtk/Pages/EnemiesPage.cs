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
        [UI] private readonly Box _enemiesPage = null;
        [UI] private readonly TreeView _enemiesTreeView = null;
        [UI] private readonly Button _newEnemyButton = null;
        
        private BindableListControl<Enemy> _enemiesControl;

        private void InitEnemiesPage()
        {
            var columns = new List<DataColumn>
            {
                new DataColumnText(nameof(Enemy.Name)),
                new DataColumnBool(nameof(Enemy.Boss)){Title = "Is Boss?"},
                new DataColumnBool(nameof(Enemy.Defeated)){Title = "Defeated?"}
            };
            _enemiesControl = new BindableListControl<Enemy>(columns, nameof(Enemy.EnemyId), _enemiesTreeView);
            
            _newEnemyButton.Clicked += NewEnemyButtonOnClicked;
        }

        private void EnemiesPageNavigatedTo()
        {
            var _ = ReloadEnemies();
        }

        private void NewEnemyButtonOnClicked(object sender, EventArgs e)
        {
            var enemyEditor = new EnemyEditor(this, new Enemy(), SeasonsControl.Items);
            enemyEditor.DialogClosed += EnemyEditorOnDialogClosed;
            enemyEditor.Show();
        }

        private async void EnemyEditorOnDialogClosed(object o, DialogClosedEventArgs<Enemy> args)
        {
            if (!args.Ok) return;
            var res = await Client.Enemies.SaveEnemy(new SaveEnemyParameter(args.Model));
            if (!res.OperationResult.Success)
            {
                Info($"Unable to save: {res.OperationResult.ShortMessage}");
                ErrorDialog.Show(res.OperationResult);
                return;
            }
            
            await ReloadEnemies();
        }

        private async Task ReloadEnemies()
        {
            var res = await Task.Factory.StartNew(()
                => Client.Enemies.GetEnemies(new GetEnemiesParameter()).Result);
            if (!res.OperationResult.Success)
            {
                InfoLabel.Text = $"Refresh failed: {res.OperationResult.ShortMessage}";
                ErrorDialog.Show(res.OperationResult);
                return;
            }

            _enemiesControl.Items = res.Data.Enemies;
            _enemiesControl.DataBind();
            Info("Enemy list refreshed");
        }
    }
}