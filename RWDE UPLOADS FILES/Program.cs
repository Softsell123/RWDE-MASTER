using ClosedXML.Excel;
using RWDE;
using RWDE_UPLOADS_FILES;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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

            
            string currentConnectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(currentConnectionString);
            if (builder.DataSource == "PLACEHOLDER")
            {
                // Show Connection Setup Form

                using (var connectionSetupForm = new frmConnectionSetup())
                {
                    connectionSetupForm.StartPosition = FormStartPosition.CenterScreen;
                    if (connectionSetupForm.ShowDialog() == DialogResult.OK)
                    {
                        // Load Main Form only if connection is successfully set
                        Application.Run(new frmMain());
                    }
                }
            }
            else
            {
                Application.Run(new frmMain());
            }
        }
    }
}
