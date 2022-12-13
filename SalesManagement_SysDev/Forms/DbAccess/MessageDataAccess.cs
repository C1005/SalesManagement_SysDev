using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Runtime.InteropServices;

namespace SalesManagement_SysDev.Forms.DbAccess
{
    class MessageDataAccess
    {
        internal static void btn_CleateDabase_Messages()
        {
            M_Message TplMessages = new M_Message()
            {
                MsgCD = "M0001",
                MsgComments = "テスト1",
                MsgTitle = "テスト2",
                MsgButton = 1,
                MsgICon = 32
            };
            SalesManagement_DevContext context = new SalesManagement_DevContext();
            context.M_Messages.Add(TplMessages);
            context.SaveChanges();

            TplMessages = new M_Message()
            {
                MsgCD = "M0002",
                MsgComments = "テスト2",
                MsgTitle = "テスト3",
                MsgButton = 1,
                MsgICon = 32
            };
            context = new SalesManagement_DevContext();
            context.M_Messages.Add(TplMessages);
            context.SaveChanges();

            context.Dispose();
        }
    }
}
