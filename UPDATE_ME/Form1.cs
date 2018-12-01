using Ionic.Zip;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace UPDATE_ME
{
    public partial class Form1 : Form
    {
        string Yol = Application.StartupPath ;
        string URLL = "https://github.com/mehmet02/mecesoft/raw/master/mece.zip";
        string Dosyaadi = "mece.zip";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(Yol + @"\Temp"))
            {
                Directory.CreateDirectory(Yol + @"\Temp");
            }
            if (!File.Exists(Yol + @"\MeceSoft.exe"))
            {

            }
            else
            {
                File.Copy("MeceSoft.exe", Yol + @"\Temp\MeceSoft.exe", true);
            }
        }
        void ZipAc()
        {
            using (ZipFile zip1 = ZipFile.Read(Yol+"\\mece.zip"))
            {
                foreach (ZipEntry e in zip1)
                {
                    e.Extract(Yol + @"\", ExtractExistingFileAction.OverwriteSilently);
                }
            }
        }
        void Dosyaİndir(string Link, string Dizin, string DosyaAdı)
        {
            WebClient İstemci = new WebClient();
            İstemci.DownloadFileCompleted += new AsyncCompletedEventHandler(DosyaİndirmeTamamlandığında);
            İstemci.DownloadProgressChanged += new DownloadProgressChangedEventHandler(İndirmeİlerlemesi);
            İstemci.DownloadFileAsync(new Uri(URLL), Dizin + @"\" + DosyaAdı);
        }

        void İndirmeİlerlemesi(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBarControl1.Position = (int)e.TotalBytesToReceive / 100;
            progressBarControl1.Position = (int)e.BytesReceived / 100;
        }

        void DosyaİndirmeTamamlandığında(object sender, AsyncCompletedEventArgs e)
        {
            ZipAc();
            MessageBox.Show("Program Başarıyla Güncellenmiştir.");
            this.Close();
        }
        private void btnGuncel_Click(object sender, EventArgs e)
        {
            Dosyaİndir(URLL, Yol, Dosyaadi);
            btnGuncel.Enabled = false;
            btnKapat.Enabled = false;
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}