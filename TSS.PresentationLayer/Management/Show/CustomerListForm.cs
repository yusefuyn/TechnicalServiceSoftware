using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TSS.UserInterface.Management.Show
{
    public partial class CustomerListForm : Form
    {
        Panel panel = null;
        int TotalItem = 0, selectedItem = 0;
        public CustomerListForm(Panel _pnl)
        {
            InitializeComponent();
            panel = _pnl;

        }


        private void FormCustomer_Load(object sender, EventArgs e)
        {
            //dgvTheme();
            dgvCustomer.DefaultCellStyle.BackColor = Color.FromArgb(28, 28, 28);
            refreshData();
        }

        private void dgvTheme()
        {
            dgvCustomer.BorderStyle = BorderStyle.None;
            dgvCustomer.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(56, 56, 56);
            dgvCustomer.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvCustomer.DefaultCellStyle.SelectionBackColor = Color.Maroon;
            dgvCustomer.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvCustomer.BackgroundColor = Color.FromArgb(28, 28, 28);

            dgvCustomer.EnableHeadersVisualStyles = false;
            dgvCustomer.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvCustomer.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgvCustomer.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void refreshData() {
            dgvCustomer.DataSource = Program.customerManager.getAllEntities();
            dgvCustomer.Columns[0].Visible = false;
            TotalItem = dgvCustomer.Rows.Count;
            lblSelectedItem.Text = string.Format("Total obje sayısı {0} seçilen obje {1}", TotalItem, selectedItem);
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            Management.Add.CustomerAddForm customerAddForm = new Add.CustomerAddForm();
            if (customerAddForm.ShowDialog() == DialogResult.OK)
            {
                refreshData();
            }
        }

        private void btnUpdateCustomer_Click(object sender, EventArgs e)
        {
            if (lblSelectedItem.Text == "secilenid")
            {
                Dialog.MessageDialog dialog = new Dialog.MessageDialog("Müşteri seçilmemiş.", "Hata", Dialog.MessageDialog.TypeEnum.Hata);
                dialog.ShowDialog();
                return;
            }
            Management.Add.CustomerAddForm customerAddForm = new Add.CustomerAddForm(selectedItem.ToString());
            if (customerAddForm.ShowDialog() == DialogResult.OK)
            {
                refreshData();
            }
        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedItem = Convert.ToInt32(dgvCustomer.CurrentRow.Cells[0].Value.ToString());
            lblSelectedItem.Text = string.Format("Total obje sayısı {0} seçilen obje {1}", TotalItem, selectedItem);
        }

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            Dialog.YesNoMessageDialog messageBox = new Dialog.YesNoMessageDialog("Seçili Müşteri("+dgvCustomer.CurrentRow.Cells[1].Value+") silmek istediğinize eminmisiniz ?","Karar Anı :P", Dialog.YesNoMessageDialog.TypeEnum.Soru);
            if (messageBox.ShowDialog() == DialogResult.Yes)
            {
                if (lblSelectedItem.Text != "secilenid")
                {
                    Program.customerManager.DeleteEntities(selectedItem);
                    refreshData();
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text == "")
            {
                refreshData();
            }
            else
            {
                dgvCustomer.DataSource = Program.customerManager.GetAllEntitiesByFiltered(t => t.Name == txtSearch.Text);
            }
        }
    }
}
