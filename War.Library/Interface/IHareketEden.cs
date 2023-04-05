//Ertuğrul Taştemür b201200006

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using War.Library.Enum;

namespace War.Library.Interface
{
    internal interface IHareketEden
    {
        int HareketMesafesi { get; }

        Size HareketAlaniBoyutlar { get; }

        bool HareketEttir(Yon yon);
    }
}
