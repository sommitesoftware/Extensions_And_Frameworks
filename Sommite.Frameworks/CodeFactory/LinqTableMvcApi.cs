using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sommite.Frameworks.CodeFactory
{
    /// <summary>
/// Represents the class public.
/// </summary>
public class LinqTableMvcApi
    {
        

        #region Declaration
        public Type Type { get; set; }
        private string tableName { get; set; }
        private string className { get; set; }
        private string classNamePlural { get; set; }
        public string PrimaryKey { get; set; }
        private LinqTable linqTable { get; set; } 
        #endregion

        public LinqTableMvcApi(Type type,DataContext dataContext)
        {
            Type = type;
            className = Type.Name;
            classNamePlural = (className.EndsWith("y") ? className.TrimEnd('y') : className) +
                              (className.EndsWith("y") ? "ies" : "s");

            linqTable = new LinqTable(Type, dataContext);
            tableName = linqTable.Name;

            foreach (var prop in Type.GetProperties())
            {
                var attr = prop.GetCustomAttributes(typeof(ColumnAttribute), true);
                if (attr.Any() && ((ColumnAttribute) attr[0]).IsPrimaryKey) PrimaryKey = prop.Name;
            }

        }

        public string GetDefinition(string nameSpace, string contextName,List<string> usings  ,string[] exlude = null)
        {
            var dt = linqTable.AsDataTable;

            var sb = new StringBuilder();

            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Web.Http;");

            foreach (var @using in usings)
            {
                sb.AppendLine(@using);
            }

            sb.AppendLine("");
            sb.AppendLine(string.Format("namespace {0}", nameSpace));
            sb.AppendLine("{");

            sb.AppendLine(string.Format("public class {0}Controller : ApiController", className));

            sb.AppendLine("{");
            sb.AppendLine("");

            sb.AppendLine(string.Format(@"//GET api/{0}", className.ToLower()));
            sb.AppendLine("[HttpGet]");
            sb.AppendLine(string.Format("public IEnumerable<json.{0}> Get()", className));
            sb.AppendLine("{");
            sb.AppendLine(string.Format("var list = new List<json.{0}>();", className));
            sb.AppendLine("try");
            sb.AppendLine("{");
            sb.AppendLine(string.Format("    using ({0} db = {0}.ConnectWeb())", contextName));
            sb.AppendLine("    {");
            sb.AppendLine(string.Format("        foreach (var {0} in db.{1})", className, classNamePlural));
            sb.AppendLine("        {");
            sb.AppendLine(string.Format("            list.Add(new json.{0}({0}));", className));
            sb.AppendLine("        }");
            sb.AppendLine("    }");
            sb.AppendLine("}");
            sb.AppendLine("catch");
            sb.AppendLine("{");
            sb.AppendLine("}");
            sb.AppendLine("return list.AsEnumerable();");
            sb.AppendLine("}");
            sb.AppendLine("");

            sb.AppendLine("[HttpPost]");
            sb.AppendLine(string.Format("public void Post([FromBody] json.{0} value)", className));
            sb.AppendLine("{");
            sb.AppendLine("    if (value == null) return;");
            sb.AppendLine("    try");
            sb.AppendLine("    {");
            sb.AppendLine(string.Format("        using (var db = {0}.ConnectWeb())", contextName));
            sb.AppendLine("        {");
            sb.AppendLine(string.Format("            var id = (int) value.{0};", PrimaryKey));
            sb.AppendLine(string.Format("            var a = db.{0}.SingleOrDefault(x => x.{1} == id);", classNamePlural, PrimaryKey));
            sb.AppendLine(string.Format("            a = DataAccess.JSON.Translate<{0}>.From(value, a);", className));
            sb.AppendLine("            db.SubmitChanges();");
            sb.AppendLine("        }");
            sb.AppendLine("    }");
            sb.AppendLine("    catch(Exception ex)");
            sb.AppendLine("    {");
            sb.AppendLine("        var err = ex.ToString();");
            sb.AppendLine("    }");
            sb.AppendLine("}");
            sb.AppendLine("");

            sb.AppendLine("[HttpPut]");
            sb.AppendLine(string.Format("public void Put([FromBody] json.{0} value)", className));
            sb.AppendLine("{");
            sb.AppendLine("    if (value == null) return;");
            sb.AppendLine("    try");
            sb.AppendLine("    {");
            sb.AppendLine(string.Format("        using (var db = {0}.ConnectWeb())", contextName));
            sb.AppendLine("        {");
            sb.AppendLine(string.Format("            var id = (int) value.{0};", PrimaryKey));
            sb.AppendLine(string.Format("            var a = db.{0}.SingleOrDefault(x => x.{1} == id);", classNamePlural, PrimaryKey));
            sb.AppendLine(string.Format("            a = DataAccess.JSON.Translate<{0}>.From(value, a);", className));
            sb.AppendLine("            if (a == null)");
            sb.AppendLine("            {");
            sb.AppendLine(string.Format("                db.{0}.InsertOnSubmit(a);", classNamePlural));
            sb.AppendLine("            }");
            sb.AppendLine("            db.SubmitChanges();");
            sb.AppendLine("        }");
            sb.AppendLine("    }");
            sb.AppendLine("    catch(Exception ex)");
            sb.AppendLine("    {");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("}");
            sb.AppendLine("");

            sb.AppendLine("// DELETE api/values/5");
            sb.AppendLine("[HttpDelete]");
            sb.AppendLine("public void Delete(int id)");
            sb.AppendLine("{");
            sb.AppendLine("    try");
            sb.AppendLine("    {");
            sb.AppendLine(string.Format("        using (var db = {0}.ConnectWeb())", contextName));
            sb.AppendLine("        {");
            sb.AppendLine(string.Format("            var a = db.{0}.SingleOrDefault(x => x.{1} == id);", classNamePlural, PrimaryKey));
            sb.AppendLine(string.Format("            db.{0}.DeleteOnSubmit(a);", classNamePlural));
            sb.AppendLine("        }");
            sb.AppendLine("    }");
            sb.AppendLine("    catch (Exception ex)");
            sb.AppendLine("    {");
            sb.AppendLine("        var err = ex.ToString();");
            sb.AppendLine("    }");
            sb.AppendLine("}");
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
