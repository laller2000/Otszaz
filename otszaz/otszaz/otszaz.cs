using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace otszaz
{
    class otszaz
    {
        static string[] termekek;
        static List<Vasarlas> vasarlas = new List<Vasarlas>();
        static void Main(string[] args)
        {
            Beolvas("penztar.txt");
            Console.WriteLine("adatok beolvasva!");
            Console.WriteLine("\n2.feladat:");
            Console.WriteLine($"\tA fizetések száma:{termekek.Count(a => a.Equals("F"))}");
            Console.WriteLine("\n3.feladat:");
            int db = 0;
            foreach (string item in termekek)
            {
                if (item.Equals("F"))
                {
                    break;
                }
                db++;
            }
            Console.WriteLine($"\tAz első vásárló {db} darab árucikket vásárolt");
            Console.WriteLine("\n4.feladat:");
            int ssz = 0;
            do
            {
                Console.Write("\tAdja meg egy vásárlás sorszámát!");
            } while (!int.TryParse(Console.ReadLine(),out ssz) || ssz< 1 || ssz> termekek.Count(a => a.Equals("F")));
            string cikk = string.Empty;
            do
            {
                Console.Write("\tAdja meg egy árucikk nevét!");
                cikk = Console.ReadLine();
            } while (string.IsNullOrEmpty(cikk) || !termekek.Contains(cikk));
            int darab = 0;
            do
            {
                Console.Write($"\tAdja meg a vásárolt darabszámot!");

            } while (!int.TryParse(Console.ReadLine(),out darab) || darab<1);
            int elso = -1;
            int utolso = -1;
            int hanyszor = 0;
            int kosar = 1;
            int kosarban = -1; //melyik kosarat találtunk utoljára figyeljük
            foreach (string item in termekek)
            {
                if (item.Equals(cikk))
                {
                    if (kosarban!=kosar)
                    {
                        hanyszor++;
                        kosarban = kosar;
                    }
                    if (elso<1)
                    {
                        elso = kosar;
                    }
                    if (kosar>utolso)
                    {
                        utolso = kosar;
                    }
                    
                }
                if (item.Equals("F"))
                {
                    kosar++;
                }
            }
            Console.WriteLine("\n5.feladat:");
            Console.WriteLine($"\tAz első vásárlás sorszáma:{elso}");
            Console.WriteLine($"\tAz utolsó vásárlás sorszáma:{utolso}");
            Console.WriteLine($"\t{hanyszor} vásárlás során vettek belőle.");
            Console.WriteLine("\n6.feladat:");
            Console.WriteLine($"\t {darab} darab vételekor fizetendő: {ertek(darab)}");
            Console.WriteLine("\n7.feladat:");
            kosar = 1;
            foreach (string item in termekek)
            {
                if (kosar == ssz)
                {
                    bool van_mar_ilyen = false;
                    foreach (Vasarlas termek in vasarlas)
                    {
                        if (termek.Cikknev.Equals(item))
                        {
                            termek.Db++;
                            van_mar_ilyen = true;
                        }
                    }
                    if (!van_mar_ilyen && !item.Equals("F"))
                    {
                        vasarlas.Add(new Vasarlas(item));
                    }
                }
                if (item.Equals("F"))
                {
                    kosar++;
                }

            }
            foreach (Vasarlas item in vasarlas)
            {
                Console.WriteLine($"\t{item.Db} {item.Cikknev}");
            }
            Console.WriteLine("\n8.feladat:");
            using (StreamWriter sw=new StreamWriter("osszeg.txt"))
            {
                 kosar = 0;
                vasarlas = new List<Vasarlas>();
                foreach (string item in termekek)
                {
                    bool van_mar_ilyen = false;
                    foreach (Vasarlas termek in vasarlas)
                    {
                        if (termek.Cikknev.Equals(item))
                        {
                            termek.Db++;
                            van_mar_ilyen = true;
                        }
                    }
                    if (!van_mar_ilyen && !item.Equals("F"))
                    {
                        vasarlas.Add(new Vasarlas(item));
                    }
                    if (item.Equals("F"))
                    {
                        kosar++;
                        int osszeg = 0;
                        foreach (Vasarlas tt in vasarlas)
                        {
                            osszeg += ertek(tt.Db);
                        }
                        sw.WriteLine($"{kosar} {osszeg}");
                        vasarlas = new List<Vasarlas>();
                    }
                }
            }
            Console.WriteLine("\nProgram vége");
            Console.ReadLine();
        }
        static void Beolvas(string fajl)
        {
            termekek = File.ReadAllLines(fajl);
        }
        static int ertek(int db)
        {
            int v = 0;
            if (db ==1)
            {
                v = 500;
            }
            if (db==2)
            {
                v = 500 + 450;
            }
            if (db==3)
            {
                v = 500 + 450 + 400;
            }
            if (db>3)
            {
                v = 500 + 450+400*(db-2);
            }
            return v;
        }
    }
}
