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

            GenerujAlgorytmGenetyczny();
        }

        private void GenerujAlgorytmGenetyczny()
        {
            int ZDMin = 0;
            int ZDMax = 100;
            int ZD = ZDMax - ZDMin;
            int LBnP = 3;
            int TurRozm = 3;
            int liczbaParametrow = 2;
            int liczbaOsobnikow = 9;
            int LBnOs = LBnP * liczbaParametrow;
            int liczbaIteracji = 20;

            List<(string chromosom, double przystosowanie)> populacja = new List<(string, double)>();

            Random rnd = new Random();

            for (int i = 0; i < liczbaOsobnikow; i++)
            {
                double x1_pm = rnd.NextDouble() * ZD + ZDMin;
                double x2_pm = rnd.NextDouble() * ZD + ZDMin;

                string x1_bin = Zakodowanie(x1_pm, ZDMin, ZDMax, LBnP);
                string x2_bin = Zakodowanie(x2_pm, ZDMin, ZDMax, LBnP);

                string chromosom = x1_bin + x2_bin;

                double przystosowanie = FunkcjaPrzystosowania(x1_pm, x2_pm);

                populacja.Add((chromosom, przystosowanie));
            }

            WynikiAlgorytmu.Items.Add("--- Populacja początkowa ---");

            foreach (var k in populacja)
            {
                WynikiAlgorytmu.Items.Add("Wartość chromosomu: " + k.chromosom + " Wartość funkcji: " + Math.Round(k.przystosowanie, 2));
            }

            double maxymalneP = populacja[0].przystosowanie;
            double sumaP = 0;

            foreach (var k in populacja)
            {
                if (k.przystosowanie > maxymalneP)
                {
                    maxymalneP = k.przystosowanie;
                }
                sumaP += k.przystosowanie;
            }
            WynikiAlgorytmu.Items.Add("Najlepsza funkcja osobnika: " + Math.Round(maxymalneP, 2));
            WynikiAlgorytmu.Items.Add("Średnia wartość przystosowania: " + Math.Round(sumaP / populacja.Count, 2) + "\n");

            for (int i = 0; i < liczbaIteracji; i++)
            {
                WynikiAlgorytmu.Items.Add("--- Pokolenie " + (i + 1) + " ---");

                List<(string chromosom, double przystosowanie)> nowaPopulacja = new List<(string, double)>();

                for (int j = 0; j < liczbaOsobnikow - 1; j++)
                {
                    string zwyciezca = SelekcjaTurniejowa(populacja, TurRozm);
                    string zmutowany = Mutacja(zwyciezca, LBnOs);

                    string x1_bin = zmutowany.Substring(0, LBnP);
                    string x2_bin = zmutowany.Substring(LBnP);

                    double x1_pm = Dekodowanie(x1_bin, ZDMin, ZDMax, LBnP);
                    double x2_pm = Dekodowanie(x2_bin, ZDMin, ZDMax, LBnP);

                    double przystosowanie = FunkcjaPrzystosowania(x1_pm, x2_pm);

                    nowaPopulacja.Add((zmutowany, przystosowanie));
                }

                string najlepszyOsobnik = SelekcjaHotDeck(populacja);

                string x1_best = najlepszyOsobnik.Substring(0, LBnP);
                string x2_best = najlepszyOsobnik.Substring(LBnP);

                double x1_best_pm = Dekodowanie(x1_best, ZDMin, ZDMax, LBnP);
                double x2_best_pm = Dekodowanie(x2_best, ZDMin, ZDMax, LBnP);

                double najlepszaOcena = FunkcjaPrzystosowania(x1_best_pm, x2_best_pm);

                nowaPopulacja.Add((najlepszyOsobnik, najlepszaOcena));

                double maxPrzystosowanie = populacja[0].przystosowanie;
                double sumaPrzystosowania = 0;

                foreach (var k in populacja)
                {
                    if (k.przystosowanie > maxPrzystosowanie)
                    {
                        maxPrzystosowanie = k.przystosowanie;
                    }
                    sumaPrzystosowania += k.przystosowanie;
                    WynikiAlgorytmu.Items.Add("Wartość chromosomu: " + k.chromosom + " Wartość funkcji: " + Math.Round(k.przystosowanie, 2));
                }

                WynikiAlgorytmu.Items.Add("Najlepsza funkcja osobnika: " + Math.Round(maxPrzystosowanie, 2));
                WynikiAlgorytmu.Items.Add("Średnia wartość przystosowania: " + Math.Round(sumaPrzystosowania / populacja.Count, 2) + "\n");

                populacja = nowaPopulacja;
            }
        }

        static string Zakodowanie(double pm, int ZDMin, int ZDMax, int LBnP)
        {
            int ZD = ZDMax - ZDMin;
            int[] cb = new int[LBnP];

            double ctmp = Math.Round(((pm - ZDMin) / ZD) * (Math.Pow(2, LBnP) - 1));

            for (int b = 0; b < LBnP; b++)
            {
                cb[b] = (int)Math.Floor(ctmp / Math.Pow(2, b)) % 2;
            }

            Array.Reverse(cb);
            return string.Join("", cb);
        }

        static double Dekodowanie(string cb, int ZDMin, int ZDMax, int LBnP)
        {
            int ZD = ZDMax - ZDMin;
            int ctmp = 0;

            for (int b = 0; b < LBnP; b++)
            {
                ctmp += (cb[b] - '0') * (int)Math.Pow(2, LBnP - 1 - b);
            }

            return ZDMin + (ctmp / (Math.Pow(2, LBnP) - 1)) * ZD;
        }

        static double FunkcjaPrzystosowania(double x1, double x2)
        {
            return Math.Sin(x1 * 0.05) + Math.Sin(x2 * 0.05) + 0.4 * Math.Sin(x1 * 0.15) + Math.Sin(x2 * 0.15);
        }
        static string SelekcjaTurniejowa(List<(string chromosom, double przystosowanie)> populacja, int TurRozm)
        {
            Random rnd = new Random();

            List<(string chromosom, double przystosowanie)> turniej = new List<(string, double)>();

            for (int i = 0; i < TurRozm; i++)
            {
                int losowanie = rnd.Next(populacja.Count);
                turniej.Add(populacja[losowanie]);
            }

            string najlepszyOsobnik = turniej[0].chromosom;
            double najlepszaOcena = turniej[0].przystosowanie;

            for (int i = 0; i < turniej.Count; i++)
            {
                if (turniej[i].przystosowanie > najlepszaOcena)
                {
                    najlepszyOsobnik = turniej[i].chromosom;
                    najlepszaOcena = turniej[i].przystosowanie;
                }
            }

            return najlepszyOsobnik;
        }

        static string SelekcjaHotDeck(List<(string chromosom, double przystosowanie)> populacja)
        {
            double najlepszaOcena = double.MinValue;
            string najlepszyOsobnik = "";

            foreach (var k in populacja)
            {
                if (k.przystosowanie > najlepszaOcena)
                {
                    najlepszaOcena = k.przystosowanie;
                    najlepszyOsobnik = k.chromosom;
                }
            }

            return najlepszyOsobnik;
        }

        static (string, string) OperatorKrzyzowania(string rodzic_1, string rodzic_2, int LBnOs)
        {
            Random rnd = new Random();
            int b_ciecie = rnd.Next(0, LBnOs - 1);

            char[] potomek_1 = new char[LBnOs];
            char[] potomek_2 = new char[LBnOs];

            for (int b = 0; b <= b_ciecie; b++)
            {
                potomek_1[b] = rodzic_1[b];
                potomek_2[b] = rodzic_2[b];
            }

            for (int b = b_ciecie + 1; b < LBnOs; b++)
            {
                potomek_1[b] = rodzic_2[b];
                potomek_2[b] = rodzic_1[b];
            }

            string potomek1 = new string(potomek_1);
            string potomek2 = new string(potomek_2);

            return (potomek1, potomek2);
        }

        static string Mutacja(string chromosom, int LBnOs)
        {
            Random rnd = new Random();
            int b_punkt = rnd.Next(0, LBnOs);

            char[] chromosomTablica = chromosom.ToCharArray();

            if (chromosomTablica[b_punkt] == '1')
            {
                chromosomTablica[b_punkt] = '0';
            }
            else
            {
                chromosomTablica[b_punkt] = '1';
            }

            string chromosomWyjscie = new string(chromosomTablica);

            return chromosomWyjscie;
        }
    }
}
