using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagement_SysDev.Forms.DbAccess
{
    
    class PositionDataAccess
    {

        ///////////////////////////////
        //メソッド名：CheckPositionCDExistence()
        //引　数   ：役職コード
        //戻り値   ：True or False
        //機　能   ：一致する役職コードの有無を確認
        //          ：一致データありの場合True
        //          ：一致データなしの場合False
        ///////////////////////////////
        public bool CheckPositionCDExistence(string positionID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //役職IDで一致するデータが存在するか
                flg = context.M_Positions.Any(x => x.PoID.ToString() == positionID);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }


        ///////////////////////////////
        //メソッド名：GetPositionData()
        //引　数   ：なし
        //戻り値   ：役職データ
        //機　能   ：役職データの取得
        ///////////////////////////////
        public List<M_Position> GetPositionData()
        {
            List<M_Position> position = null;
            try
            {
                var context = new SalesManagement_DevContext();
                position = context.M_Positions.Where(x => x.PoFlag == 0).ToList();
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return position;

        }

        ///////////////////////////////
        //メソッド名：GetPositionData()　オーバーロード
        //引　数   ：検索条件
        //戻り値   ：条件一致部署データ
        //機　能   ：条件一致部署データの取得
        ///////////////////////////////
        public List<M_Position> GetPositionData(M_Position selectCondition)
        {
            List<M_Position> Position = new List<M_Position>();
            try
            {
                if (Master.FormEmployee.F_Position.mPoID != "")
                {
                    var context = new SalesManagement_DevContext();
                    Position = context.M_Positions.Where(x =>
                                                        x.PoID.ToString().Contains(selectCondition.PoID.ToString()) &&
                                                        x.PoFlag.ToString().Contains(selectCondition.PoFlag.ToString())).ToList();
                    context.Dispose();
                }
                else if (Master.FormEmployee.F_Position.mPoName != "")
                {
                    var context = new SalesManagement_DevContext();
                    Position = context.M_Positions.Where(x =>
                                                        x.PoName.Contains(selectCondition.PoName) &&
                                                        x.PoFlag.ToString().Contains(selectCondition.PoFlag.ToString())).ToList();
                    context.Dispose();
                }
                else if (Master.FormEmployee.F_Position.mPoFlg == true)
                {
                    var context = new SalesManagement_DevContext();
                    Position = context.M_Positions.Where(x => x.PoFlag.ToString().Contains(selectCondition.PoFlag.ToString())).ToList();
                    context.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Position;

        }

        public List<M_Position> GetPositionFlag(M_Position selectCondition)
        {
            List<M_Position> position = new List<M_Position>();
            try
            {
                var context = new SalesManagement_DevContext();
                position = context.M_Positions.Where(x => x.PoFlag.ToString().Contains(selectCondition.PoFlag.ToString())).ToList();
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return position;

        }

        ///////////////////////////////
        //メソッド名：GetPositionDspData()
        //引　数   ：なし
        //戻り値   ：表示用役職データ
        //機　能   ：表示用役職データの取得
        ///////////////////////////////
        public List<M_Position> GetPositionDspData()
        {
            List<M_Position> position = null;
            try
            {
                var context = new SalesManagement_DevContext();
                position = context.M_Positions.Where(x => x.PoFlag == 0).ToList();
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return position;

        }

        ///////////////////////////////
        //メソッド名：AddPositionData()
        //引　数   ：役職データ
        //戻り値   ：True or False
        //機　能   ：役職データの登録
        //          ：登録成功の場合True
        //          ：登録失敗の場合False
        ///////////////////////////////
        public bool AddPositionData(M_Position regPosition)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                context.M_Positions.Add(regPosition);
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
        //メソッド名：UpdatePositionData()
        //引　数   ：役職データ
        //戻り値   ：True or False
        //機　能   ：役職データの更新
        //          ：更新成功の場合True
        //          ：更新失敗の場合False
        ///////////////////////////////
        public bool UpdatePositionData(M_Position updPosition)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                var position = context.M_Positions.Single(x => x.PoID == updPosition.PoID);
                position.PoName = updPosition.PoName;
                position.PoFlag = updPosition.PoFlag;
                position.PoHidden = updPosition.PoHidden;

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
        //メソッド名：DeletePositionData()
        //引　数   ：役職データ
        //戻り値   ：True or False
        //機　能   ：役職データの削除
        //          ：削除成功の場合True
        //          ：削除失敗の場合False
        ///////////////////////////////
        public bool DeletePositionData(M_Position delPosition)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                var position = context.M_Positions.Single(x => x.PoID == delPosition.PoID);
                position.PoName = delPosition.PoName;
                position.PoFlag = delPosition.PoFlag;
                position.PoHidden = delPosition.PoHidden;

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
