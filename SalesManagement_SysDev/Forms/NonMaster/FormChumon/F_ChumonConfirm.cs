using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagement_SysDev.Forms.NonMaster.FormChumon
{
    public partial class F_ChumonConfirm : Form
    {
        //メッセージ表示用クラスのインスタンス化
        MessageDsp messageDsp = new MessageDsp();
        //データベース役職テーブルアクセス用クラスのインスタンス化
        DbAccess.ChumonDataAccess chumonDataAccess = new DbAccess.ChumonDataAccess();
        //入力形式チェック用クラスのインスタンス化
        DataInputFormCheck dataInputFormCheck = new DataInputFormCheck();
        //データグリッドビュー用の役職データ
        private static List<T_ChumonDsp> Chumon;
        private static List<T_ChumonDsp> filteredList;
        //アクティブ用の変数
        internal static int stflg = 0;

        public F_ChumonConfirm()
        {
            InitializeComponent();
        }

        private void F_ChumonConfirm_Load(object sender, EventArgs e)
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
            dataGridViewChumon.ReadOnly = true;
            //行内をクリックすることで行を選択
            dataGridViewChumon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //ヘッダー位置の指定
            dataGridViewChumon.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

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
            // 受注データの取得
            Chumon = chumonDataAccess.GetChumonData();

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
                textBoxPageSize.Text = "10";
            }
            if (textBoxPageNo.Text == "" || textBoxPageNo.Text == "0" || int.Parse(textBoxPageSize.Text) > 9)
            {
                textBoxPageNo.Text = "1";
            }
            int pageSize = int.Parse(textBoxPageSize.Text);
            int pageNo = int.Parse(textBoxPageNo.Text) - 1;

            filteredList = Chumon.Where(x => x.ChFlag != 2).ToList(); //OrFlagが2のレコードは排除する
            filteredList = filteredList.Where(x => x.ChStateFlag != 1).ToList(); //確定済みのレコードは表示しない
            dataGridViewChumon.DataSource = filteredList.Skip(pageSize * pageNo).Take(pageSize).ToList();

            if (filteredList.Count == 0)
            {
                labelNoTable.Visible = true;
            }
            else
            {
                labelNoTable.Visible = false;
            }

            dataGridViewChumon.Columns[1].Visible = false;
            dataGridViewChumon.Columns[2].Visible = false;
            dataGridViewChumon.Columns[7].Visible = false;
            dataGridViewChumon.Columns[8].Visible = false;

            //各列幅の指定
            dataGridViewChumon.Columns[0].Width = 67;     //ChID
            //dataGridViewChumon.Columns[1].Width = 100;  //SoID
            //dataGridViewChumon.Columns[2].Width = 67;     //EmID
            dataGridViewChumon.Columns[3].Width = 67;     //ClID
            dataGridViewChumon.Columns[4].Width = 67;     //OrID
            dataGridViewChumon.Columns[5].Width = 97;     //ChDate
            dataGridViewChumon.Columns[6].Width = 105;    //ChStateFlag
            //dataGridViewChumon.Columns[7].Width = 110;  //ChFlag
            //dataGridViewChumon.Columns[8].Width = 400;  //ChHiddenn
            dataGridViewChumon.Columns[9].Width = 90;     //ChDetailID
            dataGridViewChumon.Columns[10].Width = 67;    //PrID
            dataGridViewChumon.Columns[11].Width = 67;  //ChQuantity

            //各列の文字位置の指定
            dataGridViewChumon.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridViewChumon.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridViewChumon.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewChumon.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewChumon.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewChumon.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewChumon.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridViewChumon.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridViewChumon.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewChumon.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewChumon.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewChumon.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //dataGridViewの総ページ数
            labelPage.Text = "/" + ((int)Math.Ceiling(filteredList.Count / (double)pageSize)) + "ページ";

            dataGridViewChumon.Refresh();

            // 確定可能なデータがない場合
            if (filteredList.Count == 0)
            {
                MessageBox.Show("確定可能なデータはありません", "データなし", 0);
                NonMaster.FormChumon.F_Chumon.stflg = 1;
                this.Close();
                return;
            }
        }

        private void dataGridViewChumon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewChumon.CurrentRow != null)
            {
                //クリックされた行データをテキストボックスへ
                textBoxChID.Text = dataGridViewChumon.Rows[dataGridViewChumon.CurrentRow.Index].Cells[0].Value.ToString();
                textBoxOrID.Text = dataGridViewChumon.Rows[dataGridViewChumon.CurrentRow.Index].Cells[1].Value.ToString();
                textBoxClID.Text = dataGridViewChumon.Rows[dataGridViewChumon.CurrentRow.Index].Cells[3].Value.ToString();

                //日付が設定されていない場合、初期値として現在の日付を設定
                if (dataGridViewChumon.Rows[dataGridViewChumon.CurrentRow.Index].Cells[5].Value == null)
                {
                    dateTimePickerOrDate.Value = DateTime.Now;
                    dateTimePickerOrDate.Checked = false;
                }
                else
                {
                    dateTimePickerOrDate.Text = dataGridViewChumon.Rows[dataGridViewChumon.CurrentRow.Index].Cells[5].Value.ToString();
                }
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
                textBoxPageSize.Text = "10";
            }
            int pageSize = int.Parse(textBoxPageSize.Text);
            dataGridViewChumon.DataSource = filteredList.Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewChumon.Refresh();
            //ページ番号の設定
            textBoxPageNo.Text = "1";
        }

        private void buttonPreviousPage_Click(object sender, EventArgs e)
        {
            if (textBoxPageSize.Text == "" || textBoxPageSize.Text == "0" || textBoxPageSize.TextLength > 9) //Int32の最大値は 2,147,483,647
            {
                textBoxPageSize.Text = "10";
            }
            if (textBoxPageNo.Text == "" || textBoxPageNo.Text == "0" || int.Parse(textBoxPageSize.Text) > 9)
            {
                textBoxPageNo.Text = "1";
            }
            int pageSize = int.Parse(textBoxPageSize.Text);
            int pageNo = int.Parse(textBoxPageNo.Text) - 2;
            dataGridViewChumon.DataSource = filteredList.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewChumon.Refresh();
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
                textBoxPageSize.Text = "10";
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
                dataGridViewChumon.DataSource = filteredList.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewChumon.Refresh();
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
                textBoxPageSize.Text = "10";
            }
            if (textBoxPageNo.Text == "" || textBoxPageNo.Text == "0" || int.Parse(textBoxPageSize.Text) > 9)
            {
                textBoxPageNo.Text = "1";
            }
            int pageSize = int.Parse(textBoxPageSize.Text);
            //最終ページの計算
            int pageNo = (int)Math.Ceiling(filteredList.Count / (double)pageSize) - 1;
            dataGridViewChumon.DataSource = filteredList.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewChumon.Refresh();
            //ページ番号の設定
            textBoxPageNo.Text = (pageNo + 1).ToString();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            // 入力エリアのクリア
            ClearInput();
            // データグリッドビューの表示
            SetFormDataGridView();
        }

        private void ClearInput()
        {
            textBoxChID.Text = "";
            textBoxClID.Text = "";
            textBoxOrID.Text = "";
            dateTimePickerOrDate.Value = DateTime.Now;
            dateTimePickerOrDate.Checked = false;
        }

        private void buttonList_Click(object sender, EventArgs e)
        {
            // データグリッドビューの表示
            SetFormDataGridView();
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
        //　8.2.4.1 妥当な受注データ取得
        //メソッド名：GetValidDataAtSlect()
        //引　数   ：なし
        //戻り値   ：true or false
        //機　能   ：入力データの形式チェック
        //          ：エラーがない場合True
        //          ：エラーがある場合False
        ///////////////////////////////
        private bool GetValidDataAtSelect()
        {
            // 注文IDの適否
            if (!String.IsNullOrEmpty(textBoxChID.Text.Trim()))
            {
                // 注文IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxChID.Text.Trim()))
                {
                    //MessageBox.Show("注文IDは全て半角数字入力です");
                    messageDsp.DspMsg("M12054");
                    textBoxChID.Focus();
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
                    messageDsp.DspMsg("M12064");
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
                    messageDsp.DspMsg("M12001");
                    textBoxClID.Focus();
                    return false;
                }
                // 顧客IDの文字数チェック
                if (textBoxClID.TextLength > 6)
                {
                    //MessageBox.Show("顧客IDは6文字です");
                    messageDsp.DspMsg("M12002");
                    textBoxClID.Focus();
                    return false;
                }
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
        public static string mOrID;
        public static string mClID;
        public static bool mDate;
        private void GenerateDataAtSelect()
        {
            T_ChumonDsp selectCondition;

            mOrID = textBoxOrID.Text.Trim();
            mClID = textBoxClID.Text.Trim();
            mDate = dateTimePickerOrDate.Checked;

            if (mOrID != "")
            {
                // 検索条件のセット
                selectCondition = new T_ChumonDsp()
                {
                    OrID = int.Parse(textBoxOrID.Text.Trim())
                };
                // データの抽出
                Chumon = chumonDataAccess.SearchChumonConfirm(selectCondition);
                return;
            }
            else if (mClID != "")
            {
                // 検索条件のセット
                selectCondition = new T_ChumonDsp()
                {
                    ClID = int.Parse(textBoxClID.Text.Trim()),
                };
                // データの抽出
                Chumon = chumonDataAccess.SearchChumonConfirm(selectCondition);
                return;
            }
            else if (mDate != false)
            {
                // 検索条件のセット
                selectCondition = new T_ChumonDsp()
                {
                    //時刻は含めない(Dateのみ取得)
                    ChDate = dateTimePickerOrDate.Value.Date
                };
                // データの抽出
                Chumon = chumonDataAccess.SearchChumonConfirm(selectCondition);
                return;
            }
        }
        ///////////////////////////////
        //　8.2.4.3 受注抽出結果表示
        //メソッド名：SetSelectData()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：受注情報の表示
        ///////////////////////////////
        private void SetSelectData()
        {
            textBoxPageNo.Text = "1";

            int pageSize = int.Parse(textBoxPageSize.Text);

            dataGridViewChumon.DataSource = Chumon;
            if (Chumon.Count == 0)
            {
                labelNoTable.Visible = true;
            }
            else
            {
                labelNoTable.Visible = false;
            }

            labelPage.Text = "/" + ((int)Math.Ceiling(Chumon.Count / (double)pageSize)) + "ページ";
            dataGridViewChumon.Refresh();

            if (Chumon.Count == 0) //検索結果のデータ数が0ならエラー
            {
                messageDsp.DspMsg("M12068");
                SetFormDataGridView();
            }
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            // 8.2.2.1 妥当な受注データ取得
            if (!GetValidDataAtConfirm())
                return;

            var conChumon = GenerateDataAtChumon();

            //8.2.2.3 受注情報更新
            ConfirmChumon(conChumon);
        }

        ///////////////////////////////
        //　8.2.4.1 妥当な受注データ取得
        //メソッド名：GetValidDataAtSlect()
        //引　数   ：なし
        //戻り値   ：true or false
        //機　能   ：入力データの形式チェック
        //          ：エラーがない場合True
        //          ：エラーがある場合False
        ///////////////////////////////
        private bool GetValidDataAtConfirm()
        {
            // 受注IDの適否
            if (!String.IsNullOrEmpty(textBoxChID.Text.Trim()))
            {
                // 受注IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxChID.Text.Trim()))
                {
                    //MessageBox.Show("受注IDは全て半角数字入力です");
                    messageDsp.DspMsg("M12064");
                    textBoxChID.Focus();
                    return false;
                }
                // 受注IDの存在チェック
                if (!chumonDataAccess.CheckChumonCDExistence(textBoxChID.Text.Trim()))
                {
                    //MessageBox.Show("入力された受注IDは存在しません");
                    messageDsp.DspMsg("M12063");
                    textBoxChID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("受注IDが入力されていません");
                messageDsp.DspMsg("M12070");
                textBoxChID.Focus();
                return false;
            }
            
            // 状態フラグの重複チェック
            if (chumonDataAccess.CheckStateFlagExistence(textBoxChID.Text.Trim()))
            {
                //MessageBox.Show("入力された受注IDは既に確定済みです");
                messageDsp.DspMsg("M12072");
                textBoxChID.Focus();
                return false;
            }

            // 管理フラグのチェック状態
            if (chumonDataAccess.CheckFlagExistence(textBoxChID.Text.Trim()))
            {
                //MessageBox.Show("入力された注文IDは非表示中のため確定できません");
                messageDsp.DspMsg("M12073");
                textBoxChID.Focus();
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
        private T_Chumon GenerateDataAtChumon()
        {
            return new T_Chumon
            {
                ChID = int.Parse(textBoxChID.Text.Trim()),
                ChStateFlag = 1
            };
        }

        private void ConfirmChumon(T_Chumon conChumon)
        {
            // 更新確認メッセージ
            DialogResult result = messageDsp.DspMsg("M12058");
            if (result == DialogResult.Cancel)
                return;

            // 役職情報の更新
            bool flg = chumonDataAccess.ConfirmChumonData(conChumon);
            if (flg == true)
                //MessageBox.Show("データを確定しました。");
                messageDsp.DspMsg("M12059");
            else
                //MessageBox.Show("データの確定に失敗しました。");
                messageDsp.DspMsg("M12060");

            textBoxOrID.Focus();

            // 入力エリアのクリア
            ClearInput();

            // データグリッドビューの表示
            GetDataGridView();

            if(System.Windows.Forms.Form.ActiveForm != null) //nullエラー防止(特定の操作を行うと何故かnullが返り、例外エラーが出る)
            {
                F_Chumon.stflg = 1;
                F_Chumon.ActiveForm.Activate();
                Master.FormStock.F_Stock.stflg = 1;
                Master.FormStock.F_Stock.ActiveForm.Activate();
                FormSyukko.F_Syukko.stflg = 1;
                FormSyukko.F_Syukko.ActiveForm.Activate();
            }
        }

        private void label受注ID_Click(object sender, EventArgs e)
        {
            OpenForm(((Label)sender).Text);
        }

        private void label顧客ID_Click(object sender, EventArgs e)
        {
            OpenForm(((Label)sender).Text);
        }

        private void OpenForm(string formName)
        {
            Form frm = new Form();
            //引数より、開くフォームを設定
            switch (formName)
            {
                case "顧客ID": //ボタンのテキスト名
                    frm = new Master.FormClient.F_Client(); //フォームの名前
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

        private void label顧客ID_MouseEnter(object sender, EventArgs e)
        {
            label顧客ID.BackColor = Color.Aqua;
        }

        private void label顧客ID_MouseLeave(object sender, EventArgs e)
        {
            label顧客ID.BackColor = Color.Transparent;
        }

        private void label受注ID_MouseEnter(object sender, EventArgs e)
        {
            label受注ID.BackColor = Color.Aqua;
        }

        private void label受注ID_MouseLeave(object sender, EventArgs e)
        {
            label受注ID.BackColor = Color.Transparent;
        }

        private void F_ChumonConfirm_Activated(object sender, EventArgs e)
        {
            if (stflg == 1)
            {
                GetDataGridView();
                stflg = 0;
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
                labelFlag.Visible = false;
                labelStateFlag.Visible = false;
                labelStateFlag.Text = "確定済";
            }
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
            }
        }
    }
}
