using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagement_SysDev.Forms.DbAccess
{
    class SyukkoDataAccess
    {
        public bool CheckSyukkoCDExistence(string syukkoID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //出庫IDで一致するデータが存在するか
                flg = context.T_Syukkos.Any(x => x.SyID.ToString() == syukkoID);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }

        public bool CheckOrderIDExistence(string syukkoID, string orderID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //受注IDで一致するデータを取得し、その出庫IDが一致するか
                var a = context.T_Syukkos.SingleOrDefault(x => x.OrID.ToString() == orderID);
                if (a != null && a.SyID == int.Parse(syukkoID))
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

        public bool CheckSyukkoDetailCDExistence(string syukkoDetailID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //出庫詳細IDで一致するデータが存在するか
                flg = context.T_SyukkoDetails.Any(x => x.SyDetailID.ToString() == syukkoDetailID);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }
        public bool DeleteSyukkoData(T_Syukko delSyukko)
        {
            try
            {
                var context = new SalesManagement_DevContext();

                var Order = context.T_Orders.Single(x => x.OrID == delSyukko.OrID);
                var Chumon = context.T_Chumons.Single(x => x.OrID == delSyukko.OrID);
                var Syukko = context.T_Syukkos.Single(x => x.OrID == delSyukko.OrID);
                var Arrival = context.T_Arrivals.SingleOrDefault(x => x.OrID == delSyukko.OrID);
                var Shipment = context.T_Shipments.SingleOrDefault(x => x.OrID == delSyukko.OrID);
                var Sale = context.T_Sales.SingleOrDefault(x => x.ChID == delSyukko.OrID);

                //レコード削除されたときに在庫数を増やす(元に戻す)
                var SyukkoDetail = context.T_SyukkoDetails.Where(x => x.SyID == delSyukko.SyID).ToList();
                foreach (var p in SyukkoDetail)
                {
                    //商品IDは重複しない前提
                    var StockUndo = context.T_Stocks.SingleOrDefault(x => x.PrID == p.PrID);
                    StockUndo.StQuantity = StockUndo.StQuantity + p.SyQuantity;
                }

                Order.OrFlag = delSyukko.SyFlag;
                Order.OrHidden = delSyukko.SyHidden;
                Chumon.ChFlag = delSyukko.SyFlag;
                Chumon.ChHidden = delSyukko.SyHidden;
                Syukko.SyFlag = delSyukko.SyFlag;
                Syukko.SyHidden = delSyukko.SyHidden;

                if (Arrival != null)
                {
                    Arrival.ArFlag = delSyukko.SyFlag;
                    Arrival.ArHidden = delSyukko.SyHidden;
                }

                if (Shipment != null)
                {
                    Shipment.ShFlag = delSyukko.SyFlag;
                    Shipment.ShHidden = delSyukko.SyHidden;
                }

                if (Sale != null)
                {
                    Sale.SaFlag = delSyukko.SyFlag;
                    Sale.SaHidden = delSyukko.SyHidden;
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
        //メソッド名：GetSyukkoData()
        //引　数   ：なし
        //戻り値   ：役職データ
        //機　能   ：役職データの取得
        ///////////////////////////////
        public List<T_SyukkoDsp> GetSyukkoData()
        {
            List<T_SyukkoDsp> syukko = new List<T_SyukkoDsp>();
            try
            {
                var context = new SalesManagement_DevContext();
                // tbはIEnumerable型
                var tb = from t1 in context.T_Syukkos

                         join t2 in context.T_SyukkoDetails
                         on t1.SyID equals t2.SyID

                         select new
                         {
                             t1.ClID,
                             t1.SyID,
                             t1.EmID,
                             t1.SoID,
                             t1.SyDate,
                             t1.OrID,
                             t1.SyStateFlag,
                             t1.SyFlag,
                             t1.SyHidden,
                             t2.SyDetailID,
                             t2.PrID,
                             t2.SyQuantity
                         };

                // IEnumerable型のデータをList型へ

                foreach (var p in tb)
                {
                    syukko.Add(new T_SyukkoDsp()
                    {
                        ClID = p.ClID,
                        SyID = p.SyID,
                        EmID = p.EmID,
                        SoID = p.SoID,
                        SyDate = p.SyDate,
                        OrID = p.OrID,
                        SyStateFlag = p.SyStateFlag,
                        SyFlag = p.SyFlag,
                        SyHidden = p.SyHidden,
                        SyDetailID = p.SyDetailID,
                        PrID = p.PrID,
                        SyQuantity = p.SyQuantity
                    });
                }
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return syukko;
        }

        //確定検索用
        public List<T_SyukkoDsp> SearchSyukkoConfirm(T_SyukkoDsp selectCondition)
        {
            List<T_SyukkoDsp> Syukko = GetSyukkoData();
            try
            {
                if (NonMaster.FormSyukko.F_SyukkoConfirm.mSyID != "")
                {
                    Syukko = Syukko.Where(x =>
                                                        x.SyID.ToString().Contains(selectCondition.SyID.ToString())).ToList();
                }
                else if (NonMaster.FormSyukko.F_SyukkoConfirm.mOrID != "")
                {
                    Syukko = Syukko.Where(x =>
                                                        x.OrID.ToString().Contains(selectCondition.OrID.ToString())).ToList();
                }
                else if (NonMaster.FormSyukko.F_SyukkoConfirm.mClID != "")
                {
                    Syukko = Syukko.Where(x =>
                                                        x.ClID.ToString().Contains(selectCondition.ClID.ToString())).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Syukko;
        }

        public bool CheckStateFlagExistence(string syukkoID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //状態フラグで一致するデータが存在するか
                var SyDate = context.T_Syukkos.Where(x => x.SyID.ToString() == syukkoID).ToList();
                flg = SyDate.Any(x => x.SyStateFlag == 1);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }
        public bool CheckFlagExistence(string syukkoID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //管理フラグで一致するデータが存在するか
                var SyDate = context.T_Syukkos.Where(x => x.SyID.ToString() == syukkoID).ToList();
                flg = SyDate.Any(x => x.SyFlag == 2);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }

        public bool ConfirmSyukkoData(T_Syukko conSyukko)
        {
            try
            {
                var context = new SalesManagement_DevContext();

                //確定する1件の出庫情報を得る
                var syukko = context.T_Syukkos.Single(x => x.SyID == conSyukko.SyID);

                //出庫情報を入荷情報として1件の注文レコードを新規作成
                var arrival = new T_Arrival
                {
                    ClID = syukko.ClID,
                    EmID = 0, //DBでNULL許可されていないので'0 == NULL'と判断して'0'で登録
                    SoID = syukko.SoID,
                    OrID = syukko.OrID,
                    ArDate = null,
                    ArFlag = 0,
                    ArStateFlag = 0,
                    ArHidden = syukko.SyHidden
                };

                syukko.SyStateFlag = conSyukko.SyStateFlag;
                syukko.EmID = int.Parse(F_menu.loginEmID); //社員IDを確定を行った社員IDで変更
                syukko.SyDate = DateTime.Now; //出庫確定処理を行った日付
                context.T_Arrivals.Add(arrival); //入荷テーブル反映(一旦、主キーを自動採番取得のために登録しておく)
                context.SaveChanges();


                //確定する1件の出庫情報を得る
                var syukkoDetail = context.T_SyukkoDetails.Where(x => x.SyID == conSyukko.SyID).ToList();

                // 入荷データの取得
                List<T_Arrival> Arrival = GetArrivalTable();
                // T_Arrivalから、直前にAddした末尾の行を取得する
                T_Arrival lastArrivalTable = Arrival[Arrival.Count - 1];
                // 末尾行のIDを取得
                int ArrivalID = lastArrivalTable.ArID;

                List<T_ArrivalDetail> arrivalDetail = new List<T_ArrivalDetail>();
                foreach (var p in syukkoDetail)
                {
                    arrivalDetail.Add(new T_ArrivalDetail()
                    {
                        ArID = ArrivalID,
                        PrID = p.PrID,
                        ArQuantity = p.SyQuantity
                    });
                }

                context.T_ArrivalDetails.AddRange(arrivalDetail); //入荷詳細テーブル反映
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
        //メソッド名：GetArrivalData()
        //引　数   ：なし
        //戻り値   ：注文データ
        //機　能   ：注文データの取得
        ///////////////////////////////
        public List<T_Arrival> GetArrivalTable()
        {
            List<T_Arrival> arrival = new List<T_Arrival>();
            try
            {
                var context = new SalesManagement_DevContext();
                // tbはIEnumerable型
                var tb = from t1 in context.T_Arrivals

                         select new
                         {
                             t1.ArID,
                             t1.SoID,
                             t1.EmID,
                             t1.ClID,
                             t1.OrID,
                             t1.ArDate,
                             t1.ArStateFlag,
                             t1.ArFlag,
                             t1.ArHidden
                         };

                // IEnumerable型のデータをList型へ

                foreach (var p in tb)
                {
                    arrival.Add(new T_Arrival()
                    {
                        ArID = p.ArID,
                        SoID = p.SoID,
                        EmID = p.EmID,
                        ClID = p.ClID,
                        OrID = p.OrID,
                        ArDate = p.ArDate,
                        ArStateFlag = p.ArStateFlag,
                        ArFlag = p.ArFlag,
                        ArHidden = p.ArHidden
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
    }
}
