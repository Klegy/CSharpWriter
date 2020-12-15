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
    internal class UPCEBarcodeEncoder : BarcodeEncoder
    {
        private static readonly string[] EAN_CodeA = { "0001101", "0011001", "0010011", "0111101", "0100011", "0110001", "0101111", "0111011", "0110111", "0001011" };
        private static readonly string[] EAN_CodeB = { "0100111", "0110011", "0011011", "0100001", "0011101", "0111001", "0000101", "0010001", "0001001", "0010111" };
        private static readonly string[] EAN_CodeC = { "1110010", "1100110", "1101100", "1000010", "1011100", "1001110", "1010000", "1000100", "1001000", "1110100" };
        private static readonly string[] EAN_Pattern = { "aaaaaa", "aababb", "aabbab", "aabbba", "abaabb", "abbaab", "abbbaa", "ababab", "ababba", "abbaba" };
        private static readonly string[] UPCE_Code_0 = { "bbbaaa", "bbabaa", "bbaaba", "bbaaab", "babbaa", "baabba", "baaabb", "bababa", "babaab", "baabab" };
        private static readonly string[] UPCE_Code_1 = { "aaabbb", "aababb", "aabbab", "aabbba", "abaabb", "abbaab", "abbbaa", "ababab", "ababba", "abbaba" };

        public UPCEBarcodeEncoder(string input)
        {
            Raw_Data = input;
        }//UPCE
        /// <summary>
        /// Encode the raw data using the UPC-E algorithm.
        /// </summary>
        private string Encode_UPCE()
        {
            base.ErrorMessage = null;
            if (Raw_Data.Length != 8 && Raw_Data.Length != 12)
            {
                base.ErrorMessage = BarcodeStrings.UPCEInvaliData;
                return null;
            }

            if (!CheckNumericOnly(Raw_Data))
            {
                base.ErrorMessage = BarcodeStrings.UPCEInvaliData;
                return null;
            }

            int CheckDigit = Int32.Parse(Raw_Data[Raw_Data.Length - 1].ToString());
            int NumberSystem = Int32.Parse(Raw_Data[0].ToString());

            //Convert to UPC-E from UPC-A if necessary
            if (Raw_Data.Length == 12)
            {
                string UPCECode = "";

                //break apart into components
                string Manufacturer = Raw_Data.Substring(1, 5);
                string ProductCode = Raw_Data.Substring(6, 5);

                //check for a valid number system
                if (NumberSystem != 0 && NumberSystem != 1)
                {
                    base.ErrorMessage = BarcodeStrings.UPCEInvaliData;
                    return null;
                }

                if (Manufacturer.EndsWith("000") || Manufacturer.EndsWith("100") || Manufacturer.EndsWith("200") && Int32.Parse(ProductCode) <= 999)
                {
                    //rule 1
                    UPCECode += Manufacturer.Substring(0, 2); //first two of manufacturer
                    UPCECode += ProductCode.Substring(2, 3); //last three of product
                    UPCECode += Manufacturer[2].ToString(); //third of manufacturer
                }//if
                else if (Manufacturer.EndsWith("00") && Int32.Parse(ProductCode) <= 99)
                {
                    //rule 2
                    UPCECode += Manufacturer.Substring(0, 3); //first three of manufacturer
                    UPCECode += ProductCode.Substring(3, 2); //last two of product
                    UPCECode += "3"; //number 3
                }//else if
                else if (Manufacturer.EndsWith("0") && Int32.Parse(ProductCode) <= 9)
                {
                    //rule 3
                    UPCECode += Manufacturer.Substring(0, 4); //first four of manufacturer
                    UPCECode += ProductCode[4]; //last digit of product
                    UPCECode += "4"; //number 4
                }//else if
                else if (!Manufacturer.EndsWith("0") && Int32.Parse(ProductCode) <= 9 && Int32.Parse(ProductCode) >= 5)
                {
                    //rule 4
                    UPCECode += Manufacturer; //manufacturer
                    UPCECode += ProductCode[4]; //last digit of product
                }//else if
                else
                {
                    base.ErrorMessage = BarcodeStrings.UPCEInvaliData;
                    return null;
                    //throw new Exception("EUPCE-4: Illegal UPC-A entered for conversion.  Unable to convert.");
                }
                Raw_Data = UPCECode;
            }//if

            //get encoding pattern 
            string pattern = "";

            if (NumberSystem == 0) pattern = UPCE_Code_0[CheckDigit];
            else pattern = UPCE_Code_1[CheckDigit];

            //encode the data
            string result = "101";

            int pos = 0;
            foreach (char c in pattern)
            {
                int i = Int32.Parse(Raw_Data[pos++].ToString());
                if (c == 'a')
                {
                    result += EAN_CodeA[i];
                }//if
                else if (c == 'b')
                {
                    result += EAN_CodeB[i];
                }//else if
            }//foreach

            //guard bars
            result += "01010";

            //end bars
            result += "1";

            return result;
        }//Encode_UPCE

        public override string Encoded_Value
        {
            get { return Encode_UPCE(); }
        }
    }
}
