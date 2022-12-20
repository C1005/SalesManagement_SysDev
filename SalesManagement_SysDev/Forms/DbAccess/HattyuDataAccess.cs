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
                hattyu.MaID = updHattyu.MaID;
                hattyu.EmID = updHattyu.EmID;
                hattyu.HaDate = updHattyu.HaDate;
                hattyu.WaWarehouseFlag = updHattyu.WaWarehouseFlag;
                hattyu.HaFlag = updHattyu.HaFlag;
                hattyu.HaHidden = hattyu.HaHidden;

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
        //public bool ConfirmHattyuData(T_Hattyu conHattyu)
        //{
        //    try
        //    {
        //        var context = new SalesManagement_DevContext();
        //        var hattyu = context.T_Hattyus.Single(x => x.HaID == conHattyu.HaID);
        //        hattyu.WaWarehouseFlag = conHattyu.WaWarehouseFlag;

        //        context.SaveChanges();
        //        context.Dispose();

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false;
        //    }
        //}
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
    }
}
