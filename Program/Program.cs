namespace Program;

class Program
{
    static void Main(string[] args)
    {
        int ZDMin = -5;
        int ZDMax = 5;
		int ZD = ZDMax - ZDMin;
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

        string rodzic1 = "101011";
        string rodzic2 = "001100";

        Console.WriteLine(rodzic1[2]);

        Console.WriteLine("\n");

        Console.WriteLine(OperatorKrzyzowania(rodzic1, rodzic2, LBnOs));

        Console.WriteLine("\n");

        string testMutacji = "10100101";
        Console.WriteLine(Mutacja(testMutacji, LBnOs));
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

        Console.WriteLine("Rodzic1: " + rodzic_1);
        Console.WriteLine("Rodzic2: " + rodzic_2);

        Console.WriteLine("Punkt cięcia[index]: " + b_ciecie);

        for (int b = b_ciecie + 1; b < LBnOs; b++)
        {
            potomek_1[b] = rodzic_2[b];
            potomek_2[b] = rodzic_1[b];
        }

        string potomek1 = new string(potomek_1);
        string potomek2 = new string(potomek_2);

        Console.WriteLine("Potomek1: " + potomek1);
        Console.WriteLine("Potomek2: " + potomek2);

        return (potomek1, potomek2);
    }

    static string Mutacja(string chromosom, int LBnOs)
    {
        Random rnd = new Random();
        int b_punkt = rnd.Next(0, LBnOs);

        char[] chromosomTablica = chromosom.ToCharArray();

        Console.WriteLine("Chromosom do mutacji: " + chromosom);
        Console.WriteLine("Punkt mutacji[index]: " + b_punkt);


        if (chromosomTablica[b_punkt] == '1')
        {
            chromosomTablica[b_punkt] = '0';
        } else
        {
            chromosomTablica[b_punkt] = '1';
        }

        string chromosomWyjscie = new string(chromosomTablica);
        Console.WriteLine("Chromosom po mutacji: " + chromosomWyjscie);

        return chromosomWyjscie;
    }
}