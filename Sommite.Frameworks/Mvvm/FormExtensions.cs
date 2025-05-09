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
    public static class FormExtensions
    {
        public static Color ControlEnter(this Control control)
        {
            var retval = control.BackColor;
            control.BackColor = Color.FromArgb(255, 235, 173);
            return retval;
        }

        public static void ControlLeave(this Control control, Color originalBackColor)
        {
            control.BackColor = originalBackColor;
        }
    }
}
