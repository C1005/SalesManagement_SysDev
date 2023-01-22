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
        DbAccess.ShipmentDataAccess shipmentDataAccess = new DbAccess.ShipmentDataAccess();
        //入力形式チェック用クラスのインスタンス化
        DataInputFormCheck dataInputFormCheck = new DataInputFormCheck();
        //データグリッドビュー用の出荷データ
        private static List<T_ShipmentDsp> Shipment;
        private static List<T_ShipmentDsp> filteredList;
        //管理フラグを数値型で入れるための変数
        int ShFlg;
        int ShStateFlg;
        internal static int stflg = 0;

        public F_Shipment()
        {
            InitializeComponent();
        }

        private void buttonConfirmForm_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
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

        private Form frm2;
        private void OpenForm(string formName)
        {
            Form frm = new Form();
            //引数より、開くフォームを設定
            switch (formName)
            {
                case "出荷確定画面へ": //ボタンのテキスト名
                    frm = new F_ShipmentConfirm(); //フォームの名前
                    frm2 = frm;
                    break;
                case "顧客ID": //ボタンのテキスト名
                    frm = new Master.FormClient.F_Client(); //フォームの名前
                    break;
                case "商品ID": //ボタンのテキスト名
                    frm = new Master.FormProduct.F_Product(); //フォームの名前
                    break;
                case "社員ID": //ボタンのテキスト名
                    frm = new Master.FormEmployee.F_Employee(); //フォームの名前
                    break;
                case "営業所ID": //ボタンのテキスト名
                    frm = new Master.FormEmployee.F_SalesOffice(); //フォームの名前
                    break;
                case "受注ID": //ボタンのテキスト名
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
            textBoxPageSize.Text = "15";
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
            Shipment = shipmentDataAccess.GetShipmentData();

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
            filteredList = Shipment.Where(x => x.ShFlag != 2).ToList(); //OrFlagが2のレコードは排除する
            dataGridViewShipment.DataSource = filteredList.Skip(pageSize * pageNo).Take(pageSize).ToList();

            if (filteredList.Count == 0)
            {
                labelNoTable.Visible = true;
            }
            else
            {
                labelNoTable.Visible = false;
            }

            //各列幅の指定
            //dataGridViewShipment.Columns[0].Width = 70;
            //dataGridViewShipment.Columns[1].Width = 70;
            //dataGridViewShipment.Columns[2].Width = 70;
            //dataGridViewShipment.Columns[3].Width = 70;
            //dataGridViewShipment.Columns[4].Width = 70;
            //dataGridViewShipment.Columns[5].Width = 70;
            //dataGridViewShipment.Columns[6].Width = 70;
            //dataGridViewShipment.Columns[7].Width = 70;
            //dataGridViewShipment.Columns[8].Width = 70;
            //dataGridViewShipment.Columns[9].Width = 70;
            //dataGridViewShipment.Columns[10].Width = 70;
            //dataGridViewShipment.Columns[11].Width = 50;

            // 自動サイズ調整を有効にする
            dataGridViewShipment.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //各列の文字位置の指定
            dataGridViewShipment.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewShipment.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewShipment.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewShipment.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewShipment.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewShipment.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewShipment.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewShipment.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewShipment.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewShipment.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewShipment.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewShipment.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //dataGridViewの総ページ数
            labelPage.Text = "/" + ((int)Math.Ceiling(filteredList.Count / (double)pageSize)) + "ページ";

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

            // 出荷ID入力時チェック
            if (!String.IsNullOrEmpty(textBoxShID.Text.Trim()))
            {
                // 出荷IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxShID.Text.Trim()))
                {
                    //MessageBox.Show("出荷IDは全て半角数字入力です");
                    messageDsp.DspMsg("M15062");
                    textBoxShID.Focus();
                    return false;
                }
            }

            // 顧客IDの入力時チェック
            if (!String.IsNullOrEmpty(textBoxClID.Text.Trim()))
            {
                if (!dataInputFormCheck.CheckNumeric(textBoxClID.Text.Trim()))
                {
                    //MessageBox.Show("顧客IDは全て半角数字入力です");
                    messageDsp.DspMsg("M15001");
                    textBoxClID.Focus();
                    return false;
                }
                // 顧客IDの文字数チェック
                if (textBoxClID.TextLength > 6)
                {
                    //MessageBox.Show("顧客IDは6文字までです");
                    messageDsp.DspMsg("M15002");
                    textBoxClID.Focus();
                    return false;
                }
            }

            // 社員IDの入力時チェック
            if (!String.IsNullOrEmpty(textBoxEmID.Text.Trim()))
            {
                // 社員IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxEmID.Text.Trim()))
                {
                    //MessageBox.Show("社員IDは全て半角数字入力です");
                    messageDsp.DspMsg("M15005");
                    textBoxEmID.Focus();
                    return false;
                }
                // 社員IDの文字数チェック
                if (textBoxEmID.TextLength > 6)
                {
                    //MessageBox.Show("社員IDは6文字までです");
                    messageDsp.DspMsg("M15006");
                    textBoxEmID.Focus();
                    return false;
                }
            }

            // 営業所IDの入力時チェック
            if (!String.IsNullOrEmpty(textBoxSoID.Text.Trim()))
            {
                if (!dataInputFormCheck.CheckNumeric(textBoxSoID.Text.Trim()))
                {
                    //MessageBox.Show("営業所IDは全て半角数字入力です");
                    messageDsp.DspMsg("M15009");
                    textBoxSoID.Focus();
                    return false;
                }
                // 営業所IDの文字数チェック
                if (textBoxSoID.TextLength > 2)
                {
                    //MessageBox.Show("営業所IDは2文字までです");
                    messageDsp.DspMsg("M15010");
                    textBoxSoID.Focus();
                    return false;
                }
            }

            // 受注IDの入力時チェック
            if (!String.IsNullOrEmpty(textBoxOrID.Text.Trim()))
            {
                if (!dataInputFormCheck.CheckNumeric(textBoxOrID.Text.Trim()))
                {
                    //MessageBox.Show("受注IDは全て半角数字入力です");
                    messageDsp.DspMsg("M15013");
                    textBoxOrID.Focus();
                    return false;
                }
                // 受注IDの文字数チェック
                if (textBoxOrID.TextLength > 6)
                {
                    //MessageBox.Show("受注IDは6文字までです");
                    messageDsp.DspMsg("M15014");
                    textBoxOrID.Focus();
                    return false;
                }
            }

            if (dateTimePickerShFinishDate.Checked == true)
            {
                //MessageBox.Show("注文年月日は検索対象外です");
                messageDsp.DspMsg("M15063");
                dateTimePickerShFinishDate.Focus();
                return false;
            }
            if (textBoxShDquantity.Text != "")
            {
                //MessageBox.Show("数量は検索対象外です");
                messageDsp.DspMsg("M15064");
                textBoxShDquantity.Focus();
                return false;
            }

            // 状態フラグの適否
            if (checkBoxShStateFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("状態フラグが不確定の状態です");
                messageDsp.DspMsg("M15027");
                checkBoxShStateFlag.Focus();
                return false;
            }

            // 管理フラグの適否
            if (checkBoxShFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("管理フラグが不確定の状態です");
                messageDsp.DspMsg("M15028");
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
                Shipment = shipmentDataAccess.GetShipmentData(selectCondition);
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
                Shipment = shipmentDataAccess.GetShipmentData(selectCondition);
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
                Shipment = shipmentDataAccess.GetShipmentData(selectCondition);
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
                Shipment = shipmentDataAccess.GetShipmentData(selectCondition);
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
                Shipment = shipmentDataAccess.GetShipmentData(selectCondition);
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
                Shipment = shipmentDataAccess.GetShipmentData(selectCondition);
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
                Shipment = shipmentDataAccess.SearchShipmentData(selectCondition);
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
                Shipment = shipmentDataAccess.SearchShipmentData(selectCondition);
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
                Shipment = shipmentDataAccess.GetShipmentData(selectCondition);
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
            if (Shipment.Count == 0)
            {
                labelNoTable.Visible = true;
            }
            else
            {
                labelNoTable.Visible = false;
            }

            labelPage.Text = "/" + ((int)Math.Ceiling(Shipment.Count / (double)pageSize)) + "ページ";
            dataGridViewShipment.Refresh();

            if (Shipment.Count == 0) //検索結果のデータ数が0ならエラー
            {
                messageDsp.DspMsg("M15037");
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

            // 出荷IDの未入力チェック
            if (!String.IsNullOrEmpty(textBoxShID.Text.Trim()))
            {
                // 出荷IDの半角英数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxShID.Text.Trim()))
                {
                    //MessageBox.Show("出荷IDは全て半角英数字入力です");
                    messageDsp.DspMsg("M15065");
                    textBoxShID.Focus();
                    return false;
                }
                // 出荷IDの文字数チェック
                if (textBoxShID.TextLength > 6)
                {
                    //MessageBox.Show("出荷IDは6文字です");
                    messageDsp.DspMsg("M15066");
                    textBoxShID.Focus();
                    return false;
                }
                // 出荷IDの存在チェック
                if (!shipmentDataAccess.CheckShipmentCDExistence(textBoxShID.Text.Trim()))
                {
                    //MessageBox.Show("入力された出荷IDは存在しません");
                    messageDsp.DspMsg("M15067");
                    textBoxShID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("出荷IDが入力されていません");
                messageDsp.DspMsg("M15068");
                textBoxShID.Focus();
                return false;
            }

            // 受注IDの未入力チェック
            if (!String.IsNullOrEmpty(textBoxOrID.Text.Trim()))
            {
                // 受注IDの文字数チェック
                if (textBoxOrID.TextLength > 6)
                {
                    //MessageBox.Show("受注IDは6文字です");
                    messageDsp.DspMsg("M15014");
                    textBoxOrID.Focus();
                    return false;
                }
                // 受注IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxOrID.Text.Trim()))
                {
                    //MessageBox.Show("受注IDは全て半角数字入力です");
                    messageDsp.DspMsg("M15013");
                    textBoxOrID.Focus();
                    return false;
                }
                // 受注IDの存在チェック
                if (!shipmentDataAccess.CheckOrderIDExistence(textBoxShID.Text.Trim(), textBoxOrID.Text.Trim()))
                {
                    //MessageBox.Show("出荷IDに関連する受注IDが一致しません");
                    messageDsp.DspMsg("M15015");
                    textBoxOrID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("受注IDが入力されていません");
                messageDsp.DspMsg("M15069");
                textBoxOrID.Focus();
                return false;
            }

            // 管理フラグの適否
            if (checkBoxShFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("管理フラグが不確定の状態です");
                messageDsp.DspMsg("M15028");
                checkBoxShFlag.Focus();
                return false;
            }

            // 管理フラグの適否２
            if (checkBoxShFlag.Checked == false)
            {
                //MessageBox.Show("管理フラグが未チェックです");
                messageDsp.DspMsg("M15070");
                checkBoxShFlag.Focus();
                return false;
            }

            // 非表示理由の適否
            if (checkBoxShFlag.Checked == true)
            {
                if (textBoxShHidden.Text == "")
                {
                    //MessageBox.Show("非表示理由の入力が必要です");
                    messageDsp.DspMsg("M15030");
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
                OrID = int.Parse(textBoxOrID.Text.Trim()),
                ShFlag = ShFlg,
                ShHidden = textBoxShHidden.Text.Trim()
            };
        }

        private void DeleteShipment(T_Shipment delShipment)
        {

            // 更新確認メッセージ
            DialogResult result = messageDsp.DspMsg("M15038");
            if (result == DialogResult.Cancel)
            {
                return;
            }

            // 役職情報の更新
            bool flg = shipmentDataAccess.DeleteShipmentData(delShipment);
            if (flg == true)
                //MessageBox.Show("データを削除しました。");
                messageDsp.DspMsg("M15039");
            else
                //MessageBox.Show("データの削除に失敗しました。");
                messageDsp.DspMsg("M15040");

            textBoxShID.Focus();

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
            NonMaster.FormShipment.F_ShipmentConfirm.stflg = 1;
            NonMaster.FormShipment.F_ShipmentConfirm.ActiveForm.Activate();
            NonMaster.FormSale.F_Sale.stflg = 1;
            NonMaster.FormSale.F_Sale.ActiveForm.Activate();
            Master.FormStock.F_Stock.stflg = 1;
            Master.FormStock.F_Stock.ActiveForm.Activate();
        }


        private void dataGridViewShipment_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewShipment.CurrentRow != null)
            {
                //クリックされた行データをテキストボックスへ
                textBoxShID.Text = dataGridViewShipment.Rows[dataGridViewShipment.CurrentRow.Index].Cells[0].Value.ToString();
                textBoxClID.Text = dataGridViewShipment.Rows[dataGridViewShipment.CurrentRow.Index].Cells[1].Value.ToString();
                textBoxEmID.Text = dataGridViewShipment.Rows[dataGridViewShipment.CurrentRow.Index].Cells[2].Value.ToString();
                textBoxSoID.Text = dataGridViewShipment.Rows[dataGridViewShipment.CurrentRow.Index].Cells[3].Value.ToString();
                textBoxOrID.Text = dataGridViewShipment.Rows[dataGridViewShipment.CurrentRow.Index].Cells[4].Value.ToString();

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

                //日付が設定されていない場合、初期値として現在の日付を設定
                if (dataGridViewShipment.Rows[dataGridViewShipment.CurrentRow.Index].Cells[6].Value == null)
                {
                    dateTimePickerShFinishDate.Value = DateTime.Now;
                    dateTimePickerShFinishDate.Checked = false;
                }
                else
                {
                    dateTimePickerShFinishDate.Text = dataGridViewShipment.Rows[dataGridViewShipment.CurrentRow.Index].Cells[6].Value.ToString();
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
            if (checkBoxShStateFlag.Checked == true)
            {
                ShStateFlg = 1;
                return;
            }
            else if (checkBoxShStateFlag.Checked == false)
            {
                ShStateFlg = 0;
                return;
            }
        }

        private void checkBoxShFlag_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShFlag.Checked == true)
            {
                ShFlg = 2;
                textBoxShHidden.Enabled = true;
                return;
            }
            else if (checkBoxShFlag.Checked == false)
            {
                ShFlg = 0;
                textBoxShHidden.Enabled = false;
                textBoxShHidden.Text = "";
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
            dataGridViewShipment.DataSource = filteredList.Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewShipment.Refresh();
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
            dataGridViewShipment.DataSource = filteredList.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewShipment.Refresh();
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
                    textBoxSoID.Text = "";
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
                textBoxSoID.Text = "";
                labelFlag.Visible = false;
                labelStateFlag.Visible = false;
                labelStateFlag.Text = "確定済";
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

        private void F_Shipment_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (frm2 != null)
                frm2.Close();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (frm2 != null)
                frm2.Close();
            this.Close();
        }

        private void F_Shipment_Activated(object sender, EventArgs e)
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
