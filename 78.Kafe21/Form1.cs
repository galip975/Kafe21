using _78.Kafe21.Properties;
using Kafe21.Data;
using Newtonsoft.Json;
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

namespace _78.Kafe21
{
    public partial class Form1 : Form
    {
        KafeVeri db;
        public Form1()
        {
            InitializeComponent();
            VerileriOku();
            MasalariYukle();
        }

        private void VerileriOku()
        {
            try
            {
                string json = File.ReadAllText("veri.json");
                db = JsonConvert.DeserializeObject<KafeVeri>(json);
            }
            catch (Exception)
            {
                db = new KafeVeri();
            }
        }

        private void MasalariYukle()
        {
            ImageList imgList = new ImageList();
            imgList.Images.Add("bos", Resources.bos2); //imageları keyleri ile birlikte veriyoruz.
            imgList.Images.Add("dolu", Resources.dolu2);
            imgList.ImageSize = new Size(64, 64); //imageların boyutu
            lvwMasalar.LargeImageList = imgList; //imageListesini listview'e imagaList olarak ekliyoruz.

            for (int i = 1; i < db.MasaAdet + 1; i++)
            {
                ListViewItem lvi = new ListViewItem("Masa" + i);
                bool doluMu = db.AktifSiparisler.Any(x => x.MasaNo == i);
                lvi.ImageKey = doluMu ? "dolu" : "bos"; //burdada keyini vererek hangi imageları vereceğimizi belirtiyoruz.
                lvi.Tag = i;//masaNo
                lvwMasalar.Items.Add(lvi);
            }
        }

        private void OrnekVerileriYukle()
        {
            db.Urunler.Add(new Urun()
            {
                UrunAd = "Ayran",
                BirimFiyat = 4.50m
            });
            db.Urunler.Add(new Urun()
            {
                UrunAd = "Kola",
                BirimFiyat = 5.00m
            });
        }

        private void tsmiUrunler_Click(object sender, EventArgs e)
        {
            UrunlerForm frmUrunler = new UrunlerForm(db);
            frmUrunler.ShowDialog();
        }

        private void tsmiGecmisSiparisler_Click(object sender, EventArgs e)
        {
            GecmisSiparislerForm frmGecmisSiparislerForm = new GecmisSiparislerForm(db);
            frmGecmisSiparislerForm.ShowDialog();
        }

        private void lvwMasalar_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem lvi = lvwMasalar.SelectedItems[0]; //seçili öğenin ilkini al.
            int masaNo = (int)lvi.Tag;
            Siparis siparis = db.AktifSiparisler.FirstOrDefault(x => x.MasaNo == masaNo);//sipariş varsa sipariş gelicek yoksa sipariş eklicez.
            if (siparis == null)
            {
                siparis = new Siparis() { MasaNo = masaNo };
                db.AktifSiparisler.Add(siparis);
                lvi.ImageKey = "dolu";
            }
            SiparisForm siparisForm = new SiparisForm(db, siparis);
            siparisForm.MasaTasindi += SiparisForm_MasaTasindi;
            DialogResult dr = siparisForm.ShowDialog();
            if (dr == DialogResult.OK)
            {
                lvi.ImageKey = "bos";
            }
        }

        private void SiparisForm_MasaTasindi(object sender, MasaTasindiEventArgs e)
        {
            lvwMasalar.Items.Cast<ListViewItem>().First(x => (int)x.Tag == e.EskiMasaNo).ImageKey = "bos";
            lvwMasalar.Items.Cast<ListViewItem>().First(x => (int)x.Tag == e.YeniMasaNo).ImageKey = "dolu";

            //foreach (ListViewItem lvi in lvwMasalar.Items)
            //{
            //    if ((int)lvi.Tag == e.EskiMasaNo)
            //    {
            //        lvi.ImageKey = "bos";
            //    }
            //    else if ((int)lvi.Tag == e.YeniMasaNo)
            //    {
            //        lvi.ImageKey = "dolu";
            //    }
            //}
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            VerileriKaydet();
        }

        private void VerileriKaydet()
        {
            var json = JsonConvert.SerializeObject(db,Formatting.Indented);//düzgün kaydeder.
            File.WriteAllText("veri.json", json);
        }
    }
}
