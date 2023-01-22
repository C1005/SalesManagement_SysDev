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
    public partial class F_Position : Form
    {
        //メッセージ表示用クラスのインスタンス化
        MessageDsp messageDsp = new MessageDsp();
        //データベース役職テーブルアクセス用クラスのインスタンス化
        DbAccess.PositionDataAccess positionDataAccess = new  DbAccess.PositionDataAccess();
        //入力形式チェック用クラスのインスタンス化
        DataInputFormCheck dataInputFormCheck = new DataInputFormCheck();
        //データグリッドビュー用の役職データ
        private static List<M_Position> Position;
        //管理フラグを数値型で入れるための変数
        int PoFlg;

        public F_Position()
        {
            InitializeComponent();
        }
        private void F_Position_Load(object sender, EventArgs e)
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
            dataGridViewPosition.ReadOnly = true;
            //行内をクリックすることで行を選択
            dataGridViewPosition.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //ヘッダー位置の指定
            dataGridViewPosition.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

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

            // 役職データの取得
            Position = positionDataAccess.GetPositionData();

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
            dataGridViewPosition.DataSource = Position.Skip(pageSize * pageNo).Take(pageSize).ToList();

            if (Position.Count == 0)
            {
                labelNoTable.Visible = true;
            }
            else
            {
                labelNoTable.Visible = false;
            }

            //各列幅の指定
            dataGridViewPosition.Columns[0].Width = 140;
            dataGridViewPosition.Columns[1].Width = 230;
            dataGridViewPosition.Columns[2].Width = 150;
            dataGridViewPosition.Columns[3].AutoSizeMode = (DataGridViewAutoSizeColumnMode)DataGridViewAutoSizeColumnsMode.Fill;

            //各列の文字位置の指定
            dataGridViewPosition.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewPosition.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewPosition.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewPosition.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            //dataGridViewの総ページ数
            labelPage.Text = "/" + ((int)Math.Ceiling(Position.Count / (double)pageSize)) + "ページ";

            dataGridViewPosition.Refresh();
        }
        ///////////////////////////////
        //メソッド名：buttonPageSizeChange_Click()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：データグリッドビューの表示件数変更
        ///////////////////////////////
        private void buttonPageSizeChange_Click_1(object sender, EventArgs e)
        {
            SetDataGridView();
        }

        ///////////////////////////////
        //メソッド名：buttonFirstPage_Click()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：データグリッドビューの先頭ページ表示
        ///////////////////////////////
        private void buttonFirstPage_Click_1(object sender, EventArgs e)
        {
            if (textBoxPageSize.Text == "" || textBoxPageSize.Text == "0" || textBoxPageSize.TextLength > 9) //Int32の最大値は 2,147,483,647
            {
                textBoxPageSize.Text = "15";
            }
            int pageSize = int.Parse(textBoxPageSize.Text);
            dataGridViewPosition.DataSource = Position.Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewPosition.Refresh();
            //ページ番号の設定
            textBoxPageNo.Text = "1";
        }
        ///////////////////////////////
        //メソッド名：buttonPreviousPage_Click()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：データグリッドビューの前ページ表示
        ///////////////////////////////
        private void buttonPreviousPage_Click_1(object sender, EventArgs e)
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
            dataGridViewPosition.DataSource = Position.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewPosition.Refresh();
            //ページ番号の設定
            if (pageNo + 1 > 1)
                textBoxPageNo.Text = (pageNo + 1).ToString();
            else
                textBoxPageNo.Text = "1";
        }
        ///////////////////////////////
        //メソッド名：buttonNextPage_Click()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：データグリッドビューの次ページ表示
        ///////////////////////////////

        private void buttonNextPage_Click_1(object sender, EventArgs e)
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
            int lastNo = (int)Math.Ceiling(Position.Count / (double)pageSize) - 1;
            //最終ページでなければ
            if (pageNo <= lastNo)
                dataGridViewPosition.DataSource = Position.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewPosition.Refresh();
            //ページ番号の設定
            int lastPage = (int)Math.Ceiling(Position.Count / (double)pageSize);
            if (pageNo >= lastPage)
                textBoxPageNo.Text = lastPage.ToString();
            else
                textBoxPageNo.Text = (pageNo + 1).ToString();
        }
        ///////////////////////////////
        //メソッド名：buttonLastPage_Click()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：データグリッドビューの最終ページ表示
        ///////////////////////////////
        
        private void buttonLastPage_Click_1(object sender, EventArgs e)
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
            int pageNo = (int)Math.Ceiling(Position.Count / (double)pageSize) - 1;
            dataGridViewPosition.DataSource = Position.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewPosition.Refresh();
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
            textBoxPoID.Text = "";
            textBoxPoName.Text = "";
            textBoxPoHidden.Text = "";
            checkBoxPoFlag.Checked = false;
        }

        private void buttonRegist_Click(object sender, EventArgs e)
        {
            // 8.2.1.1 妥当な役職データ取得
            if (!GetValidDataAtRegistration())
                return;

            // 8.2.1.2 役職情報作成
            var regPosition = GenerateDataAtRegistration();

            // 8.2.1.3 役職情報登録
            RegistrationPosition(regPosition);
        }

        ///////////////////////////////
        //　8.2.1.1 妥当な役職データ取得
        //メソッド名：GetValidDataAtRegistration()
        //引　数   ：なし
        //戻り値   ：true or false
        //機　能   ：入力データの形式チェック
        //          ：エラーがない場合True
        //          ：エラーがある場合False
        ///////////////////////////////
        private bool GetValidDataAtRegistration()
        {

            // 役職IDの適否
            if (!String.IsNullOrEmpty(textBoxPoID.Text.Trim()))
            {
                // 役職IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxPoID.Text.Trim()))
                {
                    //MessageBox.Show("役職IDは全て半角数字入力です");
                    messageDsp.DspMsg("M1001");
                    textBoxPoID.Focus();
                    return false;
                }
                // 役職IDの文字数チェック
                if (textBoxPoID.TextLength > 2)
                {
                    //MessageBox.Show("役職IDは2文字です");
                    messageDsp.DspMsg("M1002");
                    textBoxPoID.Focus();
                    return false;
                }
                // 役職IDの重複チェック
                if (positionDataAccess.CheckPositionCDExistence(textBoxPoID.Text.Trim()))
                {
                    //MessageBox.Show("入力された役職IDは既に存在します");
                    messageDsp.DspMsg("M1003");
                    textBoxPoID.Focus();
                    return false;
                }
                // 役職IDが0ではないかチェック
                if (int.Parse(textBoxPoID.Text.Trim()) == 0)
                {
                    //MessageBox.Show("役職IDは01から割り当ててください");
                    messageDsp.DspMsg("M1024");
                    textBoxPoID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("役職IDが入力されていません");
                messageDsp.DspMsg("M1004");
                textBoxPoID.Focus();
                return false;
            }

            // 役職名の適否
            if (!String.IsNullOrEmpty(textBoxPoName.Text.Trim()))
            {
                // 役職名の全角チェック
                if (!dataInputFormCheck.CheckFullWidth(textBoxPoName.Text.Trim()))
                {
                    //MessageBox.Show("役職名は全て全角入力です");
                    messageDsp.DspMsg("M1005");
                    textBoxPoName.Focus();
                    return false;
                }
                // 役職名の文字数チェック
                if (textBoxPoName.TextLength > 50)
                {
                    //MessageBox.Show("役職名は25文字以下です");
                    messageDsp.DspMsg("M1006");
                    textBoxPoName.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("役職名が入力されていません");
                messageDsp.DspMsg("M1007");
                textBoxPoName.Focus();
                return false;
            }

            // 管理フラグの適否
            if (checkBoxPoFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("管理フラグが不確定の状態です");
                messageDsp.DspMsg("M1008");
                checkBoxPoFlag.Focus();
                return false;
            }

            // 管理フラグの適否２
            if (checkBoxPoFlag.Checked == true)
            {
                //MessageBox.Show("登録では管理フラグは適用されません");
                messageDsp.DspMsg("M1022");
                checkBoxPoFlag.Focus();
                return false;
            }

            return true;
        }
        ///////////////////////////////
        //　8.2.1.2 役職情報作成
        //メソッド名：GenerateDataAtRegistration()
        //引　数   ：なし
        //戻り値   ：役職登録情報
        //機　能   ：登録データのセット
        ///////////////////////////////
        private M_Position GenerateDataAtRegistration()
        {
            return new M_Position
            {
                PoID = int.Parse(textBoxPoID.Text.Trim()),
                PoName = textBoxPoName.Text.Trim(),
                PoFlag = PoFlg,
                PoHidden = textBoxPoHidden.Text.Trim(),
            };
        }
        ///////////////////////////////
        //　8.2.1.3 役職情報登録
        //メソッド名：RegistrationPosition()
        //引　数   ：役職情報
        //戻り値   ：なし
        //機　能   ：役職データの登録
        ///////////////////////////////
        private void RegistrationPosition(M_Position regPosition)
        {
            // 登録確認メッセージ
            DialogResult result = messageDsp.DspMsg("M1010");
            if (result == DialogResult.Cancel)
                return;
            
            // 役職情報の登録
            bool flg = positionDataAccess.AddPositionData(regPosition);
            if (flg == true)
                //MessageBox.Show("データを登録しました。");
                messageDsp.DspMsg("M1011");
            else
                //MessageBox.Show("データの登録に失敗しました。");
                messageDsp.DspMsg("M1012");

            textBoxPoID.Focus();

            // 入力エリアのクリア
            ClearInput();

            // データグリッドビューの表示
            GetDataGridView();

        }

        private void checkBoxPoFlag_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPoFlag.Checked)
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
                PoFlg = 2;
                textBoxPoHidden.Enabled = true;
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
                PoFlg = 0;
                textBoxPoHidden.Enabled = false;
                textBoxPoHidden.Text = "";
                return;
            }
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
            if (!String.IsNullOrEmpty(textBoxPoID.Text.Trim()))
            {
                // 役職IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxPoID.Text.Trim()))
                {
                    //MessageBox.Show("役職IDは全て半角数字入力です");
                    messageDsp.DspMsg("M1001");
                    textBoxPoID.Focus();
                    return false;
                }
                // 役職IDの文字数チェック
                if (textBoxPoID.TextLength > 2)
                {
                    //MessageBox.Show("役職IDは2文字までです");
                    messageDsp.DspMsg("M1002");
                    textBoxPoID.Focus();
                    return false;
                }
            }
            // 役職名入力時チェック
            if (!String.IsNullOrEmpty(textBoxPoName.Text.Trim()))
            {
                // 役職名の全角チェック
                if (!dataInputFormCheck.CheckFullWidth(textBoxPoName.Text.Trim()))
                {
                    //MessageBox.Show("役職名は全て全角入力です");
                    messageDsp.DspMsg("M1005");
                    textBoxPoName.Focus();
                    return false;
                }
                // 役職名の文字数チェック
                if (textBoxPoName.TextLength > 50)
                {
                    //MessageBox.Show("役職名は50文字以下です");
                    messageDsp.DspMsg("M1006");
                    textBoxPoName.Focus();
                    return false;
                }
            }
            // 管理フラグの適否
            if (checkBoxPoFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("管理フラグが不確定の状態です");
                messageDsp.DspMsg("M1008");
                checkBoxPoFlag.Focus();
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

        public static string mPoID; //cSoIDを別クラス(SalesOfficeDataAccess)でも使用できるように定義
        public static string mPoName;
        public static bool mPoFlg;
        private void GenerateDataAtSelect()
        {
            M_Position selectCondition;

            mPoID = textBoxPoID.Text.Trim(); //IDはnullではなく、""で空白が入るのでstringで代入
            mPoName = textBoxPoName.Text.Trim();
            mPoFlg = checkBoxPoFlag.Checked;

            if (mPoID != "") //IDで検索
            {
                // 検索条件のセット
                selectCondition = new M_Position()
                {
                    PoID = int.Parse(textBoxPoID.Text.Trim()),
                    PoFlag = PoFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // 営業所データの抽出
                Position = positionDataAccess.GetPositionData(selectCondition);
                return;
            }
            else if (mPoName != "") // 名前で検索
            {
                // 検索条件のセット
                selectCondition = new M_Position()
                {
                    PoName = textBoxPoName.Text.Trim(),
                    PoFlag = PoFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // 営業所データの抽出
                Position = positionDataAccess.GetPositionData(selectCondition);
                return;
            }
            else if (mPoFlg == true) // ただの非表示一覧表示
            {
                // 検索条件のセット
                selectCondition = new M_Position()
                {
                    PoFlag = PoFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // 営業所データの抽出
                Position = positionDataAccess.GetPositionData(selectCondition);
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

            dataGridViewPosition.DataSource = Position;
            if (Position.Count == 0)
            {
                labelNoTable.Visible = true;
            }
            else
            {
                labelNoTable.Visible = false;
            }

            labelPage.Text = "/" + ((int)Math.Ceiling(Position.Count / (double)pageSize)) + "ページ";
            dataGridViewPosition.Refresh();

            if (Position.Count == 0) //検索結果のデータ数が0ならエラー
            {
                messageDsp.DspMsg("M1025");
                SetFormDataGridView();
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            // 8.2.2.1 妥当な役職データ取得
            if (!GetValidDataAtUpdate())
                return;

            // 8.2.2.2 役職情報作成
            var updPosition = GenerateDataAtUpdate();

            // 8.2.2.3 役職情報更新
            UpdatePosition(updPosition);
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
        private bool GetValidDataAtUpdate()
        {

            // 役職IDの未入力チェック
            if (!String.IsNullOrEmpty(textBoxPoID.Text.Trim()))
            {
                // 役職IDの半角英数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxPoID.Text.Trim()))
                {
                    //MessageBox.Show("役職IDは全て半角英数字入力です");
                    messageDsp.DspMsg("M1001");
                    textBoxPoID.Focus();
                    return false;
                }
                // 役職IDの文字数チェック
                if (textBoxPoID.TextLength > 2)
                {
                    //MessageBox.Show("役職IDは2文字です");
                    messageDsp.DspMsg("M1002");
                    textBoxPoID.Focus();
                    return false;
                }
                // 役職IDの存在チェック
                if (!positionDataAccess.CheckPositionCDExistence(textBoxPoID.Text.Trim()))
                {
                    //MessageBox.Show("入力された役職IDは存在しません");
                    messageDsp.DspMsg("M1013");
                    textBoxPoID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("役職IDが入力されていません");
                messageDsp.DspMsg("M1004");
                textBoxPoID.Focus();
                return false;
            }


            // 役職名の適否
            if (!String.IsNullOrEmpty(textBoxPoName.Text.Trim()))
            {
                // 役職名の全角チェック
                if (!dataInputFormCheck.CheckFullWidth(textBoxPoName.Text.Trim()))
                {
                    //MessageBox.Show("役職名は全て全角入力です");
                    messageDsp.DspMsg("M1005");
                    textBoxPoName.Focus();
                    return false;
                }
                // 役職名の文字数チェック
                if (textBoxPoName.TextLength > 50)
                {
                    //MessageBox.Show("役職名は50文字以下です");
                    messageDsp.DspMsg("M1006");
                    textBoxPoName.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("役職名が入力されていません");
                messageDsp.DspMsg("M1007");
                textBoxPoName.Focus();
                return false;
            }

            // 管理フラグの適否
            if (checkBoxPoFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("管理フラグが不確定の状態です");
                messageDsp.DspMsg("M1008");
                checkBoxPoFlag.Focus();
                return false;
            }

            // 非表示理由の適否
            if (checkBoxPoFlag.Checked == true)
            {
                if (textBoxPoHidden.Text == "")
                {
                    //MessageBox.Show("非表示理由の入力が必要です");
                    messageDsp.DspMsg("M1023");
                    textBoxPoHidden.Focus();
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
        private M_Position GenerateDataAtUpdate()
        {
            return new M_Position
            {
                PoID = int.Parse(textBoxPoID.Text.Trim()),
                PoName = textBoxPoName.Text.Trim(),
                PoFlag = PoFlg,
                PoHidden = textBoxPoHidden.Text.Trim(),
            };
        }
        ///////////////////////////////
        //　8.2.2.3 役職情報更新
        //メソッド名：UpdatePosition()
        //引　数   ：役職情報
        //戻り値   ：なし
        //機　能   ：役職情報の更新
        ///////////////////////////////
        private void UpdatePosition(M_Position updPosition)
        {
            if (PoFlg == 0)
            {
                // 更新確認メッセージ
                DialogResult result = messageDsp.DspMsg("M1014");
                if (result == DialogResult.Cancel)
                    return;

                // 役職情報の更新
                bool flg = positionDataAccess.UpdatePositionData(updPosition);
                if (flg == true)
                    //MessageBox.Show("データを更新しました。");
                    messageDsp.DspMsg("M1015");
                else
                    //MessageBox.Show("データの更新に失敗しました。");
                    messageDsp.DspMsg("M1016");

                textBoxPoID.Focus();

                // 入力エリアのクリア
                ClearInput();

                // データグリッドビューの表示
                GetDataGridView();
            }

            else if (PoFlg == 2)
            {
                DeletePosition(updPosition);
            }
        }
        private void DeletePosition(M_Position delPosition)
        {
           
                // 更新確認メッセージ
                DialogResult result = messageDsp.DspMsg("M1018");
                if (result == DialogResult.Cancel)
                    return;

                // 役職情報の更新
                bool flg = positionDataAccess.DeletePositionData(delPosition);
                if (flg == true)
                    //MessageBox.Show("データを削除しました。");
                    messageDsp.DspMsg("M1019");
                else
                    //MessageBox.Show("データの削除に失敗しました。");
                    messageDsp.DspMsg("M1020");

            textBoxPoID.Focus();

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

        private void dataGridViewPosition_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewPosition.CurrentRow != null)
            {
                //クリックされた行データをテキストボックスへ
                textBoxPoID.Text = dataGridViewPosition.Rows[dataGridViewPosition.CurrentRow.Index].Cells[0].Value.ToString();
                textBoxPoName.Text = dataGridViewPosition.Rows[dataGridViewPosition.CurrentRow.Index].Cells[1].Value.ToString();
                int PoFlg2 = int.Parse(dataGridViewPosition.Rows[dataGridViewPosition.CurrentRow.Index].Cells[2].Value.ToString());
                if (PoFlg2 == 0)
                {
                    checkBoxPoFlag.Checked = false;
                }
                else if (PoFlg2 == 2)
                {
                    checkBoxPoFlag.Checked = true;
                }
                textBoxPoHidden.Text = dataGridViewPosition.Rows[dataGridViewPosition.CurrentRow.Index].Cells[3].Value.ToString();
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void F_Position_Activated(object sender, EventArgs e)
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
