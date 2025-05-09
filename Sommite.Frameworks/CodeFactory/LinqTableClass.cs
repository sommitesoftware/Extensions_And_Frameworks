using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sommite.Frameworks.CodeFactory
{
    /// <summary>
/// Represents the class public.
/// </summary>
public class LinqTableClass
    {
        #region Declaration

        private Type t { get; set; }
        private string tableName { get; set; }
        private string className { get; set; }
        private LinqTable linqTable { get; set; }
        public string PrimaryKey { get; set; }

        #endregion

        public LinqTableClass(Type type, DataContext dataContext)
        {
            t = type;
            className = t.Name;

            linqTable = new LinqTable(t, dataContext);
            tableName = linqTable.Name;
        }

        public string GetDefinition(string nameSpace, string[] exclude = null)
        {

            var dt = linqTable.AsDataTable;

            var sb = new StringBuilder();

            sb.AppendLine("using System;");
            sb.AppendLine("");
            sb.AppendLine("namespace " + nameSpace);
            sb.AppendLine("{");

            sb.AppendLine("public partial class " + className);

            sb.AppendLine("{");
            sb.AppendLine("");

            sb.AppendLine("public " + className + "()");
            sb.AppendLine("{");
            sb.AppendLine("}");
            sb.AppendLine();

            sb.AppendLine("public " + className + "(" + t + " o)");
            sb.AppendLine("{");
            var translate = @"    Translate<" + className + @">.To(this,o);";
            sb.AppendLine(translate);
            sb.AppendLine("    Initialise(o);");
            sb.AppendLine("}");
            sb.AppendLine("");

            sb.AppendLine("partial void Initialise(" + t + " o);");

            foreach (var prop in t.GetProperties())
            {
                if (dt.Columns.Contains(prop.Name))
                {
                    var attr = prop.GetCustomAttributes(typeof (ColumnAttribute),true);
                    var nul = !((ColumnAttribute) attr[0]).DbType.Contains("NOT NULL");
                    if (attr.Any() && ((ColumnAttribute)attr[0]).IsPrimaryKey) PrimaryKey = prop.Name;
                    sb.AppendLine(string.Format("public {0}{4} {1} {2} get; set; {3}", dt.Columns[prop.Name].DataType,
                        prop.Name,
                        "{", "}", ((nul && dt.Columns[prop.Name].DataType.ToString() != "System.String") ? "?" : "")));
                }
            }
            sb.AppendLine("");
            sb.AppendLine("}");
            sb.AppendLine("}");

            return sb.ToString();
        }

        public override string ToString()
        {
            return className;
        }
    }
}
