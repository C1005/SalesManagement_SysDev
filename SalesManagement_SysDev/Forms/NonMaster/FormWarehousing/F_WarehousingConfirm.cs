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
    public partial class F_WarehousingConfirm : Form
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
        //アクティブ用の変数
        internal static int stflg = 0;

        public F_WarehousingConfirm()
        {
            InitializeComponent();
        }

        private void F_WarehousingConfirm_Load(object sender, EventArgs e)
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
            textBoxPageSize.Text = "10";
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
                textBoxPageSize.Text = "10";
            }
            if (textBoxPageNo.Text == "" || textBoxPageNo.Text == "0" || int.Parse(textBoxPageSize.Text) > 9)
            {
                textBoxPageNo.Text = "1";
            }
            int pageSize = int.Parse(textBoxPageSize.Text);
            int pageNo = int.Parse(textBoxPageNo.Text) - 1;
            filteredList = Warehousing.Where(x => x.WaFlag != 2).ToList(); //OrFlagが2のレコードは排除する
            filteredList = filteredList.Where(x => x.WaShelfFlag != 1).ToList(); //確定済みのレコードは表示しない
            dataGridViewWarehousing.DataSource = filteredList.Skip(pageSize * pageNo).Take(pageSize).ToList();

            if (filteredList.Count == 0)
            {
                labelNoTable.Visible = true;
            }
            else
            {
                labelNoTable.Visible = false;
            }

            dataGridViewWarehousing.Columns[5].Visible = false;
            dataGridViewWarehousing.Columns[6].Visible = false;

            //各列幅の指定
            dataGridViewWarehousing.Columns[0].Width = 63;
            dataGridViewWarehousing.Columns[1].Width = 63;
            dataGridViewWarehousing.Columns[2].Width = 111;
            dataGridViewWarehousing.Columns[3].Width = 88;
            dataGridViewWarehousing.Columns[4].Width = 109;
            //dataGridViewWarehousing.Columns[5].Width = 400;
            //dataGridViewWarehousing.Columns[6].Width = 110;
            dataGridViewWarehousing.Columns[7].Width = 88;
            dataGridViewWarehousing.Columns[8].Width = 63;
            dataGridViewWarehousing.Columns[9].Width = 52;

            //各列の文字位置の指定
            dataGridViewWarehousing.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewWarehousing.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewWarehousing.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewWarehousing.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewWarehousing.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridViewWarehousing.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //dataGridViewWarehousing.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewWarehousing.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewWarehousing.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewWarehousing.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //dataGridViewの総ページ数
            labelPage.Text = "/" + ((int)Math.Ceiling(filteredList.Count / (double)pageSize)) + "ページ";

            dataGridViewWarehousing.Refresh();

            // 確定可能なデータがない場合
            if (filteredList.Count == 0)
            {
                MessageBox.Show("確定可能なデータはありません", "データなし", 0);
                NonMaster.FormWarehousing.F_Warehousing.stflg = 1;
                this.Close();
                return;
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
                textBoxPageSize.Text = "10";
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
                textBoxPageSize.Text = "10";
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
            }
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
            dateTimePickerWaDate.Value = DateTime.Now;
            dateTimePickerWaDate.Checked = false;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonList_Click(object sender, EventArgs e)
        {
            // データグリッドビューの表示
            SetFormDataGridView();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            // 8.2.4.1 妥当な入庫データ取得
            if (!GetValidDataAtSelect())
                return;

            // 8.2.4.2 入庫情報抽出
            GenerateDataAtSelect();

            // 8.2.4.3 入庫抽出結果表示
            SetSelectData();
        }

        ///////////////////////////////
        //　8.2.4.1 妥当な入庫データ取得
        //メソッド名：GetValidDataAtSlect()
        //引　数   ：なし
        //戻り値   ：true or false
        //機　能   ：入力データの形式チェック
        //          ：エラーがない場合True
        //          ：エラーがある場合False
        ///////////////////////////////
        private bool GetValidDataAtSelect()
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
            }

            // 発注IDの適否
            if (!String.IsNullOrEmpty(textBoxHaID.Text.Trim()))
            {
                // 発注IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxHaID.Text.Trim()))
                {
                    //MessageBox.Show("発注IDは全て半角数字入力です");
                    messageDsp.DspMsg("M17054");
                    textBoxHaID.Focus();
                    return false;
                }
            }
            return true;
        }

        ///////////////////////////////
        //　8.2.4.2 入庫情報抽出
        //メソッド名：GenerateDataAtSelect()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：入庫情報の取得
        ///////////////////////////////

        //cSoIDを別クラス(SalesOfficeDataAccess)でも使用できるように定義
        public static string mWaID;
        public static string mHaID;
        public static bool mDate;
        private void GenerateDataAtSelect()
        {
            T_WarehousingDsp selectCondition;

            mWaID = textBoxWaID.Text.Trim();
            mHaID = textBoxHaID.Text.Trim();
            mDate = dateTimePickerWaDate.Checked;

            if (mWaID != "")
            {
                // 検索条件のセット
                selectCondition = new T_WarehousingDsp()
                {
                    WaID = int.Parse(textBoxWaID.Text.Trim())
                };
                // データの抽出
                Warehousing = warehousingDataAccess.SearchWarehousingConfirm(selectCondition);
                return;
            }
            else if (mHaID != "")
            {
                // 検索条件のセット
                selectCondition = new T_WarehousingDsp()
                {
                    HaID = int.Parse(textBoxHaID.Text.Trim())
                };
                // データの抽出
                Warehousing = warehousingDataAccess.SearchWarehousingConfirm(selectCondition);
                return;
            }
            else if (mDate != false)
            {
                // 検索条件のセット
                selectCondition = new T_WarehousingDsp()
                {
                    //時刻は含めない(Dateのみ取得)
                    WaDate = dateTimePickerWaDate.Value.Date
                };
                // データの抽出
                Warehousing = warehousingDataAccess.SearchWarehousingConfirm(selectCondition);
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

            dataGridViewWarehousing.DataSource = Warehousing;
            if (Warehousing.Count == 0)
            {
                labelNoTable.Visible = true;
            }
            else
            {
                labelNoTable.Visible = false;
            }

            labelPage.Text = "/" + ((int)Math.Ceiling(Warehousing.Count / (double)pageSize)) + "ページ";
            dataGridViewWarehousing.Refresh();

            if (Warehousing.Count == 0) //検索結果のデータ数が0ならエラー
            {
                messageDsp.DspMsg("M17037");
                SetFormDataGridView();
            }
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            // 8.2.2.1 妥当な受注データ取得
            if (!GetValidDataAtConfirm())
                return;

            var conWarehousing = GenerateDataAtWarehousing();

            //8.2.2.3 受注情報更新
            ConfirmWarehousing(conWarehousing);
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

            // 状態フラグの重複チェック
            if (warehousingDataAccess.CheckStateFlagExistence(textBoxWaID.Text.Trim()))
            {
                //MessageBox.Show("入力された入庫IDは既に確定済みです");
                messageDsp.DspMsg("M17061");
                textBoxWaID.Focus();
                return false;
            }

            // 管理フラグのチェック状態
            if (warehousingDataAccess.CheckFlagExistence(textBoxWaID.Text.Trim()))
            {
                //MessageBox.Show("入力された入庫IDは非表示中のため確定できません");
                messageDsp.DspMsg("M17064");
                textBoxWaID.Focus();
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
        private T_Warehousing GenerateDataAtWarehousing()
        {
            return new T_Warehousing
            {
                WaID = int.Parse(textBoxWaID.Text.Trim()),
                WaShelfFlag = 1
            };
        }

        private void ConfirmWarehousing(T_Warehousing conWarehousing)
        {
            // 更新確認メッセージ
            DialogResult result = messageDsp.DspMsg("M17058");
            if (result == DialogResult.Cancel)
                return;

            // 役職情報の更新
            bool flg = warehousingDataAccess.ConfirmWarehousingData(conWarehousing);
            if (flg == true)
                //MessageBox.Show("データを確定しました。");
                messageDsp.DspMsg("M17059");
            else
                //MessageBox.Show("データの確定に失敗しました。");
                messageDsp.DspMsg("M17060");

            textBoxWaID.Focus();

            // 入力エリアのクリア
            ClearInput();

            // データグリッドビューの表示
            GetDataGridView();

            if (System.Windows.Forms.Form.ActiveForm != null) //nullエラー防止(特定の操作を行うと何故かnullが返り、例外エラーが出る
            {
                F_Warehousing.stflg = 1;
                F_Warehousing.ActiveForm.Activate();
                Master.FormStock.F_Stock.stflg = 1;
                Master.FormStock.F_Stock.ActiveForm.Activate();
            }
        }

        private void label発注ID_Click(object sender, EventArgs e)
        {
            OpenForm(((Label)sender).Text);
        }

        private void OpenForm(string formName)
        {
            Form frm = new Form();
            //引数より、開くフォームを設定
            switch (formName)
            {
                case "発注ID": //ボタンのテキスト名
                    frm = new NonMaster.FormHattyu.F_Hattyu(); //フォームの名前
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

        private void label発注ID_MouseEnter(object sender, EventArgs e)
        {
            label発注ID.BackColor = Color.Aqua;
        }

        private void label発注ID_MouseLeave(object sender, EventArgs e)
        {
            label発注ID.BackColor = Color.Transparent;
        }

        private void F_WarehousingConfirm_Activated(object sender, EventArgs e)
        {
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
