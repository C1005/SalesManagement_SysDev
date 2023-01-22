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
    public partial class F_menu : Form
    {
        //他のフォームから変数の内容を共有できるように宣言
        internal static string loginName = "";
        internal static string loginSalesOffice = "";
        internal static string loginEmID = "";
        Image backgroundImage_Master;
        Image backgroundImage_NonMaster;
        Image backgroundImage_NonMaster_Stcok;

        public F_menu()
        {
            InitializeComponent();
        }

        private void F_menu_Load(object sender, EventArgs e)
        {

        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            if (buttonLogout.Text == "ログイン")
                OpenForm(((Button)sender).Text);
            else
            {
                //ログアウト確認メッセージ
                DialogResult result = MessageBox.Show("ログアウトしてもよろしいですか？", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    F_Login.SysMode = 0;
                    // 全てのフォームを取得する
                    FormCollection forms = Application.OpenForms;
                    List<Form> formList = new List<Form>(forms.OfType<Form>());
                    foreach (Form form in formList)
                    {
                        if (form.Name != "F_menu")
                        {
                            F_menu.loginEmID = "";
                            F_menu.loginName = "";
                            F_menu.loginSalesOffice = "";
                            form.Close();
                        }
                    }

                    //ログアウト後、すべてのボタンを使用不可
                    SetEnable(false);

                    //ログアウトしたら一旦グループボックスを閉じる
                    if (groupBoxClient.Visible == true)
                    {
                        //既に開いているなら元に戻す処理
                        groupBoxClient.Visible = false;
                        buttonClientManager.Text = "顧客マスタ";
                        buttonClientManager.BackgroundImage = Properties.Resources.Fixed_Menu_ログイン制限;
                        return;
                    }
                    if (groupBoxProduct.Visible == true)
                    {
                        //既に開いているなら元に戻す処理
                        groupBoxProduct.Visible = false;
                        buttonProductManager.Text = "商品マスタ";
                        buttonProductManager.BackgroundImage = Properties.Resources.Fixed_Menu_ログイン制限;
                        return;
                    }
                    if (groupBoxEmployee.Visible == true)
                    {
                        //既に開いているなら元に戻す処理
                        groupBoxEmployee.Visible = false;
                        buttonEmployeeManager.Text = "社員マスタ";
                        buttonEmployeeManager.BackgroundImage = Properties.Resources.Fixed_Menu_ログイン制限;
                        return;
                    }
                }
            }
        }

        private void buttonClientManager_Click(object sender, EventArgs e)
        {
            //顧客マスターのメニュー表示されてるか判断
            if (groupBoxClient.Visible == true)
            {
                //既に開いているなら元に戻す処理
                panel1.Enabled = true; //tabが移動しないようにする
                groupBoxClient.Visible = false;
                buttonClientManager.Text = "顧客マスタ";
                buttonClientManager.BackgroundImage = Properties.Resources.Fixed_マスタ用ボタン;
                return;
            }

            //マスタメニュー表示
            groupBoxClient.Visible = true;
            buttonClientManager.Text = "閉じる";
            buttonClientManager.BackgroundImage = Properties.Resources.Fixed_マスタ用ボタン_閉じる;
            groupBoxClient.BringToFront(); //背面から最前面に移動

            //別のボタンを元に戻す
            panel1.Enabled = false; //tabが移動しないようにする
            groupBoxProduct.Visible = false;
            groupBoxEmployee.Visible = false;
            buttonProductManager.Text = "商品マスタ";
            buttonEmployeeManager.Text = "社員マスタ";
            buttonProductManager.BackgroundImage = Properties.Resources.Fixed_マスタ用ボタン;
            buttonEmployeeManager.BackgroundImage = Properties.Resources.Fixed_マスタ用ボタン;
        }

        private void buttonProductManager_Click(object sender, EventArgs e)
        {
            //商品マスターのメニュー表示されてるか判断
            if (groupBoxProduct.Visible == true)
            {
                //既に開いているなら元に戻す処理
                panel1.Enabled = true; //tabが移動しないようにする
                groupBoxProduct.Visible = false;
                buttonProductManager.Text = "商品マスタ";
                buttonProductManager.BackgroundImage = Properties.Resources.Fixed_マスタ用ボタン;
                return;
            }

            //マスタメニュー表示
            groupBoxProduct.Visible = true;
            buttonProductManager.Text = "閉じる";
            buttonProductManager.BackgroundImage = Properties.Resources.Fixed_マスタ用ボタン_閉じる;
            groupBoxProduct.BringToFront(); //背面から最前面に移動

            //別のボタンを元に戻す
            panel1.Enabled = false; //tabが移動しないようにする
            groupBoxClient.Visible = false;
            groupBoxEmployee.Visible = false;
            buttonClientManager.Text = "顧客マスタ";
            buttonEmployeeManager.Text = "社員マスタ";
            buttonClientManager.BackgroundImage = Properties.Resources.Fixed_マスタ用ボタン;
            buttonEmployeeManager.BackgroundImage = Properties.Resources.Fixed_マスタ用ボタン;
        }

        private void buttonEmployeeManager_Click(object sender, EventArgs e)
        {
            //社員マスターのメニュー表示されてるか判断
            if (groupBoxEmployee.Visible == true)
            {
                //既に開いているなら元に戻す処理
                panel1.Enabled = true; //tabが移動しないようにする
                groupBoxEmployee.Visible = false;
                buttonEmployeeManager.Text = "社員マスタ";
                buttonEmployeeManager.BackgroundImage = Properties.Resources.Fixed_マスタ用ボタン;
                return;
            }

            //マスタメニュー表示
            groupBoxEmployee.Visible = true;
            buttonEmployeeManager.Text = "閉じる";
            buttonEmployeeManager.BackgroundImage = Properties.Resources.Fixed_マスタ用ボタン_閉じる;
            groupBoxEmployee.BringToFront(); //背面から最前面に移動

            //別のボタンを元に戻す
            panel1.Enabled = false; //tabが移動しないようにする
            groupBoxClient.Visible = false;
            groupBoxProduct.Visible = false;
            buttonClientManager.Text = "顧客マスタ";
            buttonProductManager.Text = "商品マスタ";
            buttonClientManager.BackgroundImage = Properties.Resources.Fixed_マスタ用ボタン;
            buttonProductManager.BackgroundImage = Properties.Resources.Fixed_マスタ用ボタン;
        }

        private void buttonStockManager_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private void buttonSaleManager_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private void buttonWarehousingManager_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private void buttonHattyuManager_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private void buttonOrderManager_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private void buttonChumonManager_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private void buttonSyukkoManager_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private void buttonArrivalManager_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private void buttonShipmentManager_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private void OpenForm(string formName)
        {
            Form frm = new Form();
            //引数より、開くフォームを設定
            switch (formName)
            {
                case "ログイン": //ボタンのテキスト名
                    frm = new F_Login(); //フォームの名前
                    break;

                case "顧客管理": //ボタンのテキスト名
                    frm = new Forms.Master.FormClient.F_Client(); //フォームの名前
                    break;
                case "営業所管理": 
                    frm = new Forms.Master.FormEmployee.F_SalesOffice(); //フォームの名前
                    break;
                case "商品管理":
                    frm = new Forms.Master.FormProduct.F_Product(); //フォルダも指定する必要がある
                    break;
                case "メーカ管理": 
                    frm = new Forms.Master.FormProduct.F_Maker();
                    break;
                case "大分類管理":
                    frm = new Forms.Master.FormProduct.F_MajorCassification();
                    break;
                case "小分類管理":
                    frm = new Forms.Master.FormProduct.F_SmallClassification();
                    break;
                case "在庫管理":
                    frm = new Forms.Master.FormStock.F_Stock();
                    break;
                case "社員管理":
                    frm = new Forms.Master.FormEmployee.F_Employee();
                    break;
                case "役職管理":
                    frm = new Forms.Master.FormEmployee.F_Position();
                    break;

                case "受注管理":
                    frm = new Forms.NonMaster.FormOrder.F_Order();
                    break;
                case "注文管理":
                    frm = new Forms.NonMaster.FormChumon.F_Chumon();
                    break;
                case "入庫管理":
                    frm = new Forms.NonMaster.FormWarehousing.F_Warehousing();
                    break;
                case "出庫管理":
                    frm = new Forms.NonMaster.FormSyukko.F_Syukko();
                    break;
                case "入荷管理":
                    frm = new Forms.NonMaster.FormArrival.F_Arrival();
                    break;
                case "出荷管理":
                    frm = new Forms.NonMaster.FormShipment.F_Shipment();
                    break;
                case "売上管理":
                    frm = new Forms.NonMaster.FormSale.F_Sale();
                    break;
                case "発注管理":
                    frm = new Forms.NonMaster.FormHattyu.F_Hattyu();
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

            ////フォームを透明化
            //Opacity = 0; //(透明化だけだとトップに戻るときに何もない状態になる)

            ////選択されたフォームを開く
            //frm.ShowDialog();

            ////開いたフォームから戻ってきたら
            ////メモリを解放する
            //Opacity = 100;
            //frm.Dispose();
        }

        ///////////////////////////////
        //メソッド名：SetEnable()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：ログイン状態でメニューのEnable設定
        //          ：ログイン中は、メニュー利用可
        //          ：ログアウト中は、メニュー利用不可
        ///////////////////////////////
        private void SetEnable(bool flg)
        {
            if (loginSalesOffice == "本社")
            {
                //制限によってボタンの色を判断
                ButtonLimit(flg);

                buttonClientManager.BackgroundImage = backgroundImage_Master;
                buttonEmployeeManager.BackgroundImage = backgroundImage_Master;
                buttonProductManager.BackgroundImage = backgroundImage_Master;

                buttonStockManager.BackgroundImage = backgroundImage_NonMaster_Stcok;
                buttonArrivalManager.BackgroundImage = backgroundImage_NonMaster;
                buttonChumonManager.BackgroundImage = backgroundImage_NonMaster;
                buttonHattyuManager.BackgroundImage = backgroundImage_NonMaster_Stcok;
                buttonOrderManager.BackgroundImage = backgroundImage_NonMaster;
                buttonSaleManager.BackgroundImage = backgroundImage_NonMaster;
                buttonShipmentManager.BackgroundImage = backgroundImage_NonMaster;
                buttonSyukkoManager.BackgroundImage = backgroundImage_NonMaster;
                buttonWarehousingManager.BackgroundImage = backgroundImage_NonMaster_Stcok;

                //ボタンのEnable設定
                buttonClientManager.Enabled = flg;
                buttonEmployeeManager.Enabled = flg;
                buttonProductManager.Enabled = flg;

                buttonStockManager.Enabled = flg;
                buttonArrivalManager.Enabled = flg;
                buttonChumonManager.Enabled = flg;
                buttonHattyuManager.Enabled = flg;
                buttonOrderManager.Enabled = flg;
                buttonSaleManager.Enabled = flg;
                buttonShipmentManager.Enabled = flg;
                buttonSyukkoManager.Enabled = flg;
                buttonWarehousingManager.Enabled = flg;
            }
            else if (loginSalesOffice.Contains("倉庫") && loginSalesOffice != null)
            {
                //制限によってボタンの色を判断
                ButtonLimit(flg);

                buttonClientManager.BackgroundImage = Properties.Resources.Fixed_Menu_ログイン制限;
                buttonEmployeeManager.BackgroundImage = Properties.Resources.Fixed_Menu_ログイン制限;
                buttonProductManager.BackgroundImage = Properties.Resources.Fixed_Menu_ログイン制限;
                buttonArrivalManager.BackgroundImage = Properties.Resources.Fixed_Menu_ログイン制限;
                buttonChumonManager.BackgroundImage = Properties.Resources.Fixed_Menu_ログイン制限;
                buttonOrderManager.BackgroundImage = Properties.Resources.Fixed_Menu_ログイン制限;
                buttonShipmentManager.BackgroundImage = Properties.Resources.Fixed_Menu_ログイン制限;

                buttonStockManager.BackgroundImage = backgroundImage_NonMaster_Stcok;
                buttonSyukkoManager.BackgroundImage = backgroundImage_NonMaster;
                buttonWarehousingManager.BackgroundImage = backgroundImage_NonMaster_Stcok;
                buttonHattyuManager.BackgroundImage = backgroundImage_NonMaster_Stcok;
                buttonSaleManager.BackgroundImage = backgroundImage_NonMaster;

                //ボタンのEnable設定
                buttonClientManager.Enabled = false;
                buttonEmployeeManager.Enabled = false;
                buttonProductManager.Enabled = false;
                buttonArrivalManager.Enabled = false;
                buttonChumonManager.Enabled = false;
                buttonOrderManager.Enabled = false;
                buttonShipmentManager.Enabled = false;

                buttonSaleManager.Enabled = flg;
                buttonStockManager.Enabled = flg;
                buttonSyukkoManager.Enabled = flg;
                buttonWarehousingManager.Enabled = flg;
                buttonHattyuManager.Enabled = flg;
            }
            else if (loginSalesOffice.Contains("営業所") && loginSalesOffice != null)
            {
                //制限によってボタンの色を判断
                ButtonLimit(flg);

                buttonClientManager.BackgroundImage = Properties.Resources.Fixed_Menu_ログイン制限;
                buttonEmployeeManager.BackgroundImage = Properties.Resources.Fixed_Menu_ログイン制限;
                buttonProductManager.BackgroundImage = Properties.Resources.Fixed_Menu_ログイン制限;
                buttonHattyuManager.BackgroundImage = Properties.Resources.Fixed_Menu_ログイン制限;
                buttonWarehousingManager.BackgroundImage = Properties.Resources.Fixed_Menu_ログイン制限;

                buttonStockManager.BackgroundImage = backgroundImage_NonMaster_Stcok;
                buttonArrivalManager.BackgroundImage = backgroundImage_NonMaster;
                buttonChumonManager.BackgroundImage = backgroundImage_NonMaster;
                buttonOrderManager.BackgroundImage = backgroundImage_NonMaster;
                buttonSaleManager.BackgroundImage = backgroundImage_NonMaster;
                buttonShipmentManager.BackgroundImage = backgroundImage_NonMaster;
                buttonSyukkoManager.BackgroundImage = backgroundImage_NonMaster;


                //ボタンのEnable設定
                buttonClientManager.Enabled = false;
                buttonEmployeeManager.Enabled = false;
                buttonHattyuManager.Enabled = false;
                buttonProductManager.Enabled = false;
                buttonWarehousingManager.Enabled = false;

                buttonSyukkoManager.Enabled = flg;
                buttonArrivalManager.Enabled = flg;
                buttonChumonManager.Enabled = flg;
                buttonOrderManager.Enabled = flg;
                buttonShipmentManager.Enabled = flg;
                buttonSaleManager.Enabled = flg;
                buttonStockManager.Enabled = flg;
            }
            else if (F_Login.SysMode == 1)
            {
                //制限によってボタンの色を判断
                ButtonLimit(flg);

                buttonClientManager.BackgroundImage = backgroundImage_Master;
                buttonEmployeeManager.BackgroundImage = backgroundImage_Master;
                buttonProductManager.BackgroundImage = backgroundImage_Master;

                buttonStockManager.BackgroundImage = backgroundImage_NonMaster_Stcok;
                buttonArrivalManager.BackgroundImage = backgroundImage_NonMaster;
                buttonChumonManager.BackgroundImage = backgroundImage_NonMaster;
                buttonHattyuManager.BackgroundImage = backgroundImage_NonMaster_Stcok;
                buttonOrderManager.BackgroundImage = backgroundImage_NonMaster;
                buttonSaleManager.BackgroundImage = backgroundImage_NonMaster;
                buttonShipmentManager.BackgroundImage = backgroundImage_NonMaster;
                buttonSyukkoManager.BackgroundImage = backgroundImage_NonMaster;
                buttonWarehousingManager.BackgroundImage = backgroundImage_NonMaster_Stcok;

                //ボタンのEnable設定
                buttonClientManager.Enabled = flg;
                buttonEmployeeManager.Enabled = flg;
                buttonProductManager.Enabled = flg;

                buttonStockManager.Enabled = flg;
                buttonArrivalManager.Enabled = flg;
                buttonChumonManager.Enabled = flg;
                buttonHattyuManager.Enabled = flg;
                buttonOrderManager.Enabled = flg;
                buttonSaleManager.Enabled = flg;
                buttonShipmentManager.Enabled = flg;
                buttonSyukkoManager.Enabled = flg;
                buttonWarehousingManager.Enabled = flg;
            }
            else
            {
                //制限によってボタンの色を判断
                buttonClientManager.BackgroundImage = Properties.Resources.Fixed_Menu_ログイン制限;
                buttonEmployeeManager.BackgroundImage = Properties.Resources.Fixed_Menu_ログイン制限;
                buttonProductManager.BackgroundImage = Properties.Resources.Fixed_Menu_ログイン制限;

                buttonStockManager.BackgroundImage = Properties.Resources.Fixed_Menu_ログイン制限;
                buttonArrivalManager.BackgroundImage = Properties.Resources.Fixed_Menu_ログイン制限;
                buttonChumonManager.BackgroundImage = Properties.Resources.Fixed_Menu_ログイン制限;
                buttonHattyuManager.BackgroundImage = Properties.Resources.Fixed_Menu_ログイン制限;
                buttonOrderManager.BackgroundImage = Properties.Resources.Fixed_Menu_ログイン制限;
                buttonSaleManager.BackgroundImage = Properties.Resources.Fixed_Menu_ログイン制限;
                buttonShipmentManager.BackgroundImage = Properties.Resources.Fixed_Menu_ログイン制限;
                buttonSyukkoManager.BackgroundImage = Properties.Resources.Fixed_Menu_ログイン制限;
                buttonWarehousingManager.BackgroundImage = Properties.Resources.Fixed_Menu_ログイン制限;

                //ボタンのEnable設定
                buttonClientManager.Enabled = false;
                buttonEmployeeManager.Enabled = false;
                buttonProductManager.Enabled = false;
                buttonStockManager.Enabled = false;

                buttonArrivalManager.Enabled = false;
                buttonChumonManager.Enabled = false;
                buttonHattyuManager.Enabled = false;
                buttonOrderManager.Enabled = false;
                buttonSaleManager.Enabled = false;
                buttonShipmentManager.Enabled = false;
                buttonSyukkoManager.Enabled = false;
                buttonWarehousingManager.Enabled = false;
            }

            if (flg == true)
            {
                buttonLogout.BackgroundImage.Dispose();
                buttonLogout.BackgroundImage = Properties.Resources.Fixed_ログアウト;
                buttonLogout.Text = "ログアウト";
            }
            else
            {
                buttonLogout.BackgroundImage.Dispose();
                buttonLogout.BackgroundImage = Properties.Resources.Fixed_ログイン;
                loginName = "";
                buttonLogout.Text = "ログイン";
                labelEmpID.Text = "";
                labelEmpName.Text = "";
                labelOfficeName.Text = "";
            }
        }

        private void ButtonLimit(bool flg)
        {
            if (flg == false)
            {
                backgroundImage_Master = Properties.Resources.Fixed_Menu_ログイン制限;
                backgroundImage_NonMaster = Properties.Resources.Fixed_Menu_ログイン制限;
                backgroundImage_NonMaster_Stcok = Properties.Resources.Fixed_Menu_ログイン制限;
            }
            else
            {
                backgroundImage_Master = Properties.Resources.Fixed_マスタ用ボタン;
                backgroundImage_NonMaster = Properties.Resources.Fixed_マスタ以外用ボタン;
                backgroundImage_NonMaster_Stcok = Properties.Resources.Fixed_マスタ以外用ボタン_発注関連_;
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("販売管理システムを終了しますか？", "終了確認" , MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
                return;

            this.Dispose();
        }

        private void F_menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("販売管理システムを終了しますか？", "終了確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
                e.Cancel = true;
        }

        private void buttonToClient_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private void buttonToSalesOffice_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private void buttonToMaker_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private void buttonToMajor_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private void buttonToSmall_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private void buttonToSalesOffice2_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private void buttonToPosition_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private void buttonEmployee_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private void buttonToProduct_Click(object sender, EventArgs e)
        {
            OpenForm(((Button)sender).Text);
        }

        private void F_menu_Activated(object sender, EventArgs e)
        {
            labelEmpName.Text = loginName;
            labelEmpID.Text = loginEmID;
            labelOfficeName.Text = loginSalesOffice;
            //メニュー、ボタンEnable設定
            if (loginName != "" && loginName != null)
            {
                SetEnable(true);
            }
            else if (F_Login.SysMode == 1)
            {
                SetEnable(true);
            }
            else
            {
                SetEnable(false);
            }
            if (Opacity == 0)
            {
                Opacity = 1;
            }
        }
    }
}
