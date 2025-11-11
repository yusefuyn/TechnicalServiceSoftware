using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TSS.Entities.Concretes;
using TSS.Tools;

namespace TSS.UserInterface.Management.Show
{
    public partial class HardwareListForm : Form
    {
        public HardwareListForm()
        {
            InitializeComponent();
        }

        int TotalItem = 0, selectedItem = -1;

        private void DataRefresh()
        {
            List<Hardwares_Type> hardwares_Types = Program.hardwareTypeManager.getAllEntities();
            dgvHardwares.DataSource = Program.hardwareManager.getAllEntities();
            //dgvHardwares.Rows.Clear();
            //foreach (var item in Program.hardwareManager.getAllEntities())
            //    dgvHardwares.Rows.Add(new string[] { item.ID.ToString(),
            //        hardwares_Types.Find(x=>x.ID == item.HardwareType.ID).Name.ToString(),
            //        item.Name,
            //        item.Model,
            //        item.Producer
            //    });
            TotalItem = dgvHardwares.Rows.Count;
        }

        private void HardwareListForm_Load(object sender, EventArgs e)
        {
            //dgvTheme();
            DataRefresh();
        }

        private void dgvTheme()
        {

            dgvHardwares.Columns.Add("C1","ID");
            dgvHardwares.Columns.Add("C2", "Tür");
            dgvHardwares.Columns.Add("C3", "İsim");
            dgvHardwares.Columns.Add("C4", "Model");
            dgvHardwares.Columns.Add("C5", "Üretici");

            dgvHardwares.Columns[0].Visible = false;


            //dgvHardwares.BorderStyle = BorderStyle.None;
            //dgvHardwares.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(56, 56, 56);
            //dgvHardwares.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            //dgvHardwares.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 134, 138);
            //dgvHardwares.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            //dgvHardwares.BackgroundColor = Color.FromArgb(28, 28, 28);

            //dgvHardwares.EnableHeadersVisualStyles = false;
            //dgvHardwares.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            //dgvHardwares.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            //dgvHardwares.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void btnAddHardware_Click(object sender, EventArgs e)
        {
            Management.Add.HardwareAddForm addForm = new Add.HardwareAddForm();
            if (addForm.ShowDialog() == DialogResult.OK)
                DataRefresh();
        }

        private void btnDeleteHardware_Click(object sender, EventArgs e)
        {
            if (dgvHardwares.SelectedRows.Count != 0)
            {
                Dialog.YesNoMessageDialog messageBox = new Dialog.YesNoMessageDialog("Seçili Donanım(" + dgvHardwares.CurrentRow.Cells[2].Value + ") ile ilgili \"TÜM VERİLERİ\" silmek istediğinize eminmisiniz ?", "Karar Anı :P", Dialog.YesNoMessageDialog.TypeEnum.Soru);
                if (messageBox.ShowDialog() == DialogResult.Yes)
                {
                    if (selectedItem != -1)
                    {
                        foreach (var item in Program.hardwareSizeManager.GetAllEntitiesByFiltered(t => t.HardwaresID.ID == selectedItem))
                        {
                            Program.hardwareSizeManager.DeleteEntities(item.ID);
                        }
                        foreach (var item in Program.hardwareDriveManager.GetAllEntitiesByFiltered(t => t.Hardware.ID == selectedItem))
                        {
                            Program.hardwareDriveManager.DeleteEntities(item.ID);
                        }
                        Program.hardwareManager.DeleteEntities(selectedItem);
                        DataRefresh();
                    }
                }
            }
        }

        private void btnViewHardware_Click(object sender, EventArgs e)
        {
            Theme.FormReDesign(new HardwareForm(selectedItem)).ShowDialog();
        }

        private void dgvHardwares_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedItem = Convert.ToInt32(dgvHardwares.CurrentRow.Cells[0].Value.ToString());
            lblSelectedItem.Text = string.Format("Total obje sayısı {0} seçilen obje {1}", TotalItem, selectedItem);
        }
    }
}
