using System;
using Gtk;

namespace EstusShots.Gtk.Controls
{
    public abstract class DataColumn : TreeViewColumn
    {
        protected DataColumn(string propertyName)
        {
            PropertyName = propertyName;
            Title = propertyName;

            Resizable = true;
            Reorderable = true;
        }

        public abstract string ValueAttribute { get; }

        /// <summary>
        ///     The name of the property in the data source, that should be show nin the view
        /// </summary>
        public string PropertyName { get; }

        /// <summary>
        ///     Applies the given transformation on each item in the column.
        ///     This changes only the display of the value.
        /// </summary>
        public abstract Func<object, string> DisplayConverter { get; set; }

        /// <summary>
        /// Cell renderer for rows in the column
        /// </summary>
        public abstract CellRenderer Cell { get; set; }
    }

    public class DataColumnText : DataColumn
    {
        public DataColumnText(string propertyName) : base(propertyName)
        {
            ValueAttribute = "text";
            Cell = new CellRendererText();
            PackStart(Cell, true);
        }


        public override string ValueAttribute { get; }

        public override Func<object, string> DisplayConverter { get; set; }

        /// <summary>
        /// Cell renderer for rows in the column
        /// </summary>
        public sealed override CellRenderer Cell { get; set; }
    }

    public class DataColumnBool : DataColumn
    {
        public DataColumnBool(string propertyName) : base(propertyName)
        {
            ValueAttribute = "active";
            Cell = new CellRendererToggle();
            PackStart(Cell, true);
        }

        public override string ValueAttribute { get; }
        public override Func<object, string> DisplayConverter { get; set; }
        public sealed override CellRenderer Cell { get; set; }
    }

    public class DataColumnDouble : DataColumn
    {
        public DataColumnDouble(string propertyName) : base(propertyName)
        {
            ValueAttribute = "text";
            Cell = new CellRendererSpin();
            PackStart(Cell, true);
        }

        public int Digits
        {
            set => SetAttributes(Cell, "digits", value);
        }

        public override string ValueAttribute { get; }
        public override Func<object, string> DisplayConverter { get; set; }
        public sealed override CellRenderer Cell { get; set; }
    }
}