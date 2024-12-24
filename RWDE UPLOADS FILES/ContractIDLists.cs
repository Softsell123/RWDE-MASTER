using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RWDE
{
    public partial class ContractIdLists : Form
    {
        private readonly DbHelper dbHelper;
        private readonly string connectionString;//CONNECTION STRING
        public ContractIdLists()//Initialize the form or object
        {
            InitializeComponent();
            dbHelper = new DbHelper();
            connectionString = dbHelper.GetConnectionString();
            this.WindowState = FormWindowState.Maximized;
            this.ControlBox = false;
            PopulateDataGridView();
            InitializeDataGridView();
            dataGridView.CellClick += dataGridView_Celledit;
            dataGridView.KeyDown += dataGridView_KeyDown;
            dataGridView.CellEndEdit += dataGridView_CellEndEdit;
            dataGridView.CellValidating += dataGridView_CellValidating;
            // Set the event handler for the value change of the editing control
            dataGridView.EditingControlShowing += DataGridView_EditingControlShowing;
            dataGridView.EditingControlShowing += DataGridView_EditingControlShowingStatus;
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
        private void InitializeDataGridView()//Initialize the form or object
        {
            // Your DataGridView initialization code
            dataGridView.AllowUserToAddRows = false;
        }
        public class DataGridViewCalendarColumn : DataGridViewColumn// sets up the DateTimePicker with a custom format.
        {
            public DataGridViewCalendarColumn() : base(new DataGridViewCalendarCell())
            {
                // Constructor initializes the column with a DataGridViewCalendarCell as its CellTemplate.

            }
            public override DataGridViewCell CellTemplate
            {
                get
                {
                    return base.CellTemplate;
                }
                set
                {
                    if (value != null && !value.GetType().IsAssignableFrom(typeof(DataGridViewCalendarCell)))
                    {
                        throw new InvalidCastException(ContractIdList.MustbeaDataGridViewCalendarCell);
                    }
                    base.CellTemplate = value;
                }
            }
            // Ensure that you can use the properties from DataGridViewColumn
            public new string DataPropertyName
            {
                get { return base.DataPropertyName; }
                set { base.DataPropertyName = value; }
            }
            public new string HeaderText//for dropdown column grid 
            {
                get { return base.HeaderText; }
                set { base.HeaderText = value; }
            }
            public new string Name
            {
                get { return base.Name; }
                set { base.Name = value; }
            }

            public new int Width
            {
                get { return base.Width; }
                set { base.Width = value; }
            }
            public new DataGridViewCellStyle DefaultCellStyle
            {
                get { return base.DefaultCellStyle; }
                set { base.DefaultCellStyle = value; }
            }
        }
        public class DataGridViewCalendarEditingControl : DateTimePicker, IDataGridViewEditingControl// sets up the DateTimePicker with a custom format.
        {
            DataGridView dataGridView;
            private bool valueChanged = false;
            int rowIndex;

            public DataGridViewCalendarEditingControl()
            {
                this.Format = DateTimePickerFormat.Custom;
                this.CustomFormat = Constants.DdMMyyyyHHmmss; // Custom format with seconds
            }

            public object EditingControlFormattedValue
            {
                get
                {
                    return this.Value.ToString(Constants.DdMMyyyyHHmmss);
                }
                set
                {
                    if (value is string s)
                    {
                        this.Value = DateTime.Parse(s);
                    }
                }
            }
            public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)//edit data of particular row
            {
                try
                {
                    return EditingControlFormattedValue;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
            }
            public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)//apply font styles
            {
                try
                {
                    this.Font = dataGridViewCellStyle.Font;
                    this.CalendarForeColor = dataGridViewCellStyle.ForeColor;
                    this.CalendarMonthBackground = dataGridViewCellStyle.BackColor;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            public int EditingControlRowIndex//edit control box
            {
                get { return rowIndex; }
                set { rowIndex = value; }
            }
            public bool EditingControlWantsInputKey(Keys key, bool dataGridViewWantsInputKey)//This method is used to determine whether the editing control (such as a custom DateTimePicker) 
            {
                switch (key & Keys.KeyCode)
                {
                    case Keys.Left:
                    case Keys.Up:
                    case Keys.Down:
                    case Keys.Right:
                    case Keys.Home:
                    case Keys.End:
                    case Keys.PageDown:
                    case Keys.PageUp:
                        return true;
                    default:
                        return !dataGridViewWantsInputKey;
                }
            }
            public void PrepareEditingControlForEdit(bool selectAll)//for edit control
            {
                // No preparation needed
            }
            public bool RepositionEditingControlOnValueChange
            {
                get { return false; }
            }
            public DataGridView EditingControlDataGridView
            {
                get { return dataGridView; }
                set { dataGridView = value; }
            }
            public bool EditingControlValueChanged
            {
                get { return valueChanged; }
                set { valueChanged = value; }
            }
            public Cursor EditingPanelCursor
            {
                get { return base.Cursor; }
            }
            protected override void OnValueChanged(EventArgs eventargs)
            {
                valueChanged = true;
                this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
                base.OnValueChanged(eventargs);
            }
        }
        public class DataGridViewCalendarCell : DataGridViewTextBoxCell//Constructor sets up the DateTimePicker with a custom format.
        {
            public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
            {
                base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
                DataGridViewCalendarEditingControl ctl = DataGridView.EditingControl as DataGridViewCalendarEditingControl;
                if (this.Value == null || this.Value == DBNull.Value)
                {
                    //  ctl.Value = (DateTime)this.Value;
                }
                else
                {
                    if (ctl != null)
                    {
                        ctl.Value = (DateTime)this.Value;
                    }
                }
            }
            public override Type EditType
            {
                get { return typeof(DataGridViewCalendarEditingControl); }
            }

            public override Type ValueType
            {
                get { return typeof(DateTime); }
            }

        }
        public void PopulateDataGridView()  // Populate the DataGridView with data and configure its columns

        {
            try
            {
                this.dataGridView.AutoGenerateColumns = false;
                DataTable dataTable = dbHelper.GetAllContractLists();//to get the contractid which are stored in db table

                if (dataTable != null)
                {
                    this.dataGridView.DataSource = dataTable;
                }

                this.dataGridView.Columns.Clear();

                this.dataGridView.Columns.Add(Constants.ContractId, Constants.ContractIDs);
                this.dataGridView.Columns[Constants.ContractId].DataPropertyName = Constants.ContractId;
                this.dataGridView.Columns[Constants.ContractId].Width = 220;
                this.dataGridView.RowTemplate.Height = 40;
                this.dataGridView.Columns.Add(Constants.ContractName, Constants.Name);
                this.dataGridView.Columns[Constants.ContractName].DataPropertyName = Constants.ContractName;
                this.dataGridView.Columns[Constants.ContractName].Width = 220;

                // Add the StartedDateTime column with a custom format that includes seconds
                DataGridViewCalendarColumn startedDateTimeColumn = new DataGridViewCalendarColumn
                {
                    Name = Constants.StartedDateTime,
                    HeaderText = Constants.StartedOn,
                    DataPropertyName = Constants.StartedDateTime,
                    Width = 220,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = Constants.DdMMyyyyHHmmss }
                    // Custom format with seconds
                };
                this.dataGridView.Columns.Add(startedDateTimeColumn);

                DataGridViewCalendarColumn endedDateTimeColumn = new DataGridViewCalendarColumn
                {
                    Name =Constants.EndedDateTime,
                    HeaderText = Constants.EndedAt,
                    DataPropertyName = Constants.EndedDateTime,
                    Width = 220,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = Constants.DdMMyyyyHHmmss }
                };

                this.dataGridView.Columns.Add(endedDateTimeColumn);
                
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

                this.dataGridView.Columns.Add(statusColumn);

                dataGridView.EditingControlShowing += dataGridView_EditingControlShowing;//to edit the particular row
                // Adding the edit button column
                DataGridViewButtonColumn editButtonColumn = new DataGridViewButtonColumn
                {
                    Name = Constants.Edit,
                    Text = Constants.Edit,
                    Width = 130,
                    UseColumnTextForButtonValue = true
                };
                this.dataGridView.Columns.Add(editButtonColumn);
                // Ensure the DataTable has the 'Status' column with valid values
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
                    this.dataGridView.DataSource = dataTable;
                    dataGridView.Columns[Constants.Status].ReadOnly = true;
                }

                DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn
                {
                    Name = Constants.DeleteColumnName,
                    Text = Constants.DeleteColumnName,
                    Width = 130,
                    UseColumnTextForButtonValue = true
                };

                dataGridView.Columns.Add(deleteButtonColumn);
                dataGridView.CellPainting += dataGridView_CelleditPainting;
                /*dataGridView.CellClick += dataGridView_Celledit;*/
                dataGridView.CellClick += dataGridView_CellContentClick;
                dataGridView.CellClick += dataGridView_CellClickdelete;
                dataGridView.CellPainting += dataGridView_CellPainting;

                dataGridView.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"{Constants.ErrorLoadingData}{ex.Message}", Constants.ContractsSetup, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try { 
                if (dataGridView.CurrentCell.ColumnIndex == dataGridView.Columns[Constants.Status].Index && e.Control is ComboBox)
                {
                    if (e.Control is ComboBox comboBox)
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
        private void btnAdd_Click(object sender, EventArgs e)//to add new contract data
        {
            try { 
                 // Allow editing for the entire DataGridView initially
            dataGridView.ReadOnly = false;
            dataGridView.Columns[Constants.Status].ReadOnly = false;

            // Display confirmation message
            var result = MessageBox.Show($@"{Constants.Areyousureyouwanttoaddanewcontract}", Constants.ContractsSetup, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                 DataTable dataTable= (DataTable)dataGridView.DataSource;
                    // Create a new row in the DataTable
                    DataRow newRow = dataTable.NewRow();

                // Insert the new row at index 1 in the DataTable
                dataTable.Rows.InsertAt(newRow, 0);

                // Refresh the DataGridView to ensure the new row is displayed
                dataGridView.Refresh();

                // Select the entire row of the newly added row
                dataGridView.Rows[0].Selected = true;

                // Set the current cell to the first cell of the new row and begin edit mode
                dataGridView.CurrentCell = dataGridView.Rows[0].Cells[0];
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
        private void DataGridView_EditingControlShowingStatus(object sender, DataGridViewEditingControlShowingEventArgs e)//to update status
        {
            try { 
                 if (dataGridView.CurrentCell.ColumnIndex == dataGridView.Columns[Constants.Status].Index && e.Control is ComboBox comboBox)
                 {
                  // Remove existing event handlers to prevent multiple subscriptions
                 comboBox.SelectedIndexChanged -= ComboBox_SelectedIndexChanged;
                 comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
                 }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)//to update status
        {
            try { 
                if (sender is ComboBox comboBox)
                {

                 string selectedStatus = comboBox.SelectedItem.ToString();
                DataGridViewRow currentRow = dataGridView.CurrentRow;
                if (currentRow == null || currentRow.Cells[Constants.ContractName].Value == null) return;
                string contractid = currentRow.Cells[Constants.ContractId].Value?.ToString();

                string contractName = currentRow.Cells[Constants.ContractName].Value.ToString();
                string currentStatus = currentRow.Cells[Constants.Status].Value?.ToString();

                if (currentStatus != selectedStatus)
                { 
                    if (contractid == "")
                    {
                        MessageBox.Show(Constants.ContractIdisrequired, Constants.ContractsSetupError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }


                    string message = selectedStatus == Constants.Active
                        ? string.Format(Constants.AreYouSureMakeActive, contractName)
                        : string.Format(Constants.AreYouSureMakeInactive, contractName);

                        var result = MessageBox.Show(message, selectedStatus == Constants.Active ? ContractIdList.ConfirmMakeActive : ContractIdList.ConfirmMakeInactive, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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

                    if (selectedStatus == Constants.Active || selectedStatus == Constants.Inactive || selectedStatus == Constants.Delete)
                    {
                        dataGridView.CurrentRow.Cells[Constants.Status].ReadOnly = false;
                    }
                }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
         
            }
        }
        private void DataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)//to update startdate time
        {
            try { 
               if (dataGridView.CurrentCell != null && dataGridView.CurrentCell.ColumnIndex == dataGridView.Columns[Constants.StartedDateTime].Index)
               {
                   if (e.Control is DataGridViewCalendarEditingControl startedDateTimePicker)
                   {
                       startedDateTimePicker.ValueChanged -= StartedDateTimePicker_ValueChanged; // Unsubscribe to avoid multiple subscriptions
                       startedDateTimePicker.ValueChanged += StartedDateTimePicker_ValueChanged;
                   }
               }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void StartedDateTimePicker_ValueChanged(object sender, EventArgs e)//modify ended date according to starttime 
        {
            try { 
                if (sender is DataGridViewCalendarEditingControl startedDateTimePicker)
                {
                    DateTime startedDateTime = startedDateTimePicker.Value;

                    // Calculate the EndedDateTime as one day less than one year from the StartedDateTime
                    DateTime endedDateTime = startedDateTime.AddYears(1).AddDays(-1).Date.AddHours(11).AddMinutes(59).AddSeconds(59);
                    // Get the current row being edited
                    DataGridViewRow currentRow = dataGridView.CurrentRow;
                    if (currentRow != null)
                    {
                        currentRow.Cells[Constants.StartedDateTime].Value = startedDateTime;
                        currentRow.Cells[Constants.EndedDateTime].Value = endedDateTime;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGridView_KeyDown(object sender, KeyEventArgs e)//to control the delete button functionality
        {
            try{// Check if the Delete key is pressed
                if (e.KeyCode == Keys.Delete)
                {
                    // Prevent the default action
                    e.Handled = true;
                    e.SuppressKeyPress = true;

                    // Show confirmation dialog
                    var result = MessageBox.Show($@"{Constants.AreyousureyouwanttodeleteSelectedrow}", Constants.Confirmation, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // Allow the deletion
                        DeleteSelectedRow();
                    }
                    else
                    {
                        // Optionally, show a message to the user
                        MessageBox.Show($@"{Constants.Deletioncancelled}", Constants.Information, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteSelectedRow() // Method to delete the selected row
        {
            try {
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
        private void btnSave_Click(object sender, EventArgs e) // to save the particular row to the database table
        {
            try { 
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
                        MessageBox.Show($@"{Constants.Selectonlyonerowatatime}", Constants.ContractsSetup, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    int rowIndex = dataGridView.CurrentRow.Index;
                    DataRow row = ((DataRowView)dataGridView.CurrentRow.DataBoundItem).Row;

                    string currentContractId = row[Constants.ContractId].ToString();

                    // Check for duplicate Contract ID in other rows
                    bool isDuplicate = false;
                    for (int i = 0; i < dataGridView.Rows.Count; i++)
                    {
                        if (i != rowIndex) // Skip the current row
                        {
                            var otherRow = dataGridView.Rows[i];
                            if (otherRow.Cells[Constants.ContractId].Value != null &&
                                otherRow.Cells[Constants.ContractId].Value.ToString() == currentContractId)
                            {
                                isDuplicate = true;
                                break;
                            }
                        }
                    }

                    if (isDuplicate)
                    {
                        MessageBox.Show($@"{Constants.ContractIDalreadyexists}", Constants.ContractsSetup, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Exit the method if a duplicate is found
                    }

                    if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                    {
                        SaveContract(row);
                    }
                    else
                    {
                        MessageBox.Show(ContractIdList.Nochangesdetectedintheselectedrow, Constants.ContractsSetup, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show(ContractIdList.Nochangesdetectedintheselectedrow, Constants.ContractsSetup, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SaveContract(DataRow row)//insert and update into table 
        {
            try { 
                if (dataGridView.ReadOnly)
                {
                    MessageBox.Show($@"{ContractId}{Constants.Alreadysaved}");
                }

                var statusString = row[Constants.Status].ToString();

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

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string currentContractId = row[Constants.ContractId].ToString();
                    string contractName = row[Constants.ContractName].ToString();
                    string startedDateTimeStr = row[Constants.StartedDateTime].ToString();

                    // Validate Contract ID and Contract Name
                    if (string.IsNullOrWhiteSpace(currentContractId))
                    {
                        MessageBox.Show($@"{Constants.PleaseaddContractIDbeforesaving}.", Constants.ContractsSetup, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(contractName))
                    {
                        MessageBox.Show($@"{Constants.PleasefillinContractNamebeforesaving}", Constants.ContractsSetup, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Validate and parse StartedDateTime
                    if (string.IsNullOrWhiteSpace(startedDateTimeStr) || !DateTime.TryParse(startedDateTimeStr, out DateTime startedDateTime))
                    {
                        MessageBox.Show($@"{Constants.PleaseenteravalidStartedDateTimeandEndedDateTime}", Constants.ContractsSetup, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Calc
                    // Calculate EndedDateTime
                    DateTime endedDateTime = startedDateTime.AddYears(1).AddDays(-1).Date.AddHours(23).AddMinutes(59).AddSeconds(59);

                    // Execute the stored procedure
                    string operation = dbHelper.InsertOrUpdateContract(
                        int.Parse(currentContractId),
                        contractName,
                        startedDateTime, // Use DateTime value here
                        endedDateTime,   // Use DateTime value here
                        statusString,
                        Constants.Sakku,
                        DateTime.Now);

                    dataGridView.ReadOnly = true;

                    // Display appropriate message
                    if (operation == ContractIdList.Update)
                    {
                        MessageBox.Show($@"{ContractIdList.Contractupdatedsuccessfully}. {Constants.ContractId}: {currentContractId}", Constants.ContractsSetup, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (operation == ContractIdList.Insert)
                    {
                        MessageBox.Show(ContractIdList.Contractsavedsuccessfully, Constants.ContractsSetup, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGridView_CellClickdelete(object sender, DataGridViewCellEventArgs e)//// Ensure the clicked cell is in the correct column and row
        {
            try { 
                if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView.Columns[Constants.DeleteColumnName].Index)
                {
                    DataGridViewRow row = dataGridView.Rows[e.RowIndex];

                    // Get the Contract ID column index
                    int contractIdColumnIndex = dataGridView.Columns[Constants.ContractId].Index;

                    // Check if the Contract ID cell is empty or null
                    if (row.Cells[contractIdColumnIndex].Value == null || string.IsNullOrWhiteSpace(row.Cells[contractIdColumnIndex].Value.ToString()))
                    {
                        MessageBox.Show($@"{Constants.ContractIDhastobepresentbeforedeleting}", Constants.ContractsSetup, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // Cancel the edit
                        dataGridView.CancelEdit();
                        return;
                    }
                    string contractName = row.Cells[Constants.ContractName].Value.ToString();
                    // Get the Status column index
                    int statusColumnIndex = dataGridView.Columns[Constants.Status].Index;

                    // Check if the Status cell is set to 'deleted'
                    if (row.Cells[statusColumnIndex].Value != null && row.Cells[statusColumnIndex].Value.ToString().Equals(Constants.Delete, StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show($@"{Constants.This} {contractName} {Constants.Hasalreadybeendeleted}", Constants.ContractsSetup, MessageBoxButtons.OK, MessageBoxIcon.Question);
                        // Status is already 'deleted', proceed with deletion without confirmation
                        return;
                    }

                    // Confirm deletion
                    var result = MessageBox.Show($@"{ContractIdList.Areyousureyouwanttodeletecontract} {contractName}?", Constants.ContractsSetup, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        // Set the DataGridView to read-only except the row to be deleted
                        foreach (DataGridViewRow r in dataGridView.Rows)
                        {
                            r.ReadOnly = true;
                            if (r.Index == e.RowIndex)
                            {
                                r.ReadOnly = false;
                                break;
                            }
                        }
                        HandleDelete(e.RowIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGridView_Celledit(object sender, DataGridViewCellEventArgs e)//Ensure the clicked cell is in the correct column and row
        {
            try { 
                // Check if the clicked cell is in a valid row and is the "Edit" column
                if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView.Columns[Constants.Edit].Index)
                {
                    if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                    {
                        // Get the Contract ID column index
                        int contractIdColumnIndex = dataGridView.Columns[Constants.ContractId].Index;

                        // Check if the current cell being edited is not in the Contract ID column
                        if (e.ColumnIndex != contractIdColumnIndex)
                        {
                            DataGridViewRow row = dataGridView.Rows[e.RowIndex];

                            // Check if the Contract ID cell is empty or null
                            if (row.Cells[contractIdColumnIndex].Value == null || string.IsNullOrWhiteSpace(row.Cells[contractIdColumnIndex].Value.ToString()))
                            {
                                MessageBox.Show($@"{Constants.ContractIDhastobepresentbeforeediting}", Constants.ContractsSetup, MessageBoxButtons.OK, MessageBoxIcon.Error);

                                // Cancel the edit
                                dataGridView.CancelEdit();
                                return;
                            }
                            row.DefaultCellStyle.BackColor = Color.FromArgb(200, 230, 255);
                            //dataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(200, 230, 255); // Adjust the color as needed
                        }
                    }

                    // Get the ContractID from the current row
                    string contractId = dataGridView.Rows[e.RowIndex].Cells[Constants.ContractId].Value.ToString();

                    // Prompt user for confirmation before editing, showing the ContractID
                    var result = MessageBox.Show($@"{Constants.AreyousureyouwanttoeditContractId}: {contractId}?", Constants.ContractsSetup, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.No)
                    {
                        // Reset the row's background color to the default
                        dataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = dataGridView.DefaultCellStyle.BackColor; // Default color
                    }
                    if (result == DialogResult.Yes)
                    {
                        dataGridView.Rows[e.RowIndex].Cells[Constants.Status].ReadOnly = true;
                        // Make all rows read-only initially
                        foreach (DataGridViewRow row in dataGridView.Rows)
                        {
                            row.ReadOnly = true;
                        }
                        dataGridView.ReadOnly = false;
                        // Make the specific row editable
                        dataGridView.Rows[e.RowIndex].ReadOnly = false;

                        // Make all cells editable for the selected row except for the Constants.Status column
                        foreach (DataGridViewColumn column in dataGridView.Columns)
                        {
                            if (column.Name == Constants.Status)
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
                        HandleEdit(e.RowIndex);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)//to highlight the editing row
        {
            try{// Reset the row's background color when editing is completed
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
        private void dataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)// Reset the row's background color if editing is canceled
        {
            try { 
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
        private void HandleEdit(int rowIndex)//to update the edition column into database table
        {
            try
            {
                var cell = dataGridView.Rows[rowIndex].Cells[Constants.Edit];
                if (cell.Value != null && cell.Value.ToString() == Constants.Edit)
                {
                    int contractId = Convert.ToInt32(dataGridView.Rows[rowIndex].Cells[Constants.ContractId].Value);
                    string contractName = dataGridView.Rows[rowIndex].Cells[Constants.ContractName].Value.ToString();
                    object startedDateTime = dataGridView.Rows[rowIndex].Cells[Constants.StartedDateTime].Value;
                    object endedDateTime = dataGridView.Rows[rowIndex].Cells[Constants.EndedDateTime].Value;
                    string status = dataGridView.Rows[rowIndex].Cells[Constants.Status].Value.ToString();

                    if (contractId > 0)
                    {
                        // Call the edit function
                        dbHelper.ContractIdEdit(contractId, contractName, startedDateTime, endedDateTime, status);
                        dataGridView.Columns[Constants.Status].ReadOnly = true;
                        // Refresh the DataGridView after editing
                        //PopulateDataGridView();
                    }
                    else
                    {
                        MessageBox.Show(ContractIdList.ContractIDortypeisinvalid, Constants.ContractsSetup, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Example: Fetch row data from DataGridView
        private void dataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)//to dynamially display delete button in grid
        {
            try { 

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGridView_CelleditPainting(object sender, DataGridViewCellPaintingEventArgs e)//to dynamially display edit button in grid
        {
            try { 
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
        private GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int radius)  // Create a new GraphicsPath object to define the rounded rectangle
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
        private bool IsRowEmpty(int rowIndex) // Iterate through each cell in the specified row
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
        public void HandleDelete(int rowIndex)//to update status when deleted
        {
            try { 
                var cell = dataGridView.Rows[rowIndex].Cells[Constants.DeleteColumnName];
                if (cell.Value != null && cell.Value.ToString() == Constants.DeleteButtonText)
                {
                    int contractId = Convert.ToInt32(dataGridView.Rows[rowIndex].Cells[Constants.ContractId].Value);

                    if (contractId > 0)
                    {
                        // Update the status to "DELETE"
                        dataGridView.Rows[rowIndex].Cells[Constants.Status].Value = Constants.Delete;
                        dbHelper.ContractIdUpdateStatus(contractId, Constants.DeleteContractstatus); // Assume this function updates the status in the database

                        // Refresh the DataGridView to reflect the status change
                        // PopulateDataGridView();
                        MessageBox.Show($@"{Constants.DeletedContractId}{contractId} {Constants.Successfully}", Constants.ContractsSetup, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show(ContractIdList.ContractIDortypeisinvalid, Constants.ContractsSetup, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)//to close the form
        {
            try { 
                this.Close();
                Application.Restart();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)// Check if the clicked cell is in a valid row and is the "Edit" column
        {
           try{

            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView.Columns[Constants.Edit].Index)
            {
                // Set all rows to read-only
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    row.ReadOnly = true;
                }
                // Make all cells in the specific row editable, except for the Constants.Status column
                foreach (DataGridViewCell cell in dataGridView.Rows[e.RowIndex].Cells)
                {
                    if (cell.OwningColumn.Name !=Constants.Status)
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
    }
}
     

