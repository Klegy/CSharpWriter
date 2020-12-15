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
    /// <summary>
    /// 条码样式
    /// </summary>
    /// <remarks>编写 袁永福</remarks>
    public enum BarcodeStyle
    {
        /// <summary>
        /// 未指定样式
        /// </summary>
        UNSPECIFIED,
        /// <summary>
        /// UPCA条码
        /// </summary>
        UPCA,
        /// <summary>
        /// UPCE条码
        /// </summary>
        UPCE,
        /// <summary>
        /// UPC_SUPPLEMENTAL_2DIGIT,UPCS2条码
        /// </summary>
        SUPP2,
        /// <summary>
        /// UPC_SUPPLEMENTAL_5DIGIT,UPCS5条码
        /// </summary>
        SUPP5,
        /// <summary>
        /// EAN13条码
        /// </summary>
        EAN13,
        /// <summary>
        /// EAN8条码
        /// </summary>
        EAN8,
        /// <summary>
        /// I2of5条码
        /// </summary>
        Interleaved2of5,
        /// <summary>
        /// 25条码
        /// </summary>
        Standard2of5,
        /// <summary>
        /// Industrial2of5.工业25条码
        /// </summary>
        I2of5,
        /// <summary>
        /// 39条码
        /// </summary>
        Code39,
        /// <summary>
        /// 扩展型39条码
        /// </summary>
        Code39Extended,
        /// <summary>
        /// 93条码
        /// </summary>
        Code93,
        /// <summary>
        /// Cobadar条码
        /// </summary>
        Codabar,
        /// <summary>
        /// PostNet条码
        /// </summary>
        PostNet,
        /// <summary>
        /// BOOKLAND条码
        /// </summary>
        BOOKLAND,
        /// <summary>
        /// ISBN条码
        /// </summary>
        ISBN,
        /// <summary>
        /// JAN13条码
        /// </summary>
        JAN13,
        /// <summary>
        /// MSIMod10条码
        /// </summary>
        MSI_Mod10,
        /// <summary>
        /// MSI2Mod10条码
        /// </summary>
        MSI_2Mod10,
        /// <summary>
        /// MSI Mod11条码
        /// </summary>
        MSI_Mod11,
        /// <summary>
        /// MSI Mod11-10条码
        /// </summary>
        MSI_Mod11_Mod10,
        /// <summary>
        /// Modified Plessey条码
        /// </summary>
        Modified_Plessey,
        /// <summary>
        /// 11条码
        /// </summary>
        CODE11,
        /// <summary>
        /// USD8条码
        /// </summary>
        USD8,
        /// <summary>
        /// UCC12条码
        /// </summary>
        UCC12,
        /// <summary>
        /// UCC13条码
        /// </summary>
        UCC13,
        /// <summary>
        /// LOGMARS条码
        /// </summary>
        LOGMARS,
        ///////// <summary>
        ///////// CODE128条码
        ///////// </summary>
        //////Code128,
        /// <summary>
        /// 128A条码
        /// </summary>
        Code128A,
        /// <summary>
        /// 128B条码
        /// </summary>
        Code128B,
        /// <summary>
        /// 128C条码
        /// </summary>
        Code128C
    };
}
