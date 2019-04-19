using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
namespace QTests
{
    class Program0
    {
        static void Main0()
        {
            var intList0 = new List<int>() { 1, 2 };
            var stringList = new List<string>() { "小洋粉", "大洋粉" }; // 编译器会将初始值设定项 转换为 每一项的Add()方法;
            var intList1 = new List<int>(10) { 3, 5, 7, 4 };
            var racers0 = new List<Racer>();

            var graham = new Racer(7, "Graham", "Hill", "UK", 14);
            var emerson = new Racer(13, "Emerson", "Fittipaldi", "Brazil", 14);
            var mario = new Racer(16, "Mario", "Andretti", "USA", 12);

            var racers1 = new List<Racer>(20) { graham, emerson, mario };

            racers1.Add(new Racer(24, "Michael", "Schumacher", "Germany", 91));
            racers1.Add(new Racer(24, "Mika", "Hakkinen", "Finland", 20));

            racers1.AddRange(new Racer[]
            {
                new Racer(14, "Niki", "Lauda", "Austria", 25),
                new Racer(21, "Alain", "Prost", "France", 51)
            });

            racers1.Insert(3, new Racer(6, "Phil", "Hill", "USA", 3));

            ////////////////////////////////////
            ///

            foreach (var e in racers1)
            {
                WriteLine(e.ToString("a"));
            }
            racers1.Sort(new RacerComparer(CompareType.Country));
        
            WriteLine(":::::::::");
            foreach (var e in racers1)
            {
                WriteLine(e.ToString("a"));
            }
            WriteLine(":::::::::");

            racers1.Sort();

            Racer racer = racers1.Find(r => r.Id == 21);
            WriteLine(racer.ToString("a"));

            int index1 = racers1.FindIndex(4,r => r.FirstName == "Mika");

            WriteLine(index1);





            int index0 = racers1.FindIndex(new FindCountry("Fittipaldi").FindCountryPredicate);
            WriteLine(index0);


            Racer r1 = racers1[3];
            WriteLine(r1);
            for(int i = 0; i < racers1.Count; i++)
            {
                WriteLine(racers1[i]);
            }
            WriteLine(":::::::::::::");

            racers1.RemoveAt(3);
            bool a = racers1.Remove(graham);
            WriteLine(a);
            racers1.RemoveRange(0, 5);
            foreach (var r in racers1)
            {
                WriteLine(r.ToString("a"));
            }
           


            stringList.Add("痒痒粉");
            stringList.Add("痒痒粉");
            stringList.Add("痒痒粉");
            stringList.Add("痒痒粉");

            foreach(var e in stringList)
            {
                WriteLine(e);
            }

            Console.WriteLine(intList1.Capacity);
            Console.WriteLine(intList1.Count);
            Console.WriteLine(intList1[1]);
            intList1.TrimExcess();
            Console.WriteLine(intList1.Capacity);
        }
    }
}
