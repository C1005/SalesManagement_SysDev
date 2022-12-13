using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;

namespace SalesManagement_SysDev.Common
{
    ///////////////////////////////
    //メソッド名：ImportData()
    //引　数   ：string型：strTbl（インポートテーブル名）
    //戻り値   ：なし
    //機　能   ：引数で指定されたテーブルへデータインポート
    ///////////////////////////////
    class ImportData
    {
        public void DataImport(string strTbl)
        {
            try
            {
                //インポートするCSVファイルの指定
                string csvpth = System.Environment.CurrentDirectory + "\\" + "csv" + "\\" + strTbl + ".csv";
                //データテーブルの設定
                DataTable dt = new DataTable();
                dt.TableName = strTbl;

                //csvファイルの内容をDataTableへ
                //csvファイル及び、デリミタの指定
                var parser = new Microsoft.VisualBasic.FileIO.TextFieldParser(csvpth, Encoding.GetEncoding("Shift-JIS"))
                {
                    TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited,
                    Delimiters = new string[] { "," }
                };
                // 全行読み込み
                var rows = new List<string[]>();
                while (!parser.EndOfData)
                {
                    rows.Add(parser.ReadFields());
                }
                // 列設定
                dt.Columns.AddRange(rows.First().Select(s => new DataColumn(s)).ToArray());
                // 行追加
                foreach (var row in rows.Skip(1))
                {
                    dt.Rows.Add(row);
                }

                //DB接続情報の取得(App.configに接続先指定)
                var dbpth = ConfigurationManager.ConnectionStrings["SalesManagement_DevContext"].ConnectionString;
                //DataTableの内容をDBへ追加
                using (var bulkCopy = new SqlBulkCopy(dbpth))
                {
                    bulkCopy.DestinationTableName = dt.TableName;
                    bulkCopy.WriteToServer(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
