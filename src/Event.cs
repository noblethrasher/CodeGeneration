using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CodeGeneration
{
    abstract class Event : Member
    {
        public Event(string name, Type type, Accessiblity accessibility = null) : base (name, type, accessibility) { }
    }
}
