using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows.Forms;

namespace RWDE
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        [DebuggerNonUserCode]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string currentConnectionString = ConfigurationManager.ConnectionStrings[Constants.MyConnection].ConnectionString;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(currentConnectionString);
            if (builder.DataSource == Constants.PlaceHolder)
            {
                // Show Connection Setup Form
                using (var connectionSetupForm = new FrmConnectionSetup())
                {
                    connectionSetupForm.StartPosition = FormStartPosition.CenterScreen;
                    if (connectionSetupForm.ShowDialog() == DialogResult.OK)
                    {
                        // Load Main Form only if connection is successfully set
                        Application.Run(new FrmMain());
                    }
                }
            }
            else
            {
                Application.Run(new FrmMain());
            }
        }
    }
}
