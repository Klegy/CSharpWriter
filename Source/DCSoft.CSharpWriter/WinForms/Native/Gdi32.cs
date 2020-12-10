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
    //[DCSoft.Common.ObfuscationLevel()]
	public class Gdi32
	{
		public static int ColorToInt(System.Drawing.Color color)
		{
			return (color.B << 16 | color.G << 8 | color.R);
		}

		public static System.Drawing.Color IntToColor(int color)
		{
			int b = (color >> 16) & 0xFF;
			int g = (color >> 8) & 0xFF;
			int r = (color) & 0xFF;
			return System.Drawing.Color.FromArgb(r, g, b);
		}

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern bool ScaleViewportExtEx( int hdc , int XNum , int XDenom , int YNum , int YDenom , int lpSize );

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern bool ScaleWindowExtEx( int hdc , int XNum , int XDenom , int YNum , int YDenom , int lpSize );

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern bool OffsetViewportOrgEx( int hdc , int XOffset , int YOffset , int lpPoint );

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern bool OffsetWindowOrgEx( int hdc , int XOffset , int YOffset , int lpPoint );

		/// <summary>
		/// draws an elliptical arc
		/// </summary>
		/// <param name="hdc">handle to device context</param>
		/// <param name="LeftRect">x-coord of rectangle's upper-left corner</param>
		/// <param name="TopRect">y-coord of rectangle's upper-left corner</param>
		/// <param name="RightRect">x-coord of rectangle's lower-left corner</param>
		/// <param name="BottomRect">y-coord of rectangle's lower-left corner</param>
		/// <param name="XStartArc">x-coord of first radial ending point</param>
		/// <param name="YStartArc">y-coord of first radial ending point</param>
		/// <param name="XEndArc">x-coord of secend radial ending point</param>
		/// <param name="YEndArc">y-coord of secend radial ending point</param>
		/// <returns>ir tha arc is drawn , return nonzero else return zero</returns>
		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern bool Arc( int hdc , int LeftRect ,int TopRect , int RightRect , int BottomRect , int XStartArc , int YStartArc , int XEndArc , int YEndArc );

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern int CombineRgn(int dest, int src1, int src2, int flags);

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern int CreateRectRgnIndirect(ref RECT rect); 

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern int GetClipBox(int hDC, ref RECT rectBox); 

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr CreatePen( int PenStyle , int Width , int Color );

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern int SelectClipRgn(int hDC, int hRgn); 

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr CreateBrushIndirect(ref LOGBRUSH brush); 

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr CreateSolidBrush( int color ); 

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern bool PatBlt(int hDC, int x, int y, int width, int height, uint flags); 

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern int DeleteObject(System.IntPtr hObject);

		
		[System.Runtime.InteropServices.DllImport("gdi32.dll", CharSet=System.Runtime.InteropServices.CharSet.Auto)]
		public static extern IntPtr CreateDC( string strDriver , string strDevice , int Output , int InitData );

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern bool DeleteDC(System.IntPtr hDC);

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr SelectObject(System.IntPtr hDC, System.IntPtr hObject);

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr CreateCompatibleDC(System.IntPtr hDC);

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr CreateCompatibleBitmap(System.IntPtr hDC , int Width , int Height );

		//		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		//		public static extern int BitBlt
		//		(
		//			int hdcDest ,
		//			int XDest ,
		//			int YDest ,
		//			int Width , 
		//			int Height ,
		//			int dcSrc ,
		//			int XSrc , 
		//			int YSrc ,
		//			int Rop
		//			);

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern int GdiFlush();

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern int GetDeviceCaps(System.IntPtr hDC , int index );

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern int GetPixel ( System.IntPtr hDC , int x , int y );

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern int SetPixel ( System.IntPtr hDC , int x , int y , int Color );

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern int SetROP2( System.IntPtr hDC , int DrawMode );

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern int GetROP2 ( System.IntPtr hDC );
		
		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern bool LineTo( System.IntPtr hDC , int X , int Y );

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern bool MoveToEx ( System.IntPtr hDC , int X , int Y , int lpPoint );

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern bool Rectangle ( System.IntPtr hDC , int left , int top , int right , int bottom );

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern bool RoundRect ( System.IntPtr hDC , int left , int top , int right , int bottom , int width , int height );


		[DllImport("gdi32")] //ANSI
		public static extern bool TextOutA(System.IntPtr hdc, int x, int y, string textstring, int charCount);

		[DllImport("gdi32")] //ANSI
		public static extern bool ExtTextOutA( System.IntPtr hdc, int X, int Y, uint fuOptions, int lprc, String lpString, uint cbCount,int lpDx);

		[DllImport("gdi32")]
		public static extern bool Polygon( System.IntPtr hdc, NativePOINT[] lpPoints, int nCount);

		[DllImport("gdi32")]
		public static extern int SetPolyFillMode( System.IntPtr hdc, int iPolyFillMode );

		//Get font information
		[DllImport("gdi32")]
		public static extern bool GetTextMetricsA( System.IntPtr hdc, out TEXTMETRIC lptm);

		//Measure size and width of text
		[DllImport("gdi32")] //ANSI
		public static extern int GetTextExtentPoint32A(System.IntPtr hdc, string str, int len, ref SIZE size);

		[DllImport("gdi32")]
		public static extern int GetStockObject( StockObjectType fnObject );

		[DllImport("gdi32")]
		public static extern int SetBkMode(System.IntPtr hdc, BackGroundModeType iBkMode);

		[DllImport("gdi32")]
		public static extern bool Pie( System.IntPtr hdc, int nLeftRect,int nTopRect, int nRightRect, int nBottomRect, int nXRadial1, int nYRadial1, int nXRadial2, int nYRadial2 );

		[DllImport("gdi32")]
		public static extern int SetTextColor( System.IntPtr hdc, int crColor);

		[DllImport("gdi32")]
		public static extern int SetBkColor( System.IntPtr hdc,int crColor);


		[DllImport("gdi32")]
		public static extern int CreatePatternBrush( System.IntPtr hbmp );

		[DllImport("gdi32")]
		public static extern int CreateBitmap( int nWidth, int nHeight, uint cPlanes, uint cBitsPerPel, byte[] lpvBits);


		[DllImport("gdi32")]
		public static extern int CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

		[DllImport("gdi32")]
		public static extern int CombineRgn( int hrgnDest, int hrgnSrc1, int hrgnSrc2, RegionCombineMode fnCombineMode);

		[DllImport("gdi32")]
		public static extern int ExtSelectClipRgn( System.IntPtr hdc, int hrgn, RegionCombineMode fnMode );

		[DllImport("gdi32")]
		public static extern int GetClipRgn( System.IntPtr hdc, int hrgn );

		[DllImport("gdi32")]
		public static extern int OffsetRgn( System.IntPtr hrgn, int nXOffset, int nYOffset );

		[DllImport("gdi32")]
		public static extern int GetObject( System.IntPtr hgdiobj, int cbBuffer, out LOGFONT lpvObject );

		[DllImport("gdi32")]
		public static extern int CreateFontIndirectA(ref LOGFONT lf);


		[DllImport("gdi32")]
		public static extern int CreateDIBSection(int hdc, [In,MarshalAs(UnmanagedType.LPStruct)] BITMAPINFO pbmi, DibColorTableType iUsage, out int ppvBits, int hSection, uint dwOffset);

		[DllImport("gdi32")]
		public static extern int BitBlt (
			IntPtr hdcDest, int x, int y, int nWidth, int nHeight, 
			IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

		[DllImport("gdi32")]
		public static extern int SetDIBitsToDevice(
			IntPtr hdc,                 // handle to DC
			int XDest,               // x-coord of destination upper-left corner
			int YDest,               // y-coord of destination upper-left corner
			uint dwWidth,           // source rectangle width
			uint dwHeight,          // source rectangle height
			int XSrc,                // x-coord of source lower-left corner
			int YSrc,                // y-coord of source lower-left corner
			uint uStartScan,         // first scan line in array
			uint cScanLines,         // number of scan lines
			ref byte lpvBits,     // array of DIB bits
			BITMAPINFO lpbmi, // bitmap information
			uint fuColorUse          // RGB or palette indexes
			);

		[DllImport("kernel32")]
		public static extern int GlobalLock( int hMem);

		[DllImport("kernel32")]
		public static extern bool GlobalUnlock(int hMem);

		[DllImport("gdi32")]
		public static extern int SaveDC( IntPtr hdc );

		[DllImport("gdi32")]
		public static extern bool RestoreDC( IntPtr hdc, int nSavedDC );

		[DllImport("gdi32")]
		public static extern int ExtCreateRegion( int lpXform, uint nCount, ref byte lpRgnData);

		[DllImport("gdi32")]
		public static extern uint GetRegionData( IntPtr hRgn, uint dwCount, ref byte lpRgnData);

//		[DllImport("gdi32")]
//		public static extern int SetBkColor( int hdc , int color );

		[DllImport("gdi32")]
		public static extern int GetBkColor( IntPtr hdc );

		/// <summary>
		/// 获得屏幕上指定点的象素
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public static int GetScreenPixel ( int x , int y )
		{
			IntPtr hDC = User32.GetDC( IntPtr.Zero );
			int iReturn = GetPixel( hDC , x , y );
			User32.ReleaseDC( IntPtr.Zero , hDC );
			return iReturn ;
		}
		/// <summary>
		/// 获得屏幕上指定点的象素
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		public static int GetScreenPixel( System.Drawing.Point p )
		{
			IntPtr hDC = User32.GetDC( IntPtr.Zero );
			int iReturn = GetPixel( hDC , p.X  , p.Y  );
			User32.ReleaseDC( IntPtr.Zero , hDC );
			return iReturn ;
		}
	}//public class Gdi32
}