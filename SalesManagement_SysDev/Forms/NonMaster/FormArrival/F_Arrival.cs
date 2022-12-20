using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagement_SysDev.Forms.NonMaster.FormArrival
{
    public partial class F_Arrival : Form
    {
        //メッセージ表示用クラスのインスタンス化
        MessageDsp messageDsp = new MessageDsp();
        //データベース受注テーブルアクセス用クラスのインスタンス化
        DbAccess.ArrivalDataAccess arrivalDataAccess = new DbAccess.ArrivalDataAccess();
        //入力形式チェック用クラスのインスタンス化
        DataInputFormCheck dataInputFormCheck = new DataInputFormCheck();
        //データグリッドビュー用の受注データ
        private static List<T_ArrivalDsp> Arrival;
        private static List<T_ArrivalDsp> filteredList;
        //フラグを数値型で入れるための変数
        int ArStateFlg = 0;
        int ArFlg = 0;

        public F_Arrival()
        {
            InitializeComponent();
        }

        private void buttonCusSearch_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private void buttonProductSearch_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private void buttonEmSearch_Click(object sender, EventArgs e)
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
                case "入荷確定画面へ": //ボタンのテキスト名
                    frm = new F_ArrivalConfirm(); //フォームの名前
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

        private void F_Arrival_Load(object sender, EventArgs e)
        {
            //ログイン名の表示
            //labelLoginName.Text = FormMenu.loginName;

            // データグリッドビューの表示
            SetFormDataGridView();
        }

        ///////////////////////////////
        //
        //メソッド名：SetFormDataGridView()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：データグリッドビューの設定
        ///////////////////////////////
        private void SetFormDataGridView()
        {
            //dataGridViewのページサイズ指定
            textBoxPageSize.Text = "12";
            //dataGridViewのページ番号指定
            textBoxPageNo.Text = "1";
            //読み取り専用に指定
            dataGridViewArrival.ReadOnly = true;
            //行内をクリックすることで行を選択
            dataGridViewArrival.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //ヘッダー位置の指定
            dataGridViewArrival.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

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
            // 入荷データの取得
            Arrival = arrivalDataAccess.GetArrivalData();

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

            filteredList = Arrival.Where(x => x.ArFlag != 2).ToList(); //ArFlagが2のレコードは排除する
            dataGridViewArrival.DataSource = filteredList.Skip(pageSize * pageNo).Take(pageSize).ToList();

            //各列幅の指定
            dataGridViewArrival.Columns[0].Width = 100;
            dataGridViewArrival.Columns[1].Width = 100;
            dataGridViewArrival.Columns[2].Width = 100;
            dataGridViewArrival.Columns[3].Width = 100;
            dataGridViewArrival.Columns[4].Width = 200;
            dataGridViewArrival.Columns[5].Width = 130;
            dataGridViewArrival.Columns[6].Width = 110;
            dataGridViewArrival.Columns[7].Width = 110;
            dataGridViewArrival.Columns[8].Width = 400;
            dataGridViewArrival.Columns[9].Width = 100;
            dataGridViewArrival.Columns[10].Width = 100;
            dataGridViewArrival.Columns[11].Width = 70;
            //各列の文字位置の指定
            dataGridViewArrival.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewArrival.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewArrival.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewArrival.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewArrival.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewArrival.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewArrival.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewArrival.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewArrival.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewArrival.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewArrival.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewArrival.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dataGridViewの総ページ数
            labelPage.Text = "/" + ((int)Math.Ceiling(Arrival.Count / (double)pageSize)) + "ページ";

            dataGridViewArrival.Refresh();
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
        //　8.2.4.1 妥当な入荷データ取得
        //メソッド名：GetValidDataAtSlect()
        //引　数   ：なし
        //戻り値   ：true or false
        //機　能   ：入力データの形式チェック
        //          ：エラーがない場合True
        //          ：エラーがある場合False
        ///////////////////////////////
        private bool GetValidDataAtSelect()
        {

            // 入荷ID入力時チェック
            if (!String.IsNullOrEmpty(textBoxArID.Text.Trim()))
            {
                // 入荷IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxArID.Text.Trim()))
                {
                    //MessageBox.Show("入荷IDは全て半角数字入力です");
                    messageDsp.DspMsg("M11001");
                    textBoxArID.Focus();
                    return false;
                }
                // 入荷IDの文字数チェック
                if (textBoxArID.TextLength > 2)
                {
                    //MessageBox.Show("入荷IDは2文字までです");
                    messageDsp.DspMsg("M11002");
                    textBoxArID.Focus();
                    return false;
                }
            }
            // 受注IDの適否
            if (!String.IsNullOrEmpty(textBoxOrID.Text.Trim()))
            {
                // 受注IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxOrID.Text.Trim()))
                {
                    //MessageBox.Show("受注IDは全て半角数字入力です");
                    messageDsp.DspMsg("M10054");
                    textBoxOrID.Focus();
                    return false;
                }
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
                // {
                //MessageBox.Show("社員IDは01から割り当ててください");
                // messageDsp.DspMsg("M10007");
                //textBoxEmID.Focus();
                //return false;
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
            // 入荷詳細IDの未入力チェック
            if (!String.IsNullOrEmpty(textBoxArDetailID.Text.Trim()))
            {
                // 入荷詳細IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxArDetailID.Text.Trim()))
                {
                    //MessageBox.Show("受注詳細IDは全て半角数字入力です");
                    messageDsp.DspMsg("M10055");
                    textBoxArDetailID.Focus();
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
            if (!String.IsNullOrEmpty(textBoxArQuantity.Text.Trim()))
            {
                // 数量の半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxArQuantity.Text.Trim()))
                {
                    //MessageBox.Show("数量は全て半角数字入力です");
                    messageDsp.DspMsg("M10020");
                    textBoxArQuantity.Focus();
                    return false;
                }
                // 数量の文字数チェック
                if (textBoxArQuantity.TextLength > 4)
                {
                    //MessageBox.Show("数量は4文字です");
                    messageDsp.DspMsg("M10021");
                    textBoxArQuantity.Focus();
                    return false;
                }
            }
            if (textBoxArID.Text == "" && textBoxClID.Text == "" && checkBoxArFlag.Checked == false)
            {
                // データグリッドビューの表示
                SetFormDataGridView();
                return false;
            }
            // 状態フラグの適否
            if (checkBoxArStateFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("状態フラグが不確定の状態です");
                messageDsp.DspMsg("M10027");
                checkBoxArStateFlag.Focus();
                return false;
            }
            // 管理フラグの適否
            if (checkBoxArFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("管理フラグが不確定の状態です");
                messageDsp.DspMsg("M1008");
                checkBoxArFlag.Focus();
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
        public static string mArID; //cSoIDを別クラス(SalesOfficeDataAccess)でも使用できるように定義
        public static string mClID;
        public static int mArFlg;
        public static string mEmID;
        public static string mSoID;
        public static string mArDate;
        public static string mOrID;
        public static int? mArStateFlg;
        public static string mArDetailID;
        public static string mPrID;
        private void GenerateDataAtSelect()
        {
            T_ArrivalDsp selectCondition;

            //boolからintに変換して検索条件セット準備
            if (checkBoxArStateFlag.Checked == true)
            {
                mArStateFlg = 1;
            }
            else if (checkBoxArStateFlag.Checked == false)
            {
                mArStateFlg = null;
            }
            if (checkBoxArFlag.Checked == true)
            {
                mArFlg = 2;
            }
            else if (checkBoxArFlag.Checked == false)
            {
                mArFlg = 0;
            }
            mArID = textBoxArID.Text.Trim(); //IDはnullではなく、""で空白が入るのでstringで代入
            mClID = textBoxClID.Text.Trim();
            mEmID = textBoxEmID.Text.Trim();
            mSoID = textBoxSoID.Text.Trim();
            mArDate = dateTimePickerArDate.Text.Trim();
            mOrID = textBoxOrID.Text.Trim();
            mArDetailID = textBoxArDetailID.Text.Trim();
            mPrID = textBoxPrID.Text.Trim();

            if (mArID != "")
            {
                // 検索条件のセット
                selectCondition = new T_ArrivalDsp()
                {
                    ArID = int.Parse(textBoxArID.Text.Trim()),
                    ArStateFlag = mArStateFlg,
                    ArFlag = mArFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Arrival = arrivalDataAccess.SearchArrivalData(selectCondition);
                return;
            }
            else if (mArDetailID != "")
            {
                // 検索条件のセット
                selectCondition = new T_ArrivalDsp()
                {
                    ArDetailID = int.Parse(textBoxArDetailID.Text.Trim()),
                    ArStateFlag = mArStateFlg,
                    ArFlag = mArFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Arrival = arrivalDataAccess.SearchArrivalData(selectCondition);
                return;
            }
            else if (mSoID != "")
            {
                // 検索条件のセット
                selectCondition = new T_ArrivalDsp()
                {
                    SoID = int.Parse(textBoxSoID.Text.Trim()),
                    ArStateFlag = mArStateFlg,
                    ArFlag = mArFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Arrival = arrivalDataAccess.SearchArrivalData(selectCondition);
                return;
            }
            else if (mEmID != "")
            {
                // 検索条件のセット
                selectCondition = new T_ArrivalDsp()
                {
                    EmID = int.Parse(textBoxEmID.Text.Trim()),
                    ArStateFlag = mArStateFlg,
                    ArFlag = mArFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Arrival = arrivalDataAccess.SearchArrivalData(selectCondition);
                return;
            }
            else if (mClID != "")
            {
                // 検索条件のセット
                selectCondition = new T_ArrivalDsp()
                {
                    ClID = int.Parse(textBoxClID.Text.Trim()),
                    ArStateFlag = mArStateFlg,
                    ArFlag = mArFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Arrival = arrivalDataAccess.SearchArrivalData(selectCondition);
                return;
            }
            else if (mOrID != "")
            {
                // 検索条件のセット
                selectCondition = new T_ArrivalDsp()
                {
                    OrID = int.Parse(textBoxOrID.Text.Trim()),
                    ArStateFlag = mArStateFlg,
                    ArFlag = mArFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Arrival = arrivalDataAccess.SearchArrivalData(selectCondition);
                return;
            }
            else if (mArStateFlg == 1)
            {
                // 検索条件のセット
                selectCondition = new T_ArrivalDsp()
                {
                    ArStateFlag = mArStateFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Arrival = arrivalDataAccess.SearchArrivalData(selectCondition);
                return;
            }
            else if (mArFlg == 2)
            {
                // 検索条件のセット
                selectCondition = new T_ArrivalDsp()
                {
                    ArFlag = mArFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Arrival = arrivalDataAccess.SearchArrivalData(selectCondition);
                return;
            }
            else if (mPrID != "")
            {
                // 検索条件のセット
                selectCondition = new T_ArrivalDsp()
                {
                    PrID = int.Parse(textBoxPrID.Text.Trim()),
                    ArStateFlag = mArStateFlg,
                    ArFlag = mArFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Arrival = arrivalDataAccess.SearchArrivalData(selectCondition);
                return;
            }
        }
        //　8.2.4.3 入荷抽出結果表示
        //メソッド名：SetSelectData()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：入荷情報の表示
        ///////////////////////////////
        private void SetSelectData()
        {
            textBoxPageNo.Text = "1";

            int pageSize = int.Parse(textBoxPageSize.Text);

            dataGridViewArrival.DataSource = Arrival;

            labelPage.Text = "/" + ((int)Math.Ceiling(Arrival.Count / (double)pageSize)) + "ページ";
            dataGridViewArrival.Refresh();

            if (Arrival.Count == 0) //検索結果のデータ数が0ならエラー
            {
                messageDsp.DspMsg("M1025");
                SetFormDataGridView();
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            // 8.2.2.1 妥当な入荷データ取得
            if (!GetValidDataAtDelete())
                return;

            // 8.2.2.2 入荷情報作成
            var updArrival = GenerateDataAtDelete();

            // 8.2.2.3 入荷情報削除
            DeleteArrival(updArrival);
        }

        private bool GetValidDataAtDelete()
        {

            // 入荷IDの未入力チェック
            if (!String.IsNullOrEmpty(textBoxArID.Text.Trim()))
            {
                // 入荷IDの半角英数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxArID.Text.Trim()))
                {
                    //MessageBox.Show("入荷IDは全て半角英数字入力です");
                    messageDsp.DspMsg("M11001");
                    textBoxArID.Focus();
                    return false;
                }
                // 入荷IDの文字数チェック
                if (textBoxArID.TextLength > 2)
                {
                    //MessageBox.Show("入荷IDは2文字です");
                    messageDsp.DspMsg("M11002");
                    textBoxArID.Focus();
                    return false;
                }
                // 入荷IDの存在チェック
                if (!arrivalDataAccess.CheckArrivalCDExistence(textBoxArID.Text.Trim()))
                {
                    //MessageBox.Show("入力された入荷IDは存在しません");
                    messageDsp.DspMsg("M11013");
                    textBoxArID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("入荷IDが入力されていません");
                messageDsp.DspMsg("M11004");
                textBoxArID.Focus();
                return false;
            }

            // 入荷詳細IDの未入力チェック
            if (!String.IsNullOrEmpty(textBoxArDetailID.Text.Trim()))
            {
                // 入荷詳細IDの半角英数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxArDetailID.Text.Trim()))
                {
                    //MessageBox.Show("入荷詳細IDは全て半角英数字入力です");
                    messageDsp.DspMsg("M11005");
                    textBoxArDetailID.Focus();
                    return false;
                }
                // 入荷詳細IDの文字数チェック
                if (textBoxArDetailID.TextLength > 2)
                {
                    //MessageBox.Show("入荷詳細IDは2文字です");
                    messageDsp.DspMsg("M11006");
                    textBoxArDetailID.Focus();
                    return false;
                }
                // 入荷詳細IDの存在チェック
                if (!arrivalDataAccess.CheckArrivalCDExistence(textBoxArDetailID.Text.Trim()))
                {
                    //MessageBox.Show("入力された入荷詳細IDは存在しません");
                    messageDsp.DspMsg("M11007");
                    textBoxArDetailID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("入荷詳細IDが入力されていません");
                messageDsp.DspMsg("M11008");
                textBoxArDetailID.Focus();
                return false;
            }

            // 管理フラグの適否
            if (checkBoxArFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("管理フラグが不確定の状態です");
                messageDsp.DspMsg("M1008");
                checkBoxArFlag.Focus();
                return false;
            }

            // 管理フラグの適否２
            if (checkBoxArFlag.Checked == false)
            {
                //MessageBox.Show("管理フラグが未チェックです");
                messageDsp.DspMsg("M10029");
                checkBoxArFlag.Focus();
                return false;
            }

            // 非表示理由の適否
            if (checkBoxArFlag.Checked == true)
            {
                if (textBoxArHidden.Text == "")
                {
                    //MessageBox.Show("非表示理由の入力が必要です");
                    messageDsp.DspMsg("M1023");
                    textBoxArHidden.Focus();
                    return false;
                }
            }
            return true;
        }
        ///////////////////////////////
        //　8.2.2.2 入荷情報作成
        //メソッド名：GenerateDataAtUpdate()
        //引　数   ：なし
        //戻り値   ：入荷削除情報
        //機　能   ：削除データのセット
        ///////////////////////////////
        private T_Arrival GenerateDataAtDelete()
        {
            return new T_Arrival
            {
                ArID = int.Parse(textBoxArID.Text.Trim()),
                ArFlag = ArFlg,
                ArHidden = textBoxArHidden.Text.Trim()
            };
        }
        ///////////////////////////////
        //　8.2.2.3 入荷情報削除
        //メソッド名：UpdateArrival()
        //引　数   ：入荷情報
        //戻り値   ：なし
        //機　能   ：入荷情報の削除
        ///////////////////////////////
        private void DeleteArrival(T_Arrival delArrival)
        {

            // 削除確認メッセージ
            DialogResult result = messageDsp.DspMsg("M1018");
            if (result == DialogResult.Cancel)
                return;

            // 入荷情報の削除
            bool flg = arrivalDataAccess.DeleteArrivalData(delArrival);
            if (flg == true)
                //MessageBox.Show("データを削除しました。");
                messageDsp.DspMsg("M1019");
            else
                //MessageBox.Show("データの削除に失敗しました。");
                messageDsp.DspMsg("M1020");

            textBoxArID.Focus();
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
            GetDataGridView();
        }

        ///////////////////////////////
        //メソッド名：ClearInput()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：入力エリアをクリア
        ///////////////////////////////
        private void ClearInput()
        {
            textBoxArID.Text = "";
            textBoxClID.Text = "";
            textBoxOrID.Text = "";
            textBoxEmID.Text = "";
            textBoxSoID.Text = "";
            dateTimePickerArDate.Value = DateTime.Now;
            dateTimePickerArDate.Checked = false;
            textBoxArDetailID.Text = "";
            textBoxPrID.Text = "";
            textBoxArQuantity.Text = "";
            textBoxArHidden.Text = "";
            checkBoxArStateFlag.Checked = false;
            checkBoxArFlag.Checked = false;
        }

        private void buttonDetailClear_Click(object sender, EventArgs e)
        {
            // 受注詳細欄の入力エリアのクリア
            textBoxArDetailID.Text = "";
            textBoxPrID.Text = "";
            textBoxArQuantity.Text = "";

            // データグリッドビューの表示
            SetFormDataGridView();
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewArrival_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //クリックされた行データをテキストボックスへ
            textBoxOrID.Text = dataGridViewArrival.Rows[dataGridViewArrival.CurrentRow.Index].Cells[0].Value.ToString();
            textBoxSoID.Text = dataGridViewArrival.Rows[dataGridViewArrival.CurrentRow.Index].Cells[1].Value.ToString();
            textBoxEmID.Text = dataGridViewArrival.Rows[dataGridViewArrival.CurrentRow.Index].Cells[2].Value.ToString();
            textBoxClID.Text = dataGridViewArrival.Rows[dataGridViewArrival.CurrentRow.Index].Cells[3].Value.ToString();
            textBoxArID.Text = dataGridViewArrival.Rows[dataGridViewArrival.CurrentRow.Index].Cells[4].Value.ToString();

            //日付が設定されていない場合、初期値として現在の日付を設定
            if (dataGridViewArrival.Rows[dataGridViewArrival.CurrentRow.Index].Cells[5].Value == null)
            {
                dateTimePickerArDate.Value = DateTime.Now;
                dateTimePickerArDate.Checked = false;
            }
            else
            {
                dateTimePickerArDate.Text = dataGridViewArrival.Rows[dataGridViewArrival.CurrentRow.Index].Cells[5].Value.ToString();
            }

            //状態フラグの数値型をbool型に変換して取得
            int OrStateFlg2 = int.Parse(dataGridViewArrival.Rows[dataGridViewArrival.CurrentRow.Index].Cells[6].Value.ToString());
            if (OrStateFlg2 == 0)
            {
                checkBoxArStateFlag.Checked = false;
            }
            else
            {
                checkBoxArStateFlag.Checked = true;
            }
            //管理フラグの数値型をbool型に変換して取得
            int OrFlg2 = int.Parse(dataGridViewArrival.Rows[dataGridViewArrival.CurrentRow.Index].Cells[7].Value.ToString());
            if (OrFlg2 == 0)
            {
                checkBoxArFlag.Checked = false;
            }
            else if (OrFlg2 == 2)
            {
                checkBoxArFlag.Checked = true;
            }

            textBoxArHidden.Text = dataGridViewArrival.Rows[dataGridViewArrival.CurrentRow.Index].Cells[8].Value.ToString();
            textBoxArDetailID.Text = dataGridViewArrival.Rows[dataGridViewArrival.CurrentRow.Index].Cells[9].Value.ToString();
            textBoxPrID.Text = dataGridViewArrival.Rows[dataGridViewArrival.CurrentRow.Index].Cells[10].Value.ToString();
            textBoxArQuantity.Text = dataGridViewArrival.Rows[dataGridViewArrival.CurrentRow.Index].Cells[11].Value.ToString();
        }

        private void checkBoxArStateFlag_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxArStateFlag.Checked == true)
            {
                ArStateFlg = 1;
                return;
            }
            else if (checkBoxArStateFlag.Checked == false)
            {
                ArStateFlg = 0;
                return;
            }
        }

        private void checkBoxArFlag_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxArFlag.Checked == true)
            {
                ArFlg = 2;
                textBoxArHidden.Enabled = true;
                return;
            }
            else if (checkBoxArFlag.Checked == false)
            {
                ArFlg = 0;
                textBoxArHidden.Enabled = false;
                textBoxArHidden.Text = "";
                return;
            }
        }

        private void buttonPageSizeChange_Click(object sender, EventArgs e)
        {
            SetDataGridView();
        }

        private void buttonFirstPage_Click(object sender, EventArgs e)
        {
            int pageSize = int.Parse(textBoxPageSize.Text);
            dataGridViewArrival.DataSource = filteredList.Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewArrival.Refresh();
            //ページ番号の設定
            textBoxPageNo.Text = "1";
        }

        private void buttonPreviousPage_Click(object sender, EventArgs e)
        {
            int pageSize = int.Parse(textBoxPageSize.Text);
            int pageNo = int.Parse(textBoxPageNo.Text) - 2;
            dataGridViewArrival.DataSource = filteredList.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewArrival.Refresh();
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
            int lastNo = (int)Math.Ceiling(filteredList.Count / (double)pageSize) - 1;
            //最終ページでなければ
            if (pageNo <= lastNo)
                dataGridViewArrival.DataSource = filteredList.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewArrival.Refresh();
            //ページ番号の設定
            int lastPage = (int)Math.Ceiling(filteredList.Count / (double)pageSize);
            if (pageNo >= lastPage)
                textBoxPageNo.Text = lastPage.ToString();
            else
                textBoxPageNo.Text = (pageNo + 1).ToString();
        }

        private void buttonLastPage_Click(object sender, EventArgs e)
        {
            int pageSize = int.Parse(textBoxPageSize.Text);
            //最終ページの計算
            int pageNo = (int)Math.Ceiling(filteredList.Count / (double)pageSize) - 1;
            dataGridViewArrival.DataSource = filteredList.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewArrival.Refresh();
            //ページ番号の設定
            textBoxPageNo.Text = (pageNo + 1).ToString();
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
                    mEmName = mEmployee.EmName;
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
