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

            string FunkcjaPrzystosow = comboBoxFunkcjaPrzystosow.SelectedItem.ToString();

            List<double> x = null;
            List<double> y = null;

            if (FunkcjaPrzystosow == "Funkcja Zad2")
            {
                x = new List<double> { -1.00000, -0.80000, -0.60000, -0.40000, -0.20000, 0.00000, 0.20000,
                0.40000, 0.60000, 0.80000, 1.0000, 1.20000, 1.40000, 1.60000, 1.80000, 2.0000, 2.20000, 2.40000, 2.60000,
                2.80000, 3.00000, 3.20000, 3.40000, 3.60000, 3.80000, 4.00000, 4.20000, 4.40000, 4.60000, 4.80000, 5.00000,
                5.20000, 5.40000, 5.60000, 5.80000, 6.00000};

                y = new List<double> { 0.59554, 0.58813, 0.64181, 0.68587, 0.44783, 0.40836, 0.38241, -0.05933,
                -0.12478, -0.36847, -0.39935, -0.50881, -0.63435, -0.59979, -0.64107, -0.51808, -0.38127, -0.12349, -0.09624,
                0.27893, 0.48965, 0.33089, 0.70615, 0.53342, 0.43321, 0.64790, 0.48834, 0.18440, -0.02389, -0.10261, -0.33594,
                -0.35101, -0.62027, -0.55719, -0.66377, -0.62740};
            }

            GenerujAlgorytmGenetyczny(ZdMin, ZdMax, LBnp, LiczbaParametrow, LiczbaOsobnikow, LiczbaIteracji, turRozm, FunkcjaPrzystosow, x, y);
        }

        private void GenerujAlgorytmGenetyczny(int ZdMin, int ZdMax, int LBnp, int LiczbaParametrow, int LiczbaOsobnikow, int LiczbaIteracji, int turRozm, string FunkcjaPrzystosow, List<double> daneX = null, List<double> daneY = null)
        {
            var algorytm = new AlgorytmGenetyczny(ZdMin, ZdMax, LBnp, LiczbaParametrow, LiczbaOsobnikow, LiczbaIteracji, turRozm, FunkcjaPrzystosow, daneX, daneY);

            algorytm.ZapiszWynikiAlgorytmu = (linia) => WynikiAlgorytmu.Items.Add(linia);

            algorytm.StartAlgorytmu();
        }
    }
}
