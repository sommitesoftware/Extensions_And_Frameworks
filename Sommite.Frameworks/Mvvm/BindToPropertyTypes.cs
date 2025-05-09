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
public class BindToPropertyTypes : List<KeyValuePair<string, string>>
    {
        public BindToPropertyTypes()
        {
            Add(new KeyValuePair<string, string>("tx", "Text"));
            Add(new KeyValuePair<string, string>("dt", "DateTime"));
            Add(new KeyValuePair<string, string>("ev", "EditValue"));
            Add(new KeyValuePair<string, string>("lu", "Lookup"));
            Add(new KeyValuePair<string, string>("gc", "DataMember"));
            Add(new KeyValuePair<string, string>("ck", "Checked"));
            Add(new KeyValuePair<string, string>("me", "Text"));
            Add(new KeyValuePair<string, string>("ce", "Value"));
            Add(new KeyValuePair<string, string>("im", "Image"));
            
        }
    }
}
