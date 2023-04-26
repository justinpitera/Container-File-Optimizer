using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace Container_File_Optimizer
{
    public partial class ContainerViewer : Form
    {
        // index[listbox], index [database]

        Dictionary<int, int> appIDCollection = new Dictionary<int, int>();
        Dictionary<int, int> fileIDCollection = new Dictionary<int, int>();
        string connectionString = ConfigurationManager.ConnectionStrings["Container_File_Optimizer.Properties.Settings.ContainerfileDatabaseConnectionString"].ConnectionString;

        public ContainerViewer()
        {
            InitializeComponent();
        }

        private void ContainerViewer_Load(object sender, EventArgs e)
        {
            ViewContainers();
        }


        /// <summary>
        /// This function populates the container viewer with all of the app names and stores their ids in a Dictionary 
        /// </summary>
        public void ViewContainers()
        {
            // Define SQL query to retrieve system_name and system_id columns
            string sqlQuery = "SELECT app_name, app_id FROM Application";


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
                        string applicationName = row["app_name"].ToString();
                        int applicationID = Convert.ToInt32(row["app_id"]);
                        listBoxContainerViewer.Items.Add(applicationName);
                        appIDCollection.Add(i, applicationID);
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
        /// <param name="appID">The application id that the fiels are connected to.</param>
        public void GetFileNames(int appID)
        {
            string query = "SELECT * FROM AppFile af LEFT JOIN [File] fi ON af.file_id = fi.file_id WHERE af.app_id = @app_id";

            try
            {
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
            catch (SqlException ex)
            {

                //error message to show if error ocurs during delete
                MessageBox.Show("An error ocured!");
            }
            
        }

        /// <summary>
        /// This function deletes the selected application from the Application, SysApp, and AppFile tables.
        /// </summary>
        /// <param name="aplicationID">The file_id that needs to be dleted.</param>
        public void deleteApplication(int aplicationID)
        {

            try
            {
                //this query deletes the aplication from the AppFile table
                string query = "DELETE FROM AppFile Where app_id = @app_id";
                using (SqlConnection connection = new SqlConnection(connectionString)) //connection for query
                using (SqlCommand command = new SqlCommand(query, connection)) //command to execute
                {
                    //open connection
                    connection.Open();

                    //sets the  app_id paramater in the query to the appID variable 
                    command.Parameters.AddWithValue("@app_id", aplicationID);

                    //exacute the delete
                    command.ExecuteNonQuery();



                    //close connection
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {

                //error message to show if error ocurs during delete
                MessageBox.Show("An error ocured when attempting to delete the Application!");
            }

            try
            {
                //this query deletes the aplication from the SysApp table
                string query = "DELETE FROM SysApp Where app_id = @app_id";
                using (SqlConnection connection = new SqlConnection(connectionString)) //connection for query
                using (SqlCommand command = new SqlCommand(query, connection)) //command to execute
                {
                    //open connection
                    connection.Open();

                    //sets the  app_id paramater in the query to the appID variable 
                    command.Parameters.AddWithValue("@app_id", aplicationID);

                    //exacute the delete
                    command.ExecuteNonQuery();


                    //close connection
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {

                //error message to show if error ocurs during delete
                MessageBox.Show("An error ocured when attempting to delete the Application!");
            }


            try
            {
                //this query deletes the application from the application table
                string query = "DELETE FROM Application Where app_id = @app_id";
                using (SqlConnection connection = new SqlConnection(connectionString)) //connection for query
                using (SqlCommand command = new SqlCommand(query, connection)) //command to execute
                {
                    //open connection
                    connection.Open();

                    //sets the  app_id paramater in the query to the appID variable 
                    command.Parameters.AddWithValue("@app_id", aplicationID);

                    //exacute the delete
                    command.ExecuteNonQuery();


                    //close connection
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {

                //error message to show if error ocurs during delete
                MessageBox.Show("An error ocured when attempting to delete the Application!");
            }
        }

        /// <summary>
        /// This function deletes the selected File from both the File and AppFile tables.
        /// It will also delete the system files from the optimized folder
        /// </summary>
        /// <param name="fileID">The file_id that needs to be dleted.</param>
        public void deleteFile(int fileID, int appID)
        {
            //variable to hold the file count
            int fileCount = getFileCount(fileID);

            try
            {
                //this query deletes the File from the AppFile table
                string query = "DELETE FROM AppFile Where file_id = @file_id AND app_id = @app_id";
                using (SqlConnection connection = new SqlConnection(connectionString)) //connection for query
                using (SqlCommand command = new SqlCommand(query, connection)) //command to execute
                {
                    //open connection
                    connection.Open();

                    //sets the  paramater in the query to the correct variable 
                    command.Parameters.AddWithValue("@file_id", fileID);
                    command.Parameters.AddWithValue("@app_id", appID);

                    //exacute the delete
                    command.ExecuteNonQuery();


                    //close connection
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {

                //error message to show if error ocurs during delete
                MessageBox.Show("An error ocured when attempting to delete the file!");
            }

            if (fileCount <= 1)
            {
                try
                {
                    //this query deletes the aplication from the AppFile table
                    string query = "DELETE FROM [File] WHERE file_id = @file_id";
                    using (SqlConnection connection = new SqlConnection(connectionString)) //connection for query
                    using (SqlCommand command = new SqlCommand(query, connection)) //command to execute
                    {
                        //open connection
                        connection.Open();

                        //sets the  file_id paramater in the query to the fileID variable 
                        command.Parameters.AddWithValue("@file_id", fileID);

                        //exacute the delete
                        command.ExecuteNonQuery();


                        //close connection
                        connection.Close();
                    }
                }
                catch (SqlException ex)
                {

                    //error message to show if error ocurs during delete
                    MessageBox.Show("An error ocured when attempting to delete the Application!");
                }


            }
        }
        /// <summary>
        /// This function return the count for the number of times a given file apears in AppFile
        /// </summary>
        /// <param name="fileID">The file_id to be counted.</param>
        public int getFileCount(int fileID)
        {
            //variable to hold the file count
            int fileCount = 0;

            try
            {
                //this query get the file countfrom the AppFile table
                string query = "SELECT COUNT(*) FROM AppFile WHERE file_id = @file_id";
                using (SqlConnection connection = new SqlConnection(connectionString)) //connection for query
                using (SqlCommand command = new SqlCommand(query, connection)) //command to execute
                {
                    //open connection
                    connection.Open();

                    //sets the  file_id paramater in the query to the fileID variable 
                    command.Parameters.AddWithValue("@file_id", fileID);

                    //exacute the command
                    fileCount = (int)command.ExecuteScalar();


                    //close connection
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {

                //error message to show if error ocurs during delete
                MessageBox.Show("An error ocured!");
            }

            return fileCount;
        }



        /// <summary>
        /// Gets all of the file that a application is conected to and returns their ids.
        /// </summary>
        /// <param name="appID">The the id of the aplication the files are connect to.</param>
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
        /// This function creates a new file record in the [File] table of the database.
        /// </summary>
        /// <param name="filePath">The file path of the file to be added to the database.</param>
        private void CreateFile(string filePath)
        {
            // Extract the file name and type from the file path.
            string fileName = Path.GetFileName(filePath);
            string pattern = @"(?<=\.)\w+$"; // matches the file extension after the dot
            Match match = Regex.Match(fileName, pattern);
            string fileType = match.Value;

            // Connect to the database.
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Define the SQL query for inserting a new file record.
                    string query = "INSERT INTO [File] (file_name, file_path, file_type) VALUES (@file_name, @file_path, @file_type)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@file_name", fileName);

                    // Determine the file type based on its content.
                    if (fileType == string.Empty) //If no extension file is a bin
                    {
                        command.Parameters.AddWithValue("@file_path", filePath);
                        command.Parameters.AddWithValue("@file_type", ".bin");
                        int rowsAffected = command.ExecuteNonQuery();
                    }
                    else if (fileType == "so") //if extension is so than file is a library
                    {
                        command.Parameters.AddWithValue("@file_path", filePath);
                        command.Parameters.AddWithValue("@file_type", "." + fileType);
                        int rowsAffected = command.ExecuteNonQuery();
                    }
                    else //any other extension is a config
                    {
                        command.Parameters.AddWithValue("@file_path", filePath);
                        command.Parameters.AddWithValue("@file_type", ".config");
                        int rowsAffected = command.ExecuteNonQuery();
                    }

                    // Close the database connection.
                    connection.Close();


                }
                catch (SqlException ex)
                {

                    //error message to show if error ocurs during delete
                    MessageBox.Show("An error ocured when adding the file!");
                }
               
            }
        }

        /// <summary>
        /// This method adds a connection between an application and a file in the database, based on the app ID and file path.
        /// </summary>
        /// <param name="appID">The application to connect the file to.</param>
        /// <param name="filePath">The file path of the file to be added to the database.</param>
        private void AddAppFileConnection(int appID, string filePath)
        {
            // Extract the file name from the file path using a regular expression.
            string fileName = Regex.Match(filePath, @"(?<=\\)[^\\]*$").Value;

            // Create a SQL connection using the connection string.
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                try
                {
                    cnn.Open();

                    // Declare a variable to store the file ID.
                    int fileID;

                    // Define the SQL query to retrieve the file ID based on the file name and path.
                    string query = "SELECT file_id FROM [File] WHERE file_name = @file_name AND file_path = @file_path";

                    // Create a SQL command using the query and connection.
                    SqlCommand command = new SqlCommand(query, cnn);

                    // Add parameters to the command to specify the file name and path.
                    command.Parameters.AddWithValue("@file_name", fileName);
                    command.Parameters.AddWithValue("@file_path", filePath);

                    // Execute the command and retrieve the file ID as a scalar value.
                    fileID = (int)command.ExecuteScalar();

                    // Define the SQL query to insert a new row into the AppFile table with the app ID and file ID.
                    query = "INSERT INTO AppFile (app_id, file_id) VALUES (@app_id, @file_id)";

                    // Create a new SQL command using the query and connection.
                    command = new SqlCommand(query, cnn);

                    // Add parameters to the command to specify the app ID and file ID.
                    command.Parameters.AddWithValue("@app_id", appID);
                    command.Parameters.AddWithValue("@file_id", fileID);

                    // Execute the command to insert the new row.
                    command.ExecuteNonQuery();

                    // Close the SQL connection.
                    cnn.Close();
                }
                catch (SqlException ex)
                {

                    //error message to show if error ocurs during delete
                    MessageBox.Show("An error ocured when adding the file!");
                }
                
            }
        }

        private void listBoxContainerViewer_SelectedIndexChanged(object sender, EventArgs e)
        {
            fileIDCollection.Clear();
            listBoxFiles.Items.Clear();
            if (listBoxContainerViewer.SelectedItems.Count > 0)
            {
                // To populate the list box
                GetFileNames(appIDCollection[listBoxContainerViewer.SelectedIndex]);
                // to populate the dictionary
                GetFileIDS(appIDCollection[listBoxContainerViewer.SelectedIndex]);
            }
        }
        private void buttonDeleteFile_Click(object sender, EventArgs e)
        {
            if (listBoxFiles.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("Are you sure you would like to remove: " + listBoxFiles.SelectedItem.ToString().Trim() + "?", "Confirmation of removal", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    MessageBox.Show("Deleted file: " + listBoxFiles.SelectedItem.ToString() + " from " + listBoxContainerViewer.SelectedItem.ToString());
                    deleteFile(fileIDCollection[listBoxFiles.SelectedIndex], appIDCollection[listBoxContainerViewer.SelectedIndex]);
                    //Clear the list box and id collection dictionary 
                    listBoxFiles.Items.Clear();
                    fileIDCollection.Clear();
                    // To populate the list box
                    GetFileNames(appIDCollection[listBoxContainerViewer.SelectedIndex]);
                    // to populate the dictionary
                    GetFileIDS(appIDCollection[listBoxContainerViewer.SelectedIndex]);
                }
                else
                {
                    // Do nothing
                    MessageBox.Show("No changes made");
                }

            }
            else
            {
                MessageBox.Show("Error: No file was selected to be deleted...");
            }
        }

        private void buttonDeleteContainer_Click(object sender, EventArgs e)
        {
            if (listBoxContainerViewer.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("Are you sure you would like to remove: " + listBoxContainerViewer.SelectedItem.ToString().Trim() + "?", "Confirmation of removal", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    // First delete all the files in that app if they only occur once which is done through the deleteFiles() function
                    for (int i = 0; i < listBoxFiles.Items.Count; i++)
                    {
                        deleteFile(fileIDCollection[i], appIDCollection[listBoxContainerViewer.SelectedIndex]);
                    }
                    // Next delete the empty container
                    deleteApplication(appIDCollection[listBoxContainerViewer.SelectedIndex]);
                    MessageBox.Show("Deleted container: " + listBoxContainerViewer.SelectedItem.ToString());

                    // Clear form objects and collections

                    appIDCollection.Clear();
                    listBoxContainerViewer.Items.Clear();

                    fileIDCollection.Clear();
                    listBoxFiles.Items.Clear();

                    // Populate list box and app ids collection dictionary
                    ViewContainers();
                }
                else
                {
                    // Do nothing
                    MessageBox.Show("No changes made");
                }

            }
            else
            {
                MessageBox.Show("Error: No container was selected to be deleted...");
            }




        }

        private void buttonNewContainer_Click(object sender, EventArgs e)
        {
            this.Close();
            NewContainer newContainerForm = new NewContainer();
            newContainerForm.Show();
        }
    }
}
