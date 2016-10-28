using BoxViewClock;
using System;

namespace TestModel
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime d = new DateTime(1984, 1, 5, 13, 49, 00);
            Console.WriteLine(d.ToShortDateString());
            var rep = new RepublicanDatetime(d);
            Console.WriteLine(rep.ToString());

            d = new DateTime(1984, 4, 10, 13, 49, 00);
            Console.WriteLine(d.ToShortDateString());
            rep = new RepublicanDatetime(d);
            Console.WriteLine(rep.ToString());

            rep = new RepublicanDatetime(225, 2, 2);
            Console.WriteLine(rep.ToString());
            Console.WriteLine(rep.ToString("ddd-MMMM-yyy"));

            Console.ReadLine();
        }
    }
}
