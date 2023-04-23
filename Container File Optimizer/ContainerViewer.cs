using System;
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

        Dictionary<int, int> applicationIDCollection = new Dictionary<int, int>();
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
                    applicationIDCollection.Add(i, applicationID);
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
                MessageBox.Show("An error ocured when attempting to delete the Application!");
            }

        }

        /// <summary>
        /// This function deletes the selected File from both the File and AppFile tables.
        /// It will also delete the system files from the optimized folder
        /// </summary>
        /// <param name="fileID">The file_id that needs to be dleted.</param>
        public void deleteFile(int fileID)
        {
            //variable to hold the file count
            int fileCount = getFileCount(fileID);

            try
            {
                //this query deletes the File from the AppFile table
                string query = "DELETE FROM AppFile Where file_ID = @fileID";
                using (SqlConnection connection = new SqlConnection(connectionString)) //connection for query
                using (SqlCommand command = new SqlCommand(query, connection)) //command to execute
                {
                    //open connection
                    connection.Open();

                    //sets the  file_id paramater in the query to the fileID variable 
                    command.Parameters.AddWithValue("@fileID", fileID);

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

            if (fileCount <= 0) {
                try
                {
                    //this query deletes the aplication from the AppFile table
                    string query = "DELETE FROM AppFile Where file_id = @file_id";
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
                        MessageBox.Show("Deleted...");


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
                string query = "SELECT COUNT(*) FROM AppFile WHERE file_id = @fileID";
                using (SqlConnection connection = new SqlConnection(connectionString)) //connection for query
                using (SqlCommand command = new SqlCommand(query, connection)) //command to execute
                {
                    //open connection
                    connection.Open();

                    //sets the  file_id paramater in the query to the fileID variable 
                    command.Parameters.AddWithValue("@fileID", fileID);

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


        private void listBoxContainerViewer_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxFiles.Items.Clear();
            if (listBoxContainerViewer.SelectedItems.Count > 0)
            {
                GetFileNames(applicationIDCollection[listBoxContainerViewer.SelectedIndex]);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBoxFiles.Items.Clear();
        }
    }
}
