using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagement_SysDev.Forms.NonMaster.FormHattyu
{
    public partial class F_HattyuUpdate : Form
    {
        //メッセージ表示用クラスのインスタンス化
        MessageDsp messageDsp = new MessageDsp();
        //データベース受注テーブルアクセス用クラスのインスタンス化
        DbAccess.HattyuDataAccess hattyuDataAccess = new DbAccess.HattyuDataAccess();
        //入力形式チェック用クラスのインスタンス化
        DataInputFormCheck dataInputFormCheck = new DataInputFormCheck();
        //データグリッドビュー用の受注データ
        private static List<T_HattyuDsp> Hattyu;
        private static List<T_HattyuDsp> filteredList;
        //アクティブ用の変数
        internal static int stflg = 0;

        public F_HattyuUpdate()
        {
            InitializeComponent();
        }

        private void F_HattyuUpdate_Load(object sender, EventArgs e)
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
            dataGridViewHattyu.ReadOnly = true;
            //行内をクリックすることで行を選択
            dataGridViewHattyu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //ヘッダー位置の指定
            dataGridViewHattyu.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

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
            // 受注データの取得(通常)
            Hattyu = hattyuDataAccess.GetHattyuData();

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

            filteredList = Hattyu.Where(x => x.HaFlag != 2).ToList(); //HaFlagが2のレコードは排除する
            filteredList = filteredList.Where(x => x.WaWarehouseFlag != 1).ToList(); //確定済みのレコードは表示しない
            dataGridViewHattyu.DataSource = filteredList.Skip(pageSize * pageNo).Take(pageSize).ToList();

            if (filteredList.Count == 0)
            {
                labelNoTable.Visible = true;
            }
            else
            {
                labelNoTable.Visible = false;
            }

            dataGridViewHattyu.Columns[5].Visible = false;
            dataGridViewHattyu.Columns[6].Visible = false;

            //各列幅の指定
            dataGridViewHattyu.Columns[0].Width = 63;
            dataGridViewHattyu.Columns[1].Width = 66;
            dataGridViewHattyu.Columns[2].Width = 88;
            dataGridViewHattyu.Columns[3].Width = 88;
            dataGridViewHattyu.Columns[4].Width = 126;
            //dataGridViewHattyu.Columns[5].Width = 110;
            //dataGridViewHattyu.Columns[6].Width = 400;
            dataGridViewHattyu.Columns[7].Width = 89;
            dataGridViewHattyu.Columns[8].Width = 63;
            dataGridViewHattyu.Columns[9].Width = 54;

            //各列の文字位置の指定
            dataGridViewHattyu.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewHattyu.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewHattyu.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewHattyu.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewHattyu.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridViewHattyu.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridViewHattyu.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewHattyu.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewHattyu.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewHattyu.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //dataGridViewの総ページ数
            labelPage.Text = "/" + ((int)Math.Ceiling(filteredList.Count / (double)pageSize)) + "ページ";

            dataGridViewHattyu.Refresh();

            // 確定可能なデータがない場合
            if (filteredList.Count == 0)
            {
                MessageBox.Show("確定可能なデータはありません", "データなし", 0);
                NonMaster.FormHattyu.F_Hattyu.stflg = 1;
                this.Close();
                return;
            }
        }

        private void dataGridViewHattyu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewHattyu.CurrentRow != null)
            {
                //クリックされた行データをテキストボックスへ
                textBoxHaID.Text = dataGridViewHattyu.Rows[dataGridViewHattyu.CurrentRow.Index].Cells[0].Value.ToString();
                textBoxMaID.Text = dataGridViewHattyu.Rows[dataGridViewHattyu.CurrentRow.Index].Cells[1].Value.ToString();

                //日付が設定されていない場合、初期値として現在の日付を設定
                if (dataGridViewHattyu.Rows[dataGridViewHattyu.CurrentRow.Index].Cells[3].Value == null)
                {
                    dateTimePickerHaDate.Value = DateTime.Now;
                    dateTimePickerHaDate.Checked = false;
                }
                else
                {
                    dateTimePickerHaDate.Text = dataGridViewHattyu.Rows[dataGridViewHattyu.CurrentRow.Index].Cells[3].Value.ToString();
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
            dataGridViewHattyu.DataSource = filteredList.Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewHattyu.Refresh();
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
            dataGridViewHattyu.DataSource = filteredList.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewHattyu.Refresh();
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
                dataGridViewHattyu.DataSource = filteredList.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewHattyu.Refresh();
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
            dataGridViewHattyu.DataSource = filteredList.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewHattyu.Refresh();
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

        ///////////////////////////////
        //メソッド名：ClearInput()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：入力エリアをクリア
        ///////////////////////////////
        private void ClearInput()
        {
            textBoxHaID.Text = "";
            textBoxMaID.Text = "";
            dateTimePickerHaDate.Value = DateTime.Now;
            dateTimePickerHaDate.Checked = false;
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
            // 発注IDの適否
            if (!String.IsNullOrEmpty(textBoxHaID.Text.Trim()))
            {
                // 発注IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxHaID.Text.Trim()))
                {
                    //MessageBox.Show("発注IDは全て半角数字入力です");
                    messageDsp.DspMsg("M13058");
                    textBoxHaID.Focus();
                    return false;
                }
            }

            // メーカIDの適否
            if (!String.IsNullOrEmpty(textBoxMaID.Text.Trim()))
            {
                // メーカIDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxMaID.Text.Trim()))
                {
                    //MessageBox.Show("メーカIDは全て半角数字入力です");
                    messageDsp.DspMsg("M13005");
                    textBoxMaID.Focus();
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
        public static string mHaID;
        public static string mMaID;
        public static bool mDate;
        private void GenerateDataAtSelect()
        {
            T_HattyuDsp selectCondition;

            mHaID = textBoxHaID.Text.Trim();
            mMaID = textBoxMaID.Text.Trim();
            mDate = dateTimePickerHaDate.Checked;

            if (mHaID != "")
            {
                // 検索条件のセット
                selectCondition = new T_HattyuDsp()
                {
                    HaID = int.Parse(textBoxHaID.Text.Trim())
                };
                // データの抽出
                Hattyu = hattyuDataAccess.SearchHattyuConfirm(selectCondition);
                return;
            }
            else if (mMaID != "")
            {
                // 検索条件のセット
                selectCondition = new T_HattyuDsp()
                {
                    MaID = int.Parse(textBoxMaID.Text.Trim())
                };
                // データの抽出
                Hattyu = hattyuDataAccess.SearchHattyuConfirm(selectCondition);
                return;
            }
            else if (mDate != false)
            {
                // 検索条件のセット
                selectCondition = new T_HattyuDsp()
                {
                    //時刻は含めない(Dateのみ取得)
                    HaDate = dateTimePickerHaDate.Value.Date
                };
                // データの抽出
                Hattyu = hattyuDataAccess.SearchHattyuConfirm(selectCondition);
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

            dataGridViewHattyu.DataSource = Hattyu;
            if (Hattyu.Count == 0)
            {
                labelNoTable.Visible = true;
            }
            else
            {
                labelNoTable.Visible = false;
            }

            labelPage.Text = "/" + ((int)Math.Ceiling(Hattyu.Count / (double)pageSize)) + "ページ";
            dataGridViewHattyu.Refresh();

            if (Hattyu.Count == 0) //検索結果のデータ数が0ならエラー
            {
                messageDsp.DspMsg("M13035");
                SetFormDataGridView();
            }
        }

        private void buttonUpdateConfirm_Click(object sender, EventArgs e)
        {
            // 8.2.2.1 妥当な受注データ取得
            if (!GetValidDataAtConfirm())
                return;

            var conHattyu = GenerateDataAtHattyu();

            //8.2.2.3 受注情報更新
            ConfirmHattyu(conHattyu);
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
            // 発注IDの適否
            if (!String.IsNullOrEmpty(textBoxHaID.Text.Trim()))
            {
                // 発注IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxHaID.Text.Trim()))
                {
                    //MessageBox.Show("発注IDは全て半角数字入力です");
                    messageDsp.DspMsg("M13058");
                    textBoxHaID.Focus();
                    return false;
                }
                // 発注IDの存在チェック
                if (!hattyuDataAccess.CheckHattyuCDExistence(textBoxHaID.Text.Trim()))
                {
                    //MessageBox.Show("入力された発注IDは存在しません");
                    messageDsp.DspMsg("13059");
                    textBoxHaID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("発注IDが入力されていません");
                messageDsp.DspMsg("M13060");
                textBoxHaID.Focus();
                return false;
            }

            // 状態フラグの重複チェック
            if (hattyuDataAccess.CheckStateFlagExistence(textBoxHaID.Text.Trim()))
            {
                //MessageBox.Show("入力された発注IDは既に確定済みです");
                messageDsp.DspMsg("M13061");
                textBoxHaID.Focus();
                return false;
            }

            // 管理フラグのチェック状態
            if (hattyuDataAccess.CheckFlagExistence(textBoxHaID.Text.Trim()))
            {
                //MessageBox.Show("入力された発注IDは非表示中のため確定できません");
                messageDsp.DspMsg("M13062");
                textBoxHaID.Focus();
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
        private T_Hattyu GenerateDataAtHattyu()
        {
            return new T_Hattyu
            {
                HaID = int.Parse(textBoxHaID.Text.Trim()),
                WaWarehouseFlag = 1
            };
        }

        private void ConfirmHattyu(T_Hattyu conHattyu)
        {
            // 更新確認メッセージ
            DialogResult result = messageDsp.DspMsg("M13047");
            if (result == DialogResult.Cancel)
                return;

            // 役職情報の更新
            bool flg = hattyuDataAccess.ConfirmHattyuData(conHattyu);
            if (flg == true)
                //MessageBox.Show("データを確定しました。");
                messageDsp.DspMsg("M13048");
            else
                //MessageBox.Show("データの確定に失敗しました。");
                messageDsp.DspMsg("M13049");

            textBoxHaID.Focus();

            // 入力エリアのクリア
            ClearInput();

            // データグリッドビューの表示
            GetDataGridView();

            if (System.Windows.Forms.Form.ActiveForm != null) //nullエラー防止(特定の操作を行うと何故かnullが返り、例外エラーが出る)
            {
                F_Hattyu.stflg = 1;
                F_Hattyu.ActiveForm.Activate();
                FormWarehousing.F_Warehousing.stflg = 1;
                FormWarehousing.F_Warehousing.ActiveForm.Activate();
            }
        }

        private void labelメーカID_Click(object sender, EventArgs e)
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
                    frm = new Master.FormProduct.F_Maker(); //フォームの名前
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

        private void labelメーカID_MouseEnter(object sender, EventArgs e)
        {
            labelメーカID.BackColor = Color.Aqua;
        }

        private void labelメーカID_MouseLeave(object sender, EventArgs e)
        {
            labelメーカID.BackColor = Color.Transparent;
        }

        private void F_HattyuUpdate_Activated(object sender, EventArgs e)
        {
            if (stflg == 1)
            {
                GetDataGridView();
                stflg = 0;
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
    }
}
