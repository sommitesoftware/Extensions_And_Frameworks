namespace Sommite.Frameworks.MVVM
{
    partial class ManageLookup<T>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageLookup<T>));
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.rilueEntityType = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colOrder = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDocumentDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKey = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.GridControl = new DevExpress.XtraGrid.GridControl();
            this.listBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bbiDeleteCurrent = new DevExpress.XtraBars.BarButtonItem();
            this.bbiSave = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.ic16x16 = new DevExpress.Utils.ImageCollection(this.components);
            this.repositoryItemMemoExEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rilueEntityType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ic16x16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.LinesCount = 3;
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // rilueEntityType
            // 
            this.rilueEntityType.AutoHeight = false;
            this.rilueEntityType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rilueEntityType.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.rilueEntityType.DisplayMember = "DisplayName";
            this.rilueEntityType.Name = "rilueEntityType";
            this.rilueEntityType.ValueMember = "ID";
            // 
            // colOrder
            // 
            this.colOrder.Caption = "Volgorde";
            this.colOrder.FieldName = "Order";
            this.colOrder.Name = "colOrder";
            this.colOrder.OptionsColumn.FixedWidth = true;
            this.colOrder.Visible = true;
            this.colOrder.VisibleIndex = 2;
            // 
            // colDocumentDescription
            // 
            this.colDocumentDescription.Caption = "Omschrijving";
            this.colDocumentDescription.FieldName = "DocumentDescription";
            this.colDocumentDescription.Name = "colDocumentDescription";
            this.colDocumentDescription.Visible = true;
            this.colDocumentDescription.VisibleIndex = 1;
            this.colDocumentDescription.Width = 530;
            // 
            // colKey
            // 
            this.colKey.Caption = "Naam";
            this.colKey.FieldName = "Name";
            this.colKey.Name = "colKey";
            this.colKey.OptionsColumn.FixedWidth = true;
            this.colKey.Visible = true;
            this.colKey.VisibleIndex = 0;
            this.colKey.Width = 132;
            // 
            // gvList
            // 
            this.gvList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colKey,
            this.colDocumentDescription,
            this.colOrder});
            this.gvList.GridControl = this.GridControl;
            this.gvList.Name = "gvList";
            this.gvList.OptionsDetail.ShowDetailTabs = false;
            this.gvList.OptionsDetail.SmartDetailExpand = false;
            this.gvList.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gvList.OptionsView.ShowDetailButtons = false;
            this.gvList.OptionsView.ShowGroupPanel = false;
            this.gvList.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gvList_ValidateRow);
            // 
            // GridControl
            // 
            this.GridControl.Cursor = System.Windows.Forms.Cursors.Default;
            this.GridControl.DataSource = this.listBindingSource;
            this.GridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridControl.Location = new System.Drawing.Point(0, 31);
            this.GridControl.MainView = this.gvList;
            this.GridControl.MenuManager = this.barManager1;
            this.GridControl.Name = "GridControl";
            this.GridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rilueEntityType,
            this.repositoryItemMemoExEdit1,
            this.repositoryItemMemoEdit1});
            this.GridControl.Size = new System.Drawing.Size(877, 404);
            this.GridControl.TabIndex = 7;
            this.GridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvList});
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Images = this.ic16x16;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbiSave,
            this.bbiDeleteCurrent});
            this.barManager1.MaxItemId = 2;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiDeleteCurrent),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiSave)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // bbiDeleteCurrent
            // 
            this.bbiDeleteCurrent.Caption = "Verwijderen";
            this.bbiDeleteCurrent.Id = 1;
            this.bbiDeleteCurrent.ImageIndex = 1;
            this.bbiDeleteCurrent.Name = "bbiDeleteCurrent";
            this.bbiDeleteCurrent.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiDeleteCurrent_ItemClick);
            // 
            // bbiSave
            // 
            this.bbiSave.Caption = "Opslaan";
            this.bbiSave.Id = 0;
            this.bbiSave.ImageIndex = 0;
            this.bbiSave.Name = "bbiSave";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(877, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 435);
            this.barDockControlBottom.Size = new System.Drawing.Size(877, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 404);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(877, 31);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 404);
            // 
            // ic16x16
            // 
            //this.ic16x16.Images.SetKeyName(0, "save-16.png");
            //this.ic16x16.Images.SetKeyName(1, "x-mark-3-16.png");
            //this.ic16x16.Images.SetKeyName(2, "add-icon.png");
            //this.ic16x16.Images.SetKeyName(3, "arrow_refresh.png");
            //this.ic16x16.Images.SetKeyName(4, "textfield_key-16.png");
            // 
            // repositoryItemMemoExEdit1
            // 
            this.repositoryItemMemoExEdit1.AutoHeight = false;
            this.repositoryItemMemoExEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemMemoExEdit1.Name = "repositoryItemMemoExEdit1";
            this.repositoryItemMemoExEdit1.PopupFormMinSize = new System.Drawing.Size(450, 450);
            this.repositoryItemMemoExEdit1.ShowIcon = false;
            // 
            // ManageLookup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GridControl);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ManageLookup";
            this.Size = new System.Drawing.Size(877, 435);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rilueEntityType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ic16x16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rilueEntityType;
        private DevExpress.XtraGrid.Columns.GridColumn colOrder;
        private DevExpress.XtraGrid.Columns.GridColumn colDocumentDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colKey;
        private DevExpress.XtraGrid.Views.Grid.GridView gvList;
        private DevExpress.XtraGrid.GridControl GridControl;
        private System.Windows.Forms.BindingSource listBindingSource;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem bbiDeleteCurrent;
        private DevExpress.XtraBars.BarButtonItem bbiSave;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.Utils.ImageCollection ic16x16;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit1;

    }
}
