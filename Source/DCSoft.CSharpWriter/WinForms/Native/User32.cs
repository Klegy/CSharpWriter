/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using System.Runtime.InteropServices;
namespace DCSoft.WinForms.Native
{
    //[DCSoft.Common.ObfuscationLevel()]
	public class User32
	{
		
		public static System.Drawing.Point GetMousePositionFromMSG( IntPtr hwnd ,uint lParam)
		{
			NativePOINT pt = new NativePOINT();
			pt.x  = (short)( lParam & 0x0000FFFFU);
			pt.y  = (short)(( lParam & 0xFFFF0000U) >> 16);
			ClientToScreen( hwnd ,ref pt);
			return new System.Drawing.Point(pt.x , pt.y);
		}

		public static System.Drawing.Point GetMousePositionFromMSG(NativeMSG msg)
		{
			NativePOINT pt = new NativePOINT();
			pt.x  = (short)((uint)msg.lParam & 0x0000FFFFU);
			pt.y  = (short)(((uint)msg.lParam & 0xFFFF0000U) >> 16);
			ClientToScreen( new IntPtr( msg.hwnd ),ref pt);
			return new System.Drawing.Point(pt.x , pt.y);
		}

		/// <summary>
		/// 根据Windows消息内容计算鼠标光标在屏幕中的位置
		/// </summary>
		/// <param name="intMessage">Windows消息编号</param>
		/// <param name="Hwnd">Windows消息中的窗体句柄</param>
		/// <param name="lParam">Windows消息的Param参数</param>
		/// <returns>鼠标光标在屏幕中的位置</returns>
		public static System.Drawing.Point GetMousePositionFromMessage(int intMessage , IntPtr Hwnd, uint lParam)
		{
			bool bolClient = true;

            if (// 鼠标在非客户区的按键按下消息
                (intMessage == (int)Msgs.WM_NCLBUTTONDOWN) ||
                (intMessage == (int)Msgs.WM_NCMBUTTONDOWN) ||
                (intMessage == (int)Msgs.WM_NCRBUTTONDOWN) ||
                (intMessage == (int)Msgs.WM_NCXBUTTONDOWN) ||
                // 鼠标在非客户区的按键松开消息
                (intMessage == (int)Msgs.WM_NCLBUTTONUP) ||
                (intMessage == (int)Msgs.WM_NCMBUTTONUP) ||
                (intMessage == (int)Msgs.WM_NCRBUTTONUP) ||
                (intMessage == (int)Msgs.WM_NCXBUTTONUP) ||
                // 鼠标在非客户区的移动消息
                (intMessage == (int)Msgs.WM_NCMOUSEMOVE))
            {
                bolClient = false;
            }
			NativePOINT pt = new NativePOINT();
			pt.x  = (short)( lParam & 0x0000FFFFU);
			pt.y  = (short)(( lParam & 0xFFFF0000U) >> 16);
            if (bolClient)
            {
                ClientToScreen(Hwnd, ref pt);
            }
			return new System.Drawing.Point(pt.x , pt.y);
		}

		/// <summary>
		/// 判断该Windows消息是否是鼠标按键按下消息
		/// </summary>
		/// <param name="intMessage">消息编码</param>
		/// <returns>判断结果</returns>
		public static bool IsMouseDownMessage(int intMessage)
		{
			// 鼠标在客户区的按钮按下消息
            if ((intMessage == (int)Msgs.WM_LBUTTONDOWN) ||
                (intMessage == (int)Msgs.WM_MBUTTONDOWN) ||
                (intMessage == (int)Msgs.WM_RBUTTONDOWN) ||
                (intMessage == (int)Msgs.WM_XBUTTONDOWN))
            {
                return true;
            }
			// 鼠标在非客户区的按键按下消息
            if ((intMessage == (int)Msgs.WM_NCLBUTTONDOWN) ||
                (intMessage == (int)Msgs.WM_NCMBUTTONDOWN) ||
                (intMessage == (int)Msgs.WM_NCRBUTTONDOWN) ||
                (intMessage == (int)Msgs.WM_NCXBUTTONDOWN))
            {
                return true;
            }
			return false;
		}
		/// <summary>
		/// 判断该Windows消息是否是鼠标移动消息
		/// </summary>
		/// <param name="intMessage">消息编码</param>
		/// <returns>判断结果</returns>
		public static bool IsMouseMoveMessage(int intMessage)
		{
            if ((intMessage == (int)Msgs.WM_MOUSEMOVE) ||
                (intMessage == (int)Msgs.WM_NCMOUSEMOVE))
            {
                return true;
            }
			return false;
		}
		/// <summary>
		/// 判断该Windows消息是否是鼠标按键松开消息
		/// </summary>
		/// <param name="intMessage">消息编码</param>
		/// <returns>判断结果</returns>
		public static bool IsMouseUpMessage(int intMessage)
		{
			// 鼠标在客户区的按钮松开消息
            if ((intMessage == (int)Msgs.WM_LBUTTONUP) ||
                (intMessage == (int)Msgs.WM_MBUTTONUP) ||
                (intMessage == (int)Msgs.WM_RBUTTONUP) ||
                (intMessage == (int)Msgs.WM_XBUTTONUP))
            {
                return true;
            }
			// 鼠标在非客户区的按键松开消息
            if ((intMessage == (int)Msgs.WM_NCLBUTTONUP) ||
                (intMessage == (int)Msgs.WM_NCMBUTTONUP) ||
                (intMessage == (int)Msgs.WM_NCRBUTTONUP) ||
                (intMessage == (int)Msgs.WM_NCXBUTTONUP))
            {
                return true;
            }
			return false;
		}
		/// <summary>
		/// 判断Windows消息是否是键盘消息
		/// </summary>
		/// <param name="intMessage">消息编码</param>
		/// <returns>判断结果</returns>
		public static bool isKeyMessage(int intMessage)
		{
            if (intMessage == (int)Msgs.WM_KEYDOWN ||
                intMessage == (int)Msgs.WM_KEYUP ||
                intMessage == (int)Msgs.WM_CHAR)
            {
                return true;
            }
			return false;
		}


		/// <summary>
		/// 获得高8位字节数值
		/// </summary>
		/// <param name="intValue"></param>
		/// <returns></returns>
		public static int GetHeightOrder( uint intValue)
		{
			return (int)( (intValue & 0xFFFF0000U) >> 16 );
		}
		/// <summary>
		/// 获得低8位字节数值
		/// </summary>
		/// <param name="intValue"></param>
		/// <returns></returns>
		public static int GetLowOrder( uint intValue)
		{
			return (int) ( intValue & 0x0000FFFFU );
		}

		public static int SetWindowRgn( int hWnd , System.Drawing.Rectangle rect )
		{
			int rgn = Gdi32.CreateRectRgn( rect.Left , rect.Top , rect.Right , rect.Bottom );
			return SetWindowRgn( hWnd , rgn , true );
		}

		public static int SetWindowRgn( int hWnd , int left , int top , int width , int height )
		{
			int rgn = Gdi32.CreateRectRgn( left , top , left + width , top + height );
			return SetWindowRgn( hWnd , rgn , true );
		}

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern int SetWindowRgn( int hWnd , int Rgn , bool Redraw );

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern int ScrollWindow( int hWnd , int xAmound , int yAmount , int rect , int clipRect );
		
		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern int GetWindowLong(int hWnd, int nIndex);
            
		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern int SetWindowLong(int hWnd, int nIndex, int newLong);
            
		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool SystemParametersInfo(uint uiAction, uint uiParam, ref int bRetValue, uint fWinINI);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool AnimateWindow(int hWnd, uint dwTime, uint dwFlags);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool InvalidateRect(int hWnd, ref RECT rect, bool erase);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern int LoadCursor(int hInstance, uint cursor);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern int SetCursor(IntPtr hCursor);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern int GetFocus();

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern int SetFocus(IntPtr hWnd);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool ReleaseCapture();

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool WaitMessage();

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool TranslateMessage(ref NativeMSG msg);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool DispatchMessage(ref NativeMSG msg);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
        public static extern bool PostMessage(IntPtr hWnd, int Msg, uint wParam, uint lParam);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
        public static extern uint SendMessage(IntPtr hWnd, int Msg, uint wParam, uint lParam);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool GetMessage(ref NativeMSG msg, IntPtr hWnd, uint wFilterMin, uint wFilterMax);
	
		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool PeekMessage(ref NativeMSG msg, IntPtr hWnd, uint wFilterMin, uint wFilterMax, uint wFlag);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern int BeginPaint(IntPtr hWnd, ref PAINTSTRUCT ps);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool EndPaint(IntPtr hWnd, ref PAINTSTRUCT ps);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr GetDC(IntPtr hWnd);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern int ShowWindow(IntPtr hWnd, short cmdShow);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool MoveWindow(IntPtr hWnd, int x, int y, int width, int height, bool repaint);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern int SetWindowPos(IntPtr hWnd, int hWndAfter, int X, int Y, int Width, int Height, uint flags);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool UpdateLayeredWindow(IntPtr hwnd, int hdcDst, ref NativePOINT pptDst, ref SIZE psize, int hdcSrc, ref NativePOINT pprSrc, Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool GetWindowRect(IntPtr hWnd, ref RECT rect);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool ClientToScreen(IntPtr hWnd, ref NativePOINT pt);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool ScreenToClient(IntPtr hWnd, ref NativePOINT pt);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool TrackMouseEvent(ref TRACKMOUSEEVENTS tme);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool SetWindowRgn(IntPtr hWnd, int hRgn, bool redraw);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern ushort GetKeyState(int virtKey);

		
		[DllImport("user32")] //ANSI
		public static extern int RegisterClassA(ref WNDCLASS wc);

		[DllImport("user32")] //ANSI
		public static extern int DefWindowProcA(IntPtr hwnd, int msg, int wParam, int lParam);

		[DllImport("user32")]
		public static extern void PostQuitMessage(int nExitCode);

		[DllImport("user32")] //ANSI
		public static extern int GetMessageA(out NativeMSG msg, int hwnd, int minFilter, int maxFilter);

		[DllImport("user32")] //ANSI
		public static extern bool PeekMessageA(out NativeMSG lpMsg, int hWnd, uint wMsgFilterMin, uint wMsgFilterMax, PeekMessageFlags wRemoveMsg );

		[DllImport("user32",CharSet=CharSet.Ansi)] //ANSI
		public static extern int DispatchMessageA(ref NativeMSG msg);
 
		
		[DllImport("user32")]
		public static extern bool GetClientRect(IntPtr hwnd, out RECT rect);

		
		[DllImport("user32")] //ANSI
		public static extern int CreateWindowExA(WindowExStyles dwExStyle, string lpszClassName, string lpszWindowName, WindowStyles style, int x, int y, int width, int height, int hWndParent, int hMenu, int hInst, int pvParam);

		

		[DllImport("user32")]
		public static extern bool SetWindowPos( IntPtr hwnd, int hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags );

		[DllImport("user32")]
		public static extern bool SetWindowPos( IntPtr hwnd, SetWindowsPosPosition hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags );

		[DllImport("user32")]
		public static extern bool ShowWindow(IntPtr hwnd, ShowWindowStyles nCmdShow );

		[DllImport("user32")]
		public static extern bool UpdateWindow(IntPtr hwnd);

		
		[DllImport("user32")] //ANSI
		public static extern bool PostMessageA(IntPtr hwnd, Msgs Msg, int wParam, int lParam);

		[DllImport("user32")]
		public static extern int GetSystemMetrics (SystemMetricsConst nIndex);

		[DllImport("user32")] //ANSI
		public static extern bool SystemParametersInfoA(SystemParametersAction uiAction, uint uiParam, out RECT pvParam, uint fWinIni );

		[DllImport("user32")] //ANSI
		public static extern bool SystemParametersInfoA(SystemParametersAction uiAction, uint uiParam, out int pvParam, uint fWinIni );

		[DllImport("user32")] //ANSI
		public static extern bool SystemParametersInfoA(SystemParametersAction uiAction, uint uiParam, out bool pvParam, uint fWinIni );

		[DllImport("user32")] //ANSI
		public static extern bool SystemParametersInfoA(uint uiAction, uint uiParam, ref NONCLIENTMETRICS pvParam,uint fWinIni);

		
		[DllImport("user32")]
		public static extern int FillRect(IntPtr hDC, ref RECT lprc, int hbr);

		[DllImport("user32")] //ANSI
		public static extern bool SetWindowTextA( IntPtr hWnd, string lpString);

		[DllImport("user32")]
		public static extern bool InvalidateRect(IntPtr hWnd, int hRgn, bool bErase);

		[DllImport("user32")]
		public static extern bool TrackMouseEvent( ref TRACKMOUSEEVENT lpEventTrack);

		[DllImport("user32")] //ANSI
		public static extern int LoadCursorA( int hInstance, CursorName lpCursorName);

		[DllImport("user32")]
		public static extern bool DestroyWindow( IntPtr hWnd );

		[DllImport("user32")]
		public static extern int GetForegroundWindow();

		[DllImport("user32")]
		public static extern bool CloseWindow(IntPtr hWnd);

		
		[DllImport("user32")]
		public static extern bool IsWindowVisible( IntPtr hWnd);

		
		[DllImport("user32")]
		public static extern int SetWindowLongA( IntPtr hWnd, SetWindowLongType nIndex, WindowStyles dwNewLong);

		[DllImport("user32")]
		public static extern int SetWindowLongA( IntPtr hWnd, SetWindowLongType nIndex, WindowExStyles dwNewLong);

		[DllImport("user32")]
		public static extern int GetWindowLongA( IntPtr hWnd, SetWindowLongType nIndex);

		[DllImport("user32")]
		public static extern bool RedrawWindow( IntPtr hWnd, int lprcUpdate, int hrgnUpdate, RedrawWindowFlags flags);

		[DllImport("user32")]
		public static extern bool AdjustWindowRectEx(ref RECT lpRect, WindowStyles dwStyle, bool bMenu, WindowExStyles dwExStyle);

		[DllImport("user32")]
		public static extern short GetKeyState( VirtualKeys nVirtKey );

		
		[DllImport("user32")]
		public static extern uint SetTimer(IntPtr hwnd, uint nIDEvent, uint uElapse, TimerProc lpTimerFunc);

		[DllImport("user32")]
		public static extern uint SetTimer(IntPtr hwnd, uint nIDEvent, uint uElapse, int lpTimerFunc);

		[DllImport("user32")]
		public static extern bool KillTimer(IntPtr hwnd, uint uIDEvent);

		[DllImport("user32")]
		public static extern int SetParent( IntPtr hWndChild, IntPtr hWndNewParent );

		[DllImport("user32")]
		public static extern int SetCapture( IntPtr hWnd );

		[DllImport("user32")]
		public static extern int GetCapture();

		[DllImport("user32")]
		public static extern int WindowFromPoint( NativePOINT Point);

		/// <summary>
		/// 获得桌面的窗体句柄
		/// </summary>
		/// <returns>桌面的窗体句柄</returns>
		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern int GetDesktopWindow();

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool GetInputState( );

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern int GetParent(IntPtr hWnd);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool DrawFocusRect(IntPtr hWnd, ref RECT rect);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool HideCaret(IntPtr hWnd);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool ShowCaret(IntPtr hWnd);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern int InvertRect( IntPtr hdc , ref RECT vRect );

        /// <summary>
        /// 获得剪切板数据来源窗体句柄
        /// </summary>
        /// <returns></returns>
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetClipboardOwner();

		public static int InvertRect( IntPtr hdc , int left , int top , int width , int height )
		{
			RECT rect = new RECT();
			rect.left = left ;
			rect.top = top ;
			rect.right = left + width ;
			rect.bottom = top + height ;
			return InvertRect( hdc , ref rect );
		}
		public static int InvertRect( IntPtr hdc , System.Drawing.Rectangle r )
		{
			RECT rect = new RECT();
			rect.left = r.Left ;
			rect.top = r.Top ;
			rect.right = r.Right ;
			rect.bottom = r.Bottom ;
			return InvertRect( hdc , ref rect );
		}

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern int SetActiveWindow( IntPtr hWnd);
		
		//		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		//		public static extern int SetFocus( int hWnd);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern int GetSystemMetrics( int nIndex );
	}
}