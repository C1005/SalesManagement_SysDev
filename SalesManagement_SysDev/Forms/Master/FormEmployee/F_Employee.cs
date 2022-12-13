using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagement_SysDev.Forms.Master.FormEmployee
{
    public partial class F_Employee : Form
    {
        public F_Employee()
        {
            InitializeComponent();
        }

        private void buttonSalesOfficeForm_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private void buttonPositionForm_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private void OpenForm(string formName)
        {
            Form frm = new Form();
            //引数より、開くフォームを設定
            switch (formName)
            {
                case "営業所管理画面へ": //ボタンのテキスト名
                    frm = new F_SalesOffice(); //フォームの名前
                    break;
                case "役職管理画面へ": //ボタンのテキスト名
                    frm = new F_Position(); //フォームの名前
                    break;
            }
            //選択されたフォームを開く
            frm.ShowDialog();

            //開いたフォームから戻ってきたら
            //メモリを解放する
            frm.Dispose();
        }
        ///////////////////////////////
        //メソッド名：SetDataGridView()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：データグリッドビューへの表示
        ///////////////////////////////
        private void SetDataGridView()
        {
            int pageSize = int.Parse(textBoxPageSize.Text);
            int pageNo = int.Parse(textBoxPageNo.Text) - 1;
            //////
            ///後でやる/// dataGridViewEmployee.DataSource = .Skip(pageSize * pageNo).Take(pageSize).ToList();
            //各列幅の指定
            dataGridViewEmployee.Columns[0].Width = 100;
            dataGridViewEmployee.Columns[1].Width = 200;
            dataGridViewEmployee.Columns[2].Width = 100;
            dataGridViewEmployee.Columns[3].Width = 400;

            //各列の文字位置の指定
            dataGridViewEmployee.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewEmployee.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewEmployee.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewEmployee.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            //後でやる// dataGridViewの総ページ数
            //labelPage.Text = "/" + ((int)Math.Ceiling(Position.Count / (double)pageSize)) + "ページ";

            dataGridViewEmployee.Refresh();

        }
    }
}
