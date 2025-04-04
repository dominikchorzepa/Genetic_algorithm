namespace Program;

class Parametr
{
    public int Wartosc {get; set;}
    public double Funkcja {get; set;}

    public Parametr(int wartosc, double funkcja){
        Wartosc = wartosc;
        Funkcja = funkcja;
    }

    public void Wyswietl(){
        Console.WriteLine("Parametr: " + Wartosc + " " + "Funkcja: " + Funkcja);
    }
}

class Program
{
    static void Main(string[] args)
    {
        int ZDMin = -1;
        int ZDMax = 2;
        int LBnP = 2;
        int ZD = ZDMax - ZDMin;
        int[] cb = new int[LBnP];

        Dictionary<string, double> kodowanie = new Dictionary<string, double>();

        for (int i = 0; i < Math.Pow(2, LBnP); i++)
        {
            double pm = ZDMin + (ZD / (Math.Pow(2, LBnP) - 1.0)) * i;

            pm = Math.Max(pm, ZDMin);
            pm = Math.Min(pm, ZDMax);

            string chromosom = Zakodowanie(pm, ZDMin, ZDMax, LBnP);
            kodowanie.Add(chromosom, pm);
        }

        Console.WriteLine("--- Kodowanie ---");

        foreach (var k in kodowanie)
        {
            Console.WriteLine("Wartość chromosomu: " + k.Key + " Wartość parametru: " + Math.Round(k.Value, 2));
        }

        Console.WriteLine("\n--- Dekodowanie ---");

        foreach (var k in kodowanie)
        {
            double pmDekodowany = Dekodowanie(k.Key, ZDMin, ZDMax, LBnP);
            Console.WriteLine("Wartość chromosomu: " + k.Key + " Wartość parametru: " + Math.Round(k.Value, 2));
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
                ctmp += cb[b] * (int)Math.Pow(2, b);
            }

            return ZDMin + (ctmp / (Math.Pow(2, LBnP) - 1)) * ZD;
        }


        //pm = Math.Max(pm, ZDMin);
        //pm = Math.Min(pm, ZDMax);

        //double ctmp = Math.Round(((pm - ZDMin) / ZD) * (Math.Pow(2, LBnP) - 1));

        //for (int b = 0; b < LBnP; b++)
        //{
        //    cb[b] = (int)Math.Floor(ctmp / Math.Pow(2, b)) % 2;
        //}

        //Array.Reverse(cb);

        //foreach (int b in cb)
        //{
        //    Console.Write(b);
        //}
        //Console.WriteLine(pm);
        //double ctmp2 = 0;
        //for (int b = 0; b < LBnP; b++)
        //{
        //    ctmp2 += cb[b] * Math.Pow(2, b);
        //}

        //double pm2 = ZDMin + (ctmp / Math.Pow(2, LBnP) - 1) * ZD;
        //Console.WriteLine(pm2);

        
        

        
    }
}
