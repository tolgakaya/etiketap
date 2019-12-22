using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToExcel;
using LinqToExcel.Domain;
using LinqToExcel.Query;
namespace EtikeTAP
{
    class CollectVerili
    {
        //burada bütün excel tablolarındaki verileri toplayarak veritabanına hazır hale getiriyoruz
        public List<Satislar> satislar;

        string path;
        public CollectVerili(string path)
        {
            this.path = path;
            satislar = new List<Satislar>();
        }
        private ExcelQueryFactory dosya()
        {
            var excelFile = new ExcelQueryFactory(this.path);
            excelFile.DatabaseEngine = DatabaseEngine.Jet;
            excelFile.TrimSpaces = TrimSpacesType.Both;
            return excelFile;
        }
        public void get()
        {
            string dir = Properties.Settings.Default.sutun;

            var data = from a in dosya().Worksheet(0) select a;
            foreach (var a in data)
            {
                Satislar s = new Satislar();

                s.urun = a[0];
                string satis_fiyati = a[1];
                if (!satis_fiyati.Contains("TL"))
                {
                    satis_fiyati = satis_fiyati + " TL";
                }


                s.satis_fiyati = satis_fiyati;

                string birim_fiyat = a[2];
                if (!birim_fiyat.Contains("TL"))
                {
                    birim_fiyat = birim_fiyat + "TL";
                }
                if (!birim_fiyat.Contains("/"))
                {
                    birim_fiyat = birim_fiyat + "/" + a[3];
                }

                s.birim_fiyati = birim_fiyat;
                s.birim = a[3];
                string tarih = a[4];
                if (!String.IsNullOrEmpty(tarih))
                {
                    s.tarih = tarih.Trim();
                }
                else
                {
                    s.tarih = DateTime.Now.ToShortDateString();
                }

                string y = a[5];
                string yy = y.ToUpper().Trim();
                s.uretim_yeri = yy;
                string barkod = a[6];
                if (!String.IsNullOrEmpty(barkod))
                {
                    s.barkod = barkod.Trim();
                    s.barvar = true;
                }
                else
                {
                    s.barkod = string.Empty;
                    s.barvar = false;
                }
                s.yerli = yy.Equals("TÜRKİYE");
                satislar.Add(s);
            }
        }



    }
}
