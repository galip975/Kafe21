﻿using Kafe21.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _78.Kafe21
{
    public partial class UrunlerForm : Form
    {
        KafeVeri db;
        BindingList<Urun> blUrunler;
        public UrunlerForm(KafeVeri kafeVeri)
        {
            db = kafeVeri;
            InitializeComponent();
            dgvUrunler.AutoGenerateColumns = false; //otomatik sütun oluşturma biz elimizle girdik datapropertyname lerine classdaki değişkenlerin isimlerini verdik ki eşleşebilsin.
            blUrunler = new BindingList<Urun>(db.Urunler);
            dgvUrunler.DataSource = blUrunler;
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            string urunAd = txtUrunAd.Text.Trim();
            foreach (var item in db.Urunler)
            {
                if (item.UrunAd == urunAd)
                {
                    MessageBox.Show("Eklediğiniz ürün bulunmaktadır.Düzenleme işlemini tercih ediniz.");
                    return;
                }
            }
            if (urunAd == "")
            {
                MessageBox.Show("Ürün adı giriniz.");
                return;
            }

            if (duzenlenen == null)//Ekleme Modu
            {
                blUrunler.Add(new Urun()//bindinglist e eklediğimiz zaman hem datagridview hemde kafeverideki listelerimiz değişikliği anlıyor.
                {
                    UrunAd = urunAd,
                    BirimFiyat = nudBirimFiyat.Value
                });
            }

            else//Düzenleme Modu
            {
                duzenlenen.UrunAd = urunAd;
                duzenlenen.BirimFiyat = nudBirimFiyat.Value;
                blUrunler.ResetBindings();//Kendini yenile sende değişiklik var.
            }

            FormuResetle();
        }

        private void FormuResetle()
        {
            txtUrunAd.Clear();
            nudBirimFiyat.Value = 0;
            btnIptal.Visible = false;
            btnEkle.Text = "EKLE";
            duzenlenen = null;
            txtUrunAd.Focus();
        }

        Urun duzenlenen;//ister yukarı ister buraya yaz fark etmez sıra önemli değil class fielddır.
        private void dgvUrunler_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            var satir = dgvUrunler.Rows[e.RowIndex];
            Urun urun = (Urun)satir.DataBoundItem;
            txtUrunAd.Text = urun.UrunAd;
            nudBirimFiyat.Value = urun.BirimFiyat;
            btnEkle.Text = "Kaydet";
            btnIptal.Show();
            duzenlenen = urun;
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            FormuResetle();
        }
    }
}
