using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CodeGeneration
{
    abstract class Field : Member
    {
        string value;

        public Field(string name, Type type, Accessiblity accessibility = null, string value = null) : base (name, type, accessibility) { this.value = value; }

        public override string ToString(int n)
        {
            return this.MemberType.FullName + " " + this.Name + (value != null ? " " + value : "") + ";";
        }
    }
}
