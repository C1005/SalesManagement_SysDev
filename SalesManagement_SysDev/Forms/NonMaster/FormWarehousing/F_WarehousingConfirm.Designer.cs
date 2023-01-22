
namespace SalesManagement_SysDev.Forms.NonMaster.FormWarehousing
{
    partial class F_WarehousingConfirm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_WarehousingConfirm));
            this.buttonConfirm = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label発注ID = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.labelStateFlag = new System.Windows.Forms.Label();
            this.labelFlag = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label入庫年月日 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dateTimePickerWaDate = new System.Windows.Forms.DateTimePicker();
            this.textBoxWaID = new System.Windows.Forms.TextBox();
            this.textBoxHaID = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonList = new System.Windows.Forms.Button();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.labelNoTable = new System.Windows.Forms.Label();
            this.labelPage = new System.Windows.Forms.Label();
            this.dataGridViewWarehousing = new System.Windows.Forms.DataGridView();
            this.textBoxPageNo = new System.Windows.Forms.TextBox();
            this.buttonFirstPage = new System.Windows.Forms.Button();
            this.buttonLastPage = new System.Windows.Forms.Button();
            this.textBoxPageSize = new System.Windows.Forms.TextBox();
            this.buttonNextPage = new System.Windows.Forms.Button();
            this.buttonPageSizeChange = new System.Windows.Forms.Button();
            this.buttonPreviousPage = new System.Windows.Forms.Button();
            this.labelPageSize = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWarehousing)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonConfirm
            // 
            this.buttonConfirm.BackColor = System.Drawing.Color.RoyalBlue;
            this.buttonConfirm.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_受注確定ボタン;
            this.buttonConfirm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonConfirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonConfirm.Font = new System.Drawing.Font("MS UI Gothic", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonConfirm.ForeColor = System.Drawing.Color.White;
            this.buttonConfirm.Location = new System.Drawing.Point(764, 178);
            this.buttonConfirm.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.buttonConfirm.Name = "buttonConfirm";
            this.buttonConfirm.Size = new System.Drawing.Size(169, 83);
            this.buttonConfirm.TabIndex = 4;
            this.buttonConfirm.Text = "入庫確定";
            this.buttonConfirm.UseVisualStyleBackColor = false;
            this.buttonConfirm.Click += new System.EventHandler(this.buttonConfirm_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.Firebrick;
            this.label1.Location = new System.Drawing.Point(41, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 19);
            this.label1.TabIndex = 1413;
            this.label1.Text = "入庫ID";
            // 
            // label発注ID
            // 
            this.label発注ID.AutoSize = true;
            this.label発注ID.BackColor = System.Drawing.Color.Transparent;
            this.label発注ID.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label発注ID.ForeColor = System.Drawing.Color.Black;
            this.label発注ID.Location = new System.Drawing.Point(41, 1);
            this.label発注ID.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label発注ID.Name = "label発注ID";
            this.label発注ID.Size = new System.Drawing.Size(68, 19);
            this.label発注ID.TabIndex = 1389;
            this.label発注ID.Text = "発注ID";
            this.toolTip1.SetToolTip(this.label発注ID, "発注検索");
            this.label発注ID.Click += new System.EventHandler(this.label発注ID_Click);
            this.label発注ID.MouseEnter += new System.EventHandler(this.label発注ID_MouseEnter);
            this.label発注ID.MouseLeave += new System.EventHandler(this.label発注ID_MouseLeave);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.SystemColors.MenuBar;
            this.panel3.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_マスタ以外panel;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.labelStateFlag);
            this.panel3.Controls.Add(this.labelFlag);
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Controls.Add(this.buttonConfirm);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.dateTimePickerWaDate);
            this.panel3.Controls.Add(this.textBoxWaID);
            this.panel3.Controls.Add(this.textBoxHaID);
            this.panel3.Location = new System.Drawing.Point(39, 83);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(947, 272);
            this.panel3.TabIndex = 0;
            // 
            // labelStateFlag
            // 
            this.labelStateFlag.AutoSize = true;
            this.labelStateFlag.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelStateFlag.Location = new System.Drawing.Point(432, 157);
            this.labelStateFlag.Name = "labelStateFlag";
            this.labelStateFlag.Size = new System.Drawing.Size(52, 15);
            this.labelStateFlag.TabIndex = 1461;
            this.labelStateFlag.Text = "確定済";
            this.labelStateFlag.Visible = false;
            // 
            // labelFlag
            // 
            this.labelFlag.AutoSize = true;
            this.labelFlag.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelFlag.Location = new System.Drawing.Point(432, 179);
            this.labelFlag.Name = "labelFlag";
            this.labelFlag.Size = new System.Drawing.Size(52, 15);
            this.labelFlag.TabIndex = 1460;
            this.labelFlag.Text = "非表示";
            this.labelFlag.Visible = false;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.MenuBar;
            this.panel6.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_マスタ用ボタン_閉じる;
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel6.Controls.Add(this.label入庫年月日);
            this.panel6.Location = new System.Drawing.Point(464, 80);
            this.panel6.Margin = new System.Windows.Forms.Padding(4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(166, 26);
            this.panel6.TabIndex = 1430;
            // 
            // label入庫年月日
            // 
            this.label入庫年月日.AutoSize = true;
            this.label入庫年月日.BackColor = System.Drawing.Color.Transparent;
            this.label入庫年月日.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label入庫年月日.ForeColor = System.Drawing.Color.Black;
            this.label入庫年月日.Location = new System.Drawing.Point(23, 1);
            this.label入庫年月日.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label入庫年月日.Name = "label入庫年月日";
            this.label入庫年月日.Size = new System.Drawing.Size(109, 19);
            this.label入庫年月日.TabIndex = 1423;
            this.label入庫年月日.Text = "入庫年月日";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.panel1.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_マスタ用ボタン_閉じる;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(57, 80);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(166, 26);
            this.panel1.TabIndex = 1429;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.MenuBar;
            this.panel4.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_マスタ用ボタン_閉じる;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.label発注ID);
            this.panel4.Location = new System.Drawing.Point(57, 162);
            this.panel4.Margin = new System.Windows.Forms.Padding(4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(166, 26);
            this.panel4.TabIndex = 1428;
            // 
            // dateTimePickerWaDate
            // 
            this.dateTimePickerWaDate.Checked = false;
            this.dateTimePickerWaDate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dateTimePickerWaDate.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.dateTimePickerWaDate.Location = new System.Drawing.Point(637, 80);
            this.dateTimePickerWaDate.Name = "dateTimePickerWaDate";
            this.dateTimePickerWaDate.ShowCheckBox = true;
            this.dateTimePickerWaDate.Size = new System.Drawing.Size(200, 26);
            this.dateTimePickerWaDate.TabIndex = 3;
            // 
            // textBoxWaID
            // 
            this.textBoxWaID.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.textBoxWaID.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBoxWaID.Location = new System.Drawing.Point(228, 80);
            this.textBoxWaID.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.textBoxWaID.MaxLength = 6;
            this.textBoxWaID.Name = "textBoxWaID";
            this.textBoxWaID.Size = new System.Drawing.Size(200, 26);
            this.textBoxWaID.TabIndex = 1;
            // 
            // textBoxHaID
            // 
            this.textBoxHaID.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.textBoxHaID.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBoxHaID.Location = new System.Drawing.Point(228, 162);
            this.textBoxHaID.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.textBoxHaID.MaxLength = 6;
            this.textBoxHaID.Name = "textBoxHaID";
            this.textBoxHaID.Size = new System.Drawing.Size(200, 26);
            this.textBoxHaID.TabIndex = 2;
            this.textBoxHaID.TextChanged += new System.EventHandler(this.textBoxHaID_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label12.Location = new System.Drawing.Point(16, 10);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(133, 30);
            this.label12.TabIndex = 1388;
            this.label12.Text = "入庫確定";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.MenuBar;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel5.Controls.Add(this.label12);
            this.panel5.Location = new System.Drawing.Point(39, 24);
            this.panel5.Margin = new System.Windows.Forms.Padding(4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(177, 54);
            this.panel5.TabIndex = 1418;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.SystemColors.MenuBar;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.buttonClose);
            this.panel2.Controls.Add(this.buttonClear);
            this.panel2.Controls.Add(this.buttonList);
            this.panel2.Controls.Add(this.buttonSearch);
            this.panel2.Location = new System.Drawing.Point(224, 24);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(762, 54);
            this.panel2.TabIndex = 5;
            // 
            // buttonClose
            // 
            this.buttonClose.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_その他;
            this.buttonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonClose.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonClose.Location = new System.Drawing.Point(642, 3);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(103, 45);
            this.buttonClose.TabIndex = 9;
            this.buttonClose.Text = "閉じる";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_その他;
            this.buttonClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonClear.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonClear.Location = new System.Drawing.Point(521, 3);
            this.buttonClear.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(103, 45);
            this.buttonClear.TabIndex = 8;
            this.buttonClear.Text = "入力クリア";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonList
            // 
            this.buttonList.BackColor = System.Drawing.Color.Orchid;
            this.buttonList.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_一覧表示;
            this.buttonList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonList.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonList.Location = new System.Drawing.Point(159, 3);
            this.buttonList.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.buttonList.Name = "buttonList";
            this.buttonList.Size = new System.Drawing.Size(103, 45);
            this.buttonList.TabIndex = 7;
            this.buttonList.Text = "一覧表示";
            this.buttonList.UseVisualStyleBackColor = false;
            this.buttonList.Click += new System.EventHandler(this.buttonList_Click);
            // 
            // buttonSearch
            // 
            this.buttonSearch.BackColor = System.Drawing.Color.PowderBlue;
            this.buttonSearch.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_検索;
            this.buttonSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonSearch.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonSearch.Location = new System.Drawing.Point(38, 3);
            this.buttonSearch.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(103, 45);
            this.buttonSearch.TabIndex = 6;
            this.buttonSearch.Text = "検索";
            this.buttonSearch.UseVisualStyleBackColor = false;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel7.BackColor = System.Drawing.SystemColors.MenuBar;
            this.panel7.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_マスタ以外panel;
            this.panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel7.Controls.Add(this.labelNoTable);
            this.panel7.Controls.Add(this.labelPage);
            this.panel7.Controls.Add(this.dataGridViewWarehousing);
            this.panel7.Controls.Add(this.textBoxPageNo);
            this.panel7.Controls.Add(this.buttonFirstPage);
            this.panel7.Controls.Add(this.buttonLastPage);
            this.panel7.Controls.Add(this.textBoxPageSize);
            this.panel7.Controls.Add(this.buttonNextPage);
            this.panel7.Controls.Add(this.buttonPageSizeChange);
            this.panel7.Controls.Add(this.buttonPreviousPage);
            this.panel7.Controls.Add(this.labelPageSize);
            this.panel7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel7.Location = new System.Drawing.Point(39, 363);
            this.panel7.Margin = new System.Windows.Forms.Padding(4);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(947, 383);
            this.panel7.TabIndex = 10;
            // 
            // labelNoTable
            // 
            this.labelNoTable.AutoSize = true;
            this.labelNoTable.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.labelNoTable.Font = new System.Drawing.Font("MS UI Gothic", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelNoTable.ForeColor = System.Drawing.Color.LightGray;
            this.labelNoTable.Location = new System.Drawing.Point(319, 155);
            this.labelNoTable.Name = "labelNoTable";
            this.labelNoTable.Size = new System.Drawing.Size(278, 43);
            this.labelNoTable.TabIndex = 1433;
            this.labelNoTable.Text = "－ NoTable －";
            this.labelNoTable.Visible = false;
            // 
            // labelPage
            // 
            this.labelPage.AutoSize = true;
            this.labelPage.BackColor = System.Drawing.Color.Transparent;
            this.labelPage.Location = new System.Drawing.Point(679, 352);
            this.labelPage.Name = "labelPage";
            this.labelPage.Size = new System.Drawing.Size(43, 15);
            this.labelPage.TabIndex = 1387;
            this.labelPage.Text = "ページ";
            // 
            // dataGridViewWarehousing
            // 
            this.dataGridViewWarehousing.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewWarehousing.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewWarehousing.Location = new System.Drawing.Point(12, 9);
            this.dataGridViewWarehousing.Name = "dataGridViewWarehousing";
            this.dataGridViewWarehousing.RowHeadersWidth = 51;
            this.dataGridViewWarehousing.RowTemplate.Height = 24;
            this.dataGridViewWarehousing.Size = new System.Drawing.Size(920, 328);
            this.dataGridViewWarehousing.TabIndex = 1367;
            this.dataGridViewWarehousing.TabStop = false;
            this.dataGridViewWarehousing.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewWarehousing_CellClick);
            // 
            // textBoxPageNo
            // 
            this.textBoxPageNo.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBoxPageNo.Location = new System.Drawing.Point(592, 347);
            this.textBoxPageNo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxPageNo.Name = "textBoxPageNo";
            this.textBoxPageNo.Size = new System.Drawing.Size(81, 22);
            this.textBoxPageNo.TabIndex = 13;
            // 
            // buttonFirstPage
            // 
            this.buttonFirstPage.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_その他;
            this.buttonFirstPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonFirstPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonFirstPage.Location = new System.Drawing.Point(770, 344);
            this.buttonFirstPage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonFirstPage.Name = "buttonFirstPage";
            this.buttonFirstPage.Size = new System.Drawing.Size(33, 28);
            this.buttonFirstPage.TabIndex = 14;
            this.buttonFirstPage.Text = "|◀";
            this.buttonFirstPage.UseVisualStyleBackColor = true;
            this.buttonFirstPage.Click += new System.EventHandler(this.buttonFirstPage_Click);
            // 
            // buttonLastPage
            // 
            this.buttonLastPage.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_その他;
            this.buttonLastPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonLastPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonLastPage.Location = new System.Drawing.Point(881, 344);
            this.buttonLastPage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonLastPage.Name = "buttonLastPage";
            this.buttonLastPage.Size = new System.Drawing.Size(33, 28);
            this.buttonLastPage.TabIndex = 17;
            this.buttonLastPage.Text = "▶|";
            this.buttonLastPage.UseVisualStyleBackColor = true;
            this.buttonLastPage.Click += new System.EventHandler(this.buttonLastPage_Click);
            // 
            // textBoxPageSize
            // 
            this.textBoxPageSize.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBoxPageSize.Location = new System.Drawing.Point(113, 347);
            this.textBoxPageSize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxPageSize.Name = "textBoxPageSize";
            this.textBoxPageSize.Size = new System.Drawing.Size(62, 22);
            this.textBoxPageSize.TabIndex = 11;
            // 
            // buttonNextPage
            // 
            this.buttonNextPage.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_その他;
            this.buttonNextPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonNextPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonNextPage.Location = new System.Drawing.Point(844, 344);
            this.buttonNextPage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonNextPage.Name = "buttonNextPage";
            this.buttonNextPage.Size = new System.Drawing.Size(33, 28);
            this.buttonNextPage.TabIndex = 16;
            this.buttonNextPage.Text = "▶";
            this.buttonNextPage.UseVisualStyleBackColor = true;
            this.buttonNextPage.Click += new System.EventHandler(this.buttonNextPage_Click);
            // 
            // buttonPageSizeChange
            // 
            this.buttonPageSizeChange.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_その他;
            this.buttonPageSizeChange.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonPageSizeChange.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonPageSizeChange.Location = new System.Drawing.Point(201, 341);
            this.buttonPageSizeChange.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonPageSizeChange.Name = "buttonPageSizeChange";
            this.buttonPageSizeChange.Size = new System.Drawing.Size(123, 35);
            this.buttonPageSizeChange.TabIndex = 12;
            this.buttonPageSizeChange.Text = "行数変更";
            this.buttonPageSizeChange.UseVisualStyleBackColor = true;
            this.buttonPageSizeChange.Click += new System.EventHandler(this.buttonPageSizeChange_Click);
            // 
            // buttonPreviousPage
            // 
            this.buttonPreviousPage.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_その他;
            this.buttonPreviousPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonPreviousPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonPreviousPage.Location = new System.Drawing.Point(807, 344);
            this.buttonPreviousPage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonPreviousPage.Name = "buttonPreviousPage";
            this.buttonPreviousPage.Size = new System.Drawing.Size(33, 28);
            this.buttonPreviousPage.TabIndex = 15;
            this.buttonPreviousPage.Text = "◀";
            this.buttonPreviousPage.UseVisualStyleBackColor = true;
            this.buttonPreviousPage.Click += new System.EventHandler(this.buttonPreviousPage_Click);
            // 
            // labelPageSize
            // 
            this.labelPageSize.AutoSize = true;
            this.labelPageSize.BackColor = System.Drawing.Color.Transparent;
            this.labelPageSize.Location = new System.Drawing.Point(28, 351);
            this.labelPageSize.Name = "labelPageSize";
            this.labelPageSize.Size = new System.Drawing.Size(81, 15);
            this.labelPageSize.TabIndex = 1379;
            this.labelPageSize.Text = "1ページ行数";
            // 
            // F_WarehousingConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_確定画面類;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1027, 773);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel5);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "F_WarehousingConfirm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "F_WarehousingConfirm";
            this.Activated += new System.EventHandler(this.F_WarehousingConfirm_Activated);
            this.Load += new System.EventHandler(this.F_WarehousingConfirm_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWarehousing)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonConfirm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label発注ID;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox textBoxWaID;
        private System.Windows.Forms.TextBox textBoxHaID;
        private System.Windows.Forms.DateTimePicker dateTimePickerWaDate;
        private System.Windows.Forms.Label label入庫年月日;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonList;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label labelPage;
        private System.Windows.Forms.DataGridView dataGridViewWarehousing;
        private System.Windows.Forms.TextBox textBoxPageNo;
        private System.Windows.Forms.Button buttonFirstPage;
        private System.Windows.Forms.Button buttonLastPage;
        private System.Windows.Forms.TextBox textBoxPageSize;
        private System.Windows.Forms.Button buttonNextPage;
        private System.Windows.Forms.Button buttonPageSizeChange;
        private System.Windows.Forms.Button buttonPreviousPage;
        private System.Windows.Forms.Label labelPageSize;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label labelNoTable;
        private System.Windows.Forms.Label labelStateFlag;
        private System.Windows.Forms.Label labelFlag;
    }
}