using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagement_SysDev.Forms.Master.FormStock
{
    public partial class F_Stock : Form
    {
        //メッセージ表示用クラスのインスタンス化
        MessageDsp messageDsp = new MessageDsp();
        //データベース在庫テーブルアクセス用クラスのインスタンス化
        DbAccess.StockDataAccess StockDataAccess = new DbAccess.StockDataAccess();
        //入力形式チェック用クラスのインスタンス化
        DataInputFormCheck dataInputFormCheck = new DataInputFormCheck();
        //データグリッドビュー用の在庫データ
        private static List<T_Stock> Stock;
        //管理フラグを数値型で入れるための変数
        int StFlg;

        public F_Stock()
        {
            InitializeComponent();
        }

        private void F_Stock_Load(object sender, EventArgs e)
        {
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
            dataGridViewStock.ReadOnly = true;
            //行内をクリックすることで行を選択
            dataGridViewStock.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //ヘッダー位置の指定
            dataGridViewStock.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

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

            // 在庫データの取得
            Stock = StockDataAccess.GetStockData();

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
            dataGridViewStock.DataSource = Stock.Skip(pageSize * pageNo).Take(pageSize).ToList();
            //各列幅の指定
            dataGridViewStock.Columns[0].Width = 80;
            dataGridViewStock.Columns[1].Width = 80;
            dataGridViewStock.Columns[2].Width = 80;
            dataGridViewStock.Columns[3].Width = 80;
            dataGridViewStock.Columns[4].Width = 400;

            //各列の文字位置の指定
            dataGridViewStock.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewStock.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewStock.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewStock.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewStock.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            //dataGridViewの総ページ数
            labelPage.Text = "/" + ((int)Math.Ceiling(Stock.Count / (double)pageSize)) + "ページ";

            dataGridViewStock.Refresh();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            // 8.2.4.1 妥当な在庫データ取得
            if (!GetValidDataAtSelect())
                return;

            // 8.2.4.2 在庫情報抽出
            GenerateDataAtSelect();

            // 8.2.4.3 在庫抽出結果表示
            SetSelectData();
        }

        ///////////////////////////////
        //　8.2.4.1 妥当な在庫データ取得
        //メソッド名：GetValidDataAtSelect()
        //引　数   ：なし
        //戻り値   ：true or false
        //機　能   ：入力データの形式チェック
        //          ：エラーがない場合True
        //          ：エラーがある場合False
        ///////////////////////////////
        private bool GetValidDataAtSelect()
        {

            // 在庫IDの適否
            if (!String.IsNullOrEmpty(textBoxStID.Text.Trim()))
            {
                // スタッフCDの半角英数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxStID.Text.Trim()))
                {
                    //MessageBox.Show("在庫IDは全て半角数字入力です");
                    messageDsp.DspMsg("M3001");
                    textBoxStID.Focus();
                    return false;
                }
                // 在庫IDの文字数チェック
                if (textBoxStID.TextLength > 2)
                {
                    //MessageBox.Show("在庫IDは2文字です");
                    messageDsp.DspMsg("M3002");
                    textBoxStID.Focus();
                    return false;
                }

            }


            if (textBoxStID.Text == "" && textBoxPrID.Text == "" && checkBoxStFlag.Checked == false)
            {
                // データグリッドビューの表示
                SetFormDataGridView();
                return false;
            }
            // 管理フラグの適否
            if (checkBoxStFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("管理フラグが不確定の状態です");
                messageDsp.DspMsg("M3008");
                checkBoxStFlag.Focus();
                return false;
            }
            return true;

        }
        ///////////////////////////////
        //　8.2.4.2 在庫情報抽出
        //メソッド名：GenerateDataAtSelect()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：在庫情報の取得
        ///////////////////////////////

        public static string mStID; //cSoIDを別クラス(StockDataAccess)でも使用できるように定義
        public static string mPrID;
        public static bool mStFlg;
        private void GenerateDataAtSelect()
        {
            T_Stock selectCondition;

            mStID = textBoxStID.Text.Trim(); //IDはnullではなく、""で空白が入るのでstringで代入
            mPrID = textBoxPrID.Text.Trim();
            mStFlg = checkBoxStFlag.Checked;

            if (mStID != "") //IDで検索
            {
                // 検索条件のセット
                selectCondition = new T_Stock()
                {
                    StID = int.Parse(textBoxStID.Text.Trim()),
                    StFlag = StFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // 在庫データの抽出
                Stock = StockDataAccess.GetStockData(selectCondition);
                return;
            }
            else if (mPrID != "") // 名前で検索
            {
                // 検索条件のセット
                selectCondition = new T_Stock()
                {
                    PrID = int.Parse(textBoxPrID.Text.Trim()),
                    StFlag = StFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // 在庫データの抽出
                Stock = StockDataAccess.GetStockData(selectCondition);
                return;
            }
            else if (mStFlg == true) // ただの非表示一覧表示
            {
                // 検索条件のセット
                selectCondition = new T_Stock()
                {
                    StFlag = StFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // 在庫データの抽出
                Stock = StockDataAccess.GetStockData(selectCondition);
                return;
            }
        }

        ///////////////////////////////
        //　8.2.4.3 在庫抽出結果表示
        //メソッド名：SetSelectData()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：在庫情報の表示
        ///////////////////////////////
        private void SetSelectData()
        {
            textBoxPageNo.Text = "1";

            int pageSize = int.Parse(textBoxPageSize.Text);

            dataGridViewStock.DataSource = Stock;

            labelPage.Text = "/" + ((int)Math.Ceiling(Stock.Count / (double)pageSize)) + "ページ";
            dataGridViewStock.Refresh();

            if (Stock.Count == 0) //検索結果のデータ数が0ならエラー
            {
                messageDsp.DspMsg("M1025");
                SetFormDataGridView();
                return;
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            // 8.2.2.1 妥当な在庫データ取得
            if (!GetValidDataAtUpdate())
                return;

            // 8.2.2.2 在庫情報作成
            var updStock = GenerateDataAtUpdate();

            // 8.2.2.3 在庫情報更新
            UpdateStock(updStock);
        }

        ///////////////////////////////
        //　8.2.2.1 妥当な在庫データ取得
        //メソッド名：GetValidDataAtUpdate()
        //引　数   ：なし
        //戻り値   ：true or false
        //機　能   ：入力データの形式チェック
        //          ：エラーがない場合True
        //          ：エラーがある場合False
        ///////////////////////////////
        private bool GetValidDataAtUpdate()
        {

            // 在庫IDの未入力チェック
            if (!String.IsNullOrEmpty(textBoxStID.Text.Trim()))
            {
                // 在庫IDの半角英数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxStID.Text.Trim()))
                {
                    //MessageBox.Show("在庫IDは全て半角英数字入力です");
                    messageDsp.DspMsg("M3001");
                    textBoxStID.Focus();
                    return false;
                }
                // 在庫IDの文字数チェック
                if (textBoxStID.TextLength > 2)
                {
                    //MessageBox.Show("在庫IDは2文字です");
                    messageDsp.DspMsg("M3002");
                    textBoxStID.Focus();
                    return false;
                }
                // 在庫IDの存在チェック
                if (!StockDataAccess.CheckStockCDExistence(textBoxStID.Text.Trim()))
                {
                    //MessageBox.Show("入力された在庫IDは存在しません");
                    messageDsp.DspMsg("M3017");
                    textBoxStID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("在庫IDが入力されていません");
                messageDsp.DspMsg("M3004");
                textBoxStID.Focus();
                return false;
            }


            // 商品IDの未入力チェック
            if (!String.IsNullOrEmpty(textBoxPrID.Text.Trim()))
            {
                // 商品IDの半角英数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxPrID.Text.Trim()))
                {
                    //MessageBox.Show("商品IDは全て半角英数字入力です");
                    messageDsp.DspMsg("M3001");
                    textBoxPrID.Focus();
                    return false;
                }
                // 商品IDの文字数チェック
                if (textBoxStID.TextLength > 2)
                {
                    //MessageBox.Show("商品IDは2文字です");
                    messageDsp.DspMsg("M3002");
                    textBoxPrID.Focus();
                    return false;
                }
                // 商品IDの存在チェック
                if (!StockDataAccess.CheckStockCDExistence(textBoxStID.Text.Trim()))
                {
                    //MessageBox.Show("入力された商品IDは存在しません");
                    messageDsp.DspMsg("M3017");
                    textBoxPrID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("商品IDが入力されていません");
                messageDsp.DspMsg("M3004");
                textBoxPrID.Focus();
                return false;
            }

            // 在庫数の未入力チェック
            if (!String.IsNullOrEmpty(textBoxStQuantity.Text.Trim()))
            {
                // 在庫数の半角英数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxPrID.Text.Trim()))
                {
                    //MessageBox.Show("在庫数は全て半角英数字入力です");
                    messageDsp.DspMsg("M3001");
                    textBoxStQuantity.Focus();
                    return false;
                }
                // 在庫数の文字数チェック
                if (textBoxStID.TextLength > 4)
                {
                    //MessageBox.Show("在庫数は4文字です");
                    messageDsp.DspMsg("M3002");
                    textBoxStQuantity.Focus();
                    return false;
                }
                // 在庫数の存在チェック
                if (!StockDataAccess.CheckStockCDExistence(textBoxStID.Text.Trim()))
                {
                    //MessageBox.Show("入力された在庫数は存在しません");
                    messageDsp.DspMsg("M3017");
                    textBoxStQuantity.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("在庫数が入力されていません");
                messageDsp.DspMsg("M3004");
                textBoxStQuantity.Focus();
                return false;
            }
            return true;
        }

        ///////////////////////////////
        //　8.2.2.2 在庫情報作成
        //メソッド名：GenerateDataAtUpdate()
        //引　数   ：なし
        //戻り値   ：在庫更新情報
        //機　能   ：更新データのセット
        ///////////////////////////////
        private T_Stock GenerateDataAtUpdate()
        {
            return new T_Stock
            {
                PrID = int.Parse(textBoxPrID.Text.Trim()),
                StID = int.Parse(textBoxStID.Text.Trim()),
                StQuantity = int.Parse(textBoxStQuantity.Text.Trim()),
                StFlag = StFlg,
                StHidden = textBoxStHidden.Text.Trim(),
            };
        }

        ///////////////////////////////
        //　8.2.2.3 在庫情報更新
        //メソッド名：UpdateStock()
        //引　数   ：在庫情報
        //戻り値   ：なし
        //機　能   ：在庫情報の更新
        ///////////////////////////////
        private void UpdateStock(T_Stock updStock)
        {
            if (StFlg == 0)
            {
                // 更新確認メッセージ
                DialogResult result = messageDsp.DspMsg("M3018");
                if (result == DialogResult.Cancel)
                    return;

                // 在庫情報の更新
                bool flg = StockDataAccess.UpdateStockData(updStock);
                if (flg == true)
                    //MessageBox.Show("データを更新しました。");
                    messageDsp.DspMsg("M3019");
                else
                    //MessageBox.Show("データの更新に失敗しました。");
                    messageDsp.DspMsg("M3020");

                textBoxStID.Focus();

                // 入力エリアのクリア
                ClearInput();

                // データグリッドビューの表示
                GetDataGridView();
            }

            else if (StFlg == 2)
            {
                DeleteStock(updStock);
            }
        }
        private void DeleteStock(T_Stock delStock)
        {

            // 更新確認メッセージ
            DialogResult result = messageDsp.DspMsg("M3022");
            if (result == DialogResult.Cancel)
                return;

            // 在庫情報の更新
            bool flg = StockDataAccess.DeleteStockData(delStock);
            if (flg == true)
                //MessageBox.Show("データを削除しました。");
                messageDsp.DspMsg("M3023");
            else
                //MessageBox.Show("データの削除に失敗しました。");
                messageDsp.DspMsg("M3024");

            textBoxStID.Focus();

            // 入力エリアのクリア
            ClearInput();

            // データグリッドビューの表示
            GetDataGridView();
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

        ///////////////////////////////
        //メソッド名：ClearInput()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：入力エリアをクリア
        ///////////////////////////////
        private void ClearInput()
        {
            textBoxStID.Text = "";
            textBoxPrID.Text = "";
            textBoxStQuantity.Text = "";
            textBoxStQuantity.Text = "";
            checkBoxStFlag.Checked = false;
            textBoxStHidden.Text = "";
        }

        private void checkBoxStFlag_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxStFlag.Checked == true)
            {
                StFlg = 2;
                textBoxStHidden.Enabled = true;
                return;
            }
            else
            {
                StFlg = 0;
                textBoxStHidden.Enabled = false;
                textBoxStHidden.Text = "";
                return;
            }
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {

        }

        private void buttonPrdctSearch_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private void OpenForm(string formName)
        {
            Form frm = new Form();
            //引数より、開くフォームを設定
            switch (formName)
            {
                case "商品検索":
                    frm = new Forms.Master.FormProduct.F_Product(); //フォルダも指定する必要がある
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

        private void buttonPageSizeChange_Click(object sender, EventArgs e)
        {
            SetDataGridView();
        }

        private void buttonFirstPage_Click(object sender, EventArgs e)
        {
            int pageSize = int.Parse(textBoxPageSize.Text);
            dataGridViewStock.DataSource = Stock.Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewStock.Refresh();
            //ページ番号の設定
            textBoxPageNo.Text = "1";
        }

        private void buttonPreviousPage_Click(object sender, EventArgs e)
        {
            int pageSize = int.Parse(textBoxPageSize.Text);
            int pageNo = int.Parse(textBoxPageNo.Text) - 2;
            dataGridViewStock.DataSource = Stock.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewStock.Refresh();
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
            int lastNo = (int)Math.Ceiling(Stock.Count / (double)pageSize) - 1;
            //最終ページでなければ
            if (pageNo <= lastNo)
                dataGridViewStock.DataSource = Stock.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewStock.Refresh();
            //ページ番号の設定
            int lastPage = (int)Math.Ceiling(Stock.Count / (double)pageSize);
            if (pageNo >= lastPage)
                textBoxPageNo.Text = lastPage.ToString();
            else
                textBoxPageNo.Text = (pageNo + 1).ToString();
        }

        private void buttonLastPage_Click(object sender, EventArgs e)
        {
            int pageSize = int.Parse(textBoxPageSize.Text);
            //最終ページの計算
            int pageNo = (int)Math.Ceiling(Stock.Count / (double)pageSize) - 1;
            dataGridViewStock.DataSource = Stock.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewStock.Refresh();
            //ページ番号の設定
            textBoxPageNo.Text = (pageNo + 1).ToString();
        }

        private void dataGridViewStock_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //クリックされた行データをテキストボックスへ
            textBoxStID.Text = dataGridViewStock.Rows[dataGridViewStock.CurrentRow.Index].Cells[0].Value.ToString();
            textBoxPrID.Text = dataGridViewStock.Rows[dataGridViewStock.CurrentRow.Index].Cells[1].Value.ToString();
            textBoxStQuantity.Text = dataGridViewStock.Rows[dataGridViewStock.CurrentRow.Index].Cells[2].Value.ToString();
            int StFlg2 = int.Parse(dataGridViewStock.Rows[dataGridViewStock.CurrentRow.Index].Cells[3].Value.ToString());
            if (StFlg2 == 0)
            {
                checkBoxStFlag.Checked = false;
            }
            else if (StFlg2 == 2)
            {
                checkBoxStFlag.Checked = true;
            }
            textBoxStHidden.Text = dataGridViewStock.Rows[dataGridViewStock.CurrentRow.Index].Cells[4].Value.ToString();
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
                        labelPrName.Text = "(非表示)" + mPrName;
                        labelPrName.Visible = true;
                        context.Dispose();
                    }
                    else
                    {
                        string mPrName = mProduct.PrName;
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
