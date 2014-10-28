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
            Console.ReadLine();
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
