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
    public partial class LoginForm : Form
    {
        TextBox focusableText = null;
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            StartSettingForm settingForm = new StartSettingForm();
            Theme.FormReDesign(settingForm);
            settingForm.ShowDialog();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap("Images\\hacker.gif");
            focusableText = txtUserName;
        }

        private void txtUserName_Click(object sender, EventArgs e)
        {
            focusableText = txtUserName;
        }

        private void txtPassword_Click(object sender, EventArgs e)
        {
            focusableText = txtPassword;

        }

        private void btnZero_Click(object sender, EventArgs e)
        {
            focusableText.Text += "0";
        }

        private void btnOne_Click(object sender, EventArgs e)
        {
            focusableText.Text += "1";
        }

        private void btnTwo_Click(object sender, EventArgs e)
        {
            focusableText.Text += "2";
        }

        private void btnThree_Click(object sender, EventArgs e)
        {
            focusableText.Text += "3";
        }

        private void btnFour_Click(object sender, EventArgs e)
        {
            focusableText.Text += "4";
        }

        private void btnFive_Click(object sender, EventArgs e)
        {
            focusableText.Text += "5";
        }

        private void btnSix_Click(object sender, EventArgs e)
        {
            focusableText.Text += "6";
        }

        private void btnSeven_Click(object sender, EventArgs e)
        {
            focusableText.Text += "7";
        }

        private void btnEight_Click(object sender, EventArgs e)
        {
            focusableText.Text += "8";
        }

        private void btnNine_Click(object sender, EventArgs e)
        {
            focusableText.Text += "9";
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (focusableText.Text.Length > 0)
                focusableText.Text = focusableText.Text.Substring(0, focusableText.Text.Length - 1);
        }
    }
}
