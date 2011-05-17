using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CodeGeneration
{
    public abstract class Exstensibility
    {

        public static Exstensibility @abstract = new Abstract ();
        public static Exstensibility @sealed = new Sealed ();
        public static Exstensibility @neither = new Neither ();

        public abstract override string ToString();

        class Sealed : Exstensibility
        {
            public override string ToString()
            {
                return "sealed";
            }
        }

        class Abstract : Exstensibility
        {
            public override string ToString()
            {
                return "abstract";
            }
        }

        class Neither : Exstensibility
        {
            public override string ToString()
            {
                return "";
            }
        }
    }


    interface Eater<T>
    {
        void Eat(T x);
    }

    public abstract class Foo<T> : IEnumerable<T>, Eater<T> where T : Foo<T>
    {
        public abstract IEnumerator<T> GetEnumerator();
        public abstract void Eat(T x);
        
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }       
    }

    public class Bar : Foo<Bar>
    {

        void foo()
        {
            
        }
        
        
        public override IEnumerator<Bar> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public override void Eat(Bar x)
        {
            
        }
    }
}
