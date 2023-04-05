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
    internal class Canavar:Cisimler
    {
        private static Random Random = new Random();
        public Canavar(Size hareketAlaniBoyutlar) : base(hareketAlaniBoyutlar)
        {
            HareketMesafesi = (int) (Height * 0.3);
            Left = Random.Next(hareketAlaniBoyutlar.Width-Width+1);
        }

        public Mermi VurulduMu(List<Mermi> mermiler)
        {
            foreach (var mermi in mermiler)
            {
                var vurulduMu = mermi.Top < Bottom && mermi.Right > Left && mermi.Left < Right;
                if (vurulduMu) return mermi;
            }
            return null;
        }
    }
}
