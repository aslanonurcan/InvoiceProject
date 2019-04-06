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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCustomer frmCustomer = new FormCustomer();
            frmCustomer.Show();
        }

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormProduct frmProduct = new FormProduct();
            frmProduct.Show();
        }

        private void unitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUnit frmUnit = new FormUnit();
            frmUnit.Show();
        }

        private void cityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCity frmCity = new FormCity();
            frmCity.Show();
        }

        private void countyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCounty frmCounty = new FormCounty();
            frmCounty.Show();
        }

        private void ınvoiceHeaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormNewInvoiceTransaction frmNewInvoice = new FormNewInvoiceTransaction();
            frmNewInvoice.Show();
        }

        private void ınvoiceDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormViewEditSearch formViewEdit = new FormViewEditSearch();
            formViewEdit.Show();
        }
    }
}
