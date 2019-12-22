using LinqToExcel;
using LinqToExcel.Domain;
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
using System.Reflection;
using QLicense;
using Lisans;
namespace EtikeTAP
{
    public partial class frmAna : Form
    {
        byte[] _certPubicKeyData;

        public frmAna()
        {
            InitializeComponent();
        }

        frmFatura fFatura;

        protected void lisans()
        {
            //Initialize variables with default values
            MyLicense _lic = null;
            string _msg = string.Empty;
            LicenseStatus _status = LicenseStatus.UNDEFINED;

            //Read public key from assembly
            Assembly _assembly = Assembly.GetExecutingAssembly();
            using (MemoryStream _mem = new MemoryStream())
            {
                _assembly.GetManifestResourceStream("EtikeTAP.LicenseVerify.cer").CopyTo(_mem);

                _certPubicKeyData = _mem.ToArray();
            }

            //Check if the XML license file exists
            if (File.Exists("license.lic"))
            {
                _lic = (MyLicense)LicenseHandler.ParseLicenseFromBASE64String(
                    typeof(MyLicense),
                    File.ReadAllText("license.lic"),
                    _certPubicKeyData,
                    out _status,
                    out _msg);
            }
            else
            {
                _status = LicenseStatus.INVALID;
                _msg = "Programınızın lisansı yok. Lütfen lisanslama için arayın. 0506 946 86 93";
            }

            switch (_status)
            {
                case LicenseStatus.VALID:

                    //TODO: If license is valid, you can do extra checking here
                    //TODO: E.g., check license expiry date if you have added expiry date property to your license entity
                    //TODO: Also, you can set feature switch here based on the different properties you added to your license entity 

                    //Here for demo, just show the license information and RETURN without additional checking       
                    //licInfo.ShowLicenseInfo(_lic);

                    return;

                default:
                    //for the other status of license file, show the warning message
                    //and also popup the activation form for user to activate your application
                    MessageBox.Show(_msg, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    using (frmActivation frm = new frmActivation())
                    {
                        frm.CertificatePublicKeyData = _certPubicKeyData;
                        frm.ShowDialog();
                        //Exit the application after activation to reload the license file 
                        //Actually it is not nessessary, you may just call the API to reload the license file
                        //Here just simplied the demo process

                        Application.Exit();
                    }
                    break;
            }
        }
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            lisans();

            if (fFatura == null || fFatura.IsDisposed)
            {
                fFatura = new frmFatura();
                fFatura.MdiParent = this;
                fFatura.Show();
            }
            else if (fFatura.WindowState == FormWindowState.Minimized)
            {
                fFatura.WindowState = FormWindowState.Maximized;
            }
            else
            {
                fFatura.Activate();
            }

        }




        frmAyarlar fAyar;
        private void barButtonItem4_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fAyar == null || fAyar.IsDisposed)
            {
                fAyar = new frmAyarlar();
                fAyar.MdiParent = this;
                fAyar.Show();
            }
            else if (fAyar.WindowState == FormWindowState.Minimized)
            {
                fAyar.WindowState = FormWindowState.Normal;
            }
            else
            {
                fAyar.Activate();
            }
        }

        private void frmAna_Load(object sender, EventArgs e)
        {

        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmInfo f = new frmInfo();
            f.ShowDialog();
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmLisans l = new frmLisans();
            l.MdiParent = this;
            l.Show();
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmLisans f = new frmLisans();
            f.MdiParent = this;
            f.Show();
        }

        private void btnDinamikEtiket_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmDinamikEtiket f = new frmDinamikEtiket();
            f.MdiParent = this;
            f.Show();
        }



    }


}
