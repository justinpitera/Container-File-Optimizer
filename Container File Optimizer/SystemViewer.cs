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

        // These collections used to keep track of what index in the list box == to what index in the database
        // For example in the systemIDCollection, index[0] in the list box (first item) may relate to database index[53] in the database. 
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



        // Gets all of the systems within a system

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
                    listBoxSystems.Items.Add(systemName);
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
                    listBoxContainers.Items.Add(app_name);
                    appIDCollection.Add(i, app_ID);
                }
            }
        }





        // Gets all of the apps that a file is contained in and returns their ids.
        public void GetFileIDS(int appID)
        {
            // Define SQL query to retrieve system_name and system_id columns

            //string query = "SELECT * FROM [File] fi LEFT JOIN AppFile af ON fi.file_id = af.file_id WHERE sa.system_id = @file_id";
            string query = "SELECT file_id FROM AppFile WHERE app_id = @app_id";

            // Create SqlConnection and SqlCommand objects
            using (SqlConnection cnn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, cnn))
            {
                cmd.Parameters.AddWithValue("@app_id", appID);
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
                    int file_id = Convert.ToInt32(row["file_id"]);
                    fileIDCollection.Add(i, file_id);
                }
            }


        }





        // Gets the file names from the application ID. This is used to populate the files listbox
        public void GetFileNames(int appID)
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
                    listBoxFiles.Items.Add(file_name);

                }

                // Close the SqlDataReader and the SqlConnection
                reader.Close();

                connection.Close();
            }
        }




        // Gets the list of files that an application (container) contains
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
                    listBoxFiles.Items.Add(file_id);

                }

                // Close the SqlDataReader and the SqlConnection
                reader.Close();

                connection.Close();
            }
        }



        // Get the list of shared apps that a certain file ID shares within a system
        public void GetSharedApps(int fileID, int systemID)
        {
            string query = "SELECT app_name FROM Application ap LEFT JOIN SysApp sa ON ap.app_id = sa.app_id LEFT JOIN AppFile af ON af.app_id = ap.app_id WHERE sa.system_id = @system_id AND af.file_id = @file_id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@file_id", fileID);
                command.Parameters.AddWithValue("@system_id", systemID);

                SqlDataReader reader = command.ExecuteReader();

                // Iterate through the SqlDataReader and populate the CheckedListBox
                List<string> listOfSharedContainers = new List<string>();
                string fileName = listBoxFiles.GetItemText(listBoxFiles.SelectedItem);
                while (reader.Read())
                {
                    string app_name = reader.GetString(0);
                    listOfSharedContainers.Add(app_name);

                }
                // concatenate everything to list of Shared containers string

                MessageBox.Show("List of shared containers for " + fileName + ":\n" + string.Join(Environment.NewLine,listOfSharedContainers)) ;
                // Close the SqlDataReader and the SqlConnection
                reader.Close();

                connection.Close();
            }
        }

        /// <summary>
        /// This function deletes the selected system from both the System and SysApp tables.
        /// It will also delete the system files from the optimized folder
        /// </summary>
        /// <param name="systemID">The file_id that needs to be dleted.</param>
        public void deleteSystem(int systemID) 
        {
            try
            {
                //this query deletes the system from the System table
                string query = "DELETE FROM System Where system_id = @system_id";
                using (SqlConnection connection = new SqlConnection(connectionString)) //connection for query
                using (SqlCommand command = new SqlCommand(query, connection)) //command to execute
                {
                    //open connection
                    connection.Open();

                    //sets the  system_id paramater in the query to the systemID variable 
                    command.Parameters.AddWithValue("@system_id", systemID);

                    //exacute the delete
                    command.ExecuteNonQuery();


                    //close connection
                    connection.Close();
                }
            }
            catch(SqlException ex) {

                //error message to show if error ocurs during delete
                MessageBox.Show("An error ocured when attempting to delete the system!");
            }


            try
            {
                //this query deletes the system from the SysApp table
                string query = "DELETE FROM SyApp Where system_id = @system_id";
                using (SqlConnection connection = new SqlConnection(connectionString)) //connection for query
                using (SqlCommand command = new SqlCommand(query, connection)) //command to execute
                {
                    //open connection
                    connection.Open();

                    //sets the  system_id paramater in the query to the suystemID variable 
                    command.Parameters.AddWithValue("@system_id", systemID);

                    //exacute the delete
                    command.ExecuteNonQuery();

                    //message to show the system was deleted
                    MessageBox.Show("Deleted...");


                    //close connection
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {

                //error message to show if error ocurs during delete
                MessageBox.Show("An error ocured when attempting to delete the system!");
            }

        }


        // Updates containers list box based on the selecteed index of the systems list box 
        private void listBoxSystems_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Put this here to avoid crashing if they dont select anything 
            if (listBoxSystems.SelectedItems.Count > 0)
            {
                appIDCollection.Clear();
                listBoxContainers.Items.Clear();
                GetApps(systemIDCollection[listBoxSystems.SelectedIndex]);
                listBoxFiles.Items.Clear();
            }

        }


        // Update files list box based on the selected index of the containers list box 
        private void listBoxContainers_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Put this here to avoid crashing if they dont select anything 
            if (listBoxContainers.SelectedItems.Count > 0)
            {
                listBoxFiles.Items.Clear();
                fileIDCollection.Clear();
                GetFileNames(appIDCollection[listBoxContainers.SelectedIndex]);
                GetFileIDS(appIDCollection[listBoxContainers.SelectedIndex]);
            }

        }


        private void listBoxFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxFiles.SelectedItems.Count > 0)
            {
               GetSharedApps(fileIDCollection[listBoxFiles.SelectedIndex], systemIDCollection[listBoxSystems.SelectedIndex]);
            }
        }
    }







}
