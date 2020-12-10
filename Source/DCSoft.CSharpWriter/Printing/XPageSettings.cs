/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using DCSoft.Drawing;
using System.Drawing;
using System.Drawing.Printing;

namespace DCSoft.Printing
{
    /// <summary>
    /// 页面设置对象
    /// </summary>
    [Serializable()]
    [System.ComponentModel.TypeConverter( typeof( XPageSettingsTypeConverter ))]
    [System.ComponentModel.Editor( typeof( XPageSettingEditor ) , typeof( UITypeEditor ))]
    public class XPageSettings : ICloneable
    {
        private static System.Collections.Hashtable myStandardPaperSize = null;
        /// <summary>
        /// 静态构造函数
        /// </summary>
        static XPageSettings()
        {
            myStandardPaperSize = new System.Collections.Hashtable();
            // 定义标准页面大小
            myStandardPaperSize[System.Drawing.Printing.PaperKind.A2] = new Size(1654, 2339); 	//A2 纸（420 毫米 × 594 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.A3] = new Size(1169, 1654); 	//A3 纸（297 毫米 × 420 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.A3Extra] = new Size(1268, 1752); 	//A3 extra 纸（322 毫米 × 445 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.A3ExtraTransverse] = new Size(1268, 1752); 	//A3 extra transverse 纸（322 毫米 × 445 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.A3Rotated] = new Size(1654, 1169); 	//A3 rotated 纸（420 毫米 × 297 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.A3Transverse] = new Size(1169, 1654); 	//A3 transverse 纸（297 毫米 × 420 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.A4] = new Size(827, 1169); 	//A4 纸（210 毫米 × 297 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.A4Extra] = new Size(929, 1268); 	//A4 extra 纸（236 毫米 × 322 毫米）。该值是针对 PostScript 驱动程序的，仅供 Linotronic 打印机使用以节省纸张。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.A4Plus] = new Size(827, 1299); 	//A4 plus 纸（210 毫米 × 330 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.A4Rotated] = new Size(1169, 827); 	//A4 rotated 纸（297 毫米 × 210 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.A4Small] = new Size(827, 1169); 	//A4 small 纸（210 毫米 × 297 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.A4Transverse] = new Size(827, 1169); 	//A4 transverse 纸（210 毫米 × 297 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.A5] = new Size(583, 827); 	//A5 纸（148 毫米 × 210 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.A5Extra] = new Size(685, 925); 	//A5 extra 纸（174 毫米 × 235 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.A5Rotated] = new Size(827, 583); 	//A5 rotated 纸（210 毫米 × 148 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.A5Transverse] = new Size(583, 827); 	//A5 transverse 纸（148 毫米 × 210 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.A6] = new Size(413, 583); 	//A6 纸（105 毫米 × 148 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.A6Rotated] = new Size(583, 413); 	//A6 rotated 纸（148 毫米 × 105 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.APlus] = new Size(894, 1402); 	//SuperA/SuperA/A4 纸（227 毫米 × 356 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.B4] = new Size(984, 1390); 	//B4 纸（250 × 353 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.B4Envelope] = new Size(984, 1390); 	//B4 信封（250 × 353 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.B5] = new Size(693, 984); 	//B5 纸（176 毫米 × 250 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.B5Envelope] = new Size(693, 984); 	//B5 信封（176 毫米 × 250 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.B5Extra] = new Size(791, 1087); 	//ISO B5 extra 纸（201 毫米 × 276 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.B5JisRotated] = new Size(1012, 717); 	//JIS B5 rotated 纸（257 毫米 × 182 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.B5Transverse] = new Size(717, 1012); 	//JIS B5 transverse 纸（182 毫米 × 257 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.B6Envelope] = new Size(693, 492); 	//B6 信封（176 毫米 × 125 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.B6Jis] = new Size(504, 717); 	//JIS B6 纸（128 毫米 × 182 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.BPlus] = new Size(1201, 1917); 	//SuperB/SuperB/A3 纸（305 毫米 × 487 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.C3Envelope] = new Size(1201, 1917); 	//SuperB/SuperB/A3 纸（305 毫米 × 487 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.C4Envelope] = new Size(902, 1276); 	//C4 信封（229 毫米 × 324 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.C5Envelope] = new Size(638, 902); 	//C5 信封（162 毫米 × 229 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.C65Envelope] = new Size(449, 902); 	//C65 信封（114 毫米 × 229 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.C6Envelope] = new Size(449, 638); 	//C6 信封（114 毫米 × 162 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.CSheet] = new Size(449, 638); 	//C6 信封（114 毫米 × 162 毫米）。 
            //////////myStandardPaperSize[System.Drawing.Printing.PaperKind.Custom] = new Size(0, 0); // 自定义大小
            myStandardPaperSize[System.Drawing.Printing.PaperKind.DLEnvelope] = new Size(433, 866); 	//DL 信封（110 毫米 × 220 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.DSheet] = new Size(2201, 3402); 	//D 纸（559 毫米 × 864 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.ESheet] = new Size(3402, 4402); 	//E 纸（864 毫米 × 1118 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.Executive] = new Size(724, 1051); 	//Executive 纸（184 毫米 × 267 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.Folio] = new Size(850, 1299); 	//Folio 纸（216 毫米 × 330 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.GermanLegalFanfold] = new Size(850, 1299); 	//German legal fanfold（216 毫米 × 330 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.GermanStandardFanfold] = new Size(850, 1201); 	//German standard fanfold（216 毫米 × 305 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.InviteEnvelope] = new Size(866, 866); 	//Invite envelope（220 毫米 × 220 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.IsoB4] = new Size(984, 1390); 	//ISO B4（250 毫米 × 353 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.ItalyEnvelope] = new Size(433, 906); 	//Italy envelope（110 毫米 × 230 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.JapaneseDoublePostcard] = new Size(787, 583); 	//Japanese double postcard（200 毫米 × 148 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.JapaneseDoublePostcardRotated] = new Size(583, 787); 	//Japanese rotated double postcard（148 毫米 × 200 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.JapanesePostcard] = new Size(394, 583); 	//Japanese postcard（100 毫米 × 148 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.JapanesePostcardRotated] = new Size(583, 394); 	//Japanese rotated postcard（148 毫米 × 100 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.Ledger] = new Size(1701, 1098); 	//Ledger 纸（432 × 279 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.Legal] = new Size(850, 1402); 	//Legal 纸（216 × 356 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.LegalExtra] = new Size(929, 1500); 	//Legal extra 纸（236 毫米 × 381 毫米）。该值特定于 PostScript 驱动程序，仅供 Linotronic 打印机使用以节省纸张。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.Letter] = new Size(850, 1098); 	//Letter 纸（216 毫米 × 279 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.LetterExtra] = new Size(929, 1197); 	//Letter extra 纸（236 毫米 × 304 毫米）。该值特定于 PostScript 驱动程序，仅供 Linotronic 打印机使用以节省纸张。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.LetterExtraTransverse] = new Size(929, 1201); 	//Letter extra transverse 纸（236 毫米 × 305 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.LetterPlus] = new Size(850, 1268); 	//Letter plus 纸（216 毫米 毫米 × 322 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.LetterRotated] = new Size(1098, 850); 	//Letter rotated 纸（279 毫米 × 216 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.LetterSmall] = new Size(850, 1098); 	//Letter small 纸（216 × 279 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.LetterTransverse] = new Size(827, 1098); 	//Letter transverse 纸（210 毫米 × 279 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.MonarchEnvelope] = new Size(386, 752); 	//Monarch envelope（98 毫米 × 191 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.Note] = new Size(850, 1098); 	//Note 纸（216 × 279 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.Number10Envelope] = new Size(413, 949); 	//#10 envelope（105 × 241 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.PersonalEnvelope] = new Size(362, 650); 	//6 3/4 envelope（92 毫米 × 165 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.Prc16K] = new Size(575, 846); 	//PRC 16K 纸（146 × 215 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.Prc16KRotated] = new Size(575, 846); 	//PRC 16K rotated 纸（146 × 215 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.Prc32K] = new Size(382, 594); 	//PRC 32K 纸（97 × 151 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.Prc32KBig] = new Size(382, 594); 	//PRC 32K(Big) 纸（97 × 151 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.Prc32KBigRotated] = new Size(382, 594); 	//PRC 32K rotated 纸（97 × 151 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.Prc32KRotated] = new Size(382, 594); 	//PRC 32K rotated 纸（97 × 151 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.PrcEnvelopeNumber1] = new Size(402, 650); 	//PRC #1 envelope（102 × 165 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.PrcEnvelopeNumber10] = new Size(1276, 1803); 	//PRC #10 envelope（324 × 458 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.PrcEnvelopeNumber10Rotated] = new Size(1803, 1276); 	//PRC #10 rotated envelope（458 × 324 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.PrcEnvelopeNumber1Rotated] = new Size(650, 402); 	//PRC #1 rotated envelope（165 × 102 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.PrcEnvelopeNumber2] = new Size(402, 693); 	//PRC #2 envelope（102 × 176 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.PrcEnvelopeNumber2Rotated] = new Size(693, 402); 	//PRC #2 rotated envelope（176 × 102 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.PrcEnvelopeNumber3] = new Size(492, 693); 	//PRC #3 envelope（125 × 176 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.PrcEnvelopeNumber3Rotated] = new Size(693, 492); 	//PRC #3 rotated envelope（176 × 125 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.PrcEnvelopeNumber4] = new Size(433, 819); 	//PRC #4 envelope（110 × 208 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.PrcEnvelopeNumber4Rotated] = new Size(819, 433); 	//PRC #4 rotated envelope（208 × 110 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.PrcEnvelopeNumber5] = new Size(433, 866); 	//PRC #5 envelope（110 × 220 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.PrcEnvelopeNumber5Rotated] = new Size(866, 433); 	//PRC #5 rotated envelope（220 × 110 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.PrcEnvelopeNumber6] = new Size(472, 906); 	//PRC #6 envelope（120 × 230 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.PrcEnvelopeNumber6Rotated] = new Size(906, 472); 	//PRC #6 rotated envelope（230 × 120 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.PrcEnvelopeNumber7] = new Size(630, 906); 	//PRC #7 envelope（160 × 230 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.PrcEnvelopeNumber7Rotated] = new Size(906, 630); 	//PRC #7 rotated envelope（230 × 160 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.PrcEnvelopeNumber8] = new Size(472, 1217); 	//PRC #8 envelope（120 × 309 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.PrcEnvelopeNumber8Rotated] = new Size(1217, 472); 	//PRC #8 rotated envelope（309 × 120 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.PrcEnvelopeNumber9] = new Size(902, 1276); 	//PRC #9 envelope（229 × 324 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.PrcEnvelopeNumber9Rotated] = new Size(902, 1276); 	//PRC #9 rotated envelope（229 × 324 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.Quarto] = new Size(846, 1083); 	//Quarto 纸（215 毫米 × 275 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.Standard10x11] = new Size(1000, 1098); 	//Standard 纸（254 毫米 × 279 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.Standard10x14] = new Size(1000, 1402); 	//Standard 纸（254 毫米 × 356 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.Standard11x17] = new Size(1098, 1701); 	//Standard 纸（279 毫米 × 432 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.Standard12x11] = new Size(1201, 1098); 	//Standard 纸（305 × 279 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.Standard15x11] = new Size(1500, 1098); 	//Standard 纸（381 毫米 × 279 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.Standard9x11] = new Size(902, 1098); 	//Standard 纸（229 × 279 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.Statement] = new Size(551, 850); 	//Statement 纸（140 毫米 × 216 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.Tabloid] = new Size(1098, 1701); 	//Tabloid 纸（279 毫米 × 432 毫米）。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.TabloidExtra] = new Size(1169, 1799); 	//Tabloid extra 纸（297 毫米 × 457 毫米）。该值特定于 PostScript 驱动程序，仅供 Linotronic 打印机使用以节省纸张。
            myStandardPaperSize[System.Drawing.Printing.PaperKind.USStandardFanfold] = new Size(1488, 1098); 	//US standard fanfold（378 毫米 × 279 毫米）。

            // 定义 MS WORD 使用的标准页面设置
            myWordDefault = new XPageSettings();
            myWordDefault.PaperKind = PaperKind.A4;
            myWordDefault.Landscape = false;
            myWordDefault.LeftMargin = 125;
            myWordDefault.TopMargin = 100;
            myWordDefault.RightMargin = 125;
            myWordDefault.BottomMargin = 100;
            // 定义 IE 使用的标准页面设置
            myIEDefault = new XPageSettings();
            myIEDefault.PaperKind = PaperKind.A4;
            myIEDefault.Landscape = false;
            myIEDefault.LeftMargin = 75;
            myIEDefault.TopMargin = 75;
            myIEDefault.RightMargin = 75;
            myIEDefault.BottomMargin = 75;
        }
        public static Size GetStandardPaperSize(PaperKind kind)
        {
            if (myStandardPaperSize.ContainsKey(kind))
                return (Size)myStandardPaperSize[kind];
            else
                return Size.Empty;
        }

        private static XPageSettings myWordDefault = null;
        /// <summary>
        /// MS Word使用的默认页面设置
        /// </summary>
        public static XPageSettings WordDefault
        {
            get
            {
                return myWordDefault;
            }
        }

        private static XPageSettings myIEDefault = null;
        /// <summary>
        /// IE使用的默认页面设置
        /// </summary>
        public static XPageSettings IEDefault
        {
            get
            {
                return myIEDefault;
            }
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        public XPageSettings()
        {
        }

        //private bool bolStickToPageSize = true;
        ///// <summary>
        ///// 坚持本对象的纸张大小设置
        ///// </summary>
        ///// <remarks>
        ///// 若该属性值为true时,而且用户选择的纸张大小和本对象设置的纸张大小
        ///// 不一致时,程序坚持使用本对象中定义的纸张大小.
        ///// </remarks>
        //[System.ComponentModel.DefaultValue( true )]
        //public bool StickToPageSize
        //{
        //    get
        //    {
        //        return bolStickToPageSize; 
        //    }
        //    set
        //    {
        //        bolStickToPageSize = value; 
        //    }
        //}

        private string strPrinterName = null;
        /// <summary>
        /// 使用的打印机的名称
        /// </summary>
        [System.ComponentModel.DefaultValue( null )]
        [System.ComponentModel.Editor( 
            "DCSoft.WinForms.Design.PrinterNameEditor" ,
            typeof( System.Drawing.Design.UITypeEditor ))]
        public string PrinterName
        {
            get
            {
                return strPrinterName; 
            }
            set
            {
                strPrinterName = value; 
            }
        }

        private string strPaperSource = null;
        /// <summary>
        /// 纸张来源
        /// </summary>
        [System.ComponentModel.DefaultValue(null)]
        [System.ComponentModel.Editor(
            "DCSoft.WinForms.Design.PaperSourceNameEditor" ,
            typeof( System.Drawing.Design.UITypeEditor ))]
        public string PaperSource
        {
            get
            {
                return strPaperSource;
            }
            set
            {
                strPaperSource = value;
            }
        }

        /// <summary>
        /// 纸张大小
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        [System.Xml.Serialization.XmlIgnore()]
        public PaperSize PaperSize
        {
            get
            {
                return new PaperSize(
                    this.PaperKind.ToString(),
                    this.PaperWidth, 
                    this.PaperHeight);
            }
            set
            {
                intPaperKind = value.Kind;
                intPaperWidth = value.Width;
                intPaperHeight = value.Height;
            }
        }

        private System.Drawing.Printing.PaperKind intPaperKind = System.Drawing.Printing.PaperKind.A4;
        /// <summary>
        /// 纸张尺寸类型
        /// </summary>
        [System.ComponentModel.DefaultValue(PaperKind.A4)]
        public System.Drawing.Printing.PaperKind PaperKind
        {
            get
            {
                return intPaperKind;
            }
            set
            {
                intPaperKind = value;
                //if (myStandardPaperSize.ContainsKey(intPaperKind))
                //{
                //    Size size = (Size)myStandardPaperSize[intPaperKind];
                //    intPaperWidth = size.Width;
                //    intPaperHeight = size.Height;
                //}
            }
        }

        private bool bolAutoPaperWidth = false;
        /// <summary>
        /// 自动设置纸张宽度
        /// </summary>
        [System.ComponentModel.DefaultValue( false )]
        public bool AutoPaperWidth
        {
            get
            {
                return bolAutoPaperWidth; 
            }
            set
            {
                bolAutoPaperWidth = value; 
            }
        }

        private bool bolAutoFitPageSize = false;
        /// <summary>
        /// 自动适应纸张大小
        /// </summary>
        [System.ComponentModel.DefaultValue( false )]
        public bool AutoFitPageSize
        {
            get
            {
                return bolAutoFitPageSize; 
            }
            set
            {
                bolAutoFitPageSize = value; 
            }
        }



        private int _HeaderDistance = 50;
        /// <summary>
        /// 页眉顶端距离页面上边缘的距离，,单位百分之一英寸
        /// </summary>
        [DefaultValue(50)]
        public int HeaderDistance
        {
            get
            {
                return _HeaderDistance;
            }
            set
            {
                _HeaderDistance = value;
                if (_HeaderDistance < 0)
                {
                    _HeaderDistance = 0;
                }
            }
        }

        /// <summary>
        /// 采用视图单位的页眉距离
        /// </summary>
        [Browsable( false )]
        public float ViewHeaderDistance
        {
            get
            {
                return (float)GraphicsUnitConvert.Convert(
                        this.HeaderDistance,
                        GraphicsUnit.Document,
                        this.ViewUnit) * 3;
            }
        }


        /// <summary>
        /// 获得页眉视图高度
        /// </summary>
        [Browsable(false)]
        public float ViewHeaderHeight
        {
            get
            {
                return (float)GraphicsUnitConvert.Convert(
                    this.TopMargin - this.HeaderDistance,
                    GraphicsUnit.Document,
                    this.ViewUnit) * 3;
            }
        }

        private int _FooterDistance = 50;
        /// <summary>
        /// 页脚低端距离页面下边缘的距离，,单位百分之一英寸
        /// </summary>
        [DefaultValue( 50 )]
        public int FooterDistance
        {
            get
            {
                return _FooterDistance;
            }
            set
            {
                _FooterDistance = value;
            }
        }


        /// <summary>
        /// 采用视图单位的页眉距离
        /// </summary>
        [Browsable(false)]
        public float ViewFooterDistance
        {
            get
            {
                return (float)GraphicsUnitConvert.Convert(
                    this.FooterDistance,
                    GraphicsUnit.Document,
                    this.ViewUnit) * 3;
            }
        }

        /// <summary>
        /// 获得页脚视图高度
        /// </summary>
        [Browsable(false)]
        public float ViewFooterHeight
        {
            get
            {
                return (float)GraphicsUnitConvert.Convert(
                    this.BottomMargin - this.FooterDistance,
                    GraphicsUnit.Document,
                    this.ViewUnit) * 3;
            }
        }


        private int _DesignerPaperWidth = 0;
        /// <summary>
        /// 设计器纸张宽度,单位百分之一英寸
        /// </summary>
        [DefaultValue(0)]
        public int DesignerPaperWidth
        {
            get
            {
                return _DesignerPaperWidth; 
            }
            set
            {
                _DesignerPaperWidth = value; 
            }
        }

        private int intPaperWidth = 827 ;
        /// <summary>
        /// 纸张宽度,单位百分之一英寸
        /// </summary>
        [System.ComponentModel.DefaultValue( 827 )]
        public int PaperWidth
        {
            get
            {
                if( intPaperKind != PaperKind.Custom
                    && myStandardPaperSize.ContainsKey(intPaperKind))
                {
                    Size size = (Size)myStandardPaperSize[intPaperKind];
                    return size.Width ;
                }
                else
                {
                    return intPaperWidth ;
                }
            }
            set
            {
                DesignerPaperWidth = value;
                intPaperWidth = value;
            }
        }

        private int _DesignerPaperHeight = 0;
        /// <summary>
        /// 设计器纸张高度
        /// </summary>
        [DefaultValue( 0 )]
        public int DesignerPaperHeight
        {
            get { return _DesignerPaperHeight; }
            set { _DesignerPaperHeight = value; }
        }

        private int intPaperHeight = 1169 ;
        /// <summary>
        /// 纸张高度 单位百分之一英寸
        /// </summary>
        [System.ComponentModel.DefaultValue( 1169 )]
        public int PaperHeight
        {
            get
            {
                if (intPaperKind != PaperKind.Custom
                    && myStandardPaperSize.ContainsKey(intPaperKind))
                {
                    Size size = (Size)myStandardPaperSize[intPaperKind];
                    return size.Height;
                }
                else
                {
                    return intPaperHeight;
                }
            }
            set
            {
                DesignerPaperHeight = value;
                intPaperHeight = value;
            }
        }

        /// <summary>
        /// 页边距,单位百分之一英寸
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        [System.Xml.Serialization.XmlIgnore()]
        public Margins Margins
        {
            get
            {
                return new Margins(
                    intLeftMargin ,
                    intRightMargin ,
                    intTopMargin , 
                    intBottomMargin );
            }
            set
            {
                intLeftMargin = value.Left;
                intTopMargin = value.Top;
                intRightMargin = value.Right;
                intBottomMargin = value.Bottom;
            }
        }

        private int intLeftMargin = 100;
        /// <summary>
        /// 左页边距 单位百分之一英寸
        /// </summary>
        [System.ComponentModel.DefaultValue( 100 )]
        public int LeftMargin
        {
            get
            {
                return intLeftMargin; 
            }
            set
            {
                intLeftMargin = value; 
            }
        }

        private int intTopMargin = 100;
        /// <summary>
        /// 顶页边距 单位百分之一英寸
        /// </summary>
        [System.ComponentModel.DefaultValue(100)]
        public int TopMargin
        {
            get
            { 
                return intTopMargin; 
            }
            set
            {
                intTopMargin = value; 
            }
        }

        private int intRightMargin = 100;
        /// <summary>
        /// 右页边距 单位百分之一英寸
        /// </summary>
        [System.ComponentModel.DefaultValue(100)]
        public int RightMargin
        {
            get
            {
                return intRightMargin; 
            }
            set 
            {
                intRightMargin = value; 
            }
        }

        private int intBottomMargin = 100;
        /// <summary>
        /// 底页边距 单位百分之一英寸
        /// </summary>
        [System.ComponentModel.DefaultValue(100)]
        public int BottomMargin
        {
            get
            {
                return intBottomMargin; 
            }
            set
            {
                intBottomMargin = value; 
            }
        }

        private bool bolLandscape = false;
        /// <summary>
        /// 横向打印标记
        /// </summary>
        [System.ComponentModel.DefaultValue( false )]
        public bool Landscape
        {
            get
            {
                return bolLandscape; 
            }
            set
            {
                bolLandscape = value; 
            }
        }
        
        /// <summary>
        /// 设置或返回标准的页面设置对象
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        [System.Xml.Serialization.XmlIgnore()]
        public System.Drawing.Printing.PageSettings StdPageSettings
        {
            get
            {
                System.Drawing.Printing.PageSettings ps = new System.Drawing.Printing.PageSettings();
                if (intPaperKind == System.Drawing.Printing.PaperKind.Custom)
                {
                    if (this.bolLandscape == false)
                        ps.PaperSize = new System.Drawing.Printing.PaperSize("Custom", this.PaperWidth, this.PaperHeight);
                    else
                        ps.PaperSize = new System.Drawing.Printing.PaperSize("Custom", this.PaperWidth, this.PaperHeight);
                }
                else
                {
                    bool bolSet = false;
                    System.Drawing.Printing.PrinterSettings pst = new System.Drawing.Printing.PrinterSettings();
                    foreach (System.Drawing.Printing.PaperSize size in pst.PaperSizes)
                    {
                        if (size.Kind == intPaperKind )
                        {
                            ps.PaperSize = size;
                            bolSet = true;
                            break;
                        }
                    }
                    if (bolSet == false)
                    {
                        if (this.bolLandscape == false)
                            ps.PaperSize = new System.Drawing.Printing.PaperSize("Custom", this.PaperWidth, this.PaperHeight);
                        else
                            ps.PaperSize = new System.Drawing.Printing.PaperSize("Custom", this.PaperWidth, this.PaperHeight);
                    }
                }
                ps.Margins = new Margins(intLeftMargin, intRightMargin , intTopMargin, intBottomMargin );
                ps.Landscape = this.bolLandscape;

                return ps;
            }
            set
            {
                if (value != null)
                {
                    intLeftMargin = value.Margins.Left;
                    intTopMargin = value.Margins.Top;
                    intRightMargin = value.Margins.Right;
                    intBottomMargin = value.Margins.Bottom;
                    intPaperKind = value.PaperSize.Kind;
                    intPaperWidth = value.PaperSize.Width;
                    intPaperHeight = value.PaperSize.Height;
                    this.bolLandscape = value.Landscape;
                }
            }
        }

        private GraphicsUnit intViewUnit = GraphicsUnit.Document;
        /// <summary>
        /// 视图区域使用的度量单位
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        [System.Xml.Serialization.XmlIgnore()]
        public GraphicsUnit ViewUnit
        {
            get
            {
                return intViewUnit; 
            }
            set
            {
                intViewUnit = value; 
            }
        }

        /// <summary>
        /// 视图单位的左页边距
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public float ViewLeftMargin
        {
            get
            {
                return (float)GraphicsUnitConvert.Convert(
                    intLeftMargin,
                    GraphicsUnit.Document,
                    intViewUnit) * 3;
            }
        }

        /// <summary>
        /// 视图单位的顶页边距
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public float ViewTopMargin
        {
            get
            {
                return (float)GraphicsUnitConvert.Convert(
                    intTopMargin,
                    GraphicsUnit.Document,
                    intViewUnit) * 3;
            }
        }

        /// <summary>
        /// 视图单位的右页边距
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public float ViewRightMargin
        {
            get
            {
                return (float)GraphicsUnitConvert.Convert(
                    intRightMargin,
                    GraphicsUnit.Document,
                    intViewUnit) * 3;
            }
        }

        /// <summary>
        /// 视图单位的下页边距
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public float ViewBottomMargin
        {
            get
            {
                return (float)GraphicsUnitConvert.Convert(
                    intBottomMargin,
                    GraphicsUnit.Document,
                    intViewUnit) * 3;
            }
        }

        /// <summary>
        /// 纸张的视图宽度
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public float ViewPaperWidth
        {
            get
            {
                return ( float ) GraphicsUnitConvert.Convert(
                    this.Landscape ? this.PaperHeight : this.PaperWidth, 
                    GraphicsUnit.Document,
                    this.ViewUnit)
                    * 3;
            }
        }

        /// <summary>
        /// 纸张的视图高度
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public float ViewPaperHeight
        {
            get
            {
                return (float)GraphicsUnitConvert.Convert(
                    this.Landscape ? this.PaperWidth : this.PaperHeight ,
                    GraphicsUnit.Document,
                    this.ViewUnit)
                    * 3;
            }
        }

        /// <summary>
        /// 纸张可打印的客户区域的宽度,单位 Document
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        public float ViewClientWidth
        {
            get
            {
                int w = this.Landscape ? this.PaperHeight : this.PaperWidth;
                w = w - this.LeftMargin - this.RightMargin;
                return (float)GraphicsUnitConvert.Convert(w, GraphicsUnit.Document, this.ViewUnit) * 3;
            }
            set
            {
                double w = GraphicsUnitConvert.Convert(value, this.ViewUnit, GraphicsUnit.Document) / 3.0;
                w = w + this.LeftMargin + this.RightMargin;
                if (this.Landscape)
                {
                    this.PaperHeight = (int)w;
                }
                else
                {
                    this.PaperWidth = (int)w;
                }
            }
        }


        /// <summary>
        /// 纸张可打印的客户区域的高度
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public float ViewClientHeight
        {
            get
            {
                int h = this.Landscape ? this.PaperWidth : this.PaperHeight;
                h = h - this.TopMargin - this.BottomMargin;
                return (float)GraphicsUnitConvert.Convert(h, GraphicsUnit.Document, this.ViewUnit) * 3;
            }
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        object ICloneable.Clone()
        {
            XPageSettings ps = new XPageSettings();
            this.CopyTo(ps);
            return ps;
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public XPageSettings Clone()
        {
            return (XPageSettings)((ICloneable)this).Clone();
        }

        public void CopyTo(XPageSettings ps)
        {
            if (ps != null)
            {
                ps.intPaperKind = intPaperKind;
                ps.intPaperWidth = intPaperWidth;
                ps.intPaperHeight = intPaperHeight;

                ps.intLeftMargin = intLeftMargin;
                ps.intTopMargin = intTopMargin;
                ps.intRightMargin = intRightMargin;
                ps.intBottomMargin = intBottomMargin;

                ps.bolLandscape = bolLandscape;
                ps.strPaperSource = strPaperSource;

                ps.intViewUnit = intViewUnit;
                ps.strPrinterName = this.strPrinterName;
                //ps.bolStickToPageSize = this.bolStickToPageSize;
                ps.bolAutoPaperWidth = this.bolAutoPaperWidth;
                ps.bolAutoFitPageSize = this.bolAutoFitPageSize;
                ps._HeaderDistance = this._HeaderDistance;
                ps._FooterDistance = this._FooterDistance;
            }
        }

        /// <summary>
        /// 解析文本获得页面设置信息,本方法能解析ToString()函数输出的文本
        /// </summary>
        /// <remarks >
        /// 本方法支持的文本格式为“纸张类型,Landscape,LeftMargin=整数,TopMargin=整数,RightMargin=整数,BttomMargin=整数,PrinterName=打印机名称,PaperSource=纸张来源,StickToPageSize=True/False,AutoPaperWidth”
        /// 文本中各个项目间用半角逗号分开,可以只设置某些属性，未指明的属性值采用默认值。
        /// </remarks>
        /// <param name="Value">要解析的文本</param>
        public void Parse(string Value)
        {
            if (Value == null)
                return;
            string[] items = Value.Split(',');
            foreach (string item in items)
            {
                if (Enum.IsDefined(typeof(System.Drawing.Printing.PaperKind), item))
                {
                    // 解析纸张类型
                    this.PaperKind = (PaperKind)Enum.Parse(typeof(PaperKind), item, true);
                }
                else
                {
                    string strName = item.Trim().ToLower();
                    string strValue = "";
                    int index = item.IndexOf("=");
                    if (index > 0)
                    {
                        strName = item.Substring(0, index).Trim().ToLower();
                        strValue = item.Substring(index + 1).Trim();
                    }
                    switch (strName)
                    {
                        case "landscape":
                            // 横向打印
                            if (strValue.Length > 0)
                            {
                                bool b = false;
                                if (bool.TryParse(strValue, out b))
                                {
                                    this.Landscape = b;
                                }
                            }
                            else
                            {
                                this.Landscape = true;
                            }
                            break;
                        //case "sticktopagesize":
                        //    // 强制引用纸张大小
                        //    if (strValue.Length > 0)
                        //    {
                        //        bool b = true;
                        //        if (bool.TryParse(strValue, out b))
                        //        {
                        //            this.StickToPageSize = b;
                        //        }
                        //    }
                        //    else
                        //    {
                        //        this.StickToPageSize = true;
                        //    }
                        //    break;
                        case "autopaperwidth":
                            // 自动设置纸张宽度
                            if (strValue.Length > 0)
                            {
                                bool b = true;
                                if (bool.TryParse(strValue, out b))
                                {
                                    this.AutoPaperWidth = b;
                                }
                            }
                            else
                            {
                                this.AutoPaperWidth = true;
                            }
                            break;
                        case "autofitpagesize":
                            // 自动适应纸张大小
                            if (strValue.Length > 0)
                            {
                                bool b = true;
                                if (bool.TryParse(strValue, out b))
                                {
                                    this.AutoFitPageSize = b;
                                }
                            }
                            else
                            {
                                this.AutoFitPageSize = true;
                            }
                            break;
                        case "paperwidth":
                            // 纸张宽度
                            int w = 0;
                            if (strValue.Length > 0 && int.TryParse(strValue, out w))
                            {
                                this.PaperWidth = w;
                            }
                            break;
                        case "paperheight":
                            // 纸张高度
                            int h = 0;
                            if (strValue.Length > 0 && int.TryParse(strValue, out h))
                            {
                                this.PaperHeight = h;
                            }
                            break;
                        case "leftmargin":
                            // 左页边距
                            int left = 0;
                            if (strValue.Length > 0 && int.TryParse(strValue, out left))
                            {
                                this.LeftMargin = left;
                            }
                            break;
                        case "topmargin":
                            // 上页边距
                            int top = 0;
                            if (strValue.Length > 0 && int.TryParse(strValue, out top))
                            {
                                this.TopMargin = top;
                            }
                            break;
                        case "rightmargin":
                            // 右页边距
                            int right = 0;
                            if (strValue.Length > 0 && int.TryParse(strValue, out right))
                            {
                                this.RightMargin = right;
                            }
                            break;
                        case "bottommargin":
                            // 下页边距
                            int bottom = 0;
                            if (strValue.Length > 0 && int.TryParse(strValue, out bottom))
                            {
                                this.BottomMargin = bottom;
                            }
                            break;
                        case "headerdistance":
                            {
                                int v = 0;
                                if (strValue.Length > 0 && int.TryParse(strValue, out v))
                                {
                                    this.HeaderDistance = v;
                                }
                            }
                            break;
                        case "footerdistance":
                            {
                                int v = 0;
                                if (strValue.Length > 0 && int.TryParse(strValue, out v))
                                {
                                    this.FooterDistance = v;
                                }
                            }
                            break;
                        case "printername":
                            // 打印机名称
                            this.PrinterName = strValue;
                            break;
                        case "papersource":
                            // 纸张来源
                            this.PaperSource = strValue;
                            break;
                    }
                }
            }//foreach
        }

        public override string ToString()
        {
            System.Text.StringBuilder str = new StringBuilder();
            str.Append(intPaperKind.ToString());
            if (this.PaperKind == PaperKind.Custom)
            {
                str.Append(",PaperWidth=" + this.PaperWidth);
                str.Append(",PaperHeight=" + this.PaperHeight);
            }
            if (this.Landscape)
            {
                str.Append(",Landscape");
            }
            if (this.LeftMargin != 100)
            {
                str.Append(",LeftMargin=" + this.LeftMargin);
            }
            if (this.TopMargin != 100)
            {
                str.Append(",TopMargin=" + this.TopMargin);
            }
            if (this.RightMargin != 100)
            {
                str.Append(",RightMargin=" + this.RightMargin );
            }
            if( this.BottomMargin != 100 )
            {
                str.Append(",BottomMargin=" + this.BottomMargin);
            }
            if (this.PrinterName != null && this.PrinterName.Length > 0)
            {
                str.Append(",PrinterName=" + this.PrinterName);
            }
            if (this.PaperSource != null && this.PaperSource.Length > 0)
            {
                str.Append(",PaperSource=" + this.PaperSource);
            }
            //if (this.StickToPageSize == false)
            //{
            //    str.Append(",StickToPageSize=False");
            //}
            if (this.AutoPaperWidth)
            {
                str.Append(",AutoPaperWidth");
            }
            if (this.AutoFitPageSize)
            {
                str.Append(",AutoFitPageSize");
            }
            if (this.HeaderDistance > 0)
            {
                str.Append(",HeaderDistance=" + this.HeaderDistance );
            }
            if (this.FooterDistance > 0)
            {
                str.Append(",FooterDistance=" + this.FooterDistance);
            }
            return str.ToString();
        }

    }//public class XPageSettings
     

    public class XPageSettingEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object Value)
        {
            using (dlgPageSetup dlg = new dlgPageSetup())
            {
                XPageSettings settings = Value as XPageSettings;
                if (settings == null)
                    dlg.PageSettings = new XPageSettings();
                else
                    dlg.PageSettings = settings.Clone();
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    return dlg.PageSettings;
                }
            }
            return Value;
        }
    }

    public class XPageSettingsTypeConverter : TypeConverter
    {
        public override PropertyDescriptorCollection GetProperties(
            ITypeDescriptorContext context,
            object Value,
            Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(typeof(XPageSettings), attributes).Sort(new string[]{
                "PaperKind" ,
                "PaperWidth" ,
                "PaperHeight",
                "Landscape" ,
                "LeftMargin",
                "TopMargin",
                "RightMargin" ,
                "BottomMargin",
                "PaperSource",
                "PrinterName",
                "StickToPageSize",
                "HeaderDistance",
                "FooterDistance"
                });
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if( sourceType.Equals( typeof( string )))
                return true ;
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object Value)
        {
            if (Value is string)
            {
                XPageSettings settings = new XPageSettings();
                settings.Parse((string)Value);
                return settings;
            }//if
            return base.ConvertFrom(context, culture, Value);
        }
    }

}
