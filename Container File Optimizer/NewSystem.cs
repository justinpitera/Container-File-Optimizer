using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;

namespace Container_File_Optimizer
{



    public partial class NewSystem : Form
    {
        public int currentVersionNumber = 0;
        public string connectionString = ConfigurationManager.ConnectionStrings["Container_File_Optimizer.Properties.Settings.ContainerfileDatabaseConnectionString"].ConnectionString;
        public string systemPath = "";




        public NewSystem()
        {
            InitializeComponent();
        }


        public void PopulateContainers()
        {
            string query = "SELECT * FROM Application";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();


                SqlDataReader reader = command.ExecuteReader();

                // Iterate through the SqlDataReader and populate the CheckedListBox
                while (reader.Read())
                {
                    int app_id = reader.GetInt32(0); // see if this can work with Int16 
                    string app_name = reader.GetString(1);
                    string app_desc = reader.GetString(2);
                    checkedListBoxContainers.DisplayMember = "Name";
                    checkedListBoxContainers.ValueMember = "Id";
                    checkedListBoxContainers.Items.Add(app_id + " - " + app_name);
                }

                // Close the SqlDataReader and the SqlConnection
                reader.Close();

                connection.Close();
            }
        }


        private int GetSystemID()
        {
            int systemID = 0;

            // Establish connection to database and prepare SQL command
            using (SqlConnection cnn = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("SELECT system_id FROM [System] WHERE system_name = @system_name AND system_creator = @system_creator AND version_number = @version_number", cnn))
            {
                command.Parameters.AddWithValue("@system_name", textBoxSystemName.Text);
                command.Parameters.AddWithValue("@system_creator", textBoxCreator.Text);
                command.Parameters.AddWithValue("@version_number", currentVersionNumber);

                // Open database connection, execute the SQL command, and retrieve the system ID
                cnn.Open();
                systemID = (int)command.ExecuteScalar();
                cnn.Close();
            }

            return systemID;
        }


        private void NewSystem_Load(object sender, EventArgs e)
        {
            PopulateContainers();

        }



        private void buttonCreateSystem_Click(object sender, EventArgs e)
        {
            // Check if required fields are filled in and at least one container is selected
            if (textBoxSystemName.Text.Trim() == string.Empty || textBoxCreator.Text.Trim() == string.Empty || checkedListBoxContainers.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please provide a system name, creator, and select at least one container to continue...");
                return;
            }

            // Create system in the database and get system ID
            CreateSystem();
            int systemID = GetSystemID();

            // Create system-app connections in the database for each selected container
            foreach (string currentLine in checkedListBoxContainers.CheckedItems)
            {
                // Extract the current app ID from the selected item
                int currentAppID;
                if (int.TryParse(Regex.Match(currentLine, @"^\d+").Value, out currentAppID))
                {
                    // Create system-app connection in the database
                    AddSysAppConnection(currentAppID, systemID);
                }
            }

            // Optimize the system
            OptimizeSystem(systemID);

            // Show a success message and open the system viewer
            MessageBox.Show("Created and optimized system: " + textBoxSystemName.Text);
            SystemViewer systemViewer = new SystemViewer();
            systemViewer.Show();

            // Close the current form
            this.Close();
        }


        private void buttonRemoveContainer_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you would like to remove: " + checkedListBoxContainers.SelectedItems.ToString() +  "?", "Confirmation of removal", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                MessageBox.Show("Removed: " + checkedListBoxContainers.SelectedItems.ToString());
            } else
            {
                // Do nothing
                MessageBox.Show("No changes made");
            }
        }

        private void CreateSystem()
        {
            
            int versionNumber;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string systemName = textBoxSystemName.Text;
                string creatorName = textBoxCreator.Text;
                versionNumber = 1;

                // Check if a system with the same systemName and creatorName already exists
                string checkQuery = "SELECT COUNT(*) FROM System WHERE system_name = @systemName AND system_creator = @creatorName";
                using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@systemName", systemName);
                    checkCommand.Parameters.AddWithValue("@creatorName", creatorName);
                    int count = (int)checkCommand.ExecuteScalar();

                    // If a system with the same systemName and creatorName already exists, increment the version number
                    if (count > 0)
                    {
                        string versionQuery = "SELECT MAX(version_number) FROM System WHERE system_name = @systemName AND system_creator = @creatorName";
                        using (SqlCommand versionCommand = new SqlCommand(versionQuery, connection))
                        {
                            versionCommand.Parameters.AddWithValue("@systemName", systemName);
                            versionCommand.Parameters.AddWithValue("@creatorName", creatorName);
                            versionNumber = count + 1;
                        }
                    }

                    // Add the new system to the Systems table
                    string insertQuery = "INSERT INTO System (system_name, system_creator, version_number, optimized_path) VALUES (@systemName, @creatorName, @versionNumber, @optimizedPath)";
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        systemPath = Application.StartupPath + "\\Systems\\" + textBoxSystemName.Text.Trim() + " Version " + versionNumber;
                        insertCommand.Parameters.AddWithValue("@systemName", systemName);
                        insertCommand.Parameters.AddWithValue("@creatorName", creatorName);
                        insertCommand.Parameters.AddWithValue("@versionNumber", versionNumber);
                        insertCommand.Parameters.AddWithValue("@optimizedPath", systemPath);
                        currentVersionNumber = versionNumber;
                        int rowsAffected = insertCommand.ExecuteNonQuery();
                    }
                    // Create the folder for the optimized system
                    Directory.CreateDirectory(systemPath);
                }

                connection.Close();
            }
        }


        private void AddSysAppConnection(int currentAppID, int systemID)
        {
            // Create a SQL command to insert a new system-application connection record
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO SysApp (system_id, app_id) VALUES (@system_id, @app_id)", sqlConnection))
            {
                // Add parameters to the SQL command to specify the system and application IDs
                sqlCommand.Parameters.AddWithValue("@system_id", systemID);
                sqlCommand.Parameters.AddWithValue("@app_id", currentAppID);

                // Open the database connection, execute the SQL command, and close the connection
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }


        public Dictionary<int, List<int>> PopulateSystemCollection(int systemID)
        {
            // Create an empty dictionary to store the app_id and associated file_ids
            Dictionary<int, List<int>> currentSystemCollection = new Dictionary<int, List<int>>();

            // SQL query to retrieve app_id for a given system_id
            string query = "SELECT app_id FROM SysApp WHERE system_id = @system_id";

            // Use a using block to ensure that resources are released even if an exception occurs
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Add system_id parameter to the SQL command
                command.Parameters.AddWithValue("@system_id", systemID);

                // Open database connection
                connection.Open();

                // Execute the SQL command and retrieve app_id values
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Iterate through the reader and add app_id to the dictionary
                    while (reader.Read())
                    {
                        int appID = reader.GetInt32(0);
                        currentSystemCollection.Add(appID, new List<int>());
                    }
                }

                // SQL query to retrieve file_id for a given app_id
                query = "SELECT file_id FROM AppFile WHERE app_id = @app_id";

                // Set the command text to the new query
                command.CommandText = query;

                // Clear the command parameters
                command.Parameters.Clear();

                // Iterate through the dictionary and add the file_ids for each app_id
                foreach (int appID in currentSystemCollection.Keys)
                {
                    // Add app_id parameter to the SQL command
                    command.Parameters.AddWithValue("@app_id", appID);

                    // Execute the SQL command and retrieve file_id values
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Iterate through the reader and add the file_id to the list for the current app_id
                        while (reader.Read())
                        {
                            currentSystemCollection[appID].Add(reader.GetInt32(0));
                        }
                    }

                    // Clear the command parameters for the next iteration
                    command.Parameters.Clear();
                }

                // Close the database connection
                connection.Close();
            }

            // Return the dictionary with the app_id and associated file_ids
            return currentSystemCollection;
        }




        public void OptimizeSystem(int systemID)
        {
            Dictionary<int, List<int>> currentSystemCollection = PopulateSystemCollection(systemID);
            //Temporary
            Dictionary<int, int> fileCount = new Dictionary<int, int>();


            foreach (int appID in currentSystemCollection.Keys)
            {
                List<int> fileIDs = currentSystemCollection[appID];
                foreach (int currentID in fileIDs)
                {
                    int currentCount = GetFileCount(currentID, systemID);
                    if (fileCount.Keys.Contains(currentID))
                    {
                        // Dont add to list
                        continue;
                    }
                    else
                    {
                        fileCount.Add(currentID, currentCount);
                    }
                }
            }

            // wtaf
            List<KeyValuePair<int, int>> fileCounts = new List<KeyValuePair<int, int>>();
            var sortedList = fileCount.ToList();
            sortedList.Sort((x, y) => y.Value.CompareTo(x.Value));

            Dictionary<int, int> sortedFileCount = new Dictionary<int, int>();
            foreach (var keyValuePair in sortedList)
            {
                sortedFileCount.Add(keyValuePair.Key, keyValuePair.Value);
            }



            GenerateOptimizedFiles(currentSystemCollection, sortedFileCount);



        }


        public int  GetFileCount(int currentID, int systemID) {
            int fileCount;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM AppFile ap LEFT JOIN SysApp sa ON ap.app_id = sa.app_id WHERE ap.file_id = @file_id AND sa.system_id = @system_id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@file_id", currentID);
                command.Parameters.AddWithValue("@system_id", systemID);
                fileCount = (int)command.ExecuteScalar();
                connection.Close();
            }

            return fileCount;
        }









        private void GenerateOptimizedFiles(Dictionary<int, List<int>> currentSystemCollection, Dictionary<int, int> sortedFileCount)
        {
            // Loop through each app ID in the collection
            foreach (int appID in currentSystemCollection.Keys)
            {
                // Create a temporary copy of the sortedFileCount dictionary
                Dictionary<int, int> tempFileCounts = new Dictionary<int, int>(sortedFileCount);

                try
                {
                    // Create a new file for the current app ID
                    string fileName = $"{GetAppName(appID)}.app{appID}";
                    string filePath = Path.Combine(Application.StartupPath, "Systems", $"{textBoxSystemName.Text} Version {currentVersionNumber}", fileName);

                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        // Write Dockerfile commands to install dependencies and create directories
                        writer.WriteLine("FROM ubi8:latest");
                        writer.WriteLine("");
                        writer.WriteLine($"RUN useradd {textBoxCreator.Text} && mkdir -p /home/{textBoxCreator.Text}/{{lib,config}} \n");

                        // Write libraries, configs, and binaries to the file, updating the file count dictionary
                        sortedFileCount = WriteLibraries(tempFileCounts, currentSystemCollection, appID, writer);
                        sortedFileCount = WriteConfigs(tempFileCounts, currentSystemCollection, appID, writer);
                        sortedFileCount = WriteBinaries(tempFileCounts, currentSystemCollection, appID, writer);

                        // Write the final command and close the file
                        writer.WriteLine("CMD[\"/bin/bash\"]");
                    }
                }
                catch (Exception ex)
                {
                    // Show an error message if file creation fails
                    MessageBox.Show("Error creating optimized file: " + ex);
                }
            }
        }




        public Dictionary<int, int> WriteLibraries(Dictionary<int, int> tempFileCounts, Dictionary<int, List<int>> currentSystemCollection, int appID, StreamWriter writer)
        {

            foreach (int fileID in tempFileCounts.Keys.ToList())
            {
                string fileType = GetFileType(fileID).Trim();
                if (currentSystemCollection[appID].Contains(fileID) && fileType == ".so" && tempFileCounts[fileID] > 1)
                {
                    writer.Write("COPY ");
                    string fileName = Path.GetFileName(GetFilePath(fileID));
                    writer.WriteLine("\t./lib/" + fileName + " \\");
                    writer.WriteLine("\t/home/" + textBoxCreator.Text + "/lib \n");
                    //tempFileCounts.Remove(fileID);

                }

            }
            if (!(!tempFileCounts.Any()))
            {


                bool containsCopy = false;
                foreach (int fileID in tempFileCounts.Keys.ToList())
                {

   


                    string fileType = GetFileType(fileID).Trim();
                    if (currentSystemCollection[appID].Contains(fileID) && fileType == ".so" && tempFileCounts[fileID] <= 1)
                    {
                        if (!containsCopy)
                        {
                            writer.Write("COPY ");
                            containsCopy = true;
                        }
                        string fileName = Path.GetFileName(GetFilePath(fileID));
                        writer.WriteLine("\t./lib/" + fileName + " \\");
                        //tempFileCounts.Remove(fileID);

                    }

                }

                if (containsCopy == true)
                {

                    writer.WriteLine("\t/home/" + textBoxCreator.Text + "/lib \n");
                }

            }
            return tempFileCounts;
        }







        public Dictionary<int, int> WriteConfigs(Dictionary<int, int> tempFileCounts, Dictionary<int, List<int>> currentSystemCollection, int appID, StreamWriter writer)
        {

            foreach (int fileID in tempFileCounts.Keys.ToList())
            {
                string fileType = GetFileType(fileID).Trim();
                if (currentSystemCollection[appID].Contains(fileID) && fileType == ".config" && tempFileCounts[fileID] > 1)
                {
                    writer.Write("COPY ");
                    string fileName = Path.GetFileName(GetFilePath(fileID));
                    writer.WriteLine("\t./config/" + fileName + " \\");
                    writer.WriteLine("\t/home/" + textBoxCreator.Text + "/config \n");
                    //tempFileCounts.Remove(fileID);

                }

            }
            if (!(!tempFileCounts.Any()))
            {
                bool containsCopy = false;

                    foreach (int fileID in tempFileCounts.Keys.ToList())
                    {

                    string fileType = GetFileType(fileID).Trim();
                    if (currentSystemCollection[appID].Contains(fileID) && fileType == ".config" && tempFileCounts[fileID] <= 1)
                    {
                        if (!containsCopy)
                        {
                            writer.Write("COPY ");
                            containsCopy = true;
                        }

                        string fileName = Path.GetFileName(GetFilePath(fileID));
                        writer.WriteLine("\t./config/" + fileName + " \\");
                        //tempFileCounts.Remove(fileID);

                    }
                }


                if (containsCopy == true)
                {
                    writer.WriteLine("\t/home/" + textBoxCreator.Text + "/config \n");
                }

            }
            return tempFileCounts;
        }



        public Dictionary<int, int> WriteBinaries(Dictionary<int, int> tempFileCounts, Dictionary<int, List<int>> currentSystemCollection, int appID, StreamWriter writer)
        {

            foreach (int fileID in tempFileCounts.Keys.ToList())
            {
                string fileType = GetFileType(fileID).Trim();
                if (currentSystemCollection[appID].Contains(fileID) && fileType == ".bin" && tempFileCounts[fileID] > 1)
                {
                    writer.Write("COPY ");
                    string fileName = Path.GetFileName(GetFilePath(fileID));
                    writer.WriteLine("\t./bin/" + fileName + " \\");
                    writer.WriteLine("\t/home/" + textBoxCreator.Text + "/bin \n");
                    //tempFileCounts.Remove(fileID);

                }

            }
            if (!(!tempFileCounts.Any()))
            {
                bool containsCopy = false;


                foreach (int fileID in tempFileCounts.Keys.ToList())
                {

                    string fileType = GetFileType(fileID).Trim();
                    if (currentSystemCollection[appID].Contains(fileID) && fileType == ".bin" && tempFileCounts[fileID] <= 1)
                    {
                        if (!containsCopy)
                        {
                            writer.Write("COPY ");
                            containsCopy = true;
                        }
                        string fileName = Path.GetFileName(GetFilePath(fileID));
                        writer.WriteLine("\t./bin/" + fileName + " \\");
                        // tempFileCounts.Remove(fileID);

                    }
                }
                if (containsCopy == true)
                {

                    writer.WriteLine("\t/home/" + textBoxCreator.Text + "/bin \n");
                }

            }
            return tempFileCounts;
        }








        // Returns the fileName of a specific file from the database
        private string GetAppName(int appID)
        {
            string file_name = "";
            //get SQL connection and Command
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                string query = "SELECT app_name FROM Application WHERE app_id = @app_id";

                SqlCommand command = new SqlCommand(query, cnn);

                command.Parameters.AddWithValue("@app_id", appID);


                file_name = (string)command.ExecuteScalar();
                cnn.Close();
                return file_name.Trim();

            }
        }

        // returns the specific filepath of a file in the database
        private string GetFilePath(int fileID)
        {
            string file_path = "";
            //get SQL connection and Command
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                string query = "SELECT file_path FROM [File] WHERE file_id = @file_id";

                SqlCommand command = new SqlCommand(query, cnn);

                command.Parameters.AddWithValue("@file_id", fileID);


                file_path = (string)command.ExecuteScalar();
                cnn.Close();
                return file_path;

            }
        }



        // returns the specific filepath of a file in the database
        private string GetFileType(int fileID)
        {
            string file_type = "";
            //get SQL connection and Command
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                string query = "SELECT file_type FROM [File] WHERE file_id = @file_id";

                SqlCommand command = new SqlCommand(query, cnn);

                command.Parameters.AddWithValue("@file_id", fileID);


                file_type = (string)command.ExecuteScalar();
                cnn.Close();
                return file_type;

            }
        }



        private void textBoxSystemName_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the key pressed is not the backspace key and is an invalid character for a file path
            if (e.KeyChar != '\b' && Path.GetInvalidFileNameChars().Contains(e.KeyChar))
            {
                // Cancel the key press if it is an invalid character
                e.Handled = true;
            }
        }


        private void textBoxSystemName_TextChanged(object sender, EventArgs e)
        {
            int current = textBoxSystemName.Text.Length;
            int max = textBoxSystemName.MaxLength;
            labelSystemNameCount.Text = current.ToString() + " / 50";

            if (current == max)
            {
                labelSystemNameCount.ForeColor = Color.Red;
            }
            else
            {
                labelSystemNameCount.ForeColor = Color.White;
            }
        }

        private void textBoxCreatorName_TextChanged(object sender, EventArgs e)
        {
            int current = textBoxCreator.Text.Length;
            int max = textBoxCreator.MaxLength;
            labelCreatorCount.Text = current.ToString() + " / 50";
            if (current == max)
            {
                labelCreatorCount.ForeColor = Color.Red;
            }
            else
            {
                labelCreatorCount.ForeColor = Color.White;
            }

        }

        private void buttonAddContainer_Click(object sender, EventArgs e)
        {
            ContainerViewer newContainerViewerForm = new ContainerViewer();
            newContainerViewerForm.Show();
            this.Close();
        }

        private void buttonSystemViewer_Click(object sender, EventArgs e)
        {
            SystemViewer systemViewerForm = new SystemViewer();
            systemViewerForm.Show();
            this.Close();
        }
    }
}
