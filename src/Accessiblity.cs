using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CodeGeneration
{
    public abstract class Accessiblity
    {

        public static Accessiblity @public = new Public();
        public static Accessiblity @protected = new Protected();
        public static Accessiblity @internal = new Internal();
        public static Accessiblity @private = new Private();
        public static Accessiblity @protected_internal = new ProtectedInternal();


        protected readonly string str;

        protected Accessiblity(string name)
        {
            str = name;
        }

        public override sealed string ToString()
        {
            return str;
        }

        class Private : Accessiblity
        {
            public Private() : base("private") { }
        }

        class Public : Accessiblity
        {
            public Public() : base("public") { }
        }

        class Protected : Accessiblity
        {
            public Protected() : base("protected") { }
        }

        class Internal : Accessiblity
        {
            public Internal() : base("internal") { }
        }

        class ProtectedInternal : Accessiblity
        {
            public ProtectedInternal() : base("protected internal") { }
        }
    }
}