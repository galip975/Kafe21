using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafe21.Data
{
    public class KafeVeri : DbContext
    {
        public KafeVeri() : base("name=KafeVeri")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SiparisDetay>()//siparis detay entity'sinin
                .HasRequired(x => x.Urun)//zorunlu olarak urun'u vardir
                .WithMany(x => x.SiparisDetaylar)//ki bu urun birden çok siparisdetay'da bulunabilir
                .HasForeignKey(x => x.UrunId)//siparis detay'dan urun'e referans veren FK'sı urunId alanıdır.
                .WillCascadeOnDelete(false);//urun silinirse siparisdetay'ı otomatik silme
        }
        public int MasaAdet { get; set; } = 20; //default girdik.
        public DbSet<Urun> Urunler { get; set; }
        public DbSet<Siparis> Siparisler { get; set; }
        public DbSet<SiparisDetay> SiparisDetaylar { get; set; }
    }
}
