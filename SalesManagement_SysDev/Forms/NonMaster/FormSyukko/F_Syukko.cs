using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagement_SysDev.Forms.NonMaster.FormSyukko
{
    public partial class F_Syukko : Form
    {
        public F_Syukko()
        {
            InitializeComponent();
        }

        private void buttonConfirmForm_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private void OpenForm(string formName)
        {
            Form frm = new Form();
            //引数より、開くフォームを設定
            switch (formName)
            {
                case "出庫確定画面へ": //ボタンのテキスト名
                    frm = new F_SyukkoConfirm(); //フォームの名前
                    break;
            }
            //選択されたフォームを開く
            frm.ShowDialog();

            //開いたフォームから戻ってきたら
            //メモリを解放する
            frm.Dispose();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
