using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagement_SysDev
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //スプラッシュウィンドウの設定
            F_start frm = new F_start();
            frm.ShowDialog();

            Application.Run(new F_Login());
            Application.Run(new F_menu());
        }
    }
}
