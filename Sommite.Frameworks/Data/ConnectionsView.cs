using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sommite.Frameworks.DX.Helpers;


namespace Sommite.Frameworks.Data
{
    public partial class ConnectionsView : DevExpress.XtraEditors.XtraUserControl
    {
        public ConnectionsView(List<ConnectionsViewItem> list)
        {
            InitializeComponent();
            GridView.Initialize(ref gv);
            GridView.Initialize(ref gv, GridViewPropertySet.ReadOnly);
            GridView.Initialize(ref gv, GridViewPropertySet.Minimalistic);
            SetData(list);
        }

        public void SetData(List<ConnectionsViewItem> list)
        {
            connectionsViewItemBindingSource.DataSource = list;
            connectionsViewItemBindingSource.ResetBindings(false);
        }
    }
}
