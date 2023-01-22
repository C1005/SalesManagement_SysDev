using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagement_SysDev.Forms.DbAccess
{
    class WarehousingDataAccess
    {
        public bool CheckWarehousingCDExistence(string warehousingID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //入庫IDで一致するデータが存在するか
                flg = context.T_Warehousings.Any(x => x.WaID.ToString() == warehousingID);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }

        public bool CheckHattyuIDExistence(string warehousingID, string hattyuID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //発注IDで一致するデータを取得し、その入庫IDが一致するか
                var a = context.T_Warehousings.SingleOrDefault(x => x.HaID.ToString() == hattyuID);
                if (a != null && a.WaID == int.Parse(warehousingID))
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
        //メソッド名：GetWarehousingData()
        //引　数   ：なし
        //戻り値   ：受注データ
        //機　能   ：受注データの取得
        ///////////////////////////////
        public List<T_WarehousingDsp> GetWarehousingData()
        {
            List<T_WarehousingDsp> warehousing = new List<T_WarehousingDsp>();
            try
            {
                var context = new SalesManagement_DevContext();
                // tbはIEnumerable型
                var tb = from t1 in context.T_Warehousings

                         join t2 in context.T_WarehousingDetails
                         on t1.WaID equals t2.WaID

                         select new
                         {
                             t1.WaID,
                             t1.HaID,
                             t1.EmID,
                             t1.WaDate,
                             t1.WaShelfFlag,
                             t1.WaHidden,
                             t1.WaFlag,
                             t2.WaDetailID,
                             t2.PrID,
                             t2.WaQuantity
                         };

                // IEnumerable型のデータをList型へ

                foreach (var p in tb)
                {
                    warehousing.Add(new T_WarehousingDsp()
                    {
                        WaID = p.WaID,
                        HaID = p.HaID,
                        EmID = p.EmID,
                        WaDate = p.WaDate,
                        WaShelfFlag = p.WaShelfFlag,
                        WaHidden = p.WaHidden,
                        WaFlag = p.WaFlag,
                        WaDetailID = p.WaDetailID,
                        PrID = p.PrID,
                        WaQuantity = p.WaQuantity,
                    });
                }
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return warehousing;
        }

        ///////////////////////////////
        //メソッド名：DeletePositionData()
        //引　数   ：役職データ
        //戻り値   ：True or False
        //機　能   ：役職データの削除
        //          ：削除成功の場合True
        //          ：削除失敗の場合False
        ///////////////////////////////
        public bool DeleteWarehousingData(T_Warehousing delWarehousing)
        {
            try
            {
                var context = new SalesManagement_DevContext();

                var warehousing = context.T_Warehousings.Single(x => x.WaID == delWarehousing.WaID);
                var hattyu = context.T_Hattyus.Single(x => x.HaID == delWarehousing.HaID);

                //確定された入庫レコードのみ以下を実行
                if (warehousing.WaShelfFlag == 1)
                {
                    //レコード削除されたときに在庫数を減らす(元に戻す)
                    var WarehousingDetail = context.T_WarehousingDetails.Where(x => x.WaID == delWarehousing.WaID).ToList();
                    foreach (var p in WarehousingDetail)
                    {
                        //商品IDは重複しない前提
                        var StockUndo = context.T_Stocks.SingleOrDefault(x => x.PrID == p.PrID);
                        StockUndo.StQuantity = StockUndo.StQuantity - p.WaQuantity;
                    }
                }

                warehousing.WaFlag = delWarehousing.WaFlag;
                warehousing.WaHidden = delWarehousing.WaHidden;
                hattyu.HaFlag = delWarehousing.WaFlag;
                hattyu.HaHidden = delWarehousing.WaHidden;

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
        public List<T_WarehousingDsp> SearchWarehousingConfirm(T_WarehousingDsp selectCondition)
        {
            List<T_WarehousingDsp> Warehousing = GetWarehousingData();
            try
            {
                if (NonMaster.FormWarehousing.F_WarehousingConfirm.mWaID != "")
                {
                    Warehousing = Warehousing.Where(x =>
                                                        x.WaID.ToString().Contains(selectCondition.WaID.ToString())).ToList();
                }
                else if (NonMaster.FormWarehousing.F_WarehousingConfirm.mHaID != "")
                {
                    Warehousing = Warehousing.Where(x =>
                                                        x.HaID.ToString().Contains(selectCondition.HaID.ToString())).ToList();
                }
                else if (NonMaster.FormWarehousing.F_WarehousingConfirm.mDate != false)
                {
                    Warehousing = Warehousing.Where(x =>
                                                        x.WaDate.ToString().Contains(selectCondition.WaDate.ToString())).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Warehousing;
        }

        public bool CheckStateFlagExistence(string warehousingID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //状態フラグで一致するデータが存在するか
                var WaDate = context.T_Warehousings.Where(x => x.WaID.ToString() == warehousingID).ToList();
                flg = WaDate.Any(x => x.WaShelfFlag == 1);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }
        public bool CheckFlagExistence(string warehousingID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //管理フラグで一致するデータが存在するか
                var WaDate = context.T_Warehousings.Where(x => x.WaID.ToString() == warehousingID).ToList();
                flg = WaDate.Any(x => x.WaFlag == 2);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }

        public bool ConfirmWarehousingData(T_Warehousing conWarehousing)
        {
            try
            {
                var context = new SalesManagement_DevContext();

                //確定する1件の入庫情報を得る
                var warehousing = context.T_Warehousings.Single(x => x.WaID == conWarehousing.WaID);
                //確定する1件の入庫詳細情報を得る
                var warehousingDetail = context.T_WarehousingDetails.Where(x => x.WaID == conWarehousing.WaID).ToList();

                foreach (var p in warehousingDetail)
                {
                    // 在庫管理のp.PrID(p件目の商品ID)と一致する入庫詳細テーブルのs.PrID(s件目の商品ID)の1件のレコード取得
                    var StPr = context.T_Stocks.SingleOrDefault(x => x.PrID == p.PrID); //SingleOrDefault メソッドは、要素が複数存在する場合や要素が存在しない場合でも、 null を返すので、例外がスローされることはない。
                    if (StPr != null)
                    {
                        // StPr にデータが正常に取得(在庫管理に存在している商品IDに入庫)
                        StPr.StQuantity = StPr.StQuantity + p.WaQuantity;
                        context.SaveChanges();
                    }
                    else
                    {
                        // StPr にデータ取得失敗(在庫管理に存在しない新しく入庫される商品ID)
                        List<T_Stock> stockAdd = new List<T_Stock>();
                        stockAdd.Add(new T_Stock()
                        {
                            PrID = p.PrID,
                            StQuantity = p.WaQuantity,
                            StFlag = 0,
                            StHidden = ""
                        });
                        context.T_Stocks.AddRange(stockAdd);
                        context.SaveChanges();
                    }
                }

                warehousing.EmID = int.Parse(F_menu.loginEmID);
                warehousing.WaShelfFlag = conWarehousing.WaShelfFlag; //入庫状態フラグ'01'を反映
                warehousing.WaDate = DateTime.Now;
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
        //メソッド名：GetStockData()
        //引　数   ：なし
        //戻り値   ：注文データ
        //機　能   ：注文データの取得
        ///////////////////////////////
        public List<T_Stock> GetStockTable()
        {
            List<T_Stock> stock = new List<T_Stock>();
            try
            {
                var context = new SalesManagement_DevContext();
                // tbはIEnumerable型
                var tb = from t1 in context.T_Stocks

                         select new
                         {
                             t1.StID,
                             t1.PrID,
                             t1.StQuantity,
                             t1.StFlag,
                             t1.StHidden
                         };

                // IEnumerable型のデータをList型へ

                foreach (var p in tb)
                {
                    stock.Add(new T_Stock()
                    {
                        StID = p.StID,
                        PrID = p.PrID,
                        StQuantity = p.StQuantity,
                        StFlag = p.StFlag,
                        StHidden = p.StHidden
                    });
                }
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return stock;
        }
    }
}
