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
    public partial class FormCity : Form
    {
        InvoiceProjectContext ctx = new InvoiceProjectContext();
        int selectedCityID;
        List<int> deleteIDList;
        public FormCity()
        {
            InitializeComponent();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                City c = new City();
                c.CityName = txtCityName.Text;
                ctx.Cities.Add(c);
                ctx.SaveChanges();
                List();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void List()
        {
            dataGridView1.DataSource = ctx.Cities.Select(x=>new {
                x.CityID,
                x.CityName
            }).ToList();
            dataGridView1.ClearSelection();
            selectedCityID = 0;
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedCityID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            txtCityName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentCell != null)
                {
                    if (dataGridView1.SelectedRows.Count == 1)
                    {
                        City c = ctx.Cities.Find(selectedCityID);
                        c.CityName = txtCityName.Text;
                        ctx.SaveChanges();
                    }
                    else if (dataGridView1.SelectedRows.Count > 1)
                    {
                        MessageBox.Show("Güncelleme için tek satır seçiniz");
                    }
                }
                else
                {
                    MessageBox.Show("Güncelleme için satır seçiniz");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Güncellemek istediğiniz satırı seçiniz");
            }
            List();
            dataGridView1.CurrentCell = null;
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentCell != null)
                {
                    if (dataGridView1.SelectedRows.Count == 1)
                    {
                        ctx.Cities.Remove(ctx.Cities.Find(selectedCityID));
                        ctx.SaveChanges();
                    }
                    else if (dataGridView1.SelectedRows.Count > 1)
                    {
                        MessageBox.Show("Çoklu silme için MultiDelete Butonunu kullanın");
                    }
                }
                else 
                {
                    MessageBox.Show("Silmek istediğiniz satırı seçiniz");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Silmek istediğiniz satırı seçiniz");
            }
            List();
            dataGridView1.CurrentCell = null;
        }
        private void btnMultiDelete_Click(object sender, EventArgs e)
        {
            deleteIDList = new List<int>();
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                deleteIDList.Add(Convert.ToInt32(row.Cells[0].Value));
            }
            if (dataGridView1.SelectedRows.Count > 1)
            {
                DialogResult dr = new DialogResult();
                string s = string.Format("Seçili {0} satırı silmek istiyormusunuz   ", dataGridView1.SelectedRows.Count);
                dr = MessageBox.Show(s, "Warning", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    foreach (var item in deleteIDList)
                    {
                        City city = ctx.Cities.Find(item);
                        ctx.Cities.Remove(city);
                        ctx.SaveChanges();
                    }
                }
                else
                {
                    deleteIDList = new List<int>();
                }
            }
            else
            {
                MessageBox.Show("Tek satır silmek için Delete Butonunu kullanın");
            }
            List();
            dataGridView1.CurrentCell = null;
        }
        private void FormCity_Load(object sender, EventArgs e)
        {
            List();
            dataGridView1.CurrentCell = null;
        }
    }
}
