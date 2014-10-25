using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heapsort
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 71, 65, 24, 96, 84, 53, 81, 47, 87, 33, 13, 92, 83, 77, 91, 34, 54, 49, 30, 76, 15, 60, 78, 51, 45, 85, 40, 62, 67, 72, 63, 92, 27, 44, 18, 46, 95, 56, 31, 74, 32, 66 };
            Console.WriteLine(string.Join(",", arr));
            Heapsort(arr);
            Console.WriteLine(string.Join(",", arr));
            Console.ReadLine();
        }

        static void Heapsort(int[] A)
        {
            var heap = new Heap(A);
            heap.BuildMaxHeap();
            for (int i = heap.Length - 1; i >= 1; i--)
            {
                var temp = heap[i];
                heap[i] = heap[0];
                heap[0] = temp;

                heap.HeapSize--;
                heap.MaxHeapify(0);
            }
        }
    }

    class Heap
    {
        private int[] data;
        
        public Heap(int[] init)
        {
            HeapSize = init.Length - 1;
            data = init;
        }

        public int Length { get { return data.Length;} }

        public int HeapSize { get; set; }


        public int this[int key]
        {
            get
            {
                return data[key];
            }
            set
            {
               data[key] = value;
            }
        }

        public void MaxHeapify(int i)
        {
            var l = Left(i);
            var r = Right(i);
            var largest = i;
            if (l <= HeapSize && data[l] > data[i])
            {
                largest = l;
            }
            if (r <= HeapSize && data[r] > data[largest])
            {
                largest = r;
            }

            if (largest != i)
            {
                var temp = data[i];
                data[i] = data[largest];
                data[largest] = temp;
                MaxHeapify(largest);
            }
        }

        public void BuildMaxHeap()
        {
            for (int i = (HeapSize - 1) / 2; i >= 0; i--)
            {
                MaxHeapify(i);
            }
        }

        private int Parrent(int i)
        {
            return (i - 1) / 2;
        }

        private int Right(int i)
        {
            return 2 * i + 2;
        }

        private int Left(int i)
        {
            return 2 * i + 1;
        }

    }
}
