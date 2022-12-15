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
                var arrival = context.T_Arrivals.Single(x => x.ArID == delArrival.ArID);
                arrival.ArFlag = delArrival.ArFlag;

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
