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
    /// The PaddingMode specifies wether or not an empty, non-data-bound column is shown with Fixed-Wdith set to false
    /// </summary>
    public enum GridColumnPaddingMode
    {
        None = 0,
        Left = 1,
        Right = 2,
    }
}
