using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EtikeTAP
{
    public partial class frmTasarim : Form
    {
        public frmTasarim()
        {
            InitializeComponent();

        }

        public string yol { get; set; }
        private void frmTasarim_Load(object sender, EventArgs e)
        {

            XtraReport report = new etiketX();
            if (File.Exists(this.yol))
            {

                report.LoadLayout(this.yol);
            }

            this.reportDesigner1.OpenReport(report);

        }

        private void commandBarItem33_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string klasor = Directory.GetCurrentDirectory();
            string path = klasor + @"\etiketX.repx";

        }

        private void commandBarItem32_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }


    }
}
