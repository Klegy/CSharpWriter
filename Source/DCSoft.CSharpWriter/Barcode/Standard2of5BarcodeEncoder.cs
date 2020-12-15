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
    internal class Standard2of5BarcodeEncoder : BarcodeEncoder
    {
        private static readonly string[] S25_Code = { "11101010101110", "10111010101110", "11101110101010", "10101110101110", "11101011101010", "10111011101010", "10101011101110", "10101110111010", "11101010111010", "10111010111010" };

        public Standard2of5BarcodeEncoder(string input)
        {
            Raw_Data = input;
        }//Standard2of5
        /// <summary>
        /// Encode the raw data using the Standard 2 of 5 algorithm.
        /// </summary>
        private string Encode_Standard2of5()
        {
            base.ErrorMessage = null;
            if (!CheckNumericOnly(Raw_Data))
            {
                base.ErrorMessage = BarcodeStrings.S25InvaliData;
                return null;
            }

            string result = "11011010";

            foreach (char c in Raw_Data)
            {
                result += S25_Code[Int32.Parse(c.ToString())];
            }//foreach

            //add ending bars
            result += "1101011";
            return result;
        }//Encode_Standard2of5

        public override string Encoded_Value
        {
            get { return Encode_Standard2of5(); }
        }

    }
}
