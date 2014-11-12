using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LinqStatistics;

class Test
{
    static List<String> names = new List<String>();
    public static void Main()
    {
        loadFile("drengenavne.csv");
        loadFile("pigenavne.csv");
        loadFile("unisexnavne.csv");

        Console.WriteLine("testing naive:");
        testAndPrint(x => 1);

        Console.WriteLine("testing sumHash:");
        testAndPrint(sumHash);

        Console.WriteLine("testing rotHash:");
        testAndPrint(rotHash);

        Console.WriteLine("testing monoLikeHash:");
        testAndPrint(monoLikeHash);

        Console.WriteLine("testing simpleMonoLikeHash:");
        testAndPrint(simpleMonoLikeHash);

        Console.WriteLine("testing default:");
        testAndPrint(x => (uint)x.GetHashCode());


        Console.WriteLine("DONE!!");
        Console.ReadLine();
    }

    static public uint sumHash(string s)
    {
        uint sum = 0;
        foreach (var item in s)
        {
            sum += item;
        }

        return sum;
    }

    public static uint RotateLeft(uint value, int count)
    {
        return (value << count) | (value >> (32 - count));
    }

    static public uint rotHash(string s){
        uint sum = 0;
        for(int i = 0; i < s.Length; i++) {
            sum += RotateLeft(s[i], i);
        }
        return sum;
    }

    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System/String.cs#L2696
    static public uint monoLikeHash(string s)
    {        
        int cc = 0;
        int end = s.Length - 1;
        int h = 0;
        for (; cc < end; cc += 2)
        {
            h = (h << 5) - h + s[cc];
            h = (h << 5) - h + s[cc+1];
        }
        ++end;
        if (cc < end)
            h = (h << 5) - h + s[cc];
        return (uint)h;        
    }

    static public uint simpleMonoLikeHash(string s)
    {
        uint h = 0;
        foreach (char c in s)
        {
            h = (h << 5) - h + c;
        }
        return h;
    }

    static void testAndPrint(Func<String, uint> fn)
    {
        var buk = new int[100];
        foreach (var item in names)
        {
            var index = fn(item) % 100;
            buk[(int)index] += 1;
        }
        Console.WriteLine(buk.StandardDeviation());
        Console.WriteLine(string.Join(",", buk) + "\n");

    }

    static void loadFile(String name)
    {
        try
        {
            using (StreamReader sr = new StreamReader(name))
            {
                while (!sr.EndOfStream)
                {
                    names.Add(sr.ReadLine());
                }

            }
        }
        catch (Exception e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
    }
}