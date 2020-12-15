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
    internal class PostnetBarcodeEncoder : BarcodeEncoder
    {
        private static readonly string[] POSTNET_Code =
            {   "11000",
                "00011",
                "00101",
                "00110",
                "01001",
                "01010",
                "01100",
                "10001",
                "10010",
                "10100" };

        public PostnetBarcodeEncoder(string input)
        {
            Raw_Data = input;
        }//Postnet

        /// <summary>
        /// Encode the raw data using the PostNet algorithm.
        /// </summary>
        private string Encode_Postnet()
        {
            base.ErrorMessage = null;
            //remove dashes if present
            Raw_Data = Raw_Data.Replace("-", "");

            switch (Raw_Data.Length)
            {
                case 5:
                case 6:
                case 9:
                case 11: break;
                default:
                    base.ErrorMessage = BarcodeStrings.PostnetError;
                    return null;
            }//switch

            //Note: 0 = half bar and 1 = full bar
            //initialize the result with the starting bar
            string result = "1";
            int checkdigitsum = 0;

            foreach (char c in Raw_Data)
            {
                try
                {
                    int index = "0123456789".IndexOf(c);
                    if (index < 0)
                    {
                        base.ErrorMessage = BarcodeStrings.PostnetError;
                        return null;
                    }
                    result += POSTNET_Code[index];
                    checkdigitsum += index;
                }//try
                catch (Exception ex)
                {
                    base.ErrorMessage = BarcodeStrings.PostnetError + "->" + ex.Message;
                    return null;
                }//catch
            }//foreach

            //calculate and add check digit
            int temp = checkdigitsum % 10;
            int checkdigit = 10 - (temp == 0 ? 10 : temp);

            result += POSTNET_Code[checkdigit];

            //ending bar
            result += "1";

            return result;
        }//Encode_PostNet

        public override string Encoded_Value
        {
            get { return Encode_Postnet(); }
        }

    }//class
}
