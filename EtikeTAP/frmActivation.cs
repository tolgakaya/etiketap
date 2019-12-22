using System;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using QLicense;
using Lisans;

namespace EtikeTAP
{
    public partial class frmActivation : Form
    {
        public byte[] CertificatePublicKeyData { private get; set; }
       
        public frmActivation()
        {
            InitializeComponent();
        }

        private void frmActivation_Shown(object sender, EventArgs e)
        {
      
        }

        private void btnTamam_Click(object sender, EventArgs e)
        {
            //Call license control to validate the license string
            if (licActCtrl.ValidateLicense())
            {
                //If license if valid, save the license string into a local file
                File.WriteAllText(Path.Combine(Application.StartupPath, "license.lic"), licActCtrl.LicenseBASE64String);

                MessageBox.Show("Lisans kabul edildi. Şimdi program kapatılacak, lütfen yeniden başlatın.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
        }

        private void frmActivation_Load(object sender, EventArgs e)
        {
            //Assign the application information values to the license control
            licActCtrl.AppName = "EtikeTAP";
            licActCtrl.LicenseObjectType = typeof(Lisans.MyLicense);
            licActCtrl.CertificatePublicKeyData = this.CertificatePublicKeyData;
            //Display the device unique ID
            licActCtrl.ShowUID();
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("İptal etmek istiyor musunuz?", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
