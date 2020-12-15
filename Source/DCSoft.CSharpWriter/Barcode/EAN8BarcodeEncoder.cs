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
    internal class EAN8BarcodeEncoder : BarcodeEncoder//, IBarcode
    {
        private static readonly string[] EAN_CodeA = { "0001101", "0011001", "0010011", "0111101", "0100011", "0110001", "0101111", "0111011", "0110111", "0001011" };
        private static readonly string[] EAN_CodeC = { "1110010", "1100110", "1101100", "1000010", "1011100", "1001110", "1010000", "1000100", "1001000", "1110100" };

        public EAN8BarcodeEncoder(string input)
        {
            Raw_Data = input;
        }
        /// <summary>
        /// Encode the raw data using the EAN-8 algorithm.
        /// </summary>
        private string Encode_EAN8()
        {
            base.ErrorMessage = null;
            //check length
            if (Raw_Data.Length != 8 && Raw_Data.Length != 7)
            {
                base.ErrorMessage = BarcodeStrings.EAN8InvaliData;
                return null;
            }

            //check numeric only
            if (!CheckNumericOnly(Raw_Data))
            {
                base.ErrorMessage = BarcodeStrings.EAN8InvaliData;
                return null;
            }

            int checksum = 0;
            //calculate the checksum digit if necessary
            if (Raw_Data.Length == 7)
            {
                //calculate the checksum digit
                int even = 0;
                int odd = 0;

                //odd
                for (int i = 0; i <= 6; i += 2)
                {
                    odd += Int32.Parse(Raw_Data.Substring(i, 1)) * 3;
                }//for

                //even
                for (int i = 1; i <= 5; i += 2)
                {
                    even += Int32.Parse(Raw_Data.Substring(i, 1));
                }//for

                int total = even + odd;
                checksum = total % 10;
                checksum = 10 - checksum;
                if (checksum == 10)
                    checksum = 0;

                //add the checksum to the end of the 
                Raw_Data += checksum.ToString();
            }//if

            //encode the data
            string result = "101";

            //first half (Encoded using left hand / odd parity)
            for (int i = 0; i < Raw_Data.Length / 2; i++)
            {
                result += EAN_CodeA[Int32.Parse(Raw_Data[i].ToString())];
            }//for

            //center guard bars
            result += "01010";

            //second half (Encoded using right hand / even parity)
            for (int i = Raw_Data.Length / 2; i < Raw_Data.Length; i++)
            {
                result += EAN_CodeC[Int32.Parse(Raw_Data[i].ToString())];
            }//for

            result += "101";

            return result;
        }//Encode_EAN8


        public override string Encoded_Value
        {
            get { return Encode_EAN8(); }
        }

    }
}
