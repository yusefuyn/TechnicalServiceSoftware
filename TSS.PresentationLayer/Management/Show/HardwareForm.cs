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
using TSS.Entities.Concretes;
using TSS.UserInterface.Management.UserControls;

namespace TSS.UserInterface.Management.Show
{
    public partial class HardwareForm : Form
    {


        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );


        Hardwares hardware = null;
        List<Hardwares_Size> hardwareSize = null;
        List<Hardwares_Drive> hardwareDrive = null;

        List<Computer_Unit> computer_Units = null;
        List<Components_Type> components_Types = null;
        List<Components> comps = null;
        List<Hardwares_Component> hardwares_Components = null;
        List<Hardwares_Type> hardwares_Types = null;


        public HardwareForm(int HardwareID)
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            hardware = Program.hardwareManager.GetEntitieByFiltered(c => c.ID == HardwareID);
            hardwareSize = Program.hardwareSizeManager.GetAllEntitiesByFiltered(c => c.HardwaresID.ID == HardwareID);
            //hardwareDrive = Program.hardwareDriveManager.GetAllEntitiesByFiltered(c => c.Hardware.ID == HardwareID);
            hardwares_Components = Program.hardwaresComponentManager.GetAllEntitiesByFiltered(c => c.HardwaresID == HardwareID);
            components_Types = Program.componentTypeManager.getAllEntities();
            comps = Program.componentManager.getAllEntities();
            computer_Units = Program.computerUnitManager.getAllEntities();
            hardwares_Types = Program.hardwareTypeManager.getAllEntities();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void HardwareForm_Load(object sender, EventArgs e)
        {
            DataRefresh();
        }
        List<HardwareDriveObj> hardwareDriveObjs = new List<HardwareDriveObj>();
        private void DataRefresh()
        {
            hardwareDriveObjs = new List<HardwareDriveObj>();
            txtHardwareName.Text = hardware.Name;
            txtHardwareCompany.Text = hardware.Producer;
            //txtUnit.Text =  hardwares_Types.Find(x => x.ID == hardware.HardwareType).Name;
            txtHardwareModel.Text =hardware.Model;
            int i = 0;
            if (hardwareDrive != null)
            foreach (var item in hardwareDrive)
            {
                HardwareDriveObj hd = new HardwareDriveObj(item.Description + " :", item.Drive);
                hd.Location = new Point(0, 28 * i);
                pnlHardwareDrivers.Controls.Add(hd);
                i++;
                hardwareDriveObjs.Add(hd);
            }
            i = 0;
            foreach (var item in hardwareSize)
            {
                HardwareDriveObj hd = new HardwareDriveObj(item.Description + " :", item.Size.ToString() + " " + computer_Units.Find(x => x.ID == item.ComputerUnitID.ID).Name);
                hd.Location = new Point(0, 28 * i);
                pnlHardwareSize.Controls.Add(hd);
                i++;
                hardwareDriveObjs.Add(hd);
            }
            i = 0;
            foreach (var item in hardwares_Components)
            {
                Components compss = comps.Find(x => x.ID == item.ComponentID);
                HardwareDriveObj hd = new HardwareDriveObj(compss.Name, components_Types.Find(x => x.ID == compss.ComponentTypeId).Name);
                hd.Location = new Point(0, 28 * i);
                i++;
                pnlComponent.Controls.Add(hd);
                hardwareDriveObjs.Add(hd);
            }
            pnlComponent.Size = new Size(23, i * 28);
        }

        private void btnComponentAdd_Click(object sender, EventArgs e)
        {
            TSS.Tools.Theme.FormReDesign(new Management.Add.DevicesComponentAddForm(hardware.ID)).ShowDialog();
            DataRefresh();
        }

        private void HardwareForm_Click(object sender, EventArgs e)
        {
            pnlComponent.BackColor = Color.Red;
        }
    }
}
