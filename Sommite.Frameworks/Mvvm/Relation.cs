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
public class Relation<T>
    {
        public T Parent { get; set; }
        public object Child { get; set; }

        public string ParentForeignKey { get; set; }
        public string ChildPrimaryKey { get; set; }

        public Relation(T parent, object child, string parentForeignKey, string childPrimaryKey)
        {
            Parent = parent;
            Child = child;
            ParentForeignKey = parentForeignKey;
            ChildPrimaryKey = childPrimaryKey;
        }
    }
}
