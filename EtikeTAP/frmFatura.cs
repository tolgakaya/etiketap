using DevExpress.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Controls;
using DevExpress.XtraGrid.Accessibility;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.EditForm;
using System.Globalization;
using DevExpress.XtraReports.UI;
using System.IO;
using DevExpress.XtraBars;

namespace EtikeTAP
{
    public partial class frmFatura : Form
    {

        public frmFatura()
        {
            InitializeComponent();
        }

        private void frmTest_Load(object sender, EventArgs e)
        {
            BarButtonItem item4 = new BarButtonItem(barManager1, "Yeni Tasarım");
            item4.Name = "YeniTasarimButonu";
            popupMenu3.AddItems(new BarItem[] { item4 });
            item4.ItemClick += new ItemClickEventHandler(this.TasarimAc);

            string dizin = Directory.GetCurrentDirectory();
            string[] dizi = Directory.GetFiles(dizin, "*repx", SearchOption.AllDirectories);

            foreach (string s in dizi)
            {
                string caption = Path.GetFileNameWithoutExtension(s);

                //hepsini bas butonları
                BarButtonItem item1 = new BarButtonItem(barManager1, caption);
                item1.Name = s;
                popupMenu1.AddItems(new BarItem[] { item1 });
                item1.ItemClick += new ItemClickEventHandler(this.HepsiniBas);

                BarButtonItem item2 = new BarButtonItem(barManager1, caption);
                item2.Name = s;
                popupMenu2.AddItems(new BarItem[] { item2 });
                item2.ItemClick += new ItemClickEventHandler(this.SecileniBas);

                BarButtonItem item3 = new BarButtonItem(barManager1, caption);
                item3.Name = s;
                popupMenu3.AddItems(new BarItem[] { item3 });
                item3.ItemClick += new ItemClickEventHandler(this.TasarimAc);
            }

        

        }

        private void TasarimAc(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            using (new WaitIndicator())
            {
                frmTasarim fTasarim = new frmTasarim();
                fTasarim = new frmTasarim();
                fTasarim.yol = e.Item.Name;
                //fTasarim.MdiParent = this.Parent.f;
                fTasarim.Show();


            }
        }

        private void HepsiniBas(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (new WaitIndicator())
            {
                etiketX report = new etiketX();

                if (File.Exists(e.Item.Name))
                {
                    report.LoadLayout(e.Item.Name);
                }

                report.bindingSource1.DataSource = this.liste;

                ReportPrintTool tool = new ReportPrintTool(report);
                tool.ShowPreview();
            }

        }

        private void SecileniBas(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] secilenler = gridView1.GetSelectedRows();
            if (secilenler.Length > 0)
            {
                using (new WaitIndicator())
                {
                    List<Satislar> yeniListe = new List<Satislar>();
                    foreach (int i in secilenler)
                    {
                        frmBaski b = new frmBaski();
                        GridColumn col = gridView1.GetVisibleColumn(1);
                        string urun = gridView1.GetRowCellDisplayText(i, col);

                        Satislar s = this.liste.Find(x => x.urun == urun);
                        if (s != null)
                        {
                            yeniListe.Add(s);

                        }
                    }

                    etiketX report = new etiketX();

                    //fatura yolunu bulalım

                    if (File.Exists(e.Item.Name))
                    {
                        report.LoadLayout(e.Item.Name);
                    }

                    report.bindingSource1.DataSource = yeniListe;

                    ReportPrintTool tool = new ReportPrintTool(report);
                    tool.ShowPreview();
                }

            }
            else
            {
                MessageBox.Show("Seçili bir etiket bilgisi bulunmuyor.");
            }
        }
        private List<Satislar> liste { get; set; }


        private void barLargeButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Collect c = new Collect();
            //c.get();
            // var data = c.satislar.OrderBy(x=>x.Tarih);
            //this.liste = data.ToList();
            //var data = Dummy.get();

            //Birinci bir = new Birinci("birinci.xlsx");
            //bir.get();
            // List<BirinciHelper> data = bir.helper;

            // Ikinci iki = new Ikinci("ikinci.xlsx");
            //iki.get();
            // List<IkinciHelper> data = iki.helper;

            //Ucuncu uc = new Ucuncu("ucuncu.xlsx");
            //uc.get();
            //List<UcuncuHelper> data = uc.helper;

            //bindingSource1.DataSource = data;
            //gridControl1.DataSource = bindingSource1;

        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            //int[] secilenler = gridView1.GetSelectedRows();


            //foreach (int i in secilenler)
            //{
            //    frmBaski b = new frmBaski();
            //    GridColumn col = gridView1.GetVisibleColumn(1);
            //    int no = Int32.Parse(gridView1.GetRowCellDisplayText(i, col));

            //    Satislar s = this.liste.Find(x => x.No == no);
            //    Baski bas = new Baski(s, b.documentViewer1);
            //    bas.InternetBas();
            //    b.Show();

            //}




        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CultureInfo c = CultureInfo.CurrentCulture;
            string name = c.Name + " " + c.NativeName + " " + c.NumberFormat.CurrencyDecimalSeparator;


            MessageBox.Show(name);
            /*VeriTabani v = new VeriTabani();
            Satislar s = new Satislar();
            Decimal f = 1.2M;
           
            Random r = new Random();
            s.Ad = (r.NextDouble() + 1).ToString();
            s.Adres = (r.NextDouble() + 1).ToString(); ;
            s.Cinsi = (r.NextDouble() + 1).ToString();
            s.Fiyat = 1.2M;
            s.Miktar = (r.Next() + 1);
            s.No = (r.Next() + 1);
            s.Soyad = (r.NextDouble() + 1).ToString();
            s.Tarih = DateTime.Now;
            s.Vd = (r.NextDouble() + 1).ToString();
            s.Vno = (r.NextDouble() + 1).ToString();
            s.yazi = "yazi";
            s.tutar = 1.2M;
            s.toplam = 1.2M;
            s.oiv = 1.2M;
            s.kdv = 1.2M;
            s.genelToplam = 1.2M;

            v.ekle(s);*/


        }

        private void barLargeButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //int[] secilenler = gridView1.GetSelectedRows();
            //if (secilenler.Length > 0)
            //{
            //    using (new WaitIndicator())
            //    {
            //        List<Satislar> yeniListe = new List<Satislar>();
            //        foreach (int i in secilenler)
            //        {
            //            frmBaski b = new frmBaski();
            //            GridColumn col = gridView1.GetVisibleColumn(1);
            //            string urun = gridView1.GetRowCellDisplayText(i, col);

            //            Satislar s = this.liste.Find(x => x.urun == urun);
            //            if (s != null)
            //            {
            //                yeniListe.Add(s);

            //            }
            //        }


            //        etiketX report = new etiketX();

            //        string klasor = Directory.GetCurrentDirectory();

            //        string sutun = Properties.Settings.Default.sutun;

            //        string yol = klasor + @"\etiketX.repx";

            //        //fatura yolunu bulalım

            //        if (File.Exists(yol))
            //        {
            //            report.LoadLayout(yol);
            //        }

            //        report.bindingSource1.DataSource = yeniListe;

            //        ReportPrintTool tool = new ReportPrintTool(report);
            //        tool.ShowPreview();
            //    }

            //}
            //else
            //{
            //    MessageBox.Show("Seçili bir etiket bilgisi bulunmuyor.");
            //}

        }

        private void barLargeButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Collect c = new Collect();
            //c.get();
            //var data = c.satislar.OrderBy(x => x.Tarih);
            //this.liste = data.ToList();
            //bindingSource1.DataSource = data;
            //gridControl1.DataSource = bindingSource1;
        }

        private void barLargeButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {



        }

        private void gridControl1_DataSourceChanged(object sender, EventArgs e)
        {
            //int[] secilenler = gridView1.GetSelectedRows();


            //foreach (int i in secilenler)
            //{
            //    frmBaski b = new frmBaski();
            //    GridColumn col = gridView1.GetVisibleColumn(1);
            //    int no = Int32.Parse(gridView1.GetRowCellDisplayText(i, col));

            //    Satislar s = this.liste.Find(x => x.No == no);

            //    Baski bas = new Baski(s, b.documentViewer1);
            //    bas.InternetBas();
            //    s.basildi = true;
            //    b.Show();

            //}
            //bindingSource1.DataSource = this.liste;
            //gridControl1.DataSource = bindingSource1;
            //gridControl1.Refresh();
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            //int satir_no = e.RowHandle;



            //GridColumn col = gridView1.GetVisibleColumn(7);
            //// int no = Int32.Parse(gridView1.GetRowCellDisplayText(i, col));
            //bool basildi = Convert.ToBoolean(gridView1.GetRowCellValue(satir_no, col));
            //AppearanceObject obj = new AppearanceObject();
            //obj.BackColor = Color.Pink;

            //if (basildi == true)
            //{

            //    e.CombineAppearance(obj);

            //}
        }

        private void barButtonItem2_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barLargeButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barLargeButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem2_ItemClick_2(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (new WaitIndicator())
            {
                etiketX report = new etiketX();

                string klasor = Directory.GetCurrentDirectory();

                //fatura yolunu bulalım
                string yol = klasor + @"\etiketX.repx";

                if (File.Exists(yol))
                {
                    report.LoadLayout(yol);
                }

                report.bindingSource1.DataSource = this.liste;

                ReportPrintTool tool = new ReportPrintTool(report);
                tool.ShowPreview();
            }

        }

        private void barLargeButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.liste != null)
            {
                this.liste.Clear();
                this.bindingSource1.DataSource = this.liste;
                gridControl1.DataSource = bindingSource1;
                gridControl1.Refresh();
                gridView1.RefreshData();
            }

        }

        private void barLargeButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmEkle f = new frmEkle();
            if (f.ShowDialog() == DialogResult.OK)
            {
                if (!String.IsNullOrEmpty(f.txtBirim.Text) && !String.IsNullOrEmpty(f.txtBirimFiyat.Text) && !String.IsNullOrEmpty(f.txtFiyat.Text) && !String.IsNullOrEmpty(f.txtUretimYeri.Text) && !String.IsNullOrEmpty(f.txtUrun.Text))
                {
                    Satislar s = new Satislar();
                    string barkod = f.txtBarkod.Text;
                    if (!string.IsNullOrEmpty(barkod))
                    {
                        s.barkod = barkod;
                        s.barvar = true;
                    }
                    else
                    {
                        s.barvar = false;
                    }
                    s.birim = f.txtBirim.Text;
                    s.birim_fiyati = f.txtBirimFiyat.Text + " TL/" + f.txtBirim.Text;
                    s.satis_fiyati = f.txtFiyat.Text + " TL";
                    if (!String.IsNullOrEmpty(f.txtTarih.Text))
                    {
                        s.tarih = f.txtTarih.Text;
                    }
                    else
                    {
                        s.tarih = DateTime.Now.ToShortDateString();
                    }

                    string uretim = (f.txtUretimYeri.Text).ToUpper().Trim();
                    s.urun = f.txtUrun.Text;
                    s.uretim_yeri = uretim;
                    s.yerli = uretim.Equals("TÜRKİYE");
                    if (this.liste == null)
                    {
                        this.liste = new List<Satislar>();

                    }

                    this.liste.Add(s);
                    this.bindingSource1.DataSource = this.liste;
                    gridControl1.DataSource = bindingSource1;
                    gridControl1.Refresh();
                    gridView1.RefreshData();
                }

            }
            else
            {
                f.Close();
            }
        }

        private void barLargeButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] secilenler = gridView1.GetSelectedRows();

            if (secilenler.Length > 0)
            {
                foreach (int i in secilenler)
                {
                    GridColumn col = gridView1.GetVisibleColumn(1);
                    string urun = gridView1.GetRowCellDisplayText(i, col);

                    Satislar s = this.liste.Find(x => x.urun == urun);
                    if (s != null)
                    {
                        this.liste.Remove(s);

                    }
                }

                this.bindingSource1.DataSource = this.liste;
                gridControl1.DataSource = bindingSource1;
                gridControl1.Refresh();
                gridView1.RefreshData();
            }
            else
            {
                MessageBox.Show("Lütfen listeden çıkarmak istediğiniz etiket bilgisini seçiniz");
            }

        }

        private void barLargeButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (this.liste.Count > 0)
            //{
            //    using (new WaitIndicator())
            //    {
            //        etiketX report = new etiketX();

            //        string klasor = Directory.GetCurrentDirectory();

            //        //fatura yolunu bulalım
            //        string yol = klasor + @"\etiketX.repx";

            //        if (File.Exists(yol))
            //        {
            //            report.LoadLayout(yol);
            //        }

            //        report.bindingSource1.DataSource = this.liste;

            //        ReportPrintTool tool = new ReportPrintTool(report);
            //        tool.ShowPreview();
            //    }

            //}
            //else
            //{
            //    MessageBox.Show("Bastırılacak etiket listesi bulunmuyor. Ya excel belgesi yükleyin ya da listeye ekle butonu ile bir etiket bilgisi ekleyin.");
            //}

        }

        private void barLargeButtonItem12_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.liste.Count>0)
            {
                string klasor = Directory.GetCurrentDirectory();
                string yol = klasor + @"\etiketlistesi.xlsx";
                gridControl1.ExportToXlsx(yol);
            }
       
        }

        private void excelKayittan_ItemClick(object sender, ItemClickEventArgs e)
        {
            string dizin = Directory.GetCurrentDirectory();
            List<string> dizi = Directory.GetFiles(dizin, "*.xls*", SearchOption.AllDirectories).ToList();
            if (dizi.Count > 0)
            {
                using (new WaitIndicator())
                {
                    CollectVerili c = new CollectVerili(dizi.First());
                    c.get();
                    var data = c.satislar.ToList();
                    this.liste = data;
                    bindingSource1.DataSource = data;
                    gridControl1.DataSource = bindingSource1;
                }
            }


        }

        private void excelYukle_ItemClick(object sender, ItemClickEventArgs e)
        {
            string path = "";
            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "Excel Files|*.xls;*.xlsx;";
            choofdlog.FilterIndex = 1;
            choofdlog.Multiselect = false;

            if (choofdlog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                path = choofdlog.FileName;
                using (new WaitIndicator())
                {
                    CollectVerili c = new CollectVerili(path);
                    c.get();
                    var data = c.satislar.ToList();
                    this.liste = data;
                    bindingSource1.DataSource = data;
                    gridControl1.DataSource = bindingSource1;
                }

            }
        }


    }
}
