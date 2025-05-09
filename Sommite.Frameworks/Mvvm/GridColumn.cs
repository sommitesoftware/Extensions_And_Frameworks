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
public class GridColumn : IDisposable
    {
        public void Dispose()
        {
            if (ColumnEdit != null)
            {
                ColumnEdit.Dispose();
                ColumnEdit = null;
            }
        }
        public string ColumnName { get; set; }
        public int Width { get; set; }
        public bool FixedWidth { get; set; }
        public Control ColumnEdit { get; set; }
        public string FieldName { get; set; }
    }
}
