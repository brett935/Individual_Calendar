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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text; //get username from textbox
            string password = textBox2.Text; //get password from textbox

            DataTable myTable = new DataTable(); //create a data table to store results of sql query
            string connStr = "server=brettnapier.com;user=csc340Individual;database=csc340IndividualProject;port=3306;password=cscproject;";
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);

            //attempt to make a connection to the SQL server
            try
            {

                Console.WriteLine("Connecting to MySQL...");

                conn.Open();

                string sql = "SELECT * FROM Users WHERE userName=@userName;";

                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userName", username); //add paramaters to query
                MySql.Data.MySqlClient.MySqlDataAdapter myAdapter = new MySql.Data.MySqlClient.MySqlDataAdapter(cmd);
                myAdapter.Fill(myTable); //fill the data table with results
                Console.WriteLine("Table is ready");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();

            string sqlPass = ""; //instantiate string to hold retrieved password
            string userID = ""; //instantiate string to hold userID

            //iterate through retrieved tables and process the elements
            foreach (DataRow row in myTable.Rows)
            {
                sqlPass = row["userPassword"].ToString(); //get the user password
                userID = row["userID"].ToString(); //get the user id
            }


            Console.WriteLine("Retrieved user password.");

            //if entered password matches then login
            if (password == sqlPass)
            {

                Form1 calendarGUI = new Form1(userID); //create a new monthly calendar form
                calendarGUI.Show(); //show the calendar GUI form
                this.Hide(); //hide the login window
            }
            else {
                System.Windows.Forms.MessageBox.Show("Incorrect password or username!"); //show incorrect password error message
            }
        }
    }
}
