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
                var syukko = context.T_Syukkos.Single(x => x.SyID == delSyukko.SyID);
                syukko.SyFlag = delSyukko.SyFlag;

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
                         on t1.SyID equals t2.SyDetailID

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
    }
}
