using Rwde;
using RWDE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RWDE_UPLOADS_FILES
{
    public partial class ServiceIDLists : Form
    {
        private readonly DBHelper dBHelper = new DBHelper();

        private DataTable dataTable;
        public ServiceIDLists()
        {
            this.WindowState = FormWindowState.Maximized;
            InitializeComponent();
            PopulateGrid();



            List<string> item = new List<string>();
            item.Add("HOPWA 23/24");
            item.Add("ELC 2 Expansion 23/24");
            item.Add("MCWP 2024 CA/OA");
            item.Add("CalOptima 24/25");
            item.Add("MAI 24/25 RW");
            item.Add("Other Agency 24/25");
            item.Add("Part A 24/25 RW");
            item.Add("Part A 24/25 EHE");
            foreach (var items in item)
            {
                this.ContractName.Items.Add(items);
            }
            List<string> Maphcc = new List<string>();
            Maphcc.Add("True");
            Maphcc.Add("False");

            foreach (var Map in Maphcc)
            {
                this.MaptoHcc.Items.Add(Map);
            }
            List<string> statusdropdown = new List<string>();
            statusdropdown.Add("Active");
            statusdropdown.Add("Inactive");
            statusdropdown.Add("Delete");


            foreach (var status in statusdropdown)
            {
                this.Status.Items.Add(status);

            }

            //DataGridViewButtonColumn editButtonColumn = new DataGridViewButtonColumn
            //{
            //    Name = Constants.EditColumnName,
            //    Text = Constants.EditColumnName,
            //    Width = 130,
            //    UseColumnTextForButtonValue = true
            //};
            //dataGridView.Columns.Add(editButtonColumn);

            //dataGridView.CellPainting += dataGridView_CelleditPainting;

            //DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn
            //{
            //    Name = Constants.DeleteColumnName,
            //    Text = Constants.DeleteColumnName,
            //    Width = 130,
            //    UseColumnTextForButtonValue = true
            //};

            //dataGridView.Columns.Add(deleteButtonColumn);
            //dataGridView.CellPainting += dataGridView_CellPainting;

            //dataGridView.ClearSelection();


        }
        private void InitializeComboBox()
        {
            ComboBox comboBox = new ComboBox
            {
                DrawMode = DrawMode.OwnerDrawFixed,
                Location = new Point(10, 10),
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            comboBox.Items.Add("True");
            comboBox.Items.Add("False");

            comboBox.DrawItem += new DrawItemEventHandler(comboBox_DrawItem);

            this.Controls.Add(comboBox);
        }

        private void comboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;

            // Draw the background of the item.
            e.DrawBackground();

            // Create the font with the desired size.
            Font font = new Font(e.Font.FontFamily, 16); // Change the size here

            // Get the text of the item.
            string text = comboBox.Items[e.Index].ToString();

            // Draw the text with the specified font.
            e.Graphics.DrawString(text, font, Brushes.Black, e.Bounds);

            // Draw the focus rectangle if the item has focus.
            e.DrawFocusRectangle();
        }

        private void PopulateGrid()
        {
            try
            {
                this.dataGridView.AutoGenerateColumns = false;
                dataTable = dBHelper.GetAllServiceLists();

                if (dataTable != null)
                {
                    this.dataGridView.DataSource = dataTable;
                }

                this.dataGridView.Columns.Clear();

                this.dataGridView.Columns.Add("ServiceCodeID", "ServiceCodeID");
                this.dataGridView.Columns["ServiceCodeID"].DataPropertyName = "ServiceCodeID";
                this.dataGridView.Columns["ServiceCodeID"].Width = 220;

                this.dataGridView.Columns.Add("Service", "Service");
                this.dataGridView.Columns["Service"].DataPropertyName = "Service";
                this.dataGridView.Columns["Service"].Width = 220;
                this.dataGridView.Columns.Add("HCC_ExportToAries", "HCC_ExportToAries");
                this.dataGridView.Columns["HCC_ExportToAries"].DataPropertyName = "HCC_ExportToAries";
                this.dataGridView.Columns["HCC_ExportToAries"].Width = 220;
                this.dataGridView.Columns.Add("HCC_ContractID", "HCC_ContractID");
                this.dataGridView.Columns["HCC_ContractID"].DataPropertyName = "HCC_ContractID";
                this.dataGridView.Columns["HCC_ContractID"].Width = 220;
                this.dataGridView.Columns.Add("HCC_PrimaryService", "HCC_PrimaryService");
                this.dataGridView.Columns["HCC_PrimaryService"].DataPropertyName = "HCC_PrimaryService";
                this.dataGridView.Columns["HCC_PrimaryService"].Width = 220;
                this.dataGridView.Columns.Add("HCC_SecondaryService", "HCC_SecondaryService");
                this.dataGridView.Columns["HCC_SecondaryService"].DataPropertyName = "HCC_SecondaryService";
                this.dataGridView.Columns["HCC_SecondaryService"].Width = 220;
                this.dataGridView.Columns.Add("HCC_Subservice", "HCC_Subservice");
                this.dataGridView.Columns["HCC_Subservice"].DataPropertyName = "HCC_Subservice";
                this.dataGridView.Columns["HCC_Subservice"].Width = 220;
                this.dataGridView.Columns.Add("UnitsOfMeasure", "UnitsOfMeasure");
                this.dataGridView.Columns["UnitsOfMeasure"].DataPropertyName = "UnitsOfMeasure";
                this.dataGridView.Columns["UnitsOfMeasure"].Width = 220;
                this.dataGridView.Columns.Add("UnitValue", "UnitValue");
                this.dataGridView.Columns["UnitValue"].DataPropertyName = "UnitValue";
                this.dataGridView.Columns["UnitValue"].Width = 220;
                this.dataGridView.Columns.Add("Status", "Status");
                this.dataGridView.Columns["Status"].DataPropertyName = "Status";
                this.dataGridView.Columns["Status"].Width = 220;






                //calling Service Lsit function
                DataTable result = this.dBHelper.GetAllServiceLists();
                //dataGridView.Columns.Clear();
                //if (result.Rows.Count >0)
                //{
                //    dataGridView.DataSource = result;
                //}                                             
                //dataGridView.Columns.Add("Service ID's","ServiceId");
                //dataGridView.Columns["Service ID's"].DataPropertyName = "ServicelistId";
                //dataGridView.Columns[""].Width = 300;
                //dataGridView.Columns.Add("", "");
                //dataGridView.Columns[""].DataPropertyName = "";
                //dataGridView.Columns[""].Width = 310;


                DataGridViewButtonColumn editButtonColumn = new DataGridViewButtonColumn
                {
                    Name = Constants.EditColumnName,
                    Text = Constants.EditColumnName,
                    Width = 130,
                    UseColumnTextForButtonValue = true
                };
                dataGridView.Columns.Add(editButtonColumn);

                dataGridView.CellPainting += dataGridView_CelleditPainting;

                DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn
                {
                    Name = Constants.DeleteColumnName,
                    Text = Constants.DeleteColumnName,
                    Width = 130,
                    UseColumnTextForButtonValue = true
                };

                dataGridView.Columns.Add(deleteButtonColumn);
                dataGridView.CellPainting += dataGridView_CellPainting;

                dataGridView.ClearSelection();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void dataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

            if (e.ColumnIndex == dataGridView.Columns[Constants.DeleteColumnName].Index && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.Border);

                var buttonRectangle = new Rectangle(e.CellBounds.X + 2, e.CellBounds.Y + 2, e.CellBounds.Width - 4, e.CellBounds.Height - 4);
                var buttonText = Constants.DeleteColumnName;
                Color buttonColor = Color.FromArgb(128, 128, 255);
                bool isEmptyRow = IsRowEmpty(e.RowIndex);

                if (isEmptyRow)
                {
                    buttonColor = Color.Silver;
                    buttonText = string.Empty;
                }
                using (GraphicsPath path = CreateRoundedRectanglePath(buttonRectangle, 5))
                {
                    using (Brush brush = new SolidBrush(buttonColor))
                    {
                        e.Graphics.FillPath(brush, path);
                    }

                    using (Pen pen = new Pen(Color.Black, 1))
                    {
                        e.Graphics.DrawPath(pen, path);
                    }

                    if (!string.IsNullOrEmpty(buttonText))
                    {
                        TextRenderer.DrawText(e.Graphics, buttonText, dataGridView.Font, buttonRectangle, Color.Black, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                    }
                }

                e.Handled = true;
            }
        }
        private void dataGridView_CelleditPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            dataGridView.Rows[e.RowIndex].Cells["Status"].ReadOnly = true;
            if (e.ColumnIndex == dataGridView.Columns[Constants.EditColumnName].Index && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.Border);

                var buttonRectangle = new Rectangle(e.CellBounds.X + 2, e.CellBounds.Y + 2, e.CellBounds.Width - 4, e.CellBounds.Height - 4);
                var buttonText = Constants.EditColumnName;
                Color buttonColor = Color.FromArgb(128, 128, 255);
                bool isEmptyRow = IsRowEmpty(e.RowIndex);

                if (isEmptyRow)
                {
                    buttonColor = Color.Silver;
                    buttonText = string.Empty;
                }
                using (GraphicsPath path = CreateRoundedRectanglePath(buttonRectangle, 5))
                {
                    using (Brush brush = new SolidBrush(buttonColor))
                    {
                        e.Graphics.FillPath(brush, path);
                    }

                    using (Pen pen = new Pen(Color.Black, 1))
                    {
                        e.Graphics.DrawPath(pen, path);
                    }

                    if (!string.IsNullOrEmpty(buttonText))
                    {
                        TextRenderer.DrawText(e.Graphics, buttonText, dataGridView.Font, buttonRectangle, Color.Black, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                    }
                }

                e.Handled = true;
            }
        }

        private GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);

            path.CloseFigure();
            return path;
        }

        private bool IsRowEmpty(int rowIndex)
        {
            foreach (DataGridViewCell cell in dataGridView.Rows[rowIndex].Cells)
            {
                if (cell.Value != null && !string.IsNullOrEmpty(cell.Value.ToString()))
                {
                    return false;
                }
            }
            return true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            dataGridView.ReadOnly = false;
            dataGridView.ReadOnly = false;

            int rowIndex = dataGridView.Rows.Add();
            DataGridViewRow newRow = dataGridView.Rows[rowIndex];
            //  dataGridView.Rows.Add();
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void lblHeading_Click(object sender, EventArgs e)
        {

        }

        private void ServiceIDLists_Load(object sender, EventArgs e)
        {

        }

    }
}

