namespace Program;

class Program
{
    static void Main(string[] args)
    {
        int ZDMin = 0;
        int ZDMax = 3;
        int ZD = ZDMax - ZDMin;
        int LBnP = 4;
        int TurRozm = 3;
        int liczbaParametrow = 3;
        int liczbaOsobnikow = 13;
        int LBnOs = LBnP * liczbaParametrow;
        int liczbaIteracji = 100;

        List<double> x = new List<double> { -1.00000, -0.80000, -0.60000, -0.40000, -0.20000, 0.00000, 0.20000,
        0.40000, 0.60000, 0.80000, 1.0000, 1.20000, 1.40000, 1.60000, 1.80000, 2.0000, 2.20000, 2.40000, 2.60000,
        2.80000, 3.00000, 3.20000, 3.40000, 3.60000, 3.80000, 4.00000, 4.20000, 4.40000, 4.60000, 4.80000, 5.00000,
        5.20000, 5.40000, 5.60000, 5.80000, 6.00000};

        List<double> y = new List<double> { 0.59554, 0.58813, 0.64181, 0.68587, 0.44783, 0.40836, 0.38241, -0.05933,
        -0.12478, -0.36847, -0.39935, -0.50881, -0.63435, -0.59979, -0.64107, -0.51808, -0.38127, -0.12349, -0.09624,
        0.27893, 0.48965, 0.33089, 0.70615, 0.53342, 0.43321, 0.64790, 0.48834, 0.18440, -0.02389, -0.10261, -0.33594,
        -0.35101, -0.62027, -0.55719, -0.66377, -0.62740};

        List<(string chromosom, double przystosowanie)> populacja = new List<(string, double)>();

        Random rnd = new Random();

        for (int i = 0; i < liczbaOsobnikow; i++)
        {
            double pa_pm = rnd.NextDouble() * ZD + ZDMin;
            double pb_pm = rnd.NextDouble() * ZD + ZDMin;
            double pc_pm = rnd.NextDouble() * ZD + ZDMin;

            string pa_bin = Zakodowanie(pa_pm, ZDMin, ZDMax, LBnP);
            string pb_bin = Zakodowanie(pb_pm, ZDMin, ZDMax, LBnP);
            string pc_bin = Zakodowanie(pc_pm, ZDMin, ZDMax, LBnP);

            string chromosom = pa_bin + pb_bin + pc_bin;

            double przystosowanie = FunkcjaPrzystosowania(x, y, pa_pm, pb_pm, pc_pm);

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
                nowaPopulacja[a] = (potomek2, 0);
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

                string pa_bin = chromosom.Substring(0, LBnP);
                string pb_bin = chromosom.Substring(LBnP, LBnP);
                string pc_bin = chromosom.Substring(2 * LBnP, LBnP);

                double pa_pm = Dekodowanie(pa_bin, ZDMin, ZDMax, LBnP);
                double pb_pm = Dekodowanie(pb_bin, ZDMin, ZDMax, LBnP);
                double pc_pm = Dekodowanie(pc_bin, ZDMin, ZDMax, LBnP);

                double przystosowanie = FunkcjaPrzystosowania(x, y, pa_pm, pb_pm, pc_pm);

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

    static double FunkcjaPrzystosowania(List<double> x, List<double> y, double pa, double pb, double pc)
    {
        double suma = 0;
        for (int i = 0; i < x.Count; i++)
        {
            double f_x = pa * Math.Sin(pb * x[i] + pc);
            suma += Math.Pow(y[i] - f_x, 2);
        }
        return suma;
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