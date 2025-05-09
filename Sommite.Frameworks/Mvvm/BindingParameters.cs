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
public class BindingParameters
    {
        public BindingParameters(object json)
        {
            BindJson(json);

        }

        private void BindJson(object json)
        {
            if (json == null) return;
            if (json.ToString().IsEmpty()) return;
            try
            {
                var jss = new JavaScriptSerializer();
                var dict = jss.Deserialize<Dictionary<string, string>>(json.ToString());

                foreach (var val in dict)
                {
                    var propInfo = this.GetType().GetProperties();
                    foreach (var prop in propInfo)
                    {
                        if (prop.Name == val.Key) prop.SetValue(this, val.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                var err = ex.ToString();
            }
        }

        public BindingParameters(Control ctrl)
        {
            if (ctrl.Name.IsEmpty()) return;
            var btp = new BindToPropertyTypes().SingleOrDefault(x => x.Key == ctrl.Name.Substring(0, 2));
            if (!btp.Equals(default(KeyValuePair<string, string>)))
            {
                BindToProperty = btp.Value;
                ValueField = ctrl.Name.Substring(2, ctrl.Name.Length - 2);
            }
            BindJson(ctrl.Tag);
        }

        public BindingParameters()
        {

        }

        public string BindToProperty { get; set; }
        public string Type { get; set; }
        public string ValueField { get; set; }
        public string DisplayField { get; set; }
        public string LookupField { get; set; }
        public string ListName { get; set; }
        public string DataMember { get; set; }
    }
}
