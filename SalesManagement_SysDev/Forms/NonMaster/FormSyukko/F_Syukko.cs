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

        public F_Syukko()
        {
            InitializeComponent();
        }

        private void buttonConfirmForm_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private void buttonCusSearch_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private void buttonProductSearch_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private void buttonEmSearch_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private void buttonOrderSearch_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private void OpenForm(string formName)
        {
            Form frm = new Form();
            //引数より、開くフォームを設定
            switch (formName)
            {
                case "出庫検索": //ボタンのテキスト名
                    frm = new Master.FormClient.F_Client(); //フォームの名前
                    break;
                case "商品検索": //ボタンのテキスト名
                    frm = new Master.FormProduct.F_Product(); //フォームの名前
                    break;
                case "社員検索": //ボタンのテキスト名
                    frm = new Master.FormEmployee.F_Employee(); //フォームの名前
                    break;
                case "受注検索": //ボタンのテキスト名
                    frm = new NonMaster.FormOrder.F_Order(); //フォームの名前
                    break;
                case "出庫確定画面へ": //ボタンのテキスト名
                    frm = new F_SyukkoConfirm(); //フォームの名前
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
            textBoxPageSize.Text = "12";
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
            int pageSize = int.Parse(textBoxPageSize.Text);
            int pageNo = int.Parse(textBoxPageNo.Text) - 1;
            filteredList = Syukko.Where(x => x.SyFlag != 2).ToList(); //OrFlagが2のレコードは排除する
            dataGridViewSyukko.DataSource = Syukko.Skip(pageSize * pageNo).Take(pageSize).ToList();
            //各列幅の指定
            dataGridViewSyukko.Columns[0].Width = 100;
            dataGridViewSyukko.Columns[1].Width = 100;
            dataGridViewSyukko.Columns[2].Width = 100;
            dataGridViewSyukko.Columns[3].Width = 100;
            dataGridViewSyukko.Columns[4].Width = 100;
            dataGridViewSyukko.Columns[5].Width = 130;
            dataGridViewSyukko.Columns[6].Width = 110;
            dataGridViewSyukko.Columns[7].Width = 110;
            dataGridViewSyukko.Columns[8].Width = 400;
            dataGridViewSyukko.Columns[9].Width = 100;
            dataGridViewSyukko.Columns[10].Width = 100;
            dataGridViewSyukko.Columns[11].Width = 70;
            //各列の文字位置の指定
            dataGridViewSyukko.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewSyukko.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewSyukko.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewSyukko.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewSyukko.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewSyukko.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewSyukko.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewSyukko.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewSyukko.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewSyukko.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewSyukko.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewSyukko.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //dataGridViewの総ページ数
            labelPage.Text = "/" + ((int)Math.Ceiling(Syukko.Count / (double)pageSize)) + "ページ";

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

        private void buttonLogout_Click(object sender, EventArgs e)
        {

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
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
                    messageDsp.DspMsg("M16001");
                    textBoxSyID.Focus();
                    return false;
                }
                // 出庫IDの文字数チェック
                if (textBoxSyID.TextLength > 2)
                {
                    //MessageBox.Show("出庫IDは2文字です");
                    messageDsp.DspMsg("M16002");
                    textBoxSyID.Focus();
                    return false;
                }
                // 出庫IDの存在チェック
                if (!syukkoDataAccess.CheckSyukkoCDExistence(textBoxSyID.Text.Trim()))
                {
                    //MessageBox.Show("入力された出庫IDは存在しません");
                    messageDsp.DspMsg("M16003");
                    textBoxSyID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("出庫IDが入力されていません");
                messageDsp.DspMsg("M16004");
                textBoxSyID.Focus();
                return false;
            }

            // 出庫詳細IDの未入力チェック
            if (!String.IsNullOrEmpty(textBoxSyDetailID.Text.Trim()))
            {
                // 出庫詳細IDの半角英数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxSyDetailID.Text.Trim()))
                {
                    //MessageBox.Show("出庫詳細IDは全て半角英数字入力です");
                    messageDsp.DspMsg("M16005");
                    textBoxSyDetailID.Focus();
                    return false;
                }
                // 出庫詳細IDの文字数チェック
                if (textBoxSyDetailID.TextLength > 2)
                {
                    //MessageBox.Show("出庫詳細IDは2文字です");
                    messageDsp.DspMsg("M16006");
                    textBoxSyDetailID.Focus();
                    return false;
                }
                // 出庫詳細IDの存在チェック
                if (!syukkoDataAccess.CheckSyukkoCDExistence(textBoxSyDetailID.Text.Trim()))
                {
                    //MessageBox.Show("入力された出庫詳細IDは存在しません");
                    messageDsp.DspMsg("M16007");
                    textBoxSyDetailID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("出庫詳細IDが入力されていません");
                messageDsp.DspMsg("M16008");
                textBoxSyDetailID.Focus();
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
                messageDsp.DspMsg("M10029");
                checkBoxSyFlag.Focus();
                return false;
            }

            // 非表示理由の適否
            if (checkBoxSyFlag.Checked == true)
            {
                if (textBoxSyHidden.Text == "")
                {
                    //MessageBox.Show("非表示理由の入力が必要です");
                    messageDsp.DspMsg("M16010");
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
            DialogResult result = messageDsp.DspMsg("M16011");
            if (result == DialogResult.Cancel)
                return;

            // 役職情報の更新
            bool flg = syukkoDataAccess.DeleteSyukkoData(delSyukko);
            if (flg == true)
                //MessageBox.Show("データを削除しました。");
                messageDsp.DspMsg("M16012");
            else
                //MessageBox.Show("データの削除に失敗しました。");
                messageDsp.DspMsg("M16013");

            textBoxSyID.Focus();
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

        private void buttonPageSizeChange_Click(object sender, EventArgs e)
        {
            SetDataGridView();
        }

        private void buttonFirstPage_Click(object sender, EventArgs e)
        {
            int pageSize = int.Parse(textBoxPageSize.Text);
            dataGridViewSyukko.DataSource = filteredList.Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewSyukko.Refresh();
            //ページ番号の設定
            textBoxPageNo.Text = "1";
        }

        private void buttonPreviousPage_Click(object sender, EventArgs e)
        {
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
                    int mOrID = int.Parse(textBoxOrID.Text);
                    var mOrder = context.T_Orders.Single(x => x.OrID == mOrID); //入力されたOrIDで一致する一件のレコードを探す
                    if (mOrder.OrFlag == 2)
                    {
                        labelFlag.Visible = true;
                    }
                    if (mOrder.OrStateFlag == 1)
                    {
                        labelStateFlag.Visible = true;
                    }
                    int mClID = mOrder.ClID;
                    int mEmID = mOrder.EmID;
                    int mSoID = mOrder.SoID;
                    textBoxClID.Text = mClID.ToString();
                    textBoxEmID.Text = mEmID.ToString();
                    textBoxSoID.Text = mSoID.ToString();
                    context.Dispose();
                }
                catch
                {
                    labelStateFlag.Visible = true;
                    labelStateFlag.Text = "“UnknownID”";
                    context.Dispose();
                    return;
                }
            }
            else
            {
                textBoxClID.Text = "";
                textBoxEmID.Text = "";
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
    }
}
