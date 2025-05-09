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
using System.Drawing;
using System.Web.Script.Serialization;

namespace Sommite.Frameworks.MVVM
{
    /// <summary>
/// Represents the class public.
/// </summary>
public class DetailForm<T>
    {

        public event EventHandler Changed;

        public void OnChanged()
        {
            if (Changed != null)
            {
                Changed(this, EventArgs.Empty);
            }
        }

        private readonly object _form;
        public DetailForm(Type detailForm, EventHandler @event)
        {
            _form = Activator.CreateInstance(detailForm);
            var evChangedInfo = detailForm.GetEvent("Changed");
            var addHandler = evChangedInfo.GetAddMethod();
            Object[] addHandlerArgs = { @event };
            addHandler.Invoke(_form, addHandlerArgs);
        }

        public void Edit(T detailObject)
        {
            _form.GetType().GetMethod("BindData").Invoke(_form, new object[] { detailObject });
            var argtype = new Type[1];
            argtype[0] = _form.GetType();
            var mi = _form.GetType().GetMethod("Show", argtype);
            mi.Invoke(_form, new object[] { null });
        }

        public void Add(T detailObject)
        {
            _form.GetType().GetMethod("BindData").Invoke(_form, new object[] { detailObject });
            var argtype = new Type[1];
            argtype[0] = _form.GetType();
            var mi = _form.GetType().GetMethod("Show", argtype);
            mi.Invoke(_form, new object[] { null });
        }

        public void SaveAdd(T detailObject)
        {
            _form.GetType().GetMethod("BindData").Invoke(_form, new object[] { detailObject });
            _form.GetType().GetMethod("Save").Invoke(_form, null);
        }


    }
}
