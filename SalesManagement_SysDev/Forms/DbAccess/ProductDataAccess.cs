using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagement_SysDev.Forms.DbAccess
{
    class ProductDataAccess
    {
        ///////////////////////////////
        //メソッド名：CheckProductCDExistence()
        //引　数   ：メーカコード
        //戻り値   ：True or False
        //機　能   ：一致するメーカコードの有無を確認
        //          ：一致データありの場合True
        //          ：一致データなしの場合False
        ///////////////////////////////
        public bool CheckProductCDExistence(string ProductID)
        {
            bool flg = false;
            try
            {
                var context = new SalesManagement_DevContext();
                //メーカIDで一致するデータが存在するか
                flg = context.M_Products.Any(x => x.PrID.ToString() == ProductID);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }


        ///////////////////////////////
        //メソッド名：GetProductData()
        //引　数   ：なし
        //戻り値   ：メーカデータ
        //機　能   ：メーカデータの取得
        ///////////////////////////////
        public List<M_Product> GetProductData()
        {
            List<M_Product> Product = null;
            try
            {
                var context = new SalesManagement_DevContext();
                Product = context.M_Products.Where(x => x.PrFlag == 0).ToList();
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Product;

        }

        ///////////////////////////////
        //メソッド名：GetProductData()　オーバーロード
        //引　数   ：検索条件
        //戻り値   ：条件一致部署データ
        //機　能   ：条件一致部署データの取得
        ///////////////////////////////
        public List<M_Product> GetProductData(M_Product selectCondition)
        {
            List<M_Product> Product = new List<M_Product>();
            try
            {
                if (Master.FormProduct.F_Product.mPrID != "")
                {
                    var context = new SalesManagement_DevContext();
                    Product = context.M_Products.Where(x =>
                                                        x.PrID.ToString().Contains(selectCondition.PrID.ToString()) &&
                                                        x.PrFlag.ToString().Contains(selectCondition.PrFlag.ToString())).ToList();
                    context.Dispose();
                }
                else if (Master.FormProduct.F_Product.mMaID != "")
                {
                    var context = new SalesManagement_DevContext();
                    Product = context.M_Products.Where(x =>
                                                        x.MaID.ToString().Contains(selectCondition.MaID.ToString()) &&
                                                        x.PrFlag.ToString().Contains(selectCondition.PrFlag.ToString())).ToList();
                    context.Dispose();
                }
                else if (Master.FormProduct.F_Product.mPrName != "")
                {
                    var context = new SalesManagement_DevContext();
                    Product = context.M_Products.Where(x =>
                                                        x.PrName.Contains(selectCondition.PrName) &&
                                                        x.PrFlag.ToString().Contains(selectCondition.PrFlag.ToString())).ToList();
                    context.Dispose();
                }
                else if (Master.FormProduct.F_Product.mScID != "")
                {
                    var context = new SalesManagement_DevContext();
                    Product = context.M_Products.Where(x =>
                                                        x.ScID.ToString().Contains(selectCondition.ScID.ToString()) &&
                                                        x.PrFlag.ToString().Contains(selectCondition.PrFlag.ToString())).ToList();
                    context.Dispose();
                }
                else if (Master.FormProduct.F_Product.mPrFlg == true)
                {
                    var context = new SalesManagement_DevContext();
                    Product = context.M_Products.Where(x => x.PrFlag.ToString().Contains(selectCondition.PrFlag.ToString())).ToList();
                    context.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Product;

        }

        public List<M_Product> GetProductFlag(M_Product selectCondition)
        {
            List<M_Product> Product = new List<M_Product>();
            try
            {
                var context = new SalesManagement_DevContext();
                Product = context.M_Products.Where(x => x.PrFlag.ToString().Contains(selectCondition.PrFlag.ToString())).ToList();
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Product;

        }

        ///////////////////////////////
        //メソッド名：GetProductDspData()
        //引　数   ：なし
        //戻り値   ：表示用メーカデータ
        //機　能   ：表示用メーカデータの取得
        ///////////////////////////////
        public List<M_Product> GetProductDspData()
        {
            List<M_Product> Product = null;
            try
            {
                var context = new SalesManagement_DevContext();
                Product = context.M_Products.Where(x => x.PrFlag == 0).ToList();
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Product;

        }

        ///////////////////////////////
        //メソッド名：AddProductData()
        //引　数   ：メーカデータ
        //戻り値   ：True or False
        //機　能   ：メーカデータの登録
        //          ：登録成功の場合True
        //          ：登録失敗の場合False
        ///////////////////////////////
        public bool AddProductData(M_Product regProduct)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                context.M_Products.Add(regProduct);
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
        //メソッド名：UpdateProductData()
        //引　数   ：メーカデータ
        //戻り値   ：True or False
        //機　能   ：メーカデータの更新
        //          ：更新成功の場合True
        //          ：更新失敗の場合False
        ///////////////////////////////
        public bool UpdateProductData(M_Product updProduct)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                var Product = context.M_Products.Single(x => x.PrID == updProduct.PrID);
                Product.MaID = updProduct.MaID;
                Product.PrName = updProduct.PrName;
                Product.ScID = updProduct.ScID;
                Product.PrColor = updProduct.PrColor;
                Product.PrFlag = updProduct.PrFlag;
                Product.PrHidden = updProduct.PrHidden;
                Product.PrModelNumber = updProduct.PrModelNumber;
                Product.PrReleaseDate = updProduct.PrReleaseDate;
                Product.PrSafetyStock = updProduct.PrSafetyStock;
                Product.Price = updProduct.Price;

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
        //メソッド名：DeleteProductData()
        //引　数   ：メーカデータ
        //戻り値   ：True or False
        //機　能   ：メーカデータの削除
        //          ：削除成功の場合True
        //          ：削除失敗の場合False
        ///////////////////////////////
        public bool DeleteProductData(M_Product delProduct)
        {
            try
            {
                var context = new SalesManagement_DevContext();
                var Product = context.M_Products.Single(x => x.MaID == delProduct.MaID);
                Product.MaID = delProduct.MaID;
                Product.PrName = delProduct.PrName;
                Product.ScID = delProduct.ScID;
                Product.PrColor = delProduct.PrColor;
                Product.PrFlag = delProduct.PrFlag;
                Product.PrHidden = delProduct.PrHidden;
                Product.PrModelNumber = delProduct.PrModelNumber;
                Product.PrReleaseDate = delProduct.PrReleaseDate;
                Product.PrSafetyStock = delProduct.PrSafetyStock;
                Product.Price = delProduct.Price;

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
