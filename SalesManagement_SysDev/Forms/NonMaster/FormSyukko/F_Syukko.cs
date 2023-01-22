using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagement_SysDev.Forms.NonMaster.FormSyukko
{
    public partial class F_Syukko : Form
    {
        //メッセージ表示用クラスのインスタンス化
        MessageDsp messageDsp = new MessageDsp();
        //データベース受注テーブルアクセス用クラスのインスタンス化
        DbAccess.SyukkoDataAccess syukkoDataAccess = new DbAccess.SyukkoDataAccess();
        //入力形式チェック用クラスのインスタンス化
        DataInputFormCheck dataInputFormCheck = new DataInputFormCheck();
        //データグリッドビュー用の受注データ
        private static List<T_SyukkoDsp> Syukko;
        private static List<T_SyukkoDsp> filteredList;
        //フラグを数値型で入れるための変数
        int SyStateFlg = 0;
        int SyFlg = 0;
        internal static int stflg = 0;

        public F_Syukko()
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
                case "出庫確定画面へ": //ボタンのテキスト名
                    frm = new F_SyukkoConfirm(); //フォームの名前
                    frm2 = frm;
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

        private void F_Syukko_Load(object sender, EventArgs e)
        {
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
            dataGridViewSyukko.ReadOnly = true;
            //行内をクリックすることで行を選択
            dataGridViewSyukko.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //ヘッダー位置の指定
            dataGridViewSyukko.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

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
            Syukko = syukkoDataAccess.GetSyukkoData();

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
            filteredList = Syukko.Where(x => x.SyFlag != 2).ToList(); //OrFlagが2のレコードは排除する
            dataGridViewSyukko.DataSource = filteredList.Skip(pageSize * pageNo).Take(pageSize).ToList();

            if (filteredList.Count == 0)
            {
                labelNoTable.Visible = true;
            }
            else
            {
                labelNoTable.Visible = false;
            }

            //各列幅の指定
            //dataGridViewSyukko.Columns[0].Width = 100;
            //dataGridViewSyukko.Columns[1].Width = 100;
            //dataGridViewSyukko.Columns[2].Width = 100;
            //dataGridViewSyukko.Columns[3].Width = 100;
            //dataGridViewSyukko.Columns[4].Width = 100;
            //dataGridViewSyukko.Columns[5].Width = 130;
            //dataGridViewSyukko.Columns[6].Width = 110;
            //dataGridViewSyukko.Columns[7].Width = 110;
            //dataGridViewSyukko.Columns[8].Width = 400;
            //dataGridViewSyukko.Columns[9].Width = 100;
            //dataGridViewSyukko.Columns[10].Width = 100;
            //dataGridViewSyukko.Columns[11].Width = 70;

            // 自動サイズ調整を有効にする
            dataGridViewSyukko.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //各列の文字位置の指定
            dataGridViewSyukko.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewSyukko.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewSyukko.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewSyukko.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewSyukko.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewSyukko.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewSyukko.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewSyukko.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewSyukko.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewSyukko.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewSyukko.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewSyukko.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //dataGridViewの総ページ数
            labelPage.Text = "/" + ((int)Math.Ceiling(filteredList.Count / (double)pageSize)) + "ページ";

            dataGridViewSyukko.Refresh();
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
            GetDataGridView();
        }

        private void buttonDetailClear_Click(object sender, EventArgs e)
        {
            // 受注詳細欄の入力エリアのクリア
            textBoxSyDetailID.Text = "";
            textBoxPrID.Text = "";
            textBoxSyQuantity.Text = "";

            // データグリッドビューの表示
            SetFormDataGridView();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (frm2 != null)
                frm2.Close();
            this.Close();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            // 8.2.2.1 妥当な役職データ取得
            if (!GetValidDataAtUpdate())
                return;

            // 8.2.2.2 役職情報作成
            var updSyukko = GenerateDataAtUpdate();

            // 8.2.2.3 役職情報更新
            DeleteSyukko(updSyukko);
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
            // 出庫IDの未入力チェック
            if (!String.IsNullOrEmpty(textBoxSyID.Text.Trim()))
            {
                // 出庫IDの半角英数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxSyID.Text.Trim()))
                {
                    //MessageBox.Show("出庫IDは全て半角英数字入力です");
                    messageDsp.DspMsg("M16013");
                    textBoxSyID.Focus();
                    return false;
                }
                // 出庫IDの文字数チェック
                if (textBoxSyID.TextLength > 6)
                {
                    //MessageBox.Show("出庫IDは6文字です");
                    messageDsp.DspMsg("M16014");
                    textBoxSyID.Focus();
                    return false;
                }
                // 出庫IDの存在チェック
                if (!syukkoDataAccess.CheckSyukkoCDExistence(textBoxSyID.Text.Trim()))
                {
                    //MessageBox.Show("入力された出庫IDは存在しません");
                    messageDsp.DspMsg("M16015");
                    textBoxSyID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("出庫IDが入力されていません");
                messageDsp.DspMsg("M16051");
                textBoxSyID.Focus();
                return false;
            }

            // 受注IDの未入力チェック
            if (!String.IsNullOrEmpty(textBoxOrID.Text.Trim()))
            {
                // 受注IDの文字数チェック
                if (textBoxOrID.TextLength > 6)
                {
                    //MessageBox.Show("受注IDは6文字です");
                    messageDsp.DspMsg("M16055");
                    textBoxOrID.Focus();
                    return false;
                }
                // 受注IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxOrID.Text.Trim()))
                {
                    //MessageBox.Show("受注IDは全て半角数字入力です");
                    messageDsp.DspMsg("M16054");
                    textBoxOrID.Focus();
                    return false;
                }
                // 受注IDの存在チェック
                if (!syukkoDataAccess.CheckOrderIDExistence(textBoxSyID.Text.Trim(), textBoxOrID.Text.Trim()))
                {
                    //MessageBox.Show("出庫IDに関連する受注IDが一致しません");
                    messageDsp.DspMsg("M16053");
                    textBoxOrID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("受注IDが入力されていません");
                messageDsp.DspMsg("M16062");
                textBoxOrID.Focus();
                return false;
            }

            // 管理フラグの適否
            if (checkBoxSyFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("管理フラグが不確定の状態です");
                messageDsp.DspMsg("M16009");
                checkBoxSyFlag.Focus();
                return false;
            }

            // 管理フラグの適否２
            if (checkBoxSyFlag.Checked == false)
            {
                //MessageBox.Show("管理フラグが未チェックです");
                messageDsp.DspMsg("M16029");
                checkBoxSyFlag.Focus();
                return false;
            }

            // 非表示理由の適否
            if (checkBoxSyFlag.Checked == true)
            {
                if (textBoxSyHidden.Text == "")
                {
                    //MessageBox.Show("非表示理由の入力が必要です");
                    messageDsp.DspMsg("M16030");
                    textBoxSyHidden.Focus();
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
        private T_Syukko GenerateDataAtUpdate()
        {
            return new T_Syukko
            {
                SyID = int.Parse(textBoxSyID.Text.Trim()),
                OrID = int.Parse(textBoxOrID.Text.Trim()),
                SyFlag = SyFlg,
                SyHidden = textBoxSyHidden.Text.Trim()
            };
        }
        ///////////////////////////////
        //　8.2.2.3 役職情報更新
        //メソッド名：UpdateSyukko()
        //引　数   ：役職情報
        //戻り値   ：なし
        //機　能   ：役職情報の更新
        ///////////////////////////////
        private void DeleteSyukko(T_Syukko delSyukko)
        {
            // 更新確認メッセージ
            DialogResult result = messageDsp.DspMsg("M16038");
            if (result == DialogResult.Cancel)
            {
                return;
            }

            // 役職情報の更新
            bool flg = syukkoDataAccess.DeleteSyukkoData(delSyukko);
            if (flg == true)
                //MessageBox.Show("データを削除しました。");
                messageDsp.DspMsg("M16039");
            else
                //MessageBox.Show("データの削除に失敗しました。");
                messageDsp.DspMsg("M16040");

            textBoxSyID.Focus();

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
            NonMaster.FormSyukko.F_SyukkoConfirm.stflg = 1;
            NonMaster.FormSyukko.F_SyukkoConfirm.ActiveForm.Activate();
            NonMaster.FormArrival.F_Arrival.stflg = 1;
            NonMaster.FormArrival.F_Arrival.ActiveForm.Activate();
            NonMaster.FormShipment.F_Shipment.stflg = 1;
            NonMaster.FormShipment.F_Shipment.ActiveForm.Activate();
            NonMaster.FormSale.F_Sale.stflg = 1;
            NonMaster.FormSale.F_Sale.ActiveForm.Activate();
            Master.FormStock.F_Stock.stflg = 1;
            Master.FormStock.F_Stock.ActiveForm.Activate();
        }

        private void checkBoxSyStateFlag_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSyStateFlag.Checked == true)
            {
                SyStateFlg = 1;
                return;
            }
            else if (checkBoxSyStateFlag.Checked == false)
            {
                SyStateFlg = 0;
                return;
            }
        }

        private void checkBoxSyFlag_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSyFlag.Checked == true)
            {
                SyFlg = 2;
                textBoxSyHidden.Enabled = true;
                return;
            }
            else if (checkBoxSyFlag.Checked == false)
            {
                SyFlg = 0;
                textBoxSyHidden.Enabled = false;
                textBoxSyHidden.Text = "";
                return;
            }
        }

        private void ClearInput()
        {
            textBoxClID.Text = "";
            textBoxSyID.Text = "";
            textBoxEmID.Text = "";
            textBoxSoID.Text = "";
            dateTimePickerSyDate.Value = DateTime.Now;
            dateTimePickerSyDate.Checked = false;
            textBoxOrID.Text = "";
            textBoxSyHidden.Text = "";
            checkBoxSyStateFlag.Checked = false;
            checkBoxSyFlag.Checked = false;
            textBoxSyDetailID.Text = "";
            textBoxPrID.Text = "";
            textBoxSyQuantity.Text = "";
        }

        private void dataGridViewSyukko_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewSyukko.CurrentRow != null)
            {
                //クリックされた行データをテキストボックスへ
                textBoxSyID.Text = dataGridViewSyukko.Rows[dataGridViewSyukko.CurrentRow.Index].Cells[0].Value.ToString();
                textBoxEmID.Text = dataGridViewSyukko.Rows[dataGridViewSyukko.CurrentRow.Index].Cells[1].Value.ToString();
                textBoxClID.Text = dataGridViewSyukko.Rows[dataGridViewSyukko.CurrentRow.Index].Cells[2].Value.ToString();
                textBoxSoID.Text = dataGridViewSyukko.Rows[dataGridViewSyukko.CurrentRow.Index].Cells[3].Value.ToString();
                textBoxOrID.Text = dataGridViewSyukko.Rows[dataGridViewSyukko.CurrentRow.Index].Cells[4].Value.ToString();
                //日付が設定されていない場合、初期値として現在の日付を設定
                if (dataGridViewSyukko.Rows[dataGridViewSyukko.CurrentRow.Index].Cells[5].Value == null)
                {
                    dateTimePickerSyDate.Value = DateTime.Now;
                    dateTimePickerSyDate.Checked = false;
                }
                else
                {
                    dateTimePickerSyDate.Text = dataGridViewSyukko.Rows[dataGridViewSyukko.CurrentRow.Index].Cells[5].Value.ToString();
                }

                int OrStateFlg2 = int.Parse(dataGridViewSyukko.Rows[dataGridViewSyukko.CurrentRow.Index].Cells[6].Value.ToString());
                if (OrStateFlg2 == 0)
                {
                    checkBoxSyStateFlag.Checked = false;
                }
                else
                {
                    checkBoxSyStateFlag.Checked = true;
                }
                int SyFlg2 = int.Parse(dataGridViewSyukko.Rows[dataGridViewSyukko.CurrentRow.Index].Cells[7].Value.ToString());
                if (SyFlg2 == 0)
                {
                    checkBoxSyFlag.Checked = false;
                }
                else if (SyFlg2 == 2)
                {
                    checkBoxSyFlag.Checked = true;
                }
                textBoxSyHidden.Text = dataGridViewSyukko.Rows[dataGridViewSyukko.CurrentRow.Index].Cells[8].Value.ToString();
                textBoxSyDetailID.Text = dataGridViewSyukko.Rows[dataGridViewSyukko.CurrentRow.Index].Cells[9].Value.ToString();
                textBoxPrID.Text = dataGridViewSyukko.Rows[dataGridViewSyukko.CurrentRow.Index].Cells[10].Value.ToString();
                textBoxSyQuantity.Text = dataGridViewSyukko.Rows[dataGridViewSyukko.CurrentRow.Index].Cells[11].Value.ToString();
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
            dataGridViewSyukko.DataSource = filteredList.Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewSyukko.Refresh();
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
            dataGridViewSyukko.DataSource = filteredList.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewSyukko.Refresh();
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
                dataGridViewSyukko.DataSource = filteredList.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewSyukko.Refresh();
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
            dataGridViewSyukko.DataSource = filteredList.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewSyukko.Refresh();
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
                    mEmName = mEmployee.EmName;
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
                        labelPrName.Text = "(非表示)" + mPrName;
                        labelPrName.Visible = true;
                        context.Dispose();
                    }
                    else
                    {
                        string mPrName = mProduct.PrName;
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

        private void F_Syukko_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (frm2 != null)
                frm2.Close();
        }

        private void F_Syukko_Activated(object sender, EventArgs e)
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
            if (F_menu.loginSalesOffice.Contains("営業所"))
            {
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
