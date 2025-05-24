using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    internal class AlgorytmGenetyczny
    {
        public int ZDMin;
        public int ZDMax;
        public int LBnP;
        public int liczbaParametrow;
        public int liczbaOsobnikow;
        int LBnOs;
        public int liczbaIteracji;
        public int TurRozm;
        public Random rnd;

        public List<Osobnik> populacja;

        public AlgorytmGenetyczny(int ZdMin, int ZdMax, int LBnp, int LiczbaParametrow, int LiczbaOsobnikow, int LiczbaIteracji, int turRozm)
        {
            ZDMin = ZdMin;
            ZDMax = ZdMax;
            LBnP = LBnp;
            liczbaParametrow = LiczbaParametrow;
            liczbaOsobnikow = LiczbaOsobnikow;
            LBnOs = LBnP * liczbaParametrow;
            liczbaIteracji = LiczbaIteracji;
            TurRozm = turRozm;
            rnd = new Random();
            populacja = new List<Osobnik>();
        }

        public void PopulacjaPoczatkowa()
        {
            for (int i = 0; i < liczbaOsobnikow; i++)
            {
                double[] pm = new double[liczbaParametrow];
                string chromosom = "";

                for (int p = 0; p < liczbaParametrow; p++)
                {
                    pm[p] = rnd.NextDouble() * (ZDMax - ZDMin) + ZDMin;
                    chromosom += Zakodowanie(pm[p], ZDMin, ZDMax, LBnP);
                }

                double przystosowanie = FunkcjaPrzystosowania(pm);
                populacja.Add(new Osobnik(chromosom, przystosowanie));
            }
        }

        public string Zakodowanie(double pm, int ZDMin, int ZDMax, int LBnP)
        {
            int ZD = ZDMax - ZDMin;
            int[] cb = new int[LBnP];

            double ctmp = Math.Round(((pm - ZDMin) / ZD) * Math.Pow(2, LBnP) - 1);

            for (int b = 0; b < LBnP; b++)
            {
                cb[b] = (int)Math.Floor(ctmp / Math.Pow(2, b)) % 2;
            }

            Array.Reverse(cb);
            return string.Join("", cb);
        }

        public double FunkcjaPrzystosowania(double[] x)
        {
            double x1 = x[0];
            double x2 = x[1];
            return Math.Sin(x1 * 0.05) + Math.Sin(x2 * 0.05) + 0.4 * Math.Sin(x1 * 0.15) + Math.Sin(x2 * 0.15);
        }

        public string SelekcjaTurniejowa(List<Osobnik> populacja, int TurRozm)
        {
            Random rnd = new Random();
            List<Osobnik> turniej = new List<Osobnik>();

            for (int i = 0; i < TurRozm; i++)
            {
                int losowanie = rnd.Next(populacja.Count);
                turniej.Add(populacja[losowanie]);
            }

            string najlepszyOsobnik = turniej[0].Chromosom;
            double najlepszaOcena = turniej[0].Przystosowanie;

            for (int i = 0; i < turniej.Count; i++)
            {
                if (turniej[i].Przystosowanie > najlepszaOcena)
                {
                    najlepszyOsobnik = turniej[i].Chromosom;
                    najlepszaOcena = turniej[i].Przystosowanie;
                }
            }

            return najlepszyOsobnik;
        }

        public string SelekcjaHotDeck(List<Osobnik> populacja)
        {
            double najlepszaOcena = double.MinValue;
            string najlepszyOsobnik = "";

            foreach (var k in populacja)
            {
                if (k.Przystosowanie > najlepszaOcena)
                {
                    najlepszaOcena = k.Przystosowanie;
                    najlepszyOsobnik = k.Chromosom;
                }
            }

            return najlepszyOsobnik;
        }

        public string Mutacja(string chromosom, int LBnOs)
        {
            Random rnd = new Random();
            int b_punkt = rnd.Next(0, LBnOs);

            char[] chromosomTablica = chromosom.ToCharArray();

            if (chromosomTablica[b_punkt] == '1')
            {
                chromosomTablica[b_punkt] = '0';
            } else
            {
                chromosomTablica[b_punkt] = '1';
            }

            string chromosomWyjscie = new string(chromosomTablica);

            return chromosomWyjscie;
        }
    }
}