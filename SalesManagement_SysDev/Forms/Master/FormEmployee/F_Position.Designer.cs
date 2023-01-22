
namespace SalesManagement_SysDev.Forms.Master.FormEmployee
{
    partial class F_Position
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_Position));
            this.buttonPageSizeChange = new System.Windows.Forms.Button();
            this.labelPageSize = new System.Windows.Forms.Label();
            this.buttonLastPage = new System.Windows.Forms.Button();
            this.labelPage = new System.Windows.Forms.Label();
            this.buttonNextPage = new System.Windows.Forms.Button();
            this.buttonPreviousPage = new System.Windows.Forms.Button();
            this.textBoxPageNo = new System.Windows.Forms.TextBox();
            this.buttonFirstPage = new System.Windows.Forms.Button();
            this.checkBoxPoFlag = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.labelNoTable = new System.Windows.Forms.Label();
            this.textBoxPageSize = new System.Windows.Forms.TextBox();
            this.panel13 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPoHidden = new System.Windows.Forms.TextBox();
            this.panel14 = new System.Windows.Forms.Panel();
            this.dataGridViewPosition = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label役職ID = new System.Windows.Forms.Label();
            this.textBoxPoID = new System.Windows.Forms.TextBox();
            this.textBoxPoName = new System.Windows.Forms.TextBox();
            this.panel11 = new System.Windows.Forms.Panel();
            this.label役職名 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label社員管理 = new System.Windows.Forms.Label();
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
            this.buttonRegist = new System.Windows.Forms.Button();
            this.panel3.SuspendLayout();
            this.panel13.SuspendLayout();
            this.panel14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPosition)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
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
            this.buttonPageSizeChange.Click += new System.EventHandler(this.buttonPageSizeChange_Click_1);
            // 
            // labelPageSize
            // 
            this.labelPageSize.AutoSize = true;
            this.labelPageSize.BackColor = System.Drawing.Color.Transparent;
            this.labelPageSize.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelPageSize.Location = new System.Drawing.Point(28, 578);
            this.labelPageSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPageSize.Name = "labelPageSize";
            this.labelPageSize.Size = new System.Drawing.Size(81, 15);
            this.labelPageSize.TabIndex = 1343;
            this.labelPageSize.Text = "1ページ行数";
            // 
            // buttonLastPage
            // 
            this.buttonLastPage.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_その他;
            this.buttonLastPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonLastPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonLastPage.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonLastPage.Location = new System.Drawing.Point(1852, 570);
            this.buttonLastPage.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.buttonLastPage.Name = "buttonLastPage";
            this.buttonLastPage.Size = new System.Drawing.Size(33, 28);
            this.buttonLastPage.TabIndex = 20;
            this.buttonLastPage.Text = "▶|";
            this.buttonLastPage.UseVisualStyleBackColor = true;
            this.buttonLastPage.Click += new System.EventHandler(this.buttonLastPage_Click_1);
            // 
            // labelPage
            // 
            this.labelPage.AutoSize = true;
            this.labelPage.BackColor = System.Drawing.Color.Transparent;
            this.labelPage.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelPage.Location = new System.Drawing.Point(1650, 578);
            this.labelPage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPage.Name = "labelPage";
            this.labelPage.Size = new System.Drawing.Size(43, 15);
            this.labelPage.TabIndex = 1351;
            this.labelPage.Text = "ページ";
            // 
            // buttonNextPage
            // 
            this.buttonNextPage.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_その他;
            this.buttonNextPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonNextPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonNextPage.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonNextPage.Location = new System.Drawing.Point(1815, 570);
            this.buttonNextPage.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.buttonNextPage.Name = "buttonNextPage";
            this.buttonNextPage.Size = new System.Drawing.Size(33, 28);
            this.buttonNextPage.TabIndex = 19;
            this.buttonNextPage.Text = "▶";
            this.buttonNextPage.UseVisualStyleBackColor = true;
            this.buttonNextPage.Click += new System.EventHandler(this.buttonNextPage_Click_1);
            // 
            // buttonPreviousPage
            // 
            this.buttonPreviousPage.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_その他;
            this.buttonPreviousPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonPreviousPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonPreviousPage.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonPreviousPage.Location = new System.Drawing.Point(1778, 570);
            this.buttonPreviousPage.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.buttonPreviousPage.Name = "buttonPreviousPage";
            this.buttonPreviousPage.Size = new System.Drawing.Size(33, 28);
            this.buttonPreviousPage.TabIndex = 18;
            this.buttonPreviousPage.Text = "◀";
            this.buttonPreviousPage.UseVisualStyleBackColor = true;
            this.buttonPreviousPage.Click += new System.EventHandler(this.buttonPreviousPage_Click_1);
            // 
            // textBoxPageNo
            // 
            this.textBoxPageNo.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxPageNo.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBoxPageNo.Location = new System.Drawing.Point(1563, 573);
            this.textBoxPageNo.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.textBoxPageNo.Name = "textBoxPageNo";
            this.textBoxPageNo.Size = new System.Drawing.Size(81, 22);
            this.textBoxPageNo.TabIndex = 16;
            // 
            // buttonFirstPage
            // 
            this.buttonFirstPage.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_その他;
            this.buttonFirstPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonFirstPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonFirstPage.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonFirstPage.Location = new System.Drawing.Point(1741, 570);
            this.buttonFirstPage.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.buttonFirstPage.Name = "buttonFirstPage";
            this.buttonFirstPage.Size = new System.Drawing.Size(33, 28);
            this.buttonFirstPage.TabIndex = 17;
            this.buttonFirstPage.Text = "|◀";
            this.buttonFirstPage.UseVisualStyleBackColor = true;
            this.buttonFirstPage.Click += new System.EventHandler(this.buttonFirstPage_Click_1);
            // 
            // checkBoxPoFlag
            // 
            this.checkBoxPoFlag.AutoSize = true;
            this.checkBoxPoFlag.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxPoFlag.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBoxPoFlag.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBoxPoFlag.Location = new System.Drawing.Point(10, 16);
            this.checkBoxPoFlag.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxPoFlag.Name = "checkBoxPoFlag";
            this.checkBoxPoFlag.Size = new System.Drawing.Size(121, 19);
            this.checkBoxPoFlag.TabIndex = 5;
            this.checkBoxPoFlag.Text = "役職管理フラグ";
            this.checkBoxPoFlag.UseVisualStyleBackColor = false;
            this.checkBoxPoFlag.CheckedChanged += new System.EventHandler(this.checkBoxPoFlag_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.MenuBar;
            this.panel3.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_マスタ以外panel;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.labelNoTable);
            this.panel3.Controls.Add(this.textBoxPageSize);
            this.panel3.Controls.Add(this.panel13);
            this.panel3.Controls.Add(this.buttonPageSizeChange);
            this.panel3.Controls.Add(this.textBoxPoHidden);
            this.panel3.Controls.Add(this.labelPageSize);
            this.panel3.Controls.Add(this.panel14);
            this.panel3.Controls.Add(this.buttonLastPage);
            this.panel3.Controls.Add(this.labelPage);
            this.panel3.Controls.Add(this.buttonNextPage);
            this.panel3.Controls.Add(this.buttonPreviousPage);
            this.panel3.Controls.Add(this.textBoxPageNo);
            this.panel3.Controls.Add(this.buttonFirstPage);
            this.panel3.Controls.Add(this.dataGridViewPosition);
            this.panel3.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.panel3.Location = new System.Drawing.Point(39, 363);
            this.panel3.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1940, 617);
            this.panel3.TabIndex = 3;
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
            this.labelNoTable.TabIndex = 1438;
            this.labelNoTable.Text = "－ NoTable －";
            this.labelNoTable.Visible = false;
            // 
            // textBoxPageSize
            // 
            this.textBoxPageSize.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBoxPageSize.Location = new System.Drawing.Point(113, 574);
            this.textBoxPageSize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxPageSize.Name = "textBoxPageSize";
            this.textBoxPageSize.Size = new System.Drawing.Size(60, 22);
            this.textBoxPageSize.TabIndex = 14;
            // 
            // panel13
            // 
            this.panel13.BackColor = System.Drawing.SystemColors.MenuBar;
            this.panel13.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_Menu_ログイン制限;
            this.panel13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel13.Controls.Add(this.label2);
            this.panel13.Location = new System.Drawing.Point(215, 18);
            this.panel13.Margin = new System.Windows.Forms.Padding(4);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(534, 27);
            this.panel13.TabIndex = 1436;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(8, 1);
            this.label2.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 19);
            this.label2.TabIndex = 1368;
            this.label2.Text = "非表示理由";
            // 
            // textBoxPoHidden
            // 
            this.textBoxPoHidden.Enabled = false;
            this.textBoxPoHidden.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.textBoxPoHidden.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBoxPoHidden.Location = new System.Drawing.Point(215, 44);
            this.textBoxPoHidden.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxPoHidden.Multiline = true;
            this.textBoxPoHidden.Name = "textBoxPoHidden";
            this.textBoxPoHidden.Size = new System.Drawing.Size(534, 27);
            this.textBoxPoHidden.TabIndex = 6;
            // 
            // panel14
            // 
            this.panel14.BackColor = System.Drawing.SystemColors.MenuBar;
            this.panel14.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.MenuPanel_B;
            this.panel14.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel14.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel14.Controls.Add(this.checkBoxPoFlag);
            this.panel14.Location = new System.Drawing.Point(31, 18);
            this.panel14.Margin = new System.Windows.Forms.Padding(4);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(153, 55);
            this.panel14.TabIndex = 4;
            // 
            // dataGridViewPosition
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewPosition.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewPosition.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewPosition.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewPosition.Location = new System.Drawing.Point(31, 84);
            this.dataGridViewPosition.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewPosition.Name = "dataGridViewPosition";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewPosition.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewPosition.RowHeadersWidth = 51;
            this.dataGridViewPosition.RowTemplate.Height = 24;
            this.dataGridViewPosition.Size = new System.Drawing.Size(1874, 478);
            this.dataGridViewPosition.TabIndex = 1327;
            this.dataGridViewPosition.TabStop = false;
            this.dataGridViewPosition.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPosition_CellClick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.panel1.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_マスタ以外panel;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.textBoxPoID);
            this.panel1.Controls.Add(this.textBoxPoName);
            this.panel1.Controls.Add(this.panel11);
            this.panel1.Location = new System.Drawing.Point(39, 83);
            this.panel1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1940, 272);
            this.panel1.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.MenuBar;
            this.panel4.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_マスタ用ボタン_閉じる;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.label役職ID);
            this.panel4.Location = new System.Drawing.Point(69, 120);
            this.panel4.Margin = new System.Windows.Forms.Padding(4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(220, 26);
            this.panel4.TabIndex = 1439;
            // 
            // label役職ID
            // 
            this.label役職ID.AutoSize = true;
            this.label役職ID.BackColor = System.Drawing.Color.Transparent;
            this.label役職ID.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label役職ID.ForeColor = System.Drawing.Color.Firebrick;
            this.label役職ID.Location = new System.Drawing.Point(67, 1);
            this.label役職ID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label役職ID.Name = "label役職ID";
            this.label役職ID.Size = new System.Drawing.Size(68, 19);
            this.label役職ID.TabIndex = 1326;
            this.label役職ID.Text = "役職ID";
            // 
            // textBoxPoID
            // 
            this.textBoxPoID.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxPoID.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.textBoxPoID.Location = new System.Drawing.Point(297, 120);
            this.textBoxPoID.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxPoID.MaxLength = 2;
            this.textBoxPoID.Name = "textBoxPoID";
            this.textBoxPoID.Size = new System.Drawing.Size(348, 26);
            this.textBoxPoID.TabIndex = 1;
            // 
            // textBoxPoName
            // 
            this.textBoxPoName.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.textBoxPoName.Location = new System.Drawing.Point(908, 120);
            this.textBoxPoName.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxPoName.MaxLength = 50;
            this.textBoxPoName.Name = "textBoxPoName";
            this.textBoxPoName.Size = new System.Drawing.Size(348, 26);
            this.textBoxPoName.TabIndex = 2;
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.SystemColors.MenuBar;
            this.panel11.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_マスタ用ボタン_閉じる;
            this.panel11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel11.Controls.Add(this.label役職名);
            this.panel11.Location = new System.Drawing.Point(680, 120);
            this.panel11.Margin = new System.Windows.Forms.Padding(4);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(220, 26);
            this.panel11.TabIndex = 1438;
            // 
            // label役職名
            // 
            this.label役職名.AutoSize = true;
            this.label役職名.BackColor = System.Drawing.Color.Transparent;
            this.label役職名.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label役職名.ForeColor = System.Drawing.Color.Black;
            this.label役職名.Location = new System.Drawing.Point(67, 1);
            this.label役職名.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label役職名.Name = "label役職名";
            this.label役職名.Size = new System.Drawing.Size(69, 19);
            this.label役職名.TabIndex = 1332;
            this.label役職名.Text = "役職名";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.MenuBar;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel5.Controls.Add(this.label社員管理);
            this.panel5.Location = new System.Drawing.Point(39, 24);
            this.panel5.Margin = new System.Windows.Forms.Padding(4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(177, 54);
            this.panel5.TabIndex = 1403;
            // 
            // label社員管理
            // 
            this.label社員管理.AutoSize = true;
            this.label社員管理.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label社員管理.Location = new System.Drawing.Point(16, 10);
            this.label社員管理.Name = "label社員管理";
            this.label社員管理.Size = new System.Drawing.Size(133, 30);
            this.label社員管理.TabIndex = 1388;
            this.label社員管理.Text = "役職管理";
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
            this.panel2.Controls.Add(this.buttonRegist);
            this.panel2.Location = new System.Drawing.Point(224, 24);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1755, 54);
            this.panel2.TabIndex = 7;
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
            this.panel8.TabIndex = 1406;
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
            this.buttonSearch.TabIndex = 8;
            this.buttonSearch.Text = "検索";
            this.buttonSearch.UseVisualStyleBackColor = false;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // buttonRegist
            // 
            this.buttonRegist.BackColor = System.Drawing.Color.Chartreuse;
            this.buttonRegist.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_登録;
            this.buttonRegist.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonRegist.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonRegist.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonRegist.Location = new System.Drawing.Point(156, 3);
            this.buttonRegist.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.buttonRegist.Name = "buttonRegist";
            this.buttonRegist.Size = new System.Drawing.Size(103, 45);
            this.buttonRegist.TabIndex = 9;
            this.buttonRegist.Text = "登録";
            this.buttonRegist.UseVisualStyleBackColor = false;
            this.buttonRegist.Click += new System.EventHandler(this.buttonRegist_Click);
            // 
            // F_Position
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_管理画面へ;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1902, 1033);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel2);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "F_Position";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "F_Position";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.F_Position_Activated);
            this.Load += new System.EventHandler(this.F_Position_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel13.ResumeLayout(false);
            this.panel13.PerformLayout();
            this.panel14.ResumeLayout(false);
            this.panel14.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPosition)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonPageSizeChange;
        private System.Windows.Forms.Label labelPageSize;
        private System.Windows.Forms.Button buttonLastPage;
        private System.Windows.Forms.Label labelPage;
        private System.Windows.Forms.Button buttonNextPage;
        private System.Windows.Forms.Button buttonPreviousPage;
        private System.Windows.Forms.TextBox textBoxPageNo;
        private System.Windows.Forms.Button buttonFirstPage;
        private System.Windows.Forms.CheckBox checkBoxPoFlag;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dataGridViewPosition;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBoxPoName;
        private System.Windows.Forms.Label label役職ID;
        private System.Windows.Forms.Label label役職名;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label社員管理;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonList;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Button buttonRegist;
        private System.Windows.Forms.TextBox textBoxPageSize;
        private System.Windows.Forms.TextBox textBoxPoID;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPoHidden;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel11;
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