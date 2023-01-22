using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagement_SysDev.Forms.DbAccess
{
    class OrderDataAccess
    {
        public bool CheckOrderCDExistence(string orderID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //受注IDで一致するデータが存在するか
                flg = context.T_Orders.Any(x => x.OrID.ToString() == orderID);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }

        //仮登録機能
        public bool CheckProductIDExistence(string productID, string orderID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                if (NonMaster.FormOrder.F_Order.provisionalMode == true)
                {
                    //仮登録中の受注詳細テーブル内で商品IDで一致するデータが存在するか
                    flg = context.T_OrderDetailProvisionals.Any(x => x.PrID.ToString() == productID);
                }
                else
                {
                    //受注詳細テーブルの中で商品IDで一致するデータが存在するか
                    var OrderDetailTables = context.T_OrderDetails.Where(x => x.OrID.ToString() == orderID);
                    flg = OrderDetailTables.Any(x => x.PrID.ToString() == productID);
                    context.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }

        //更新機能
        public bool CheckProductIDExistence2(string productID, string orderID, string OrDetailID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                if (NonMaster.FormOrder.F_Order.provisionalMode == true)
                {
                    //現在選択されている受注詳細IDを除く処理(同じ詳細IDで数量だけを更新するとき、重複エラーを出さないようにするための処理)
                    var a = GetOrderProvisionalData();
                    var b = a.Single(x => x.OrDetailID.ToString() == OrDetailID);
                    a.Remove(b);

                    //仮登録中の受注詳細テーブル内で商品IDで一致するデータが存在するか
                    flg = a.Any(x => x.PrID.ToString() == productID);
                }
                else
                {
                    //現在選択されている受注詳細IDを除く処理(同じ詳細IDで数量だけを更新するとき、重複エラーを出さないようにするための処理)
                    var a = context.T_OrderDetails.Where(x => x.OrID.ToString() == orderID).ToList();
                    var b = a.Single(x => x.OrDetailID.ToString() == OrDetailID);
                    a.Remove(b);

                    //受注詳細テーブルの中で商品IDで一致するデータが存在するか
                    flg = a.Any(x => x.PrID.ToString() == productID);
                }
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }

        public bool CheckStateFlagExistence(string orderID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //状態フラグで一致するデータが存在するか
                var OrDate = context.T_Orders.Where(x => x.OrID.ToString() == orderID).ToList();
                flg = OrDate.Any(x => x.OrStateFlag == 1);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }
        public bool CheckFlagExistence(string orderID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //管理フラグで一致するデータが存在するか
                var OrDate = context.T_Orders.Where(x => x.OrID.ToString() == orderID).ToList();
                flg = OrDate.Any(x => x.OrFlag == 2);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }

        public bool CheckOrderDetailCDExistence(string orderDetailID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //受注詳細IDで一致するデータが存在するか
                flg = context.T_OrderDetails.Any(x => x.OrDetailID.ToString() == orderDetailID);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }

        ///////////////////////////////
        //メソッド名：GetOrderData()
        //引　数   ：なし
        //戻り値   ：受注データ
        //機　能   ：受注データの取得
        ///////////////////////////////
        public List<T_OrderDsp> GetOrderData()
        {
            List<T_OrderDsp> order = new List<T_OrderDsp>();
            try
            {
                var context = new SalesManagement_DevContext();
                // tbはIEnumerable型
                var tb = from t1 in context.T_Orders

                         join t2 in context.T_OrderDetails
                         on t1.OrID equals t2.OrID 

                         select new
                         {
                             t1.OrID,
                             t1.SoID,
                             t1.EmID,
                             t1.ClID,
                             t1.ClCharge,
                             t1.OrDate,
                             t1.OrStateFlag,
                             t1.OrFlag,
                             t1.OrHidden,
                             t2.OrDetailID,
                             t2.PrID,
                             t2.OrQuantity,
                             t2.OrTotalPrice,
                         };

                // IEnumerable型のデータをList型へ

                foreach (var p in tb)
                {
                    order.Add(new T_OrderDsp()
                    {
                        OrID = p.OrID,
                        SoID = p.SoID,
                        EmID = p.EmID,
                        ClID = p.ClID,
                        ClCharge = p.ClCharge,
                        OrDate= p.OrDate,
                        OrStateFlag = p.OrStateFlag,
                        OrFlag = p.OrFlag,
                        OrHidden = p.OrHidden,
                        OrDetailID = p.OrDetailID,
                        PrID = p.PrID,
                        OrQuantity= p.OrQuantity,
                        OrTotalPrice = p.OrTotalPrice,
                    });
                }
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return order;

        }

        ///////////////////////////////
        //メソッド名：GetOrderData()
        //引　数   ：なし
        //戻り値   ：仮登録データ
        //機　能   ：仮登録データの取得
        ///////////////////////////////
        public List<Entity.T_OrderProvisionalDsp> GetOrderProvisionalData()
        {
            List<Entity.T_OrderProvisionalDsp> orderProvisional = new List<Entity.T_OrderProvisionalDsp>();
            try
            {
                var context = new SalesManagement_DevContext();
                // tbはIEnumerable型
                var tb = from t1 in context.T_OrderProvisionals

                         join t2 in context.T_OrderDetailProvisionals
                         on t1.OrID equals t2.OrID

                         select new
                         {
                             t1.OrID,
                             t1.SoID,
                             t1.EmID,
                             t1.ClID,
                             t1.ClCharge,
                             t1.OrDate,
                             t1.OrStateFlag,
                             t1.OrFlag,
                             t1.OrHidden,
                             t2.OrDetailID,
                             t2.PrID,
                             t2.OrQuantity,
                             t2.OrTotalPrice,
                         };

                // IEnumerable型のデータをList型へ
                foreach (var p in tb)
                {
                    orderProvisional.Add(new Entity.T_OrderProvisionalDsp()
                    {
                        OrID = p.OrID,
                        SoID = p.SoID,
                        EmID = p.EmID,
                        ClID = p.ClID,
                        ClCharge = p.ClCharge,
                        OrDate = p.OrDate,
                        OrStateFlag = p.OrStateFlag,
                        OrFlag = p.OrFlag,
                        OrHidden = p.OrHidden,
                        OrDetailID = p.OrDetailID,
                        PrID = p.PrID,
                        OrQuantity = p.OrQuantity,
                        OrTotalPrice = p.OrTotalPrice,
                    });
                }
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return orderProvisional;

        }

        public bool AddOrderData(List<T_Order> regOrder)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                context.T_Orders.AddRange(regOrder);
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
        public bool AddOrderDetailData(List<T_OrderDetail> regOrderDetail)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                context.T_OrderDetails.AddRange(regOrderDetail);
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

        //仮登録用
        public bool AddOrderProvisionalData(Entity.T_OrderProvisional regOrderProvisional, bool provisionalMode)
        {
            try
            {
                if(provisionalMode == false)
                {
                    var context = new SalesManagement_DevContext();
                    context.T_OrderProvisionals.Add(regOrderProvisional);
                    context.SaveChanges();
                    context.Dispose();
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public bool AddOrderDetailProvisionalData(Entity.T_OrderDetailProvisional regOrderDetailProvisional)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                context.T_OrderDetailProvisionals.Add(regOrderDetailProvisional);
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
        //メソッド名：GetPositionData()　オーバーロード
        //引　数   ：検索条件
        //戻り値   ：条件一致受注データ
        //機　能   ：条件一致受注データの取得
        ///////////////////////////////
        public List<T_OrderDsp> SearchOrderData(T_OrderDsp selectCondition)
        {
            List<T_OrderDsp> Order = GetOrderData();
            try
            {
                if (NonMaster.FormOrder.F_Order.mOrID != "")
                {
                    Order = Order.Where(x =>
                                                        x.OrID.ToString().Contains(selectCondition.OrID.ToString()) &&
                                                        x.OrStateFlag.ToString().Contains(selectCondition.OrStateFlag.ToString()) &&
                                                        x.OrFlag.ToString().Contains(selectCondition.OrFlag.ToString())).ToList();
                }
                else if (NonMaster.FormOrder.F_Order.mOrDetailID != "")
                {
                    Order = Order.Where(x =>
                                                        x.OrDetailID.ToString().Contains(selectCondition.OrDetailID.ToString()) &&
                                                        x.OrStateFlag.ToString().Contains(selectCondition.OrStateFlag.ToString()) &&
                                                        x.OrFlag.ToString().Contains(selectCondition.OrFlag.ToString())).ToList();
                }
                else if (NonMaster.FormOrder.F_Order.mSoID != "")
                {
                    Order = Order.Where(x =>
                                                        x.SoID.ToString().Contains(selectCondition.SoID.ToString()) &&
                                                        x.OrStateFlag.ToString().Contains(selectCondition.OrStateFlag.ToString()) &&
                                                        x.OrFlag.ToString().Contains(selectCondition.OrFlag.ToString())).ToList();
                }
                else if (NonMaster.FormOrder.F_Order.mEmID != "")
                {
                    Order = Order.Where(x =>
                                                        x.EmID.ToString().Contains(selectCondition.EmID.ToString()) &&
                                                        x.OrStateFlag.ToString().Contains(selectCondition.OrStateFlag.ToString()) &&
                                                        x.OrFlag.ToString().Contains(selectCondition.OrFlag.ToString())).ToList();
                }
                else if (NonMaster.FormOrder.F_Order.mClID != "")
                {
                    Order = Order.Where(x =>
                                                        x.ClID.ToString().Contains(selectCondition.ClID.ToString()) &&
                                                        x.OrStateFlag.ToString().Contains(selectCondition.OrStateFlag.ToString()) &&
                                                        x.OrFlag.ToString().Contains(selectCondition.OrFlag.ToString())).ToList();
                }
                else if (NonMaster.FormOrder.F_Order.mClCharge != "")
                {
                    Order = Order.Where(x =>
                                                        x.ClCharge.Contains(selectCondition.ClCharge) &&
                                                        x.OrStateFlag.ToString().Contains(selectCondition.OrStateFlag.ToString()) &&
                                                        x.OrFlag.ToString().Contains(selectCondition.OrFlag.ToString())).ToList();
                }
                else if (NonMaster.FormOrder.F_Order.mOrStateFlg == 1)
                {
                    Order = Order.Where(x => x.OrStateFlag.ToString().Contains(selectCondition.OrStateFlag.ToString())).ToList();
                }
                else if (NonMaster.FormOrder.F_Order.mOrFlg == 2)
                {
                    Order = Order.Where(x => x.OrFlag.ToString().Contains(selectCondition.OrFlag.ToString())).ToList();
                }
                else if (NonMaster.FormOrder.F_Order.mPrID != "")
                {
                    Order = Order.Where(x =>
                                                        x.PrID.ToString().Contains(selectCondition.PrID.ToString()) &&
                                                        x.OrStateFlag.ToString().Contains(selectCondition.OrStateFlag.ToString()) &&
                                                        x.OrFlag.ToString().Contains(selectCondition.OrFlag.ToString())).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Order;
        }

        //確定検索用
        public List<T_OrderDsp> SearchOrderConfirm(T_OrderDsp selectCondition)
        {
            List<T_OrderDsp> Order = GetOrderData();
            try
            {
                if (NonMaster.FormOrder.F_OrderConfirm.mOrID != "")
                {
                    Order = Order.Where(x =>
                                                        x.OrID.ToString().Contains(selectCondition.OrID.ToString())).ToList();
                }
                else if (NonMaster.FormOrder.F_OrderConfirm.mClID != "")
                {
                    Order = Order.Where(x =>
                                                        x.ClID.ToString().Contains(selectCondition.ClID.ToString())).ToList();
                }
                else if (NonMaster.FormOrder.F_OrderConfirm.mDate != false)
                {
                    Order = Order.Where(x =>
                                                        x.OrDate.ToString().Contains(selectCondition.OrDate.ToString())).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Order;
        }

        ///////////////////////////////
        //メソッド名：UpdatePositionData()
        //引　数   ：受注データ
        //戻り値   ：True or False
        //機　能   ：受注データの更新
        //          ：更新成功の場合True
        //          ：更新失敗の場合False
        ///////////////////////////////
        public bool UpdateOrderData(T_Order updOrder)
        {
            try
            {
                var context = new SalesManagement_DevContext();

                var order = context.T_Orders.Single(x => x.OrID == updOrder.OrID);
                var Chumon = context.T_Chumons.SingleOrDefault(x => x.OrID == updOrder.OrID);
                var Syukko = context.T_Syukkos.SingleOrDefault(x => x.OrID == updOrder.OrID);
                var Arrival = context.T_Arrivals.SingleOrDefault(x => x.OrID == updOrder.OrID);
                var Shipment = context.T_Shipments.SingleOrDefault(x => x.OrID == updOrder.OrID);
                var Sale = context.T_Sales.SingleOrDefault(x => x.ChID == updOrder.OrID);


                if (order.OrStateFlag == 1)
                {
                    if (Forms.NonMaster.FormOrder.F_Order.OrFlg == 0)
                    {
                        //非表示レコードかつ、checkBoxには管理フラグが外されているか(非表示から復元されるか)
                        if (order.OrFlag == 2 && NonMaster.FormOrder.F_Order.OrFlg != 2)
                        {
                            //まず今のレコードの受注IDに一致する注文レコードが存在するか、そしてレコードが注文確定済か(これは在庫減らす処理がされてるかを判断)
                            if (Chumon != null && Chumon.ChStateFlag == 1)
                            {
                                //非表示レコードかつ、checkBoxには管理フラグが外されているか(非表示から復元されるか)
                                if (order.OrFlag == 2 && NonMaster.FormOrder.F_Order.OrFlg != 2)
                                {
                                    int i = 0;
                                    //レコード復元されたときに在庫数を減らす(元に戻す)
                                    var orderDetail = context.T_OrderDetails.Where(x => x.OrID == updOrder.OrID).ToList();
                                    foreach (var p in orderDetail)
                                    {
                                        i++;
                                        //商品IDは重複しない前提
                                        var StockUndo = context.T_Stocks.SingleOrDefault(x => x.PrID == p.PrID);
                                        StockUndo.StQuantity = StockUndo.StQuantity - p.OrQuantity;
                                        if (StockUndo.StQuantity < 0)
                                        {
                                            MessageBox.Show($"'受注ID {updOrder.OrID}'に一致する確定済み'注文ID {Chumon.ChID}'の {i}件目の数量が\n現在庫数よりも多いため復元できません", "在庫不足", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            context.Dispose();
                                            return false;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("確定済みのデータは更新できません\n削除のみ機能します", "更新不可", 0, MessageBoxIcon.Error);
                            return false;
                        }

                        order.OrFlag = updOrder.OrFlag;
                        order.OrHidden = updOrder.OrHidden;

                        if (Chumon != null)
                        {
                            Chumon.ChFlag = updOrder.OrFlag;
                            Chumon.ChHidden = updOrder.OrHidden;
                        }

                        if (Syukko != null)
                        {
                            Syukko.SyFlag = updOrder.OrFlag;
                            Syukko.SyHidden = updOrder.OrHidden;
                        }

                        if (Arrival != null)
                        {
                            Arrival.ArFlag = updOrder.OrFlag;
                            Arrival.ArHidden = updOrder.OrHidden;
                        }

                        if (Shipment != null)
                        {
                            Shipment.ShFlag = updOrder.OrFlag;
                            Shipment.ShHidden = updOrder.OrHidden;
                        }

                        if (Sale != null)
                        {
                            Sale.SaFlag = updOrder.OrFlag;
                            Sale.SaHidden = updOrder.OrHidden;
                        }
                    }
                    else
                    {
                        //まず今のレコードの受注IDに一致する注文レコードが存在するか、そしてレコードが注文確定済か(これは在庫減らす処理がされてるかを判断)
                        if (Chumon != null && Chumon.ChStateFlag == 1)
                        {
                            //注文確定済レコードを非表示にする
                            if (order.OrFlag != 2 && NonMaster.FormOrder.F_Order.OrFlg == 2)
                            {
                                //レコード削除されたときに在庫数を増やす(元に戻す)
                                var orderDetail = context.T_OrderDetails.Where(x => x.OrID == updOrder.OrID).ToList();
                                foreach (var p in orderDetail)
                                {
                                    //商品IDは重複しない前提
                                    var StockUndo = context.T_Stocks.SingleOrDefault(x => x.PrID == p.PrID);
                                    StockUndo.StQuantity = StockUndo.StQuantity + p.OrQuantity;
                                }
                            }
                        }

                        order.OrFlag = updOrder.OrFlag;
                        order.OrHidden = updOrder.OrHidden;

                        if (Chumon != null)
                        {
                            Chumon.ChFlag = updOrder.OrFlag;
                            Chumon.ChHidden = updOrder.OrHidden;
                        }

                        if (Syukko != null)
                        {
                            Syukko.SyFlag = updOrder.OrFlag;
                            Syukko.SyHidden = updOrder.OrHidden;
                        }

                        if (Arrival != null)
                        {
                            Arrival.ArFlag = updOrder.OrFlag;
                            Arrival.ArHidden = updOrder.OrHidden;
                        }

                        if (Shipment != null)
                        {
                            Shipment.ShFlag = updOrder.OrFlag;
                            Shipment.ShHidden = updOrder.OrHidden;
                        }

                        if (Sale != null)
                        {
                            Sale.SaFlag = updOrder.OrFlag;
                            Sale.SaHidden = updOrder.OrHidden;
                        }
                    }
                }
                else //未確定のレコードは通常の更新機能が実行される
                {
                    //まず今のレコードの受注IDに一致する注文レコードが存在するか、そしてレコードが注文確定済か(これは在庫減らす処理がされてるかを判断)
                    if (Chumon != null && Chumon.ChStateFlag == 1)
                    {
                        //注文確定済レコードを非表示にする
                        if (order.OrFlag != 2 && NonMaster.FormOrder.F_Order.OrFlg == 2)
                        {
                            //レコード削除されたときに在庫数を増やす(元に戻す)
                            var orderDetail = context.T_OrderDetails.Where(x => x.OrID == updOrder.OrID).ToList();
                            foreach (var p in orderDetail)
                            {
                                //商品IDは重複しない前提
                                var StockUndo = context.T_Stocks.SingleOrDefault(x => x.PrID == p.PrID);
                                StockUndo.StQuantity = StockUndo.StQuantity + p.OrQuantity;
                            }
                        }
                        //非表示レコードかつ、checkBoxには管理フラグが外されているか(非表示から復元されるか)
                        if (order.OrFlag == 2 && NonMaster.FormOrder.F_Order.OrFlg != 2)
                        {
                            int i = 0;
                            //レコード復元されたときに在庫数を減らす(元に戻す)
                            var orderDetail = context.T_OrderDetails.Where(x => x.OrID == updOrder.OrID).ToList();
                            foreach (var p in orderDetail)
                            {
                                i++;
                                //商品IDは重複しない前提
                                var StockUndo = context.T_Stocks.SingleOrDefault(x => x.PrID == p.PrID);
                                StockUndo.StQuantity = StockUndo.StQuantity - p.OrQuantity;
                                if (StockUndo.StQuantity < 0)
                                {
                                    MessageBox.Show($"'受注ID {updOrder.OrID}'に一致する確定済み'注文ID {Chumon.ChID}'の {i}件目の数量が\n現在庫数よりも多いため復元できません", "在庫不足", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    context.Dispose();
                                    return false;
                                }
                            }
                        }
                    }

                    order.SoID = updOrder.SoID;
                    order.EmID = updOrder.EmID;
                    order.ClID = updOrder.ClID;
                    order.ClCharge = updOrder.ClCharge;
                    order.OrDate = updOrder.OrDate;
                    order.OrFlag = updOrder.OrFlag;
                    order.OrHidden = updOrder.OrHidden;

                    if (Chumon != null)
                    {
                        Chumon.ChFlag = updOrder.OrFlag;
                        Chumon.ChHidden = updOrder.OrHidden;
                    }

                    if (Syukko != null)
                    {
                        Syukko.SyFlag = updOrder.OrFlag;
                        Syukko.SyHidden = updOrder.OrHidden;
                    }

                    if (Arrival != null)
                    {
                        Arrival.ArFlag = updOrder.OrFlag;
                        Arrival.ArHidden = updOrder.OrHidden;
                    }

                    if (Shipment != null)
                    {
                        Shipment.ShFlag = updOrder.OrFlag;
                        Shipment.ShHidden = updOrder.OrHidden;
                    }

                    if (Sale != null)
                    {
                        Sale.SaFlag = updOrder.OrFlag;
                        Sale.SaHidden = updOrder.OrHidden;
                    }
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
        public bool UpdateOrderDetailData(T_OrderDetail updOrderDetail)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                var orderDetail = context.T_OrderDetails.Single(x => x.OrDetailID == updOrderDetail.OrDetailID);
                orderDetail.PrID = updOrderDetail.PrID;
                orderDetail.OrQuantity = updOrderDetail.OrQuantity;
                orderDetail.OrTotalPrice = updOrderDetail.OrTotalPrice;

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
        public bool ConfirmOrderData(T_Order conOrder)
        {
            try
            {
                var context = new SalesManagement_DevContext();

                //確定する1件の受注情報を得る
                var order = context.T_Orders.Single(x => x.OrID == conOrder.OrID);

                //受注情報を注文情報として1件の注文レコードを新規作成
                var chumon = new T_Chumon
                {
                    ClID = order.ClID,
                    EmID = 0, //DBでNULL許可されていないので'0 == NULL'と判断して'0'で登録
                    SoID = order.SoID,
                    OrID = order.OrID,
                    ChDate = DateTime.Now,
                    ChFlag = 0,
                    ChStateFlag = 0,
                    ChHidden = order.OrHidden
                };

                order.OrStateFlag = conOrder.OrStateFlag; //受注状態フラグ'01'を反映
                context.T_Chumons.Add(chumon); //注文テーブル反映(一旦、主キーを自動採番取得のために登録しておく)
                context.SaveChanges();


                //確定する1件の受注情報を得る
                var orderDetail = context.T_OrderDetails.Where(x => x.OrID == conOrder.OrID).ToList();

                // 注文データの取得
                List<T_Chumon> Chumon = GetChumonTable();
                // T_Chumonから、直前にAddした末尾の行を取得する
                T_Chumon lastChumonTable = Chumon[Chumon.Count - 1];
                // 末尾行のIDを取得
                int ChumonID = lastChumonTable.ChID;

                List<T_ChumonDetail> chumonDetail = new List<T_ChumonDetail>();
                foreach (var p in orderDetail)
                {
                    chumonDetail.Add(new T_ChumonDetail()
                    {
                        ChID = ChumonID,
                        PrID = p.PrID,
                        ChQuantity = p.OrQuantity
                    });
                }

                context.T_ChumonDetails.AddRange(chumonDetail); //注文詳細テーブル反映
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
        public bool UpdateOrderDetailProvisionalData(Entity.T_OrderDetailProvisional updOrderDetailProvisional)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                var orderDetailProvisional = context.T_OrderDetailProvisionals.Single(x => x.OrDetailID == updOrderDetailProvisional.OrDetailID);
                orderDetailProvisional.PrID = updOrderDetailProvisional.PrID;
                orderDetailProvisional.OrQuantity = updOrderDetailProvisional.OrQuantity;
                orderDetailProvisional.OrTotalPrice = updOrderDetailProvisional.OrTotalPrice;

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
        //メソッド名：GetChumonData()
        //引　数   ：なし
        //戻り値   ：注文データ
        //機　能   ：注文データの取得
        ///////////////////////////////
        public List<T_Chumon> GetChumonTable()
        {
            List<T_Chumon> chumon = new List<T_Chumon>();
            try
            {
                var context = new SalesManagement_DevContext();
                // tbはIEnumerable型
                var tb = from t1 in context.T_Chumons

                         select new
                         {
                             t1.ChID,
                             t1.SoID,
                             t1.EmID,
                             t1.ClID,
                             t1.OrID,
                             t1.ChDate,
                             t1.ChStateFlag,
                             t1.ChFlag,
                             t1.ChHidden
                         };

                // IEnumerable型のデータをList型へ

                foreach (var p in tb)
                {
                    chumon.Add(new T_Chumon()
                    {
                        ChID = p.ChID,
                        SoID = p.SoID,
                        EmID = p.EmID,
                        ClID = p.ClID,
                        OrID = p.OrID,
                        ChDate = p.ChDate,
                        ChStateFlag = p.ChStateFlag,
                        ChFlag = p.ChFlag,
                        ChHidden = p.ChHidden
                    });
                }
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return chumon;
        }
    }
}
