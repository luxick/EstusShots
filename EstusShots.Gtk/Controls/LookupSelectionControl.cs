using System;
using System.Collections.Generic;
using System.Linq;
using Gdk;
using GLib;
using Gtk;
using Key = Gtk.Key;
using Menu = Gtk.Menu;
using MenuItem = Gtk.MenuItem;
using UI = Gtk.Builder.ObjectAttribute;

namespace EstusShots.Gtk.Controls
{
    public class LookupSelectionControlOptions<T>
    {
        public List<DataColumn> Columns { get; set; }
        public List<T> SearchSpace { get; set; }
        public string KeyProperty { get; set; }
        public string DisplayProperty { get; set; }
    }

    public class LookupSelectionControl<T> : Box
    {
        [UI] private readonly ComboBox _searchBox = null;
        [UI] private readonly TreeView _selectionTreeView = null;
        [UI] private readonly Button _addButton = null;
        private readonly BindableListControl<T> _selectedItemsListControl;

        public string KeyProperty { get; }
        public string DisplayProperty { get; }
        public List<T> AllItems { get; }
        public List<T> SelectedItems { get; set; }

        public LookupSelectionControl(LookupSelectionControlOptions<T> options) :
            this(options, new Builder("Controls.glade"))
        {
        }

        public LookupSelectionControl(LookupSelectionControlOptions<T> options, Builder builder) :
            base(builder.GetObject("_multiLookupControl").Handle)
        {
            builder.Autoconnect(this);

            KeyProperty = options.KeyProperty;
            DisplayProperty = options.DisplayProperty;
            AllItems = options.SearchSpace;
            SelectedItems = new List<T>();

            var completionStore = new ListStore(GType.String, GType.String);
            BindCompletionList(completionStore);

            _searchBox.Model = completionStore;
            _searchBox.IdColumn = 0;
            _searchBox.EntryTextColumn = 1;

            _searchBox.Entry.Completion = new EntryCompletion
            {
                Model = completionStore,
                InlineSelection = true,
                InlineCompletion = true,
                TextColumn = 1,
                PopupCompletion = true
            };

            // Init the selected items TreeView
            _selectedItemsListControl = new BindableListControl<T>(options.Columns, KeyProperty, _selectionTreeView)
            {
                Items = SelectedItems
            };
            _selectionTreeView.CanFocus = false;

            _selectedItemsListControl.RowContextMenuOpened += selection =>
            {
                var remove = new MenuItem {Label = "Remove"};
                remove.Activated += (sender, eventArgs) =>
                {
                    SelectedItems.Remove(_selectedItemsListControl.SelectedItem);
                    _selectedItemsListControl.DataBind();
                };
                var menu = new Menu {remove};
                menu.ShowAll();
                menu.Popup();
            };

            _addButton.Clicked += (sender, args) => AddSelectionToTreeView();
            _searchBox.Entry.Activated += (sender, args) => AddSelectionToTreeView();
        }

        private void AddSelectionToTreeView()
        {
            // FInd item preferably via its ID, try matching the display value as fallback 
            var item = Guid.TryParse(_searchBox.ActiveId, out var selectedSeason)
                ? AllItems.FirstOrDefault(x => x.GetPropertyValue(KeyProperty).Equals(selectedSeason))
                : AllItems.FirstOrDefault(y => y.GetPropertyValue(DisplayProperty).Equals(_searchBox.Entry.Text));
            if (item == null) return;

            if (!SelectedItems.Contains(item)) SelectedItems.Add(item);
            _selectedItemsListControl.DataBind();
            _searchBox.Entry.Text = "";
        }

        private void BindCompletionList(ListStore store)
        {
            foreach (var item in AllItems)
            {
                // value of key
                var keyValue = item.GetType().GetProperty(KeyProperty)?.GetValue(item).ToString();
                // value for display
                var displayValue = item.GetType().GetProperty(DisplayProperty)?.GetValue(item);
                store.AppendValues(keyValue, displayValue);
            }
        }
    }
}