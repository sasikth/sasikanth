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
    public partial class UC_W_UpdateFood : UserControl
    {

        function fn = new function();
        String query;

        public UC_W_UpdateFood()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtFoodId.Text != "")
            {
                query = "select * from food where mid = '" + txtFoodId.Text + "'";
                DataSet ds = fn.getData(query);
                if (ds.Tables[0].Rows.Count != 0)
                {
                    txtFoodName.Text = ds.Tables[0].Rows[0][2].ToString();
                    txtFoodNumber.Text = ds.Tables[0].Rows[0][3].ToString();
                    txtAvailableQuantity.Text = ds.Tables[0].Rows[0][4].ToString();
                    txtPricePerUnit.Text = ds.Tables[0].Rows[0][5].ToString();
                }
                else
                {
                    MessageBox.Show("No Food with ID: " + txtFoodId.Text + " exists.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                clearAll();
            }
        }

        private void clearAll()
        {
            txtFoodName.Clear();
            txtFoodNumber.Clear();
            txtAvailableQuantity.Clear();
            txtPricePerUnit.Clear();
            if(txtAddQuantity.Text != "0")
            {
                txtAddQuantity.Text = "0";
            }
            else
            {
                txtAddQuantity.Text = "0";
            }

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        Int64 totalQuantity;
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            String mname = txtFoodName.Text;
            String mnumber = txtFoodNumber.Text;
            Int64 quantity = Int64.Parse(txtAvailableQuantity.Text);
            Int64 addQuantity = Int64.Parse(txtAddQuantity.Text);
            Int64 unitprice = Int64.Parse(txtPricePerUnit.Text);

            totalQuantity = quantity + addQuantity;

            query = "update food set mname = '" + mname + "',mnumber = '" + mnumber + "',quantity =" + totalQuantity + ",perunit =" + unitprice + " where mid = '" + txtFoodId.Text + "'";
            fn.setData(query, "Food Details Updated.!");
        }
    }
}
