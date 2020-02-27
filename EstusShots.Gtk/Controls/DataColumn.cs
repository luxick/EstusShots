namespace EstusShots.Gtk.Controls
{
    public class DataColumn
    {
        public string PropertyName { get; set; }
        
        public string Title { get; set; }

        public DataColumn()  { }

        public DataColumn(string propertyName)
        {
            PropertyName = propertyName;
        }
    }
}