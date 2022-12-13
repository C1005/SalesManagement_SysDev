using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagement_SysDev
{
    class MessageDsp
    {
        ///////////////////////////////
        //メソッド名：DspMsg()
        //引　数   ：string型：msgCD（メッセージ番号）
        //戻り値   ：メッセージのボタン値
        //機　能   ：メッセージを取得して表示
        ///////////////////////////////

        public DialogResult DspMsg(string msgCD)
        {
            DialogResult result = DialogResult.None;
            try
            {
                var context = new SalesManagement_DevContext();
                var message = context.M_Messages.Single(x => x.MsgCD == msgCD);
                MessageBoxButtons btn = new MessageBoxButtons();
                MessageBoxIcon icon = new MessageBoxIcon();
                string msgcom = message.MsgComments.Replace("\\r", "\r").Replace("\\n", "\n");
                string msgtitle = message.MsgTitle + "　（メッセージ番号：" + msgCD + "）";
                btn = (MessageBoxButtons)message.MsgButton;
                icon = (MessageBoxIcon)message.MsgICon;
                result = MessageBox.Show(msgcom, msgtitle, btn, icon);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }
    }
}
