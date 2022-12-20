using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagement_SysDev.Forms.DbAccess
{
    class MakerDataAccess
    {
        ///////////////////////////////
        //メソッド名：CheckMakerCDExistence()
        //引　数   ：メーカコード
        //戻り値   ：True or False
        //機　能   ：一致するメーカコードの有無を確認
        //          ：一致データありの場合True
        //          ：一致データなしの場合False
        ///////////////////////////////
        public bool CheckMakerCDExistence(string makerID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //メーカIDで一致するデータが存在するか
                flg = context.M_Makers.Any(x => x.MaID.ToString() == makerID);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }


        ///////////////////////////////
        //メソッド名：GetMakerData()
        //引　数   ：なし
        //戻り値   ：メーカデータ
        //機　能   ：メーカデータの取得
        ///////////////////////////////
        public List<M_Maker> GetMakerData()
        {
            List<M_Maker> maker = null;
            try
            {
                var context = new SalesManagement_DevContext();
                maker = context.M_Makers.Where(x => x.MaFlag == 0).ToList();
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return maker;

        }

        ///////////////////////////////
        //メソッド名：GetMakerData()　オーバーロード
        //引　数   ：検索条件
        //戻り値   ：条件一致部署データ
        //機　能   ：条件一致部署データの取得
        ///////////////////////////////
        public List<M_Maker> GetMakerData(M_Maker selectCondition)
        {
            List<M_Maker> Maker = new List<M_Maker>();
            try
            {
                if (Master.FormProduct.F_Maker.mMaID != "")
                {
                    var context = new SalesManagement_DevContext();
                    Maker = context.M_Makers.Where(x =>
                                                        x.MaID.ToString().Contains(selectCondition.MaID.ToString()) &&
                                                        x.MaFlag.ToString().Contains(selectCondition.MaFlag.ToString())).ToList();
                    context.Dispose();
                }
                else if (Master.FormProduct.F_Maker.mMaName != "")
                {
                    var context = new SalesManagement_DevContext();
                    Maker = context.M_Makers.Where(x =>
                                                        x.MaName.Contains(selectCondition.MaName) &&
                                                        x.MaFlag.ToString().Contains(selectCondition.MaFlag.ToString())).ToList();
                    context.Dispose();
                }
                else if (Master.FormProduct.F_Maker.mMaFlg == true)
                {
                    var context = new SalesManagement_DevContext();
                    Maker = context.M_Makers.Where(x => x.MaFlag.ToString().Contains(selectCondition.MaFlag.ToString())).ToList();
                    context.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Maker;

        }

        public List<M_Maker> GetMakerFlag(M_Maker selectCondition)
        {
            List<M_Maker> maker = new List<M_Maker>();
            try
            {
                var context = new SalesManagement_DevContext();
                maker = context.M_Makers.Where(x => x.MaFlag.ToString().Contains(selectCondition.MaFlag.ToString())).ToList();
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return maker;

        }

        ///////////////////////////////
        //メソッド名：GetMakerDspData()
        //引　数   ：なし
        //戻り値   ：表示用メーカデータ
        //機　能   ：表示用メーカデータの取得
        ///////////////////////////////
        public List<M_Maker> GetMakerDspData()
        {
            List<M_Maker> maker = null;
            try
            {
                var context = new SalesManagement_DevContext();
                maker = context.M_Makers.Where(x => x.MaFlag == 0).ToList();
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return maker;

        }

        ///////////////////////////////
        //メソッド名：AddMakerData()
        //引　数   ：メーカデータ
        //戻り値   ：True or False
        //機　能   ：メーカデータの登録
        //          ：登録成功の場合True
        //          ：登録失敗の場合False
        ///////////////////////////////
        public bool AddMakerData(M_Maker regMaker)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                context.M_Makers.Add(regMaker);
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
        //メソッド名：UpdateMakerData()
        //引　数   ：メーカデータ
        //戻り値   ：True or False
        //機　能   ：メーカデータの更新
        //          ：更新成功の場合True
        //          ：更新失敗の場合False
        ///////////////////////////////
        public bool UpdateMakerData(M_Maker updMaker)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                var maker = context.M_Makers.Single(x => x.MaID == updMaker.MaID);
                maker.MaName = updMaker.MaName;
                maker.MaFlag = updMaker.MaFlag;
                maker.MaHidden = updMaker.MaHidden;
                maker.MaPostal = updMaker.MaPostal;
                maker.MaAdress = updMaker.MaAdress;
                maker.MaFAX = updMaker.MaFAX;
                maker.MaPhone = updMaker.MaPhone;

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
        //メソッド名：DeleteMakerData()
        //引　数   ：メーカデータ
        //戻り値   ：True or False
        //機　能   ：メーカデータの削除
        //          ：削除成功の場合True
        //          ：削除失敗の場合False
        ///////////////////////////////
        public bool DeleteMakerData(M_Maker delMaker)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                var maker = context.M_Makers.Single(x => x.MaID == delMaker.MaID);
                maker.MaName = delMaker.MaName;
                maker.MaFlag = delMaker.MaFlag;
                maker.MaHidden = delMaker.MaHidden;
                maker.MaPostal = delMaker.MaPostal;
                maker.MaAdress = delMaker.MaAdress;
                maker.MaFAX = delMaker.MaFAX;
                maker.MaPhone = delMaker.MaPhone;

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
