using System;
using System.Collections.Generic;
using System.Linq;
using Gtk;

namespace EstusShots.Gtk.Controls
{
    public class SelectionChangedEventArgs : EventArgs
    {
        public object Selection { get; }

        public SelectionChangedEventArgs(object selection)
        {
            Selection = selection;
        }
    }
    
    public delegate void SelectionChangedEventHandler(object o, SelectionChangedEventArgs args);
    
    public class BindableListView<T>
    {
        public ListStore ListStore { get; internal set; }

        public TreeView TreeView { get; }

        public string KeyField { get; }

        public IEnumerable<T> Items { get; set; }

        public T SelectedItem { get; set; }
        
        public List<DataColumn> Columns { get; }

        public event SelectionChangedEventHandler OnSelectionChanged;

        /// <summary>
        /// Initialize a new BindableListView with an existing TreeView Widget
        /// </summary>
        /// <param name="columns">The columns of the grid</param>
        /// <param name="keyField">Unique key field in the item sources type</param>
        /// <param name="treeView">An instance of an existing TreeView Widget</param>
        public BindableListView(List<DataColumn> columns, string keyField, TreeView treeView = null)
        {
            TreeView = treeView ?? new TreeView();
            Columns = columns;
            KeyField = keyField;
            InitTreeViewColumns();
            InitListStore();
            TreeView.Model = ListStore;
            Items = new List<T>();
            
            TreeView.Selection.Changed += TreeView_SelectionChanged;
        }

        private void TreeView_SelectionChanged(object sender, EventArgs e)
        {
            if (!(sender is TreeSelection selection)) return;
            selection.GetSelected(out var model, out var iter);
            var key = model.GetValue(iter, 0);
            var item  = Items.FirstOrDefault(x =>
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
            OnSelectionChanged?.Invoke(this, new SelectionChangedEventArgs(SelectedItem));
        }

        public void DataBind()
        {
            ListStore.Clear();
            foreach (var item in Items)
            {
                var row = new List<object>();
                foreach (var column in Columns)
                {
                    var prop = item.GetType().GetProperty(column.PropertyName);
                    
                    if (prop == null)
                        throw new TypeLoadException(
                            $"Property '{column.PropertyName}' does not exist on Type '{item.GetType()}'");
                    
                    row.Add(prop.GetValue(item));
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
        }

        private void InitTreeViewColumns()
        {
            foreach (var dataColumn in Columns)
            {
                // Offset by one, because the first column in the data store is fixed to the key value of the row
                var index = Columns.IndexOf(dataColumn) + 1;
                var column = new TreeViewColumn(
                    dataColumn.Title,
                    new CellRendererText(),
                    "text", index);
                TreeView.AppendColumn(column);
            }
        }

        private void InitListStore()
        {
            var types = Columns
                .Select(x => typeof(T).GetProperty(x.PropertyName)?.PropertyType);

            // The first column in the data store is always the key field.
            var columns = new List<Type> {typeof(T).GetProperty(KeyField)?.PropertyType};
            columns.AddRange(types);
            
            ListStore = new ListStore(columns.ToArray());
        }
    }
}