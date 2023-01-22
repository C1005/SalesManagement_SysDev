
namespace SalesManagement_SysDev.Forms.Master.FormStock
{
    partial class F_Stock
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_Stock));
            this.panel5 = new System.Windows.Forms.Panel();
            this.label受注管理 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label在庫数 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label在庫ID = new System.Windows.Forms.Label();
            this.labelPrName = new System.Windows.Forms.Label();
            this.textBoxPrID = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label商品ID = new System.Windows.Forms.Label();
            this.textBoxStID = new System.Windows.Forms.TextBox();
            this.textBoxStQuantity = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label営業所名 = new System.Windows.Forms.Label();
            this.labelEmpName = new System.Windows.Forms.Label();
            this.label社員名 = new System.Windows.Forms.Label();
            this.label社員ID = new System.Windows.Forms.Label();
            this.labelOfficeName = new System.Windows.Forms.Label();
            this.labelEmpID = new System.Windows.Forms.Label();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonList = new System.Windows.Forms.Button();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.buttonPreviousPage = new System.Windows.Forms.Button();
            this.labelPageSize = new System.Windows.Forms.Label();
            this.checkBoxStFlag = new System.Windows.Forms.CheckBox();
            this.labelPage = new System.Windows.Forms.Label();
            this.dataGridViewStock = new System.Windows.Forms.DataGridView();
            this.textBoxPageNo = new System.Windows.Forms.TextBox();
            this.buttonFirstPage = new System.Windows.Forms.Button();
            this.buttonLastPage = new System.Windows.Forms.Button();
            this.textBoxPageSize = new System.Windows.Forms.TextBox();
            this.buttonNextPage = new System.Windows.Forms.Button();
            this.buttonPageSizeChange = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.labelNoTable = new System.Windows.Forms.Label();
            this.panel15 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxStHidden = new System.Windows.Forms.TextBox();
            this.panel17 = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStock)).BeginInit();
            this.panel7.SuspendLayout();
            this.panel15.SuspendLayout();
            this.panel17.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.MenuBar;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel5.Controls.Add(this.label受注管理);
            this.panel5.Location = new System.Drawing.Point(39, 24);
            this.panel5.Margin = new System.Windows.Forms.Padding(4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(177, 54);
            this.panel5.TabIndex = 1372;
            // 
            // label受注管理
            // 
            this.label受注管理.AutoSize = true;
            this.label受注管理.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label受注管理.Location = new System.Drawing.Point(16, 10);
            this.label受注管理.Name = "label受注管理";
            this.label受注管理.Size = new System.Drawing.Size(133, 30);
            this.label受注管理.TabIndex = 1388;
            this.label受注管理.Text = "在庫管理";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.MenuBar;
            this.panel3.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_マスタ以外panel;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.labelPrName);
            this.panel3.Controls.Add(this.textBoxPrID);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.textBoxStID);
            this.panel3.Controls.Add(this.textBoxStQuantity);
            this.panel3.Location = new System.Drawing.Point(39, 83);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1940, 272);
            this.panel3.TabIndex = 0;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.MenuBar;
            this.panel6.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_マスタ用ボタン_閉じる;
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel6.Controls.Add(this.label在庫数);
            this.panel6.Location = new System.Drawing.Point(884, 123);
            this.panel6.Margin = new System.Windows.Forms.Padding(4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(166, 26);
            this.panel6.TabIndex = 1443;
            // 
            // label在庫数
            // 
            this.label在庫数.AutoSize = true;
            this.label在庫数.BackColor = System.Drawing.Color.Transparent;
            this.label在庫数.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, System.Drawing.FontStyle.Bold);
            this.label在庫数.ForeColor = System.Drawing.Color.Black;
            this.label在庫数.Location = new System.Drawing.Point(41, 1);
            this.label在庫数.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label在庫数.Name = "label在庫数";
            this.label在庫数.Size = new System.Drawing.Size(69, 19);
            this.label在庫数.TabIndex = 1390;
            this.label在庫数.Text = "在庫数";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.MenuBar;
            this.panel4.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_マスタ用ボタン_閉じる;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.label在庫ID);
            this.panel4.Location = new System.Drawing.Point(472, 123);
            this.panel4.Margin = new System.Windows.Forms.Padding(4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(166, 26);
            this.panel4.TabIndex = 1443;
            // 
            // label在庫ID
            // 
            this.label在庫ID.AutoSize = true;
            this.label在庫ID.BackColor = System.Drawing.Color.Transparent;
            this.label在庫ID.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label在庫ID.ForeColor = System.Drawing.Color.Firebrick;
            this.label在庫ID.Location = new System.Drawing.Point(41, 1);
            this.label在庫ID.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label在庫ID.Name = "label在庫ID";
            this.label在庫ID.Size = new System.Drawing.Size(68, 19);
            this.label在庫ID.TabIndex = 1356;
            this.label在庫ID.Text = "在庫ID";
            // 
            // labelPrName
            // 
            this.labelPrName.AutoSize = true;
            this.labelPrName.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelPrName.Location = new System.Drawing.Point(231, 150);
            this.labelPrName.Name = "labelPrName";
            this.labelPrName.Size = new System.Drawing.Size(52, 15);
            this.labelPrName.TabIndex = 1438;
            this.labelPrName.Text = "商品名";
            this.labelPrName.Visible = false;
            // 
            // textBoxPrID
            // 
            this.textBoxPrID.Location = new System.Drawing.Point(234, 124);
            this.textBoxPrID.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxPrID.MaxLength = 6;
            this.textBoxPrID.Name = "textBoxPrID";
            this.textBoxPrID.Size = new System.Drawing.Size(200, 22);
            this.textBoxPrID.TabIndex = 1;
            this.textBoxPrID.TextChanged += new System.EventHandler(this.textBoxPrID_TextChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.panel1.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_マスタ用ボタン_閉じる;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label商品ID);
            this.panel1.Location = new System.Drawing.Point(60, 123);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(166, 26);
            this.panel1.TabIndex = 1442;
            // 
            // label商品ID
            // 
            this.label商品ID.AutoSize = true;
            this.label商品ID.BackColor = System.Drawing.Color.Transparent;
            this.label商品ID.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label商品ID.ForeColor = System.Drawing.Color.Black;
            this.label商品ID.Location = new System.Drawing.Point(41, 1);
            this.label商品ID.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label商品ID.Name = "label商品ID";
            this.label商品ID.Size = new System.Drawing.Size(68, 19);
            this.label商品ID.TabIndex = 1389;
            this.label商品ID.Text = "商品ID";
            this.toolTip1.SetToolTip(this.label商品ID, "商品検索");
            this.label商品ID.Click += new System.EventHandler(this.label商品ID_Click);
            this.label商品ID.MouseEnter += new System.EventHandler(this.label商品ID_MouseEnter);
            this.label商品ID.MouseLeave += new System.EventHandler(this.label商品ID_MouseLeave);
            // 
            // textBoxStID
            // 
            this.textBoxStID.Location = new System.Drawing.Point(646, 124);
            this.textBoxStID.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxStID.MaxLength = 6;
            this.textBoxStID.Name = "textBoxStID";
            this.textBoxStID.Size = new System.Drawing.Size(200, 22);
            this.textBoxStID.TabIndex = 2;
            // 
            // textBoxStQuantity
            // 
            this.textBoxStQuantity.Location = new System.Drawing.Point(1058, 124);
            this.textBoxStQuantity.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxStQuantity.MaxLength = 4;
            this.textBoxStQuantity.Name = "textBoxStQuantity";
            this.textBoxStQuantity.Size = new System.Drawing.Size(200, 22);
            this.textBoxStQuantity.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.MenuBar;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.panel8);
            this.panel2.Controls.Add(this.buttonUpdate);
            this.panel2.Controls.Add(this.buttonClose);
            this.panel2.Controls.Add(this.buttonClear);
            this.panel2.Controls.Add(this.buttonList);
            this.panel2.Controls.Add(this.buttonSearch);
            this.panel2.Location = new System.Drawing.Point(224, 24);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1755, 54);
            this.panel2.TabIndex = 8;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.SystemColors.MenuBar;
            this.panel8.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_確定panel;
            this.panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel8.Controls.Add(this.label営業所名);
            this.panel8.Controls.Add(this.labelEmpName);
            this.panel8.Controls.Add(this.label社員名);
            this.panel8.Controls.Add(this.label社員ID);
            this.panel8.Controls.Add(this.labelOfficeName);
            this.panel8.Controls.Add(this.labelEmpID);
            this.panel8.Location = new System.Drawing.Point(892, 4);
            this.panel8.Margin = new System.Windows.Forms.Padding(4);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(693, 44);
            this.panel8.TabIndex = 1407;
            // 
            // label営業所名
            // 
            this.label営業所名.AutoSize = true;
            this.label営業所名.BackColor = System.Drawing.Color.Transparent;
            this.label営業所名.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label営業所名.Location = new System.Drawing.Point(4, 20);
            this.label営業所名.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label営業所名.Name = "label営業所名";
            this.label営業所名.Size = new System.Drawing.Size(100, 19);
            this.label営業所名.TabIndex = 1391;
            this.label営業所名.Text = "営業所名：";
            // 
            // labelEmpName
            // 
            this.labelEmpName.AutoSize = true;
            this.labelEmpName.BackColor = System.Drawing.Color.Transparent;
            this.labelEmpName.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelEmpName.Location = new System.Drawing.Point(534, 20);
            this.labelEmpName.Name = "labelEmpName";
            this.labelEmpName.Size = new System.Drawing.Size(85, 19);
            this.labelEmpName.TabIndex = 1396;
            this.labelEmpName.Text = "池田翔大";
            // 
            // label社員名
            // 
            this.label社員名.AutoSize = true;
            this.label社員名.BackColor = System.Drawing.Color.Transparent;
            this.label社員名.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label社員名.Location = new System.Drawing.Point(447, 20);
            this.label社員名.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label社員名.Name = "label社員名";
            this.label社員名.Size = new System.Drawing.Size(80, 19);
            this.label社員名.TabIndex = 1397;
            this.label社員名.Text = "社員名：";
            // 
            // label社員ID
            // 
            this.label社員ID.AutoSize = true;
            this.label社員ID.BackColor = System.Drawing.Color.Transparent;
            this.label社員ID.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label社員ID.Location = new System.Drawing.Point(264, 20);
            this.label社員ID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label社員ID.Name = "label社員ID";
            this.label社員ID.Size = new System.Drawing.Size(79, 19);
            this.label社員ID.TabIndex = 1395;
            this.label社員ID.Text = "社員ID：";
            // 
            // labelOfficeName
            // 
            this.labelOfficeName.AutoSize = true;
            this.labelOfficeName.BackColor = System.Drawing.Color.Transparent;
            this.labelOfficeName.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelOfficeName.Location = new System.Drawing.Point(110, 20);
            this.labelOfficeName.Name = "labelOfficeName";
            this.labelOfficeName.Size = new System.Drawing.Size(119, 19);
            this.labelOfficeName.TabIndex = 1392;
            this.labelOfficeName.Text = "ＯＩＣ専門学校";
            // 
            // labelEmpID
            // 
            this.labelEmpID.AutoSize = true;
            this.labelEmpID.BackColor = System.Drawing.Color.Transparent;
            this.labelEmpID.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelEmpID.Location = new System.Drawing.Point(350, 20);
            this.labelEmpID.Name = "labelEmpID";
            this.labelEmpID.Size = new System.Drawing.Size(69, 19);
            this.labelEmpID.TabIndex = 1394;
            this.labelEmpID.Text = "111111";
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.BackColor = System.Drawing.Color.Gold;
            this.buttonUpdate.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_更新;
            this.buttonUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonUpdate.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonUpdate.Location = new System.Drawing.Point(274, 3);
            this.buttonUpdate.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(103, 45);
            this.buttonUpdate.TabIndex = 10;
            this.buttonUpdate.Text = "更新";
            this.buttonUpdate.UseVisualStyleBackColor = false;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_その他;
            this.buttonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonClose.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonClose.Location = new System.Drawing.Point(1617, 5);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(103, 45);
            this.buttonClose.TabIndex = 13;
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
            this.buttonClear.Location = new System.Drawing.Point(510, 4);
            this.buttonClear.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(103, 45);
            this.buttonClear.TabIndex = 12;
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
            this.buttonList.Location = new System.Drawing.Point(392, 3);
            this.buttonList.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.buttonList.Name = "buttonList";
            this.buttonList.Size = new System.Drawing.Size(103, 45);
            this.buttonList.TabIndex = 11;
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
            this.buttonSearch.TabIndex = 9;
            this.buttonSearch.Text = "検索";
            this.buttonSearch.UseVisualStyleBackColor = false;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // buttonPreviousPage
            // 
            this.buttonPreviousPage.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_その他;
            this.buttonPreviousPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonPreviousPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonPreviousPage.Location = new System.Drawing.Point(1778, 570);
            this.buttonPreviousPage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonPreviousPage.Name = "buttonPreviousPage";
            this.buttonPreviousPage.Size = new System.Drawing.Size(33, 28);
            this.buttonPreviousPage.TabIndex = 18;
            this.buttonPreviousPage.Text = "◀";
            this.buttonPreviousPage.UseVisualStyleBackColor = true;
            this.buttonPreviousPage.Click += new System.EventHandler(this.buttonPreviousPage_Click);
            // 
            // labelPageSize
            // 
            this.labelPageSize.AutoSize = true;
            this.labelPageSize.BackColor = System.Drawing.Color.Transparent;
            this.labelPageSize.Location = new System.Drawing.Point(28, 578);
            this.labelPageSize.Name = "labelPageSize";
            this.labelPageSize.Size = new System.Drawing.Size(81, 15);
            this.labelPageSize.TabIndex = 1379;
            this.labelPageSize.Text = "1ページ行数";
            // 
            // checkBoxStFlag
            // 
            this.checkBoxStFlag.AutoSize = true;
            this.checkBoxStFlag.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxStFlag.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBoxStFlag.Location = new System.Drawing.Point(10, 16);
            this.checkBoxStFlag.Name = "checkBoxStFlag";
            this.checkBoxStFlag.Size = new System.Drawing.Size(121, 19);
            this.checkBoxStFlag.TabIndex = 6;
            this.checkBoxStFlag.Text = "在庫管理フラグ";
            this.checkBoxStFlag.UseVisualStyleBackColor = false;
            this.checkBoxStFlag.CheckedChanged += new System.EventHandler(this.checkBoxStFlag_CheckedChanged);
            // 
            // labelPage
            // 
            this.labelPage.AutoSize = true;
            this.labelPage.BackColor = System.Drawing.Color.Transparent;
            this.labelPage.Location = new System.Drawing.Point(1650, 578);
            this.labelPage.Name = "labelPage";
            this.labelPage.Size = new System.Drawing.Size(43, 15);
            this.labelPage.TabIndex = 1387;
            this.labelPage.Text = "ページ";
            // 
            // dataGridViewStock
            // 
            this.dataGridViewStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewStock.Location = new System.Drawing.Point(31, 84);
            this.dataGridViewStock.Name = "dataGridViewStock";
            this.dataGridViewStock.RowHeadersWidth = 51;
            this.dataGridViewStock.RowTemplate.Height = 24;
            this.dataGridViewStock.Size = new System.Drawing.Size(1874, 478);
            this.dataGridViewStock.TabIndex = 21;
            this.dataGridViewStock.TabStop = false;
            this.dataGridViewStock.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewStock_CellClick);
            // 
            // textBoxPageNo
            // 
            this.textBoxPageNo.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBoxPageNo.Location = new System.Drawing.Point(1563, 573);
            this.textBoxPageNo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxPageNo.Name = "textBoxPageNo";
            this.textBoxPageNo.Size = new System.Drawing.Size(81, 22);
            this.textBoxPageNo.TabIndex = 16;
            // 
            // buttonFirstPage
            // 
            this.buttonFirstPage.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_その他;
            this.buttonFirstPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonFirstPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonFirstPage.Location = new System.Drawing.Point(1741, 570);
            this.buttonFirstPage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonFirstPage.Name = "buttonFirstPage";
            this.buttonFirstPage.Size = new System.Drawing.Size(33, 28);
            this.buttonFirstPage.TabIndex = 17;
            this.buttonFirstPage.Text = "|◀";
            this.buttonFirstPage.UseVisualStyleBackColor = true;
            this.buttonFirstPage.Click += new System.EventHandler(this.buttonFirstPage_Click);
            // 
            // buttonLastPage
            // 
            this.buttonLastPage.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_その他;
            this.buttonLastPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonLastPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonLastPage.Location = new System.Drawing.Point(1852, 570);
            this.buttonLastPage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonLastPage.Name = "buttonLastPage";
            this.buttonLastPage.Size = new System.Drawing.Size(33, 28);
            this.buttonLastPage.TabIndex = 20;
            this.buttonLastPage.Text = "▶|";
            this.buttonLastPage.UseVisualStyleBackColor = true;
            this.buttonLastPage.Click += new System.EventHandler(this.buttonLastPage_Click);
            // 
            // textBoxPageSize
            // 
            this.textBoxPageSize.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBoxPageSize.Location = new System.Drawing.Point(113, 574);
            this.textBoxPageSize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxPageSize.Name = "textBoxPageSize";
            this.textBoxPageSize.Size = new System.Drawing.Size(62, 22);
            this.textBoxPageSize.TabIndex = 14;
            // 
            // buttonNextPage
            // 
            this.buttonNextPage.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_その他;
            this.buttonNextPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonNextPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonNextPage.Location = new System.Drawing.Point(1815, 570);
            this.buttonNextPage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonNextPage.Name = "buttonNextPage";
            this.buttonNextPage.Size = new System.Drawing.Size(33, 28);
            this.buttonNextPage.TabIndex = 19;
            this.buttonNextPage.Text = "▶";
            this.buttonNextPage.UseVisualStyleBackColor = true;
            this.buttonNextPage.Click += new System.EventHandler(this.buttonNextPage_Click);
            // 
            // buttonPageSizeChange
            // 
            this.buttonPageSizeChange.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_その他;
            this.buttonPageSizeChange.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonPageSizeChange.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonPageSizeChange.Location = new System.Drawing.Point(201, 568);
            this.buttonPageSizeChange.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonPageSizeChange.Name = "buttonPageSizeChange";
            this.buttonPageSizeChange.Size = new System.Drawing.Size(123, 35);
            this.buttonPageSizeChange.TabIndex = 15;
            this.buttonPageSizeChange.Text = "行数変更";
            this.buttonPageSizeChange.UseVisualStyleBackColor = true;
            this.buttonPageSizeChange.Click += new System.EventHandler(this.buttonPageSizeChange_Click);
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.SystemColors.MenuBar;
            this.panel7.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_マスタ以外panel;
            this.panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel7.Controls.Add(this.labelNoTable);
            this.panel7.Controls.Add(this.panel15);
            this.panel7.Controls.Add(this.textBoxStHidden);
            this.panel7.Controls.Add(this.labelPage);
            this.panel7.Controls.Add(this.dataGridViewStock);
            this.panel7.Controls.Add(this.panel17);
            this.panel7.Controls.Add(this.textBoxPageNo);
            this.panel7.Controls.Add(this.buttonFirstPage);
            this.panel7.Controls.Add(this.buttonLastPage);
            this.panel7.Controls.Add(this.textBoxPageSize);
            this.panel7.Controls.Add(this.buttonNextPage);
            this.panel7.Controls.Add(this.buttonPageSizeChange);
            this.panel7.Controls.Add(this.buttonPreviousPage);
            this.panel7.Controls.Add(this.labelPageSize);
            this.panel7.Location = new System.Drawing.Point(39, 363);
            this.panel7.Margin = new System.Windows.Forms.Padding(4);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1940, 617);
            this.panel7.TabIndex = 4;
            // 
            // labelNoTable
            // 
            this.labelNoTable.AutoSize = true;
            this.labelNoTable.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.labelNoTable.Font = new System.Drawing.Font("MS UI Gothic", 31.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelNoTable.ForeColor = System.Drawing.Color.LightGray;
            this.labelNoTable.Location = new System.Drawing.Point(803, 280);
            this.labelNoTable.Name = "labelNoTable";
            this.labelNoTable.Size = new System.Drawing.Size(342, 53);
            this.labelNoTable.TabIndex = 1446;
            this.labelNoTable.Text = "－ NoTable －";
            this.labelNoTable.Visible = false;
            // 
            // panel15
            // 
            this.panel15.BackColor = System.Drawing.SystemColors.MenuBar;
            this.panel15.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_Menu_ログイン制限;
            this.panel15.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel15.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel15.Controls.Add(this.label1);
            this.panel15.Location = new System.Drawing.Point(215, 18);
            this.panel15.Margin = new System.Windows.Forms.Padding(4);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(534, 27);
            this.panel15.TabIndex = 1445;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(8, 1);
            this.label1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 19);
            this.label1.TabIndex = 1368;
            this.label1.Text = "非表示理由";
            // 
            // textBoxStHidden
            // 
            this.textBoxStHidden.Enabled = false;
            this.textBoxStHidden.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.textBoxStHidden.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBoxStHidden.Location = new System.Drawing.Point(215, 44);
            this.textBoxStHidden.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxStHidden.Multiline = true;
            this.textBoxStHidden.Name = "textBoxStHidden";
            this.textBoxStHidden.Size = new System.Drawing.Size(534, 27);
            this.textBoxStHidden.TabIndex = 7;
            // 
            // panel17
            // 
            this.panel17.BackColor = System.Drawing.SystemColors.MenuBar;
            this.panel17.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.MenuPanel_B;
            this.panel17.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel17.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel17.Controls.Add(this.checkBoxStFlag);
            this.panel17.Location = new System.Drawing.Point(31, 18);
            this.panel17.Margin = new System.Windows.Forms.Padding(4);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(153, 55);
            this.panel17.TabIndex = 5;
            // 
            // F_Stock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_マスタ以外1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1902, 1033);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel7);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "F_Stock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "F_Stock";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.F_Stock_Activated);
            this.Load += new System.EventHandler(this.F_Stock_Load);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStock)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel15.ResumeLayout(false);
            this.panel15.PerformLayout();
            this.panel17.ResumeLayout(false);
            this.panel17.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label受注管理;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label在庫数;
        private System.Windows.Forms.Label label商品ID;
        private System.Windows.Forms.Label label在庫ID;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonList;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Button buttonPreviousPage;
        private System.Windows.Forms.Label labelPageSize;
        private System.Windows.Forms.CheckBox checkBoxStFlag;
        private System.Windows.Forms.Label labelPage;
        private System.Windows.Forms.DataGridView dataGridViewStock;
        private System.Windows.Forms.TextBox textBoxPageNo;
        private System.Windows.Forms.Button buttonFirstPage;
        private System.Windows.Forms.Button buttonLastPage;
        private System.Windows.Forms.TextBox textBoxPageSize;
        private System.Windows.Forms.Button buttonNextPage;
        private System.Windows.Forms.Button buttonPageSizeChange;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.TextBox textBoxPrID;
        private System.Windows.Forms.TextBox textBoxStID;
        private System.Windows.Forms.TextBox textBoxStQuantity;
        private System.Windows.Forms.Label labelPrName;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxStHidden;
        private System.Windows.Forms.Panel panel17;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label営業所名;
        private System.Windows.Forms.Label labelEmpName;
        private System.Windows.Forms.Label label社員名;
        private System.Windows.Forms.Label label社員ID;
        private System.Windows.Forms.Label labelOfficeName;
        private System.Windows.Forms.Label labelEmpID;
        private System.Windows.Forms.Label labelNoTable;
    }
}