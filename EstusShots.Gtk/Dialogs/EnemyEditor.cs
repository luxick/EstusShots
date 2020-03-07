using System.Collections.Generic;
using EstusShots.Gtk.Controls;
using EstusShots.Shared.Dto;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace EstusShots.Gtk.Dialogs
{
    public class EnemyEditor : DialogBase<Enemy>
    {
        [UI] private readonly Entry _nameEntry = null;
        [UI] private readonly CheckButton _isBossCheckButton = null;
        [UI] private readonly SearchEntry _searchSeasonEntry = null;
        [UI] private readonly TreeView _selectedSeasonsTreeView = null;

        private BindableListControl<Season> _selectedSeasonsControl;
        private readonly List<Season> _allSeasons;
        private readonly EntryCompletion _allSeasonsCompletion;

        public EnemyEditor(Window parent, Enemy enemy, List<Season> seasons) :
            base(parent, new Builder("EnemyEditor.glade"))
        {
            EditObject = enemy;
            _allSeasons = seasons;

            var columns = new List<DataColumn>
            {
                new DataColumnText(nameof(Season.DisplayName)) {Title = "Seasons"}
            };
            _selectedSeasonsControl =
                new BindableListControl<Season>(columns, nameof(Season.SeasonId), _selectedSeasonsTreeView);
            
            _allSeasonsCompletion = new EntryCompletion();
            
            _searchSeasonEntry.Completion = new EntryCompletion();
        }

        protected override void LoadToModel()
        {
            throw new System.NotImplementedException();
        }

        protected override void LoadFromModel()
        {
            throw new System.NotImplementedException();
        }
    }
}