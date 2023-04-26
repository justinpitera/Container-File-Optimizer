using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

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



        /// <summary>
        /// This method creates a new application record in the Application table of the database.
        /// </summary>
        /// <param name="appName">The name of the aplication being added.</param>
        /// <param name="appDescription">The description of the aplication being added.</param>
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
                    MessageBox.Show("An error ocured trying to add the aplication!");
                }
                finally
                {
                    // Close the database connection.
                    connection.Close();
                }
            }
        }




        /// <summary>
        /// This method creates a new file record in the [File] table of the database.
        /// </summary>
        /// <param name="filePath">The path of the file to be added.</param>
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
                    if (fileType == string.Empty)
                    {
                        command.Parameters.AddWithValue("@file_path", filePath);
                        command.Parameters.AddWithValue("@file_type", ".bin");
                        int rowsAffected = command.ExecuteNonQuery();
                    }
                    else if (fileType == "so")
                    {
                        command.Parameters.AddWithValue("@file_path", filePath);
                        command.Parameters.AddWithValue("@file_type", "." + fileType);
                        int rowsAffected = command.ExecuteNonQuery();
                    }
                    else
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
                    // Log any database errors.
                    MessageBox.Show("An error ocured trying to add the file!");
                }
                
            }
        }


        /// <summary>
        /// This method adds a connection between an application and a file in the database, based on the app ID and file path.
        /// </summary>
        /// <param name="appID">The id of the aplication the file will be connected to.</param>
        /// <param name="filePath">The path of the file being connected.</param>
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
                    // Log any database errors.
                    MessageBox.Show("An error ocured trying to add the file!");
                }
                
            }
        }

        /// <summary>
        /// This function finds the id of a given aplication.
        /// </summary>
        /// <returns> the id of the aplication being searched for</returns>
        private int GetAppID()
        {
            int appID = 0;

            // Create a SQL connection using the connection string.
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                try
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

                }
                catch (SqlException ex)
                {
                    // Log any database errors.
                    MessageBox.Show("An error ocured!");
                }
                
            }
            return appID;
        }


        /// <summary>
        /// Create app, then create each file from file list, then create connections in bridge table for apps and files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCreateContainer_Click(object sender, EventArgs e)
        {
            // Check if user has provided a name to the container and that at least one file has been added
            if (filesList.Items.Count > 0 || !(textBoxContainerName.Text == string.Empty))
            {
                CreateApp(textBoxContainerName.Text, textBoxContainerDesc.Text);
                // Create files from the list into Database table for Files
                foreach (String filePath in filesList.CheckedItems)
                {
                    CreateFile(filePath);
                }
                // Creating connections between files and app
                foreach (String filePath in filesList.CheckedItems)
                {
                    AddAppFileConnection(GetAppID(), filePath);
                }
                ContainerViewer containerViewer = new ContainerViewer();
                containerViewer.Show();
                this.Close();
            }
            else // Handle user not adding required information into the form
            {
                MessageBox.Show("Please select at least one file and provide a name for the container.");
            }
        }

        /// <summary>
        /// Add a file of list of files to add to container
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <summary>
        /// Opens a file dialog to add one or more files to the container.
        /// </summary>
        private void buttonAddFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.ShowDialog();

            // Get the currently selected items before adding new files
            List<string> selectedFiles = filesList.CheckedItems.Cast<string>().ToList();

            // Add new files to the list box and automatically select them
            foreach (string filePath in openFileDialog.FileNames)
            {
                if (filesList.Items.Contains(filePath))
                {
                    MessageBox.Show($"Error: file already exists in container: {filePath}", "Container File Optimizer");
                }
                else
                {
                    filesList.Items.Add(filePath, true); // Automatically select new items
                }
            }

            // Select the previously selected files and the new items
            for (int i = 0; i < filesList.Items.Count; i++)
            {
                if (selectedFiles.Contains(filesList.Items[i].ToString()))
                {
                    filesList.SetItemChecked(i, true);
                }
            }

            // If there are no items in the list box, select all by default
            if (filesList.Items.Count == 0)
            {
                for (int i = 0; i < openFileDialog.FileNames.Length; i++)
                {
                    filesList.SetItemChecked(i, true);
                }
            }
        }


        /// <summary>
        /// Remove a file from the list of files to add to the container
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRemoveFile_Click(object sender, EventArgs e)
        {
            // Determine if any files have been selected
            if (filesList.SelectedItems.Count > 0)
            {
                // Remove file
                filesList.Items.RemoveAt(filesList.SelectedIndex);
            }
            else
            {
                // Display error
                MessageBox.Show("Error: Must select a file to remove.");
            }

        }


        // This method is called when the text in the container name textbox changes
        private void textBoxContainer_TextChanged(object sender, EventArgs e)
        {
            // Get the length of the current text in the textbox and the maximum allowed length
            int current = textBoxContainerName.Text.Length;
            int max = textBoxContainerName.MaxLength;

            // Update the label to show the current and maximum lengths of the text
            labelSystemNameCount.Text = current.ToString() + " / 50";

            // If the length of the text equals the maximum allowed length, change the color of the label to red
            if (current == max)
            {
                labelSystemNameCount.ForeColor = Color.Red;
            }
            else
            {
                labelSystemNameCount.ForeColor = Color.White;
            }
        }

        // This method is called when the text in the container description textbox changes
        private void textBoxContainerDesc_TextChanged(object sender, EventArgs e)
        {
            // Get the length of the current text in the textbox and the maximum allowed length
            int current = textBoxContainerDesc.Text.Length;
            int max = textBoxContainerDesc.MaxLength;

            // Update the label to show the current and maximum lengths of the text
            labelCreatorCount.Text = current.ToString() + " / 50";

            // If the length of the text equals the maximum allowed length, change the color of the label to red
            if (current == max)
            {
                labelCreatorCount.ForeColor = Color.Red;
            }
            else
            {
                labelCreatorCount.ForeColor = Color.White;
            }
        }

        private void textBoxContainerName_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the key pressed is not the backspace key and is an invalid character for a file path
            if (e.KeyChar != '\b' && Path.GetInvalidFileNameChars().Contains(e.KeyChar))
            {
                // Cancel the key press if it is an invalid character
                e.Handled = true;
            }
        }
    }
}
