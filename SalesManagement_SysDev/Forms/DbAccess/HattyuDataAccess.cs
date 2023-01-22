using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagement_SysDev.Forms.DbAccess
{
    class HattyuDataAccess
    {
        public bool CheckHattyuCDExistence(string hattyuID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //受注IDで一致するデータが存在するか
                flg = context.T_Hattyus.Any(x => x.HaID.ToString() == hattyuID);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }

        //仮登録機能
        public bool CheckProductIDExistence(string productID, string hattyuID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                if(NonMaster.FormHattyu.F_Hattyu.provisionalMode == true)
                {
                    //仮登録中の発注詳細テーブル内で商品IDで一致するデータが存在するか
                    flg = context.T_HattyuDetailProvisionals.Any(x => x.PrID.ToString() == productID);
                }
                else
                {
                    //発注詳細テーブルの中で商品IDで一致するデータが存在するか
                    var HattyuDetailTables = context.T_HattyuDetails.Where(x => x.HaID.ToString() == hattyuID);
                    flg = HattyuDetailTables.Any(x => x.PrID.ToString() == productID);
                }
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }

        //更新機能
        public bool CheckProductIDExistence2(string productID, string hattyuID, string HaDetailID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                if (NonMaster.FormHattyu.F_Hattyu.provisionalMode == true)
                {
                    //現在選択されている発注詳細IDを除く処理(同じ詳細IDで数量だけを更新するとき、重複エラーを出さないようにするための処理)
                    var a = GetHattyuProvisionalData();
                    var b = a.Single(x => x.HaDetailID.ToString() == HaDetailID);
                    a.Remove(b);

                    //仮登録中の発注詳細テーブル内で商品IDで一致するデータが存在するか
                    flg = a.Any(x => x.PrID.ToString() == productID);
                }
                else
                {
                    //現在選択されている発注詳細IDを除く処理(同じ詳細IDで数量だけを更新するとき、重複エラーを出さないようにするための処理)
                    var a = context.T_HattyuDetails.Where(x => x.HaID.ToString() == hattyuID).ToList();
                    var b = a.Single(x => x.HaDetailID.ToString() == HaDetailID);
                    a.Remove(b);

                    //発注詳細テーブルの中で商品IDで一致するデータが存在するか
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
        public bool CheckHattyuDetailCDExistence(string hattyuDetailID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //受注詳細IDで一致するデータが存在するか
                flg = context.T_HattyuDetails.Any(x => x.HaDetailID.ToString() == hattyuDetailID);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }

        ///////////////////////////////
        //メソッド名：GetHattyuData()
        //引　数   ：なし
        //戻り値   ：受注データ
        //機　能   ：受注データの取得
        ///////////////////////////////
        public List<T_HattyuDsp> GetHattyuData()
        {
            List<T_HattyuDsp> hattyu = new List<T_HattyuDsp>();
            try
            {
                var context = new SalesManagement_DevContext();
                // tbはIEnumerable型
                var tb = from t1 in context.T_Hattyus

                         join t2 in context.T_HattyuDetails
                         on t1.HaID equals t2.HaID

                         select new
                         {
                             t1.HaID,
                             t1.MaID,
                             t1.EmID,
                             t1.HaDate,
                             t1.WaWarehouseFlag,
                             t1.HaFlag,
                             t1.HaHidden,
                             t2.HaDetailID,
                             t2.PrID,
                             t2.HaQuantity
                         };

                // IEnumerable型のデータをList型へ

                foreach (var p in tb)
                {
                    hattyu.Add(new T_HattyuDsp()
                    {
                        HaID = p.HaID,
                        MaID = p.MaID,
                        EmID = p.EmID,
                        HaDate = p.HaDate,
                        WaWarehouseFlag = p.WaWarehouseFlag,
                        HaFlag = p.HaFlag,
                        HaHidden = p.HaHidden,
                        HaDetailID = p.HaDetailID,
                        PrID = p.PrID,
                        HaQuantity = p.HaQuantity,
                    });
                }
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return hattyu;

        }

        ///////////////////////////////
        //メソッド名：GetHattyuData()
        //引　数   ：なし
        //戻り値   ：仮登録データ
        //機　能   ：仮登録データの取得
        ///////////////////////////////
        public List<Entity.T_HattyuProvisionalDsp> GetHattyuProvisionalData()
        {
            List<Entity.T_HattyuProvisionalDsp> hattyuProvisional = new List<Entity.T_HattyuProvisionalDsp>();
            try
            {
                var context = new SalesManagement_DevContext();
                // tbはIEnumerable型
                var tb = from t1 in context.T_HattyuProvisionals

                         join t2 in context.T_HattyuDetailProvisionals
                         on t1.HaID equals t2.HaID

                         select new
                         {
                             t1.HaID,
                             t1.MaID,
                             t1.EmID,
                             t1.HaDate,
                             t1.WaWarehouseFlag,
                             t1.HaFlag,
                             t1.HaHidden,
                             t2.HaDetailID,
                             t2.PrID,
                             t2.HaQuantity,
                         };

                // IEnumerable型のデータをList型へ
                foreach (var p in tb)
                {
                    hattyuProvisional.Add(new Entity.T_HattyuProvisionalDsp()
                    {
                        HaID = p.HaID,
                        MaID = p.MaID,
                        EmID = p.EmID,
                        HaDate = p.HaDate,
                        WaWarehouseFlag = p.WaWarehouseFlag,
                        HaFlag = p.HaFlag,
                        HaHidden = p.HaHidden,
                        HaDetailID = p.HaDetailID,
                        PrID = p.PrID,
                        HaQuantity = p.HaQuantity,
                    });
                }
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return hattyuProvisional;

        }

        public bool AddHattyuData(List<T_Hattyu> regHattyu)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                context.T_Hattyus.AddRange(regHattyu);
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
        public bool AddHattyuDetailData(List<T_HattyuDetail> regHattyuDetail)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                context.T_HattyuDetails.AddRange(regHattyuDetail);
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
        public bool AddHattyuProvisionalData(Entity.T_HattyuProvisional regHattyuProvisional, bool provisionalMode)
        {
            try
            {
                if (provisionalMode == false)
                {
                    var context = new SalesManagement_DevContext();
                    context.T_HattyuProvisionals.Add(regHattyuProvisional);
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
        public bool AddHattyuDetailProvisionalData(Entity.T_HattyuDetailProvisional regHattyuDetailProvisional)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                context.T_HattyuDetailProvisionals.Add(regHattyuDetailProvisional);
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
        public List<T_HattyuDsp> SearchHattyuData(T_HattyuDsp selectCondition)
        {
            List<T_HattyuDsp> Hattyu = GetHattyuData();
            try
            {
                if (NonMaster.FormHattyu.F_Hattyu.mHaID != "")
                {
                    Hattyu = Hattyu.Where(x =>
                                                        x.HaID.ToString().Contains(selectCondition.HaID.ToString()) &&
                                                        x.WaWarehouseFlag.ToString().Contains(selectCondition.WaWarehouseFlag.ToString()) &&
                                                        x.HaFlag.ToString().Contains(selectCondition.HaFlag.ToString())).ToList();
                }
                else if (NonMaster.FormHattyu.F_Hattyu.mHaDetailID != "")
                {
                    Hattyu = Hattyu.Where(x =>
                                                        x.HaDetailID.ToString().Contains(selectCondition.HaDetailID.ToString()) &&
                                                        x.WaWarehouseFlag.ToString().Contains(selectCondition.WaWarehouseFlag.ToString()) &&
                                                        x.HaFlag.ToString().Contains(selectCondition.HaFlag.ToString())).ToList();
                }
                else if (NonMaster.FormHattyu.F_Hattyu.mMaID != "")
                {
                    Hattyu = Hattyu.Where(x =>
                                                        x.MaID.ToString().Contains(selectCondition.MaID.ToString()) &&
                                                        x.WaWarehouseFlag.ToString().Contains(selectCondition.WaWarehouseFlag.ToString()) &&
                                                        x.HaFlag.ToString().Contains(selectCondition.HaFlag.ToString())).ToList();
                }
                else if (NonMaster.FormHattyu.F_Hattyu.mEmID != "")
                {
                    Hattyu = Hattyu.Where(x =>
                                                        x.EmID.ToString().Contains(selectCondition.EmID.ToString()) &&
                                                        x.WaWarehouseFlag.ToString().Contains(selectCondition.WaWarehouseFlag.ToString()) &&
                                                        x.HaFlag.ToString().Contains(selectCondition.HaFlag.ToString())).ToList();
                }
                else if (NonMaster.FormHattyu.F_Hattyu.mPrID != "")
                {
                    Hattyu = Hattyu.Where(x =>
                                                        x.PrID.ToString().Contains(selectCondition.PrID.ToString()) &&
                                                        x.WaWarehouseFlag.ToString().Contains(selectCondition.WaWarehouseFlag.ToString()) &&
                                                        x.HaFlag.ToString().Contains(selectCondition.HaFlag.ToString())).ToList();
                }
                else if (NonMaster.FormHattyu.F_Hattyu.mWaWarehouseFlg == 1)
                {
                    Hattyu = Hattyu.Where(x => x.WaWarehouseFlag.ToString().Contains(selectCondition.WaWarehouseFlag.ToString())).ToList();
                }
                else if (NonMaster.FormHattyu.F_Hattyu.mHaFlg == 2)
                {
                    Hattyu = Hattyu.Where(x => x.HaFlag.ToString().Contains(selectCondition.HaFlag.ToString())).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Hattyu;
        }

        //確定検索用
        //public List<T_HattyuDsp> SearchHattyuConfirm(T_HattyuDsp selectCondition)
        //{
        //    List<T_HattyuDsp> Hattyu = GetHattyuData();
        //    try
        //    {
        //        if (NonMaster.FormHattyu.F_HattyuUpdate.mHaID != "")
        //        {
        //            Hattyu = Hattyu.Where(x =>
        //                                                x.HaID.ToString().Contains(selectCondition.HaID.ToString())).ToList();
        //        }
        //        else if (NonMaster.FormHattyu.F_HattyuUpdate.mClID != "")
        //        {
        //            Hattyu = Hattyu.Where(x =>
        //                                                x.ClID.ToString().Contains(selectCondition.ClID.ToString())).ToList();
        //        }
        //        else if (NonMaster.FormHattyu.F_HattyuUpdate.mDate != false)
        //        {
        //            Hattyu = Hattyu.Where(x =>
        //                                                x.HaDate.ToString().Contains(selectCondition.HaDate.ToString())).ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    return Hattyu;
        //}

        ///////////////////////////////
        //メソッド名：UpdatePositionData()
        //引　数   ：受注データ
        //戻り値   ：True or False
        //機　能   ：受注データの更新
        //          ：更新成功の場合True
        //          ：更新失敗の場合False
        ///////////////////////////////
        public bool UpdateHattyuData(T_Hattyu updHattyu)
        {
            try
            {
                var context = new SalesManagement_DevContext();

                var hattyu = context.T_Hattyus.Single(x => x.HaID == updHattyu.HaID);
                var Warehousing = context.T_Warehousings.SingleOrDefault(x => x.HaID == updHattyu.HaID);

                if(hattyu.WaWarehouseFlag == 1)
                {
                    if (Forms.NonMaster.FormHattyu.F_Hattyu.HaFlg == 0)
                    {
                        //非表示レコードかつ、checkBoxには管理フラグが外されているか(非表示から復元されるか)
                        if (hattyu.HaFlag == 2 && NonMaster.FormHattyu.F_Hattyu.HaFlg == 0)
                        {
                            //まず今のレコードの発注IDに一致する入庫レコードが存在するか、そしてレコードが入庫確定済か(これは在庫減らす処理がされてるかを判断)
                            if (Warehousing != null && Warehousing.WaShelfFlag == 1)
                            {
                                //非表示レコードかつ、checkBoxには管理フラグが外されているか(非表示から復元されるか)
                                if (hattyu.HaFlag == 2 && NonMaster.FormHattyu.F_Hattyu.HaFlg != 2)
                                {
                                    int i = 0;
                                    //レコード復元されたときに在庫数を増やす(元に戻す)
                                    var hattyuDetail = context.T_HattyuDetails.Where(x => x.HaID == updHattyu.HaID).ToList();
                                    foreach (var p in hattyuDetail)
                                    {
                                        i++;
                                        //商品IDは重複しない前提
                                        var StockUndo = context.T_Stocks.SingleOrDefault(x => x.PrID == p.PrID);
                                        StockUndo.StQuantity = StockUndo.StQuantity + p.HaQuantity;
                                        if (StockUndo.StQuantity < 0)
                                        {
                                            MessageBox.Show($"'発注ID {updHattyu.HaID}'に一致する確定済み'入庫ID {Warehousing.WaID}'の {i}件目の数量が\n現在庫数よりも多いため復元できません", "在庫不足", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                        hattyu.HaFlag = updHattyu.HaFlag;
                        hattyu.HaHidden = updHattyu.HaHidden;

                        if (Warehousing != null)
                        {
                            Warehousing.WaFlag = updHattyu.HaFlag;
                            Warehousing.WaHidden = updHattyu.HaHidden;
                        }
                    }
                    else
                    {
                        //まず今のレコードの発注IDに一致する入庫レコードが存在するか、そしてレコードが入庫確定済か(これは在庫減らす処理がされてるかを判断)
                        if (Warehousing != null && Warehousing.WaShelfFlag == 1)
                        {
                            //入庫確定済レコードを非表示にする
                            if (hattyu.HaFlag != 2 && NonMaster.FormHattyu.F_Hattyu.HaFlg == 2)
                            {
                                //レコード削除されたときに在庫数を減らす(元に戻す)
                                var hattyuDetail = context.T_HattyuDetails.Where(x => x.HaID == updHattyu.HaID).ToList();
                                foreach (var p in hattyuDetail)
                                {
                                    //商品IDは重複しない前提
                                    var StockUndo = context.T_Stocks.SingleOrDefault(x => x.PrID == p.PrID);
                                    StockUndo.StQuantity = StockUndo.StQuantity - p.HaQuantity;
                                }
                            }
                        }
                        hattyu.HaFlag = updHattyu.HaFlag;
                        hattyu.HaHidden = updHattyu.HaHidden;

                        if (Warehousing != null)
                        {
                            Warehousing.WaFlag = updHattyu.HaFlag;
                            Warehousing.WaHidden = updHattyu.HaHidden;
                        }
                    }
                }
                else //未確定のレコードは通常の更新機能が実行される
                {
                    //まず今のレコードの発注IDに一致する入庫レコードが存在するか、そしてレコードが入庫確定済か(これは在庫減らす処理がされてるかを判断)
                    if (Warehousing != null && Warehousing.WaShelfFlag == 1)
                    {
                        //入庫確定済レコードを非表示にする
                        if (hattyu.HaFlag != 2 && NonMaster.FormHattyu.F_Hattyu.HaFlg == 2)
                        {
                            //レコード削除されたときに在庫数を減らす(元に戻す)
                            var hattyuDetail = context.T_HattyuDetails.Where(x => x.HaID == updHattyu.HaID).ToList();
                            foreach (var p in hattyuDetail)
                            {
                                //商品IDは重複しない前提
                                var StockUndo = context.T_Stocks.SingleOrDefault(x => x.PrID == p.PrID);
                                StockUndo.StQuantity = StockUndo.StQuantity - p.HaQuantity;
                            }
                        }
                        //非表示レコードかつ、checkBoxには管理フラグが外されているか(非表示から復元されるか)
                        if (hattyu.HaFlag == 2 && NonMaster.FormHattyu.F_Hattyu.HaFlg != 2)
                        {
                            int i = 0;
                            //レコード復元されたときに在庫数を増やす(元に戻す)
                            var hattyuDetail = context.T_HattyuDetails.Where(x => x.HaID == updHattyu.HaID).ToList();
                            foreach (var p in hattyuDetail)
                            {
                                i++;
                                //商品IDは重複しない前提
                                var StockUndo = context.T_Stocks.SingleOrDefault(x => x.PrID == p.PrID);
                                StockUndo.StQuantity = StockUndo.StQuantity + p.HaQuantity;
                                if (StockUndo.StQuantity < 0)
                                {
                                    MessageBox.Show($"'発注ID {updHattyu.HaID}'に一致する確定済み'入庫ID {Warehousing.WaID}'の {i}件目の数量が\n現在庫数よりも多いため復元できません", "在庫不足", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    context.Dispose();
                                    return false;
                                }
                            }
                        }
                    }
                    hattyu.MaID = updHattyu.MaID;
                    hattyu.EmID = updHattyu.EmID;
                    hattyu.HaDate = updHattyu.HaDate;
                    hattyu.HaFlag = updHattyu.HaFlag;
                    hattyu.HaHidden = updHattyu.HaHidden;

                    if (Warehousing != null)
                    {
                        Warehousing.WaFlag = updHattyu.HaFlag;
                        Warehousing.WaHidden = updHattyu.HaHidden;
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

        public bool UpdateHattyuDetailData(T_HattyuDetail updHattyuDetail)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                var hattyuDetail = context.T_HattyuDetails.Single(x => x.HaDetailID == updHattyuDetail.HaDetailID);
                hattyuDetail.PrID = updHattyuDetail.PrID;
                hattyuDetail.HaQuantity = updHattyuDetail.HaQuantity;

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

        public bool UpdateHattyuDetailProvisionalData(Entity.T_HattyuDetailProvisional updHattyuDetailProvisional)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                var hattyuDetailProvisional = context.T_HattyuDetailProvisionals.Single(x => x.HaDetailID == updHattyuDetailProvisional.HaDetailID);
                hattyuDetailProvisional.PrID = updHattyuDetailProvisional.PrID;
                hattyuDetailProvisional.HaQuantity = updHattyuDetailProvisional.HaQuantity;

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
        public List<T_HattyuDsp> SearchHattyuConfirm(T_HattyuDsp selectCondition)
        {
            List<T_HattyuDsp> Hattyu = GetHattyuData();
            try
            {
                if (NonMaster.FormHattyu.F_HattyuUpdate.mHaID != "")
                {
                    Hattyu = Hattyu.Where(x =>
                                                        x.HaID.ToString().Contains(selectCondition.HaID.ToString())).ToList();
                }
                else if (NonMaster.FormHattyu.F_HattyuUpdate.mMaID != "")
                {
                    Hattyu = Hattyu.Where(x =>
                                                        x.MaID.ToString().Contains(selectCondition.MaID.ToString())).ToList();
                }
                else if (NonMaster.FormHattyu.F_HattyuUpdate.mDate != false)
                {
                    Hattyu = Hattyu.Where(x =>
                                                        x.HaDate.ToString().Contains(selectCondition.HaDate.ToString())).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Hattyu;
        }

        public bool CheckStateFlagExistence(string hattyuID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //状態フラグで一致するデータが存在するか
                var HaDate = context.T_Hattyus.Where(x => x.HaID.ToString() == hattyuID).ToList();
                flg = HaDate.Any(x => x.WaWarehouseFlag == 1);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }
        public bool CheckFlagExistence(string hattyuID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //管理フラグで一致するデータが存在するか
                var HaDate = context.T_Hattyus.Where(x => x.HaID.ToString() == hattyuID).ToList();
                flg = HaDate.Any(x => x.HaFlag == 2);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }

        public bool ConfirmHattyuData(T_Hattyu conHattyu)
        {
            try
            {
                var context = new SalesManagement_DevContext();

                //確定する1件の発注情報を得る
                var hattyu = context.T_Hattyus.Single(x => x.HaID == conHattyu.HaID);

                //発注情報を入庫情報として1件の注文レコードを新規作成
                var warehousing = new T_Warehousing
                {
                    HaID = hattyu.HaID,
                    EmID = hattyu.EmID,
                    WaDate = DateTime.Now,
                    WaFlag = 0,
                    WaShelfFlag = 0,
                    WaHidden = hattyu.HaHidden
                };

                hattyu.WaWarehouseFlag = conHattyu.WaWarehouseFlag; //発注状態フラグ'01'を反映
                context.T_Warehousings.Add(warehousing); //入庫テーブル反映(一旦、主キーを自動採番取得のために登録しておく)
                context.SaveChanges();

                //確定する1件の発注情報を得る
                var hattyuDetail = context.T_HattyuDetails.Where(x => x.HaID == conHattyu.HaID).ToList();

                // 入庫データの取得
                List<T_Warehousing> Warehousing = GetWarehousingTable();
                // T_Warehousingから、直前にAddした末尾の行を取得する
                T_Warehousing lastWarehousingTable = Warehousing[Warehousing.Count - 1];
                // 末尾行のIDを取得
                int WarehousingID = lastWarehousingTable.WaID;

                List<T_WarehousingDetail> warehousingDetail = new List<T_WarehousingDetail>();
                foreach (var p in hattyuDetail)
                {
                    warehousingDetail.Add(new T_WarehousingDetail()
                    {
                        WaID = WarehousingID,
                        PrID = p.PrID,
                        WaQuantity = p.HaQuantity
                    });
                }

                context.T_WarehousingDetails.AddRange(warehousingDetail); //入庫詳細テーブル反映
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
        //メソッド名：GetWarehousingData()
        //引　数   ：なし
        //戻り値   ：入庫データ
        //機　能   ：入庫データの取得
        ///////////////////////////////
        public List<T_Warehousing> GetWarehousingTable()
        {
            List<T_Warehousing> warehousing = new List<T_Warehousing>();
            try
            {
                var context = new SalesManagement_DevContext();
                // tbはIEnumerable型
                var tb = from t1 in context.T_Warehousings

                         select new
                         {
                             t1.WaID,
                             t1.HaID,
                             t1.EmID,
                             t1.WaDate,
                             t1.WaShelfFlag,
                             t1.WaFlag,
                             t1.WaHidden
                         };

                // IEnumerable型のデータをList型へ

                foreach (var p in tb)
                {
                    warehousing.Add(new T_Warehousing()
                    {
                        WaID = p.WaID,
                        HaID = p.HaID,
                        EmID = p.EmID,
                        WaDate = p.WaDate,
                        WaShelfFlag = p.WaShelfFlag,
                        WaFlag = p.WaFlag,
                        WaHidden = p.WaHidden
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
    }
}
