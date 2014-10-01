using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxSubArr
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var array = new List<int>() {
                13 ,-3 ,-25 ,20 ,-3 ,-16 ,-23 ,18 ,20 ,7 ,12 ,-5 ,-22 ,15 ,-4 ,7
            };
            Console.WriteLine(Recursive(array));
            Console.ReadLine();


        }

        static public Tuple<int, int, int> Stupid(List<int> A)
        {
            var maxLeft = 0;
            var maxRight = 0;
            var maxSum = A[0];
            for (int i = 0; i < A.Count; i++)
            {
                for (int j = i; j < A.Count; j++)
                {
                    var sum = 0;
                    for (int k = i; k <= j; k++)
                    {
                        sum += A[k];
                    }
                    if (sum > maxSum)
                    {
                        maxSum = sum;
                        maxLeft = i;
                        maxRight = j;
                    }
                }
            }
            return Tuple.Create(maxLeft, maxRight, maxSum);
        }

        static public Tuple<int, int, int> Naive(List<int> A)
        {
            var maxLeft = 0;
            var maxRight = 0;
            var maxSum = A[0];
            for (int i = 0; i < A.Count; i++)
            {
                var bestRight = i;
                var sum = A[i];
                var bestSum = A[i];
                for (int j = i + 1; j < A.Count; j++)
                {
                    sum += A[j];
                    if (sum > bestSum)
                    {
                        bestSum = sum;
                        bestRight = j;
                    }
                }

                if (bestSum > maxSum)
                {
                    maxSum = bestSum;
                    maxLeft = i;
                    maxRight = bestRight;
                }
            }
            return Tuple.Create(maxLeft, maxRight, maxSum);
        }

        static public Tuple<int, int, int> Recursive(List<int> A)
        {
            return RecursiveHelp(A, 0, A.Count - 1);
        }

        static private Tuple<int, int, int> RecursiveHelp(List<int> A, int low, int high)
        {
            if (low == high) {
                return Tuple.Create(low, high, A[low]);
            }

            int mid = (high + low) / 2;
            var leftSum = RecursiveHelp(A, low, mid);
            var rightSum = RecursiveHelp(A, mid + 1, high);
            var crossSum = Cross(A, low, mid, high);
            if (leftSum.Item3 > rightSum.Item3 && leftSum.Item3 > crossSum.Item3)
            {
                return leftSum;
            } else if (rightSum.Item3 > crossSum.Item3)
            {
                return rightSum;
            }
            else
            {
                return crossSum;
            }
        }

        static private Tuple<int, int, int> Cross(List<int> A, int low, int mid, int high)
        {
            var leftSum = A[mid];
            var maxLeft = mid;
            var sum = 0;
            for (int i = mid; i >= 0; i--)
            {
                sum += A[i];
                if (sum > leftSum)
                {
                    leftSum = sum;
                    maxLeft = i;
                }
            }

            var rightSum = A[mid + 1];
            var maxRight = mid + 1;
            sum = 0;
            for (int i = mid + 1; i <= high; i++)
            {
                sum += A[i];
                if (sum > rightSum)
                {
                    rightSum = sum;
                    maxRight = i;
                }
            }

            return Tuple.Create(maxLeft, maxRight, rightSum);
        }
    }
}
