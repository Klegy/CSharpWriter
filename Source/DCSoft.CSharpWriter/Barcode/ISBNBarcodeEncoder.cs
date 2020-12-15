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
    internal class ISBNBarcodeEncoder : BarcodeEncoder//, IBarcode
    {
        public ISBNBarcodeEncoder(string input)
        {
            Raw_Data = input;
        }
        /// <summary>
        /// Encode the raw data using the Bookland/ISBN algorithm.
        /// </summary>
        private string Encode_ISBN_Bookland()
        {
            base.ErrorMessage = null;
            if (!CheckNumericOnly(Raw_Data))
            {
                base.ErrorMessage = BarcodeStrings.ISBNInvaliData;
                return null;
            }

            //string type = "UNKNOWN";
            if (Raw_Data.Length == 10 || Raw_Data.Length == 9)
            {
                if (Raw_Data.Length == 10) Raw_Data = Raw_Data.Remove(9, 1);
                Raw_Data = "978" + Raw_Data;
                //type = "ISBN";
            }//if
            else if (Raw_Data.Length == 12 && Raw_Data.StartsWith("978"))
            {
                //type = "BOOKLAND-NOCHECKDIGIT";
            }//else if
            else if (Raw_Data.Length == 13 && Raw_Data.StartsWith("978"))
            {
                //type = "BOOKLAND-CHECKDIGIT";
                Raw_Data = Raw_Data.Remove(12, 1);
            }//else if
            else
            {
                // 未知类型
                base.ErrorMessage = BarcodeStrings.ISBNInvaliData;
                return null;
            }

            EAN13BarcodeEncoder ean13 = new EAN13BarcodeEncoder(Raw_Data);
            string result = ean13.Encoded_Value;
            base.ErrorMessage = ean13.ErrorMessage;
            return result;

        }//Encode_ISBN_Bookland


        public override string Encoded_Value
        {
            get { return Encode_ISBN_Bookland(); }
        }

    }
}
