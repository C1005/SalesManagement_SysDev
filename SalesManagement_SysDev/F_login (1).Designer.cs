namespace SalesManagement_SysDev
{
    partial class F_Login
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_CleateDabase = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSignup = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonLogon = new System.Windows.Forms.Button();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.textBoxUserID = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.labelUserID = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_CleateDabase
            // 
            this.btn_CleateDabase.Location = new System.Drawing.Point(619, 357);
            this.btn_CleateDabase.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_CleateDabase.Name = "btn_CleateDabase";
            this.btn_CleateDabase.Size = new System.Drawing.Size(141, 61);
            this.btn_CleateDabase.TabIndex = 0;
            this.btn_CleateDabase.Text = "データベース生成";
            this.btn_CleateDabase.UseVisualStyleBackColor = true;
            this.btn_CleateDabase.Click += new System.EventHandler(this.btn_CleateDabase_Click);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.MenuBar;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel5.Controls.Add(this.label1);
            this.panel5.Location = new System.Drawing.Point(13, 19);
            this.panel5.Margin = new System.Windows.Forms.Padding(4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(143, 50);
            this.panel5.TabIndex = 1300;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(9, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "ログイン画面";
            // 
            // buttonClose
            // 
            this.buttonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonClose.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonClose.Location = new System.Drawing.Point(495, 2);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(101, 42);
            this.buttonClose.TabIndex = 1289;
            this.buttonClose.TabStop = false;
            this.buttonClose.Text = "閉じる";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // buttonSignup
            // 
            this.buttonSignup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(210)))), ((int)(((byte)(170)))));
            this.buttonSignup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonSignup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonSignup.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonSignup.ForeColor = System.Drawing.Color.Black;
            this.buttonSignup.Location = new System.Drawing.Point(32, 3);
            this.buttonSignup.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSignup.Name = "buttonSignup";
            this.buttonSignup.Size = new System.Drawing.Size(101, 40);
            this.buttonSignup.TabIndex = 3;
            this.buttonSignup.Text = "新規登録";
            this.buttonSignup.UseVisualStyleBackColor = false;
            // 
            // buttonClear
            // 
            this.buttonClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonClear.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonClear.Location = new System.Drawing.Point(375, 2);
            this.buttonClear.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(101, 42);
            this.buttonClear.TabIndex = 1288;
            this.buttonClear.TabStop = false;
            this.buttonClear.Text = "入力クリア";
            this.buttonClear.UseVisualStyleBackColor = true;
            // 
            // buttonLogon
            // 
            this.buttonLogon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonLogon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonLogon.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonLogon.ForeColor = System.Drawing.Color.Blue;
            this.buttonLogon.Location = new System.Drawing.Point(152, 2);
            this.buttonLogon.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonLogon.Name = "buttonLogon";
            this.buttonLogon.Size = new System.Drawing.Size(101, 42);
            this.buttonLogon.TabIndex = 2;
            this.buttonLogon.Text = "ログオン";
            this.buttonLogon.UseVisualStyleBackColor = true;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(271, 269);
            this.textBoxPassword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(332, 22);
            this.textBoxPassword.TabIndex = 1298;
            // 
            // textBoxUserID
            // 
            this.textBoxUserID.Location = new System.Drawing.Point(271, 178);
            this.textBoxUserID.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxUserID.Name = "textBoxUserID";
            this.textBoxUserID.Size = new System.Drawing.Size(332, 22);
            this.textBoxUserID.TabIndex = 1297;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(142)))));
            this.labelPassword.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelPassword.ForeColor = System.Drawing.Color.Yellow;
            this.labelPassword.Location = new System.Drawing.Point(168, 272);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(69, 15);
            this.labelPassword.TabIndex = 1304;
            this.labelPassword.Text = "パスワード";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.MenuBar;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.buttonClose);
            this.panel3.Controls.Add(this.buttonSignup);
            this.panel3.Controls.Add(this.buttonClear);
            this.panel3.Controls.Add(this.buttonLogon);
            this.panel3.Location = new System.Drawing.Point(164, 19);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(621, 50);
            this.panel3.TabIndex = 1299;
            // 
            // labelUserID
            // 
            this.labelUserID.AutoSize = true;
            this.labelUserID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(142)))));
            this.labelUserID.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelUserID.ForeColor = System.Drawing.Color.Yellow;
            this.labelUserID.Location = new System.Drawing.Point(168, 181);
            this.labelUserID.Name = "labelUserID";
            this.labelUserID.Size = new System.Drawing.Size(75, 15);
            this.labelUserID.TabIndex = 1303;
            this.labelUserID.Text = "ユーザーID";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(142)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Location = new System.Drawing.Point(146, 161);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(483, 54);
            this.panel1.TabIndex = 1301;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(142)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Location = new System.Drawing.Point(146, 254);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(483, 54);
            this.panel2.TabIndex = 1302;
            // 
            // F_Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_ログイン;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxUserID);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.labelUserID);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btn_CleateDabase);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "F_Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "販売管理システムログイン画面";
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_CleateDabase;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSignup;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonLogon;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.TextBox textBoxUserID;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label labelUserID;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}

