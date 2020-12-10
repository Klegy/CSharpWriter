/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using DCSoft.WinForms.Native;

namespace DCSoft.WinForms
{
	/// <summary>
	/// 带边框的用户控件对象
	/// </summary>
	/// <remarks>本控件在UserControl的基础上实现了标准下陷的控件边框,并支持滚动事件
	/// 新增属性 BorderStyle 可让控件不显示边框,显示简单的细边框或3D下陷式的粗边框 
	/// 新增事件 Scroll ,当用户滚动控件时会触发该事件</remarks>
	[System.ComponentModel.Browsable(false)]
    [System.ComponentModel.ToolboxItem(false)]
	public class BorderUserControl : System.Windows.Forms.UserControl
	{
		/// <summary>
		/// 是否启用双缓冲设置
		/// </summary>
		public static bool DoubleBuffer = true ;
		/// <summary>
		/// 初始化对象
		/// </summary>
		public BorderUserControl()
		{
			this.AutoScroll = true ;
			this.SetStyle( System.Windows.Forms.ControlStyles.ResizeRedraw , true );
            this.SetStyle(ControlStyles.Selectable, true);
			if( DoubleBuffer )
			{
				this.SetStyle( System.Windows.Forms.ControlStyles.UserPaint , true );
				this.SetStyle( System.Windows.Forms.ControlStyles.DoubleBuffer , true );
				this.SetStyle( System.Windows.Forms.ControlStyles.AllPaintingInWmPaint , true );
			}
            
        }

//#if DOTNET11
		/// <summary>
		/// 启动双缓冲样式
		/// </summary>
		[System.ComponentModel.DefaultValue( true )]
        [System.ComponentModel.Category("Appearance")]
		public bool DoubleBuffering
		{
			get
			{
				return this.GetStyle( ControlStyles.DoubleBuffer );
			}
			set
			{
				if( this.GetStyle( ControlStyles.DoubleBuffer ) != value )
				{
					if( value )
					{
						this.SetStyle( ControlStyles.UserPaint , true );
						this.SetStyle( ControlStyles.DoubleBuffer , true );
						this.SetStyle( ControlStyles.AllPaintingInWmPaint , true );
					}
					else
					{
						this.SetStyle( ControlStyles.DoubleBuffer , false );
					}
					this.UpdateStyles();
					this.Invalidate();
				}
			}
		}
//#endif

        private System.Drawing.Bitmap _FreezeUIBmp = null;
        /// <summary>
        /// 冻结用户界面
        /// </summary>
        public void FreezeUI()
        {
            if (_FreezeUIBmp == null)
            {
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(
                    this.ClientSize.Width,
                    this.ClientSize.Height);
                using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp))
                {
                    g.Clear( this.BackColor );
                    System.Windows.Forms.PaintEventArgs args = new PaintEventArgs(
                        g, 
                        new System.Drawing.Rectangle(
                            0,
                            0,
                            this.ClientSize.Width,
                            this.ClientSize.Height));
                    this.OnPaint(args);
                }
                _FreezeUIBmp = bmp;
                //this.DrawToBitmap(
                //    _FreezeUIBmp,
                //    new System.Drawing.Rectangle(
                //        0,
                //        0,
                //        this.ClientSize.Width,
                //        this.ClientSize.Height));
            }
        }

        /// <summary>
        /// 正在冻结用户界面
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public bool IsFreezeUI
        {
            get
            {
                return _FreezeUIBmp != null;
            }
        }

        /// <summary>
        /// 解冻用户界面
        /// </summary>
        public void ReleaseFreezeUI()
        {
            if (_FreezeUIBmp != null)
            {
                _FreezeUIBmp.Dispose();
                _FreezeUIBmp = null;
                this.Invalidate();
            }
        }

        /// <summary>
        /// 绘制冻结的用户界面
        /// </summary>
        /// <param name="args">参数</param>
        protected void DrawFreezeUI(PaintEventArgs args)
        {
            if (_FreezeUIBmp != null)
            {
                args.Graphics.DrawImage(
                    _FreezeUIBmp,
                    args.ClipRectangle.Left,
                    args.ClipRectangle.Top,
                    new System.Drawing.RectangleF(
                        args.ClipRectangle.Left,
                        args.ClipRectangle.Top,
                        args.ClipRectangle.Width,
                        args.ClipRectangle.Height),
                    System.Drawing.GraphicsUnit.Pixel);
            }
        }

        /// <summary>
        /// 绘制控件内容
        /// </summary>
        /// <param name="e">参数</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.IsFreezeUI)
            {
                DrawFreezeUI(e);
            }
            else
            {
                base.OnPaint(e);
            }
        }

        /// <summary>
        /// 销毁对象
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (_FreezeUIBmp != null)
            {
                _FreezeUIBmp.Dispose();
                _FreezeUIBmp = null;
            }
            base.Dispose(disposing);
        }

        //声明一些API函数
        [DllImport("imm32.dll")]
        public static extern IntPtr ImmGetContext(IntPtr hwnd);
        [DllImport("imm32.dll")]
        public static extern bool ImmGetOpenStatus(IntPtr himc);
        [DllImport("imm32.dll")]
        public static extern bool ImmSetOpenStatus(IntPtr himc, bool b);
        [DllImport("imm32.dll")]
        public static extern bool ImmGetConversionStatus(IntPtr himc, ref int lpdw, ref int lpdw2);
        [DllImport("imm32.dll")]
        public static extern int ImmSimulateHotKey(IntPtr hwnd, int lngHotkey);
        private const int IME_CMODE_FULLSHAPE = 0x8;
        private const int IME_CHOTKEY_SHAPE_TOGGLE = 0x11;

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);

            IntPtr HIme = ImmGetContext(this.Handle);
            if (ImmGetOpenStatus(HIme))  //如果输入法处于打开状态
            {
                int iMode = 0;
                int iSentence = 0;
                bool bSuccess = ImmGetConversionStatus(HIme, ref iMode, ref iSentence);  //检索输入法信息
                if (bSuccess)
                {
                    if ((iMode & IME_CMODE_FULLSHAPE) > 0)   //如果是全角
                    {
                        ImmSimulateHotKey(this.Handle, IME_CHOTKEY_SHAPE_TOGGLE);  //转换成半角
                    }
                }
            }
        }
        
        
        #region 处理边框和滚动的成员 ******************************************
         
        /// <summary>
		/// 发生滚动时的处理
		/// </summary>
		public event System.EventHandler XScroll = null;
		/// <summary>
		/// 触发滚动事件
		/// </summary>
        protected virtual void OnXScroll()
		{
            if( XScroll != null)
				XScroll( this , null);
		}
		/// <summary>
		/// 内部的调用 OnScroll 的接口
		/// </summary>
		public void InnerOnScroll()
		{
			OnXScroll();
		}
		

		/// <summary>
		/// 设置是否可见水平滚动条
		/// </summary>
		/// <param name="flag">设置值</param>
		public void SetHScroll( bool flag)
		{
			base.HScroll = flag ;
		}
		/// <summary>
		/// 设置是否可见垂直滚动条
		/// </summary>
		/// <param name="flag">设置值</param>
		public void SetVScroll( bool flag )
		{
			base.VScroll = flag ;
		}
		/// <summary>
		/// 已重载:处理Windows底层消息,此处用于判断是否触发 Scroll 滚动事件
		/// </summary>
		/// <param name="m">Windows消息结构体</param>
		protected override void WndProc(ref Message m)
		{
            //base.WndProc(ref m);
            if (m.Msg == 0x020A)//(int) DCSoft.Win32.Msgs.WM_MOUSEWHEEL )
            {
                this.OnXScroll();
            }
            else if (m.HWnd == this.Handle)
            {
                if (m.Msg == 0x0114 //(int)DCSoft.Win32.Msgs.WM_HSCROLL 
                    || m.Msg == 0x0115)//(int)DCSoft.Win32.Msgs.WM_VSCROLL )
                {
                    int v = m.WParam.ToInt32();
                    if ((v & 0xf) == 5)
                    {
                        // 滚动消息是 THUMBTRACK 类型
                        base.WndProc(ref m);
                        if ( bolDragFullWindows == false)
                        {
                            // Windows操作系统没有设置为拖动时显示窗口内容
                            // 则重复执行 THUMBPOSITION 类型的滚动消息
                            v = v - 1;
                            m.WParam = new IntPtr(v);
                            base.WndProc(ref m);
                        }
                        this.OnXScroll();
                    }
                    else
                    {
                        base.WndProc(ref m);
                        this.OnXScroll();
                    }
                    return;
                    //System.Console.WriteLine("Scroll" + m.Msg );
                }
                else if (m.Msg == (int) Msgs.WM_PAINT)
                {
                    //System.Console.WriteLine("Paint");
                }
            }
 
            base.WndProc(ref m);
		}

        private bool bolDragFullWindows = false;

        protected override void OnHandleCreated(EventArgs e)
        {
            //new System.Security.Permissions.UIPermission(System.Security.Permissions.PermissionState.Unrestricted).Assert();
            bolDragFullWindows = SystemParametersInfoClass.DragFullWindows;
            base.OnHandleCreated(e);
            //System.Security.Permissions.EnvironmentPermission.RevertAssert();
        }
		#endregion

	}//public class BorderUserControl : System.Windows.Forms.UserControl
}