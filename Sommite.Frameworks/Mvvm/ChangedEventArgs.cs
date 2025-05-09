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
using System.Reflection;
using System.Drawing;
using System.Web.Script.Serialization;

namespace Sommite.Frameworks.Shared
{
    /// <summary>
/// Represents the class public.
/// </summary>
public class ChangedEventArgs : EventArgs
    {
        public object objectId { get; set; }

        public ChangedEventArgs()
        {
            IsNew = false;
        }

        public bool IsNew { get; set; }
    }
}
