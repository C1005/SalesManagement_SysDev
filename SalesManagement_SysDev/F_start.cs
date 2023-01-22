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
    public partial class F_start : Form
    {

        public F_start()
        {
            InitializeComponent();
        }

        private void F_start_Load(object sender, EventArgs e)
        {
            // フォームの境界線、タイトルバーを無しに設定
            this.FormBorderStyle = FormBorderStyle.None;

            // フォームの背景を黒に設定
            this.BackColor = Color.Black;

            // フォームの不透明度を設定
            this.Opacity = 0.9;
            // 丸み値
            int radius = 50;
            int diameter = radius * 2;
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();

            // 左上
            gp.AddPie(0, 0, diameter, diameter, 180, 90);
            // 右上
            gp.AddPie(this.Width - diameter, 0, diameter, diameter, 270, 90);
            // 左下
            gp.AddPie(0, this.Height - diameter, diameter, diameter, 90, 90);
            // 右下
            gp.AddPie(this.Width - diameter, this.Height - diameter, diameter, diameter, 0, 90);
            // 中央
            gp.AddRectangle(new Rectangle(radius, 0, this.Width - diameter, this.Height));
            // 左
            gp.AddRectangle(new Rectangle(0, radius, radius, this.Height - diameter));
            // 右
            gp.AddRectangle(new Rectangle(this.Width - radius, radius, radius, this.Height - diameter));

            this.Region = new Region(gp);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                //DBの作成
                var context = new SalesManagement_DevContext();
                context.M_Clients.Create();
                context.M_Employees.Create();
                context.M_MajorCassifications.Create();
                context.M_Makers.Create();
                context.M_Messages.Create();
                context.M_Positions.Create();
                context.M_Products.Create();
                context.M_SalesOffices.Create();
                context.M_SmallClassifications.Create();
                context.T_Arrivals.Create();
                context.T_ArrivalDetails.Create();
                context.T_Chumons.Create();
                context.T_ChumonDetails.Create();
                context.T_Hattyus.Create();
                context.T_HattyuDetails.Create();
                context.T_LoginHistorys.Create();
                context.T_OperationHistorys.Create();
                context.T_Orders.Create();
                context.T_OrderDetails.Create();
                context.T_Sales.Create();
                context.T_SaleDetails.Create();
                context.T_Shipments.Create();
                context.T_ShipmentDetails.Create();
                context.T_Stocks.Create();
                context.T_Syukkos.Create();
                context.T_SyukkoDetails.Create();
                context.T_Warehousings.Create();
                context.T_WarehousingDetails.Create();
                context.SaveChanges();
                context.Dispose();

                //データインポート
                ImportData("M_Client");
                ImportData("M_Employee");
                ImportData("M_MajorCassification");
                ImportData("M_Maker");
                ImportData("M_Message");
                ImportData("M_Position");
                ImportData("M_Product");
                ImportData("M_SalesOffice");
                ImportData("M_SmallClassification");
                //ImportData("T_Arrival");
                //ImportData("T_ArrivalDetail");
                //ImportData("T_Chumon");
                //ImportData("T_ChumonDetail");
                ImportData("T_Hattyu");
                ImportData("T_HattyuDetail");
                ImportData("T_LoginHistory");
                ImportData("T_OperationHistory");
                //ImportData("T_Order");
                //ImportData("T_OrderDetail");
                //ImportData("T_Sale");
                //ImportData("T_SaleDetail");
                //ImportData("T_Shipment");
                //ImportData("T_ShipmentDetail");
                ImportData("T_Stock");
                //ImportData("T_Syukko");
                //ImportData("T_SyukkoDetail");
                //ImportData("T_Warehousing");
                //ImportData("T_WarehousingDetail");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Close();
        }

        private void ImportData(string strTbl)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                //DBのM_Messageテーブルデータ有無チェック
                //データが存在していなけばデータをインポート
                int cntMsg = context.Database.SqlQuery<int>("SELECT count(*) FROM [dbo].[" + strTbl + "]").First();
                context.Dispose();
                if (cntMsg > 0)
                {
                    return;
                }
                Common.ImportData TblImport = new Common.ImportData();
                TblImport.DataImport(strTbl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            labelLoad.Visible = true;
        }
    }
}
