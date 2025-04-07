namespace Program;

class Program
{
    static void Main(string[] args)
    {
        int ZDMin = -5;
        int ZDMax = 5;
        int LBnP = 3;
        int TurRozm = 3;
        int liczbaParametrow = 2;
        int liczbaOsobnikow = 6;
        int LBnOs = LBnP * liczbaParametrow;

        List<(string chromosom, double przystosowanie)> populacja = new List<(string, double)>();

        Random rnd = new Random();

        for (int i = 0; i < liczbaOsobnikow; i++)
        {
            string chromosom = "";
            for (int j = 0; j < LBnOs; j++)
            {
                chromosom += rnd.Next(2);
            }

            double przystosowanie = Dekodowanie(chromosom, ZDMin, ZDMax, LBnP);

            populacja.Add((chromosom, przystosowanie));
        }

        Console.WriteLine("--- Populacja początkowa ---");

        foreach (var k in populacja)
        {
            Console.WriteLine("Wartość chromosomu: " + k.chromosom + " Wartość funkcji: " + Math.Round(k.przystosowanie, 2));
        }

        string x = Zakodowanie(1.0, ZDMin, ZDMax, LBnP);
        Console.WriteLine(x);

        Console.WriteLine(Dekodowanie(x, ZDMin, ZDMax, LBnP));

        Console.WriteLine(populacja[0]);
        Console.WriteLine("\n");

        string y = SelekcjaTurniejowa(populacja, TurRozm);
        Console.WriteLine(y);

        string z = SelekcjaHotDeck(populacja);
        Console.WriteLine(z);
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

    static string SelekcjaTurniejowa(List<(string chromosom, double przystosowanie)> populacja, int TurRozm)
    {
        Random rnd = new Random();

        List<(string chromosom, double przystosowanie)> turniej = new List<(string, double)>();

        for (int i = 0; i < TurRozm; i++)
        {
            int losowanie = rnd.Next(populacja.Count);
            turniej.Add(populacja[losowanie]);
            Console.WriteLine("Dodano do turnieju: " + populacja[losowanie]);
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

        Console.WriteLine("Zwycięzca: " + najlepszyOsobnik + " jego ocena: " + najlepszaOcena);
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

        Console.WriteLine("Wybrany został: " + najlepszyOsobnik + " jego ocena: " + najlepszaOcena);
        return najlepszyOsobnik;
    }
}