using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Container_File_Optimizer
{

    public partial class NewSystem : Form
    {
        //variable to hold version number
        public int currentVersionNumber = 0;

        //variable to hold the connection string fr the database
        public string connectionString = ConfigurationManager.ConnectionStrings["Container_File_Optimizer.Properties.Settings.ContainerfileDatabaseConnectionString"].ConnectionString;

        //variable to hold the system path
        public string systemPath = "";


        public NewSystem()
        {
            InitializeComponent();
        }


        /// <summary>
        /// This function populates the container box with all of the containers saved in the application
        /// </summary>
        public void PopulateContainers()
        {
            string query = "SELECT * FROM Application"; //query to get all containers from the aplication table

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();//open connection


                    SqlDataReader reader = command.ExecuteReader(); //exucute query

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

                    connection.Close(); //close connection


                }
                catch (SqlException ex)
                {

                    //error message to show if error 
                    MessageBox.Show("An error ocured!");
                }

            }
        }


        /// <summary>
        /// This function gets the system ID using the information given in the text boxes
        /// </summary>
        /// <returns>system id</returns>
        private int GetSystemID()
        {
            int systemID = 0;

            // Establish connection to database and prepare SQL command
            using (SqlConnection cnn = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("SELECT system_id FROM [System] WHERE system_name = @system_name AND system_creator = @system_creator AND version_number = @version_number", cnn))
            {
                try
                {
                    command.Parameters.AddWithValue("@system_name", textBoxSystemName.Text);
                    command.Parameters.AddWithValue("@system_creator", textBoxCreator.Text);
                    command.Parameters.AddWithValue("@version_number", currentVersionNumber);

                    // Open database connection, execute the SQL command, and retrieve the system ID
                    cnn.Open();
                    systemID = (int)command.ExecuteScalar();
                    cnn.Close();
                }
                catch (SqlException ex)
                {

                    //error message to show if error ocurs 
                    MessageBox.Show("An error ocured!");
                }

            }

            return systemID;
        }


        private void NewSystem_Load(object sender, EventArgs e)
        {
            PopulateContainers();

        }


        /// <summary>
        /// This function is called when the create system button is clicked and performs all of the function calls to create a system
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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


        /// <summary>
        /// This function removes a container when the remove container button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRemoveContainer_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you would like to remove: " + checkedListBoxContainers.SelectedItems.ToString() + "?", "Confirmation of removal", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                MessageBox.Show("Removed: " + checkedListBoxContainers.SelectedItems.ToString());
            }
            else
            {
                // Do nothing
                MessageBox.Show("No changes made");
            }
        }

        /// <summary>
        /// This function uses a query to create a system in the database
        /// </summary>
        private void CreateSystem()
        {

            int versionNumber;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string systemName = textBoxSystemName.Text;
                    string creatorName = textBoxCreator.Text;
                    versionNumber = 1;

                    // Check if a system with the same systemName and creatorName already exists
                    string checkQuery = "SELECT COUNT(*) FROM System WHERE system_name = @systemName";
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
                catch (SqlException ex)
                {

                    //error message to show if error ocurs while creting a system
                    MessageBox.Show("An error ocured while creating a system!");
                }

            }
        }

        /// <summary>
        /// This function is called after creating a system and adds all of the connections between the system and selected containers
        /// to the SysApp table
        /// </summary>
        /// <param name="currentAppID">id of the container being connected to</param>
        /// <param name="systemID">id of the system being connected to</param>
        private void AddSysAppConnection(int currentAppID, int systemID)
        {
            // Create a SQL command to insert a new system-application connection record
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO SysApp (system_id, app_id) VALUES (@system_id, @app_id)", sqlConnection))
            {
                try
                {
                    // Add parameters to the SQL command to specify the system and application IDs
                    sqlCommand.Parameters.AddWithValue("@system_id", systemID);
                    sqlCommand.Parameters.AddWithValue("@app_id", currentAppID);

                    // Open the database connection, execute the SQL command, and close the connection
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();

                }
                catch (SqlException ex)
                {

                    //error message to show if error ocurs 
                    MessageBox.Show("An error ocured!");
                }

            }
        }

        /// <summary>
        /// This function creates the system collection for ue with optimization
        /// </summary>
        /// <param name="systemID"></param>
        /// <returns>currentSystemCollection</returns>
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
                try
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
                catch (SqlException ex)
                {

                    //error message to show if error 
                    MessageBox.Show("An error ocured!");
                }

            }

            // Return the dictionary with the app_id and associated file_ids
            return currentSystemCollection;
        }



        /// <summary>
        /// This function begins the proccess for optimizing a system
        /// </summary>
        /// <param name="systemID">The id of the system to optimize</param>
        public void OptimizeSystem(int systemID)
        {
            Dictionary<int, List<int>> currentSystemCollection = PopulateSystemCollection(systemID); //collection holds the system ids

            Dictionary<int, int> fileCount = new Dictionary<int, int>();//Temporary collection to hold the file counts for each system id

            //loop through each appid in in the collection and get its counts.
            foreach (int appID in currentSystemCollection.Keys)
            {
                //get each file id in the curent system colleection
                List<int> fileIDs = currentSystemCollection[appID];
                foreach (int currentID in fileIDs)
                {
                    int currentCount = GetFileCount(currentID, systemID); //get the count of the current file
                    if (fileCount.Keys.Contains(currentID))
                    {
                        // Dont add to list
                        continue;
                    }
                    else
                    {
                        fileCount.Add(currentID, currentCount); //else add to the list
                    }
                }
            }

            // Converts the Dictionary into a List, sorts the List, then converts it back into a Dictionary.
            List<KeyValuePair<int, int>> fileCounts = new List<KeyValuePair<int, int>>();
            var sortedList = fileCount.ToList();
            sortedList.Sort((x, y) => y.Value.CompareTo(x.Value));

            //converts back to dictiniary
            Dictionary<int, int> sortedFileCount = new Dictionary<int, int>();
            foreach (var keyValuePair in sortedList)
            {
                sortedFileCount.Add(keyValuePair.Key, keyValuePair.Value);
            }

            GenerateOptimizedFiles(currentSystemCollection, sortedFileCount); //call the function to generate the optimized files

        }


        /// <summary>
        /// This function return the version number of a given system
        /// </summary>
        /// <param name="system_id">the system id</param>
        /// <returns>version number</returns>
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

                    //error message to show if error 
                    MessageBox.Show("An error ocured!");
                }

            }

            return versionNumber + 1;
        }

        /// <summary>
        /// This function returns the file count of a given file
        /// </summary>
        /// <param name="currentID">the file id</param>
        /// <param name="systemID">the system id</param>
        /// <returns></returns>
        public int GetFileCount(int currentID, int systemID)
        {

            //variable to hold the file count
            int fileCount;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    //query to get the file count by joining Appfile and SysApp tables in the database
                    string query = "SELECT COUNT(*) FROM AppFile ap LEFT JOIN SysApp sa ON ap.app_id = sa.app_id WHERE ap.file_id = @file_id AND sa.system_id = @system_id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@file_id", currentID);
                    command.Parameters.AddWithValue("@system_id", systemID);
                    fileCount = (int)command.ExecuteScalar();
                    connection.Close();

                    return fileCount; //return file count
                }
                catch (SqlException ex)
                {

                    //error message to show if error 
                    MessageBox.Show("An error ocured!");
                    return -1;
                }
            }

        }

        /// <summary>
        /// This function generates all of the optimized.app files for the given container
        /// </summary>
        /// <param name="currentSystemCollection">the current system collection</param>
        /// <param name="sortedFileCount">the count of all of the files</param>
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
                        writer.WriteLine("CMD [\"/bin/bash\"]");
                    }
                }
                catch (Exception ex)
                {
                    // Show an error message if file creation fails
                    MessageBox.Show("Error creating optimized file: " + ex);
                }
            }
        }



        /// <summary>
        /// This function writes all of the library files to the optimized container file
        /// </summary>
        /// <param name="tempFileCounts">the file conts</param>
        /// <param name="currentSystemCollection">the collection of all of the systems</param>
        /// <param name="appID">the current container id</param>
        /// <param name="writer">a writer to write to the file</param>
        /// <returns></returns>
        public Dictionary<int, int> WriteLibraries(Dictionary<int, int> tempFileCounts, Dictionary<int, List<int>> currentSystemCollection, int appID, StreamWriter writer)
        {
            //first write out all of the files that have a count greater than 1
            foreach (int fileID in tempFileCounts.Keys.ToList())
            {
                string fileType = GetFileType(fileID).Trim();//get file type

                //if count is greater than 1 than write the file to its own copy statement
                if (currentSystemCollection[appID].Contains(fileID) && fileType == ".so" && tempFileCounts[fileID] > 1)
                {
                    //write out the copy statement
                    writer.Write("COPY ");
                    string fileName = Path.GetFileName(GetFilePath(fileID));
                    writer.WriteLine("\t./lib/" + fileName + " \\");
                    writer.WriteLine("\t/home/" + textBoxCreator.Text + "/lib \n");

                }

            }
            if (!(!tempFileCounts.Any())) //check if there are any remaining library files
            {


                bool containsCopy = false;

                //write out each library file that only appears in one container in one grouped copy statement
                foreach (int fileID in tempFileCounts.Keys.ToList())
                {
                    string fileType = GetFileType(fileID).Trim();//get file extension

                    //if the file count equalls one then add to the copy
                    if (currentSystemCollection[appID].Contains(fileID) && fileType == ".so" && tempFileCounts[fileID] <= 1)
                    {
                        //write out the first line to be copy
                        if (!containsCopy)
                        {
                            //write the copy statment
                            writer.Write("COPY ");
                            containsCopy = true;
                        }
                        string fileName = Path.GetFileName(GetFilePath(fileID));
                        writer.WriteLine("\t./lib/" + fileName + " \\");

                    }

                }

                //if the copy was added then write out the home directory 
                if (containsCopy == true)
                {
                    writer.WriteLine("\t/home/" + textBoxCreator.Text + "/lib \n");
                }

            }
            return tempFileCounts;//return file counts
        }

        /// <summary>
        /// This function writes all of the conig files to the optimized container file
        /// </summary>
        /// <param name="tempFileCounts">the file conts</param>
        /// <param name="currentSystemCollection">the collection of all of the systems</param>
        /// <param name="appID">the current container id</param>
        /// <param name="writer">a writer to write to the file</param>
        /// <returns></returns>
        public Dictionary<int, int> WriteConfigs(Dictionary<int, int> tempFileCounts, Dictionary<int, List<int>> currentSystemCollection, int appID, StreamWriter writer)
        {
            //first write out all of the files that have a count greater than 1
            foreach (int fileID in tempFileCounts.Keys.ToList())
            {
                string fileType = GetFileType(fileID).Trim();//get file type
                if (currentSystemCollection[appID].Contains(fileID) && fileType == ".config" && tempFileCounts[fileID] > 1)
                {
                    //write out the copy statement
                    writer.Write("COPY ");
                    string fileName = Path.GetFileName(GetFilePath(fileID));
                    writer.WriteLine("\t./config/" + fileName + " \\");
                    writer.WriteLine("\t/home/" + textBoxCreator.Text + "/config \n");
                }

            }
            if (!(!tempFileCounts.Any()))//check if there are any remaining config files
            {
                bool containsCopy = false;

                //write out each config file that only appears in one container in one grouped copy statement
                foreach (int fileID in tempFileCounts.Keys.ToList())
                {

                    string fileType = GetFileType(fileID).Trim();
                    if (currentSystemCollection[appID].Contains(fileID) && fileType == ".config" && tempFileCounts[fileID] <= 1)
                    {
                        if (!containsCopy)
                        {
                            //write out the first line to be copy
                            writer.Write("COPY ");
                            containsCopy = true;
                        }

                        string fileName = Path.GetFileName(GetFilePath(fileID));
                        writer.WriteLine("\t./config/" + fileName + " \\");

                    }
                }

                //if the copy was added then write out the home directory 
                if (containsCopy == true)
                {
                    writer.WriteLine("\t/home/" + textBoxCreator.Text + "/config \n");
                }

            }
            return tempFileCounts;//return the file collection
        }

        /// <summary>
        /// This function writes all of the binary files to the optimized container file
        /// </summary>
        /// <param name="tempFileCounts">the file conts</param>
        /// <param name="currentSystemCollection">the collection of all of the systems</param>
        /// <param name="appID">the current container id</param>
        /// <param name="writer">a writer to write to the file</param>
        /// <returns></returns>
        public Dictionary<int, int> WriteBinaries(Dictionary<int, int> tempFileCounts, Dictionary<int, List<int>> currentSystemCollection, int appID, StreamWriter writer)
        {
            //first write out all of the files that have a count greater than 1
            foreach (int fileID in tempFileCounts.Keys.ToList())
            {
                string fileType = GetFileType(fileID).Trim();//get file type
                if (currentSystemCollection[appID].Contains(fileID) && fileType == ".bin" && tempFileCounts[fileID] > 1)
                {
                    //write out the copy statement
                    writer.Write("COPY ");
                    string fileName = Path.GetFileName(GetFilePath(fileID));
                    writer.WriteLine("\t./bin/" + fileName + " \\");
                    writer.WriteLine("\t/home/" + textBoxCreator.Text + "/bin \n");

                }

            }
            if (!(!tempFileCounts.Any()))//check if there are any remaining binary files
            {
                bool containsCopy = false;

                //write out each binary file that only appears in one container in one grouped copy statement
                foreach (int fileID in tempFileCounts.Keys.ToList())
                {

                    string fileType = GetFileType(fileID).Trim();
                    if (currentSystemCollection[appID].Contains(fileID) && fileType == ".bin" && tempFileCounts[fileID] <= 1)
                    {
                        //write out the first line to be copy
                        if (!containsCopy)
                        {
                            writer.Write("COPY ");
                            containsCopy = true;
                        }
                        string fileName = Path.GetFileName(GetFilePath(fileID));
                        writer.WriteLine("\t./bin/" + fileName + " \\");

                    }
                }
                //if the copy was added then write out the home directory 
                if (containsCopy == true)
                {

                    writer.WriteLine("\t/home/" + textBoxCreator.Text + "/bin \n");
                }

            }
            return tempFileCounts;//return the file collection
        }

        /// <summary>
        /// Returns the fileName of a specific file from the database
        /// </summary>
        /// <param name="appID">the app id</param>
        /// <returns>file name</returns>
        private string GetAppName(int appID)
        {
            string file_name = "";
            //get SQL connection and Command
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                try
                {
                    cnn.Open();
                    string query = "SELECT app_name FROM Application WHERE app_id = @app_id";

                    SqlCommand command = new SqlCommand(query, cnn);

                    command.Parameters.AddWithValue("@app_id", appID);


                    file_name = (string)command.ExecuteScalar();
                    cnn.Close();
                    return file_name.Trim();

                }
                catch (SqlException ex)
                {

                    //error message to show if error 
                    MessageBox.Show("An error ocured!");
                    return file_name.Trim();
                }


            }
        }

        /// <summary>
        /// returns the specific filepath of a file in the database
        /// </summary>
        /// <param name="fileID">the file id</param>
        /// <returns>return file path</returns>
        private string GetFilePath(int fileID)
        {
            string file_path = "";
            //get SQL connection and Command
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                try
                {
                    cnn.Open();
                    string query = "SELECT file_path FROM [File] WHERE file_id = @file_id";

                    SqlCommand command = new SqlCommand(query, cnn);

                    command.Parameters.AddWithValue("@file_id", fileID);


                    file_path = (string)command.ExecuteScalar();
                    cnn.Close();
                    return file_path;


                }
                catch (SqlException ex)
                {

                    //error message to show if error 
                    MessageBox.Show("An error ocured!");
                    return file_path;
                }
            }
        }

        /// <summary>
        /// returns the specific filepath of a file in the database
        /// </summary>
        /// <param name="fileID">the file id</param>
        /// <returns>file type</returns>
        private string GetFileType(int fileID)
        {
            string file_type = "";
            //get SQL connection and Command
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                try
                {
                    cnn.Open();
                    string query = "SELECT file_type FROM [File] WHERE file_id = @file_id";

                    SqlCommand command = new SqlCommand(query, cnn);

                    command.Parameters.AddWithValue("@file_id", fileID);


                    file_type = (string)command.ExecuteScalar();
                    cnn.Close();
                    return file_type;


                }
                catch (SqlException ex)
                {

                    //error message to show if error 
                    MessageBox.Show("An error ocured!");
                    return file_type;
                }

            }
        }

        /// <summary>
        /// This method does not allow the user to enter anything into the System name textbox that would not be allowed in a File/Folder name 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxSystemName_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the key pressed is not the backspace key and is an invalid character for a file path
            if (e.KeyChar != '\b' && Path.GetInvalidFileNameChars().Contains(e.KeyChar))
            {
                // Cancel the key press if it is an invalid character
                e.Handled = true;
            }
        }

        /// <summary>
        /// This code is called when the text in the System Name text box is changed.
        /// It does not allow the user to enter more than 50 characters into the text box, if they reach 50 the count label turns red
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxSystemName_TextChanged(object sender, EventArgs e)
        {
            // The current amount of characters in the system name text box
            int current = textBoxSystemName.Text.Length;
            // The max amount of characters allowed in system name text box 
            int max = textBoxSystemName.MaxLength;
            // Update the label 
            labelSystemNameCount.Text = current.ToString() + " / 50";

            // If the current character count is at the max limit, set the color of the label to red
            if (current == max)
            {
                labelSystemNameCount.ForeColor = Color.Red;
            }
            // Otherwise, set the label to white again
            else
            {
                labelSystemNameCount.ForeColor = Color.White;
            }
        }

        private void textBoxCreatorName_TextChanged(object sender, EventArgs e)
        {
            // The current amount of characters in the Creator Name text box
            int current = textBoxCreator.Text.Length;
            // The max amount of characters allowed in the Creator Name text box 
            int max = textBoxCreator.MaxLength;
            // Update the label 
            labelCreatorCount.Text = current.ToString() + " / 50";

            // If the current character count is at the max limit, set the color of the label to red
            if (current == max)
            {
                labelCreatorCount.ForeColor = Color.Red;
            }
            // Otherwise, set the label to white again
            else
            {
                labelCreatorCount.ForeColor = Color.White;
            }
        }


        // Call the form to create a new container
        private void buttonAddContainer_Click(object sender, EventArgs e)
        {
            ContainerViewer newContainerViewerForm = new ContainerViewer();
            newContainerViewerForm.Show();
            this.Close();
        }


        // Call the form for the System Viewer
        private void buttonSystemViewer_Click(object sender, EventArgs e)
        {
            SystemViewer systemViewerForm = new SystemViewer();
            systemViewerForm.Show();
            this.Close();
        }
    }
}
