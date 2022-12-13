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
    }
}
