using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Windows.Forms;

namespace Container_File_Optimizer
{
    public partial class SystemViewer : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Container_File_Optimizer.Properties.Settings.ContainerfileDatabaseConnectionString"].ConnectionString;
        //        listbox[index]   database[index]
        Dictionary<int, int> systemIDCollection = new Dictionary<int, int>();
        Dictionary<int, int> appIDCollection = new Dictionary<int, int>();
        Dictionary<int, int> fileIDCollection = new Dictionary<int, int>();
        public SystemViewer()
        {
            InitializeComponent();
        }

        private void SystemViewer_Load(object sender, EventArgs e)
        {
            ViewSystems();
        }



        public void ViewSystems()
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
                    listBox1.Items.Add(systemName);
                    systemIDCollection.Add(i, systemId);
                }
            }
        }

        /// <summary>
        /// Gets the Application names and IDS associated with the currently selected item in the listbox for Systems 
        /// </summary>
        /// <param name="systemID">The file_id that needs to be evaluated.</param>
        /// <returns> The count for the number of times that id appeard.</returns>
        public void GetApps(int systemID)
        {
            // Define SQL query to retrieve system_name and system_id columns

            string query = "SELECT * FROM Application ap LEFT JOIN SysApp sa ON ap.app_id = sa.app_id WHERE sa.system_id = @system_id";

            // Create SqlConnection and SqlCommand objects
            using (SqlConnection cnn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, cnn))
            {
                cmd.Parameters.AddWithValue("@system_id", systemID);
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
                    string app_name = row["app_name"].ToString();
                    int app_ID = Convert.ToInt32(row["app_id"]);
                    listBox2.Items.Add(app_name);
                    appIDCollection.Add(i, app_ID);
                }
            }
        }




        public void GetAppIDs(int fileID)
        {
            // Define SQL query to retrieve system_name and system_id columns

            //string query = "SELECT * FROM [File] fi LEFT JOIN AppFile af ON fi.file_id = af.file_id WHERE sa.system_id = @file_id";
            string query = "SELECT app_id FROM AppFile WHERE file_id = @file_id";

            // Create SqlConnection and SqlCommand objects
            using (SqlConnection cnn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, cnn))
            {
                cmd.Parameters.AddWithValue("@file_id", fileID);
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
                    int app_ID = Convert.ToInt32(row["app_id"]);
                    fileIDCollection.Add(i, app_ID);
                }
            }


        }






        public void GetFileName(int appID)
        {
            string query = "SELECT * FROM AppFile af LEFT JOIN [File] fi ON af.file_id = fi.file_id WHERE af.app_id = @app_id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@app_id", appID);

                SqlDataReader reader = command.ExecuteReader();

                // Iterate through the SqlDataReader and populate the CheckedListBox
                while (reader.Read())
                {
                    int app_id = reader.GetInt32(0); // see if this can work with Int16 
                    int file_id = reader.GetInt32(1); // Same with this 
                    string file_name = reader.GetString(3);
                    listBox3.Items.Add(file_name);

                }

                // Close the SqlDataReader and the SqlConnection
                reader.Close();

                connection.Close();
            }
        }



        public void GetFiles(int appID)
        {
            string query = "SELECT * FROM AppFile WHERE app_id = @app_id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@app_id", appID);

                SqlDataReader reader = command.ExecuteReader();

                // Iterate through the SqlDataReader and populate the CheckedListBox
                while (reader.Read())
                {
                    int app_id = reader.GetInt32(0); // see if this can work with Int16 
                    int file_id = reader.GetInt32(1); // Same with this 
                    listBox3.Items.Add(file_id);

                }

                // Close the SqlDataReader and the SqlConnection
                reader.Close();

                connection.Close();
            }
        }


        public void GetSharedApps(int fileID, int systemID)
        {
            string query = "SELECT app_name FROM Application app LEFT JOIN SysApp sa ON ap.system_id = sa.system_id LEFT JOIN AppFile af ON app.app_id = af.app_id WHERE af.file_id = @file_id AND sa.system_id = @system_id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@file_id", fileID);
                command.Parameters.AddWithValue("@system_id", systemID);

                SqlDataReader reader = command.ExecuteReader();

                // Iterate through the SqlDataReader and populate the CheckedListBox
                string listOfSharedContainers = "";
                string fileName = listBox3.GetItemText(listBox3.SelectedItem);
                while (reader.Read())
                {
                    string app_name = reader.GetString(1);

                    listOfSharedContainers = listOfSharedContainers + app_name;
                    // concatenate everything to list of Shared containers string

                }
                MessageBox.Show("List of shared containers for " + fileName + listOfSharedContainers) ;
                // Close the SqlDataReader and the SqlConnection
                reader.Close();

                connection.Close();
            }
        }



        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Put this here to avoid crashing if they dont select anything 
            if (listBox1.SelectedItems.Count > 0)
            {
                appIDCollection.Clear();
                listBox2.Items.Clear();
                GetApps(systemIDCollection[listBox1.SelectedIndex]);
                listBox3.Items.Clear();
            }

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Put this here to avoid crashing if they dont select anything 
            if (listBox2.SelectedItems.Count > 0)
            {
                listBox3.Items.Clear();
                GetFileName(appIDCollection[listBox2.SelectedIndex]);
                
               
            }

        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox3.SelectedItems.Count > 0)
            {
               // GetSharedApps(fileIDCollection[listBox3.SelectedIndex], systemIDCollection[listBox1.SelectedIndex]);
            }
        }
    }







}
