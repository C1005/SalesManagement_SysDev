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
                var chumon = context.T_Chumons.Single(x => x.ChID == delChumon.ChID);
                chumon.ChFlag = delChumon.ChFlag;

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
