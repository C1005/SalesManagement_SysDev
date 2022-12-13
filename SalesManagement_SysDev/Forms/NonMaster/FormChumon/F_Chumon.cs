using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagement_SysDev.Forms.NonMaster.FormChumon
{
    public partial class F_Chumon : Form
    {
        //メッセージ表示用クラスのインスタンス化
        MessageDsp messageDsp = new MessageDsp();
        //データベース役職テーブルアクセス用クラスのインスタンス化
        DbAccess.ChumonDataAccess chumonDataAccess = new DbAccess.ChumonDataAccess();
        //入力形式チェック用クラスのインスタンス化
        DataInputFormCheck dataInputFormCheck = new DataInputFormCheck();
        //データグリッドビュー用の役職データ
        private static List<T_ChumonDsp> Chumon;
        //管理フラグを数値型で入れるための変数
        int ChFlg;
        int ChStateFlg;

        public F_Chumon()
        {
            InitializeComponent();
        }

        private void F_Chumon_Load(object sender, EventArgs e)
        {
            //ログイン名の表示
            //labelLoginName.Text = FormMenu.loginName;

            // データグリッドビューの表示
            SetFormDataGridView();
        }

        ///////////////////////////////
        //メソッド名：SetFormDataGridView()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：データグリッドビューの設定
        ///////////////////////////////
        private void SetFormDataGridView()
        {
            //dataGridViewのページサイズ指定
            textBoxPageSize.Text = "10";
            //dataGridViewのページ番号指定
            textBoxPageNo.Text = "1";
            //読み取り専用に指定
            dataGridViewChumon.ReadOnly = true;
            //行内をクリックすることで行を選択
            dataGridViewChumon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //ヘッダー位置の指定
            dataGridViewChumon.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //データグリッドビューのデータ取得
            GetDataGridView();
        }
        ///////////////////////////////
        //メソッド名：GetDataGridView()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：データグリッドビューに表示するデータの取得
        ///////////////////////////////
        private void GetDataGridView()
        {

            // 役職データの取得
            Chumon = chumonDataAccess.GetChumonData();

            // DataGridViewに表示するデータを指定
            SetDataGridView();
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
            dataGridViewChumon.DataSource = Chumon.Skip(pageSize * pageNo).Take(pageSize).ToList();
            //各列幅の指定
            dataGridViewChumon.Columns[0].Width = 100;
            dataGridViewChumon.Columns[1].Width = 100;
            dataGridViewChumon.Columns[2].Width = 100;
            dataGridViewChumon.Columns[3].Width = 100;
            dataGridViewChumon.Columns[4].Width = 100;
            dataGridViewChumon.Columns[5].Width = 130;
            dataGridViewChumon.Columns[6].Width = 110;
            dataGridViewChumon.Columns[7].Width = 110;
            dataGridViewChumon.Columns[8].Width = 400;
            dataGridViewChumon.Columns[9].Width = 100;
            dataGridViewChumon.Columns[10].Width = 100;
            dataGridViewChumon.Columns[11].Width = 70;

            //各列の文字位置の指定
            dataGridViewChumon.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewChumon.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewChumon.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewChumon.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewChumon.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewChumon.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewChumon.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewChumon.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewChumon.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewChumon.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewChumon.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewChumon.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //dataGridViewの総ページ数
            labelPage.Text = "/" + ((int)Math.Ceiling(Chumon.Count / (double)pageSize)) + "ページ";

            dataGridViewChumon.Refresh();
        }

        private void buttonPageSizeChange_Click(object sender, EventArgs e)
        {
            SetDataGridView();
        }

        private void buttonFirstPage_Click(object sender, EventArgs e)
        {
            int pageSize = int.Parse(textBoxPageSize.Text);
            dataGridViewChumon.DataSource = Chumon.Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewChumon.Refresh();
            //ページ番号の設定
            textBoxPageNo.Text = "1";
        }

        private void buttonPreviousPage_Click(object sender, EventArgs e)
        {
            int pageSize = int.Parse(textBoxPageSize.Text);
            int pageNo = int.Parse(textBoxPageNo.Text) - 2;
            dataGridViewChumon.DataSource = Chumon.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewChumon.Refresh();
            //ページ番号の設定
            if (pageNo + 1 > 1)
                textBoxPageNo.Text = (pageNo + 1).ToString();
            else
                textBoxPageNo.Text = "1";
        }

        private void buttonNextPage_Click(object sender, EventArgs e)
        {
            int pageSize = int.Parse(textBoxPageSize.Text);
            int pageNo = int.Parse(textBoxPageNo.Text);
            //最終ページの計算
            int lastNo = (int)Math.Ceiling(Chumon.Count / (double)pageSize) - 1;
            //最終ページでなければ
            if (pageNo <= lastNo)
                dataGridViewChumon.DataSource = Chumon.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewChumon.Refresh();
            //ページ番号の設定
            int lastPage = (int)Math.Ceiling(Chumon.Count / (double)pageSize);
            if (pageNo >= lastPage)
                textBoxPageNo.Text = lastPage.ToString();
            else
                textBoxPageNo.Text = (pageNo + 1).ToString();
        }

        private void buttonLastPage_Click(object sender, EventArgs e)
        {
            int pageSize = int.Parse(textBoxPageSize.Text);
            //最終ページの計算
            int pageNo = (int)Math.Ceiling(Chumon.Count / (double)pageSize) - 1;
            dataGridViewChumon.DataSource = Chumon.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewChumon.Refresh();
            //ページ番号の設定
            textBoxPageNo.Text = (pageNo + 1).ToString();
        }

        private void buttonCusSearch_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private void buttonProductSearch_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private void buttonEmployeeSearch_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private void buttonOrderSearch_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
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
                case "顧客検索": //ボタンのテキスト名
                    frm = new Master.FormClient.F_Client(); //フォームの名前
                    break;
                case "商品検索": //ボタンのテキスト名
                    frm = new Master.FormProduct.F_Product(); //フォームの名前
                    break;
                case "社員検索": //ボタンのテキスト名
                    frm = new Master.FormEmployee.F_Employee(); //フォームの名前
                    break;
                case "受注検索": //ボタンのテキスト名
                    frm = new NonMaster.FormOrder.F_Order(); //フォームの名前
                    break;
                case "注文確定画面へ": //ボタンのテキスト名
                    frm = new F_ChumonConfirm(); //フォームの名前
                    break;
            }
            //選択されたフォームを開く
            frm.ShowDialog();

            //開いたフォームから戻ってきたら
            //メモリを解放する
            frm.Dispose();
        }

        private void dataGridViewChumon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //クリックされた行データをテキストボックスへ
            textBoxChID.Text = dataGridViewChumon.Rows[dataGridViewChumon.CurrentRow.Index].Cells[0].Value.ToString();
            textBoxSoID.Text = dataGridViewChumon.Rows[dataGridViewChumon.CurrentRow.Index].Cells[1].Value.ToString();
            textBoxEmID.Text = dataGridViewChumon.Rows[dataGridViewChumon.CurrentRow.Index].Cells[2].Value.ToString();
            textBoxClID.Text = dataGridViewChumon.Rows[dataGridViewChumon.CurrentRow.Index].Cells[3].Value.ToString();
            textBoxOrID.Text = dataGridViewChumon.Rows[dataGridViewChumon.CurrentRow.Index].Cells[4].Value.ToString();

            //日付が設定されていない場合、初期値として現在の日付を設定
            if (dataGridViewChumon.Rows[dataGridViewChumon.CurrentRow.Index].Cells[5].Value == null)
            {
                dateTimePickerChDate.Value = DateTime.Now;
                dateTimePickerChDate.Checked = false;
            }
            else
            {
                dateTimePickerChDate.Text = dataGridViewChumon.Rows[dataGridViewChumon.CurrentRow.Index].Cells[5].Value.ToString();



            }

            //状態フラグの数値型をbool型に変換して取得
            int ChStateFlg2 = int.Parse(dataGridViewChumon.Rows[dataGridViewChumon.CurrentRow.Index].Cells[6].Value.ToString());
            if (ChStateFlg2 == 0)
            {
                checkBoxChStateFlag.Checked = false;
            }
            else
            {
                checkBoxChStateFlag.Checked = true;
            }
            //管理フラグの数値型をbool型に変換して取得
            int ChFlg2 = int.Parse(dataGridViewChumon.Rows[dataGridViewChumon.CurrentRow.Index].Cells[7].Value.ToString());
            if (ChFlg2 == 0)
            {
                checkBoxChFlag.Checked = false;
            }
            else if (ChFlg2 == 2)
            {
                checkBoxChFlag.Checked = true;
            }

            textBoxChHidden.Text = dataGridViewChumon.Rows[dataGridViewChumon.CurrentRow.Index].Cells[8].Value.ToString();
            textBoxChDetailID.Text = dataGridViewChumon.Rows[dataGridViewChumon.CurrentRow.Index].Cells[9].Value.ToString();
            textBoxPrID.Text = dataGridViewChumon.Rows[dataGridViewChumon.CurrentRow.Index].Cells[10].Value.ToString();
            textBoxChQuantity.Text = dataGridViewChumon.Rows[dataGridViewChumon.CurrentRow.Index].Cells[11].Value.ToString();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            // 8.2.4.1 妥当な受注データ取得
            if (!GetValidDataAtSelect())
                return;

            // 8.2.4.2 受注情報抽出
            GenerateDataAtSelect();

            // 8.2.4.3 受注抽出結果表示
            SetSelectData();
        }

        ///////////////////////////////
        //　8.2.4.1 妥当な受注データ取得
        //メソッド名：GetValidDataAtSlect()
        //引　数   ：なし
        //戻り値   ：true or false
        //機　能   ：入力データの形式チェック
        //          ：エラーがない場合True
        //          ：エラーがある場合False
        ///////////////////////////////
        private bool GetValidDataAtSelect()
        {
            // 注文IDの適否
            if (!String.IsNullOrEmpty(textBoxChID.Text.Trim()))
            {
                //// 顧客IDに一致するレコードの存在チェック
                //if (labelClName.Text == "“UnknownID”")
                //{
                //    //MessageBox.Show("入力された顧客IDは存在しません");
                //    messageDsp.DspMsg("M10025");
                //    textBoxClID.Focus();
                //    return false;
                //}
                //// 顧客IDの半角数字チェック
                //if (!dataInputFormCheck.CheckNumeric(textBoxClID.Text.Trim()))
                //{
                //    //MessageBox.Show("顧客IDは全て半角数字入力です");
                //    messageDsp.DspMsg("M10001");
                //    textBoxClID.Focus();
                //    return false;
                //}
                //// 顧客IDの文字数チェック
                //if (textBoxClID.TextLength > 6)
                //{
                //    //MessageBox.Show("顧客IDは6文字です");
                //    messageDsp.DspMsg("M10002");
                //    textBoxClID.Focus();
                //    return false;
                //}
            }

            // 受注IDの適否
            if (!String.IsNullOrEmpty(textBoxOrID.Text.Trim()))
            {
                //// 顧客IDに一致するレコードの存在チェック
                //if (labelClName.Text == "“UnknownID”")
                //{
                //    //MessageBox.Show("入力された顧客IDは存在しません");
                //    messageDsp.DspMsg("M10025");
                //    textBoxClID.Focus();
                //    return false;
                //}
                //// 顧客IDの半角数字チェック
                //if (!dataInputFormCheck.CheckNumeric(textBoxClID.Text.Trim()))
                //{
                //    //MessageBox.Show("顧客IDは全て半角数字入力です");
                //    messageDsp.DspMsg("M10001");
                //    textBoxClID.Focus();
                //    return false;
                //}
                //// 顧客IDの文字数チェック
                //if (textBoxClID.TextLength > 6)
                //{
                //    //MessageBox.Show("顧客IDは6文字です");
                //    messageDsp.DspMsg("M10002");
                //    textBoxClID.Focus();
                //    return false;
                //}
            }

            // 顧客IDの適否
            if (!String.IsNullOrEmpty(textBoxClID.Text.Trim()))
            {
                // 顧客IDに一致するレコードの存在チェック
                if (labelClName.Text == "“UnknownID”")
                {
                    //MessageBox.Show("入力された顧客IDは存在しません");
                    messageDsp.DspMsg("M10025");
                    textBoxClID.Focus();
                    return false;
                }
                // 顧客IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxClID.Text.Trim()))
                {
                    //MessageBox.Show("顧客IDは全て半角数字入力です");
                    messageDsp.DspMsg("M10001");
                    textBoxClID.Focus();
                    return false;
                }
                // 顧客IDの文字数チェック
                if (textBoxClID.TextLength > 6)
                {
                    //MessageBox.Show("顧客IDは6文字です");
                    messageDsp.DspMsg("M10002");
                    textBoxClID.Focus();
                    return false;
                }
            }

            // 社員IDの適否
            if (!String.IsNullOrEmpty(textBoxEmID.Text.Trim()))
            {
                //// 社員IDが0ではないかチェック
                //if (int.Parse(textBoxEmID.Text.Trim()) == 0)
                //{
                //    //MessageBox.Show("社員IDは01から割り当ててください");
                //    messageDsp.DspMsg("M10007");
                //    textBoxEmID.Focus();
                //    return false;
                //}

                /// ↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑
                /// 仕様書で社員IDがNULLを許容する管理は実行しない
                /// ただし社員IDが必須入力の場合はCtrl + K + U でコメント解除してチェックさせてください

                // 社員IDに一致するレコードの存在チェック
                if (labelEmName.Text == "“UnknownID”")
                {
                    //MessageBox.Show("入力された社員IDは存在しません");
                    messageDsp.DspMsg("M10041");
                    textBoxEmID.Focus();
                    return false;
                }

                // 社員IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxEmID.Text.Trim()))
                {
                    //MessageBox.Show("社員IDは全て半角数字入力です");
                    messageDsp.DspMsg("M10005");
                    textBoxEmID.Focus();
                    return false;
                }
                // 社員IDの文字数チェック
                if (textBoxEmID.TextLength > 6)
                {
                    //MessageBox.Show("社員IDは6文字です");
                    messageDsp.DspMsg("M10006");
                    textBoxEmID.Focus();
                    return false;
                }
            }

            // 営業所IDの適否
            if (!String.IsNullOrEmpty(textBoxSoID.Text.Trim()))
            {
                // 営業所IDに一致するレコードの存在チェック
                if (labelSoName.Text == "“UnknownID”")
                {
                    //MessageBox.Show("入力された営業所IDは存在しません");
                    messageDsp.DspMsg("M10042");
                    textBoxSoID.Focus();
                    return false;
                }
                // 営業所IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxSoID.Text.Trim()))
                {
                    //MessageBox.Show("営業所IDは全て半角数字入力です");
                    messageDsp.DspMsg("M10009");
                    textBoxSoID.Focus();
                    return false;
                }
                // 営業所IDの文字数チェック
                if (textBoxSoID.TextLength > 2)
                {
                    //MessageBox.Show("営業所IDは6文字です");
                    messageDsp.DspMsg("M10010");
                    textBoxSoID.Focus();
                    return false;
                }
            }

            // 顧客担当者名の適否
            if (!String.IsNullOrEmpty(textBoxClCharge.Text.Trim()))
            {
                // 顧客担当者名の全角チェック
                if (!dataInputFormCheck.CheckFullWidth(textBoxClCharge.Text.Trim()))
                {
                    //MessageBox.Show("顧客担当者名は全て全角入力です");
                    messageDsp.DspMsg("M10013");
                    textBoxClCharge.Focus();
                    return false;
                }
                // 顧客担当者名の文字数チェック
                if (textBoxClCharge.TextLength > 50)
                {
                    //MessageBox.Show("顧客担当者名は50文字以下です");
                    messageDsp.DspMsg("M10014");
                    textBoxClCharge.Focus();
                    return false;
                }
            }

            // 受注詳細IDの未入力チェック
            if (!String.IsNullOrEmpty(textBoxOrDetailID.Text.Trim()))
            {
                // 受注詳細IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxOrDetailID.Text.Trim()))
                {
                    //MessageBox.Show("受注詳細IDは全て半角数字入力です");
                    messageDsp.DspMsg("M10055");
                    textBoxOrDetailID.Focus();
                    return false;
                }
            }

            // 商品IDの適否
            if (!String.IsNullOrEmpty(textBoxPrID.Text.Trim()))
            {
                // 商品IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxPrID.Text.Trim()))
                {
                    //MessageBox.Show("商品IDは全て半角数字入力です");
                    messageDsp.DspMsg("M10016");
                    textBoxPrID.Focus();
                    return false;
                }
                // 商品IDの文字数チェック
                if (textBoxPrID.TextLength > 6)
                {
                    //MessageBox.Show("商品IDは6文字です");
                    messageDsp.DspMsg("M10017");
                    textBoxPrID.Focus();
                    return false;
                }
            }

            // 数量の適否
            if (!String.IsNullOrEmpty(textBoxOrQuantity.Text.Trim()))
            {
                // 数量の半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxOrQuantity.Text.Trim()))
                {
                    //MessageBox.Show("数量は全て半角数字入力です");
                    messageDsp.DspMsg("M10020");
                    textBoxOrQuantity.Focus();
                    return false;
                }
                // 数量の文字数チェック
                if (textBoxOrQuantity.TextLength > 4)
                {
                    //MessageBox.Show("数量は4文字です");
                    messageDsp.DspMsg("M10021");
                    textBoxOrQuantity.Focus();
                    return false;
                }
            }

            // 合計金額の適否
            if (!String.IsNullOrEmpty(textBoxOrTotalPrice.Text.Trim()))
            {
                // 合計金額の半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxOrTotalPrice.Text.Trim()))
                {
                    //MessageBox.Show("合計金額は全て半角数字入力です");
                    messageDsp.DspMsg("M10023");
                    textBoxOrTotalPrice.Focus();
                    return false;
                }
                // 合計金額の文字数チェック
                if (textBoxOrTotalPrice.TextLength > 10)
                {
                    //MessageBox.Show("合計金額は10文字です");
                    messageDsp.DspMsg("M10024");
                    textBoxOrTotalPrice.Focus();
                    return false;
                }
            }

            // 状態フラグの適否
            if (checkBoxOrStateFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("状態フラグが不確定の状態です");
                messageDsp.DspMsg("M10027");
                checkBoxOrStateFlag.Focus();
                return false;
            }

            // 管理フラグの適否
            if (checkBoxOrFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("管理フラグが不確定の状態です");
                messageDsp.DspMsg("M10028");
                checkBoxOrFlag.Focus();
                return false;
            }
            return true;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {

        }

        private void buttonList_Click(object sender, EventArgs e)
        {
            // データグリッドビューの表示
            SetFormDataGridView();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            // 入力エリアのクリア
            ClearInput();
            // データグリッドビューの表示
            SetFormDataGridView();
        }

        private void ClearInput()
        {
            textBoxChID.Text = "";
            textBoxSoID.Text = "";
            textBoxEmID.Text = "";
            textBoxClID.Text = "";
            textBoxOrID.Text = "";
            dateTimePickerChDate.Value = DateTime.Now;
            dateTimePickerChDate.Checked = false;
            textBoxChDetailID.Text = "";
            textBoxPrID.Text = "";
            textBoxChQuantity.Text = "";
            textBoxChHidden.Text = "";
            checkBoxChStateFlag.Checked = false;
            checkBoxChFlag.Checked = false;
        }

        private void checkBoxChStateFlag_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxChStateFlag.Checked == true)
            {
                ChStateFlg = 1;
                return;
            }
            else if (checkBoxChStateFlag.Checked == false)
            {
                ChStateFlg = 0;
                return;
            }
        }

        private void checkBoxChFlag_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxChFlag.Checked == true)
            {
                ChFlg = 2;
                textBoxChHidden.Enabled = true;
                return;
            }
            else if (checkBoxChFlag.Checked == false)
            {
                ChFlg = 0;
                textBoxChHidden.Enabled = false;
                textBoxChHidden.Text = "";
                return;
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonDetailClear_Click(object sender, EventArgs e)
        {
            // 受注詳細欄の入力エリアのクリア
            textBoxChDetailID.Text = "";
            textBoxPrID.Text = "";
            textBoxChQuantity.Text = "";

            // データグリッドビューの表示
            SetFormDataGridView();
        }

        //関連するIDを表示(受注IDは複雑)
        private void textBoxOrID_TextChanged(object sender, EventArgs e)
        {
            if (textBoxOrID.Text != "")
            {
                var context = new SalesManagement_DevContext();
                try
                {
                    int mOrID = int.Parse(textBoxOrID.Text);
                    var mOrder = context.T_Orders.Single(x => x.OrID == mOrID); //入力されたOrIDで一致する一件のレコードを探す
                    if (mOrder.OrFlag == 2)
                    {
                        labelFlag.Visible = true;
                    }
                    if (mOrder.OrStateFlag == 1)
                    {
                        labelStateFlag.Visible = true;
                    }
                    int mClID = mOrder.ClID;
                    int mEmID = mOrder.EmID;
                    int mSoID = mOrder.SoID;
                    textBoxClID.Text = mClID.ToString();
                    textBoxEmID.Text = mEmID.ToString();
                    textBoxSoID.Text = mSoID.ToString();
                    context.Dispose();
                }
                catch
                {
                    labelStateFlag.Visible = true;
                    labelStateFlag.Text = "“UnknownID”";
                    context.Dispose();
                    return;
                }
            }
            else
            {
                textBoxClID.Text = "";
                textBoxEmID.Text = "";
                textBoxSoID.Text = "";
                labelFlag.Visible = false;
                labelStateFlag.Visible = false;
                labelStateFlag.Text = "確定済";
            }
        }

        private void textBoxClID_TextChanged(object sender, EventArgs e)
        {
            if (textBoxClID.Text != "")
            {
                var context = new SalesManagement_DevContext();
                try
                {
                    int mClID = int.Parse(textBoxClID.Text);
                    var mClient = context.M_Clients.Single(x => x.ClID == mClID);
                    if (mClient.ClFlag == 2)
                    {
                        string mClName = mClient.ClName;
                        labelClName.Text = "(非表示)" + mClName;
                        labelClName.Visible = true;
                        context.Dispose();
                    }
                    else
                    {
                        string mClName = mClient.ClName;
                        labelClName.Text = mClName;
                        labelClName.Visible = true;
                        context.Dispose();
                    }
                    int mSoID = mClient.SoID;
                    textBoxSoID.Text = mSoID.ToString();
                }
                catch
                {
                    labelClName.Visible = true;
                    labelClName.Text = "“UnknownID”";
                    context.Dispose();
                    return;
                }
            }
            else
            {
                labelClName.Visible = false;
                labelClName.Text = "顧客名";
                textBoxSoID.Text = "";
            }
        }

        private void textBoxSoID_TextChanged(object sender, EventArgs e)
        {
            if (textBoxSoID.Text != "")
            {
                var context = new SalesManagement_DevContext();
                try
                {
                    int mSoID = int.Parse(textBoxSoID.Text);
                    var mSalesOffice = context.M_SalesOffices.Single(x => x.SoID == mSoID);
                    if (mSalesOffice.SoFlag == 2)
                    {
                        string mSoName = mSalesOffice.SoName;
                        labelSoName.Text = "(非表示)" + mSoName;
                        labelSoName.Visible = true;
                        context.Dispose();
                    }
                    else
                    {
                        string mSoName = mSalesOffice.SoName;
                        labelSoName.Text = mSoName;
                        labelSoName.Visible = true;
                        context.Dispose();
                    }
                }
                catch
                {
                    labelSoName.Visible = true;
                    labelSoName.Text = "“UnknownID”";
                    context.Dispose();
                    return;
                }
            }
            else
            {
                labelSoName.Visible = false;
                labelSoName.Text = "営業所名";
            }
        }

        private void textBoxEmID_TextChanged(object sender, EventArgs e)
        {
            if (textBoxEmID.Text != "")
            {
                var context = new SalesManagement_DevContext();
                try
                {
                    string mEmName;
                    int mEmID = int.Parse(textBoxEmID.Text);
                    var mEmployee = context.M_Employees.Single(x => x.EmID == mEmID);
                    if (mEmployee.EmFlag == 2)
                    {
                        mEmName = mEmployee.EmName;
                        labelEmName.Text = "(非表示)" + mEmName;
                        labelEmName.Visible = true;
                        context.Dispose();
                    }
                    else
                    {
                        mEmName = mEmployee.EmName;
                        labelEmName.Text = mEmName;
                        labelEmName.Visible = true;
                        context.Dispose();
                    }
                    if (textBoxClID.Text == "")
                    {
                        int mSoID = mEmployee.SoID;
                        textBoxSoID.Text = mSoID.ToString();
                    }
                }
                catch
                {
                    // 仕様書で社員IDがNULLを許容する管理のみifを実行
                    // ただし社員IDが必須入力の場合はifを排除してください
                    if (int.Parse(textBoxEmID.Text) == 0)
                    {
                        labelEmName.Text = "“NULLとして設定”";
                        labelEmName.Visible = true;
                        context.Dispose();
                        return;
                    }
                    labelEmName.Text = "“UnknownID”";
                    labelEmName.Visible = true;
                    context.Dispose();
                    return;
                }
            }
            else
            {
                labelEmName.Visible = false;
                labelEmName.Text = "社員名";
                textBoxSoID.Text = "";
            }
        }

        private void textBoxPrID_TextChanged(object sender, EventArgs e)
        {
            if (textBoxPrID.Text != "")
            {
                var context = new SalesManagement_DevContext();
                try
                {
                    int mPrID = int.Parse(textBoxPrID.Text);
                    var mProduct = context.M_Products.Single(x => x.PrID == mPrID);
                    if (mProduct.PrFlag == 2)
                    {
                        string mPrName = mProduct.PrName;
                        int mPrice = mProduct.Price;
                        labelPrName.Text = "(非表示)" + mPrName;
                        labelPrName.Visible = true;
                        context.Dispose();
                    }
                    else
                    {
                        string mPrName = mProduct.PrName;
                        int mPrice = mProduct.Price;
                        labelPrName.Text = mPrName;
                        labelPrName.Visible = true;
                        context.Dispose();
                    }
                }
                catch
                {
                    labelPrName.Text = "“UnknownID”";
                    labelPrName.Visible = true;
                    context.Dispose();
                    return;
                }
            }
            else
            {
                labelPrName.Visible = false;
                labelPrName.Text = "商品名";
            }
        }
    }
}
