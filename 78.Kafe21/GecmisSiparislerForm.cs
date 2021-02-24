using Kafe21.Data;
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
    public partial class GecmisSiparislerForm : Form
    {
        private readonly KafeVeri kafeVeri;//readonly oluşturulduktan sonra 1 kere veri ata sonra değiştireme.

        public GecmisSiparislerForm(KafeVeri kafeVeri)
        {
            InitializeComponent();
            this.kafeVeri = kafeVeri; //classtakine this ile erişilir localdekine kendi ismiyle
            dgvSiparisler.DataSource = kafeVeri.Siparisler.Where(x=>x.Durum != SiparisDurum.Aktif).ToList();
        }
        private void dgvSiparisDetaylar_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSiparisler.SelectedRows.Count == 0)
            {
                dgvSiparisDetaylar.DataSource = null;
                return;
            }
            DataGridViewRow satir = dgvSiparisler.SelectedRows[0]; //seçili satırı al
            Siparis siparis = (Siparis)satir.DataBoundItem;//satırdaki bağlı nesneyi al(datasource'a attığımız için item ı alabildik.)
            dgvSiparisDetaylar.DataSource = siparis.SiparisDetaylar.ToList();
        }

        private void GecmisSiparislerForm_Load(object sender, EventArgs e)
        {
            dgvSiparisler.ClearSelection();
        }
    }
}
