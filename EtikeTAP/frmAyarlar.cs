using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;


namespace EtikeTAP
{
    public partial class frmAyarlar : Form
    {
        public frmAyarlar()
        {
            InitializeComponent();
        }



        private void frmAyarlar_Load(object sender, EventArgs e)
        {
            //string sutun = Properties.Settings.Default.sutun;
            //comboBox1.SelectedIndex = Int32.Parse(sutun);

            tasarimlar();
        }

        private void tasarimlar()
        {
            string dizin = Directory.GetCurrentDirectory();
            string[] dizi = Directory.GetFiles(dizin, "*repx", SearchOption.AllDirectories);

            foreach (string s in dizi)
            {
                string caption = Path.GetFileNameWithoutExtension(s);
                listBoxControl1.Items.Add(caption);

            }
        }
        private void sil(string isim)
        {
            DialogResult dr = MessageBox.Show("Seçmiş olduğunuz tasarım silinsin mi?", "Dikkat!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                string klasor = Directory.GetCurrentDirectory();

                //fatura yolunu bulalım
                string yol = klasor + @"\" + isim + ".repx";
                try
                {
                    File.Delete(yol);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }

            }

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

            //Properties.Settings.Default.sutun = comboBox1.SelectedIndex.ToString();
            //Properties.Settings.Default.Save();
            //MessageBox.Show("Kaydedildi!");

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string secilen = listBoxControl1.SelectedValue.ToString();
            sil(secilen);
            listBoxControl1.Items.Remove(secilen);

        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string secilen = listBoxControl1.SelectedValue.ToString();
            sil(secilen);
            listBoxControl1.Items.Remove(secilen);
        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Kayıtlı varsayılan tasarımları indirmek istiyor musunuz? Aynı isimdeki tasarımların üzerine yazılabilir.", "Dikkat!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                using (new WaitIndicator())
                {
                    string url = "ftp://kitapcimiz.site/wp-content/uploads/etiketap/";

                    string klasor = Directory.GetCurrentDirectory();

                    NetworkCredential credentials = new NetworkCredential("etikettasarim@bilgitap.site", "T0m12233jfhq-");

                    DownloadFtpDirectory(url, credentials, klasor);

                    listBoxControl1.Items.Clear();
                    string[] dizi = Directory.GetFiles(klasor, "*repx", SearchOption.AllDirectories);

                    foreach (string s in dizi)
                    {
                        string caption = Path.GetFileNameWithoutExtension(s);
                        listBoxControl1.Items.Add(caption);
                    }
                }

            }

        }

        void DownloadFtpDirectory(string url, NetworkCredential credentials, string localPath)
        {
            FtpWebRequest listRequest = (FtpWebRequest)WebRequest.Create(url);
            listRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            listRequest.Credentials = credentials;
            listRequest.UsePassive = false;

            List<string> lines = new List<string>();

            using (FtpWebResponse listResponse = (FtpWebResponse)listRequest.GetResponse())
            using (Stream listStream = listResponse.GetResponseStream())
            using (StreamReader listReader = new StreamReader(listStream))
            {
                while (!listReader.EndOfStream)
                {
                    lines.Add(listReader.ReadLine());
                }
            }

            foreach (string line in lines)
            {

                string[] tokens =
                    line.Split(new[] { ' ' }, 9, StringSplitOptions.RemoveEmptyEntries);
                string name = tokens[8];
                if (name.Contains(".repx"))
                {
                    string permissions = tokens[0];

                    string localFilePath = Path.Combine(localPath, name);
                    string fileUrl = url + name;

                    if (permissions[0] == 'd')
                    {
                        if (!Directory.Exists(localFilePath))
                        {
                            Directory.CreateDirectory(localFilePath);
                        }

                        DownloadFtpDirectory(fileUrl + "/", credentials, localFilePath);
                    }
                    else
                    {
                        FtpWebRequest downloadRequest = (FtpWebRequest)WebRequest.Create(fileUrl);
                        downloadRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                        downloadRequest.Credentials = credentials;

                        using (FtpWebResponse downloadResponse =
                                  (FtpWebResponse)downloadRequest.GetResponse())
                        using (Stream sourceStream = downloadResponse.GetResponseStream())
                        using (Stream targetStream = File.Create(localFilePath))
                        {
                            byte[] buffer = new byte[10240];
                            int read;
                            while ((read = sourceStream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                targetStream.Write(buffer, 0, read);
                            }
                        }
                    }

                }


            }
        }
    }
}
