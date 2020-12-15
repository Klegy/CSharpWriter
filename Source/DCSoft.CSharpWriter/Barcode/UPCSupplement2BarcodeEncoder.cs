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
    internal class UPCSupplement2BarcodeEncoder : BarcodeEncoder
    {
        private static readonly string[] EAN_CodeA = { "0001101", "0011001", "0010011", "0111101", "0100011", "0110001", "0101111", "0111011", "0110111", "0001011" };
        private static readonly string[] EAN_CodeB = { "0100111", "0110011", "0011011", "0100001", "0011101", "0111001", "0000101", "0010001", "0001001", "0010111" };
        private static readonly string[] UPC_SUPP_2 = { "aa", "ab", "ba", "bb" };

        public UPCSupplement2BarcodeEncoder(string input)
        {
            Raw_Data = input;
        }

        /// <summary>
        /// Encode the raw data using the UPC Supplemental 2-digit algorithm.
        /// </summary>
        private string Encode_UPCSupplemental_2()
        {
            base.ErrorMessage = null;
            if (Raw_Data.Length != 2 || CheckNumericOnly(Raw_Data) == false)
            {
                base.ErrorMessage = BarcodeStrings.UPCS2InvaliData;
                return null;
            }

            string pattern = "";

            try
            {
                pattern = UPC_SUPP_2[Int32.Parse(Raw_Data.Trim()) % 4];
            }//try
            catch
            {
                base.ErrorMessage = BarcodeStrings.UPCS2InvaliData;
                return null;
            }

            string result = "1011";

            int pos = 0;
            foreach (char c in pattern)
            {
                if (c == 'a')
                {
                    //encode using odd parity
                    result += EAN_CodeA[Int32.Parse(Raw_Data[pos].ToString())];
                }//if
                else if (c == 'b')
                {
                    //encode using even parity
                    result += EAN_CodeB[Int32.Parse(Raw_Data[pos].ToString())];
                }//else if

                if (pos++ == 0) result += "01"; //Inter-character separator
            }//foreach
            return result;
        }//Encode_UPSSupplemental_2

        public override string Encoded_Value
        {
            get { return Encode_UPCSupplemental_2(); }
        }

    }//class
}
