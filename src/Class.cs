using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CodeGeneration
{
    class Class : Interface
    {
        public Exstensibility exstensibilty { get; set; }
        public List<Field> Fields { get; private set; }        
        public List<Constructor> Constructors { get; private set; }


        public Class(string Name)
            : base (Name)
        {
                
        }

        public override string ToString()
        {
            return ToString (0);
        }

        public override string ToString(int n)
        {
            var sb = new StringBuilder ();

            var name = accessibility + " " + exstensibilty + " class " + Name;

            sb.AppendLine (name, n);
            sb.AppendLine ("{", n);


            foreach (var fld in Fields)
                sb.AppendLine (fld.ToString (n + 1));

            foreach (var cstor in Constructors)
                sb.AppendLine (cstor.ToString (n + 1));

            foreach (var mthod in Methods)
                sb.AppendLine (mthod.ToString (n + 1));

            foreach (var prop in Properties)
                sb.AppendLine (prop.ToString (n + 1));


            sb.AppendLine ("}", n);

            return sb.ToString ();
        }

    }
}
