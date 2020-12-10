/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using System.Runtime.InteropServices;
namespace DCSoft.WinForms.Native
{
	public delegate int WNDPROC( IntPtr hwnd, int msg, int wParam, int lParam);
	public delegate void TimerProc(IntPtr hwnd, uint uMsg, uint idEvent, uint dwTime);

	[StructLayout(LayoutKind.Sequential)]
	public struct TRACKMOUSEEVENT
	{
		public int cbSize;
		public TrackMouseEventFlags dwFlags;
		public IntPtr hwndTrack;
		public uint dwHoverTime;
	}


	[StructLayout(LayoutKind.Sequential,CharSet=CharSet.Ansi)]
	public struct WNDCLASS
	{
		public ClassStyle style;
		public WNDPROC lpfnWndProc;
		public int cbClsExtra;
		public int cbWndExtra;
		public IntPtr hInstance;
		public IntPtr hIcon;
		public IntPtr hCursor;
		public IntPtr hbrBackground;
		private IntPtr lpszMenuName__;
		private IntPtr lpszClassName__;

		// Hack - Portable.NET doesn't support structure marshaling yet.
		public string lpszMenuName
		{
			set
			{
				if(value == null)
				{
					lpszMenuName__ = IntPtr.Zero;
				}
				else
				{
					lpszMenuName__ =
						Marshal.StringToHGlobalAnsi(value);
				}
			}
		}
		public string lpszClassName
		{
			set
			{
				if(value == null)
				{
					lpszClassName__ = IntPtr.Zero;
				}
				else
				{
					lpszClassName__ =
						Marshal.StringToHGlobalAnsi(value);
				}
			}
		}
	}



	[StructLayout(LayoutKind.Sequential)]
	public class BITMAPINFO
	{
		public int biSize;
		public int biWidth;
		public int biHeight;
		public short biPlanes;
		public short biBitCount;
		public BitMapInfoCompressionType biCompression;
		public int biSizeImage = 0;
		public int biXPelsPerMeter = 0;
		public int biYPelsPerMeter = 0;
		public int biClrUsed = 0;
		public int biClrImportant = 0;
		[MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=1024)]
		public byte[] bmiColors;
	};


	[StructLayout(LayoutKind.Sequential)]
	public struct LOGFONT
	{
		public int lfHeight;
		public int lfWidth;
		public int lfEscapement;
		public int lfOrientation;
		public int lfWeight;
		public byte lfItalic;
		public byte lfUnderline;
		public byte lfStrikeout;
		public byte lfCharSet;
		public byte lfOutPrecision;
		public byte lfClipPrecision;
		public FontQuality lfQuality;
		public byte lfPitchAndFamily;

		// Hack - Portable.NET doesn't support structure marshaling yet.
		private byte lfFaceName_0;
		private byte lfFaceName_1;
		private byte lfFaceName_2;
		private byte lfFaceName_3;
		private byte lfFaceName_4;
		private byte lfFaceName_5;
		private byte lfFaceName_6;
		private byte lfFaceName_7;
		private byte lfFaceName_8;
		private byte lfFaceName_9;
		private byte lfFaceName_10;
		private byte lfFaceName_11;
		private byte lfFaceName_12;
		private byte lfFaceName_13;
		private byte lfFaceName_14;
		private byte lfFaceName_15;
		private byte lfFaceName_16;
		private byte lfFaceName_17;
		private byte lfFaceName_18;
		private byte lfFaceName_19;
		private byte lfFaceName_20;
		private byte lfFaceName_21;
		private byte lfFaceName_22;
		private byte lfFaceName_23;
		private byte lfFaceName_24;
		private byte lfFaceName_25;
		private byte lfFaceName_26;
		private byte lfFaceName_27;
		private byte lfFaceName_28;
		private byte lfFaceName_29;
		private byte lfFaceName_30;
		private byte lfFaceName_31;

		private static void SetFaceName(out byte dest, string value, int index)
		{
			if(value == null || index >= value.Length)
			{
				dest = 0;
			}
			else
			{
				dest = (byte)(value[index]);
			}
		}

		public string lfFaceName
		{
			set
			{
				SetFaceName(out lfFaceName_0, value, 0);
				SetFaceName(out lfFaceName_1, value, 1);
				SetFaceName(out lfFaceName_2, value, 2);
				SetFaceName(out lfFaceName_3, value, 3);
				SetFaceName(out lfFaceName_4, value, 4);
				SetFaceName(out lfFaceName_5, value, 5);
				SetFaceName(out lfFaceName_6, value, 6);
				SetFaceName(out lfFaceName_7, value, 7);
				SetFaceName(out lfFaceName_8, value, 8);
				SetFaceName(out lfFaceName_9, value, 9);
				SetFaceName(out lfFaceName_10, value, 10);
				SetFaceName(out lfFaceName_11, value, 11);
				SetFaceName(out lfFaceName_12, value, 12);
				SetFaceName(out lfFaceName_13, value, 13);
				SetFaceName(out lfFaceName_14, value, 14);
				SetFaceName(out lfFaceName_15, value, 15);
				SetFaceName(out lfFaceName_16, value, 16);
				SetFaceName(out lfFaceName_17, value, 17);
				SetFaceName(out lfFaceName_18, value, 18);
				SetFaceName(out lfFaceName_19, value, 19);
				SetFaceName(out lfFaceName_20, value, 20);
				SetFaceName(out lfFaceName_21, value, 21);
				SetFaceName(out lfFaceName_22, value, 22);
				SetFaceName(out lfFaceName_23, value, 23);
				SetFaceName(out lfFaceName_24, value, 24);
				SetFaceName(out lfFaceName_25, value, 25);
				SetFaceName(out lfFaceName_26, value, 26);
				SetFaceName(out lfFaceName_27, value, 27);
				SetFaceName(out lfFaceName_28, value, 28);
				SetFaceName(out lfFaceName_29, value, 29);
				SetFaceName(out lfFaceName_30, value, 30);
				SetFaceName(out lfFaceName_31, value, 31);
			}
		}
	}


	[StructLayout(LayoutKind.Sequential)]
	public struct NONCLIENTMETRICS
	{
		public uint cbSize;
		public int iBorderWidth;
		public int iScrollWidth;
		public int iScrollHeight;
		public int iCaptionWidth;
		public int iCaptionHeight;
		public LOGFONT lfCaptionFont;
		public int iSmCaptionWidth;
		public int iSmCaptionHeight;
		public LOGFONT lfSmCaptionFont;
		public int iMenuWidth;
		public int iMenuHeight;
		public LOGFONT lfMenuFont;
		public LOGFONT lfStatusFont;
		public LOGFONT lfMessageFont;
	}


	[StructLayout(LayoutKind.Sequential,CharSet=CharSet.Ansi)]
	public struct TEXTMETRIC
	{
		public int tmHeight;
		public int tmAscent;
		public int tmDescent;
		public int tmInternalLeading;
		public int tmExternalLeading;
		public int tmAveCharWidth;
		public int tmMaxCharWidth;
		public int tmWeight;
		public int tmOverhang;
		public int tmDigitizedAspectX;
		public int tmDigitizedAspectY;
		public char tmFirstChar;
		public char tmLastChar;
		public char tmDefaultChar;
		public char tmBreakChar;
		public byte tmItalic;
		public byte tmUnderlined;
		public byte tmStruckOut;
		public byte tmPitchAndFamily;
		public byte tmCharSet;
	}


	[StructLayout(LayoutKind.Sequential)]
	public struct PAINTSTRUCT
	{
		public int hdc;
		public int fErase;
		public System.Drawing.Rectangle rcPaint;
		public int fRestore;
		public int fIncUpdate;
		public int Reserved1;
		public int Reserved2;
		public int Reserved3;
		public int Reserved4;
		public int Reserved5;
		public int Reserved6;
		public int Reserved7;
		public int Reserved8;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct RECT
	{
		public int left;
		public int top;
		public int right;
		public int bottom;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct NativePOINT
	{
		public int x;
		public int y;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct SIZE
	{
		public int cx;
		public int cy;
	}

	[StructLayout(LayoutKind.Sequential, Pack=1)]
	public struct BLENDFUNCTION
	{
		public byte BlendOp;
		public byte BlendFlags;
		public byte SourceConstantAlpha;
		public byte AlphaFormat;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct TRACKMOUSEEVENTS
	{
		public uint cbSize;
		public uint dwFlags;
		public int hWnd;
		public uint dwHoverTime;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct LOGBRUSH
	{
		public uint lbStyle; 
		public uint lbColor; 
		public uint lbHatch; 
	}
}