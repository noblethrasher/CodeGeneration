using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CodeGeneration
{
     abstract class Method : Member
    {
        public Method(string name, Type type, Accessiblity accessibility = null) : base (name, type, accessibility) { }
    }
}
