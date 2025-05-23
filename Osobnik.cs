using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    internal class Osobnik
    {
        public string Chromosom;
        public double Przystosowanie;

        public Osobnik(string chromosom, double przystosowanie)
        {
            Chromosom = chromosom;
            Przystosowanie = przystosowanie;
        }
    }
}
