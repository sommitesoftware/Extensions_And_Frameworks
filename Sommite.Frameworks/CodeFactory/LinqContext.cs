using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Sommite.Frameworks.CodeFactory
{
    /// <summary>
/// Represents the class public.
/// </summary>
public class LinqContext:List<LinqTable>
    {

        public DataContext DataContext { get; set; }

        private LinqWebConnect webConnect { get; set; }

        public LinqContext(DataContext dataContext)
        {
            DataContext = dataContext;
            _selectedTables=new List<LinqTable>();
            _usings = new List<string>();
            webConnect=new LinqWebConnect(_selectedTables);
            GetTables();
        }

        private void GetTables()
        {
            var mapping = DataContext.Mapping;
            var tables = mapping.GetTables();
            foreach (MetaTable table in tables)
            {
                MetaType tt = table.RowType;
                Add(new LinqTable(tt.Type, DataContext));
            }
        }

        public string TableClassNameSpace { get; set; }
        public string TableMvcApiNameSpace { get; set; }
        public string WebConnectNameSpace { get; set; }

        public string TableClassRootPath { get; set; }
        public string TableMvcApiRootPath { get; set; }
        public string WebConnectRootPath { get; set; }

        public string CombinedTableClassPath(string codeFile)
        {
            if (TableClassRootPath.EndsWith(@"\"))
            {
                return TableClassRootPath + codeFile;
            }
            else
            {
                return TableClassRootPath + @"\" + codeFile;
            }
        }

        public string CombinedTableMvcApiPath(string codeFile)
        {
            if (TableMvcApiRootPath.EndsWith(@"\"))
            {
                return TableMvcApiRootPath + codeFile;
            }
            else
            {
                return TableMvcApiRootPath + @"\" + codeFile;
            }
        }

        public string CombinedWebConnectPath(string codeFile)
        {
            if (WebConnectRootPath.EndsWith(@"\"))
            {
                return WebConnectRootPath + codeFile;
            }
            else
            {
                return WebConnectRootPath + @"\" + codeFile;
            }
        }

        private List<LinqTable> _selectedTables { get; set; }

        public List<LinqTable> SelectedTables
        {
            get { return _selectedTables; }
        }

        private List<string> _usings { get; set; } 
        public List<string> Usings {
            get { return _usings; }
        } 

        public void GenerateCode(bool overwrite = true)
        {
            foreach (LinqTable selectedTable in SelectedTables)
            {
                if (!File.Exists(CombinedTableClassPath(selectedTable.TableClassCodeFile)) || overwrite)
                {
                    File.WriteAllText(CombinedTableClassPath(selectedTable.TableClassCodeFile), selectedTable.TableClass.GetDefinition(TableClassNameSpace));
                }

                //if (!File.Exists(CombinedTableMvcApiPath(selectedTable.TableMvcApiCodeFile)) || overwrite)
                //{
                //    File.WriteAllText(CombinedTableMvcApiPath(selectedTable.TableMvcApiCodeFile), selectedTable.TableMvcApi.GetDefinition(TableMvcApiNameSpace, DataContext.GetType().Name, Usings));
                //}
                
            }

            if (!File.Exists(CombinedWebConnectPath("Client.cs")) || overwrite)
            {
                File.WriteAllText(CombinedWebConnectPath("Client.cs"), webConnect.GetDefinition(WebConnectNameSpace, DataContext.GetType().Name, Usings));
            }
        }

    }
}
