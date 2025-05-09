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
public class BindingLookups<T> : List<BindingLookup<T>>
    {
        public BindingLookups()
        {

        }
        public BindingLookups(int capacity)
            : base(capacity)
        {

        }
        public BindingLookups(IEnumerable<BindingLookup<T>> collection)
            : base(collection)
        {

        }
        public List<T> GetBindingLookup(string listName)
        {
            try
            {

                var lookup = this.SingleOrDefault(x => x.ListName == listName);

                if (lookup != null)
                {
                    return lookup.List;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
