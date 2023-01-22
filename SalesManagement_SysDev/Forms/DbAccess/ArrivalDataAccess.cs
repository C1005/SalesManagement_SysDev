using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagement_SysDev.Forms.DbAccess
{
    class ArrivalDataAccess
    {
        public bool CheckArrivalCDExistence(string arrivalID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //受注IDで一致するデータが存在するか
                flg = context.T_Arrivals.Any(x => x.ArID.ToString() == arrivalID);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }

        public bool CheckOrderIDExistence(string arrivalID, string orderID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //受注IDで一致するデータを取得し、その入荷IDが一致するか
                var a = context.T_Arrivals.SingleOrDefault(x => x.OrID.ToString() == orderID);
                if(a != null && a.ArID == int.Parse(arrivalID))
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

        public bool CheckArrivalDetailCDExistence(string arrivalDetailID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //受注詳細IDで一致するデータが存在するか
                flg = context.T_ArrivalDetails.Any(x => x.ArDetailID.ToString() == arrivalDetailID);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }
        ///////////////////////////////
        //メソッド名：GetArrivalData()
        //引　数   ：なし
        //戻り値   ：受注データ
        //機　能   ：受注データの取得
        ///////////////////////////////
        public List<T_ArrivalDsp> GetArrivalData()
        {
            List<T_ArrivalDsp> arrival = new List<T_ArrivalDsp>();
            try
            {
                var context = new SalesManagement_DevContext();
                // tbはIEnumerable型
                var tb = from t1 in context.T_Arrivals

                         join t2 in context.T_ArrivalDetails
                         on t1.ArID equals t2.ArID

                         select new
                         {
                             t1.ArID,
                             t1.OrID,
                             t1.SoID,
                             t1.EmID,
                             t1.ClID,
                             t1.ArDate,
                             t1.ArStateFlag,
                             t1.ArFlag,
                             t1.ArHidden,
                             t2.ArDetailID,
                             t2.PrID,
                             t2.ArQuantity,
                         };

                // IEnumerable型のデータをList型へ

                foreach (var p in tb)
                {
                    arrival.Add(new T_ArrivalDsp()
                    {
                        ArID = p.ArID,
                        OrID = p.OrID,
                        SoID = p.SoID,
                        EmID = p.EmID,
                        ClID = p.ClID,
                        ArDate = p.ArDate,
                        ArStateFlag = p.ArStateFlag,
                        ArFlag = p.ArFlag,
                        ArHidden = p.ArHidden,
                        ArDetailID = p.ArDetailID,
                        PrID = p.PrID,
                        ArQuantity = p.ArQuantity,
                    });
                }
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return arrival;

        }
        ///////////////////////////////
        //メソッド名：GetPositionData()　オーバーロード
        //引　数   ：検索条件
        //戻り値   ：条件一致受注データ
        //機　能   ：条件一致受注データの取得
        ///////////////////////////////
        public List<T_ArrivalDsp> SearchArrivalData(T_ArrivalDsp selectCondition)
        {
            List<T_ArrivalDsp> Arrival = GetArrivalData();
            try
            {
                if (NonMaster.FormArrival.F_Arrival.mOrID != "")
                {
                    Arrival = Arrival.Where(x =>
                                                        x.ArID.ToString().Contains(selectCondition.ArID.ToString()) &&
                                                        x.ArStateFlag.ToString().Contains(selectCondition.ArStateFlag.ToString()) &&
                                                        x.ArFlag.ToString().Contains(selectCondition.ArFlag.ToString())).ToList();
                }
                else if (NonMaster.FormArrival.F_Arrival.mArDetailID != "")
                {
                    Arrival = Arrival.Where(x =>
                                                        x.ArDetailID.ToString().Contains(selectCondition.ArDetailID.ToString()) &&
                                                        x.ArStateFlag.ToString().Contains(selectCondition.ArStateFlag.ToString()) &&
                                                        x.ArFlag.ToString().Contains(selectCondition.ArFlag.ToString())).ToList();
                }
                else if (NonMaster.FormArrival.F_Arrival.mSoID != "")
                {
                    Arrival = Arrival.Where(x =>
                                                        x.SoID.ToString().Contains(selectCondition.SoID.ToString()) &&
                                                        x.ArStateFlag.ToString().Contains(selectCondition.ArStateFlag.ToString()) &&
                                                        x.ArFlag.ToString().Contains(selectCondition.ArFlag.ToString())).ToList();
                }
                else if (NonMaster.FormArrival.F_Arrival.mEmID != "")
                {
                    Arrival = Arrival.Where(x =>
                                                        x.EmID.ToString().Contains(selectCondition.EmID.ToString()) &&
                                                        x.ArStateFlag.ToString().Contains(selectCondition.ArStateFlag.ToString()) &&
                                                        x.ArFlag.ToString().Contains(selectCondition.ArFlag.ToString())).ToList();
                }
                else if (NonMaster.FormArrival.F_Arrival.mClID != "")
                {
                    Arrival = Arrival.Where(x =>
                                                        x.ClID.ToString().Contains(selectCondition.ClID.ToString()) &&
                                                        x.ArStateFlag.ToString().Contains(selectCondition.ArStateFlag.ToString()) &&
                                                        x.ArFlag.ToString().Contains(selectCondition.ArFlag.ToString())).ToList();
                }
                else if (NonMaster.FormArrival.F_Arrival.mArStateFlg == 1)
                {
                    Arrival = Arrival.Where(x => x.ArStateFlag.ToString().Contains(selectCondition.ArStateFlag.ToString())).ToList();
                }
                else if (NonMaster.FormArrival.F_Arrival.mArFlg == 2)
                {
                    Arrival = Arrival.Where(x => x.ArFlag.ToString().Contains(selectCondition.ArFlag.ToString())).ToList();
                }
                else if (NonMaster.FormArrival.F_Arrival.mPrID != "")
                {
                    Arrival = Arrival.Where(x =>
                                                        x.PrID.ToString().Contains(selectCondition.PrID.ToString()) &&
                                                        x.ArStateFlag.ToString().Contains(selectCondition.ArStateFlag.ToString()) &&
                                                        x.ArFlag.ToString().Contains(selectCondition.ArFlag.ToString())).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Arrival;
        }
        public bool DeleteArrivalData(T_Arrival delArrival)
        {
            try
            {
                var context = new SalesManagement_DevContext();

                var Order = context.T_Orders.Single(x => x.OrID == delArrival.OrID);
                var Chumon = context.T_Chumons.Single(x => x.OrID == delArrival.OrID);
                var Syukko = context.T_Syukkos.Single(x => x.OrID == delArrival.OrID);
                var Arrival = context.T_Arrivals.Single(x => x.OrID == delArrival.OrID);
                var Shipment = context.T_Shipments.SingleOrDefault(x => x.OrID == delArrival.OrID);
                var Sale = context.T_Sales.SingleOrDefault(x => x.ChID == delArrival.OrID);

                //レコード削除されたときに在庫数を増やす(元に戻す)
                var ArrivalDetail = context.T_ArrivalDetails.Where(x => x.ArID == delArrival.ArID).ToList();
                foreach (var p in ArrivalDetail)
                {
                    //商品IDは重複しない前提
                    var StockUndo = context.T_Stocks.SingleOrDefault(x => x.PrID == p.PrID);
                    StockUndo.StQuantity = StockUndo.StQuantity + p.ArQuantity;
                }

                Order.OrFlag = delArrival.ArFlag;
                Order.OrHidden = delArrival.ArHidden;
                Chumon.ChFlag = delArrival.ArFlag;
                Chumon.ChHidden = delArrival.ArHidden;
                Syukko.SyFlag = delArrival.ArFlag;
                Syukko.SyHidden = delArrival.ArHidden;
                Arrival.ArFlag = delArrival.ArFlag;
                Arrival.ArHidden = delArrival.ArHidden;

                if (Shipment != null)
                {
                    Shipment.ShFlag = delArrival.ArFlag;
                    Shipment.ShHidden = delArrival.ArHidden;
                }

                if (Sale != null)
                {
                    Sale.SaFlag = delArrival.ArFlag;
                    Sale.SaHidden = delArrival.ArHidden;
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

        //確定検索用
        public List<T_ArrivalDsp> SearchArrivalConfirm(T_ArrivalDsp selectCondition)
        {
            List<T_ArrivalDsp> Arrival = GetArrivalData();
            try
            {
                if (NonMaster.FormArrival.F_ArrivalConfirm.mArID != "")
                {
                    Arrival = Arrival.Where(x =>
                                                        x.ArID.ToString().Contains(selectCondition.ArID.ToString())).ToList();
                }
                else if (NonMaster.FormArrival.F_ArrivalConfirm.mOrID != "")
                {
                    Arrival = Arrival.Where(x =>
                                                        x.OrID.ToString().Contains(selectCondition.OrID.ToString())).ToList();
                }
                else if (NonMaster.FormArrival.F_ArrivalConfirm.mClID != "")
                {
                    Arrival = Arrival.Where(x =>
                                                        x.ClID.ToString().Contains(selectCondition.ClID.ToString())).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Arrival;
        }

        public bool CheckStateFlagExistence(string arrivalID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //状態フラグで一致するデータが存在するか
                var ArDate = context.T_Arrivals.Where(x => x.ArID.ToString() == arrivalID).ToList();
                flg = ArDate.Any(x => x.ArStateFlag == 1);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }
        public bool CheckFlagExistence(string arrivalID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //管理フラグで一致するデータが存在するか
                var ArDate = context.T_Arrivals.Where(x => x.ArID.ToString() == arrivalID).ToList();
                flg = ArDate.Any(x => x.ArFlag == 2);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }

        public bool ConfirmArrivalData(T_Arrival conArrival)
        {
            try
            {
                var context = new SalesManagement_DevContext();

                //確定する1件の入荷情報を得る
                var arrival = context.T_Arrivals.Single(x => x.ArID == conArrival.ArID);

                //入荷情報を出荷情報として1件の出荷レコードを新規作成
                var shipment = new T_Shipment
                {
                    ClID = arrival.ClID,
                    EmID = 0, //DBでNULL許可されていないので'0 == NULL'と判断して'0'で登録
                    SoID = arrival.SoID,
                    OrID = arrival.OrID,
                    ShFinishDate = null,
                    ShFlag = 0,
                    ShStateFlag = 0,
                    ShHidden = arrival.ArHidden
                };

                arrival.ArStateFlag = conArrival.ArStateFlag;
                arrival.EmID = int.Parse(F_menu.loginEmID); //社員IDを確定を行った社員IDで変更
                arrival.ArDate = DateTime.Now; //入荷確定処理を行った日付
                context.T_Shipments.Add(shipment); //出荷テーブル反映(一旦、主キーを自動採番取得のために登録しておく)
                context.SaveChanges();


                //確定する1件の入荷情報を得る
                var arrivalDetail = context.T_ArrivalDetails.Where(x => x.ArID == conArrival.ArID).ToList();

                // 出荷データの取得
                List<T_Shipment> Shipment = GetShipmentTable();
                // T_Shipmentから、直前にAddした末尾の行を取得する
                T_Shipment lastShipmentTable = Shipment[Shipment.Count - 1];
                // 末尾行のIDを取得
                int ShipmentID = lastShipmentTable.ShID;

                List<T_ShipmentDetail> shipmentDetail = new List<T_ShipmentDetail>();
                foreach (var p in arrivalDetail)
                {
                    shipmentDetail.Add(new T_ShipmentDetail()
                    {
                        ShID = ShipmentID,
                        PrID = p.PrID,
                        ShDquantity = p.ArQuantity
                    });
                }

                context.T_ShipmentDetails.AddRange(shipmentDetail); //出荷詳細テーブル反映
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
        //メソッド名：GetShipmentData()
        //引　数   ：なし
        //戻り値   ：出荷データ
        //機　能   ：出荷データの取得
        ///////////////////////////////
        public List<T_Shipment> GetShipmentTable()
        {
            List<T_Shipment> shipment = new List<T_Shipment>();
            try
            {
                var context = new SalesManagement_DevContext();
                // tbはIEnumerable型
                var tb = from t1 in context.T_Shipments

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
                             t1.ShHidden
                         };

                // IEnumerable型のデータをList型へ

                foreach (var p in tb)
                {
                    shipment.Add(new T_Shipment()
                    {
                        ShID = p.ShID,
                        SoID = p.SoID,
                        EmID = p.EmID,
                        ClID = p.ClID,
                        OrID = p.OrID,
                        ShFinishDate = p.ShFinishDate,
                        ShStateFlag = p.ShStateFlag,
                        ShFlag = p.ShFlag,
                        ShHidden = p.ShHidden
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
    }
}
