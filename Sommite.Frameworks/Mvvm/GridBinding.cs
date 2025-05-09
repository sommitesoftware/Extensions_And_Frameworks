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
public class GridBinding : IDisposable
    {
        public Control GridControl { get; set; }
        public List<GridColumn> Columns { get; set; }
        public GridColumnPaddingMode PaddingMode { get; set; }

        public void Dispose()
        {
            
        }
        public GridBinding()
        {
            Columns = new List<GridColumn>();
            PaddingMode = GridColumnPaddingMode.None;
        }

        public GridBinding(GridColumnPaddingMode paddingMode)
        {
            Columns = new List<GridColumn>();
            PaddingMode = paddingMode;
        }


        void IDisposable.Dispose()
        {
            if (GridControl != null)
            {
                GridControl.Dispose();
                GridControl = null;
            }
        }
    }
}
