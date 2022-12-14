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


        public List<int> GetOrIDRange(int startNo, int endNo)
        {
            if (startNo == endNo)
            {
                return new List<int>();
            }
            if (startNo > endNo)
            {
                return Enumerable.Range(endNo, startNo - endNo + 1).Reverse().ToList();
            }
            return Enumerable.Range(startNo, endNo - startNo + 1).ToList();
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
                order.SoID = updOrder.SoID;
                order.EmID = updOrder.EmID;
                order.ClID = updOrder.ClID;
                order.ClCharge = updOrder.ClCharge;
                order.OrDate = updOrder.OrDate;
                order.OrStateFlag = updOrder.OrStateFlag;
                order.OrFlag = updOrder.OrFlag;
                order.OrHidden = order.OrHidden;

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
                var order = context.T_Orders.Single(x => x.OrID == conOrder.OrID);
                order.OrStateFlag = conOrder.OrStateFlag;

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
    }
}
