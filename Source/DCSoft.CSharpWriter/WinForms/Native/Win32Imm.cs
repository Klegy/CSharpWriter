/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using System.Runtime.InteropServices ;

namespace DCSoft.WinForms.Native
{
	/// <summary>
	/// 在Win32环境下处理输入法的模块
	/// </summary>
    /// <remarks>编制 袁永福</remarks>
	public class Win32Imm
	{
		/// <summary>
		/// 初始化对象
		/// </summary>
		/// <param name="hwnd">窗体句柄</param>
		public Win32Imm( int hwnd )
		{
			Win32Handle handle = new Win32Handle();
			handle.handle = new IntPtr( hwnd ) ;
			myControl = handle ;
		}

		/// <summary>
		/// 初始化对象
		/// </summary>
		/// <param name="hwnd">窗体句柄</param>
		public Win32Imm( IntPtr hwnd )
		{
			Win32Handle handle = new Win32Handle();
			handle.handle = hwnd ;
			myControl = handle ;
		}
		/// <summary>
		/// 初始化对象
		/// </summary>
		/// <param name="ctl">窗体对象</param>
		public Win32Imm( System.Windows.Forms.IWin32Window ctl )
		{
			myControl = ctl ;
		}
		
		/// <summary>
		/// 判断指定的窗口中输入法是否打开
		/// </summary>
		/// <returns>输入法是否打开</returns>
		public bool IsImmOpen( )
		{
			int hImc = ImmGetContext( myControl.Handle );
			if( hImc == 0 )
				return false;
			bool bolReturn = ImmGetOpenStatus( hImc );
			ImmReleaseContext( myControl.Handle , hImc );
			return bolReturn ;
		}
		/// <summary>
		/// 设置输入法窗体的位置
		/// </summary>
		/// <param name="p">输入法窗体的位置</param>
		public void SetImmPos( System.Drawing.Point p )
		{
			SetImmPos( p.X , p.Y );
		}
		/// <summary>
		/// 为指定的窗口设置输入法的位置
		/// </summary>
		/// <param name="x">输入法位置的X坐标</param>
		/// <param name="y">输入法位置的Y坐标</param>
		public void SetImmPos( int x , int y )
		{
			bool bolReturn = false;
			int iError = 0 ;
			int hImc = ImmGetContext( myControl.Handle );
			if( hImc != 0 )
			{
				CompositionForm frm2 = new CompositionForm();
				frm2.CurrentPos.x = x ;
				frm2.CurrentPos.y = y ;
				frm2.Style = (int)CandidateFormStyle.CFS_POINT ;
				bolReturn = ImmSetCompositionWindow( hImc , ref frm2 );
				iError = System.Runtime.InteropServices.Marshal.GetLastWin32Error();
				ImmReleaseContext( myControl.Handle , hImc );
			}
		}// void SetImmPos

		#region 声明处理输入法的Win32API函数及类型 ************************************************

		private System.Windows.Forms.IWin32Window myControl = null;

		private class Win32Handle : System.Windows.Forms.IWin32Window
		{
			public IntPtr handle = IntPtr.Zero ;

			public System.IntPtr Handle
			{
				get
				{
					return handle ;
				}
			}
		}

		[DllImport("imm32.dll", CharSet=CharSet.Auto)]
		private static extern int ImmCreateContext();

		[DllImport("imm32.dll", CharSet=CharSet.Auto)]
		private static extern bool ImmDestroyContext(int hImc);

//		[DllImport("imm32.dll", CharSet=CharSet.Auto)]
//		private static extern bool ImmSetCandidateWindow( int hImc , ref CandidateForm frm );

		[DllImport("imm32.dll", CharSet=CharSet.Auto)]
		private static extern bool ImmSetStatusWindowPos( int hImc , ref POINT pos);

		[DllImport("imm32.dll", CharSet=CharSet.Auto)]
		private static extern int ImmGetContext( IntPtr hwnd );

		[DllImport("imm32.dll", CharSet=CharSet.Auto)]
		private static extern bool ImmReleaseContext( IntPtr hwnd , int hImc );

		[DllImport("imm32.dll", CharSet=CharSet.Auto)]
		private static extern bool ImmGetOpenStatus(int hImc );

		[DllImport("imm32.dll", CharSet=CharSet.Auto)]
		private static extern bool ImmSetCompositionWindow( int hImc , ref CompositionForm frm );

		
		
		private enum CandidateFormStyle
		{
			CFS_DEFAULT                    = 0x0000,
			CFS_RECT                       = 0x0001,
			CFS_POINT                      = 0x0002,
			CFS_FORCE_POSITION             = 0x0020,
			CFS_CANDIDATEPOS               = 0x0040,
			CFS_EXCLUDE                    = 0x0080
		}
		private struct CompositionForm
		{
			public int		Style ;
			public POINT	CurrentPos ;
			public RECT		Area;
		}
		
		[StructLayout(LayoutKind.Sequential)]
		private struct RECT
		{
			public int left;
			public int top;
			public int right;
			public int bottom;
		}

		[StructLayout(LayoutKind.Sequential)]
		private struct POINT
		{
			public int x;
			public int y;
		}

        public const int IMN_CLOSESTATUSWINDOW = 0x0001;
        public const int IMN_OPENSTATUSWINDOW = 0x0002;
        public const int IMN_CHANGECANDIDATE = 0x0003;
        public const int IMN_CLOSECANDIDATE = 0x0004;
        public const int IMN_OPENCANDIDATE = 0x0005;
        public const int IMN_SETCONVERSIONMODE = 0x0006;
        public const int IMN_SETSENTENCEMODE = 0x0007;
        public const int IMN_SETOPENSTATUS = 0x0008;
        public const int IMN_SETCANDIDATEPOS = 0x0009;
        public const int IMN_SETCOMPOSITIONFONT = 0x000A;
        public const int IMN_SETCOMPOSITIONWINDOW = 0x000B;
        public const int IMN_SETSTATUSWINDOWPOS = 0x000C;
        public const int IMN_GUIDELINE = 0x000D;
        public const int IMN_PRIVATE = 0x000E;

        public const int WM_IME_SETCONTEXT = 0x0281;
        public const int WM_IME_NOTIFY = 0x0282;
        public const int WM_IME_CONTROL = 0x0283;
        public const int WM_IME_COMPOSITIONFULL = 0x0284;
        public const int WM_IME_SELECT = 0x0285;
        public const int WM_IME_CHAR = 0x0286;
        public const int WM_IME_REQUEST = 0x0288;
        public const int WM_IME_KEYDOWN = 0x0290;
        public const int WM_IME_KEYUP = 0x0291;

//		private struct CandidateForm
//		{
//			public int		dwIndex ;
//			public int		dwStyle ;
//			public POINT	ptCurrentPos ;
//			public RECT		rcArea ;
//		}

		#endregion

	}// public class Imm32
}