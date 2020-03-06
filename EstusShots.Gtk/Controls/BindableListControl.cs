using System;
using System.Collections.Generic;
using System.Linq;
using GLib;
using Gtk;
using DateTime = GLib.DateTime;

namespace EstusShots.Gtk.Controls
{
    public class SelectionChangedEventArgs<T> : EventArgs
    {
        public SelectionChangedEventArgs(T selection)
        {
            Selection = selection;
        }

        public T Selection { get; }
    }

    public delegate void SelectionChangedEventHandler<T>(object o, SelectionChangedEventArgs<T> args);

    public delegate void ItemActivatedEventHandler<T>(T item);

    public class BindableListControl<T>
    {
        /// <summary> The GTK ListStore that is managed by this <see cref="BindableListControl{T}" />. </summary>
        public ListStore ListStore { get; internal set; }

        /// <summary> The GTK TreeView control that is managed by this <see cref="BindableListControl{T}" />. </summary>
        public TreeView TreeView { get; }

        /// <summary> Property of the element type that is used as a unique identifier for accessing elements. </summary>
        public string KeyField { get; }

        /// <summary> The collection of all elements, that should be shown in the list view. </summary>
        public List<T> Items { get; set; }

        /// <summary> The currently selected item in the view. </summary>
        public T SelectedItem { get; set; }

        /// <summary> All columns that are displayed in the list. </summary>
        public List<DataColumn> Columns { get; }

        /// <summary>
        ///     Event will be invoked when the selected item in the <see cref="TreeView" /> has changed.
        /// </summary>
        public event SelectionChangedEventHandler<T> SelectionChanged;

        /// <summary>
        /// Will be invoked when a row in the view has ben acitvated (e.g. double clicked)
        /// </summary>
        public event ItemActivatedEventHandler<T> ItemActivated;

        /// <summary>
        ///     Initialize a new BindableListView with an existing TreeView Widget
        /// </summary>
        /// <param name="columns">The columns of the grid</param>
        /// <param name="keyField">Unique key field in the item sources type</param>
        /// <param name="treeView">An instance of an existing TreeView Widget</param>
        public BindableListControl(List<DataColumn> columns, string keyField, TreeView treeView = null)
        {
            TreeView = treeView ?? new TreeView();
            Columns = columns;
            KeyField = keyField;
            InitTreeViewColumns();
            InitListStore();
            TreeView.Model = ListStore;
            Items = new List<T>();

            TreeView.RowActivated += TreeViewOnRowActivated;
            TreeView.Selection.Changed += TreeViewSelectionOnChanged;
        }

        /// <summary>
        ///     Set elements from the <see cref="Items" /> property in the <see cref="ListStore" />.
        /// </summary>
        /// <exception cref="TypeLoadException"></exception>
        public void DataBind()
        {
            ListStore.Clear();
            Items.ForEach(BindItem);
        }

        private void BindItem(T item)
        {
            var row = new List<object>();
            foreach (var column in Columns)
            {
                var prop = item.GetType().GetProperty(column.PropertyName);
                if (prop == null)
                    throw new TypeLoadException(
                        $"Property '{column.PropertyName}' does not exist on Type '{item.GetType()}'");
                var val = prop.GetValue(item);
                if (column.DisplayConverter != null) val = column.DisplayConverter(val);
                row.Add(val);
            }

            // The key value must be the first value in the row
            var key = item.GetType().GetProperty(KeyField)?.GetValue(item);
            row.Insert(0, key);
            try
            {
                ListStore.AppendValues(row.ToArray());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void TreeViewOnRowActivated(object o, RowActivatedArgs args)
        {
            if (!(o is TreeView tree)) return;
            var selection = tree.Selection;
            selection.GetSelected(out var model, out var iter);
            var key = model.GetValue(iter, 0);
            var item = Items.FirstOrDefault(x =>
            {
                var prop = x.GetType().GetProperty(KeyField);
                var value = prop?.GetValue(x);
                return value != null && value.Equals(key);
            });

            if (item == null)
            {
                Console.WriteLine($"No item for key '{key}' found in data store");
                return;
            }

            SelectedItem = item;
            ItemActivated?.Invoke(SelectedItem);
        }
        
        private void TreeViewSelectionOnChanged(object sender, EventArgs e)
        {
            if (!(sender is TreeSelection selection)) return;
            selection.GetSelected(out var model, out var iter);
            var key = model.GetValue(iter, 0);
            var item = Items.FirstOrDefault(x =>
            {
                var prop = x.GetType().GetProperty(KeyField);
                var value = prop?.GetValue(x);
                return value != null && value.Equals(key);
            });

            if (item == null)
            {
                Console.WriteLine($"No item for key '{key}' found in data store");
                return;
            }

            SelectedItem = item;
            SelectionChanged?.Invoke(selection, new SelectionChangedEventArgs<T>(SelectedItem));
        }

        private void InitTreeViewColumns()
        {
            foreach (var dataColumn in Columns)
            {
                // Offset by one, because the first column in the data store is fixed to the key value of the row
                var valueIndex = Columns.IndexOf(dataColumn) + 1;
                dataColumn.AddAttribute(dataColumn.Cell, dataColumn.ValueAttribute, valueIndex);
                dataColumn.SortColumnId = valueIndex;
                TreeView.AppendColumn(dataColumn);
            }
        }

        private void InitListStore()
        {
            var types = Columns
                .Select(x =>
                {
                    var propType = typeof(T).GetProperty(x.PropertyName)?.PropertyType;
                    var gType = (GType) propType;
                    if (gType.ToString() == "GtkSharpValue") gType = MapType(propType);
                    return gType;
                });
            var data = new DateTime();
            // The first column in the data store is always the key field.
            var columns = new List<GType> {(GType) typeof(T).GetProperty(KeyField)?.PropertyType};
            columns.AddRange(types);
            ListStore = new ListStore(columns.ToArray());
        }

        private static GType MapType(Type type)
        {
            return type.Name switch
            {
                _ => GType.String
            };
        }
    }
}