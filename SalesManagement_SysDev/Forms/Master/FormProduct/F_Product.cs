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
    public partial class F_Product : Form
    {
        //メッセージ表示用クラスのインスタンス化
        MessageDsp messageDsp = new MessageDsp();
        //データベース大分類テーブルアクセス用クラスのインスタンス化
        DbAccess.ProductDataAccess productDataAccess = new DbAccess.ProductDataAccess();
        //入力形式チェック用クラスのインスタンス化
        DataInputFormCheck dataInputFormCheck = new DataInputFormCheck();
        //データグリッドビュー用の大分類データ
        private static List<M_Product> Product;
        //管理フラグを数値型で入れるための変数
        int PrFlg;

        public F_Product()
        {
            InitializeComponent();
        }

        private void F_Product_Load(object sender, EventArgs e)
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
            dataGridViewProduct.ReadOnly = true;
            //行内をクリックすることで行を選択
            dataGridViewProduct.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //ヘッダー位置の指定
            dataGridViewProduct.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

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
            // 商品データの取得
            Product = productDataAccess.GetProductData();

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
            dataGridViewProduct.DataSource = Product.Skip(pageSize * pageNo).Take(pageSize).ToList();

            if (Product.Count == 0)
            {
                labelNoTable.Visible = true;
            }
            else
            {
                labelNoTable.Visible = false;
            }

            //各列幅の指定
            dataGridViewProduct.Columns[0].Width = 100;
            dataGridViewProduct.Columns[1].Width = 100;
            dataGridViewProduct.Columns[2].Width = 130;
            dataGridViewProduct.Columns[3].Width = 80;
            dataGridViewProduct.Columns[4].Width = 90;
            dataGridViewProduct.Columns[5].Width = 100;
            dataGridViewProduct.Columns[6].Width = 140;
            dataGridViewProduct.Columns[7].Width = 90;
            dataGridViewProduct.Columns[8].Width = 110;
            dataGridViewProduct.Columns[9].Width = 110;
            dataGridViewProduct.Columns[10].AutoSizeMode = (DataGridViewAutoSizeColumnMode)DataGridViewAutoSizeColumnsMode.Fill;


            //各列の文字位置の指定
            dataGridViewProduct.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewProduct.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewProduct.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewProduct.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewProduct.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewProduct.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewProduct.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewProduct.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewProduct.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewProduct.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewProduct.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;


            //dataGridViewの総ページ数
            labelPage.Text = "/" + ((int)Math.Ceiling(Product.Count / (double)pageSize)) + "ページ";

            dataGridViewProduct.Refresh();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
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
            // 商品ID入力時チェック
            if (!String.IsNullOrEmpty(textBoxPrID.Text.Trim()))
            {
                // 商品IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxPrID.Text.Trim()))
                {
                    //MessageBox.Show("商品IDは全て半角数字入力です");
                    messageDsp.DspMsg("M6001");
                    textBoxPrID.Focus();
                    return false;
                }
                // 商品IDの文字数チェック
                if (textBoxPrID.TextLength > 6)
                {
                    //MessageBox.Show("商品IDは6文字までです");
                    messageDsp.DspMsg("M6002");
                    textBoxPrID.Focus();
                    return false;
                }
            }

            // メーカID入力時チェック
            if (!String.IsNullOrEmpty(textBoxMaID.Text.Trim()))
            {
                // メーカIDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxMaID.Text.Trim()))
                {
                    //MessageBox.Show("メーカIDは全て半角数字入力です");
                    messageDsp.DspMsg("M6028");
                    textBoxMaID.Focus();
                    return false;
                }
                // メーカIDの文字数チェック
                if (textBoxMaID.TextLength > 4)
                {
                    //MessageBox.Show("メーカIDは4文字までです");
                    messageDsp.DspMsg("M6013");
                    textBoxMaID.Focus();
                    return false;
                }
            }

            // 小分類ID入力時チェック
            if (!String.IsNullOrEmpty(textBoxScID.Text.Trim()))
            {
                // 小分類IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxScID.Text.Trim()))
                {
                    //MessageBox.Show("小分類IDは全て半角数字入力です");
                    messageDsp.DspMsg("M6029");
                    textBoxScID.Focus();
                    return false;
                }
                // 小分類IDの文字数チェック
                if (textBoxScID.TextLength > 2)
                {
                    //MessageBox.Show("小分類IDは2文字までです");
                    messageDsp.DspMsg("M6012");
                    textBoxScID.Focus();
                    return false;
                }
            }

            // 商品名入力時チェック
            if (!String.IsNullOrEmpty(textBoxPrName.Text.Trim()))
            {
                // 商品名の全角チェック
                if (!dataInputFormCheck.CheckFullWidth(textBoxPrName.Text.Trim()))
                {
                    //MessageBox.Show("商品名は全て全角入力です");
                    messageDsp.DspMsg("M6005");
                    textBoxPrName.Focus();
                    return false;
                }
                // 商品名の文字数チェック
                if (textBoxPrName.TextLength > 50)
                {
                    //MessageBox.Show("商品名は50文字以下です");
                    messageDsp.DspMsg("M6009");
                    textBoxPrName.Focus();
                    return false;
                }
            }

            //色の検索不可チェック
            if (textBoxPrColor.Text != "")
            {
                //MessageBox.Show("色は検索対象外です");
                messageDsp.DspMsg("M6030");
                textBoxPrColor.Focus();
                return false;
            }
            //型番の検索不可チェック
            if (textBoxPrModelNumber.Text != "")
            {
                //MessageBox.Show("型番は検索対象外です");
                messageDsp.DspMsg("M6031");
                textBoxPrModelNumber.Focus();
                return false;
            }
            //発売日の検索不可チェック
            if (dateTimePickerPrReleaseDate.Checked == true)
            {
                //MessageBox.Show("発売日は検索対象外です");
                messageDsp.DspMsg("M6032");
                dateTimePickerPrReleaseDate.Focus();
                return false;
            }
            //安全在庫数の検索不可チェック
            if (textBoxPrSafetyStock.Text != "")
            {
                //MessageBox.Show("安全在庫数は検索対象外です");
                messageDsp.DspMsg("M6033");
                textBoxPrSafetyStock.Focus();
                return false;
            }
            //価格の検索不可チェック
            if (textBoxPrice.Text != "")
            {
                //MessageBox.Show("価格は検索対象外です");
                messageDsp.DspMsg("M6034");
                textBoxPrice.Focus();
                return false;
            }

            // 管理フラグの適否
            if (checkBoxPrFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("管理フラグが不確定の状態です");
                messageDsp.DspMsg("M6007");
                checkBoxPrFlag.Focus();
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

        public static string mPrID; //cSoIDを別クラス(SalesOfficeDataAccess)でも使用できるように定義
        public static string mPrName;
        public static string mScID;
        public static string mMaID;
        public static string mColor;
        public static string mPrModelNumber;
        public static string mPrReleaseDate;
        public static string mtextBoxPrSafetyStock;
        public static string mPrice;
        public static bool mPrFlg;

        private void GenerateDataAtSelect()
        {
            M_Product selectCondition;

            mPrID = textBoxPrID.Text.Trim(); //IDはnullではなく、""で空白が入るのでstringで代入
            mScID = textBoxScID.Text.Trim();
            mMaID = textBoxMaID.Text.Trim();
            mPrName = textBoxPrName.Text.Trim();
            mPrFlg = checkBoxPrFlag.Checked;

            if (mPrID != "") //IDで検索
            {
                // 検索条件のセット
                selectCondition = new M_Product()
                {
                    PrID = int.Parse(textBoxPrID.Text.Trim()),
                    PrFlag = PrFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // 商品データの抽出
                Product = productDataAccess.GetProductData(selectCondition);
                return;
            }
            else if (mMaID != "") //IDで検索
            {
                // 検索条件のセット
                selectCondition = new M_Product()
                {
                    MaID = int.Parse(textBoxMaID.Text.Trim()),
                    PrFlag = PrFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // 営業所データの抽出
                Product = productDataAccess.GetProductData(selectCondition);
                return;
            }
            else if (mPrName != "") // 名前で検索
            {
                // 検索条件のセット
                selectCondition = new M_Product()
                {
                    PrName = textBoxPrName.Text.Trim(),
                    PrFlag = PrFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // 営業所データの抽出
                Product = productDataAccess.GetProductData(selectCondition);
                return;
            }

            else if (mScID != "") //IDで検索
            {
                // 検索条件のセット
                selectCondition = new M_Product()
                {
                    ScID = int.Parse(textBoxScID.Text.Trim()),
                    PrFlag = PrFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // 営業所データの抽出
                Product = productDataAccess.GetProductData(selectCondition);
                return;
            }
            else if (mPrFlg == true) // ただの非表示一覧表示
            {
                // 検索条件のセット
                selectCondition = new M_Product()
                {
                    PrFlag = PrFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // 営業所データの抽出
                Product = productDataAccess.GetProductData(selectCondition);
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

            dataGridViewProduct.DataSource = Product;
            if (Product.Count == 0)
            {
                labelNoTable.Visible = true;
            }
            else
            {
                labelNoTable.Visible = false;
            }

            labelPage.Text = "/" + ((int)Math.Ceiling(Product.Count / (double)pageSize)) + "ページ";
            dataGridViewProduct.Refresh();

            if (Product.Count == 0) //検索結果のデータ数が0ならエラー
            {
                messageDsp.DspMsg("M6035");
                SetFormDataGridView();
            }
        }

        private void buttonRegist_Click(object sender, EventArgs e)
        {
            // 8.2.1.1 妥当な商品データ取得
            if (!GetValidDataAtRegistration())
                return;

            // 8.2.1.2 商品情報作成
            var regProduct = GenerateDataAtRegistration();

            // 8.2.1.3 商品情報登録
            RegistrationProduct(regProduct);
        }

        ///////////////////////////////
        //　8.2.1.1 妥当な商品データ取得
        //メソッド名：GetValidDataAtRegistration()
        //引　数   ：なし
        //戻り値   ：true or false
        //機　能   ：入力データの形式チェック
        //          ：エラーがない場合True
        //          ：エラーがある場合False
        ///////////////////////////////
        private bool GetValidDataAtRegistration()
        {
            // メーカIDの適否
            if (!String.IsNullOrEmpty(textBoxMaID.Text.Trim()))
            {
                // メーカIDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxMaID.Text.Trim()))
                {
                    //MessageBox.Show("メーカIDは全て半角数字入力です");
                    messageDsp.DspMsg("M6028");
                    textBoxMaID.Focus();
                    return false;
                }
                // メーカIDの文字数チェック
                if (textBoxMaID.TextLength > 4)
                {
                    //MessageBox.Show("メーカIDは4文字です");
                    messageDsp.DspMsg("M6013");
                    textBoxMaID.Focus();
                    return false;
                }

                // メーカIDに一致するレコードの存在チェック
                if (labelMaName.Text == "“UnknownID”")
                {
                    //MessageBox.Show("入力されたメーカIDは存在しません");
                    messageDsp.DspMsg("M6036");
                    textBoxMaID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("メーカIDが入力されていません");
                messageDsp.DspMsg("M6037");
                textBoxMaID.Focus();
                return false;
            }

            // 小分類IDの適否
            if (!String.IsNullOrEmpty(textBoxScID.Text.Trim()))
            {
                // 小分類IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxScID.Text.Trim()))
                {
                    //MessageBox.Show("小分類IDは全て半角数字入力です");
                    messageDsp.DspMsg("M6029");
                    textBoxScID.Focus();
                    return false;
                }
                // 小分類IDの文字数チェック
                if (textBoxScID.TextLength > 2)
                {
                    //MessageBox.Show("小分類IDは2文字です");
                    messageDsp.DspMsg("M6012");
                    textBoxScID.Focus();
                    return false;
                }

                // 小分類IDに一致するレコードの存在チェック
                if (labelScName.Text == "“UnknownID”")
                {
                    //MessageBox.Show("入力された小分類IDは存在しません");
                    messageDsp.DspMsg("M6038");
                    textBoxScID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("小分類IDが入力されていません");
                messageDsp.DspMsg("M6039");
                textBoxScID.Focus();
                return false;
            }

            // 商品名の適否
            if (!String.IsNullOrEmpty(textBoxPrName.Text.Trim()))
            {
                // 商品名の全角チェック
                if (!dataInputFormCheck.CheckFullWidth(textBoxPrName.Text.Trim()))
                {
                    //MessageBox.Show("商品名は全て全角入力です");
                    messageDsp.DspMsg("M6005");
                    textBoxPrName.Focus();
                    return false;
                }
                // 商品名の文字数チェック
                if (textBoxPrName.TextLength > 50)
                {
                    //MessageBox.Show("商品名は50文字以下です");
                    messageDsp.DspMsg("M6009");
                    textBoxPrName.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("商品名が入力されていません");
                messageDsp.DspMsg("M6006");
                textBoxPrName.Focus();
                return false;
            }

            // 色の適否
            if (!String.IsNullOrEmpty(textBoxPrColor.Text.Trim()))
            {
                // 色の全角チェック
                if (!dataInputFormCheck.CheckFullWidth(textBoxPrColor.Text.Trim()))
                {
                    //MessageBox.Show("色は全て全角入力です");
                    messageDsp.DspMsg("M6040");
                    textBoxPrColor.Focus();
                    return false;
                }
                // 色の文字数チェック
                if (textBoxPrColor.TextLength > 20)
                {
                    //MessageBox.Show("色は20文字以下です");
                    messageDsp.DspMsg("M6015");
                    textBoxPrColor.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("色が入力されていません");
                messageDsp.DspMsg("M6041");
                textBoxPrColor.Focus();
                return false;
            }

            // 型番の適否
            if (!String.IsNullOrEmpty(textBoxPrModelNumber.Text.Trim()))
            {
                // 型番の全角チェック
                if (!dataInputFormCheck.CheckNumericWithHyphen(textBoxPrModelNumber.Text.Trim()))
                {
                    //MessageBox.Show("型番は全て全角入力です");
                    messageDsp.DspMsg("M6042");
                    textBoxPrModelNumber.Focus();
                    return false;
                }
                // 型番の文字数チェック
                if (textBoxPrModelNumber.TextLength > 20)
                {
                    //MessageBox.Show("型番は20文字以下です");
                    messageDsp.DspMsg("M6014");
                    textBoxPrModelNumber.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("型番が入力されていません");
                messageDsp.DspMsg("M6043");
                textBoxPrColor.Focus();
                return false;
            }

            // 安全在庫数の適否
            if (!String.IsNullOrEmpty(textBoxPrSafetyStock.Text.Trim()))
            {
                // 安全在庫数の半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxScID.Text.Trim()))
                {
                    //MessageBox.Show("安全在庫数は全て半角数字入力です");
                    messageDsp.DspMsg("M6044");
                    textBoxScID.Focus();
                    return false;
                }
                // 安全在庫数の文字数チェック
                if (textBoxPrSafetyStock.TextLength > 4)
                {
                    //MessageBox.Show("安全在庫数は4文字以下です");
                    messageDsp.DspMsg("M6011");
                    textBoxPrSafetyStock.Focus();
                    return false;
                }
                // 安全在庫数の0チェック
                if (textBoxPrSafetyStock.Text == "0")
                {
                    //MessageBox.Show("安全在庫数は01以上です");
                    messageDsp.DspMsg("M6045");
                    textBoxPrSafetyStock.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("安全在庫数が入力されていません");
                messageDsp.DspMsg("M6046");
                textBoxPrSafetyStock.Focus();
                return false;
            }

            // 価格の適否
            if (!String.IsNullOrEmpty(textBoxPrice.Text.Trim()))
            {
                // 価格の半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxScID.Text.Trim()))
                {
                    //MessageBox.Show("価格は全て半角数字入力です");
                    messageDsp.DspMsg("M6047");
                    textBoxScID.Focus();
                    return false;
                }
                // 価格の文字数チェック
                if (textBoxPrice.TextLength > 9)
                {
                    //MessageBox.Show("価格は9文字以下です");
                    messageDsp.DspMsg("M6010");
                    textBoxPrice.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("価格が入力されていません");
                messageDsp.DspMsg("M6048");
                textBoxPrSafetyStock.Focus();
                return false;
            }

            // 管理フラグの適否
            if (checkBoxPrFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("管理フラグが不確定の状態です");
                messageDsp.DspMsg("M6007");
                checkBoxPrFlag.Focus();
                return false;
            }

            // 管理フラグの適否２
            if (checkBoxPrFlag.Checked == true)
            {
                //MessageBox.Show("登録では管理フラグは適用されません");
                messageDsp.DspMsg("M6049");
                checkBoxPrFlag.Focus();
                return false;
            }

            return true;
        }
        ///////////////////////////////
        //　8.2.1.2 商品情報作成
        //メソッド名：GenerateDataAtRegistration()
        //引　数   ：なし
        //戻り値   ：商品登録情報
        //機　能   ：登録データのセット
        ///////////////////////////////
        private M_Product GenerateDataAtRegistration()
        {
            return new M_Product
            {
                MaID = int.Parse(textBoxMaID.Text.Trim()),
                PrName = textBoxPrName.Text.Trim(),
                ScID = int.Parse(textBoxScID.Text.Trim()),
                PrColor = textBoxPrColor.Text.Trim(),
                PrFlag = PrFlg,
                PrHidden = textBoxPrHidden.Text.Trim(),
                PrModelNumber = textBoxPrModelNumber.Text.Trim(),
                PrReleaseDate = dateTimePickerPrReleaseDate.Value,
                PrSafetyStock = int.Parse(textBoxPrSafetyStock.Text.Trim()),
                Price = int.Parse(textBoxPrice.Text.Trim())
            };
        }
        ///////////////////////////////
        //　8.2.1.3 商品情報登録
        //メソッド名：RegistrationMaker()
        //引　数   ：商品情報
        //戻り値   ：なし
        //機　能   ：商品データの登録
        ///////////////////////////////
        private void RegistrationProduct(M_Product regProduct)
        {
            // 登録確認メッセージ
            DialogResult result = messageDsp.DspMsg("M6016");
            if (result == DialogResult.Cancel)
                return;

            // 商品情報の登録
            bool flg = productDataAccess.AddProductData(regProduct);
            if (flg == true)
                //MessageBox.Show("データを登録しました。");
                messageDsp.DspMsg("M6017");
            else
                //MessageBox.Show("データの登録に失敗しました。");
                messageDsp.DspMsg("M6018");

            textBoxPrID.Focus();

            // 入力エリアのクリア
            ClearInput();

            // データグリッドビューの表示
            GetDataGridView();

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
        //　8.2.2.1 妥当な商品データ取得
        //メソッド名：GetValidDataAtUpdate()
        //引　数   ：なし
        //戻り値   ：true or false
        //機　能   ：入力データの形式チェック
        //          ：エラーがない場合True
        //          ：エラーがある場合False
        ///////////////////////////////
        private bool GetValidDataAtUpdate()
        {

            // 商品IDの未入力チェック
            if (!String.IsNullOrEmpty(textBoxPrID.Text.Trim()))
            {
                // 商品IDの半角英数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxPrID.Text.Trim()))
                {
                    //MessageBox.Show("商品IDは全て半角英数字入力です");
                    messageDsp.DspMsg("M6001");
                    textBoxPrID.Focus();
                    return false;
                }
                // 商品IDの文字数チェック
                if (textBoxPrID.TextLength > 6)
                {
                    //MessageBox.Show("商品IDは6文字です");
                    messageDsp.DspMsg("M6002");
                    textBoxPrID.Focus();
                    return false;
                }
                // 商品IDの存在チェック
                if (!productDataAccess.CheckProductCDExistence(textBoxPrID.Text.Trim()))
                {
                    //MessageBox.Show("入力された商品IDは存在しません");
                    messageDsp.DspMsg("M6019");
                    textBoxPrID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("商品IDが入力されていません");
                messageDsp.DspMsg("M6004");
                textBoxPrID.Focus();
                return false;
            }

            // メーカIDの適否
            if (!String.IsNullOrEmpty(textBoxMaID.Text.Trim()))
            {
                // メーカIDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxMaID.Text.Trim()))
                {
                    //MessageBox.Show("メーカIDは全て半角数字入力です");
                    messageDsp.DspMsg("M6028");
                    textBoxMaID.Focus();
                    return false;
                }
                // メーカIDの文字数チェック
                if (textBoxMaID.TextLength > 4)
                {
                    //MessageBox.Show("メーカIDは4文字です");
                    messageDsp.DspMsg("M6013");
                    textBoxMaID.Focus();
                    return false;
                }

                // メーカIDに一致するレコードの存在チェック
                if (labelMaName.Text == "“UnknownID”")
                {
                    //MessageBox.Show("入力されたメーカIDは存在しません");
                    messageDsp.DspMsg("M6036");
                    textBoxMaID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("メーカIDが入力されていません");
                messageDsp.DspMsg("M6037");
                textBoxMaID.Focus();
                return false;
            }

            // 商品名の適否
            if (!String.IsNullOrEmpty(textBoxPrName.Text.Trim()))
            {
                // 商品名の全角チェック
                if (!dataInputFormCheck.CheckFullWidth(textBoxPrName.Text.Trim()))
                {
                    //MessageBox.Show("商品名は全て全角入力です");
                    messageDsp.DspMsg("M6005");
                    textBoxPrName.Focus();
                    return false;
                }
                // 商品名の文字数チェック
                if (textBoxPrName.TextLength > 50)
                {
                    //MessageBox.Show("商品名は50文字以下です");
                    messageDsp.DspMsg("M6009");
                    textBoxPrName.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("商品名が入力されていません");
                messageDsp.DspMsg("M6006");
                textBoxPrName.Focus();
                return false;
            }

            // 小分類IDの適否
            if (!String.IsNullOrEmpty(textBoxScID.Text.Trim()))
            {
                // 小分類IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxScID.Text.Trim()))
                {
                    //MessageBox.Show("小分類IDは全て半角数字入力です");
                    messageDsp.DspMsg("M6029");
                    textBoxScID.Focus();
                    return false;
                }
                // 小分類IDの文字数チェック
                if (textBoxScID.TextLength > 2)
                {
                    //MessageBox.Show("小分類IDは2文字です");
                    messageDsp.DspMsg("M6012");
                    textBoxScID.Focus();
                    return false;
                }

                // 小分類IDに一致するレコードの存在チェック
                if (labelScName.Text == "“UnknownID”")
                {
                    //MessageBox.Show("入力された小分類IDは存在しません");
                    messageDsp.DspMsg("M6038");
                    textBoxScID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("小分類IDが入力されていません");
                messageDsp.DspMsg("M6039");
                textBoxScID.Focus();
                return false;
            }

            // 色の適否
            if (!String.IsNullOrEmpty(textBoxPrColor.Text.Trim()))
            {
                // 色の全角チェック
                if (!dataInputFormCheck.CheckFullWidth(textBoxPrColor.Text.Trim()))
                {
                    //MessageBox.Show("色は全て全角入力です");
                    messageDsp.DspMsg("M6040");
                    textBoxPrColor.Focus();
                    return false;
                }
                // 色の文字数チェック
                if (textBoxPrColor.TextLength > 20)
                {
                    //MessageBox.Show("色は20文字以下です");
                    messageDsp.DspMsg("M6015");
                    textBoxPrColor.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("色が入力されていません");
                messageDsp.DspMsg("M6041");
                textBoxPrColor.Focus();
                return false;
            }

            // 型番の適否
            if (!String.IsNullOrEmpty(textBoxPrModelNumber.Text.Trim()))
            {
                // 型番の全角チェック
                if (!dataInputFormCheck.CheckNumericWithHyphen(textBoxPrModelNumber.Text.Trim()))
                {
                    //MessageBox.Show("型番は全て全角入力です");
                    messageDsp.DspMsg("M6042");
                    textBoxPrModelNumber.Focus();
                    return false;
                }
                // 型番の文字数チェック
                if (textBoxPrModelNumber.TextLength > 20)
                {
                    //MessageBox.Show("型番は20文字以下です");
                    messageDsp.DspMsg("M6014");
                    textBoxPrModelNumber.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("型番が入力されていません");
                messageDsp.DspMsg("M6043");
                textBoxPrColor.Focus();
                return false;
            }

            // 安全在庫数の適否
            if (!String.IsNullOrEmpty(textBoxPrSafetyStock.Text.Trim()))
            {
                // 安全在庫数の半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxScID.Text.Trim()))
                {
                    //MessageBox.Show("安全在庫数は全て半角数字入力です");
                    messageDsp.DspMsg("M6044");
                    textBoxScID.Focus();
                    return false;
                }
                // 安全在庫数の文字数チェック
                if (textBoxPrSafetyStock.TextLength > 4)
                {
                    //MessageBox.Show("安全在庫数は4文字以下です");
                    messageDsp.DspMsg("M6011");
                    textBoxPrSafetyStock.Focus();
                    return false;
                }
                // 安全在庫数の0チェック
                if (textBoxPrSafetyStock.Text == "0")
                {
                    //MessageBox.Show("安全在庫数は01以上です");
                    messageDsp.DspMsg("M6045");
                    textBoxPrSafetyStock.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("安全在庫数が入力されていません");
                messageDsp.DspMsg("M6046");
                textBoxPrSafetyStock.Focus();
                return false;
            }

            // 価格の適否
            if (!String.IsNullOrEmpty(textBoxPrice.Text.Trim()))
            {
                // 価格の半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxScID.Text.Trim()))
                {
                    //MessageBox.Show("価格は全て半角数字入力です");
                    messageDsp.DspMsg("M6047");
                    textBoxScID.Focus();
                    return false;
                }
                // 価格の文字数チェック
                if (textBoxPrice.TextLength > 9)
                {
                    //MessageBox.Show("価格は9文字以下です");
                    messageDsp.DspMsg("M6010");
                    textBoxPrice.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("価格が入力されていません");
                messageDsp.DspMsg("M6048");
                textBoxPrSafetyStock.Focus();
                return false;
            }


            // 管理フラグの適否
            if (checkBoxPrFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("管理フラグが不確定の状態です");
                messageDsp.DspMsg("M6007");
                checkBoxPrFlag.Focus();
                return false;
            }

            // 非表示理由の適否
            if (checkBoxPrFlag.Checked == true)
            {
                if (textBoxPrHidden.Text == "")
                {
                    //MessageBox.Show("非表示理由の入力が必要です");
                    messageDsp.DspMsg("M6008");
                    textBoxPrHidden.Focus();
                    return false;
                }
            }
            return true;
        }
        ///////////////////////////////
        //　8.2.2.2 商品情報作成
        //メソッド名：GenerateDataAtUpdate()
        //引　数   ：なし
        //戻り値   ：商品更新情報
        //機　能   ：更新データのセット
        ///////////////////////////////
        private M_Product GenerateDataAtUpdate()
        {
            return new M_Product
            {
                PrID = int.Parse(textBoxPrID.Text.Trim()),
                MaID = int.Parse(textBoxMaID.Text.Trim()),
                PrName = textBoxPrName.Text.Trim(),
                ScID = int.Parse(textBoxScID.Text.Trim()),
                PrColor = textBoxPrColor.Text.Trim(),
                PrFlag = PrFlg,
                PrHidden = textBoxPrHidden.Text.Trim(),
                PrModelNumber = textBoxPrModelNumber.Text.Trim(),
                PrReleaseDate = dateTimePickerPrReleaseDate.Value,
                PrSafetyStock = int.Parse(textBoxPrSafetyStock.Text.Trim()),
                Price = int.Parse(textBoxPrice.Text.Trim())
            };
        }
        ///////////////////////////////
        //　8.2.2.3 商品情報更新
        //メソッド名：UpdateMaker()
        //引　数   ：商品情報
        //戻り値   ：なし
        //機　能   ：商品情報の更新
        ///////////////////////////////
        private void UpdateMaker(M_Product updProduct)
        {
            if (PrFlg == 0)
            {
                // 更新確認メッセージ
                DialogResult result = messageDsp.DspMsg("M6020");
                if (result == DialogResult.Cancel)
                    return;

                // 商品情報の更新
                bool flg = productDataAccess.UpdateProductData(updProduct);
                if (flg == true)
                    //MessageBox.Show("データを更新しました。");
                    messageDsp.DspMsg("M6021");
                else
                    //MessageBox.Show("データの更新に失敗しました。");
                    messageDsp.DspMsg("M6022");

                textBoxMaID.Focus();

                // 入力エリアのクリア
                ClearInput();

                // データグリッドビューの表示
                GetDataGridView();
            }

            else if (PrFlg == 2)
            {
                DeleteProduct(updProduct);
            }
        }

        private void DeleteProduct(M_Product delProduct)
        {

            // 更新確認メッセージ
            DialogResult result = messageDsp.DspMsg("M6024");
            if (result == DialogResult.Cancel)
                return;

            // メーカ情報の更新
            bool flg = productDataAccess.DeleteProductData(delProduct);
            if (flg == true)
                //MessageBox.Show("データを削除しました。");
                messageDsp.DspMsg("M6025");
            else
                //MessageBox.Show("データの削除に失敗しました。");
                messageDsp.DspMsg("M6026");

            textBoxPrID.Focus();

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

        private void checkBoxPrFlag_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPrFlag.Checked)
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
                PrFlg = 2;
                textBoxPrHidden.Enabled = true;
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
                PrFlg = 0;
                textBoxPrHidden.Enabled = false;
                textBoxPrHidden.Text = "";
                return;
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
            dataGridViewProduct.DataSource = Product.Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewProduct.Refresh();
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
            dataGridViewProduct.DataSource = Product.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewProduct.Refresh();
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
            int lastNo = (int)Math.Ceiling(Product.Count / (double)pageSize) - 1;
            //最終ページでなければ
            if (pageNo <= lastNo)
                dataGridViewProduct.DataSource = Product.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewProduct.Refresh();
            //ページ番号の設定
            int lastPage = (int)Math.Ceiling(Product.Count / (double)pageSize);
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
            int pageNo = (int)Math.Ceiling(Product.Count / (double)pageSize) - 1;
            dataGridViewProduct.DataSource = Product.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewProduct.Refresh();
            //ページ番号の設定
            textBoxPageNo.Text = (pageNo + 1).ToString();
        }

        private void dataGridViewProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewProduct.CurrentRow != null)
            {
                //クリックされた行データをテキストボックスへ
                textBoxPrID.Text = dataGridViewProduct.Rows[dataGridViewProduct.CurrentRow.Index].Cells[0].Value.ToString();
                textBoxMaID.Text = dataGridViewProduct.Rows[dataGridViewProduct.CurrentRow.Index].Cells[1].Value.ToString();
                textBoxPrName.Text = dataGridViewProduct.Rows[dataGridViewProduct.CurrentRow.Index].Cells[2].Value.ToString();
                textBoxPrice.Text = dataGridViewProduct.Rows[dataGridViewProduct.CurrentRow.Index].Cells[3].Value.ToString();
                textBoxPrSafetyStock.Text = dataGridViewProduct.Rows[dataGridViewProduct.CurrentRow.Index].Cells[4].Value.ToString();
                textBoxScID.Text = dataGridViewProduct.Rows[dataGridViewProduct.CurrentRow.Index].Cells[5].Value.ToString();
                textBoxPrModelNumber.Text = dataGridViewProduct.Rows[dataGridViewProduct.CurrentRow.Index].Cells[6].Value.ToString();
                textBoxPrColor.Text = dataGridViewProduct.Rows[dataGridViewProduct.CurrentRow.Index].Cells[7].Value.ToString();
                dateTimePickerPrReleaseDate.Text = dataGridViewProduct.Rows[dataGridViewProduct.CurrentRow.Index].Cells[8].Value.ToString();
                int PrFlg2 = int.Parse(dataGridViewProduct.Rows[dataGridViewProduct.CurrentRow.Index].Cells[9].Value.ToString());
                if (PrFlg2 == 0)
                {
                    checkBoxPrFlag.Checked = false;
                }
                else if (PrFlg2 == 2)
                {
                    checkBoxPrFlag.Checked = true;
                }
                textBoxPrHidden.Text = dataGridViewProduct.Rows[dataGridViewProduct.CurrentRow.Index].Cells[10].Value.ToString();
            }
        }

        private void textBoxMaID_TextChanged(object sender, EventArgs e)
        {
            if (textBoxMaID.Text != "")
            {
                var context = new SalesManagement_DevContext();
                try
                {
                    string mMaName;
                    int mMaID = int.Parse(textBoxMaID.Text);
                    var mMaker = context.M_Makers.Single(x => x.MaID == mMaID);
                    if (mMaker.MaFlag == 2)
                    {
                        mMaName = mMaker.MaName;
                        labelMaName.Text = "(非表示)" + mMaName;
                        labelMaName.Visible = true;
                        context.Dispose();
                    }
                    else
                    {
                        mMaName = mMaker.MaName;
                        labelMaName.Text = mMaName;
                        labelMaName.Visible = true;
                        context.Dispose();
                    }
                }
                catch
                {
                    labelMaName.Text = "“UnknownID”";
                    labelMaName.Visible = true;
                    context.Dispose();
                    return;
                }
            }
            else
            {
                labelMaName.Visible = false;
                labelMaName.Text = "メーカ名";
            }
        }

        private void textBoxScID_TextChanged(object sender, EventArgs e)
        {
            if (textBoxScID.Text != "")
            {
                var context = new SalesManagement_DevContext();
                try
                {
                    string mScName;
                    int mScID = int.Parse(textBoxScID.Text);
                    var mSmallClassification = context.M_SmallClassifications.Single(x => x.ScID == mScID);
                    if (mSmallClassification.ScFlag == 2)
                    {
                        mScName = mSmallClassification.ScName;
                        labelScName.Text = "(非表示)" + mScName;
                        labelScName.Visible = true;
                        context.Dispose();
                    }
                    else
                    {
                        mScName = mSmallClassification.ScName;
                        labelScName.Text = mScName;
                        labelScName.Visible = true;
                        context.Dispose();
                    }
                }
                catch
                {
                    labelScName.Text = "“UnknownID”";
                    labelScName.Visible = true;
                    context.Dispose();
                    return;
                }
            }
            else
            {
                labelScName.Visible = false;
                labelScName.Text = "小分類名";
            }
        }

        private void labelメーカID_Click(object sender, EventArgs e)
        {
            OpenForm(((Label)sender).Text);
        }

        private void label小分類ID_Click(object sender, EventArgs e)
        {
            OpenForm(((Label)sender).Text);
        }

        private void OpenForm(string formName)
        {
            Form frm = new Form();
            //引数より、開くフォームを設定
            switch (formName)
            {
                case "メーカID": //ボタンのテキスト名
                    frm = new F_Maker(); //フォームの名前
                    break;
                case "小分類ID":
                    frm = new F_SmallClassification();
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

        private void ClearInput()
        {
            textBoxPrID.Text = "";
            textBoxMaID.Text = "";
            textBoxPrName.Text = "";
            textBoxScID.Text = "";
            textBoxPrColor.Text = "";
            textBoxPrModelNumber.Text = "";
            textBoxPrSafetyStock.Text = "";
            textBoxPrice.Text = "";
            dateTimePickerPrReleaseDate.Value = DateTime.Now;
            dateTimePickerPrReleaseDate.Checked = false;
            textBoxPrHidden.Text = "";
            checkBoxPrFlag.Checked = false;
        }

        private void labelメーカID_MouseEnter(object sender, EventArgs e)
        {
            labelメーカID.BackColor = Color.Aqua;
        }

        private void labelメーカID_MouseLeave(object sender, EventArgs e)
        {
            labelメーカID.BackColor = Color.Transparent;
        }

        private void label小分類ID_MouseEnter(object sender, EventArgs e)
        {
            label小分類ID.BackColor = Color.Aqua;
        }

        private void label小分類ID_MouseLeave(object sender, EventArgs e)
        {
            label小分類ID.BackColor = Color.Transparent;
        }

        private void F_Product_Activated(object sender, EventArgs e)
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
