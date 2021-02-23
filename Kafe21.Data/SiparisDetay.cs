using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafe21.Data
{
    [Table("SiparisDetaylar")]
    public class SiparisDetay
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string UrunAd { get; set; }
        public decimal BirimFiyat { get; set; }
        public int Adet { get; set; }

        [NotMapped]//veri tabanına kaydedilmeyecek. seti olmadıgı için zaten eklenilmicek. garantiye aldık.
        public string TutarTL { get { return Tutar().ToString("N"); } } // set yoksa read only 

        public int UrunId { get; set; }
        public virtual Urun Urun { get; set; }

        public int SiparisId { get; set; }
        public virtual Siparis Siparis { get; set; }
        public decimal Tutar()
        {
            return BirimFiyat * Adet;
        }
    }
}
