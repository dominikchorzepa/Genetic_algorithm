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
        Random rand = new Random();

        double pm = 5;
        int ZDMin = -1;
        int ZDMax = 2;
        int LBnP = 3;
        int ZD = ZDMax - ZDMin;
        int[] cb = new int[3];

    
        pm = Math.Max(pm, ZDMin);
        pm = Math.Min(pm, ZDMax);

        double ctmp = Math.Round(((pm - ZDMin) / ZD) * (Math.Pow(2, LBnP) - 1));

        for (int b = 0; b < LBnP; b++)
        {
            cb[b] = (int)Math.Floor(ctmp / Math.Pow(2, b)) % 2;
        }

        Array.Reverse(cb);

        foreach (int b in cb)
        {
            Console.Write(b);
        }

        

        
    }
}
