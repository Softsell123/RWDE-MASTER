using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RWDE
{
    public partial class ServiceCodeSetup : Form
    {
        private readonly DBHelper dbHelper;

        private DataTable dataTable;
        private readonly string connectionString;
        public ServiceCodeSetup()// Your custom initialization code goes here
        {
           
            DataGridView gridView = new DataGridView();
         
            gridView.EditMode = DataGridViewEditMode.EditOnEnter;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;
            InitializeComponent();
            dbHelper = new DBHelper();
            connectionString = dbHelper.GetConnectionString();
            InitializeDataGridView();
            gridView.CellEndEdit += dataGridView_CellEndEdit;
            gridView.CellValidating += dataGridView_CellValidating;
            gridView.EditingControlShowing += DataGridView_EditingControlShowingStatus;
            PopulateGrid();
           // Setup the DataTable and DataGridView when the form loads
            SetupDataGridView();
            RegisterEvents(this);
        }
        private void Control_MouseHover(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }
        private void Control_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }
        private void RegisterEvents(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is System.Windows.Forms.Button || control is CheckBox || control is DateTimePicker)
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
        private void DataGridView_KeyDown(object sender, KeyEventArgs e)//Your custom code for handling key down events goes here
        {
            if (e.KeyCode == Keys.Delete)
            {
                // Suppress the default delete action when the "Delete" key is pressed
                e.Handled = true; // Mark the event as handled
                e.SuppressKeyPress = true; // Prevent the key press from being processed further               
            }
        }
        private void DataGridView_CellClickDelete(object sender, DataGridViewCellEventArgs e)//Your custom code for handling cell click and row deletion goes here
        {
            // Disable the event handler temporarily to prevent multiple triggers
            dataGridView.CellClick -= DataGridView_CellClickDelete;

            try
            {
                // Check if the clicked cell is valid and it's in the "Delete" column
                if (e.RowIndex >= 0 && e.RowIndex < dataGridView.Rows.Count &&
                    e.ColumnIndex >= 0 && dataGridView.Columns[e.ColumnIndex].Name == Constants.DeleteColumnName)
                {
                    DataGridViewRow selectedRow = dataGridView.Rows[e.RowIndex];

                    // Ensure the "Status" column exists before accessing it
                    if (dataGridView.Columns.Contains("Status"))
                    {
                        string currentStatus = selectedRow.Cells["Status"].Value?.ToString();

                        // Ask for user confirmation before marking the status as "Deleted"
                        var result = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            // Mark the status as "Deleted"
                            selectedRow.Cells["Status"].Value = ContractIDList.DELETE;
                            MessageBox.Show(Constants.Therecordhasbeenmarkedasdeleted, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        // Clear the selection after user's decision
                        dataGridView.ClearSelection();
                    }
                    else
                    {
                        MessageBox.Show(Constants.TheStatuscolumnismissing,Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            finally
            {
                // Re-enable the event handler after processing
               
            }
        }
        private void dataGridView_KeyDown(object sender, KeyEventArgs e)//to control the delete button functionality
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
                    var result = MessageBox.Show($"{Constants.AreyousureyouwanttodeleteSelectedrow}", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // Allow the deletion
                        DeleteSelectedRow();
                    }
                    else
                    {
                        // Optionally, show a message to the user
                        MessageBox.Show($"{Constants.Deletioncancelled}", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            try{// Reset the row's background color to white when editing is completed
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
           try{// Reset the row's background color to white if editing is canceled
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
        private void InitializeDataGridView()
        {
            try { 
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
            try
            {
                this.dataGridView.AutoGenerateColumns = false;
                dataTable = dbHelper.GetAllServiceLists();


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
                DataGridViewComboBoxColumn Maptohcc = new DataGridViewComboBoxColumn
                {
                    Name = "HCC_ExportToAries",
                    HeaderText = "Map to HCC",
                    DataPropertyName = "HCC_ExportToAries", // Ensure this matches your DataTable column
                    Width = 130,
                    ValueType = typeof(string), // Ensure ValueType is set correctly
                    FlatStyle = FlatStyle.Flat
                };
                // Add items to the drop-down list
                Maptohcc.Items.Add(XmlConstants.True);
                Maptohcc.Items.Add(XmlConstants.False);
                this.dataGridView.Columns.Add(Maptohcc);
                // Assuming 'dataGridView' is your DataGridView
                // Make sure 'dataGridView' is properly instantiated and initialized
                try
                {
                    // Fetch contracts from database
                    DataTable contracts = dbHelper.GetActiveContracts();

                    // Create the ComboBox column
                    DataGridViewComboBoxColumn contractColumn = new DataGridViewComboBoxColumn
                    {
                        Name = "ContractID",
                        HeaderText = "Contract",
                        DataPropertyName = "HCC_ContractID", // Ensure this matches your DataTable's column name
                        Width = 130,
                        ValueType = typeof(string),
                        FlatStyle = FlatStyle.Flat,
                        DisplayMember = "ContractID", // Display member can still be "ContractID" if that's what you want to display
                        ValueMember = "ContractID"
                    };

                    // Assign DataSource to the ComboBox column
                    contractColumn.DataSource = contracts;

                    // Add the ComboBox column to the DataGridView
                    dataGridView.Columns.Add(contractColumn);

                    // Set EditMode to EditOnEnter for single click activation
                    dataGridView.EditMode = DataGridViewEditMode.EditOnEnter;

                    // Optionally set DefaultCellStyle for visual enhancement
                    dataGridView.DefaultCellStyle.ForeColor = Color.Black;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }


                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (row.Cells["HCC_ExportToAries"].Value == null ||
                        !Maptohcc.Items.Contains(row.Cells["HCC_ExportToAries"].Value))
                    {
                        row.Cells["HCC_ExportToAries"].Value = "";
                    }
                }
                this.Load += new EventHandler(Form1_Load);//to load form

                // Handle the DataError event to catch and manage errors gracefully
                this.dataGridView.DataError += new DataGridViewDataErrorEventHandler(dataGridView_DataError);

                void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
                {
                    // Handle the error here
                   // MessageBox.Show("Invalid value for ComboBox column.");
                    e.ThrowException = false; // Prevent the exception from being thrown
                }
                this.dataGridView.RowTemplate.Height = 40;
                this.dataGridView.Columns.Add("HCC_PrimaryService", "Primary Services");
                this.dataGridView.Columns["HCC_PrimaryService"].DataPropertyName = "HCC_PrimaryService";
                this.dataGridView.Columns["HCC_PrimaryService"].Width = 220;
                this.dataGridView.Columns.Add("HCC_SecondaryService", "Secondary Services");
                this.dataGridView.Columns["HCC_SecondaryService"].DataPropertyName = "HCC_SecondaryService";
                this.dataGridView.Columns["HCC_SecondaryService"].Width = 220;
                this.dataGridView.Columns.Add("HCC_Subservice", "Sub Service");
                this.dataGridView.Columns["HCC_Subservice"].DataPropertyName = "HCC_Subservice";
                this.dataGridView.Columns["HCC_Subservice"].Width = 220;
                this.dataGridView.Columns.Add("UnitsOfMeasure", "Units Of Measure");
                this.dataGridView.Columns["UnitsOfMeasure"].DataPropertyName = "UnitsOfMeasure";
                this.dataGridView.Columns["UnitsOfMeasure"].Width = 220;
                this.dataGridView.Columns.Add("UnitValue", "Unit");
                this.dataGridView.Columns["UnitValue"].DataPropertyName = "UnitValue";
                this.dataGridView.Columns["UnitValue"].Width = 220;
                //calling Service Lsit function
                DataTable result = this.dbHelper.GetAllServiceLists();

                DataGridViewComboBoxColumn statusColumn = new DataGridViewComboBoxColumn
                {
                    Name = "Status",
                    HeaderText = "Status",
                    DataPropertyName = "Status", // Assuming you have a "Status" column in your DataTable
                    Width = 130

                };
                // Adding items to the drop-down list
                statusColumn.Items.Add(ContractIDList.ACTIVE);
                statusColumn.Items.Add(ContractIDList.INACTIVE);
                statusColumn.Items.Add(ContractIDList.DELETE);
                Font smallerFont = new Font(dataGridView.Font.FontFamily, 8); // Adjust the size as needed

                // Apply the smaller font to the DefaultCellStyle
                statusColumn.DefaultCellStyle.Font = smallerFont;

                // Add the column to the DataGridView
                this.dataGridView.Columns.Add(statusColumn);

                dataGridView.EditingControlShowing += dataGridView_EditingControlShowing;//to edit particular row
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
                dataGridView.KeyDown += DataGridView_KeyDown;

                dataGridView.Columns.Add(deleteButtonColumn);
                dataGridView.CellPainting += dataGridView_CelleditPainting;
                /*dataGridView.CellClick += dataGridView_Celledit;*/
               
                dataGridView.CellPainting += dataGridView_CellPainting;
                dataGridView.CellClick += dataGridView_CellClickdelete;//to apply styles to delete button
                //dataGridView.ClearSelection();

                dataGridView.Columns["Status"].ReadOnly = true;

                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        string statusValue = row["Status"].ToString();

                        switch (statusValue)
                        {
                            case "29":
                                row["Status"] = ContractIDList.ACTIVE;
                                break;
                            case "28":
                                row["Status"] = ContractIDList.INACTIVE;
                                break;

                            default:
                                row["Status"] = ContractIDList.DELETE;
                                break;
                        }
                    }

                    // Set the DataTable as the DataSource of the DataGridView
                    this.dataGridView.DataSource = dataTable;
                    dataGridView.Columns["Status"].ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }        }
        private void Form1_Load(object sender, EventArgs e)//call function to load form
        {
            PopulateContractComboBoxColumn();
            PopulateGrid();
        }
        private void PopulateContractComboBoxColumn()//to populate data 
        {
            try { 
                DataTable contracts = dbHelper.GetActiveContracts();//to get contracts list in service code
                DataGridViewComboBoxColumn comboBoxColumn = (DataGridViewComboBoxColumn)this.dataGridView.Columns["ContractID"];
                comboBoxColumn.DataSource = contracts;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGridView_CellContentClick2(object sender, DataGridViewCellEventArgs e)//Check if the clicked cell is in a valid row and is the "Edit" column
        {
            try
            {
                dataGridView.Columns["Status"].ReadOnly = true;

                if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView.Columns[Constants.EditColumnName].Index)
                {
                    foreach (DataGridViewRow row in dataGridView.Rows)
                    {
                        row.ReadOnly = true;
                    }

                    // Make all cells in the specific row editable, except for the "Status" column
                    foreach (DataGridViewCell cell in dataGridView.Rows[e.RowIndex].Cells)
                    {
                        if (cell.OwningColumn.Name != "Status")
                        {
                            cell.ReadOnly = false;
                        }
                    }

                    // Set the specific row to read-only = false
                    dataGridView.Rows[e.RowIndex].ReadOnly = false;
                }
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int radius)//design format of button
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
        private bool IsRowEmpty(int rowIndex)//to update boolean value
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
        private void SetupDataGridView()//setup grid
        {
            // Other column setup code...

            // Assuming 'dataGridView' is your DataGridView and 'dataTable' is your DataTable
            this.dataGridView.DataSource = dataTable;

            // Set the height for individual rows
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                row.Height = 80; // Adjust the height as needed
            }
        }
        private void btnClose_Click_1(object sender, EventArgs e)//to close the form
        {
            try
            {
                // Close the current form (dispose it)
                this.Close();
                Application.Restart();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)//to display the drop down
        {
            if (dataGridView.CurrentCell.ColumnIndex == dataGridView.Columns["Status"].Index && e.Control is System.Windows.Forms.ComboBox)
            {

                System.Windows.Forms.ComboBox comboBox = e.Control as System.Windows.Forms.ComboBox;

                if (comboBox != null)
                {
                    comboBox.Items.Clear();


                    // Get the current row's status value
                    string currentStatus = dataGridView.CurrentRow.Cells["Status"].Value.ToString();

                    // Populate the ComboBox based on the current status
                    if (currentStatus == ContractIDList.ACTIVE)
                    {
                        comboBox.Items.Add(ContractIDList.INACTIVE);
                        comboBox.Items.Add(ContractIDList.DELETE);
                    }
                    else if (currentStatus == ContractIDList.INACTIVE)
                    {
                        comboBox.Items.Add(ContractIDList.ACTIVE);
                        comboBox.Items.Add(ContractIDList.DELETE);
                    }
                    else if (currentStatus == ContractIDList.DELETE)
                    {
                        comboBox.Items.Add(ContractIDList.ACTIVE);
                        comboBox.Items.Add(ContractIDList.INACTIVE);
                    }
                    else
                    {
                        comboBox.Items.Add(ContractIDList.ACTIVE);
                        comboBox.Items.Add(ContractIDList.INACTIVE);
                        comboBox.Items.Add(ContractIDList.DELETE);
                    }

                }
            }
        }
        private void btnAdd_Click_1(object sender, EventArgs e)//to add a new row
        {
            try
            {
                // Allow editing for the entire DataGridView initially
                dataGridView.ReadOnly = false;
                dataGridView.Columns["Status"].ReadOnly = false;

                // Display confirmation message
                var result = MessageBox.Show($"{Constants.AreyousureyouwanttoaddanewService}", Constants.ServiceCodeSetup, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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
                    if (row.Cells["Status"].Value != null)
                    {
                        string statusValue = row.Cells["Status"].Value.ToString();

                        // Assuming you have specific conditions for read-only rows based on status
                        if (statusValue == ContractIDList.ACTIVE || statusValue == ContractIDList.INACTIVE || statusValue == ContractIDList.DELETE)
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
                    MessageBox.Show(Constants.Norowselectedtosave, "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dataGridView.CurrentRow != null && !dataGridView.CurrentCell.OwningColumn.Name.Equals(Constants.EditColumnName))
                {
                    if (dataGridView.SelectedRows.Count > 1)
                    {
                        MessageBox.Show($"{Constants.Selectonlyonerowatatime}", Constants.ServiceCodeSetup, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    int rowIndex = dataGridView.CurrentRow.Index;
                    DataRow row = ((DataRowView)dataGridView.CurrentRow.DataBoundItem).Row;

                    string currentContractID = row["ServiceCodeID"].ToString();

                    // Check for duplicate Contract ID in other rows
                    bool isDuplicate = false;
                    for (int i = 0; i < dataGridView.Rows.Count; i++)
                    {
                        if (i != rowIndex) // Skip the current row
                        {
                            var otherRow = dataGridView.Rows[i];
                            if (otherRow.Cells["ServiceCodeID"].Value != null &&
                                otherRow.Cells["ServiceCodeID"].Value.ToString() == currentContractID)
                            {
                                isDuplicate = true;
                                break;
                            }
                        }
                    }

                    if (isDuplicate)
                    {
                        MessageBox.Show($"{Constants.ServiceCodeIDIDalreadyexists}", "ServiceCode Setup-Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Exit the method if a duplicate is found
                    }

                    if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                    {
                        SaveServiceCode(row);
                    }
                    else
                    {
                        MessageBox.Show(ContractIDList.Nochangesdetectedintheselectedrow, Constants.ServiceCodeSetup, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show(ContractIDList.Nochangesdetectedintheselectedrow, Constants.ServiceCodeSetup, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
            private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)// Event handler for when a cell is clicked, specifically to handle edit button
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView.Columns[Constants.EditColumnName].Index)
            {
                dataGridView.Rows[e.RowIndex].Cells["Status"].ReadOnly = true;

                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    // Get the Contract ID column index
                    int contractIDColumnIndex = dataGridView.Columns["ServiceCodeID"].Index;

                    // Check if the current cell being edited is not in the Contract ID column
                    if (e.ColumnIndex != contractIDColumnIndex)
                    {
                        DataGridViewRow row = dataGridView.Rows[e.RowIndex];

                        // Check if the Contract ID cell is empty or null
                        if (row.Cells[contractIDColumnIndex].Value == null || string.IsNullOrWhiteSpace(row.Cells[contractIDColumnIndex].Value.ToString()))
                        {

                            MessageBox.Show($"{Constants.ServiceCodeIDhastobepresentbeforeediting}", "ServiceCode Setup-Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            return;
                        }
                        row.DefaultCellStyle.BackColor = Color.FromArgb(200, 230, 255);

                    }


                    // Get the ContractID from the current row
                    string ServiceCodeID = dataGridView.Rows[e.RowIndex].Cells["ServiceCodeID"].Value.ToString();


                    var result = MessageBox.Show($"{Constants.AreyousureyouwanttoeditServiceCodeID} {ServiceCodeID} ", Constants.ServiceCodeSetup, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    // alreadyDeletedMessageShown = true; // Set the flag to true after showing the message


                    // Prompt user for confirmation before editing, showing the ContractID

                    if (result == DialogResult.No)
                    {
                        // Reset the row's background color to the default
                        dataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = dataGridView.DefaultCellStyle.BackColor; // Default color
                    }
                    if (result == DialogResult.Yes)
                    {
                        dataGridView.Rows[e.RowIndex].Cells["Status"].ReadOnly = true;
                        // Make all rows read-only initially
                        foreach (DataGridViewRow row in dataGridView.Rows)
                        {
                            row.ReadOnly = true;
                        }
                        dataGridView.ReadOnly = false;
                        // Make the specific row editable
                        dataGridView.Rows[e.RowIndex].ReadOnly = false;
                    }

                    // Make all cells editable for the selected row except for the "Status" column
                    foreach (DataGridViewColumn column in dataGridView.Columns)
                    {
                        if (column.Name == "Status")
                        {
                            dataGridView.Rows[e.RowIndex].Cells[column.Index].ReadOnly = true;
                        }
                        else
                        {
                            dataGridView.Rows[e.RowIndex].Cells[column.Index].ReadOnly = false;
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
        private bool alreadyDeletedMessageShown = false;
        private void dataGridView_CellClickdelete(object sender, DataGridViewCellEventArgs e)//to delete particular row
        {
            try
            { // Ensure the row index is valid and the clicked cell is in the "Delete" column
                if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView.Columns["Delete"].Index)
                {
                    DataGridViewRow currentRow = dataGridView.Rows[e.RowIndex];

                    // Get the Contract ID and Status column indices
                    int contractIDColumnIndex = dataGridView.Columns["ServiceCodeID"].Index;
                    int statusColumnIndex = dataGridView.Columns["Status"].Index;

                    // Retrieve values
                    string ServiceCodeID = currentRow.Cells[contractIDColumnIndex].Value?.ToString();
                    string status = currentRow.Cells[statusColumnIndex].Value?.ToString();

                    // Check if the Contract ID cell is empty or null
                    if (string.IsNullOrWhiteSpace(ServiceCodeID))
                    {
                        MessageBox.Show($"{Constants.ServiceIDhastobepresentbeforedeleting}", "ServiceCode Setup - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Check if the status is 'DELETE' and whether the message has already been shown
                    if (status != null && status.Equals("DELETE", StringComparison.OrdinalIgnoreCase))
                    {
                        if (!alreadyDeletedMessageShown)
                        {
                            MessageBox.Show($"This {ServiceCodeID} has already been deleted.", Constants.ServiceCodeSetup, MessageBoxButtons.OK, MessageBoxIcon.Question);
                            alreadyDeletedMessageShown = true; // Set the flag to prevent further messages
                        }
                        return;  // Exit method here to prevent further actions
                    }

                    // Confirm deletion
                    var result = MessageBox.Show($"Do you want to delete Service {ServiceCodeID}?", Constants.ServiceCodeSetup, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
            var cell = dataGridView.Rows[rowIndex].Cells[Constants.DeleteColumnName];
            if (cell.Value != null && cell.Value.ToString() == Constants.DeleteButtonText)
            {
                int ServiceCodeID = Convert.ToInt32(dataGridView.Rows[rowIndex].Cells["ServiceCodeID"].Value);

                if (ServiceCodeID > 0)
                {
                    // Update the status to "DELETE"
                    dataGridView.Rows[rowIndex].Cells["Status"].Value = ContractIDList.DELETE;
                    dbHelper.ServiceCodeIdUpdateStatus(ServiceCodeID, "30"); // Assume this function updates the status in the database
                    alreadyDeletedMessageShown = true;
                    MessageBox.Show($"Deleted ContractID: {ServiceCodeID} Successfully", Constants.ServiceCodeSetup, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                   
                }
                else
                {
                    MessageBox.Show(ContractIDList.ContractIDortypeisinvalid, "ServiceCode Setup-Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }
        private void HandleEditServiceCode(int rowIndex)//updating data
        {
            var cell = dataGridView.Rows[rowIndex].Cells[Constants.EditColumnName];
            if (cell.Value != null && cell.Value.ToString() == Constants.EditColumnName)
            {

                try
                {
                    int serviceCodeID = Convert.ToInt32(dataGridView.Rows[rowIndex].Cells["ServiceCodeID"].Value);
                    string service = dataGridView.Rows[rowIndex].Cells["Service"].Value.ToString();
                    string hccExportToAries = dataGridView.Rows[rowIndex].Cells["HCC_ExportToAries"].Value.ToString();
                    string hccContractID = dataGridView.Rows[rowIndex].Cells["ContractID"].Value.ToString();
                    string hccPrimaryService = dataGridView.Rows[rowIndex].Cells["HCC_PrimaryService"].Value.ToString();
                    string hccSecondaryService = dataGridView.Rows[rowIndex].Cells["HCC_SecondaryService"].Value.ToString();
                    string hccSubservice = dataGridView.Rows[rowIndex].Cells["HCC_Subservice"].Value.ToString();
                    string unitsOfMeasure = dataGridView.Rows[rowIndex].Cells["UnitsOfMeasure"].Value.ToString();
                    decimal unitValue = Convert.ToDecimal(dataGridView.Rows[rowIndex].Cells["UnitValue"].Value);
                    string status = dataGridView.Rows[rowIndex].Cells["Status"].Value.ToString();

                    if (serviceCodeID > 0)
                    {
                        // Calling the helper function to update the service code
                        dbHelper.EditServiceCode(serviceCodeID, service, hccExportToAries, hccContractID, hccPrimaryService, hccSecondaryService, hccSubservice, unitsOfMeasure, unitValue, status);

                    }
                }
                catch (InvalidCastException ex)
                {
                    return; // Skip this row
                }
            }
        }
        private void SaveServiceCode(DataRow row)//save data in database
        {
            try
            {
                var statusString = row["Status"].ToString();

                // Ensure the status is set correctly based on your conditions
                if (statusString == "ACTIVE")
                {
                    statusString = "29";
                }
                else if (statusString == "INACTIVE")
                {
                    statusString = "28";
                }
                else if (statusString == "DELETE")
                {
                    statusString = "30";
                }

                int serviceCodeID;
                int hccContractID;
                decimal unitValue;

                // Check for empty or null HCC_ContractID before parsing
                if (string.IsNullOrEmpty(row["HCC_ContractID"].ToString()))
                {
                    MessageBox.Show(Constants.HCCContractIDcannotbeempty,Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!int.TryParse(row["ServiceCodeID"].ToString(), out serviceCodeID))
                {
                    MessageBox.Show(Constants.InvalidServiceCodeID, Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrEmpty(row["UnitValue"].ToString()))
                {
                    unitValue = 0; // or any default value you want to use
                }
                if (!int.TryParse(row["HCC_ContractID"].ToString(), out hccContractID))
                {
                    MessageBox.Show(Constants.InvalidHCCContractID,Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Check if UnitValue is null or empty and set a default value if necessary
                if (string.IsNullOrEmpty(row["UnitValue"].ToString()))
                {
                    unitValue = 0; // or any default value you want to use
                }

                else if (!decimal.TryParse(row["UnitValue"].ToString(), out unitValue))
                {
                    MessageBox.Show(Constants.InvalidUnitValue, Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Execute the stored procedure to insert or update row into table
                string operation = dbHelper.InsertOrUpdateServiceCode(
                    serviceCodeID,
                    row["Service"].ToString(),
                    row["HCC_ExportToAries"].ToString(),
                    hccContractID,
                    row["HCC_PrimaryService"].ToString(),
                    row["HCC_SecondaryService"].ToString(),
                    row["HCC_Subservice"].ToString(),
                    row["UnitsOfMeasure"].ToString(),
                    unitValue,
                    statusString
                );
                dataGridView.ReadOnly = true;

                // Display appropriate message
                if (operation == "Update")
                {
                    MessageBox.Show($"{Constants.Servicecodeupdatedsuccessfully}", Constants.ServiceCodeSetup, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (operation == "Insert")
                {
                    MessageBox.Show($"{Constants.Servicecodeinsertedsuccessfully}",Constants.ServiceCodeSetup, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (dataGridView.CurrentCell.ColumnIndex == dataGridView.Columns["Status"].Index && e.Control is System.Windows.Forms.ComboBox comboBox)
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
        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)//to create dropdown
        {
            if (sender is ComboBox comboBox)
            {
                DataGridViewRow currentRow = dataGridView.CurrentRow;
                if (currentRow == null || currentRow.Cells["ServiceCodeID"].Value == null)
                    return;

                string selectedStatus = comboBox.SelectedItem.ToString();
                string ServiceCodeID = currentRow.Cells["ServiceCodeID"].Value.ToString();
                string currentStatus = currentRow.Cells["Status"].Value?.ToString();

                if (currentStatus != selectedStatus)
                {
                    string message = selectedStatus == "Active"
                        ? $"Do you want to make the contract {ServiceCodeID} active?"
                        : $"Do you want to make the contract {ServiceCodeID} inactive?";

                    var result = MessageBox.Show(message, $"Confirm {selectedStatus} Status", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        // Update the status cell value
                        currentRow.Cells["Status"].Value = selectedStatus;
                    }
                    else
                    {
                        // Revert the ComboBox selection to the previous value
                        comboBox.SelectedItem = currentStatus;
                    }
                }
            }
        }
    }
}