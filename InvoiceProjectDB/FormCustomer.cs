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
    public partial class FormCustomer : Form
    {
        public FormCustomer()
        {
            InitializeComponent();
        }
        InvoiceProjectContext ctx = new InvoiceProjectContext();
        int clickedCityID;
        int clickedCustomerID;
        List<int> deleteID;

        private void FormCustomer_Load(object sender, EventArgs e)
        {
            ComboCityList();
            List();
            dataGridView1.MultiSelect = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        public void ComboCityList()
        {
            cmbCity.DisplayMember = "CityName";
            cmbCity.ValueMember = "CityID";
            cmbCity.DataSource = ctx.Cities.ToList();
        }
        public void ComboCountyList()
        {
            City city = ctx.Cities.Find(clickedCityID);
            cmbCounty.DisplayMember = "CountyName";
            cmbCounty.ValueMember = "CountyID";
            cmbCounty.DataSource = city.counties.ToList();
        }
        public void List()
        {
            dataGridView1.DataSource = ctx.Customers.Select(x => new
            {
                x.CustomerID,
                x.CompanyName,
                x.county.city.CityName,
                x.county.CountyName,
                x.Address
            }).ToList();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Customer customer = new Customer();
                customer.CompanyName = txtCompanyName.Text;
                customer.CountyID = (int)cmbCounty.SelectedValue;
                customer.Address = txtAddress.Text;
                ctx.Customers.Add(customer);
                ctx.SaveChanges();
                List();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Customer customer = ctx.Customers.Find(clickedCustomerID);
                customer.CompanyName = txtCompanyName.Text;
                customer.CountyID = (int)cmbCounty.SelectedValue;
                customer.Address = txtAddress.Text;
                ctx.SaveChanges();
                List();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Customer customer = ctx.Customers.Find(clickedCustomerID);
                ctx.Customers.Remove(customer);
                ctx.SaveChanges();
                List();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void cmbCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            clickedCityID = (int)cmbCity.SelectedValue;
            ComboCountyList();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clickedCustomerID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            txtCompanyName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            cmbCity.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            cmbCounty.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtAddress.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
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
                        Customer customer = ctx.Customers.Find(item);
                        ctx.Customers.Remove(customer);
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
