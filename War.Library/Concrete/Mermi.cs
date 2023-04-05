//Ertuğrul Taştemür b201200006

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using War.Library.Abstract;

namespace War.Library.Concrete
{
    internal class Mermi:Cisimler
    {
        public Mermi(Size hareketAlaniBoyutlar,int namluUcuOrtaKonum) : base(hareketAlaniBoyutlar)
        {
            BasladigiKonumuAyarla(namluUcuOrtaKonum);
            HareketMesafesi = (int)(Height * 1.2);
        }

        private void BasladigiKonumuAyarla(int namluUcuOrtaKonum)
        {
            Bottom = HareketAlaniBoyutlar.Height;
            Center = namluUcuOrtaKonum;
        }
    }
}
