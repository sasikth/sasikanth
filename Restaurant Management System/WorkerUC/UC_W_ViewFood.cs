using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_Management_System.WorkerUC
{
    public partial class UC_W_ViewFood : UserControl
    {
        function fn = new function();
        String query;

        public UC_W_ViewFood()
        {
            InitializeComponent();
        }

        private void UC_W_ViewFood_Load(object sender, EventArgs e)
        {
            query = "select * from food";
            setDataGridView(query);
        }

        private void txtFoodName_TextChanged(object sender, EventArgs e)
        {
            query = "select * from food where mname like '"+txtFoodName.Text+"%'";
            setDataGridView(query);
        }

        private void setDataGridView(String query)
        {
            DataSet ds = fn.getData(query);
            guna2DataGridView1.DataSource = ds.Tables[0];
        }

        String foodId;
        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                foodId = guna2DataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();            }
            catch
            {

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure?","Delete Confirmation !",MessageBoxButtons.YesNo,MessageBoxIcon.Warning ) == DialogResult.Yes)
            {
                query = "delete from food where mid = '"+foodId+"'";
                fn.setData(query, "Food Record Deleted.");
                UC_W_ViewFood_Load(this, null);
            }
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            UC_W_ViewFood_Load(this, null);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
