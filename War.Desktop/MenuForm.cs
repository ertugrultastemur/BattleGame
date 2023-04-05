using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace War.Desktop
{
    public partial class MenuForm : Form
    {
        public bool basildiMi;
        public MenuForm()
        {
            InitializeComponent();
            basildiMi = false;
        }

        public void MenuForm_Load(object sender, EventArgs e)
        {
            
        }

        public void button1_Click(object sender, EventArgs e)
        {
            Close();
            basildiMi = true;
        }

        public void VeriOku()
        {
            FileStream fs = new FileStream("Siralama.txt", FileMode.Open, FileAccess.Read);
            StreamReader sw = new StreamReader(fs);
            string yazi = sw.ReadLine();
            while (yazi != null)
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[] siralama = new int[5];
            {
                FileStream tempFileStream = new FileStream("SiralamaTemp.txt", FileMode.Open, FileAccess.Read);
                StreamReader tempStreamReader = new StreamReader(tempFileStream);
                for (int i = 0; i < 5; i++)
                {
                    siralama[i] += Convert.ToInt32(tempStreamReader.ReadLine());
                }
                tempFileStream.Close();
                tempStreamReader.Close();
            }
            MessageBox.Show(siralama[0] + "\n" + siralama[1] + "\n" + siralama[2] + "\n" + siralama[3] + "\n" +
                            siralama[4]);
        }
    }
}
