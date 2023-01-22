using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagement_SysDev.Forms.NonMaster.FormSale
{
    public partial class F_Sale : Form
    {
        //メッセージ表示用クラスのインスタンス化
        MessageDsp messageDsp = new MessageDsp();
        //データベース役職テーブルアクセス用クラスのインスタンス化
        DbAccess.SaleDataAccess saleDataAccess = new DbAccess.SaleDataAccess();
        //入力形式チェック用クラスのインスタンス化
        DataInputFormCheck dataInputFormCheck = new DataInputFormCheck();
        //データグリッドビュー用の役職データ
        private static List<T_SaleDsp> Sale;
        private static List<T_SaleDsp> filteredList;
        //管理フラグを数値型で入れるための変数
        internal static int SaFlg;
        int SaStateFlg;
        int CopySaID;
        internal static int stflg = 0;

        public F_Sale()
        {
            InitializeComponent();
        }

        private void F_Sale_Load(object sender, EventArgs e)
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
            textBoxPageSize.Text = "15";
            //dataGridViewのページ番号指定
            textBoxPageNo.Text = "1";
            //読み取り専用に指定
            dataGridViewSale.ReadOnly = true;
            //行内をクリックすることで行を選択
            dataGridViewSale.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //ヘッダー位置の指定
            dataGridViewSale.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

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
            Sale = saleDataAccess.GetSaleData();

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
            if (textBoxPageSize.Text == "" || textBoxPageSize.Text == "0" || textBoxPageSize.TextLength > 9) //Int32の最大値は 2,147,483,647
            {
                textBoxPageSize.Text = "15";
            }
            if (textBoxPageNo.Text == "" || textBoxPageNo.Text == "0" || int.Parse(textBoxPageSize.Text) > 9)
            {
                textBoxPageNo.Text = "1";
            }

            int pageSize = int.Parse(textBoxPageSize.Text);
            int pageNo = int.Parse(textBoxPageNo.Text) - 1;
            filteredList = Sale.Where(x => x.SaFlag != 2).ToList(); //OrFlagが2のレコードは排除する
            dataGridViewSale.DataSource = filteredList.Skip(pageSize * pageNo).Take(pageSize).ToList();

            if (filteredList.Count == 0)
            {
                labelNoTable.Visible = true;
            }
            else
            {
                labelNoTable.Visible = false;
            }

            //各列幅の指定
            //dataGridViewSale.Columns[0].Width = 100;
            //dataGridViewSale.Columns[1].Width = 100;
            //dataGridViewSale.Columns[2].Width = 100;
            //dataGridViewSale.Columns[3].Width = 100;
            //dataGridViewSale.Columns[4].Width = 100;
            //dataGridViewSale.Columns[5].Width = 130;
            //dataGridViewSale.Columns[6].Width = 400;
            //dataGridViewSale.Columns[7].Width = 110;
            //dataGridViewSale.Columns[8].Width = 100;
            //dataGridViewSale.Columns[9].Width = 100;
            //dataGridViewSale.Columns[10].Width = 70;
            //dataGridViewSale.Columns[11].Width = 90;

            // 自動サイズ調整を有効にする
            dataGridViewSale.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //各列の文字位置の指定
            dataGridViewSale.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewSale.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewSale.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewSale.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewSale.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewSale.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewSale.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewSale.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewSale.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewSale.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewSale.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewSale.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //dataGridViewの総ページ数
            labelPage.Text = "/" + ((int)Math.Ceiling(filteredList.Count / (double)pageSize)) + "ページ";

            dataGridViewSale.Refresh();
        }

        private void buttonPageSizeChange_Click(object sender, EventArgs e)
        {
            panelTotalPrice.Visible = false;
            labelID.Text = "111111";
            labelPrice.Text = "100000000 円";

            SetDataGridView();
        }

        private void buttonFirstPage_Click(object sender, EventArgs e)
        {
            panelTotalPrice.Visible = false;
            labelID.Text = "111111";
            labelPrice.Text = "100000000 円";

            if (textBoxPageSize.Text == "" || textBoxPageSize.Text == "0" || textBoxPageSize.TextLength > 9) //Int32の最大値は 2,147,483,647
            {
                textBoxPageSize.Text = "15";
            }
            int pageSize = int.Parse(textBoxPageSize.Text);
            dataGridViewSale.DataSource = filteredList.Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewSale.Refresh();
            //ページ番号の設定
            textBoxPageNo.Text = "1";
        }

        private void buttonPreviousPage_Click(object sender, EventArgs e)
        {
            panelTotalPrice.Visible = false;
            labelID.Text = "111111";
            labelPrice.Text = "100000000 円";

            if (textBoxPageSize.Text == "" || textBoxPageSize.Text == "0" || textBoxPageSize.TextLength > 9) //Int32の最大値は 2,147,483,647
            {
                textBoxPageSize.Text = "15";
            }
            if (textBoxPageNo.Text == "" || textBoxPageNo.Text == "0" || int.Parse(textBoxPageSize.Text) > 9)
            {
                textBoxPageNo.Text = "1";
            }
            int pageSize = int.Parse(textBoxPageSize.Text);
            int pageNo = int.Parse(textBoxPageNo.Text) - 2;
            dataGridViewSale.DataSource = filteredList.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewSale.Refresh();
            //ページ番号の設定
            if (pageNo + 1 > 1)
                textBoxPageNo.Text = (pageNo + 1).ToString();
            else
                textBoxPageNo.Text = "1";
        }

        private void buttonNextPage_Click(object sender, EventArgs e)
        {
            panelTotalPrice.Visible = false;
            labelID.Text = "111111";
            labelPrice.Text = "100000000 円";

            if (textBoxPageSize.Text == "" || textBoxPageSize.Text == "0" || textBoxPageSize.TextLength > 9) //Int32の最大値は 2,147,483,647
            {
                textBoxPageSize.Text = "15";
            }
            if (textBoxPageNo.Text == "" || textBoxPageNo.Text == "0" || int.Parse(textBoxPageSize.Text) > 9)
            {
                textBoxPageNo.Text = "1";
            }
            int pageSize = int.Parse(textBoxPageSize.Text);
            int pageNo = int.Parse(textBoxPageNo.Text);
            //最終ページの計算
            int lastNo = (int)Math.Ceiling(filteredList.Count / (double)pageSize) - 1;
            //最終ページでなければ
            if (pageNo <= lastNo)
                dataGridViewSale.DataSource = filteredList.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewSale.Refresh();
            //ページ番号の設定
            int lastPage = (int)Math.Ceiling(filteredList.Count / (double)pageSize);
            if (pageNo >= lastPage)
                textBoxPageNo.Text = lastPage.ToString();
            else
                textBoxPageNo.Text = (pageNo + 1).ToString();
        }

        private void buttonLastPage_Click(object sender, EventArgs e)
        {
            panelTotalPrice.Visible = false;
            labelID.Text = "111111";
            labelPrice.Text = "100000000 円";

            if (textBoxPageSize.Text == "" || textBoxPageSize.Text == "0" || textBoxPageSize.TextLength > 9) //Int32の最大値は 2,147,483,647
            {
                textBoxPageSize.Text = "15";
            }
            if (textBoxPageNo.Text == "" || textBoxPageNo.Text == "0" || int.Parse(textBoxPageSize.Text) > 9)
            {
                textBoxPageNo.Text = "1";
            }
            int pageSize = int.Parse(textBoxPageSize.Text);
            //最終ページの計算
            int pageNo = (int)Math.Ceiling(filteredList.Count / (double)pageSize) - 1;
            dataGridViewSale.DataSource = filteredList.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewSale.Refresh();
            //ページ番号の設定
            textBoxPageNo.Text = (pageNo + 1).ToString();
        }

        private void label顧客ID_Click(object sender, EventArgs e)
        {
            OpenForm(((Label)sender).Text);
        }

        private void label5_Click(object sender, EventArgs e)
        {
            OpenForm(((Label)sender).Text);
        }

        private void label2営業所ID_Click(object sender, EventArgs e)
        {
            OpenForm(((Label)sender).Text);
        }

        private void label6_Click(object sender, EventArgs e)
        {
            OpenForm(((Label)sender).Text);
        }

        private void label12_Click(object sender, EventArgs e)
        {
            OpenForm(((Label)sender).Text);
        }

        private void OpenForm(string formName)
        {
            Form frm = new Form();
            //引数より、開くフォームを設定
            switch (formName)
            {
                case "顧客ID": //ボタンのテキスト名
                    frm = new Master.FormClient.F_Client(); //フォームの名前
                    break;
                case "商品ID": //ボタンのテキスト名
                    frm = new Master.FormProduct.F_Product(); //フォームの名前
                    break;
                case "受注社員ID": //ボタンのテキスト名
                    frm = new Master.FormEmployee.F_Employee(); //フォームの名前
                    break;
                case "受注ID": //ボタンのテキスト名
                    frm = new FormOrder.F_Order(); //フォームの名前
                    break;
                case "営業所ID": //ボタンのテキスト名
                    frm = new Master.FormEmployee.F_SalesOffice(); //フォームの名前
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
            // 売上IDの適否
            if (!String.IsNullOrEmpty(textBoxSaID.Text.Trim()))
            {
                // 顧客IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxSaID.Text.Trim()))
                {
                    //MessageBox.Show("顧客IDは全て半角数字入力です");
                    messageDsp.DspMsg("M14001");
                    textBoxSaID.Focus();
                    return false;
                }
            }

            // 顧客IDの適否
            if (!String.IsNullOrEmpty(textBoxClID.Text.Trim()))
            {
                // 顧客IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxClID.Text.Trim()))
                {
                    //MessageBox.Show("顧客IDは全て半角数字入力です");
                    messageDsp.DspMsg("M14001");
                    textBoxClID.Focus();
                    return false;
                }
                // 顧客IDの文字数チェック
                if (textBoxClID.TextLength > 6)
                {
                    //MessageBox.Show("顧客IDは6文字です");
                    messageDsp.DspMsg("M14002");
                    textBoxClID.Focus();
                    return false;
                }
            }

            // 営業所IDの適否
            if (!String.IsNullOrEmpty(textBoxSoID.Text.Trim()))
            {
                // 営業所IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxSoID.Text.Trim()))
                {
                    //MessageBox.Show("営業所IDは全て半角数字入力です");
                    messageDsp.DspMsg("M14009");
                    textBoxSoID.Focus();
                    return false;
                }
                // 営業所IDの文字数チェック
                if (textBoxSoID.TextLength > 2)
                {
                    //MessageBox.Show("営業所IDは2文字です");
                    messageDsp.DspMsg("M14010");
                    textBoxSoID.Focus();
                    return false;
                }
            }

            // 社員IDの適否
            if (!String.IsNullOrEmpty(textBoxEmID.Text.Trim()))
            {
                // 社員IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxEmID.Text.Trim()))
                {
                    //MessageBox.Show("社員IDは全て半角数字入力です");
                    messageDsp.DspMsg("M14005");
                    textBoxEmID.Focus();
                    return false;
                }
                // 社員IDの文字数チェック
                if (textBoxEmID.TextLength > 6)
                {
                    //MessageBox.Show("社員IDは6文字です");
                    messageDsp.DspMsg("M14006");
                    textBoxEmID.Focus();
                    return false;
                }
            }

            // 受注IDの適否
            if (!String.IsNullOrEmpty(textBoxChID.Text.Trim()))
            {
                // 顧客IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxChID.Text.Trim()))
                {
                    //MessageBox.Show("顧客IDは全て半角数字入力です");
                    messageDsp.DspMsg("M14001");
                    textBoxChID.Focus();
                    return false;
                }
                // 顧客IDの文字数チェック
                if (textBoxChID.TextLength > 6)
                {
                    //MessageBox.Show("顧客IDは6文字です");
                    messageDsp.DspMsg("M14002");
                    textBoxChID.Focus();
                    return false;
                }
            }

            // 売上詳細IDの未入力チェック
            if (!String.IsNullOrEmpty(textBoxSaDetailID.Text.Trim()))
            {
                // 売上詳細IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxSaDetailID.Text.Trim()))
                {
                    //MessageBox.Show("売上詳細IDは全て半角数字入力です");
                    messageDsp.DspMsg("M14055");
                    textBoxSaDetailID.Focus();
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
                    messageDsp.DspMsg("M14016");
                    textBoxPrID.Focus();
                    return false;
                }
                // 商品IDの文字数チェック
                if (textBoxPrID.TextLength > 6)
                {
                    //MessageBox.Show("商品IDは6文字です");
                    messageDsp.DspMsg("M14017");
                    textBoxPrID.Focus();
                    return false;
                }
            }

            if (dateTimePickerSadDate.Checked == true)
            {
                //MessageBox.Show("注文年月日は検索対象外です");
                messageDsp.DspMsg("M14062");
                dateTimePickerSadDate.Focus();
                return false;
            }
            if (textBoxSaQuantity.Text != "")
            {
                //MessageBox.Show("数量は検索対象外です");
                messageDsp.DspMsg("M14063");
                textBoxSaQuantity.Focus();
                return false;
            }

            // 管理フラグの適否
            if (checkBoxSaFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("管理フラグが不確定の状態です");
                messageDsp.DspMsg("M14028");
                checkBoxSaFlag.Focus();
                return false;
            }
            return true;
        }

        ///////////////////////////////
        //　8.2.4.2 受注情報抽出
        //メソッド名：GenerateDataAtSelect()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：受注情報の取得
        ///////////////////////////////

        //cSoIDを別クラス(SalesOfficeDataAccess)でも使用できるように定義
        public static string mSaID;
        public static string mClID;
        public static string mSoID;
        public static string mEmID;
        public static string mChID;
        public static int mSaFlg;
        public static string mSaDetailID;
        public static string mPrID;
        private void GenerateDataAtSelect()
        {
            T_SaleDsp selectCondition;

            //boolからintに変換して検索条件セット準備
            if (checkBoxSaFlag.Checked == true)
            {
                mSaFlg = 2;
            }
            else if (checkBoxSaFlag.Checked == false)
            {
                mSaFlg = 0;
            }
            mSaID = textBoxSaID.Text.Trim();
            mClID = textBoxClID.Text.Trim();
            mSoID = textBoxSoID.Text.Trim();
            mEmID = textBoxEmID.Text.Trim();
            mChID = textBoxChID.Text.Trim();
            mSaDetailID = textBoxSaDetailID.Text.Trim();
            mPrID = textBoxPrID.Text.Trim();

            if (mSaID != "")
            {
                // 検索条件のセット
                selectCondition = new T_SaleDsp()
                {
                    SaID = int.Parse(textBoxSaID.Text.Trim()),
                    SaFlag = mSaFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Sale = saleDataAccess.SearchSaleData(selectCondition);
                return;
            }
            else if (mSaDetailID != "")
            {
                // 検索条件のセット
                selectCondition = new T_SaleDsp()
                {
                    SaDetailID = int.Parse(textBoxSaDetailID.Text.Trim()),
                    SaFlag = mSaFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Sale = saleDataAccess.SearchSaleData(selectCondition);
                return;
            }
            else if (mSoID != "")
            {
                // 検索条件のセット
                selectCondition = new T_SaleDsp()
                {
                    SoID = int.Parse(textBoxSoID.Text.Trim()),
                    SaFlag = mSaFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Sale = saleDataAccess.SearchSaleData(selectCondition);
                return;
            }
            else if (mEmID != "")
            {
                // 検索条件のセット
                selectCondition = new T_SaleDsp()
                {
                    EmID = int.Parse(textBoxEmID.Text.Trim()),
                    SaFlag = mSaFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Sale = saleDataAccess.SearchSaleData(selectCondition);
                return;
            }
            else if (mClID != "")
            {
                // 検索条件のセット
                selectCondition = new T_SaleDsp()
                {
                    ClID = int.Parse(textBoxClID.Text.Trim()),
                    SaFlag = mSaFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Sale = saleDataAccess.SearchSaleData(selectCondition);
                return;
            }
            else if (mChID != "")
            {
                // 検索条件のセット
                selectCondition = new T_SaleDsp()
                {
                    ChID = int.Parse(textBoxChID.Text.Trim()),
                    SaFlag = mSaFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Sale = saleDataAccess.SearchSaleData(selectCondition);
                return;
            }
            else if (mSaFlg == 2)
            {
                // 検索条件のセット
                selectCondition = new T_SaleDsp()
                {
                    SaFlag = mSaFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Sale = saleDataAccess.SearchSaleData(selectCondition);
                return;
            }
            else if (mPrID != "")
            {
                // 検索条件のセット
                selectCondition = new T_SaleDsp()
                {
                    PrID = int.Parse(textBoxPrID.Text.Trim()),
                    SaFlag = mSaFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Sale = saleDataAccess.SearchSaleData(selectCondition);
                return;
            }
        }
        ///////////////////////////////
        //　8.2.4.3 受注抽出結果表示
        //メソッド名：SetSelectData()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：受注情報の表示
        ///////////////////////////////
        private void SetSelectData()
        {
            panelTotalPrice.Visible = false;
            labelID.Text = "111111";
            labelPrice.Text = "100000000 円";

            textBoxPageNo.Text = "1";

            int pageSize = int.Parse(textBoxPageSize.Text);

            dataGridViewSale.DataSource = Sale;
            if (Sale.Count == 0)
            {
                labelNoTable.Visible = true;
            }
            else
            {
                labelNoTable.Visible = false;
            }

            labelPage.Text = "/" + ((int)Math.Ceiling(Sale.Count / (double)pageSize)) + "ページ";
            dataGridViewSale.Refresh();

            if (Sale.Count == 0) //検索結果のデータ数が0ならエラー
            {
                messageDsp.DspMsg("M14037");
                SetFormDataGridView();
            }
        }


        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            // 8.2.2.1 妥当な受注データ取得
            if (!GetValidDataAtUpdate())
                return;

            // 8.2.2.2 受注情報作成
            var updSale = GenerateDataAtUpdate();

            // 8.2.2.3 受注情報削除
            DeleteSale(updSale);
        }

        ///////////////////////////////
        //　8.2.2.1 妥当な受注データ取得
        //メソッド名：GetValidDataAtUpdate()
        //引　数   ：なし
        //戻り値   ：true or false
        //機　能   ：入力データの形式チェック
        //          ：エラーがない場合True
        //          ：エラーがある場合False
        ///////////////////////////////
        private bool GetValidDataAtUpdate()
        {
            // 売上IDの適否
            if (!String.IsNullOrEmpty(textBoxSaID.Text.Trim()))
            {
                // 売上IDの文字数チェック
                if (textBoxSaID.TextLength > 6)
                {
                    //MessageBox.Show("売上IDは6文字です");
                    messageDsp.DspMsg("M14002");
                    textBoxSaID.Focus();
                    return false;
                }
                // 売上IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxSaID.Text.Trim()))
                {
                    //MessageBox.Show("売上IDは全て半角数字入力です");
                    messageDsp.DspMsg("M14001");
                    textBoxSaID.Focus();
                    return false;
                }
                // 売上IDの存在チェック
                if (!saleDataAccess.CheckSaleCDExistence(textBoxSaID.Text.Trim()))
                {
                    //MessageBox.Show("入力された売上IDは存在しません");
                    messageDsp.DspMsg("M14050");
                    textBoxSaID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("売上IDが入力されていません");
                messageDsp.DspMsg("M14051");
                textBoxSaID.Focus();
                return false;
            }

            // 受注IDの未入力チェック
            if (!String.IsNullOrEmpty(textBoxChID.Text.Trim()))
            {
                // 受注IDの文字数チェック
                if (textBoxChID.TextLength > 6)
                {
                    //MessageBox.Show("受注IDは6文字です");
                    messageDsp.DspMsg("M14064");
                    textBoxChID.Focus();
                    return false;
                }
                // 受注IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxChID.Text.Trim()))
                {
                    //MessageBox.Show("受注IDは全て半角数字入力です");
                    messageDsp.DspMsg("M14065");
                    textBoxChID.Focus();
                    return false;
                }
                // 受注IDの存在チェック
                if (!saleDataAccess.CheckOrderIDExistence(textBoxSaID.Text.Trim(), textBoxChID.Text.Trim()))
                {
                    //MessageBox.Show("売上IDに関連する受注IDが一致しません");
                    messageDsp.DspMsg("M14066");
                    textBoxChID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("受注IDが入力されていません");
                messageDsp.DspMsg("M14067");
                textBoxChID.Focus();
                return false;
            }

            // 非表示理由の適否
            if (checkBoxSaFlag.Checked == true)
            {
                if (textBoxSaHidden.Text == "")
                {
                    //MessageBox.Show("非表示理由の入力が必要です");
                    messageDsp.DspMsg("M14030");
                    textBoxSaHidden.Focus();
                    return false;
                }
            }

            // 管理フラグの適否
            if (checkBoxSaFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("管理フラグが不確定の状態です");
                messageDsp.DspMsg("M14028");
                checkBoxSaFlag.Focus();
                return false;
            }

            // 管理フラグの適否２
            if (checkBoxSaFlag.Checked == false)
            {
                //MessageBox.Show("管理フラグが未チェックです");
                messageDsp.DspMsg("M14068");
                checkBoxSaFlag.Focus();
                return false;
            }
            return true;
        }
        ///////////////////////////////
        //　8.2.2.2 受注情報作成
        //メソッド名：GenerateDataAtUpdate()
        //引　数   ：なし
        //戻り値   ：受注更新情報
        //機　能   ：更新データのセット
        ///////////////////////////////
        private T_Sale GenerateDataAtUpdate()
        {
            return new T_Sale
            {
                SaID = int.Parse(textBoxSaID.Text.Trim()),
                ChID = int.Parse(textBoxChID.Text.Trim()),
                SaFlag = SaFlg,
                SaHidden = textBoxSaHidden.Text.Trim()
            };
        }

        ///////////////////////////////
        //　8.2.2.3 受注情報更新
        //メソッド名：UpdatePosition()
        //引　数   ：受注情報
        //戻り値   ：なし
        //機　能   ：受注情報の更新
        ///////////////////////////////
        private void DeleteSale(T_Sale updSale)
        {
            // 更新確認メッセージ
            DialogResult result = messageDsp.DspMsg("M14038");
            if (result == DialogResult.Cancel)
            {
                return;
            }

            // 受注情報の更新
            bool flg = saleDataAccess.UpdateSaleData(updSale);
            if (flg == true)
            {
                //MessageBox.Show("データを非表示しました。");
                messageDsp.DspMsg("M14039");
            }
            else
            {
                //MessageBox.Show("データの非表示に失敗しました。");
                messageDsp.DspMsg("M14040");
            }

            panelTotalPrice.Visible = false;
            labelID.Text = "111111";
            labelPrice.Text = "100000000 円";

            textBoxSaID.Focus();

            // 入力エリアのクリア
            ClearInput();

            // データグリッドビューの表示
            GetDataGridView();

            if (System.Windows.Forms.Form.ActiveForm != null) //nullエラー防止(特定の操作を行うと何故かnullが返り、例外エラーが出る
            {
                //開いている画面も自動リロード
                ActForm();
            }
        }

        private void ActForm()
        {
            NonMaster.FormOrder.F_Order.stflg = 1;
            NonMaster.FormOrder.F_Order.ActiveForm.Activate();
            NonMaster.FormChumon.F_Chumon.stflg = 1;
            NonMaster.FormChumon.F_Chumon.ActiveForm.Activate();
            NonMaster.FormSyukko.F_Syukko.stflg = 1;
            NonMaster.FormSyukko.F_Syukko.ActiveForm.Activate();
            NonMaster.FormArrival.F_Arrival.stflg = 1;
            NonMaster.FormArrival.F_Arrival.ActiveForm.Activate();
            NonMaster.FormShipment.F_Shipment.stflg = 1;
            NonMaster.FormShipment.F_Shipment.ActiveForm.Activate();
            Master.FormStock.F_Stock.stflg = 1;
            Master.FormStock.F_Stock.ActiveForm.Activate();
        }

        private void buttonList_Click(object sender, EventArgs e)
        {
            panelTotalPrice.Visible = false;
            labelID.Text = "111111";
            labelPrice.Text = "100000000 円";

            // データグリッドビューの表示
            SetFormDataGridView();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            // 入力エリアのクリア
            ClearInput();
            // データグリッドビューの表示
            SetFormDataGridView();

            panelTotalPrice.Visible = false;
            labelID.Text = "111111";
            labelPrice.Text = "100000000 円";
        }

        private void ClearInput()
        {
            textBoxSaID.Text = "";
            textBoxClID.Text = "";
            textBoxSoID.Text = "";
            textBoxEmID.Text = "";
            textBoxChID.Text = "";
            dateTimePickerSadDate.Value = DateTime.Now;
            dateTimePickerSadDate.Checked = false;
            textBoxSaHidden.Text = "";
            checkBoxSaFlag.Checked = false;
            textBoxSaDetailID.Text = "";
            textBoxPrID.Text = "";
            textBoxSaQuantity.Text = "";
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBoxSaFlag_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSaFlag.Checked == true)
            {
                SaFlg = 2;
                textBoxSaHidden.Enabled = true;
                return;
            }
            else if (checkBoxSaFlag.Checked == false)
            {
                SaFlg = 0;
                textBoxSaHidden.Enabled = false;
                textBoxSaHidden.Text = "";
                return;
            }
        }

        private void dataGridViewSale_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewSale.CurrentRow != null)
            {
                //クリックされた行データをテキストボックスへ
                textBoxSaID.Text = dataGridViewSale.Rows[dataGridViewSale.CurrentRow.Index].Cells[0].Value.ToString();
                textBoxClID.Text = dataGridViewSale.Rows[dataGridViewSale.CurrentRow.Index].Cells[1].Value.ToString();
                textBoxSoID.Text = dataGridViewSale.Rows[dataGridViewSale.CurrentRow.Index].Cells[2].Value.ToString();
                textBoxEmID.Text = dataGridViewSale.Rows[dataGridViewSale.CurrentRow.Index].Cells[3].Value.ToString();
                textBoxChID.Text = dataGridViewSale.Rows[dataGridViewSale.CurrentRow.Index].Cells[4].Value.ToString();

                //日付が設定されていない場合、初期値として現在の日付を設定
                if (dataGridViewSale.Rows[dataGridViewSale.CurrentRow.Index].Cells[5].Value == null)
                {
                    dateTimePickerSadDate.Value = DateTime.Now;
                    dateTimePickerSadDate.Checked = false;
                }
                else
                {
                    dateTimePickerSadDate.Text = dataGridViewSale.Rows[dataGridViewSale.CurrentRow.Index].Cells[5].Value.ToString();
                }

                textBoxSaHidden.Text = dataGridViewSale.Rows[dataGridViewSale.CurrentRow.Index].Cells[6].Value.ToString();

                //管理フラグの数値型をbool型に変換して取得
                int OrFlg2 = int.Parse(dataGridViewSale.Rows[dataGridViewSale.CurrentRow.Index].Cells[7].Value.ToString());
                if (OrFlg2 == 0)
                {
                    checkBoxSaFlag.Checked = false;
                }
                else if (OrFlg2 == 2)
                {
                    checkBoxSaFlag.Checked = true;
                }

                textBoxSaDetailID.Text = dataGridViewSale.Rows[dataGridViewSale.CurrentRow.Index].Cells[8].Value.ToString();
                textBoxPrID.Text = dataGridViewSale.Rows[dataGridViewSale.CurrentRow.Index].Cells[9].Value.ToString();
                int SaQuantity2 = int.Parse(dataGridViewSale.Rows[dataGridViewSale.CurrentRow.Index].Cells[10].Value.ToString());
                int SaPrice2 = int.Parse(dataGridViewSale.Rows[dataGridViewSale.CurrentRow.Index].Cells[11].Value.ToString());
                int SaPrTotalPrice2 = SaPrice2 / SaQuantity2;
                textBoxSaQuantity.Text = dataGridViewSale.Rows[dataGridViewSale.CurrentRow.Index].Cells[10].Value.ToString();
                textBoxSaPrTotalPrice.Text = SaPrTotalPrice2.ToString();
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
                    textBoxSoID.Text = "";
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
                }
                catch
                {
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

        private void textBoxChID_TextChanged(object sender, EventArgs e)
        {
            if (textBoxChID.Text != "")
            {
                var context = new SalesManagement_DevContext();
                try
                {
                    labelStateFlag.Text = "確定済"; //ラベルがUnknow状態で再度入力実行されたときに確定済に戻しておく
                    int mChID = int.Parse(textBoxChID.Text);
                    var mOrder = context.T_Orders.Single(x => x.OrID == mChID); //入力されたChIDで一致する一件のレコードを探す
                    if (mOrder.OrFlag == 2 && mOrder.OrStateFlag == 1) //確定と非表示両方表示
                    {
                        labelFlag.Visible = true;
                        labelStateFlag.Visible = true;
                    }
                    else
                    {
                        if (mOrder.OrFlag == 2) //管理フラグを表示するか
                        {
                            labelFlag.Visible = true;
                        }
                        else
                        {
                            labelFlag.Visible = false;
                        }
                        if (mOrder.OrStateFlag == 1) //状態フラグを表示するか
                        {
                            labelStateFlag.Visible = true;
                        }
                        else
                        {
                            labelStateFlag.Visible = false;
                        }
                    }
                    int mClID = mOrder.ClID; //関連するIDを取得
                    int mEmID = mOrder.EmID; //上記同様
                    int mSoID = mOrder.SoID; //上記同様
                    textBoxClID.Text = mClID.ToString(); //取得したIDを自動入力
                    textBoxEmID.Text = mEmID.ToString(); //上記同様
                    textBoxSoID.Text = mSoID.ToString(); //上記同様
                    context.Dispose();
                }
                catch
                {
                    labelFlag.Visible = false;
                    labelStateFlag.Visible = true;
                    labelStateFlag.Text = "“UnknownID”";
                    textBoxClID.Text = "";
                    textBoxEmID.Text = "";
                    textBoxSoID.Text = "";
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
                        textBoxSaPrTotalPrice.Text = mPrice.ToString();
                        context.Dispose();
                    }
                    else
                    {
                        string mPrName = mProduct.PrName;
                        int mPrice = mProduct.Price;
                        labelPrName.Text = mPrName;
                        textBoxSaPrTotalPrice.Text = mPrice.ToString();
                        labelPrName.Visible = true;
                        context.Dispose();
                    }
                }
                catch
                {
                    labelPrName.Text = "“UnknownID”";
                    labelPrName.Visible = true;
                    textBoxSaPrTotalPrice.Text = "“UnknownID”";
                    context.Dispose();
                    return;
                }
            }
            else
            {
                labelPrName.Visible = false;
                labelPrName.Text = "商品名";
                textBoxSaPrTotalPrice.Text = "";
            }
        }

        private void label顧客ID_MouseEnter(object sender, EventArgs e)
        {
            label顧客ID.BackColor = Color.Aqua;
        }

        private void label顧客ID_MouseLeave(object sender, EventArgs e)
        {
            label顧客ID.BackColor = Color.Transparent;
        }

        private void label5_MouseEnter(object sender, EventArgs e)
        {
            label5.BackColor = Color.Aqua;
        }

        private void label5_MouseLeave(object sender, EventArgs e)
        {
            label5.BackColor = Color.Transparent;
        }

        private void label2営業所ID_MouseEnter(object sender, EventArgs e)
        {
            label2営業所ID.BackColor = Color.Aqua;
        }

        private void label2営業所ID_MouseLeave(object sender, EventArgs e)
        {
            label2営業所ID.BackColor = Color.Transparent;
        }

        private void label6_MouseEnter(object sender, EventArgs e)
        {
            label6.BackColor = Color.Aqua;
        }

        private void label6_MouseLeave(object sender, EventArgs e)
        {
            label6.BackColor = Color.Transparent;
        }

        private void label12_MouseEnter(object sender, EventArgs e)
        {
            label12.BackColor = Color.Aqua;
        }

        private void label12_MouseLeave(object sender, EventArgs e)
        {
            label12.BackColor = Color.Transparent;
        }

        private void F_Sale_Activated(object sender, EventArgs e)
        {
            labelEmpName.Text = F_menu.loginName;
            labelEmpID.Text = F_menu.loginEmID;
            labelOfficeName.Text = F_menu.loginSalesOffice;
            if (!F_menu.loginSalesOffice.Contains("営業所"))
            {
                buttonUpdate.Enabled = false;
                buttonUpdate.BackgroundImage = Properties.Resources.Fixed_キャンセル使用不可;
            }
            if(F_Login.SysMode == 1) //開発者モード
            {
                buttonUpdate.Enabled = true;
                buttonUpdate.BackgroundImage = Properties.Resources.Fixed_削除;
            }
            if (stflg == 1)
            {
                GetDataGridView();
                stflg = 0;
            }
        }

        private void textBoxSaID_TextChanged(object sender, EventArgs e)
        {
            string saID = textBoxSaID.Text;
            try
            {
                var context = new SalesManagement_DevContext();
                var saleDetail = context.T_SaleDetails.Where(x => x.SaID.ToString() == saID).ToList();
                if(saleDetail.Count == 0)
                {
                    panelTotalPrice.Visible = false;
                    labelID.Text = "111111";
                    labelPrice.Text = "100000000 円";
                }
                else
                {
                    int total = 0;

                    foreach (var p in saleDetail)
                    {
                        total = total + p.SaPrTotalPrice;
                    }

                    panelTotalPrice.Visible = true;
                    labelID.Text = saID;
                    labelPrice.Text = total.ToString() + " 円";
                }
                context.Dispose();
            }
            catch
            {
                MessageBox.Show("売上金額の計算に失敗しました", "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
