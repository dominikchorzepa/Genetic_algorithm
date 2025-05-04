using System.Diagnostics;

namespace Program;

class Program
{
    static void Main(string[] args)
    {
        int ZDMin = -10;
        int ZDMax = 10;
        int ZD = ZDMax - ZDMin;
        int LBnP = 3;
        int TurRozm = 3;
        int liczbaParametrow = 9;
        int liczbaOsobnikow = 13;
        int LBnOs = LBnP * liczbaParametrow;
        int liczbaIteracji = 1000;

        List<(string chromosom, double przystosowanie)> populacja = new List<(string, double)>();

        Random rnd = new Random();

        for (int i = 0; i < liczbaOsobnikow; i++)
        {
            double w1 = rnd.NextDouble() * ZD + ZDMin;
            double w2 = rnd.NextDouble() * ZD + ZDMin;
            double w3 = rnd.NextDouble() * ZD + ZDMin;
            double w4 = rnd.NextDouble() * ZD + ZDMin;
            double w5 = rnd.NextDouble() * ZD + ZDMin;
            double w6 = rnd.NextDouble() * ZD + ZDMin;
            double w7 = rnd.NextDouble() * ZD + ZDMin;
            double w8 = rnd.NextDouble() * ZD + ZDMin;
            double w9 = rnd.NextDouble() * ZD + ZDMin;

            string w1_bin = Zakodowanie(w1, ZDMin, ZDMax, LBnP);
            string w2_bin = Zakodowanie(w2, ZDMin, ZDMax, LBnP);
            string w3_bin = Zakodowanie(w3, ZDMin, ZDMax, LBnP);
            string w4_bin = Zakodowanie(w4, ZDMin, ZDMax, LBnP);
            string w5_bin = Zakodowanie(w5, ZDMin, ZDMax, LBnP);
            string w6_bin = Zakodowanie(w6, ZDMin, ZDMax, LBnP);
            string w7_bin = Zakodowanie(w7, ZDMin, ZDMax, LBnP);
            string w8_bin = Zakodowanie(w8, ZDMin, ZDMax, LBnP);
            string w9_bin = Zakodowanie(w9, ZDMin, ZDMax, LBnP);

            string chromosom = w1_bin + w2_bin + w3_bin + w4_bin + w5_bin + w6_bin + w7_bin + w8_bin + w9_bin;

            double[] wagi = { w1, w2, w3, w4, w5, w6, w7, w8, w9 };
            double przystosowanie = FunkcjaPrzystosowania(wagi);

            populacja.Add((chromosom, przystosowanie));
        }

        Console.WriteLine("--- Populacja początkowa ---");

        foreach (var k in populacja)
        {
            Console.WriteLine("Wartość chromosomu: " + k.chromosom + " Wartość funkcji: " + k.przystosowanie, 2);
        }

        double minimalneP = double.MaxValue;
        double sumaP = 0;

        foreach (var k in populacja)
        {
            if (k.przystosowanie < minimalneP)
            {
                minimalneP = k.przystosowanie;
            }
            sumaP += k.przystosowanie;
        }
        Console.WriteLine("Najlepsza funkcja osobnika: " + minimalneP);
        Console.WriteLine("Średnia wartość przystosowania: " + sumaP / populacja.Count + "\n");

        for (int i = 0; i < liczbaIteracji; i++)
        {
            Console.WriteLine("--- Populacja " + (i + 1) + " ---");

            List<(string chromosom, double przystosowanie)> nowaPopulacja = new List<(string, double)>();

            for (int j = 0; j < liczbaOsobnikow - 1; j++)
            {
                string zwyciezca = SelekcjaTurniejowa(populacja, TurRozm);
                nowaPopulacja.Add((zwyciezca, 0));
            }

            List<(int a, int b)> krzyzowanieIndexy = new List<(int, int)> { (0, 1), (2, 3), (8, 9), (10, 11) };

            foreach (var (a, b) in krzyzowanieIndexy)
            {
                var (potomek1, potomek2) = OperatorKrzyzowania(nowaPopulacja[a].chromosom, nowaPopulacja[b].chromosom, LBnOs);
                nowaPopulacja[a] = (potomek1, 0);
                nowaPopulacja[b] = (potomek2, 0);
            }

            for (int j = 4; j < nowaPopulacja.Count; j++)
            {
                string zmutowany = Mutacja(nowaPopulacja[j].chromosom, LBnOs);
                nowaPopulacja[j] = (zmutowany, 0);
            }

            string najlepszyOsobnik = SelekcjaHotDeck(populacja);
            nowaPopulacja.Add((najlepszyOsobnik, 0));

            for (int j = 0; j < nowaPopulacja.Count; j++)
            {
                string chromosom = nowaPopulacja[j].chromosom;

                double[] wagi = DekodowanieNaTablice(chromosom, ZDMin, ZDMax, LBnP);

                double przystosowanie = FunkcjaPrzystosowania(wagi);

                nowaPopulacja[j] = (chromosom, przystosowanie);
            }

            double minPrzystosowanie = double.MaxValue;
            double sumaPrzystosowania = 0;

            foreach (var k in nowaPopulacja)
            {
                if (k.przystosowanie < minPrzystosowanie)
                {
                    minPrzystosowanie = k.przystosowanie;
                }
                sumaPrzystosowania += k.przystosowanie;
                Console.WriteLine("Wartość chromosomu: " + k.chromosom + " Wartość funkcji: " + k.przystosowanie, 2);
            }
            Console.WriteLine("Najlepsza funkcja osobnika: " + minPrzystosowanie, 2);
            Console.WriteLine("Średnia wartość przystosowania: " + sumaPrzystosowania / populacja.Count + "\n");

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

    static double[] DekodowanieNaTablice(string chromosom, int ZDMin, int ZDMax, int LBnP)
    {
        int liczbaWag = chromosom.Length / LBnP;
        double[] wagi = new double[liczbaWag];

        for (int i = 0; i < liczbaWag; i++)
        {
            string fragment = chromosom.Substring(i * LBnP, LBnP);
            wagi[i] = Dekodowanie(fragment, ZDMin, ZDMax, LBnP);
        }
        return wagi;
    }

    static double FunkcjaPrzystosowania(double[] wagi)
    {
        var dane = new List<(double[], double)>
        {
            (new double[] {1, 0, 0 }, 0),
            (new double[] {1, 0, 1 }, 1),
            (new double[] {1, 1, 0 }, 1),
            (new double[] {1, 1, 1 }, 0)
        };

        double suma = 0;

        foreach (var (wejscia, wyjscia) in dane)
        {
            double[] wyjsciaUkryte =
            {
                Sigmoid(wagi[0] * wejscia[0] + wagi[1] * wejscia[1] + wagi[2] * wejscia[2]),
                Sigmoid(wagi[3] * wejscia[0] + wagi[4] * wejscia[1] + wagi[5] * wejscia[2])
            };
            double wyjscieKoncowe = Sigmoid(
                wagi[6] * wyjsciaUkryte[0] + 
                wagi[7] * wyjsciaUkryte[1] + 
                wagi[8] * 1
                );
            suma += Math.Pow(wyjscia - wyjscieKoncowe, 2);
        }

        return suma;
    }

    static double Sigmoid(double x)
    {
        return 1 / (1 + Math.Exp(-x));
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
            if (turniej[i].przystosowanie < najlepszaOcena)
            {
                najlepszyOsobnik = turniej[i].chromosom;
                najlepszaOcena = turniej[i].przystosowanie;
            }
        }

        return najlepszyOsobnik;
    }

    static string SelekcjaHotDeck(List<(string chromosom, double przystosowanie)> populacja)
    {
        double najlepszaOcena = double.MaxValue;
        string najlepszyOsobnik = "";

        foreach (var k in populacja)
        {
            if (k.przystosowanie < najlepszaOcena)
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