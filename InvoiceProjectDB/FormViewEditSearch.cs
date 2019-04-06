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
    public partial class FormViewEditSearch : Form
    {
        private InvoiceProjectContext _db;
        private Customer selectedCustomer;
        private List<InvoiceHeader> _invoiceHeader;
        public FormViewEditSearch()
        {
            InitializeComponent();

            Initialize();
        }
        private void Initialize()
        {
            _db = new InvoiceProjectContext();
            selectedCustomer = null;
            _invoiceHeader = new List<InvoiceHeader>();

            cmbCity.DisplayMember = "CityName";
            cmbCity.ValueMember = "CityID";
            cmbCity.DataSource = _db.Cities.ToList();
        }

        private void cmbCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCity.SelectedIndex == -1)
                return;

            int ilID = Convert.ToInt32(cmbCity.SelectedValue);
            cmbCounty.DisplayMember = "CountyName";
            cmbCounty.ValueMember = "CountyID";
            cmbCounty.DataSource = _db.Counties.Where(ilce => ilce.CityID == ilID).ToList();
        }

        private void cmbCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCounty.SelectedIndex == -1)
                return;

            int ilceID = Convert.ToInt32(cmbCounty.SelectedValue);
            cmbCompanyName.DisplayMember = "CompanyName";
            cmbCompanyName.ValueMember = "CustomerID";
            cmbCompanyName.DataSource = _db.Customers.Where(musteri => musteri.CountyID == ilceID).ToList();
        }

        private void cmbCompanyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCompanyName.SelectedIndex == -1)
                return;

            selectedCustomer = (Customer)cmbCompanyName.SelectedItem;
        }

        private void btnListAllInvoices_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtInvoiceNumber.Text))
            {
                if (selectedCustomer == null)
                    _invoiceHeader = _db.InvoiceHeaders.ToList();
                else
                    _invoiceHeader = _db.InvoiceHeaders.Where(fatura => fatura.CustomerID == selectedCustomer.CustomerID).ToList();
            }
            else
            {
                if (!CheckFaturaID())
                    return;

                int faturaID = Convert.ToInt32(txtInvoiceNumber.Text);

                if (selectedCustomer == null)
                    _invoiceHeader = _db.InvoiceHeaders.Where(fatura => fatura.InvoieceID == faturaID).ToList();
                else
                    _invoiceHeader = _db.InvoiceHeaders.Where(fatura => fatura.InvoieceID == faturaID && fatura.CustomerID == selectedCustomer.CustomerID).ToList();
            }

            dataGridView1.DataSource = _invoiceHeader;
        }
        private bool CheckFaturaID()
        {
            if (string.IsNullOrWhiteSpace(txtInvoiceNumber.Text))
                return false;

            try
            {
                Convert.ToInt32(txtInvoiceNumber.Text);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || dataGridView1.CurrentRow == null)
                return;

            InvoiceHeader faturaMaster = (InvoiceHeader)dataGridView1.CurrentRow.DataBoundItem;
            dataGridView1.DataSource = faturaMaster.invoicedetail.ToList();
        }
    }
}
