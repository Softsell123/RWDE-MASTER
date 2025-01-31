using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
// ReSharper disable PossibleNullReferenceException

namespace RWDE
{
    public sealed partial class ServiceCodeSetup : Form
    {
        private readonly DbHelper dbHelper;
         
        public ServiceCodeSetup()// Your custom initialization code goes here
        {
            DataGridView gridView = new DataGridView
            {
                EditMode = DataGridViewEditMode.EditOnEnter
            };
            ControlBox = false;
            DoubleBuffered = true;
            MaximizedBounds = Screen.FromHandle(Handle).WorkingArea;
            WindowState = FormWindowState.Maximized;
            InitializeComponent();
            dbHelper = new DbHelper();

            //to initialize the gridview
            InitializeDataGridView();
            gridView.CellEndEdit += dataGridView_CellEndEdit;
            gridView.CellValidating += dataGridView_CellValidating;
            gridView.EditingControlShowing += DataGridView_EditingControlShowingStatus;

            // Setup the DataTable and DataGridView when the form loads
            PopulateGrid();
            RegisterEvents(this); //Assigning events to all Controls
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
                    if (control is Button || control is CheckBox || control is DateTimePicker)
                    {
                        control.MouseHover += Control_MouseHover;
                        control.MouseLeave += Control_MouseLeave;
                    }
                    // Check for child controls in containers
                    if (control.HasChildren)
                    {
                        RegisterEvents(control);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DataGridView_KeyDown(object sender, KeyEventArgs e)//Your custom code for handling key down events goes here
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    // Suppress the default delete action when the Constants.DeleteColumnName key is pressed
                    e.Handled = true; // Mark the event as handled
                    e.SuppressKeyPress = true; // Prevent the key press from being processed further               
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DataGridView_KeyDownDelete(object sender, KeyEventArgs e)//to control the delete button functionality
        {
            try
            {
                // Check if the Delete key is pressed
                if (e.KeyCode == Keys.Delete)
                {
                    // Prevent the default action
                    e.Handled = true;
                    e.SuppressKeyPress = true;

                    // Show confirmation dialog
                    var result = MessageBox.Show(Constants.AreyousureyouwanttodeleteSelectedrow, Constants.Confirmation, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // Allow the deletion
                        DeleteSelectedRow();
                    }
                    else
                    {
                        // Optionally, show a message to the user
                        MessageBox.Show(Constants.Deletioncancelled, Constants.Information, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DeleteSelectedRow()//delete if any rows are selected in the DataGridView
        {
            try
            {
                if (dataGridView.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow row in dataGridView.SelectedRows)
                    {
                        if (!row.IsNewRow)
                        {
                            dataGridView.Rows.Remove(row);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)//to edit the selected row
        {
            try
            {// Reset the row's background color to white when editing is completed
                if (e.RowIndex >= 0)
                {
                    dataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White; // Set back to default color
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e) // Event handler for when cell validation occurs
        {
            try
            {// Reset the row's background color to white if editing is canceled
                if (e.RowIndex >= 0)
                {
                    dataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White; // Set back to default color
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void InitializeDataGridView()//to initialize the gridview
        {
            try
            {
                // Your DataGridView initialization code
                dataGridView.AllowUserToAddRows = false; // Prevents user from adding new rows
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // Ensure that these event handlers are hooked up in the form's constructor or Load event
        private void PopulateGrid()//populate grid
        {
            DataTable dataTable;
            try
            {
                dataGridView.AutoGenerateColumns = false;

                //to retrieve the service list data
                dataTable = dbHelper.GetAllServiceLists();
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }
                if (dataTable != null)
                {
                    dataGridView.DataSource = dataTable;
                }

                dataGridView.Columns.Clear();
                dataGridView.Columns.Add(Constants.ServiceCodeId, Constants.ServiceCodeId);
                dataGridView.Columns[Constants.ServiceCodeId].DataPropertyName = Constants.ServiceCodeId;
                dataGridView.Columns[Constants.ServiceCodeId].Width = 220;

                dataGridView.Columns.Add(Constants.Service, Constants.Service);
                dataGridView.Columns[Constants.Service].DataPropertyName = Constants.Service;
                dataGridView.Columns[Constants.Service].Width = 220;
                DataGridViewComboBoxColumn maptohcc = new DataGridViewComboBoxColumn
                {
                    Name = Constants.HccExportToAries,
                    HeaderText = Constants.MapToHcc,
                    DataPropertyName = Constants.HccExportToAries, // Ensure this matches your DataTable column
                    Width = 130,
                    ValueType = typeof(string), // Ensure ValueType is set correctly
                    FlatStyle = FlatStyle.Flat
                };
                // Add items to the drop-down list
                maptohcc.Items.Add(XmlConstants.True);
                maptohcc.Items.Add(XmlConstants.False);
                dataGridView.Columns.Add(maptohcc);
                // Make sure 'dataGridView' is properly instantiated and initialized
                try
                {
                    // Fetch contracts from database
                    DataTable contracts = dbHelper.GetActiveContracts();
                    if (dbHelper.ErrorOccurred)
                    {
                        MessageBox.Show(Constants.ErrorOccurred);
                        return;
                    }

                    // Create the ComboBox column
                    DataGridViewComboBoxColumn contractColumn = new DataGridViewComboBoxColumn
                    {
                        Name = Constants.ContractId,
                        HeaderText = Constants.Contract,
                        DataPropertyName = Constants.HccContractId, // Ensure this matches your DataTable's column name
                        Width = 130,
                        ValueType = typeof(string),
                        FlatStyle = FlatStyle.Flat,
                        DisplayMember = Constants.ContractId, // Display member can still be Constants.ContractID if that's what you want to display
                        ValueMember = Constants.ContractId,
                        // Assign DataSource to the ComboBox column
                        DataSource = contracts
                    };

                    // Add the ComboBox column to the DataGridView
                    dataGridView.Columns.Add(contractColumn);

                    // Set EditMode to EditOnEnter for single click activation
                    dataGridView.EditMode = DataGridViewEditMode.EditOnEnter;

                    // Optionally set DefaultCellStyle for visual enhancement
                    dataGridView.DefaultCellStyle.ForeColor = Color.Black;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(Constants.ErrorMessagedynamic, ex.Message));
                }
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (row.Cells[Constants.HccExportToAries].Value == null ||
                        !maptohcc.Items.Contains(row.Cells[Constants.HccExportToAries].Value))
                    {
                        row.Cells[Constants.HccExportToAries].Value = "";
                    }
                }
                Load += new EventHandler(Form1_Load);//to load form

                // Handle the DataError event to catch and manage errors gracefully
                dataGridView.DataError += new DataGridViewDataErrorEventHandler(DataGridViewDataError);

                void DataGridViewDataError(object sender, DataGridViewDataErrorEventArgs e)
                {
                    // MessageBox.Show("Invalid value for ComboBox column.");
                    e.ThrowException = false; // Prevent the exception from being thrown
                }
                dataGridView.RowTemplate.Height = 40;
                dataGridView.Columns.Add(Constants.HccPrimaryService, Constants.PrimaryService);
                dataGridView.Columns[Constants.HccPrimaryService].DataPropertyName = Constants.HccPrimaryService;
                dataGridView.Columns[Constants.HccPrimaryService].Width = 220;
                dataGridView.Columns.Add(Constants.HccSecondaryService, Constants.SecondaryService);
                dataGridView.Columns[Constants.HccSecondaryService].DataPropertyName = Constants.HccSecondaryService;
                dataGridView.Columns[Constants.HccSecondaryService].Width = 220;
                dataGridView.Columns.Add(Constants.HccSubService, Constants.SubService);
                dataGridView.Columns[Constants.HccSubService].DataPropertyName = Constants.HccSubService;
                dataGridView.Columns[Constants.HccSubService].Width = 220;
                dataGridView.Columns.Add(Constants.UnitsOfMeasure, Constants.UnitsOfMeasuresp);
                dataGridView.Columns[Constants.UnitsOfMeasure].DataPropertyName = Constants.UnitsOfMeasure;
                dataGridView.Columns[Constants.UnitsOfMeasure].Width = 220;
                dataGridView.Columns.Add(Constants.UnitValue, Constants.Unit);
                dataGridView.Columns[Constants.UnitValue].DataPropertyName = Constants.UnitValue;
                dataGridView.Columns[Constants.UnitValue].Width = 220;

                //to retrieve the service list data
                DataTable result = dbHelper.GetAllServiceLists();
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }
                DataGridViewComboBoxColumn statusColumn = new DataGridViewComboBoxColumn
                {
                    Name = Constants.Status,
                    HeaderText = Constants.Status,
                    DataPropertyName = Constants.Status, // Assuming you have a Constants.Status column in your DataTable
                    Width = 130

                };
                // Adding items to the drop-down list
                statusColumn.Items.Add(Constants.Active);
                statusColumn.Items.Add(Constants.Inactive);
                statusColumn.Items.Add(Constants.Delete);
                Font smallerFont = new Font(dataGridView.Font.FontFamily, 8); // Adjust the size as needed

                // Apply the smaller font to the DefaultCellStyle
                statusColumn.DefaultCellStyle.Font = smallerFont;

                // Add the column to the DataGridView
                dataGridView.Columns.Add(statusColumn);

                dataGridView.EditingControlShowing += dataGridView_EditingControlShowing;//to edit particular row
                DataGridViewButtonColumn editButtonColumn = new DataGridViewButtonColumn
                {
                    Name = Constants.Edit,
                    Text = Constants.Edit,
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
                dataGridView.KeyDown += DataGridView_KeyDown;
                dataGridView.KeyDown += DataGridView_KeyDownDelete;
                dataGridView.Columns.Add(deleteButtonColumn);
                dataGridView.CellPainting += dataGridView_CelleditPainting;
                /*dataGridView.CellClick += dataGridView_Celledit;*/

                dataGridView.CellPainting += dataGridView_CellPainting;
                dataGridView.CellClick += dataGridView_CellClickdelete;//to apply styles to delete button
                //dataGridView.ClearSelection();

                dataGridView.Columns[Constants.Status].ReadOnly = true;

                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        string statusValue = row[Constants.Status].ToString();

                        switch (statusValue)
                        {
                            case Constants.ActiveContractstatus:
                                row[Constants.Status] = Constants.Active;
                                break;
                            case Constants.InactiveContractstatus:
                                row[Constants.Status] = Constants.Inactive;
                                break;
                            default:
                                row[Constants.Status] = Constants.Delete;
                                break;
                        }
                    }
                    // Set the DataTable as the DataSource of the DataGridView
                    dataGridView.DataSource = dataTable;
                    dataGridView.Columns[Constants.Status].ReadOnly = true;
                    dataGridView.DataSource = dataTable;

                    // Set the height for individual rows
                    foreach (DataGridViewRow row in dataGridView.Rows)
                    {
                        row.Height = 37; // Adjust the height as needed
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Form1_Load(object sender, EventArgs e)//call function to load form
        {
            try
            {
                //to populate data in ComboBox
                PopulateContractComboBoxColumn();
                //populate grid 
                PopulateGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void PopulateContractComboBoxColumn()//to populate data in contracts ComboBox
        {
            try
            {
                DataTable contracts = dbHelper.GetActiveContracts();//to get contracts list in service code
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }

                DataGridViewComboBoxColumn comboBoxColumn = (DataGridViewComboBoxColumn)dataGridView.Columns[Constants.ContractId];
                comboBoxColumn.DataSource = contracts;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)// Check if the clicked cell is in a valid row and is the "Edit" column
        {
            try
            {
                if (e.ColumnIndex == dataGridView.Columns[Constants.DeleteColumnName].Index && e.RowIndex >= 0)
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.Border);

                    var buttonRectangle = new Rectangle(e.CellBounds.X + 2, e.CellBounds.Y + 2, e.CellBounds.Width - 4, e.CellBounds.Height - 4);
                    var buttonText = Constants.DeleteColumnName;
                    Color buttonColor = Color.FromArgb(128, 128, 255);
                    bool isEmptyRow = IsRowEmpty(e.RowIndex);//to apply styles

                    if (isEmptyRow)
                    {
                        buttonColor = Color.Silver;
                        buttonText = string.Empty;
                    }
                    //design format of button
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGridView_CelleditPainting(object sender, DataGridViewCellPaintingEventArgs e)// Check if the clicked cell is in a valid row and is the "Edit" column
        {
            try
            {
                if (e.ColumnIndex == dataGridView.Columns[Constants.Edit].Index && e.RowIndex >= 0)
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.Border);

                    var buttonRectangle = new Rectangle(e.CellBounds.X + 2, e.CellBounds.Y + 2, e.CellBounds.Width - 4, e.CellBounds.Height - 4);
                    var buttonText = Constants.Edit;
                    Color buttonColor = Color.FromArgb(128, 128, 255);
                    bool isEmptyRow = IsRowEmpty(e.RowIndex);

                    if (isEmptyRow)
                    {
                        buttonColor = Color.Silver;
                        buttonText = string.Empty;
                    }
                    //design format of button
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int radius)//design format of button
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        private bool IsRowEmpty(int rowIndex)//to update boolean value
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
        private void btnClose_Click(object sender, EventArgs e)//to close the form
        {
            try
            {
                Close();
                Application.Restart();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)//to display the drop down
        {
            try
            {
                if (dataGridView.CurrentCell.ColumnIndex == dataGridView.Columns[Constants.Status].Index && e.Control is ComboBox comboBox)
                {
                    if (comboBox != null)
                    {
                        comboBox.Items.Clear();

                        // Get the current row's status value
                        string currentStatus = dataGridView.CurrentRow.Cells[Constants.Status].Value.ToString();

                        // Populate the ComboBox based on the current status
                        if (currentStatus == Constants.Active)
                        {
                            comboBox.Items.Add(Constants.Inactive);
                            comboBox.Items.Add(Constants.Delete);
                        }
                        else if (currentStatus == Constants.Inactive)
                        {
                            comboBox.Items.Add(Constants.Active);
                            comboBox.Items.Add(Constants.Delete);
                        }
                        else if (currentStatus == Constants.Delete)
                        {
                            comboBox.Items.Add(Constants.Active);
                            comboBox.Items.Add(Constants.Inactive);
                        }
                        else
                        {
                            comboBox.Items.Add(Constants.Active);
                            comboBox.Items.Add(Constants.Inactive);
                            comboBox.Items.Add(Constants.Delete);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)//to add a new row
        {
            try
            {
                DataTable dataTable = (DataTable)dataGridView.DataSource;
                // Allow editing for the entire DataGridView initially
                dataGridView.ReadOnly = false;
                dataGridView.Columns[Constants.Status].ReadOnly = false;

                // Display confirmation message
                var result = MessageBox.Show(Constants.AreyousureyouwanttoaddanewService, Constants.ServiceCodeSetup, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Create a new row in the DataTable
                    DataRow newRow = dataTable.NewRow();

                    // Insert the new row at index 1 in the DataTable
                    dataTable.Rows.InsertAt(newRow, 0);

                    // Refresh the DataGridView to ensure the new row is displayed
                    dataGridView.Refresh();

                    // Set the current cell to the first cell of the new row
                    dataGridView.CurrentCell = dataGridView.Rows[0].Cells[0];
                    dataGridView.Rows[0].Selected = true;
                    // Enter edit mode for the current cell
                    dataGridView.BeginEdit(true);
                }
                // Exit edit mode
                dataGridView.BeginEdit(false);

                // Iterate through each row in the DataGridView to set read-only based on status
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (row.Cells[Constants.Status].Value != null)
                    {
                        string statusValue = row.Cells[Constants.Status].Value.ToString();

                        // Assuming you have specific conditions for read-only rows based on status
                        if (statusValue == Constants.Active || statusValue == Constants.Inactive || statusValue == Constants.Delete)
                        {
                            row.ReadOnly = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)//to save into database table
        {
            try
            {
                if (dataGridView.SelectedRows.Count == 0)
                {
                    // No row selected
                    MessageBox.Show(Constants.Norowselectedtosave, Constants.SaveError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dataGridView.CurrentRow != null && !dataGridView.CurrentCell.OwningColumn.Name.Equals(Constants.Edit))
                {
                    if (dataGridView.SelectedRows.Count > 1)
                    {
                        MessageBox.Show(Constants.Selectonlyonerowatatime, Constants.ServiceCodeSetup, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    int rowIndex = dataGridView.CurrentRow.Index;
                    DataRow row = ((DataRowView)dataGridView.CurrentRow.DataBoundItem).Row;

                    string currentContractId = row[Constants.ServiceCodeId].ToString();

                    // Check for duplicate Contract ID in other rows
                    bool isDuplicate = false;
                    for (int i = 0; i < dataGridView.Rows.Count; i++)
                    {
                        if (i != rowIndex) // Skip the current row
                        {
                            var otherRow = dataGridView.Rows[i];
                            if (otherRow.Cells[Constants.ServiceCodeId].Value != null &&
                                otherRow.Cells[Constants.ServiceCodeId].Value.ToString() == currentContractId)
                            {
                                isDuplicate = true;
                                break;
                            }
                        }
                    }
                    if (isDuplicate)
                    {
                        MessageBox.Show(Constants.ServiceCodeIdiDalreadyexists, Constants.ServiceCodeSetupError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Exit the method if a duplicate is found
                    }

                    if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                    {
                        SaveServiceCode(row);
                    }
                    else
                    {
                        MessageBox.Show(ContractIdList.Nochangesdetectedintheselectedrow, Constants.ServiceCodeSetup, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show(ContractIdList.Nochangesdetectedintheselectedrow, Constants.ServiceCodeSetup, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)// Event handler for when a cell is clicked, specifically to handle edit button
        {
            try
            {
                
                if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView.Columns[Constants.Edit].Index)
                {
                    dataGridView.Rows[e.RowIndex].Cells[Constants.Status].ReadOnly = true;

                    if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                    {
                        // Get the Contract ID column index
                        int contractIdColumnIndex = dataGridView.Columns[Constants.ServiceCodeId].Index;

                        // Check if the current cell being edited is not in the Contract ID column
                        if (e.ColumnIndex != contractIdColumnIndex)
                        {
                            DataGridViewRow row = dataGridView.Rows[e.RowIndex];

                            // Check if the Contract ID cell is empty or null
                            if (row.Cells[contractIdColumnIndex].Value == null || string.IsNullOrWhiteSpace(row.Cells[contractIdColumnIndex].Value.ToString()))
                            {
                                MessageBox.Show(Constants.ServiceCodeIDhastobepresentbeforeediting, Constants.ServiceCodeSetupError, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            row.DefaultCellStyle.BackColor = Color.FromArgb(200, 230, 255);
                        }

                        // Get the ContractID from the current row
                        string serviceCodeId = dataGridView.Rows[e.RowIndex].Cells[Constants.ServiceCodeId].Value.ToString();

                        // Prompt user for confirmation before editing, showing the ContractID
                        var result = MessageBox.Show($@"{Constants.AreyousureyouwanttoeditServiceCodeId} {serviceCodeId}", Constants.ServiceCodeSetup, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                        if (result == DialogResult.No)
                        {
                            // Reset the row's background color to the default
                            dataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = dataGridView.DefaultCellStyle.BackColor; // Default color
                        }
                        dataGridView.ReadOnly = false;
                        if (result == DialogResult.Yes)
                        {
                            dataGridView.Rows[e.RowIndex].Cells[Constants.Status].ReadOnly = true;
                            // Make all rows read-only initially
                            foreach (DataGridViewRow row in dataGridView.Rows)
                            {
                                if (row != dataGridView.Rows[e.RowIndex])
                                {
                                    row.ReadOnly = true;
                                    row.DefaultCellStyle.BackColor = dataGridView.DefaultCellStyle.BackColor;
                                }
                            }
                        }

                        // Make all cells editable for the selected row except for the Constants.Status column
                        foreach (DataGridViewColumn column in dataGridView.Columns)
                        {
                            if (column.Name == Constants.Status)
                            {
                                dataGridView.Rows[e.RowIndex].Cells[column.Index].ReadOnly = true;
                            }
                        }

                        // Unsubscribe and re-subscribe to EditingControlShowing event to ensure single subscription
                        dataGridView.EditingControlShowing -= DataGridView_EditingControlShowingStatus;
                        dataGridView.EditingControlShowing += DataGridView_EditingControlShowingStatus;

                        // Call a method to handle the edit process (update UI, save changes, etc.)
                        HandleEditServiceCode(e.RowIndex);//to handle edit data
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView_CellClickdelete(object sender, DataGridViewCellEventArgs e) //to delete particular row
        {
            try
            {
                bool isHandlingClick=false;

                if (isHandlingClick)
                {
                    isHandlingClick = false;
                    return;
                } // Exit if already handling

                // Ensure the row index is valid and the clicked cell is in the Constants.DeleteColumnName column
                if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView.Columns[Constants.DeleteColumnName].Index)
                {
                    DataGridViewRow currentRow = dataGridView.Rows[e.RowIndex];

                    // Get the Contract ID and Status column indices
                    int contractIdColumnIndex = dataGridView.Columns[Constants.ServiceCodeId].Index;
                    int statusColumnIndex = dataGridView.Columns[Constants.Status].Index;

                    // Retrieve values
                    string serviceCodeId = currentRow.Cells[contractIdColumnIndex].Value?.ToString();
                    string status = currentRow.Cells[statusColumnIndex].Value?.ToString();

                    // Check if the Contract ID cell is empty or null
                    if (string.IsNullOrWhiteSpace(serviceCodeId))
                    {
                        MessageBox.Show(Constants.ServiceIDhastobepresentbeforedeleting, Constants.ServiceCodeSetupError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Check if the status is 'DELETE' and whether the message has already been shown
                    if (status != null && status.Equals(Constants.Delete, StringComparison.OrdinalIgnoreCase))
                    {
                        if (!isHandlingClick)
                        {
                            isHandlingClick = true;// Set the flag to prevent further messages
                            MessageBox.Show($@"{Constants.This} {serviceCodeId} {Constants.Hasalreadybeendeleted}", Constants.ServiceCodeSetup, MessageBoxButtons.OK, MessageBoxIcon.Question);
                        }
                        return;  // Exit method here to prevent further actions
                    }

                    // Confirm deletion
                    var result = MessageBox.Show(string.Format(Constants.DoYouWantToDeleteServiceCodeId, serviceCodeId), Constants.ServiceCodeSetup, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    isHandlingClick = true;
                    if (result == DialogResult.Yes)
                    {
                        // Ensure only the current row is editable
                        foreach (DataGridViewRow row in dataGridView.Rows)
                        {
                            row.ReadOnly = row.Index != e.RowIndex;
                        }
                        HandleDelete(e.RowIndex);//function to perform the delete data in database
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void HandleDelete(int rowIndex)//to delete particular row
        {
            try
            {
                var cell = dataGridView.Rows[rowIndex].Cells[Constants.DeleteColumnName];
                if (cell.Value != null && cell.Value.ToString() == Constants.DeleteColumnName)
                {
                    int serviceCodeId = Convert.ToInt32(dataGridView.Rows[rowIndex].Cells[Constants.ServiceCodeId].Value);

                    if (serviceCodeId > 0)
                    {
                        // Update the status to Constants.Delete
                        dataGridView.Rows[rowIndex].Cells[Constants.Status].Value = Constants.Delete;
                        dbHelper.ServiceCodeIdUpdateStatus(serviceCodeId, Constants.DeleteContractstatus); // this function updates the status in the database
                        if (dbHelper.ErrorOccurred)
                        {
                            MessageBox.Show(Constants.ErrorOccurred);
                            return;
                        }
                        MessageBox.Show($@"{Constants.DeletedContractId} {serviceCodeId} {Constants.Successfully}", Constants.ServiceCodeSetup, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        MessageBox.Show(ContractIdList.ContractIDortypeisinvalid, Constants.ServiceCodeSetupError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void HandleEditServiceCode(int rowIndex)//updating data
        {
            try
            {
                var cell = dataGridView.Rows[rowIndex].Cells[Constants.Edit];
                if (cell.Value != null && cell.Value.ToString() == Constants.Edit)
                {
                    try
                    {
                        int serviceCodeId = Convert.ToInt32(dataGridView.Rows[rowIndex].Cells[Constants.ServiceCodeId].Value);
                        string service = dataGridView.Rows[rowIndex].Cells[Constants.Service].Value.ToString();
                        string hccExportToAries = dataGridView.Rows[rowIndex].Cells[Constants.HccExportToAries].Value.ToString();
                        string hccContractId = dataGridView.Rows[rowIndex].Cells[Constants.ContractId].Value.ToString();
                        string hccPrimaryService = dataGridView.Rows[rowIndex].Cells[Constants.HccPrimaryService].Value.ToString();
                        string hccSecondaryService = dataGridView.Rows[rowIndex].Cells[Constants.HccSecondaryService].Value.ToString();
                        string hccSubservice = dataGridView.Rows[rowIndex].Cells[Constants.HccSubService].Value.ToString();
                        string unitsOfMeasure = dataGridView.Rows[rowIndex].Cells[Constants.UnitsOfMeasure].Value.ToString();
                        decimal unitValue = Convert.ToDecimal(dataGridView.Rows[rowIndex].Cells[Constants.UnitValue].Value);
                        string status = dataGridView.Rows[rowIndex].Cells[Constants.Status].Value.ToString();

                        status = (status == Constants.Active) ? Constants.ActiveContractstatus :
                            (status == Constants.Inactive) ? Constants.InactiveContractstatus :
                            (status == Constants.Delete) ? Constants.DeleteContractstatus :
                            status;

                        if (serviceCodeId > 0)
                        {
                            // Calling the helper function to update the service code
                            dbHelper.EditServiceCode(serviceCodeId, service, hccExportToAries, hccContractId, hccPrimaryService, hccSecondaryService, hccSubservice, unitsOfMeasure, unitValue, status);
                            if (dbHelper.ErrorOccurred)
                            {
                                MessageBox.Show(Constants.ErrorOccurred);
                                return;
                            }

                        }
                    }
                    catch (InvalidCastException)
                    {
                        return; // Skip this row
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SaveServiceCode(DataRow row)//save data in database
        {
            try
            {
                var statusString = row[Constants.Status].ToString();

                // Ensure the status is set correctly based on your conditions
                if (statusString == Constants.Active)
                {
                    statusString = Constants.ActiveContractstatus;
                }
                else if (statusString == Constants.Inactive)
                {
                    statusString = Constants.InactiveContractstatus;
                }
                else if (statusString == Constants.Delete)
                {
                    statusString = Constants.DeleteContractstatus;
                }

                decimal unitValue;

                // Check for empty or null HCC_ContractID before parsing
                if (string.IsNullOrEmpty(row[Constants.HccContractId].ToString()))
                {
                    MessageBox.Show(Constants.HccContractIDcannotbeempty, Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!int.TryParse(row[Constants.ServiceCodeId].ToString(), out int serviceCodeId))
                {
                    MessageBox.Show(Constants.InvalidServiceCodeId, Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrEmpty(row[Constants.UnitValue].ToString()))
                {
                    unitValue = 0; // or any default value you want to use
                }
                if (!int.TryParse(row[Constants.HccContractId].ToString(), out int hccContractId))
                {
                    MessageBox.Show(Constants.InvalidHccContractId, Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Check if UnitValue is null or empty and set a default value if necessary
                if (string.IsNullOrEmpty(row[Constants.UnitValue].ToString()))
                {
                    unitValue = 0; // or any default value you want to use
                }

                else if (!decimal.TryParse(row[Constants.UnitValue].ToString(), out unitValue))
                {
                    MessageBox.Show(Constants.InvalidUnitValue, Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Execute the stored procedure to insert or update row into table
                string operation = dbHelper.InsertOrUpdateServiceCode(
                    serviceCodeId,
                    row[Constants.Service].ToString(),
                    row[Constants.HccExportToAries].ToString(),
                    hccContractId,
                    row[Constants.HccPrimaryService].ToString(),
                    row[Constants.HccSecondaryService].ToString(),
                    row[Constants.HccSubService].ToString(),
                    row[Constants.UnitsOfMeasure].ToString(),
                    unitValue,
                    statusString
                );
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }
                dataGridView.ReadOnly = true;

                // Display appropriate message
                if (operation == Constants.Update)
                {
                    MessageBox.Show(Constants.Servicecodeupdatedsuccessfully, Constants.ServiceCodeSetup, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (operation == Constants.Insert)
                {
                    MessageBox.Show(Constants.Servicecodeinsertedsuccessfully, Constants.ServiceCodeSetup, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(Constants.Operationnotrecognized, Constants.ServiceCodeSetup, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DataGridView_EditingControlShowingStatus(object sender, DataGridViewEditingControlShowingEventArgs e)//to update status
        {
            try
            {
                if (dataGridView.CurrentCell.ColumnIndex == dataGridView.Columns[Constants.Status].Index && e.Control is ComboBox comboBox)
                {
                    // Remove existing event handlers to prevent multiple subscriptions
                    comboBox.SelectedIndexChanged -= ComboBox_SelectedIndexChanged;//to create dropdown
                    comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
                    comboBox.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)//to handle the dropdown of Status
        {
            try
            {
                if (sender is ComboBox comboBox)
                {
                    DataGridViewRow currentRow = dataGridView.CurrentRow;
                    if (currentRow == null || currentRow.Cells[Constants.ServiceCodeId].Value == null)
                        return;

                    string selectedStatus = comboBox.SelectedItem.ToString();
                    string serviceCodeId = currentRow.Cells[Constants.ServiceCodeId].Value.ToString();
                    string currentStatus = currentRow.Cells[Constants.Status].Value?.ToString();

                    if (currentStatus != selectedStatus)
                    {
                        string message = selectedStatus == Constants.ActiveSmall
                            ? string.Format(Constants.AreYouSureMakeActive, selectedStatus)
                            : string.Format(Constants.AreYouSureMakeInactive, selectedStatus);

                        var result = MessageBox.Show(message, string.Format(Constants.ConfirmStatus, selectedStatus), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            // Update the status cell value
                            currentRow.Cells[Constants.Status].Value = selectedStatus;
                        }
                        else
                        {
                            // Revert the ComboBox selection to the previous value
                            comboBox.SelectedItem = currentStatus;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}