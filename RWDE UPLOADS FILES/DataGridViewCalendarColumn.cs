using System.Windows.Forms;

namespace RWDE
{
    internal class DataGridViewCalendarColumn
    {
        public string DataPropertyName { get; internal set; }
        public string HeaderText { get; internal set; }
        public string Name { get; internal set; }
        public int Width { get; internal set; }
        public DataGridViewCellStyle DefaultCellStyle { get; internal set; }
    }
}