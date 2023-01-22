using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagement_SysDev.Forms.NonMaster.FormWarehousing
{
    public partial class F_Warehousing : Form
    {
        //メッセージ表示用クラスのインスタンス化
        MessageDsp messageDsp = new MessageDsp();
        //データベース役職テーブルアクセス用クラスのインスタンス化
        DbAccess.WarehousingDataAccess warehousingDataAccess = new DbAccess.WarehousingDataAccess();
        //入力形式チェック用クラスのインスタンス化
        DataInputFormCheck dataInputFormCheck = new DataInputFormCheck();
        //データグリッドビュー用の役職データ
        private static List<T_WarehousingDsp> Warehousing;
        private static List<T_WarehousingDsp> filteredList;
        //管理フラグを数値型で入れるための変数
        int WaFlg;
        int WaShelfFlg;
        internal static int stflg = 0;

        public F_Warehousing()
        {
            InitializeComponent();
        }

        private void buttonConfirmForm_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private void label発注ID_Click(object sender, EventArgs e)
        {
            OpenForm(((Label)sender).Text);
        }

        private void label入庫確認社員ID_Click(object sender, EventArgs e)
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
                case "発注ID": //ボタンのテキスト名
                    frm = new FormHattyu.F_Hattyu(); //フォームの名前
                    break;
                case "商品ID": //ボタンのテキスト名
                    frm = new Master.FormProduct.F_Product(); //フォームの名前
                    break;
                case "入庫確認社員ID": //ボタンのテキスト名
                    frm = new Master.FormEmployee.F_Employee(); //フォームの名前
                    break;
                case "入庫確定画面へ": //ボタンのテキスト名
                    frm = new F_WarehousingConfirm(); //フォームの名前
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

        private void F_Warehousing_Load(object sender, EventArgs e)
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
            dataGridViewWarehousing.ReadOnly = true;
            //行内をクリックすることで行を選択
            dataGridViewWarehousing.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //ヘッダー位置の指定
            dataGridViewWarehousing.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

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
            Warehousing = warehousingDataAccess.GetWarehousingData();

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
            filteredList = Warehousing.Where(x => x.WaFlag != 2).ToList(); //OrFlagが2のレコードは排除する
            dataGridViewWarehousing.DataSource = filteredList.Skip(pageSize * pageNo).Take(pageSize).ToList();

            if (filteredList.Count == 0)
            {
                labelNoTable.Visible = true;
            }
            else
            {
                labelNoTable.Visible = false;
            }

            //各列幅の指定
            dataGridViewWarehousing.Columns[0].Width = 100;
            dataGridViewWarehousing.Columns[1].Width = 100;
            dataGridViewWarehousing.Columns[2].Width = 120;
            dataGridViewWarehousing.Columns[3].Width = 130;
            dataGridViewWarehousing.Columns[4].Width = 120;
            dataGridViewWarehousing.Columns[5].AutoSizeMode = (DataGridViewAutoSizeColumnMode)DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewWarehousing.Columns[6].Width = 110;
            dataGridViewWarehousing.Columns[7].Width = 100;
            dataGridViewWarehousing.Columns[8].Width = 100;
            dataGridViewWarehousing.Columns[9].Width = 70;

            //各列の文字位置の指定
            dataGridViewWarehousing.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewWarehousing.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewWarehousing.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewWarehousing.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewWarehousing.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewWarehousing.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewWarehousing.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewWarehousing.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewWarehousing.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewWarehousing.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //dataGridViewの総ページ数
            labelPage.Text = "/" + ((int)Math.Ceiling(filteredList.Count / (double)pageSize)) + "ページ";

            dataGridViewWarehousing.Refresh();
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
            dataGridViewWarehousing.DataSource = filteredList.Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewWarehousing.Refresh();
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
            dataGridViewWarehousing.DataSource = filteredList.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewWarehousing.Refresh();
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
                dataGridViewWarehousing.DataSource = filteredList.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewWarehousing.Refresh();
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
            dataGridViewWarehousing.DataSource = filteredList.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewWarehousing.Refresh();
            //ページ番号の設定
            textBoxPageNo.Text = (pageNo + 1).ToString();
        }

        private void dataGridViewWarehousing_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewWarehousing.CurrentRow != null)
            {
                //クリックされた行データをテキストボックスへ
                textBoxWaID.Text = dataGridViewWarehousing.Rows[dataGridViewWarehousing.CurrentRow.Index].Cells[0].Value.ToString();
                textBoxHaID.Text = dataGridViewWarehousing.Rows[dataGridViewWarehousing.CurrentRow.Index].Cells[1].Value.ToString();
                textBoxEmID.Text = dataGridViewWarehousing.Rows[dataGridViewWarehousing.CurrentRow.Index].Cells[2].Value.ToString();

                //日付が設定されていない場合、初期値として現在の日付を設定
                if (dataGridViewWarehousing.Rows[dataGridViewWarehousing.CurrentRow.Index].Cells[3].Value == null)
                {
                    dateTimePickerWaDate.Value = DateTime.Now;
                    dateTimePickerWaDate.Checked = false;
                }
                else
                {
                    dateTimePickerWaDate.Text = dataGridViewWarehousing.Rows[dataGridViewWarehousing.CurrentRow.Index].Cells[3].Value.ToString();
                }

                //入庫済フラグの数値型をbool型に変換して取得
                int WaShelfFlag2 = int.Parse(dataGridViewWarehousing.Rows[dataGridViewWarehousing.CurrentRow.Index].Cells[4].Value.ToString());
                if (WaShelfFlag2 == 0)
                {
                    checkBoxWaShelfFlag.Checked = false;
                }
                else
                {
                    checkBoxWaShelfFlag.Checked = true;
                }

                textBoxWaHidden.Text = dataGridViewWarehousing.Rows[dataGridViewWarehousing.CurrentRow.Index].Cells[5].Value.ToString();

                //管理フラグの数値型をbool型に変換して取得
                int WaFlg2 = int.Parse(dataGridViewWarehousing.Rows[dataGridViewWarehousing.CurrentRow.Index].Cells[6].Value.ToString());
                if (WaFlg2 == 0)
                {
                    checkBoxWaFlag.Checked = false;
                }
                else if (WaFlg2 == 2)
                {
                    checkBoxWaFlag.Checked = true;
                }

                textBoxWaDetailID.Text = dataGridViewWarehousing.Rows[dataGridViewWarehousing.CurrentRow.Index].Cells[7].Value.ToString();
                textBoxPrID.Text = dataGridViewWarehousing.Rows[dataGridViewWarehousing.CurrentRow.Index].Cells[8].Value.ToString();
                textBoxWaQuantity.Text = dataGridViewWarehousing.Rows[dataGridViewWarehousing.CurrentRow.Index].Cells[9].Value.ToString();
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            // 8.2.2.1 妥当な受注データ取得
            if (!GetValidDataAtDelete())
                return;

            var delWarehousing = GenerateDataAtWarehousing();

            //8.2.2.3 受注情報更新
            DeleteWarehousing(delWarehousing);
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
        private bool GetValidDataAtDelete()
        {
            // 入庫IDの適否
            if (!String.IsNullOrEmpty(textBoxWaID.Text.Trim()))
            {
                // 入庫IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxWaID.Text.Trim()))
                {
                    //MessageBox.Show("入庫IDは全て半角数字入力です");
                    messageDsp.DspMsg("M17013");
                    textBoxWaID.Focus();
                    return false;
                }
                // 入庫IDの文字数チェック
                if (textBoxWaID.TextLength > 6)
                {
                    //MessageBox.Show("入庫IDは6文字です");
                    messageDsp.DspMsg("M17014");
                    textBoxWaID.Focus();
                    return false;
                }
                // 入庫IDの存在チェック
                if (!warehousingDataAccess.CheckWarehousingCDExistence(textBoxWaID.Text.Trim()))
                {
                    //MessageBox.Show("入力された入庫IDは存在しません");
                    messageDsp.DspMsg("M17015");
                    textBoxWaID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("入庫IDが入力されていません");
                messageDsp.DspMsg("M17063");
                textBoxWaID.Focus();
                return false;
            }

            // 発注IDの未入力チェック
            if (!String.IsNullOrEmpty(textBoxHaID.Text.Trim()))
            {
                // 発注IDの文字数チェック
                if (textBoxHaID.TextLength > 6)
                {
                    //MessageBox.Show("発注IDは6文字です");
                    messageDsp.DspMsg("M17002");
                    textBoxHaID.Focus();
                    return false;
                }
                // 発注IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxHaID.Text.Trim()))
                {
                    //MessageBox.Show("発注IDは全て半角数字入力です");
                    messageDsp.DspMsg("M17001");
                    textBoxHaID.Focus();
                    return false;
                }
                // 発注IDの存在チェック
                if (!warehousingDataAccess.CheckHattyuIDExistence(textBoxWaID.Text.Trim(), textBoxHaID.Text.Trim()))
                {
                    //MessageBox.Show("入庫IDに関連する発注IDが一致しません");
                    messageDsp.DspMsg("M17053");
                    textBoxHaID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("発注IDが入力されていません");
                messageDsp.DspMsg("M17062");
                textBoxHaID.Focus();
                return false;
            }

            // 管理フラグの適否
            if (checkBoxWaFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("管理フラグが不確定の状態です");
                messageDsp.DspMsg("M17028");
                checkBoxWaFlag.Focus();
                return false;
            }

            // 管理フラグの適否２
            if (checkBoxWaFlag.Checked == false)
            {
                //MessageBox.Show("管理フラグが未チェックです");
                messageDsp.DspMsg("M17029");
                checkBoxWaFlag.Focus();
                return false;
            }

            // 非表示理由の適否
            if (checkBoxWaFlag.Checked == true)
            {
                if (textBoxWaHidden.Text == "")
                {
                    //MessageBox.Show("非表示理由の入力が必要です");
                    messageDsp.DspMsg("M17030");
                    textBoxWaHidden.Focus();
                    return false;
                }
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
        private T_Warehousing GenerateDataAtWarehousing()
        {
            return new T_Warehousing
            {
                WaID = int.Parse(textBoxWaID.Text.Trim()),
                HaID = int.Parse(textBoxHaID.Text.Trim()),
                WaFlag = WaFlg,
                WaHidden = textBoxWaHidden.Text.Trim()
            };
        }

        private void DeleteWarehousing (T_Warehousing delWarehousing)
        {

            // 更新確認メッセージ
            DialogResult result = messageDsp.DspMsg("M17038");
            if (result == DialogResult.Cancel)
            {
                return;
            }

            // 役職情報の更新
            bool flg = warehousingDataAccess.DeleteWarehousingData(delWarehousing);
            if (flg == true)
                //MessageBox.Show("データを削除しました。");
                messageDsp.DspMsg("M17039");
            else
                //MessageBox.Show("データの削除に失敗しました。");
                messageDsp.DspMsg("M17040");

            textBoxWaID.Focus();

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
            NonMaster.FormHattyu.F_Hattyu.stflg = 1;
            NonMaster.FormHattyu.F_Hattyu.ActiveForm.Activate();
            NonMaster.FormWarehousing.F_WarehousingConfirm.stflg = 1;
            NonMaster.FormWarehousing.F_WarehousingConfirm.ActiveForm.Activate();
            Master.FormStock.F_Stock.stflg = 1;
            Master.FormStock.F_Stock.ActiveForm.Activate();
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

        private void ClearInput()
        {
            textBoxWaID.Text = "";
            textBoxHaID.Text = "";
            textBoxEmID.Text = "";
            dateTimePickerWaDate.Value = DateTime.Now;
            dateTimePickerWaDate.Checked = false;
            textBoxWaDetailID.Text = "";
            textBoxPrID.Text = "";
            textBoxWaQuantity.Text = "";
            textBoxWaHidden.Text = "";
            checkBoxWaShelfFlag.Checked = false;
            checkBoxWaFlag.Checked = false;
        }

        private void checkBoxWaShelfFlag_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxWaShelfFlag.Checked == true)
            {
                WaShelfFlg = 1;
                return;
            }
            else if (checkBoxWaShelfFlag.Checked == false)
            {
                WaShelfFlg = 0;
                return;
            }
        }

        private void checkBoxWaFlag_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxWaFlag.Checked == true)
            {
                WaFlg = 2;
                textBoxWaHidden.Enabled = true;
                return;
            }
            else if (checkBoxWaFlag.Checked == false)
            {
                WaFlg = 0;
                textBoxWaHidden.Enabled = false;
                textBoxWaHidden.Text = "";
                return;
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (frm2 != null)
                frm2.Close();
            this.Close();
        }

        private void buttonDetailClear_Click(object sender, EventArgs e)
        {
            // 受注詳細欄の入力エリアのクリア
            textBoxWaDetailID.Text = "";
            textBoxPrID.Text = "";
            textBoxWaQuantity.Text = "";

            // データグリッドビューの表示
            SetFormDataGridView();
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
                }
                catch
                {
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

        private void buttonLogout_Click(object sender, EventArgs e)
        {

        }

        private void label発注ID_MouseEnter(object sender, EventArgs e)
        {
            label発注ID.BackColor = Color.Aqua;
        }

        private void label発注ID_MouseLeave(object sender, EventArgs e)
        {
            label発注ID.BackColor = Color.Transparent;
        }

        private void label入庫確認社員ID_MouseEnter(object sender, EventArgs e)
        {
            label入庫確認社員ID.BackColor = Color.Aqua;
        }

        private void label入庫確認社員ID_MouseLeave(object sender, EventArgs e)
        {
            label入庫確認社員ID.BackColor = Color.Transparent;
        }

        private void label商品ID_MouseEnter(object sender, EventArgs e)
        {
            label商品ID.BackColor = Color.Aqua;
        }

        private void label商品ID_MouseLeave(object sender, EventArgs e)
        {
            label商品ID.BackColor = Color.Transparent;
        }

        private void F_Warehousing_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (frm2 != null)
                frm2.Close();
        }

        private void F_Warehousing_Activated(object sender, EventArgs e)
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

        private void textBoxHaID_TextChanged(object sender, EventArgs e)
        {
            if (textBoxHaID.Text != "")
            {
                var context = new SalesManagement_DevContext();
                try
                {
                    labelStateFlag.Text = "確定済"; //ラベルがUnknow状態で再度入力実行されたときに確定済に戻しておく
                    int mHaID = int.Parse(textBoxHaID.Text);
                    var mHattyu = context.T_Hattyus.Single(x => x.HaID == mHaID); //入力されたHaIDで一致する一件のレコードを探す
                    if (mHattyu.HaFlag == 2 && mHattyu.WaWarehouseFlag == 1) //確定と非表示両方表示
                    {
                        labelFlag.Visible = true;
                        labelStateFlag.Visible = true;
                    }
                    else
                    {
                        if (mHattyu.HaFlag == 2) //管理フラグを表示するか
                        {
                            labelFlag.Visible = true;
                        }
                        else
                        {
                            labelFlag.Visible = false;
                        }
                        if (mHattyu.WaWarehouseFlag == 1) //状態フラグを表示するか
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
    }
}
