using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using System.Runtime.CompilerServices;

namespace Container_File_Optimizer
{



    public partial class NewSystem : Form
    {
        int currentVersionNumber = 0;
        string connectionString = ConfigurationManager.ConnectionStrings["Container_File_Optimizer.Properties.Settings.ContainerfileDatabaseConnectionString"].ConnectionString;

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



        private void AddApplicationSystemConnection(int app_id)
        {
            //get SQL connection and Command
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                int system_id;


                string query = "SELECT system_id FROM System WHERE system_name = @system_name AND system_creator = @system_creator AND version_number = @version_number";


                SqlCommand command = new SqlCommand(query, cnn);

                command.Parameters.AddWithValue("@system_name", textBoxSystemName.Text);
                command.Parameters.AddWithValue("@system_creator", textBoxCreator.Text);
                command.Parameters.AddWithValue("@version_number", currentVersionNumber);

                system_id = (int)command.ExecuteScalar();


                query = "INSERT INTO SysApp (system_id, app_id) VALUES (@system_id, @app_id)";



                command = new SqlCommand(query, cnn);

                command.Parameters.AddWithValue("@system_id", system_id);
                command.Parameters.AddWithValue("@app_id", app_id);

                command.ExecuteNonQuery();
                cnn.Close();
            }
        }


        private int GetSystemID()
        {
            int systemID = 0;
            //get SQL connection and Command
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                string query = "SELECT system_id FROM System WHERE system_name = @system_name AND system_creator = @system_creator AND version_number = @version_number";


                SqlCommand command = new SqlCommand(query, cnn);

                command.Parameters.AddWithValue("@system_name", textBoxSystemName.Text);
                command.Parameters.AddWithValue("@system_creator", textBoxCreator.Text);
                command.Parameters.AddWithValue("@version_number", currentVersionNumber);

                systemID = (int)command.ExecuteScalar();
                cnn.Close();
                return systemID;

            }

        }

        private void NewSystem_Load(object sender, EventArgs e)
        {
            PopulateContainers();

            // To Do : 
            // Make it so the NewSystem form shows up and this form hides, then when newsystem closes this form reappears
        }



        private void buttonCreateSystem_Click(object sender, EventArgs e)
        {
            //
            if (!(textBoxSystemName.Text == string.Empty))
            {
                CreateSystem();
                int systemID = GetSystemID();
                SystemViewer systemViewer = new SystemViewer();
                systemViewer.Show();
                foreach (String currentLine in checkedListBoxContainers.CheckedItems)
                {

                    // Gets the integer before space in listbox which happens to be the currentAppID
                    int currentSpace = currentLine.IndexOf(' ');
                    int currentAppID = Convert.ToInt32(currentLine.Substring(0, currentSpace));
                    // Create a system app connection in the database
                    AddSysAppConnection(currentAppID, systemID);
                }
                OptimizeSystem(systemID);
                this.Close();
            }
            else
            {
                MessageBox.Show("Please provide a system name to continue...");
            }

            
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


        /// <summary>
        /// Creates a new system with the given system name and creator name in the database.
        /// </summary>
        private void CreateSystem()
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Get the system name and creator name from the text boxes
                string system_name = textBoxSystemName.Text;
                string system_creator = textBoxCreator.Text;
                int version_number = 1;

                // Check if a system with the same system_name and system_creator already exists
                string query = "SELECT COUNT(*) FROM System WHERE system_name = @system_name AND system_creator = @system_creator";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@system_name", system_name);
                command.Parameters.AddWithValue("@system_creator", system_creator);

                int count = (int)command.ExecuteScalar();

                // If a system with the same system_name and system_creator already exists, increment the version_number number
                if (count > 0)
                {
                    query = "SELECT MAX(version_number) FROM System WHERE system_name = @system_name AND system_creator = @system_creator";
                    command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@system_name", system_name);
                    command.Parameters.AddWithValue("@system_creator", system_creator);
                    version_number = count + 1;
                }

                // Add the new system to the Systems table
                query = "INSERT INTO System (system_name, system_creator, version_number) VALUES (@system_name, @system_creator, @version_number)";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@system_name", system_name);
                command.Parameters.AddWithValue("@system_creator", system_creator);
                command.Parameters.AddWithValue("@version_number", version_number);
                currentVersionNumber = version_number;
                int rowsAffected = command.ExecuteNonQuery();

                connection.Close();
            }

        }


        /// <summary>
        /// Creates a new system-application connection record in the database.
        /// </summary>
        /// <param name="currentAppID">The ID of the current application.</param>
        /// <param name="systemID">The ID of the system to associate with the application.</param>
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




        /// <summary>
        /// Returns the count of systems in the database that match the specified system name and creator.
        /// </summary>
        /// <param name="cnn">The SQL connection to use.</param>
        /// <returns>The count of systems that match the specified name and creator.</returns>
        private int SystemCount(SqlConnection cnn)
        {

            // Create a SQL command to retrieve the count of systems that match the specified name and creator
            SqlCommand cmd = new SqlCommand("SELECT count(*) FROM System" +
                                                                                     "WHERE system_name = @currSystem AND system_creator = @currCreator", cnn);


            // Add parameters to the SQL command to match the specified system name and creator
            cmd.Parameters.AddWithValue("@currSystem", textBoxSystemName);
            cmd.Parameters.AddWithValue("@currCreator", textBoxSystemName);


            // Initialize a count variable to store the result
            int count = 0;
            // Execute the SQL command and retrieve the count of systems
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                // Iterate through the reader and increment the count for each row
                while (reader.Read())
                {
                    count++;
                }
            }
            // Return the count of systems that match the specified name and creator
            return count;
        }


        /// <summary>
        /// Populates a dictionary with app_id and associated file_ids for a given system_id.
        /// </summary>
        /// <param name="systemID">The system_id for which to retrieve the app_ids and associated file_ids.</param>
        /// <returns>A dictionary with app_id keys and associated file_id values.</returns>
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

        /// <summary>
        /// Helper Function that uses a join to get the number of times a given file apeares in a system.
        /// </summary>
        /// <param name="currentID">The file_id that needs to be counted.</param>
        /// <returns> The count for the number of times that id appeard.</returns>
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
            foreach (int appID in currentSystemCollection.Keys)
            {
                Dictionary<int, int> tempFileCounts = new Dictionary<int, int>(sortedFileCount);



                using (StreamWriter writer = new StreamWriter(GetAppName(appID) + ".app" + appID))
                {
                    writer.WriteLine("FROM ubi8:latest");
                    writer.WriteLine("");
                    writer.WriteLine("RUN useradd elvis && mkdir -p /home/elvis/lib \n");

                    sortedFileCount = WriteLibraries(tempFileCounts, currentSystemCollection, appID, writer);

                    sortedFileCount = WriteConfigs(tempFileCounts, currentSystemCollection, appID, writer);

                    sortedFileCount = WriteBinaries(tempFileCounts, currentSystemCollection, appID, writer);


                    writer.WriteLine("CMD[\"/bin/bash\"]");
                    writer.Close();

                }


                try
                {

                
                }
                catch (Exception ex)
                {
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
                    writer.WriteLine(GetFilePath(fileID) + " \\");
                    writer.WriteLine("/home/elvis/lib \n");
                    //tempFileCounts.Remove(fileID);

                }

            }
            if (!(!tempFileCounts.Any()))
            {
                writer.Write("COPY ");


                foreach (int fileID in tempFileCounts.Keys.ToList())
                {

                    string fileType = GetFileType(fileID).Trim();
                    if (currentSystemCollection[appID].Contains(fileID) && fileType == ".so" && tempFileCounts[fileID] <= 1)
                    {

                        writer.WriteLine(GetFilePath(fileID) + " \\");
                        //tempFileCounts.Remove(fileID);

                    }
                }

               // writer.WriteLine("/home/elvis/lib \n");
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
                    writer.WriteLine(GetFilePath(fileID) + " \\");
                    writer.WriteLine("/home/elvis/config \n");
                    //tempFileCounts.Remove(fileID);

                }

            }
            if (!(!tempFileCounts.Any()))
            {
                writer.Write("COPY ");


                foreach (int fileID in tempFileCounts.Keys.ToList())
                {

                    string fileType = GetFileType(fileID).Trim();
                    if (currentSystemCollection[appID].Contains(fileID) && fileType == ".config" && tempFileCounts[fileID] <= 1)
                    {

                        writer.WriteLine(GetFilePath(fileID) + " \\");
                        //tempFileCounts.Remove(fileID);

                    }
                }

                //writer.WriteLine("/home/elvis/config \n");
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
                    writer.WriteLine(GetFilePath(fileID) + " \\");
                    writer.WriteLine("/home/elvis/bin \n");
                    //tempFileCounts.Remove(fileID);

                }

            }
            if (!(!tempFileCounts.Any()))
            {
                writer.Write("COPY ");


                foreach (int fileID in tempFileCounts.Keys.ToList())
                {

                    string fileType = GetFileType(fileID).Trim();
                    if (currentSystemCollection[appID].Contains(fileID) && fileType == ".bin" && tempFileCounts[fileID] <= 1)
                    {

                        writer.WriteLine(GetFilePath(fileID) + " \\");
                       // tempFileCounts.Remove(fileID);

                    }
                }

                //writer.WriteLine("/home/elvis/bin \n");
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





















        private void ShowToolTip(object sender, EventArgs e)
        {
            String message = "Initialize System with above applications, and continue to the System Builder interface.";
            toolTipInfo.SetToolTip(buttonCreateSystem, message);
        }

        private void textBoxSystemName_MouseHover(object sender, EventArgs e)
        {
            String message = "Enter the name of the system to be created.";
            toolTipInfo.SetToolTip(textBoxSystemName, message);
        }

        private void textBoxCreator_MouseHover(object sender, EventArgs e)
        {
            String message = "Enter the name of the person responsible for the system.";
            toolTipInfo.SetToolTip(textBoxCreator, message);
        }

        private void listViewContainers_MouseHover(object sender, EventArgs e)
        {
            String message = "The list of containers to initialize the system with. \n To add more containers, click the 'Add Container' button below. \n To remove the selected container, click the 'Remove Container' button below.";
            toolTipInfo.SetToolTip(checkedListBoxContainers, message);
        }

        private void buttonAddContainer_MouseHover(object sender, EventArgs e)
        {
            String message = "Add a new container to the list of containers to initialize the new system with.";
            toolTipInfo.Show(message, buttonAddContainer);
        }
        private void buttonRemoveContainer_MouseHover(object sender, EventArgs e)
        {
            String message = "Remove selected container from the list of containers to initialize the new system with.";
            toolTipInfo.Show(message, buttonRemoveContainer);
        }

        private void textBoxSystemName_TextChanged(object sender, EventArgs e)
        {
            int current = textBoxSystemName.Text.Length;
            int max = textBoxSystemName.MaxLength;
            labelSystemNameCount.Text = current.ToString() + " / 32";
            this.Text = "Create System - " + textBoxSystemName.Text;

            if (current == max)
            {
                labelSystemNameCount.ForeColor = Color.Red;
            }
            else
            {
                labelSystemNameCount.ForeColor = Form.DefaultForeColor;
            }
            if (current == 0)
            {
                this.Text = "Create System - New System" + textBoxSystemName.Text;
            }
        }

        private void textBoxCreatorName_TextChanged(object sender, EventArgs e)
        {
            int current = textBoxCreator.Text.Length;
            int max = textBoxCreator.MaxLength;
            labelCreatorCount.Text = current.ToString() + " / 32";
            if (current == max)
            {
                labelCreatorCount.ForeColor = Color.Red;
            }
            else
            {
                labelCreatorCount.ForeColor = Form.DefaultForeColor;
            }

        }

    }
}
