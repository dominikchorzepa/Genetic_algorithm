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
    }
}