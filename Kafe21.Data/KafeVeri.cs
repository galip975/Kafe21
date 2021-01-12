﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafe21.Data
{
    public class KafeVeri
    {
        public int MasaAdet { get; set; } = 20; //default girdik.
        public List<Siparis> AktifSiparisler { get; set; } = new List<Siparis>(); // defaultu boş liste.
        public List<Siparis> GecmisSiparisler { get; set; } = new List<Siparis>(); // defaultu boş liste .
        public List<Urun> Urunler { get; set; } = new List<Urun>(); // defaultu boş liste, yapmasaydık null olurdu.
    }
}
