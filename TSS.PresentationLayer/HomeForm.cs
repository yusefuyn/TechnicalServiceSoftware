using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TSS.Tools;
namespace TSS.UserInterface
{
    public partial class HomeForm : Form
    {
        public HomeForm()
        {
            InitializeComponent();
        }

        string UserName = "";
        string Password = "";

        Form _chilForm = null;
        Button _ActiveButton = null;
        private void chilFormOpen(Form chilForm, Button btn)
        {
            if (_chilForm != null)
            {
                _chilForm.Close();
                _chilForm = null;
            }
            if (_ActiveButton != null)
            {
                _ActiveButton.BackColor = Theme.backgroundColor;
                _ActiveButton.ForeColor = Theme.themeColor;
                _ActiveButton = null;
            }
            if (btn != null)
            {
                _ActiveButton = btn;
                _ActiveButton.BackColor = Theme.themeColor;
                _ActiveButton.ForeColor = Theme.backgroundColor;
            }
            _chilForm = chilForm;
            _chilForm.TopLevel = false;
            _chilForm.Dock = DockStyle.Fill;
            _chilForm.Dock = DockStyle.None;
            label1.Text = btn.Text;
            _chilForm.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
            panel1_reverse.Controls.Add(_chilForm);
            Theme.FormReDesign(chilForm);
            _chilForm.Show();
        }


        private void HomeForm_Load(object sender, EventArgs e)
        {
            if (UserName == "" && Password == "")
            {
                LoginForm loginForm = new LoginForm();
                Theme.FormReDesign(loginForm);
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    UserName = loginForm.txtUserName.Text;
                    Password = loginForm.txtPassword.Text;
                    Theme.FormReDesign(this);
                }
            }
            btnHome.PerformClick();
            //btnChangeTheme.Image = new Bitmap("Images/Dark.gif");
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            pBoxLogo.Image = new Bitmap("Images\\customer.gif");
            chilFormOpen(new Management.Show.CustomerListForm(panel1_reverse), btnCustomer);
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            pBoxLogo.Image = new Bitmap("Images\\employee.gif");
            chilFormOpen(new Management.Show.EmployeeListForm(), btnEmployee);
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            pBoxLogo.Image = new Bitmap("Images\\product.gif");
            chilFormOpen(new Management.Show.ProductListForm(), btnProduct);
        }

        private void btnTrouble_Click(object sender, EventArgs e)
        {
            pBoxLogo.Image = new Bitmap("Images\\service.gif");
            chilFormOpen(new Management.Show.TroubleListForm(), btnTrouble);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Dialog.YesNoMessageDialog yesNoDialog = new Dialog.YesNoMessageDialog("Çıkmak istiyormusunuz ?", "??", Dialog.YesNoMessageDialog.TypeEnum.Soru, this.Size.Width);
            if (yesNoDialog.ShowDialog() == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        bool dark = true;

        private void btnMaximized_Click(object sender, EventArgs e)
        {
            if (dark)
            {
                Theme.themeColor = Color.FromArgb(211, 211, 211);
                Theme.backgroundColor = Color.FromArgb(51, 52, 53);
                Theme.FormReDesign(this);
                dark = false;
                btnChangeTheme.Image = new Bitmap("Images\\Light.gif");
            }
            else if (!dark)
            {
                Theme.themeColor = Color.FromArgb(51, 52, 53);
                Theme.backgroundColor = Color.FromArgb(211, 211, 211);
                Theme.FormReDesign(this);
                dark = true;
                btnChangeTheme.Image = new Bitmap("Images\\Dark.gif");
            }
            _ActiveButton.PerformClick();
        }

        private void btnMinimized_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnHardware_Click(object sender, EventArgs e)
        {
            pBoxLogo.Image = new Bitmap("Images\\Hardware.gif");
            chilFormOpen(new Management.Show.HardwareListForm(), btnHardware);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            //pBoxLogo.Image = new Bitmap("Images\\settings.gif");
            chilFormOpen(new Management.ApplicationSettings(), btnSettings);
        }

        private void btnDevices_Click(object sender, EventArgs e)
        {
            pBoxLogo.Image = new Bitmap("Images\\device.gif");
            chilFormOpen(new Management.Show.DevicesListForm(), btnDevices);
        }

        private void btnSolution_Click(object sender, EventArgs e)
        {
            pBoxLogo.Image = new Bitmap("Images\\solution.gif");
        }

        private void btnComponents_Click(object sender, EventArgs e)
        {
            pBoxLogo.Image = new Bitmap("Images\\component.gif");
            chilFormOpen(new Management.Show.ComponentListForm(), btnComponents);
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            pBoxLogo.Image = new Bitmap("Images\\account.gif");

        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            pBoxLogo.Image = new Bitmap("Images\\home.gif");
            chilFormOpen(new Management.Show.Home(), btnHome);
        }
    }
}
