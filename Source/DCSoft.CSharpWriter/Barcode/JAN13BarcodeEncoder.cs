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
    internal class JAN13BarcodeEncoder : BarcodeEncoder
    {
        public JAN13BarcodeEncoder(string input)
        {
            Raw_Data = input;
        }
        /// <summary>
        /// Encode the raw data using the JAN-13 algorithm.
        /// </summary>
        private string Encode_JAN13()
        {
            base.ErrorMessage = null;
            if (!Raw_Data.StartsWith("49") || CheckNumericOnly(Raw_Data) == false)
            {
                base.ErrorMessage = BarcodeStrings.JAN13InvaliData;
                return null;
            }

            EAN13BarcodeEncoder ean13 = new EAN13BarcodeEncoder(Raw_Data);
            string result = ean13.Encoded_Value;
            base.ErrorMessage = ean13.ErrorMessage;
            return result;
        }//Encode_JAN13


        public override string Encoded_Value
        {
            get { return Encode_JAN13(); }
        }

    }

}
