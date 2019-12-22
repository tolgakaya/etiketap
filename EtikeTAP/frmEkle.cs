using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EtikeTAP
{
    public partial class frmEkle : Form
    {
        public frmEkle()
        {
            InitializeComponent();
        }

        private string mesaj { get; set; }
        private void txtUrun_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(txtUrun.Text))
            {
                mesaj += "\n Lütfen ürün ismi giriniz";
            }
            lblError.ForeColor = Color.Red;
            lblError.Text = mesaj;
            lblError.Visible = true;
        }

        private void txtFiyat_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(txtFiyat.Text))
            {
                mesaj += "\n Lütfen satış fiyatı giriniz";
            }
            lblError.ForeColor = Color.Red;
            lblError.Text = mesaj;
            lblError.Visible = true;
        }

        private void txtBirimFiyat_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(txtBirimFiyat.Text))
            {
                mesaj += "\n Lütfen birim fiyat giriniz";
            }
            lblError.ForeColor = Color.Red;
            lblError.Text = mesaj;
            lblError.Visible = true;
        }

        private void txtBirim_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(txtBirim.Text))
            {
                mesaj += "\n Lütfen birim giriniz";
            }
            lblError.ForeColor = Color.Red;
            lblError.Text = mesaj;
            lblError.Visible = true;
        }

        private void txtUretimYeri_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(txtUretimYeri.Text))
            {
                mesaj += "\n Lütfen üretim yeri giriniz";
            }
            lblError.ForeColor = Color.Red;
            lblError.Text = mesaj;
            lblError.Visible = true;
        }
    }
}
