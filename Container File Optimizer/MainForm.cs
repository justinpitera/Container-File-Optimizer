﻿using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Configuration;

namespace Container_File_Optimizer
{
    public partial class Main : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Container_File_Optimizer.Properties.Settings.ContainerfileDatabaseConnectionString"].ConnectionString;
        public Main()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void buttonCreateNewSystem_Click(object sender, EventArgs e)
        {
            NewSystem newSystem = new NewSystem();
            newSystem.ShowDialog();
        }

        private void buttonEditSystem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openSystemFile = new OpenFileDialog();
            openSystemFile.Filter = "Container File Optimizer files (*.cfo)|*.cfo";
            openSystemFile.InitialDirectory = Application.StartupPath;
            openSystemFile.ShowDialog();
            MessageBox.Show("Opening: " + openSystemFile.FileName);
        }

        private void buttonCreateContainer_Click(object sender, EventArgs e)
        {
            NewContainer newContainer = new NewContainer();
            newContainer.ShowDialog();
        }

        /// <summary>
        /// This Fundction uses SQL commands to view the saved applications
        /// </summary>
        /// 

        private void ViewApps()
        {
            //get SQL connection and Command
            using (SqlConnection cnn = new SqlConnection(connectionString))
            using (SqlDataAdapter adp = new SqlDataAdapter("SELECT * FROM Application", cnn))
            {
                DataTable appTable = new DataTable();
                adp.Fill(appTable);

                cnn.Open();
                cnn.Close();

            }
        }

        /*
       *  This Fundction uses SQL commands to view the saved Systems 
       */
        private void ViewSystems()
        {
            //get SQL connection and Command
            using (SqlConnection cnn = new SqlConnection(connectionString))
            using (SqlDataAdapter adp = new SqlDataAdapter("SELECT * FROM System", cnn))
            {
                DataTable appTable = new DataTable();
                adp.Fill(appTable);

                cnn.Open();
                cnn.Close();
            }
        }
    }
}