using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagement_SysDev.Forms.DbAccess
{
    class SaleDataAccess
    {
        public bool CheckSaleCDExistence(string saleID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //受注IDで一致するデータが存在するか
                flg = context.T_Sales.Any(x => x.SaID.ToString() == saleID);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }

        public bool CheckOrderIDExistence(string saleID, string orderID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //受注IDで一致するデータを取得し、その売上IDが一致するか
                var a = context.T_Sales.SingleOrDefault(x => x.ChID.ToString() == orderID);
                if (a != null && a.SaID == int.Parse(saleID))
                {
                    flg = true;
                }
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }

        public bool CheckSaleDetailCDExistence(string saleDetailID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //受注詳細IDで一致するデータが存在するか
                flg = context.T_SaleDetails.Any(x => x.SaDetailID.ToString() == saleDetailID);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }

        ///////////////////////////////
        //メソッド名：GetSaleData()
        //引　数   ：なし
        //戻り値   ：注文データ
        //機　能   ：注文データの取得
        ///////////////////////////////
        public List<T_SaleDsp> GetSaleData()
        {
            List<T_SaleDsp> sale = new List<T_SaleDsp>();
            try
            {
                var context = new SalesManagement_DevContext();
                // tbはIEnumerable型
                var tb = from t1 in context.T_Sales

                         join t2 in context.T_SaleDetails
                         on t1.SaID equals t2.SaID

                         select new
                         {
                             t1.SaID,
                             t1.ClID,
                             t1.SoID,
                             t1.EmID,
                             t1.ChID,
                             t1.SaDate,
                             t1.SaHidden,
                             t1.SaFlag,
                             t2.SaDetailID,
                             t2.PrID,
                             t2.SaQuantity,
                             t2.SaPrTotalPrice
                         };

                // IEnumerable型のデータをList型へ

                foreach (var p in tb)
                {
                    sale.Add(new T_SaleDsp()
                    {
                        SaID = p.SaID,
                        ClID = p.ClID,
                        SoID = p.SoID,
                        EmID = p.EmID,
                        ChID = p.ChID,
                        SaDate = p.SaDate,
                        SaHidden = p.SaHidden,
                        SaFlag = p.SaFlag,
                        SaDetailID = p.SaDetailID,
                        PrID = p.PrID,
                        SaQuantity = p.SaQuantity,
                        SaPrTotalPrice = p.SaPrTotalPrice
                    });
                }
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sale;
        }

        ///////////////////////////////
        //メソッド名：SearchSaleData()　オーバーロード
        //引　数   ：検索条件
        //戻り値   ：条件一致売上データ
        //機　能   ：条件一致売上データの取得
        ///////////////////////////////
        public List<T_SaleDsp> SearchSaleData(T_SaleDsp selectCondition)
        {
            List<T_SaleDsp> Sale = GetSaleData();
            try
            {
                if (NonMaster.FormSale.F_Sale.mSaID != "")
                {
                    Sale = Sale.Where(x =>
                                                        x.SaID.ToString().Contains(selectCondition.SaID.ToString()) &&
                                                        x.SaFlag.ToString().Contains(selectCondition.SaFlag.ToString())).ToList();
                }
                else if (NonMaster.FormSale.F_Sale.mSaDetailID != "")
                {
                    Sale = Sale.Where(x =>
                                                        x.SaDetailID.ToString().Contains(selectCondition.SaDetailID.ToString()) &&
                                                        x.SaFlag.ToString().Contains(selectCondition.SaFlag.ToString())).ToList();
                }
                else if (NonMaster.FormSale.F_Sale.mSoID != "")
                {
                    Sale = Sale.Where(x =>
                                                        x.SoID.ToString().Contains(selectCondition.SoID.ToString()) &&
                                                        x.SaFlag.ToString().Contains(selectCondition.SaFlag.ToString())).ToList();
                }
                else if (NonMaster.FormSale.F_Sale.mEmID != "")
                {
                    Sale = Sale.Where(x =>
                                                        x.EmID.ToString().Contains(selectCondition.EmID.ToString()) &&
                                                        x.SaFlag.ToString().Contains(selectCondition.SaFlag.ToString())).ToList();
                }
                else if (NonMaster.FormSale.F_Sale.mClID != "")
                {
                    Sale = Sale.Where(x =>
                                                        x.ClID.ToString().Contains(selectCondition.ClID.ToString()) &&
                                                        x.SaFlag.ToString().Contains(selectCondition.SaFlag.ToString())).ToList();
                }
                else if (NonMaster.FormSale.F_Sale.mChID != "")
                {
                    Sale = Sale.Where(x =>
                                                        x.ChID.ToString().Contains(selectCondition.ChID.ToString()) &&
                                                        x.SaFlag.ToString().Contains(selectCondition.SaFlag.ToString())).ToList();
                }
                else if (NonMaster.FormSale.F_Sale.mSaFlg == 2)
                {
                    Sale = Sale.Where(x => x.SaFlag.ToString().Contains(selectCondition.SaFlag.ToString())).ToList();
                }
                else if (NonMaster.FormSale.F_Sale.mPrID != "")
                {
                    Sale = Sale.Where(x =>
                                                        x.PrID.ToString().Contains(selectCondition.PrID.ToString()) &&
                                                        x.SaFlag.ToString().Contains(selectCondition.SaFlag.ToString())).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Sale;
        }

        ///////////////////////////////
        //メソッド名：UpdatePositionData()
        //引　数   ：受注データ
        //戻り値   ：True or False
        //機　能   ：受注データの更新
        //          ：更新成功の場合True
        //          ：更新失敗の場合False
        ///////////////////////////////
        public bool UpdateSaleData(T_Sale updSale)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                var sale = context.T_Sales.Single(x => x.ChID == updSale.ChID);
                var Order = context.T_Orders.Single(x => x.OrID == updSale.ChID);
                var Chumon = context.T_Chumons.Single(x => x.OrID == updSale.ChID);
                var Syukko = context.T_Syukkos.Single(x => x.OrID == updSale.ChID);
                var Arrival = context.T_Arrivals.Single(x => x.OrID == updSale.ChID);
                var Shipment = context.T_Shipments.Single(x => x.OrID == updSale.ChID);

                //レコード削除されたときに在庫数を増やす(元に戻す)
                var saleDetail = context.T_SaleDetails.Where(x => x.SaID == updSale.SaID).ToList();
                foreach (var p in saleDetail)
                {
                    //商品IDは重複しない前提
                    var StockUndo = context.T_Stocks.SingleOrDefault(x => x.PrID == p.PrID);
                    StockUndo.StQuantity = StockUndo.StQuantity + p.SaQuantity;
                }

                sale.SoID = updSale.SoID;
                sale.EmID = updSale.EmID;
                sale.ClID = updSale.ClID;
                sale.ChID = updSale.ChID;
                sale.SaDate = updSale.SaDate;
                sale.SaFlag = updSale.SaFlag;
                sale.SaHidden = updSale.SaHidden;

                Order.OrFlag = updSale.SaFlag;
                Order.OrHidden = updSale.SaHidden;
                Chumon.ChFlag = updSale.SaFlag;
                Chumon.ChHidden = updSale.SaHidden;
                Syukko.SyFlag = updSale.SaFlag;
                Syukko.SyHidden = updSale.SaHidden;
                Arrival.ArFlag = updSale.SaFlag;
                Arrival.ArHidden = updSale.SaHidden;
                Shipment.ShFlag = updSale.SaFlag;
                Shipment.ShHidden = updSale.SaHidden;

                context.SaveChanges();
                context.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
