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
using System.Collections;

namespace DCSoft.Barcode
{
    internal class CodabarBarcodeEncoder : BarcodeEncoder
    {
        public CodabarBarcodeEncoder(string input)
        {
            Raw_Data = input;
        }//Codabar

        /// <summary>
        /// Encode the raw data using the Codabar algorithm.
        /// </summary>
        private string Encode_Codabar()
        {
            base.ErrorMessage = null;
            if (Raw_Data.Length < 2)
            {
                base.ErrorMessage = BarcodeStrings.CodabarError;
                return null;
            }

            //check first char to make sure its a start/stop char
            switch (Raw_Data[0].ToString().ToUpper().Trim())
            {
                case "A": break;
                case "B": break;
                case "C": break;
                case "D": break;
                default:
                    base.ErrorMessage = BarcodeStrings.CodabarError;
                    return null;
            }//switch

            //check the ending char to make sure its a start/stop char
            switch (Raw_Data[Raw_Data.Trim().Length - 1].ToString().ToUpper().Trim())
            {
                case "A": break;
                case "B": break;
                case "C": break;
                case "D": break;
                default:
                    base.ErrorMessage = BarcodeStrings.CodabarError;
                    return null;
            }//switch

            System.Text.StringBuilder result = new StringBuilder();

            //populate the hashtable to begin the process
            this.init_Codabar();

            foreach (char c in Raw_Data)
            {
                result.Append(Codabar_Code[c].ToString());
                result.Append("0"); //inter-character space
            }//foreach

            //remove the extra 0 at the end of the result
            result.Remove(result.Length - 1, 1);
            //result = result.Remove(result.Length - 1);

            return result.ToString();
        }//Encode_Codabar

        private static System.Collections.Hashtable Codabar_Code = null; //is initialized by init_Codabar()

        private void init_Codabar()
        {
            if (Codabar_Code != null)
                return;
            Codabar_Code = new Hashtable();
            Codabar_Code.Add('0', "101010011");//"101001101101");
            Codabar_Code.Add('1', "101011001");//"110100101011");
            Codabar_Code.Add('2', "101001011");//"101100101011");
            Codabar_Code.Add('3', "110010101");//"110110010101");
            Codabar_Code.Add('4', "101101001");//"101001101011");
            Codabar_Code.Add('5', "110101001");//"110100110101");
            Codabar_Code.Add('6', "100101011");//"101100110101");
            Codabar_Code.Add('7', "100101101");//"101001011011");
            Codabar_Code.Add('8', "100110101");//"110100101101");
            Codabar_Code.Add('9', "110100101");//"101100101101");
            Codabar_Code.Add('-', "101001101");//"110101001011");
            Codabar_Code.Add('$', "101100101");//"101101001011");
            Codabar_Code.Add(':', "1101011011");//"110110100101");
            Codabar_Code.Add('/', "1101101011");//"101011001011");
            Codabar_Code.Add('.', "1101101101");//"110101100101");
            Codabar_Code.Add('+', "101100110011");//"101101100101");
            Codabar_Code.Add('A', "1011001001");//"110110100101");
            Codabar_Code.Add('B', "1010010011");//"101011001011");
            Codabar_Code.Add('C', "1001001011");//"110101100101");
            Codabar_Code.Add('D', "1010011001");//"101101100101");
            Codabar_Code.Add('a', "1011001001");//"110110100101");
            Codabar_Code.Add('b', "1010010011");//"101011001011");
            Codabar_Code.Add('c', "1001001011");//"110101100101");
            Codabar_Code.Add('d', "1010011001");//"101101100101");
        }//init_Codeabar


        public override string Encoded_Value
        {
            get
            {
                return Encode_Codabar();
            }
        }
    }//class
}
