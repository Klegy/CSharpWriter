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
    internal class Interleaved2of5BarcodeEncoder : BarcodeEncoder
    {
        private static readonly string[] I25_Code = { "NNWWN", "WNNNW", "NWNNW", "WWNNN", "NNWNW", "WNWNN", "NWWNN", "NNNWW", "WNNWN", "NWNWN" };

        public Interleaved2of5BarcodeEncoder(string input)
        {
            Raw_Data = input;
        }
        /// <summary>
        /// Encode the raw data using the Interleaved 2 of 5 algorithm.
        /// </summary>
        private string Encode_Interleaved2of5()
        {
            base.ErrorMessage = null;
            //check length of input
            if (Raw_Data.Length % 2 != 0)
            {
                base.ErrorMessage = BarcodeStrings.I25InvaliData;
                return null;
            }

            if (!CheckNumericOnly(Raw_Data))
            {
                base.ErrorMessage = BarcodeStrings.I25InvaliData;
                return null;
            }

            string result = "1010";

            for (int i = 0; i < Raw_Data.Length; i += 2)
            {
                bool bars = true;
                string patternbars = I25_Code[Int32.Parse(Raw_Data[i].ToString())];
                string patternspaces = I25_Code[Int32.Parse(Raw_Data[i + 1].ToString())];
                string patternmixed = "";

                //interleave
                while (patternbars.Length > 0)
                {
                    patternmixed += patternbars[0].ToString() + patternspaces[0].ToString();
                    patternbars = patternbars.Substring(1);
                    patternspaces = patternspaces.Substring(1);
                }//while

                foreach (char c1 in patternmixed)
                {
                    if (bars)
                    {
                        if (c1 == 'N')
                            result += "1";
                        else
                            result += "11";
                    }//if
                    else
                    {
                        if (c1 == 'N')
                            result += "0";
                        else
                            result += "00";
                    }//else
                    bars = !bars;
                }//foreach

            }//foreach

            //add ending bars
            result += "1101";
            return result;
        }//Encode_Interleaved2of5

        public override string Encoded_Value
        {
            get { return this.Encode_Interleaved2of5(); }
        }

    }
}
