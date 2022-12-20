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
            this.panel5 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSignup = new System.Windows.Forms.Button();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.textBoxUserID = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.buttonClear = new System.Windows.Forms.Button();
            this.labelUserID = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_CleateDabase = new System.Windows.Forms.Button();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
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
            this.buttonClose.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_その他;
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
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSignup
            // 
            this.buttonSignup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.buttonSignup.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_登録;
            this.buttonSignup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonSignup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonSignup.ForeColor = System.Drawing.Color.Black;
            this.buttonSignup.Location = new System.Drawing.Point(29, 4);
            this.buttonSignup.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSignup.Name = "buttonSignup";
            this.buttonSignup.Size = new System.Drawing.Size(101, 40);
            this.buttonSignup.TabIndex = 3;
            this.buttonSignup.Text = "新規登録";
            this.buttonSignup.UseVisualStyleBackColor = false;
            this.buttonSignup.Click += new System.EventHandler(this.buttonSignup_Click);
            // 
            // buttonLogin
            // 
            this.buttonLogin.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_更新;
            this.buttonLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonLogin.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(40)))), ((int)(((byte)(180)))));
            this.buttonLogin.Location = new System.Drawing.Point(336, 360);
            this.buttonLogin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(141, 61);
            this.buttonLogin.TabIndex = 2;
            this.buttonLogin.Text = "ログイン";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogon_Click);
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(123, 14);
            this.textBoxPassword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(332, 22);
            this.textBoxPassword.TabIndex = 1;
            // 
            // textBoxUserID
            // 
            this.textBoxUserID.Location = new System.Drawing.Point(288, 177);
            this.textBoxUserID.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxUserID.Name = "textBoxUserID";
            this.textBoxUserID.Size = new System.Drawing.Size(332, 22);
            this.textBoxUserID.TabIndex = 0;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(142)))));
            this.labelPassword.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelPassword.ForeColor = System.Drawing.Color.Yellow;
            this.labelPassword.Location = new System.Drawing.Point(20, 17);
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
            this.panel3.Location = new System.Drawing.Point(166, 19);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(621, 50);
            this.panel3.TabIndex = 1302;
            // 
            // buttonClear
            // 
            this.buttonClear.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_その他;
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
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // labelUserID
            // 
            this.labelUserID.AutoSize = true;
            this.labelUserID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(142)))));
            this.labelUserID.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelUserID.ForeColor = System.Drawing.Color.Yellow;
            this.labelUserID.Location = new System.Drawing.Point(185, 180);
            this.labelUserID.Name = "labelUserID";
            this.labelUserID.Size = new System.Drawing.Size(55, 15);
            this.labelUserID.TabIndex = 1303;
            this.labelUserID.Text = "社員ID";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(142)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Location = new System.Drawing.Point(163, 161);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(483, 54);
            this.panel1.TabIndex = 1299;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(142)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.labelPassword);
            this.panel2.Controls.Add(this.textBoxPassword);
            this.panel2.Location = new System.Drawing.Point(163, 256);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(483, 54);
            this.panel2.TabIndex = 1301;
            // 
            // btn_CleateDabase
            // 
            this.btn_CleateDabase.Location = new System.Drawing.Point(646, 369);
            this.btn_CleateDabase.Margin = new System.Windows.Forms.Padding(4);
            this.btn_CleateDabase.Name = "btn_CleateDabase";
            this.btn_CleateDabase.Size = new System.Drawing.Size(141, 61);
            this.btn_CleateDabase.TabIndex = 1304;
            this.btn_CleateDabase.Text = "データベース生成";
            this.btn_CleateDabase.UseVisualStyleBackColor = true;
            this.btn_CleateDabase.Visible = false;
            this.btn_CleateDabase.Click += new System.EventHandler(this.btn_CleateDabase_Click_1);
            // 
            // F_Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SalesManagement_SysDev.Properties.Resources.Fixed_ログイン;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(802, 443);
            this.Controls.Add(this.btn_CleateDabase);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.textBoxUserID);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.labelUserID);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "F_Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "販売管理システムログイン画面";
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSignup;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.TextBox textBoxUserID;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label labelUserID;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_CleateDabase;
        private System.Windows.Forms.Button buttonClear;
    }
}

