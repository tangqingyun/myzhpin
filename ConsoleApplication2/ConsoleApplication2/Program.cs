using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(GetInt());
            Console.ReadKey();
        }

        static int GetInt()
        {
            int i = 8;
            try
            {

                i++;
                Console.WriteLine("a");
                return i;//把返回值设定为i，然后“尽快”返回（没啥事就回去吧）
            }

            finally
            {
                Console.WriteLine("b");
                i++;
            }
        }


    }
}
