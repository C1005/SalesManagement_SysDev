using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace SalesManagement_SysDev.Forms.DbAccess
{
    class OrderDetailDataAccess
    {
        public bool CheckOrderCDExistence(string orderDetailID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //役職IDで一致するデータが存在するか
                flg = context.T_OrderDetails.Any(x => x.OrDetailID.ToString() == orderDetailID);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }
   
    }
}
