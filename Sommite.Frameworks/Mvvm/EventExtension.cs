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
    public static class EventExtension
    {
        public static void Raise<TEventArgs>(this object source, string eventName,
          TEventArgs eventArgs) where TEventArgs : EventArgs
        {
            var eventInfo = source.GetType().GetEvent(eventName,
                      BindingFlags.Public | BindingFlags.NonPublic
                    | BindingFlags.Instance);
            var fieldInfo = source.GetType()
                .GetField(eventName, BindingFlags.Instance
                                     | BindingFlags.NonPublic);
            if (fieldInfo != null)
            {
                var eventDelegate = (MulticastDelegate)fieldInfo.GetValue(source);
                if (eventDelegate != null)
                {
                    foreach (var handler in eventDelegate.GetInvocationList())
                    {
                        handler.Method.Invoke(handler.Target,
                            new object[] { source, eventArgs });
                    }
                }
            }
        }
    }
}
