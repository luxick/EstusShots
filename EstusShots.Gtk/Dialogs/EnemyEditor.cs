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
        [UI] private readonly Box _seasonSelectionContainer = null;

        private LookupSelectionControl<Season> _seasonSelectionControl;

        public EnemyEditor(Window parent, Enemy enemy, List<Season> seasons) :
            base(parent, new Builder("EnemyEditor.glade"))
        {
            EditObject = enemy;
            _seasonSelectionControl = new LookupSelectionControl<Season>(new LookupSelectionControlOptions<Season>
            {
                KeyProperty = nameof(Season.SeasonId),
                DisplayProperty = nameof(Season.DisplayName),
                Columns = new List<DataColumn>
                {
                    new DataColumnText(nameof(Season.DisplayName)) {Title = "Seasons"}
                },
                SearchSpace = seasons
            });
            _seasonSelectionContainer.PackStart(_seasonSelectionControl, true, true, 5);
        }

        protected override void LoadToModel()
        {
        }

        protected override void LoadFromModel()
        {
        }
    }
}