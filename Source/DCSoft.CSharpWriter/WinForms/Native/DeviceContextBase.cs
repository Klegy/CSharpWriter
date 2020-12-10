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
	/// 设备上下文基础对象
	/// </summary>
    /// <remarks>,编写 袁永福</remarks>
    //[DCSoft.Common.ObfuscationLevel()]
	public class DeviceContextBase : System.IDisposable
	{
		/// <summary>
		/// 使用一个设备上下文句柄初始化对象
		/// </summary>
		/// <param name="hdc">设备上下文句柄</param>
		protected void InitFromHDC( IntPtr hdc )
		{
			intHDC = hdc ;
			InitMode = 0 ;
		}
		/// <summary>
		/// 使用一个窗体句柄初始化对象
		/// </summary>
		/// <param name="hwnd">窗体句柄</param>
		protected void InitFromHWnd( IntPtr hwnd )
		{
			intHwnd = hwnd ;
			intHDC = GetDC( hwnd );
			InitMode = 1;
		}
		/// <summary>
		/// 使用一个绘图对象初始化对象
		/// </summary>
		/// <param name="g">绘图对象</param>
		protected void InitFromGraphics( System.Drawing.Graphics g )
		{
			intHDC = g.GetHdc() ;
			myGraphics = g ;
			InitMode = 2 ;
		}

		/// <summary>
		/// 使用一个设备名称初始化对象
		/// </summary>
		/// <param name="strDriver">设备名称</param>
		protected void InitFromDriverName( string strDriver )
		{
			intHDC =  CreateDC( strDriver , null , 0 , 0 );
			InitMode = 3 ;
		}

		/// <summary>
		/// 初始化一个兼容的设备上下文
		/// </summary>
		/// <param name="hdc">上下文句柄</param>
		protected void InitCompatibleDC( IntPtr hdc )
		{
			intHDC = CreateCompatibleDC( hdc );
			InitMode = 4 ;
		}

		/// <summary>
		/// 设备上下文句柄
		/// </summary>
		protected System.IntPtr intHDC = IntPtr.Zero ;
		/// <summary>
		/// 对象初始化模式
		/// </summary>
		private int InitMode = 0 ;
		/// <summary>
		/// 初始化对象使用的图形绘制对象
		/// </summary>
		private System.Drawing.Graphics myGraphics = null;
		/// <summary>
		/// 初始化对象使用的窗体句柄
		/// </summary>
		private IntPtr intHwnd = IntPtr.Zero  ;

		/// <summary>
		/// 设备上下文句柄
		/// </summary>
		public IntPtr HDC
		{
			get
            {
                return intHDC ;
            }
		}

		/// <summary>
		/// 消耗对象
		/// </summary>
		public virtual void Dispose()
		{
			if( intHDC != IntPtr.Zero )
			{
				if( InitMode == 1 )
				{
					ReleaseDC( intHwnd , intHDC );
				}
				if( InitMode == 2  && myGraphics != null )
				{
					myGraphics.ReleaseHdc( intHDC );
                    myGraphics.Flush(System.Drawing.Drawing2D.FlushIntention.Flush);
                }
				if( InitMode == 3 )
				{
					DeleteDC( intHDC );
				}
				if( InitMode == 4 )
				{
					DeleteDC( intHDC );
				}
			}
			intHDC = IntPtr.Zero  ;
			intHwnd = IntPtr.Zero  ;
			InitMode = 0 ;
			myGraphics = null;
		}

		/// <summary>
		/// 选择对象
		/// </summary>
		/// <param name="obj">新对象的句柄</param>
		/// <returns>替换的对象的句柄</returns>
		public IntPtr SelectObject( IntPtr obj )
		{
			return SelectObject( this.intHDC , obj );
		}

		#region 声明Win32API函数 ******************************************************************

		[System.Runtime.InteropServices.DllImport("gdi32.dll", CharSet=System.Runtime.InteropServices.CharSet.Auto)]
		private static extern bool DeleteDC(IntPtr hDC);

		[System.Runtime.InteropServices.DllImport("User32.dll", CharSet=System.Runtime.InteropServices.CharSet.Auto)]
		private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

		[System.Runtime.InteropServices.DllImport("User32.dll", CharSet=System.Runtime.InteropServices.CharSet.Auto)]
		private static extern IntPtr GetDC(IntPtr hWnd);

		[System.Runtime.InteropServices.DllImport("gdi32.dll", CharSet=System.Runtime.InteropServices.CharSet.Auto)]
		private static extern IntPtr CreateDC( string strDriver , string strDevice , int Output , int InitData );

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		private static extern IntPtr SelectObject(System.IntPtr hDC, System.IntPtr hObject);

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		private static extern IntPtr CreateCompatibleDC(System.IntPtr hDC);

		#endregion

	}//public class DeviceContextBase : System.IDisposable
}