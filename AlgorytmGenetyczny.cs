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
    }
}