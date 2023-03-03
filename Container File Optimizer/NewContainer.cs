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
using System.Configuration;

namespace Container_File_Optimizer
{
    public partial class NewContainer : Form
    {
        string dbPath = "C:\\Users\\ff7fa\\OneDrive\\Desktop\\Containerfile\\Container File Optimizer\\ContainerfileDatabase.mdf";

        public NewContainer()
        {
            InitializeComponent();
        }

        private void NewContainer_Load(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();   
            for (FileDialog )
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
            string dbPath = "C:\\Users\\ff7fa\\OneDrive\\Desktop\\Containerfile\\Container File Optimizer\\ContainerfileDatabase.mdf";
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + dbPath + ";Integrated Security=True;";

            using (SqlConnection cnn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Application (app_name,app_desc) VALUES (@a, @b)", cnn))
            {
                //Execute SQL INSERT
                cmd.Parameters.AddWithValue("@a", textBoxSystemName.Text);
                cmd.Parameters.AddWithValue("@b", textBoxCreator.Text);

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

            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + dbPath + ";Integrated Security=True;";
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

            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + dbPath + ";Integrated Security=True;";
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

        private void buttonCreateSystem_Click(object sender, EventArgs e)
        {
            createApp();
        }

        private void textBoxCreator_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
