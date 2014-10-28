using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountSort
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine(chooseNDiget(1234, 2));
            var test = new List<int>{ 4,7,8,4,7,1,6,2,9,6,3,6,5,4,4,3,1,7,8,9,2 };
            foreach (var item in test)
            {
                Console.Write(item + " ");
            }

            var sorted = CountSort(test, x => x);
            Console.WriteLine();
            Console.WriteLine();

            foreach (var item in sorted)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
            Console.WriteLine();

            var big = new List<int> { 714545, 523607, 948186, 704382, 198691, 208747, 511010, 675355, 524446, 769473, 196244, 40224, 969673, 427211, 502027, 117098, 259961, 127679, 422139, 687129 };
            foreach (var item in big)
            {
                Console.Write(item + " ");
            }
            var sot = RadixSort(big, 6);
            Console.WriteLine();
            Console.WriteLine();

            foreach (var item in sot)
            {
                Console.Write(item + " ");
            }

            Console.ReadLine();
        }
        private static int chooseNDiget(int number, int n)
        {
            n--;
            number = number / (int)Math.Pow(10, n);
            return number % 10;
        }

        static IEnumerable<int> RadixSort(IEnumerable<int> A, int d)
        {
            for (int i = 1; i <= 6; i++)
			{
			    A= CountSort(A, x => chooseNDiget(x, i));
			}

            return A;
        }

        static IEnumerable<T> CountSort<T>(IEnumerable<T> A, Func<T, int> fn)
        {
            var max = A.Max(fn);
            var C = new int[max + 1];

            foreach (var item in A)
            {
                C[fn(item)]++;
            }

            for (int i = 1; i < C.Length; i++)
            {
                C[i] += C[i - 1];
            }

            var B = new T[A.Count()];

            foreach (var item in A.Reverse())
	        {
                B[C[fn(item)] - 1] = item;
                C[fn(item)]--;
	        }
            
           
            return B;
        }   
    }
}
