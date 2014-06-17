using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NGnono.Doubts.AnonymousFunc
{
    class Program
    {
        private static Func<int, int, int> _add = (a, b) => a + b;


        static void Main(string[] args)
        {
            var a = _add(1, 2);
            var b = Add(1, 2);

            Console.WriteLine(a);
            Console.WriteLine(b);
        }


        private static int Add(int x, int y)
        {
            return x + y;
        }
    }
}
