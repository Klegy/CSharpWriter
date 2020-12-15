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
    internal class UPCSupplement5BarcodeEncoder : BarcodeEncoder
    {
        private static readonly string[] EAN_CodeA = { "0001101", "0011001", "0010011", "0111101", "0100011", "0110001", "0101111", "0111011", "0110111", "0001011" };
        private static readonly string[] EAN_CodeB = { "0100111", "0110011", "0011011", "0100001", "0011101", "0111001", "0000101", "0010001", "0001001", "0010111" };
        private static readonly string[] UPC_SUPP_5 = { "bbaaa", "babaa", "baaba", "baaab", "abbaa", "aabba", "aaabb", "ababa", "abaab", "aabab" };

        public UPCSupplement5BarcodeEncoder(string input)
        {
            Raw_Data = input;
        }

        /// <summary>
        /// Encode the raw data using the UPC Supplemental 5-digit algorithm.
        /// </summary>
        private string Encode_UPCSupplemental_5()
        {
            base.ErrorMessage = null;
            if (Raw_Data.Length != 5 || CheckNumericOnly(Raw_Data) == false)
            {
                base.ErrorMessage = BarcodeStrings.UPCS5InvaliData;
                return null;
            }

            //calculate the checksum digit
            int even = 0;
            int odd = 0;

            //odd
            for (int i = 0; i <= 4; i += 2)
            {
                odd += Int32.Parse(Raw_Data.Substring(i, 1)) * 3;
            }//for

            //even
            for (int i = 1; i < 4; i += 2)
            {
                even += Int32.Parse(Raw_Data.Substring(i, 1)) * 9;
            }//for

            int total = even + odd;
            int cs = total % 10;

            string pattern = UPC_SUPP_5[cs];

            string result = "";

            int pos = 0;
            foreach (char c in pattern)
            {
                //Inter-character separator
                if (pos == 0) result += "1011";
                else result += "01";

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
                pos++;
            }//foreach
            return result;
        }//Encode_UPCSupplemental_5

        public override string Encoded_Value
        {
            get { return Encode_UPCSupplemental_5(); }
        }
    }//class
}
