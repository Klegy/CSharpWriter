/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;

namespace DCSoft.CSharpWriter.Dom
{
	/// <summary>
	/// 文档用户界面事件类型
	/// </summary>
    /// <remarks>编制 袁永福</remarks>
	public enum DocumentEventStyles
	{
        /// <summary>
        /// 无效类型
        /// </summary>
		None ,
        /// <summary>
        /// 鼠标按键按下事件
        /// </summary>
		MouseDown ,
        /// <summary>
        /// 鼠标移动事件
        /// </summary>
		MouseMove ,
        /// <summary>
        /// 鼠标按键松开事件
        /// </summary>
		MouseUp ,
        /// <summary>
        /// 鼠标单击事件
        /// </summary>
        MouseClick ,
        /// <summary>
        /// 鼠标双击事件
        /// </summary>
        MouseDblClick,
        /// <summary>
        /// 键盘按键按下事件
        /// </summary>
		KeyDown ,
        /// <summary>
        /// 键盘字符事件
        /// </summary>
		KeyPress,
        /// <summary>
        /// 键盘按键松开事件
        /// </summary>
		KeyUp,
        /// <summary>
        /// 失去输入焦点事件
        /// </summary>
        LostFocus,
        /// <summary>
        /// 获得输入焦点事件
        /// </summary>
        GotFocus,
        /// <summary>
        /// 鼠标光标进入事件
        /// </summary>
        MouseEnter ,
        /// <summary>
        /// 鼠标光标离开事件
        /// </summary>
        MouseLeave 
	}

    /// <summary>
    /// 文档事件委托类型
    /// </summary>
    /// <param name="sender">参数</param>
    /// <param name="args">参数</param>
    public delegate void DocumentEventHandelr( object sender , DocumentEventArgs args );

	/// <summary>
	/// 文档事件参数
	/// </summary>
	public class DocumentEventArgs
	{
		public static System.Windows.Forms.Cursor DefaultCursor 
            = System.Windows.Forms.Cursors.IBeam ;

		/// <summary>
		/// 创建鼠标按键按下事件参数
		/// </summary>
		/// <param name="doc">文档对象</param>
		/// <param name="e">原始事件参数</param>
		/// <returns>创建的参数</returns>
		public static DocumentEventArgs CreateMouseDown(
			DomDocument doc ,
			System.Windows.Forms.MouseEventArgs e )
		{
			return CreateMouseEvent( doc , e , DocumentEventStyles.MouseDown );
		}
		/// <summary>
		/// 创建鼠标移动按下事件参数
		/// </summary>
		/// <param name="doc">文档对象</param>
		/// <param name="e">原始事件参数</param>
		/// <returns>创建的参数</returns>
		public static DocumentEventArgs CreateMouseMove(
			DomDocument doc ,
			System.Windows.Forms.MouseEventArgs e )
		{
			return CreateMouseEvent( doc , e , DocumentEventStyles.MouseMove );
		}
		/// <summary>
		/// 创建鼠标按键松开事件参数
		/// </summary>
		/// <param name="doc">文档对象</param>
		/// <param name="e">原始事件参数</param>
		/// <returns>创建的参数</returns>
		public static DocumentEventArgs CreateMouseUp(
			DomDocument doc ,
			System.Windows.Forms.MouseEventArgs e )
		{
			return CreateMouseEvent( doc , e , DocumentEventStyles.MouseUp );
		}
		/// <summary>
		/// 创建键盘按键按下事件参数对象
		/// </summary>
		/// <param name="doc">文档对象</param>
		/// <param name="e">原始事件参数</param>
		/// <returns>创建的事件参数对象</returns>
		public static DocumentEventArgs CreateKeyDown( 
			DomDocument doc ,
			System.Windows.Forms.KeyEventArgs e )
		{
			DocumentEventArgs args = new DocumentEventArgs();
			args.myDocument = doc ;
			args.bolAltKey = e.Alt ;
			args.bolCtlKey = e.Control ;
			args.bolShiftKey = e.Shift ;
			args.intKeyCode = e.KeyCode ;
			args.intKeyChar = ( char ) e.KeyCode ;
			args.intStyle = DocumentEventStyles.KeyDown ;
			return args ;
		}

		/// <summary>
		/// 创建键盘按键按下事件参数对象
		/// </summary>
		/// <param name="doc">文档对象</param>
		/// <param name="e">原始事件参数</param>
		/// <returns>创建的事件参数对象</returns>
		public static DocumentEventArgs CreateKeyPress( 
			DomDocument doc ,
			System.Windows.Forms.KeyPressEventArgs e )
		{
			DocumentEventArgs args = new DocumentEventArgs();
			args.myDocument = doc ;
			args.UpdateKeyState();
			args.intKeyChar = e.KeyChar ;
			args.intStyle = DocumentEventStyles.KeyPress ;
			return args ;
		}

		/// <summary>
		/// 创建键盘按键松开事件参数对象
		/// </summary>
		/// <param name="doc">文档对象</param>
		/// <param name="e">原始事件参数</param>
		/// <returns>创建的事件参数对象</returns>
		public static DocumentEventArgs CreateKeyUp( 
			DomDocument doc ,
			System.Windows.Forms.KeyEventArgs e )
		{
			DocumentEventArgs args = new DocumentEventArgs();
			args.myDocument = doc ;
			args.bolAltKey = e.Alt ;
			args.bolCtlKey = e.Control ;
			args.bolShiftKey = e.Shift ;
			args.intKeyCode = e.KeyCode ;
			args.intKeyChar = ( char ) e.KeyCode ;
			args.intStyle = DocumentEventStyles.KeyUp ;
			return args ;
		}

		private static DocumentEventArgs CreateMouseEvent(
			DomDocument doc ,
			System.Windows.Forms.MouseEventArgs e ,
			DocumentEventStyles style )
		{
			DocumentEventArgs args = new DocumentEventArgs();
            args._MouseClicks = e.Clicks;
			args.myDocument = doc ;
			args.intX = e.X ;
			args.intY = e.Y ;
			args.intButton = e.Button ;
			args.intWheelDelta = e.Delta ;
			args.UpdateKeyState();
			args.intStyle =style ;
			return args ;
		}

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="document">文档对象</param>
        /// <param name="element">文档元素对象</param>
        /// <param name="style">事件类型</param>
        public DocumentEventArgs(DomDocument document, DomElement element, DocumentEventStyles style)
        {
            this.myDocument = document;
            this._Element = element;
            this.intStyle = style;
            this.UpdateKeyState();
        }

		/// <summary>
		/// 内部使用的构造函数
		/// </summary>
		internal DocumentEventArgs()
		{
			myCursor = DefaultCursor ;
			strTooltip = null;
		}
//
//		/// <summary>
//		/// 初始化对象
//		/// </summary>
//		/// <param name="doc"></param>
//		/// <param name="name"></param>
//		/// <param name="alt"></param>
//		/// <param name="ctl"></param>
//		/// <param name="shift"></param>
//		/// <param name="key"></param>
//		/// <param name="vchar"></param>
//		/// <param name="clientx"></param>
//		/// <param name="clienty"></param>
//		/// <param name="x"></param>
//		/// <param name="y"></param>
//		/// <param name="button"></param>
//		/// <param name="delta"></param>
//		internal DocumentEventArgs( 
//			XTextDocument doc , 
//			string name , 
//			bool alt ,
//			bool ctl ,
//			bool shift ,
//			System.Windows.Forms.Keys key ,
//			char vchar ,
//			int clientx ,
//			int clienty ,
//			int x , 
//			int y ,
//			System.Windows.Forms.MouseButtons button ,
//			int delta )
//		{
//			myDocument = doc ;
//			strName = name ;
//			bolAltKey = alt ;
//			bolCtlKey = ctl ;
//			bolShiftKey = shift ;
//			intKeyCode = key ;
//			intKeyChar = vchar ;
//			intClientX = clientx ;
//			intClientY = clienty ;
//			intX = x ;
//			intY = y ;
//			intButton = button ;
//			intWheelDelta = delta ;
//			myCursor = DefaultCursor ;
//			strTooltip = null;
//		}

		private void UpdateKeyState()
		{
			System.Windows.Forms.Keys key = 
                System.Windows.Forms.Control.ModifierKeys ;
			bolAltKey = ( ( key & System.Windows.Forms.Keys.Shift) != 0);
			bolCtlKey = ( ( key & System.Windows.Forms.Keys.Control ) != 0 );
			bolShiftKey = ( ( key & System.Windows.Forms.Keys.Shift ) != 0 );
		}



		internal DocumentEventStyles intStyle = DocumentEventStyles.None ;
		/// <summary>
		/// 文档事件类型
		/// </summary>
		public DocumentEventStyles Style
		{
			get
            { 
                return intStyle ;
            }
		}

		private DomDocument myDocument = null;
		/// <summary>
		/// 对象所在文档对象
		/// </summary>
		public DomDocument Document
		{
			get
            {
                return myDocument ;
            }
		}

        private DomElement _Element = null;
        /// <summary>
        /// 事件相关的文档元素对象
        /// </summary>
        public DomElement Element
        {
            get { return _Element; }
            set { _Element = value; }
        }

		internal string strName = null;
		/// <summary>
		/// 事件名称
		/// </summary>
		public string Name
		{
			get
            { 
                return strName ;
            }
		}

		private bool bolAltKey = false;
		/// <summary>
		/// 用户是否按下了 Alt 键
		/// </summary>
		public bool AltKey
		{
			get
            { 
                return bolAltKey ;
            }
       }

		private bool bolCtlKey = false;
		/// <summary>
		/// 用户是否按下的 Ctl 键
		/// </summary>
		public bool CtlKey 
		{
			get
            { 
                return bolCtlKey ;
            }
		}

		private bool bolShiftKey = false;
		/// <summary>
		/// 用户是否按下了 Shift 键
		/// </summary>
		public bool ShiftKey
		{
			get
            {
                return bolShiftKey ;
            }
		}

		internal System.Windows.Forms.Keys intKeyCode 
            = System.Windows.Forms.Keys.None ;
		/// <summary>
		/// 键盘按键值
		/// </summary>
		public System.Windows.Forms.Keys KeyCode
		{
			get
            {
                return intKeyCode ;
            }
		}

		internal char intKeyChar = char.MinValue ;
		/// <summary>
		/// 键盘字符值
		/// </summary>
		public char KeyChar
		{
			get
            { 
                return intKeyChar ;
            }
		}

      

		private bool bolCancelBubble = false;
		/// <summary>
		/// 事件是否上浮到上层元素
		/// </summary>
		public bool CancelBubble
		{
			get
            {
                return bolCancelBubble ;
            }
			set
            { 
                bolCancelBubble = value;
            }
		}

        /// <summary>
        /// 鼠标光标坐标转换时出现了严格命中
        /// </summary>
        internal bool _StrictMatch = true;
        /// <summary>
        /// 鼠标光标坐标转换时出现了严格命中
        /// </summary>
        public bool StrictMatch
        {
            get { return _StrictMatch; }
        }

        private int _MouseClicks = 0;
        /// <summary>
        /// 鼠标点击次数
        /// </summary>
        public int MouseClicks
        {
            get { return _MouseClicks; }
            set { _MouseClicks = value; }
        }

		internal int intClientX = 0 ;
		/// <summary>
		/// 鼠标在文档控件客户区的X坐标
		/// </summary>
		public int ClientX
		{
			get
            { 
                return intClientX ;
            }
		}

		internal int intClientY = 0 ;
		/// <summary>
		/// 鼠标在文档控件客户区的Y坐标
		/// </summary>
		public int ClientY 
		{
			get
            { 
                return intClientY ;
            }
		}

		internal int intX = 0 ;
		/// <summary>
		/// 鼠标光标在视图中的X坐标
		/// </summary>
		public int X
		{
			get
            { 
                return intX ;
            }
		}

		internal int intY = 0 ;
		/// <summary>
		/// 鼠标光标在视图中的Y坐标
		/// </summary>
		public int Y
		{
			get
            { 
                return intY ;
            }
		}

		internal System.Windows.Forms.MouseButtons intButton = System.Windows.Forms.MouseButtons.None ;
		/// <summary>
		/// 鼠标按键值
		/// </summary>
		public System.Windows.Forms.MouseButtons Button
		{
			get
            { 
                return intButton ;
            }
		}

		internal int intWheelDelta = 0 ;
		/// <summary>
		/// 鼠标滚轮值
		/// </summary>
		public int WheelDelta
		{
			get
            { 
                return intWheelDelta ;
            }
		}

		private object objReturnValue = null;
		/// <summary>
		/// 事件返回值
		/// </summary>
		public object ReturnValue
		{
			get
            { 
                return objReturnValue ;
            }
			set
            { 
                objReturnValue = value;
            }
		}

		internal static System.Windows.Forms.Cursor myCursor 
            = System.Windows.Forms.Cursors.IBeam ;
		/// <summary>
		/// 视图区鼠标光标对象
		/// </summary>
		public System.Windows.Forms.Cursor Cursor
		{
			get
            { 
                return myCursor ;
            }
			set
            { 
                myCursor = value;
            }
		}
		
		/// <summary>
		/// 提示文本
		/// </summary>
		internal static string strTooltip = null;
		/// <summary>
		/// 提示文本
		/// </summary>
		public string Tooltip
		{
			get
            { 
                return strTooltip ;
            }
			set
            {
                strTooltip = value;
            }
		}


		/// <summary>
		/// 复制对象
		/// </summary>
		/// <returns>对象复制品</returns>
		public DocumentEventArgs Clone()
		{
            DocumentEventArgs args = (DocumentEventArgs)this.MemberwiseClone();
			//args.myCursor = this.myCursor ;
			return args ;
		}
	}
}