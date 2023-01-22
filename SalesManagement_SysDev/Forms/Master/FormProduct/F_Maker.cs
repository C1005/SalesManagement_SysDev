using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagement_SysDev.Forms.Master.FormProduct
{
    public partial class F_Maker : Form
    {
        //メッセージ表示用クラスのインスタンス化
        MessageDsp messageDsp = new MessageDsp();
        //データベースメーカテーブルアクセス用クラスのインスタンス化
        DbAccess.MakerDataAccess makerDataAccess = new DbAccess.MakerDataAccess();
        //入力形式チェック用クラスのインスタンス化
        DataInputFormCheck dataInputFormCheck = new DataInputFormCheck();
        //データグリッドビュー用のメーカデータ
        private static List<M_Maker> Maker;
        //管理フラグを数値型で入れるための変数
        int MaFlg;

        public F_Maker()
        {
            InitializeComponent();
        }

        private void F_Maker_Load(object sender, EventArgs e)
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
            dataGridViewMaker.ReadOnly = true;
            //行内をクリックすることで行を選択
            dataGridViewMaker.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //ヘッダー位置の指定
            dataGridViewMaker.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

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
            // メーカデータの取得
            Maker = makerDataAccess.GetMakerData();

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
            dataGridViewMaker.DataSource = Maker.Skip(pageSize * pageNo).Take(pageSize).ToList();

            if (Maker.Count == 0)
            {
                labelNoTable.Visible = true;
            }
            else
            {
                labelNoTable.Visible = false;
            }

            //各列幅の指定
            dataGridViewMaker.Columns[0].Width = 100;
            dataGridViewMaker.Columns[1].Width = 130;
            dataGridViewMaker.Columns[2].Width = 300;
            dataGridViewMaker.Columns[3].Width = 110;
            dataGridViewMaker.Columns[4].Width = 100;
            dataGridViewMaker.Columns[5].Width = 110;
            dataGridViewMaker.Columns[6].Width = 120;
            dataGridViewMaker.Columns[7].AutoSizeMode = (DataGridViewAutoSizeColumnMode)DataGridViewAutoSizeColumnsMode.Fill;

            //各列の文字位置の指定
            dataGridViewMaker.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewMaker.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewMaker.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewMaker.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewMaker.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewMaker.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewMaker.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewMaker.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            //dataGridViewの総ページ数
            labelPage.Text = "/" + ((int)Math.Ceiling(Maker.Count / (double)pageSize)) + "ページ";

            dataGridViewMaker.Refresh();
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
            dataGridViewMaker.DataSource = Maker.Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewMaker.Refresh();
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
            dataGridViewMaker.DataSource = Maker.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewMaker.Refresh();
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
            int lastNo = (int)Math.Ceiling(Maker.Count / (double)pageSize) - 1;
            //最終ページでなければ
            if (pageNo <= lastNo)
                dataGridViewMaker.DataSource = Maker.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewMaker.Refresh();
            //ページ番号の設定
            int lastPage = (int)Math.Ceiling(Maker.Count / (double)pageSize);
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
            int pageNo = (int)Math.Ceiling(Maker.Count / (double)pageSize) - 1;
            dataGridViewMaker.DataSource = Maker.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewMaker.Refresh();
            //ページ番号の設定
            textBoxPageNo.Text = (pageNo + 1).ToString();
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
            textBoxMaID.Text = "";
            textBoxMaName.Text = "";
            textBoxMaHidden.Text = "";
            textBoxMaPostal.Text = "";
            textBoxMaAddress.Text = "";
            textBoxMaFAX.Text = "";
            textBoxMaPhone.Text = "";
            checkBoxMaFlag.Checked = false;
        }

        private void buttonRegist_Click(object sender, EventArgs e)
        {
            // 8.2.1.1 妥当なメーカデータ取得
            if (!GetValidDataAtRegistration())
                return;

            // 8.2.1.2 メーカ情報作成
            var regMaker = GenerateDataAtRegistration();

            // 8.2.1.3 メーカ情報登録
            RegistrationMaker(regMaker);
        }

        ///////////////////////////////
        //　8.2.1.1 妥当なメーカデータ取得
        //メソッド名：GetValidDataAtRegistration()
        //引　数   ：なし
        //戻り値   ：true or false
        //機　能   ：入力データの形式チェック
        //          ：エラーがない場合True
        //          ：エラーがある場合False
        ///////////////////////////////
        private bool GetValidDataAtRegistration()
        {
            // メーカ名の適否
            if (!String.IsNullOrEmpty(textBoxMaName.Text.Trim()))
            {
                // メーカ名の全角チェック
                if (!dataInputFormCheck.CheckFullWidth(textBoxMaName.Text.Trim()))
                {
                    //MessageBox.Show("メーカ名は全て全角入力です");
                    messageDsp.DspMsg("M2005");
                    textBoxMaName.Focus();
                    return false;
                }
                // メーカ名の文字数チェック
                if (textBoxMaName.TextLength > 50)
                {
                    //MessageBox.Show("メーカ名は50文字以下です");
                    messageDsp.DspMsg("M2006");
                    textBoxMaName.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("メーカ名が入力されていません");
                messageDsp.DspMsg("M2007");
                textBoxMaName.Focus();
                return false;
            }

            // 郵便番号の数値チェック
            if (!String.IsNullOrEmpty(textBoxMaPostal.Text.Trim()))
            {
                if (!dataInputFormCheck.CheckNumericWithHyphen(textBoxMaPostal.Text.Trim()))
                {
                    //MessageBox.Show("郵便番号は半角数値です");
                    messageDsp.DspMsg("M2032");
                    textBoxMaPostal.Focus();
                    return false;
                }
                // 郵便番号の文字数チェック
                if (textBoxMaPostal.TextLength != 7)
                {
                    //MessageBox.Show("郵便番号は7文字です");
                    messageDsp.DspMsg("M2011");
                    textBoxMaPostal.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("郵便番号が入力されていません");
                messageDsp.DspMsg("M2033");
                textBoxMaPostal.Focus();
                return false;
            }

            // 住所の文字数チェック
            if (!String.IsNullOrEmpty(textBoxMaAddress.Text.Trim()))
            {
                if (textBoxMaAddress.TextLength > 50)
                {
                    //MessageBox.Show("住所は50文字以下です");
                    messageDsp.DspMsg("M2014");
                    textBoxMaAddress.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("住所が入力されていません");
                messageDsp.DspMsg("M2034");
                textBoxMaAddress.Focus();
                return false;
            }

            // FAXの半角数値チェック
            if (!String.IsNullOrEmpty(textBoxMaFAX.Text.Trim()))
            {
                if (!dataInputFormCheck.CheckNumericWithHyphen(textBoxMaFAX.Text.Trim()))
                {
                    //MessageBox.Show("FAXは半角数値です");
                    messageDsp.DspMsg("M2035");
                    textBoxMaFAX.Focus();
                    return false;
                }
                // FAXの文字数チェック
                if (textBoxMaFAX.TextLength > 13)
                {
                    //MessageBox.Show("FAXは13文字以下です");
                    messageDsp.DspMsg("M2013");
                    textBoxMaFAX.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("FAXが入力されていません");
                messageDsp.DspMsg("M2036");
                textBoxMaFAX.Focus();
                return false;
            }

            // 電話番号の半角数値チェック
            if (!String.IsNullOrEmpty(textBoxMaPhone.Text.Trim()))
            {
                if (!dataInputFormCheck.CheckNumericWithHyphen(textBoxMaPhone.Text.Trim()))
                {
                    //MessageBox.Show("電話番号は半角数値です");
                    messageDsp.DspMsg("M2037");
                    textBoxMaPhone.Focus();
                    return false;
                }
                // 電話番号の文字数チェック
                if (textBoxMaPhone.TextLength > 13)
                {
                    //MessageBox.Show("電話番号は13文字以下です");
                    messageDsp.DspMsg("M2012");
                    textBoxMaPhone.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("電話番号が入力されていません");
                messageDsp.DspMsg("M2038");
                textBoxMaPhone.Focus();
                return false;
            }

            // 管理フラグの適否
            if (checkBoxMaFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("管理フラグが不確定の状態です");
                messageDsp.DspMsg("M2008");
                checkBoxMaFlag.Focus();
                return false;
            }

            // 管理フラグの適否２
            if (checkBoxMaFlag.Checked == true)
            {
                //MessageBox.Show("登録では管理フラグは適用されません");
                messageDsp.DspMsg("M2039");
                checkBoxMaFlag.Focus();
                return false;
            }

            return true;
        }
        ///////////////////////////////
        //　8.2.1.2 メーカ情報作成
        //メソッド名：GenerateDataAtRegistration()
        //引　数   ：なし
        //戻り値   ：メーカ登録情報
        //機　能   ：登録データのセット
        ///////////////////////////////
        private M_Maker GenerateDataAtRegistration()
        {
            return new M_Maker
            {
                MaName = textBoxMaName.Text.Trim(),
                MaFlag = MaFlg,
                MaHidden = textBoxMaHidden.Text.Trim(),
                MaPostal = textBoxMaPostal.Text.Trim(),
                MaAdress = textBoxMaAddress.Text.Trim(),
                MaFAX = textBoxMaFAX.Text.Trim(),
                MaPhone = textBoxMaPhone.Text.Trim(),
            };
        }
        ///////////////////////////////
        //　8.2.1.3 メーカ情報登録
        //メソッド名：RegistrationMaker()
        //引　数   ：メーカ情報
        //戻り値   ：なし
        //機　能   ：メーカデータの登録
        ///////////////////////////////
        private void RegistrationMaker(M_Maker regMaker)
        {
            // 登録確認メッセージ
            DialogResult result = messageDsp.DspMsg("M2015");
            if (result == DialogResult.Cancel)
                return;

            // メーカ情報の登録
            bool flg = makerDataAccess.AddMakerData(regMaker);
            if (flg == true)
                //MessageBox.Show("データを登録しました。");
                messageDsp.DspMsg("M2016");
            else
                //MessageBox.Show("データの登録に失敗しました。");
                messageDsp.DspMsg("M2017");

            textBoxMaID.Focus();

            // 入力エリアのクリア
            ClearInput();

            // データグリッドビューの表示
            GetDataGridView();

        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            // 8.2.4.1 妥当なメーカデータ取得
            if (!GetValidDataAtSelect())
                return;

            // 8.2.4.2 メーカ情報抽出
            GenerateDataAtSelect();

            // 8.2.4.3 メーカ抽出結果表示
            SetSelectData();
        }

        ///////////////////////////////
        //　8.2.4.1 妥当なメーカデータ取得
        //メソッド名：GetValidDataAtSlect()
        //引　数   ：なし
        //戻り値   ：true or false
        //機　能   ：入力データの形式チェック
        //          ：エラーがない場合True
        //          ：エラーがある場合False
        ///////////////////////////////
        private bool GetValidDataAtSelect()
        {

            // メーカID入力時チェック
            if (!String.IsNullOrEmpty(textBoxMaID.Text.Trim()))
            {
                // メーカIDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxMaID.Text.Trim()))
                {
                    //MessageBox.Show("メーカIDは全て半角数字入力です");
                    messageDsp.DspMsg("M2001");
                    textBoxMaID.Focus();
                    return false;
                }
                // メーカIDの文字数チェック
                if (textBoxMaID.TextLength > 4)
                {
                    //MessageBox.Show("メーカIDは4文字までです");
                    messageDsp.DspMsg("M2002");
                    textBoxMaID.Focus();
                    return false;
                }
            }
            // メーカ名入力時チェック
            if (!String.IsNullOrEmpty(textBoxMaName.Text.Trim()))
            {
                // メーカ名の全角チェック
                if (!dataInputFormCheck.CheckFullWidth(textBoxMaName.Text.Trim()))
                {
                    //MessageBox.Show("メーカ名は全て全角入力です");
                    messageDsp.DspMsg("M2005");
                    textBoxMaName.Focus();
                    return false;
                }
                // メーカ名の文字数チェック
                if (textBoxMaName.TextLength > 50)
                {
                    //MessageBox.Show("メーカ名は50文字以下です");
                    messageDsp.DspMsg("M2006");
                    textBoxMaName.Focus();
                    return false;
                }
            }

            //郵便番号の検索不可チェック
            if (textBoxMaPostal.Text != "")
            {
                //MessageBox.Show("郵便番号は検索対象外です");
                messageDsp.DspMsg("M2027");
                textBoxMaPostal.Focus();
                return false;
            }
            //住所の検索不可チェック
            if (textBoxMaAddress.Text != "")
            {
                //MessageBox.Show("住所は検索対象外です");
                messageDsp.DspMsg("M2028");
                textBoxMaAddress.Focus();
                return false;
            }
            //FAXの検索不可チェック
            if (textBoxMaFAX.Text != "")
            {
                //MessageBox.Show("FAXは検索対象外です");
                messageDsp.DspMsg("M2029");
                textBoxMaFAX.Focus();
                return false;
            }
            //電話番号の検索不可チェック
            if (textBoxMaPhone.Text != "")
            {
                //MessageBox.Show("電話番号は検索対象外です");
                messageDsp.DspMsg("M2030");
                textBoxMaPhone.Focus();
                return false;
            }

            // 管理フラグの適否
            if (checkBoxMaFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("管理フラグが不確定の状態です");
                messageDsp.DspMsg("M2008");
                checkBoxMaFlag.Focus();
                return false;
            }
            return true;

        }
        ///////////////////////////////
        //　8.2.4.2 メーカ情報抽出
        //メソッド名：GenerateDataAtSelect()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：メーカ情報の取得
        ///////////////////////////////

        public static string mMaID; //cSoIDを別クラス(SalesOfficeDataAccess)でも使用できるように定義
        public static string mMaName;
        public static bool mMaFlg;
        public static string mMaPostal;
        public static string mMaAdress;
        public static string mMaFAX;
        public static string mMaPhone;
        private void GenerateDataAtSelect()
        {
            M_Maker selectCondition;

            mMaID = textBoxMaID.Text.Trim(); //IDはnullではなく、""で空白が入るのでstringで代入
            mMaName = textBoxMaName.Text.Trim();
            mMaFlg = checkBoxMaFlag.Checked;

            if (mMaID != "") //IDで検索
            {
                // 検索条件のセット
                selectCondition = new M_Maker()
                {
                    MaID = int.Parse(textBoxMaID.Text.Trim()),
                    MaFlag = MaFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // 営業所データの抽出
                Maker = makerDataAccess.GetMakerData(selectCondition);
                return;
            }
            else if (mMaName != "") // 名前で検索
            {
                // 検索条件のセット
                selectCondition = new M_Maker()
                {
                    MaName = textBoxMaName.Text.Trim(),
                    MaFlag = MaFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // 営業所データの抽出
                Maker = makerDataAccess.GetMakerData(selectCondition);
                return;
            }
            else if (mMaFlg == true) // ただの非表示一覧表示
            {
                // 検索条件のセット
                selectCondition = new M_Maker()
                {
                    MaFlag = MaFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // 営業所データの抽出
                Maker = makerDataAccess.GetMakerData(selectCondition);
                return;
            }
        }
        ///////////////////////////////
        //　8.2.4.3 メーカ抽出結果表示
        //メソッド名：SetSelectData()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：メーカ情報の表示
        ///////////////////////////////
        private void SetSelectData()
        {
            textBoxPageNo.Text = "1";

            int pageSize = int.Parse(textBoxPageSize.Text);

            dataGridViewMaker.DataSource = Maker;
            if (Maker.Count == 0)
            {
                labelNoTable.Visible = true;
            }
            else
            {
                labelNoTable.Visible = false;
            }

            labelPage.Text = "/" + ((int)Math.Ceiling(Maker.Count / (double)pageSize)) + "ページ";
            dataGridViewMaker.Refresh();

            if (Maker.Count == 0) //検索結果のデータ数が0ならエラー
            {
                messageDsp.DspMsg("M2031");
                SetFormDataGridView();
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            // 8.2.2.1 妥当な役職データ取得
            if (!GetValidDataAtUpdate())
                return;

            // 8.2.2.2 役職情報作成
            var updMaker = GenerateDataAtUpdate();

            // 8.2.2.3 役職情報更新
            UpdateMaker(updMaker);
        }

        ///////////////////////////////
        //　8.2.2.1 妥当なメーカデータ取得
        //メソッド名：GetValidDataAtUpdate()
        //引　数   ：なし
        //戻り値   ：true or false
        //機　能   ：入力データの形式チェック
        //          ：エラーがない場合True
        //          ：エラーがある場合False
        ///////////////////////////////
        private bool GetValidDataAtUpdate()
        {
            // メーカIDの未入力チェック
            if (!String.IsNullOrEmpty(textBoxMaID.Text.Trim()))
            {
                // メーカIDの半角英数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxMaID.Text.Trim()))
                {
                    //MessageBox.Show("メーカIDは全て半角英数字入力です");
                    messageDsp.DspMsg("M2001");
                    textBoxMaID.Focus();
                    return false;
                }
                // メーカIDの文字数チェック
                if (textBoxMaID.TextLength > 4)
                {
                    //MessageBox.Show("メーカIDは4文字です");
                    messageDsp.DspMsg("M2002");
                    textBoxMaID.Focus();
                    return false;
                }
                // メーカIDの存在チェック
                if (!makerDataAccess.CheckMakerCDExistence(textBoxMaID.Text.Trim()))
                {
                    //MessageBox.Show("入力されたメーカIDは存在しません");
                    messageDsp.DspMsg("M2018");
                    textBoxMaID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("メーカIDが入力されていません");
                messageDsp.DspMsg("M2004");
                textBoxMaID.Focus();
                return false;
            }

            // メーカ名の適否
            if (!String.IsNullOrEmpty(textBoxMaName.Text.Trim()))
            {
                // メーカ名の全角チェック
                if (!dataInputFormCheck.CheckFullWidth(textBoxMaName.Text.Trim()))
                {
                    //MessageBox.Show("メーカ名は全て全角入力です");
                    messageDsp.DspMsg("M2005");
                    textBoxMaName.Focus();
                    return false;
                }
                // メーカ名の文字数チェック
                if (textBoxMaName.TextLength > 50)
                {
                    //MessageBox.Show("メーカ名は50文字以下です");
                    messageDsp.DspMsg("M2006");
                    textBoxMaName.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("メーカ名が入力されていません");
                messageDsp.DspMsg("M2007");
                textBoxMaName.Focus();
                return false;
            }

            // 郵便番号の数値チェック
            if (!String.IsNullOrEmpty(textBoxMaPostal.Text.Trim()))
            {
                if (!dataInputFormCheck.CheckNumericWithHyphen(textBoxMaPostal.Text.Trim()))
                {
                    //MessageBox.Show("郵便番号は半角数値です");
                    messageDsp.DspMsg("M2032");
                    textBoxMaPostal.Focus();
                    return false;
                }
                // 郵便番号の文字数チェック
                if (textBoxMaPostal.TextLength != 7)
                {
                    //MessageBox.Show("郵便番号は7文字です");
                    messageDsp.DspMsg("M2011");
                    textBoxMaPostal.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("郵便番号が入力されていません");
                messageDsp.DspMsg("M2033");
                textBoxMaPostal.Focus();
                return false;
            }

            // 住所の文字数チェック
            if (!String.IsNullOrEmpty(textBoxMaAddress.Text.Trim()))
            {
                if (textBoxMaAddress.TextLength > 50)
                {
                    //MessageBox.Show("住所は50文字以下です");
                    messageDsp.DspMsg("M2014");
                    textBoxMaAddress.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("住所が入力されていません");
                messageDsp.DspMsg("M2034");
                textBoxMaAddress.Focus();
                return false;
            }

            // FAXの半角数値チェック
            if (!String.IsNullOrEmpty(textBoxMaFAX.Text.Trim()))
            {
                if (!dataInputFormCheck.CheckNumericWithHyphen(textBoxMaFAX.Text.Trim()))
                {
                    //MessageBox.Show("FAXは半角数値です");
                    messageDsp.DspMsg("M2035");
                    textBoxMaFAX.Focus();
                    return false;
                }
                // FAXの文字数チェック
                if (textBoxMaFAX.TextLength > 13)
                {
                    //MessageBox.Show("FAXは13文字以下です");
                    messageDsp.DspMsg("M2013");
                    textBoxMaFAX.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("FAXが入力されていません");
                messageDsp.DspMsg("M2036");
                textBoxMaFAX.Focus();
                return false;
            }

            // 電話番号の半角数値チェック
            if (!String.IsNullOrEmpty(textBoxMaPhone.Text.Trim()))
            {
                if (!dataInputFormCheck.CheckNumericWithHyphen(textBoxMaPhone.Text.Trim()))
                {
                    //MessageBox.Show("電話番号は半角数値です");
                    messageDsp.DspMsg("M2037");
                    textBoxMaPhone.Focus();
                    return false;
                }
                // 電話番号の文字数チェック
                if (textBoxMaPhone.TextLength > 13)
                {
                    //MessageBox.Show("電話番号は13文字以下です");
                    messageDsp.DspMsg("M2012");
                    textBoxMaPhone.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("電話番号が入力されていません");
                messageDsp.DspMsg("M2038");
                textBoxMaPhone.Focus();
                return false;
            }

            // 管理フラグの適否
            if (checkBoxMaFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("管理フラグが不確定の状態です");
                messageDsp.DspMsg("M2008");
                checkBoxMaFlag.Focus();
                return false;
            }

            // 非表示理由の適否
            if (checkBoxMaFlag.Checked == true)
            {
                if (textBoxMaHidden.Text == "")
                {
                    //MessageBox.Show("非表示理由の入力が必要です");
                    messageDsp.DspMsg("M2009");
                    textBoxMaHidden.Focus();
                    return false;
                }
            }
            return true;
        }
        ///////////////////////////////
        //　8.2.2.2 メーカ情報作成
        //メソッド名：GenerateDataAtUpdate()
        //引　数   ：なし
        //戻り値   ：メーカ更新情報
        //機　能   ：更新データのセット
        ///////////////////////////////
        private M_Maker GenerateDataAtUpdate()
        {
            return new M_Maker
            {
                MaID = int.Parse(textBoxMaID.Text.Trim()),
                MaName = textBoxMaName.Text.Trim(),
                MaFlag = MaFlg,
                MaHidden = textBoxMaHidden.Text.Trim(),
                MaPostal = textBoxMaPostal.Text.Trim(),
                MaAdress = textBoxMaAddress.Text.Trim(),
                MaFAX = textBoxMaFAX.Text.Trim(),
                MaPhone = textBoxMaPhone.Text.Trim(),
            };
        }
        ///////////////////////////////
        //　8.2.2.3 メーカ情報更新
        //メソッド名：UpdateMaker()
        //引　数   ：メーカ情報
        //戻り値   ：なし
        //機　能   ：メーカ情報の更新
        ///////////////////////////////
        private void UpdateMaker(M_Maker updMaker)
        {
            if (MaFlg == 0)
            {
                // 更新確認メッセージ
                DialogResult result = messageDsp.DspMsg("M2019");
                if (result == DialogResult.Cancel)
                    return;

                // メーカ情報の更新
                bool flg = makerDataAccess.UpdateMakerData(updMaker);
                if (flg == true)
                    //MessageBox.Show("データを更新しました。");
                    messageDsp.DspMsg("M2020");
                else
                    //MessageBox.Show("データの更新に失敗しました。");
                    messageDsp.DspMsg("M2021");

                textBoxMaID.Focus();

                // 入力エリアのクリア
                ClearInput();

                // データグリッドビューの表示
                GetDataGridView();
            }

            else if (MaFlg == 2)
            {
                DeleteMaker(updMaker);
            }
        }
        private void DeleteMaker(M_Maker delMaker)
        {

            // 更新確認メッセージ
            DialogResult result = messageDsp.DspMsg("M2023");
            if (result == DialogResult.Cancel)
                return;

            // メーカ情報の更新
            bool flg = makerDataAccess.DeleteMakerData(delMaker);
            if (flg == true)
                //MessageBox.Show("データを削除しました。");
                messageDsp.DspMsg("M2024");
            else
                //MessageBox.Show("データの削除に失敗しました。");
                messageDsp.DspMsg("M2025");

            textBoxMaID.Focus();

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

        private void dataGridViewMaker_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewMaker.CurrentRow != null)
            {
                //クリックされた行データをテキストボックスへ
                textBoxMaID.Text = dataGridViewMaker.Rows[dataGridViewMaker.CurrentRow.Index].Cells[0].Value.ToString();
                textBoxMaName.Text = dataGridViewMaker.Rows[dataGridViewMaker.CurrentRow.Index].Cells[1].Value.ToString();
                textBoxMaAddress.Text = dataGridViewMaker.Rows[dataGridViewMaker.CurrentRow.Index].Cells[2].Value.ToString();
                textBoxMaPhone.Text = dataGridViewMaker.Rows[dataGridViewMaker.CurrentRow.Index].Cells[3].Value.ToString();
                textBoxMaPostal.Text = dataGridViewMaker.Rows[dataGridViewMaker.CurrentRow.Index].Cells[4].Value.ToString();
                textBoxMaFAX.Text = dataGridViewMaker.Rows[dataGridViewMaker.CurrentRow.Index].Cells[5].Value.ToString();

                int MaFlg2 = int.Parse(dataGridViewMaker.Rows[dataGridViewMaker.CurrentRow.Index].Cells[6].Value.ToString());
                if (MaFlg2 == 0)
                {
                    checkBoxMaFlag.Checked = false;
                }
                else if (MaFlg2 == 2)
                {
                    checkBoxMaFlag.Checked = true;
                }
                textBoxMaHidden.Text = dataGridViewMaker.Rows[dataGridViewMaker.CurrentRow.Index].Cells[7].Value.ToString();
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBoxMaFlag_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxMaFlag.Checked)
            {
                if (buttonUpdate.Enabled == false)
                {
                    BackColor = Color.Tomato;
                    buttonUpdate.Text = "削除";
                }
                else
                {
                    BackColor = Color.Tomato;
                    buttonUpdate.BackgroundImage = Properties.Resources.Fixed_削除;
                    buttonUpdate.Text = "削除";
                }
                MaFlg = 2;
                textBoxMaHidden.Enabled = true;
                return;
            }
            else
            {
                if (buttonUpdate.Enabled == false)
                {
                    BackColor = Color.Gold;
                    buttonUpdate.Text = "更新";
                }
                else
                {
                    BackColor = Color.Gold;
                    buttonUpdate.BackgroundImage = Properties.Resources.Fixed_更新;
                    buttonUpdate.Text = "更新";
                }
                MaFlg = 0;
                textBoxMaHidden.Enabled = false;
                textBoxMaHidden.Text = "";
                return;
            }
        }

        private void F_Maker_Activated(object sender, EventArgs e)
        {
            labelEmpName.Text = F_menu.loginName;
            labelEmpID.Text = F_menu.loginEmID;
            labelOfficeName.Text = F_menu.loginSalesOffice;
            if (F_menu.loginSalesOffice != "本社")
            {
                buttonRegist.Enabled = false;
                buttonUpdate.Enabled = false;
                buttonRegist.BackgroundImage = Properties.Resources.Fixed_キャンセル使用不可;
                buttonUpdate.BackgroundImage = Properties.Resources.Fixed_キャンセル使用不可;
            }
            if (F_Login.SysMode == 1) //開発者モード
            {
                buttonRegist.Enabled = true;
                buttonUpdate.Enabled = true;
                buttonRegist.BackgroundImage = Properties.Resources.Fixed_登録;
                buttonUpdate.BackgroundImage = Properties.Resources.Fixed_更新;
            }
        }
    }
}
