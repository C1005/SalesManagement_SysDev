using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagement_SysDev.Forms.DbAccess
{
    class StockDataAccess
    {
        ///////////////////////////////
        //メソッド名：CheckStockCDExistence()
        //引　数   ：在庫コード
        //戻り値   ：True or False
        //機　能   ：一致する在庫コードの有無を確認
        //          ：一致データありの場合True
        //          ：一致データなしの場合False
        ///////////////////////////////
        public bool CheckStockCDExistence(string StockID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //在庫IDで一致するデータが存在するか
                flg = context.T_Stocks.Any(x => x.StID.ToString() == StockID);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }
        public bool CheckProductIDExistence(string stockID, string productID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();

                //現在選択されている在庫IDを除く処理(同じ在庫IDで数量だけを更新するとき、重複エラーを出さないようにするための処理)
                var a = AllGetStockData();
                var b = a.Single(x => x.StID.ToString() == stockID);
                a.Remove(b);

                //商品IDで一致するデータが存在するか
                flg = a.Any(x => x.PrID.ToString() == productID);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }

        ///////////////////////////////
        //メソッド名：GetT_StockData()
        //引　数   ：なし
        //戻り値   ：在庫データ
        //機　能   ：在庫データの取得
        ///////////////////////////////
        public List<T_Stock> GetStockData()
        {
            List<T_Stock> Stock = null;
            try
            {
                var context = new SalesManagement_DevContext();
                Stock = context.T_Stocks.Where(x => x.StFlag == 0).ToList();
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Stock;

        }

        ///////////////////////////////
        //メソッド名：AllGetT_StockData()
        //引　数   ：なし
        //戻り値   ：在庫データ
        //機　能   ：在庫データの取得
        ///////////////////////////////
        public List<T_Stock> AllGetStockData()
        {
            List<T_Stock> Stock = new List<T_Stock>();
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
                    Stock.Add(new T_Stock()
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
            return Stock;

        }

        ///////////////////////////////
        //メソッド名：GetStockData()　オーバーロード
        //引　数   ：検索条件
        //戻り値   ：条件一致部署データ
        //機　能   ：条件一致部署データの取得
        ///////////////////////////////
        public List<T_Stock> GetStockData(T_Stock selectCondition)
        {
            List<T_Stock> Stock = new List<T_Stock>();
            try
            {
                if (Master.FormStock.F_Stock.mStID != "")
                {
                    var context = new SalesManagement_DevContext();
                    Stock = context.T_Stocks.Where(x =>
                                                        x.StID.ToString().Contains(selectCondition.StID.ToString()) &&
                                                        x.StFlag.ToString().Contains(selectCondition.StFlag.ToString())).ToList();
                    context.Dispose();
                }
                else if (Master.FormStock.F_Stock.mPrID != "")
                {
                    var context = new SalesManagement_DevContext();
                    Stock = context.T_Stocks.Where(x =>
                                                        x.StFlag.ToString().Contains(selectCondition.StFlag.ToString())).ToList();
                    context.Dispose();
                }
                else if (Master.FormStock.F_Stock.mStFlg == true)
                {
                    var context = new SalesManagement_DevContext();
                    Stock = context.T_Stocks.Where(x => x.StFlag.ToString().Contains(selectCondition.StFlag.ToString())).ToList();
                    context.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Stock;
        }

        ///////////////////////////////
        //メソッド名：AddStockData()
        //引　数   ：在庫データ
        //戻り値   ：True or False
        //機　能   ：在庫データの登録
        //          ：登録成功の場合True
        //          ：登録失敗の場合False
        ///////////////////////////////
        public bool AddStockData(T_Stock regStock)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                context.T_Stocks.Add(regStock);
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
        //メソッド名：UpdateStockData()
        //引　数   ：在庫データ
        //戻り値   ：True or False
        //機　能   ：在庫データの更新
        //          ：更新成功の場合True
        //          ：更新失敗の場合False
        ///////////////////////////////
        public bool UpdateStockData(T_Stock updStock)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                var Stock = context.T_Stocks.Single(x => x.StID == updStock.StID);
                Stock.PrID = updStock.PrID;
                Stock.StQuantity = updStock.StQuantity;
                Stock.StFlag = updStock.StFlag;
                Stock.StHidden = updStock.StHidden;

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
        //メソッド名：DeleteStockData()
        //引　数   ：在庫データ
        //戻り値   ：True or False
        //機　能   ：在庫データの削除
        //          ：削除成功の場合True
        //          ：削除失敗の場合False
        ///////////////////////////////
        public bool DeleteStockData(T_Stock delStock)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                var Stock = context.T_Stocks.Single(x => x.StID == delStock.StID);
                Stock.PrID = delStock.PrID;
                Stock.StQuantity = delStock.StQuantity;
                Stock.StFlag = delStock.StFlag;
                Stock.StHidden = delStock.StHidden;

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
