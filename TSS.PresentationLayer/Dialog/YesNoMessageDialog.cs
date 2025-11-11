using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TSS.UserInterface.Dialog
{
    public partial class YesNoMessageDialog : Form
    {
        public YesNoMessageDialog(string Message, string Title, TypeEnum typeEnum, int width = 300)
        {
            InitializeComponent();
            label1.Text = Message;
        }

        private void YesNoMessageDialog_Load(object sender, EventArgs e)
        {

        }

        public enum TypeEnum
        {
            Soru
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }
    }


}
