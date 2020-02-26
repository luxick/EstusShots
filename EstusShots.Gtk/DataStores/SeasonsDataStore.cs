using System;
using System.Collections.Generic;
using System.Globalization;
using EstusShots.Shared.Models;
using GLib;
using Gtk;

namespace EstusShots.Gtk.DataStores
{
    public class SeasonsDataStore
    {
        public ListStore ListStore { get; private set; }

        public TreeView View { get; set; }

        public List<Season> Data { get; set; }

        public SeasonsDataStore(TreeView view)
        {
            ListStore = new ListStore(GType.Int, GType.String, GType.String);
            Data = new List<Season>();
            View = view;
            var columns = BuildColumns();
            columns.ForEach(column => View.AppendColumn(column));
            View.Model = ListStore;
        }
        
        public void DataBind()
        {
            ListStore.Clear();
            foreach (var datum in Data)
            {
                var row = new object[] {datum.Number, datum.Game, datum.Start.ToString(CultureInfo.InvariantCulture)};
                try
                {
                    ListStore.AppendValues(row);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
        private List<TreeViewColumn> BuildColumns()
        {
            var columns = new List<TreeViewColumn>
            {
                new TreeViewColumn("Number", new CellRendererText(), "text", 0),
                new TreeViewColumn("Game", new CellRendererText(), "text", 1),
                new TreeViewColumn("Start", new CellRendererText(), "text", 2)
            };
            return columns;
        }
    }
}