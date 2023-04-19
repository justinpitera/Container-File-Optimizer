using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;
using System.Text.RegularExpressions;
using System.IO;

namespace Container_File_Optimizer
{

    public partial class NewContainer : Form
    {
        // database path
        string connectionString = ConfigurationManager.ConnectionStrings["Container_File_Optimizer.Properties.Settings.ContainerfileDatabaseConnectionString"].ConnectionString;

        public NewContainer()
        {
            InitializeComponent();
        }

        private void NewContainer_Load(object sender, EventArgs e)
        {
        }






        // This method creates a new application record in the Application table of the database.
        private void CreateApp(string appName, string appDescription)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Define the SQL query for inserting a new application record.
                string query = "INSERT INTO Application (app_name, app_desc) VALUES (@app_name, @app_desc)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@app_name", appName);
                command.Parameters.AddWithValue("@app_desc", appDescription);

                try
                {
                    // Open the database connection and execute the query.
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    // Check if the query was successful.
                    if (rowsAffected == 1)
                    {
                        Console.WriteLine("Application record created successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to create application record.");
                    }
                }
                catch (SqlException ex)
                {
                    // Log any database errors.
                    MessageBox.Show("Database error: " + ex);
                }
                finally
                {
                    // Close the database connection.
                    connection.Close();
                }
            }
        }




        // This method creates a new file record in the [File] table of the database.
        private void CreateFile(string filePath)
        {
            // Extract the file name and type from the file path.
            string fileName = Path.GetFileName(filePath);
            string fileType = Path.GetExtension(filePath);

            // Connect to the database.
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Define the SQL query for inserting a new file record.
                string query = "INSERT INTO [File] (file_name, file_path, file_type) VALUES (@file_name, @file_path, @file_type)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@file_name", fileName);

                // Determine the file type based on its content.
                if (FileHasNullBytes(filePath) && fileType != ".so")
                {
                    command.Parameters.AddWithValue("@file_path", filePath);
                    command.Parameters.AddWithValue("@file_type", ".bin");
                    int rowsAffected = command.ExecuteNonQuery();
                }
                else if (!FileHasNullBytes(filePath) && fileType != ".so")
                {
                    command.Parameters.AddWithValue("@file_path", filePath);
                    command.Parameters.AddWithValue("@file_type", ".config");
                    int rowsAffected = command.ExecuteNonQuery();
                }
                else
                {
                    command.Parameters.AddWithValue("@file_path", filePath);
                    command.Parameters.AddWithValue("@file_type", fileType);
                    int rowsAffected = command.ExecuteNonQuery();
                }

                // Close the database connection.
                connection.Close();
            }
        }




        // This method adds a connection between an application and a file in the database, based on the app ID and file path.
        private void AddAppFileConnection(int appID, string filePath)
        {
            // Extract the file name from the file path using a regular expression.
            string fileName = Regex.Match(filePath, @"(?<=\\)[^\\]*$").Value;

            // Create a SQL connection using the connection string.
            using (SqlConnection cnn = new SqlConnection(connectionString))
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
        }




        // This method retrieves the ID of an application from the database, based on its name and description.
        // It returns the app ID as an integer.
        private int GetAppID()
        {
            int appID = 0;

            // Create a SQL connection using the connection string.
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();

                // Define the SQL query to retrieve the app ID based on the app name and description.
                string query = "SELECT app_id FROM Application WHERE app_name = @app_name AND app_desc = @app_desc";

                // Create a SQL command using the query and connection.
                SqlCommand command = new SqlCommand(query, cnn);

                // Add parameters to the command to specify the app name and description.
                command.Parameters.AddWithValue("@app_name", textBoxContainerName.Text);
                command.Parameters.AddWithValue("@app_desc", textBoxContainerDesc.Text);

                // Execute the command and retrieve the app ID as a scalar value.
                appID = (int)command.ExecuteScalar();

                // Close the SQL connection.
                cnn.Close();

                // Return the app ID.
                return appID;
            }
        }


        // Create app, then create each file from file list, then create connections in bridge table for apps and files
        private void buttonCreateContainer_Click(object sender, EventArgs e)
        {

            if (checkedListBoxFiles.Items.Count == 0 || textBoxContainerName.Text == string.Empty)
            {
                MessageBox.Show("Please select at least one file and provide a name for the container.");
            }
            else
            {
                CreateApp(textBoxContainerName.Text, textBoxContainerDesc.Text);
                // Create files from the list into Database table for Files
                foreach (String filePath in checkedListBoxFiles.CheckedItems)
                {
                    CreateFile(filePath);
                }
                // Creating connections between files and app
                foreach (String filePath in checkedListBoxFiles.CheckedItems)
                {
                    AddAppFileConnection(GetAppID(), filePath);
                }
                this.Close();
            }



        }

        // Add a file of list of files to add to container
        private void buttonAddFile_Click(object sender, EventArgs e)
        {
            // Windows explorer open file dialog with multiselect enabled to allow multiple files
            OpenFileDialog findFiles = new OpenFileDialog();
            findFiles.Multiselect = true;

            findFiles.ShowDialog();


            // For each file in the open file dialog selected
            foreach (String filePath in findFiles.FileNames)
            {
                // Check if file is already in the list based on its file path
                if (!checkedListBoxFiles.Items.Contains(filePath))
                {
                    // Add file
                    checkedListBoxFiles.Items.Add(filePath);
                }
                else
                {
                    // Display error
                    MessageBox.Show("Error: file already exists in container: " + filePath, "Container File Optimizer");
                }
            }
            // by default, set all items to checked
            for (int i = 0; i < checkedListBoxFiles.Items.Count; i++)
            {
                checkedListBoxFiles.SetItemChecked(i, true);
            }

        }

        // Remove a file from the list of files to add to the container
        private void buttonRemoveFile_Click(object sender, EventArgs e)
        {
            // Determine if any files have been selected
            if (checkedListBoxFiles.SelectedItems.Count > 0)
            {
                // Remove file
                checkedListBoxFiles.Items.RemoveAt(checkedListBoxFiles.SelectedIndex);
            }
            else
            {
                // Display error
                MessageBox.Show("Error: Must select a file to remove.");
            }
            
        }

        // Used to determine if file is an executable or not
        public static bool FileHasNullBytes(string filePath)
        {
            // Open the file as a binary stream
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                int byteValue;
                // Read the file byte by byte
                while ((byteValue = fileStream.ReadByte()) != -1)
                {
                    // Check if the byte value is 0 (a null byte)
                    if (byteValue == 0)
                    {
                        // If a null byte is found, return true
                        return true;
                    }
                }
            }
            // If no null bytes are found, return false
            return false;
        }


    }
}
