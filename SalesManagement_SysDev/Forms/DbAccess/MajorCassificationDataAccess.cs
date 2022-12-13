using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SalesManagement_SysDev.Forms.DbAccess
{
    class MajorCassificationDataAccess
    {
        ///////////////////////////////
        //メソッド名：CheckMajorCassificationCDExistence()
        //引　数   ：役職コード
        //戻り値   ：True or False
        //機　能   ：一致する役職コードの有無を確認
        //          ：一致データありの場合True
        //          ：一致データなしの場合False
        ///////////////////////////////
        public bool CheckMajorClassificationCDExistence(string majorClassificationID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //役職IDで一致するデータが存在するか
                flg = context.M_MajorCassifications.Any(x => x.McID.ToString() == majorClassificationID);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }


        ///////////////////////////////
        //メソッド名：GetMajorCassificationData()
        //引　数   ：なし
        //戻り値   ：役職データ
        //機　能   ：役職データの取得
        ///////////////////////////////
        public List<M_MajorCassification> GetMajorClassificationData()
        {
            List<M_MajorCassification> majorClassification = null;
            try
            {
                var context = new SalesManagement_DevContext();
                majorClassification = context.M_MajorCassifications.Where(x => x.McFlag == 0).ToList();
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return majorClassification;

        }

        ///////////////////////////////
        //メソッド名：GetMajorCassificationData()　オーバーロード
        //引　数   ：検索条件
        //戻り値   ：条件一致部署データ
        //機　能   ：条件一致部署データの取得
        ///////////////////////////////
        public List<M_MajorCassification> GetMajorClassificationData(M_MajorCassification selectCondition)
        {
            List<M_MajorCassification> MajorCassification = new List<M_MajorCassification>();
            try
            {
                if (Master.FormProduct.F_MajorCassification.mMcID != "")
                {
                    var context = new SalesManagement_DevContext();
                    MajorCassification = context.M_MajorCassifications.Where(x =>
                                                        x.McID.ToString().Contains(selectCondition.McID.ToString()) &&
                                                        x.McFlag.ToString().Contains(selectCondition.McFlag.ToString())).ToList();
                    context.Dispose();
                }
                else if (Master.FormProduct.F_MajorCassification.mMcName != "")
                {
                    var context = new SalesManagement_DevContext();
                    MajorCassification = context.M_MajorCassifications.Where(x =>
                                                        x.McName.Contains(selectCondition.McName) &&
                                                        x.McFlag.ToString().Contains(selectCondition.McFlag.ToString())).ToList();
                    context.Dispose();
                }
                else if (Master.FormProduct.F_MajorCassification.mMcFlg == true)
                {
                    var context = new SalesManagement_DevContext();
                    MajorCassification = context.M_MajorCassifications.Where(x => x.McFlag.ToString().Contains(selectCondition.McFlag.ToString())).ToList();
                    context.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return MajorCassification;

        }

        public List<M_MajorCassification> GetMajorClassificationFlag(M_MajorCassification selectCondition)
        {
            List<M_MajorCassification> majorCassification = new List<M_MajorCassification>();
            try
            {
                var context = new SalesManagement_DevContext();
                majorCassification = context.M_MajorCassifications.Where(x => x.McFlag.ToString().Contains(selectCondition.McFlag.ToString())).ToList();
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return majorCassification;

        }

        ///////////////////////////////
        //メソッド名：GetMajorCassificationDspData()
        //引　数   ：なし
        //戻り値   ：表示用役職データ
        //機　能   ：表示用役職データの取得
        ///////////////////////////////
        public List<M_MajorCassification> GetMajorClassificationDspData()
        {
            List<M_MajorCassification> majorCassification = null;
            try
            {
                var context = new SalesManagement_DevContext();
                majorCassification = context.M_MajorCassifications.Where(x => x.McFlag == 0).ToList();
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return majorCassification;

        }

        ///////////////////////////////
        //メソッド名：AddMajorCassificationData()
        //引　数   ：役職データ
        //戻り値   ：True or False
        //機　能   ：役職データの登録
        //          ：登録成功の場合True
        //          ：登録失敗の場合False
        ///////////////////////////////
        public bool AddMajorCassificationData(M_MajorCassification regMajorCassification)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                context.M_MajorCassifications.Add(regMajorCassification);
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
        //メソッド名：UpdateMajorCassificationData()
        //引　数   ：役職データ
        //戻り値   ：True or False
        //機　能   ：役職データの更新
        //          ：更新成功の場合True
        //          ：更新失敗の場合False
        ///////////////////////////////
        public bool UpdateMajorCassificationData(M_MajorCassification updMajorCassification)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                var majorCassification = context.M_MajorCassifications.Single(x => x.McID == updMajorCassification.McID);
                majorCassification.McName = updMajorCassification.McName;
                majorCassification.McFlag = updMajorCassification.McFlag;
                majorCassification.McHidden = updMajorCassification.McHidden;

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
        //メソッド名：DeleteMajorCassificationData()
        //引　数   ：役職データ
        //戻り値   ：True or False
        //機　能   ：役職データの削除
        //          ：削除成功の場合True
        //          ：削除失敗の場合False
        ///////////////////////////////
        public bool DeleteMajorCassificationData(M_MajorCassification delMajorCassification)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                var majorCassification = context.M_MajorCassifications.Single(x => x.McID == delMajorCassification.McID);
                majorCassification.McName = delMajorCassification.McName;
                majorCassification.McFlag = delMajorCassification.McFlag;
                majorCassification.McHidden = delMajorCassification.McHidden;

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
