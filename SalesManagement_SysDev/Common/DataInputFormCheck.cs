using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace SalesManagement_SysDev

{
    class DataInputFormCheck
    {
        ///////////////////////////////
        //メソッド名：CheckFullWidth()
        //引　数   ：文字列
        //戻り値   ：True or False
        //機　能   ：全角文字列のチェック
        //          ：全角文字列の場合True
        //          ：全角文字列でない場合False
        ///////////////////////////////
        public bool CheckFullWidth(string text)
        {
            bool flg;

            int textLength = text.Replace("\r\n", string.Empty).Length;
            int textByte = Encoding.GetEncoding("Shift_JIS").GetByteCount(text.Replace("\r\n", string.Empty));
            if (textByte != textLength * 2)
                flg = false;
            else
                flg = true;

            return flg;
        }
        ///////////////////////////////
        //メソッド名：CheckHalfAlphabetNumeric()
        //引　数   ：文字列
        //戻り値   ：True or False
        //機　能   ：半角英数字文字列のチェック
        //          ：半角文字列の場合True
        //          ：半角文字列でない場合False
        ///////////////////////////////
        public bool CheckHalfAlphabetNumeric(string text)
        {
            bool flg;

            Regex regex = new Regex("^[a-zA-Z0-9]+$");
            if (!regex.IsMatch(text))
                flg = false;
            else
                flg = true;

            return flg;
        }

        ///////////////////////////////
        //メソッド名：CheckNumeric()
        //引　数   ：文字列
        //戻り値   ：True or False
        //機　能   ：数値のチェック
        //          ：数値の場合True
        //          ：数値でない場合False
        ///////////////////////////////
        public bool CheckNumeric(string text)
        {
            bool flg;

            Regex regex = new Regex("^[0-9]+$");
            if (!regex.IsMatch(text))
                flg = false;
            else
                flg = true;

            return flg;
        }

        ///////////////////////////////
        //メソッド名：CheckNumericWithHyphen()
        //引　数   ：文字列
        //戻り値   ：True or False
        //機　能   ：数値のチェック(ハイフンを使う電話番号用)
        //          ：数値の場合True
        //          ：数値でない場合False
        ///////////////////////////////
        public bool CheckNumericWithHyphen(string text)
        {
            bool flg;

            Regex regex = new Regex("^[0-9-]+$");
            if (!regex.IsMatch(text))
                flg = false;
            else
                flg = true;

            return flg;
        }
    }
}
