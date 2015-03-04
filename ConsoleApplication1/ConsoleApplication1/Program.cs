using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            double a = 0.27;

            Console.WriteLine(Convert.ToDouble(a));
            Console.WriteLine(Convert.ToDouble(a)>0?"大于":"0");
            Console.ReadKey();
        }
    }
}
