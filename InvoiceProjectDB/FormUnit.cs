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
    public partial class FormUnit : Form
    {
        public FormUnit()
        {
            InitializeComponent();
        }
        InvoiceProjectContext ctx = new InvoiceProjectContext();
        int clickedUnitID;
        List<int> deleteID;
        public void List()
        {
            dataGridView1.DataSource = ctx.Units.ToList();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[2].Visible = false;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            Unit unit = new Unit();
            unit.UnitName = txtUnitName.Text;
            ctx.Units.Add(unit);
            ctx.SaveChanges();
            List();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Unit unit = ctx.Units.Find(clickedUnitID);
            unit.UnitName = txtUnitName.Text;
            ctx.SaveChanges();
            List();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Unit unit = ctx.Units.Find(clickedUnitID);
            ctx.Units.Remove(unit);
            ctx.SaveChanges();
            List();
        }

        private void FormUnit_Load(object sender, EventArgs e)
        {
            List();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clickedUnitID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            txtUnitName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
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
                        Unit unit = ctx.Units.Find(item);
                        ctx.Units.Remove(unit);
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
