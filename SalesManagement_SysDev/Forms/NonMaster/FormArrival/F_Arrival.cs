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
        internal static int stflg = 0;

        public F_Arrival()
        {
            InitializeComponent();
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
            textBoxPageSize.Text = "15";
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

            filteredList = Arrival.Where(x => x.ArFlag != 2).ToList(); //ArFlagが2のレコードは排除する
            dataGridViewArrival.DataSource = filteredList.Skip(pageSize * pageNo).Take(pageSize).ToList();

            if(filteredList.Count == 0)
            {
                labelNoTable.Visible = true;
            }
            else
            {
                labelNoTable.Visible = false;
            }

            //各列幅の指定
            //dataGridViewArrival.Columns[0].Width = 100;
            //dataGridViewArrival.Columns[1].Width = 100;
            //dataGridViewArrival.Columns[2].Width = 100;
            //dataGridViewArrival.Columns[3].Width = 100;
            //dataGridViewArrival.Columns[4].Width = 200;
            //dataGridViewArrival.Columns[5].Width = 130;
            //dataGridViewArrival.Columns[6].Width = 110;
            //dataGridViewArrival.Columns[7].Width = 110;
            //dataGridViewArrival.Columns[8].Width = 400;
            //dataGridViewArrival.Columns[9].Width = 100;
            //dataGridViewArrival.Columns[10].Width = 100;
            //dataGridViewArrival.Columns[11].Width = 70;

            // 自動サイズ調整を有効にする
            dataGridViewArrival.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //各列の文字位置の指定
            dataGridViewArrival.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewArrival.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewArrival.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewArrival.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewArrival.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
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
                    messageDsp.DspMsg("M11054");
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
                    messageDsp.DspMsg("M11063");
                    textBoxOrID.Focus();
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
                    messageDsp.DspMsg("M11064");
                    textBoxClID.Focus();
                    return false;
                }
                // 顧客IDの文字数チェック
                if (textBoxClID.TextLength > 6)
                {
                    //MessageBox.Show("顧客IDは6文字です");
                    messageDsp.DspMsg("M11002");
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

                // 社員IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxEmID.Text.Trim()))
                {
                    //MessageBox.Show("社員IDは全て半角数字入力です");
                    messageDsp.DspMsg("M11005");
                    textBoxEmID.Focus();
                    return false;
                }
                // 社員IDの文字数チェック
                if (textBoxEmID.TextLength > 6)
                {
                    //MessageBox.Show("社員IDは6文字です");
                    messageDsp.DspMsg("M11006");
                    textBoxEmID.Focus();
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
                    messageDsp.DspMsg("M11009");
                    textBoxSoID.Focus();
                    return false;
                }
                // 営業所IDの文字数チェック
                if (textBoxSoID.TextLength > 2)
                {
                    //MessageBox.Show("営業所IDは2文字です");
                    messageDsp.DspMsg("M11010");
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
                    //MessageBox.Show("入荷詳細IDは全て半角数字入力です");
                    messageDsp.DspMsg("M11055");
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
                    messageDsp.DspMsg("M11016");
                    textBoxPrID.Focus();
                    return false;
                }
                // 商品IDの文字数チェック
                if (textBoxPrID.TextLength > 6)
                {
                    //MessageBox.Show("商品IDは6文字です");
                    messageDsp.DspMsg("M11017");
                    textBoxPrID.Focus();
                    return false;
                }
            }
            if (dateTimePickerArDate.Checked == true)
            {
                //MessageBox.Show("受注年月日は検索対象外です");
                messageDsp.DspMsg("M11065");
                dateTimePickerArDate.Focus();
                return false;
            }
            if (textBoxArQuantity.Text != "")
            {
                //MessageBox.Show("数量は検索対象外です");
                messageDsp.DspMsg("M11066");
                textBoxArQuantity.Focus();
                return false;
            }

            // 状態フラグの適否
            if (checkBoxArStateFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("状態フラグが不確定の状態です");
                messageDsp.DspMsg("M11027");
                checkBoxArStateFlag.Focus();
                return false;
            }
            // 管理フラグの適否
            if (checkBoxArFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("管理フラグが不確定の状態です");
                messageDsp.DspMsg("M11028");
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
            if (Arrival.Count == 0)
            {
                labelNoTable.Visible = true;
            }
            else
            {
                labelNoTable.Visible = false;
            }

            labelPage.Text = "/" + ((int)Math.Ceiling(Arrival.Count / (double)pageSize)) + "ページ";
            dataGridViewArrival.Refresh();

            if (Arrival.Count == 0) //検索結果のデータ数が0ならエラー
            {
                messageDsp.DspMsg("M11067");
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
                    messageDsp.DspMsg("M11068");
                    textBoxArID.Focus();
                    return false;
                }
                // 入荷IDの文字数チェック
                if (textBoxArID.TextLength > 6)
                {
                    //MessageBox.Show("入荷IDは6文字です");
                    messageDsp.DspMsg("M11069");
                    textBoxArID.Focus();
                    return false;
                }
                // 入荷IDの存在チェック
                if (!arrivalDataAccess.CheckArrivalCDExistence(textBoxArID.Text.Trim()))
                {
                    //MessageBox.Show("入力された入荷IDは存在しません");
                    messageDsp.DspMsg("M11070");
                    textBoxArID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("入荷IDが入力されていません");
                messageDsp.DspMsg("M11071");
                textBoxArID.Focus();
                return false;
            }

            // 受注IDの未入力チェック
            if (!String.IsNullOrEmpty(textBoxOrID.Text.Trim()))
            {
                // 受注IDの文字数チェック
                if (textBoxOrID.TextLength > 6)
                {
                    //MessageBox.Show("受注IDは6文字です");
                    messageDsp.DspMsg("M11072");
                    textBoxOrID.Focus();
                    return false;
                }
                // 受注IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxOrID.Text.Trim()))
                {
                    //MessageBox.Show("受注IDは全て半角数字入力です");
                    messageDsp.DspMsg("M11063");
                    textBoxOrID.Focus();
                    return false;
                }
                // 受注IDの存在チェック
                if (!arrivalDataAccess.CheckOrderIDExistence(textBoxArID.Text.Trim(), textBoxOrID.Text.Trim()))
                {
                    //MessageBox.Show("入荷IDに関連する受注IDが一致しません");
                    messageDsp.DspMsg("M11073");
                    textBoxOrID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("受注IDが入力されていません");
                messageDsp.DspMsg("M11074");
                textBoxOrID.Focus();
                return false;
            }

            // 管理フラグの適否
            if (checkBoxArFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("管理フラグが不確定の状態です");
                messageDsp.DspMsg("M11028");
                checkBoxArFlag.Focus();
                return false;
            }

            // 管理フラグの適否２
            if (checkBoxArFlag.Checked == false)
            {
                //MessageBox.Show("管理フラグが未チェックです");
                messageDsp.DspMsg("M11075");
                checkBoxArFlag.Focus();
                return false;
            }

            // 非表示理由の適否
            if (checkBoxArFlag.Checked == true)
            {
                if (textBoxArHidden.Text == "")
                {
                    //MessageBox.Show("非表示理由の入力が必要です");
                    messageDsp.DspMsg("M11030");
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
                OrID = int.Parse(textBoxOrID.Text.Trim()),
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
            DialogResult result = messageDsp.DspMsg("M11047");
            if (result == DialogResult.Cancel)
            {
                return;
            }

            // 入荷情報の削除
            bool flg = arrivalDataAccess.DeleteArrivalData(delArrival);
            if (flg == true)
                //MessageBox.Show("データを削除しました。");
                messageDsp.DspMsg("M11048");
            else
                //MessageBox.Show("データの削除に失敗しました。");
                messageDsp.DspMsg("M11049");

            textBoxArID.Focus();

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
            NonMaster.FormArrival.F_ArrivalConfirm.stflg = 1;
            NonMaster.FormArrival.F_ArrivalConfirm.ActiveForm.Activate();
            NonMaster.FormShipment.F_Shipment.stflg = 1;
            NonMaster.FormShipment.F_Shipment.ActiveForm.Activate();
            NonMaster.FormSale.F_Sale.stflg = 1;
            NonMaster.FormSale.F_Sale.ActiveForm.Activate();
            Master.FormStock.F_Stock.stflg = 1;
            Master.FormStock.F_Stock.ActiveForm.Activate();
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

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (frm2 != null)
                frm2.Close();
            this.Close();
        }

        private void dataGridViewArrival_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewArrival.CurrentRow != null)
            {
                //クリックされた行データをテキストボックスへ
                textBoxArID.Text = dataGridViewArrival.Rows[dataGridViewArrival.CurrentRow.Index].Cells[0].Value.ToString();
                textBoxSoID.Text = dataGridViewArrival.Rows[dataGridViewArrival.CurrentRow.Index].Cells[1].Value.ToString();
                textBoxEmID.Text = dataGridViewArrival.Rows[dataGridViewArrival.CurrentRow.Index].Cells[2].Value.ToString();
                textBoxClID.Text = dataGridViewArrival.Rows[dataGridViewArrival.CurrentRow.Index].Cells[3].Value.ToString();
                textBoxOrID.Text = dataGridViewArrival.Rows[dataGridViewArrival.CurrentRow.Index].Cells[4].Value.ToString();

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
            if (textBoxPageSize.Text == "" || textBoxPageSize.Text == "0" || textBoxPageSize.TextLength > 9) //Int32の最大値は 2,147,483,647
            {
                textBoxPageSize.Text = "15";
            }
            int pageSize = int.Parse(textBoxPageSize.Text);
            dataGridViewArrival.DataSource = filteredList.Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewArrival.Refresh();
            //ページ番号の設定
            textBoxPageNo.Text = "1";
        }

        private void buttonPreviousPage_Click(object sender, EventArgs e)
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
                    labelStateFlag.Text = "確定済"; //ラベルがUnknow状態で再度入力実行されたときに確定済に戻しておく
                    int mOrID = int.Parse(textBoxOrID.Text);
                    var mOrder = context.T_Orders.Single(x => x.OrID == mOrID); //入力されたOrIDで一致する一件のレコードを探す
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
                    int mSoID = mOrder.SoID; //上記同様
                    textBoxClID.Text = mClID.ToString(); //取得したIDを自動入力
                    textBoxSoID.Text = mSoID.ToString(); //上記同様
                    context.Dispose();
                }
                catch
                {
                    labelFlag.Visible = false;
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

        private void label顧客ID_Click(object sender, EventArgs e)
        {
            OpenForm(((Label)sender).Text);
        }

        private void label2_Click(object sender, EventArgs e)
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

        private void label商品ID_Click(object sender, EventArgs e)
        {
            OpenForm(((Label)sender).Text);
        }

        private void buttonConfirmForm_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private Form frm2;
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
                case "社員ID": //ボタンのテキスト名
                    frm = new Master.FormEmployee.F_Employee(); //フォームの名前
                    break;
                case "受注ID": //ボタンのテキスト名
                    frm = new FormOrder.F_Order(); //フォームの名前
                    break;
                case "営業所ID": //ボタンのテキスト名
                    frm = new Master.FormEmployee.F_SalesOffice(); //フォームの名前
                    break;
                case "入荷確定画面へ": //ボタンのテキスト名
                    frm = new F_ArrivalConfirm(); //フォームの名前
                    frm2 = frm;
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

        private void label顧客ID_MouseEnter(object sender, EventArgs e)
        {
            label顧客ID.BackColor = Color.Aqua;
        }

        private void label顧客ID_MouseLeave(object sender, EventArgs e)
        {
            label顧客ID.BackColor = Color.Transparent;
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            label2.BackColor = Color.Aqua;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.BackColor = Color.Transparent;
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

        private void label商品ID_MouseEnter(object sender, EventArgs e)
        {
            label商品ID.BackColor = Color.Aqua;
        }

        private void label商品ID_MouseLeave(object sender, EventArgs e)
        {
            label商品ID.BackColor = Color.Transparent;
        }

        private void F_Arrival_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (frm2 != null)
                frm2.Close();
        }

        private void F_Arrival_Activated(object sender, EventArgs e)
        {
            labelEmpName.Text = F_menu.loginName;
            labelEmpID.Text = F_menu.loginEmID;
            labelOfficeName.Text = F_menu.loginSalesOffice;
            if (F_menu.loginSalesOffice == "本社")
            {
                buttonDelete.Enabled = false;
                buttonDelete.BackgroundImage = Properties.Resources.Fixed_キャンセル使用不可;
                buttonConfirmForm.Enabled = false;
                buttonConfirmForm.BackgroundImage = Properties.Resources.Fixed_キャンセル使用不可;
            }
            if (stflg == 1)
            {
                GetDataGridView();
                stflg = 0;
            }
        }
    }
}
