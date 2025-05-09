using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Sommite.Extensions;
using Sommite.Frameworks.Shared;
using System.Reflection;

namespace Sommite.Frameworks.MVVM
{
    /// <summary>
/// Represents the class public.
/// </summary>
public class BindingProvider<T>
    {
        #region Events
        public event ChangedEventHandler Added;
        
        public event ChangedEventHandler Updated;

        public event ChangedEventHandler Deleted;

        private ChangedEventArgs GetChangedEventArgs(bool isNew)
        {
            return new ChangedEventArgs()
            {
                IsNew = true,
                objectId = ViewModel.GetType().GetProperty(keyName).GetValue(ViewModel, null)
            };
        }

        public void OnAdded(ChangedEventArgs e)
        {
            if (Added != null)
            {
                e.IsNew = true;
                Added(this, e);
            }
        }

        public void OnUpdated(ChangedEventArgs e)
        {
            if (Updated != null)
                Updated(this, e);
        }

        public void OnDeleted(ChangedEventArgs e)
        {
            if (Deleted != null)
                Deleted(this, e);
        }

        #endregion

        #region Lookups
        private BindingLookups<object> bindingLookups { get; set; }
        private GridBindings gridBindings { get; set; }

        public BindingLookups<object> BindingLookups
        {
            get
            {
                if (bindingLookups == null)
                {
                    bindingLookups=new BindingLookups<object>();
                }

                return bindingLookups;
            }
        }

        public GridBindings GridBindings
        {
            get
            {
                if (gridBindings == null)
                {
                    gridBindings = new GridBindings();

                }

                return gridBindings;
            }
        }
        #endregion

        #region DataContext
        private DataContext initialContext { get; set; }

        private DataContext db { get; set; }

        public DataContext Db(bool refresh = false)
        {
            if (db == null || refresh)
            {
                db = new DataContext(initialContext.Connection);
            }

            return db;
        }

        public T GetDb<T>()
        {
            try
            {
                return (T)Convert.ChangeType(db, typeof(T));
            }
            catch (InvalidCastException)
            {
                return default(T);
            }
        }
       
        #endregion

        #region Declarations
        private bool isNew { get; set; }

        private string keyName { get; set; }

        private Type t { get; set; }
        #endregion

        public object ViewModel { get; set; }

        public void ExecuteMethod(string methodName)
        {
            try
            {
                ViewModel.GetType().GetMethod(methodName).Invoke(ViewModel,null);
            }
            catch (Exception ex)
            {
                var e = ex.Message;
            }
        }


        /// <summary>
        /// The BindingProvider replaces the BindingSource which is most commonly used on Winforms to bind controls to datasources
        /// </summary>
        /// <param name="T"></param>
        /// <param name="keyName"></param>
        /// <param name="context"></param>
        /// <param name="bindingLookups"></param>
        public BindingProvider(object T, string keyName, DataContext context, BindingLookups<object> bindingLookups=null)
        {
            initialContext = context;
            this.keyName = keyName;
            if (T == null) T = Activator.CreateInstance<T>();
            var ID = (int) T.GetType().GetProperty(keyName).GetValue(T, null);
            isNew = ID == 0;

            t = T.GetType();

            if (!isNew)
            {
                var table = Db().GetTable(t).Cast<T>();
                foreach (var row in table)
                {
                    if ((int) row.GetType().GetProperty(keyName).GetValue(row, null) == ID) ViewModel = row;
                }
            }
            else
            {
                ViewModel = Activator.CreateInstance<T>();
                ViewModel = T;
            }

            if (bindingLookups != null)
            {
                this.bindingLookups = bindingLookups;
            }
        }

        #region Binding Methods

        private int level = 0;

        public void PerformBinding(Control control, object parent, string fkField, string title = "")
        {
            level++;
            if (level == 1) BindParent(parent, fkField);

            foreach (Control ctrl in control.Controls)
            {
                if (ctrl.HasChildren && ctrl.Tag==null)
                {
                    PerformBinding(ctrl, null, null);
                }
                else
                {
                    if (!ctrl.Name.IsEmpty() && new BindToPropertyTypes().Select(x=>x.Key).Contains(ctrl.Name.Substring(0,2)))
                    {
                        var par = new BindingParameters(ctrl);
                        if (!par.ValueField.IsEmpty() && par.ValueField.Contains(".") && isNew) continue;
                        switch (par.BindToProperty)
                        {
                            case "Title":
                                if (!title.IsEmpty()) BindTitle(ctrl, par.ValueField, title);
                                break;
                            case "Lookup":
                                BindLookup(ctrl, BindingLookups.GetBindingLookup(par.ListName), par.ValueField,
                                    par.DisplayField,
                                    par.LookupField);
                                break;
                            case "DataMember":
                                BindGrid(ctrl,par.DataMember);
                                break;
                            default:
                                BindToProperty(ctrl, par.BindToProperty, par.ValueField);
                                break;
                        }

                    }
                }
            }
        }

       

        public void BindToProperty(Control control, string property, string valueField)
        {
            try
            {
                control.DataBindings.Clear();
                if (valueField.Contains("."))
                {
                    var o = ViewModel.GetType().GetProperty(valueField.Split('.')[0]).GetValue(ViewModel, null);
                    if (o == null) return;
                }
                control.DataBindings.Add(property, ViewModel, valueField, true,
                    DataSourceUpdateMode.OnPropertyChanged, null);
            }
            catch(Exception ex)
            {
                var e = ex.Message;
            }
        }

        
        
        public void BindChecked(Control control, string valueField)
        {
            BindToProperty(control, "Checked", valueField);
        }

        public void BindVisible(Control control, string valueField)
        {
            BindToProperty(control, "Visible", valueField);
        }

        public void BindEnable(Control control, string valueField)
        {
            BindToProperty(control, "Enabled", valueField);
        }

        public void BindLookup<L>(Control control, IEnumerable<L> list, string valueField, string displayField, string lookupValueField = "")
        {
            
            try
            {
                if (!list.Any()) return;
                var lookup = (LookUpEdit) control;
                lookup.DataBindings.Clear();
                lookup.Properties.DataSource = list;
                lookup.Properties.Columns.Clear();
                lookup.Properties.Columns.Add(new LookUpColumnInfo(displayField, 150, "Omschrijving"));
                lookup.Properties.DisplayMember = displayField;
                lookup.Properties.ValueMember = lookupValueField == "" ? valueField : lookupValueField;
                lookup.DataBindings.Add("EditValue", ViewModel, valueField, true, DataSourceUpdateMode.OnPropertyChanged, null);
            }
            catch (Exception ex)
            {
                var err = ex.ToString();
            }
        }

        public void BindGrid(Control control, string dataMember)
        {
            control.DataBindings.Clear();
            var gridBinding = GridBindings.SingleOrDefault(x => x.GridControl.Name == control.Name);
            if (gridBinding != null)
            {
                try
                {
                    var paddingColumn = new DevExpress.XtraGrid.Columns.GridColumn()
                    {
                        Name = control.Name + "_paddingColumn",
                        Caption = "",
                        FieldName = "",
                        Visible = true
                    };

                    var grid = (GridControl)control;
                    var view = grid.Views[0] as GridView;

                    view.Columns.Clear();

                    var cnt = 0;
                    if (gridBinding.PaddingMode == GridColumnPaddingMode.Left) view.Columns.Add(paddingColumn);
                    foreach (var column in gridBinding.Columns)
                    {
                        var colName = string.Format("{0}_column_{1}", control.Name, cnt);
                        var col = new DevExpress.XtraGrid.Columns.GridColumn()
                        {
                            Name = colName,
                            Caption = column.ColumnName,
                            FieldName = column.FieldName,
                            Visible = true,
                            Width = column.Width,
                        };
                        col.OptionsColumn.FixedWidth = gridBinding.PaddingMode != GridColumnPaddingMode.None || column.FixedWidth;
                        view.Columns.Add(col);
                        cnt++;
                    }
                    if (gridBinding.PaddingMode == GridColumnPaddingMode.Right) view.Columns.Add(paddingColumn);


                }
                catch (Exception ex)
                {
                }
            }

            control.DataBindings.Add("DataSource", ViewModel, dataMember, true,
                DataSourceUpdateMode.OnPropertyChanged, null);

            //var test = ((GridControl) control).DataMember;
        }

        public void BindGrid(Control control, IEnumerable<T> list)
        {
            control.DataBindings.Clear();
            var gridBinding = GridBindings.SingleOrDefault(x => x.GridControl.Name == control.Name);
            if (gridBinding != null)
            {
                try
                {
                    var paddingColumn = new DevExpress.XtraGrid.Columns.GridColumn()
                    {
                        Name = control.Name + "_paddingColumn",
                        Caption = "",
                        FieldName = "",
                        Visible = true
                    };

                    var grid = (GridControl)control;
                    var view = grid.Views[0] as GridView;

                    view.Columns.Clear();

                    var cnt = 0;
                    if (gridBinding.PaddingMode == GridColumnPaddingMode.Left) view.Columns.Add(paddingColumn);
                    foreach (var column in gridBinding.Columns)
                    {
                        var colName = string.Format("{0}_column_{1}", control.Name, cnt);
                        var col = new DevExpress.XtraGrid.Columns.GridColumn()
                        {
                            Name = colName,
                            Caption = column.ColumnName,
                            FieldName = column.FieldName,
                            Visible = true,
                            Width = column.Width,
                        };
                        col.OptionsColumn.FixedWidth = gridBinding.PaddingMode != GridColumnPaddingMode.None || column.FixedWidth;
                        view.Columns.Add(col);
                        cnt++;
                    }
                    if (gridBinding.PaddingMode == GridColumnPaddingMode.Right) view.Columns.Add(paddingColumn);


                }
                catch (Exception ex)
                {
                }
            }

        }
        
        public void BindCombo(ComboBoxEdit combo, List<object> items, string valueField)
        {
            combo.Properties.Items.Clear();
            foreach (var item in items)
            {
                combo.Properties.Items.Add(item);
            }
            combo.DataBindings.Clear();
            combo.DataBindings.Add("Text", ViewModel, valueField, true, DataSourceUpdateMode.OnPropertyChanged, null);
        }

        public void BindParent(object value, string fkFieldName)
        {
            if (value == null) return;
            PropertyInfo propertyInfo = ViewModel.GetType().GetProperty(fkFieldName);
            propertyInfo.SetValue(ViewModel, Convert.ChangeType(value, propertyInfo.PropertyType), null);
        }

        //public void BindParent(object value, string fkFieldName)
        //{
        //    if (value == null) return;
        //    PropertyInfo propertyInfo = value.GetType().GetProperty(fkFieldName);
        //    propertyInfo.SetValue(value, Convert.ChangeType(value, propertyInfo.PropertyType), null);
        //}

        

        public void BindTitle(Control title, string valueField, string format = null)
        {
            if (format == null)
            {
                title.DataBindings.Add("Title", ViewModel, valueField, true);

            }
            else
            {
                title.DataBindings.Add("Title", ViewModel, valueField, true,
                    DataSourceUpdateMode.OnPropertyChanged, "", format);
            }
        }

        #endregion

        #region Data Methods
        public bool Save()
        {
            bool retVal = true;
            try
            {
                if (isNew)
                {
                    try
                    {
                        ViewModel.GetType().GetProperty("CreatedOn").SetValue(ViewModel, DateTime.Now);
                        ViewModel.GetType().GetProperty("CreatedBy").SetValue(ViewModel, Environment.UserName);
                    }
                    catch
                    {
                    }
                    Add();

                }
                else
                {
                    try
                    {
                        ViewModel.GetType().GetProperty("ChangedOn").SetValue(ViewModel, DateTime.Now);
                        ViewModel.GetType().GetProperty("ChangedBy").SetValue(ViewModel, Environment.UserName);
                    }
                    catch
                    {
                    }
                    Update();
                }
            }
            catch
            {
                retVal = false;
            }
            return retVal;

        }

        private void Add()
        {
            try
            {
                //See if there's a property "Active" and if so, set it to true
                try
                {
                    ViewModel.GetProperty("Active").SetValue(ViewModel, true);
                }
                catch
                {
                }
                //bool isAttached = (from prop in ViewModel.GetType().GetProperties() let attr = prop.GetCustomAttributes(typeof (AssociationAttribute), true) where attr.Any() && !((AssociationAttribute) attr[0]).OtherKey.IsEmpty() select prop).Any(prop => prop.GetValue(ViewModel, null) != null);

                bool isAttached = false;

                foreach (var prop in ViewModel.GetType().GetProperties())
                {
                    var attr = prop.GetCustomAttributes(typeof(AssociationAttribute), true);
                    if (attr.Any() && !((AssociationAttribute)attr[0]).OtherKey.IsEmpty())
                    {
                        var val = prop.GetValue(ViewModel, null);
                        try
                        {
                            var cnt = (int?) val.GetType().GetProperty("Count").GetValue(val, null);
                            if (cnt.HasValue && cnt == 0) continue;
                            isAttached = true;
                            break;
                        }
                        catch
                        {
                        }
                    }
                }

                if (!isAttached)
                {
                    Db().GetTable(t).InsertOnSubmit(ViewModel);
                }
                Db().SubmitChanges();
                OnAdded(GetChangedEventArgs(true));

            }
            catch (DataException dex)
            {
            }
            catch (NotSupportedException nse)
            {
                try
                {
                    Db().ChangeConflicts.Clear();
                    //Db().GetChangeSet().Inserts.Clear();

                    Db().SubmitChanges();
                }
                catch (Exception ex2)
                {
                    var err = ex2.ToString();
                }
            }
            catch (Exception ex)
            {
                var err = ex.ToString();
            }
        }

        private void Update()
        {
            try
            {
                Db().SubmitChanges();
                OnUpdated(GetChangedEventArgs(false));

            }
            catch (DataException dex)
            {
            }
            catch (Exception ex)
            {
                var err = ex.ToString();
            }

        }

        public void Delete()
        {
            try
            {
                Db().GetTable(t).DeleteOnSubmit(ViewModel);
                Db().SubmitChanges();
                OnDeleted(GetChangedEventArgs(false));
            }
            catch (DataException dex)
            {

            }
            catch (Exception ex)
            {
                var err = ex.ToString();
            }
        }
        #endregion

        #region Misc Methods
        public void Release()
        {
            ViewModel = null;
            try
            {
                db.Connection.Close();
            }
            catch
            {
            }
            db = null;
            GC.Collect();
        }
        #endregion

    }
}

