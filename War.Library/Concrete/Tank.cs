//Ertuğrul Taştemür b201200006

using System.Drawing;
using System.Windows.Forms;
using War.Library.Abstract;

namespace War.Library.Concrete
{
    internal class Tank : Cisimler
    {
        public Tank(int panelUzunluk,Size HareketAlaniBoyutlar): base(HareketAlaniBoyutlar)
        {
            Center = panelUzunluk /2;
            HareketMesafesi = Width;
        }
    }
}
