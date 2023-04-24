﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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


        public void ViewContainers()
        {
            // Define SQL query to retrieve system_name and system_id columns
            string sqlQuery = "SELECT app_name, app_id FROM Application";

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
                    string applicationName = row["app_name"].ToString();
                    int applicationID = Convert.ToInt32(row["app_id"]);
                    listBoxContainerViewer.Items.Add(applicationName);
                    appIDCollection.Add(i, applicationID);
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

                    //message to show the system was deleted
                    MessageBox.Show("Deleted...");


                    //close connection
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {

                //error message to show if error ocurs during delete
                MessageBox.Show("An error ocured when attempting to delete the Application!" + ex);
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
                MessageBox.Show("An error ocured when attempting to delete the Application!" + ex);
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
                MessageBox.Show("An error ocured when attempting to delete the Application!" + ex);
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
                MessageBox.Show("An error ocured when attempting to delete the file!" + ex);
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

                        //message to show the system was deleted
                        MessageBox.Show("Deleted file");


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
                MessageBox.Show("An error ocured!" + ex);
            }

            return fileCount;
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
                deleteFile(fileIDCollection[listBoxFiles.SelectedIndex], appIDCollection[listBoxContainerViewer.SelectedIndex]);
            }
            //Clear the list box and id collection dictionary 
            listBoxFiles.Items.Clear();
            fileIDCollection.Clear();
            // To populate the list box
            GetFileNames(appIDCollection[listBoxContainerViewer.SelectedIndex]);
            // to populate the dictionary
            GetFileIDS(appIDCollection[listBoxContainerViewer.SelectedIndex]);
        }

        private void buttonDeleteContainer_Click(object sender, EventArgs e)
        {
            deleteApplication(appIDCollection[listBoxContainerViewer.SelectedIndex]);
            for (int i = 0; i < listBoxContainerViewer.Items.Count; i++)
            {

                deleteFile(fileIDCollection[i], appIDCollection[listBoxContainerViewer.SelectedIndex]);
            }



            listBoxFiles.Items.Clear();
            fileIDCollection.Clear();
            listBoxContainerViewer.Items.Clear();

            // To populate the list box
            GetFileNames(appIDCollection[listBoxContainerViewer.SelectedIndex]);
            // to populate the dictionary
            GetFileIDS(appIDCollection[listBoxContainerViewer.SelectedIndex]);
            ViewContainers();
        }
    }
}
