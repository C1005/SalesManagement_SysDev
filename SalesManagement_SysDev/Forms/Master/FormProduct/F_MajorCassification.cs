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
    public partial class F_MajorCassification : Form
    {
        //メッセージ表示用クラスのインスタンス化
        MessageDsp messageDsp = new MessageDsp();
        //データベース大分類テーブルアクセス用クラスのインスタンス化
        DbAccess.MajorCassificationDataAccess majorCassificationDataAccess = new DbAccess.MajorCassificationDataAccess();
        //入力形式チェック用クラスのインスタンス化
        DataInputFormCheck dataInputFormCheck = new DataInputFormCheck();
        //データグリッドビュー用の大分類データ
        private static List<M_MajorCassification> MajorCassification;
        //管理フラグを数値型で入れるための変数
        int McFlg;

        public F_MajorCassification()
        {
            InitializeComponent();
        }

        private void F_MajorCassification_Load(object sender, EventArgs e)
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
            dataGridViewMajorClassification.ReadOnly = true;
            //行内をクリックすることで行を選択
            dataGridViewMajorClassification.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //ヘッダー位置の指定
            dataGridViewMajorClassification.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

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

            // 大分類データの取得
            MajorCassification = majorCassificationDataAccess.GetMajorClassificationData();

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
            dataGridViewMajorClassification.DataSource = MajorCassification.Skip(pageSize * pageNo).Take(pageSize).ToList();
            //各列幅の指定
            dataGridViewMajorClassification.Columns[0].Width = 100;
            dataGridViewMajorClassification.Columns[1].Width = 200;
            dataGridViewMajorClassification.Columns[2].Width = 120;
            dataGridViewMajorClassification.Columns[3].Width = 400;

            //各列の文字位置の指定
            dataGridViewMajorClassification.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewMajorClassification.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewMajorClassification.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewMajorClassification.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            //dataGridViewの総ページ数
            labelPage.Text = "/" + ((int)Math.Ceiling(MajorCassification.Count / (double)pageSize)) + "ページ";

            dataGridViewMajorClassification.Refresh();
        }

        private void buttonPageSizeChange_Click(object sender, EventArgs e)
        {
            SetDataGridView();
        }

        private void buttonFirstPage_Click(object sender, EventArgs e)
        {
            int pageSize = int.Parse(textBoxPageSize.Text);
            dataGridViewMajorClassification.DataSource = MajorCassification.Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewMajorClassification.Refresh();
            //ページ番号の設定
            textBoxPageNo.Text = "1";
        }

        private void buttonPreviousPage_Click(object sender, EventArgs e)
        {
            int pageSize = int.Parse(textBoxPageSize.Text);
            int pageNo = int.Parse(textBoxPageNo.Text) - 2;
            dataGridViewMajorClassification.DataSource = MajorCassification.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewMajorClassification.Refresh();
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
            int lastNo = (int)Math.Ceiling(MajorCassification.Count / (double)pageSize) - 1;
            //最終ページでなければ
            if (pageNo <= lastNo)
                dataGridViewMajorClassification.DataSource = MajorCassification.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewMajorClassification.Refresh();
            //ページ番号の設定
            int lastPage = (int)Math.Ceiling(MajorCassification.Count / (double)pageSize);
            if (pageNo >= lastPage)
                textBoxPageNo.Text = lastPage.ToString();
            else
                textBoxPageNo.Text = (pageNo + 1).ToString();
        }

        private void buttonLastPage_Click(object sender, EventArgs e)
        {
            int pageSize = int.Parse(textBoxPageSize.Text);
            //最終ページの計算
            int pageNo = (int)Math.Ceiling(MajorCassification.Count / (double)pageSize) - 1;
            dataGridViewMajorClassification.DataSource = MajorCassification.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewMajorClassification.Refresh();
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
            textBoxMcID.Text = "";
            textBoxMcName.Text = "";
            textBoxMcHidden.Text = "";
            checkBoxMcFlag.Checked = false;
        }

        private void buttonRegist_Click(object sender, EventArgs e)
        {
            // 8.2.1.1 妥当な大分類データ取得
            if (!GetValidDataAtRegistration())
                return;

            // 8.2.1.2 大分類情報作成
            var regMajorClassification = GenerateDataAtRegistration();

            // 8.2.1.3 大分類情報登録
            RegistrationMajorCassification(regMajorClassification);
        }

        ///////////////////////////////
        //　8.2.1.1 妥当な大分類データ取得
        //メソッド名：GetValidDataAtRegistration()
        //引　数   ：なし
        //戻り値   ：true or false
        //機　能   ：入力データの形式チェック
        //          ：エラーがない場合True
        //          ：エラーがある場合False
        ///////////////////////////////
        private bool GetValidDataAtRegistration()
        {

            // 大分類IDの適否
            if (!String.IsNullOrEmpty(textBoxMcID.Text.Trim()))
            {
                // 大分類IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxMcID.Text.Trim()))
                {
                    //MessageBox.Show("大分類IDは全て半角英数字入力です");
                    messageDsp.DspMsg("M1001");
                    textBoxMcID.Focus();
                    return false;
                }
                // 大分類IDの文字数チェック
                if (textBoxMcID.TextLength > 2)
                {
                    //MessageBox.Show("大分類IDは2文字です");
                    messageDsp.DspMsg("M1002");
                    textBoxMcID.Focus();
                    return false;
                }
                // 大分類IDの重複チェック
                if (majorCassificationDataAccess.CheckMajorClassificationCDExistence(textBoxMcID.Text.Trim()))
                {
                    //MessageBox.Show("入力された大分類IDは既に存在します");
                    messageDsp.DspMsg("M1003");
                    textBoxMcID.Focus();
                    return false;
                }
                // 大分類IDが0ではないかチェック
                if (int.Parse(textBoxMcID.Text.Trim()) == 0)
                {
                    //MessageBox.Show("大分類IDは01から割り当ててください");
                    messageDsp.DspMsg("M1024");
                    textBoxMcID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("大分類IDが入力されていません");
                messageDsp.DspMsg("M1004");
                textBoxMcID.Focus();
                return false;
            }

            // 大分類名の適否
            if (!String.IsNullOrEmpty(textBoxMcName.Text.Trim()))
            {
                // 大分類名の全角チェック
                if (!dataInputFormCheck.CheckFullWidth(textBoxMcName.Text.Trim()))
                {
                    //MessageBox.Show("大分類名は全て全角入力です");
                    messageDsp.DspMsg("M1005");
                    textBoxMcName.Focus();
                    return false;
                }
                // 大分類名の文字数チェック
                if (textBoxMcName.TextLength > 50)
                {
                    //MessageBox.Show("大分類名は50文字以下です");
                    messageDsp.DspMsg("M1006");
                    textBoxMcName.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("大分類名が入力されていません");
                messageDsp.DspMsg("M1007");
                textBoxMcName.Focus();
                return false;
            }

            // 管理フラグの適否
            if (checkBoxMcFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("管理フラグが不確定の状態です");
                messageDsp.DspMsg("M1008");
                checkBoxMcFlag.Focus();
                return false;
            }

            // 管理フラグの適否２
            if (checkBoxMcFlag.Checked == true)
            {
                //MessageBox.Show("登録では管理フラグは適用されません");
                messageDsp.DspMsg("M1022");
                checkBoxMcFlag.Focus();
                return false;
            }

            return true;
        }

        ///////////////////////////////
        //　8.2.1.2 大分類情報作成
        //メソッド名：GenerateDataAtRegistration()
        //引　数   ：なし
        //戻り値   ：大分類登録情報
        //機　能   ：登録データのセット
        ///////////////////////////////
        private M_MajorCassification GenerateDataAtRegistration()
        {
            return new M_MajorCassification
            {
                McID = int.Parse(textBoxMcID.Text.Trim()),
                McName = textBoxMcName.Text.Trim(),
                McFlag = McFlg,
                McHidden = textBoxMcHidden.Text.Trim(),
            };
        }
        ///////////////////////////////
        //　8.2.1.3 大分類情報登録
        //メソッド名：RegistrationPosition()
        //引　数   ：大分類情報
        //戻り値   ：なし
        //機　能   ：大分類データの登録
        ///////////////////////////////
        private void RegistrationMajorCassification(M_MajorCassification regMajorCassification)
        {
            // 登録確認メッセージ
            DialogResult result = messageDsp.DspMsg("M1010");
            if (result == DialogResult.Cancel)
                return;

            // 大分類情報の登録
            bool flg = majorCassificationDataAccess.AddMajorCassificationData(regMajorCassification);
            if (flg == true)
                //MessageBox.Show("データを登録しました。");
                messageDsp.DspMsg("M1011");
            else
                //MessageBox.Show("データの登録に失敗しました。");
                messageDsp.DspMsg("M1012");

            textBoxMcID.Focus();

            // 入力エリアのクリア
            ClearInput();

            // データグリッドビューの表示
            GetDataGridView();

        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            // 8.2.2.1 妥当な小分類データ取得
            if (!GetValidDataAtUpdate())
                return;

            // 8.2.2.2 小分類情報作成
            var updMajorCassification = GenerateDataAtUpdate();

            // 8.2.2.3 小分類情報更新
            UpdateMajorCassification(updMajorCassification);
        }

        ///////////////////////////////
        //　8.2.1.1 妥当な大分類データ取得
        //メソッド名：GetValidDataAtRegistration()
        //引　数   ：なし
        //戻り値   ：true or false
        //機　能   ：入力データの形式チェック
        //          ：エラーがない場合True
        //          ：エラーがある場合False
        ///////////////////////////////
        private bool GetValidDataAtUpdate()
        {
            // 大分類IDの適否
            if (!String.IsNullOrEmpty(textBoxMcID.Text.Trim()))
            {
                // 大分類IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxMcID.Text.Trim()))
                {
                    //MessageBox.Show("大分類IDは全て半角英数字入力です");
                    messageDsp.DspMsg("M1001");
                    textBoxMcID.Focus();
                    return false;
                }
                // 大分類IDの文字数チェック
                if (textBoxMcID.TextLength > 2)
                {
                    //MessageBox.Show("大分類IDは2文字です");
                    messageDsp.DspMsg("M1002");
                    textBoxMcID.Focus();
                    return false;
                }
                // 大分類IDが0ではないかチェック
                if (int.Parse(textBoxMcID.Text.Trim()) == 0)
                {
                    //MessageBox.Show("大分類IDは01から割り当ててください");
                    messageDsp.DspMsg("M1024");
                    textBoxMcID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("大分類IDが入力されていません");
                messageDsp.DspMsg("M1004");
                textBoxMcID.Focus();
                return false;
            }

            // 大分類名の適否
            if (!String.IsNullOrEmpty(textBoxMcName.Text.Trim()))
            {
                // 大分類名の全角チェック
                if (!dataInputFormCheck.CheckFullWidth(textBoxMcName.Text.Trim()))
                {
                    //MessageBox.Show("大分類名は全て全角入力です");
                    messageDsp.DspMsg("M1005");
                    textBoxMcName.Focus();
                    return false;
                }
                // 大分類名の文字数チェック
                if (textBoxMcName.TextLength > 50)
                {
                    //MessageBox.Show("大分類名は50文字以下です");
                    messageDsp.DspMsg("M1006");
                    textBoxMcName.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("大分類名が入力されていません");
                messageDsp.DspMsg("M1007");
                textBoxMcName.Focus();
                return false;
            }

            // 管理フラグの適否
            if (checkBoxMcFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("管理フラグが不確定の状態です");
                messageDsp.DspMsg("M1008");
                checkBoxMcFlag.Focus();
                return false;
            }

            return true;
        }

        ///////////////////////////////
        //　8.2.2.2 大分類情報作成
        //メソッド名：GenerateDataAtUpdate()
        //引　数   ：なし
        //戻り値   ：大分類更新情報
        //機　能   ：更新データのセット
        ///////////////////////////////
        private M_MajorCassification GenerateDataAtUpdate()
        {
            return new M_MajorCassification
            {
                McID = int.Parse(textBoxMcID.Text.Trim()),
                McName = textBoxMcName.Text.Trim(),
                McFlag = McFlg,
                McHidden = textBoxMcHidden.Text.Trim(),
            };
        }

        ///////////////////////////////
        //　8.2.2.3 小分類情報更新
        //メソッド名：UpdateSmallClassification()
        //引　数   ：小分類情報
        //戻り値   ：なし
        //機　能   ：小分類情報の更新
        ///////////////////////////////
        private void UpdateMajorCassification(M_MajorCassification updMajorCassification)
        {
            if (McFlg == 0)
            {
                // 更新確認メッセージ
                DialogResult result = messageDsp.DspMsg("M8015");
                if (result == DialogResult.Cancel)
                    return;

                // 小分類情報の更新
                bool flg = majorCassificationDataAccess.UpdateMajorCassificationData(updMajorCassification);
                if (flg == true)
                    //MessageBox.Show("データを更新しました。");
                    messageDsp.DspMsg("M8016");
                else
                    //MessageBox.Show("データの更新に失敗しました。");
                    messageDsp.DspMsg("M8017");

                textBoxMcID.Focus();

                // 入力エリアのクリア
                ClearInput();

                // データグリッドビューの表示
                GetDataGridView();
            }

            else if (McFlg == 2)
            {
                DeleteSmallClassification(updMajorCassification);
            }
        }
        private void DeleteSmallClassification(M_MajorCassification delMajorCassification)
        {

            // 更新確認メッセージ
            DialogResult result = messageDsp.DspMsg("M8019");
            if (result == DialogResult.Cancel)
                return;

            // 小分類情報の更新
            bool flg = majorCassificationDataAccess.DeleteMajorCassificationData(delMajorCassification);
            if (flg == true)
                //MessageBox.Show("データを削除しました。");
                messageDsp.DspMsg("M8020");
            else
                //MessageBox.Show("データの削除に失敗しました。");
                messageDsp.DspMsg("M8021");

            textBoxMcID.Focus();

            // 入力エリアのクリア
            ClearInput();

            // データグリッドビューの表示
            GetDataGridView();
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

        private bool GetValidDataAtSelect()
        {
            // 大分類IDの適否
            if (!String.IsNullOrEmpty(textBoxMcID.Text.Trim()))
            {
                // 大分類IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxMcID.Text.Trim()))
                {
                    //MessageBox.Show("大分類IDは全て半角英数字入力です");
                    messageDsp.DspMsg("M1001");
                    textBoxMcID.Focus();
                    return false;
                }
                // 大分類IDの文字数チェック
                if (textBoxMcID.TextLength > 2)
                {
                    //MessageBox.Show("大分類IDは2文字です");
                    messageDsp.DspMsg("M1002");
                    textBoxMcID.Focus();
                    return false;
                }
                // 大分類IDが0ではないかチェック
                if (int.Parse(textBoxMcID.Text.Trim()) == 0)
                {
                    //MessageBox.Show("大分類IDは01から割り当ててください");
                    messageDsp.DspMsg("M1024");
                    textBoxMcID.Focus();
                    return false;
                }
            }

            // 大分類名の適否
            if (!String.IsNullOrEmpty(textBoxMcName.Text.Trim()))
            {
                // 大分類名の全角チェック
                if (!dataInputFormCheck.CheckFullWidth(textBoxMcName.Text.Trim()))
                {
                    //MessageBox.Show("大分類名は全て全角入力です");
                    messageDsp.DspMsg("M1005");
                    textBoxMcName.Focus();
                    return false;
                }
                // 大分類名の文字数チェック
                if (textBoxMcName.TextLength > 50)
                {
                    //MessageBox.Show("大分類名は50文字以下です");
                    messageDsp.DspMsg("M1006");
                    textBoxMcName.Focus();
                    return false;
                }
            }

            // 管理フラグの適否
            if (checkBoxMcFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("管理フラグが不確定の状態です");
                messageDsp.DspMsg("M1008");
                checkBoxMcFlag.Focus();
                return false;
            }
            return true;
        }

        ///////////////////////////////
        //　8.2.4.2 大分類情報抽出
        //メソッド名：GenerateDataAtSelect()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：大分類情報の取得
        ///////////////////////////////
        public static string mMcID; //cSoIDを別クラス(SalesOfficeDataAccess)でも使用できるように定義
        public static string mMcName;
        public static bool mMcFlg;
        private void GenerateDataAtSelect()
        {
            M_MajorCassification selectCondition;

            mMcID = textBoxMcID.Text.Trim(); //IDはnullではなく、""で空白が入るのでstringで代入
            mMcName = textBoxMcName.Text.Trim();
            mMcFlg = checkBoxMcFlag.Checked;

            if (mMcID != "") //IDで検索
            {
                // 検索条件のセット
                selectCondition = new M_MajorCassification()
                {
                    McID = int.Parse(textBoxMcID.Text.Trim()),
                    McFlag = McFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // 営業所データの抽出
                MajorCassification = majorCassificationDataAccess.GetMajorClassificationData(selectCondition);
                return;
            }
            else if (mMcName != "") // 名前で検索
            {
                // 検索条件のセット
                selectCondition = new M_MajorCassification()
                {
                    McName = textBoxMcName.Text.Trim(),
                    McFlag = McFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // 営業所データの抽出
                MajorCassification = majorCassificationDataAccess.GetMajorClassificationData(selectCondition);
                return;
            }
            else if (mMcFlg == true) // ただの非表示一覧表示
            {
                // 検索条件のセット
                selectCondition = new M_MajorCassification()
                {
                    McFlag = McFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // 営業所データの抽出
                MajorCassification = majorCassificationDataAccess.GetMajorClassificationData(selectCondition);
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

            dataGridViewMajorClassification.DataSource = MajorCassification;

            labelPage.Text = "/" + ((int)Math.Ceiling(MajorCassification.Count / (double)pageSize)) + "ページ";
            dataGridViewMajorClassification.Refresh();

            if (MajorCassification.Count == 0) //検索結果のデータ数が0ならエラー
            {
                messageDsp.DspMsg("M1025");
                SetFormDataGridView();
            }
        }

        private void buttonList_Click(object sender, EventArgs e)
        {
            // データグリッドビューの表示
            SetFormDataGridView();
        }

        private void checkBoxMcFlag_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxMcFlag.Checked)
            {
                McFlg = 2;
                textBoxMcHidden.Enabled = true;
                return;
            }
            else
            {
                McFlg = 0;
                textBoxMcHidden.Enabled = false;
                textBoxMcHidden.Text = "";
                return;
            }
        }

        private void dataGridViewMajorClassification_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //クリックされた行データをテキストボックスへ
            textBoxMcID.Text = dataGridViewMajorClassification.Rows[dataGridViewMajorClassification.CurrentRow.Index].Cells[0].Value.ToString();
            textBoxMcName.Text = dataGridViewMajorClassification.Rows[dataGridViewMajorClassification.CurrentRow.Index].Cells[1].Value.ToString();
            int ScFlg2 = int.Parse(dataGridViewMajorClassification.Rows[dataGridViewMajorClassification.CurrentRow.Index].Cells[2].Value.ToString());
            if (ScFlg2 == 0)
            {
                checkBoxMcFlag.Checked = false;
            }
            else if (ScFlg2 == 2)
            {
                checkBoxMcFlag.Checked = true;
            }
            textBoxMcHidden.Text = dataGridViewMajorClassification.Rows[dataGridViewMajorClassification.CurrentRow.Index].Cells[3].Value.ToString();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {

        }
    }
}
