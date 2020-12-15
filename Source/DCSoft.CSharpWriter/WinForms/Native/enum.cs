/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace DCSoft.WinForms.Native
{
	public enum TrackMouseEventFlags : uint
	{
		TME_LEAVE = 2
	}

	public enum CursorName : uint
	{
		IDC_ARROW		= 32512U,
		IDC_IBEAM       = 32513U,
		IDC_WAIT        = 32514U,
		IDC_CROSS       = 32515U,
		IDC_UPARROW     = 32516U,
		IDC_SIZE        = 32640U,
		IDC_ICON        = 32641U,
		IDC_SIZENWSE    = 32642U,
		IDC_SIZENESW    = 32643U,
		IDC_SIZEWE      = 32644U,
		IDC_SIZENS      = 32645U,
		IDC_SIZEALL     = 32646U,
		IDC_NO          = 32648U,
		IDC_HAND        = 32649U,
		IDC_APPSTARTING = 32650U,
		IDC_HELP        = 32651U
	}

	public enum DibColorTableType : uint
	{
		DIB_RGB_COLORS = 0, /* color table in RGBs */
		DIB_PAL_COLORS = 1 /* color table in palette indices */
	}

    public enum BitMapInfoCompressionType : uint
	{
		BI_RGB = 0,
		BI_RLE8 = 1,
		BI_RLE4 = 2,
		BI_BITFIELDS = 3,
		BI_JPEG = 4,
		BI_PNG = 5
	}

	public enum SetWindowsPosPosition : int
	{
		HWND_TOP =0,
		HWND_BOTTOM = 1,
		HWND_TOPMOST = -1,
		HWND_NOTOPMOST = -2
	}


	public enum SetWindowLongType
	{
		GWL_STYLE = -16,
		GWL_EXSTYLE = -20
	}

    public enum RedrawWindowFlags
	{
		RDW_INVALIDATE = 0x1,
		RDW_FRAME = 0x400
	}


    public enum FontQuality : byte
	{
		NONANTIALIASED_QUALITY = 3,
		ANTIALIASED_QUALITY = 4,
		CLEARTYPE_QUALITY =5,
		CLEARTYPE_NATURAL_QUALITY = 6
	}


	public enum RegionCombineMode
	{
		RGN_AND = 1,
		RGN_OR = 2,
		RGN_XOR = 3,
		RGN_DIFF = 4,
		RGN_COPY = 5,
		RGN_MIN = RGN_AND,
		RGN_MAX = RGN_COPY
	}

	public enum SystemParametersAction : uint
	{
		SPI_GETBORDER = 0x5,
		SPI_GETNONCLIENTMETRICS=0x29,
		SPI_GETWORKAREA=0x30,
		SPI_GETGRADIENTCAPTIONS=0x1008
	}

    public enum StockObjectType
	{
		WHITE_BRUSH         = 0 , 
		LTGRAY_BRUSH        = 1 ,
		GRAY_BRUSH          = 2 ,
		DKGRAY_BRUSH        = 3 ,
		BLACK_BRUSH         = 4 ,
		NULL_BRUSH          = 5 ,
		HOLLOW_BRUSH        = 5 ,
		WHITE_PEN           = 6 ,
		BLACK_PEN           = 7 ,
		NULL_PEN            = 8 ,
		OEM_FIXED_FONT      = 10,
		ANSI_FIXED_FONT     = 11,
		ANSI_VAR_FONT       = 12,
		SYSTEM_FONT         = 13,
		DEVICE_DEFAULT_FONT = 14,
		DEFAULT_PALETTE     = 15,
		SYSTEM_FIXED_FONT   = 16,
		DEFAULT_GUI_FONT    = 17,
		DC_BRUSH            = 18,
		DC_PEN              = 19
    }


    public enum BackGroundModeType
	{
		Transparent = 1 ,
		Opaque = 2 
	}

    public enum PeekMessageFlags
	{
		PM_NOREMOVE		= 0,
		PM_REMOVE		= 1,
		PM_NOYIELD		= 2
	}
	/// <summary>
	/// For ShowWindow API
	/// </summary>
	public enum SetWindowPosFlags : uint
	{
		SWP_NOSIZE          = 0x0001,
		SWP_NOMOVE          = 0x0002,
		SWP_NOZORDER        = 0x0004,
		SWP_NOREDRAW        = 0x0008,
		SWP_NOACTIVATE      = 0x0010,
		SWP_FRAMECHANGED    = 0x0020,
		SWP_SHOWWINDOW      = 0x0040,
		SWP_HIDEWINDOW      = 0x0080,
		SWP_NOCOPYBITS      = 0x0100,
		SWP_NOOWNERZORDER   = 0x0200, 
		SWP_NOSENDCHANGING  = 0x0400,
		SWP_DRAWFRAME       = 0x0020,
		SWP_NOREPOSITION    = 0x0200,
		SWP_DEFERERASE      = 0x2000,
		SWP_ASYNCWINDOWPOS  = 0x4000
	}

	public enum ShowWindowStyles : short
	{
		SW_HIDE             = 0,
		SW_SHOWNORMAL       = 1,
		SW_NORMAL           = 1,
		SW_SHOWMINIMIZED    = 2,
		SW_SHOWMAXIMIZED    = 3,
		SW_MAXIMIZE         = 3,
		SW_SHOWNOACTIVATE   = 4,
		SW_SHOW             = 5,
		SW_MINIMIZE         = 6,
		SW_SHOWMINNOACTIVE  = 7,
		SW_SHOWNA           = 8,
		SW_RESTORE          = 9,
		SW_SHOWDEFAULT      = 10,
		SW_FORCEMINIMIZE    = 11,
		SW_MAX              = 11
	}

	/// <summary>
	/// Class Style 
	/// </summary>
	public enum ClassStyle
	{
		CS_VREDRAW         = 0x0001     ,         
		CS_HREDRAW         = 0x0002     ,
		CS_DBLCLKS         = 0x0008     ,
		CS_OWNDC           = 0x0020     ,
		CS_CLASSDC         = 0x0040     ,
		CS_PARENTDC        = 0x0080     ,
		CS_NOCLOSE         = 0x0200     ,
		CS_SAVEBITS        = 0x0800     ,
		CS_BYTEALIGNCLIENT = 0x1000     ,
		CS_BYTEALIGNWINDOW = 0x2000     ,
		CS_GLOBALCLASS     = 0x4000     ,
		CS_IME             = 0x00010000 
	}

    public enum WindowStyles : uint
	{
		WS_OVERLAPPED       = 0x00000000,
		WS_POPUP            = 0x80000000,
		WS_CHILD            = 0x40000000,
		WS_MINIMIZE         = 0x20000000,
		WS_VISIBLE          = 0x10000000,
		WS_DISABLED         = 0x08000000,
		WS_CLIPSIBLINGS     = 0x04000000,
		WS_CLIPCHILDREN     = 0x02000000,
		WS_MAXIMIZE         = 0x01000000,
		WS_CAPTION          = 0x00C00000,
		WS_BORDER           = 0x00800000,
		WS_DLGFRAME         = 0x00400000,
		WS_VSCROLL          = 0x00200000,
		WS_HSCROLL          = 0x00100000,
		WS_SYSMENU          = 0x00080000,
		WS_THICKFRAME       = 0x00040000,
		WS_GROUP            = 0x00020000,
		WS_TABSTOP          = 0x00010000,
		WS_MINIMIZEBOX      = 0x00020000,
		WS_MAXIMIZEBOX      = 0x00010000,
		WS_TILED            = 0x00000000,
		WS_ICONIC           = 0x20000000,
		WS_SIZEBOX          = 0x00040000,
		WS_POPUPWINDOW      = 0x80880000,
		WS_OVERLAPPEDWINDOW = 0x00CF0000,
		WS_TILEDWINDOW      = 0x00CF0000,
		WS_CHILDWINDOW      = 0x40000000
	}

    public enum WindowExStyles
	{
		WS_EX_DLGMODALFRAME     = 0x00000001,
		WS_EX_NOPARENTNOTIFY    = 0x00000004,
		WS_EX_TOPMOST           = 0x00000008,
		WS_EX_ACCEPTFILES       = 0x00000010,
		WS_EX_TRANSPARENT       = 0x00000020,
		WS_EX_MDICHILD          = 0x00000040,
		WS_EX_TOOLWINDOW        = 0x00000080,
		WS_EX_WINDOWEDGE        = 0x00000100,
		WS_EX_CLIENTEDGE        = 0x00000200,
		WS_EX_CONTEXTHELP       = 0x00000400,
		WS_EX_RIGHT             = 0x00001000,
		WS_EX_LEFT              = 0x00000000,
		WS_EX_RTLREADING        = 0x00002000,
		WS_EX_LTRREADING        = 0x00000000,
		WS_EX_LEFTSCROLLBAR     = 0x00004000,
		WS_EX_RIGHTSCROLLBAR    = 0x00000000,
		WS_EX_CONTROLPARENT     = 0x00010000,
		WS_EX_STATICEDGE        = 0x00020000,
		WS_EX_APPWINDOW         = 0x00040000,
		WS_EX_OVERLAPPEDWINDOW  = 0x00000300,
		WS_EX_PALETTEWINDOW     = 0x00000188,
		WS_EX_LAYERED			= 0x00080000
	}

    public enum TrackerEventFlags : uint
	{
		TME_HOVER	= 0x00000001,
		TME_LEAVE	= 0x00000002,
		TME_QUERY	= 0x40000000,
		TME_CANCEL	= 0x80000000
	}

    public enum MouseActivateFlags
	{
		MA_ACTIVATE			= 1,
		MA_ACTIVATEANDEAT   = 2,
		MA_NOACTIVATE       = 3,
		MA_NOACTIVATEANDEAT = 4
	}

    public enum DialogCodes
	{
		DLGC_WANTARROWS			= 0x0001,
		DLGC_WANTTAB			= 0x0002,
		DLGC_WANTALLKEYS		= 0x0004,
		DLGC_WANTMESSAGE		= 0x0004,
		DLGC_HASSETSEL			= 0x0008,
		DLGC_DEFPUSHBUTTON		= 0x0010,
		DLGC_UNDEFPUSHBUTTON	= 0x0020,
		DLGC_RADIOBUTTON		= 0x0040,
		DLGC_WANTCHARS			= 0x0080,
		DLGC_STATIC				= 0x0100,
		DLGC_BUTTON				= 0x2000
	}


    public enum UpdateLayeredWindowsFlags
	{
		ULW_COLORKEY = 0x00000001,
		ULW_ALPHA    = 0x00000002,
		ULW_OPAQUE   = 0x00000004
	}

    public enum AlphaFlags : byte
	{
		AC_SRC_OVER  = 0x00,
		AC_SRC_ALPHA = 0x01
	}

	/// <summary>
	/// 图形设备上下文字节运算掩码
	/// </summary>
	public enum DCRasterOperations
	{
		R2_BLACK            = 1   , /*  0       */
		R2_NOTMERGEPEN      = 2   , /* DPon     */
		R2_MASKNOTPEN       = 3   , /* DPna     */
		R2_NOTCOPYPEN       = 4   , /* PN       */
		R2_MASKPENNOT       = 5   , /* PDna     */
		R2_NOT              = 6   , /* Dn       */
		R2_XORPEN           = 7   , /* DPx      */
		R2_NOTMASKPEN       = 8   , /* DPan     */
		R2_MASKPEN          = 9   , /* DPa      */
		R2_NOTXORPEN        = 10  , /* DPxn     */
		R2_NOP              = 11  , /* D        */
		R2_MERGENOTPEN      = 12  , /* DPno     */
		R2_COPYPEN          = 13  , /* P        */
		R2_MERGEPENNOT      = 14  , /* PDno     */
		R2_MERGEPEN         = 15  , /* DPo      */
		R2_WHITE            = 16  , /*  1       */
		R2_LAST             = 16
	}

    public enum RasterOperations : uint
	{
		SRCCOPY = 0x00CC0020, /* dest = source */
		SRCPAINT = 0x00EE0086, /* dest = source OR dest */
		SRCAND =0x008800C6, /* dest = source AND dest */
		SRCINVERT = 0x00660046, /* dest = source XOR dest */
		SRCERASE =0x00440328, /* dest = source AND (NOT dest ) */
		NOTSRCCOPY =0x00330008, /* dest = (NOT source) */
		NOTSRCERASE =0x001100A6, /* dest = (NOT src) AND (NOT dest) */
		MERGECOPY =0x00C000CA, /* dest = (source AND pattern) */
		MERGEPAINT =0x00BB0226, /* dest = (NOT source) OR dest */
		PATCOPY =0x00F00021, /* dest = pattern */
		PATPAINT =0x00FB0A09, /* dest = DPSnoo */
		PATINVERT =0x005A0049, /* dest = pattern XOR dest */
		DSTINVERT =0x00550009 ,/* dest = (NOT dest) */
		BLACKNESS =0x00000042, /* dest = BLACK */
		WHITENESS =0x00FF0062 /* dest = WHITE */
	}

    public enum BrushStyles
	{
		BS_SOLID			= 0,
		BS_NULL             = 1,
		BS_HOLLOW           = 1,
		BS_HATCHED          = 2,
		BS_PATTERN          = 3,
		BS_INDEXED          = 4,
		BS_DIBPATTERN       = 5,
		BS_DIBPATTERNPT     = 6,
		BS_PATTERN8X8       = 7,
		BS_DIBPATTERN8X8    = 8,
		BS_MONOPATTERN      = 9
	}

    public enum HatchStyles
	{
		HS_HORIZONTAL       = 0,
		HS_VERTICAL         = 1,
		HS_FDIAGONAL        = 2,
		HS_BDIAGONAL        = 3,
		HS_CROSS            = 4,
		HS_DIAGCROSS        = 5
	}

    public enum CombineFlags
	{
		RGN_AND		= 1,
		RGN_OR		= 2,
		RGN_XOR		= 3,
		RGN_DIFF	= 4,
		RGN_COPY	= 5
	}

    public enum HitTest
	{
		HTERROR			= -2,
		HTTRANSPARENT   = -1,
		HTNOWHERE		= 0,
		HTCLIENT		= 1,
		HTCAPTION		= 2,
		HTSYSMENU		= 3,
		HTGROWBOX		= 4,
		HTSIZE			= 4,
		HTMENU			= 5,
		HTHSCROLL		= 6,
		HTVSCROLL		= 7,
		HTMINBUTTON		= 8,
		HTMAXBUTTON		= 9,
		HTLEFT			= 10,
		HTRIGHT			= 11,
		HTTOP			= 12,
		HTTOPLEFT		= 13,
		HTTOPRIGHT		= 14,
		HTBOTTOM		= 15,
		HTBOTTOMLEFT	= 16,
		HTBOTTOMRIGHT	= 17,
		HTBORDER		= 18,
		HTREDUCE		= 8,
		HTZOOM			= 9 ,
		HTSIZEFIRST		= 10,
		HTSIZELAST		= 17,
		HTOBJECT		= 19,
		HTCLOSE			= 20,
		HTHELP			= 21
	}

    public enum AnimateFlags
	{
		AW_HOR_POSITIVE = 0x00000001,
		AW_HOR_NEGATIVE = 0x00000002,
		AW_VER_POSITIVE = 0x00000004,
		AW_VER_NEGATIVE = 0x00000008,
		AW_CENTER		= 0x00000010,
		AW_HIDE			= 0x00010000,
		AW_ACTIVATE		= 0x00020000,
		AW_SLIDE		= 0x00040000,
		AW_BLEND		= 0x00080000
	}

    public enum GetWindowLongFlags
	{
		GWL_WNDPROC         = -4,
		GWL_HINSTANCE       = -6,
		GWL_HWNDPARENT      = -8,
		GWL_STYLE           = -16,
		GWL_EXSTYLE         = -20,
		GWL_USERDATA        = -21,
		GWL_ID              = -12
	}
    

	/// <summary>
	/// 获得系统信息的编号,本变量传送给Win32API GetSystemMetrics
	/// </summary>

    public enum SystemMetricsConst : int
	{
		SM_CXSCREEN             = 0 , 
		SM_CYSCREEN             = 1 ,
		SM_CXVSCROLL            = 2 ,
		SM_CYHSCROLL            = 3 ,
		SM_CYCAPTION            = 4 ,
		SM_CXBORDER             = 5 ,
		SM_CYBORDER             = 6 ,
		SM_CXDLGFRAME           = 7 ,
		SM_CYDLGFRAME           = 8 ,
		SM_CYVTHUMB             = 9 ,
		SM_CXHTHUMB             = 10,
		SM_CXICON               = 11,
		SM_CYICON               = 12,
		SM_CXCURSOR             = 13,
		SM_CYCURSOR             = 14,
		SM_CYMENU               = 15,
		SM_CXFULLSCREEN         = 16,
		SM_CYFULLSCREEN         = 17,
		SM_CYKANJIWINDOW        = 18,
		SM_MOUSEPRESENT         = 19,
		SM_CYVSCROLL            = 20,
		SM_CXHSCROLL            = 21,
		SM_DEBUG                = 22,
		SM_SWAPBUTTON           = 23,
		SM_RESERVED1            = 24,
		SM_RESERVED2            = 25,
		SM_RESERVED3            = 26,
		SM_RESERVED4            = 27,
		SM_CXMIN                = 28,
		SM_CYMIN                = 29,
		SM_CXSIZE               = 30,
		SM_CYSIZE               = 31,
		SM_CXFRAME              = 32,
		SM_CYFRAME              = 33,
		SM_CXMINTRACK           = 34,
		SM_CYMINTRACK           = 35,
		SM_CXDOUBLECLK          = 36,
		SM_CYDOUBLECLK          = 37,
		SM_CXICONSPACING        = 38,
		SM_CYICONSPACING        = 39,
		SM_MENUDROPALIGNMENT    = 40,
		SM_PENWINDOWS           = 41,
		SM_DBCSENABLED          = 42,
		SM_CMOUSEBUTTONS        = 43,
		SM_SECURE               = 44,
		SM_CXEDGE               = 45,
		SM_CYEDGE               = 46,
		SM_CXMINSPACING         = 47,
		SM_CYMINSPACING         = 48,
		SM_CXSMICON             = 49,
		SM_CYSMICON             = 50,
		SM_CYSMCAPTION          = 51,
		SM_CXSMSIZE             = 52,
		SM_CYSMSIZE             = 53,
		SM_CXMENUSIZE           = 54,
		SM_CYMENUSIZE           = 55,
		SM_ARRANGE              = 56,
		SM_CXMINIMIZED          = 57,
		SM_CYMINIMIZED          = 58,
		SM_CXMAXTRACK           = 59,
		SM_CYMAXTRACK           = 60,
		SM_CXMAXIMIZED          = 61,
		SM_CYMAXIMIZED          = 62,
		SM_NETWORK              = 63,
		SM_CLEANBOOT            = 67,
		SM_CXDRAG               = 68,
		SM_CYDRAG               = 69,
		SM_SHOWSOUNDS           = 70,
		SM_CXMENUCHECK          = 71,   /* Use instead of GetMenuCheckMarkDimensions()! */
		SM_CYMENUCHECK          = 72,
		SM_SLOWMACHINE          = 73,
		SM_MIDEASTENABLED       = 74,
		SM_MOUSEWHEELPRESENT    = 75,
		SM_XVIRTUALSCREEN       = 76,
		SM_YVIRTUALSCREEN       = 77,
		SM_CXVIRTUALSCREEN      = 78,
		SM_CYVIRTUALSCREEN      = 79,
		SM_CMONITORS            = 80,
		SM_SAMEDISPLAYFORMAT    = 81,
		//SM_CMETRICS             = 76, // (WINVER < 0x0500) && (!defined(_WIN32_WINNT) || (_WIN32_WINNT < 0x0400))
		SM_CMETRICS             = 83
	}// public enum SystemMetricsConst : int

	public enum SPIWinINIFlags
	{
		SPIF_UPDATEINIFILE		= 0x0001,
		SPIF_SENDWININICHANGE	= 0x0002,
		SPIF_SENDCHANGE			= 0x0002
	}
}//namespace DCSoft.WinForms.Native