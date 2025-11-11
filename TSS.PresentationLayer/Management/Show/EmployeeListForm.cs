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
    public partial class EmployeeListForm : Form
    {
        public EmployeeListForm()
        {
            InitializeComponent();
        }
        int TotalItem = 0, selectedItem = 0;
        private void FormEmployee_Load(object sender, EventArgs e)
        {
            dgvEmployee.DefaultCellStyle.BackColor = Color.FromArgb(28, 28, 28);
            //dgvTheme();
            refreshData();
        }
        private void dgvTheme()
        {
            dgvEmployee.BorderStyle = BorderStyle.None;
            dgvEmployee.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(56, 56, 56);
            dgvEmployee.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvEmployee.DefaultCellStyle.SelectionBackColor = Color.Maroon;
            dgvEmployee.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvEmployee.BackgroundColor = Color.FromArgb(28, 28, 28);

            dgvEmployee.EnableHeadersVisualStyles = false;
            dgvEmployee.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvEmployee.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgvEmployee.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }
        private void refreshData()
        {
            dgvEmployee.DataSource = Program.employeeManager.getAllEntities();
            TotalItem = dgvEmployee.Rows.Count;
            lblSelectedItem.Text = string.Format("Total obje sayısı {0} seçilen obje {1}", TotalItem, selectedItem);
        }

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            Management.Add.EmployeeAddForm employeeAddForm = new Add.EmployeeAddForm();
            if (employeeAddForm.ShowDialog() == DialogResult.OK)
                refreshData();
        }

        private void btnUpdateEmployee_Click(object sender, EventArgs e)
        {
            if (lblSelectedItem.Text == "secilenid")
            {
                Dialog.MessageDialog dialog = new Dialog.MessageDialog("Çalışan seçilmemiş.", "Hata", Dialog.MessageDialog.TypeEnum.Hata);
                dialog.ShowDialog();
                return;
            }
            Management.Add.EmployeeAddForm employeeAddForm = new Add.EmployeeAddForm(selectedItem.ToString());
            if (employeeAddForm.ShowDialog() == DialogResult.OK)
                refreshData();
        }

        private void btnDeleteEmployee_Click(object sender, EventArgs e)
        {
            Dialog.YesNoMessageDialog messageBox = new Dialog.YesNoMessageDialog("Seçili çalışanı(" + dgvEmployee.CurrentRow.Cells[1].Value + " " + dgvEmployee.CurrentRow.Cells[2].Value + ") silmek istediğinize eminmisiniz ?", "Karar Anı :P", Dialog.YesNoMessageDialog.TypeEnum.Soru);
            if (messageBox.ShowDialog() == DialogResult.Yes)
            {
                if (lblSelectedItem.Text != "secilenid")
                {
                    Program.employeeManager.DeleteEntities(selectedItem);
                    refreshData();
                }
            }
        }

        private void dgvEmployee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedItem = Convert.ToInt32(dgvEmployee.CurrentRow.Cells[0].Value.ToString());
            lblSelectedItem.Text = string.Format("Total obje sayısı {0} seçilen obje {1}", TotalItem, selectedItem);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text == "")
                refreshData();
            else
                dgvEmployee.DataSource = Program.employeeManager.GetAllEntitiesByFiltered(c => c.FirstName == txtSearch.Text);
        }
    }
}
