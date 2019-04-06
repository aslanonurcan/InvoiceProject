using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InvoiceProjectDB
{
    public partial class FormNewInvoiceTransaction : Form
    {
        private InvoiceProjectContext _db;
        private Customer _selectedCustomer;
        private Product _selectedProduct;
        private HashSet<InvoiceDetail> _invoiceDetail;
        private int _invoiceID;
        private InvoiceDetail _selectedInvoiceDetail;
        public FormNewInvoiceTransaction()
        {
            InitializeComponent();

            Initialize();
        }

        private void Initialize()
        {
            _db = new InvoiceProjectContext();
            _selectedCustomer = null;
            _selectedProduct = null;
            _invoiceDetail = new HashSet<InvoiceDetail>();
            _selectedInvoiceDetail = null;

            dataGridView1.DataSource = _invoiceDetail;

            InvoiceDetail invoice = _db.InvoiceDetails.ToList().LastOrDefault();
            _invoiceID = invoice == null ? 1 : invoice.InvoiceID;
            lblInvoiceID.Text = _invoiceID.ToString();

            cmbProductName.DisplayMember = "ProductName";
            cmbProductName.ValueMember = "ProductID";
            cmbProductName.DataSource = _db.Products.ToList();

            cmbCity.DisplayMember = "CityName";
            cmbCity.ValueMember = "CityID";
            cmbCity.DataSource = _db.Cities.ToList();
            cmbCity.Enabled = false;

            cmbCounty.DisplayMember = "CountyName";
            cmbCounty.ValueMember = "CountyID";
            cmbCounty.DataSource = _db.Counties.ToList();
            cmbCounty.Enabled = false;

            cmbCustomer.DisplayMember = "CompanyName";
            cmbCustomer.ValueMember = "CustomerID";
            cmbCustomer.DataSource = _db.Customers.ToList();
        }

        private void btnAddInvoice_Click(object sender, EventArgs e)
        {
            if (!CheckInvoiceInputs())
                return;

            InvoiceHeader invoiceHeader = new InvoiceHeader()
            {
                InvoieceID = _invoiceID,
                invoicedetail = _invoiceDetail,
                InvoiceDateTime = DateTime.Now,
                DeliveryNoteNumber = Convert.ToInt32(txtDeliveryNumber.Text),
                CustomerID = _selectedCustomer.CustomerID,
                PaymentDateTime = dtpPaymentDate.Value,
                InvoiceAmount = _invoiceDetail.Select(f => f.TotalAmount).Sum()
            };

            DbContextTransaction tran = _db.Database.BeginTransaction();

            try
            {
                _db.InvoiceHeaders.Add(invoiceHeader);
                _db.SaveChanges();
                ClearFaturaInputs();
                tran.Commit();
            }
            catch (Exception)
            {
                tran.Rollback();
                MessageBox.Show("Beklenmeyen bir hata meydana geldi");
            }
        }


        private void btnClearList_Click(object sender, EventArgs e)
        {
            ClearDgvYeniFatura();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!CheckUrunInputs())
                return;

            decimal kdv = Convert.ToDecimal(txtVAT.Text);
            int adet = (int)nudQuantity.Value;
            decimal toplam = (adet * _selectedProduct.UnitPrice) + (adet * _selectedProduct.UnitPrice * kdv / 100);

            InvoiceDetail invoice = new InvoiceDetail();
            invoice.InvoiceID = _invoiceID;
            invoice.product = _selectedProduct;

            InvoiceDetail urun = _invoiceDetail.FirstOrDefault(f => f.ProductID == _selectedProduct.ProductID && f.InvoiceID == _invoiceID);
            if (urun == null)
            {
                invoice.ProductID = _selectedProduct.ProductID;
                invoice.Quantity = adet;
                /*invoice.ToplamFiyat = adet * _selectedProduct.UnitPrice;
                invoice.KDV = kdv;
                invoice.VATAmount = _selectedProduct.UnitPrice + (adet * _selectedProduct.UnitPrice * kdv / 100);
                invoice.GenelToplam = invoice.VATAmount * invoice.Miktar;*/
            }
            else
            {
                invoice = urun;
                invoice.Quantity += adet;
            }

            invoice.TotalAmount = invoice.Quantity * _selectedProduct.UnitPrice;
            invoice.VAT = kdv;
            invoice.VATAmount = _selectedProduct.UnitPrice + (_selectedProduct.UnitPrice * kdv / 100);
            invoice.TotalAmount = invoice.VATAmount * invoice.Quantity;

            _invoiceDetail.Add(invoice);
            ClearProductInputs();
        }

        private void ClearProductInputs()
        {
            _selectedInvoiceDetail = null;
            nudQuantity.Value = 1;
            txtVAT.Text = "";
            dataGridView1.DataSource = _invoiceDetail.ToList();
            label13.Text = _invoiceDetail.Select(f => f.TotalAmount).Sum().ToString();
        }
        private bool CheckUrunInputs()
        {
            if (cmbProductName.SelectedItem == null)
                return false;

            if (_db.Products.ToList().FirstOrDefault(u => u.ProductID == (int)cmbProductName.SelectedValue) == null)
                return false;

            if (nudQuantity.Value <= 0)
                return false;

            try
            {
                decimal kdv = Convert.ToDecimal(txtVAT.Text);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        private void ClearFaturaInputs()
        {
            txtDeliveryNumber.Text = "";
            ClearDgvYeniFatura();

            InvoiceDetail invoice = _db.InvoiceDetails.ToList().LastOrDefault();
            _invoiceID = invoice == null ? 1 : invoice.InvoiceID;
            lblInvoiceID.Text = _invoiceID.ToString();

            ClearProductInputs();
        }
        private void ClearDgvYeniFatura()
        {
            _invoiceDetail = new HashSet<InvoiceDetail>();
            dataGridView1.DataSource = _invoiceDetail;
        }
        private bool CheckInvoiceInputs()
        {
            if (_selectedCustomer == null)
                return false;

            if (_invoiceDetail.Count < 1)
                return false;

            if (string.IsNullOrWhiteSpace(txtDeliveryNumber.Text))
                return false;

            if (dtpPaymentDate.Value == null)
                return false;

            try
            {
                Convert.ToInt32(txtDeliveryNumber.Text);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedInvoiceDetail == null)
                return;

            DialogResult result = MessageBox.Show($"{_selectedInvoiceDetail.product.ProductName} adlı ürünü silmek istiyor musunuz?", "Uyarı", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                _invoiceDetail.Remove(_selectedInvoiceDetail);
                ClearProductInputs();
            }
        }

        private void FormNewInvoiceTransaction_Load(object sender, EventArgs e)
        {

        }

        private void cmbProductName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProductName.SelectedIndex == -1)
                return;

            _selectedProduct = (Product)cmbProductName.SelectedItem;
            txtUnit.Text = _selectedProduct.unit.UnitName;
            txtUnitPrice.Text = _selectedProduct.UnitPrice.ToString();
        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbCustomer.SelectedIndex == -1)
                return;

            _selectedCustomer = (Customer)cmbCustomer.SelectedItem;
            cmbCounty.SelectedItem = _selectedCustomer.county;
            cmbCity.SelectedItem = _selectedCustomer.county.city;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || dataGridView1.CurrentRow == null)
                return;

            _selectedInvoiceDetail = (InvoiceDetail)dataGridView1.CurrentRow.DataBoundItem;
            cmbProductName.SelectedItem = _selectedInvoiceDetail.product;
            txtVAT.Text = _selectedInvoiceDetail.VAT.ToString();
        }

        private void cmbCounty_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbCity_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
