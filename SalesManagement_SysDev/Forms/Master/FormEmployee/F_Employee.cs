using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagement_SysDev.Forms.Master.FormEmployee
{
    public partial class F_Employee : Form
    {
        //メッセージ表示用クラスのインスタンス化
        MessageDsp messageDsp = new MessageDsp();
        //データベース顧客テーブルアクセス用クラスのインスタンス化
        DbAccess.EmployeeDataAccess employeeDataAccess = new DbAccess.EmployeeDataAccess();
        //入力形式チェック用クラスのインスタンス化
        DataInputFormCheck dataInputFormCheck = new DataInputFormCheck();
        //パスワードハッシュ化用クラスのインスタンス化
        Common.PassWordHash passWordHash = new Common.PassWordHash();
        //データグリッドビュー用の顧客データ
        private static List<M_Employee> Employee;
        //管理フラグを数値型で入れるための変数
        int EmFlg;

        public F_Employee()
        {
            InitializeComponent();
        }

        private void buttonSalesOfficeForm_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private void buttonPositionForm_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private void OpenForm(string formName)
        {
            Form frm = new Form();
            //引数より、開くフォームを設定
            switch (formName)
            {
                case "営業所検索": //ボタンのテキスト名
                    frm = new F_SalesOffice(); //フォームの名前
                    break;
                case "役職検索": //ボタンのテキスト名
                    frm = new F_Position(); //フォームの名前
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

        private void F_Employee_Load(object sender, EventArgs e)
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
            dataGridViewEmployee.ReadOnly = true;
            //行内をクリックすることで行を選択
            dataGridViewEmployee.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //ヘッダー位置の指定
            dataGridViewEmployee.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

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

            // 顧客データの取得
            Employee = employeeDataAccess.GetEmployeeData();

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
            dataGridViewEmployee.DataSource = Employee.Skip(pageSize * pageNo).Take(pageSize).ToList();
            dataGridViewEmployee.Columns[5].Visible = false;

            //各列幅の指定
            dataGridViewEmployee.Columns[0].Width = 100;
            dataGridViewEmployee.Columns[1].Width = 200;
            dataGridViewEmployee.Columns[2].Width = 100;
            dataGridViewEmployee.Columns[3].Width = 100;
            dataGridViewEmployee.Columns[4].Width = 100;
            dataGridViewEmployee.Columns[6].Width = 200;
            dataGridViewEmployee.Columns[7].Width = 100;
            dataGridViewEmployee.Columns[8].Width = 400;

            //各列の文字位置の指定
            dataGridViewEmployee.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewEmployee.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewEmployee.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewEmployee.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewEmployee.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewEmployee.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewEmployee.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewEmployee.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            labelPage.Text = "/" + ((int)Math.Ceiling(Employee.Count / (double)pageSize)) + "ページ";

            dataGridViewEmployee.Refresh();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            // 8.2.4.1 妥当な顧客データ取得
            if (!GetValidDataAtSelect())
                return;

            // 8.2.4.2 顧客情報抽出
            GenerateDataAtSelect();

            // 8.2.4.3 顧客抽出結果表示
            SetSelectData();
        }

        ///////////////////////////////
        //　8.2.4.1 妥当な顧客データ取得
        //メソッド名：GetValidDataAtSlect()
        //引　数   ：なし
        //戻り値   ：true or false
        //機　能   ：入力データの形式チェック
        //          ：エラーがない場合True
        //          ：エラーがある場合False
        ///////////////////////////////
        private bool GetValidDataAtSelect()
        {
            // 営業所ID入力時チェック
            if (!String.IsNullOrEmpty(textBoxSoID.Text.Trim()))
            {
                // 営業所IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxSoID.Text.Trim()))
                {
                    //MessageBox.Show("営業所IDは全て半角数字入力です");
                    messageDsp.DspMsg("M1001");
                    textBoxSoID.Focus();
                    return false;
                }
                // 営業IDの文字数チェック
                if (textBoxSoID.TextLength > 2)
                {
                    //MessageBox.Show("顧客IDは2文字までです");
                    messageDsp.DspMsg("M1002");
                    textBoxSoID.Focus();
                    return false;
                }
            }

            // 社員ID入力時チェック
            if (!String.IsNullOrEmpty(textBoxEmID.Text.Trim()))
            {
                // 顧客IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxEmID.Text.Trim()))
                {
                    //MessageBox.Show("顧客IDは全て半角数字入力です");
                    messageDsp.DspMsg("M1001");
                    textBoxEmID.Focus();
                    return false;
                }
                // 顧客IDの文字数チェック
                if (textBoxEmID.TextLength > 6)
                {
                    //MessageBox.Show("顧客IDは6文字までです");
                    messageDsp.DspMsg("M1002");
                    textBoxEmID.Focus();
                    return false;
                }
            }

            // 顧客名入力時チェック
            if (!String.IsNullOrEmpty(textBoxEmName.Text.Trim()))
            {
                // 顧客名の全角チェック
                if (!dataInputFormCheck.CheckFullWidth(textBoxEmName.Text.Trim()))
                {
                    //MessageBox.Show("顧客名は全て全角入力です");
                    messageDsp.DspMsg("M1005");
                    textBoxEmName.Focus();
                    return false;
                }
                // 顧客名の文字数チェック
                if (textBoxEmName.TextLength > 50)
                {
                    //MessageBox.Show("顧客名は50文字以下です");
                    messageDsp.DspMsg("M1006");
                    textBoxEmName.Focus();
                    return false;
                }
            }

            // 管理フラグの適否
            if (checkBoxEmFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("管理フラグが不確定の状態です");
                messageDsp.DspMsg("M1008");
                checkBoxEmFlag.Focus();
                return false;
            }
            return true;

        }

        ///////////////////////////////
        //　8.2.4.2 顧客情報抽出
        //メソッド名：GenerateDataAtSelect()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：顧客情報の取得
        ///////////////////////////////

        public static string mEmID; //mClIDを別クラス(SalesOfficeDataAccess)でも使用できるように定義
        public static string mSoID;
        public static string mEmName;
        public static bool mEmFlg;
        private void GenerateDataAtSelect()
        {
            M_Employee selectCondition;

            mEmID = textBoxEmID.Text.Trim(); //IDはnullではなく、""で空白が入るのでstringで代入
            mSoID = textBoxSoID.Text.Trim();
            mEmName = textBoxEmName.Text.Trim();
            mEmFlg = checkBoxEmFlag.Checked;

            if (mEmID != "") //IDで検索
            {
                // 検索条件のセット
                selectCondition = new M_Employee()
                {
                    EmID = int.Parse(textBoxEmID.Text.Trim()),
                    EmFlag = EmFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // 営業所データの抽出
                Employee = employeeDataAccess.GetEmployeeData(selectCondition);
                return;
            }
            else if (mEmName != "") // 名前で検索
            {
                // 検索条件のセット
                selectCondition = new M_Employee()
                {
                    EmName = textBoxEmName.Text.Trim(),
                    EmFlag = EmFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // 営業所データの抽出
                Employee = employeeDataAccess.GetEmployeeData(selectCondition);
                return;
            }
            else if (mSoID != "") // 名前で検索
            {
                // 検索条件のセット
                selectCondition = new M_Employee()
                {
                    SoID = int.Parse(textBoxSoID.Text.Trim()),
                    EmFlag = EmFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // 営業所データの抽出
                Employee = employeeDataAccess.GetEmployeeData(selectCondition);
                return;
            }
            else if (mEmFlg == true) // ただの非表示一覧表示
            {
                // 検索条件のセット
                selectCondition = new M_Employee()
                {
                    EmFlag = EmFlg //フラグがチェック状態なら非表示一覧表示内で検索、そうでなければ通常検索
                };
                // 営業所データの抽出
                Employee = employeeDataAccess.GetEmployeeData(selectCondition);
                return;
            }
        }

        ///////////////////////////////
        //　8.2.4.3 顧客抽出結果表示
        //メソッド名：SetSelectData()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：顧客情報の表示
        ///////////////////////////////
        private void SetSelectData()
        {
            textBoxPageNo.Text = "1";

            int pageSize = int.Parse(textBoxPageSize.Text);

            dataGridViewEmployee.DataSource = Employee;

            labelPage.Text = "/" + ((int)Math.Ceiling(Employee.Count / (double)pageSize)) + "ページ";
            dataGridViewEmployee.Refresh();

            if (Employee.Count == 0) //検索結果のデータ数が0ならエラー
            {
                messageDsp.DspMsg("M1025");
                SetFormDataGridView();
            }
        }

        private void buttonRegist_Click(object sender, EventArgs e)
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

            // 営業所IDの適否
            if (!String.IsNullOrEmpty(textBoxSoID.Text.Trim()))
            {
                // 営業所IDの半角数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxSoID.Text.Trim()))
                {
                    //MessageBox.Show("営業所IDは全て半角数字入力です");
                    messageDsp.DspMsg("M1001");
                    textBoxSoID.Focus();
                    return false;
                }
                // 営業所IDの文字数チェック
                if (textBoxSoID.TextLength > 2)
                {
                    //MessageBox.Show("営業所IDは2文字までです");
                    messageDsp.DspMsg("M1002");
                    textBoxSoID.Focus();
                    return false;
                }

                // 営業所IDが0ではないかチェック
                if (int.Parse(textBoxSoID.Text.Trim()) == 0)
                {
                    //MessageBox.Show("営業所IDは01から割り当ててください");
                    messageDsp.DspMsg("M1024");
                    textBoxSoID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("営業所IDが入力されていません");
                messageDsp.DspMsg("M1004");
                textBoxSoID.Focus();
                return false;
            }

            // 社員名の適否
            if (!String.IsNullOrEmpty(textBoxEmName.Text.Trim()))
            {
                // 顧客名の全角チェック
                if (!dataInputFormCheck.CheckFullWidth(textBoxEmName.Text.Trim()))
                {
                    //MessageBox.Show("顧客名は全て全角入力です");
                    messageDsp.DspMsg("M1005");
                    textBoxEmName.Focus();
                    return false;
                }
                // 顧客名の文字数チェック
                if (textBoxEmName.TextLength > 50)
                {
                    //MessageBox.Show("顧客名は25文字以下です");
                    messageDsp.DspMsg("M1006");
                    textBoxEmName.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("顧客名が入力されていません");
                messageDsp.DspMsg("M1007");
                textBoxEmName.Focus();
                return false;
            }

            // 電話番号の半角数値チェック
            if (!String.IsNullOrEmpty(textBoxEmPhone.Text.Trim()))
            {
                if (!dataInputFormCheck.CheckNumeric(textBoxEmPhone.Text.Trim()))
                {
                    //MessageBox.Show("電話番号は半角数値です");
                    messageDsp.DspMsg("M3030");
                    textBoxEmPhone.Focus();
                    return false;
                }
                // 電話番号の文字数チェック
                if (textBoxEmPhone.TextLength > 13)
                {
                    //MessageBox.Show("電話番号は13文字以下です");
                    messageDsp.DspMsg("M3011");
                    textBoxEmPhone.Focus();
                    return false;
                }
            }

            // ログインPWの未入力チェック
            if (!String.IsNullOrEmpty(textBoxEmPassword.Text.Trim()))
            {
                // ログインPWの半角英数字チェック
                if (!dataInputFormCheck.CheckHalfAlphabetNumeric(textBoxEmPassword.Text.Trim()))
                {
                    //MessageBox.Show("ログインPWは全て半角英数字入力です");
                    messageDsp.DspMsg("M3044");
                    textBoxEmPassword.Focus();
                    return false;
                }
                // ログインPWの文字数チェック
                if (textBoxEmPassword.TextLength > 13)
                {
                    //MessageBox.Show("ログインPWは13文字以下です");
                    messageDsp.DspMsg("M3045");
                    textBoxEmPassword.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("ログインPWが入力されていません");
                messageDsp.DspMsg("M3046");
                textBoxEmPassword.Focus();
                return false;
            }

            // 管理フラグの適否
            if (checkBoxEmFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("管理フラグが不確定の状態です");
                messageDsp.DspMsg("M1008");
                checkBoxEmFlag.Focus();
                return false;
            }

            // 管理フラグの適否２
            if (checkBoxEmFlag.Checked == true)
            {
                //MessageBox.Show("登録では管理フラグは適用されません");
                messageDsp.DspMsg("M1022");
                checkBoxEmFlag.Focus();
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
            DateTime? mEmDate;
            if (dateTimePickerEmHiredate.Checked == false)
            {
                mEmDate = null;
            }
            else
            {
                mEmDate = DateTime.Parse(dateTimePickerEmHiredate.Text);
            }

            string pw = passWordHash.CreatePasswordHash(textBoxEmPassword.Text.Trim());
            return new M_Employee
            {
                EmID = int.Parse(textBoxEmID.Text.Trim()),
                EmName = textBoxEmName.Text.Trim(),
                SoID = int.Parse(textBoxSoID.Text.Trim()),
                PoID = int.Parse(textBoxPoID.Text.Trim()),
                EmHiredate = (DateTime)mEmDate,
                EmPassword = pw,
                EmPhone = textBoxEmPhone.Text.Trim(),
                EmFlag = EmFlg,
                EmHidden = textBoxEmHidden.Text.Trim()
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
            DialogResult result = messageDsp.DspMsg("M1010");
            if (result == DialogResult.Cancel)
                return;

            // 顧客情報の登録
            bool flg = employeeDataAccess.AddEmployeeData(regEmployee);
            if (flg == true)
                //MessageBox.Show("データを登録しました。");
                messageDsp.DspMsg("M1011");
            else
                //MessageBox.Show("データの登録に失敗しました。");
                messageDsp.DspMsg("M1012");

            textBoxEmID.Focus();

            // 入力エリアのクリア
            ClearInput();

            // データグリッドビューの表示
            GetDataGridView();

        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            // 8.2.2.1 妥当な顧客データ取得
            if (!GetValidDataAtUpdate())
                return;

            // 8.2.2.2 顧客情報作成
            var updEmployeet = GenerateDataAtUpdate();

            // 8.2.2.3 顧客情報更新
            UpdateEmployee(updEmployeet);
        }

        ///////////////////////////////
        //　8.2.2.1 妥当な顧客データ取得
        //メソッド名：GetValidDataAtUpdate()
        //引　数   ：なし
        //戻り値   ：true or false
        //機　能   ：入力データの形式チェック
        //          ：エラーがない場合True
        //          ：エラーがある場合False
        ///////////////////////////////
        private bool GetValidDataAtUpdate()
        {

            // 顧客IDの未入力チェック
            if (!String.IsNullOrEmpty(textBoxEmID.Text.Trim()))
            {
                // 顧客IDの半角英数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxEmID.Text.Trim()))
                {
                    //MessageBox.Show("顧客IDは全て半角英数字入力です");
                    messageDsp.DspMsg("M1001");
                    textBoxEmID.Focus();
                    return false;
                }
                // 顧客IDの文字数チェック
                if (textBoxEmID.TextLength > 6)
                {
                    //MessageBox.Show("顧客IDは2文字です");
                    messageDsp.DspMsg("M1002");
                    textBoxEmID.Focus();
                    return false;
                }
                // 顧客IDの存在チェック
                if (!employeeDataAccess.CheckEmployeeCDExistence(textBoxEmID.Text.Trim()))
                {
                    //MessageBox.Show("入力された顧客IDは存在しません");
                    messageDsp.DspMsg("M1013");
                    textBoxEmID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("顧客IDが入力されていません");
                messageDsp.DspMsg("M1004");
                textBoxEmID.Focus();
                return false;
            }

            // 営業所IDの未入力チェック
            if (!String.IsNullOrEmpty(textBoxSoID.Text.Trim()))
            {
                // 営業所IDの半角英数字チェック
                if (!dataInputFormCheck.CheckNumeric(textBoxSoID.Text.Trim()))
                {
                    //MessageBox.Show("営業所IDは全て半角英数字入力です");
                    messageDsp.DspMsg("M3001");
                    textBoxSoID.Focus();
                    return false;
                }
                // 営業所IDの文字数チェック
                if (textBoxSoID.TextLength > 2)
                {
                    //MessageBox.Show("営業所IDは2文字です");
                    messageDsp.DspMsg("M3002");
                    textBoxSoID.Focus();
                    return false;
                }
                // 営業所IDの存在チェック
                if (!employeeDataAccess.CheckEmployeeCDExistence(textBoxSoID.Text.Trim()))
                {
                    //MessageBox.Show("入力された営業所IDは存在しません");
                    messageDsp.DspMsg("M3017");
                    textBoxSoID.Focus();
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("営業所IDが入力されていません");
                messageDsp.DspMsg("M3004");
                textBoxSoID.Focus();
                return false;
            }

            // 顧客名の適否
            if (!String.IsNullOrEmpty(textBoxEmName.Text.Trim()))
            {
                // 顧客名の全角チェック
                if (!dataInputFormCheck.CheckFullWidth(textBoxEmName.Text.Trim()))
                {
                    //MessageBox.Show("顧客名は全て全角入力です");
                    messageDsp.DspMsg("M1005");
                    textBoxEmName.Focus();
                    return false;
                }
                // 顧客名の文字数チェック
                if (textBoxEmName.TextLength > 50)
                {
                    //MessageBox.Show("顧客名は25文字以下です");
                    messageDsp.DspMsg("M1006");
                    textBoxEmName.Focus();
                    return false;
                }


            }

            // 電話番号の半角数値チェック
            if (!String.IsNullOrEmpty(textBoxEmPhone.Text.Trim()))
            {
                if (!dataInputFormCheck.CheckNumeric(textBoxEmPhone.Text.Trim()))
                {
                    //MessageBox.Show("電話番号は半角数値です");
                    messageDsp.DspMsg("M3030");
                    textBoxEmPhone.Focus();
                    return false;
                }
                // 電話番号の文字数チェック
                if (textBoxEmPhone.TextLength > 13)
                {
                    //MessageBox.Show("電話番号は13文字以下です");
                    messageDsp.DspMsg("M3011");
                    textBoxEmPhone.Focus();
                    return false;
                }
                // 顧客IDの存在チェック
                if (!employeeDataAccess.CheckEmployeeCDExistence(textBoxEmID.Text.Trim()))
                {
                    //MessageBox.Show("入力された顧客IDは存在しません");
                    messageDsp.DspMsg("M1013");
                    textBoxEmID.Focus();
                    return false;
                }
            }

            // 管理フラグの適否
            if (checkBoxEmFlag.CheckState == CheckState.Indeterminate)
            {
                //MessageBox.Show("管理フラグが不確定の状態です");
                messageDsp.DspMsg("M1008");
                checkBoxEmFlag.Focus();
                return false;
            }

            // 非表示理由の適否
            if (checkBoxEmFlag.Checked == true)
            {
                if (textBoxEmHidden.Text == "")
                {
                    //MessageBox.Show("非表示理由の入力が必要です");
                    messageDsp.DspMsg("M1023");
                    textBoxEmHidden.Focus();
                    return false;
                }
            }
            return true;
        }

        ///////////////////////////////
        //　8.2.2.2 顧客情報作成
        //メソッド名：GenerateDataAtUpdate()
        //引　数   ：なし
        //戻り値   ：顧客更新情報
        //機　能   ：更新データのセット
        ///////////////////////////////
        private M_Employee GenerateDataAtUpdate()
        {

            DateTime? mEmDate;
            if (dateTimePickerEmHiredate.Checked == false)
                mEmDate = null;
            else
                mEmDate = DateTime.Parse(dateTimePickerEmHiredate.Text);

            // パスワード入力時、パスワードハッシュ化
            string pw = "";
            if (!String.IsNullOrEmpty(textBoxEmPassword.Text.Trim()))
                pw = passWordHash.CreatePasswordHash(textBoxEmPassword.Text.Trim());
            return new M_Employee

            {
                EmID = int.Parse(textBoxEmID.Text.Trim()),
                EmName = textBoxEmName.Text.Trim(),
                SoID = int.Parse(textBoxSoID.Text.Trim()),
                PoID = int.Parse(textBoxPoID.Text.Trim()),
                EmHiredate = (DateTime)mEmDate,
                EmPassword = pw,
                EmPhone = textBoxEmPhone.Text.Trim(),
                EmHidden = textBoxEmHidden.Text.Trim(),
                EmFlag = EmFlg
            };
        }
        ///////////////////////////////
        //　8.2.2.3 顧客情報更新
        //メソッド名：UpdateEmployee()
        //引　数   ：顧客情報
        //戻り値   ：なし
        //機　能   ：顧客情報の更新
        ///////////////////////////////
        private void UpdateEmployee(M_Employee updEmployee)
        {
            if (EmFlg == 0)
            {
                // 更新確認メッセージ
                DialogResult result = messageDsp.DspMsg("M1014");
                if (result == DialogResult.Cancel)
                    return;

                // 顧客情報の更新
                bool flg = employeeDataAccess.UpdateEmployeeData(updEmployee);
                if (flg == true)
                    //MessageBox.Show("データを更新しました。");
                    messageDsp.DspMsg("M1015");
                else
                    //MessageBox.Show("データの更新に失敗しました。");
                    messageDsp.DspMsg("M1016");

                textBoxEmID.Focus();

                // 入力エリアのクリア
                ClearInput();

                // データグリッドビューの表示
                GetDataGridView();
            }

            else if (EmFlg == 2)
            {
                DeleteEmployee(updEmployee);
            }
        }
        private void DeleteEmployee(M_Employee delEmployee)
        {

            // 更新確認メッセージ
            DialogResult result = messageDsp.DspMsg("M1018");
            if (result == DialogResult.Cancel)
                return;

            // 顧客情報の更新
            bool flg = employeeDataAccess.DeleteEmployeeData(delEmployee);
            if (flg == true)
                //MessageBox.Show("データを削除しました。");
                messageDsp.DspMsg("M1019");
            else
                //MessageBox.Show("データの削除に失敗しました。");
                messageDsp.DspMsg("M1020");

            textBoxEmID.Focus();

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

        private void buttonClear_Click(object sender, EventArgs e)
        {
            // 入力エリアのクリア
            ClearInput();

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

        ///////////////////////////////
        //メソッド名：ClearInput()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：入力エリアをクリア
        ///////////////////////////////
        private void ClearInput()
        {
            textBoxEmID.Text = "";
            textBoxPoID.Text = "";
            textBoxSoID.Text = "";
            dateTimePickerEmHiredate.Value = DateTime.Now;
            dateTimePickerEmHiredate.Checked = false;
            textBoxEmName.Text = "";
            textBoxEmPhone.Text = "";
            textBoxEmPassword.Text = "";
            textBoxEmHidden.Text = "";
            checkBoxEmFlag.Checked = false;
        }

        private void dataGridViewEmployee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //クリックされた行データをテキストボックスへ
            textBoxEmID.Text = dataGridViewEmployee.Rows[dataGridViewEmployee.CurrentRow.Index].Cells[0].Value.ToString();
            textBoxEmName.Text = dataGridViewEmployee.Rows[dataGridViewEmployee.CurrentRow.Index].Cells[1].Value.ToString();
            textBoxSoID.Text = dataGridViewEmployee.Rows[dataGridViewEmployee.CurrentRow.Index].Cells[2].Value.ToString();
            textBoxPoID.Text = dataGridViewEmployee.Rows[dataGridViewEmployee.CurrentRow.Index].Cells[3].Value.ToString();
            //日付が設定されていない場合、初期値として現在の日付を設定
            if (dataGridViewEmployee.Rows[dataGridViewEmployee.CurrentRow.Index].Cells[4].Value == null)
            {
                dateTimePickerEmHiredate.Value = DateTime.Now;
                dateTimePickerEmHiredate.Checked = false;
            }
            else
            {
                dateTimePickerEmHiredate.Text = dataGridViewEmployee.Rows[dataGridViewEmployee.CurrentRow.Index].Cells[4].Value.ToString();
            }
            //Cell[5]はパスワード(Visible = false)のためスキップ
            textBoxEmPhone.Text = dataGridViewEmployee.Rows[dataGridViewEmployee.CurrentRow.Index].Cells[6].Value.ToString();
            int EmFlg2 = int.Parse(dataGridViewEmployee.Rows[dataGridViewEmployee.CurrentRow.Index].Cells[7].Value.ToString());
            if (EmFlg2 == 0)
            {
                checkBoxEmFlag.Checked = false;
            }
            else if (EmFlg2 == 2)
            {
                checkBoxEmFlag.Checked = true;
            }
            textBoxEmHidden.Text = dataGridViewEmployee.Rows[dataGridViewEmployee.CurrentRow.Index].Cells[8].Value.ToString();
        }

        private void checkBoxEmFlag_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxEmFlag.Checked == true)
            {
                EmFlg = 2;
                textBoxEmHidden.Enabled = true;
                return;
            }
            else if (checkBoxEmFlag.Checked == false)
            {
                EmFlg = 0;
                textBoxEmHidden.Enabled = false;
                textBoxEmHidden.Text = "";
                return;
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

        private void textBoxPoID_TextChanged(object sender, EventArgs e)
        {
            if (textBoxPoID.Text != "")
            {
                var context = new SalesManagement_DevContext();
                try
                {
                    int mPoID = int.Parse(textBoxPoID.Text);
                    var mPosition = context.M_Positions.Single(x => x.PoID == mPoID);
                    if (mPosition.PoFlag == 2)
                    {
                        string mPoName = mPosition.PoName;
                        labelPoName.Text = "(非表示)" + mPoName;
                        labelPoName.Visible = true;
                        context.Dispose();
                    }
                    else
                    {
                        string mPoName = mPosition.PoName;
                        labelPoName.Text = mPoName;
                        labelPoName.Visible = true;
                        context.Dispose();
                    }
                }
                catch
                {
                    labelPoName.Visible = true;
                    labelPoName.Text = "“UnknownID”";
                    context.Dispose();
                    return;
                }
            }
            else
            {
                labelPoName.Visible = false;
                labelPoName.Text = "営業所名";
            }
        }

        private void buttonPageSizeChange_Click(object sender, EventArgs e)
        {
            SetDataGridView();
        }

        private void buttonFirstPage_Click(object sender, EventArgs e)
        {
            int pageSize = int.Parse(textBoxPageSize.Text);
            dataGridViewEmployee.DataSource = Employee.Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewEmployee.Refresh();
            //ページ番号の設定
            textBoxPageNo.Text = "1";
        }

        private void buttonPreviousPage_Click(object sender, EventArgs e)
        {
            int pageSize = int.Parse(textBoxPageSize.Text);
            int pageNo = int.Parse(textBoxPageNo.Text) - 2;
            dataGridViewEmployee.DataSource = Employee.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewEmployee.Refresh();
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
            int lastNo = (int)Math.Ceiling(Employee.Count / (double)pageSize) - 1;
            //最終ページでなければ
            if (pageNo <= lastNo)
                dataGridViewEmployee.DataSource = Employee.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewEmployee.Refresh();
            //ページ番号の設定
            int lastPage = (int)Math.Ceiling(Employee.Count / (double)pageSize);
            if (pageNo >= lastPage)
                textBoxPageNo.Text = lastPage.ToString();
            else
                textBoxPageNo.Text = (pageNo + 1).ToString();
        }

        private void buttonLastPage_Click(object sender, EventArgs e)
        {
            int pageSize = int.Parse(textBoxPageSize.Text);
            //最終ページの計算
            int pageNo = (int)Math.Ceiling(Employee.Count / (double)pageSize) - 1;
            dataGridViewEmployee.DataSource = Employee.Skip(pageSize * pageNo).Take(pageSize).ToList();

            // DataGridViewを更新
            dataGridViewEmployee.Refresh();
            //ページ番号の設定
            textBoxPageNo.Text = (pageNo + 1).ToString();
        }
    }
}
