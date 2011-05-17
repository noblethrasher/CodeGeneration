using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Management.Smo;

namespace Data_Access_Primitives_Builder
{
    public abstract class Accessiblity
    {

        public static Accessiblity @public = new Public ();
        public static Accessiblity @protected = new Protected ();
        public static Accessiblity @internal = new Internal ();
        public static Accessiblity @private = new Private ();
        public static Accessiblity @protected_internal = new ProtectedInternal ();


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
            public Private() : base ("private") { }
        }

        class Public : Accessiblity
        {
            public Public() : base ("public") { }
        }

        class Protected : Accessiblity
        {
            public Protected() : base ("protected") { }
        }

        class Internal : Accessiblity
        {
            public Internal() : base ("internal") { }
        }

        class ProtectedInternal : Accessiblity
        {
            public ProtectedInternal() : base ("protected internal") { }
        }
    }
    

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

            foreach (var mthod in Methods)
                sb.AppendLine (mthod.ToString (n + 1));

            foreach (var prop in Properties)
                sb.AppendLine (prop.ToString (n + 1));


            sb.AppendLine ("}", n);

            return sb.ToString ();
        }
    }


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

    abstract class Property : Member
    {
        protected bool canRead, canWrite;

        public Property(string name, Type type, Accessiblity accessibility = null) : base (name, type, accessibility) { }
    }

    abstract class Field : Member
    {
        string value;

        public Field(string name, Type type, Accessiblity accessibility = null, string value = null) : base (name, type, accessibility) { this.value = value; }

        public override string ToString(int n)
        {
            return this.MemberType.FullName + " " + this.Name + (value != null ? " " + value : "") + ";";
        }
    }

    abstract class Method : Member
    {
        public Method(string name, Type type, Accessiblity accessibility = null) : base (name, type, accessibility) { }
    }

    abstract class Constructor : Member
    {
        public Constructor(string name, Type type, Accessiblity accessibility = null) : base (name, type, accessibility) { }
    }

    abstract class Event : Member
    {
        public Event(string name, Type type, Accessiblity accessibility = null) : base (name, type, accessibility) { }
    }




    static class Utils
    {

        static public IEnumerable<Table> GetParentTables(this Table table)
        {
            var db = table.Parent;
            var pks = table.Columns.ToEnumerable ().Where (x => x.InPrimaryKey && x.IsForeignKey);
            var fks = table.ForeignKeys.ToEnumerable ();

            var keys = from p in pks
                       let f =  fks.Where(x => x.Columns.Contains(p.Name)).ToList()
                       where f.Any()
                       select new {p, f = f[0] };

            var list = new List<Table> ();

            foreach (var k in keys)
                list.Add (db.Tables[k.f.ReferencedTable]);

            return list;

        }


        public static bool IsReadOnly(this Column column)
        {
            return !column.Computed && !column.Identity;
        }

        
        public static Type GetClrType(this Column column)
        {
            return column.DataType.GetClrType (column.Nullable);
        }


        public static Type GetClrType(this DataType SqlType, bool nullable = false)
        {
            var name = SqlType.Name.ToUpper ();

            switch (name)
            {
                case "INT":
                    return nullable ? typeof (int?) : typeof (int);

                case "BYTE":
                    return nullable ? typeof (byte?) : typeof (byte);

                case "FLOAT":
                    return nullable ? typeof (double?) : typeof (double);

                case "TINYINT":
                    return nullable ? typeof (byte?) : typeof (byte);

                case "SMALLINT":
                    return nullable ? typeof (short?) : typeof (short);

                case "DECIMAL":
                    return nullable ? typeof(decimal?) : typeof (decimal);



                case "CHAR":
                    return SqlType.MaximumLength == 1 ? (nullable ? typeof(char?) : typeof(char)) : typeof (char[]);

                case "VARCHAR":
                    return typeof (string);

                case "VARCHAR(MAX)":
                    return typeof (string);

                case "NVARCHAR":
                    return typeof (string);

                case "NVARCHAR(MAX)":
                    return typeof (string);

                case "TEXT":
                    return typeof (string);

                case "NTEXT":
                    return typeof (string);



                case "DATETIME":
                    return nullable ? typeof (DateTime?) : typeof (DateTime);

                case "SMALLDATETIME":
                    return nullable ? typeof (DateTime?) : typeof (DateTime);



                case "bit":
                    return nullable ? typeof (bool?) : typeof (bool);

                case "UNIQUEIDENTIFIER":
                    return nullable ? typeof (Guid?) : typeof (Guid);

                default:
                    return typeof (object);

            }
        }

        public static string GetFriendlyClrName(this Type type)
        {
            bool nullable = false;
            Type x = null;
            string typeName = null;

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                nullable = true;
                x = type.GetGenericArguments ()[0];
            }
            else
            {
                x = type;
            }

            typeName = GetCSharpTypeAlias(x.FullName);

            return typeName + (nullable ? "?" : "");


        }

        static readonly Dictionary<string, string> CSharpTypeAliases = new Dictionary<string, string>
            {
                {"String", "string"},
                {"Char", "string"},
                {"Boolean", "string"},
                {"Int32", "int"},
                {"Int16", "short"},                
                {"Byte", "byte"},
                {"Double", "double"},
                {"Decimal", "decimal"}
            };

        private static string GetCSharpTypeAlias(string Name)
        {
            var xs = Name.Split ('.');

            if(xs.Length == 2 && xs[0] == "System" && CSharpTypeAliases.ContainsKey(xs[1])) //inefficient.
            {
                Name = CSharpTypeAliases[xs[1]];
            }


            return Name;

        }


        public static bool Implies(this bool antecedent, bool consequent)
        {
            return !antecedent || consequent;
        }       


        public static IEnumerable<StoredProcedureParameter> ToEnumerable(this StoredProcedureParameterCollection xs)
        {
            foreach (StoredProcedureParameter @param in xs)
                yield return @param;
        }


        public static IEnumerable<Column> ToEnumerable(this ColumnCollection xs)
        {
            foreach (Column column in xs)
                yield return column;
        }

        public static IEnumerable<ForeignKeyColumn> ToEnumerable(this ForeignKeyColumnCollection xs)
        {
            foreach (ForeignKeyColumn column in xs)
                yield return column;
        }

        public static IEnumerable<ForeignKey> ToEnumerable(this ForeignKeyCollection xs)
        {
            foreach (ForeignKey fk in xs)
                yield return fk;
        }

        public static IEnumerable<Table> ToEnumerable(this TableCollection xs)
        {
            foreach (Table x in xs)
                yield return x;
        }
    }

    static class StringUtils
    {
        public static void AppendLine(this StringBuilder sb, string str, int n)
        {
            var s = "\t".Repeat (n) + str;

            sb.AppendLine (s);
        }

        public static string Repeat(this string s, int n)
        {
            var sb = new StringBuilder ();

            for (var i = 0; i < n; i++)
                sb.Append (s);
            
            return sb.ToString ();
        }

        public static string Repeat(this char c, int n)
        {
            var array = new char[n];
            
            for (var i = 0; i < n; i++)
                array[i] = c;
            
            return new String (array);
        }

        public static string Join(this IEnumerable<string> xs, string sep)
        {
            return string.Join (sep, xs);
        }
    }
}
