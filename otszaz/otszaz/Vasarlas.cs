using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace otszaz
{
    class Vasarlas
    {
        int db;
        string cikknev;

        public Vasarlas(string cikknev)
        {
            this.Cikknev = cikknev;
            this.Db = 1;
        }

        public int Db { get => db; set => db = value; }
        public string Cikknev { get => cikknev; set => cikknev = value; }
    }
}
