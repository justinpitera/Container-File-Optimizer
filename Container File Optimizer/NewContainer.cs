using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Container_File_Optimizer
{
    public partial class NewContainer : Form
    {
        //conection string for SQL
        string connectionString = "Container_File_Optimizer.Properties.Settings.ContainerfileDatabaseConnectionString";

        public NewContainer()
        {
            InitializeComponent();
        }

        private void NewContainer_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBoxSystemName_TextChanged(object sender, EventArgs e)
        {

        }


        /*
        *  This Fundction uses SQL commands to add a application to the database 
        */
        private void createApp()
        {
            //get SQL connection and Command
            using (SqlConnection cnn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Application (app_name,app_creator) VALUES (@a, @b)", cnn))
            {
                //Execute SQL INSERT
                cmd.Parameters.AddWithValue("@a", "value");
                cmd.Parameters.AddWithValue("@b", "value");

                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();

            }
        }

        /*
       *  This Fundction uses SQL commands to add a File to the database 
       */
        private void createFile()
        {
            //get SQL connection and Command
            using (SqlConnection cnn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("INSERT INTO File (file_name,file_path) VALUES (@a, @b)", cnn))
            {
                //Execute SQL INSERT
                cmd.Parameters.AddWithValue("@a", "value");
                cmd.Parameters.AddWithValue("@b", "value");

                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();

            }
        }

        /*
         *  This Fundction uses SQL commands to add a connection between a system and an application 
         */
        private void addAppFileConection()
        {
            //get SQL connection and Command
            using (SqlConnection cnn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("INSERT INTO AppFile (app_id,file_id) VALUES (@a, @b)", cnn))
            {
                //Execute SQL INSERT
                cmd.Parameters.AddWithValue("@a", "value");
                cmd.Parameters.AddWithValue("@b", "value");

                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();

            }
        }
    }
}
