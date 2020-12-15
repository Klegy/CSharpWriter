/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using System.Text;

namespace DCSoft.Barcode
{
    /// <summary>
    /// 条码编码对象基础类型
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    internal abstract class BarcodeEncoder
    {
        /// <summary>
        /// 原始数据
        /// </summary>
        public string Raw_Data = "";
        /// <summary>
        /// 编码错误消息
        /// </summary>
        public string ErrorMessage = null;
        public virtual string Encoded_Value
        {
            get
            {
                return null;
            }
        }//Encoded_Value

        protected bool CheckNumericOnly(string Data)
        {
            foreach (char c in Data)
            {
                if ("0123456789".IndexOf(c) < 0)
                    return false;
            }
            return true;
            //
            //            //This function takes a string of data and breaks it into parts and trys to do Int64.TryParse
            //            //This will verify that only numeric data is contained in the string passed in.  The complexity below
            //            //was done to ensure that the minimum number of interations and checks could be performed.
            //
            //            //9223372036854775808 is the largest number a 64bit number(signed) can hold so ... make sure its less than that by one place
            //            int STRING_LENGTHS = 18;
            //
            //            string temp = Data;
            //            string[] strings = new string[(Data.Length / STRING_LENGTHS) 
            //                + ((Data.Length % STRING_LENGTHS == 0) ? 0 : 1)];
            //
            //            int i = 0;
            //            while (i < strings.Length)
            //                if (temp.Length >= STRING_LENGTHS)
            //                {
            //                    strings[i++] = temp.Substring(0, STRING_LENGTHS);
            //                    temp = temp.Substring(STRING_LENGTHS);
            //                }//if
            //                else
            //                {
            //                    strings[i++] = temp.Substring(0);
            //                }
            //
            //            foreach (string s in strings)
            //            {
            //                long value = 0;
            //                if (!Int64.TryParse(s, out value))
            //                    return false;
            //            }//foreach
            //
            //            return true;
        }//CheckNumericOnly

    }//BarcodeVariables abstract class
}
