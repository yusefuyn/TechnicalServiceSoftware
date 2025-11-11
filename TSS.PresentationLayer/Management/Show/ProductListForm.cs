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
    public partial class ProductListForm : Form
    {

        int TotalItem = 0, selectedItem = 0;
        public ProductListForm()
        {
            InitializeComponent();
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            if (lblSelectedItem.Text == "secilenid")
            {
                Dialog.MessageDialog dialog = new Dialog.MessageDialog("Ürün seçilmemiş.", "Hata", Dialog.MessageDialog.TypeEnum.Hata);
                dialog.ShowDialog();
                return;
            }

            Dialog.YesNoMessageDialog messageBox = new Dialog.YesNoMessageDialog("Seçili Ürün (" + dgvProduct.CurrentRow.Cells[1].Value + ") silmek istediğinize eminmisiniz ?", "Karar Anı :P", Dialog.YesNoMessageDialog.TypeEnum.Soru);
            if (messageBox.ShowDialog() == DialogResult.Yes)
            {
                if (lblSelectedItem.Text != "secilenid")
                {
                    Program.productManager.DeleteEntities(Convert.ToInt32(lblSelectedItem.Text));
                    refreshData(Program.productManager.getAllEntities());
                }
            }

        }

        private void refreshData(List<Entities.Concretes.Products> dataSource)
        {

            List<Hardwares_Type> hardwares_Types = Program.hardwareTypeManager.getAllEntities();
            List<Products_Category> products_Categories = Program.productCategoryManager.getAllEntities();
            dgvProduct.Rows.Clear();
            foreach (var item in dataSource)
                dgvProduct.Rows.Add(new string[] { item.ID.ToString(),
                    item.Name,
                    item.Brand,
                    item.Model,
                    //hardwares_Types.Find(z => z.ID == item.HardwareTypeID.ID).Name,
                    products_Categories.Find(x=> x.ID == item.CategoryID.ID).Name
                });
            TotalItem = dgvProduct.Rows.Count;
            lblSelectedItem.Text = string.Format("Total obje sayısı {0} seçilen obje {1}", TotalItem, selectedItem);
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            Add.ProductAddForm productForm = new Add.ProductAddForm();
            if (productForm.ShowDialog() == DialogResult.OK)
                refreshData(Program.productManager.getAllEntities());
        }

        private void FormProduct_Load(object sender, EventArgs e)
        {
            dgvProduct.Columns.Add("C1", "ID");
            dgvProduct.Columns.Add("C2", "Isim");
            dgvProduct.Columns.Add("C3", "Marka");
            dgvProduct.Columns.Add("C4", "Model");
            dgvProduct.Columns.Add("C5", "Tip");
            dgvProduct.Columns.Add("C6", "Kategori");
            dgvProduct.Columns[0].Visible = false;

            dgvProduct.DefaultCellStyle.BackColor = Color.FromArgb(28, 28, 28);
            //dgvTheme();
            refreshData(Program.productManager.getAllEntities());
        }

        private void dgvTheme()
        {
            //dgvProduct.BorderStyle = BorderStyle.None;
            //dgvProduct.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(56, 56, 56);
            //dgvProduct.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            //dgvProduct.DefaultCellStyle.SelectionBackColor = Color.Maroon;
            //dgvProduct.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            //dgvProduct.BackgroundColor = Color.FromArgb(28, 28, 28);

            //dgvProduct.EnableHeadersVisualStyles = false;
            //dgvProduct.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            //dgvProduct.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            //dgvProduct.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            if (lblSelectedItem.Text == "secilenid")
            {
                Dialog.MessageDialog dialog = new Dialog.MessageDialog("Ürün seçilmemiş.", "Hata", Dialog.MessageDialog.TypeEnum.Hata);
                dialog.ShowDialog();
                return;
            }
            Management.Add.ProductAddForm productAddForm = new Add.ProductAddForm(selectedItem);
            if (productAddForm.ShowDialog() == DialogResult.OK)
                refreshData(Program.productManager.getAllEntities());
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
                dgvProduct.DataSource = Program.productManager.GetAllEntitiesByFiltered(t => t.Name == txtSearch.Text);
            else
                refreshData(Program.productManager.getAllEntities());
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedItem = Convert.ToInt32(dgvProduct.CurrentRow.Cells[0].Value.ToString());
            lblSelectedItem.Text = string.Format("Total obje sayısı {0} seçilen obje {1}", TotalItem, selectedItem);
        }

        private void btnAllProduct_Click(object sender, EventArgs e)
        {
            refreshData(Program.productManager.getAllEntities());
        }

        private void btnService_Click(object sender, EventArgs e)
        {
            refreshData(Program.productManager.GetAllEntitiesByFiltered(t => t.CategoryID.ID == 2));
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            refreshData(Program.productManager.GetAllEntitiesByFiltered(t => t.CategoryID.ID == 1));
        }

        private void btnProductPrice_Click(object sender, EventArgs e)
        {
            ProductPriceListForm form = new ProductPriceListForm();
            form.ShowDialog();
        }
    }
}
