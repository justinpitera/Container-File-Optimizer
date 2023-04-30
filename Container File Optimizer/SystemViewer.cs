using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
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

        /// <summary>
        /// This function returns the version number of a given system
        /// </summary>
        /// <param name="system_id"> The id of the system whose version number we are finding</param>
        /// <returns>the version number for the system</returns>
        public int GetVersionNumber(int system_id)
        {
            int versionNumber = 0;
            // SQL query to retrieve version number from the System table where system_id matches the provided parameter value
            string sqlQuery = "SELECT version_number FROM System WHERE system_id = @system_id";

            // Create a SqlConnection object and open the connection to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Create a SqlCommand object with the SQL query and the SqlConnection object
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        // Add a parameter for the system_id value to the SqlCommand object
                        command.Parameters.AddWithValue("@system_id", system_id);

                        // Execute the SQL query and retrieve the version number using the ExecuteScalar method
                        object result = command.ExecuteScalar();
                        // Convert the result to an int and assign it to the versionNumber variable
                        versionNumber = Convert.ToInt32(result);
                    }

                }
                catch (SqlException ex)
                {

                    //error message to show if error ocurs during delete
                    MessageBox.Show("An error ocured!");
                }

            }

            return versionNumber;
        }



        /// <summary>
        /// Gets all of the systems within the application to display in the system viewer
        /// </summary>
        public void ViewSystems()
        {
            // Define SQL query to retrieve system_name and system_id columns
            string sqlQuery = "SELECT system_name, system_id FROM System";

            // Create SqlConnection and SqlCommand objects
            using (SqlConnection cnn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
            {
                try
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
                        systemIDCollection.Add(i, systemId);
                        systemsList.Items.Add(systemName.Trim() + " Version " + GetVersionNumber(systemId));

                    }


                }
                catch (SqlException ex)
                {

                    //error message to show if error ocurs during delete
                    MessageBox.Show("An error ocured!");
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
                try
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
                        containersList.Items.Add(app_name);
                        appIDCollection.Add(i, app_ID);
                    }
                }
                catch (SqlException ex)
                {

                    //error message to show if error ocurs during delete
                    MessageBox.Show("An error ocured!");
                }

            }
        }

        /// <summary>
        /// Gets all of the apps that a file is contained in and returns their ids.
        /// </summary>
        /// <param name="appID">The id of the app the files are connected to</param>
        public void GetFileIDS(int appID)
        {
            // Define SQL query to retrieve system_name and system_id columns

            //string query = "SELECT * FROM [File] fi LEFT JOIN AppFile af ON fi.file_id = af.file_id WHERE sa.system_id = @file_id";
            string query = "SELECT file_id FROM AppFile WHERE app_id = @app_id";

            // Create SqlConnection and SqlCommand objects
            using (SqlConnection cnn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, cnn))
            {
                try
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
                catch (SqlException ex)
                {

                    //error message to show if error ocurs during delete
                    MessageBox.Show("An error ocured!");
                }

            }


        }

        /// <summary>
        /// Gets the file names from the application ID. This is used to populate the files listbox
        /// </summary>
        /// <param name="appID"></param>
        public void GetFileNames(int appID)
        {
            string query = "SELECT * FROM AppFile af LEFT JOIN [File] fi ON af.file_id = fi.file_id WHERE af.app_id = @app_id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
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
                        filesList.Items.Add(file_name);

                    }

                    // Close the SqlDataReader and the SqlConnection
                    reader.Close();

                    connection.Close();

                }
                catch (SqlException ex)
                {

                    //error message to show if error ocurs during delete
                    MessageBox.Show("An error ocured!");
                }

            }
        }

        /// <summary>
        /// Gets the list of files that an application (container) contains
        /// </summary>
        /// <param name="appID"> the id of the aplication the containers areconnected to</param>
        public void GetFiles(int appID)
        {
            string query = "SELECT * FROM AppFile WHERE app_id = @app_id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@app_id", appID);

                    SqlDataReader reader = command.ExecuteReader();

                    // Iterate through the SqlDataReader and populate the CheckedListBox
                    while (reader.Read())
                    {
                        int app_id = reader.GetInt32(0); // see if this can work with Int16 
                        int file_id = reader.GetInt32(1); // Same with this 
                        filesList.Items.Add(file_id);

                    }

                    // Close the SqlDataReader and the SqlConnection
                    reader.Close();

                    connection.Close();
                }
                catch (SqlException ex)
                {

                    //error message to show if error ocurs during delete
                    MessageBox.Show("An error ocured!");
                }

            }
        }

        /// <summary>
        /// Get the list of shared apps that a certain file ID shares within a system
        /// </summary>
        /// <param name="fileID">The id of the file that the applications are connected to</param>
        /// <param name="systemID"> The id of the system the files are connected to</param>
        public void GetSharedApps(int fileID, int systemID)
        {
            string query = "SELECT app_name FROM Application ap LEFT JOIN SysApp sa ON ap.app_id = sa.app_id LEFT JOIN AppFile af ON af.app_id = ap.app_id WHERE sa.system_id = @system_id AND af.file_id = @file_id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@file_id", fileID);
                    command.Parameters.AddWithValue("@system_id", systemID);

                    SqlDataReader reader = command.ExecuteReader();

                    // Iterate through the SqlDataReader and populate the CheckedListBox
                    List<string> listOfSharedContainers = new List<string>();
                    string fileName = filesList.GetItemText(filesList.SelectedItem);
                    while (reader.Read())
                    {
                        string app_name = reader.GetString(0);
                        listOfSharedContainers.Add(app_name);

                    }
                    // concatenate everything to list of Shared containers string

                    MessageBox.Show("List of shared containers for " + fileName + ":\n" + string.Join(Environment.NewLine, listOfSharedContainers));
                    // Close the SqlDataReader and the SqlConnection
                    reader.Close();

                    connection.Close();

                }
                catch (SqlException ex)
                {

                    //error message to show if error ocurs during delete
                    MessageBox.Show("An error ocured!");
                }

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
                //this query deletes the system from the SysApp table
                string query = "DELETE FROM SysApp Where system_id = @system_id";
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
                    MessageBox.Show("Deleted system: " + systemsList.SelectedItem.ToString());


                    //close connection
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {

                //error message to show if error ocurs during delete
                MessageBox.Show("An error ocured when attempting to delete the system!" + ex);
            }

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
            catch (SqlException ex)
            {

                //error message to show if error ocurs during delete
                MessageBox.Show("An error ocured when attempting to delete the system!" + ex);
            }
        }

        /// <summary>
        /// This function returns the optimized path of a given system
        /// </summary>
        /// <param name="system_id">The id of the system we are looking for the path for</param>
        /// <returns>The optimized path of the system</returns>
        public string GetOptimizedPath(int system_id)
        {
            string optimized_path = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT optimized_path FROM System WHERE system_id = @systemId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@systemId", system_id);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        optimized_path = reader["optimized_path"].ToString();
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("error:" + ex.ToString());
                }
            }

            return optimized_path;
        }

        /// <summary>
        /// Updates containers list box based on the selecteed index of the systems list box 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxSystems_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Put this here to avoid crashing if they dont select anything 
            if (systemsList.SelectedItems.Count > 0)
            {
                appIDCollection.Clear();
                containersList.Items.Clear();
                GetApps(systemIDCollection[systemsList.SelectedIndex]);
                filesList.Items.Clear();
            }

        }

        /// <summary>
        /// Update files list box based on the selected index of the containers list box 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxContainers_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Put this here to avoid crashing if they dont select anything 
            if (containersList.SelectedItems.Count > 0)
            {
                filesList.Items.Clear();
                fileIDCollection.Clear();
                // To populate the list box
                GetFileNames(appIDCollection[containersList.SelectedIndex]);
                // to populate the dictionary
                GetFileIDS(appIDCollection[containersList.SelectedIndex]);
            }

        }


        private void listBoxFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (filesList.SelectedItems.Count > 0)
            {
                GetSharedApps(fileIDCollection[filesList.SelectedIndex], systemIDCollection[systemsList.SelectedIndex]);
            }
        }

        private void buttonOpenContainerViewer_Click(object sender, EventArgs e)
        {
            ContainerViewer containerViewerForm = new ContainerViewer();
            containerViewerForm.Show();
            this.Close();
        }

        private void buttonShowAppDirectory_Click(object sender, EventArgs e)
        {
            Process.Start(Application.StartupPath + @"\\Systems");
        }

        private void buttonDeleteContainer_Click(object sender, EventArgs e)
        {

        }


        private void buttonNewContainer_Click(object sender, EventArgs e)
        {
            this.Close();
            NewContainer newContainerForm = new NewContainer();
            newContainerForm.Show();
        }

        private void buttonDeleteSystem_Click(object sender, EventArgs e)
        {
            if (systemsList.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("Are you sure you would like to remove: " + systemsList.SelectedItem.ToString().Trim() + "?", "Confirmation of removal", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Directory.Delete(GetOptimizedPath(systemIDCollection[systemsList.SelectedIndex]), true);
                    //Delete the system
                    deleteSystem(systemIDCollection[systemsList.SelectedIndex]);



                    systemsList.Items.Clear();
                    containersList.Items.Clear();
                    filesList.Items.Clear();
                    systemIDCollection.Clear();
                    ViewSystems();
                }
                else
                {
                    // Do nothing
                    MessageBox.Show("No changes made");
                }

            }
            else
            {
                MessageBox.Show("Error: No system was selected to be deleted.");
            }
        }

        private void buttonNewSystem_Click(object sender, EventArgs e)
        {
            this.Close();
            NewSystem newSystemForm = new NewSystem();
            newSystemForm.Show();
        }
    }
}
