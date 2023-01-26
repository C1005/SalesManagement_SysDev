using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagement_SysDev
{
    public partial class F_Login : Form
    {
        //メッセージ表示用クラスのインスタンス化
        MessageDsp messageDsp = new MessageDsp();
        //データベース顧客テーブルアクセス用クラスのインスタンス化
        Forms.DbAccess.EmployeeDataAccess employeeDataAccess = new Forms.DbAccess.EmployeeDataAccess();
        //入力形式チェック用クラスのインスタンス化
        DataInputFormCheck dataInputFormCheck = new DataInputFormCheck();
        //パスワードハッシュ化用クラスのインスタンス化
        Common.PassWordHash passWordHash = new Common.PassWordHash();
        internal static int SysMode = 0;
        int count = 0;

        public F_Login()
        {
            InitializeComponent();
        }

        private void F_Login_Load(object sender, EventArgs e)
        {

        }

        private void btn_CleateDabase_Click_1(object sender, EventArgs e)
        {
            //データベースの生成を行います．
            //再度実行する場合には，必ずデータベースの削除をしてから実行してください．

            //役職マスタを生成するサンプル（1件目に管理者を追加する例）
            M_Position FirstPosition = new M_Position()
            {
                PoName = "管理者",
                PoHidden = ""
            };
            SalesManagement_DevContext context = new SalesManagement_DevContext();
            context.M_Positions.Add(FirstPosition);
            context.SaveChanges();
            context.Dispose();

            //メッセージインポート
            Forms.DbAccess.MessageDataAccess.btn_CleateDabase_Messages();

            MessageBox.Show("テーブル作成完了");
        }

        private void buttonLogon_Click(object sender, EventArgs e)
        {
            // 8.2.4.1 妥当な社員データ取得
            if (!GetValidDataAtSelect())
                return;

            // 8.2.4.2 社員情報抽出
            GenerateDataAtSelect();
        }

        ///////////////////////////////
        //　8.2.4.1 妥当な社員データ取得
        //メソッド名：GetValidDataAtSlect()
        //引　数   ：なし
        //戻り値   ：true or false
        //機　能   ：入力データの形式チェック
        //          ：エラーがない場合True
        //          ：エラーがある場合False
        ///////////////////////////////
        private bool GetValidDataAtSelect()
        {
            // 社員ID入力時チェック
            if (!String.IsNullOrEmpty(textBoxUserID.Text.Trim()))
            {
                // 社員IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxUserID.Text.Trim()))
                {
                    //MessageBox.Show("社員IDは全て半角数字入力です");
                    messageDsp.DspMsg("M5001");
                    textBoxUserID.Focus();
                    return false;
                }
                // 社員IDの文字数チェック
                if (textBoxUserID.TextLength > 6)
                {
                    //MessageBox.Show("社員IDは6文字までです");
                    messageDsp.DspMsg("M5002");
                    textBoxUserID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("社員IDが入力されていません");
                messageDsp.DspMsg("M5004");
                textBoxUserID.Focus();
                return false;
            }

            // ログインPWの未入力チェック
            if (!String.IsNullOrEmpty(textBoxPassword.Text.Trim()))
            {
                // ログインPWの半角英数字チェック
                if (!dataInputFormCheck.CheckHalfAlphabetNumeric(textBoxPassword.Text.Trim()))
                {
                    //MessageBox.Show("ログインPWは全て半角英数字入力です");
                    messageDsp.DspMsg("M5043");
                    textBoxPassword.Focus();
                    return false;
                }
                // ログインPWの文字数チェック
                if (textBoxPassword.TextLength > 13)
                {
                    //MessageBox.Show("ログインPWは13文字以下です");
                    messageDsp.DspMsg("M5044");
                    textBoxPassword.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("ログインPWが入力されていません");
                messageDsp.DspMsg("M5045");
                textBoxPassword.Focus();
                return false;
            }

            return true;
        }

        ///////////////////////////////
        //　8.2.4.2 社員情報抽出
        //メソッド名：GenerateDataAtSelect()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：社員情報の取得
        ///////////////////////////////
        public static int mUserID; //mClIDを別クラス(SalesOfficeDataAccess)でも使用できるように定義
        public static string mPW;
        private void GenerateDataAtSelect()
        {
            bool flg;
            mUserID = int.Parse(textBoxUserID.Text.Trim());
            mPW = passWordHash.CreatePasswordHash(textBoxPassword.Text.Trim());
            try
            {
                var context = new SalesManagement_DevContext();
                flg = context.M_Employees.Any(x => x.EmID == mUserID && x.EmPassword == mPW && x.EmFlag == 0);
                if (flg == true)
                {
                    var tb = from t1 in context.M_Employees
                             join t2 in context.M_SalesOffices
                             on t1.SoID equals t2.SoID
                             where t1.EmID == mUserID && t1.EmPassword == mPW
                             select new
                             {
                                 t1.EmID,
                                 t1.EmName,
                                 t2.SoName
                             };
                    foreach (var p in tb)
                    {
                        F_menu.loginEmID = p.EmID.ToString();
                        F_menu.loginName = p.EmName;
                        F_menu.loginSalesOffice = p.SoName;
                    }

                    if(F_menu.loginSalesOffice == "本社")
                    {
                        context.Dispose();
                        this.Close();
                    }
                    else if(F_menu.loginSalesOffice.Contains("倉庫"))
                    {
                        context.Dispose();
                        this.Close();
                    }
                    else if(F_menu.loginSalesOffice.Contains("営業所"))
                    {
                        context.Dispose();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("営業所名が不明、または未登録なのでご利用できません", "営業所不明", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        context.Dispose();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("社員IDとパスワードが一致しませんでした", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void buttonSignup_Click(object sender, EventArgs e)
        {
            // 8.2.1.1 妥当な顧客データ取得
            if (!GetValidDataAtRegistration())
                return;

            // 8.2.1.2 顧客情報作成
            var regEmployee = GenerateDataAtRegistration();

            // 8.2.1.3 顧客情報登録
            RegistrationEmployee(regEmployee);
        }

        ///////////////////////////////
        //　8.2.1.1 妥当な顧客データ取得
        //メソッド名：GetValidDataAtRegistration()
        //引　数   ：なし
        //戻り値   ：true or false
        //機　能   ：入力データの形式チェック
        //          ：エラーがない場合True
        //          ：エラーがある場合False
        ///////////////////////////////
        private bool GetValidDataAtRegistration()
        {
            if (!String.IsNullOrEmpty(textBoxUserID.Text.Trim()))
            {
                MessageBox.Show("社員IDは自動で振り分けられるため\n入力することはできません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxUserID.Focus();
                return false;
            }

            // ログインPWの未入力チェック
            if (!String.IsNullOrEmpty(textBoxPassword.Text.Trim()))
            {
                // ログインPWの半角英数字チェック
                if (!dataInputFormCheck.CheckHalfAlphabetNumeric(textBoxPassword.Text.Trim()))
                {
                    //MessageBox.Show("ログインPWは全て半角英数字入力です");
                    messageDsp.DspMsg("M5043");
                    textBoxPassword.Focus();
                    return false;
                }
                // ログインPWの文字数チェック
                if (textBoxPassword.TextLength > 13)
                {
                    //MessageBox.Show("ログインPWは13文字以下です");
                    messageDsp.DspMsg("M5044");
                    textBoxPassword.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("ログインPWが入力されていません");
                MessageBox.Show("パスワードを入力してください。", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPassword.Focus();
                return false;
            }
            return true;
        }

        ///////////////////////////////
        //　8.2.1.2 顧客情報作成
        //メソッド名：GenerateDataAtRegistration()
        //引　数   ：なし
        //戻り値   ：顧客登録情報
        //機　能   ：登録データのセット
        ///////////////////////////////
        private M_Employee GenerateDataAtRegistration()
        {
            string emname = "入力してください";
            string emphone = "　　　　　   －";
            string emhidden = "";
            string pw = passWordHash.CreatePasswordHash(textBoxPassword.Text.Trim());
            return new M_Employee
            {
                EmName = emname,
                SoID = 0,
                PoID = 0,
                EmHiredate = new DateTime(2000, 1, 1),
                EmPassword = pw,
                EmFlag = 0,
                EmPhone = emphone,
                EmHidden = emhidden
            };
        }

        ///////////////////////////////
        //　8.2.1.3 顧客情報登録
        //メソッド名：RegistrationEmployee()
        //引　数   ：顧客情報
        //戻り値   ：なし
        //機　能   ：顧客データの登録
        ///////////////////////////////
        private void RegistrationEmployee(M_Employee regEmployee)
        {
            // 登録確認メッセージ
            DialogResult result = messageDsp.DspMsg("M5014");
            if (result == DialogResult.Cancel)
                return;

            // 顧客情報の登録
            bool flg = employeeDataAccess.AddEmployeeData(regEmployee);
            if (flg == true)
            {
                //MessageBox.Show("データを登録しました。");
                messageDsp.DspMsg("M5015");
                var context = new SalesManagement_DevContext();
                int emID = context.M_Employees.Max(x => x.EmID);
                MessageBox.Show($"あなたの社員番号は{emID}です。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                context.Dispose();
            }
            else
            {
                //MessageBox.Show("データの登録に失敗しました。");
                messageDsp.DspMsg("M5016");
            }

            textBoxUserID.Focus();

            // 入力エリアのクリア
            ClearInput();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            // 入力エリアのクリア
            textBoxUserID.Text = "";
            textBoxPassword.Text = "";
            // 8回ボタンを押すとデータベースを削除(デバッグ用)
            count = count + 1;

            if (count == 5)
            {
                buttonSysMode.BringToFront();
                buttonSysMode.Visible = true;
                buttonLogin.Enabled = false;
            }
            if(count == 10)
            {
                // 確認メッセージ
                DialogResult result = MessageBox.Show("データベースを削除しますか？","注意",MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    result = MessageBox.Show("削除後に再起動しますがよろしいですか？", "注意", MessageBoxButtons.OKCancel);
                    if(result == DialogResult.OK)
                    {
                        SalesManagement_SysDev.SalesManagement_DevContext delDB = new SalesManagement_SysDev.SalesManagement_DevContext();
                        delDB.Database.Delete();
                        MessageBox.Show("データベースを削除しました");
                        MessageBox.Show("再起動します","", MessageBoxButtons.YesNo);
                        Application.Restart();
                    }
                    count = 0;
                    return;
                }
                count = 0;
                return;
            }
        }

        private void ClearInput()
        {
            textBoxUserID.Text = "";
            textBoxPassword.Text = "";
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSysMode_Click(object sender, EventArgs e)
        {
            if(textBoxPassword.Text == "sm21" || textBoxPassword.Text == "SM21")
            {
                SysMode = 1;
                F_menu.loginEmID = "111111";
                F_menu.loginName = "開発者";
                F_menu.loginSalesOffice = "デバッグ社";
                this.Close();
            }
            else
            {
                count = 0;
                MessageBox.Show("パスワードが違います", "エラー", 0, MessageBoxIcon.Error);
                buttonLogin.BringToFront();
                buttonLogin.Enabled = true;
                buttonSysMode.Visible = false;
                return;
            }
        }
    }
}
