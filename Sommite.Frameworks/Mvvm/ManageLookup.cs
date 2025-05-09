using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.Linq;


namespace Sommite.Frameworks.MVVM
{
    public partial class ManageLookup<T> : DevExpress.XtraEditors.XtraUserControl
    {
        public ManageLookup(DataContext dc,string refreshTable="TableRefreshes")
        {
            InitializeComponent();
            initialContext = dc;
            this.refreshTable = refreshTable;
            ViewModel = Activator.CreateInstance<T>();
            t = ViewModel.GetType();
            RefreshData();
        }


        public object ViewModel { get; set; }
        private DataContext initialContext { get; set; }
        private string refreshTable;
        private Type t { get; set; }
        


        private DataContext db { get; set; }

        public DataContext Db(bool refresh = false)
        {
            if (db == null || refresh)
            {
                db = new DataContext(initialContext.Connection);
            }

            return db;
        }

        private void RefreshTable()
        {
            try
            {
                using (DataContext _refreshDb = new DataContext(initialContext.Connection))
                {
                    string q = string.Format("update {0} set Status=1 where TableName = {1}",refreshTable,ViewModel.GetType().Name);
                    _refreshDb.ExecuteCommand(q);
                }
            }
            catch { }
        }

        public void RefreshData()
        {
            listBindingSource.DataSource = Db().GetTable(t); //AsQueryable().OrderBy(x => x.GetType().GetProperty("Name").GetValue(x));
        }

        private bool _isNew = false;
        private void gvList_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {

            try
            {
                
                var current = (T)e.Row;
                if ((int)current.GetType().GetProperty("ID").GetValue(current) == 0)
                {
                    _isNew = true;
                    Db().GetTable(t).InsertOnSubmit(current);
                    Db().SubmitChanges();

                }
                else
                {
                    Db().SubmitChanges();
                }
                RefreshTable();
                RefreshData();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void bbiDeleteCurrent_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show(this, "Wilt u deze regel verwijderen?", "Bevestigen a.u.b.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                listBindingSource.RemoveCurrent();
                try
                {
                    Db().SubmitChanges();
                }
                catch
                {
                    MessageBox.Show(this, "Deze regel is in gebruik", "Kan niet verwijderen", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    RefreshTable();
                }
            }

        }

    }
}
