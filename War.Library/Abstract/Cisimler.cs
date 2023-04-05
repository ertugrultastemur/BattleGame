//Ertuğrul Taştemür b201200006

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using War.Library.Enum;
using War.Library.Interface;

namespace War.Library.Abstract
{
    public abstract class Cisimler:PictureBox, IHareketEden
    {
        public int HareketMesafesi { get; protected set; }
        public Size HareketAlaniBoyutlar { get; }
        public new int Right 
        { 
            get=> base.Right; 
            set=> Left =value-Width;
        }
        public new int Bottom
        {
            get=> base.Bottom; 
            set=> Top=value-Height;
        }
        public int Center
        {
            get => Left + Width / 2;
            set => Left = value - Width / 2;
        }
        public int Mid
        {
            get => Top + Height / 2;
            set => Top = value - Height / 2;
        }

        protected Cisimler(Size hareketAlaniBoyutlar)
        {
            Image = System.Drawing.Image.FromFile($@"Gorseller\{GetType().Name}.png");
            HareketAlaniBoyutlar = hareketAlaniBoyutlar;
            SizeMode = PictureBoxSizeMode.AutoSize;
        }

        public bool HareketEttir(Yon yon)
        {
            switch (yon)
            {
                case Yon.Yukari:
                    return YukariHareketEttir();
                case Yon.Asagi:
                    return AsagiHareketEttir();
                case Yon.Saga:
                    return SagaHareketEttir();
                case Yon.Sola:
                    return SolaHareketEttir();
                default:
                    throw new ArgumentOutOfRangeException(nameof(yon), yon, null);
            }
        }

        private bool SolaHareketEttir()
        {
            if (Left == 0) return true;
            var tastiMi = Left - HareketMesafesi < 0;
            Left = tastiMi ? 0 : Left - HareketMesafesi;
            return Left == 0;
        }

        private bool SagaHareketEttir()
        {
            if (Right == HareketAlaniBoyutlar.Width) return true;
            var tastiMi = (Right + HareketMesafesi) > HareketAlaniBoyutlar.Width;
            Right = tastiMi ? HareketAlaniBoyutlar.Width : (Right + HareketMesafesi);
            return Right == HareketAlaniBoyutlar.Width;

        }

        private bool AsagiHareketEttir()
        {
            if (Bottom == HareketAlaniBoyutlar.Height) return true;
            var tastiMi = (Bottom + HareketMesafesi) > HareketAlaniBoyutlar.Height;
            Bottom = tastiMi ? HareketAlaniBoyutlar.Height : (Bottom + HareketMesafesi);
            return Bottom == HareketAlaniBoyutlar.Width;
        }

        private bool YukariHareketEttir()
        {
            if (Top == 0) return true;
            var tastiMi = Top - HareketMesafesi < 0;
            Top = tastiMi ? 0 : Top - HareketMesafesi;
            return Top == 0;
        }
    }
}
