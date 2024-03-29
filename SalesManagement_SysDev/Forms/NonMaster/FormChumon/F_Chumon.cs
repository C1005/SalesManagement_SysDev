﻿using System;
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
    public partial class F_Chumon : Form
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
        //管理フラグを数値型で入れるための変数
        int ChFlg;
        int ChStateFlg;
        internal static int stflg = 0;

        public F_Chumon()
        {
            InitializeComponent();
        }

        private void F_Chumon_Load(object sender, EventArgs e)
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
            // 役職データの取得
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
                textBoxPageSize.Text = "15";
            }
            if (textBoxPageNo.Text == "" || textBoxPageNo.Text == "0" || int.Parse(textBoxPageSize.Text) > 9)
            {
                textBoxPageNo.Text = "1";
            }
            int pageSize = int.Parse(textBoxPageSize.Text);
            int pageNo = int.Parse(textBoxPageNo.Text) - 1;
            filteredList = Chumon.Where(x => x.ChFlag != 2).ToList(); //OrFlagが2のレコードは排除する
            dataGridViewChumon.DataSource = filteredList.Skip(pageSize * pageNo).Take(pageSize).ToList();

            if (filteredList.Count == 0)
            {
                labelNoTable.Visible = true;
            }
            else
            {
                labelNoTable.Visible = false;
            }

            ////各列幅の指定
            //dataGridViewChumon.Columns[0].Width = 100;
            //dataGridViewChumon.Columns[1].Width = 100;
            //dataGridViewChumon.Columns[2].Width = 100;
            //dataGridViewChumon.Columns[3].Width = 100;
            //dataGridViewChumon.Columns[4].Width = 100;
            //dataGridViewChumon.Columns[5].Width = 130;
            //dataGridViewChumon.Columns[6].Width = 110;
            //dataGridViewChumon.Columns[7].Width = 110;
            //dataGridViewChumon.Columns[8].Width = 400;
            //dataGridViewChumon.Columns[9].Width = 100;
            //dataGridViewChumon.Columns[10].Width = 100;
            //dataGridViewChumon.Columns[11].Width = 70;

            // 自動サイズ調整を有効にする
            dataGridViewChumon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //各列の文字位置の指定
            dataGridViewChumon.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewChumon.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewChumon.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewChumon.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewChumon.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewChumon.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewChumon.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewChumon.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewChumon.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewChumon.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewChumon.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewChumon.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //dataGridViewの総ページ数
            labelPage.Text = "/" + ((int)Math.Ceiling(filteredList.Count / (double)pageSize)) + "ページ";

            dataGridViewChumon.Refresh();
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
                textBoxPageSize.Text = "15";
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
                textBoxPageSize.Text = "15";
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

        private void buttonConfirmForm_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
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
                case "注文確定画面へ": //ボタンのテキスト名
                    frm = new F_ChumonConfirm(); //フォームの名前
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

        private void dataGridViewChumon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewChumon.CurrentRow != null)
            {
                //クリックされた行データをテキストボックスへ
                textBoxChID.Text = dataGridViewChumon.Rows[dataGridViewChumon.CurrentRow.Index].Cells[0].Value.ToString();
                textBoxSoID.Text = dataGridViewChumon.Rows[dataGridViewChumon.CurrentRow.Index].Cells[1].Value.ToString();
                textBoxEmID.Text = dataGridViewChumon.Rows[dataGridViewChumon.CurrentRow.Index].Cells[2].Value.ToString();
                textBoxClID.Text = dataGridViewChumon.Rows[dataGridViewChumon.CurrentRow.Index].Cells[3].Value.ToString();
                textBoxOrID.Text = dataGridViewChumon.Rows[dataGridViewChumon.CurrentRow.Index].Cells[4].Value.ToString();

                //日付が設定されていない場合、初期値として現在の日付を設定
                if (dataGridViewChumon.Rows[dataGridViewChumon.CurrentRow.Index].Cells[5].Value == null)
                {
                    dateTimePickerChDate.Value = DateTime.Now;
                    dateTimePickerChDate.Checked = false;
                }
                else
                {
                    dateTimePickerChDate.Text = dataGridViewChumon.Rows[dataGridViewChumon.CurrentRow.Index].Cells[5].Value.ToString();
                }

                //状態フラグの数値型をbool型に変換して取得
                int ChStateFlg2 = int.Parse(dataGridViewChumon.Rows[dataGridViewChumon.CurrentRow.Index].Cells[6].Value.ToString());
                if (ChStateFlg2 == 0)
                {
                    checkBoxChStateFlag.Checked = false;
                }
                else
                {
                    checkBoxChStateFlag.Checked = true;
                }
                //管理フラグの数値型をbool型に変換して取得
                int ChFlg2 = int.Parse(dataGridViewChumon.Rows[dataGridViewChumon.CurrentRow.Index].Cells[7].Value.ToString());
                if (ChFlg2 == 0)
                {
                    checkBoxChFlag.Checked = false;
                }
                else if (ChFlg2 == 2)
                {
                    checkBoxChFlag.Checked = true;
                }

                textBoxChHidden.Text = dataGridViewChumon.Rows[dataGridViewChumon.CurrentRow.Index].Cells[8].Value.ToString();
                textBoxChDetailID.Text = dataGridViewChumon.Rows[dataGridViewChumon.CurrentRow.Index].Cells[9].Value.ToString();
                textBoxPrID.Text = dataGridViewChumon.Rows[dataGridViewChumon.CurrentRow.Index].Cells[10].Value.ToString();
                textBoxChQuantity.Text = dataGridViewChumon.Rows[dataGridViewChumon.CurrentRow.Index].Cells[11].Value.ToString();
            }
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

            // 営業所IDの適否
            if (!String.IsNullOrEmpty(textBoxSoID.Text.Trim()))
            {
                // 営業所IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxSoID.Text.Trim()))
                {
                    //MessageBox.Show("営業所IDは全て半角数字入力です");
                    messageDsp.DspMsg("M12009");
                    textBoxSoID.Focus();
                    return false;
                }
                // 営業所IDの文字数チェック
                if (textBoxSoID.TextLength > 2)
                {
                    //MessageBox.Show("営業所IDは2文字です");
                    messageDsp.DspMsg("M12010");
                    textBoxSoID.Focus();
                    return false;
                }
            }

            // 社員IDの適否
            if (!String.IsNullOrEmpty(textBoxEmID.Text.Trim()))
            {
                //// 社員IDが0ではないかチェック
                //if (int.Parse(textBoxEmID.Text.Trim()) == 0)
                //{
                //    //MessageBox.Show("社員IDは01から割り当ててください");
                //    messageDsp.DspMsg("M10007");
                //    textBoxEmID.Focus();
                //    return false;
                //}

                /// ↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑
                /// 仕様書で社員IDがNULLを許容する管理は実行しない
                /// ただし社員IDが必須入力の場合はCtrl + K + U でコメント解除してチェックさせてください

                // 社員IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxEmID.Text.Trim()))
                {
                    //MessageBox.Show("社員IDは全て半角数字入力です");
                    messageDsp.DspMsg("M12005");
                    textBoxEmID.Focus();
                    return false;
                }
                // 社員IDの文字数チェック
                if (textBoxEmID.TextLength > 6)
                {
                    //MessageBox.Show("社員IDは6文字です");
                    messageDsp.DspMsg("M12006");
                    textBoxEmID.Focus();
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
                // 受注IDの文字数チェック
                if (textBoxOrID.TextLength > 6)
                {
                    //MessageBox.Show("受注IDは6文字です");
                    messageDsp.DspMsg("M12065");
                    textBoxOrID.Focus();
                    return false;
                }
            }

            // 注文詳細IDの未入力チェック
            if (!String.IsNullOrEmpty(textBoxChDetailID.Text.Trim()))
            {
                // 注文詳細IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxChDetailID.Text.Trim()))
                {
                    //MessageBox.Show("注文詳細IDは全て半角数字入力です");
                    messageDsp.DspMsg("M12055");
                    textBoxChDetailID.Focus();
                    return false;
                }
            }

            // 商品IDの適否
            if (!String.IsNullOrEmpty(textBoxPrID.Text.Trim()))
            {
                // 商品IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxPrID.Text.Trim()))
                {
                    //MessageBox.Show("商品IDは全て半角数字入力です");
                    messageDsp.DspMsg("M12016");
                    textBoxPrID.Focus();
                    return false;
                }
                // 商品IDの文字数チェック
                if (textBoxPrID.TextLength > 6)
                {
                    //MessageBox.Show("商品IDは6文字です");
                    messageDsp.DspMsg("M12017");
                    textBoxPrID.Focus();
                    return false;
                }
            }

            if (dateTimePickerChDate.Checked == true)
            {
                //MessageBox.Show("注文年月日は検索対象外です");
                messageDsp.DspMsg("M12066");
                dateTimePickerChDate.Focus();
                return false;
            }
            if (textBoxChQuantity.Text != "")
            {
                //MessageBox.Show("数量は検索対象外です");
                messageDsp.DspMsg("M12067");
                textBoxChQuantity.Focus();
                return false;
            }

            // 状態フラグの適否
            if (checkBoxChStateFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("状態フラグが不確定の状態です");
                messageDsp.DspMsg("M12027");
                checkBoxChStateFlag.Focus();
                return false;
            }

            // 管理フラグの適否
            if (checkBoxChFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("管理フラグが不確定の状態です");
                messageDsp.DspMsg("M12028");
                checkBoxChFlag.Focus();
                return false;
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
        public static string mChID;
        public static string mSoID;
        public static string mEmID;
        public static string mClID;
        public static string mOrID;
        public static int? mChStateFlg;
        public static int mChFlg;
        public static string mChDetailID;
        public static string mPrID;
        private void GenerateDataAtSelect()
        {
            T_ChumonDsp selectCondition;

            //boolからintに変換して検索条件セット準備
            if (checkBoxChStateFlag.Checked == true)
            {
                mChStateFlg = 1;
            }
            else if (checkBoxChStateFlag.Checked == false)
            {
                mChStateFlg = null;
            }
            if (checkBoxChFlag.Checked == true)
            {
                mChFlg = 2;
            }
            else if (checkBoxChFlag.Checked == false)
            {
                mChFlg = 0;
            }
            mChID = textBoxChID.Text.Trim();
            mSoID = textBoxSoID.Text.Trim();
            mEmID = textBoxEmID.Text.Trim();
            mClID = textBoxClID.Text.Trim();
            mOrID = textBoxOrID.Text.Trim();
            mChDetailID = textBoxChDetailID.Text.Trim();
            mPrID = textBoxPrID.Text.Trim();

            if (mChID != "")
            {
                // 検索条件のセット
                selectCondition = new T_ChumonDsp()
                {
                    ChID = int.Parse(textBoxChID.Text.Trim()),
                    ChStateFlag = mChStateFlg,
                    ChFlag = mChFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Chumon = chumonDataAccess.SearchChumonData(selectCondition);
                return;
            }
            else if (mChDetailID != "")
            {
                // 検索条件のセット
                selectCondition = new T_ChumonDsp()
                {
                    ChDetailID = int.Parse(textBoxChDetailID.Text.Trim()),
                    ChStateFlag = mChStateFlg,
                    ChFlag = mChFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Chumon = chumonDataAccess.SearchChumonData(selectCondition);
                return;
            }
            else if (mSoID != "")
            {
                // 検索条件のセット
                selectCondition = new T_ChumonDsp()
                {
                    SoID = int.Parse(textBoxSoID.Text.Trim()),
                    ChStateFlag = mChStateFlg,
                    ChFlag = mChFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Chumon = chumonDataAccess.SearchChumonData(selectCondition);
                return;
            }
            else if (mEmID != "")
            {
                // 検索条件のセット
                selectCondition = new T_ChumonDsp()
                {
                    EmID = int.Parse(textBoxEmID.Text.Trim()),
                    ChStateFlag = mChStateFlg,
                    ChFlag = mChFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Chumon = chumonDataAccess.SearchChumonData(selectCondition);
                return;
            }
            else if (mClID != "")
            {
                // 検索条件のセット
                selectCondition = new T_ChumonDsp()
                {
                    ClID = int.Parse(textBoxClID.Text.Trim()),
                    ChStateFlag = mChStateFlg,
                    ChFlag = mChFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Chumon = chumonDataAccess.SearchChumonData(selectCondition);
                return;
            }
            else if (mOrID != "")
            {
                // 検索条件のセット
                selectCondition = new T_ChumonDsp()
                {
                    OrID = int.Parse(textBoxOrID.Text.Trim()),
                    ChStateFlag = mChStateFlg,
                    ChFlag = mChFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Chumon = chumonDataAccess.SearchChumonData(selectCondition);
                return;
            }
            else if (mChStateFlg == 1)
            {
                // 検索条件のセット
                selectCondition = new T_ChumonDsp()
                {
                    ChStateFlag = mChStateFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Chumon = chumonDataAccess.SearchChumonData(selectCondition);
                return;
            }
            else if (mChFlg == 2)
            {
                // 検索条件のセット
                selectCondition = new T_ChumonDsp()
                {
                    ChFlag = mChFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Chumon = chumonDataAccess.SearchChumonData(selectCondition);
                return;
            }
            else if (mPrID != "")
            {
                // 検索条件のセット
                selectCondition = new T_ChumonDsp()
                {
                    PrID = int.Parse(textBoxPrID.Text.Trim()),
                    ChStateFlag = mChStateFlg,
                    ChFlag = mChFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Chumon = chumonDataAccess.SearchChumonData(selectCondition);
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

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            // 8.2.2.1 妥当な受注データ取得
            if (!GetValidDataAtDelete())
                return;

            var delChumon = GenerateDataAtChumon();

            //8.2.2.3 受注情報更新
            DeleteChumon(delChumon);
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
                // 注文IDの文字数チェック
                if (textBoxChID.TextLength > 6)
                {
                    //MessageBox.Show("注文IDは6文字です");
                    messageDsp.DspMsg("M12062");
                    textBoxChID.Focus();
                    return false;
                }
                // 注文IDの存在チェック
                if (!chumonDataAccess.CheckChumonCDExistence(textBoxChID.Text.Trim()))
                {
                    //MessageBox.Show("入力された注文IDは存在しません");
                    messageDsp.DspMsg("M12050");
                    textBoxChID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("注文IDが入力されていません");
                messageDsp.DspMsg("M12051");
                textBoxChID.Focus();
                return false;
            }

            // 受注IDの未入力チェック
            if (!String.IsNullOrEmpty(textBoxOrID.Text.Trim()))
            {
                // 受注IDの文字数チェック
                if (textBoxOrID.TextLength > 6)
                {
                    //MessageBox.Show("受注IDは6文字です");
                    messageDsp.DspMsg("M12065");
                    textBoxOrID.Focus();
                    return false;
                }
                // 受注IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxOrID.Text.Trim()))
                {
                    //MessageBox.Show("受注IDは全て半角数字入力です");
                    messageDsp.DspMsg("M12064");
                    textBoxOrID.Focus();
                    return false;
                }
                // 受注IDの存在チェック
                if (!chumonDataAccess.CheckOrderIDExistence(textBoxChID.Text.Trim(), textBoxOrID.Text.Trim()))
                {
                    //MessageBox.Show("受注IDに関連する受注IDが一致しません");
                    messageDsp.DspMsg("M12069");
                    textBoxOrID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("受注IDが入力されていません");
                messageDsp.DspMsg("M12070");
                textBoxOrID.Focus();
                return false;
            }

            // 管理フラグの適否
            if (checkBoxChFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("管理フラグが不確定の状態です");
                messageDsp.DspMsg("M12028");
                checkBoxChFlag.Focus();
                return false;
            }

            // 管理フラグの適否２
            if (checkBoxChFlag.Checked == false)
            {
                //MessageBox.Show("管理フラグが未チェックです");
                messageDsp.DspMsg("M12071");
                checkBoxChFlag.Focus();
                return false;
            }

            // 非表示理由の適否
            if (checkBoxChFlag.Checked == true)
            {
                if (textBoxChHidden.Text == "")
                {
                    //MessageBox.Show("非表示理由の入力が必要です");
                    messageDsp.DspMsg("M12030");
                    textBoxChHidden.Focus();
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
        private T_Chumon GenerateDataAtChumon()
        {
            return new T_Chumon
            {
                ChID = int.Parse(textBoxChID.Text.Trim()),
                OrID = int.Parse(textBoxOrID.Text.Trim()),
                ChFlag = ChFlg,
                ChHidden = textBoxChHidden.Text.Trim()
            };
        }

        private void DeleteChumon(T_Chumon delChumon)
        {

            // 更新確認メッセージ
            DialogResult result = messageDsp.DspMsg("M12047");
            if (result == DialogResult.Cancel)
            {
                return;
            }

            // 役職情報の更新
            bool flg = chumonDataAccess.DeleteChumonData(delChumon);
            if (flg == true)
                //MessageBox.Show("データを削除しました。");
                messageDsp.DspMsg("M12048");
            else
                //MessageBox.Show("データの削除に失敗しました。");
                messageDsp.DspMsg("M12049");

            textBoxChID.Focus();

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
            NonMaster.FormChumon.F_ChumonConfirm.stflg = 1;
            NonMaster.FormChumon.F_ChumonConfirm.ActiveForm.Activate();
            NonMaster.FormSyukko.F_Syukko.stflg = 1;
            NonMaster.FormSyukko.F_Syukko.ActiveForm.Activate();
            NonMaster.FormArrival.F_Arrival.stflg = 1;
            NonMaster.FormArrival.F_Arrival.ActiveForm.Activate();
            NonMaster.FormShipment.F_Shipment.stflg = 1;
            NonMaster.FormShipment.F_Shipment.ActiveForm.Activate();
            NonMaster.FormSale.F_Sale.stflg = 1;
            NonMaster.FormSale.F_Sale.ActiveForm.Activate();
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
            textBoxChID.Text = "";
            textBoxSoID.Text = "";
            textBoxEmID.Text = "";
            textBoxClID.Text = "";
            textBoxOrID.Text = "";
            dateTimePickerChDate.Value = DateTime.Now;
            dateTimePickerChDate.Checked = false;
            textBoxChDetailID.Text = "";
            textBoxPrID.Text = "";
            textBoxChQuantity.Text = "";
            textBoxChHidden.Text = "";
            checkBoxChStateFlag.Checked = false;
            checkBoxChFlag.Checked = false;
        }

        private void checkBoxChStateFlag_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxChStateFlag.Checked == true)
            {
                ChStateFlg = 1;
                return;
            }
            else if (checkBoxChStateFlag.Checked == false)
            {
                ChStateFlg = 0;
                return;
            }
        }

        private void checkBoxChFlag_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxChFlag.Checked == true)
            {
                ChFlg = 2;
                textBoxChHidden.Enabled = true;
                return;
            }
            else if (checkBoxChFlag.Checked == false)
            {
                ChFlg = 0;
                textBoxChHidden.Enabled = false;
                textBoxChHidden.Text = "";
                return;
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (frm2 != null)
                frm2.Close();
            this.Close();
        }

        //関連するIDを表示(受注IDは複雑)
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
                    textBoxSoID.Text = "";
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

        private void F_Chumon_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (frm2 != null)
                frm2.Close();
        }

        private void F_Chumon_Activated(object sender, EventArgs e)
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
                Master.FormStock.F_Stock.stflg = 1;
                NonMaster.FormSyukko.F_Syukko.stflg = 1;
            }
        }
    }
}
