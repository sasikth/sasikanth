using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_Management_System
{
    public partial class Worker : Form
    {
        public Worker()
        {
            InitializeComponent();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            uC_W_Dashboard1.Visible = true;
            uC_W_Dashboard1.BringToFront();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Login fm = new Login();
            fm.Show();
            this.Hide();
        }

        private void Worker_Load(object sender, EventArgs e)
        {
            uC_W_Dashboard1.Visible = false;
            uC_W_AddFood1.Visible = false;
            uC_W_ViewFood1.Visible = false;
            uC_W_UpdateFood1.Visible = false;
            uC_W_Billing1.Visible = false;
            btnDashboard.PerformClick();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            uC_W_AddFood1.Visible = true;
            uC_W_AddFood1.BringToFront();
        }

        private void btnViewFood_Click(object sender, EventArgs e)
        {
            uC_W_ViewFood1.Visible = true;
            uC_W_ViewFood1.BringToFront();
        }

        private void btnModifyFood_Click(object sender, EventArgs e)
        {
            uC_W_UpdateFood1.Visible = true;
            uC_W_UpdateFood1.BringToFront();
        }

        private void btnSellFood_Click(object sender, EventArgs e)
        {
            uC_W_Billing1.Visible = true;
            uC_W_Billing1.BringToFront();
        }
    }
}
