
namespace SalesManagement_SysDev.Forms.NonMaster.FormHattyu
{
    partial class F_HattyuUpdate
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
            this.buttonUpdateConfirm = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelメーカID = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dateTimePickerHaDate = new System.Windows.Forms.DateTimePicker();
            this.panel10 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label発注年月日 = new System.Windows.Forms.Label();
            this.textBoxHaID = new System.Windows.Forms.TextBox();
            this.textBoxMaID = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonList = new System.Windows.Forms.Button();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.labelPage = new System.Windows.Forms.Label();
            this.dataGridViewChumon = new System.Windows.Forms.DataGridView();
            this.textBoxPageNo = new System.Windows.Forms.TextBox();
            this.buttonFirstPage = new System.Windows.Forms.Button();
            this.buttonLastPage = new System.Windows.Forms.Button();
            this.textBoxPageSize = new System.Windows.Forms.TextBox();
            this.buttonNextPage = new System.Windows.Forms.Button();
            this.buttonPageSizeChange = new System.Windows.Forms.Button();
            this.buttonPreviousPage = new System.Windows.Forms.Button();
            this.labelPageSize = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewChumon)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonUpdateConfirm
            // 
            this.buttonUpdateConfirm.BackColor = System.Drawing.Color.RoyalBlue;
            this.buttonUpdateConfirm.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_発注更新ボタン;
            this.buttonUpdateConfirm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonUpdateConfirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonUpdateConfirm.Font = new System.Drawing.Font("MS UI Gothic", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonUpdateConfirm.ForeColor = System.Drawing.Color.White;
            this.buttonUpdateConfirm.Location = new System.Drawing.Point(764, 178);
            this.buttonUpdateConfirm.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.buttonUpdateConfirm.Name = "buttonUpdateConfirm";
            this.buttonUpdateConfirm.Size = new System.Drawing.Size(169, 83);
            this.buttonUpdateConfirm.TabIndex = 1398;
            this.buttonUpdateConfirm.TabStop = false;
            this.buttonUpdateConfirm.Text = "発注更新";
            this.buttonUpdateConfirm.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(95, 82);
            this.label1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(68, 19);
            this.label1.TabIndex = 1413;
            this.label1.Text = "発注ID";
            // 
            // labelメーカID
            // 
            this.labelメーカID.AutoSize = true;
            this.labelメーカID.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelメーカID.ForeColor = System.Drawing.Color.Black;
            this.labelメーカID.Location = new System.Drawing.Point(95, 165);
            this.labelメーカID.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.labelメーカID.Name = "labelメーカID";
            this.labelメーカID.Size = new System.Drawing.Size(74, 19);
            this.labelメーカID.TabIndex = 1389;
            this.labelメーカID.Text = "メーカID";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.SystemColors.MenuBar;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.buttonUpdateConfirm);
            this.panel3.Controls.Add(this.dateTimePickerHaDate);
            this.panel3.Controls.Add(this.panel10);
            this.panel3.Controls.Add(this.label発注年月日);
            this.panel3.Controls.Add(this.textBoxHaID);
            this.panel3.Controls.Add(this.textBoxMaID);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.labelメーカID);
            this.panel3.Location = new System.Drawing.Point(39, 83);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(947, 272);
            this.panel3.TabIndex = 1422;
            // 
            // dateTimePickerHaDate
            // 
            this.dateTimePickerHaDate.Checked = false;
            this.dateTimePickerHaDate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dateTimePickerHaDate.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.dateTimePickerHaDate.Location = new System.Drawing.Point(558, 79);
            this.dateTimePickerHaDate.Name = "dateTimePickerHaDate";
            this.dateTimePickerHaDate.ShowCheckBox = true;
            this.dateTimePickerHaDate.Size = new System.Drawing.Size(200, 26);
            this.dateTimePickerHaDate.TabIndex = 1485;
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
            // label発注年月日
            // 
            this.label発注年月日.AutoSize = true;
            this.label発注年月日.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label発注年月日.ForeColor = System.Drawing.Color.Black;
            this.label発注年月日.Location = new System.Drawing.Point(423, 82);
            this.label発注年月日.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label発注年月日.Name = "label発注年月日";
            this.label発注年月日.Size = new System.Drawing.Size(109, 19);
            this.label発注年月日.TabIndex = 1486;
            this.label発注年月日.Text = "発注年月日";
            // 
            // textBoxHaID
            // 
            this.textBoxHaID.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.textBoxHaID.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBoxHaID.Location = new System.Drawing.Point(228, 80);
            this.textBoxHaID.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.textBoxHaID.MaxLength = 6;
            this.textBoxHaID.Name = "textBoxHaID";
            this.textBoxHaID.Size = new System.Drawing.Size(140, 26);
            this.textBoxHaID.TabIndex = 1430;
            // 
            // textBoxMaID
            // 
            this.textBoxMaID.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.textBoxMaID.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBoxMaID.Location = new System.Drawing.Point(228, 162);
            this.textBoxMaID.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.textBoxMaID.MaxLength = 4;
            this.textBoxMaID.Name = "textBoxMaID";
            this.textBoxMaID.Size = new System.Drawing.Size(140, 26);
            this.textBoxMaID.TabIndex = 1429;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label12.Location = new System.Drawing.Point(16, 10);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(133, 30);
            this.label12.TabIndex = 1388;
            this.label12.Text = "発注更新";
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
            this.panel5.TabIndex = 1419;
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
            this.panel2.TabIndex = 1423;
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
            this.panel7.TabIndex = 1424;
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
            // F_HattyuUpdate
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
            this.Name = "F_HattyuUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "F_HattyuUpdate";
            this.Load += new System.EventHandler(this.F_HattyuUpdate_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewChumon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonUpdateConfirm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelメーカID;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox textBoxHaID;
        private System.Windows.Forms.TextBox textBoxMaID;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dateTimePickerHaDate;
        private System.Windows.Forms.Label label発注年月日;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonList;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label labelPage;
        private System.Windows.Forms.DataGridView dataGridViewChumon;
        private System.Windows.Forms.TextBox textBoxPageNo;
        private System.Windows.Forms.Button buttonFirstPage;
        private System.Windows.Forms.Button buttonLastPage;
        private System.Windows.Forms.TextBox textBoxPageSize;
        private System.Windows.Forms.Button buttonNextPage;
        private System.Windows.Forms.Button buttonPageSizeChange;
        private System.Windows.Forms.Button buttonPreviousPage;
        private System.Windows.Forms.Label labelPageSize;
    }
}