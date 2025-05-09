namespace Sommite.Frameworks.Data
{
    partial class ConnectionsView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gc = new DevExpress.XtraGrid.GridControl();
            this.gv = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gc1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.connectionsViewItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.connectionsViewItemBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // gc
            // 
            this.gc.DataSource = this.connectionsViewItemBindingSource;
            this.gc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc.Location = new System.Drawing.Point(0, 0);
            this.gc.MainView = this.gv;
            this.gc.Name = "gc";
            this.gc.Size = new System.Drawing.Size(620, 232);
            this.gc.TabIndex = 0;
            this.gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv});
            // 
            // gv
            // 
            this.gv.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gc1,
            this.gc2,
            this.gc3});
            this.gv.GridControl = this.gc;
            this.gv.Name = "gv";
            // 
            // gc1
            // 
            this.gc1.Caption = "Prestatiemeter Item";
            this.gc1.FieldName = "Name";
            this.gc1.Name = "gc1";
            this.gc1.Visible = true;
            this.gc1.VisibleIndex = 0;
            // 
            // gc2
            // 
            this.gc2.Caption = "Waarde";
            this.gc2.FieldName = "Value";
            this.gc2.Name = "gc2";
            this.gc2.Visible = true;
            this.gc2.VisibleIndex = 1;
            // 
            // gc3
            // 
            this.gc3.Name = "gc3";
            this.gc3.Visible = true;
            this.gc3.VisibleIndex = 2;
            // 
            // connectionsViewItemBindingSource
            // 
            this.connectionsViewItemBindingSource.DataSource = typeof(Sommite.Frameworks.Data.ConnectionsViewItem);
            // 
            // ConnectionsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gc);
            this.Name = "ConnectionsView";
            this.Size = new System.Drawing.Size(620, 232);
            ((System.ComponentModel.ISupportInitialize)(this.gc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.connectionsViewItemBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gc;
        private DevExpress.XtraGrid.Views.Grid.GridView gv;
        private DevExpress.XtraGrid.Columns.GridColumn gc1;
        private DevExpress.XtraGrid.Columns.GridColumn gc2;
        private DevExpress.XtraGrid.Columns.GridColumn gc3;
        private System.Windows.Forms.BindingSource connectionsViewItemBindingSource;
    }
}
