using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CodeGeneration
{
    class Interface
    {

        public List<Interface> BaseInterfaces { get; private set; }
        public Accessiblity accessibility { get; set; }

        public List<Method> Methods { get; private set; }
        public List<Property> Properties { get; private set; }
        public string Name { get; set; }


        private Interface()
        {
            Methods = new List<Method> ();
            Properties = new List<Property> ();

        }

        public Interface(string Name) : this()
        {
            this.accessibility = Accessiblity.@public;
            this.Name = Name;
        }

        public override string ToString()
        {
            return ToString (0);
        }

        public virtual string ToString(int n)
        {
            var sb = new StringBuilder ();

            var name = accessibility + " interface " + Name;

            sb.AppendLine (name, n);
            sb.AppendLine ("{", n);

            foreach (var method in Methods)
                sb.AppendLine(method.ToString(n + 1));

            foreach (var property in Properties)
                sb.AppendLine(property.ToString(n + 1));


            sb.AppendLine ("}", n);

            return sb.ToString ();
        }
    }
}
