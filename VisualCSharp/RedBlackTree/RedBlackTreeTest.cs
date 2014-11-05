using Xunit;
using RedBlackTree;
using System;
using System.Collections.Generic;

public class RedBlackTreeTest
{
    ISortedDictionary<int, int> t = null;
    ISortedDictionary<string, int> t2 = null;

    public RedBlackTreeTest()
    {
        t = new RedBlackTree<int, int>();
        t2 = new RedBlackTree<string, int>();
    }

    [Fact]
    public void InsertTest1()
    {
        t.Insert(1, 1);
        Assert.Equal(1, t[1]);
    }

    [Fact]
    public void InsertTest2()
    {
        t.Insert(1, 1);
        t.Insert(2, 2);
        t.Insert(3, 3);
        t.Insert(4, 4);
        Assert.Equal(4, t[4]);
        Assert.Equal(2, t[2]);
    }

    [Fact]
    public void InsertNullTest()
    {
        Assert.Throws<ArgumentNullException>(
           delegate
           {
               t2.Insert(null, 1);
           });
    }

    [Fact]
    public void DeleteTest()
    {
        t.Insert(1, 1);
        t.Insert(2, 2);
        Assert.Equal(2, t[2]);
        t.Delete(2);
        Assert.False(t.Search(2).HasValue);
        Assert.Equal(1, t[1]);
    }

    [Fact(Skip = "reason")]
    public void DeleteNullTest()
    {
        Assert.Throws<ArgumentNullException>(
           delegate
           {
               t2.Delete(null);
           });
    }

    [Fact]
    public void MaximumTest1()
    {
        t.Insert(1, 1);
        t.Insert(2, 2);
        t.Insert(3, 3);
        t.Insert(4, 4);
        Assert.Equal(4, t.Maximum().Key);
    }

    [Fact]
    public void MaximumTest2()
    {
        t.Insert(1, 23);
        t.Insert(2, 42);
        t.Insert(3, 76);
        t.Insert(4, 3);
        Assert.Equal(4, t.Maximum().Key);
    }

    [Fact]
    public void MinimumTest1()
    {
        t.Insert(1, 1);
        t.Insert(2, 2);
        t.Insert(3, 3);
        t.Insert(4, 4);
        Assert.Equal(1, t.Minimum().Key);
    }

    [Fact]
    public void MinimumTest2()
    {
        t.Insert(1, 23);
        t.Insert(2, 42);
        t.Insert(3, 0);
        t.Insert(4, 3);
        Assert.Equal(1, t.Minimum().Key);
    }

    [Fact]
    public void SuccessorTest()
    {
        t.Insert(1, 23);
        t.Insert(2, 42);
        t.Insert(3, 0);
        t.Insert(4, 3);
        Assert.Equal(2, t.Successor(1).Key);
    }

    [Fact]
    public void PredecessorTest()
    {
        t.Insert(1, 23);
        t.Insert(2, 42);
        t.Insert(3, 0);
        t.Insert(4, 3);
        Assert.Equal(1, t.Predecessor(2).Key);
    }

    [Fact(Skip = "reason")]
    public void InvalidOperationsTest()
    {
        Assert.Throws<InvalidOperationException>(
           delegate
           {
               t.Minimum();
           });

        Assert.Throws<InvalidOperationException>(
           delegate
           {
               t.Maximum();
           });
    }

    [Fact(Skip = "reason")]
    public void KeyNotFoundTest()
    {
        t.Insert(2, 42);
        Assert.Throws<KeyNotFoundException>(
           delegate
           {
               t.Predecessor(1);
           });

        Assert.Throws<KeyNotFoundException>(
           delegate
           {
               t.Successor(1);
           });
        Assert.Throws<KeyNotFoundException>(
           delegate
           {
               var x = t[1];
           });
        Assert.Throws<KeyNotFoundException>(
           delegate
           {
               t[1] = 2;
           });
    }

    [Fact(Skip = "reason")]
    public void MegaTest()
    {
        Random rng = new Random();
        for (int i = 0; i < 50; i++)
        {
            t.Insert(rng.Next(25, 75), rng.Next(25, 75));
        }
        t.Insert(1, 1);
        for (int i = 0; i < 50; i++)
        {
            var num = rng.Next(25, 75);
            t.Insert(num, num);
        }
        t.Insert(50, 50);
        for (int i = 0; i < 50; i++)
        {
            t.Insert(rng.Next(25, 75), rng.Next(0, 1000));
        }
        t.Insert(100, 100);
        for (int i = 0; i < 50; i++)
        {
            t.Insert(rng.Next(25, 75), 42);
        }
        //Assert.Equal(1, t.Minimum().Key);
        //Assert.Equal(100, t.Maximum().Key);
        //Assert.Equal(1, t[50]);
    }
}