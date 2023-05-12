using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSAY_LATLONG_UTM_GRID
{
    public partial class FrmAbout : Form
    {
        public FrmAbout()
        {
            InitializeComponent();
        }

        private void FrmAbout_Load(object sender, EventArgs e)
        {
            string msg;
            msg = "Created by Ajay Yadav" +
                "\ngithub.com/ajayyadavay" +
                "\n\nHow to use?" +
                "\n1. Prepared text file as per given format" +
                "\n2. File>Load RWY data" +
                "\n3. Calculate> baseline parameter" +
                "\n4. Calculate> Gridlines" +
                "\n5. Draw> Gridlines and Draw> Circle" +
                "\n6. Export gridlines and cricle" +
                "\n\nNote1: You can change the mode as per requirement" +
                "\nNote2: Mode North means one gridlines will be parallel to North" +
                "\nNote3: Mode Baseline means one gridlines will be parallel to baseline";
            label1.Text = msg;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
