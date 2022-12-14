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
            simpleSound.Play();
            OpenForm(((Button)sender).Text);
        }

        private void buttonProductManager_Click(object sender, EventArgs e)
        {
            simpleSound.Play();
            OpenForm(((Button)sender).Text);
        }

        private void buttonStockManager_Click(object sender, EventArgs e)
        {
            simpleSound.Play();
            OpenForm(((Button)sender).Text);
        }

        private void buttonEmployeeManager_Click(object sender, EventArgs e)
        {
            simpleSound.Play();
            OpenForm(((Button)sender).Text);
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
                case "商品管理":
                    frm = new Forms.Master.FormProduct.F_Product(); //フォルダも指定する必要がある
                    break;
                case "在庫管理":
                    frm = new Forms.Master.FormStock.F_Stock();
                    break;
                case "社員管理":
                    frm = new Forms.Master.FormEmployee.F_Employee();
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

            //フォームを透明化
            Opacity = 0; //(透明化だけだとトップに戻るときに何もない状態になる)

            //選択されたフォームを開く
            frm.ShowDialog();

            //開いたフォームから戻ってきたら
            //メモリを解放する
            Opacity = 100;
            frm.Dispose();
        }
    }
}
