using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagement_SysDev.Forms.NonMaster.FormOrder
{
    public partial class F_Order : Form
    {
        //メッセージ表示用クラスのインスタンス化
        MessageDsp messageDsp = new MessageDsp();
        //データベース受注テーブルアクセス用クラスのインスタンス化
        DbAccess.OrderDataAccess orderDataAccess = new DbAccess.OrderDataAccess();
        //入力形式チェック用クラスのインスタンス化
        DataInputFormCheck dataInputFormCheck = new DataInputFormCheck();
        //データグリッドビュー用の受注データ
        private static List<T_OrderDsp> Order;
        private static List<Entity.T_OrderProvisionalDsp> OrderProvisional;
        private static List<T_OrderDsp> filteredList;
        //フラグを数値型で入れるための変数
        int OrStateFlg = 0;
        internal static int OrFlg = 0;
        internal static bool provisionalMode = false;
        internal static int stflg = 0;

        public F_Order()
        {
            InitializeComponent();
        }

        private void F_Order_Load(object sender, EventArgs e)
        {
            //ログイン名の表示
            //labelLoginName.Text = FormMenu.loginName;

            // 仮登録データの取得とデータの有無をカウント
            OrderProvisional = orderDataAccess.GetOrderProvisionalData();
            int Rowcount = OrderProvisional.Count();
            if(Rowcount != 0)
            {
                provisionalMode = true;
                MessageBox.Show("仮登録中のデータが残っています\n登録かキャンセルをして仮登録を終了してください", "仮登録確認", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxPrID.Focus();
                EnableChange();
            }

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
            dataGridViewOrder.ReadOnly = true;
            //行内をクリックすることで行を選択
            dataGridViewOrder.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //ヘッダー位置の指定
            dataGridViewOrder.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

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
            if(provisionalMode == true)
            {
                // 仮登録データの取得(注意：データは必ず空であること)
                OrderProvisional = orderDataAccess.GetOrderProvisionalData();
            }
            else
            {
                // 受注データの取得(通常)
                Order = orderDataAccess.GetOrderData();
            }

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
            if(provisionalMode == true)
            {
                dataGridViewOrder.DataSource = OrderProvisional.Skip(pageSize * pageNo).Take(pageSize).ToList();
            }
            else
            {
                filteredList = Order.Where(x => x.OrFlag != 2).ToList(); //OrFlagが2のレコードは排除する
                dataGridViewOrder.DataSource = filteredList.Skip(pageSize * pageNo).Take(pageSize).ToList();

                if (filteredList.Count == 0)
                {
                    labelNoTable.Visible = true;
                }
                else
                {
                    labelNoTable.Visible = false;
                }
            }

            ////各列幅の指定
            //dataGridViewOrder.Columns[0].Width = 100;
            //dataGridViewOrder.Columns[1].Width = 100;
            //dataGridViewOrder.Columns[2].Width = 100;
            //dataGridViewOrder.Columns[3].Width = 100;
            //dataGridViewOrder.Columns[4].Width = 200;
            //dataGridViewOrder.Columns[5].Width = 130;
            //dataGridViewOrder.Columns[6].Width = 110;
            //dataGridViewOrder.Columns[7].Width = 110;
            //dataGridViewOrder.Columns[8].Width = 400;
            //dataGridViewOrder.Columns[9].Width = 100;
            //dataGridViewOrder.Columns[10].Width = 100;
            //dataGridViewOrder.Columns[11].Width = 70;
            //dataGridViewOrder.Columns[12].Width = 90;

            // 自動サイズ調整を有効にする
            dataGridViewOrder.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //各列の文字位置の指定
            dataGridViewOrder.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewOrder.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewOrder.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewOrder.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewOrder.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewOrder.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewOrder.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewOrder.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewOrder.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewOrder.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewOrder.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewOrder.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewOrder.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //dataGridViewの総ページ数
            if (provisionalMode == true)
            {
                //仮登録
                labelPage.Text = "/" + ((int)Math.Ceiling(OrderProvisional.Count / (double)pageSize)) + "ページ";
            }
            else
            {
                labelPage.Text = "/" + ((int)Math.Ceiling(filteredList.Count / (double)pageSize)) + "ページ";
            }
            dataGridViewOrder.Refresh();
        }

        private void buttonConfirmForm_Click(object sender, EventArgs e)
        {
            if(provisionalMode == true)
            {
                MessageBox.Show("仮登録中は受注確定画面に移動することはできません", "ロック中", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
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
                case "受注確定画面へ": //ボタンのテキスト名
                    frm = new F_OrderConfirm(); //フォームの名前
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

        private void buttonRegist_Click(object sender, EventArgs e)
        {
            // 登録機能の制限
            if (!GetValidDataAtRegistration())
                return;

            //登録情報作成
            var regOrder = GenerateDataAtRegistration();
            var regOrderDetail = GenerateDataDetailAtRegistration();

            //登録情報を登録
            RegistrationOrder(regOrder, regOrderDetail);
        }

        private bool GetValidDataAtRegistration()
        {
            if(provisionalMode == false)
            {
                //MessageBox.Show("仮登録を行ってから登録してください");
                messageDsp.DspMsg("M10045");
                textBoxOrID.Focus();
                return false;
            }
            return true;
        }

        ///////////////////////////////
        //　8.2.1.2 受注情報作成
        //メソッド名：GenerateDataAtProvisional()
        //引　数   ：なし
        //戻り値   ：受注登録情報
        //機　能   ：登録データのセット
        ///////////////////////////////

        //仮登録の受注データ取得
        private List<T_Order> GenerateDataAtRegistration()
        {
            List<Entity.T_OrderProvisional> orderPr = new List<Entity.T_OrderProvisional>();
            List<T_Order> OrCopyTb = new List<T_Order>();

            try
            {
                var context = new SalesManagement_DevContext();
                // tbはIEnumerable型
                var tb = from t1 in context.T_OrderProvisionals

                         select new
                         {
                             t1.OrID,
                             t1.SoID,
                             t1.EmID,
                             t1.ClID,
                             t1.ClCharge,
                             t1.OrDate,
                             t1.OrStateFlag,
                             t1.OrFlag,
                             t1.OrHidden
                         };

                // IEnumerable型のデータをList型へ

                foreach (var p in tb)
                {
                    orderPr.Add(new Entity.T_OrderProvisional()
                    {
                        OrID = p.OrID,
                        SoID = p.SoID,
                        EmID = p.EmID,
                        ClID = p.ClID,
                        ClCharge = p.ClCharge,
                        OrDate = p.OrDate,
                        OrStateFlag = p.OrStateFlag,
                        OrFlag = p.OrFlag,
                        OrHidden = p.OrHidden
                    });
                }

                foreach (Entity.T_OrderProvisional p in orderPr)
                {
                    OrCopyTb.Add(new T_Order
                    {
                        OrID = p.OrID,
                        SoID = p.SoID,
                        EmID = p.EmID,
                        ClID = p.ClID,
                        ClCharge = p.ClCharge,
                        OrDate = p.OrDate,
                        OrStateFlag = p.OrStateFlag,
                        OrFlag = p.OrFlag,
                        OrHidden = p.OrHidden
                    });
                }
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return OrCopyTb;
        }

        //仮登録の受注詳細データ取得
        private List<T_OrderDetail> GenerateDataDetailAtRegistration()
        {
            List<Entity.T_OrderDetailProvisional> orderDetailPr = new List<Entity.T_OrderDetailProvisional>();
            List<T_OrderDetail> OrDetailCopyTb = new List<T_OrderDetail>();

            try
            {
                var context = new SalesManagement_DevContext();
                // tbはIEnumerable型
                var tbDetail = from t2 in context.T_OrderDetailProvisionals

                               select new
                               {
                                   t2.OrDetailID,
                                   t2.OrID,
                                   t2.PrID,
                                   t2.OrQuantity,
                                   t2.OrTotalPrice
                               };

                // IEnumerable型のデータをList型へ

                foreach (var p in tbDetail)
                {
                    orderDetailPr.Add(new Entity.T_OrderDetailProvisional()
                    {
                        OrDetailID = p.OrDetailID,
                        OrID = p.OrID,
                        PrID = p.PrID,
                        OrQuantity = p.OrQuantity,
                        OrTotalPrice = p.OrTotalPrice
                    });
                }

                foreach (Entity.T_OrderDetailProvisional p in orderDetailPr)
                {
                    OrDetailCopyTb.Add(new T_OrderDetail
                    {
                        OrDetailID = p.OrDetailID,
                        OrID = p.OrID,
                        PrID = p.PrID,
                        OrQuantity = p.OrQuantity,
                        OrTotalPrice = p.OrTotalPrice
                    });
                }

                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return OrDetailCopyTb;

        }
        ///////////////////////////////
        //　8.2.1.3 受注情報登録
        //メソッド名：RegistrationOrder()
        //引　数   ：受注情報
        //戻り値   ：なし
        //機　能   ：受注データの登録
        ///////////////////////////////
        private void RegistrationOrder(List<T_Order> regOrder, List<T_OrderDetail> regOrderDetail)
        {
            // 登録確認メッセージ
            DialogResult result = messageDsp.DspMsg("M10031");
            if (result == DialogResult.Cancel)
                return;

            // 受注情報の登録
            bool flg = orderDataAccess.AddOrderData(regOrder);
            if (flg == true)
            {
                flg = orderDataAccess.AddOrderDetailData(regOrderDetail);  //メインOrIDと詳細のOrIDを常に一致させるため、
                                                                                     //T_Order regOrderの連番割り当て済みメインOrIDを受注詳細にもっていく
                if (flg == true)
                {
                    //MessageBox.Show("データを登録しました。");
                    messageDsp.DspMsg("M10032");
                    provisionalMode = false; //グリッドビューを元に戻す

                    //仮登録中のみ詳細テーブルのみ登録するので不要な項目は非表示(仮登録終了後は元に戻す)
                    EnableChange();

                    //仮登録のデータを全て削除
                    GenerateDataAtRemove();
                    GenerateDataDetailAtRemove();
                }
                else
                {
                    //MessageBox.Show("データの登録に失敗しました。");
                    messageDsp.DspMsg("M10033");
                }
            }
            else
            {
                //MessageBox.Show("データの登録に失敗しました。");
                messageDsp.DspMsg("M10033");
            }

            textBoxOrID.Focus();

            // 入力エリアのクリア
            ClearInput();

            // データグリッドビューの表示
            GetDataGridView();
            lastpage();

        }

        private void buttonProvisionalCancel_Click(object sender, EventArgs e)
        {
            // 中止確認メッセージ
            DialogResult result = MessageBox.Show("仮登録を中止しますか？","中止確認",MessageBoxButtons.OKCancel,MessageBoxIcon.Exclamation);
            if (result == DialogResult.OK)
            {
                result = MessageBox.Show("キャンセルを行うと現在の仮登録データは失われます。\nよろしいですか？", "中止確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                if (result == DialogResult.OK)
                {
                    provisionalMode = false;
                    EnableChange();
                    textBoxOrID.Focus();
                    GenerateDataAtRemove();
                    GenerateDataDetailAtRemove();
                    SetFormDataGridView();
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }

        //受注の仮登録データ削除
        private void GenerateDataAtRemove()
        {
            try
            {
                var context = new SalesManagement_DevContext();
                // tbはIEnumerable型
                var OrRemoveTb = from t1 in context.T_OrderProvisionals select t1;

                context.T_OrderProvisionals.RemoveRange(OrRemoveTb);
                context.SaveChanges();
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //受注詳細の仮登録データ削除
        private void GenerateDataDetailAtRemove()
        {
            try
            {
                var context = new SalesManagement_DevContext();
                // tbはIEnumerable型
                var OrDetailRemoveTb = from t2 in context.T_OrderDetailProvisionals select t2;

                context.T_OrderDetailProvisionals.RemoveRange(OrDetailRemoveTb);
                context.SaveChanges();
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        int CopyOrID;
        private void buttonProvisional_Click(object sender, EventArgs e)
        {
            // 仮登録データ取得
            if (!GetValidDataAtProvisional())
                return;

            //仮登録情報作成
            var regOrderProvisional = GenerateDataAtProvisional();
            if(provisionalMode == false)
            {
                CopyOrID = regOrderProvisional.OrID;  //常にnullになってしまう受注詳細(OrID)を割り当て済み受注(OrID)で上書き
            }
            var regOrderDetailProvisional = GenerateDataDetailAtProvisional();

            //仮登録情報を登録
            ProvisionalOrder(regOrderProvisional, regOrderDetailProvisional);
        }

        ///////////////////////////////
        //　        妥当な受注データ取得
        //メソッド名：GetValidDataAtRegistration()
        //引　数   ：なし
        //戻り値   ：true or false
        //機　能   ：入力データの形式チェック
        //          ：エラーがない場合True
        //          ：エラーがある場合False
        ///////////////////////////////
        private bool GetValidDataAtProvisional()
        {
            if(provisionalMode == false)
            {
                // 顧客IDの適否
                if (!String.IsNullOrEmpty(textBoxClID.Text.Trim()))
                {
                    // 顧客IDに一致するレコードの存在チェック
                    if (labelClName.Text == "“UnknownID”")
                    {
                        //MessageBox.Show("入力された顧客IDは存在しません");
                        messageDsp.DspMsg("M10025");
                        textBoxClID.Focus();
                        return false;
                    }
                    // 顧客IDの半角数字チェック
                    if (!dataInputFormCheck.CheckNumeric(textBoxClID.Text.Trim()))
                    {
                        //MessageBox.Show("顧客IDは全て半角数字入力です");
                        messageDsp.DspMsg("M10001");
                        textBoxClID.Focus();
                        return false;
                    }
                    // 顧客IDの文字数チェック
                    if (textBoxClID.TextLength > 6)
                    {
                        //MessageBox.Show("顧客IDは6文字です");
                        messageDsp.DspMsg("M10002");
                        textBoxClID.Focus();
                        return false;
                    }
                    // 顧客IDが0ではないかチェック
                    if (int.Parse(textBoxClID.Text.Trim()) == 0)
                    {
                        //MessageBox.Show("顧客IDは01から割り当ててください");
                        messageDsp.DspMsg("M10003");
                        textBoxClID.Focus();
                        return false;
                    }
                }
                else
                {
                    //MessageBox.Show("顧客IDが入力されていません");
                    messageDsp.DspMsg("M10004");
                    textBoxClID.Focus();
                    return false;
                }

                // 社員IDの適否
                if (!String.IsNullOrEmpty(textBoxEmID.Text.Trim()))
                {
                    // 社員IDが0ではないかチェック
                    if (int.Parse(textBoxEmID.Text.Trim()) == 0)
                    {
                        //MessageBox.Show("社員IDは01から割り当ててください");
                        messageDsp.DspMsg("M10007");
                        textBoxEmID.Focus();
                        return false;
                    }

                    // 社員IDに一致するレコードの存在チェック
                    if (labelEmName.Text == "“UnknownID”")
                    {
                        //MessageBox.Show("入力された社員IDは存在しません");
                        messageDsp.DspMsg("M10041");
                        textBoxEmID.Focus();
                        return false;
                    }

                    // 社員IDの半角数字チェック
                    if (!dataInputFormCheck.CheckNumeric(textBoxEmID.Text.Trim()))
                    {
                        //MessageBox.Show("社員IDは全て半角数字入力です");
                        messageDsp.DspMsg("M10005");
                        textBoxEmID.Focus();
                        return false;
                    }
                    // 社員IDの文字数チェック
                    if (textBoxEmID.TextLength > 6)
                    {
                        //MessageBox.Show("社員IDは6文字です");
                        messageDsp.DspMsg("M10006");
                        textBoxEmID.Focus();
                        return false;
                    }
                }
                else
                {
                    //MessageBox.Show("社員IDが入力されていません");
                    messageDsp.DspMsg("M10008");
                    textBoxEmID.Focus();
                    return false;
                }


                // 営業所IDの適否
                if (!String.IsNullOrEmpty(textBoxSoID.Text.Trim()))
                {
                    // 営業所IDに一致するレコードの存在チェック
                    if (labelSoName.Text == "“UnknownID”")
                    {
                        //MessageBox.Show("入力された営業所IDは存在しません");
                        messageDsp.DspMsg("M10042");
                        textBoxSoID.Focus();
                        return false;
                    }
                    // 営業所IDの半角数字チェック
                    if (!dataInputFormCheck.CheckNumeric(textBoxSoID.Text.Trim()))
                    {
                        //MessageBox.Show("営業所IDは全て半角数字入力です");
                        messageDsp.DspMsg("M10009");
                        textBoxSoID.Focus();
                        return false;
                    }
                    // 営業所IDの文字数チェック
                    if (textBoxSoID.TextLength > 2)
                    {
                        //MessageBox.Show("営業所IDは2文字です");
                        messageDsp.DspMsg("M10010");
                        textBoxSoID.Focus();
                        return false;
                    }
                }
                else
                {
                    //MessageBox.Show("営業所IDが入力されていません");
                    messageDsp.DspMsg("M10012");
                    textBoxSoID.Focus();
                    return false;
                }

                // 顧客担当者名の適否
                if (!String.IsNullOrEmpty(textBoxClCharge.Text.Trim()))
                {
                    // 顧客担当者名の全角チェック
                    if (!dataInputFormCheck.CheckFullWidth(textBoxClCharge.Text.Trim()))
                    {
                        //MessageBox.Show("顧客担当者名は全て全角入力です");
                        messageDsp.DspMsg("M10013");
                        textBoxClCharge.Focus();
                        return false;
                    }
                    // 顧客担当者名の文字数チェック
                    if (textBoxClCharge.TextLength > 50)
                    {
                        //MessageBox.Show("顧客担当者名は50文字以下です");
                        messageDsp.DspMsg("M10014");
                        textBoxClCharge.Focus();
                        return false;
                    }
                }
                else
                {
                    //MessageBox.Show("顧客担当者名が入力されていません");
                    messageDsp.DspMsg("M10015");
                    textBoxClCharge.Focus();
                    return false;
                }

                // 日付が必須入力ではない場合は、このチェックは排除してください
                // (不明な場合はリーダーまで)
                if (dateTimePickerOrDate.Checked == false)
                {
                    //MessageBox.Show("受注年月日は必須です");
                    messageDsp.DspMsg("M10044");
                    dateTimePickerOrDate.Focus();
                    return false;
                }
            }

            // 商品IDの適否
            if (!String.IsNullOrEmpty(textBoxPrID.Text.Trim()))
            {
                // 商品IDに一致するレコードの存在チェック
                if (labelPrName.Text == "“UnknownID”")
                {
                    //MessageBox.Show("入力された商品IDは存在しません");
                    messageDsp.DspMsg("M10043");
                    textBoxPrID.Focus();
                    return false;
                }
                // 商品IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxPrID.Text.Trim()))
                {
                    //MessageBox.Show("商品IDは全て半角数字入力です");
                    messageDsp.DspMsg("M10016");
                    textBoxPrID.Focus();
                    return false;
                }
                // 商品IDの文字数チェック
                if (textBoxPrID.TextLength > 6)
                {
                    //MessageBox.Show("商品IDは6文字です");
                    messageDsp.DspMsg("M10017");
                    textBoxPrID.Focus();
                    return false;
                }
                // 商品IDが0ではないかチェック
                if (int.Parse(textBoxPrID.Text.Trim()) == 0)
                {
                    //MessageBox.Show("商品IDは01から割り当ててください");
                    messageDsp.DspMsg("M10018");
                    textBoxPrID.Focus();
                    return false;
                }
                // 商品IDの重複チェック
                if (orderDataAccess.CheckProductIDExistence(textBoxPrID.Text.Trim(), textBoxOrID.Text.Trim()))
                {
                    string i = textBoxOrID.Text.Trim();
                    MessageBox.Show($"入力された'受注ID{i}'の詳細テーブル内で商品IDが重複します。", "入力確認", 0, (MessageBoxIcon)16);
                    textBoxOrID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("商品IDが入力されていません");
                messageDsp.DspMsg("M10019");
                textBoxPrID.Focus();
                return false;
            }

            // 数量の適否
            if (!String.IsNullOrEmpty(textBoxOrQuantity.Text.Trim()))
            {
                // 数量の半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxOrQuantity.Text.Trim()))
                {
                    //MessageBox.Show("数量は全て半角数字入力です");
                    messageDsp.DspMsg("M10020");
                    textBoxOrQuantity.Focus();
                    return false;
                }
                // 数量の文字数チェック
                if (textBoxOrQuantity.TextLength > 4)
                {
                    //MessageBox.Show("数量は4文字です");
                    messageDsp.DspMsg("M10021");
                    textBoxOrQuantity.Focus();
                    return false;
                }
                // 数量の０チェック
                if (textBoxOrQuantity.Text == "0")
                {
                    //MessageBox.Show("数量は01以上です");
                    messageDsp.DspMsg("M10066");
                    textBoxOrQuantity.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("数量が入力されていません");
                messageDsp.DspMsg("M10022");
                textBoxOrQuantity.Focus();
                return false;
            }

            // 合計金額の適否
            if (!String.IsNullOrEmpty(textBoxOrTotalPrice.Text.Trim()))
            {
                // 合計金額の半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxOrTotalPrice.Text.Trim()))
                {
                    //MessageBox.Show("合計金額は全て半角数字入力です");
                    messageDsp.DspMsg("M10023");
                    textBoxOrTotalPrice.Focus();
                    return false;
                }
                // 合計金額の文字数チェック
                int OrQuantity2 = int.Parse(textBoxOrQuantity.Text.Trim());
                int OrPrice2 = int.Parse(textBoxOrTotalPrice.Text.Trim());
                int OrTotalPrice2 = OrQuantity2 * OrPrice2; //数量×単価(金額)を合計金額
                if (OrTotalPrice2.ToString().Length > 10)
                {
                    //MessageBox.Show("合計金額が10文字を超えました。数量または金額が間違っていないか確認してください。");
                    messageDsp.DspMsg("M10024");
                    textBoxOrQuantity.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("合計金額が入力されていません");
                messageDsp.DspMsg("M10026");
                textBoxOrTotalPrice.Focus();
                return false;
            }

            // 状態フラグの適否
            if (checkBoxOrStateFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("状態フラグが不確定の状態です");
                messageDsp.DspMsg("M10027");
                checkBoxOrStateFlag.Focus();
                return false;
            }

            // 状態フラグの適否２
            if (checkBoxOrStateFlag.Checked == true)
            {
                //MessageBox.Show("登録では管理フラグは適用されません");
                messageDsp.DspMsg("M10056");
                checkBoxOrStateFlag.Focus();
                return false;
            }

            // 管理フラグの適否
            if (checkBoxOrFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("管理フラグが不確定の状態です");
                messageDsp.DspMsg("M10028");
                checkBoxOrFlag.Focus();
                return false;
            }

            // 管理フラグの適否２
            if (checkBoxOrFlag.Checked == true)
            {
                //MessageBox.Show("登録では管理フラグは適用されません");
                messageDsp.DspMsg("M10029");
                checkBoxOrFlag.Focus();
                return false;
            }

            return true;
        }


        private Entity.T_OrderProvisional GenerateDataAtProvisional()
        {
            // 現在のT_OrderDspのテーブルを取得
            Order = orderDataAccess.GetOrderData();

            // T_OrderDspから、末尾の行を取得する
            if (Order.Count == 0)
            {
                CopyOrID = 1; //グリッドビューが空の時はCountが0ではなくnullになるので直接1を代入
            }
            else
            {
                T_OrderDsp lastOrder = Order[Order.Count - 1];
                // 特定の列の値を次の新規登録OrIDで使用するために+1して取得する
                CopyOrID = lastOrder.OrID + 1;
            }

            DateTime? mOrDate;
            if (dateTimePickerOrDate.Checked == false)
            {
                mOrDate = null;
            }
            else
            {
                mOrDate = DateTime.Parse(dateTimePickerOrDate.Text);
            }

            if (provisionalMode == false)
            {
                return new Entity.T_OrderProvisional
                {
                    OrID = CopyOrID,
                    SoID = int.Parse(textBoxSoID.Text.Trim()),
                    EmID = int.Parse(textBoxEmID.Text.Trim()),
                    ClID = int.Parse(textBoxClID.Text.Trim()),
                    ClCharge = textBoxClCharge.Text.Trim(),
                    OrDate = (DateTime)mOrDate, //日付がnull許容の場合は(DateTime)はいらない(?)
                    OrStateFlag = OrStateFlg,
                    OrFlag = OrFlg,
                    OrHidden = textBoxOrHidden.Text.Trim()
                };
            }
            else
            {
                return new Entity.T_OrderProvisional();
            }
        }
        private Entity.T_OrderDetailProvisional GenerateDataDetailAtProvisional()
        {
            if(provisionalMode == true)
            {
                // 現在のT_OrderProvisionalDspのテーブルを取得
                OrderProvisional = orderDataAccess.GetOrderProvisionalData();

                // T_OrderProvisionalDspから、末尾の行を取得する
                Entity.T_OrderProvisionalDsp lastOrder = OrderProvisional[OrderProvisional.Count - 1];

                // 特定の列の値を取得する
                CopyOrID = lastOrder.OrID;
            }

            int OrQuantity2 = int.Parse(textBoxOrQuantity.Text.Trim());
            int OrPrice2 = int.Parse(textBoxOrTotalPrice.Text.Trim());
            int OrTotalPrice2 = OrQuantity2 * OrPrice2; //数量×単価(金額)を合計金額として登録

            return new Entity.T_OrderDetailProvisional
            {
                OrID = CopyOrID,  //常にnullになってしまう受注詳細(OrID)を割り当て済み受注(OrID)で上書き
                PrID = int.Parse(textBoxPrID.Text.Trim()),
                OrQuantity = int.Parse(textBoxOrQuantity.Text.Trim()),
                OrTotalPrice = OrTotalPrice2
            };
        }

        ///////////////////////////////
        //　8.2.1.3 受注情報仮登録
        //メソッド名：ProvisionaOrder()
        //引　数   ：受注情報
        //戻り値   ：なし
        //機　能   ：受注データの仮登録
        ///////////////////////////////
        private void ProvisionalOrder(Entity.T_OrderProvisional regOrderProvisional, Entity.T_OrderDetailProvisional regOrderDetailProvisional)
        {
            // 登録確認メッセージ
            if(provisionalMode == true)
            {
                //受注詳細テーブルを追加してよろしいですか？
                DialogResult result = messageDsp.DspMsg("M10063");
                if (result == DialogResult.Cancel)
                    return;
            }
            else
            {
                //受注データを仮登録してよろしいですか？
                DialogResult result = messageDsp.DspMsg("M10034");
                if (result == DialogResult.Cancel)
                    return;
            }

            // 受注情報の仮登録
            bool flg = orderDataAccess.AddOrderProvisionalData(regOrderProvisional, provisionalMode);
            if (flg == true)
            {
                flg = orderDataAccess.AddOrderDetailProvisionalData(regOrderDetailProvisional);  //メインOrIDと詳細のOrIDを常に一致させるため、
                                                                                                                      //T_OrderProvisional regOrderProvisionalの割り当て済みメインOrIDを受注詳細にもっていく
                if (flg == true)
                {
                    if(provisionalMode == true)
                    {
                        //MessageBox.Show("受注詳細テーブルを追加しました。");
                        messageDsp.DspMsg("M10064");
                    }
                    else
                    {
                        //MessageBox.Show("データを仮登録しました。");
                        messageDsp.DspMsg("M10035");
                    }
                }
                else
                {
                    if(provisionalMode == true)
                    {
                        //MessageBox.Show("受注詳細テーブルの追加に失敗しました。");
                        messageDsp.DspMsg("M10065");
                    }
                    else
                    {
                        //MessageBox.Show("データの仮登録に失敗しました。");
                        messageDsp.DspMsg("M10036");
                    }
                }
            }
            else
            {
                if (provisionalMode == true)
                {
                    //MessageBox.Show("受注詳細テーブルの追加に失敗しました。");
                    messageDsp.DspMsg("M10065");
                }
                else
                {
                    //MessageBox.Show("データの仮登録に失敗しました。");
                    messageDsp.DspMsg("M10036");
                }
            }

            if (provisionalMode == false)
            {
                firstpage();
            }
            else
            {
                lastpage();
            }

            provisionalMode = true; //仮登録用グリッドビュー(DB)に切り替えるための変数

            textBoxPrID.Focus();

            // 入力エリアのクリア
            ClearInput();

            //仮登録中のみ詳細テーブルのみ登録するので不要な項目は非表示(仮登録終了後は元に戻す)
            EnableChange();

            // データグリッドビューの表示
            GetDataGridView();
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
            if (provisionalMode == true)
            {
                //MessageBox.Show("仮登録中は検索できません");
                messageDsp.DspMsg("M10046");
                textBoxPrID.Focus();
                return false;
            }
            else
            {
                // 受注IDの適否
                if (!String.IsNullOrEmpty(textBoxOrID.Text.Trim()))
                {
                    // 受注IDの半角数字チェック
                    if (!dataInputFormCheck.CheckNumeric(textBoxOrID.Text.Trim()))
                    {
                        //MessageBox.Show("受注IDは全て半角数字入力です");
                        messageDsp.DspMsg("M10054");
                        textBoxOrID.Focus();
                        return false;
                    }
                }

                // 顧客IDの適否
                if (!String.IsNullOrEmpty(textBoxClID.Text.Trim()))
                {
                    // 顧客IDに一致するレコードの存在チェック
                    if (labelClName.Text == "“UnknownID”")
                    {
                        //MessageBox.Show("入力された顧客IDは存在しません");
                        messageDsp.DspMsg("M10025");
                        textBoxClID.Focus();
                        return false;
                    }
                    // 顧客IDの半角数字チェック
                    if (!dataInputFormCheck.CheckNumeric(textBoxClID.Text.Trim()))
                    {
                        //MessageBox.Show("顧客IDは全て半角数字入力です");
                        messageDsp.DspMsg("M10001");
                        textBoxClID.Focus();
                        return false;
                    }
                    // 顧客IDの文字数チェック
                    if (textBoxClID.TextLength > 6)
                    {
                        //MessageBox.Show("顧客IDは6文字です");
                        messageDsp.DspMsg("M10002");
                        textBoxClID.Focus();
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

                    // 社員IDに一致するレコードの存在チェック
                    if (labelEmName.Text == "“UnknownID”")
                    {
                        //MessageBox.Show("入力された社員IDは存在しません");
                        messageDsp.DspMsg("M10041");
                        textBoxEmID.Focus();
                        return false;
                    }

                    // 社員IDの半角数字チェック
                    if (!dataInputFormCheck.CheckNumeric(textBoxEmID.Text.Trim()))
                    {
                        //MessageBox.Show("社員IDは全て半角数字入力です");
                        messageDsp.DspMsg("M10005");
                        textBoxEmID.Focus();
                        return false;
                    }
                    // 社員IDの文字数チェック
                    if (textBoxEmID.TextLength > 6)
                    {
                        //MessageBox.Show("社員IDは6文字です");
                        messageDsp.DspMsg("M10006");
                        textBoxEmID.Focus();
                        return false;
                    }
                }

                // 営業所IDの適否
                if (!String.IsNullOrEmpty(textBoxSoID.Text.Trim()))
                {
                    // 営業所IDに一致するレコードの存在チェック
                    if (labelSoName.Text == "“UnknownID”")
                    {
                        //MessageBox.Show("入力された営業所IDは存在しません");
                        messageDsp.DspMsg("M10042");
                        textBoxSoID.Focus();
                        return false;
                    }
                    // 営業所IDの半角数字チェック
                    if (!dataInputFormCheck.CheckNumeric(textBoxSoID.Text.Trim()))
                    {
                        //MessageBox.Show("営業所IDは全て半角数字入力です");
                        messageDsp.DspMsg("M10009");
                        textBoxSoID.Focus();
                        return false;
                    }
                    // 営業所IDの文字数チェック
                    if (textBoxSoID.TextLength > 2)
                    {
                        //MessageBox.Show("営業所IDは2文字です");
                        messageDsp.DspMsg("M10010");
                        textBoxSoID.Focus();
                        return false;
                    }
                }

                // 顧客担当者名の適否
                if (!String.IsNullOrEmpty(textBoxClCharge.Text.Trim()))
                {
                    // 顧客担当者名の全角チェック
                    if (!dataInputFormCheck.CheckFullWidth(textBoxClCharge.Text.Trim()))
                    {
                        //MessageBox.Show("顧客担当者名は全て全角入力です");
                        messageDsp.DspMsg("M10013");
                        textBoxClCharge.Focus();
                        return false;
                    }
                    // 顧客担当者名の文字数チェック
                    if (textBoxClCharge.TextLength > 50)
                    {
                        //MessageBox.Show("顧客担当者名は50文字以下です");
                        messageDsp.DspMsg("M10014");
                        textBoxClCharge.Focus();
                        return false;
                    }
                }

                // 受注詳細IDの未入力チェック
                if (!String.IsNullOrEmpty(textBoxOrDetailID.Text.Trim()))
                {
                    // 受注詳細IDの半角数字チェック
                    if (!dataInputFormCheck.CheckNumeric(textBoxOrDetailID.Text.Trim()))
                    {
                        //MessageBox.Show("受注詳細IDは全て半角数字入力です");
                        messageDsp.DspMsg("M10055");
                        textBoxOrDetailID.Focus();
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
                        messageDsp.DspMsg("M10016");
                        textBoxPrID.Focus();
                        return false;
                    }
                    // 商品IDの文字数チェック
                    if (textBoxPrID.TextLength > 6)
                    {
                        //MessageBox.Show("商品IDは6文字です");
                        messageDsp.DspMsg("M10017");
                        textBoxPrID.Focus();
                        return false;
                    }
                }

                if (dateTimePickerOrDate.Checked == true)
                {
                    //MessageBox.Show("注文年月日は検索対象外です");
                    messageDsp.DspMsg("M10067");
                    dateTimePickerOrDate.Focus();
                    return false;
                }
                if (textBoxOrQuantity.Text != "")
                {
                    //MessageBox.Show("数量は検索対象外です");
                    messageDsp.DspMsg("M10068");
                    textBoxOrQuantity.Focus();
                    return false;
                }

                // 状態フラグの適否
                if (checkBoxOrStateFlag.CheckState == CheckState.Indeterminate)
                {
                    //MessageBox.Show("状態フラグが不確定の状態です");
                    messageDsp.DspMsg("M10027");
                    checkBoxOrStateFlag.Focus();
                    return false;
                }

                // 管理フラグの適否
                if (checkBoxOrFlag.CheckState == CheckState.Indeterminate)
                {
                    //MessageBox.Show("管理フラグが不確定の状態です");
                    messageDsp.DspMsg("M10028");
                    checkBoxOrFlag.Focus();
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
        public static string mSoID;
        public static string mEmID;
        public static string mClID;
        public static string mClCharge;
        public static int? mOrStateFlg;
        public static int mOrFlg;
        public static string mOrDetailID;
        public static string mPrID;
        private void GenerateDataAtSelect()
        {
            T_OrderDsp selectCondition;

            //boolからintに変換して検索条件セット準備
            if (checkBoxOrStateFlag.Checked == true)
            {
                mOrStateFlg = 1;
            }
            else if (checkBoxOrStateFlag.Checked == false)
            {
                mOrStateFlg = null;
            }
            if (checkBoxOrFlag.Checked == true)
            {
                mOrFlg = 2;
            }
            else if (checkBoxOrFlag.Checked == false)
            {
                mOrFlg = 0;
            }
            mOrID = textBoxOrID.Text.Trim();
            mSoID = textBoxSoID.Text.Trim();
            mEmID = textBoxEmID.Text.Trim();
            mClID = textBoxClID.Text.Trim();
            mClCharge = textBoxClCharge.Text.Trim();
            mOrDetailID = textBoxOrDetailID.Text.Trim();
            mPrID = textBoxPrID.Text.Trim();

            if (mOrID != "")
            {
                // 検索条件のセット
                selectCondition = new T_OrderDsp()
                {
                    OrID = int.Parse(textBoxOrID.Text.Trim()),
                    OrStateFlag = mOrStateFlg,
                    OrFlag = mOrFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Order = orderDataAccess.SearchOrderData(selectCondition);
                return;
            }
            else if (mOrDetailID != "")
            {
                // 検索条件のセット
                selectCondition = new T_OrderDsp()
                {
                    OrDetailID = int.Parse(textBoxOrDetailID.Text.Trim()),
                    OrStateFlag = mOrStateFlg,
                    OrFlag = mOrFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Order = orderDataAccess.SearchOrderData(selectCondition);
                return;
            }
            else if (mSoID != "")
            {
                // 検索条件のセット
                selectCondition = new T_OrderDsp()
                {
                    SoID = int.Parse(textBoxSoID.Text.Trim()),
                    OrStateFlag = mOrStateFlg,
                    OrFlag = mOrFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Order = orderDataAccess.SearchOrderData(selectCondition);
                return;
            }
            else if (mEmID != "")
            {
                // 検索条件のセット
                selectCondition = new T_OrderDsp()
                {
                    EmID = int.Parse(textBoxEmID.Text.Trim()),
                    OrStateFlag = mOrStateFlg,
                    OrFlag = mOrFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Order = orderDataAccess.SearchOrderData(selectCondition);
                return;
            }
            else if (mClID != "") 
            {
                // 検索条件のセット
                selectCondition = new T_OrderDsp()
                {
                    ClID = int.Parse(textBoxClID.Text.Trim()),
                    OrStateFlag = mOrStateFlg,
                    OrFlag = mOrFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Order = orderDataAccess.SearchOrderData(selectCondition);
                return;
            }
            else if (mClCharge != "") 
            {
                // 検索条件のセット
                selectCondition = new T_OrderDsp()
                {
                    ClCharge = textBoxClCharge.Text.Trim(),
                    OrStateFlag = mOrStateFlg,
                    OrFlag = mOrFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Order = orderDataAccess.SearchOrderData(selectCondition);
                return;
            }
            else if (mPrID != "")
            {
                // 検索条件のセット
                selectCondition = new T_OrderDsp()
                {
                    PrID = int.Parse(textBoxPrID.Text.Trim()),
                    OrStateFlag = mOrStateFlg,
                    OrFlag = mOrFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Order = orderDataAccess.SearchOrderData(selectCondition);
                return;
            }
            else if (mOrStateFlg == 1) 
            {
                // 検索条件のセット
                selectCondition = new T_OrderDsp()
                {
                    OrStateFlag = mOrStateFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Order = orderDataAccess.SearchOrderData(selectCondition);
                return;
            }
            else if (mOrFlg == 2) 
            {
                // 検索条件のセット
                selectCondition = new T_OrderDsp()
                {
                    OrFlag = mOrFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Order = orderDataAccess.SearchOrderData(selectCondition);
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

            dataGridViewOrder.DataSource = Order;
            if (Order.Count == 0)
            {
                labelNoTable.Visible = true;
            }
            else
            {
                labelNoTable.Visible = false;
            }

            labelPage.Text = "/" + ((int)Math.Ceiling(Order.Count / (double)pageSize)) + "ページ";
            dataGridViewOrder.Refresh();

            if (Order.Count == 0) //検索結果のデータ数が0ならエラー
            {
                messageDsp.DspMsg("M10037");
                SetFormDataGridView();
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            // 8.2.2.1 妥当な受注データ取得
            if (!GetValidDataAtUpdate())
                return;

            // 8.2.2.2 受注情報作成
            if (provisionalMode == true)
            {
                var updOrderDetailProvisional = GenerateDataAtDetailProvisionalUpdate();

                // 8.2.2.3 受注情報更新
                UpdateOrderProvisional(updOrderDetailProvisional);
            }
            else
            {
                var updOrder = GenerateDataAtUpdate();
                CopyOrID = updOrder.OrID;
                var updOrderDetail = GenerateDataAtDetailUpdate();

                // 8.2.2.3 受注情報更新
                UpdateOrder(updOrder, updOrderDetail);
            }
        }

        ///////////////////////////////
        //　8.2.2.1 妥当な受注データ取得
        //メソッド名：GetValidDataAtUpdate()
        //引　数   ：なし
        //戻り値   ：true or false
        //機　能   ：入力データの形式チェック
        //          ：エラーがない場合True
        //          ：エラーがある場合False
        ///////////////////////////////
        private bool GetValidDataAtUpdate()
        {
            if (provisionalMode == false)
            {
                // 受注IDの未入力チェック
                if (!String.IsNullOrEmpty(textBoxOrID.Text.Trim()))
                {
                    // 受注IDの文字数チェック
                    if (textBoxOrID.TextLength > 6)
                    {
                        //MessageBox.Show("受注IDは6文字です");
                        messageDsp.DspMsg("M10062");
                        textBoxOrID.Focus();
                        return false;
                    }
                    // 受注IDの半角数字チェック
                    if (!dataInputFormCheck.CheckNumeric(textBoxOrID.Text.Trim()))
                    {
                        //MessageBox.Show("受注IDは全て半角数字入力です");
                        messageDsp.DspMsg("M10054");
                        textBoxOrID.Focus();
                        return false;
                    }
                    // 受注IDの存在チェック
                    if (!orderDataAccess.CheckOrderCDExistence(textBoxOrID.Text.Trim()))
                    {
                        //MessageBox.Show("入力された受注IDは存在しません");
                        messageDsp.DspMsg("M10050");
                        textBoxOrID.Focus();
                        return false;
                    }
                }
                else
                {
                    //MessageBox.Show("受注IDが入力されていません");
                    messageDsp.DspMsg("M10051");
                    textBoxOrID.Focus();
                    return false;
                }

                // 顧客IDの適否
                if (!String.IsNullOrEmpty(textBoxClID.Text.Trim()))
                {
                    // 顧客IDに一致するレコードの存在チェック
                    if (labelClName.Text == "“UnknownID”")
                    {
                        //MessageBox.Show("入力された顧客IDは存在しません");
                        messageDsp.DspMsg("M10025");
                        textBoxClID.Focus();
                        return false;
                    }
                    // 顧客IDの半角数字チェック
                    if (!dataInputFormCheck.CheckNumeric(textBoxClID.Text.Trim()))
                    {
                        //MessageBox.Show("顧客IDは全て半角数字入力です");
                        messageDsp.DspMsg("M10001");
                        textBoxClID.Focus();
                        return false;
                    }
                    // 顧客IDの文字数チェック
                    if (textBoxClID.TextLength > 6)
                    {
                        //MessageBox.Show("顧客IDは6文字です");
                        messageDsp.DspMsg("M10002");
                        textBoxClID.Focus();
                        return false;
                    }
                    // 顧客IDが0ではないかチェック
                    if (int.Parse(textBoxClID.Text.Trim()) == 0)
                    {
                        //MessageBox.Show("顧客IDは01から割り当ててください");
                        messageDsp.DspMsg("M10003");
                        textBoxClID.Focus();
                        return false;
                    }
                }
                else
                {
                    //MessageBox.Show("顧客IDが入力されていません");
                    messageDsp.DspMsg("M10004");
                    textBoxClID.Focus();
                    return false;
                }

                // 社員IDの適否
                if (!String.IsNullOrEmpty(textBoxEmID.Text.Trim()))
                {
                    // 社員IDが0ではないかチェック
                    if (int.Parse(textBoxEmID.Text.Trim()) == 0)
                    {
                        //MessageBox.Show("社員IDは01から割り当ててください");
                        messageDsp.DspMsg("M10007");
                        textBoxEmID.Focus();
                        return false;
                    }

                    // 社員IDに一致するレコードの存在チェック
                    if (labelEmName.Text == "“UnknownID”")
                    {
                        //MessageBox.Show("入力された社員IDは存在しません");
                        messageDsp.DspMsg("M10041");
                        textBoxEmID.Focus();
                        return false;
                    }

                    // 社員IDの半角数字チェック
                    if (!dataInputFormCheck.CheckNumeric(textBoxEmID.Text.Trim()))
                    {
                        //MessageBox.Show("社員IDは全て半角数字入力です");
                        messageDsp.DspMsg("M10005");
                        textBoxEmID.Focus();
                        return false;
                    }
                    // 社員IDの文字数チェック
                    if (textBoxEmID.TextLength > 6)
                    {
                        //MessageBox.Show("社員IDは6文字です");
                        messageDsp.DspMsg("M10006");
                        textBoxEmID.Focus();
                        return false;
                    }
                }
                else
                {
                    //MessageBox.Show("社員IDが入力されていません");
                    messageDsp.DspMsg("M10008");
                    textBoxEmID.Focus();
                    return false;
                }


                // 営業所IDの適否
                if (!String.IsNullOrEmpty(textBoxSoID.Text.Trim()))
                {
                    // 営業所IDに一致するレコードの存在チェック
                    if (labelSoName.Text == "“UnknownID”")
                    {
                        //MessageBox.Show("入力された営業所IDは存在しません");
                        messageDsp.DspMsg("M10042");
                        textBoxSoID.Focus();
                        return false;
                    }
                    // 営業所IDの半角数字チェック
                    if (!dataInputFormCheck.CheckNumeric(textBoxSoID.Text.Trim()))
                    {
                        //MessageBox.Show("営業所IDは全て半角数字入力です");
                        messageDsp.DspMsg("M10009");
                        textBoxSoID.Focus();
                        return false;
                    }
                    // 営業所IDの文字数チェック
                    if (textBoxSoID.TextLength > 2)
                    {
                        //MessageBox.Show("営業所IDは2文字です");
                        messageDsp.DspMsg("M10010");
                        textBoxSoID.Focus();
                        return false;
                    }
                    // 営業所IDが0ではないかチェック
                    if (int.Parse(textBoxSoID.Text.Trim()) == 0)
                    {
                        //MessageBox.Show("営業所IDは01から割り当ててください");
                        messageDsp.DspMsg("M10011");
                        textBoxSoID.Focus();
                        return false;
                    }
                }
                else
                {
                    //MessageBox.Show("営業所IDが入力されていません");
                    messageDsp.DspMsg("M10012");
                    textBoxSoID.Focus();
                    return false;
                }

                // 顧客担当者名の適否
                if (!String.IsNullOrEmpty(textBoxClCharge.Text.Trim()))
                {
                    // 顧客担当者名の全角チェック
                    if (!dataInputFormCheck.CheckFullWidth(textBoxClCharge.Text.Trim()))
                    {
                        //MessageBox.Show("顧客担当者名は全て全角入力です");
                        messageDsp.DspMsg("M10013");
                        textBoxClCharge.Focus();
                        return false;
                    }
                    // 顧客担当者名の文字数チェック
                    if (textBoxClCharge.TextLength > 50)
                    {
                        //MessageBox.Show("顧客担当者名は50文字以下です");
                        messageDsp.DspMsg("M10014");
                        textBoxClCharge.Focus();
                        return false;
                    }
                }
                else
                {
                    //MessageBox.Show("顧客担当者名が入力されていません");
                    messageDsp.DspMsg("M10015");
                    textBoxClCharge.Focus();
                    return false;
                }

                // 日付が必須入力ではない場合は、このチェックは排除してください
                // (不明な場合はリーダーまで)
                if (dateTimePickerOrDate.Checked == false)
                {
                    //MessageBox.Show("受注年月日は必須です");
                    messageDsp.DspMsg("M10044");
                    dateTimePickerOrDate.Focus();
                    return false;
                }
            }

            // 受注詳細IDの未入力チェック
            if (!String.IsNullOrEmpty(textBoxOrDetailID.Text.Trim()))
            {
                // 受注詳細IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxOrDetailID.Text.Trim()))
                {
                    //MessageBox.Show("受注詳細IDは全て半角数字入力です");
                    messageDsp.DspMsg("M10055");
                    textBoxOrDetailID.Focus();
                    return false;
                }
                // 受注詳細IDの存在チェック
                if (!orderDataAccess.CheckOrderDetailCDExistence(textBoxOrDetailID.Text.Trim()))
                {
                    //MessageBox.Show("入力された受注詳細IDは存在しません");
                    messageDsp.DspMsg("M10052");
                    textBoxOrDetailID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("受注詳細IDが入力されていません");
                messageDsp.DspMsg("M10053");
                textBoxOrDetailID.Focus();
                return false;
            }

            // 商品IDの適否
            if (!String.IsNullOrEmpty(textBoxPrID.Text.Trim()))
            {
                // 商品IDに一致するレコードの存在チェック
                if (labelPrName.Text == "“UnknownID”")
                {
                    //MessageBox.Show("入力された商品IDは存在しません");
                    messageDsp.DspMsg("M10043");
                    textBoxPrID.Focus();
                    return false;
                }
                // 商品IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxPrID.Text.Trim()))
                {
                    //MessageBox.Show("商品IDは全て半角数字入力です");
                    messageDsp.DspMsg("M10016");
                    textBoxPrID.Focus();
                    return false;
                }
                // 商品IDの文字数チェック
                if (textBoxPrID.TextLength > 6)
                {
                    //MessageBox.Show("商品IDは6文字です");
                    messageDsp.DspMsg("M10017");
                    textBoxPrID.Focus();
                    return false;
                }
                // 商品IDが0ではないかチェック
                if (int.Parse(textBoxPrID.Text.Trim()) == 0)
                {
                    //MessageBox.Show("商品IDは01から割り当ててください");
                    messageDsp.DspMsg("M10018");
                    textBoxPrID.Focus();
                    return false;
                }
                // 商品IDの重複チェック
                if (orderDataAccess.CheckProductIDExistence2(textBoxPrID.Text.Trim(), textBoxOrID.Text.Trim(), textBoxOrDetailID.Text.Trim()))
                {
                    string i = textBoxOrID.Text.Trim();
                    MessageBox.Show($"入力された'受注ID{i}'の詳細テーブル内で商品IDが重複します。", "入力確認", 0, (MessageBoxIcon)16);
                    textBoxPrID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("商品IDが入力されていません");
                messageDsp.DspMsg("M10019");
                textBoxPrID.Focus();
                return false;
            }

            // 数量の適否
            if (!String.IsNullOrEmpty(textBoxOrQuantity.Text.Trim()))
            {
                // 数量の半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxOrQuantity.Text.Trim()))
                {
                    //MessageBox.Show("数量は全て半角数字入力です");
                    messageDsp.DspMsg("M10020");
                    textBoxOrQuantity.Focus();
                    return false;
                }
                // 数量の文字数チェック
                if (textBoxOrQuantity.TextLength > 4)
                {
                    //MessageBox.Show("数量は4文字です");
                    messageDsp.DspMsg("M10021");
                    textBoxOrQuantity.Focus();
                    return false;
                }
                // 数量の０チェック
                if (textBoxOrQuantity.Text == "0")
                {
                    //MessageBox.Show("数量は01以上です");
                    messageDsp.DspMsg("M10066");
                    textBoxOrQuantity.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("数量が入力されていません");
                messageDsp.DspMsg("M10022");
                textBoxOrQuantity.Focus();
                return false;
            }

            // 合計金額の適否
            if (!String.IsNullOrEmpty(textBoxOrTotalPrice.Text.Trim()))
            {
                // 合計金額の半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxOrTotalPrice.Text.Trim()))
                {
                    //MessageBox.Show("合計金額は全て半角数字入力です");
                    messageDsp.DspMsg("M10023");
                    textBoxOrTotalPrice.Focus();
                    return false;
                }
                // 合計金額の文字数チェック
                int OrQuantity2 = int.Parse(textBoxOrQuantity.Text.Trim());
                int OrPrice2 = int.Parse(textBoxOrTotalPrice.Text.Trim());
                int OrTotalPrice2 = OrQuantity2 * OrPrice2; //数量×単価(金額)を合計金額
                if (OrTotalPrice2.ToString().Length > 10)
                {
                    //MessageBox.Show("合計金額が10文字を超えました。数量または金額が間違っていないか確認してください。");
                    messageDsp.DspMsg("M10024");
                    textBoxOrQuantity.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("合計金額が入力されていません");
                messageDsp.DspMsg("M10026");
                textBoxOrTotalPrice.Focus();
                return false;
            }

            // 非表示理由の適否
            if (checkBoxOrFlag.Checked == true)
            {
                if (textBoxOrHidden.Text == "")
                {
                    //MessageBox.Show("非表示理由の入力が必要です");
                    messageDsp.DspMsg("M10030");
                    textBoxOrHidden.Focus();
                    return false;
                }
            }

            // 状態フラグの適否
            if (checkBoxOrStateFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("状態フラグが不確定の状態です");
                messageDsp.DspMsg("M10027");
                checkBoxOrStateFlag.Focus();
                return false;
            }

            // 管理フラグの適否
            if (checkBoxOrFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("管理フラグが不確定の状態です");
                messageDsp.DspMsg("M10028");
                checkBoxOrFlag.Focus();
                return false;
            }
            return true;
        }
        ///////////////////////////////
        //　8.2.2.2 受注情報作成
        //メソッド名：GenerateDataAtUpdate()
        //引　数   ：なし
        //戻り値   ：受注更新情報
        //機　能   ：更新データのセット
        ///////////////////////////////
        private T_Order GenerateDataAtUpdate()
        {
            DateTime? mOrDate;
            if (dateTimePickerOrDate.Checked == false)
            {
                mOrDate = null;
            }
            else
            {
                mOrDate = DateTime.Parse(dateTimePickerOrDate.Text);
            }

            return new T_Order
            {
                OrID = int.Parse(textBoxOrID.Text.Trim()),
                SoID = int.Parse(textBoxSoID.Text.Trim()),
                EmID = int.Parse(textBoxEmID.Text.Trim()),
                ClID = int.Parse(textBoxClID.Text.Trim()),
                ClCharge = textBoxClCharge.Text.Trim(),
                OrDate = (DateTime)mOrDate, //日付がnull許容の場合は(DateTime)はいらない(?)
                OrStateFlag = OrStateFlg,
                OrFlag = OrFlg,
                OrHidden = textBoxOrHidden.Text.Trim()
            };
        }
        private T_OrderDetail GenerateDataAtDetailUpdate()
        {
            int OrQuantity2 = int.Parse(textBoxOrQuantity.Text.Trim());
            int OrPrice2 = int.Parse(textBoxOrTotalPrice.Text.Trim());
            int OrTotalPrice2 = OrQuantity2 * OrPrice2; //数量×単価(金額)を合計金額として登録

            return new T_OrderDetail
            {
                OrDetailID = int.Parse(textBoxOrDetailID.Text.Trim()),
                OrID = CopyOrID,
                PrID = int.Parse(textBoxPrID.Text.Trim()),
                OrQuantity = int.Parse(textBoxOrQuantity.Text.Trim()),
                OrTotalPrice = OrTotalPrice2
            };
        }
        private Entity.T_OrderDetailProvisional GenerateDataAtDetailProvisionalUpdate()
        {
            int OrQuantity2 = int.Parse(textBoxOrQuantity.Text.Trim());
            int OrPrice2 = int.Parse(textBoxOrTotalPrice.Text.Trim());
            int OrTotalPrice2 = OrQuantity2 * OrPrice2; //数量×単価(金額)を合計金額として登録

            return new Entity.T_OrderDetailProvisional
            {
                OrDetailID = int.Parse(textBoxOrDetailID.Text.Trim()),
                PrID = int.Parse(textBoxPrID.Text.Trim()),
                OrQuantity = int.Parse(textBoxOrQuantity.Text.Trim()),
                OrTotalPrice = OrTotalPrice2
            };
        }
        ///////////////////////////////
        //　8.2.2.3 受注情報更新
        //メソッド名：UpdatePosition()
        //引　数   ：受注情報
        //戻り値   ：なし
        //機　能   ：受注情報の更新
        ///////////////////////////////
        private void UpdateOrder(T_Order updOrder, T_OrderDetail updOrderDetail)
        {
            if (OrFlg == 0)
            {
                // 更新確認メッセージ
                DialogResult result = messageDsp.DspMsg("M10047");
                if (result == DialogResult.Cancel)
                    return;

                // 受注情報の更新
                bool flg = orderDataAccess.UpdateOrderData(updOrder);
                if (flg == true)
                {
                    flg = orderDataAccess.UpdateOrderDetailData(updOrderDetail);
                    if (flg == true)
                    {
                        //MessageBox.Show("データを更新しました。");
                        messageDsp.DspMsg("M10048");
                    }
                    else
                    {
                        //MessageBox.Show("データの更新に失敗しました。");
                        messageDsp.DspMsg("M10049");
                    }
                }
                else
                {
                    //MessageBox.Show("データの更新に失敗しました。");
                    messageDsp.DspMsg("M10049");
                }

                textBoxOrID.Focus();

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

            else if (OrFlg == 2)
            {
                DeleteOrder(updOrder, updOrderDetail);
            }
        }
        private void DeleteOrder(T_Order updOrder, T_OrderDetail updOrderDetail)
        {

            // 更新確認メッセージ
            DialogResult result = messageDsp.DspMsg("M10038");
            if (result == DialogResult.Cancel)
                return;

            // 受注情報の更新
            bool flg = orderDataAccess.UpdateOrderData(updOrder);
            if (flg == true)
            {
                flg = orderDataAccess.UpdateOrderDetailData(updOrderDetail);
                if (flg == true)
                {
                    //MessageBox.Show("データを非表示しました。");
                    messageDsp.DspMsg("M10039");
                }
                else
                {
                    //MessageBox.Show("データの非表示に失敗しました。");
                    messageDsp.DspMsg("M10040");
                }
            }
            else
            {
                //MessageBox.Show("データの非表示に失敗しました。");
                messageDsp.DspMsg("M10040");
            }

            textBoxOrID.Focus();

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
            NonMaster.FormOrder.F_OrderConfirm.stflg = 1;
            NonMaster.FormOrder.F_OrderConfirm.ActiveForm.Activate();
            NonMaster.FormChumon.F_Chumon.stflg = 1;
            NonMaster.FormChumon.F_Chumon.ActiveForm.Activate();
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

        private void UpdateOrderProvisional(Entity.T_OrderDetailProvisional updOrderDetailProvisional)
        {
            // 更新確認メッセージ
            DialogResult result = messageDsp.DspMsg("M10047");
            if (result == DialogResult.Cancel)
                return;

            // 受注情報の更新
            bool flg = orderDataAccess.UpdateOrderDetailProvisionalData(updOrderDetailProvisional);
            if (flg == true)
                //MessageBox.Show("データを更新しました。");
                messageDsp.DspMsg("M10048");
            else
                //MessageBox.Show("データの更新に失敗しました。");
                messageDsp.DspMsg("M10049");

            textBoxPrID.Focus();

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

        private void EnableChange()
        {
            if(provisionalMode == true)
            {
                labelProvisional.Visible = true;
                buttonProvisionalCancel.Enabled = true;
                buttonProvisionalCancel.BackgroundImage.Dispose();
                buttonProvisionalCancel.BackgroundImage = Properties.Resources.Fixed_削除;
                buttonProvisionalCancel.ForeColor = Color.Maroon;
                buttonRegist.Enabled = true;
                buttonRegist.BackgroundImage.Dispose();
                buttonRegist.BackgroundImage = Properties.Resources.Fixed_登録;
                buttonProvisional.Text = "追 加";
                labelMark1.Visible = true;
                labelMark2.Visible = true;
                textBoxClID.Enabled = false;
                textBoxOrID.Enabled = false;
                textBoxEmID.Enabled = false;
                textBoxSoID.Enabled = false;
                dateTimePickerOrDate.Enabled = false;
                textBoxClCharge.Enabled = false;
                checkBoxOrStateFlag.Enabled = false;
                checkBoxOrFlag.Enabled = false;

                if (frm2 != null)
                    frm2.Close();
            }
            else
            {
                labelProvisional.Visible = false;
                buttonProvisionalCancel.BackgroundImage.Dispose();
                buttonProvisionalCancel.BackgroundImage = Properties.Resources.Fixed_キャンセル使用不可;
                buttonProvisionalCancel.ForeColor = Color.DimGray;
                buttonProvisionalCancel.Enabled = false;
                buttonRegist.BackgroundImage.Dispose();
                buttonRegist.BackgroundImage = Properties.Resources.Fixed_キャンセル使用不可;
                buttonRegist.Enabled = false;
                buttonProvisional.Text = "仮登録";
                labelMark1.Visible = false;
                labelMark2.Visible = false;
                textBoxClID.Enabled = true;
                textBoxOrID.Enabled = true;
                textBoxEmID.Enabled = true;
                textBoxSoID.Enabled = true;
                dateTimePickerOrDate.Enabled = true;
                textBoxClCharge.Enabled = true;
                checkBoxOrStateFlag.Enabled = true;
                checkBoxOrFlag.Enabled = true;
            }
        }

        ///////////////////////////////
        //メソッド名：ClearInput()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：入力エリアをクリア
        ///////////////////////////////
        private void ClearInput()
        {
            textBoxClID.Text = "";
            textBoxOrID.Text = "";
            textBoxEmID.Text = "";
            textBoxSoID.Text = "";
            dateTimePickerOrDate.Value = DateTime.Now;
            dateTimePickerOrDate.Checked = false;
            textBoxClCharge.Text = "";
            textBoxOrDetailID.Text = "";
            textBoxPrID.Text = "";
            textBoxOrQuantity.Text = "";
            textBoxOrTotalPrice.Text = "";
            textBoxOrHidden.Text = "";
            checkBoxOrStateFlag.Checked = false;
            checkBoxOrFlag.Checked = false;
        }

        private void checkBoxOrFlag_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxOrFlag.Checked == true)
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
                OrFlg = 2;
                textBoxOrHidden.Enabled = true;
                return;
            }
            else if(checkBoxOrFlag.Checked == false)
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
                OrFlg = 0;
                textBoxOrHidden.Enabled = false;
                textBoxOrHidden.Text = "";
                return;
            }
        }

        private void checkBoxOrStateFlag_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxOrStateFlag.Checked == true)
            {
                OrStateFlg = 1;
                return;
            }
            else if(checkBoxOrStateFlag.Checked == false)
            {
                OrStateFlg = 0;
                return;
            }
        }

        private void dataGridViewOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewOrder.CurrentRow != null)
            {
                //クリックされた行データをテキストボックスへ
                textBoxOrID.Text = dataGridViewOrder.Rows[dataGridViewOrder.CurrentRow.Index].Cells[0].Value.ToString();
                textBoxSoID.Text = dataGridViewOrder.Rows[dataGridViewOrder.CurrentRow.Index].Cells[1].Value.ToString();
                textBoxEmID.Text = dataGridViewOrder.Rows[dataGridViewOrder.CurrentRow.Index].Cells[2].Value.ToString();
                textBoxClID.Text = dataGridViewOrder.Rows[dataGridViewOrder.CurrentRow.Index].Cells[3].Value.ToString();
                textBoxClCharge.Text = dataGridViewOrder.Rows[dataGridViewOrder.CurrentRow.Index].Cells[4].Value.ToString();

                //日付が設定されていない場合、初期値として現在の日付を設定
                if (dataGridViewOrder.Rows[dataGridViewOrder.CurrentRow.Index].Cells[5].Value == null)
                {
                    dateTimePickerOrDate.Value = DateTime.Now;
                    dateTimePickerOrDate.Checked = false;
                }
                else
                {
                    dateTimePickerOrDate.Text = dataGridViewOrder.Rows[dataGridViewOrder.CurrentRow.Index].Cells[5].Value.ToString();
                }

                //状態フラグの数値型をbool型に変換して取得
                int OrStateFlg2 = int.Parse(dataGridViewOrder.Rows[dataGridViewOrder.CurrentRow.Index].Cells[6].Value.ToString());
                if (OrStateFlg2 == 0)
                {
                    checkBoxOrStateFlag.Checked = false;
                }
                else
                {
                    checkBoxOrStateFlag.Checked = true;
                }
                //管理フラグの数値型をbool型に変換して取得
                int OrFlg2 = int.Parse(dataGridViewOrder.Rows[dataGridViewOrder.CurrentRow.Index].Cells[7].Value.ToString());
                if (OrFlg2 == 0)
                {
                    checkBoxOrFlag.Checked = false;
                }
                else if (OrFlg2 == 2)
                {
                    checkBoxOrFlag.Checked = true;
                }

                textBoxOrHidden.Text = dataGridViewOrder.Rows[dataGridViewOrder.CurrentRow.Index].Cells[8].Value.ToString();
                textBoxOrDetailID.Text = dataGridViewOrder.Rows[dataGridViewOrder.CurrentRow.Index].Cells[9].Value.ToString();
                textBoxPrID.Text = dataGridViewOrder.Rows[dataGridViewOrder.CurrentRow.Index].Cells[10].Value.ToString();
                int OrQuantity2 = int.Parse(dataGridViewOrder.Rows[dataGridViewOrder.CurrentRow.Index].Cells[11].Value.ToString());
                int OrPrice2 = int.Parse(dataGridViewOrder.Rows[dataGridViewOrder.CurrentRow.Index].Cells[12].Value.ToString());
                int OrTotalPrice2 = OrPrice2 / OrQuantity2;
                textBoxOrQuantity.Text = dataGridViewOrder.Rows[dataGridViewOrder.CurrentRow.Index].Cells[11].Value.ToString();
                textBoxOrTotalPrice.Text = OrTotalPrice2.ToString();
            }
        }


        // 詳細欄の商品IDに一致する商品管理の商品IDをSingleで一件だけレコードを取ってきてその中のPrice(価格)を詳細欄の金額に表示
        private void textBoxPrID_TextChanged(object sender, EventArgs e)
        {
            if (textBoxPrID.Text != "")
            {
                var context = new SalesManagement_DevContext();
                try
                {
                    int mPrID = int.Parse(textBoxPrID.Text);
                    var mProduct = context.M_Products.Single(x => x.PrID == mPrID);
                    if(mProduct.PrFlag == 2)
                    {
                        string mPrName = mProduct.PrName;
                        int mPrice = mProduct.Price;
                        labelPrName.Text = "(非表示)" + mPrName;
                        labelPrName.Visible = true;
                        textBoxOrTotalPrice.Text = mPrice.ToString();
                        context.Dispose();
                    }
                    else
                    {
                        string mPrName = mProduct.PrName;
                        int mPrice = mProduct.Price;
                        labelPrName.Text = mPrName;
                        labelPrName.Visible = true;
                        textBoxOrTotalPrice.Text = mPrice.ToString();
                        context.Dispose();
                    }
                }
                catch
                {
                    labelPrName.Text = "“UnknownID”";
                    labelPrName.Visible = true;
                    textBoxOrTotalPrice.Text = "“UnknownID”";
                    context.Dispose();
                    return;
                }
            }
            else
            {
                labelPrName.Visible = false;
                labelPrName.Text = "商品名";
                textBoxOrTotalPrice.Text = "";
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
                    if(mClient.ClFlag == 2)
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
                    if(mEmployee.EmFlag == 2)
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
                    if(textBoxClID.Text == "")
                    {
                        int mSoID = mEmployee.SoID;
                        textBoxSoID.Text = mSoID.ToString();
                    }
                    mEmName = mEmployee.EmName;
                    textBoxClCharge.Text = mEmName;
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
                textBoxClCharge.Text = "";
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
                    if(mSalesOffice.SoFlag == 2)
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

        private void buttonClear_Click(object sender, EventArgs e)
        {
            // 入力エリアのクリア
            ClearInput();
            // データグリッドビューの表示
            SetFormDataGridView();
        }

        private void buttonDetailClear_Click(object sender, EventArgs e)
        {
            // 受注詳細欄の入力エリアのクリア
            textBoxOrDetailID.Text = "";
            textBoxPrID.Text = "";
            textBoxOrQuantity.Text = "";
            textBoxOrTotalPrice.Text = "";

            // データグリッドビューの表示
            SetFormDataGridView();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (frm2 != null)
                frm2.Close();
            this.Close();
        }

        private void buttonPageSizeChange_Click(object sender, EventArgs e)
        {
            SetDataGridView();
        }

        private void buttonFirstPage_Click(object sender, EventArgs e)
        {
            firstpage();
        }

        private void firstpage()
        {
            if (textBoxPageSize.Text == "" || textBoxPageSize.Text == "0" || textBoxPageSize.TextLength > 9) //Int32の最大値は 2,147,483,647
            {
                textBoxPageSize.Text = "15";
            }
            int pageSize = int.Parse(textBoxPageSize.Text);

            if (provisionalMode == true)
            {
                dataGridViewOrder.DataSource = OrderProvisional.Take(pageSize).ToList();
            }
            else
            {
                filteredList = Order.Where(x => x.OrFlag != 2).ToList(); //OrFlagが2のレコードは排除する
                dataGridViewOrder.DataSource = filteredList.Take(pageSize).ToList();
            }

            // DataGridViewを更新
            dataGridViewOrder.Refresh();
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

            if (provisionalMode == true)
            {
                dataGridViewOrder.DataSource = OrderProvisional.Skip(pageSize * pageNo).Take(pageSize).ToList();
            }
            else
            {
                filteredList = Order.Where(x => x.OrFlag != 2).ToList(); //OrFlagが2のレコードは排除する
                dataGridViewOrder.DataSource = filteredList.Skip(pageSize * pageNo).Take(pageSize).ToList();
            }

            // DataGridViewを更新
            dataGridViewOrder.Refresh();
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
            if (provisionalMode == true)
            {
                //仮登録
                int lastNo = (int)Math.Ceiling(OrderProvisional.Count / (double)pageSize) - 1;
                //最終ページでなければ
                if (pageNo <= lastNo)
                {
                    if (provisionalMode == true)
                    {
                        dataGridViewOrder.DataSource = OrderProvisional.Skip(pageSize * pageNo).Take(pageSize).ToList();
                    }
                    else
                    {
                        filteredList = Order.Where(x => x.OrFlag != 2).ToList(); //OrFlagが2のレコードは排除する
                        dataGridViewOrder.DataSource = filteredList.Skip(pageSize * pageNo).Take(pageSize).ToList();
                    }
                }
            }
            else
            {
                int lastNo = (int)Math.Ceiling(filteredList.Count / (double)pageSize) - 1;
                //最終ページでなければ
                if (pageNo <= lastNo)
                {
                    if (provisionalMode == true)
                    {
                        dataGridViewOrder.DataSource = OrderProvisional.Skip(pageSize * pageNo).Take(pageSize).ToList();
                    }
                    else
                    {
                        filteredList = Order.Where(x => x.OrFlag != 2).ToList(); //OrFlagが2のレコードは排除する
                        dataGridViewOrder.DataSource = filteredList.Skip(pageSize * pageNo).Take(pageSize).ToList();
                    }
                }
            }

            // DataGridViewを更新
            dataGridViewOrder.Refresh();
            //ページ番号の設定
            if (provisionalMode == true)
            {
                //仮登録
                int lastPage = (int)Math.Ceiling(OrderProvisional.Count / (double)pageSize);

                if (pageNo >= lastPage)
                    textBoxPageNo.Text = lastPage.ToString();
                else
                    textBoxPageNo.Text = (pageNo + 1).ToString();
            }
            else
            {
                int lastPage = (int)Math.Ceiling(filteredList.Count / (double)pageSize);

                if (pageNo >= lastPage)
                    textBoxPageNo.Text = lastPage.ToString();
                else
                    textBoxPageNo.Text = (pageNo + 1).ToString();
            }
        }

        private void buttonLastPage_Click(object sender, EventArgs e)
        {
            lastpage();
        }
        private void lastpage()
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
            int pageNo;
            if (provisionalMode == true)
            {
                //仮登録
                pageNo = (int)Math.Ceiling(OrderProvisional.Count / (double)pageSize) - 1;
            }
            else
            {
                pageNo = (int)Math.Ceiling(filteredList.Count / (double)pageSize) - 1;
            }

            if (provisionalMode == true)
            {
                dataGridViewOrder.DataSource = OrderProvisional.Skip(pageSize * pageNo).Take(pageSize).ToList();
            }
            else
            {
                filteredList = Order.Where(x => x.OrFlag != 2).ToList(); //OrFlagが2のレコードは排除する
                dataGridViewOrder.DataSource = filteredList.Skip(pageSize * pageNo).Take(pageSize).ToList();
            }

            // DataGridViewを更新
            dataGridViewOrder.Refresh();
            //ページ番号の設定
            textBoxPageNo.Text = (pageNo + 1).ToString();
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

        private void label商品ID_MouseEnter(object sender, EventArgs e)
        {
            label商品ID.BackColor = Color.Aqua;
        }

        private void label商品ID_MouseLeave(object sender, EventArgs e)
        {
            label商品ID.BackColor = Color.Transparent;
        }

        private void F_Order_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (frm2 != null)
                frm2.Close();
        }

        private void F_Order_Activated(object sender, EventArgs e)
        {
            labelEmpName.Text = F_menu.loginName;
            labelEmpID.Text = F_menu.loginEmID;
            labelOfficeName.Text = F_menu.loginSalesOffice;
            if (F_menu.loginSalesOffice == "本社" || F_menu.loginSalesOffice.Contains("倉庫"))
            {
                buttonRegist.Enabled = false;
                buttonRegist.BackgroundImage = Properties.Resources.Fixed_キャンセル使用不可;
                buttonUpdate.Enabled = false;
                buttonUpdate.BackgroundImage = Properties.Resources.Fixed_キャンセル使用不可;
                buttonProvisional.Enabled = false;
                buttonProvisional.BackgroundImage = Properties.Resources.Fixed_キャンセル使用不可;
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
