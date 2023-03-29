﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Configuration;
using System.Collections;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices.ComTypes;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using static System.Windows.Forms.AxHost;

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
            CreateSystem();
            int systemID = GetSystemID();
            EditSystem systemBuilderForm = new EditSystem();
            systemBuilderForm.Show();
            foreach(String currentLine in checkedListBoxContainers.CheckedItems)
            {
                // Dont ask this is some wizardry
                // ... It gets the value before the space, which should always be the currentAppID
                int currentSpace = currentLine.IndexOf(' ');
                int currentAppID = Convert.ToInt32(currentLine.Substring(0, currentSpace));
                AddSysAppConnection(currentAppID, systemID);
            }
            OptimizeSystem(systemID);
            this.Close();
            
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
            if (current == 0) {
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

            foreach (int appID in currentSystemCollection.Keys)
            {
                List<int> fileIDs = currentSystemCollection[appID];
                foreach (int currentID in fileIDs)
                {

                }
            }
        }

    }
}
