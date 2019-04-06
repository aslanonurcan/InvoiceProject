using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InvoiceProjectDB
{
    public partial class FormProduct : Form
    {
        public FormProduct()
        {
            InitializeComponent();
        }
        InvoiceProjectContext ctx = new InvoiceProjectContext();
        int clickedProductID;
        List<int> deleteID;
        private void FormProduct_Load(object sender, EventArgs e)
        {
            ComboList();
            List();
        }
        public void ComboList()
        {
            cmbUnitName.DisplayMember = "UnitName";
            cmbUnitName.ValueMember = "UnitID";
            cmbUnitName.DataSource = ctx.Units.ToList();
        }
        public void List()
        {
            dataGridView1.DataSource = ctx.Products.Select(x=> new
            {
                x.ProductID,
                x.ProductName,
                x.ProductNumber,
                x.unit.UnitName,
                x.UnitPrice
            }).ToList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Product product = new Product();
            product.ProductName = txtProductName.Text;
            product.ProductNumber = Convert.ToInt32(txtProductNumber.Text);
            product.UnitID = (int)cmbUnitName.SelectedValue;
            product.UnitPrice = Convert.ToInt32(txtUnitPrice.Text);
            ctx.Products.Add(product);
            ctx.SaveChanges();
            List();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Product product = ctx.Products.Find(clickedProductID);
            product.UnitID = (int)cmbUnitName.SelectedValue;
            product.ProductName = txtProductName.Text;
            product.ProductNumber = Convert.ToInt32(txtProductNumber.Text);
            product.UnitPrice = Convert.ToInt32(txtUnitPrice.Text);
            ctx.SaveChanges();
            List();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Product product = ctx.Products.Find(clickedProductID);
            ctx.Products.Remove(product);
            ctx.SaveChanges();
            List();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clickedProductID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            txtProductName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtProductNumber.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            cmbUnitName.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtUnitPrice.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void btnMultiDelete_Click(object sender, EventArgs e)
        {
            deleteID = new List<int>();
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                deleteID.Add(Convert.ToInt32(row.Cells[0].Value));
            }
            if (dataGridView1.SelectedRows.Count > 1)
            {
                DialogResult dr = new DialogResult();
                string s = string.Format("Do you want to delete selected {0} rows", dataGridView1.SelectedRows.Count);
                dr = MessageBox.Show(s, "Warning", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    foreach (var item in deleteID)
                    {
                        Product product = ctx.Products.Find(item);
                        ctx.Products.Remove(product);
                        ctx.SaveChanges();
                        List();
                    }
                }
                else
                {
                    deleteID = new List<int>();
                }
            }
            else
            {
                MessageBox.Show("Select row more than one");
            }
        }
    }
}
