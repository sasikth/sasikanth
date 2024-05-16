using DGVPrinterHelper;
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
    public partial class UC_W_Billing : UserControl
    {
        function fn = new function();
        String query;
        DataSet ds;

        public UC_W_Billing()
        {
            InitializeComponent();
        }

        private void UC_W_Billing_Load(object sender, EventArgs e)
        {
            listBoxFoods.Items.Clear();
            query = "select mname from food where quantity >'0'";
            ds = fn.getData(query);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                listBoxFoods.Items.Add(ds.Tables[0].Rows[i][0].ToString());
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            listBoxFoods.Items.Clear();
            query = "select mname from food where mname like '" + txtSearch.Text + "%' and quantity >'0' ";
            ds = fn.getData(query);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                listBoxFoods.Items.Add(ds.Tables[0].Rows[i][0].ToString());
            }
        }

        private void listBoxFoods_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNumberofItems.Clear();

            String name = listBoxFoods.GetItemText(listBoxFoods.SelectedItem);

            txtFoodName.Text = name;
            query = "select mid,perUnit from food where mname ='" + name + "'";
            ds = fn.getData(query);
            txtFoodId.Text = ds.Tables[0].Rows[0][0].ToString();
            txtPricePerUnit.Text = ds.Tables[0].Rows[0][1].ToString();
        }

        private void txtNumberofItems_TextChanged(object sender, EventArgs e)
        {
            if (txtNumberofItems.Text != "")
            {
                Int64 unitPrice = Int64.Parse(txtPricePerUnit.Text);
                Int64 noOfUnit = Int64.Parse(txtNumberofItems.Text);
                Int64 totalAmount = unitPrice * noOfUnit;
                txtTotalPrice.Text = totalAmount.ToString();
            }
            else
            {
                txtTotalPrice.Clear();
            }
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            UC_W_Billing_Load(this, null);
        }

        protected int n, totalamount = 0;
        protected Int64 quantity, newQuantity;

        
        private void btnAddtoCart_Click(object sender, EventArgs e)
        {
            if (txtFoodId.Text != "")
            {
                query = "select quantity from food where mid='" + txtFoodId.Text + "'";
                ds = fn.getData(query);

                quantity = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());
                newQuantity = quantity - Int64.Parse(txtNumberofItems.Text);

                if (newQuantity >= 0)
                {
                    n = guna2DataGridView1.Rows.Add();
                    guna2DataGridView1.Rows[n].Cells[1].Value = txtFoodId.Text;
                    guna2DataGridView1.Rows[n].Cells[2].Value = txtFoodName.Text;
                    guna2DataGridView1.Rows[n].Cells[3].Value = txtPricePerUnit.Text;
                    guna2DataGridView1.Rows[n].Cells[4].Value = txtNumberofItems.Text;
                    guna2DataGridView1.Rows[n].Cells[5].Value = txtTotalPrice.Text;

                    totalamount = totalamount + int.Parse(txtTotalPrice.Text);
                    totalLabel.Text = "Rs. " + totalamount.ToString();

                    query = "update food set quantity='" + newQuantity + "' where mid = '" + txtFoodId.Text + "'";
                    fn.setData(query, "Food Added.");
                }
                else
                {
                    MessageBox.Show("Food is Out of Stock.\n Only " + quantity + " left", "Warning !!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                clearAll();
                UC_W_Billing_Load(this, null);
            }
            else
            {
                MessageBox.Show("Select Food First.", "Information !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        int valueAmount;
        String valueId;
        protected Int64 noOfItems;

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                valueAmount = int.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
                valueId = guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                noOfItems = Int64.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());

            }
            catch (Exception)
            {

            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (valueId != null)
            {
                try
                {
                    guna2DataGridView1.Rows.RemoveAt(this.guna2DataGridView1.SelectedRows[0].Index);
                }
                catch
                {

                }
                finally
                {
                    query = "select quantity from food where mid = '" + valueId + "'";
                    ds = fn.getData(query);
                    quantity = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());
                    newQuantity = quantity + noOfItems;

                    query = "update food set quantity = '" + newQuantity + "' where mid = '" + valueId + "'";
                    fn.setData(query, "Food Removed from Cart.");
                    totalamount = totalamount - valueAmount;
                    totalLabel.Text = "Rs. " + totalamount.ToString();
                }
                UC_W_Billing_Load(this, null);
            }
        }

        private void guna2DataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }

        private void btnPurchasePrint_Click(object sender, EventArgs e)
        {
            DGVPrinter print = new DGVPrinter();
            print.Title = "ZESTY CAFE";
            print.SubTitle = String.Format("Date:- {0}", DateTime.Now.ToLocalTime());
            print.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            print.PageNumbers = true;
            print.PageNumberInHeader = false;
            print.PorportionalColumns = true;
            print.HeaderCellAlignment = StringAlignment.Near;
            print.Footer = "Total Payable Amount : " + totalLabel.Text;
            print.FooterSpacing = 15;
            print.PrintDataGridView(guna2DataGridView1);

            totalamount = 0;
            totalLabel.Text = "Rs. 00";
            guna2DataGridView1.DataSource = 0;
        }

        private void clearAll()
        {
            txtFoodId.Clear();
            txtFoodName.Clear();
            txtPricePerUnit.Clear();
            txtNumberofItems.Clear();
        }
    }
}
