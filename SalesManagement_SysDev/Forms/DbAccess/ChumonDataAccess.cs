using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagement_SysDev.Forms.DbAccess
{
    class ChumonDataAccess
    {
        public bool CheckChumonCDExistence(string chumonID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //注文IDで一致するデータが存在するか
                flg = context.T_Chumons.Any(x => x.ChID.ToString() == chumonID);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }

        public bool CheckOrderIDExistence(string chumonID, string orderID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //受注IDで一致するデータを取得し、その注文IDが一致するか
                var a = context.T_Chumons.SingleOrDefault(x => x.OrID.ToString() == orderID);
                if (a != null && a.ChID == int.Parse(chumonID))
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

        public bool CheckStateFlagExistence(string chumonID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //状態フラグで一致するデータが存在するか
                var ChDate = context.T_Chumons.Where(x => x.ChID.ToString() == chumonID).ToList();
                flg = ChDate.Any(x => x.ChStateFlag == 1);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }

        public bool CheckFlagExistence(string chumonID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //管理フラグで一致するデータが存在するか
                var ChDate = context.T_Chumons.Where(x => x.ChID.ToString() == chumonID).ToList();
                flg = ChDate.Any(x => x.ChFlag == 2);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }

        ///////////////////////////////
        //メソッド名：GetChumonData()
        //引　数   ：なし
        //戻り値   ：注文データ
        //機　能   ：注文データの取得
        ///////////////////////////////
        public List<T_ChumonDsp> GetChumonData()
        {
            List<T_ChumonDsp> chumon = new List<T_ChumonDsp>();
            try
            {
                var context = new SalesManagement_DevContext();
                // tbはIEnumerable型
                var tb = from t1 in context.T_Chumons

                         join t2 in context.T_ChumonDetails
                         on t1.ChID equals t2.ChID

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
                             t1.ChHidden,
                             t2.ChDetailID,
                             t2.PrID,
                             t2.ChQuantity
                         };

                // IEnumerable型のデータをList型へ

                foreach (var p in tb)
                {
                    chumon.Add(new T_ChumonDsp()
                    {
                        ChID = p.ChID,
                        SoID = p.SoID,
                        EmID = p.EmID,
                        ClID = p.ClID,
                        OrID = p.OrID,
                        ChDate = p.ChDate,
                        ChStateFlag = p.ChStateFlag,
                        ChFlag = p.ChFlag,
                        ChHidden = p.ChHidden,
                        ChDetailID = p.ChDetailID,
                        PrID = p.PrID,
                        ChQuantity = p.ChQuantity,
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

        ///////////////////////////////
        //メソッド名：GetChumonData()
        //引　数   ：なし
        //戻り値   ：注文データ
        //機　能   ：注文データの取得
        ///////////////////////////////
        public List<T_ChumonDetail> GetChumonDetailData()
        {
            List<T_ChumonDetail> chumonDetail = new List<T_ChumonDetail>();
            try
            {
                var context = new SalesManagement_DevContext();
                // tbはIEnumerable型
                var tb = from t1 in context.T_ChumonDetails

                         select new
                         {
                             t1.ChID,
                             t1.ChDetailID,
                             t1.PrID,
                             t1.ChQuantity
                         };

                // IEnumerable型のデータをList型へ

                foreach (var p in tb)
                {
                    chumonDetail.Add(new T_ChumonDetail()
                    {
                        ChID = p.ChID,
                        ChDetailID = p.ChDetailID,
                        PrID = p.PrID,
                        ChQuantity = p.ChQuantity,
                    });
                }
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return chumonDetail;

        }

        ///////////////////////////////
        //メソッド名：GetPositionData()　オーバーロード
        //引　数   ：検索条件
        //戻り値   ：条件一致受注データ
        //機　能   ：条件一致受注データの取得
        ///////////////////////////////
        public List<T_ChumonDsp> SearchChumonData(T_ChumonDsp selectCondition)
        {
            List<T_ChumonDsp> Chumon = GetChumonData();
            try
            {
                if (NonMaster.FormChumon.F_Chumon.mChID != "")
                {
                    Chumon = Chumon.Where(x =>
                                                        x.ChID.ToString().Contains(selectCondition.ChID.ToString()) &&
                                                        x.ChStateFlag.ToString().Contains(selectCondition.ChStateFlag.ToString()) &&
                                                        x.ChFlag.ToString().Contains(selectCondition.ChFlag.ToString())).ToList();
                }
                else if (NonMaster.FormChumon.F_Chumon.mChDetailID != "")
                {
                    Chumon = Chumon.Where(x =>
                                                        x.ChDetailID.ToString().Contains(selectCondition.ChDetailID.ToString()) &&
                                                        x.ChStateFlag.ToString().Contains(selectCondition.ChStateFlag.ToString()) &&
                                                        x.ChFlag.ToString().Contains(selectCondition.ChFlag.ToString())).ToList();
                }
                else if (NonMaster.FormChumon.F_Chumon.mSoID != "")
                {
                    Chumon = Chumon.Where(x =>
                                                        x.SoID.ToString().Contains(selectCondition.SoID.ToString()) &&
                                                        x.ChStateFlag.ToString().Contains(selectCondition.ChStateFlag.ToString()) &&
                                                        x.ChFlag.ToString().Contains(selectCondition.ChFlag.ToString())).ToList();
                }
                else if (NonMaster.FormChumon.F_Chumon.mEmID != "")
                {
                    Chumon = Chumon.Where(x =>
                                                        x.EmID.ToString().Contains(selectCondition.EmID.ToString()) &&
                                                        x.ChStateFlag.ToString().Contains(selectCondition.ChStateFlag.ToString()) &&
                                                        x.ChFlag.ToString().Contains(selectCondition.ChFlag.ToString())).ToList();
                }
                else if (NonMaster.FormChumon.F_Chumon.mClID != "")
                {
                    Chumon = Chumon.Where(x =>
                                                        x.ClID.ToString().Contains(selectCondition.ClID.ToString()) &&
                                                        x.ChStateFlag.ToString().Contains(selectCondition.ChStateFlag.ToString()) &&
                                                        x.ChFlag.ToString().Contains(selectCondition.ChFlag.ToString())).ToList();
                }
                else if (NonMaster.FormChumon.F_Chumon.mOrID != "")
                {
                    Chumon = Chumon.Where(x =>
                                                        x.OrID.ToString().Contains(selectCondition.OrID.ToString()) &&
                                                        x.ChStateFlag.ToString().Contains(selectCondition.ChStateFlag.ToString()) &&
                                                        x.ChFlag.ToString().Contains(selectCondition.ChFlag.ToString())).ToList();
                }
                else if (NonMaster.FormChumon.F_Chumon.mChStateFlg == 1)
                {
                    Chumon = Chumon.Where(x => x.ChStateFlag.ToString().Contains(selectCondition.ChStateFlag.ToString())).ToList();
                }
                else if (NonMaster.FormChumon.F_Chumon.mChFlg == 2)
                {
                    Chumon = Chumon.Where(x => x.ChFlag.ToString().Contains(selectCondition.ChFlag.ToString())).ToList();
                }
                else if (NonMaster.FormChumon.F_Chumon.mPrID != "")
                {
                    Chumon = Chumon.Where(x =>
                                                        x.PrID.ToString().Contains(selectCondition.PrID.ToString()) &&
                                                        x.ChStateFlag.ToString().Contains(selectCondition.ChStateFlag.ToString()) &&
                                                        x.ChFlag.ToString().Contains(selectCondition.ChFlag.ToString())).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Chumon;
        }

        ///////////////////////////////
        //メソッド名：DeletePositionData()
        //引　数   ：役職データ
        //戻り値   ：True or False
        //機　能   ：役職データの削除
        //          ：削除成功の場合True
        //          ：削除失敗の場合False
        ///////////////////////////////
        public bool DeleteChumonData(T_Chumon delChumon)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                var Order = context.T_Orders.Single(x => x.OrID == delChumon.OrID);
                var Chumon = context.T_Chumons.Single(x => x.OrID == delChumon.OrID);
                var Syukko = context.T_Syukkos.SingleOrDefault(x => x.OrID == delChumon.OrID);
                var Arrival = context.T_Arrivals.SingleOrDefault(x => x.OrID == delChumon.OrID);
                var Shipment = context.T_Shipments.SingleOrDefault(x => x.OrID == delChumon.OrID);
                var Sale = context.T_Sales.SingleOrDefault(x => x.ChID == delChumon.OrID);

                //確定された注文レコードのみ以下を実行
                if (Chumon.ChStateFlag == 1)
                {
                    //レコード削除されたときに在庫数を増やす(元に戻す)
                    var ChumonDetail = context.T_ChumonDetails.Where(x => x.ChID == delChumon.ChID).ToList();
                    foreach (var p in ChumonDetail)
                    {
                        //商品IDは重複しない前提
                        var StockUndo = context.T_Stocks.SingleOrDefault(x => x.PrID == p.PrID);
                        StockUndo.StQuantity = StockUndo.StQuantity + p.ChQuantity;
                    }
                }

                Order.OrFlag = delChumon.ChFlag;
                Order.OrHidden = delChumon.ChHidden;
                Chumon.ChFlag = delChumon.ChFlag;
                Chumon.ChHidden = delChumon.ChHidden;

                if (Syukko != null)
                {
                    Syukko.SyFlag = delChumon.ChFlag;
                    Syukko.SyHidden = delChumon.ChHidden;
                }

                if (Arrival != null)
                {
                    Arrival.ArFlag = delChumon.ChFlag;
                    Arrival.ArHidden = delChumon.ChHidden;
                }

                if (Shipment != null)
                {
                    Shipment.ShFlag = delChumon.ChFlag;
                    Shipment.ShHidden = delChumon.ChHidden;
                }

                if (Sale != null)
                {
                    Sale.SaFlag = delChumon.ChFlag;
                    Sale.SaHidden = delChumon.ChHidden;
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
        public List<T_ChumonDsp> SearchChumonConfirm(T_ChumonDsp selectCondition)
        {
            List<T_ChumonDsp> Chumon = GetChumonData();
            try
            {
                if (NonMaster.FormChumon.F_ChumonConfirm.mOrID != "")
                {
                    Chumon = Chumon.Where(x =>
                                                        x.OrID.ToString().Contains(selectCondition.OrID.ToString())).ToList();
                }
                else if (NonMaster.FormChumon.F_ChumonConfirm.mClID != "")
                {
                    Chumon = Chumon.Where(x =>
                                                        x.ClID.ToString().Contains(selectCondition.ClID.ToString())).ToList();
                }
                else if (NonMaster.FormChumon.F_ChumonConfirm.mDate != false)
                {
                    Chumon = Chumon.Where(x =>
                                                        x.ChDate.ToString().Contains(selectCondition.ChDate.ToString())).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Chumon;
        }

        public bool ConfirmChumonData(T_Chumon conChumon)
        {
            try
            {
                var context = new SalesManagement_DevContext();

                //conChumonの注文IDと詳細テーブルの注文IDで一致するレコードを取得
                int chID = conChumon.ChID;
                var Chumon = context.T_ChumonDetails.Where(x => x.ChID == chID).ToList();

                int i = 0;
                foreach (var item in Chumon)
                {
                    i++;
                    // 在庫数 = 現在庫数 - 注文数
                    var stock = context.T_Stocks.Where(x => x.PrID == item.PrID).ToList();
                    if(stock.Count == 0)
                    {
                        MessageBox.Show($"'注文ID {conChumon.ChID}'の {i}件目の商品IDが\n在庫管理の商品IDで一致するデータがありませんでした。\nその商品IDを新規発注してください。", "条件不一致", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        context.Dispose();
                        return false;
                    }
                    foreach (var p in stock)
                    {
                        p.StQuantity = p.StQuantity - item.ChQuantity;
                        if (p.StQuantity < 0)
                        {
                            MessageBox.Show($"'注文ID {conChumon.ChID}'の {i}件目の数量が現在庫数よりも多いため\n注文を確定できません。足りない数量分発注してください。", "在庫不足", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            context.Dispose();
                            return false;
                        }
                    }
                }
                context.SaveChanges();


                //確定する1件の注文情報を得る
                var chumon = context.T_Chumons.Single(x => x.ChID == conChumon.ChID);

                //注文情報を出庫情報として1件の出庫レコードを新規作成
                var syukko = new T_Syukko
                {
                    ClID = chumon.ClID,
                    EmID = 0, //DBでNULL許可されていないので'0 == NULL'と判断して'0'で登録
                    SoID = chumon.SoID,
                    OrID = chumon.OrID,
                    SyDate = null,
                    SyFlag = 0,
                    SyStateFlag = 0,
                    SyHidden = chumon.ChHidden
                };

                chumon.ChStateFlag = conChumon.ChStateFlag;
                chumon.EmID = int.Parse(F_menu.loginEmID); //社員IDを確定を行った社員IDで変更
                context.T_Syukkos.Add(syukko); //出庫テーブル反映(一旦、主キーを自動採番取得のために登録しておく)
                context.SaveChanges();


                //確定する1件の注文情報を得る
                var chumonDetail = context.T_ChumonDetails.Where(x => x.ChID == conChumon.ChID).ToList();

                // 出庫データの取得
                List<T_Syukko> Syukko = GetSyukkoTable();
                // T_Syukkoから、直前にAddした末尾の行を取得する
                T_Syukko lastSyukkoTable = Syukko[Syukko.Count - 1];
                // 末尾行のIDを取得
                int SyukkoID = lastSyukkoTable.SyID;

                List<T_SyukkoDetail> syukkoDetail = new List<T_SyukkoDetail>();
                foreach (var p in chumonDetail)
                {
                    syukkoDetail.Add(new T_SyukkoDetail()
                    {
                        SyID = SyukkoID,
                        PrID = p.PrID,
                        SyQuantity = p.ChQuantity
                    });
                }

                context.T_SyukkoDetails.AddRange(syukkoDetail); //出庫詳細テーブル反映
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
        //戻り値   ：出庫データ
        //機　能   ：出庫データの取得
        ///////////////////////////////
        public List<T_Syukko> GetSyukkoTable()
        {
            List<T_Syukko> syukko = new List<T_Syukko>();
            try
            {
                var context = new SalesManagement_DevContext();
                // tbはIEnumerable型
                var tb = from t1 in context.T_Syukkos

                         select new
                         {
                             t1.SyID,
                             t1.SoID,
                             t1.EmID,
                             t1.ClID,
                             t1.OrID,
                             t1.SyDate,
                             t1.SyStateFlag,
                             t1.SyFlag,
                             t1.SyHidden
                         };

                // IEnumerable型のデータをList型へ

                foreach (var p in tb)
                {
                    syukko.Add(new T_Syukko()
                    {
                        SyID = p.SyID,
                        SoID = p.SoID,
                        EmID = p.EmID,
                        ClID = p.ClID,
                        OrID = p.OrID,
                        SyDate = p.SyDate,
                        SyStateFlag = p.SyStateFlag,
                        SyFlag = p.SyFlag,
                        SyHidden = p.SyHidden
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
    }
}
