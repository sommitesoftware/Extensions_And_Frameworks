using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sommite.Extensions;
using XtraGrid = DevExpress.XtraGrid.Views;

namespace Sommite.Frameworks.DX.Helpers
{
    public enum GridViewPropertySet
    {
        @Default = 0,
        Minimalistic = 1,
        GroupBy = 2,
        AddNew = 3,
        @ReadOnly = 4,
        NoFilter = 5,
        NoLines = 6,
        MultiSelect = 7,
        DoubleClick = 8,
    }

    /// <summary>
/// Represents the class public.
/// </summary>
public class GridView
    {

        public static void Initialize(ref XtraGrid.Grid.GridView gridView,
            GridViewPropertySet propertySet = GridViewPropertySet.Default,EventHandler doubleClick=null)
        {
            gridView.With(g =>
            {
                switch (propertySet)
                {
                    case GridViewPropertySet.Default:
                        g.OptionsView.ShowGroupPanel = false;
                        g.OptionsView.ShowAutoFilterRow = true;
                        g.OptionsView.ShowDetailButtons = false;
                        g.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
                        break;
                    case GridViewPropertySet.AddNew:
                        g.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
                        g.OptionsView.ShowAutoFilterRow = false;
                        break;
                    case GridViewPropertySet.Minimalistic:
                        g.OptionsView.ShowIndicator = false;
                        break;
                    case GridViewPropertySet.ReadOnly:
                        g.OptionsBehavior.ReadOnly = true;
                        g.OptionsBehavior.Editable = false;
                        break;
                    case GridViewPropertySet.NoFilter:
                        g.OptionsView.ShowAutoFilterRow = false;
                        break;
                    case GridViewPropertySet.NoLines:
                        g.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
                        g.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
                        break;
                    case GridViewPropertySet.MultiSelect:
                        g.OptionsSelection.MultiSelectMode =
                            DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                        g.OptionsSelection.MultiSelect = true;
                        g.OptionsSelection.UseIndicatorForSelection = g.OptionsView.ShowIndicator;
                        break;
                    case GridViewPropertySet.DoubleClick:
                        if (doubleClick == null) break;
                        g.OptionsDetail.SmartDetailExpand = false;
                        g.OptionsDetail.EnableMasterViewMode = false;
                        g.OptionsBehavior.Editable = false;
                        g.DoubleClick += doubleClick;  
                        break;
                    default:
                        break;
                }
            });
        }

        public static T Runner<T>(Func<T> funcToRun)
        {
            //Do stuff before running function as normal
            return funcToRun();
        }

    }

    /// <summary>
/// Represents the class public.
/// </summary>
public class DoubleClickEventArgs : EventArgs
    {
        public Delegate @delegate {get; set; }
    }
}
