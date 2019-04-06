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
    public partial class FormCounty : Form
    {
        public FormCounty()
        {
            InitializeComponent();
        }
        InvoiceProjectContext ctx = new InvoiceProjectContext();
        int clickedID;
        List<int> DeleteID;
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                County county = new County();
                county.CountyName = txtCounty.Text;
                county.CityID = (int)cmbCity.SelectedValue;
                ctx.Counties.Add(county);
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
                County county = ctx.Counties.Find(clickedID);
                county.CountyName = txtCounty.Text;
                county.CityID = (int)cmbCity.SelectedValue;
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
                County county = ctx.Counties.Find(clickedID);
                ctx.Counties.Remove(county);
                ctx.SaveChanges();
                txtCounty.Text = string.Empty;
                List();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }
        private void FormCounty_Load(object sender, EventArgs e)
        {
            cmbCity.DisplayMember = "CityName";
            cmbCity.ValueMember = "CityID";
            cmbCity.SelectedIndex = -1;
            cmbCity.DataSource = ctx.Cities.ToList();
            List();
        }
        public void List()
        {
            dataGridView1.DataSource = ctx.Counties.Where(x => x.CityID == clickedID).Select(x => new
            {
                x.CountyID,
                x.CountyName,
                x.city.CityName
            }).ToList();
            if (dataGridView1.RowCount != 0)
            {
                txtCounty.Text = dataGridView1.Rows[0].Cells[1].Value.ToString();
            }
            else
            {
                txtCounty.Text = "";
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCounty.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            clickedID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
        }
        private void btnMultiDelete_Click(object sender, EventArgs e)
        {
            DeleteID = new List<int>();
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                DeleteID.Add(Convert.ToInt32(row.Cells[0].Value));
            }
            if (dataGridView1.SelectedRows.Count > 1)
            {
                DialogResult dr = new DialogResult();
                string s = string.Format("Seçili {0} satırı silmek istiyormusunuz", dataGridView1.SelectedRows.Count);
                dr = MessageBox.Show(s, "Warning", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    foreach (var item in DeleteID)
                    {
                        County county = ctx.Counties.Find(item);
                        ctx.Counties.Remove(county);
                        ctx.SaveChanges();
                        List();
                    }
                }
                else
                {
                    DeleteID = new List<int>();
                }
            }
            else
            {
                MessageBox.Show("Tek satır silmek için Delete Butonunu kullanın");
            }
        }
        private void cmbCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            clickedID = (int)cmbCity.SelectedValue;
            List();
        }
    }
}
