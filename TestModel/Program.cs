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

            Console.WriteLine(rep.RepublicanHours);
            Console.WriteLine(rep.RepublicanMinutes);
            Console.WriteLine(rep.RepublicanSeconds);

            Console.WriteLine(rep.RepublicanYear);
            Console.WriteLine(rep.RepublicanMonth);
            Console.WriteLine(rep.RepublicanDay);


            d = new DateTime(1990, 3, 14, 13, 49, 00);
            Console.WriteLine(d.ToShortDateString());
            rep = new RepublicanDatetime(d);

            Console.WriteLine(rep.RepublicanHours);
            Console.WriteLine(rep.RepublicanMinutes);
            Console.WriteLine(rep.RepublicanSeconds);

            Console.WriteLine(rep.RepublicanYear);
            Console.WriteLine(rep.RepublicanMonth);
            Console.WriteLine(rep.RepublicanDay);


            Console.ReadLine();
        }
    }
}
