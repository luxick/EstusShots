using System;
using EstusShots.Shared.Dto;
using EstusShots.Shared.Extensions;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace EstusShots.Gtk.Dialogs
{
    public class SeasonEditor : DialogBase<Season>
    {
        [UI] private readonly Entry _numberEntry = null;
        [UI] private readonly Entry _gameEntry = null;
        [UI] private readonly Entry _startEntry = null;
        [UI] private readonly Entry _endEntry = null;
        [UI] private readonly TextView _descriptionTextView = null;

        public SeasonEditor(Window parent, Season season) : base(parent, new Builder("SeasonEditor.glade"))
        {
            EditObject = season;
            _startEntry.FocusOutEvent += (o, args) =>
            {
                if (!(o is Entry entry)) return;
                entry.Text = entry.Text.DateMask();
            };
            _endEntry.FocusOutEvent += (o, args) =>
            {
                if (!(o is Entry entry)) return;
                if (entry.Text.IsNullOrWhiteSpace()) return;
                entry.Text = entry.Text.DateMask();
            };
            _endEntry.IconPress += (o, args) =>
            {
                if (!(o is Entry entry)) return;
                entry.Text = "";
            };
        }
        
        protected override void LoadFromModel()
        {
            if (EditObject.SeasonId.IsEmpty()) return;
            _numberEntry.Text = EditObject.Number.ToString();
            _gameEntry.Text = EditObject.Game;
            _startEntry.Text = EditObject.Start.ToString("yyyy-MM-dd");
            _endEntry.Text = EditObject.End?.ToString("yyyy-MM-dd") ?? "";
            _descriptionTextView.Buffer =  new TextBuffer(new TextTagTable()) {Text = EditObject.Description};
        }

        protected override void LoadToModel()
        {
            EditObject.Number = _numberEntry.Text.ToInt32OrDefault();
            EditObject.Game = _gameEntry.Text;
            EditObject.Start = _startEntry.Text.ToDateTime();
            EditObject.End = _endEntry.Text.IsNullOrWhiteSpace() ? null :_endEntry.Text.ToNullableDateTime();
            EditObject.Description = _descriptionTextView.Buffer.Text;
        }
    }
}