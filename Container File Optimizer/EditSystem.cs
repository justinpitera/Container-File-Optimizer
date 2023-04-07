using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Container_File_Optimizer
{
    public partial class EditSystem : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Container_File_Optimizer.Properties.Settings.ContainerfileDatabaseConnectionString"].ConnectionString;
        Dictionary<int, int> systemIDCollection = new Dictionary<int, int>();
        public EditSystem()
        {
            InitializeComponent();
        }

        private void EditSystem_Load(object sender, EventArgs e)
        {
            ViewSystems();
        }
        private void ViewSystems()
        {
            // Define SQL query to retrieve system_name and system_id columns
            string sqlQuery = "SELECT system_name, system_id FROM System";

            // Create SqlConnection and SqlCommand objects
            using (SqlConnection cnn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
            {
                // Create a new DataTable to hold the results
                DataTable dataTable = new DataTable();

                // Create a new SqlDataAdapter and fill the DataTable
                using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                {
                    adp.Fill(dataTable);
                }

                // Create a new Dictionary to store ListView index and system_id

                // Add each system_name value to the ListView and add ListView index and system_id to the dictionary
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    DataRow row = dataTable.Rows[i];
                    string systemName = row["system_name"].ToString();
                    int systemId = Convert.ToInt32(row["system_id"]);
                    listView2.Items.Add(systemName);
                    systemIDCollection.Add(i, systemId);
                }
            }
            foreach (int i in systemIDCollection.Keys)
            {
                MessageBox.Show(i + " - " + systemIDCollection[i].ToString());
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
