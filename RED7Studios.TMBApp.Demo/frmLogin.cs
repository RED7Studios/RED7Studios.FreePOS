﻿using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace RED7Studios.TMBApp
{
    public partial class frmLogin : Form
    {
        MySqlConnection conn = new MySqlConnection("Server = 52.187.233.224; Database=freepos_demo;Uid=freepos_demo;Pwd=oP31v4!w;");

        MySqlDataAdapter adapter;

        DataTable table = new DataTable();

        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            tbUsername.Clear();
            tbPassword.Clear();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                adapter = new MySqlDataAdapter("SELECT `username`, `password`, `first`, `last` FROM `users` WHERE `username` = '" + tbUsername.Text + "' AND `password` = '" + tbPassword.Text + "'", conn);
                adapter.Fill(table);

                DataRow[] currentRows = table.Select(
    null, null, DataViewRowState.CurrentRows);

                if (currentRows.Length < 1)
                    Console.WriteLine("No Current Rows Found");
                else
                {
                    foreach (DataColumn column in table.Columns)
                        Console.Write("\t{0}", column.ColumnName);

                    Console.WriteLine("\tRowState");

                    foreach (DataRow row in currentRows)
                    {
                        foreach (DataColumn column in table.Columns)
                            Console.Write("\t{0}", row[column]);

                        Console.WriteLine("\t" + row.RowState);
                    }
                }

                if (table.Rows.Count <= 0)
                {
                    // invalid
                    //MessageBox.Show("Invalid");
                }
                else
                {
                    // correct
                    //MessageBox.Show("Correct");
                    Hide();

                    frmDashboard dash = new frmDashboard();
                    dash.Show();
                }

                table.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured: " + ex.Message, "CRITICAL ERROR!");
            }
        }

        private void mtDemo_Click(object sender, EventArgs e)
        {
            tbUsername.Text = "demo";
            tbPassword.Text = "demo";
        }

        private void mtAdministrator_Click(object sender, EventArgs e)
        {
            tbUsername.Text = "admin";
            tbPassword.Text = "password";
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}