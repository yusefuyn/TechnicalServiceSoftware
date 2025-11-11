using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TSS.UserInterface.Management.Add
{
    public partial class DevicesComponentAddForm : Form
    {
        int _id = 0;
        public DevicesComponentAddForm(int ID)
        {
            InitializeComponent();
            _id = ID;
            dgvCustomize();
        }

        private void dgvCustomize()
        {
            dataGridView2.Columns.Add("ID","id");
            dataGridView2.Columns.Add("Devid", "DevicesID");
            dataGridView2.Columns.Add("Compid","Component Adı");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDeleteDevicesComponent_Click(object sender, EventArgs e)
        {
            Program.hardwaresComponentManager.DeleteEntities(Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value.ToString()));
            DataRefresh();
        }

        private void btnAddDevicesForComponent_Click(object sender, EventArgs e)
        {
            Program.hardwaresComponentManager.AddEntities(new Entities.Concretes.Hardwares_Component()
            {
                
                HardwaresID = _id,
                ComponentID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString()),
                Description = ""
            });
            DataRefresh();
        }

        private void DataRefresh()
        {
            dataGridView1.DataSource = Program.componentManager.getAllEntities();
            dataGridView2.Rows.Clear();
            foreach (var item in Program.hardwaresComponentManager.GetAllEntitiesByFiltered(x=>x.ID == _id))
                dataGridView2.Rows.Add(item.ID,item.HardwaresID,item.ComponentID);
        }

        private void DevicesComponentAddForm_Load(object sender, EventArgs e)
        {
            DataRefresh();
        }
    }
}
