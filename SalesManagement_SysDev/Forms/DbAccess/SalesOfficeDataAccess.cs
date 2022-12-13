using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagement_SysDev.Forms.DbAccess
{
    class SalesOfficeDataAccess
    {
        ///////////////////////////////
        //メソッド名：CheckSalesOfficeCDExistence()
        //引　数   ：営業所コード
        //戻り値   ：True or False
        //機　能   ：一致する営業所コードの有無を確認
        //          ：一致データありの場合True
        //          ：一致データなしの場合False
        ///////////////////////////////
        public bool CheckSalesOfficeCDExistence(string salesOfficeID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //営業所IDで一致するデータが存在するか
                flg = context.M_SalesOffices.Any(x => x.SoID.ToString() == salesOfficeID);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }


        ///////////////////////////////
        //メソッド名：GetSalesOfficeData()
        //引　数   ：なし
        //戻り値   ：営業所データ
        //機　能   ：営業所データの取得
        ///////////////////////////////
        public List<M_SalesOffice> GetSalesOfficeData()
        {
            List<M_SalesOffice> SalesOffice = null;
            try
            {
                var context = new SalesManagement_DevContext();
                SalesOffice = context.M_SalesOffices.Where(x => x.SoFlag == 0).ToList();
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return SalesOffice;

        }

        ///////////////////////////////
        //メソッド名：GetSalesOfficeData()　オーバーロード
        //引　数   ：検索条件
        //戻り値   ：条件一致部署データ
        //機　能   ：条件一致部署データの取得
        ///////////////////////////////
        public List<M_SalesOffice> GetSalesOfficeData(M_SalesOffice selectCondition)
        {
            List<M_SalesOffice> SalesOffice = new List<M_SalesOffice>();
            try
            {
                if (Master.FormEmployee.F_SalesOffice.mSoID != "")
                {
                    var context = new SalesManagement_DevContext();
                    SalesOffice = context.M_SalesOffices.Where(x =>
                                                        x.SoID.ToString().Contains(selectCondition.SoID.ToString()) &&
                                                        x.SoFlag.ToString().Contains(selectCondition.SoFlag.ToString())).ToList();
                    context.Dispose();
                }
                else if (Master.FormEmployee.F_SalesOffice.mSoName != "")
                {
                    var context = new SalesManagement_DevContext();
                    SalesOffice = context.M_SalesOffices.Where(x =>
                                                        x.SoName.Contains(selectCondition.SoName) &&
                                                        x.SoFlag.ToString().Contains(selectCondition.SoFlag.ToString())).ToList();
                    context.Dispose();
                }
                else if (Master.FormEmployee.F_SalesOffice.mSoFlg == true)
                {
                    var context = new SalesManagement_DevContext();
                    SalesOffice = context.M_SalesOffices.Where(x => x.SoFlag.ToString().Contains(selectCondition.SoFlag.ToString())).ToList();
                    context.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return SalesOffice;
        }

        ///////////////////////////////
        //メソッド名：AddSalesOfficeData()
        //引　数   ：営業所データ
        //戻り値   ：True or False
        //機　能   ：営業所データの登録
        //          ：登録成功の場合True
        //          ：登録失敗の場合False
        ///////////////////////////////
        public bool AddSalesOfficeData(M_SalesOffice regSalesOffice)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                context.M_SalesOffices.Add(regSalesOffice);
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
        //メソッド名：UpdateSalesOfficeData()
        //引　数   ：営業所データ
        //戻り値   ：True or False
        //機　能   ：営業所データの更新
        //          ：更新成功の場合True
        //          ：更新失敗の場合False
        ///////////////////////////////
        public bool UpdateSalesOfficeData(M_SalesOffice updSalesOffice)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                var SalesOffice = context.M_SalesOffices.Single(x => x.SoID == updSalesOffice.SoID);
                SalesOffice.SoName = updSalesOffice.SoName;
                SalesOffice.SoFlag = updSalesOffice.SoFlag;
                SalesOffice.SoHidden = updSalesOffice.SoHidden;

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
        //メソッド名：DeleteSalesOfficeData()
        //引　数   ：営業所データ
        //戻り値   ：True or False
        //機　能   ：営業所データの削除
        //          ：削除成功の場合True
        //          ：削除失敗の場合False
        ///////////////////////////////
        public bool DeleteSalesOfficeData(M_SalesOffice delSalesOffice)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                var SalesOffice = context.M_SalesOffices.Single(x => x.SoID == delSalesOffice.SoID);
                SalesOffice.SoName = delSalesOffice.SoName;
                SalesOffice.SoFlag = delSalesOffice.SoFlag;
                SalesOffice.SoHidden = delSalesOffice.SoHidden;

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
