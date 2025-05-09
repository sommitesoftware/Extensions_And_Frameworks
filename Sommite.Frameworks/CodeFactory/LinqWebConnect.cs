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
public class LinqWebConnect
    {


        #region Declaration
        //public Type Type { get; set; }
        private List<LinqTable> tables { get; set; }
        #endregion

        public LinqWebConnect(List<LinqTable> tables)
        {
            this.tables = tables;

        }

        public string GetDefinition(string nameSpace, string contextName, List<string> usings, string[] exlude = null)
        {

            var sb = new StringBuilder();

            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Net.Http;");
            sb.AppendLine("using System.Net.Http.Headers;");
            sb.AppendLine("using System.Net.Http.Formatting;");
            sb.AppendLine("using Boodschapper.WebConnect.Properties;");

            foreach (var @using in usings)
            {
                sb.AppendLine(@using);
            }

            sb.AppendLine("");
            sb.AppendLine(string.Format("namespace {0}", nameSpace));
            sb.AppendLine("{");

            sb.AppendLine(string.Format("public class {0}", "Client"));

            sb.AppendLine("{");
            sb.AppendLine("");
            sb.AppendLine(@"//const string apiUrl = @""http://localhost:55363/api/"";");
            sb.AppendLine("static HttpClient client { get; set; }");
            sb.AppendLine("private static void Instantiate()");
            sb.AppendLine("{");
            sb.AppendLine("    if (client == null)");
            sb.AppendLine("    {");
            sb.AppendLine("        var apiUrl = Settings.Default.baseUrl;");
            sb.AppendLine("        client = new HttpClient {BaseAddress = new Uri(apiUrl)};");
            sb.AppendLine("        client.DefaultRequestHeaders.Clear();");
            sb.AppendLine(@"        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(""application/json""));");
            sb.AppendLine("    }");
            sb.AppendLine("}");

            foreach (LinqTable linqTable in tables)
            {

                sb.AppendLine("");
                sb.AppendLine("private static HttpResponseMessage delete" + linqTable.Type.Name + "(int id)");
                sb.AppendLine("{");
                sb.AppendLine("     Instantiate();");
                sb.AppendLine(@"    return client.DeleteAsync(""" + linqTable.Type.Name.ToLower() + @"/"" + id + ""?apikey=78e6c4b298da76f449e7d0a0f9749ecc"").Result;");
                sb.AppendLine("}");
                sb.AppendLine("");
                sb.AppendLine("public static bool Delete" + linqTable.Type.Name + "(int id)");
                sb.AppendLine("{");
                sb.AppendLine("    return delete" + linqTable.Type.Name + "(id).IsSuccessStatusCode;");
                sb.AppendLine("}");
            
            sb.AppendLine("");
            sb.AppendLine("private static HttpResponseMessage update" + linqTable.Type.Name + "(List<json." + linqTable.Type.Name + "> list)");
            sb.AppendLine("{");
            sb.AppendLine("     Instantiate();");
            sb.AppendLine(@"    return client.PostAsJsonAsync(""" + linqTable.Type.Name.ToLower() + @"?apikey=78e6c4b298da76f449e7d0a0f9749ecc"", list).Result;");
            sb.AppendLine("}");
            sb.AppendLine("");
            sb.AppendLine("public static bool Update" + linqTable.Type.Name + "(List<" + linqTable.Type.Name + "> list)");
            sb.AppendLine("{");
            sb.AppendLine("    return update" + linqTable.Type.Name + "(list.Select(x=>new json." + linqTable.Type.Name + "(x)).ToList()).IsSuccessStatusCode;");
            sb.AppendLine("}");
                
            }
            sb.AppendLine("");

           
            sb.AppendLine("}");
            sb.AppendLine("}");

            return sb.ToString();
        }

        
    }
}
