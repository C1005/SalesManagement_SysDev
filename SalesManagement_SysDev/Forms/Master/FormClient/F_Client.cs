﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagement_SysDev.Forms.Master.FormClient
{
    public partial class F_Client : Form
    {
        //メッセージ表示用クラスのインスタンス化
        MessageDsp messageDsp = new MessageDsp();
        //データベース顧客テーブルアクセス用クラスのインスタンス化
        DbAccess.ClientDataAccess clientDataAccess = new DbAccess.ClientDataAccess();
        //入力形式チェック用クラスのインスタンス化
        DataInputFormCheck dataInputFormCheck = new DataInputFormCheck();
        //データグリッドビュー用の顧客データ
        private static List<M_Client> Client;
        //管理フラグを数値型で入れるための変数
        int ClFlg;

        public F_Client()
        {
            InitializeComponent();
        }

        private void F_Client_Load(object sender, EventArgs e)
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
            dataGridViewClient.ReadOnly = true;
            //行内をクリックすることで行を選択
            dataGridViewClient.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //ヘッダー位置の指定
            dataGridViewClient.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

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

            // 顧客データの取得
            Client = clientDataAccess.GetClientData();

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
            dataGridViewClient.DataSource = Client.Skip(pageSize * pageNo).Take(pageSize).ToList();
            //各列幅の指定
            dataGridViewClient.Columns[0].Width = 100;
            dataGridViewClient.Columns[1].Width = 200;
            dataGridViewClient.Columns[2].Width = 110;
            dataGridViewClient.Columns[3].Width = 400;

            //各列の文字位置の指定
            dataGridViewClient.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewClient.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewClient.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewClient.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            //dataGridViewの総ページ数
            labelPage.Text = "/" + ((int)Math.Ceiling(Client.Count / (double)pageSize)) + "ページ";

            dataGridViewClient.Refresh();
        }

        private void buttonPageSizeChange_Click(object sender, EventArgs e)
        {
            SetDataGridView();
        }

        private void buttonFirstPage_Click(object sender, EventArgs e)
        {
            int pageSize = int.Parse(textBoxPageSize.Text);
            dataGridViewClient.DataSource = Client.Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewClient.Refresh();
            //ページ番号の設定
            textBoxPageNo.Text = "1";
        }

        private void buttonPreviousPage_Click(object sender, EventArgs e)
        {
            int pageSize = int.Parse(textBoxPageSize.Text);
            int pageNo = int.Parse(textBoxPageNo.Text) - 2;
            dataGridViewClient.DataSource = Client.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewClient.Refresh();
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
            int lastNo = (int)Math.Ceiling(Client.Count / (double)pageSize) - 1;
            //最終ページでなければ
            if (pageNo <= lastNo)
                dataGridViewClient.DataSource = Client.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewClient.Refresh();
            //ページ番号の設定
            int lastPage = (int)Math.Ceiling(Client.Count / (double)pageSize);
            if (pageNo >= lastPage)
                textBoxPageNo.Text = lastPage.ToString();
            else
                textBoxPageNo.Text = (pageNo + 1).ToString();
        }

        private void buttonLastPage_Click(object sender, EventArgs e)
        {
            int pageSize = int.Parse(textBoxPageSize.Text);
            //最終ページの計算
            int pageNo = (int)Math.Ceiling(Client.Count / (double)pageSize) - 1;
            dataGridViewClient.DataSource = Client.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewClient.Refresh();
            //ページ番号の設定
            textBoxPageNo.Text = (pageNo + 1).ToString();
        }

        private void buttonCusSearch_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private void OpenForm(string formName)
        {
            Form frm = new Form();
            //引数より、開くフォームを設定
            switch (formName)
            {
                case "営業所検索": //ボタンのテキスト名
                    frm = new Master.FormEmployee.F_SalesOffice(); //フォームの名前
                    break;
            }
            //選択されたフォームを開く
            frm.ShowDialog();

            //開いたフォームから戻ってきたら
            //メモリを解放する
            frm.Dispose();
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {

        }

        private void buttonClose_Click(object sender, EventArgs e)
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
            textBoxClID.Text = "";
            textBoxClName.Text = "";
            textBoxClHidden.Text = "";
            checkBoxClFlag.Checked = false;
            textBoxSoID.Text = "";
            textBoxClPostal.Text = "";
            textBoxClAddress.Text = "";
            textBoxClFax.Text = "";
            textBoxClPhone.Text = "";
        }

        private void checkBoxClFlag_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxClFlag.Checked)
            {
                ClFlg = 2;
                textBoxClHidden.Enabled = true;
                return;
            }
            else
            {
                ClFlg = 0;
                textBoxClHidden.Enabled = false;
                textBoxClHidden.Text = "";
                return;
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            // 入力エリアのクリア
            ClearInput();

            // データグリッドビューの表示
            SetFormDataGridView();
        }

        private void buttonList_Click(object sender, EventArgs e)
        {
            // データグリッドビューの表示
            SetFormDataGridView();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            // 8.2.4.1 妥当な顧客データ取得
            if (!GetValidDataAtSelect())
                return;

            // 8.2.4.2 顧客情報抽出
            GenerateDataAtSelect();

            // 8.2.4.3 顧客抽出結果表示
            SetSelectData();
        }

        ///////////////////////////////
        //　8.2.4.1 妥当な顧客データ取得
        //メソッド名：GetValidDataAtSlect()
        //引　数   ：なし
        //戻り値   ：true or false
        //機　能   ：入力データの形式チェック
        //          ：エラーがない場合True
        //          ：エラーがある場合False
        ///////////////////////////////
        private bool GetValidDataAtSelect()
        {

            // 顧客ID入力時チェック
            if (!String.IsNullOrEmpty(textBoxClID.Text.Trim()))
            {
                // 顧客IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxClID.Text.Trim()))
                {
                    //MessageBox.Show("顧客IDは全て半角数字入力です");
                    messageDsp.DspMsg("M1001");
                    textBoxClID.Focus();
                    return false;
                }
                // 顧客IDの文字数チェック
                if (textBoxClID.TextLength > 6)
                {
                    //MessageBox.Show("顧客IDは6文字までです");
                    messageDsp.DspMsg("M1002");
                    textBoxClID.Focus();
                    return false;
                }
            }


            // 顧客名入力時チェック
            if (!String.IsNullOrEmpty(textBoxClName.Text.Trim()))
            {
                // 顧客名の全角チェック
                if (!dataInputFormCheck.CheckFullWidth(textBoxClName.Text.Trim()))
                {
                    //MessageBox.Show("顧客名は全て全角入力です");
                    messageDsp.DspMsg("M1005");
                    textBoxClName.Focus();
                    return false;
                }
                // 顧客名の文字数チェック
                if (textBoxClName.TextLength > 50)
                {
                    //MessageBox.Show("顧客名は50文字以下です");
                    messageDsp.DspMsg("M1006");
                    textBoxClName.Focus();
                    return false;
                }
            }



            if (textBoxClID.Text == "" && textBoxClName.Text == "" && checkBoxClFlag.Checked == false)
            {
                // データグリッドビューの表示
                SetFormDataGridView();
                return false;
            }
            // 管理フラグの適否
            if (checkBoxClFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("管理フラグが不確定の状態です");
                messageDsp.DspMsg("M1008");
                checkBoxClFlag.Focus();
                return false;
            }
            return true;

        }

        ///////////////////////////////
        //　8.2.4.2 顧客情報抽出
        //メソッド名：GenerateDataAtSelect()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：顧客情報の取得
        ///////////////////////////////

        public static string mClID; //mClIDを別クラス(SalesOfficeDataAccess)でも使用できるように定義
        public static string mSoID;
        public static string mClName;
        public static bool mClFlg;
        private void GenerateDataAtSelect()
        {
            M_Client selectCondition;

            mClID = textBoxClID.Text.Trim(); //IDはnullではなく、""で空白が入るのでstringで代入
            mSoID = textBoxSoID.Text.Trim();
            mClName = textBoxClName.Text.Trim();
            mClFlg = checkBoxClFlag.Checked;

            if (mClID != "") //IDで検索
            {
                // 検索条件のセット
                selectCondition = new M_Client()
                {
                    ClID = int.Parse(textBoxClID.Text.Trim()),
                    ClFlag = ClFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // 営業所データの抽出
                Client = clientDataAccess.GetClientData(selectCondition);
                return;
            }

            else if (mSoID != "") // 名前で検索
            {
                // 検索条件のセット
                selectCondition = new M_Client()
                {
                    SoID = int.Parse(textBoxSoID.Text.Trim()),
                    ClFlag = ClFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // 営業所データの抽出
                Client = clientDataAccess.GetClientData(selectCondition);
                return;
            }


            else if (mClName != "") // 名前で検索
            {
                // 検索条件のセット
                selectCondition = new M_Client()
                {
                    ClName = textBoxClName.Text.Trim(),
                    ClFlag = ClFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // 営業所データの抽出
                Client = clientDataAccess.GetClientData(selectCondition);
                return;
            }
            else if (mClFlg == true) // ただの非表示一覧表示
            {
                // 検索条件のセット
                selectCondition = new M_Client()
                {
                    ClFlag = ClFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // 営業所データの抽出
                Client = clientDataAccess.GetClientData(selectCondition);
                return;
            }
        }
        ///////////////////////////////
        //　8.2.4.3 顧客抽出結果表示
        //メソッド名：SetSelectData()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：顧客情報の表示
        ///////////////////////////////
        private void SetSelectData()
        {
            textBoxPageNo.Text = "1";

            int pageSize = int.Parse(textBoxPageSize.Text);

            dataGridViewClient.DataSource = Client;

            labelPage.Text = "/" + ((int)Math.Ceiling(Client.Count / (double)pageSize)) + "ページ";
            dataGridViewClient.Refresh();

            if (Client.Count == 0) //検索結果のデータ数が0ならエラー
            {
                messageDsp.DspMsg("M1025");
                SetFormDataGridView();
            }
        }

        private void buttonRegist_Click(object sender, EventArgs e)
        {
            // 8.2.1.1 妥当な顧客データ取得
            if (!GetValidDataAtRegistration())
                return;

            // 8.2.1.2 顧客情報作成
            var regClient = GenerateDataAtRegistration();

            // 8.2.1.3 顧客情報登録
            RegistrationClient(regClient);
        }

        ///////////////////////////////
        //　8.2.1.1 妥当な顧客データ取得
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
                // 営業所IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxSoID.Text.Trim()))
                {
                    //MessageBox.Show("営業所IDは全て半角数字入力です");
                    messageDsp.DspMsg("M1001");
                    textBoxSoID.Focus();
                    return false;
                }
                // 営業所IDの文字数チェック
                if (textBoxSoID.TextLength > 2)
                {
                    //MessageBox.Show("営業所IDは2文字までです");
                    messageDsp.DspMsg("M1002");
                    textBoxSoID.Focus();
                    return false;
                }

                // 営業所IDの重複チェック
                if (clientDataAccess.CheckClientCDExistence(textBoxSoID.Text.Trim()))
                {
                    //MessageBox.Show("入力された顧客IDは既に存在します");
                    messageDsp.DspMsg("M1003");
                    textBoxSoID.Focus();
                    return false;
                }
                // 営業所IDが0ではないかチェック
                if (int.Parse(textBoxSoID.Text.Trim()) == 0)
                {
                    //MessageBox.Show("営業所IDは01から割り当ててください");
                    messageDsp.DspMsg("M1024");
                    textBoxSoID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("営業所IDが入力されていません");
                messageDsp.DspMsg("M1004");
                textBoxSoID.Focus();
                return false;
            }

            // 顧客名の適否
            if (!String.IsNullOrEmpty(textBoxClName.Text.Trim()))
            {
                // 顧客名の全角チェック
                if (!dataInputFormCheck.CheckFullWidth(textBoxClName.Text.Trim()))
                {
                    //MessageBox.Show("顧客名は全て全角入力です");
                    messageDsp.DspMsg("M1005");
                    textBoxClName.Focus();
                    return false;
                }
                // 顧客名の文字数チェック
                if (textBoxClName.TextLength > 50)
                {
                    //MessageBox.Show("顧客名は25文字以下です");
                    messageDsp.DspMsg("M1006");
                    textBoxClName.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("顧客名が入力されていません");
                messageDsp.DspMsg("M1007");
                textBoxClName.Focus();
                return false;
            }

            // 郵便番号の数値チェック
            if (!String.IsNullOrEmpty(textBoxClPostal.Text.Trim()))
            {
                if (!dataInputFormCheck.CheckNumeric(textBoxClPostal.Text.Trim()))
                {
                    //MessageBox.Show("郵便番号は半角数値です");
                    messageDsp.DspMsg("M3025");
                    textBoxClPostal.Focus();
                    return false;
                }
                // 郵便番号の文字数チェック
                if (textBoxClPostal.TextLength != 7)
                {
                    //MessageBox.Show("郵便番号は7文字です");
                    messageDsp.DspMsg("M3028");
                    textBoxClPostal.Focus();
                    return false;
                }
            }

            // 住所の文字数チェック
            if (!String.IsNullOrEmpty(textBoxClAddress.Text.Trim()))
            {
                if (textBoxClAddress.TextLength > 50)
                {
                    //MessageBox.Show("住所は50文字以下です");
                    messageDsp.DspMsg("M3013");
                    textBoxClAddress.Focus();
                    return false;
                }
            }
            // FAXの半角数値チェック
            if (!String.IsNullOrEmpty(textBoxClFax.Text.Trim()))
            {
                if (!dataInputFormCheck.CheckNumeric(textBoxClFax.Text.Trim()))
                {
                    //MessageBox.Show("FAXは半角数値です");
                    messageDsp.DspMsg("M3029");
                    textBoxClFax.Focus();
                    return false;
                }
                // FAXの文字数チェック
                if (textBoxClFax.TextLength > 13)
                {
                    //MessageBox.Show("FAXは13文字以下です");
                    messageDsp.DspMsg("M3012");
                    textBoxClFax.Focus();
                    return false;
                }
            }
            // 電話番号の半角数値チェック
            if (!String.IsNullOrEmpty(textBoxClPhone.Text.Trim()))
            {
                if (!dataInputFormCheck.CheckNumeric(textBoxClPhone.Text.Trim()))
                {
                    //MessageBox.Show("電話番号は半角数値です");
                    messageDsp.DspMsg("M3030");
                    textBoxClPhone.Focus();
                    return false;
                }
                // 電話番号の文字数チェック
                if (textBoxClPhone.TextLength > 13)
                {
                    //MessageBox.Show("電話番号は13文字以下です");
                    messageDsp.DspMsg("M3011");
                    textBoxClPhone.Focus();
                    return false;
                }
            }

            // 管理フラグの適否
            if (checkBoxClFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("管理フラグが不確定の状態です");
                messageDsp.DspMsg("M1008");
                checkBoxClFlag.Focus();
                return false;
            }

            // 管理フラグの適否２
            if (checkBoxClFlag.Checked == true)
            {
                //MessageBox.Show("登録では管理フラグは適用されません");
                messageDsp.DspMsg("M1022");
                checkBoxClFlag.Focus();
                return false;
            }

            return true;
        }
        ///////////////////////////////
        //　8.2.1.2 顧客情報作成
        //メソッド名：GenerateDataAtRegistration()
        //引　数   ：なし
        //戻り値   ：顧客登録情報
        //機　能   ：登録データのセット
        ///////////////////////////////
        private M_Client GenerateDataAtRegistration()
        {
            return new M_Client
            {
                ClID = int.Parse(textBoxClID.Text.Trim()),
                ClName = textBoxClName.Text.Trim(),
                ClFlag = ClFlg,
                ClHidden = textBoxClHidden.Text.Trim(),
            };
        }
        ///////////////////////////////
        //　8.2.1.3 顧客情報登録
        //メソッド名：RegistrationClient()
        //引　数   ：顧客情報
        //戻り値   ：なし
        //機　能   ：顧客データの登録
        ///////////////////////////////
        private void RegistrationClient(M_Client regClient)
        {
            // 登録確認メッセージ
            DialogResult result = messageDsp.DspMsg("M1010");
            if (result == DialogResult.Cancel)
                return;

            // 顧客情報の登録
            bool flg = clientDataAccess.AddClientData(regClient);
            if (flg == true)
                //MessageBox.Show("データを登録しました。");
                messageDsp.DspMsg("M1011");
            else
                //MessageBox.Show("データの登録に失敗しました。");
                messageDsp.DspMsg("M1012");

            textBoxClID.Focus();

            // 入力エリアのクリア
            ClearInput();

            // データグリッドビューの表示
            GetDataGridView();

        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            // 8.2.2.1 妥当な顧客データ取得
            if (!GetValidDataAtUpdate())
                return;

            // 8.2.2.2 顧客情報作成
            var updClient = GenerateDataAtUpdate();

            // 8.2.2.3 顧客情報更新
            UpdateClient(updClient);
        }

        ///////////////////////////////
        //　8.2.2.1 妥当な顧客データ取得
        //メソッド名：GetValidDataAtUpdate()
        //引　数   ：なし
        //戻り値   ：true or false
        //機　能   ：入力データの形式チェック
        //          ：エラーがない場合True
        //          ：エラーがある場合False
        ///////////////////////////////
        private bool GetValidDataAtUpdate()
        {

            // 顧客IDの未入力チェック
            if (!String.IsNullOrEmpty(textBoxClID.Text.Trim()))
            {
                // 顧客IDの半角英数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxClID.Text.Trim()))
                {
                    //MessageBox.Show("顧客IDは全て半角英数字入力です");
                    messageDsp.DspMsg("M1001");
                    textBoxClID.Focus();
                    return false;
                }
                // 顧客IDの文字数チェック
                if (textBoxClID.TextLength > 2)
                {
                    //MessageBox.Show("顧客IDは2文字です");
                    messageDsp.DspMsg("M1002");
                    textBoxClID.Focus();
                    return false;
                }
                // 顧客IDの存在チェック
                if (!clientDataAccess.CheckClientCDExistence(textBoxClID.Text.Trim()))
                {
                    //MessageBox.Show("入力された顧客IDは存在しません");
                    messageDsp.DspMsg("M1013");
                    textBoxClID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("顧客IDが入力されていません");
                messageDsp.DspMsg("M1004");
                textBoxClID.Focus();
                return false;
            }

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
                if (!clientDataAccess.CheckClientCDExistence(textBoxSoID.Text.Trim()))
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

            // 顧客名の適否
            if (!String.IsNullOrEmpty(textBoxClName.Text.Trim()))
            {
                // 顧客名の全角チェック
                if (!dataInputFormCheck.CheckFullWidth(textBoxClName.Text.Trim()))
                {
                    //MessageBox.Show("顧客名は全て全角入力です");
                    messageDsp.DspMsg("M1005");
                    textBoxClName.Focus();
                    return false;
                }
                // 顧客名の文字数チェック
                if (textBoxClName.TextLength > 50)
                {
                    //MessageBox.Show("顧客名は25文字以下です");
                    messageDsp.DspMsg("M1006");
                    textBoxClName.Focus();
                    return false;
                }
                // 顧客IDの存在チェック
                if (!clientDataAccess.CheckClientCDExistence(textBoxClName.Text.Trim()))
                {
                    //MessageBox.Show("入力された顧客IDは存在しません");
                    messageDsp.DspMsg("M1013");
                    textBoxClName.Focus();
                    return false;
                }
                else
                {
                    //MessageBox.Show("顧客名が入力されていません");
                    messageDsp.DspMsg("M1007");
                    textBoxClName.Focus();
                    return false;
                }
            }
            // 郵便番号の数値チェック
            if (!String.IsNullOrEmpty(textBoxClPostal.Text.Trim()))
            {
                if (!dataInputFormCheck.CheckNumeric(textBoxClPostal.Text.Trim()))
                {
                    //MessageBox.Show("郵便番号は半角数値です");
                    messageDsp.DspMsg("M3025");
                    textBoxClPostal.Focus();
                    return false;
                }
                // 郵便番号の文字数チェック
                if (textBoxClPostal.TextLength != 7)
                {
                    //MessageBox.Show("郵便番号は7文字です");
                    messageDsp.DspMsg("M3028");
                    textBoxClPostal.Focus();
                    return false;
                }
                // 顧客IDの存在チェック
                if (!clientDataAccess.CheckClientCDExistence(textBoxClPostal.Text.Trim()))
                {
                    //MessageBox.Show("入力された顧客IDは存在しません");
                    messageDsp.DspMsg("M1013");
                    textBoxClPostal.Focus();
                    return false;
                }
            }

            // 住所の文字数チェック
            if (!String.IsNullOrEmpty(textBoxClAddress.Text.Trim()))
            {
                if (textBoxClAddress.TextLength > 50)
                {
                    //MessageBox.Show("住所は50文字以下です");
                    messageDsp.DspMsg("M3013");
                    textBoxClAddress.Focus();
                    return false;
                }
                // 顧客IDの存在チェック
                if (!clientDataAccess.CheckClientCDExistence(textBoxClAddress.Text.Trim()))
                {
                    //MessageBox.Show("入力された顧客IDは存在しません");
                    messageDsp.DspMsg("M1013");
                    textBoxClAddress.Focus();
                    return false;
                }
            }
            // FAXの半角数値チェック
            if (!String.IsNullOrEmpty(textBoxClFax.Text.Trim()))
            {
                if (!dataInputFormCheck.CheckNumeric(textBoxClFax.Text.Trim()))
                {
                    //MessageBox.Show("FAXは半角数値です");
                    messageDsp.DspMsg("M3029");
                    textBoxClFax.Focus();
                    return false;
                }
                // FAXの文字数チェック
                if (textBoxClFax.TextLength > 13)
                {
                    //MessageBox.Show("FAXは13文字以下です");
                    messageDsp.DspMsg("M3012");
                    textBoxClFax.Focus();
                    return false;
                }
                // 顧客IDの存在チェック
                if (!clientDataAccess.CheckClientCDExistence(textBoxClFax.Text.Trim()))
                {
                    //MessageBox.Show("入力された顧客IDは存在しません");
                    messageDsp.DspMsg("M1013");
                    textBoxClFax.Focus();
                    return false;
                }
            }
            // 電話番号の半角数値チェック
            if (!String.IsNullOrEmpty(textBoxClPhone.Text.Trim()))
            {
                if (!dataInputFormCheck.CheckNumeric(textBoxClPhone.Text.Trim()))
                {
                    //MessageBox.Show("電話番号は半角数値です");
                    messageDsp.DspMsg("M3030");
                    textBoxClPhone.Focus();
                    return false;
                }
                // 電話番号の文字数チェック
                if (textBoxClPhone.TextLength > 13)
                {
                    //MessageBox.Show("電話番号は13文字以下です");
                    messageDsp.DspMsg("M3011");
                    textBoxClPhone.Focus();
                    return false;
                }
                // 顧客IDの存在チェック
                if (!clientDataAccess.CheckClientCDExistence(textBoxClID.Text.Trim()))
                {
                    //MessageBox.Show("入力された顧客IDは存在しません");
                    messageDsp.DspMsg("M1013");
                    textBoxClID.Focus();
                    return false;
                }
            }

            // 管理フラグの適否
            if (checkBoxClFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("管理フラグが不確定の状態です");
                messageDsp.DspMsg("M1008");
                checkBoxClFlag.Focus();
                return false;
            }

            // 非表示理由の適否
            if (checkBoxClFlag.Checked == true)
            {
                if (textBoxClHidden.Text == "")
                {
                    //MessageBox.Show("非表示理由の入力が必要です");
                    messageDsp.DspMsg("M1023");
                    textBoxClHidden.Focus();
                    return false;
                }
            }
            return true;
        }
        ///////////////////////////////
        //　8.2.2.2 顧客情報作成
        //メソッド名：GenerateDataAtUpdate()
        //引　数   ：なし
        //戻り値   ：顧客更新情報
        //機　能   ：更新データのセット
        ///////////////////////////////
        private M_Client GenerateDataAtUpdate()
        {
            return new M_Client
            {
                ClID = int.Parse(textBoxClID.Text.Trim()),
                ClName = textBoxClName.Text.Trim(),
                ClFlag = ClFlg,
                ClHidden = textBoxClHidden.Text.Trim(),
            };
        }
        ///////////////////////////////
        //　8.2.2.3 顧客情報更新
        //メソッド名：UpdateClient()
        //引　数   ：顧客情報
        //戻り値   ：なし
        //機　能   ：顧客情報の更新
        ///////////////////////////////
        private void UpdateClient(M_Client updClient)
        {
            if (ClFlg == 0)
            {
                // 更新確認メッセージ
                DialogResult result = messageDsp.DspMsg("M1014");
                if (result == DialogResult.Cancel)
                    return;

                // 顧客情報の更新
                bool flg = clientDataAccess.UpdateClientData(updClient);
                if (flg == true)
                    //MessageBox.Show("データを更新しました。");
                    messageDsp.DspMsg("M1015");
                else
                    //MessageBox.Show("データの更新に失敗しました。");
                    messageDsp.DspMsg("M1016");

                textBoxClID.Focus();

                // 入力エリアのクリア
                ClearInput();

                // データグリッドビューの表示
                GetDataGridView();
            }

            else if (ClFlg == 2)
            {
                DeleteClient(updClient);
            }
        }
        private void DeleteClient(M_Client delClient)
        {

            // 更新確認メッセージ
            DialogResult result = messageDsp.DspMsg("M1018");
            if (result == DialogResult.Cancel)
                return;

            // 顧客情報の更新
            bool flg = clientDataAccess.DeleteClientData(delClient);
            if (flg == true)
                //MessageBox.Show("データを削除しました。");
                messageDsp.DspMsg("M1019");
            else
                //MessageBox.Show("データの削除に失敗しました。");
                messageDsp.DspMsg("M1020");

            textBoxClID.Focus();

            // 入力エリアのクリア
            ClearInput();

            // データグリッドビューの表示
            GetDataGridView();
        }

        private void dataGridViewClient_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //クリックされた行データをテキストボックスへ
            textBoxClID.Text = dataGridViewClient.Rows[dataGridViewClient.CurrentRow.Index].Cells[0].Value.ToString();
            textBoxSoID.Text = dataGridViewClient.Rows[dataGridViewClient.CurrentRow.Index].Cells[1].Value.ToString();
            textBoxClName.Text = dataGridViewClient.Rows[dataGridViewClient.CurrentRow.Index].Cells[2].Value.ToString();
            textBoxClPostal.Text = dataGridViewClient.Rows[dataGridViewClient.CurrentRow.Index].Cells[3].Value.ToString();
            textBoxClAddress.Text = dataGridViewClient.Rows[dataGridViewClient.CurrentRow.Index].Cells[4].Value.ToString();
            textBoxClFax.Text = dataGridViewClient.Rows[dataGridViewClient.CurrentRow.Index].Cells[5].Value.ToString();
            textBoxClPhone.Text = dataGridViewClient.Rows[dataGridViewClient.CurrentRow.Index].Cells[6].Value.ToString();
            int ClFlg2 = int.Parse(dataGridViewClient.Rows[dataGridViewClient.CurrentRow.Index].Cells[7].Value.ToString());
            if (ClFlg2 == 0)
            {
                checkBoxClFlag.Checked = false;
            }
            else if (ClFlg2 == 2)
            {
                checkBoxClFlag.Checked = true;
            }
            textBoxClHidden.Text = dataGridViewClient.Rows[dataGridViewClient.CurrentRow.Index].Cells[8].Value.ToString();
        }
    }
}
