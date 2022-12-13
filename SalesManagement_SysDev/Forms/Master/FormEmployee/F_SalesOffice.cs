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
    public partial class F_SalesOffice : Form
    {
        //メッセージ表示用クラスのインスタンス化
        MessageDsp messageDsp = new MessageDsp();
        //データベース営業所テーブルアクセス用クラスのインスタンス化
        DbAccess.SalesOfficeDataAccess salesOfficeDataAccess = new DbAccess.SalesOfficeDataAccess();
        //入力形式チェック用クラスのインスタンス化
        DataInputFormCheck dataInputFormCheck = new DataInputFormCheck();
        //データグリッドビュー用の営業所データ
        private static List<M_SalesOffice> SalesOffice;
        //管理フラグを数値型で入れるための変数
        int SoFlg;

        public F_SalesOffice()
        {
            InitializeComponent();
        }

        private void F_SalesOffice_Load(object sender, EventArgs e)
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
            dataGridViewSalesOffice.ReadOnly = true;
            //行内をクリックすることで行を選択
            dataGridViewSalesOffice.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //ヘッダー位置の指定
            dataGridViewSalesOffice.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

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

            // 営業所データの取得
            SalesOffice = salesOfficeDataAccess.GetSalesOfficeData();

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
            dataGridViewSalesOffice.DataSource = SalesOffice.Skip(pageSize * pageNo).Take(pageSize).ToList();
            //各列幅の指定
            dataGridViewSalesOffice.Columns[0].Width = 120;
            dataGridViewSalesOffice.Columns[1].Width = 200;
            dataGridViewSalesOffice.Columns[2].Width = 300;
            dataGridViewSalesOffice.Columns[3].Width = 100;
            dataGridViewSalesOffice.Columns[4].Width = 80;
            dataGridViewSalesOffice.Columns[5].Width = 100;
            dataGridViewSalesOffice.Columns[6].Width = 120;
            dataGridViewSalesOffice.Columns[7].Width = 300;

            //各列の文字位置の指定
            dataGridViewSalesOffice.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewSalesOffice.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewSalesOffice.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewSalesOffice.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewSalesOffice.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewSalesOffice.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewSalesOffice.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewSalesOffice.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            //dataGridViewの総ページ数
            labelPage.Text = "/" + ((int)Math.Ceiling(SalesOffice.Count / (double)pageSize)) + "ページ";

            dataGridViewSalesOffice.Refresh();
        }

        private void buttonPageSizeChange_Click(object sender, EventArgs e)
        {
            SetDataGridView();
        }

        ///////////////////////////////
        //メソッド名：ClearInput()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：入力エリアをクリア
        ///////////////////////////////
        private void ClearInput()
        {
            textBoxSoID.Text = "";
            textBoxSoName.Text = "";
            textBoxSoAddress.Text = "";
            textBoxSoPhone.Text = "";
            textBoxSoPostal.Text = "";
            textBoxSoFAX.Text = "";
            checkBoxSoFlag.Checked = false;
            textBoxSoHidden.Text = "";
        }

        private void buttonRegist_Click(object sender, EventArgs e)
        {
            // 8.2.1.1 妥当な営業所データ取得
            if (!GetValidDataAtRegistration())
                return;

            // 8.2.1.2 営業所情報作成
            var regSalesOffice = GenerateDataAtRegistration();

            // 8.2.1.3 営業所情報登録
            RegistrationSalesOffice(regSalesOffice);
        }

        ///////////////////////////////
        //　8.2.1.1 妥当な営業所データ取得
        //メソッド名：GetValidDataAtRegistration()
        //引　数   ：なし
        //戻り値   ：true or false
        //機　能   ：入力データの形式チェック
        //          ：エラーがない場合True
        //          ：エラーがある場合False
        ///////////////////////////////
        private bool GetValidDataAtRegistration()
        {

            // 営業所IDの適否
            if (!String.IsNullOrEmpty(textBoxSoID.Text.Trim()))
            {
                // スタッフCDの半角英数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxSoID.Text.Trim()))
                {
                    //MessageBox.Show("営業所IDは全て半角数字入力です");
                    messageDsp.DspMsg("M3001");
                    textBoxSoID.Focus();
                    return false;
                }
                // 営業所IDの文字数チェック
                if (textBoxSoID.TextLength > 2)
                {
                    //MessageBox.Show("営業所IDは2文字です");
                    messageDsp.DspMsg("M3002");
                    textBoxSoID.Focus();
                    return false;
                }

            }

            // 営業所名の適否
            if (!String.IsNullOrEmpty(textBoxSoName.Text.Trim()))
            {
                // 営業所名の全角チェック
                if (!dataInputFormCheck.CheckFullWidth(textBoxSoName.Text.Trim()))
                {
                    //MessageBox.Show("営業所名は全て全角入力です");
                    messageDsp.DspMsg("M3005");
                    textBoxSoName.Focus();
                    return false;
                }
                // 営業所名の文字数チェック
                if (textBoxSoName.TextLength > 50)
                {
                    //MessageBox.Show("営業所名は50文字以下です");
                    messageDsp.DspMsg("M3006");
                    textBoxSoName.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("営業所名が入力されていません");
                messageDsp.DspMsg("M3007");
                textBoxSoName.Focus();
                return false;
            }

            // 郵便番号の数値チェック
            if (!String.IsNullOrEmpty(textBoxSoPostal.Text.Trim()))
            {
                if (!dataInputFormCheck.CheckNumeric(textBoxSoPostal.Text.Trim()))
                {
                    //MessageBox.Show("郵便番号は半角数値です");
                    messageDsp.DspMsg("M3025");
                    textBoxSoPostal.Focus();
                    return false;
                }
                // 郵便番号の文字数チェック
                if (textBoxSoPostal.TextLength != 7)
                {
                    //MessageBox.Show("郵便番号は7文字です");
                    messageDsp.DspMsg("M3028");
                    textBoxSoPostal.Focus();
                    return false;
                }
            }
            // 住所の文字数チェック
            if (!String.IsNullOrEmpty(textBoxSoAddress.Text.Trim()))
            {
                if (textBoxSoAddress.TextLength > 50)
                {
                    //MessageBox.Show("住所は50文字以下です");
                    messageDsp.DspMsg("M3013");
                    textBoxSoAddress.Focus();
                    return false;
                }
            }
            // FAXの半角数値チェック
            if (!String.IsNullOrEmpty(textBoxSoFAX.Text.Trim()))
            {
                if (!dataInputFormCheck.CheckNumeric(textBoxSoFAX.Text.Trim()))
                {
                    //MessageBox.Show("FAXは半角数値です");
                    messageDsp.DspMsg("M3029");
                    textBoxSoFAX.Focus();
                    return false;
                }
                // FAXの文字数チェック
                if (textBoxSoFAX.TextLength > 13)
                {
                    //MessageBox.Show("FAXは13文字以下です");
                    messageDsp.DspMsg("M3012");
                    textBoxSoFAX.Focus();
                    return false;
                }
            }
            // 電話番号の半角数値チェック
            if (!String.IsNullOrEmpty(textBoxSoPhone.Text.Trim()))
            {
                if (!dataInputFormCheck.CheckNumeric(textBoxSoPhone.Text.Trim()))
                {
                    //MessageBox.Show("電話番号は半角数値です");
                    messageDsp.DspMsg("M3030");
                    textBoxSoPhone.Focus();
                    return false;
                }
                // 電話番号の文字数チェック
                if (textBoxSoPhone.TextLength > 13)
                {
                    //MessageBox.Show("電話番号は13文字以下です");
                    messageDsp.DspMsg("M3011");
                    textBoxSoPhone.Focus();
                    return false;
                }
            }

            // 管理フラグの適否
            if (checkBoxSoFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("管理フラグが不確定の状態です");
                messageDsp.DspMsg("M3008");
                checkBoxSoFlag.Focus();
                return false;
            }

            // 管理フラグの適否２
            if (checkBoxSoFlag.Checked == true)
            {
                //MessageBox.Show("登録では管理フラグは適用されません");
                messageDsp.DspMsg("M3027");
                checkBoxSoFlag.Focus();
                return false;
            }
            return true;
        }
        ///////////////////////////////
        //　8.2.1.2 営業所情報作成
        //メソッド名：GenerateDataAtRegistration()
        //引　数   ：なし
        //戻り値   ：営業所登録情報
        //機　能   ：登録データのセット
        ///////////////////////////////
        private M_SalesOffice GenerateDataAtRegistration()
        {
            return new M_SalesOffice
            {
                SoName = textBoxSoName.Text.Trim(),
                SoPostal = textBoxSoPostal.Text.Trim(),
                SoAddress = textBoxSoAddress.Text.Trim(),
                SoFAX = textBoxSoFAX.Text.Trim(),
                SoPhone = textBoxSoPhone.Text.Trim(),
                SoFlag = SoFlg,
                SoHidden = textBoxSoHidden.Text.Trim()
            };
        }
        ///////////////////////////////
        //　8.2.1.3 営業所情報登録
        //メソッド名：RegistrationSalesOffice()
        //引　数   ：営業所情報
        //戻り値   ：なし
        //機　能   ：営業所データの登録
        ///////////////////////////////
        private void RegistrationSalesOffice(M_SalesOffice regSalesOffice)
        {
            // 登録確認メッセージ
            DialogResult result = messageDsp.DspMsg("M3014");
            if (result == DialogResult.Cancel)
                return;

            // 営業所情報の登録
            bool flg = salesOfficeDataAccess.AddSalesOfficeData(regSalesOffice);
            if (flg == true)
                //MessageBox.Show("データを登録しました。");
                messageDsp.DspMsg("M3015");
            else
                //MessageBox.Show("データの登録に失敗しました。");
                messageDsp.DspMsg("M3016");

            textBoxSoID.Focus();

            // 入力エリアのクリア
            ClearInput();

            // データグリッドビューの表示
            GetDataGridView();

        }

        private void checkBoxSoFlag_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSoFlag.Checked)
            {
                SoFlg = 2;
                textBoxSoHidden.Enabled = true;
                return;
            }
            else
            {
                SoFlg = 0;
                textBoxSoHidden.Enabled = false;
                textBoxSoHidden.Text = "";
                return;
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            // 8.2.4.1 妥当な営業所データ取得
            if (!GetValidDataAtSelect())
                return;

            // 8.2.4.2 営業所情報抽出
            GenerateDataAtSelect();

            // 8.2.4.3 営業所抽出結果表示
            SetSelectData();
        }

        ///////////////////////////////
        //　8.2.4.1 妥当な営業所データ取得
        //メソッド名：GetValidDataAtSelect()
        //引　数   ：なし
        //戻り値   ：true or false
        //機　能   ：入力データの形式チェック
        //          ：エラーがない場合True
        //          ：エラーがある場合False
        ///////////////////////////////
        private bool GetValidDataAtSelect()
        {

            // 営業所IDの適否
            if (!String.IsNullOrEmpty(textBoxSoID.Text.Trim()))
            {
                // スタッフCDの半角英数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxSoID.Text.Trim()))
                {
                    //MessageBox.Show("営業所IDは全て半角数字入力です");
                    messageDsp.DspMsg("M3001");
                    textBoxSoID.Focus();
                    return false;
                }
                // 営業所IDの文字数チェック
                if (textBoxSoID.TextLength > 2)
                {
                    //MessageBox.Show("営業所IDは2文字です");
                    messageDsp.DspMsg("M3002");
                    textBoxSoID.Focus();
                    return false;
                }

            }

            // 営業所名の適否
            if (!String.IsNullOrEmpty(textBoxSoName.Text.Trim()))
            {
                // 営業所名の全角チェック
                if (!dataInputFormCheck.CheckFullWidth(textBoxSoName.Text.Trim()))
                {
                    //MessageBox.Show("営業所名は全て全角入力です");
                    messageDsp.DspMsg("M3005");
                    textBoxSoName.Focus();
                    return false;
                }
                // 営業所名の文字数チェック
                if (textBoxSoName.TextLength > 50)
                {
                    //MessageBox.Show("営業所名は50文字以下です");
                    messageDsp.DspMsg("M3006");
                    textBoxSoName.Focus();
                    return false;
                }
            }

            //郵便番号の検索不可チェック
            if (textBoxSoPostal.Text != "")
            {
                //MessageBox.Show("検索は営業所IDか営業所名で行ってください");
                messageDsp.DspMsg("M3026");
                textBoxSoPostal.Focus();
                return false;
            }
            //住所の検索不可チェック
            if (textBoxSoAddress.Text != "")
            {
                //MessageBox.Show("検索は営業所IDか営業所名で行ってください");
                messageDsp.DspMsg("M3026");
                textBoxSoAddress.Focus();
                return false;
            }
            //FAXの検索不可チェック
            if (textBoxSoFAX.Text != "")
            {
                //MessageBox.Show("検索は営業所IDか営業所名で行ってください");
                messageDsp.DspMsg("M3026");
                textBoxSoFAX.Focus();
                return false;
            }
            //電話番号の検索不可チェック
            if (textBoxSoPhone.Text != "")
            {
                //MessageBox.Show("検索は営業所IDか営業所名で行ってください");
                messageDsp.DspMsg("M3026");
                textBoxSoPhone.Focus();
                return false;
            }

            if (textBoxSoID.Text == "" && textBoxSoName.Text == "" && checkBoxSoFlag.Checked == false)
            {
                // データグリッドビューの表示
                SetFormDataGridView();
                return false;
            }
            // 管理フラグの適否
            if (checkBoxSoFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("管理フラグが不確定の状態です");
                messageDsp.DspMsg("M3008");
                checkBoxSoFlag.Focus();
                return false;
            }
            return true;

        }

        ///////////////////////////////
        //　8.2.4.2 営業所情報抽出
        //メソッド名：GenerateDataAtSelect()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：営業所情報の取得
        ///////////////////////////////

        public static string mSoID; //cSoIDを別クラス(SalesOfficeDataAccess)でも使用できるように定義
        public static string mSoName;
        public static bool mSoFlg;
        private void GenerateDataAtSelect()
        {
            M_SalesOffice selectCondition;

            mSoID = textBoxSoID.Text.Trim(); //IDはnullではなく、""で空白が入るのでstringで代入
            mSoName = textBoxSoName.Text.Trim();
            mSoFlg = checkBoxSoFlag.Checked;

            if (mSoID != "") //IDで検索
            {
                // 検索条件のセット
                selectCondition = new M_SalesOffice()
                {
                    SoID = int.Parse(textBoxSoID.Text.Trim()),
                    SoFlag = SoFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // 営業所データの抽出
                SalesOffice = salesOfficeDataAccess.GetSalesOfficeData(selectCondition);
                return;
            }
            else if (mSoName != "") // 名前で検索
            {
                // 検索条件のセット
                selectCondition = new M_SalesOffice()
                {
                    SoName = textBoxSoName.Text.Trim(),
                    SoFlag = SoFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // 営業所データの抽出
                SalesOffice = salesOfficeDataAccess.GetSalesOfficeData(selectCondition);
                return;
            }
            else if (mSoFlg == true) // ただの非表示一覧表示
            {
                // 検索条件のセット
                selectCondition = new M_SalesOffice()
                {
                    SoFlag = SoFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // 営業所データの抽出
                SalesOffice = salesOfficeDataAccess.GetSalesOfficeData(selectCondition);
                return;
            }
        }
        ///////////////////////////////
        //　8.2.4.3 営業所抽出結果表示
        //メソッド名：SetSelectData()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：営業所情報の表示
        ///////////////////////////////
        private void SetSelectData()
        {
            textBoxPageNo.Text = "1";

            int pageSize = int.Parse(textBoxPageSize.Text);

            dataGridViewSalesOffice.DataSource = SalesOffice;

            labelPage.Text = "/" + ((int)Math.Ceiling(SalesOffice.Count / (double)pageSize)) + "ページ";
            dataGridViewSalesOffice.Refresh();

            if (SalesOffice.Count == 0) //検索結果のデータ数が0ならエラー
            {
                messageDsp.DspMsg("M1025");
                SetFormDataGridView();
                return;
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            // 8.2.2.1 妥当な営業所データ取得
            if (!GetValidDataAtUpdate())
                return;

            // 8.2.2.2 営業所情報作成
            var updSalesOffice = GenerateDataAtUpdate();

            // 8.2.2.3 営業所情報更新
            UpdateSalesOffice(updSalesOffice);
        }

        ///////////////////////////////
        //　8.2.2.1 妥当な営業所データ取得
        //メソッド名：GetValidDataAtUpdate()
        //引　数   ：なし
        //戻り値   ：true or false
        //機　能   ：入力データの形式チェック
        //          ：エラーがない場合True
        //          ：エラーがある場合False
        ///////////////////////////////
        private bool GetValidDataAtUpdate()
        {

            // 営業所IDの未入力チェック
            if (!String.IsNullOrEmpty(textBoxSoID.Text.Trim()))
            {
                // 営業所IDの半角英数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxSoID.Text.Trim()))
                {
                    //MessageBox.Show("営業所IDは全て半角英数字入力です");
                    messageDsp.DspMsg("M3001");
                    textBoxSoID.Focus();
                    return false;
                }
                // 営業所IDの文字数チェック
                if (textBoxSoID.TextLength > 2)
                {
                    //MessageBox.Show("営業所IDは2文字です");
                    messageDsp.DspMsg("M3002");
                    textBoxSoID.Focus();
                    return false;
                }
                // 営業所IDの存在チェック
                if (!salesOfficeDataAccess.CheckSalesOfficeCDExistence(textBoxSoID.Text.Trim()))
                {
                    //MessageBox.Show("入力された営業所IDは存在しません");
                    messageDsp.DspMsg("M3017");
                    textBoxSoID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("営業所IDが入力されていません");
                messageDsp.DspMsg("M3004");
                textBoxSoID.Focus();
                return false;
            }


            // 営業所名の適否
            if (!String.IsNullOrEmpty(textBoxSoName.Text.Trim()))
            {
                // 営業所名の全角チェック
                if (!dataInputFormCheck.CheckFullWidth(textBoxSoName.Text.Trim()))
                {
                    //MessageBox.Show("営業所名は全て全角入力です");
                    messageDsp.DspMsg("M3005");
                    textBoxSoName.Focus();
                    return false;
                }
                // 営業所名の文字数チェック
                if (textBoxSoName.TextLength > 50)
                {
                    //MessageBox.Show("営業所名は50文字以下です");
                    messageDsp.DspMsg("M3006");
                    textBoxSoName.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("営業所名が入力されていません");
                messageDsp.DspMsg("M3007");
                textBoxSoName.Focus();
                return false;
            }

            // 管理フラグの適否
            if (checkBoxSoFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("管理フラグが不確定の状態です");
                messageDsp.DspMsg("M3008");
                checkBoxSoFlag.Focus();
                return false;
            }

            // 非表示理由の適否
            if (checkBoxSoFlag.Checked == true)
            {
                if (textBoxSoHidden.Text == "")
                {
                    //MessageBox.Show("非表示理由の入力が必要です");
                    messageDsp.DspMsg("M3031");
                    textBoxSoHidden.Focus();
                    return false;
                }
            }
            return true;
        }
        ///////////////////////////////
        //　8.2.2.2 営業所情報作成
        //メソッド名：GenerateDataAtUpdate()
        //引　数   ：なし
        //戻り値   ：営業所更新情報
        //機　能   ：更新データのセット
        ///////////////////////////////
        private M_SalesOffice GenerateDataAtUpdate()
        {
            return new M_SalesOffice
            {
                SoID = int.Parse(textBoxSoID.Text.Trim()),
                SoName = textBoxSoName.Text.Trim(),
                SoFlag = SoFlg,
                SoHidden = textBoxSoHidden.Text.Trim(),
            };
        }
        ///////////////////////////////
        //　8.2.2.3 営業所情報更新
        //メソッド名：UpdateSalesOffice()
        //引　数   ：営業所情報
        //戻り値   ：なし
        //機　能   ：営業所情報の更新
        ///////////////////////////////
        private void UpdateSalesOffice(M_SalesOffice updSalesOffice)
        {
            if (SoFlg == 0)
            {
                // 更新確認メッセージ
                DialogResult result = messageDsp.DspMsg("M3018");
                if (result == DialogResult.Cancel)
                    return;

                // 営業所情報の更新
                bool flg = salesOfficeDataAccess.UpdateSalesOfficeData(updSalesOffice);
                if (flg == true)
                    //MessageBox.Show("データを更新しました。");
                    messageDsp.DspMsg("M3019");
                else
                    //MessageBox.Show("データの更新に失敗しました。");
                    messageDsp.DspMsg("M3020");

                textBoxSoID.Focus();

                // 入力エリアのクリア
                ClearInput();

                // データグリッドビューの表示
                GetDataGridView();
            }

            else if (SoFlg == 2)
            {
                DeleteSalesOffice(updSalesOffice);
            }
        }
        private void DeleteSalesOffice(M_SalesOffice delSalesOffice)
        {

            // 更新確認メッセージ
            DialogResult result = messageDsp.DspMsg("M3022");
            if (result == DialogResult.Cancel)
                return;

            // 営業所情報の更新
            bool flg = salesOfficeDataAccess.DeleteSalesOfficeData(delSalesOffice);
            if (flg == true)
                //MessageBox.Show("データを削除しました。");
                messageDsp.DspMsg("M3023");
            else
                //MessageBox.Show("データの削除に失敗しました。");
                messageDsp.DspMsg("M3024");

            textBoxSoID.Focus();

            // 入力エリアのクリア
            ClearInput();

            // データグリッドビューの表示
            GetDataGridView();
        }

        private void dataGridViewSalesOffice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //クリックされた行データをテキストボックスへ
            textBoxSoID.Text = dataGridViewSalesOffice.Rows[dataGridViewSalesOffice.CurrentRow.Index].Cells[0].Value.ToString();
            textBoxSoName.Text = dataGridViewSalesOffice.Rows[dataGridViewSalesOffice.CurrentRow.Index].Cells[1].Value.ToString();
            textBoxSoAddress.Text = dataGridViewSalesOffice.Rows[dataGridViewSalesOffice.CurrentRow.Index].Cells[2].Value.ToString();
            textBoxSoPhone.Text = dataGridViewSalesOffice.Rows[dataGridViewSalesOffice.CurrentRow.Index].Cells[3].Value.ToString();
            textBoxSoPostal.Text = dataGridViewSalesOffice.Rows[dataGridViewSalesOffice.CurrentRow.Index].Cells[4].Value.ToString();
            textBoxSoFAX.Text = dataGridViewSalesOffice.Rows[dataGridViewSalesOffice.CurrentRow.Index].Cells[5].Value.ToString();
            int SoFlg2 = int.Parse(dataGridViewSalesOffice.Rows[dataGridViewSalesOffice.CurrentRow.Index].Cells[6].Value.ToString());
            if (SoFlg2 == 0)
            {
                checkBoxSoFlag.Checked = false;
            }
            else if (SoFlg2 == 2)
            {
                checkBoxSoFlag.Checked = true;
            }
            textBoxSoHidden.Text = dataGridViewSalesOffice.Rows[dataGridViewSalesOffice.CurrentRow.Index].Cells[7].Value.ToString();
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

        private void buttonLogout_Click(object sender, EventArgs e)
        {

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonFirstPage_Click(object sender, EventArgs e)
        {
            int pageSize = int.Parse(textBoxPageSize.Text);
            dataGridViewSalesOffice.DataSource = SalesOffice.Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewSalesOffice.Refresh();
            //ページ番号の設定
            textBoxPageNo.Text = "1";
        }

        private void buttonPreviousPage_Click(object sender, EventArgs e)
        {
            int pageSize = int.Parse(textBoxPageSize.Text);
            int pageNo = int.Parse(textBoxPageNo.Text) - 2;
            dataGridViewSalesOffice.DataSource = SalesOffice.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewSalesOffice.Refresh();
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
            int lastNo = (int)Math.Ceiling(SalesOffice.Count / (double)pageSize) - 1;
            //最終ページでなければ
            if (pageNo <= lastNo)
                dataGridViewSalesOffice.DataSource = SalesOffice.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewSalesOffice.Refresh();
            //ページ番号の設定
            int lastPage = (int)Math.Ceiling(SalesOffice.Count / (double)pageSize);
            if (pageNo >= lastPage)
                textBoxPageNo.Text = lastPage.ToString();
            else
                textBoxPageNo.Text = (pageNo + 1).ToString();
        }

        private void buttonLastPage_Click(object sender, EventArgs e)
        {
            int pageSize = int.Parse(textBoxPageSize.Text);
            //最終ページの計算
            int pageNo = (int)Math.Ceiling(SalesOffice.Count / (double)pageSize) - 1;
            dataGridViewSalesOffice.DataSource = SalesOffice.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewSalesOffice.Refresh();
            //ページ番号の設定
            textBoxPageNo.Text = (pageNo + 1).ToString();
        }
    }
}
