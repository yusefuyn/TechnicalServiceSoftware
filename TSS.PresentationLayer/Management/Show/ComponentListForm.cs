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

namespace TSS.UserInterface.Management.Show
{
    public partial class ComponentListForm : Form
    {
        public ComponentListForm()
        {
            InitializeComponent();
        }

        int selectedItem = 0, totalItem = 0;
        private void dgvTheme()
        {
            dgvComponent.Columns.Clear();
            dgvComponent.Columns.Add("ID", "ID");
            dgvComponent.Columns.Add("Name", "İsim");
            dgvComponent.Columns.Add("Description", "Açıklama");
            dgvComponent.Columns.Add("Type", "Tip");
            dgvComponent.Columns[0].Visible = false;
            //dgvComponent.BorderStyle = BorderStyle.None;
            //dgvComponent.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(56, 56, 56);
            //dgvComponent.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            //dgvComponent.DefaultCellStyle.SelectionBackColor = Color.Maroon;
            //dgvComponent.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            //dgvComponent.BackgroundColor = Color.FromArgb(28, 28, 28);

            //dgvComponent.EnableHeadersVisualStyles = false;
            //dgvComponent.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            //dgvComponent.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            //dgvComponent.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void ComponentListForm_Load(object sender, EventArgs e)
        {
            dgvTheme();
            DataRefresh(Program.componentManager.getAllEntities());
        }

        private void DataRefresh(List<Components> components)
        {
            List<Components_Type> components_Types = Program.componentTypeManager.getAllEntities();
            dgvComponent.Rows.Clear();
            foreach (var item in components)
                dgvComponent.Rows.Add(new string[] { item.ID.ToString(),
                    item.Name,
                    item.Description,
                    components_Types.Find(x => x.ID == item.ComponentTypeId).Name.ToString()
                });
            totalItem = components.Count();
        }

        private void btnDeleteComponent_Click(object sender, EventArgs e)
        {
            Program.componentManager.DeleteEntities(Convert.ToInt32(dgvComponent.CurrentRow.Cells[0].Value.ToString()));
        }

        private void btnAddComponent_Click(object sender, EventArgs e)
        {
            if (TSS.Tools.Theme.FormReDesign(new Management.Add.ComponentAddForm()).ShowDialog() == DialogResult.OK)
                DataRefresh(Program.componentManager.getAllEntities());
        }

        private void dgvComponent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedItem = Convert.ToInt32(dgvComponent.CurrentRow.Cells[0].Value.ToString());
            lblSelectedItem.Text = "Toplam obje sayısı " +  totalItem.ToString() + " seçili obje " + selectedItem.ToString();
        }
    }
}
