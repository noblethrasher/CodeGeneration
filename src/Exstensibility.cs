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
}
