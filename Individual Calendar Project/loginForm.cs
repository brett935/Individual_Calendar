using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Individual_Calendar_Project
{
    public partial class loginForm : Form
    {
        public loginForm()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void login(object sender, EventArgs e)
        {
            //check login details


            //show monthly calendar form
            Form monthlyForm = new monthlyCalendarForm();
            monthlyForm.Show();

            //hide the login form
            //this.Visible=false;
            this.Hide();
            

        }
    }
}
