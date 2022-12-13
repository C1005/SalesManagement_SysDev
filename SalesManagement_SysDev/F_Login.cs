using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagement_SysDev
{
    public partial class F_Login : Form
    {
        int count = 0;
        public F_Login()
        {
            InitializeComponent();
        }

        private void btn_CleateDabase_Click_1(object sender, EventArgs e)
        {
            //データベースの生成を行います．
            //再度実行する場合には，必ずデータベースの削除をしてから実行してください．

            //役職マスタを生成するサンプル（1件目に管理者を追加する例）
            M_Position FirstPosition = new M_Position()
            {
                PoName = "管理者",
                PoHidden = ""
            };
            SalesManagement_DevContext context = new SalesManagement_DevContext();
            context.M_Positions.Add(FirstPosition);
            context.SaveChanges();
            context.Dispose();

            //メッセージインポート
            Forms.DbAccess.MessageDataAccess.btn_CleateDabase_Messages();

            MessageBox.Show("テーブル作成完了");
        }

        private void buttonLogon_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSignup_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            // 入力エリアのクリア
            textBoxUserID.Text = "";
            textBoxPassword.Text = "";

            // 8回ボタンを押すとデータベースを削除(デバッグ用)
            count = count + 1;
            if(count == 8)
            {
                // 確認メッセージ
                DialogResult result = MessageBox.Show("データベースを削除しますか？","注意",MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    result = MessageBox.Show("削除後に再起動しますがよろしいですか？", "注意", MessageBoxButtons.OKCancel);
                    if(result == DialogResult.OK)
                    {
                        SalesManagement_SysDev.SalesManagement_DevContext delDB = new SalesManagement_SysDev.SalesManagement_DevContext();
                        delDB.Database.Delete();
                        MessageBox.Show("データベースを削除しました");
                        MessageBox.Show("再起動します","", MessageBoxButtons.YesNo);
                        Application.Restart();
                    }
                    count = 0;
                    return;
                }
                count = 0;
                return;
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
