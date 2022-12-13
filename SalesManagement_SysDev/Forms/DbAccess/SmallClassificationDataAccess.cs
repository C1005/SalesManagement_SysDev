using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagement_SysDev.Forms.DbAccess
{
    class SmallClassificationDataAccess
    {
        ///////////////////////////////
        //メソッド名：ChecksmallClassificationCDExistence()
        //引　数   ：役職コード
        //戻り値   ：True or False
        //機　能   ：一致する役職コードの有無を確認
        //          ：一致データありの場合True
        //          ：一致データなしの場合False
        ///////////////////////////////
        public bool CheckSmallClassificationCDExistence(string smallClassificationID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //小分類IDで一致するデータが存在するか
                flg = context.M_SmallClassifications.Any(x => x.ScID.ToString() == smallClassificationID);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }

        ///////////////////////////////
        //メソッド名：GetSmallClassificationData()
        //引　数   ：なし
        //戻り値   ：役職データ
        //機　能   ：役職データの取得
        ///////////////////////////////
        public List<M_SmallClassification> GetSmallClassificationData()
        {
            List<M_SmallClassification> smallClassification = null;
            try
            {
                var context = new SalesManagement_DevContext();
                smallClassification = context.M_SmallClassifications.Where(x => x.ScFlag == 0).ToList();
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return smallClassification;

        }

        ///////////////////////////////
        //メソッド名：GetSmallClassificationData()　オーバーロード
        //引　数   ：検索条件
        //戻り値   ：条件一致部署データ
        //機　能   ：条件一致部署データの取得
        ///////////////////////////////
        public List<M_SmallClassification> GetSmallClassificationData(M_SmallClassification selectCondition)
        {
            List<M_SmallClassification> SmallClassification = new List<M_SmallClassification>();
            try
            {
                if (Master.FormProduct.F_SmallClassification.mScID != "")
                {
                    var context = new SalesManagement_DevContext();
                    SmallClassification = context.M_SmallClassifications.Where(x =>
                                                        x.ScID.ToString().Contains(selectCondition.ScID.ToString()) &&
                                                        x.ScFlag.ToString().Contains(selectCondition.ScFlag.ToString())).ToList();
                    context.Dispose();
                }
                else if (Master.FormProduct.F_SmallClassification.mMcID != "")
                {
                    var context = new SalesManagement_DevContext();
                    SmallClassification = context.M_SmallClassifications.Where(x =>
                                                        x.McID.ToString().Contains(selectCondition.McID.ToString()) &&
                                                        x.ScFlag.ToString().Contains(selectCondition.ScFlag.ToString())).ToList();
                    context.Dispose();
                }
                else if (Master.FormProduct.F_SmallClassification.mScName != "")
                {
                    var context = new SalesManagement_DevContext();
                    SmallClassification = context.M_SmallClassifications.Where(x =>
                                                        x.ScName.Contains(selectCondition.ScName) &&
                                                        x.ScFlag.ToString().Contains(selectCondition.ScFlag.ToString())).ToList();
                    context.Dispose();
                }
                else if (Master.FormProduct.F_SmallClassification.mScFlg == true)
                {
                    var context = new SalesManagement_DevContext();
                    SmallClassification = context.M_SmallClassifications.Where(x => x.ScFlag.ToString().Contains(selectCondition.ScFlag.ToString())).ToList();
                    context.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return SmallClassification;

        }

        public List<M_SmallClassification> GetSmallClassificationFlag(M_SmallClassification selectCondition)
        {
            List<M_SmallClassification> smallClassification = new List<M_SmallClassification>();
            try
            {
                var context = new SalesManagement_DevContext();
                smallClassification = context.M_SmallClassifications.Where(x => x.ScFlag.ToString().Contains(selectCondition.ScFlag.ToString())).ToList();
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return smallClassification;

        }

        ///////////////////////////////
        //メソッド名：GetSmallClassificationDspData()
        //引　数   ：なし
        //戻り値   ：表示用役職データ
        //機　能   ：表示用役職データの取得
        ///////////////////////////////
        public List<M_SmallClassification> GetSmallClassificationDspData()
        {
            List<M_SmallClassification> smallClassification = null;
            try
            {
                var context = new SalesManagement_DevContext();
                smallClassification = context.M_SmallClassifications.Where(x => x.ScFlag == 0).ToList();
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return smallClassification;

        }

        ///////////////////////////////
        //メソッド名：AddSmallClassificationData()
        //引　数   ：役職データ
        //戻り値   ：True or False
        //機　能   ：役職データの登録
        //          ：登録成功の場合True
        //          ：登録失敗の場合False
        ///////////////////////////////
        public bool AddSmallClassificationData(M_SmallClassification regSmallClassification)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                context.M_SmallClassifications.Add(regSmallClassification);
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
        //メソッド名：UpdateSmallClassificationData()
        //引　数   ：役職データ
        //戻り値   ：True or False
        //機　能   ：役職データの更新
        //          ：更新成功の場合True
        //          ：更新失敗の場合False
        ///////////////////////////////
        public bool UpdateSmallClassificationData(M_SmallClassification updSmallClassification)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                var smallClassification = context.M_SmallClassifications.Single(x => x.ScID == updSmallClassification.ScID);
                smallClassification.ScName = updSmallClassification.ScName;
                smallClassification.ScFlag = updSmallClassification.ScFlag;
                smallClassification.ScHidden = updSmallClassification.ScHidden;

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
        //メソッド名：DeleteSmallClassificationData()
        //引　数   ：役職データ
        //戻り値   ：True or False
        //機　能   ：役職データの削除
        //          ：削除成功の場合True
        //          ：削除失敗の場合False
        ///////////////////////////////
        public bool DeleteSmallClassificationData(M_SmallClassification delSmallClassification)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                var smallClassification = context.M_SmallClassifications.Single(x => x.ScID == delSmallClassification.ScID);
                smallClassification.ScName = delSmallClassification.ScName;
                smallClassification.ScFlag = delSmallClassification.ScFlag;
                smallClassification.ScHidden = delSmallClassification.ScHidden;

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
