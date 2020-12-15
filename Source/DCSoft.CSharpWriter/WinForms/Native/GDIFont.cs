/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DCSoft.WinForms.Native
{
    /// <summary>
    /// GDI字体对象
    /// </summary>
    /// <remarks>编写 袁永福</remarks>
    public class GDIFont : GDIObject
    {
        public GDIFont(string faceName, int height, bool bold , bool italic , bool underline , bool strickout )
        {
            _Font = new LOGFONT();
            _Font.lfHeight = height;
            _Font.lfFaceName = faceName;
            _Font = new LOGFONT();
            _Font.lfHeight = height;
            _Font.lfFaceName = faceName;
            _Font.lfWeight = bold ? 700 : 400;
            _Font.lfItalic = italic ? (byte)1 : (byte)0;
            _Font.lfUnderline = underline ? (byte)1:(byte)0;
            _Font.lfStrikeOut = strickout ? (byte)1 : (byte)0;

            base.intHandle = CreateFont(
                _Font.lfHeight,
                _Font.lfWidth,
                _Font.lfEscapement,
                _Font.lfOrientation,
                _Font.lfWeight,
                _Font.lfItalic,
                _Font.lfUnderline,
                _Font.lfStrikeOut,
                1,
                _Font.lfOutPrecision,
                _Font.lfClipPrecision,
                _Font.lfQuality,
                _Font.lfPitchAndFamily,
                _Font.lfFaceName);
        }

        public GDIFont(Font font )
        {
            float fontSizeInLayoutUnits = font.Size;// CalculateFontSizeInLayoutUnits(font);
            _Font = new LOGFONT();
            font.ToLogFont(_Font);
            _Font.lfHeight = (int)-Math.Round(fontSizeInLayoutUnits / 1f);
            base.intHandle = CreateFont(
                _Font.lfHeight,
                _Font.lfWidth,
                _Font.lfEscapement,
                _Font.lfOrientation,
                _Font.lfWeight,
                _Font.lfItalic,
                _Font.lfUnderline,
                _Font.lfStrikeOut,
                1,
                _Font.lfOutPrecision,
                _Font.lfClipPrecision,
                _Font.lfQuality,
                _Font.lfPitchAndFamily,
                _Font.lfFaceName);
        }

        private LOGFONT _Font = new LOGFONT();

        public int Height{ get{ return _Font.lfHeight ;}}
        public int Width{ get { return _Font.lfWidth ;}}
        public int Escapement{ get { return _Font.lfEscapement ;}}
        public int Weight{ get { return _Font.lfWeight ;}}
        public bool Italic{ get { return  _Font.lfItalic !=0;}}
        public bool Underline{ get { return  _Font.lfUnderline != 0 ;}}
        public bool StrikeOut{ get { return _Font.lfStrikeOut != 0 ;}}
        public LogFontCharSet Charset{ get { return _Font.lfCharSet ;}}
        public byte OutPrecision{ get{ return _Font.lfOutPrecision ;}}
        public byte ClipPrecision{ get{ return _Font.lfClipPrecision ;}}
        public byte Quality{ get{ return _Font.lfQuality ;}}
        public byte PitchAndFamily{ get{ return _Font.lfPitchAndFamily ;}}
        public string Name{ get{ return _Font.lfFaceName ;}}


        public Rectangle[] MeasureCharacterRanges(
            Graphics graphics,
            string text,
            StringFormat stringFormat )
        {
            return MeasureCharacterRanges(
                graphics,
                text,
                RectangleF.Empty,
                graphics.PageUnit);
        }

        public Rectangle[] MeasureCharacterRanges( 
            Graphics graphics,
            string text, 
            RectangleF layoutRect,
            GraphicsUnit pageUnit)
        {
            Rectangle bounds = Rectangle.Empty;
            if (layoutRect.IsEmpty)
            {
                bounds = new Rectangle(0, 0, 1000, 1000);
            }
            else
            {
                bounds = Rectangle.Round(layoutRect);
            }
			IntPtr hdc = graphics.GetHdc();
			try {
				IntPtr oldFont = SelectObject(hdc, this.Handle);
				GCP_RESULTS gcpResults = new GCP_RESULTS();
				gcpResults.lStructSize = Marshal.SizeOf(typeof(GCP_RESULTS));
				gcpResults.lpOutString = IntPtr.Zero;
				gcpResults.lpOrder = IntPtr.Zero;
				gcpResults.lpDx = IntPtr.Zero;
				gcpResults.lpCaretPos = Marshal.AllocCoTaskMem(sizeof(int) * text.Length);
				gcpResults.lpClass = IntPtr.Zero;
				gcpResults.lpGlyphs = IntPtr.Zero;
				gcpResults.nGlyphs = text.Length;
				GetCharacterPlacement(
					hdc,
					text,
					text.Length,
					0,
					ref gcpResults,
					GcpFlags.GCP_USEKERNING | GcpFlags.GCP_LIGATE);
				int count = text.Length;
				Rectangle[] result = new Rectangle[count];
				int prevPos = Marshal.ReadInt32(gcpResults.lpCaretPos, 0);
				for (int i = 0; i < count - 1; i++) {
					int nextPos = Marshal.ReadInt32(gcpResults.lpCaretPos, (i + 1) * sizeof(int));
					result[i] = new Rectangle(bounds.X + prevPos, bounds.Y, nextPos - prevPos, bounds.Height);
					prevPos = nextPos;
				}
				if (count > 0)
                {
					result[count - 1] = new Rectangle(bounds.X + prevPos, bounds.Y, bounds.Width - prevPos, bounds.Height);
                }
				Marshal.FreeCoTaskMem(gcpResults.lpCaretPos);
				SelectObject(hdc, oldFont);
                return result ;
			}
			finally {
				graphics.ReleaseHdc(hdc);
			}
		}
        
        
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto), ComVisible(false)]
        private class LOGFONT
        {
            public int lfHeight = 0;
            public int lfWidth = 0;
            public int lfEscapement = 0;
            public int lfOrientation = 0;
            public int lfWeight = 0;
            public byte lfItalic = 0;
            public byte lfUnderline = 0;
            public byte lfStrikeOut = 0;
            public LogFontCharSet lfCharSet = 0;
            public byte lfOutPrecision = 0;
            public byte lfClipPrecision = 0;
            public byte lfQuality = 0;
            public byte lfPitchAndFamily = 0;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string lfFaceName = null;
        }

        public enum LogFontCharSet : byte
        {
            ANSI = 0,
            DEFAULT = 1,
            SYMBOL = 2,
            SHIFTJIS = 128,
            HANGEUL = 129,
            HANGUL = 129,
            GB2312 = 134,
            CHINESEBIG5 = 136,
            OEM = 255,
            JOHAB = 130,
            HEBREW = 177,
            ARABIC = 178,
            GREEK = 161,
            TURKISH = 162,
            VIETNAMESE = 163,
            THAI = 222,
            EASTEUROPE = 238,
            RUSSIAN = 204,
            MAC = 77,
            BALTIC = 186
        }

        public Size[] MeasureCharactersSize(Graphics graphics, string text)
        {
            //double rate = DCSoft.Drawing.GraphicsUnitConvert.Convert(
            //    1.0,
            //    System.Drawing.GraphicsUnit.Pixel ,
            //    graphics.PageUnit);
            double rate = 1;
            IntPtr hdc = graphics.GetHdc();
            try
            {
                IntPtr oldFont = SelectObject(hdc, this.Handle);
                NativeSize[] sizes = new NativeSize[ text.Length ] ;
                int bsLen = System.Text.Encoding.Default.GetByteCount(text);
                if (GetTextExtentPoint32(hdc, text, bsLen , ref sizes[0]))
                {
                    Size[] result = new Size[text.Length];
                    for (int iCount = 0; iCount < text.Length; iCount++)
                    {
                        result[iCount].Width = (int)(sizes[iCount].cx * rate );
                        result[iCount].Height = (int) (sizes[iCount].cy * rate );
                    }
                    return result;
                }
            }
            finally
            {
                graphics.ReleaseHdc(hdc);
            }
            return null;
        }

        [DllImport("gdi32.dll")]
        private static extern bool GetTextExtentPoint32(IntPtr hdc, string text, int cbString , ref NativeSize sizes );

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto), ComVisible(false)]
        private struct NativeSize
        {
            public int cx;
            public int cy;
        }

        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateFont(
            int nHeight,
            int nWidth,
            int nEscapement,
            int nOrientation,
            int fnWeight,
            int fdwItalic,
            int fdwUnderline,
            int fdwStrikeOut,
            int fdwCharSet,
            int fdwOutputPrecision,
            int fdwClipPrecision,
            int fdwQuality,
            int fdwPitchAndFamily,
            string lpszFace
        );

        [DllImport("gdi32.dll")]
        private extern static int GetCharacterPlacement(
            IntPtr hdc, 
            string lpString,
            int nCount, 
            int nMaxExtent,
            ref GCP_RESULTS lpResults,
            int dwFlags);
        
        [DllImport("gdi32.dll")]
		private static extern IntPtr SelectObject(IntPtr hdc, IntPtr hGdiObj);
		

        [ComVisible(false)]
        private struct GCP_RESULTS
        {
            public IntPtr lpCaretPos;
            public IntPtr lpClass;
            public IntPtr lpDx;
            public IntPtr lpGlyphs;
            public IntPtr lpOrder;
            public IntPtr lpOutString;
            public int lStructSize;
            public int nGlyphs;
            public int nMaxFit;
        }


        private class GcpFlags
        {
            public const int GCP_CLASSIN = 524288;
            public const int GCP_DBCS = 1;
            public const int GCP_DIACRITIC = 256;
            public const int GCP_DISPLAYZWG = 4194304;
            public const int GCP_ERROR = 32768;
            public const int GCP_GLYPHSHAPE = 16;
            public const int GCP_JUSTIFY = 65536;
            public const int GCP_JUSTIFYIN = 2097152;
            public const int GCP_KASHIDA = 1024;
            public const int GCP_LIGATE = 32;
            public const int GCP_MAXEXTENT = 1048576;
            public const int GCP_NEUTRALOVERRIDE = 33554432;
            public const int GCP_NUMERICOVERRIDE = 16777216;
            public const int GCP_NUMERICSLATIN = 67108864;
            public const int GCP_NUMERICSLOCAL = 134217728;
            public const int GCP_REORDER = 2;
            public const int GCP_SYMSWAPOFF = 8388608;
            public const int GCP_USEKERNING = 8;
        }


        //[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        //private struct TEXTMETRIC {
        //    public int tmHeight;
        //    public int tmAscent;
        //    public int tmDescent;
        //    public int tmInternalLeading;
        //    public int tmExternalLeading;
        //    public int tmAveCharWidth;
        //    public int tmMaxCharWidth;
        //    public int tmWeight;
        //    public int tmOverhang;
        //    public int tmDigitizedAspectX;
        //    public int tmDigitizedAspectY;
        //    public byte tmFirstChar;
        //    public byte tmLastChar;
        //    public byte tmDefaultChar;
        //    public byte tmBreakChar;
        //    public byte tmItalic;
        //    public byte tmUnderlined;
        //    public byte tmStruckOut;
        //    public byte tmPitchAndFamily;
        //    public byte tmCharSet;
        //}

        //[StructLayout(LayoutKind.Sequential)]
        //internal struct PANOSE {
        //    public byte bFamilyType;
        //    public byte bSerifStyle;
        //    public byte bWeight;
        //    public byte bProportion;
        //    public byte bContrast;
        //    public byte bStrokeVariation;
        //    public byte bArmStyle;
        //    public byte bLetterform;
        //    public byte bMidline;
        //    public byte bXHeight;
        //    public byte[] ToByteArray() {
        //        return new byte[] { bFamilyType, bSerifStyle, bWeight, bProportion, bContrast, bStrokeVariation, bArmStyle, bLetterform, bMidline, bXHeight };
        //    }
        //}

        //[StructLayout(LayoutKind.Sequential)]
        //private struct POINT {
        //    public int X;
        //    public int Y;
        //    public POINT(int x, int y) {
        //        this.X = x;
        //        this.Y = y;
        //    }
        //    public static implicit operator Point(POINT p) {
        //        return new Point(p.X, p.Y);
        //    }
        //    public static implicit operator POINT(Point p) {
        //        return new POINT(p.X, p.Y);
        //    }
        //    public static explicit operator Size(POINT p) {
        //        return new Size(p.X, p.Y);
        //    }
        //}

        
        //[StructLayout(LayoutKind.Sequential)]
        //private struct RECT
        //{
        //    public int left;
        //    public int top;
        //    public int right;
        //    public int bottom;
        //}


        //[StructLayout(LayoutKind.Sequential)]
        //private struct OUTLINETEXTMETRIC {
        //    public uint otmSize;
        //    public TEXTMETRIC otmTextMetrics;
        //    public byte otmFiller;
        //    public PANOSE otmPanoseNumber;
        //    public uint otmfsSelection;
        //    public uint otmfsType;
        //    public int otmsCharSlopeRise;
        //    public int otmsCharSlopeRun;
        //    public int otmItalicAngle;
        //    public uint otmEMSquare;
        //    public int otmAscent;
        //    public int otmDescent;
        //    public uint otmLineGap;
        //    public uint otmsCapEmHeight;
        //    public uint otmsXHeight;
        //    public RECT otmrcFontBox;
        //    public int otmMacAscent;
        //    public int otmMacDescent;
        //    public uint otmMacLineGap;
        //    public uint otmusMinimumPPEM;
        //    public POINT otmptSubscriptSize;
        //    public POINT otmptSubscriptOffset;
        //    public POINT otmptSuperscriptSize;
        //    public POINT otmptSuperscriptOffset;
        //    public uint otmsStrikeoutSize;
        //    public int otmsStrikeoutPosition;
        //    public int otmsUnderscoreSize;
        //    public int otmsUnderscorePosition;
        //    public uint otmpFamilyName;
        //    public uint otmpFaceName;
        //    public uint otmpStyleName;
        //    public uint otmpFullName;
        //}

        //[DllImport("gdi32.dll", EntryPoint = "GetOutlineTextMetricsA")]
        //private static extern uint GetOutlineTextMetrics(IntPtr hdc, uint cbData, IntPtr ptrZero);
        //[DllImport("gdi32.dll")]
        //private static extern bool DeleteObject(IntPtr hObject);
		
        //[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Size = (4 + 2) * 4)]
        //private struct FONTSIGNATURE
        //{
        //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        //    public Int32[] fsUsb;
        //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        //    public Int32[] fsCsb;
        //}

        //[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        //private struct CHARSETINFO
        //{
        //    public int ciCharset;
        //    public int ciACP;
        //    [MarshalAs(UnmanagedType.Struct)]
        //    public FONTSIGNATURE fSig;
        //}

        //internal OUTLINETEXTMETRIC? GetOutlineTextMetrics(Graphics gr) {
        //    IntPtr hdc = gr.GetHdc();
        //    try {
        //        IntPtr oldFontHandle = SelectObject(hdc, this.Handle );
        //        try {
        //            return GetOutlineTextMetrics(hdc);
        //        }
        //        finally {
        //            SelectObject(hdc, oldFontHandle);
        //        }
        //    }
        //    finally {
        //        gr.ReleaseHdc(hdc);
        //    }
        //}

        //private static readonly int systemFontQuality = CalculateSystemFontQuality();

        //protected internal static int CalculateActualFontQuality(int quality) {
        //    if (quality != 0)
        //        return 0;
        //    return systemFontQuality;
        //}
        //static int CalculateSystemFontQuality() {
        //    try {
        //        switch (SystemInformation.FontSmoothingType) {
        //            case 1: 
        //                return 4; 
        //            case 2: 
        //                return 6; 
        //            default:
        //                return 0;
        //        }
        //    }
        //    catch (NotSupportedException) { 
        //        return 0;
        //    }
        //}
        //internal static IntPtr CreateGdiFontInLayoutUnits(Font font ) {
        //    LOGFONT lf = new LOGFONT();
        //    font.ToLogFont(lf);
        //    lf.lfHeight = -(int)Math.Round(font.Size / 1f);
        //    lf.lfQuality = (byte)CalculateActualFontQuality((int)lf.lfQuality);
        //    IntPtr hFont = CreateFont(lf.lfHeight, lf.lfWidth, lf.lfEscapement, lf.lfOrientation, lf.lfWeight, lf.lfItalic, lf.lfUnderline, lf.lfStrikeOut, 1, lf.lfOutPrecision, lf.lfClipPrecision, lf.lfQuality, lf.lfPitchAndFamily, lf.lfFaceName);
        //    return hFont;
        //}

        //internal static IntPtr CreateGdiFont(Font font ) {
        //    float fontSizeInLayoutUnits = font.Size;// CalculateFontSizeInLayoutUnits(font);
        //    LOGFONT lf = new LOGFONT();
        //    font.ToLogFont(lf);
        //    lf.lfHeight = (int)-Math.Round(fontSizeInLayoutUnits / 1f);
        //    IntPtr hFont = CreateFont(
        //        lf.lfHeight,
        //        lf.lfWidth, 
        //        lf.lfEscapement,
        //        lf.lfOrientation,
        //        lf.lfWeight,
        //        lf.lfItalic,
        //        lf.lfUnderline,
        //        lf.lfStrikeOut,
        //        1,
        //        lf.lfOutPrecision,
        //        lf.lfClipPrecision, 
        //        lf.lfQuality,
        //        lf.lfPitchAndFamily, 
        //        lf.lfFaceName);
        //    return hFont;
        //}

        //static float CalculateFontSizeInLayoutUnits(Font font, DocumentLayoutUnitConverter unitConverter) {
        //    switch (font.Unit) {
        //        case GraphicsUnit.Document:
        //            return unitConverter.DocumentsToFontUnitsF(font.Size);
        //        case GraphicsUnit.Inch:
        //            return unitConverter.InchesToFontUnitsF(font.Size);
        //        case GraphicsUnit.Millimeter:
        //            return unitConverter.MillimetersToFontUnitsF(font.Size);
        //        case GraphicsUnit.Point:
        //            return unitConverter.PointsToFontUnitsF(font.Size);
        //        default:
        //            Exceptions.ThrowInternalException();
        //            return 0;
        //    }
        //}
        //static OUTLINETEXTMETRIC? GetOutlineTextMetrics(IntPtr hdc) {
        //    uint bufferSize = GetOutlineTextMetrics(hdc, 0, IntPtr.Zero);
        //    if (bufferSize == 0)
        //        return null;
        //    IntPtr buffer = Marshal.AllocHGlobal((int)bufferSize);
        //    try {
        //        if (GetOutlineTextMetrics(hdc, bufferSize, buffer) != 0) {
        //            return (OUTLINETEXTMETRIC)Marshal.PtrToStructure(buffer, typeof(OUTLINETEXTMETRIC));
        //        }
        //    }
        //    finally {
        //        Marshal.FreeHGlobal(buffer);
        //    }
        //    return null;
        //}
        //#endregion

        //protected internal override void CalculateFontVerticalParameters(FontInfoMeasurer measurer) {
        //    FontFamily family = Font.FontFamily;
        //    FontStyle style = Font.Style;
        //    float sizeInUnits = Font.Size / measurer.UnitConverter.FontSizeScale;
        //    float emSize = family.GetEmHeight(style);
        //    float ratio = sizeInUnits / emSize;
        //    Ascent = (int)Math.Ceiling(family.GetCellAscent(style) * ratio);
        //    Descent = (int)Math.Ceiling(family.GetCellDescent(style) * ratio);
        //    LineSpacing = (int)Math.Ceiling(family.GetLineSpacing(style) * ratio);
        //}
        //protected internal override void CalculateUnderlineAndStrikeoutParameters( Graphics g ) {
        //    OUTLINETEXTMETRIC? otm = GetOutlineTextMetrics(g);
        //    if (otm != null)
        //        CalculateUnderlineAndStrikeoutParametersCore(otm.Value);
        //}
        //protected internal override int CalculateFontCharset( Graphics g ) {
        //    IntPtr hdc = g.GetHdc();
        //    try {
        //        IntPtr oldFontHandle =SelectObje t(hdc, this.Handle);
        //        try {
        //            return (int)GetFontCharset(hdc);
        //        }
        //        finally {
        //            SelectObject(hdc, oldFontHandle);
        //        }
        //    }
        //    finally {
        //        g.ReleaseHdc(hdc);
        //    }
        //}

	
        //internal void CalculateUnderlineAndStrikeoutParametersCore(OUTLINETEXTMETRIC otm) {
        //    this.UnderlinePosition = -otm.otmsUnderscorePosition;
        //    this.UnderlineThickness = otm.otmsUnderscoreSize;
        //    this.StrikeoutPosition = otm.otmsStrikeoutPosition;
        //    this.StrikeoutThickness = (int)otm.otmsStrikeoutSize;
        //    this.SubscriptSize = (Size)otm.otmptSubscriptSize;
        //    this.SubscriptOffset = otm.otmptSubscriptOffset;
        //    this.SuperscriptOffset = otm.otmptSuperscriptOffset;
        //    Point offset = this.SuperscriptOffset;
        //    offset.Y = -offset.Y;
        //    this.SuperscriptOffset = offset;
        //    this.SuperscriptSize = (Size)otm.otmptSuperscriptSize;
        //}
        //protected internal override void CalculateSuperscriptOffset(FontInfo baseFontInfo) {
        //    int result = baseFontInfo.SuperscriptOffset.Y;
        //    int y = baseFontInfo.AscentAndFree - this.AscentAndFree + result;
        //    if (y < 0)
        //        result -= y;
        //    this.SuperscriptOffset = new Point(this.SuperscriptOffset.X, result);
        //}
        //protected internal override void CalculateSubscriptOffset(FontInfo baseFontInfo) {
        //    int result = baseFontInfo.SubscriptOffset.Y;
        //    int maxOffset = baseFontInfo.LineSpacing - this.LineSpacing + this.AscentAndFree - baseFontInfo.AscentAndFree;
        //    if (result > maxOffset)
        //        result = maxOffset;
        //    this.SubscriptOffset = new Point(this.SubscriptOffset.X, result);
        //}

        ////public virtual bool CanDrawCharacter(UnicodeRangeInfo unicodeRangeInfo, Graphics gr, char character) {
        ////    bool result;
        ////    if (!characterDrawingAbilityTable.TryGetValue(character, out result)) {
        ////        result = CalculateCanDrawCharacter(unicodeRangeInfo, gr, character);
        ////        characterDrawingAbilityTable.Add(character, result);
        ////    }
        ////    return result;
        ////}
        ////protected internal virtual bool CalculateCanDrawCharacter(UnicodeRangeInfo unicodeRangeInfo, Graphics gr, char character) {
        ////    UnicodeSubrange unicodeSubRange = unicodeRangeInfo.LookupSubrange(character);
        ////    if (unicodeSubRange != null) {
        ////        Debug.Assert(unicodeSubRange.Bit < 126); 
        ////        BitArray bits = CalculateSupportedUnicodeSubrangeBits(unicodeRangeInfo, gr);
        ////        return bits[unicodeSubRange.Bit];
        ////    }
        ////    else
        ////        return false;
        ////}
        ////protected internal virtual List<FontCharacterRange> GetFontUnicodeRanges(Graphics gr) {
        ////    IntPtr hdc = gr.GetHdc();
        ////    try {
        ////        SafeNativeMethods.SelectObject(hdc, GdiFontHandle);
        ////        uint size = SafeNativeMethods.GetFontUnicodeRanges(hdc, IntPtr.Zero);
        ////        IntPtr glyphSet = Marshal.AllocHGlobal((int)size);
        ////        try {
        ////            SafeNativeMethods.GetFontUnicodeRanges(hdc, glyphSet);
        ////            List<FontCharacterRange> result = new List<FontCharacterRange>();
        ////            int glyphCount = Marshal.ReadInt32(glyphSet, 12);
        ////            for (int i = 0; i < glyphCount; i++) {
        ////                int low = (UInt16)Marshal.ReadInt16(glyphSet, 16 + i * 4);
        ////                int count = (UInt16)Marshal.ReadInt16(glyphSet, 18 + i * 4);
        ////                result.Add(new FontCharacterRange(low, low + count - 1));
        ////            }
        ////            return result;
        ////        }
        ////        finally {
        ////            Marshal.FreeHGlobal(glyphSet);
        ////        }				
        ////    }
        ////    finally {
        ////        gr.ReleaseHdc(hdc);
        ////    }
        ////}
        ////protected internal virtual BitArray CalculateSupportedUnicodeSubrangeBits(UnicodeRangeInfo unicodeRangeInfo, Graphics gr) {
        ////    SafeNativeMethods.FONTSIGNATURE fontSignature = new SafeNativeMethods.FONTSIGNATURE();
        ////    IntPtr hdc = gr.GetHdc();
        ////    try {
        ////        SafeNativeMethods.SelectObject(hdc, GdiFontHandle);
        ////        SafeNativeMethods.FontCharset fontCharset = SafeNativeMethods.GetFontCharsetInfo(hdc, ref fontSignature);
        ////        if (fontCharset == SafeNativeMethods.FontCharset.Default) 
        ////            return new BitArray(128, false);
        ////    }
        ////    finally {
        ////        gr.ReleaseHdc(hdc);
        ////    }
        ////    Int32[] unicodeSubrangeBits = fontSignature.fsUsb;
        ////    Debug.Assert(unicodeSubrangeBits.Length == 4);
        ////    return new BitArray(unicodeSubrangeBits);
        ////}
    }
}
