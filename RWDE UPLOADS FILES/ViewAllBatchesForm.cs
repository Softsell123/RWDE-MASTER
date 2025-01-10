using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RWDE
{
    [ComVisible(true)]
    public partial class ViewAllBatchesForm : Form
    {
        private readonly DbHelper dbHelper;
        public ViewAllBatchesForm()//INITIALIZATION OF FORM
        {
            InitializeComponent();
            dbHelper = new DbHelper();
            try
            {
                ControlBox = false;
                WindowState = FormWindowState.Maximized;
                dtpStartDate.Value = DateTime.Now.AddYears(-1);
                dtpStartDate.CustomFormat = Constants.DateFormatMMddyyyy;
                dtpEndDate.CustomFormat = Constants.DateFormatMMddyyyy;
                // Assuming you have another DateTimePicker for the End Date
                dtpEndDate.Value = DateTime.Now;
                // Populate the DataGridView with data from the Batch table
                PopulateDataGridViewLoad();
                dataGridView.DataBindingComplete += DataGridView_DataBindingComplete;//to adjust the column width
                dataGridView.CellClick += DataGridView_CellClick;
                //Handle the Batch Type Names
                GetAllBatchTypeNames();
                dtpStartDate.Value = DateTime.Today.AddDays(-1);
                dtpStartDate.CustomFormat = Constants.DateFormatMMddyyyy;
                dtpEndDate.CustomFormat = Constants.DateFormatMMddyyyy;
                RegisterEvents(this); //Assigning events to all Controls
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"{Constants.ErrorInitializingForm}{ex.Message}", Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Control_MouseHover(object sender, EventArgs e)//Changing Cursor as Hand on hover
        {
            try
            {
                Cursor = Cursors.Hand;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Control_MouseLeave(object sender, EventArgs e)//Changing back default Cursor on Leave
        {
            try
            {
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void RegisterEvents(Control parent)//Assigning events to all Controls
        {
            try
            {
                foreach (Control control in parent.Controls)
                {
                    if (control is Button || control is CheckBox || control is DateTimePicker || control is ComboBox)
                    {
                        control.MouseHover += Control_MouseHover;
                        control.MouseLeave += Control_MouseLeave;
                    }
                    // Check for child controls in containers
                    if (control.HasChildren)
                    {
                        //Assigning events to all child Controls
                        RegisterEvents(control);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // Populates the DataGridView with data from the Batch table while deleting
        private void PopulateDataGridView()
        {
            try
            {
                //Get all Values from Batch Table
                DataTable dataTable = dbHelper.GetAllBatches();

                dataGridView.AutoGenerateColumns = false;
                dataGridView.DataSource = dataTable;
                dataGridView.Columns.Clear();

                // Add columns to the DataGridView
                AddColumn(Constants.BatchId, Constants.BatchIdHeader, dataGridView);
                AddColumn(Constants.Description, Constants.DescriptionHeader, dataGridView);
                AddColumn(Constants.Type, Constants.BatchTypeHeader, dataGridView);

                // Format time columns to include seconds
                AddTimeColumn(Constants.UploadStartedAt, Constants.UploadStartedAtHeader, dataGridView);
                AddTimeColumn(Constants.UploadEndedAt, Constants.UploadEndedAtHeader, dataGridView);
                AddTimeColumn(Constants.ConversionStartedAt, Constants.ConversionStartedAtHeader, dataGridView);
                AddTimeColumn(Constants.ConversionEndedAt, Constants.ConversionEndedAtHeader, dataGridView);
                AddTimeColumn(Constants.GenerationStartedAt, Constants.GenerationStartedAtHeader, dataGridView);
                AddTimeColumn(Constants.GenerationEndedAt, Constants.GenerationEndedAtHeader, dataGridView);

                AddColumn(Constants.TotalRows, Constants.TotalRowsHeader, dataGridView);
                AddColumn(Constants.SuccessfulRows, Constants.SuccessfulRowsHeader, dataGridView);
                AddColumn(Constants.Status, Constants.StatusHeader, dataGridView);

                // Add a delete button column to the DataGridView
                DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn
                {
                    Name = Constants.DeleteColumnName,
                    Text = Constants.DeleteButtonText,
                    Width = 80,
                    UseColumnTextForButtonValue = true
                };
                dataGridView.Columns.Add(deleteButtonColumn);

                // Attach the CellPainting event handler
                dataGridView.CellPainting += dataGridView_CellPainting;//Apply styles to a button by adjusting its background color, border radius, and font size

                // Clear selection after populating DataGridView
                dataGridView.ClearSelection();

            }
            catch (Exception ex)
            {
                MessageBox.Show($@"{Constants.ErrorLoadingData}{ex.Message}", Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void PopulateDataGridViewLoad()// Populate the DataGridView with data from the Batch table
        {
            try
            {
                // Calling Batch table Values
                DataTable dataTable = dbHelper.GetAllBatchesLoad();

                dataGridView.AutoGenerateColumns = false;
                dataGridView.DataSource = dataTable;
                dataGridView.Columns.Clear();

                // Add columns to the DataGridView
                AddColumn(Constants.BatchId, Constants.BatchIdHeader, dataGridView);
                AddColumn(Constants.Description, Constants.DescriptionHeader, dataGridView);
                AddColumn(Constants.Type, Constants.BatchTypeHeader, dataGridView);

                // Format time columns to include seconds
                AddTimeColumn(Constants.UploadStartedAt, Constants.UploadStartedAtHeader, dataGridView);
                AddTimeColumn(Constants.UploadEndedAt, Constants.UploadEndedAtHeader, dataGridView);
                AddTimeColumn(Constants.ConversionStartedAt, Constants.ConversionStartedAtHeader, dataGridView);
                AddTimeColumn(Constants.ConversionEndedAt, Constants.ConversionEndedAtHeader, dataGridView);
                AddTimeColumn(Constants.GenerationStartedAt, Constants.GenerationStartedAtHeader, dataGridView);
                AddTimeColumn(Constants.GenerationEndedAt, Constants.GenerationEndedAtHeader, dataGridView);

                AddColumn(Constants.TotalRows, Constants.TotalRowsHeader, dataGridView);
                AddColumn(Constants.SuccessfulRows, Constants.SuccessfulRowsHeader, dataGridView);
                AddColumn(Constants.Status, Constants.StatusHeader, dataGridView);

                // Add a delete button column to the DataGridView
                DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn
                {
                    Name = Constants.DeleteColumnName,
                    Text = Constants.DeleteButtonText,
                    Width = 80,
                    UseColumnTextForButtonValue = true
                };
                dataGridView.Columns.Add(deleteButtonColumn);

                // Attach the CellPainting event handler
                dataGridView.CellPainting += dataGridView_CellPainting;//Apply styles to a button by adjusting its background color, border radius, and font size

                // Clear selection after populating DataGridView
                dataGridView.ClearSelection();

            }
            catch (Exception ex)
            {
                MessageBox.Show($@"{Constants.ErrorLoadingData}{ex.Message}", Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void PopulateDataGridViewLoaDhcc()// Populate the DataGridView with HCC data from the Batch table 
        {
            try
            {
                //Get all Values from Batch Table
                DataTable dataTable = dbHelper.GetAllBatcheshcc();

                dataGridView.AutoGenerateColumns = false;
                dataGridView.DataSource = dataTable;
                dataGridView.Columns.Clear();

                // Add columns to the DataGridView
                AddColumn(Constants.BatchId, Constants.BatchIdHeader, dataGridView);
                AddColumn(Constants.Description, Constants.DescriptionHeader, dataGridView);
                AddColumn(Constants.Type, Constants.BatchTypeHeader, dataGridView);

                // Format time columns to include seconds
                AddTimeColumn(Constants.UploadStartedAt, Constants.UploadStartedAtHeader, dataGridView);
                AddTimeColumn(Constants.UploadEndedAt, Constants.UploadEndedAtHeader, dataGridView);
                AddTimeColumn(Constants.ConversionStartedAt, Constants.ConversionStartedAtHeader, dataGridView);
                AddTimeColumn(Constants.ConversionEndedAt, Constants.ConversionEndedAtHeader, dataGridView);
                AddTimeColumn(Constants.GenerationStartedAt, Constants.GenerationStartedAtHeader, dataGridView);
                AddTimeColumn(Constants.GenerationEndedAt, Constants.GenerationEndedAtHeader, dataGridView);

                AddColumn(Constants.TotalRows, Constants.TotalRowsHeader, dataGridView);
                AddColumn(Constants.SuccessfulRows, Constants.SuccessfulRowsHeader, dataGridView);
                AddColumn(Constants.Status, Constants.StatusHeader, dataGridView);

                // Add a delete button column to the DataGridView
                DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn
                {
                    Name = Constants.DeleteColumnName,
                    Text = Constants.DeleteButtonText,
                    Width = 80,
                    UseColumnTextForButtonValue = true
                };
                dataGridView.Columns.Add(deleteButtonColumn);

                // Attach the CellPainting event handler
                dataGridView.CellPainting += dataGridView_CellPainting;//Apply styles to a button by adjusting its background color, border radius, and font size

                // Clear selection after populating DataGridView
                dataGridView.ClearSelection();

            }
            catch (Exception ex)
            {
                MessageBox.Show($@"{Constants.ErrorLoadingData}{ex.Message}", Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)//to create the Delete buttons
        {
            try
            {
                // Check if the column is the Delete button column and the row is valid
                if (e.ColumnIndex == dataGridView.Columns[Constants.DeleteColumnName].Index && e.RowIndex >= 0)
                {
                    bool isEmptyRow = IsRowEmpty(e.RowIndex);

                    // Always paint the cell's border
                    e.Paint(e.CellBounds, DataGridViewPaintParts.Border);

                    if (!isEmptyRow)
                    {
                        // Create the button's rectangle and draw the button
                        var buttonRectangle = new Rectangle(e.CellBounds.X + 2, e.CellBounds.Y + 2, e.CellBounds.Width - 4, e.CellBounds.Height - 4);
                        var buttonText = Constants.DeleteButtonText;
                        Color buttonColor = Color.FromArgb(128, 128, 255); // Default blue color

                        // Fill the button area with the color and rounded corners
                        using (GraphicsPath path = CreateRoundedRectanglePath(buttonRectangle, 5))
                        {
                            using (Brush brush = new SolidBrush(buttonColor))
                            {
                                e.Graphics.FillPath(brush, path);
                            }
                            // Draw black border with rounded corners
                            using (Pen pen = new Pen(Color.Black, 1))
                            {
                                e.Graphics.DrawPath(pen, path);
                            }

                            // Draw the button text if present
                            if (!string.IsNullOrEmpty(buttonText))
                            {
                                TextRenderer.DrawText(e.Graphics, buttonText, dataGridView.Font, buttonRectangle, Color.Black, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                            }
                        }
                    }
                    e.Handled = true; // Prevent default painting
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Method to create a rounded rectangle for button styling
        private GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int radius)
        {
            try
            {
                using (GraphicsPath path = new GraphicsPath())
                {
                    int diameter = radius * 2;

                    // Create rounded corners for the rectangle
                    path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90); // Top-left corner
                    path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90); // Top-right corner
                    path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90); // Bottom-right corner
                    path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90); // Bottom-left corner

                    path.CloseFigure();
                    return path;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        //Checking the Rows Values 
        private bool IsRowEmpty(int rowIndex)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return true;
            }
        }

        // Adds a new column to the DataGridView
        private void AddColumn(string name, string headerText, DataGridView dataGridViews)
        {
            try
            {
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn
                {
                    Name = name,
                    DataPropertyName = name,
                    HeaderText = headerText
                };
                dataGridViews.Columns.Add(column);
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"{Constants.ErrorAddingColumn}{headerText}: {ex.Message}", Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Adds a new column to the DataGridView with time formatting
        private void AddTimeColumn(string name, string headerText, DataGridView dataGridViews)
        {
            try
            {
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn
                {
                    Name = name,
                    DataPropertyName = name,
                    HeaderText = headerText,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = Constants.MMddyyyyHHmmss }
                };
                dataGridViews.Columns.Add(column);
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"{Constants.ErrorAddingColumn}{headerText}: {ex.Message}", Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Adjusts the column widths after data binding is complete
        private void DataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)//to adjust the column width
        {
            try
            {
                //to give styles for created button
                AdjustAllColumnWidths();
                // Sets the Delete button to read-only for rows with null BatchID
                SetDeleteButtonReadOnly();
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"{Constants.ErrorAdjustingColumnWidths}{ex.Message}", Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Adjusts the width of specific columns
        private void AdjustAllColumnWidths()//to give styles for created button
        {
            try
            {
                AdjustColumnWidth(Constants.BatchId, 120, dataGridView);
                AdjustColumnWidth(Constants.Description, 150, dataGridView);
                AdjustColumnWidth(Constants.Type, 160, dataGridView);
                AdjustColumnWidth(Constants.UploadStartedAt, 180, dataGridView);
                AdjustColumnWidth(Constants.UploadEndedAt, 180, dataGridView);
                AdjustColumnWidth(Constants.ConversionStartedAt, 180, dataGridView);
                AdjustColumnWidth(Constants.ConversionEndedAt, 180, dataGridView);
                AdjustColumnWidth(Constants.GenerationStartedAt, 180, dataGridView);
                AdjustColumnWidth(Constants.GenerationEndedAt, 180, dataGridView);
                AdjustColumnWidth(Constants.TotalRows, 140, dataGridView);
                AdjustColumnWidth(Constants.SuccessfulRows, 185, dataGridView);
                AdjustColumnWidth(Constants.Status, 260, dataGridView);
                AdjustColumnWidth(Constants.DeleteColumnName, 120, dataGridView);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SetDeleteButtonReadOnly() // Sets the Delete button to read-only for rows with null BatchID
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    var batchIdCell = row.Cells[Constants.BatchId];
                    if (batchIdCell.Value == null || batchIdCell.Value == DBNull.Value)
                    {
                        row.Cells[Constants.DeleteColumnName].Value = null;
                        row.Cells[Constants.DeleteColumnName].ReadOnly = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Adjusts the width of a specific column
        private void AdjustColumnWidth(string columnName, int width, DataGridView dataGridViews)
        {
            try
            {
                if (dataGridViews.Columns.Contains(columnName))
                {
                    dataGridViews.Columns[columnName].Width = width;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"{Constants.ErrorAdjustingColumnWidth}{columnName}: {ex.Message}", Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Handles the CellClick event to delete a batch
        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && dataGridView.Columns[e.ColumnIndex].Name == Constants.DeleteColumnName && e.RowIndex >= 0)
                {
                    //to handle the Delete button click
                    HandleDeleteButtonClick(e.RowIndex);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"{Constants.ErrorHandlingCellClick}{ex.Message}", Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Isolates the logic to handle the Delete button click
        private void HandleDeleteButtonClick(int rowIndex)
        {
            try
            {
                var cell = dataGridView.Rows[rowIndex].Cells[Constants.DeleteColumnName];
                if (cell.Value != null && cell.Value.ToString() == Constants.DeleteButtonText)
                {
                    string batchId = dataGridView.Rows[rowIndex].Cells[Constants.BatchId].Value?.ToString();
                    string type = dataGridView.Rows[rowIndex].Cells[Constants.Type].Value?.ToString();
                    string description = dataGridView.Rows[rowIndex].Cells[Constants.Description].Value?.ToString();
                    DialogResult result = MessageBox.Show($@"{Constants.ConfirmationMessage} {batchId}?{Constants.Type}:{type}{Constants.Description}:{description}"
                        , Constants.ConfirmationTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        if (!string.IsNullOrEmpty(batchId) && !string.IsNullOrEmpty(type))
                        {
                            // Calling Delete Function
                            dbHelper.DeleteBatch(batchId, type);
                            // Refresh the DataGridView after deletion
                            if (type == Constants.Hccdata)
                            {
                                // Populate the DataGridView with HCC data from the Batch table 
                                PopulateDataGridViewLoaDhcc();
                                return;
                            }
                            PopulateDataGridView();// Populates the DataGridView with data from the Batch table while deleting
                            AdjustAllColumnWidths();//to give styles for created button
                            SetDeleteButtonReadOnly();// Sets the Delete button to read-only for rows with null BatchID
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"{Constants.ErrorHandlingDeleteButtonClick}{ex.Message}", Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Handles the Close button click event
        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                WindowState = FormWindowState.Minimized;
                Application.Restart();
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"{Constants.ErrorClosingForm}{ex.Message}", Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Handle the Filter Data
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string batchType = cbBatchType.Text;
                DateTime fromDate = dtpStartDate.Value.Date;
                DateTime endDate = dtpEndDate.Value.Date;

                // Validate inputs
                if (string.IsNullOrEmpty(batchType))
                {
                    MessageBox.Show(Constants.EmptyvalueMessage, Constants.FilterTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (endDate < fromDate)
                {
                    MessageBox.Show(Constants.DateShouldBeGreaterThen, Constants.FilterTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //extraction of particular batch from data
                DataTable result = dbHelper.GetParticularDataFromBatchTable(batchType, fromDate, endDate);
                dataGridView.DataSource = result;

                if (result == null || result.Rows.Count == 0)
                {
                    MessageBox.Show(Constants.NoFilterDatas, Constants.FilterTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                // Log the exception or display an error message
                MessageBox.Show($@"{Constants.AnErrorOccurred}{ex.Message}", Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //clear All Data
        private void bnClear_Click(object sender, EventArgs e)
        {
            try
            {
                cbBatchType.Items.Clear();
                //Handle the Batch type Names
                GetAllBatchTypeNames();
                //populate the data into grid
                PopulateDataGridViewLoad();
                //Handle the Width For Grid Columns
                AdjustAllColumnWidths();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void GetAllBatchTypeNames() //Handle the Batch Type Names
        {
            try
            {
                //get all batches type
                List<string> batchTypes = dbHelper.GetAllBatchTypesview();
                cbBatchType.Items.Clear();  // Clear existing items
                foreach (string batchType in batchTypes)
                {
                    cbBatchType.Items.Add(batchType);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
