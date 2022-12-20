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
    public partial class F_Hattyu : Form
    {
        //メッセージ表示用クラスのインスタンス化
        MessageDsp messageDsp = new MessageDsp();
        //データベース受注テーブルアクセス用クラスのインスタンス化
        DbAccess.HattyuDataAccess hattyuDataAccess = new DbAccess.HattyuDataAccess();
        //入力形式チェック用クラスのインスタンス化
        DataInputFormCheck dataInputFormCheck = new DataInputFormCheck();
        //データグリッドビュー用の受注データ
        private static List<T_HattyuDsp> Hattyu;
        private static List<Entity.T_HattyuProvisionalDsp> HattyuProvisional;
        private static List<T_HattyuDsp> filteredList;
        //フラグを数値型で入れるための変数
        int WaWarehouseFlg = 0;
        int HaFlg = 0;
        bool provisionalMode = false;

        public F_Hattyu()
        {
            InitializeComponent();
        }

        private void F_Hattyu_Load(object sender, EventArgs e)
        {
            //ログイン名の表示
            //labelLoginName.Text = FormMenu.loginName;

            // 仮登録データの取得とデータの有無をカウント
            HattyuProvisional = hattyuDataAccess.GetHattyuProvisionalData();
            int Rowcount = HattyuProvisional.Count();
            if (Rowcount != 0)
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
            textBoxPageSize.Text = "12";
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
            if (provisionalMode == true)
            {
                // 仮登録データの取得(注意：データは必ず空であること)
                HattyuProvisional = hattyuDataAccess.GetHattyuProvisionalData();
            }
            else
            {
                // 受注データの取得(通常)
                Hattyu = hattyuDataAccess.GetHattyuData();
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
            int pageSize = int.Parse(textBoxPageSize.Text);
            int pageNo = int.Parse(textBoxPageNo.Text) - 1;
            if (provisionalMode == true)
            {
                dataGridViewHattyu.DataSource = HattyuProvisional.Skip(pageSize * pageNo).Take(pageSize).ToList();
            }
            else
            {
                filteredList = Hattyu.Where(x => x.HaFlag != 2).ToList(); //HaFlagが2のレコードは排除する
                dataGridViewHattyu.DataSource = filteredList.Skip(pageSize * pageNo).Take(pageSize).ToList();
            }

            //各列幅の指定
            dataGridViewHattyu.Columns[0].Width = 100;
            dataGridViewHattyu.Columns[1].Width = 100;
            dataGridViewHattyu.Columns[2].Width = 100;
            dataGridViewHattyu.Columns[3].Width = 130;
            dataGridViewHattyu.Columns[4].Width = 130;
            dataGridViewHattyu.Columns[5].Width = 110;

            dataGridViewHattyu.Columns[6].Width = 400;
            dataGridViewHattyu.Columns[7].Width = 100;
            dataGridViewHattyu.Columns[8].Width = 100;
            dataGridViewHattyu.Columns[9].Width = 70;

            //各列の文字位置の指定
            dataGridViewHattyu.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewHattyu.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewHattyu.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewHattyu.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewHattyu.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewHattyu.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewHattyu.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewHattyu.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewHattyu.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewHattyu.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //dataGridViewの総ページ数
            if (provisionalMode == true)
            {
                //仮登録
                labelPage.Text = "/" + ((int)Math.Ceiling(HattyuProvisional.Count / (double)pageSize)) + "ページ";
            }
            else
            {
                labelPage.Text = "/" + ((int)Math.Ceiling(filteredList.Count / (double)pageSize)) + "ページ";
            }
            dataGridViewHattyu.Refresh();
        }

        private void buttonMakerSearch_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private void buttonProductSearch_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private void buttonEmployeeSearch_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private void buttonConfirmForm_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private void OpenForm(string formName)
        {
            Form frm = new Form();
            //引数より、開くフォームを設定
            switch (formName)
            {
                case "メーカ検索": //ボタンのテキスト名
                    frm = new Master.FormProduct.F_Maker(); //フォームの名前
                    break;
                case "商品検索": //ボタンのテキスト名
                    frm = new Master.FormProduct.F_Product(); //フォームの名前
                    break;
                case "社員検索": //ボタンのテキスト名
                    frm = new Master.FormEmployee.F_Employee(); //フォームの名前
                    break;
                case "発注更新画面へ": //ボタンのテキスト名
                    frm = new F_HattyuUpdate(); //フォームの名前
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
            var regHattyu = GenerateDataAtRegistration();
            var regHattyuDetail = GenerateDataDetailAtRegistration();

            //登録情報を登録
            RegistrationHattyu(regHattyu, regHattyuDetail);
        }

        private bool GetValidDataAtRegistration()
        {
            if (provisionalMode == false)
            {
                //MessageBox.Show("仮登録を行ってから登録してください");
                messageDsp.DspMsg("M13034");
                textBoxHaID.Focus();
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
        private List<T_Hattyu> GenerateDataAtRegistration()
        {
            List<Entity.T_HattyuProvisional> hattyuPr = new List<Entity.T_HattyuProvisional>();
            List<T_Hattyu> HaCopyTb = new List<T_Hattyu>();

            try
            {
                var context = new SalesManagement_DevContext();
                // tbはIEnumerable型
                var tb = from t1 in context.T_HattyuProvisionals

                         select new
                         {
                             t1.HaID,
                             t1.MaID,
                             t1.EmID,
                             t1.HaDate,
                             t1.WaWarehouseFlag,
                             t1.HaFlag,
                             t1.HaHidden
                         };

                // IEnumerable型のデータをList型へ

                foreach (var p in tb)
                {
                    hattyuPr.Add(new Entity.T_HattyuProvisional()
                    {
                        HaID = p.HaID,
                        MaID = p.MaID,
                        EmID = p.EmID,
                        HaDate = p.HaDate,
                        WaWarehouseFlag = p.WaWarehouseFlag,
                        HaFlag = p.HaFlag,
                        HaHidden = p.HaHidden
                    });
                }

                foreach (Entity.T_HattyuProvisional p in hattyuPr)
                {
                    HaCopyTb.Add(new T_Hattyu
                    {
                        HaID = p.HaID,
                        MaID = p.MaID,
                        EmID = p.EmID,
                        HaDate = p.HaDate,
                        WaWarehouseFlag = p.WaWarehouseFlag,
                        HaFlag = p.HaFlag,
                        HaHidden = p.HaHidden
                    });
                }
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return HaCopyTb;
        }

        //仮登録の受注詳細データ取得
        private List<T_HattyuDetail> GenerateDataDetailAtRegistration()
        {
            List<Entity.T_HattyuDetailProvisional> hattyuDetailPr = new List<Entity.T_HattyuDetailProvisional>();
            List<T_HattyuDetail> HaDetailCopyTb = new List<T_HattyuDetail>();

            try
            {
                var context = new SalesManagement_DevContext();
                // tbはIEnumerable型
                var tbDetail = from t2 in context.T_HattyuDetailProvisionals

                               select new
                               {
                                   t2.HaDetailID,
                                   t2.HaID,
                                   t2.PrID,
                                   t2.HaQuantity
                               };

                // IEnumerable型のデータをList型へ

                foreach (var p in tbDetail)
                {
                    hattyuDetailPr.Add(new Entity.T_HattyuDetailProvisional()
                    {
                        HaDetailID = p.HaDetailID,
                        HaID = p.HaID,
                        PrID = p.PrID,
                        HaQuantity = p.HaQuantity
                    });
                }

                foreach (Entity.T_HattyuDetailProvisional p in hattyuDetailPr)
                {
                    HaDetailCopyTb.Add(new T_HattyuDetail
                    {
                        HaDetailID = p.HaDetailID,
                        HaID = p.HaID,
                        PrID = p.PrID,
                        HaQuantity = p.HaQuantity
                    });
                }

                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return HaDetailCopyTb;

        }
        ///////////////////////////////
        //　8.2.1.3 受注情報登録
        //メソッド名：RegistrationHattyu()
        //引　数   ：受注情報
        //戻り値   ：なし
        //機　能   ：受注データの登録
        ///////////////////////////////
        private void RegistrationHattyu(List<T_Hattyu> regHattyu, List<T_HattyuDetail> regHattyuDetail)
        {
            // 登録確認メッセージ
            DialogResult result = messageDsp.DspMsg("M13021");
            if (result == DialogResult.Cancel)
                return;

            // 受注情報の登録
            bool flg = hattyuDataAccess.AddHattyuData(regHattyu);
            if (flg == true)
            {
                flg = hattyuDataAccess.AddHattyuDetailData(regHattyuDetail);
                if (flg == true)
                {
                    //MessageBox.Show("データを登録しました。");
                    messageDsp.DspMsg("M13022");
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
                    messageDsp.DspMsg("M13023");
                }
            }
            else
            {
                //MessageBox.Show("データの登録に失敗しました。");
                messageDsp.DspMsg("M13023");
            }

            textBoxHaID.Focus();

            // 入力エリアのクリア
            ClearInput();

            // データグリッドビューの表示
            GetDataGridView();

        }

        private void buttonProvisionalCancel_Click(object sender, EventArgs e)
        {
            // 中止確認メッセージ
            DialogResult result = MessageBox.Show("仮登録を中止しますか？", "中止確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (result == DialogResult.OK)
            {
                result = MessageBox.Show("キャンセルを行うと現在の仮登録データは失われます。\nよろしいですか？", "中止確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                if (result == DialogResult.OK)
                {
                    provisionalMode = false;
                    EnableChange();
                    textBoxHaID.Focus();
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
                var HaRemoveTb = from t1 in context.T_HattyuProvisionals select t1;

                context.T_HattyuProvisionals.RemoveRange(HaRemoveTb);
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
                var HaDetailRemoveTb = from t2 in context.T_HattyuDetailProvisionals select t2;

                context.T_HattyuDetailProvisionals.RemoveRange(HaDetailRemoveTb);
                context.SaveChanges();
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        int CopyHaID;
        private void buttonProvisional_Click(object sender, EventArgs e)
        {
            // 仮登録データ取得
            if (!GetValidDataAtProvisional())
                return;

            //仮登録情報作成
            var regHattyuProvisional = GenerateDataAtProvisional();
            if (provisionalMode == false)
            {
                CopyHaID = regHattyuProvisional.HaID;  //常にnullになってしまう受注詳細(HaID)を割り当て済み受注(HaID)で上書き
            }
            var regHattyuDetailProvisional = GenerateDataDetailAtProvisional();

            //仮登録情報を登録
            ProvisionalHattyu(regHattyuProvisional, regHattyuDetailProvisional);
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
            if (provisionalMode == false)
            {
                // メーカIDの適否
                if (!String.IsNullOrEmpty(textBoxMaID.Text.Trim()))
                {
                    // 営業所IDに一致するレコードの存在チェック
                    if (labelMaName.Text == "“UnknownID”")
                    {
                        //MessageBox.Show("入力された営業所IDは存在しません");
                        messageDsp.DspMsg("M13050");
                        textBoxMaID.Focus();
                        return false;
                    }
                    // 営業所IDの半角数字チェック
                    if (!dataInputFormCheck.CheckNumeric(textBoxMaID.Text.Trim()))
                    {
                        //MessageBox.Show("営業所IDは全て半角数字入力です");
                        messageDsp.DspMsg("M13005");
                        textBoxMaID.Focus();
                        return false;
                    }
                    // 営業所IDの文字数チェック
                    if (textBoxMaID.TextLength > 4)
                    {
                        //MessageBox.Show("営業所IDは4文字です");
                        messageDsp.DspMsg("M13006");
                        textBoxMaID.Focus();
                        return false;
                    }
                }
                else
                {
                    //MessageBox.Show("営業所IDが入力されていません");
                    messageDsp.DspMsg("M13008");
                    textBoxMaID.Focus();
                    return false;
                }

                // 発注社員IDの適否
                if (!String.IsNullOrEmpty(textBoxEmID.Text.Trim()))
                {
                    // 社員IDが0ではないかチェック
                    if (int.Parse(textBoxEmID.Text.Trim()) == 0)
                    {
                        //MessageBox.Show("社員IDは01から割り当ててください");
                        messageDsp.DspMsg("M13003");
                        textBoxEmID.Focus();
                        return false;
                    }

                    // 社員IDに一致するレコードの存在チェック
                    if (labelEmName.Text == "“UnknownID”")
                    {
                        //MessageBox.Show("入力された社員IDは存在しません");
                        messageDsp.DspMsg("M13031");
                        textBoxEmID.Focus();
                        return false;
                    }

                    // 社員IDの半角数字チェック
                    if (!dataInputFormCheck.CheckNumeric(textBoxEmID.Text.Trim()))
                    {
                        //MessageBox.Show("社員IDは全て半角数字入力です");
                        messageDsp.DspMsg("M13001");
                        textBoxEmID.Focus();
                        return false;
                    }
                    // 社員IDの文字数チェック
                    if (textBoxEmID.TextLength > 6)
                    {
                        //MessageBox.Show("社員IDは6文字です");
                        messageDsp.DspMsg("M13002");
                        textBoxEmID.Focus();
                        return false;
                    }
                }
                else
                {
                    //MessageBox.Show("社員IDが入力されていません");
                    messageDsp.DspMsg("M13004");
                    textBoxEmID.Focus();
                    return false;
                }

                // 日付が必須入力ではない場合は、このチェックは排除してください
                // (不明な場合はリーダーまで)
                if (dateTimePickerHaDate.Checked == false)
                {
                    //MessageBox.Show("受注年月日は必須です");
                    messageDsp.DspMsg("M13033");
                    dateTimePickerHaDate.Focus();
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
                    messageDsp.DspMsg("M13032");
                    textBoxPrID.Focus();
                    return false;
                }
                // 商品IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxPrID.Text.Trim()))
                {
                    //MessageBox.Show("商品IDは全て半角数字入力です");
                    messageDsp.DspMsg("M13009");
                    textBoxPrID.Focus();
                    return false;
                }
                // 商品IDの文字数チェック
                if (textBoxPrID.TextLength > 6)
                {
                    //MessageBox.Show("商品IDは6文字です");
                    messageDsp.DspMsg("M13010");
                    textBoxPrID.Focus();
                    return false;
                }
                // 商品IDが0ではないかチェック
                if (int.Parse(textBoxPrID.Text.Trim()) == 0)
                {
                    //MessageBox.Show("商品IDは01から割り当ててください");
                    messageDsp.DspMsg("M13011");
                    textBoxPrID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("商品IDが入力されていません");
                messageDsp.DspMsg("M13012");
                textBoxPrID.Focus();
                return false;
            }

            // 数量の適否
            if (!String.IsNullOrEmpty(textBoxHaQuantity.Text.Trim()))
            {
                // 数量の半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxHaQuantity.Text.Trim()))
                {
                    //MessageBox.Show("数量は全て半角数字入力です");
                    messageDsp.DspMsg("M13013");
                    textBoxHaQuantity.Focus();
                    return false;
                }
                // 数量の文字数チェック
                if (textBoxHaQuantity.TextLength > 4)
                {
                    //MessageBox.Show("数量は4文字です");
                    messageDsp.DspMsg("M13014");
                    textBoxHaQuantity.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("数量が入力されていません");
                messageDsp.DspMsg("M13015");
                textBoxHaQuantity.Focus();
                return false;
            }

            // 状態フラグの適否
            if (checkBoxWaWarehouseFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("状態フラグが不確定の状態です");
                messageDsp.DspMsg("M13017");
                checkBoxWaWarehouseFlag.Focus();
                return false;
            }

            // 状態フラグの適否２
            if (checkBoxWaWarehouseFlag.Checked == true)
            {
                //MessageBox.Show("登録では管理フラグは適用されません");
                messageDsp.DspMsg("M13045");
                checkBoxWaWarehouseFlag.Focus();
                return false;
            }

            // 管理フラグの適否
            if (checkBoxHaFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("管理フラグが不確定の状態です");
                messageDsp.DspMsg("M13018");
                checkBoxHaFlag.Focus();
                return false;
            }

            // 管理フラグの適否２
            if (checkBoxHaFlag.Checked == true)
            {
                //MessageBox.Show("登録では管理フラグは適用されません");
                messageDsp.DspMsg("M13019");
                checkBoxHaFlag.Focus();
                return false;
            }
            return true;
        }

        private Entity.T_HattyuProvisional GenerateDataAtProvisional()
        {
            // 現在のT_HattyuDspのテーブルを取得
            Hattyu = hattyuDataAccess.GetHattyuData();

            // T_HattyuDspから、末尾の行を取得する
            if (Hattyu.Count == 0)
            {
                CopyHaID = 1; //グリッドビューが空の時はCountが0ではなくnullになるので直接1を代入
            }
            else
            {
                T_HattyuDsp lastHattyu = Hattyu[Hattyu.Count - 1];
                // 特定の列の値を次の新規登録HaIDで使用するために+1して取得する
                CopyHaID = lastHattyu.HaID + 1;
            }

            DateTime? mHaDate;
            if (dateTimePickerHaDate.Checked == false)
            {
                mHaDate = null;
            }
            else
            {
                mHaDate = DateTime.Parse(dateTimePickerHaDate.Text);
            }

            if (provisionalMode == false)
            {
                return new Entity.T_HattyuProvisional
                {
                    HaID = CopyHaID,
                    MaID = int.Parse(textBoxMaID.Text.Trim()),
                    EmID = int.Parse(textBoxEmID.Text.Trim()),
                    HaDate = (DateTime)mHaDate, //日付がnull許容の場合は(DateTime)はいらない(?)
                    WaWarehouseFlag = WaWarehouseFlg,
                    HaFlag = HaFlg,
                    HaHidden = textBoxHaHidden.Text.Trim()
                };
            }
            else
            {
                return new Entity.T_HattyuProvisional();
            }
        }
        private Entity.T_HattyuDetailProvisional GenerateDataDetailAtProvisional()
        {
            if (provisionalMode == true)
            {
                // 現在のT_HattyuProvisionalDspのテーブルを取得
                HattyuProvisional = hattyuDataAccess.GetHattyuProvisionalData();

                // T_HattyuProvisionalDspから、末尾の行を取得する
                Entity.T_HattyuProvisionalDsp lastHattyu = HattyuProvisional[HattyuProvisional.Count - 1];

                // 特定の列の値を取得する
                CopyHaID = lastHattyu.HaID;
            }

            return new Entity.T_HattyuDetailProvisional
            {
                HaID = CopyHaID,  //常にnullになってしまう受注詳細(HaID)を割り当て済み受注(HaID)で上書き
                PrID = int.Parse(textBoxPrID.Text.Trim()),
                HaQuantity = int.Parse(textBoxHaQuantity.Text.Trim()),
            };
        }

        ///////////////////////////////
        //　8.2.1.3 受注情報仮登録
        //メソッド名：ProvisionaHattyu()
        //引　数   ：受注情報
        //戻り値   ：なし
        //機　能   ：受注データの仮登録
        ///////////////////////////////
        private void ProvisionalHattyu(Entity.T_HattyuProvisional regHattyuProvisional, Entity.T_HattyuDetailProvisional regHattyuDetailProvisional)
        {
            // 登録確認メッセージ
            DialogResult result = messageDsp.DspMsg("M13024");
            if (result == DialogResult.Cancel)
                return;

            // 受注情報の仮登録
            bool flg = hattyuDataAccess.AddHattyuProvisionalData(regHattyuProvisional, provisionalMode);
            if (flg == true)
            {
                flg = hattyuDataAccess.AddHattyuDetailProvisionalData(regHattyuDetailProvisional);  //メインHaIDと詳細のHaIDを常に一致させるため、
                                                                                                 //T_HattyuProvisional regHattyuProvisionalの割り当て済みメインHaIDを受注詳細にもっていく
                if (flg == true)
                {
                    //MessageBox.Show("データを仮登録しました。");
                    messageDsp.DspMsg("M13025");
                }
                else
                {
                    //MessageBox.Show("データの仮登録に失敗しました。");
                    messageDsp.DspMsg("M13026");
                }
            }
            else
            {
                //MessageBox.Show("データの仮登録に失敗しました。");
                messageDsp.DspMsg("M13026");
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
                messageDsp.DspMsg("M13035");
                textBoxPrID.Focus();
                return false;
            }
            else
            {
                // 発注IDの適否
                if (!String.IsNullOrEmpty(textBoxHaID.Text.Trim()))
                {
                    // 受注IDの半角数字チェック
                    if (!dataInputFormCheck.CheckNumeric(textBoxHaID.Text.Trim()))
                    {
                        //MessageBox.Show("受注IDは全て半角数字入力です");
                        messageDsp.DspMsg("M13043");
                        textBoxHaID.Focus();
                        return false;
                    }
                }

                // メーカIDの適否
                if (!String.IsNullOrEmpty(textBoxMaID.Text.Trim()))
                {
                    // 営業所IDに一致するレコードの存在チェック
                    if (labelMaName.Text == "“UnknownID”")
                    {
                        //MessageBox.Show("入力された営業所IDは存在しません");
                        messageDsp.DspMsg("M13050");
                        textBoxMaID.Focus();
                        return false;
                    }
                    // 営業所IDの半角数字チェック
                    if (!dataInputFormCheck.CheckNumeric(textBoxMaID.Text.Trim()))
                    {
                        //MessageBox.Show("営業所IDは全て半角数字入力です");
                        messageDsp.DspMsg("M13005");
                        textBoxMaID.Focus();
                        return false;
                    }
                    // 営業所IDの文字数チェック
                    if (textBoxMaID.TextLength > 4)
                    {
                        //MessageBox.Show("営業所IDは4文字です");
                        messageDsp.DspMsg("M13006");
                        textBoxMaID.Focus();
                        return false;
                    }
                }

                // 発注社員IDの適否
                if (!String.IsNullOrEmpty(textBoxEmID.Text.Trim()))
                {
                    // 社員IDが0ではないかチェック
                    if (int.Parse(textBoxEmID.Text.Trim()) == 0)
                    {
                        //MessageBox.Show("社員IDは01から割り当ててください");
                        messageDsp.DspMsg("M13003");
                        textBoxEmID.Focus();
                        return false;
                    }

                    // 社員IDに一致するレコードの存在チェック
                    if (labelEmName.Text == "“UnknownID”")
                    {
                        //MessageBox.Show("入力された社員IDは存在しません");
                        messageDsp.DspMsg("M13031");
                        textBoxEmID.Focus();
                        return false;
                    }

                    // 社員IDの半角数字チェック
                    if (!dataInputFormCheck.CheckNumeric(textBoxEmID.Text.Trim()))
                    {
                        //MessageBox.Show("社員IDは全て半角数字入力です");
                        messageDsp.DspMsg("M13001");
                        textBoxEmID.Focus();
                        return false;
                    }
                    // 社員IDの文字数チェック
                    if (textBoxEmID.TextLength > 6)
                    {
                        //MessageBox.Show("社員IDは6文字です");
                        messageDsp.DspMsg("M13002");
                        textBoxEmID.Focus();
                        return false;
                    }
                }

                // 受注詳細IDの未入力チェック
                if (!String.IsNullOrEmpty(textBoxHaDetailID.Text.Trim()))
                {
                    // 受注詳細IDの半角数字チェック
                    if (!dataInputFormCheck.CheckNumeric(textBoxHaDetailID.Text.Trim()))
                    {
                        //MessageBox.Show("受注詳細IDは全て半角数字入力です");
                        messageDsp.DspMsg("M13044");
                        textBoxHaDetailID.Focus();
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
                        messageDsp.DspMsg("M13009");
                        textBoxPrID.Focus();
                        return false;
                    }
                    // 商品IDの文字数チェック
                    if (textBoxPrID.TextLength > 6)
                    {
                        //MessageBox.Show("商品IDは6文字です");
                        messageDsp.DspMsg("M13010");
                        textBoxPrID.Focus();
                        return false;
                    }
                }

                // 状態フラグの適否
                if (checkBoxWaWarehouseFlag.CheckState == CheckState.Indeterminate)
                {
                    //MessageBox.Show("状態フラグが不確定の状態です");
                    messageDsp.DspMsg("M13017");
                    checkBoxWaWarehouseFlag.Focus();
                    return false;
                }

                // 管理フラグの適否
                if (checkBoxHaFlag.CheckState == CheckState.Indeterminate)
                {
                    //MessageBox.Show("管理フラグが不確定の状態です");
                    messageDsp.DspMsg("M13018");
                    checkBoxHaFlag.Focus();
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
        public static string mEmID;
        public static int? mWaWarehouseFlg;
        public static int mHaFlg;
        public static string mHaDetailID;
        public static string mPrID;
        private void GenerateDataAtSelect()
        {
            T_HattyuDsp selectCondition;

            //boolからintに変換して検索条件セット準備
            if (checkBoxWaWarehouseFlag.Checked == true)
            {
                mWaWarehouseFlg = 1;
            }
            else if (checkBoxWaWarehouseFlag.Checked == false)
            {
                mWaWarehouseFlg = null;
            }
            if (checkBoxHaFlag.Checked == true)
            {
                mHaFlg = 2;
            }
            else if (checkBoxHaFlag.Checked == false)
            {
                mHaFlg = 0;
            }
            mHaID = textBoxHaID.Text.Trim();
            mMaID = textBoxMaID.Text.Trim();
            mEmID = textBoxEmID.Text.Trim();
            mHaDetailID = textBoxHaDetailID.Text.Trim();
            mPrID = textBoxPrID.Text.Trim();

            if (mHaID != "")
            {
                // 検索条件のセット
                selectCondition = new T_HattyuDsp()
                {
                    HaID = int.Parse(textBoxHaID.Text.Trim()),
                    WaWarehouseFlag = mWaWarehouseFlg,
                    HaFlag = mHaFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Hattyu = hattyuDataAccess.SearchHattyuData(selectCondition);
                return;
            }
            else if (mHaDetailID != "")
            {
                // 検索条件のセット
                selectCondition = new T_HattyuDsp()
                {
                    HaDetailID = int.Parse(textBoxHaDetailID.Text.Trim()),
                    WaWarehouseFlag = mWaWarehouseFlg,
                    HaFlag = mHaFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Hattyu = hattyuDataAccess.SearchHattyuData(selectCondition);
                return;
            }
            else if (mMaID != "")
            {
                // 検索条件のセット
                selectCondition = new T_HattyuDsp()
                {
                    MaID = int.Parse(textBoxMaID.Text.Trim()),
                    WaWarehouseFlag = mWaWarehouseFlg,
                    HaFlag = mHaFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Hattyu = hattyuDataAccess.SearchHattyuData(selectCondition);
                return;
            }
            else if (mEmID != "")
            {
                // 検索条件のセット
                selectCondition = new T_HattyuDsp()
                {
                    EmID = int.Parse(textBoxEmID.Text.Trim()),
                    WaWarehouseFlag = mWaWarehouseFlg,
                    HaFlag = mHaFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Hattyu = hattyuDataAccess.SearchHattyuData(selectCondition);
                return;
            }
            else if (mPrID != "")
            {
                // 検索条件のセット
                selectCondition = new T_HattyuDsp()
                {
                    PrID = int.Parse(textBoxPrID.Text.Trim()),
                    WaWarehouseFlag = mWaWarehouseFlg,
                    HaFlag = mHaFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Hattyu = hattyuDataAccess.SearchHattyuData(selectCondition);
                return;
            }
            else if (mWaWarehouseFlg == 1)
            {
                // 検索条件のセット
                selectCondition = new T_HattyuDsp()
                {
                    WaWarehouseFlag = mWaWarehouseFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Hattyu = hattyuDataAccess.SearchHattyuData(selectCondition);
                return;
            }
            else if (mHaFlg == 2)
            {
                // 検索条件のセット
                selectCondition = new T_HattyuDsp()
                {
                    HaFlag = mHaFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // データの抽出
                Hattyu = hattyuDataAccess.SearchHattyuData(selectCondition);
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

            labelPage.Text = "/" + ((int)Math.Ceiling(Hattyu.Count / (double)pageSize)) + "ページ";
            dataGridViewHattyu.Refresh();

            if (Hattyu.Count == 0) //検索結果のデータ数が0ならエラー
            {
                messageDsp.DspMsg("M13027");
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
                var updHattyuDetailProvisional = GenerateDataAtDetailProvisionalUpdate();

                // 8.2.2.3 受注情報更新
                UpdateHattyuProvisional(updHattyuDetailProvisional);
            }
            else
            {
                var updHattyu = GenerateDataAtUpdate();
                CopyHaID = updHattyu.HaID;
                var updHattyuDetail = GenerateDataAtDetailUpdate();

                // 8.2.2.3 受注情報更新
                UpdateHattyu(updHattyu, updHattyuDetail);
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
                if (!String.IsNullOrEmpty(textBoxHaID.Text.Trim()))
                {
                    // 受注IDの半角数字チェック
                    if (!dataInputFormCheck.CheckNumeric(textBoxHaID.Text.Trim()))
                    {
                        //MessageBox.Show("受注IDは全て半角数字入力です");
                        messageDsp.DspMsg("M13043");
                        textBoxHaID.Focus();
                        return false;
                    }
                    // 受注IDの存在チェック
                    if (!hattyuDataAccess.CheckHattyuCDExistence(textBoxHaID.Text.Trim()))
                    {
                        //MessageBox.Show("入力された受注IDは存在しません");
                        messageDsp.DspMsg("M13039");
                        textBoxHaID.Focus();
                        return false;
                    }
                }
                else
                {
                    //MessageBox.Show("受注IDが入力されていません");
                    messageDsp.DspMsg("M13040");
                    textBoxHaID.Focus();
                    return false;
                }

                // メーカIDの適否
                if (!String.IsNullOrEmpty(textBoxMaID.Text.Trim()))
                {
                    // 営業所IDに一致するレコードの存在チェック
                    if (labelMaName.Text == "“UnknownID”")
                    {
                        //MessageBox.Show("入力された営業所IDは存在しません");
                        messageDsp.DspMsg("M13050");
                        textBoxMaID.Focus();
                        return false;
                    }
                    // 営業所IDの半角数字チェック
                    if (!dataInputFormCheck.CheckNumeric(textBoxMaID.Text.Trim()))
                    {
                        //MessageBox.Show("営業所IDは全て半角数字入力です");
                        messageDsp.DspMsg("M13005");
                        textBoxMaID.Focus();
                        return false;
                    }
                    // 営業所IDの文字数チェック
                    if (textBoxMaID.TextLength > 4)
                    {
                        //MessageBox.Show("営業所IDは4文字です");
                        messageDsp.DspMsg("M13006");
                        textBoxMaID.Focus();
                        return false;
                    }
                }
                else
                {
                    //MessageBox.Show("営業所IDが入力されていません");
                    messageDsp.DspMsg("M13008");
                    textBoxMaID.Focus();
                    return false;
                }

                // 発注社員IDの適否
                if (!String.IsNullOrEmpty(textBoxEmID.Text.Trim()))
                {
                    // 社員IDが0ではないかチェック
                    if (int.Parse(textBoxEmID.Text.Trim()) == 0)
                    {
                        //MessageBox.Show("社員IDは01から割り当ててください");
                        messageDsp.DspMsg("M13003");
                        textBoxEmID.Focus();
                        return false;
                    }

                    // 社員IDに一致するレコードの存在チェック
                    if (labelEmName.Text == "“UnknownID”")
                    {
                        //MessageBox.Show("入力された社員IDは存在しません");
                        messageDsp.DspMsg("M13031");
                        textBoxEmID.Focus();
                        return false;
                    }

                    // 社員IDの半角数字チェック
                    if (!dataInputFormCheck.CheckNumeric(textBoxEmID.Text.Trim()))
                    {
                        //MessageBox.Show("社員IDは全て半角数字入力です");
                        messageDsp.DspMsg("M13001");
                        textBoxEmID.Focus();
                        return false;
                    }
                    // 社員IDの文字数チェック
                    if (textBoxEmID.TextLength > 6)
                    {
                        //MessageBox.Show("社員IDは6文字です");
                        messageDsp.DspMsg("M13002");
                        textBoxEmID.Focus();
                        return false;
                    }
                }
                else
                {
                    //MessageBox.Show("社員IDが入力されていません");
                    messageDsp.DspMsg("M13004");
                    textBoxEmID.Focus();
                    return false;
                }

                // 日付が必須入力ではない場合は、このチェックは排除してください
                // (不明な場合はリーダーまで)
                if (dateTimePickerHaDate.Checked == false)
                {
                    //MessageBox.Show("受注年月日は必須です");
                    messageDsp.DspMsg("M13033");
                    dateTimePickerHaDate.Focus();
                    return false;
                }
            }

            // 受注詳細IDの未入力チェック
            if (!String.IsNullOrEmpty(textBoxHaDetailID.Text.Trim()))
            {
                // 受注詳細IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxHaDetailID.Text.Trim()))
                {
                    //MessageBox.Show("受注詳細IDは全て半角数字入力です");
                    messageDsp.DspMsg("M13044");
                    textBoxHaDetailID.Focus();
                    return false;
                }
                // 受注詳細IDの存在チェック
                if (!hattyuDataAccess.CheckHattyuDetailCDExistence(textBoxHaDetailID.Text.Trim()))
                {
                    //MessageBox.Show("入力された受注詳細IDは存在しません");
                    messageDsp.DspMsg("M13041");
                    textBoxHaDetailID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("受注詳細IDが入力されていません");
                messageDsp.DspMsg("M13042");
                textBoxHaDetailID.Focus();
                return false;
            }

            // 商品IDの適否
            if (!String.IsNullOrEmpty(textBoxPrID.Text.Trim()))
            {
                // 商品IDに一致するレコードの存在チェック
                if (labelPrName.Text == "“UnknownID”")
                {
                    //MessageBox.Show("入力された商品IDは存在しません");
                    messageDsp.DspMsg("M13032");
                    textBoxPrID.Focus();
                    return false;
                }
                // 商品IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxPrID.Text.Trim()))
                {
                    //MessageBox.Show("商品IDは全て半角数字入力です");
                    messageDsp.DspMsg("M13009");
                    textBoxPrID.Focus();
                    return false;
                }
                // 商品IDの文字数チェック
                if (textBoxPrID.TextLength > 6)
                {
                    //MessageBox.Show("商品IDは6文字です");
                    messageDsp.DspMsg("M13010");
                    textBoxPrID.Focus();
                    return false;
                }
                // 商品IDが0ではないかチェック
                if (int.Parse(textBoxPrID.Text.Trim()) == 0)
                {
                    //MessageBox.Show("商品IDは01から割り当ててください");
                    messageDsp.DspMsg("M13011");
                    textBoxPrID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("商品IDが入力されていません");
                messageDsp.DspMsg("M13012");
                textBoxPrID.Focus();
                return false;
            }

            // 数量の適否
            if (!String.IsNullOrEmpty(textBoxHaQuantity.Text.Trim()))
            {
                // 数量の半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxHaQuantity.Text.Trim()))
                {
                    //MessageBox.Show("数量は全て半角数字入力です");
                    messageDsp.DspMsg("M13013");
                    textBoxHaQuantity.Focus();
                    return false;
                }
                // 数量の文字数チェック
                if (textBoxHaQuantity.TextLength > 4)
                {
                    //MessageBox.Show("数量は4文字です");
                    messageDsp.DspMsg("M13014");
                    textBoxHaQuantity.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("数量が入力されていません");
                messageDsp.DspMsg("M13015");
                textBoxHaQuantity.Focus();
                return false;
            }

            // 非表示理由の適否
            if (checkBoxHaFlag.Checked == true)
            {
                if (textBoxHaHidden.Text == "")
                {
                    //MessageBox.Show("非表示理由の入力が必要です");
                    messageDsp.DspMsg("M13020");
                    textBoxHaHidden.Focus();
                    return false;
                }
            }

            // 状態フラグの適否
            if (checkBoxWaWarehouseFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("状態フラグが不確定の状態です");
                messageDsp.DspMsg("M13017");
                checkBoxWaWarehouseFlag.Focus();
                return false;
            }

            // 状態フラグの適否２
            if (checkBoxWaWarehouseFlag.Checked == true)
            {
                //MessageBox.Show("登録では管理フラグは適用されません");
                messageDsp.DspMsg("M13045");
                checkBoxWaWarehouseFlag.Focus();
                return false;
            }

            // 管理フラグの適否
            if (checkBoxHaFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("管理フラグが不確定の状態です");
                messageDsp.DspMsg("M13018");
                checkBoxHaFlag.Focus();
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
        private T_Hattyu GenerateDataAtUpdate()
        {
            DateTime? mHaDate;
            if (dateTimePickerHaDate.Checked == false)
            {
                mHaDate = null;
            }
            else
            {
                mHaDate = DateTime.Parse(dateTimePickerHaDate.Text);
            }

            return new T_Hattyu
            {
                HaID = int.Parse(textBoxHaID.Text.Trim()),
                MaID = int.Parse(textBoxMaID.Text.Trim()),
                EmID = int.Parse(textBoxEmID.Text.Trim()),
                HaDate = (DateTime)mHaDate, //日付がnull許容の場合は(DateTime)はいらない(?)
                WaWarehouseFlag = WaWarehouseFlg,
                HaFlag = HaFlg,
                HaHidden = textBoxHaHidden.Text.Trim()
            };
        }
        private T_HattyuDetail GenerateDataAtDetailUpdate()
        {
            return new T_HattyuDetail
            {
                HaDetailID = int.Parse(textBoxHaDetailID.Text.Trim()),
                HaID = CopyHaID,
                PrID = int.Parse(textBoxPrID.Text.Trim()),
                HaQuantity = int.Parse(textBoxHaQuantity.Text.Trim()),
            };
        }
        private Entity.T_HattyuDetailProvisional GenerateDataAtDetailProvisionalUpdate()
        {
            return new Entity.T_HattyuDetailProvisional
            {
                HaDetailID = int.Parse(textBoxHaDetailID.Text.Trim()),
                PrID = int.Parse(textBoxPrID.Text.Trim()),
                HaQuantity = int.Parse(textBoxHaQuantity.Text.Trim()),
            };
        }
        ///////////////////////////////
        //　8.2.2.3 受注情報更新
        //メソッド名：UpdatePosition()
        //引　数   ：受注情報
        //戻り値   ：なし
        //機　能   ：受注情報の更新
        ///////////////////////////////
        private void UpdateHattyu(T_Hattyu updHattyu, T_HattyuDetail updHattyuDetail)
        {
            if (HaFlg == 0)
            {
                // 更新確認メッセージ
                DialogResult result = messageDsp.DspMsg("M13036");
                if (result == DialogResult.Cancel)
                    return;

                // 受注情報の更新
                bool flg = hattyuDataAccess.UpdateHattyuData(updHattyu);
                if (flg == true)
                {
                    flg = hattyuDataAccess.UpdateHattyuDetailData(updHattyuDetail);
                    if (flg == true)
                    {
                        //MessageBox.Show("データを更新しました。");
                        messageDsp.DspMsg("M13037");
                    }
                    else
                    {
                        //MessageBox.Show("データの更新に失敗しました。");
                        messageDsp.DspMsg("M13038");
                    }
                }
                else
                {
                    //MessageBox.Show("データの更新に失敗しました。");
                    messageDsp.DspMsg("M13038");
                }

                textBoxHaID.Focus();

                // 入力エリアのクリア
                ClearInput();

                // データグリッドビューの表示
                GetDataGridView();
            }

            else if (HaFlg == 2)
            {
                DeleteHattyu(updHattyu, updHattyuDetail);
            }
        }
        private void DeleteHattyu(T_Hattyu updHattyu, T_HattyuDetail updHattyuDetail)
        {

            // 更新確認メッセージ
            DialogResult result = messageDsp.DspMsg("M13028");
            if (result == DialogResult.Cancel)
                return;

            // 受注情報の更新
            bool flg = hattyuDataAccess.UpdateHattyuData(updHattyu);
            if (flg == true)
            {
                flg = hattyuDataAccess.UpdateHattyuDetailData(updHattyuDetail);
                if (flg == true)
                {
                    //MessageBox.Show("データを非表示しました。");
                    messageDsp.DspMsg("M13029");
                }
                else
                {
                    //MessageBox.Show("データの非表示に失敗しました。");
                    messageDsp.DspMsg("M13030");
                }
            }
            else
            {
                //MessageBox.Show("データの非表示に失敗しました。");
                messageDsp.DspMsg("M13030");
            }

            textBoxHaID.Focus();

            // 入力エリアのクリア
            ClearInput();

            // データグリッドビューの表示
            GetDataGridView();
        }
        private void UpdateHattyuProvisional(Entity.T_HattyuDetailProvisional updHattyuDetailProvisional)
        {
            // 更新確認メッセージ
            DialogResult result = messageDsp.DspMsg("M13036");
            if (result == DialogResult.Cancel)
                return;

            // 受注情報の更新
            bool flg = hattyuDataAccess.UpdateHattyuDetailProvisionalData(updHattyuDetailProvisional);
            if (flg == true)
                //MessageBox.Show("データを更新しました。");
                messageDsp.DspMsg("M13037");
            else
                //MessageBox.Show("データの更新に失敗しました。");
                messageDsp.DspMsg("M13038");

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
            if (provisionalMode == true)
            {
                labelProvisional.Visible = true;
                buttonProvisionalCancel.Visible = true;
                labelMark1.Visible = true;
                labelMark2.Visible = true;
                textBoxHaID.Enabled = false;
                textBoxEmID.Enabled = false;
                textBoxMaID.Enabled = false;
                dateTimePickerHaDate.Enabled = false;
                textBoxHaDetailID.Enabled = false;
                checkBoxWaWarehouseFlag.Enabled = false;
                checkBoxHaFlag.Enabled = false;
            }
            else
            {
                labelProvisional.Visible = false;
                buttonProvisionalCancel.Visible = false;
                labelMark1.Visible = false;
                labelMark2.Visible = false;
                textBoxHaID.Enabled = true;
                textBoxEmID.Enabled = true;
                textBoxMaID.Enabled = true;
                dateTimePickerHaDate.Enabled = true;
                textBoxHaDetailID.Enabled = true;
                checkBoxWaWarehouseFlag.Enabled = true;
                checkBoxHaFlag.Enabled = true;
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
            textBoxHaID.Text = "";
            textBoxEmID.Text = "";
            textBoxMaID.Text = "";
            dateTimePickerHaDate.Value = DateTime.Now;
            dateTimePickerHaDate.Checked = false;
            textBoxHaDetailID.Text = "";
            textBoxPrID.Text = "";
            textBoxHaQuantity.Text = "";
            textBoxHaHidden.Text = "";
            checkBoxWaWarehouseFlag.Checked = false;
            checkBoxHaFlag.Checked = false;
        }

        private void checkBoxWaWarehouseFlag_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxWaWarehouseFlag.Checked == true)
            {
                WaWarehouseFlg = 1;
                return;
            }
            else if (checkBoxWaWarehouseFlag.Checked == false)
            {
                WaWarehouseFlg = 0;
                return;
            }
        }

        private void checkBoxHaFlag_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxHaFlag.Checked == true)
            {
                HaFlg = 2;
                textBoxHaHidden.Enabled = true;
                return;
            }
            else if (checkBoxHaFlag.Checked == false)
            {
                HaFlg = 0;
                textBoxHaHidden.Enabled = false;
                textBoxHaHidden.Text = "";
                return;
            }
        }

        private void dataGridViewHattyu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //クリックされた行データをテキストボックスへ
            textBoxHaID.Text = dataGridViewHattyu.Rows[dataGridViewHattyu.CurrentRow.Index].Cells[0].Value.ToString();
            textBoxMaID.Text = dataGridViewHattyu.Rows[dataGridViewHattyu.CurrentRow.Index].Cells[1].Value.ToString();
            textBoxEmID.Text = dataGridViewHattyu.Rows[dataGridViewHattyu.CurrentRow.Index].Cells[2].Value.ToString();

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

            //状態フラグの数値型をbool型に変換して取得
            int WaWarehouseFlg2 = int.Parse(dataGridViewHattyu.Rows[dataGridViewHattyu.CurrentRow.Index].Cells[4].Value.ToString());
            if (WaWarehouseFlg2 == 0)
            {
                checkBoxWaWarehouseFlag.Checked = false;
            }
            else
            {
                checkBoxWaWarehouseFlag.Checked = true;
            }
            //管理フラグの数値型をbool型に変換して取得
            int HaFlg2 = int.Parse(dataGridViewHattyu.Rows[dataGridViewHattyu.CurrentRow.Index].Cells[5].Value.ToString());
            if (HaFlg2 == 0)
            {
                checkBoxHaFlag.Checked = false;
            }
            else if (HaFlg2 == 2)
            {
                checkBoxHaFlag.Checked = true;
            }

            textBoxHaHidden.Text = dataGridViewHattyu.Rows[dataGridViewHattyu.CurrentRow.Index].Cells[6].Value.ToString();
            textBoxHaDetailID.Text = dataGridViewHattyu.Rows[dataGridViewHattyu.CurrentRow.Index].Cells[7].Value.ToString();
            textBoxPrID.Text = dataGridViewHattyu.Rows[dataGridViewHattyu.CurrentRow.Index].Cells[8].Value.ToString();
            textBoxHaQuantity.Text = dataGridViewHattyu.Rows[dataGridViewHattyu.CurrentRow.Index].Cells[9].Value.ToString();
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
            textBoxHaDetailID.Text = "";
            textBoxPrID.Text = "";
            textBoxHaQuantity.Text = "";

            // データグリッドビューの表示
            SetFormDataGridView();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonPageSizeChange_Click(object sender, EventArgs e)
        {
            SetDataGridView();
        }

        private void buttonFirstPage_Click(object sender, EventArgs e)
        {
            int pageSize = int.Parse(textBoxPageSize.Text);

            if (provisionalMode == true)
            {
                dataGridViewHattyu.DataSource = HattyuProvisional.Take(pageSize).ToList();
            }
            else
            {
                filteredList = Hattyu.Where(x => x.HaFlag != 2).ToList(); //HaFlagが2のレコードは排除する
                dataGridViewHattyu.DataSource = filteredList.Take(pageSize).ToList();
            }

            // DataGridViewを更新
            dataGridViewHattyu.Refresh();
            //ページ番号の設定
            textBoxPageNo.Text = "1";
        }

        private void buttonPreviousPage_Click(object sender, EventArgs e)
        {
            int pageSize = int.Parse(textBoxPageSize.Text);
            int pageNo = int.Parse(textBoxPageNo.Text) - 2;

            if (provisionalMode == true)
            {
                dataGridViewHattyu.DataSource = HattyuProvisional.Skip(pageSize * pageNo).Take(pageSize).ToList();
            }
            else
            {
                filteredList = Hattyu.Where(x => x.HaFlag != 2).ToList(); //HaFlagが2のレコードは排除する
                dataGridViewHattyu.DataSource = filteredList.Skip(pageSize * pageNo).Take(pageSize).ToList();
            }

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
            int pageSize = int.Parse(textBoxPageSize.Text);
            int pageNo = int.Parse(textBoxPageNo.Text);

            //最終ページの計算
            if (provisionalMode == true)
            {
                //仮登録
                int lastNo = (int)Math.Ceiling(HattyuProvisional.Count / (double)pageSize) - 1;
                //最終ページでなければ
                if (pageNo <= lastNo)
                {
                    if (provisionalMode == true)
                    {
                        dataGridViewHattyu.DataSource = HattyuProvisional.Skip(pageSize * pageNo).Take(pageSize).ToList();
                    }
                    else
                    {
                        filteredList = Hattyu.Where(x => x.HaFlag != 2).ToList(); //HaFlagが2のレコードは排除する
                        dataGridViewHattyu.DataSource = filteredList.Skip(pageSize * pageNo).Take(pageSize).ToList();
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
                        dataGridViewHattyu.DataSource = HattyuProvisional.Skip(pageSize * pageNo).Take(pageSize).ToList();
                    }
                    else
                    {
                        filteredList = Hattyu.Where(x => x.HaFlag != 2).ToList(); //HaFlagが2のレコードは排除する
                        dataGridViewHattyu.DataSource = filteredList.Skip(pageSize * pageNo).Take(pageSize).ToList();
                    }
                }
            }

            // DataGridViewを更新
            dataGridViewHattyu.Refresh();
            //ページ番号の設定
            if (provisionalMode == true)
            {
                //仮登録
                int lastPage = (int)Math.Ceiling(HattyuProvisional.Count / (double)pageSize);

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
            int pageSize = int.Parse(textBoxPageSize.Text);
            //最終ページの計算
            int pageNo;
            if (provisionalMode == true)
            {
                //仮登録
                pageNo = (int)Math.Ceiling(HattyuProvisional.Count / (double)pageSize) - 1;
            }
            else
            {
                pageNo = (int)Math.Ceiling(filteredList.Count / (double)pageSize) - 1;
            }

            if (provisionalMode == true)
            {
                dataGridViewHattyu.DataSource = HattyuProvisional.Skip(pageSize * pageNo).Take(pageSize).ToList();
            }
            else
            {
                filteredList = Hattyu.Where(x => x.HaFlag != 2).ToList(); //HaFlagが2のレコードは排除する
                dataGridViewHattyu.DataSource = filteredList.Skip(pageSize * pageNo).Take(pageSize).ToList();
            }

            // DataGridViewを更新
            dataGridViewHattyu.Refresh();
            //ページ番号の設定
            textBoxPageNo.Text = (pageNo + 1).ToString();
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {

        }
    }
}
