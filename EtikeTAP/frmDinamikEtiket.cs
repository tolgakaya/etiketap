using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EtikeTAP
{
    public partial class frmDinamikEtiket : Form
    {
        public frmDinamikEtiket()
        {
            InitializeComponent();
        }
        private List<String> ozellikListesi { get; set; }
        //private List<dynamic> liste { get; set; }
        private void btnEtiketAlani_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Dictionary<string, string> properties = new Dictionary<string, string>();
            properties.Add("Ozellik1", "Ozellik1 Degeri");
            properties.Add("Ozellik2", "Ozellik2 Degeri");

            var dynamicObject = new ExpandoObject() as IDictionary<string, Object>;
            foreach (var property in properties)
            {
                dynamicObject.Add(property.Key, property.Value);
            }

            var liste = new List<dynamic>();
            liste.Add(dynamicObject);
            gridControl1.DataSource = liste;

        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            var liste = new List<dynamic>();

            if (this.ozellikListesi == null)
            {
                this.ozellikListesi = new List<string>();
            }
            this.ozellikListesi.Add("Birinci");
            this.ozellikListesi.Add("İkinci");

            if (this.ozellikListesi != null)
            {
                var dynamicObject = new ExpandoObject() as IDictionary<string, Object>;

                foreach (string ozellik in this.ozellikListesi)
                {
                    Dictionary<string, string> properties = new Dictionary<string, string>();
                    properties.Add(ozellik, "");

                    foreach (var property in properties)
                    {
                        dynamicObject.Add(property.Key, property.Value);
                    }
                }
                liste.Add(dynamicObject);
            }

            gridView1.Columns.Clear();
            gridControl1.DataSource = null;
            gridControl1.DataSource = liste;
        }


        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmDinamikAlanEkle f = new frmDinamikAlanEkle();
            if (f.ShowDialog() == DialogResult.OK)
            {

                var liste = new List<dynamic>();


                if (this.ozellikListesi == null)
                {
                    this.ozellikListesi = new List<string>();
                }

                foreach (TextBox tb in f.Controls.OfType<TextBox>())
                {
                    if (!String.IsNullOrEmpty(tb.Text))
                    {
                        this.ozellikListesi.Add(tb.Text);
                    }
                }

                if (this.ozellikListesi != null)
                {
                    var dynamicObject = new ExpandoObject() as IDictionary<string, Object>;

                    foreach (string ozellik in this.ozellikListesi)
                    {
                        Dictionary<string, string> properties = new Dictionary<string, string>();
                        properties.Add(ozellik, "");

                        foreach (var property in properties)
                        {
                            dynamicObject.Add(property.Key, property.Value);
                        }
                    }
                    liste.Add(dynamicObject);
                }


                DataTable dt = Cevir.ToDataTable(liste);

                this.bindingSource1.DataSource = dt;
                gridControl1.DataSource = bindingSource1;
                gridControl1.Refresh();
                gridView1.RefreshData();


            }
            else
            {
                f.Close();
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.gridView1.Columns.Clear();

        }
    }
}
