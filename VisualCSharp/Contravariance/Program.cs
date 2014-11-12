using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contravariance
{
    class Sodavand { }

    class Cola : Sodavand { }

    // Contravariant interface. 
    interface C<in T>  {
        void doStuff(T para);
    }

    class D<T> : C<T>
    {
        public void doStuff(T para)
        {
        }
    }

    class E : C<Sodavand>
    {
        public void doStuff(Sodavand para)
        {
        }
    }

    class F
    {
        public virtual void doStuff(Cola a) { }
    }
    class G : F
    {

        public void doStuff(Sodavand a)
        {
            throw new NotImplementedException();
        }

        public override void doStuff(Cola a)
        {
            doStuff((Sodavand)a);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            C<Cola> test = new D<Sodavand>();
            C<Cola> test1 = new E();
            //E<Object> test = new E<String>();
            Console.WriteLine("hej");
            Console.ReadLine();
        }
    }
}
