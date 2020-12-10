/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using System.Runtime.InteropServices ;

namespace DCSoft.WinForms.Native
{
	/// <summary>
	/// Windows的图形设备上下文对象
	/// </summary>
    /// <remarks>
    /// 本类型是Windows图形设备上下文的托管对象
    /// 编写 袁永福
    /// </remarks>
	public class DeviceContexts : DeviceContextBase
	{
		#region 静态函数群 ************************************************************************

		/// <summary>
		/// 根据一个窗体句柄创建对象
		/// </summary>
		/// <param name="hdc">窗体句柄</param>
		/// <returns>新创建的对象</returns>
		public static DeviceContexts FromHwnd( IntPtr hwnd )
		{
			DeviceContexts dc = new DeviceContexts();
			dc.InitFromHWnd( hwnd );
			return dc ;
		}
		/// <summary>
		/// 为指定的设备上下文创建一个兼容的内存设备上下文对象
		/// </summary>
		/// <param name="hdc">指定的设备上下文</param>
		/// <returns>新的设备上下文对象</returns>
		public static DeviceContexts CreateCompatibleDC( IntPtr hdc )
		{
			DeviceContexts dc = new DeviceContexts();
			dc.InitCompatibleDC( hdc );
			return dc ;
		}
		/// <summary>
		/// 创建一个屏幕设备上下文句柄对象
		/// </summary>
		/// <returns>新创建的对象</returns>
		public static DeviceContexts CreateDisplayDC()
		{
			return new DeviceContexts("DISPLAY");
		}

		private static DeviceContexts myScreen = null;
		/// <summary>
		/// 计算机屏幕设备上下文对象
		/// </summary>
		public static DeviceContexts Screen
		{
			get
			{
				if( myScreen == null )
					myScreen = new DeviceContexts( "DISPLAY" );
				return myScreen ;
			}
		}
		#endregion
		
		#region 构造函数群 ************************************************************************

		/// <summary>
		/// 无作为的初始化对象
		/// </summary>
		private DeviceContexts()
		{}
		/// <summary>
		/// 使用制定GDI+绘图对象初始化对象
		/// </summary>
		/// <param name="g">GDI+绘图对象</param>
		public DeviceContexts( System.Drawing.Graphics g )
		{
			if( g != null)
			{
				base.InitFromGraphics( g );
			}
		}
		/// <summary>
		/// 使用指定的设备上下文本句柄初始化对象
		/// </summary>
		/// <param name="hdc">设备上下文句柄</param>
		public DeviceContexts( IntPtr hdc )
		{
			base.InitFromHDC( hdc );
		}
		/// <summary>
		/// 使用指定设备名称初始化对象
		/// </summary>
		/// <param name="strDriver">设备名称</param>
		public DeviceContexts( string strDriver )
		{
			base.InitFromDriverName( strDriver );
		}
		
		#endregion

		/// <summary>
		/// 象素计算类型
		/// </summary>
        /// <remarks>
        /// 本属性内部调用了Win32API函数GetROP2和SetROP2.
        /// </remarks>
		public DCRasterOperations ROP2
		{
			get
            {
                return ( DCRasterOperations )GetROP2( this.intHDC );
            }
			set
            {
                SetROP2( intHDC , ( int ) value );
            }
		}

		/// <summary>
		/// 设备上下文背景颜色
		/// </summary>
        /// <remarks>
        /// 本属性内部调用了Win32API函数GetBkColor和SetBkColor.
        /// </remarks>
		public System.Drawing.Color BkColor
		{
			get
            {
                return System.Drawing.Color.FromArgb( GetBkColor( intHDC ));
            }
			set
            {
                SetBkColor( intHDC , value.ToArgb()) ;
            }
		}

		/// <summary>
		/// 创建图形绘制对象
		/// </summary>
		/// <returns>新的图形绘制对象</returns>
		public System.Drawing.Graphics CreateGraphics()
		{
			return System.Drawing.Graphics.FromHdc( intHDC );
		}

		/// <summary>
		/// 获得设备中指定位置的象素值
		/// </summary>
		/// <param name="p">指定位置的坐标</param>
		/// <returns>获得的颜色值</returns>
        /// <remarks>
        /// 本方法内部调用了Win32API函数GetPixel.
        /// </remarks>
		public System.Drawing.Color GetPixel( System.Drawing.Point p )
		{
			return GetPixel( p.X , p.Y );
		}
		/// <summary>
		/// 获得设备中指定位置的象素值
		/// </summary>
		/// <param name="x">横向坐标值</param>
		/// <param name="y">纵向坐标值</param>
		/// <returns>获得的颜色值</returns>
        /// <remarks>
        /// 本方法内部调用了Win32API函数GetPixel.
        /// </remarks>
        public System.Drawing.Color GetPixel(int x, int y)
		{
			int vValue = GetPixel( intHDC , x , y );
			return System.Drawing.Color.FromArgb( 255 ,vValue & 0xff  , ( vValue & 0xff00 ) >> 8 ,  vValue >> 16 );
		}
		/// <summary>
		/// 设置设备中指定位置的象素值
		/// </summary>
		/// <param name="p">指定位置的坐标</param>
		/// <param name="vColor">新的象素值</param>
        /// <remarks>
        /// 本方法内部调用了Win32API函数SetPixel.
        /// </remarks>
        public void SetPixel(System.Drawing.Point p, System.Drawing.Color vColor)
		{
			SetPixel( p.X , p.Y , vColor );
		}
		/// <summary>
		/// 设置设备中指定位置的象素值
		/// </summary>
		/// <param name="x">横向坐标值</param>
		/// <param name="y">纵向坐标值</param>
		/// <param name="vColor">新设置的颜色值</param>
        /// <remarks>
        /// 本方法内部调用了Win32API函数SetPixel.
        /// </remarks>
        public void SetPixel(int x, int y, System.Drawing.Color vColor)
		{
			int vValue = vColor.R  + vColor.G * 0x100 + vColor.B * 0x10000 ;
			SetPixel( intHDC , x , y , vValue );
		}

		/// <summary>
		/// 绘制可逆矩形边框
		/// </summary>
		/// <param name="Left">区域左边位置</param>
		/// <param name="Top">区域顶端位置</param>
		/// <param name="Width">宽度</param>
		/// <param name="Height">高度</param>
		public void DrawFocusRect( int Left , int Top , int Width , int Height )
		{
			if( intHDC.ToInt32() != 0 )
			{
				RECT rect = new RECT();
				rect.left = Left ;
				rect.top = Top ;
				rect.right = Left + Width ;
				rect.bottom = Top + Height ;
				DrawFocusRect( this.intHDC , ref rect );
			}
		}
		/// <summary>
		/// 绘制可逆矩形边框
		/// </summary>
		/// <param name="rect">指定的区域</param>
		public void DrawFocusRect( System.Drawing.Rectangle rect )
		{
			if( intHDC.ToInt32() != 0 )
			{
				RECT r = new RECT();
				r.left = rect.Left ;
				r.top = rect.Top ;
				r.right = rect.Right ;
				r.bottom = rect.Bottom ;
				DrawFocusRect(this.intHDC , ref r );
			}
		}

		/// <summary>
		/// 使用当前的画笔绘制一个矩形区域,并用当前的画刷填充矩形区域
		/// </summary>
		/// <param name="rect">矩形区域</param>
		public void Rectangle( System.Drawing.Rectangle rect )
		{
			Rectangle( this.intHDC , rect.Left , rect.Top , rect.Right , rect.Bottom );
		}
		/// <summary>
		/// 使用当前的画笔绘制一个矩形区域,并用当前的画刷填充矩形区域
		/// </summary>
		/// <param name="left">矩形区域左端位置</param>
		/// <param name="top">矩形区域顶端位置</param>
		/// <param name="width">矩形区域宽度</param>
		/// <param name="height">矩形区域高度</param>
		public void Rectangle( int left , int top , int width , int height )
		{
			Rectangle( this.intHDC , left , top , left + width , top + height );
		}

		/// <summary>
		/// 将指定区域象素反转
		/// </summary>
		/// <param name="Left">区域左边位置</param>
		/// <param name="Top">区域顶端位置</param>
		/// <param name="Width">宽度</param>
		/// <param name="Height">高度</param>
		public void InvertRect( int Left , int Top , int Width , int Height )
		{
			if( intHDC.ToInt32() != 0 )
			{
				RECT rect = new RECT();
				rect.left = Left ;
				rect.top = Top ;
				rect.right = Left + Width ;
				rect.bottom = Top + Height ;
				InvertRect( this.intHDC , ref rect );
			}
		}
		/// <summary>
		/// 将指定区域象素反转
		/// </summary>
		/// <param name="rect">指定的区域</param>
		public void InverRect( System.Drawing.Rectangle rect )
		{
			if( intHDC.ToInt32() != 0 )
			{
				RECT r = new RECT();
				r.left = rect.Left ;
				r.top = rect.Top ;
				r.right = rect.Right ;
				r.bottom = rect.Bottom ;
				InvertRect(this.intHDC , ref r );
			}
		}
		/// <summary>
		/// 获得设备上下文中所有区域的位图对象
		/// </summary>
		/// <returns>BMP位图对象</returns>
		public System.Drawing.Bitmap GetBMP( )
		{
			DeviceCapsClass cap = new DeviceCapsClass( this.intHDC );
			return GetBMP( 0 , 0 , cap.HORZRES , cap.VERTRES );
		}

		/// <summary>
		/// 获得设备上下文中指定区域的位图对象
		/// </summary>
		/// <param name="rect">指定的区域</param>
		/// <returns>获得的BMP位图对象</returns>
		public System.Drawing.Bitmap GetBMP( System.Drawing.Rectangle rect )
		{
			return GetBMP( rect.Left , rect.Top , rect.Width , rect.Height );
		}
		/// <summary>
		/// 获得设备上下文中指定区域的位图对象
		/// </summary>
		/// <param name="Left">区域左边位置</param>
		/// <param name="Top">区域顶端位置</param>
		/// <param name="Width">宽度</param>
		/// <param name="Height">高度</param>
		/// <returns>获得的BMP位图对象</returns>
		public System.Drawing.Bitmap GetBMP( int Left , int Top , int Width , int Height )
		{
			DeviceCapsClass cap = new DeviceCapsClass( this.intHDC );
			if( Left < 0 ) Left = 0 ;
			if( Top < 0 ) Top = 0 ;
			if( Left + Width > cap.HORZRES )
				Width = cap.HORZRES - Left ;
			if( Top + Height > cap.VERTRES )
				Height = cap.VERTRES - Top ;
			if( Width <= 0 || Height <= 0 )
				return null;
			IntPtr memdc = NativeCreateCompatibleDC( this.intHDC );
			if( memdc.ToInt32() == 0)
				return null;
			IntPtr bmp = CreateCompatibleBitmap( this.intHDC , Width , Height );
			if( bmp.ToInt32() == 0 )
				return null;
			IntPtr oldbmp = SelectObject( memdc , bmp );
			BitBlt( memdc , 0 , 0 , Width , Height , this.intHDC , Left , Top , 0xcc0020 );
			bmp = SelectObject( memdc , oldbmp);
			DeleteDC( memdc );
			System.Drawing.Bitmap myBmp = System.Drawing.Bitmap.FromHbitmap(bmp );
			return myBmp ;
		}

		#region 声明WIN32 API函数 *********************************************
		
		[DllImport("gdi32")]
		private static extern int BitBlt (
			IntPtr hdcDest, int x, int y, int nWidth, int nHeight, 
			IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		private static extern bool DeleteDC(System.IntPtr hDC);

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		private static extern IntPtr SelectObject(System.IntPtr hDC, System.IntPtr hObject);


		[DllImport("gdi32.dll", EntryPoint="CreateCompatibleDC" , CharSet=CharSet.Auto)]
		private static extern IntPtr NativeCreateCompatibleDC(System.IntPtr hDC);

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		private static extern IntPtr CreateCompatibleBitmap(System.IntPtr hDC , int Width , int Height );


		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		private static extern int GetPixel ( System.IntPtr hDC , int x , int y );

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		private static extern int SetPixel ( System.IntPtr hDC , int x , int y , int Color );

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		private static extern int SetROP2( System.IntPtr hDC , int DrawMode );

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		private static extern int GetROP2 ( System.IntPtr hDC );
		

		[DllImport("gdi32")]
		private static extern int SetBkColor( System.IntPtr hdc,int crColor);

		[DllImport("gdi32")]
		private static extern int GetBkColor( IntPtr hdc );
		
		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		private static extern bool DrawFocusRect(IntPtr hWnd, ref RECT rect);
		
		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		private static extern bool Rectangle ( System.IntPtr hDC , int left , int top , int right , int bottom );

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		private static extern int InvertRect( IntPtr hdc , ref RECT vRect );

		[StructLayout(LayoutKind.Sequential)]
		private struct RECT
		{
			public int left;
			public int top;
			public int right;
			public int bottom;
		}

		#endregion
	}
}//public class DeviceContexts : DeviceContextBase