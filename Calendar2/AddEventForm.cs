using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calendar2
{
    public partial class AddEventForm : Form
    {
        public AddEventForm(string selectedDate)
        {
            InitializeComponent();

            textBox2.Text = selectedDate; //set the date box to the selected date
            textBox2.Enabled = false; //set the textbox to read only so user can't change the date
            textBox3.Text = "12:00:00"; //set default start time
            textBox4.Text = "01:00:00"; //set default end time
        }

        //close button
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide(); //hide the form
        }

        //save button
        private void button1_Click(object sender, EventArgs e)
        {
            string title = textBox1.Text; //get the value from the title box
            string date = textBox2.Text; //get the value from the date box
            string startTime = textBox3.Text; //get the value from the start time box
            string endTime = textBox4.Text; //get the value from the ending time box
            string content = richTextBox1.Text; //get the value from the content box


            //perform SQL actions
            string connStr = "server=brettnapier.com;user=csc340Group;database=csc340GroupProject;port=3306;password=cscproject;";
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);

            //attempt to make a connection to the SQL server
            try
            {

                Console.WriteLine("Connecting to MySQL...");

                conn.Open();

                ////////////////////////////////////////////////------NEED TO IMPLEMENT USER ID--------------
                string sql = "INSERT INTO Events (eventTitle, Users_userID, eventDate, eventStartTime, eventEndTime, eventContent) VALUES (@title, '1', @date, @startTime, @endTime, @content);";
                ///////////////////////////////////////////////----------------------------------------------

                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@title", title); //add paramaters to query
                cmd.Parameters.AddWithValue("@date", date); //add paramaters to query
                cmd.Parameters.AddWithValue("@startTime", startTime); //add paramaters to query
                cmd.Parameters.AddWithValue("@endTime", endTime); //add paramaters to query
                cmd.Parameters.AddWithValue("@content", content); //add paramaters to query
                cmd.ExecuteNonQuery(); //execute the sql commands
                
                Console.WriteLine("Table is ready");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();

            //get instance of our main GUI by accessing open form and treating it as a Form1
            if (System.Windows.Forms.Application.OpenForms["Form1"] != null)
            {
                (System.Windows.Forms.Application.OpenForms["Form1"] as Form1).update(title, date, startTime, endTime, content); //call function from that form
            }


            this.Hide(); //hide the form
        }
    }
}
