using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SalesManagement_SysDev.Forms.DbAccess
{
    class ClientDataAccess
    {
        ///////////////////////////////
        //メソッド名：CheckClientCDExistence()
        //引　数   ：役職コード
        //戻り値   ：True or False
        //機　能   ：一致する役職コードの有無を確認
        //          ：一致データありの場合True
        //          ：一致データなしの場合False
        ///////////////////////////////
        public bool CheckClientCDExistence(string clientID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //役職IDで一致するデータが存在するか
                flg = context.M_Clients.Any(x => x.ClID.ToString() == clientID);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }


        ///////////////////////////////
        //メソッド名：GetClientData()
        //引　数   ：なし
        //戻り値   ：役職データ
        //機　能   ：役職データの取得
        ///////////////////////////////
        public List<M_Client> GetClientData()
        {
            List<M_Client> client = null;
            try
            {
                var context = new SalesManagement_DevContext();
                client = context.M_Clients.Where(x => x.ClFlag == 0).ToList();
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return client;

        }
        ///////////////////////////////
        //メソッド名：GetClientData()　オーバーロード
        //引　数   ：検索条件
        //戻り値   ：条件一致部署データ
        //機　能   ：条件一致部署データの取得
        ///////////////////////////////
        public List<M_Client> GetClientData(M_Client selectCondition)
        {
            List<M_Client> Client = new List<M_Client>();
            try
            {
                if (Master.FormClient.F_Client.mClID != "")
                {
                    var context = new SalesManagement_DevContext();
                    Client = context.M_Clients.Where(x =>
                                                        x.ClID.ToString().Contains(selectCondition.ClID.ToString()) &&
                                                        x.ClFlag.ToString().Contains(selectCondition.ClFlag.ToString())).ToList();
                    context.Dispose();
                }
                else if (Master.FormClient.F_Client.mSoID != "")
                {
                    var context = new SalesManagement_DevContext();
                    Client = context.M_Clients.Where(x =>
                                                        x.SoID.ToString().Contains(selectCondition.SoID.ToString()) &&
                                                        x.ClFlag.ToString().Contains(selectCondition.ClFlag.ToString())).ToList();
                    context.Dispose();
                }
                else if (Master.FormClient.F_Client.mClName != "")
                {
                    var context = new SalesManagement_DevContext();
                    Client = context.M_Clients.Where(x =>
                                                        x.ClName.Contains(selectCondition.ClName) &&
                                                        x.ClFlag.ToString().Contains(selectCondition.ClFlag.ToString())).ToList();
                    context.Dispose();
                }
                else if (Master.FormClient.F_Client.mClFlg == true)
                {
                    var context = new SalesManagement_DevContext();
                    Client = context.M_Clients.Where(x => x.ClFlag.ToString().Contains(selectCondition.ClFlag.ToString())).ToList();
                    context.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Client;

        }

        public List<M_Client> GetClientFlag(M_Client selectCondition)
        {
            List<M_Client> client = new List<M_Client>();
            try
            {
                var context = new SalesManagement_DevContext();
                client = context.M_Clients.Where(x => x.ClFlag.ToString().Contains(selectCondition.ClFlag.ToString())).ToList();
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return client;

        }

        ///////////////////////////////
        //メソッド名：GetClientDspData()
        //引　数   ：なし
        //戻り値   ：表示用役職データ
        //機　能   ：表示用役職データの取得
        ///////////////////////////////
        public List<M_Client> GetClientDspData()
        {
            List<M_Client> client = null;
            try
            {
                var context = new SalesManagement_DevContext();
                client = context.M_Clients.Where(x => x.ClFlag == 0).ToList();
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return client;

        }

        ///////////////////////////////
        //メソッド名：AddClientData()
        //引　数   ：役職データ
        //戻り値   ：True or False
        //機　能   ：役職データの登録
        //          ：登録成功の場合True
        //          ：登録失敗の場合False
        ///////////////////////////////
        public bool AddClientData(M_Client regClient)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                context.M_Clients.Add(regClient);
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
        //メソッド名：UpdateClientData()
        //引　数   ：役職データ
        //戻り値   ：True or False
        //機　能   ：役職データの更新
        //          ：更新成功の場合True
        //          ：更新失敗の場合False
        ///////////////////////////////
        public bool UpdateClientData(M_Client updClient)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                var client = context.M_Clients.Single(x => x.ClID == updClient.ClID);
                client.ClName = updClient.ClName;
                client.ClFlag = updClient.ClFlag;
                client.ClHidden = updClient.ClHidden;

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
        //メソッド名：DeleteClientData()
        //引　数   ：役職データ
        //戻り値   ：True or False
        //機　能   ：役職データの削除
        //          ：削除成功の場合True
        //          ：削除失敗の場合False
        ///////////////////////////////
        public bool DeleteClientData(M_Client delClient)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                var client = context.M_Clients.Single(x => x.ClID == delClient.ClID);
                client.ClName = delClient.ClName;
                client.ClFlag = delClient.ClFlag;
                client.ClHidden = delClient.ClHidden;

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
