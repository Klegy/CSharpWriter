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
	/// Windows窗体消息发送者
	/// </summary>
    /// <remarks>
    /// 本类型是Win32API函数的SendMessage和PostMessage的托管封装.
    /// 编写 袁永福
    /// </remarks>
	public class MessageSender
	{
		/// <summary>
		/// 初始化对象
		/// </summary>
		/// <param name="handle">目标窗体句柄</param>
		public MessageSender( IntPtr handle)
		{
			intHandle = handle ;
		}
		/// <summary>
		/// 初始化对象
		/// </summary>
		/// <param name="win">目标窗体对象</param>
		public MessageSender( System.Windows.Forms.IWin32Window win )
		{
			myWindow = win ;
		}

		/// <summary>
		/// 目标窗体句柄
		/// </summary>
		public IntPtr Handle
		{
			get
			{
				if( myWindow == null )
					return intHandle ;
				else
					return myWindow.Handle ;
			}
		}

		private int intErrorCode = 0 ;
        /// <summary>
        /// 最后一次操作的错误代码
        /// </summary>
		public int ErrorCode
		{
			get
            {
                return intErrorCode ;
            }
		}

        /// <summary>
        /// 同步的发送消息
        /// </summary>
        /// <param name="msg">消息对象</param>
        /// <returns>操作结果</returns>
        public uint Send(System.Windows.Forms.Message msg)
        {
            return Send(msg.Msg, (uint ) msg.WParam.ToInt32(), ( uint )msg.LParam.ToInt32());
        }

        /// <summary>
        /// 异步的发送消息
        /// </summary>
        /// <param name="msg">消息对象</param>
        /// <returns>操作结果</returns>
        public bool Post(System.Windows.Forms.Message msg)
        {
            return Post(msg.Msg, (uint)msg.WParam, (uint)msg.LParam);
        }

		/// <summary>
		/// 同步的发送消息
		/// </summary>
		/// <param name="Msg">消息编号</param>
		/// <param name="wParam">参数1</param>
		/// <param name="lParam">参数2</param>
		/// <returns>返回值</returns>
		public uint Send( int Msg , uint wParam , uint lParam )
		{
			intErrorCode = 0 ;
			if( this.CheckHandle())
			{
				uint result = SendMessage( this.Handle , Msg , wParam , lParam );
				intErrorCode = GetLastError();
				return result;
			}
			else
				return 0 ;
		}
		/// <summary>
		/// 异步的发送消息
		/// </summary>
		/// <param name="Msg">消息编号</param>
		/// <param name="wParam">参数1</param>
		/// <param name="lParam">参数2</param>
		/// <returns>发送是否成功</returns>
		public bool Post( int Msg , uint wParam , uint lParam )
		{
			intErrorCode = 0 ;
			if( this.CheckHandle())
			{
				bool result = PostMessage( this.Handle , Msg , wParam , lParam );
				intErrorCode = GetLastError();
				return result ;
			}
			else
				return false;
		}

		/// <summary>
		/// 同步发送窗体移动消息
		/// </summary>
		/// <param name="x">X坐标</param>
		/// <param name="y">Y坐标</param>
		/// <returns>操作是否成功</returns>
		public bool SendMoveMessage( int x , int y )
		{
			if( this.CheckHandle())
				return SendMessage( 
					this.Handle , 
					(int)Msgs.WM_MOVE ,
					0 ,
					this.Union( y ,x )) == 0 ;
			else
				return false ;
		}
		/// <summary>
		/// 异步发送窗体移动消息
		/// </summary>
		/// <param name="x">X坐标</param>
		/// <param name="y">Y坐标</param>
		/// <returns>操作是否成功</returns>
		public bool PostMoveMessage( int x , int y )
		{
			return Post(
				(int)Msgs.WM_MOVE ,
				0 , 
				Union( y , x ));
		}
		/// <summary>
		/// 同步发送窗体关闭消息
		/// </summary>
		/// <returns>操作是否成功</returns>
		public bool SendCloseMessage( )
		{
			if( this.CheckHandle())
				return SendMessage(
					this.Handle ,
					( int) Msgs.WM_CLOSE ,
					0 ,
					0 ) == 0 ;
			else
				return false;
		}
		/// <summary>
		/// 异步发送窗体关闭消息
		/// </summary>
		/// <returns>操作是否成功</returns>
		public bool PostCloseMessage()
		{
			if( this.CheckHandle())
				return PostMessage(
					this.Handle , 
					( int ) Msgs.WM_CLOSE ,
					0 ,
					0 );
			else
				return false;
		}
		/// <summary>
		/// 同步发送显示窗体消息
		/// </summary>
		/// <param name="Visible">是否显示窗体</param>
		/// <returns>操作是否成功</returns>
		public bool SendShowWindowMessage( bool Visible )
		{
			if( this.CheckHandle())
				return SendMessage(
					this.Handle ,
					( int ) Msgs.WM_SHOWWINDOW , 
					Visible ? 1u : 0u ,
					0 ) == 0 ;
			else
				return false;
		}
		/// <summary>
		/// 异步发送显示窗体消息
		/// </summary>
		/// <param name="Visible">是否显示窗体</param>
		/// <returns>操作是否成功</returns>
		public bool PostShowWindowMessage( bool Visible )
		{
			if( this.CheckHandle())
				return PostMessage( 
					this.Handle , 
					( int ) Msgs.WM_SHOWWINDOW ,
					Visible ? 1u : 0u ,
					0 );
			else
				return false;
		}
		/// <summary>
		/// 同步发送字符消息
		/// </summary>
		/// <param name="c">字符值</param>
		/// <returns>操作是否成功</returns>
		public bool SendCharMessage( char c )
		{
			if( this.CheckHandle())
				return SendMessage( 
					this.Handle ,
					( int) Msgs.WM_CHAR , 
					( uint ) c ,
					0 ) == 0 ;
			else
				return false;
		}
		/// <summary>
		/// 异步发送字符消息
		/// </summary>
		/// <param name="c">字符值</param>
		/// <returns>操作是否成功</returns>
		public bool PostCharMessage( char c )
		{
			if( this.CheckHandle())
				return PostMessage(
					this.Handle , 
					( int ) Msgs.WM_CHAR ,
					( uint) c ,
					0 );
			else
				return false;
		}
		/// <summary>
		/// 同步发送鼠标移动消息
		/// </summary>
		/// <param name="x">鼠标X坐标</param>
		/// <param name="y">鼠标Y坐标</param>
		/// <returns>操作是否成功</returns>
		public bool SendMouseMoveMessage( int x , int y )
		{
			if( this.CheckHandle())
				return SendMessage(
					this.Handle ,
					( int ) Msgs.WM_MOUSEMOVE ,
					0 , 
					Union( y , x )) == 0 ;
			else
				return false;
		}
		/// <summary>
		/// 异步发送鼠标移动消息
		/// </summary>
		/// <param name="x">鼠标X坐标</param>
		/// <param name="y">鼠标Y坐标</param>
		/// <returns>操作是否成功</returns>
		public bool PostMouseMoveMessage( int x , int y )
		{
			if( this.CheckHandle())
				return PostMessage( 
					this.Handle , 
					( int ) Msgs.WM_MOUSEMOVE ,
					0 , 
					Union( y , x ));
			else
				return false;
		}

		/// <summary>
		/// 同步发送鼠标左按键按下消息
		/// </summary>
		/// <param name="x">鼠标X坐标</param>
		/// <param name="y">鼠标Y坐标</param>
		/// <returns>操作是否成功</returns>
		public bool SendLMouseDownMessage( int x , int y )
		{
			if( this.CheckHandle())
				return SendMessage( this.Handle ,
					( int) Msgs.WM_LBUTTONDOWN ,
					0 ,
					Union( y , x ))== 0 ;
			else
				return false;
		}
		/// <summary>
		/// 异步发送鼠标左按键按下消息
		/// </summary>
		/// <param name="x">鼠标X坐标</param>
		/// <param name="y">鼠标Y坐标</param>
		/// <returns>操作是否成功</returns>
		public bool PostLMouseDownMessage( int x , int y )
		{
			return Post( 
				( int ) Msgs.WM_LBUTTONDOWN ,
				0 , 
				Union( y , x ));
		}
		/// <summary>
		/// 同步发送鼠标左按键松开消息
		/// </summary>
		/// <param name="x">鼠标X坐标</param>
		/// <param name="y">鼠标Y坐标</param>
		/// <returns>操作是否成功</returns>
		public bool SendLMouseUpMessage( int x , int y )
		{
			if( this.CheckHandle())
				return SendMessage( 
					this.Handle , 
					( int) Msgs.WM_LBUTTONUP ,
					0 , 
					Union( y , x )) == 0 ;
			else
				return false;
		}
		/// <summary>
		/// 异步发送鼠标左按键松开消息
		/// </summary>
		/// <param name="x">鼠标X坐标</param>
		/// <param name="y">鼠标Y坐标</param>
		/// <returns>操作是否成功</returns>
		public bool PostLMouseUpMessage( int x , int y )
		{
			if( this.CheckHandle())
				return PostMessage(
					this.Handle ,
					( int ) Msgs.WM_LBUTTONUP ,
					0 ,
					Union( y , x ));
			else
				return false;
		}
		/// <summary>
		/// 同步发送鼠标右按键按下消息
		/// </summary>
		/// <param name="x">鼠标X坐标</param>
		/// <param name="y">鼠标Y坐标</param>
		/// <returns>操作是否成功</returns>
		public bool SendRMouseDownMessage( int x , int y )
		{
			if( this.CheckHandle())
				return SendMessage(
					this.Handle ,
					( int) Msgs.WM_RBUTTONDOWN ,
					0 ,
					Union( y , x ))== 0 ;
			else
				return false;
		}
		/// <summary>
		/// 异步发送鼠标右按键按下消息
		/// </summary>
		/// <param name="x">鼠标X坐标</param>
		/// <param name="y">鼠标Y坐标</param>
		/// <returns>操作是否成功</returns>
		public bool PostRMouseDownMessage( int x , int y )
		{
			if( this.CheckHandle())
				return PostMessage( 
					this.Handle , 
					( int ) Msgs.WM_RBUTTONDOWN ,
					0 , 
					Union( y , x ));
			else
				return false;
		}
		/// <summary>
		/// 同步发送鼠标右按键松开消息
		/// </summary>
		/// <param name="x">鼠标X坐标</param>
		/// <param name="y">鼠标Y坐标</param>
		/// <returns>操作是否成功</returns>
		public bool SendRMouseUpMessage( int x , int y )
		{
			if( this.CheckHandle())
				return SendMessage( 
					this.Handle , 
					( int) Msgs.WM_RBUTTONUP ,
					0 ,
					Union( y , x )) == 0 ;
			else
				return false;
		}
		/// <summary>
		/// 异步步发送鼠标右按键松开消息
		/// </summary>
		/// <param name="x">鼠标X坐标</param>
		/// <param name="y">鼠标Y坐标</param>
		/// <returns>操作是否成功</returns>
		public bool PostRMouseUpMessage( int x , int y )
		{
			if( this.CheckHandle())
				return PostMessage( 
					this.Handle , 
					( int ) Msgs.WM_RBUTTONUP ,
					0 ,
					Union( y , x ));
			else
				return false;
		}
		/// <summary>
		/// 同步发送鼠标滚轮消息
		/// </summary>
		/// <param name="ScreenX">鼠标光标在屏幕中的X坐标</param>
		/// <param name="ScreenY">鼠标光标在屏幕中的Y坐标</param>
		/// <param name="Delta">滚轮量</param>
		/// <returns>操作是否成功</returns>
		public bool SendMouseWheelMessage(
			int ScreenX ,
			int ScreenY ,
			int Delta )
		{
			if( this.CheckHandle())
				return SendMessage(
					this.Handle , 
					( int ) Msgs.WM_MOUSEWHEEL , 
					( uint )( Delta << 0x10 ) , 
					Union( ScreenY , ScreenX )) == 0 ;
			else
				return false;
		}
		/// <summary>
		/// 异步发送鼠标滚轮消息
		/// </summary>
		/// <param name="ScreenX">鼠标光标在屏幕中的X坐标</param>
		/// <param name="ScreenY">鼠标光标在屏幕中的Y坐标</param>
		/// <param name="Delta">滚轮量</param>
		/// <returns>操作是否成功</returns>
		public bool PostMouseWheelMessage(
			int ScreenX ,
			int ScreenY , 
			int Delta )
		{
			if( this.CheckHandle())
				return PostMessage(
					this.Handle , 
					( int ) Msgs.WM_MOUSEWHEEL , 
					( uint )( Delta << 0x10 ) ,
					Union( ScreenY , ScreenX ) );
			else
				return false;
		}
		/// <summary>
		/// 同步发送鼠标悬停消息
		/// </summary>
		/// <param name="x">鼠标X坐标</param>
		/// <param name="y">鼠标Y坐标</param>
		/// <returns>操作是否成功</returns>
		public bool SendMouseHoverMessage( int x , int y )
		{
			if( this.CheckHandle())
				return SendMessage( 
					this.Handle ,
					( int ) Msgs.WM_MOUSEHOVER ,
					0 ,
					Union( y , x )) == 0;
			else
				return false;
		}
		/// <summary>
		/// 异步发送鼠标悬停消息
		/// </summary>
		/// <param name="x">鼠标X坐标</param>
		/// <param name="y">鼠标Y坐标</param>
		/// <returns>操作是否成功</returns>
		public bool PostMouseHoverMessage( int x , int y )
		{
			if( this.CheckHandle())
				return PostMessage( 
					this.Handle , 
					( int ) Msgs.WM_MOUSEHOVER ,
					0 ,
					Union( y , x ));
			else
				return false;
		}
		/// <summary>
		/// 同步发送鼠标离开消息
		/// </summary>
		/// <returns>操作是否成功</returns>
		public bool SendMouseLeaveMessage()
		{
			if( this.CheckHandle())
				return SendMessage(
					this.Handle , 
					( int ) Msgs.WM_MOUSELEAVE ,
					0 ,
					0 ) == 0 ;
			else
				return false;
		}
		/// <summary>
		/// 异步发送鼠标离开消息
		/// </summary>
		/// <returns>操作是否成功</returns>
		public bool PostMouseLeaveMessage()
		{
			if( this.CheckHandle())
				return PostMessage( 
					this.Handle , 
					( int ) Msgs.WM_MOUSELEAVE ,
					0 , 
					0 );
			else
				return false;
		}
		/// <summary>
		/// 同步发送键盘按下消息
		/// </summary>
		/// <param name="key">键值</param>
		/// <returns>操作是否成功</returns>
		public bool SendKeyDownMessage( System.Windows.Forms.Keys key )
		{
			if( this.CheckHandle())
				return SendMessage(
					this.Handle , 
					( int ) Msgs.WM_KEYDOWN , 
					( uint ) key ,
					0 ) == 0 ;
			else
				return false;
		}
		/// <summary>
		/// 异步发送键盘按下消息
		/// </summary>
		/// <param name="key">键值</param>
		/// <returns>操作是否成功</returns>
		public bool PostKeyDownMessage( System.Windows.Forms.Keys key )
		{
			if( this.CheckHandle())
				return PostMessage(
					this.Handle , 
					( int ) Msgs.WM_KEYDOWN ,
					( uint ) key ,
					0 );
			else
				return false;
		}
		/// <summary>
		/// 同步发送键盘松开消息
		/// </summary>
		/// <param name="key">键值</param>
		/// <returns>操作是否成功</returns>
		public bool SendKeyUPMessage( System.Windows.Forms.Keys key )
		{
			if( this.CheckHandle())
				return SendMessage( 
					this.Handle , 
					( int ) Msgs.WM_KEYUP , 
					( uint ) key ,
					0 ) == 0 ;
			else
				return false;
		}
		/// <summary>
		/// 异步发送键盘松开消息
		/// </summary>
		/// <param name="key">键值</param>
		/// <returns>操作是否成功</returns>
		public bool PostKeyUPMessage( System.Windows.Forms.Keys key )
		{
			if( this.CheckHandle())
				return PostMessage(
					this.Handle ,
					( int ) Msgs.WM_KEYUP , 
					( uint ) key ,
					0 );
			else
				return false;
		}


		#region 内部代码 ******************************************************

		private bool CheckHandle()
		{
			IntPtr handle = this.Handle ;
			if( handle == IntPtr.Zero )
				return false;
			return IsWindow( handle ) ;
		}

        /// <summary>
        /// 将两个16位的整数合并为32位整数
        /// </summary>
        /// <param name="height"></param>
        /// <param name="low"></param>
        /// <returns></returns>
		private uint Union( int height , int low )
		{
			long lng = height ;
			lng = lng << 16 ;
			lng += low ;
			return ( uint) lng ;
		}

		[DllImport("Kernel32.dll", CharSet=CharSet.Auto)]
		private static extern int GetLastError( );

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		private static extern uint SendMessage( IntPtr hWnd, int Msg, uint wParam, uint lParam);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		private static extern bool PostMessage(IntPtr hWnd, int Msg, uint wParam, uint lParam);

		[DllImport("user32.dll")]
		private extern static bool IsWindow( IntPtr hwnd );
		
		private IntPtr intHandle = IntPtr.Zero ;
		private System.Windows.Forms.IWin32Window myWindow = null;

		#endregion

	}//public class MessageSender
}