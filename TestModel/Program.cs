using BoxViewClock;
using System;

namespace TestModel
{
    class Program
    {
        static void Main(string[] args)
        {
            String outString = string.Empty;
            DateTime d = new DateTime(1984, 1, 5, 13, 49, 00);
            Console.WriteLine(d.ToShortDateString());
            var rep = new RepublicanDatetime(d);
            outString = rep.ToString();
            Console.WriteLine(outString);

            rep = new RepublicanDatetime(225, 2, 2, 6, 66, 77);
            outString = rep.ToString();
            Console.WriteLine(outString);
            outString = rep.ToString("ddd-MMMM-yyy hh:mm:ss");
            Console.WriteLine(outString);

            d = new DateTime(1984, 4, 10, 13, 49, 00);
            Console.WriteLine(d.ToShortDateString());
            rep = new RepublicanDatetime(d);
            outString = rep.ToString();
            Console.WriteLine(outString);
            outString = rep.ToString("yy-M-d");
            Console.WriteLine(outString);
            outString = rep.ToString("dd-MMMM-yyy");
            Console.WriteLine(outString);
            outString = rep.ToString("ddd-MM-yyyy hh:mm:ss");
            Console.WriteLine(outString);
            outString = rep.ToString("\\ddd pippo MM bo\\h yyyy hh?mm?ss");
            Console.WriteLine(outString);


            Console.ReadLine();
        }
    }
}
