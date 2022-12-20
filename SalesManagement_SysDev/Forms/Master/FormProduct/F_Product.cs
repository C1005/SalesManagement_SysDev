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
        //メッセージ表示用クラスのインスタンス化
        MessageDsp messageDsp = new MessageDsp();
        //データベース大分類テーブルアクセス用クラスのインスタンス化
        DbAccess.ProductDataAccess product = new DbAccess.ProductDataAccess();
        //入力形式チェック用クラスのインスタンス化
        DataInputFormCheck dataInputFormCheck = new DataInputFormCheck();
        //データグリッドビュー用の大分類データ
        private static List<M_Product> Product;
        //管理フラグを数値型で入れるための変数
        int PrFlg;

        public F_Product()
        {
            InitializeComponent();
        }

        private void F_Product_Load(object sender, EventArgs e)
        {

        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {

        }

        private void buttonRegist_Click(object sender, EventArgs e)
        {

        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {

        }

        private void buttonList_Click(object sender, EventArgs e)
        {

        }

        private void buttonClear_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxPrFlag_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {

        }

        private void buttonPageSizeChange_Click(object sender, EventArgs e)
        {

        }

        private void buttonFirstPage_Click(object sender, EventArgs e)
        {

        }

        private void buttonPreviousPage_Click(object sender, EventArgs e)
        {

        }

        private void buttonNextPage_Click(object sender, EventArgs e)
        {

        }

        private void buttonLastPage_Click(object sender, EventArgs e)
        {

        }

        private void dataGridViewProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBoxMaID_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxScID_TextChanged(object sender, EventArgs e)
        {

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
                case "メーカ検索": //ボタンのテキスト名
                    frm = new F_Maker(); //フォームの名前
                    break;
                case "大分類検索":
                    frm = new F_MajorCassification();
                    break;
                case "小分類検索":
                    frm = new F_SmallClassification();
                    break;
            }

            // すでに同じフォームが開かれているかどうかを確認する
            bool isOpen = false;
            Form openForm = null;
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == frm.GetType())
                {
                    isOpen = true;
                    openForm = form;
                    break;
                }
            }

            // 同じフォームが開かれていれば、そのフォームを最前面に持ってくる
            if (isOpen)
            {
                openForm.BringToFront();
            }
            // 同じフォームが開かれていなければ、選択されたフォームを開く
            else
            {
                frm.Show();
            }
        }
    }
}
