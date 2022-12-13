using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagement_SysDev.Forms.NonMaster.FormHattyu
{
    public partial class F_Hattyu : Form
    {
        public F_Hattyu()
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
                case "発注更新画面へ": //ボタンのテキスト名
                    frm = new F_HattyuUpdate(); //フォームの名前
                    break;
            }
            //選択されたフォームを開く
            frm.ShowDialog();

            //開いたフォームから戻ってきたら
            //メモリを解放する
            frm.Dispose();
        }
    }
}
