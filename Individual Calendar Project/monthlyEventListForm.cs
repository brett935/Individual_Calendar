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
    public partial class monthlyEventListForm : Form
    {
        public monthlyEventListForm()
        {
            InitializeComponent();
        }

        private void close_Click(object sender, EventArgs e)
        {
            
            this.Hide(); //hide the form after user clicks close
        }
    }
}
