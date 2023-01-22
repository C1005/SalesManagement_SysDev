using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagement_SysDev.Forms.DbAccess
{
    class ShipmentDataAccess
    {
        ///////////////////////////////
        //メソッド名：CheckShipmentCDExistence()
        //引　数   ：役職コード
        //戻り値   ：True or False
        //機　能   ：一致する役職コードの有無を確認
        //          ：一致データありの場合True
        //          ：一致データなしの場合False
        ///////////////////////////////
        public bool CheckShipmentCDExistence(string ShipmentID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //役職IDで一致するデータが存在するか
                flg = context.T_Shipments.Any(x => x.ShID.ToString() == ShipmentID);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }

        public bool CheckOrderIDExistence(string shipmentID, string orderID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //受注IDで一致するデータを取得し、その出荷IDが一致するか
                var a = context.T_Shipments.SingleOrDefault(x => x.OrID.ToString() == orderID);
                if (a != null && a.ShID == int.Parse(shipmentID))
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


        ///////////////////////////////
        //メソッド名：GetShipmentData()
        //引　数   ：なし
        //戻り値   ：役職データ
        //機　能   ：役職データの取得
        ///////////////////////////////
        public List<T_ShipmentDsp> GetShipmentData()
        {
            List<T_ShipmentDsp> shipment = new List<T_ShipmentDsp>();
            try
            {
                var context = new SalesManagement_DevContext();
                // tbはIEnumerable型
                var tb = from t1 in context.T_Shipments

                         join t2 in context.T_ShipmentDetails
                         on t1.ShID equals t2.ShID

                         select new
                         {
                             t1.ShID,
                             t1.SoID,
                             t1.EmID,
                             t1.ClID,
                             t1.OrID,
                             t1.ShFinishDate,
                             t1.ShStateFlag,
                             t1.ShFlag,
                             t1.ShHidden,
                             t2.ShDetailID,
                             t2.PrID,
                             t2.ShDquantity,
                         };

                // IEnumerable型のデータをList型へ

                foreach (var p in tb)
                {
                    shipment.Add(new T_ShipmentDsp()
                    {
                        ShID = p.ShID,
                        SoID = p.SoID,
                        EmID = p.EmID,
                        ClID = p.ClID,
                        OrID = p.OrID,
                        ShFinishDate = p.ShFinishDate,
                        ShStateFlag = p.ShStateFlag,
                        ShFlag = p.ShFlag,
                        ShHidden = p.ShHidden,
                        ShDetailID = p.ShDetailID,
                        PrID = p.PrID,
                        ShDquantity = p.ShDquantity,
                    });
                }
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return shipment;

        }

        ///////////////////////////////
        //メソッド名：GetShipmentData()　オーバーロード
        //引　数   ：検索条件
        //戻り値   ：条件一致部署データ
        //機　能   ：条件一致部署データの取得
        ///////////////////////////////
        public List<T_ShipmentDsp> GetShipmentData(T_ShipmentDsp selectCondition)
        {
            List<T_ShipmentDsp> Shipment = GetShipmentData();
            try
            {
                if (NonMaster.FormShipment.F_Shipment.mShID != "")
                {
                    var context = new SalesManagement_DevContext();
                    Shipment = Shipment.Where(x =>
                                                        x.ShID.ToString().Contains(selectCondition.ShID.ToString()) &&
                                                        x.ShStateFlag.ToString().Contains(selectCondition.ShStateFlag.ToString()) &&
                                                        x.ShFlag.ToString().Contains(selectCondition.ShFlag.ToString())).ToList();
                    context.Dispose();
                }
                else if (NonMaster.FormShipment.F_Shipment.mShDetailID != "")
                {
                    Shipment = Shipment.Where(x =>
                                                        x.ShDetailID.ToString().Contains(selectCondition.ShDetailID.ToString()) &&
                                                        x.ShStateFlag.ToString().Contains(selectCondition.ShStateFlag.ToString()) &&
                                                        x.ShFlag.ToString().Contains(selectCondition.ShFlag.ToString())).ToList();
                }
                else if (NonMaster.FormShipment.F_Shipment.mClID != "")
                {
                    Shipment = Shipment.Where(x =>
                                                        x.ClID.ToString().Contains(selectCondition.ClID.ToString()) &&
                                                        x.ShStateFlag.ToString().Contains(selectCondition.ShStateFlag.ToString()) &&
                                                        x.ShFlag.ToString().Contains(selectCondition.ShFlag.ToString())).ToList();
                }

                else if (NonMaster.FormShipment.F_Shipment.mEmID != "")
                {
                    Shipment = Shipment.Where(x =>
                                                        x.EmID.ToString().Contains(selectCondition.EmID.ToString()) &&
                                                        x.ShStateFlag.ToString().Contains(selectCondition.ShStateFlag.ToString()) &&
                                                        x.ShFlag.ToString().Contains(selectCondition.ShFlag.ToString())).ToList();
                }
                else if (NonMaster.FormShipment.F_Shipment.mSoID != "")
                {
                    Shipment = Shipment.Where(x =>
                                                        x.SoID.ToString().Contains(selectCondition.SoID.ToString()) &&
                                                        x.ShStateFlag.ToString().Contains(selectCondition.ShStateFlag.ToString()) &&
                                                        x.ShFlag.ToString().Contains(selectCondition.ShFlag.ToString())).ToList();
                }
                else if (NonMaster.FormShipment.F_Shipment.mOrID != "")
                {
                    Shipment = Shipment.Where(x =>
                                                        x.OrID.ToString().Contains(selectCondition.OrID.ToString()) &&
                                                        x.ShStateFlag.ToString().Contains(selectCondition.ShStateFlag.ToString()) &&
                                                        x.ShFlag.ToString().Contains(selectCondition.ShFlag.ToString())).ToList();
                }
                else if (NonMaster.FormShipment.F_Shipment.mPrID != "")
                {
                    Shipment = Shipment.Where(x =>
                                                        x.PrID.ToString().Contains(selectCondition.PrID.ToString()) &&
                                                        x.ShStateFlag.ToString().Contains(selectCondition.ShStateFlag.ToString()) &&
                                                        x.ShFlag.ToString().Contains(selectCondition.ShFlag.ToString())).ToList();
                }
                else if (NonMaster.FormShipment.F_Shipment.mShStateFlg == 1)
                {
                    Shipment = Shipment.Where(x => x.ShStateFlag.ToString().Contains(selectCondition.ShStateFlag.ToString())).ToList();
                }
                else if (NonMaster.FormShipment.F_Shipment.mShFlg == 2)
                {
                    Shipment = Shipment.Where(x => x.ShFlag.ToString().Contains(selectCondition.ShFlag.ToString())).ToList();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Shipment;

        }

        public List<T_Shipment> GetShipmentFlag(T_Shipment selectCondition)
        {
            List<T_Shipment> Shipment = new List<T_Shipment>();
            try
            {
                var context = new SalesManagement_DevContext();
                Shipment = context.T_Shipments.Where(x => x.ShFlag.ToString().Contains(selectCondition.ShFlag.ToString())).ToList();
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Shipment;

        }

        ///////////////////////////////
        //メソッド名：GetShipmentDspData()
        //引　数   ：なし
        //戻り値   ：表示用役職データ
        //機　能   ：表示用役職データの取得
        ///////////////////////////////
        public bool AddShipmentData(List<T_Shipment> regShipment)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                context.T_Shipments.AddRange(regShipment);
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
        public bool AddShipmentDetailData(List<T_ShipmentDetail> regShipmentDetail)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                context.T_ShipmentDetails.AddRange(regShipmentDetail);
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

        ///////////////////////////////
        //メソッド名：UpdateShipmentData()
        //引　数   ：役職データ
        //戻り値   ：True or False
        //機　能   ：役職データの更新
        //          ：更新成功の場合True
        //          ：更新失敗の場合False
        ///////////////////////////////
        public bool UpdateShipmentData(T_Shipment updShipment)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                var Shipment = context.T_Shipments.Single(x => x.ShID == updShipment.ShID);
                Shipment.ClID = updShipment.ClID;
                Shipment.EmID = updShipment.EmID;
                Shipment.ClID = updShipment.ClID;
                Shipment.SoID = updShipment.SoID;
                Shipment.OrID = updShipment.OrID;
                Shipment.ShFinishDate = updShipment.ShFinishDate;
                Shipment.ShStateFlag = updShipment.ShStateFlag;
                Shipment.ShFlag = updShipment.ShFlag;
                Shipment.ShHidden = Shipment.ShHidden;

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

        public bool UpdateShipmentDetailData(T_ShipmentDetail updShipmentDetail)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                var ShipmentDetail = context.T_ShipmentDetails.Single(x => x.ShDetailID == updShipmentDetail.ShDetailID);
                ShipmentDetail.PrID = updShipmentDetail.PrID;
                ShipmentDetail.ShDquantity = updShipmentDetail.ShDquantity;

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
        ///////////////////////////////
        //メソッド名：DeleteShipmentData()
        //引　数   ：役職データ
        //戻り値   ：True or False
        //機　能   ：役職データの削除
        //          ：削除成功の場合True
        //          ：削除失敗の場合False
        ///////////////////////////////
        public bool DeleteShipmentData(T_Shipment delShipment)
        {
            try
            {
                var context = new SalesManagement_DevContext();

                var Order = context.T_Orders.Single(x => x.OrID == delShipment.OrID);
                var Chumon = context.T_Chumons.Single(x => x.OrID == delShipment.OrID);
                var Syukko = context.T_Syukkos.Single(x => x.OrID == delShipment.OrID);
                var Arrival = context.T_Arrivals.Single(x => x.OrID == delShipment.OrID);
                var Shipment = context.T_Shipments.Single(x => x.OrID == delShipment.OrID);
                var Sale = context.T_Sales.SingleOrDefault(x => x.ChID == delShipment.OrID);

                //レコード削除されたときに在庫数を増やす(元に戻す)
                var ShipmentDetail = context.T_ShipmentDetails.Where(x => x.ShID == delShipment.ShID).ToList();
                foreach (var p in ShipmentDetail)
                {
                    //商品IDは重複しない前提
                    var StockUndo = context.T_Stocks.SingleOrDefault(x => x.PrID == p.PrID);
                    StockUndo.StQuantity = StockUndo.StQuantity + p.ShDquantity;
                }

                Order.OrFlag = delShipment.ShFlag;
                Order.OrHidden = delShipment.ShHidden;
                Chumon.ChFlag = delShipment.ShFlag;
                Chumon.ChHidden = delShipment.ShHidden;
                Syukko.SyFlag = delShipment.ShFlag;
                Syukko.SyHidden = delShipment.ShHidden;
                Arrival.ArFlag = delShipment.ShFlag;
                Arrival.ArHidden = delShipment.ShHidden;
                Shipment.ShFlag = delShipment.ShFlag;
                Shipment.ShHidden = delShipment.ShHidden;

                if (Sale != null)
                {
                    Sale.SaFlag = delShipment.ShFlag;
                    Sale.SaHidden = delShipment.ShHidden;
                }

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

        ///////////////////////////////
        //メソッド名：GetShipmentData()　オーバーロード
        //引　数   ：検索条件
        //戻り値   ：条件一致受注データ
        //機　能   ：条件一致受注データの取得
        ///////////////////////////////
        public List<T_ShipmentDsp> SearchShipmentData(T_ShipmentDsp selectCondition)
        {
            List<T_ShipmentDsp> Shipment = GetShipmentData();
            try
            {
                if (NonMaster.FormShipment.F_Shipment.mShID != "")
                {
                    Shipment = Shipment.Where(x =>
                                                        x.ShID.ToString().Contains(selectCondition.ShID.ToString()) &&
                                                        x.ShStateFlag.ToString().Contains(selectCondition.ShStateFlag.ToString()) &&
                                                        x.ShFlag.ToString().Contains(selectCondition.ShFlag.ToString())).ToList();
                }
                else if (NonMaster.FormShipment.F_Shipment.mShDetailID != "")
                {
                    Shipment = Shipment.Where(x =>
                                                        x.ShDetailID.ToString().Contains(selectCondition.ShDetailID.ToString()) &&
                                                        x.ShStateFlag.ToString().Contains(selectCondition.ShStateFlag.ToString()) &&
                                                        x.ShFlag.ToString().Contains(selectCondition.ShFlag.ToString())).ToList();
                }
                else if (NonMaster.FormShipment.F_Shipment.mClID != "")
                {
                    Shipment = Shipment.Where(x =>
                                                        x.ClID.ToString().Contains(selectCondition.ClID.ToString()) &&
                                                        x.ShStateFlag.ToString().Contains(selectCondition.ShStateFlag.ToString()) &&
                                                        x.ShFlag.ToString().Contains(selectCondition.ShFlag.ToString())).ToList();
                }
                else if (NonMaster.FormShipment.F_Shipment.mSoID != "")
                {
                    Shipment = Shipment.Where(x =>
                                                        x.SoID.ToString().Contains(selectCondition.SoID.ToString()) &&
                                                        x.ShStateFlag.ToString().Contains(selectCondition.ShStateFlag.ToString()) &&
                                                        x.ShFlag.ToString().Contains(selectCondition.ShFlag.ToString())).ToList();
                }
                else if (NonMaster.FormShipment.F_Shipment.mOrID != "")
                {
                    Shipment = Shipment.Where(x =>
                                                        x.OrID.ToString().Contains(selectCondition.OrID.ToString()) &&
                                                        x.ShStateFlag.ToString().Contains(selectCondition.ShStateFlag.ToString()) &&
                                                        x.ShFlag.ToString().Contains(selectCondition.ShFlag.ToString())).ToList();
                }

                else if (NonMaster.FormShipment.F_Shipment.mShStateFlg == 1)
                {
                    Shipment = Shipment.Where(x => x.ShStateFlag.ToString().Contains(selectCondition.ShStateFlag.ToString())).ToList();
                }
                else if (NonMaster.FormShipment.F_Shipment.mShFlg == 2)
                {
                    Shipment = Shipment.Where(x => x.ShFlag.ToString().Contains(selectCondition.ShFlag.ToString())).ToList();
                }
                else if (NonMaster.FormShipment.F_Shipment.mPrID != "")
                {
                    Shipment = Shipment.Where(x =>
                                                        x.PrID.ToString().Contains(selectCondition.PrID.ToString()) &&
                                                        x.ShStateFlag.ToString().Contains(selectCondition.ShStateFlag.ToString()) &&
                                                        x.ShFlag.ToString().Contains(selectCondition.ShFlag.ToString())).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Shipment;
        }

        //確定検索用
        public List<T_ShipmentDsp> SearchShipmentConfirm(T_ShipmentDsp selectCondition)
        {
            List<T_ShipmentDsp> Shipment = GetShipmentData();
            try
            {
                if (NonMaster.FormShipment.F_ShipmentConfirm.mShID != "")
                {
                    Shipment = Shipment.Where(x =>
                                                        x.ShID.ToString().Contains(selectCondition.ShID.ToString())).ToList();
                }
                else if (NonMaster.FormShipment.F_ShipmentConfirm.mOrID != "")
                {
                    Shipment = Shipment.Where(x =>
                                                        x.OrID.ToString().Contains(selectCondition.OrID.ToString())).ToList();
                }
                else if (NonMaster.FormShipment.F_ShipmentConfirm.mClID != "")
                {
                    Shipment = Shipment.Where(x =>
                                                        x.ClID.ToString().Contains(selectCondition.ClID.ToString())).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Shipment;
        }

        public bool CheckStateFlagExistence(string shipmentID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //状態フラグで一致するデータが存在するか
                var ShDate = context.T_Shipments.Where(x => x.ShID.ToString() == shipmentID).ToList();
                flg = ShDate.Any(x => x.ShStateFlag == 1);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }
        public bool CheckFlagExistence(string shipmentID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //管理フラグで一致するデータが存在するか
                var ShDate = context.T_Shipments.Where(x => x.ShID.ToString() == shipmentID).ToList();
                flg = ShDate.Any(x => x.ShFlag == 2);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }

        public bool ConfirmShipmentData(T_Shipment conShipment)
        {
            try
            {
                var context = new SalesManagement_DevContext();

                //確定する1件の出荷情報を得る
                var shipment = context.T_Shipments.Single(x => x.ShID == conShipment.ShID);

                //確定する1件の受注情報を得る
                var order = context.T_Orders.Single(x => x.OrID == shipment.OrID);
                //確定する1件の受注詳細を得る
                var orderDetail = context.T_OrderDetails.Where(x => x.OrID == shipment.OrID).ToList();

                //出荷情報を売上情報として1件の売上レコードを新規作成
                var sale = new T_Sale
                {
                    ClID = shipment.ClID,
                    EmID = order.EmID, //受注した社員ID
                    SoID = shipment.SoID,
                    ChID = shipment.OrID, //ChIDは仕様書のミスで、正確にはOrID
                    SaDate = DateTime.Now, //受注した日付を売上日時とする
                    SaFlag = 0,
                    SaHidden = shipment.ShHidden
                };

                shipment.ShStateFlag = conShipment.ShStateFlag;
                shipment.EmID = int.Parse(F_menu.loginEmID); //営業担当の社員ID ???
                shipment.ShFinishDate = DateTime.Now; //出荷確定処理を行った日付
                context.T_Sales.Add(sale); //売上テーブル反映(一旦、主キーを自動採番取得のために登録しておく)
                context.SaveChanges();


                //確定する1件の出荷情報を得る
                var shipmentDetail = context.T_ShipmentDetails.Where(x => x.ShID == conShipment.ShID).ToList();

                // 売上データの取得
                List<T_Sale> Sale = GetSaleTable();
                // T_Saleから、直前にAddした末尾の行を取得する
                T_Sale lastSaleTable = Sale[Sale.Count - 1];
                // 末尾行のIDを取得
                int SaleID = lastSaleTable.SaID;

                List<T_SaleDetail> saleDetail = new List<T_SaleDetail>();
                int saPrTotalPrice = 0;
                int AllTotalPrice = 0;
                foreach (var p in shipmentDetail)
                {
                    var productID = context.M_Products.Single(x => x.PrID == p.PrID);

                    saleDetail.Add(new T_SaleDetail()
                    {
                        SaID = SaleID,
                        PrID = p.PrID,
                        SaQuantity = p.ShDquantity,
                        SaPrTotalPrice = p.ShDquantity * productID.Price
                    });
                }

                context.T_SaleDetails.AddRange(saleDetail); //売上詳細テーブル反映
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

        ///////////////////////////////
        //メソッド名：GetSaleData()
        //引　数   ：なし
        //戻り値   ：売上データ
        //機　能   ：売上データの取得
        ///////////////////////////////
        public List<T_Sale> GetSaleTable()
        {
            List<T_Sale> sale = new List<T_Sale>();
            try
            {
                var context = new SalesManagement_DevContext();
                // tbはIEnumerable型
                var tb = from t1 in context.T_Sales

                         select new
                         {
                             t1.SaID,
                             t1.SoID,
                             t1.EmID,
                             t1.ClID,
                             t1.ChID,
                             t1.SaDate,
                             t1.SaFlag,
                             t1.SaHidden
                         };

                // IEnumerable型のデータをList型へ

                foreach (var p in tb)
                {
                    sale.Add(new T_Sale()
                    {
                        SaID = p.SaID,
                        SoID = p.SoID,
                        EmID = p.EmID,
                        ClID = p.ClID,
                        ChID = p.ChID,
                        SaDate = p.SaDate,
                        SaFlag = p.SaFlag,
                        SaHidden = p.SaHidden
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
    }
}
