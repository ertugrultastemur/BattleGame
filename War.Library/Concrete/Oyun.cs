//Ertuğrul Taştemür b201200006

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Mime;
using War.Library.Enum;
using War.Library.Interface;
using System.Windows.Forms;




namespace War.Library.Concrete
{
    public class Oyun:IOyun
    {
        #region Alanlar

        private Timer _gecenSureTimer = new Timer{Interval = 1000};
        private Timer _hareketTimer = new Timer { Interval = 200 };
        private Timer _canavarOlusturTimer = new Timer { Interval = 1500 };
        private TimeSpan _GecenSure;
        private readonly Panel _tankPanel;
        private readonly Panel _savasAlaniPanel;
        private Tank _tank;
        private readonly List<Mermi> _mermiler = new List<Mermi>();
        private readonly List<Canavar> _canavarlar = new List<Canavar>();
        private int _vurulanUcakSayi=0;
        private Label _puanLabel;
        private int[] _siralama = new int[6];

        #endregion

        #region Olaylar

        public event EventHandler GecenSureDegisti;

        #endregion

        #region Özellikler

        public bool DevamEdiyorMu { get; private set; }

        public TimeSpan GecenSure
        {
            get => _GecenSure;
            private set
            {
                _GecenSure = value;
                GecenSureDegisti?.Invoke(this,EventArgs.Empty);
            }

        }

        public int Puan { get; set; }

        #endregion

        #region Methodlar

        public Oyun(Panel tankPanel, Panel savasAlaniPanel,Label puanLabel)
        {
            _tankPanel = tankPanel;
            _savasAlaniPanel = savasAlaniPanel;
            _gecenSureTimer.Tick += GecenSureTimer_Tick;
            _hareketTimer.Tick += HareketTimer_Tick;
            _canavarOlusturTimer.Tick += CanavarOlustur_Tick;
            _puanLabel = puanLabel;
        }

        public void OyunuYenidenBaslat()
        {
            GecenSure =TimeSpan.FromSeconds(0);
            _vurulanUcakSayi = 0;
            _savasAlaniPanel.Controls.Clear();
            _tankPanel.Controls.Clear();
            _mermiler.Clear();
            _canavarlar.Clear();
            TankOlustur();
            CanavarOlustur();
            TimersBaslat();
            DevamEdiyorMu = true;
        }

        public void TankiHareketEttir(Yon yon)
        {
            if (!DevamEdiyorMu) return;
            _tank.HareketEttir(yon);
        }

        public void TankOlustur()
        {
            _tank = new Tank(_tankPanel.Width*2, _tankPanel.Size);
            _tankPanel.Controls.Add(_tank);
        }
        public void GecenSureTimer_Tick(object sender, EventArgs e)
        {
            GecenSure += TimeSpan.FromSeconds(1);
        }
        public void HareketTimer_Tick(object sender, EventArgs e)
        {
            MermiHareketEttir();
            CanavarHareketEttir();
            VurulanUcaklariKaldir();
            PuanBildir();
            HareketZorlukArttir();
        }

        private void HareketZorlukArttir()
        {
           _hareketTimer.Interval = 100;
        }

        private void VurulanUcaklariKaldir()
        {
            for (int i = _canavarlar.Count-1; i >= 0; i--)
            {
                var canavar = _canavarlar[i];
                var vuranMermi = canavar.VurulduMu(_mermiler);
                if (vuranMermi is null) continue;
                _canavarlar.Remove(canavar);
                _savasAlaniPanel.Controls.Remove(canavar);
                _mermiler.Remove(vuranMermi);
                _savasAlaniPanel.Controls.Remove(vuranMermi);
                _vurulanUcakSayi++;
            }
        }

        private void CanavarHareketEttir()
        {
            foreach (var canavar in _canavarlar)
            {
                var asagiCarptiMi = canavar.HareketEttir(Yon.Asagi);
                if (!asagiCarptiMi) continue;
                Bitir();
                break;
            }
        }

        private void CanavarOlustur_Tick(object sender, EventArgs e)
        {
            if(_vurulanUcakSayi == 3)CanavarZorlukArttir();
            CanavarOlustur();
        }

        private void CanavarZorlukArttir()
        {
            _canavarOlusturTimer.Interval = 1000;
        }

        private void MermiHareketEttir()
        {
            for (int i=_mermiler.Count-1; i>=0;i--)
            {
                var mermi = _mermiler[i];
                var yukariCarptiMi =mermi.HareketEttir(Yon.Yukari);
                if (yukariCarptiMi)
                {
                    _mermiler.Remove(mermi);
                    _savasAlaniPanel.Controls.Remove(mermi);
                }
            }
        }

        public void Baslat()
        {
            if (DevamEdiyorMu) return;
            TankOlustur();
            CanavarOlustur();
            TimersBaslat();
            DevamEdiyorMu = true;
        }

        private void CanavarOlustur()
        {
            var canavar = new Canavar(_savasAlaniPanel.Size);
            _canavarlar.Add(canavar);
            _savasAlaniPanel.Controls.Add(canavar);
        }

        private void TimersBaslat()
        {
            _gecenSureTimer.Start();
            _hareketTimer.Start();
            _canavarOlusturTimer.Start();
        }

        private void Bitir()
        {
            if (!DevamEdiyorMu) return;
            PuanSiraylaDosyala(Puan);
            TimersDurdur();
            DevamEdiyorMu = false;
        }

        private void TimersDurdur()
        {
            _gecenSureTimer.Stop();
            _hareketTimer.Stop();
            _canavarOlusturTimer.Stop();
        }

        public void Duraklat()
        {
            _gecenSureTimer.Stop();
            _hareketTimer.Stop();
            _canavarOlusturTimer.Stop();
        }

        public void DevamEttir()
        {
            _gecenSureTimer.Start();
            _hareketTimer.Start();
            _canavarOlusturTimer.Start();
        }

        public void PuanBildir()
        {
            Puan = _vurulanUcakSayi * 5;
            _puanLabel.Text = $"Puan: {Puan}";

        }
        public void AtesEt()
        {
            if (!DevamEdiyorMu) return;
            var mermi = new Mermi(_savasAlaniPanel.Size,_tank.Center);
            _mermiler.Add(mermi);
            _savasAlaniPanel.Controls.Add(mermi);
        }

        public void SiralamaEsitle()
        {
            FileStream tempFileStream = new FileStream("SiralamaTemp.txt", FileMode.Open, FileAccess.Read);
            StreamReader tempStreamReader = new StreamReader(tempFileStream);
            for (int i = 0; i < 5; i++)
            {
                _siralama[i] = Convert.ToInt32(tempStreamReader.ReadLine());

            }
            tempFileStream.Close();
            tempStreamReader.Close();
        }
        public void PuanSiraylaDosyala(int puan)
        {
            _siralama[5] = puan;
            File.Copy("SiralamaTemp.txt", "Siralama.txt", true);
            if (puan > _siralama[4])
            {
                FileStream fileStream = new FileStream("Siralama.txt", FileMode.OpenOrCreate, FileAccess.Write);
                FileStream tempFileStream = new FileStream("SiralamaTemp.txt", FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter streamWriter = new StreamWriter(fileStream);
                for (int i = 4; i >= 0; i--)
                {
                    if (_siralama[i + 1] > _siralama[i])
                    {
                        int geciciI = _siralama[i];
                        _siralama[i] =_siralama[i + 1];
                        _siralama[i + 1] = geciciI;
                    }
                }

                for (int i = 0; i < 5; i++)
                {
                    streamWriter.WriteLine(_siralama[i]);
                }
                
                streamWriter.Flush();
                streamWriter.Close();
                tempFileStream.Close();
                fileStream.Close();
                File.Copy("Siralama.txt", "SiralamaTemp.txt", true);
            }
        }

        #endregion
    }
}
