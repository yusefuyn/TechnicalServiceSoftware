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
    public partial class ProductCategoryListForm : Form
    {
        public ProductCategoryListForm()
        {
            InitializeComponent();
        }
        private void refreshData() {
            dgvProductCategory.DataSource = Program.productCategoryManager.getAllEntities();

        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text == "")
                refreshData();
            else
                dgvProductCategory.DataSource = Program.productCategoryManager.GetAllEntitiesByFiltered(t => t.Name == txtSearch.Text);

        }

        private void FormProductCategory_Load(object sender, EventArgs e)
        {
            dgvProductCategory.DefaultCellStyle.BackColor = Color.FromArgb(28, 28, 28);
            refreshData();

            dgvProductCategory.Columns[0].Visible = false;
        }

        private void dgvProductCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = dgvProductCategory.CurrentRow.Cells[0].Value.ToString();
            txtName.Text = dgvProductCategory.CurrentRow.Cells[1].Value.ToString();
            numTax.Value = Convert.ToInt32(dgvProductCategory.CurrentRow.Cells[2].Value.ToString());
        }

        private void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "secilenid")
            {
                Dialog.MessageDialog dialog = new Dialog.MessageDialog("Ürün seçilmemiş.", "Hata", Dialog.MessageDialog.TypeEnum.Hata);
                dialog.ShowDialog();
                return;
            }
            Entities.Concretes.Products_Category productCategory = new Entities.Concretes.Products_Category()
            { Name = txtName.Text, Tax = Convert.ToInt32(numTax.Value) };
            Program.productCategoryManager.UpdateEntities(Convert.ToInt32(txtID.Text),productCategory);
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            Dialog.YesNoMessageDialog messageBox = new Dialog.YesNoMessageDialog("Seçili Ürün Kategori(" + dgvProductCategory.CurrentRow.Cells[1].Value + ") silmek istediğinize eminmisiniz ?", "Karar Anı :P", Dialog.YesNoMessageDialog.TypeEnum.Soru);
            if (messageBox.ShowDialog() == DialogResult.Yes)
            {
                if (txtID.Text != "secilenid")
                {
                    Program.productCategoryManager.DeleteEntities(Convert.ToInt32(txtID.Text));
                    refreshData();
                }
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            Entities.Concretes.Products_Category productCategory = new Entities.Concretes.Products_Category()
            { Name = txtName.Text, Tax = Convert.ToInt32(numTax.Value) };
            Program.productCategoryManager.AddEntities(productCategory);
            refreshData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
