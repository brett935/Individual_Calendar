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
    public partial class monthlyCalendarForm : Form
    {
        public monthlyCalendarForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addForm addForm = new addForm();
            addForm.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void viewList_Click(object sender, EventArgs e)
        {
            monthlyEventListForm monthlyEventForm = new monthlyEventListForm();
            monthlyEventForm.Show();
        }
    }
}
