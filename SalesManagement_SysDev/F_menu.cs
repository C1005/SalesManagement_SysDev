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
        System.Media.SoundPlayer simpleSound = new System.Media.SoundPlayer(@"c:\Windows\Media\chimes.wav");

        public F_menu()
        {
            InitializeComponent();
        }

        private void F_menu_Load(object sender, EventArgs e)
        {

        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            simpleSound.Play();
            OpenForm(((Button)sender).Text);
        }

        private void buttonClientManager_Click(object sender, EventArgs e)
        {
            if (groupBoxClient.Visible == true)
            {
                groupBoxClient.Visible = false;
                return;
            }
            groupBoxClient.Visible = true;
            groupBoxClient.BringToFront();
            groupBoxProduct.Visible = false;
            groupBoxEmployee.Visible = false;
        }

        private void buttonProductManager_Click(object sender, EventArgs e)
        {
            if (groupBoxProduct.Visible == true)
            {
                groupBoxProduct.Visible = false;
                return;
            }
            groupBoxProduct.Visible = true;
            groupBoxProduct.BringToFront();
            groupBoxClient.Visible = false;
            groupBoxEmployee.Visible = false;
        }

        private void buttonStockManager_Click(object sender, EventArgs e)
        {
            simpleSound.Play();
            OpenForm(((Button)sender).Text);
        }

        private void buttonEmployeeManager_Click(object sender, EventArgs e)
        {
            if (groupBoxEmployee.Visible == true)
            {
                groupBoxEmployee.Visible = false;
                return;
            }
            groupBoxEmployee.Visible = true;
            groupBoxEmployee.BringToFront();
            groupBoxClient.Visible = false;
            groupBoxProduct.Visible = false;
        }

        private void buttonSaleManager_Click(object sender, EventArgs e)
        {
            simpleSound.Play();
            OpenForm(((Button)sender).Text);
        }

        private void buttonWarehousingManager_Click(object sender, EventArgs e)
        {
            simpleSound.Play();
            OpenForm(((Button)sender).Text);
        }

        private void buttonHattyuManager_Click(object sender, EventArgs e)
        {
            simpleSound.Play();
            OpenForm(((Button)sender).Text);
        }

        private void buttonOrderManager_Click(object sender, EventArgs e)
        {
            simpleSound.Play();
            OpenForm(((Button)sender).Text);
        }

        private void buttonChumonManager_Click(object sender, EventArgs e)
        {
            simpleSound.Play();
            OpenForm(((Button)sender).Text);
        }

        private void buttonSyukkoManager_Click(object sender, EventArgs e)
        {
            simpleSound.Play();
            OpenForm(((Button)sender).Text);
        }

        private void buttonArrivalManager_Click(object sender, EventArgs e)
        {
            simpleSound.Play();
            OpenForm(((Button)sender).Text);
        }

        private void buttonShipmentManager_Click(object sender, EventArgs e)
        {
            simpleSound.Play();
            OpenForm(((Button)sender).Text);
        }

        private void OpenForm(string formName)
        {
            Form frm = new Form();
            //引数より、開くフォームを設定
            switch (formName)
            {
                case "ログアウト": //ボタンのテキスト名
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
            simpleSound.Play();
            OpenForm(((Button)sender).Text);
        }

        private void buttonToSalesOffice_Click(object sender, EventArgs e)
        {
            simpleSound.Play();
            OpenForm(((Button)sender).Text);
        }

        private void buttonToMaker_Click(object sender, EventArgs e)
        {
            simpleSound.Play();
            OpenForm(((Button)sender).Text);
        }

        private void buttonToMajor_Click(object sender, EventArgs e)
        {
            simpleSound.Play();
            OpenForm(((Button)sender).Text);
        }

        private void buttonToSmall_Click(object sender, EventArgs e)
        {
            simpleSound.Play();
            OpenForm(((Button)sender).Text);
        }

        private void buttonToSalesOffice2_Click(object sender, EventArgs e)
        {
            simpleSound.Play();
            OpenForm(((Button)sender).Text);
        }

        private void buttonToPosition_Click(object sender, EventArgs e)
        {
            simpleSound.Play();
            OpenForm(((Button)sender).Text);
        }

        private void buttonEmployee_Click(object sender, EventArgs e)
        {
            simpleSound.Play();
            OpenForm(((Button)sender).Text);
        }

        private void buttonToProduct_Click(object sender, EventArgs e)
        {
            simpleSound.Play();
            OpenForm(((Button)sender).Text);
        }
    }
}
