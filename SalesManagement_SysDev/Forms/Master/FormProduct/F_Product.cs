using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagement_SysDev.Forms.Master.FormProduct
{
    public partial class F_Product : Form
    {
        public F_Product()
        {
            InitializeComponent();
        }

        private void buttonMakerForm_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private void buttonMajorClassForm_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private void buttonSmallClassForm_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private void OpenForm(string formName)
        {
            Form frm = new Form();
            //引数より、開くフォームを設定
            switch (formName)
            {
                case "メーカ管理画面へ": //ボタンのテキスト名
                    frm = new F_Maker(); //フォームの名前
                    break;
                case "大分類管理画面へ":
                    frm = new F_MajorCassification();
                    break;
                case "小分類管理画面へ":
                    frm = new F_SmallClassification();
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
