using System;

namespace EstusShots.Gtk.Controls
{
    public class DataColumn
    {
        public DataColumn()
        {
        }

        public DataColumn(string propertyName)
        {
            PropertyName = propertyName;
        }

        /// <summary>
        ///     The name of the property in the data source, that should be show nin the view
        /// </summary>
        public string PropertyName { get; }

        /// <summary>
        ///     The column header.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Applies the given transformation on each item in the column.
        ///     This changes only the display of the value.
        /// </summary>
        public Func<object, string> Format { get; set; }
    }
}