using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CodeGeneration
{
    abstract class Member
    {
        public Accessiblity accessibilty { get; set; }
        public string Name { get; set; }
        public Type MemberType { get; set; }
        public Class Class { get; set; }

        public Member(string Name, Type Type,  Accessiblity acc = null, Class Clas = null)
        {

            this.accessibilty = acc ?? Accessiblity.@public;

            this.Name = Name;
            this.MemberType = Type;
            this.accessibilty = acc;
        }

        public override string ToString()
        {
            return ToString (0);
        }


        public abstract string ToString(int n);
    }
}
