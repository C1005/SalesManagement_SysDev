
namespace SalesManagement_SysDev.Forms.NonMaster.FormChumon
{
    partial class F_ChumonConfirm
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
            this.buttonConfirm = new System.Windows.Forms.Button();
            this.labelPage = new System.Windows.Forms.Label();
            this.dataGridViewChumon = new System.Windows.Forms.DataGridView();
            this.textBoxPageNo = new System.Windows.Forms.TextBox();
            this.buttonFirstPage = new System.Windows.Forms.Button();
            this.buttonLastPage = new System.Windows.Forms.Button();
            this.textBoxPageSize = new System.Windows.Forms.TextBox();
            this.buttonPageSizeChange = new System.Windows.Forms.Button();
            this.buttonPreviousPage = new System.Windows.Forms.Button();
            this.labelPageSize = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.buttonNextPage = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label受注ID = new System.Windows.Forms.Label();
            this.label顧客ID = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dateTimePickerOrDate = new System.Windows.Forms.DateTimePicker();
            this.panel10 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label受注年月日 = new System.Windows.Forms.Label();
            this.textBoxChID = new System.Windows.Forms.TextBox();
            this.textBoxOrID = new System.Windows.Forms.TextBox();
            this.textBoxClID = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonList = new System.Windows.Forms.Button();
            this.buttonSearch = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewChumon)).BeginInit();
            this.panel7.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
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
            this.buttonConfirm.TabIndex = 1398;
            this.buttonConfirm.TabStop = false;
            this.buttonConfirm.Text = "注文確定";
            this.buttonConfirm.UseVisualStyleBackColor = false;
            // 
            // labelPage
            // 
            this.labelPage.AutoSize = true;
            this.labelPage.Location = new System.Drawing.Point(679, 344);
            this.labelPage.Name = "labelPage";
            this.labelPage.Size = new System.Drawing.Size(43, 15);
            this.labelPage.TabIndex = 1387;
            this.labelPage.Text = "ページ";
            // 
            // dataGridViewChumon
            // 
            this.dataGridViewChumon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewChumon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewChumon.Location = new System.Drawing.Point(19, 27);
            this.dataGridViewChumon.Name = "dataGridViewChumon";
            this.dataGridViewChumon.RowHeadersWidth = 51;
            this.dataGridViewChumon.RowTemplate.Height = 24;
            this.dataGridViewChumon.Size = new System.Drawing.Size(907, 289);
            this.dataGridViewChumon.TabIndex = 1367;
            this.dataGridViewChumon.TabStop = false;
            // 
            // textBoxPageNo
            // 
            this.textBoxPageNo.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBoxPageNo.Location = new System.Drawing.Point(592, 339);
            this.textBoxPageNo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxPageNo.Name = "textBoxPageNo";
            this.textBoxPageNo.Size = new System.Drawing.Size(81, 22);
            this.textBoxPageNo.TabIndex = 1382;
            this.textBoxPageNo.TabStop = false;
            // 
            // buttonFirstPage
            // 
            this.buttonFirstPage.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_その他;
            this.buttonFirstPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonFirstPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonFirstPage.Location = new System.Drawing.Point(770, 336);
            this.buttonFirstPage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonFirstPage.Name = "buttonFirstPage";
            this.buttonFirstPage.Size = new System.Drawing.Size(33, 28);
            this.buttonFirstPage.TabIndex = 1383;
            this.buttonFirstPage.TabStop = false;
            this.buttonFirstPage.Text = "|◀";
            this.buttonFirstPage.UseVisualStyleBackColor = true;
            // 
            // buttonLastPage
            // 
            this.buttonLastPage.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_その他;
            this.buttonLastPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonLastPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonLastPage.Location = new System.Drawing.Point(881, 336);
            this.buttonLastPage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonLastPage.Name = "buttonLastPage";
            this.buttonLastPage.Size = new System.Drawing.Size(33, 28);
            this.buttonLastPage.TabIndex = 1386;
            this.buttonLastPage.TabStop = false;
            this.buttonLastPage.Text = "▶|";
            this.buttonLastPage.UseVisualStyleBackColor = true;
            // 
            // textBoxPageSize
            // 
            this.textBoxPageSize.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBoxPageSize.Location = new System.Drawing.Point(113, 339);
            this.textBoxPageSize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxPageSize.Name = "textBoxPageSize";
            this.textBoxPageSize.Size = new System.Drawing.Size(62, 22);
            this.textBoxPageSize.TabIndex = 1380;
            this.textBoxPageSize.TabStop = false;
            // 
            // buttonPageSizeChange
            // 
            this.buttonPageSizeChange.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_その他;
            this.buttonPageSizeChange.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonPageSizeChange.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonPageSizeChange.Location = new System.Drawing.Point(201, 333);
            this.buttonPageSizeChange.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonPageSizeChange.Name = "buttonPageSizeChange";
            this.buttonPageSizeChange.Size = new System.Drawing.Size(123, 35);
            this.buttonPageSizeChange.TabIndex = 1381;
            this.buttonPageSizeChange.TabStop = false;
            this.buttonPageSizeChange.Text = "行数変更";
            this.buttonPageSizeChange.UseVisualStyleBackColor = true;
            // 
            // buttonPreviousPage
            // 
            this.buttonPreviousPage.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_その他;
            this.buttonPreviousPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonPreviousPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonPreviousPage.Location = new System.Drawing.Point(807, 336);
            this.buttonPreviousPage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonPreviousPage.Name = "buttonPreviousPage";
            this.buttonPreviousPage.Size = new System.Drawing.Size(33, 28);
            this.buttonPreviousPage.TabIndex = 1384;
            this.buttonPreviousPage.TabStop = false;
            this.buttonPreviousPage.Text = "◀";
            this.buttonPreviousPage.UseVisualStyleBackColor = true;
            // 
            // labelPageSize
            // 
            this.labelPageSize.AutoSize = true;
            this.labelPageSize.Location = new System.Drawing.Point(28, 343);
            this.labelPageSize.Name = "labelPageSize";
            this.labelPageSize.Size = new System.Drawing.Size(81, 15);
            this.labelPageSize.TabIndex = 1379;
            this.labelPageSize.Text = "1ページ行数";
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel7.BackColor = System.Drawing.SystemColors.MenuBar;
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel7.Controls.Add(this.labelPage);
            this.panel7.Controls.Add(this.dataGridViewChumon);
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
            this.panel7.TabIndex = 1420;
            // 
            // buttonNextPage
            // 
            this.buttonNextPage.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_その他;
            this.buttonNextPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonNextPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonNextPage.Location = new System.Drawing.Point(844, 336);
            this.buttonNextPage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonNextPage.Name = "buttonNextPage";
            this.buttonNextPage.Size = new System.Drawing.Size(33, 28);
            this.buttonNextPage.TabIndex = 1385;
            this.buttonNextPage.TabStop = false;
            this.buttonNextPage.Text = "▶";
            this.buttonNextPage.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(96, 62);
            this.label1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(68, 19);
            this.label1.TabIndex = 1413;
            this.label1.Text = "注文ID";
            // 
            // label受注ID
            // 
            this.label受注ID.AutoSize = true;
            this.label受注ID.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label受注ID.ForeColor = System.Drawing.Color.Black;
            this.label受注ID.Location = new System.Drawing.Point(96, 130);
            this.label受注ID.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label受注ID.Name = "label受注ID";
            this.label受注ID.Size = new System.Drawing.Size(68, 19);
            this.label受注ID.TabIndex = 1389;
            this.label受注ID.Text = "受注ID";
            // 
            // label顧客ID
            // 
            this.label顧客ID.AutoSize = true;
            this.label顧客ID.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label顧客ID.ForeColor = System.Drawing.Color.Black;
            this.label顧客ID.Location = new System.Drawing.Point(96, 196);
            this.label顧客ID.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label顧客ID.Name = "label顧客ID";
            this.label顧客ID.Size = new System.Drawing.Size(68, 19);
            this.label顧客ID.TabIndex = 1356;
            this.label顧客ID.Text = "顧客ID";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.SystemColors.MenuBar;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.buttonConfirm);
            this.panel3.Controls.Add(this.dateTimePickerOrDate);
            this.panel3.Controls.Add(this.panel10);
            this.panel3.Controls.Add(this.label受注年月日);
            this.panel3.Controls.Add(this.textBoxChID);
            this.panel3.Controls.Add(this.textBoxOrID);
            this.panel3.Controls.Add(this.textBoxClID);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.label受注ID);
            this.panel3.Controls.Add(this.label顧客ID);
            this.panel3.Location = new System.Drawing.Point(39, 83);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(947, 272);
            this.panel3.TabIndex = 1421;
            // 
            // dateTimePickerOrDate
            // 
            this.dateTimePickerOrDate.Checked = false;
            this.dateTimePickerOrDate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dateTimePickerOrDate.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.dateTimePickerOrDate.Location = new System.Drawing.Point(558, 59);
            this.dateTimePickerOrDate.Name = "dateTimePickerOrDate";
            this.dateTimePickerOrDate.ShowCheckBox = true;
            this.dateTimePickerOrDate.Size = new System.Drawing.Size(200, 26);
            this.dateTimePickerOrDate.TabIndex = 1435;
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.SystemColors.MenuBar;
            this.panel10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel10.Controls.Add(this.label3);
            this.panel10.Controls.Add(this.label5);
            this.panel10.Location = new System.Drawing.Point(4, 4);
            this.panel10.Margin = new System.Windows.Forms.Padding(4);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(235, 25);
            this.panel10.TabIndex = 1435;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 10.2F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.label3.Location = new System.Drawing.Point(106, 1);
            this.label3.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 17);
            this.label3.TabIndex = 1432;
            this.label3.Text = "外部キー：下線";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("MS UI Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.ForeColor = System.Drawing.Color.Firebrick;
            this.label5.Location = new System.Drawing.Point(1, 1);
            this.label5.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 17);
            this.label5.TabIndex = 1430;
            this.label5.Text = "主キー：赤色";
            // 
            // label受注年月日
            // 
            this.label受注年月日.AutoSize = true;
            this.label受注年月日.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label受注年月日.ForeColor = System.Drawing.Color.Black;
            this.label受注年月日.Location = new System.Drawing.Point(423, 62);
            this.label受注年月日.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label受注年月日.Name = "label受注年月日";
            this.label受注年月日.Size = new System.Drawing.Size(109, 19);
            this.label受注年月日.TabIndex = 1436;
            this.label受注年月日.Text = "注文年月日";
            // 
            // textBoxChID
            // 
            this.textBoxChID.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.textBoxChID.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBoxChID.Location = new System.Drawing.Point(229, 60);
            this.textBoxChID.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.textBoxChID.MaxLength = 6;
            this.textBoxChID.Name = "textBoxChID";
            this.textBoxChID.Size = new System.Drawing.Size(140, 26);
            this.textBoxChID.TabIndex = 1439;
            // 
            // textBoxOrID
            // 
            this.textBoxOrID.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.textBoxOrID.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBoxOrID.Location = new System.Drawing.Point(229, 127);
            this.textBoxOrID.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.textBoxOrID.MaxLength = 6;
            this.textBoxOrID.Name = "textBoxOrID";
            this.textBoxOrID.Size = new System.Drawing.Size(140, 26);
            this.textBoxOrID.TabIndex = 1438;
            // 
            // textBoxClID
            // 
            this.textBoxClID.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.textBoxClID.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBoxClID.Location = new System.Drawing.Point(229, 193);
            this.textBoxClID.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.textBoxClID.MaxLength = 6;
            this.textBoxClID.Name = "textBoxClID";
            this.textBoxClID.Size = new System.Drawing.Size(140, 26);
            this.textBoxClID.TabIndex = 1437;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label12.Location = new System.Drawing.Point(16, 10);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(133, 30);
            this.label12.TabIndex = 1388;
            this.label12.Text = "注文確定";
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
            this.panel2.TabIndex = 1422;
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
            this.buttonClose.TabIndex = 6;
            this.buttonClose.TabStop = false;
            this.buttonClose.Text = "閉じる";
            this.buttonClose.UseVisualStyleBackColor = true;
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
            this.buttonClear.TabIndex = 5;
            this.buttonClear.TabStop = false;
            this.buttonClear.Text = "入力クリア";
            this.buttonClear.UseVisualStyleBackColor = true;
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
            this.buttonList.TabIndex = 1;
            this.buttonList.Text = "一覧表示";
            this.buttonList.UseVisualStyleBackColor = false;
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
            this.buttonSearch.TabIndex = 0;
            this.buttonSearch.Text = "検索";
            this.buttonSearch.UseVisualStyleBackColor = false;
            // 
            // F_ChumonConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_確定画面類;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1027, 773);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel5);
            this.DoubleBuffered = true;
            this.Name = "F_ChumonConfirm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "F_ChumonConfirm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewChumon)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonConfirm;
        private System.Windows.Forms.Label labelPage;
        private System.Windows.Forms.DataGridView dataGridViewChumon;
        private System.Windows.Forms.TextBox textBoxPageNo;
        private System.Windows.Forms.Button buttonFirstPage;
        private System.Windows.Forms.Button buttonLastPage;
        private System.Windows.Forms.TextBox textBoxPageSize;
        private System.Windows.Forms.Button buttonPageSizeChange;
        private System.Windows.Forms.Button buttonPreviousPage;
        private System.Windows.Forms.Label labelPageSize;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button buttonNextPage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label受注ID;
        private System.Windows.Forms.Label label顧客ID;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox textBoxChID;
        private System.Windows.Forms.TextBox textBoxOrID;
        private System.Windows.Forms.TextBox textBoxClID;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dateTimePickerOrDate;
        private System.Windows.Forms.Label label受注年月日;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonList;
        private System.Windows.Forms.Button buttonSearch;
    }
}