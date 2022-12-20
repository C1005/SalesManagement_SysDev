using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagement_SysDev.Forms.DbAccess
{
    class EmployeeDataAccess
    {
        ///////////////////////////////
        //メソッド名：CheckEmployeeCDExistence()
        //引　数   ：役職コード
        //戻り値   ：True or False
        //機　能   ：一致する役職コードの有無を確認
        //          ：一致データありの場合True
        //          ：一致データなしの場合False
        ///////////////////////////////
        public bool CheckEmployeeCDExistence(string EmployeeID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //役職IDで一致するデータが存在するか
                flg = context.M_Employees.Any(x => x.EmID.ToString() == EmployeeID);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }


        ///////////////////////////////
        //メソッド名：GetEmployeeData()
        //引　数   ：なし
        //戻り値   ：役職データ
        //機　能   ：役職データの取得
        ///////////////////////////////
        public List<M_Employee> GetEmployeeData()
        {
            List<M_Employee> Employee = null;
            try
            {
                var context = new SalesManagement_DevContext();
                Employee = context.M_Employees.Where(x => x.EmFlag == 0).ToList();
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Employee;

        }
        ///////////////////////////////
        //メソッド名：GetEmployeeData()　オーバーロード
        //引　数   ：検索条件
        //戻り値   ：条件一致部署データ
        //機　能   ：条件一致部署データの取得
        ///////////////////////////////
        public List<M_Employee> GetEmployeeData(M_Employee selectcondition)
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
                else if (Master.FormEmployee.F_Employee.mSoID != "")
                {
                    var context = new SalesManagement_DevContext();
                    Employee = context.M_Employees.Where(x =>
                                                        x.SoID.ToString().Contains(selectcondition.SoID.ToString()) &&
                                                        x.EmFlag.ToString().Contains(selectcondition.EmFlag.ToString())).ToList();
                    context.Dispose();
                }
                else if (Master.FormEmployee.F_Employee.mEmName != "")
                {
                    var context = new SalesManagement_DevContext();
                    Employee = context.M_Employees.Where(x =>
                                                        x.EmName.Contains(selectcondition.EmName) &&
                                                        x.EmFlag.ToString().Contains(selectcondition.EmFlag.ToString())).ToList();
                    context.Dispose();
                }
                else if (Master.FormEmployee.F_Employee.mEmFlg == true)
                {
                    var context = new SalesManagement_DevContext();
                    Employee = context.M_Employees.Where(x => x.EmFlag.ToString().Contains(selectcondition.EmFlag.ToString())).ToList();
                    context.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Employee;

        }

        public List<M_Employee> GetEmployeeFlag(M_Employee selectCondition)
        {
            List<M_Employee> Employee = new List<M_Employee>();
            try
            {
                var context = new SalesManagement_DevContext();
                Employee = context.M_Employees.Where(x => x.EmFlag.ToString().Contains(selectCondition.EmFlag.ToString())).ToList();
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Employee;

        }

        ///////////////////////////////
        //メソッド名：GetEmployeeDspData()
        //引　数   ：なし
        //戻り値   ：表示用役職データ
        //機　能   ：表示用役職データの取得
        ///////////////////////////////
        public List<M_Employee> GetEmployeeDspData()
        {
            List<M_Employee> Employee = null;
            try
            {
                var context = new SalesManagement_DevContext();
                Employee = context.M_Employees.Where(x => x.EmFlag == 0).ToList();
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Employee;

        }

        ///////////////////////////////
        //メソッド名：AddEmployeeData()
        //引　数   ：役職データ
        //戻り値   ：True or False
        //機　能   ：役職データの登録
        //          ：登録成功の場合True
        //          ：登録失敗の場合False
        ///////////////////////////////
        public bool AddEmployeeData(M_Employee regEmployee)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                context.M_Employees.Add(regEmployee);
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
        //メソッド名：UpdateEmployeeData()
        //引　数   ：役職データ
        //戻り値   ：True or False
        //機　能   ：役職データの更新
        //          ：更新成功の場合True
        //          ：更新失敗の場合False
        ///////////////////////////////
        public bool UpdateEmployeeData(M_Employee updEmployee)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                var Employee = context.M_Employees.Single(x => x.EmID == updEmployee.EmID);

                Employee.EmName = updEmployee.EmName;
                Employee.EmHiredate = updEmployee.EmHiredate;
                Employee.PoID = updEmployee.PoID;
                Employee.SoID = updEmployee.SoID;
                Employee.EmPhone = updEmployee.EmPhone;

                // パスワード入力時のみ更新
                if (!String.IsNullOrEmpty(updEmployee.EmPassword))
                    Employee.EmPassword = updEmployee.EmPassword;
                Employee.EmFlag = updEmployee.EmFlag;
                Employee.EmHidden = updEmployee.EmHidden;

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
        //メソッド名：DeleteEmployeeData()
        //引　数   ：役職データ
        //戻り値   ：True or False
        //機　能   ：役職データの削除
        //          ：削除成功の場合True
        //          ：削除失敗の場合False
        ///////////////////////////////
        public bool DeleteEmployeeData(M_Employee delEmployee)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                var Employee = context.M_Employees.Single(x => x.EmID == delEmployee.EmID);
                Employee.EmName = delEmployee.EmName;
                Employee.EmFlag = delEmployee.EmFlag;
                Employee.EmHidden = delEmployee.EmHidden;

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
