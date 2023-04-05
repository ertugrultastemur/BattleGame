//Ertuğrul Taştemür b201200006

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using War.Library.Concrete;
using War.Library.Enum;

namespace War.Desktop
{
    public partial class AnaForm : Form
    {
        public readonly Oyun _oyun;
        public bool oyunBasladiMi;

        public AnaForm()
        {
            InitializeComponent();
            _oyun = new Oyun(tankPanel,savasAlaniPanel,puanLabel);
            _oyun.GecenSureDegisti += Oyun_GecenSureDegisti;

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            MenuForm menu = new MenuForm();
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    menu.ShowDialog();
                    if (menu.basildiMi)
                    {
                        if (oyunBasladiMi == false)
                        {
                            oyunBasladiMi = true;
                            _oyun.SiralamaEsitle();
                            _oyun.Baslat();

                        }
                        else _oyun.OyunuYenidenBaslat();
                    }

                    break;
                case Keys.Down:
                    _oyun.TankiHareketEttir(Yon.Asagi);
                    break;
                case Keys.Up:
                    _oyun.TankiHareketEttir(Yon.Yukari);
                    break;
                case Keys.Right:
                    _oyun.TankiHareketEttir(Yon.Saga);
                    break;
                case Keys.Left:
                    _oyun.TankiHareketEttir(Yon.Sola);
                    break;
                case Keys.Space:
                    _oyun.AtesEt();
                    break;
                case Keys.Escape:
                    if (_oyun.DevamEdiyorMu) _oyun.Duraklat();
                    break;
                case Keys.F1:
                    if (_oyun.DevamEdiyorMu) _oyun.DevamEttir();
                    break;
                case Keys.F2:
                    _oyun.OyunuYenidenBaslat();
                    break;
            }
        }

        public void Oyun_GecenSureDegisti(object sender,EventArgs e)
        {
            sureLabel.Text = _oyun.GecenSure.ToString(@"m\:ss");
        }

        private void AnaForm_Load(object sender, EventArgs e)
        {

        }
    }
}
