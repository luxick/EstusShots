using System;
using Gtk;

namespace EstusShots.Gtk.Controls
{
    public class DataColumn : TreeViewColumn
    {
        public DataColumn()
        {
            Resizable = true;
            Reorderable = true;
        }

        public DataColumn(string propertyName)
        {
            PropertyName = propertyName;
            Title = propertyName;
        }

        /// <summary>
        ///     The name of the property in the data source, that should be show nin the view
        /// </summary>
        public string PropertyName { get; }

        /// <summary>
        ///     Applies the given transformation on each item in the column.
        ///     This changes only the display of the value.
        /// </summary>
        public Func<object, string> Format { get; set; }
    }
}