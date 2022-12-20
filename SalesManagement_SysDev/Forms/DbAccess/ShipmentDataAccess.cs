using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagement_SysDev.Forms.DbAccess
{
    class ShipmentDataAccess
    {
        ///////////////////////////////
        //メソッド名：CheckShipmentCDExistence()
        //引　数   ：役職コード
        //戻り値   ：True or False
        //機　能   ：一致する役職コードの有無を確認
        //          ：一致データありの場合True
        //          ：一致データなしの場合False
        ///////////////////////////////
        public bool CheckShipmentCDExistence(string ShipmentID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //役職IDで一致するデータが存在するか
                flg = context.T_Shipments.Any(x => x.ShID.ToString() == ShipmentID);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }


        ///////////////////////////////
        //メソッド名：GetShipmentData()
        //引　数   ：なし
        //戻り値   ：役職データ
        //機　能   ：役職データの取得
        ///////////////////////////////
        public List<T_ShipmentDsp> GetShipmentData()
        {
            List<T_ShipmentDsp> shipment = new List<T_ShipmentDsp>();
            try
            {
                var context = new SalesManagement_DevContext();
                // tbはIEnumerable型
                var tb = from t1 in context.T_Shipments

                         join t2 in context.T_ShipmentDetails
                         on t1.ShID equals t2.ShID

                         select new
                         {
                             t1.ShID,
                             t1.SoID,
                             t1.EmID,
                             t1.ClID,
                             t1.ShFinishDate,
                             t1.ShStateFlag,
                             t1.ShFlag,
                             t1.ShHidden,
                             t2.ShDetailID,
                             t2.PrID,
                             t2.ShDquantity,
                         };

                // IEnumerable型のデータをList型へ

                foreach (var p in tb)
                {
                    shipment.Add(new T_ShipmentDsp()
                    {
                        ShID = p.ShID,
                        SoID = p.SoID,
                        EmID = p.EmID,
                        ClID = p.ClID,
                        ShFinishDate = p.ShFinishDate,
                        ShStateFlag = p.ShStateFlag,
                        ShFlag = p.ShFlag,
                        ShHidden = p.ShHidden,
                        ShDetailID = p.ShDetailID,
                        PrID = p.PrID,
                        ShDquantity = p.ShDquantity,
                    });
                }
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return shipment;

        }

        ///////////////////////////////
        //メソッド名：GetShipmentData()　オーバーロード
        //引　数   ：検索条件
        //戻り値   ：条件一致部署データ
        //機　能   ：条件一致部署データの取得
        ///////////////////////////////
        public List<T_ShipmentDsp> GetShipmentData(T_ShipmentDsp selectCondition)
        {
            List<T_ShipmentDsp> Shipment = GetShipmentData();
            try
            {
                if (NonMaster.FormShipment.F_Shipment.mShID != "")
                {
                    var context = new SalesManagement_DevContext();
                    Shipment = Shipment.Where(x =>
                                                        x.ShID.ToString().Contains(selectCondition.ShID.ToString()) &&
                                                        x.ShStateFlag.ToString().Contains(selectCondition.ShStateFlag.ToString()) &&
                                                        x.ShFlag.ToString().Contains(selectCondition.ShFlag.ToString())).ToList();
                    context.Dispose();
                }
                else if (NonMaster.FormShipment.F_Shipment.mShDetailID != "")
                {
                    Shipment = Shipment.Where(x =>
                                                        x.ShDetailID.ToString().Contains(selectCondition.ShDetailID.ToString()) &&
                                                        x.ShStateFlag.ToString().Contains(selectCondition.ShStateFlag.ToString()) &&
                                                        x.ShFlag.ToString().Contains(selectCondition.ShFlag.ToString())).ToList();
                }
                else if (NonMaster.FormShipment.F_Shipment.mClID != "")
                {
                    Shipment = Shipment.Where(x =>
                                                        x.ClID.ToString().Contains(selectCondition.ClID.ToString()) &&
                                                        x.ShStateFlag.ToString().Contains(selectCondition.ShStateFlag.ToString()) &&
                                                        x.ShFlag.ToString().Contains(selectCondition.ShFlag.ToString())).ToList();
                }

                else if (NonMaster.FormShipment.F_Shipment.mEmID != "")
                {
                    Shipment = Shipment.Where(x =>
                                                        x.EmID.ToString().Contains(selectCondition.EmID.ToString()) &&
                                                        x.ShStateFlag.ToString().Contains(selectCondition.ShStateFlag.ToString()) &&
                                                        x.ShFlag.ToString().Contains(selectCondition.ShFlag.ToString())).ToList();
                }
                else if (NonMaster.FormShipment.F_Shipment.mSoID != "")
                {
                    Shipment = Shipment.Where(x =>
                                                        x.SoID.ToString().Contains(selectCondition.SoID.ToString()) &&
                                                        x.ShStateFlag.ToString().Contains(selectCondition.ShStateFlag.ToString()) &&
                                                        x.ShFlag.ToString().Contains(selectCondition.ShFlag.ToString())).ToList();
                }
                else if (NonMaster.FormShipment.F_Shipment.mOrID != "")
                {
                    Shipment = Shipment.Where(x =>
                                                        x.OrID.ToString().Contains(selectCondition.OrID.ToString()) &&
                                                        x.ShStateFlag.ToString().Contains(selectCondition.ShStateFlag.ToString()) &&
                                                        x.ShFlag.ToString().Contains(selectCondition.ShFlag.ToString())).ToList();
                }
                else if (NonMaster.FormShipment.F_Shipment.mPrID != "")
                {
                    Shipment = Shipment.Where(x =>
                                                        x.PrID.ToString().Contains(selectCondition.PrID.ToString()) &&
                                                        x.ShStateFlag.ToString().Contains(selectCondition.ShStateFlag.ToString()) &&
                                                        x.ShFlag.ToString().Contains(selectCondition.ShFlag.ToString())).ToList();
                }
                else if (NonMaster.FormShipment.F_Shipment.mShStateFlg == 1)
                {
                    Shipment = Shipment.Where(x => x.ShStateFlag.ToString().Contains(selectCondition.ShStateFlag.ToString())).ToList();
                }
                else if (NonMaster.FormShipment.F_Shipment.mShFlg == 2)
                {
                    Shipment = Shipment.Where(x => x.ShFlag.ToString().Contains(selectCondition.ShFlag.ToString())).ToList();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Shipment;

        }

        public List<T_Shipment> GetShipmentFlag(T_Shipment selectCondition)
        {
            List<T_Shipment> Shipment = new List<T_Shipment>();
            try
            {
                var context = new SalesManagement_DevContext();
                Shipment = context.T_Shipments.Where(x => x.ShFlag.ToString().Contains(selectCondition.ShFlag.ToString())).ToList();
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Shipment;

        }

        ///////////////////////////////
        //メソッド名：GetShipmentDspData()
        //引　数   ：なし
        //戻り値   ：表示用役職データ
        //機　能   ：表示用役職データの取得
        ///////////////////////////////
        public bool AddShipmentData(List<T_Shipment> regShipment)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                context.T_Shipments.AddRange(regShipment);
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
        public bool AddShipmentDetailData(List<T_ShipmentDetail> regShipmentDetail)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                context.T_ShipmentDetails.AddRange(regShipmentDetail);
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
        //メソッド名：UpdateShipmentData()
        //引　数   ：役職データ
        //戻り値   ：True or False
        //機　能   ：役職データの更新
        //          ：更新成功の場合True
        //          ：更新失敗の場合False
        ///////////////////////////////
        public bool UpdateShipmentData(T_Shipment updShipment)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                var Shipment = context.T_Shipments.Single(x => x.ShID == updShipment.ShID);
                Shipment.ClID = updShipment.ClID;
                Shipment.EmID = updShipment.EmID;
                Shipment.ClID = updShipment.ClID;
                Shipment.SoID = updShipment.SoID;
                Shipment.OrID = updShipment.OrID;
                Shipment.ShFinishDate = updShipment.ShFinishDate;
                Shipment.ShStateFlag = updShipment.ShStateFlag;
                Shipment.ShFlag = updShipment.ShFlag;
                Shipment.ShHidden = Shipment.ShHidden;

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

        public bool UpdateShipmentDetailData(T_ShipmentDetail updShipmentDetail)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                var ShipmentDetail = context.T_ShipmentDetails.Single(x => x.ShDetailID == updShipmentDetail.ShDetailID);
                ShipmentDetail.PrID = updShipmentDetail.PrID;
                ShipmentDetail.ShDquantity = updShipmentDetail.ShDquantity;

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
        //メソッド名：DeleteShipmentData()
        //引　数   ：役職データ
        //戻り値   ：True or False
        //機　能   ：役職データの削除
        //          ：削除成功の場合True
        //          ：削除失敗の場合False
        ///////////////////////////////
        public bool DeleteShipmentData(T_Shipment delShipment)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                var Shipment = context.T_Shipments.Single(x => x.ShID == delShipment.ShID);
                Shipment.ShFlag = delShipment.ShFlag;
                Shipment.ShHidden = delShipment.ShHidden;

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
        //メソッド名：GetShipmentData()　オーバーロード
        //引　数   ：検索条件
        //戻り値   ：条件一致受注データ
        //機　能   ：条件一致受注データの取得
        ///////////////////////////////
        public List<T_ShipmentDsp> SearchShipmentData(T_ShipmentDsp selectCondition)
        {
            List<T_ShipmentDsp> Shipment = GetShipmentData();
            try
            {
                if (NonMaster.FormShipment.F_Shipment.mShID != "")
                {
                    Shipment = Shipment.Where(x =>
                                                        x.ShID.ToString().Contains(selectCondition.ShID.ToString()) &&
                                                        x.ShStateFlag.ToString().Contains(selectCondition.ShStateFlag.ToString()) &&
                                                        x.ShFlag.ToString().Contains(selectCondition.ShFlag.ToString())).ToList();
                }
                else if (NonMaster.FormShipment.F_Shipment.mShDetailID != "")
                {
                    Shipment = Shipment.Where(x =>
                                                        x.ShDetailID.ToString().Contains(selectCondition.ShDetailID.ToString()) &&
                                                        x.ShStateFlag.ToString().Contains(selectCondition.ShStateFlag.ToString()) &&
                                                        x.ShFlag.ToString().Contains(selectCondition.ShFlag.ToString())).ToList();
                }
                else if (NonMaster.FormShipment.F_Shipment.mClID != "")
                {
                    Shipment = Shipment.Where(x =>
                                                        x.ClID.ToString().Contains(selectCondition.ClID.ToString()) &&
                                                        x.ShStateFlag.ToString().Contains(selectCondition.ShStateFlag.ToString()) &&
                                                        x.ShFlag.ToString().Contains(selectCondition.ShFlag.ToString())).ToList();
                }
                else if (NonMaster.FormShipment.F_Shipment.mSoID != "")
                {
                    Shipment = Shipment.Where(x =>
                                                        x.SoID.ToString().Contains(selectCondition.SoID.ToString()) &&
                                                        x.ShStateFlag.ToString().Contains(selectCondition.ShStateFlag.ToString()) &&
                                                        x.ShFlag.ToString().Contains(selectCondition.ShFlag.ToString())).ToList();
                }
                else if (NonMaster.FormShipment.F_Shipment.mOrID != "")
                {
                    Shipment = Shipment.Where(x =>
                                                        x.OrID.ToString().Contains(selectCondition.OrID.ToString()) &&
                                                        x.ShStateFlag.ToString().Contains(selectCondition.ShStateFlag.ToString()) &&
                                                        x.ShFlag.ToString().Contains(selectCondition.ShFlag.ToString())).ToList();
                }

                else if (NonMaster.FormShipment.F_Shipment.mShStateFlg == 1)
                {
                    Shipment = Shipment.Where(x => x.ShStateFlag.ToString().Contains(selectCondition.ShStateFlag.ToString())).ToList();
                }
                else if (NonMaster.FormShipment.F_Shipment.mShFlg == 2)
                {
                    Shipment = Shipment.Where(x => x.ShFlag.ToString().Contains(selectCondition.ShFlag.ToString())).ToList();
                }
                else if (NonMaster.FormShipment.F_Shipment.mPrID != "")
                {
                    Shipment = Shipment.Where(x =>
                                                        x.PrID.ToString().Contains(selectCondition.PrID.ToString()) &&
                                                        x.ShStateFlag.ToString().Contains(selectCondition.ShStateFlag.ToString()) &&
                                                        x.ShFlag.ToString().Contains(selectCondition.ShFlag.ToString())).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Shipment;
        }

    }
}
