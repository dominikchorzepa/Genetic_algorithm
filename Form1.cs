using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Program
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void buttonGenerujAlgorytm_Click(object sender, EventArgs e)
        {
            WynikiAlgorytmu.Items.Clear();

            if (!int.TryParse(textBoxLiczbaIteracji.Text, out int LiczbaIteracji))
            {
                MessageBox.Show("Nieprawidłowa liczba iteracji!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBoxZDMin.Text, out int ZdMin))
            {
                MessageBox.Show("Nieprawidłowy zakres min!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBoxZDMax.Text, out int ZdMax))
            {
                MessageBox.Show("Nieprawidłowy zakres max!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBoxLiczbaOsobnikow.Text, out int LiczbaOsobnikow))
            {
                MessageBox.Show("Nieprawidłowa liczba osobników!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBoxLiczbaParam.Text, out int LiczbaParametrow))
            {
                MessageBox.Show("Nieprawidłowa liczba parametrów!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBoxLiczbaBitowNaParam.Text, out int LBnp))
            {
                MessageBox.Show("Nieprawidłowa liczba bitów na parametr!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBoxRozmiarTurnieju.Text, out int turRozm))
            {
                MessageBox.Show("Nieprawidłowy rozmiar turnieju!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            GenerujAlgorytmGenetyczny(ZdMin, ZdMax, LBnp, LiczbaParametrow, LiczbaOsobnikow, LiczbaIteracji, turRozm);
        }

        private void GenerujAlgorytmGenetyczny(int ZdMin, int ZdMax, int LBnp, int LiczbaParametrow, int LiczbaOsobnikow, int LiczbaIteracji, int turRozm)
        {
            var algorytm = new AlgorytmGenetyczny(ZdMin, ZdMax, LBnp, LiczbaParametrow, LiczbaOsobnikow, LiczbaIteracji, turRozm);

            algorytm.ZapiszWynikiAlgorytmu = (linia) => WynikiAlgorytmu.Items.Add(linia);
        }
    }
}
