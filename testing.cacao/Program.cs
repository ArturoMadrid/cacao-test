using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testing.cacao
{
    class Program
    {
        static int _base = 1;

        static void Main(string[] args)
        {

            for (int i = 0; i < 30; i++)
            {
                var aux = i;

                var next = NearestMultple(Convert.ToInt32(aux));
                Console.WriteLine($"El multiplo cercano de {aux} es; {next}");
            }

            Console.WriteLine("Presione enter");
            Console.ReadLine();
            Console.Clear();
        }

        static int NearestMultple(int number)
        {
            int res = number + Math.Abs((number % _base) - _base);

            return res;
        }
    }
}
