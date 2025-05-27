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

        public Action<string> ZapiszWynikiAlgorytmu;

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

        public List<Osobnik> KolejnePopulacje()
        {
            List<Osobnik> nowaPopulacja = new List<Osobnik>();

            for (int i = 0; i < liczbaOsobnikow - 1; i++)
            {
                string zwyciezca = SelekcjaTurniejowa(populacja, TurRozm);
                nowaPopulacja.Add(new Osobnik(zwyciezca, 0));
            }

            for (int i = 0; i < nowaPopulacja.Count - 1; i += 2)
            {
                var (potomek1, potomek2) = OperatorKrzyzowania(nowaPopulacja[i].Chromosom, nowaPopulacja[i + 1].Chromosom, LBnOs);
                nowaPopulacja[i] = new Osobnik(potomek1, 0);
                nowaPopulacja[i+1] = new Osobnik(potomek2, 0);
            }

            for (int i = 4; i < nowaPopulacja.Count; i++)
            {
                string zmutowany = Mutacja(nowaPopulacja[i].Chromosom, LBnOs);
                nowaPopulacja[i] = new Osobnik(zmutowany, 0);
            }

            string najlepszyOsobnik = SelekcjaHotDeck(populacja);
            nowaPopulacja.Add(new Osobnik(najlepszyOsobnik, 0));

            for (int i = 0; i < nowaPopulacja.Count; i++)
            {
                string chromosom = nowaPopulacja[i].Chromosom;
                double[] pm = DekodowanieChromosomu(chromosom);
                double przystosowanie = FunkcjaPrzystosowania(pm);
                nowaPopulacja[i] = new Osobnik(chromosom, przystosowanie);
            }

            return nowaPopulacja;
        }

        public void WypiszStatystyki()
        {
            double maxPrzystosowanie = populacja[0].Przystosowanie;
            double sumaPrzystosowania = 0;

            foreach (var osobnik in populacja)
            {
                if (osobnik.Przystosowanie > maxPrzystosowanie)
                {
                    maxPrzystosowanie = osobnik.Przystosowanie;
                }
                sumaPrzystosowania += osobnik.Przystosowanie;
                ZapiszWynikiAlgorytmu?.Invoke("Wartość chromosomu: " + osobnik.Chromosom + " Wartość funkcji: " + Math.Round(osobnik.Przystosowanie, 6));
            }

            ZapiszWynikiAlgorytmu?.Invoke("Najlepsza funkcja osobnika: " + Math.Round(maxPrzystosowanie, 10));
            ZapiszWynikiAlgorytmu?.Invoke("Średnia wartość przystosowania: " + Math.Round(sumaPrzystosowania / populacja.Count, 10) + "\n");
        }

        public string Zakodowanie(double pm, int ZDMin, int ZDMax, int LBnP)
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

        public double Dekodowanie(string cb, int ZDMin, int ZDMax, int LBnP)
        {
            int ZD = ZDMax - ZDMin;
            int ctmp = 0;

            for (int b = 0; b < LBnP; b++)
            {
                ctmp += (cb[b] - '0') * (int)Math.Pow(2, LBnP - 1 - b);
            }

            return ZDMin + (ctmp / (Math.Pow(2, LBnP) - 1)) * ZD;
        }

        public double[] DekodowanieChromosomu(string chromosom)
        {
            double[] wyniki = new double[liczbaParametrow];
            for (int i = 0; i < liczbaParametrow; i++)
            {
                string fragment = chromosom.Substring(i * LBnP, LBnP);
                wyniki[i] = Dekodowanie(fragment, ZDMin, ZDMax, LBnP);
            }
            return wyniki;
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
        
        public (string, string) OperatorKrzyzowania(string rodzic_1, string rodzic_2, int LBnOs)
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

        public string Mutacja(string chromosom, int LBnOs)
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