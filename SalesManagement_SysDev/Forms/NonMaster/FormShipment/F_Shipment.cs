using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagement_SysDev.Forms.NonMaster.FormShipment
{
    public partial class F_Shipment : Form
    {
        //メッセージ表示用クラスのインスタンス化
        MessageDsp messageDsp = new MessageDsp();
        //データベース出荷テーブルアクセス用クラスのインスタンス化
        DbAccess.ShipmentDataAccess ShipmentDataAccess = new DbAccess.ShipmentDataAccess();
        //入力形式チェック用クラスのインスタンス化
        DataInputFormCheck dataInputFormCheck = new DataInputFormCheck();
        //データグリッドビュー用の出荷データ
        private static List<T_ShipmentDsp> Shipment;
        private static List<T_ShipmentDsp> filteredList;
        //管理フラグを数値型で入れるための変数
        int ShFlg;

        public F_Shipment()
        {
            InitializeComponent();
        }

        private void buttonConfirmForm_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
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

        private void OpenForm(string formName)
        {
            Form frm = new Form();
            //引数より、開くフォームを設定
            switch (formName)
            {
                case "出荷確定画面へ": //ボタンのテキスト名
                    frm = new F_ShipmentConfirm(); //フォームの名前
                    break;
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

        private void F_Shipment_Load(object sender, EventArgs e)
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
            dataGridViewShipment.ReadOnly = true;
            //行内をクリックすることで行を選択
            dataGridViewShipment.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //ヘッダー位置の指定
            dataGridViewShipment.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

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

            // 出荷データの取得
            Shipment = ShipmentDataAccess.GetShipmentData();

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
            filteredList = Shipment.Where(x => x.ShFlag != 2).ToList(); //OrFlagが2のレコードは排除する
            dataGridViewShipment.DataSource = Shipment.Skip(pageSize * pageNo).Take(pageSize).ToList();
            //各列幅の指定
            dataGridViewShipment.Columns[0].Width = 70;
            dataGridViewShipment.Columns[1].Width = 70;
            dataGridViewShipment.Columns[2].Width = 70;
            dataGridViewShipment.Columns[3].Width = 70;
            dataGridViewShipment.Columns[4].Width = 70;
            dataGridViewShipment.Columns[5].Width = 70;
            dataGridViewShipment.Columns[6].Width = 70;
            dataGridViewShipment.Columns[7].Width = 70;
            dataGridViewShipment.Columns[8].Width = 70;
            dataGridViewShipment.Columns[9].Width = 70;
            dataGridViewShipment.Columns[10].Width = 70;
            dataGridViewShipment.Columns[11].Width = 50;

            //各列の文字位置の指定
            dataGridViewShipment.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewShipment.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewShipment.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewShipment.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewShipment.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewShipment.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewShipment.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewShipment.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewShipment.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewShipment.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewShipment.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewShipment.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //dataGridViewの総ページ数
            labelPage.Text = "/" + ((int)Math.Ceiling(Shipment.Count / (double)pageSize)) + "ページ";

            dataGridViewShipment.Refresh();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            // 8.2.4.1 妥当な役職データ取得
            if (!GetValidDataAtSelect())
                return;

            // 8.2.4.2 役職情報抽出
            GenerateDataAtSelect();

            // 8.2.4.3 役職抽出結果表示
            SetSelectData();
        }

        ///////////////////////////////
        //　8.2.4.1 妥当な役職データ取得
        //メソッド名：GetValidDataAtSlect()
        //引　数   ：なし
        //戻り値   ：true or false
        //機　能   ：入力データの形式チェック
        //          ：エラーがない場合True
        //          ：エラーがある場合False
        ///////////////////////////////
        private bool GetValidDataAtSelect()
        {

            // 役職ID入力時チェック
            if (!String.IsNullOrEmpty(textBoxShID.Text.Trim()))
            {
                // 役職IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxShID.Text.Trim()))
                {
                    //MessageBox.Show("役職IDは全て半角数字入力です");
                    messageDsp.DspMsg("M1001");
                    textBoxShID.Focus();
                    return false;
                }
                // 役職IDの文字数チェック
                if (textBoxShID.TextLength > 2)
                {
                    //MessageBox.Show("役職IDは2文字までです");
                    messageDsp.DspMsg("M1002");
                    textBoxShID.Focus();
                    return false;
                }
            }

            // 役職IDの半角数字チェック
            if (!String.IsNullOrEmpty(textBoxClID.Text.Trim()))
            {
                if (!dataInputFormCheck.CheckNumeric(textBoxClID.Text.Trim()))
                {
                    //MessageBox.Show("役職IDは全て半角数字入力です");
                    messageDsp.DspMsg("M1001");
                    textBoxClID.Focus();
                    return false;
                }
                // 役職IDの文字数チェック
                if (textBoxClID.TextLength > 2)
                {
                    //MessageBox.Show("役職IDは2文字までです");
                    messageDsp.DspMsg("M1002");
                    textBoxClID.Focus();
                    return false;
                }
            }

            if (!String.IsNullOrEmpty(textBoxEmID.Text.Trim()))
            {
                // 役職IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxEmID.Text.Trim()))
                {
                    //MessageBox.Show("役職IDは全て半角数字入力です");
                    messageDsp.DspMsg("M1001");
                    textBoxEmID.Focus();
                    return false;
                }
                // 役職IDの文字数チェック
                if (textBoxEmID.TextLength > 2)
                {
                    //MessageBox.Show("役職IDは2文字までです");
                    messageDsp.DspMsg("M1002");
                    textBoxEmID.Focus();
                    return false;
                }
            }


            // 役職IDの半角数字チェック
            if (!String.IsNullOrEmpty(textBoxSoID.Text.Trim()))
            {
                if (!dataInputFormCheck.CheckNumeric(textBoxSoID.Text.Trim()))
                {
                    //MessageBox.Show("役職IDは全て半角数字入力です");
                    messageDsp.DspMsg("M1001");
                    textBoxSoID.Focus();
                    return false;
                }
                // 役職IDの文字数チェック
                if (textBoxSoID.TextLength > 2)
                {
                    //MessageBox.Show("役職IDは2文字までです");
                    messageDsp.DspMsg("M1002");
                    textBoxSoID.Focus();
                    return false;
                }
            }

            // 役職IDの半角数字チェック
            if (!String.IsNullOrEmpty(textBoxOrID.Text.Trim()))
            {
                if (!dataInputFormCheck.CheckNumeric(textBoxOrID.Text.Trim()))
                {
                    //MessageBox.Show("役職IDは全て半角数字入力です");
                    messageDsp.DspMsg("M1001");
                    textBoxOrID.Focus();
                    return false;
                }
                // 役職IDの文字数チェック
                if (textBoxOrID.TextLength > 2)
                {
                    //MessageBox.Show("役職IDは2文字までです");
                    messageDsp.DspMsg("M1002");
                    textBoxOrID.Focus();
                    return false;
                }
            }

            // 状態フラグの適否
            if (checkBoxShStateFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("状態フラグが不確定の状態です");
                messageDsp.DspMsg("M10027");
                checkBoxShStateFlag.Focus();
                return false;
            }

            // 管理フラグの適否
            if (checkBoxShFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("管理フラグが不確定の状態です");
                messageDsp.DspMsg("M10028");
                checkBoxShFlag.Focus();
                return false;
            }
            return true;

        }

        ///////////////////////////////
        //　8.2.4.2 役職情報抽出
        //メソッド名：GenerateDataAtSelect()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：役職情報の取得
        ///////////////////////////////

        public static string mShID; //cSoIDを別クラス(ShipmentDataAccess)でも使用できるように定義
        public static string mEmID;
        public static string mClID;
        public static string mSoID;
        public static string mOrID;
        public static string mShDetailID;
        public static string mPrID;
        public static int? mShStateFlg;
        public static int mShFlg;
        private void GenerateDataAtSelect()
        {
            T_ShipmentDsp selectCondition;
            //boolからintに変換して検索条件セット準備
            if (checkBoxShStateFlag.Checked == true)
            {
                mShStateFlg = 1;
            }
            else if (checkBoxShStateFlag.Checked == false)
            {
                mShStateFlg = null;
            }
            if (checkBoxShFlag.Checked == true)
            {
                mShFlg = 2;
            }
            else if (checkBoxShFlag.Checked == false)
            {
                mShFlg = 0;
            }

            mShID = textBoxShID.Text.Trim(); //IDはnullではなく、""で空白が入るのでstringで代入
            mEmID = textBoxEmID.Text.Trim();
            mClID = textBoxClID.Text.Trim();
            mSoID = textBoxSoID.Text.Trim();
            mOrID = textBoxOrID.Text.Trim();
            mShDetailID = textBoxShDetailID.Text.Trim();
            mPrID = textBoxPrID.Text.Trim();


            if (mShID != "") //IDで検索
            {
                // 検索条件のセット
                selectCondition = new T_ShipmentDsp()
                {
                    ShID = int.Parse(textBoxShID.Text.Trim()),
                    ShStateFlag = mShStateFlg,
                    ShFlag = ShFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // 営業所データの抽出
                Shipment = ShipmentDataAccess.GetShipmentData(selectCondition);
                return;
            }
            else if (mShDetailID != "")
            {
                // 検索条件のセット
                selectCondition = new T_ShipmentDsp()
                {
                    ShDetailID = int.Parse(textBoxShDetailID.Text.Trim()),
                    ShStateFlag = mShStateFlg,
                    ShFlag = ShFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // 営業所データの抽出
                Shipment = ShipmentDataAccess.GetShipmentData(selectCondition);
                return;
            }
            else if (mClID != "")
            {
                // 検索条件のセット
                selectCondition = new T_ShipmentDsp()
                {
                    ClID = int.Parse(textBoxClID.Text.Trim()),
                    ShStateFlag = mShStateFlg,
                    ShFlag = ShFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // 営業所データの抽出
                Shipment = ShipmentDataAccess.GetShipmentData(selectCondition);
                return;
            }
            else if (mOrID != "")
            {
                // 検索条件のセット
                selectCondition = new T_ShipmentDsp()
                {
                    OrID = int.Parse(textBoxOrID.Text.Trim()),
                    ShStateFlag = mShStateFlg,
                    ShFlag = ShFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // 営業所データの抽出
                Shipment = ShipmentDataAccess.GetShipmentData(selectCondition);
                return;
            }
            else if (mSoID != "")
            {
                // 検索条件のセット
                selectCondition = new T_ShipmentDsp()
                {
                    SoID = int.Parse(textBoxSoID.Text.Trim()),
                    ShStateFlag = mShStateFlg,
                    ShFlag = ShFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // 営業所データの抽出
                Shipment = ShipmentDataAccess.GetShipmentData(selectCondition);
                return;
            }
            else if (mEmID != "")
            {
                // 検索条件のセット
                selectCondition = new T_ShipmentDsp()
                {
                    EmID = int.Parse(textBoxEmID.Text.Trim()),
                    ShStateFlag = mShStateFlg,
                    ShFlag = ShFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // 営業所データの抽出
                Shipment = ShipmentDataAccess.GetShipmentData(selectCondition);
                return;
            }
            else if (mShFlg == 2)
            {
                // 検索条件のセット
                selectCondition = new T_ShipmentDsp()
                {
                    ShFlag = mShFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Shipment = ShipmentDataAccess.SearchShipmentData(selectCondition);
                return;
            }
            else if (mShStateFlg == 1)
            {
                // 検索条件のセット
                selectCondition = new T_ShipmentDsp()
                {
                    ShStateFlag = mShStateFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Shipment = ShipmentDataAccess.SearchShipmentData(selectCondition);
                return;
            }
            else if (mPrID != "")
            {
                // 検索条件のセット
                selectCondition = new T_ShipmentDsp()
                {
                    PrID = int.Parse(textBoxPrID.Text.Trim()),
                    ShStateFlag = mShStateFlg,
                    ShFlag = ShFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // 営業所データの抽出
                Shipment = ShipmentDataAccess.GetShipmentData(selectCondition);
                return;
            }
        }
        ///////////////////////////////
        //　8.2.4.3 役職抽出結果表示
        //メソッド名：SetSelectData()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：役職情報の表示
        ///////////////////////////////
        private void SetSelectData()
        {
            textBoxPageNo.Text = "1";

            int pageSize = int.Parse(textBoxPageSize.Text);

            dataGridViewShipment.DataSource = Shipment;

            labelPage.Text = "/" + ((int)Math.Ceiling(Shipment.Count / (double)pageSize)) + "ページ";
            dataGridViewShipment.Refresh();

            if (Shipment.Count == 0) //検索結果のデータ数が0ならエラー
            {
                messageDsp.DspMsg("M1025");
                SetFormDataGridView();
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            // 8.2.2.1 妥当な役職データ取得
            if (!GetValidDataAtDelete())
                return;

            // 8.2.2.2 役職情報作成
            var updShipment = GenerateDataAtDelete();

            // 8.2.2.3 役職情報更新
            DeleteShipment(updShipment);
        }

        ///////////////////////////////
        //　8.2.2.1 妥当な役職データ取得
        //メソッド名：GetValidDataAtUpdate()
        //引　数   ：なし
        //戻り値   ：true or false
        //機　能   ：入力データの形式チェック
        //          ：エラーがない場合True
        //          ：エラーがある場合False
        ///////////////////////////////
        private bool GetValidDataAtDelete()
        {

            // 役職IDの未入力チェック
            if (!String.IsNullOrEmpty(textBoxShID.Text.Trim()))
            {
                // 役職IDの半角英数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxShID.Text.Trim()))
                {
                    //MessageBox.Show("役職IDは全て半角英数字入力です");
                    messageDsp.DspMsg("M1001");
                    textBoxShID.Focus();
                    return false;
                }
                // 役職IDの文字数チェック
                if (textBoxShID.TextLength > 2)
                {
                    //MessageBox.Show("役職IDは2文字です");
                    messageDsp.DspMsg("M1002");
                    textBoxShID.Focus();
                    return false;
                }
                // 役職IDの存在チェック
                if (!ShipmentDataAccess.CheckShipmentCDExistence(textBoxShID.Text.Trim()))
                {
                    //MessageBox.Show("入力された役職IDは存在しません");
                    messageDsp.DspMsg("M1013");
                    textBoxShID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("役職IDが入力されていません");
                messageDsp.DspMsg("M1004");
                textBoxShID.Focus();
                return false;
            }

            // 管理フラグの適否
            if (checkBoxShFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("管理フラグが不確定の状態です");
                messageDsp.DspMsg("M10028");
                checkBoxShFlag.Focus();
                return false;
            }

            // 管理フラグの適否２
            if (checkBoxShFlag.Checked == false)
            {
                //MessageBox.Show("管理フラグが未チェックです");
                messageDsp.DspMsg("M10029");
                checkBoxShFlag.Focus();
                return false;
            }

            // 非表示理由の適否
            if (checkBoxShFlag.Checked == true)
            {
                if (textBoxShHidden.Text == "")
                {
                    //MessageBox.Show("非表示理由の入力が必要です");
                    messageDsp.DspMsg("M1023");
                    textBoxShHidden.Focus();
                    return false;
                }
            }
            return true;
        }

        ///////////////////////////////
        //　8.2.2.2 役職情報作成
        //メソッド名：GenerateDataAtUpdate()
        //引　数   ：なし
        //戻り値   ：役職更新情報
        //機　能   ：更新データのセット
        ///////////////////////////////
        private T_Shipment GenerateDataAtDelete()
        {
            return new T_Shipment
            {
                ShID = int.Parse(textBoxShID.Text.Trim()),
                ShFlag = ShFlg,
                ShHidden = textBoxShHidden.Text.Trim()
            };
        }

        private void DeleteShipment(T_Shipment delShipment)
        {

            // 更新確認メッセージ
            DialogResult result = messageDsp.DspMsg("M1018");
            if (result == DialogResult.Cancel)
                return;

            // 役職情報の更新
            bool flg = ShipmentDataAccess.DeleteShipmentData(delShipment);
            if (flg == true)
                //MessageBox.Show("データを削除しました。");
                messageDsp.DspMsg("M1019");
            else
                //MessageBox.Show("データの削除に失敗しました。");
                messageDsp.DspMsg("M1020");

            textBoxShID.Focus();

            // 入力エリアのクリア
            ClearInput();

            // データグリッドビューの表示
            GetDataGridView();
        }

        private void dataGridViewShipment_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //クリックされた行データをテキストボックスへ
            textBoxShID.Text = dataGridViewShipment.Rows[dataGridViewShipment.CurrentRow.Index].Cells[0].Value.ToString();
            textBoxClID.Text = dataGridViewShipment.Rows[dataGridViewShipment.CurrentRow.Index].Cells[1].Value.ToString();
            textBoxEmID.Text = dataGridViewShipment.Rows[dataGridViewShipment.CurrentRow.Index].Cells[2].Value.ToString();
            textBoxSoID.Text = dataGridViewShipment.Rows[dataGridViewShipment.CurrentRow.Index].Cells[3].Value.ToString();
            textBoxOrID.Text = dataGridViewShipment.Rows[dataGridViewShipment.CurrentRow.Index].Cells[4].Value.ToString();

            //日付が設定されていない場合、初期値として現在の日付を設定
            if (dataGridViewShipment.Rows[dataGridViewShipment.CurrentRow.Index].Cells[5].Value == null)
            {
                dateTimePickerShFinishDate.Value = DateTime.Now;
                dateTimePickerShFinishDate.Checked = false;
            }
            else
            {
                dateTimePickerShFinishDate.Text = dataGridViewShipment.Rows[dataGridViewShipment.CurrentRow.Index].Cells[6].Value.ToString();
            }

            //状態フラグの数値型をbool型に変換して取得
            int ShStateFlg2 = int.Parse(dataGridViewShipment.Rows[dataGridViewShipment.CurrentRow.Index].Cells[5].Value.ToString());
            if (ShStateFlg2 == 0)
            {
                checkBoxShStateFlag.Checked = false;
            }
            else
            {
                checkBoxShStateFlag.Checked = true;
            }
            //管理フラグの数値型をbool型に変換して取得
            int ShFlg2 = int.Parse(dataGridViewShipment.Rows[dataGridViewShipment.CurrentRow.Index].Cells[7].Value.ToString());
            if (ShFlg2 == 0)
            {
                checkBoxShFlag.Checked = false;
            }
            else if (ShFlg2 == 2)
            {
                checkBoxShFlag.Checked = true;
            }

            textBoxShHidden.Text = dataGridViewShipment.Rows[dataGridViewShipment.CurrentRow.Index].Cells[8].Value.ToString();
            textBoxShDetailID.Text = dataGridViewShipment.Rows[dataGridViewShipment.CurrentRow.Index].Cells[9].Value.ToString();
            textBoxPrID.Text = dataGridViewShipment.Rows[dataGridViewShipment.CurrentRow.Index].Cells[10].Value.ToString();
            textBoxShDquantity.Text = dataGridViewShipment.Rows[dataGridViewShipment.CurrentRow.Index].Cells[11].Value.ToString();
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
            textBoxShID.Text = "";
            textBoxClID.Text = "";
            textBoxEmID.Text = "";
            textBoxSoID.Text = "";
            dateTimePickerShFinishDate.Text = "";
            textBoxOrID.Text = "";
            checkBoxShFlag.Checked = false;
            checkBoxShStateFlag.Checked = false;
            textBoxShHidden.Text = "";
            textBoxShDetailID.Text = "";
            textBoxPrID.Text = "";
            textBoxShDquantity.Text = "";
        }

        private void buttonDetailClear_Click(object sender, EventArgs e)
        {
            textBoxShDetailID.Text = "";
            textBoxPrID.Text = "";
            textBoxShDquantity.Text = "";

            SetFormDataGridView();
        }

        private void checkBoxShStateFlag_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxShFlag_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void buttonPageSizeChange_Click(object sender, EventArgs e)
        {
            SetDataGridView();
        }

        private void buttonFirstPage_Click(object sender, EventArgs e)
        {
            int pageSize = int.Parse(textBoxPageSize.Text);
            dataGridViewShipment.DataSource = filteredList.Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewShipment.Refresh();
            //ページ番号の設定
            textBoxPageNo.Text = "1";
        }

        private void buttonPreviousPage_Click(object sender, EventArgs e)
        {
            int pageSize = int.Parse(textBoxPageSize.Text);
            int pageNo = int.Parse(textBoxPageNo.Text) - 2;
            dataGridViewShipment.DataSource = filteredList.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewShipment.Refresh();
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
                dataGridViewShipment.DataSource = filteredList.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewShipment.Refresh();
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
            dataGridViewShipment.DataSource = filteredList.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewShipment.Refresh();
            //ページ番号の設定
            textBoxPageNo.Text = (pageNo + 1).ToString();
        }

        private void textBoxClID_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxEmID_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxSoID_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxOrID_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxPrID_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
