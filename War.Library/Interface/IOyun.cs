//Ertuğrul Taştemür b201200006

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using War.Library.Enum;

namespace War.Library.Interface
{
    internal interface IOyun
    {
        event EventHandler GecenSureDegisti;
        void Baslat();
        void Duraklat();
        void AtesEt();
        void TankiHareketEttir(Yon yon);
        bool DevamEdiyorMu { get; }
        TimeSpan GecenSure { get; }
        int Puan { get; set; }

    }
}
