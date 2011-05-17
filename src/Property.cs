using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGeneration
{
    abstract class Property : Member
    {
        protected bool canRead, canWrite;

        public Property(string name, Type type, Accessiblity accessibility = null) : base (name, type, accessibility) { }
    }
}
