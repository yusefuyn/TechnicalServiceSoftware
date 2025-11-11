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
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            pBCustomer.Image = new Bitmap("Images\\customer.gif");
            lblCustomer.Text = Program.customerManager.getAllEntities().Count.ToString() + " Müşteri";
            pbProduct.Image = new Bitmap("Images\\product.gif");
            lblProduct.Text = Program.productManager.getAllEntities().Count.ToString() + " Ürün";
            pbEmployee.Image = new Bitmap("Images\\employee.gif");
            lblEmployee.Text = Program.employeeManager.getAllEntities().Count.ToString() + " Çalışan";
            pbService.Image = new Bitmap("Images\\service.gif");
            lblService.Text = Program.troubleManager.getAllEntities().Count.ToString() + " Arıza";
            pbDevice.Image = new Bitmap("Images\\device.gif");
            lblDevice.Text = Program.deviceManager.getAllEntities().Count.ToString() + " Cihaz";
            pbSolution.Image = new Bitmap("Images\\solution.gif");
            lblSolution.Text = "0" + " Çözüm";
            pbHardware.Image = new Bitmap("Images\\hardware.gif");
            lblHardware.Text = Program.hardwareManager.getAllEntities().Count.ToString() + " Donanım";
            pbComponent.Image = new Bitmap("Images\\component.gif");
            lblComponent.Text = Program.componentManager.getAllEntities().Count.ToString() + " Komponent";
            pbAccount.Image = new Bitmap("Images\\account.gif");
            lblAccount.Text = "0" + " Hesap";
            pictureBox1.Image = new Bitmap("Images\\device.gif");
            pictureBox2.Image = new Bitmap("Images\\product.gif");
        }
    }
}
