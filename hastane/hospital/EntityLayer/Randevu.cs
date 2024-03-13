using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class Randevu
    {
        public int RandevuKodu { get; set; }
        public string HastaKimlik { get; set; }
        public string DoktorKimlik { get; set; }
        public DateTime RandevuTarihi { get; set; }
        public DateTime RandevuSaati { get; set; }

    }
}
