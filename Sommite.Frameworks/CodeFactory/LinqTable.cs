using System;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.SqlClient;

namespace Sommite.Frameworks.CodeFactory
{
    /// <summary>
/// Represents the class public.
/// </summary>
public class LinqTable
    {

        private  string name { get; set; }
        public Type Type { get; set; }
        public DataContext DataContext { get; set; }

        public LinqTable(Type type, DataContext dataContext)
        {
            DataContext = dataContext;
            Type = type;
        }

        public string TableClassCodeFile
        {
            get { return TableClass + ".cs"; }
        }

        public string WebConnectCodeFile
        {
            get { return "Client.cs"; }
        }

        public string TableMvcApiCodeFile
        {
            get { return TableMvcApi + "Controller.cs"; }
        }

        public string Name
        {
            get
            {
                name = string.Empty;

                var type = Type;
                var attributes = type.GetCustomAttributes(typeof(TableAttribute), true);

                if (attributes.Length > 0)
                    name = ((TableAttribute)attributes[0]).Name;
                if (!string.IsNullOrEmpty(name))
                    return name;

                name = type.Name;
                return name;
            }
        }

        public DataTable AsDataTable
        {
            get
            {
                var conn = new SqlConnection(DataContext.Connection.ConnectionString);

                var query = string.Format("SELECT TOP 0 * FROM {0} WHERE 1=2", name);
                var cmd = new SqlCommand(query, conn);
                conn.Open();
                var dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                var dt = new DataTable();
                dt.Load(dr);
                return dt;
            }
        }

        public LinqTableClass TableClass
        {
            get
            {
                return new LinqTableClass(Type,DataContext);
            }
        }

        public LinqTableMvcApi TableMvcApi
        {
            get
            {
                return new LinqTableMvcApi(Type,DataContext);
            }
        }
    }
}
