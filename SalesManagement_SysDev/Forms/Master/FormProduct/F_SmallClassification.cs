﻿using System;
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
    public partial class F_SmallClassification : Form
    {
        //メッセージ表示用クラスのインスタンス化
        MessageDsp messageDsp = new MessageDsp();
        //データベース小分類テーブルアクセス用クラスのインスタンス化
        DbAccess.SmallClassificationDataAccess smallClassificationDataAccess = new DbAccess.SmallClassificationDataAccess();
        //入力形式チェック用クラスのインスタンス化
        DataInputFormCheck dataInputFormCheck = new DataInputFormCheck();
        //データグリッドビュー用の小分類データ
        private static List<M_SmallClassification> SmallClassification;
        //管理フラグを数値型で入れるための変数
        int ScFlg;

        public F_SmallClassification()
        {
            InitializeComponent();
        }

        private void F_SmallClassification_Load(object sender, EventArgs e)
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
            dataGridViewSmallClassification.ReadOnly = true;
            //行内をクリックすることで行を選択
            dataGridViewSmallClassification.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //ヘッダー位置の指定
            dataGridViewSmallClassification.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

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

            // 小分類データの取得
            SmallClassification = smallClassificationDataAccess.GetSmallClassificationData();

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
            dataGridViewSmallClassification.DataSource = SmallClassification.Skip(pageSize * pageNo).Take(pageSize).ToList();
            //各列幅の指定
            dataGridViewSmallClassification.Columns[0].Width = 100;
            dataGridViewSmallClassification.Columns[1].Width = 200;
            dataGridViewSmallClassification.Columns[2].Width = 120;
            dataGridViewSmallClassification.Columns[3].Width = 400;

            //各列の文字位置の指定
            dataGridViewSmallClassification.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewSmallClassification.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewSmallClassification.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewSmallClassification.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            //dataGridViewの総ページ数
            labelPage.Text = "/" + ((int)Math.Ceiling(SmallClassification.Count / (double)pageSize)) + "ページ";

            dataGridViewSmallClassification.Refresh();
        }

        private void buttonPageSizeChange_Click(object sender, EventArgs e)
        {
            SetDataGridView();
        }

        private void buttonFirstPage_Click(object sender, EventArgs e)
        {
            int pageSize = int.Parse(textBoxPageSize.Text);
            dataGridViewSmallClassification.DataSource = SmallClassification.Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewSmallClassification.Refresh();
            //ページ番号の設定
            textBoxPageNo.Text = "1";
        }

        private void buttonPreviousPage_Click(object sender, EventArgs e)
        {
            int pageSize = int.Parse(textBoxPageSize.Text);
            int pageNo = int.Parse(textBoxPageNo.Text) - 2;
            dataGridViewSmallClassification.DataSource = SmallClassification.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewSmallClassification.Refresh();
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
            int lastNo = (int)Math.Ceiling(SmallClassification.Count / (double)pageSize) - 1;
            //最終ページでなければ
            if (pageNo <= lastNo)
                dataGridViewSmallClassification.DataSource = SmallClassification.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewSmallClassification.Refresh();
            //ページ番号の設定
            int lastPage = (int)Math.Ceiling(SmallClassification.Count / (double)pageSize);
            if (pageNo >= lastPage)
                textBoxPageNo.Text = lastPage.ToString();
            else
                textBoxPageNo.Text = (pageNo + 1).ToString();
        }

        private void buttonLastPage_Click(object sender, EventArgs e)
        {
            int pageSize = int.Parse(textBoxPageSize.Text);
            //最終ページの計算
            int pageNo = (int)Math.Ceiling(SmallClassification.Count / (double)pageSize) - 1;
            dataGridViewSmallClassification.DataSource = SmallClassification.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewSmallClassification.Refresh();
            //ページ番号の設定
            textBoxPageNo.Text = (pageNo + 1).ToString();
        }

        private void dataGridViewSmallClassification_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //クリックされた行データをテキストボックスへ
            textBoxScID.Text = dataGridViewSmallClassification.Rows[dataGridViewSmallClassification.CurrentRow.Index].Cells[0].Value.ToString();
            textBoxMcID.Text = dataGridViewSmallClassification.Rows[dataGridViewSmallClassification.CurrentRow.Index].Cells[1].Value.ToString();
            textBoxScName.Text = dataGridViewSmallClassification.Rows[dataGridViewSmallClassification.CurrentRow.Index].Cells[2].Value.ToString();
            int ScFlg2 = int.Parse(dataGridViewSmallClassification.Rows[dataGridViewSmallClassification.CurrentRow.Index].Cells[3].Value.ToString());
            if (ScFlg2 == 0)
            {
                checkBoxScFlag.Checked = false;
            }
            else if (ScFlg2 == 2)
            {
                checkBoxScFlag.Checked = true;
            }
            textBoxScHidden.Text = dataGridViewSmallClassification.Rows[dataGridViewSmallClassification.CurrentRow.Index].Cells[4].Value.ToString();
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
        ///
        private void ClearInput()
        {
            textBoxScID.Text = "";
            textBoxMcID.Text = "";
            textBoxScName.Text = "";
            textBoxScHidden.Text = "";
            checkBoxScFlag.Checked = false;
        }

        private void buttonRegist_Click(object sender, EventArgs e)
        {
            // 8.2.1.1 妥当な小分類データ取得
            if (!GetValidDataAtRegistration())
                return;

            // 8.2.1.2 小分類情報作成
            var regSmallClassification = GenerateDataAtRegistration();

            // 8.2.1.3 小分類情報登録
            RegistrationSmallClassification(regSmallClassification);
        }

        ///////////////////////////////
        //　8.2.1.1 妥当な小分類データ取得
        //メソッド名：GetValidDataAtRegistration()
        //引　数   ：なし
        //戻り値   ：true or false
        //機　能   ：入力データの形式チェック
        //          ：エラーがない場合True
        //          ：エラーがある場合False
        ///////////////////////////////
        private bool GetValidDataAtRegistration()
        {

            // 小分類IDの適否
            if (!String.IsNullOrEmpty(textBoxScID.Text.Trim()))
            {
                // 小分類IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxScID.Text.Trim()))
                {
                    //MessageBox.Show("小分類IDは全て半角数字入力です");
                    messageDsp.DspMsg("M8001");
                    textBoxScID.Focus();
                    return false;
                }
                // 小分類IDの文字数チェック
                if (textBoxScID.TextLength > 2)
                {
                    //MessageBox.Show("小分類IDは2文字です");
                    messageDsp.DspMsg("M8002");
                    textBoxScID.Focus();
                    return false;
                }
                // 小分類IDの重複チェック
                if (smallClassificationDataAccess.CheckSmallClassificationCDExistence(textBoxScID.Text.Trim()))
                {
                    //MessageBox.Show("入力された小分類IDは既に存在します");
                    messageDsp.DspMsg("M8003");
                    textBoxScID.Focus();
                    return false;
                }
                // 小分類IDが0ではないかチェック
                if (int.Parse(textBoxScID.Text.Trim()) == 0)
                {
                    //MessageBox.Show("小分類IDは01から割り当ててください");
                    messageDsp.DspMsg("M8023");
                    textBoxScID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("小分類IDが入力されていません");
                messageDsp.DspMsg("M8004");
                textBoxScID.Focus();
                return false;
            }

            // 小分類名の適否
            if (!String.IsNullOrEmpty(textBoxScName.Text.Trim()))
            {
                // 小分類名の全角チェック
                if (!dataInputFormCheck.CheckFullWidth(textBoxScName.Text.Trim()))
                {
                    //MessageBox.Show("小分類名は全て全角入力です");
                    messageDsp.DspMsg("M8005");
                    textBoxScName.Focus();
                    return false;
                }
                // 小分類名の文字数チェック
                if (textBoxScName.TextLength > 50)
                {
                    //MessageBox.Show("小分類名は25文字以下です");
                    messageDsp.DspMsg("M8006");
                    textBoxScName.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("小分類名が入力されていません");
                messageDsp.DspMsg("M8007");
                textBoxScName.Focus();
                return false;
            }

            // 管理フラグの適否
            if (checkBoxScFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("管理フラグが不確定の状態です");
                messageDsp.DspMsg("M8008");
                checkBoxScFlag.Focus();
                return false;
            }

            // 管理フラグの適否２
            if (checkBoxScFlag.Checked == true)
            {
                //MessageBox.Show("登録では管理フラグは適用されません");
                messageDsp.DspMsg("M8024");
                checkBoxScFlag.Focus();
                return false;
            }

            return true;
        }
        ///////////////////////////////
        //　8.2.1.2 小分類情報作成
        //メソッド名：GenerateDataAtRegistration()
        //引　数   ：なし
        //戻り値   ：小分類登録情報
        //機　能   ：登録データのセット
        ///////////////////////////////
        private M_SmallClassification GenerateDataAtRegistration()
        {
            return new M_SmallClassification
            {
                ScID = int.Parse(textBoxScID.Text.Trim()),
                ScName = textBoxScName.Text.Trim(),
                ScFlag = ScFlg,
                ScHidden = textBoxScHidden.Text.Trim(),
            };
        }
        ///////////////////////////////
        //　8.2.1.3 小分類情報登録
        //メソッド名：RegistrationSmallClassification()
        //引　数   ：小分類情報
        //戻り値   ：なし
        //機　能   ：小分類データの登録
        ///////////////////////////////
        private void RegistrationSmallClassification(M_SmallClassification regSmallClassification)
        {
            // 登録確認メッセージ
            DialogResult result = messageDsp.DspMsg("M8011");
            if (result == DialogResult.Cancel)
                return;

            // 小分類情報の登録
            bool flg = smallClassificationDataAccess.AddSmallClassificationData(regSmallClassification);
            if (flg == true)
                //MessageBox.Show("データを登録しました。");
                messageDsp.DspMsg("M8012");
            else
                //MessageBox.Show("データの登録に失敗しました。");
                messageDsp.DspMsg("M8013");

            textBoxScID.Focus();

            // 入力エリアのクリア
            ClearInput();

            // データグリッドビューの表示
            GetDataGridView();

        }

        private void checkBoxScFlag_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxScFlag.Checked)
            {
                ScFlg = 2;
                textBoxScHidden.Enabled = true;
                return;
            }
            else
            {
                ScFlg = 0;
                textBoxScHidden.Enabled = false;
                textBoxScHidden.Text = "";
                return;
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            // 8.2.4.1 妥当な小分類データ取得
            if (!GetValidDataAtSelect())
                return;

            // 8.2.4.2 小分類情報抽出
            GenerateDataAtSelect();

            // 8.2.4.3 小分類抽出結果表示
            SetSelectData();
        }

        ///////////////////////////////
        //　8.2.4.1 妥当な小分類データ取得
        //メソッド名：GetValidDataAtSlect()
        //引　数   ：なし
        //戻り値   ：true or false
        //機　能   ：入力データの形式チェック
        //          ：エラーがない場合True
        //          ：エラーがある場合False
        ///////////////////////////////
        private bool GetValidDataAtSelect()
        {

            // 小分類ID入力時チェック
            if (!String.IsNullOrEmpty(textBoxScID.Text.Trim()))
            {
                // 小分類IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxScID.Text.Trim()))
                {
                    //MessageBox.Show("小分類IDは全て半角数字入力です");
                    messageDsp.DspMsg("M8001");
                    textBoxScID.Focus();
                    return false;
                }
                // 小分類IDの文字数チェック
                if (textBoxScID.TextLength > 2)
                {
                    //MessageBox.Show("小分類IDは2文字までです");
                    messageDsp.DspMsg("M8002");
                    textBoxScID.Focus();
                    return false;
                }
            }
            // 小分類名入力時チェック
            if (!String.IsNullOrEmpty(textBoxScName.Text.Trim()))
            {
                // 小分類名の全角チェック
                if (!dataInputFormCheck.CheckFullWidth(textBoxScName.Text.Trim()))
                {
                    //MessageBox.Show("小分類名は全て全角入力です");
                    messageDsp.DspMsg("M8005");
                    textBoxScName.Focus();
                    return false;
                }
                // 小分類名の文字数チェック
                if (textBoxScName.TextLength > 50)
                {
                    //MessageBox.Show("小分類名は50文字以下です");
                    messageDsp.DspMsg("M8006");
                    textBoxScName.Focus();
                    return false;
                }
            }
            if (textBoxScID.Text == "" && textBoxScName.Text == "" && checkBoxScFlag.Checked == false)
            {
                // データグリッドビューの表示
                SetFormDataGridView();
                return false;
            }
            return true;

        }

        ///////////////////////////////
        //　8.2.4.2 小分類情報抽出
        //メソッド名：GenerateDataAtSelect()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：小分類情報の取得
        ///////////////////////////////
        public static string mScID; //cScIDを別クラス(SmallClassificationDataAccess)でも使用できるように定義
        public static string mMcID;
        public static string mScName;
        public static bool mScFlg;

        private void GenerateDataAtSelect()
        {
            M_SmallClassification selectCondition;

            mScID = textBoxScID.Text.Trim();
            mMcID = textBoxMcID.Text.Trim(); //IDはnullではなく、""で空白が入るのでstringで代入
            mScName = textBoxScName.Text.Trim();
            mScFlg = checkBoxScFlag.Checked;

            if (mScID != "") //IDで検索
            {
                // 検索条件のセット
                selectCondition = new M_SmallClassification()
                {
                    ScID = int.Parse(textBoxScID.Text.Trim()),
                    ScFlag = ScFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // 営業所データの抽出
                SmallClassification = smallClassificationDataAccess.GetSmallClassificationData(selectCondition);
                return;
            }
            else if (mMcID != "") // IDで検索
            {
                // 検索条件のセット
                selectCondition = new M_SmallClassification()
                {
                    McID = int.Parse(textBoxMcID.Text.Trim()),
                    ScFlag = ScFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // 営業所データの抽出
                SmallClassification = smallClassificationDataAccess.GetSmallClassificationData(selectCondition);
                return;
            }
            else if (mScName != "") // 名前で検索
            {
                // 検索条件のセット
                selectCondition = new M_SmallClassification()
                {
                    ScName = textBoxScName.Text.Trim(),
                    ScFlag = ScFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // 営業所データの抽出
                SmallClassification = smallClassificationDataAccess.GetSmallClassificationData(selectCondition);
                return;
            }
            else if (mScFlg == true) // ただの非表示一覧表示
            {
                // 検索条件のセット
                selectCondition = new M_SmallClassification()
                {
                    ScFlag = ScFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // 営業所データの抽出
                SmallClassification = smallClassificationDataAccess.GetSmallClassificationData(selectCondition);
                return;
            }
        }
        ///////////////////////////////
        //　8.2.4.3 小分類抽出結果表示
        //メソッド名：SetSelectData()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：小分類情報の表示
        ///////////////////////////////
        private void SetSelectData()
        {
            textBoxPageNo.Text = "1";

            int pageSize = int.Parse(textBoxPageSize.Text);

            dataGridViewSmallClassification.DataSource = SmallClassification;

            labelPage.Text = "/" + ((int)Math.Ceiling(SmallClassification.Count / (double)pageSize)) + "ページ";
            dataGridViewSmallClassification.Refresh();

            if (SmallClassification.Count == 0) //検索結果のデータ数が0ならエラー
            {
                messageDsp.DspMsg("M8025");
                SetFormDataGridView();
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            // 8.2.2.1 妥当な小分類データ取得
            if (!GetValidDataAtUpdate())
                return;

            // 8.2.2.2 小分類情報作成
            var updSmallClassification = GenerateDataAtUpdate();

            // 8.2.2.3 小分類情報更新
            UpdateSmallClassification(updSmallClassification);
        }

        ///////////////////////////////
        //　8.2.2.1 妥当な小分類データ取得
        //メソッド名：GetValidDataAtUpdate()
        //引　数   ：なし
        //戻り値   ：true or false
        //機　能   ：入力データの形式チェック
        //          ：エラーがない場合True
        //          ：エラーがある場合False
        ///////////////////////////////
        private bool GetValidDataAtUpdate()
        {

            // 小分類IDの未入力チェック
            if (!String.IsNullOrEmpty(textBoxScID.Text.Trim()))
            {
                // 小分類IDの半角英数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxScID.Text.Trim()))
                {
                    //MessageBox.Show("小分類IDは全て半角英数字入力です");
                    messageDsp.DspMsg("M8001");
                    textBoxScID.Focus();
                    return false;
                }
                // 小分類IDの文字数チェック
                if (textBoxScID.TextLength > 2)
                {
                    //MessageBox.Show("小分類IDは2文字です");
                    messageDsp.DspMsg("M8002");
                    textBoxScID.Focus();
                    return false;
                }
                // 小分類IDの存在チェック
                if (!smallClassificationDataAccess.CheckSmallClassificationCDExistence(textBoxScID.Text.Trim()))
                {
                    //MessageBox.Show("入力された小分類IDは存在しません");
                    messageDsp.DspMsg("M8014");
                    textBoxScID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("小分類IDが入力されていません");
                messageDsp.DspMsg("M8004");
                textBoxScID.Focus();
                return false;
            }


            // 小分類名の適否
            if (!String.IsNullOrEmpty(textBoxScName.Text.Trim()))
            {
                // 小分類名の全角チェック
                if (!dataInputFormCheck.CheckFullWidth(textBoxScName.Text.Trim()))
                {
                    //MessageBox.Show("小分類名は全て全角入力です");
                    messageDsp.DspMsg("M8005");
                    textBoxScName.Focus();
                    return false;
                }
                // 小分類名の文字数チェック
                if (textBoxScName.TextLength > 50)
                {
                    //MessageBox.Show("小分類名は25文字以下です");
                    messageDsp.DspMsg("M8006");
                    textBoxScName.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("小分類名が入力されていません");
                messageDsp.DspMsg("M8007");
                textBoxScName.Focus();
                return false;
            }

            // 管理フラグの適否
            if (checkBoxScFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("管理フラグが不確定の状態です");
                messageDsp.DspMsg("M8008");
                checkBoxScFlag.Focus();
                return false;
            }

            // 非表示理由の適否
            if (checkBoxScFlag.Checked == true)
            {
                if (textBoxScHidden.Text == "")
                {
                    //MessageBox.Show("非表示理由の入力が必要です");
                    messageDsp.DspMsg("M8026");
                    textBoxScHidden.Focus();
                    return false;
                }
            }
            return true;
        }
        ///////////////////////////////
        //　8.2.2.2 小分類情報作成
        //メソッド名：GenerateDataAtUpdate()
        //引　数   ：なし
        //戻り値   ：小分類更新情報
        //機　能   ：更新データのセット
        ///////////////////////////////
        private M_SmallClassification GenerateDataAtUpdate()
        {
            return new M_SmallClassification
            {
                ScID = int.Parse(textBoxScID.Text.Trim()),
                ScName = textBoxScName.Text.Trim(),
                ScFlag = ScFlg,
                ScHidden = textBoxScHidden.Text.Trim(),
            };
        }
        ///////////////////////////////
        //　8.2.2.3 小分類情報更新
        //メソッド名：UpdateSmallClassification()
        //引　数   ：小分類情報
        //戻り値   ：なし
        //機　能   ：小分類情報の更新
        ///////////////////////////////
        private void UpdateSmallClassification(M_SmallClassification updSmallClassification)
        {
            if (ScFlg == 0)
            {
                // 更新確認メッセージ
                DialogResult result = messageDsp.DspMsg("M8015");
                if (result == DialogResult.Cancel)
                    return;

                // 小分類情報の更新
                bool flg = smallClassificationDataAccess.UpdateSmallClassificationData(updSmallClassification);
                if (flg == true)
                    //MessageBox.Show("データを更新しました。");
                    messageDsp.DspMsg("M8016");
                else
                    //MessageBox.Show("データの更新に失敗しました。");
                    messageDsp.DspMsg("M8017");

                textBoxScID.Focus();

                // 入力エリアのクリア
                ClearInput();

                // データグリッドビューの表示
                GetDataGridView();
            }

            else if (ScFlg == 2)
            {
                DeleteSmallClassification(updSmallClassification);
            }
        }
        private void DeleteSmallClassification(M_SmallClassification delSmallClassification)
        {

            // 更新確認メッセージ
            DialogResult result = messageDsp.DspMsg("M8019");
            if (result == DialogResult.Cancel)
                return;

            // 小分類情報の更新
            bool flg = smallClassificationDataAccess.DeleteSmallClassificationData(delSmallClassification);
            if (flg == true)
                //MessageBox.Show("データを削除しました。");
                messageDsp.DspMsg("M8020");
            else
                //MessageBox.Show("データの削除に失敗しました。");
                messageDsp.DspMsg("M8021");

            textBoxScID.Focus();

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

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}