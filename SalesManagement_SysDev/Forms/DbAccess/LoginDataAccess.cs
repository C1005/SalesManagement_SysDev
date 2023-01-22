using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagement_SysDev.Forms.DbAccess
{
    class LoginDataAccess
    {
        ///////////////////////////////
        //メソッド名：GetLoginData()　オーバーロード
        //引　数   ：検索条件
        //戻り値   ：条件一致部署データ
        //機　能   ：条件一致部署データの取得
        ///////////////////////////////
        public List<M_Employee> GetLoginData(M_Employee selectcondition)
        {
            List<M_Employee> Employee = new List<M_Employee>();
            try
            {
                if (Master.FormEmployee.F_Employee.mEmID != "")
                {
                    var context = new SalesManagement_DevContext();
                    Employee = context.M_Employees.Where(x =>
                                                        x.EmID.ToString().Contains(selectcondition.EmID.ToString()) &&
                                                        x.EmFlag.ToString().Contains(selectcondition.EmFlag.ToString())).ToList();
                    context.Dispose();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Employee;

        }

        //public bool SelectLoginData()
        //{
        //    string UserID = F_Login.mUserID;
        //    string PW = F_Login.mPW;

        //    List<M_Employee> Employee = new List<M_Employee>();
        //    try
        //    {
        //        var context = new SalesManagement_DevContext();
        //        if(UserID.ToString())
        //        context.Dispose();

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false;
        //    }
        //}
    }
}
