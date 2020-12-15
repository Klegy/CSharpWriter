/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DCSoft.WinForms.Native;

namespace DCSoft.WinForms
{
    /// <summary>
    /// 弹出式窗体
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    public class XPopupForm : Form , IMessageFilter
    {
        public XPopupForm()
        {
            this.TopMost = true;
            this.ShowInTaskbar = false;
            this.ControlBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                this.SetStyle(ControlStyles.Selectable, this.CanGetFocus);
                return base.CreateParams;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Leave += new System.EventHandler(this.CheckToHide);
            this.Deactivate += new System.EventHandler(this.CheckToHide); 
            base.OnLoad(e);
        }

        private void CheckToHide(object sender, EventArgs e)
        {
            if (this.CanGetFocus)
            {
                this.Hide();
            }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (this.Visible == false)
            {
                System.Windows.Forms.Application.RemoveMessageFilter(this);
            }
        }

        private bool _AutoClose = true;
        /// <summary>
        /// 用户选择操作后是否自动关闭窗体
        /// </summary>
        [System.ComponentModel.DefaultValue(true)]
        public bool AutoClose
        {
            get
            {
                return _AutoClose; 
            }
            set
            {
                _AutoClose = value; 
            }
        }

        private bool _CanGetFocus = false;
        /// <summary>
        /// 弹出式窗口能否获得焦点
        /// </summary>
        [System.ComponentModel.DefaultValue( false )]
        public bool CanGetFocus
        {
            get
            { 
                return _CanGetFocus;
            }
            set
            {
                _CanGetFocus = value;
            }
        }

         
        private System.Windows.Forms.Control _ContentControl = null;
        /// <summary>
        /// 内容控件
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public System.Windows.Forms.Control ContentControl
        {
            get
            {
                return _ContentControl; 
            }
            set
            {
                _ContentControl = value; 
            }
        }

        /// <summary>
        /// 运行时使用的内容控件
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public System.Windows.Forms.Control RuntimeContentControl
        {
            get
            {
                if (this._ContentControl == null)
                {
                    if (this.Controls.Count > 0)
                    {
                        return this.Controls[0];
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return this._ContentControl;
                }
            }
        }

        /// <summary>
        /// 退出等待用户操作的标记
        /// </summary>
        private bool bolExitLoop = true;
        /// <summary>
        /// 用户确认操作的标记
        /// </summary>
        private bool bolUserAccept = false;


        /// <summary>
        /// 对象是否截获程序的Windows消息以便等待用户处理
        /// </summary>
        public bool WaitingForUserSelected
        {
            get
            {
                return !bolExitLoop; 
            }
            set
            {
                bolExitLoop = !value; 
            }
        }

        protected override void Dispose(bool disposing)
        {
            Application.RemoveMessageFilter( this );
            base.Dispose(disposing);
        }


        private System.Windows.Forms.Control _OwnerControl = null;
        /// <summary>
        /// 弹出该窗体的控件
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public System.Windows.Forms.Control OwnerControl
        {
            get
            {
                return _OwnerControl;
            }
            set
            {
                _OwnerControl = value;
            }
        }


        /// <summary>
        /// 设置列表位置的在使用屏幕坐标的写作区域
        /// </summary>
        private System.Drawing.Rectangle myCompositionRect 
            = System.Drawing.Rectangle.Empty;
        /// <summary>
        /// 设置列表位置的在使用屏幕坐标的写作区域
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public System.Drawing.Rectangle CompositionRect
        {
            get
            {
                return myCompositionRect; 
            }
            set
            {
                myCompositionRect = value; 
            }
        }

        /// <summary>
        /// 更新窗体位置
        /// </summary>
        public void UpdateComposition()
        {
            System.Drawing.Rectangle rect = this.CompositionRect;
            if (myCompositionRect.IsEmpty == false)
            {
                if (this.OwnerControl != null)
                {
                    rect.Location = this.OwnerControl.PointToScreen(rect.Location);
                }
                //this.SetDefaultSize();
                System.Drawing.Point p = this.GetPopupPos(
                    rect.Left,
                    rect.Top,
                    rect.Height);
                this.Location = p;
            }
        }


        /// <summary>
        /// 为指定区域计算弹出式列表的位置,保证弹出式列表全部显示将全部显示在屏幕上并不会覆盖指定的区域
        /// </summary>
        /// <remarks>本函数模仿Windows弹出式菜单的显示位置的设置,在可能的情况下,
        /// 弹出式列表的左上角列表位于指定区域的左下角,但若指定的显示区域过下,
        /// 以致该区域下方的屏幕不足以显示全部列表则显示的列表的左下角就设置在指定区域的左上角
        /// 若列表过右,以至于右边没有足够的空间显示全部列表则列表的右边缘就设置在屏幕的右编译</remarks>
        /// <param name="x">指定区域在屏幕上的左端位置</param>
        /// <param name="y">指定区域在屏幕上的顶端位置</param>
        /// <param name="height">指定区域的高度</param>
        /// <returns>计算所得的弹出式列表的最佳位置</returns>
        public System.Drawing.Point GetPopupPos(int x, int y, int height)
        {
            // 获得屏幕大小
            int ScreenWidth = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            int ScreenHeight = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            int intWidth = this.Width;
            int intHeight = this.Height;
            // 设置默认位置为指定区域的下边缘
            System.Drawing.Point pos = new System.Drawing.Point(x, y + height);
            if (pos.X < 0)
                pos.X = 0;
            if (pos.Y < 0)
                pos.Y = 0;
            if (y + height + intHeight > ScreenHeight)
            {
                // 若列表过低,不能全部显示则列表显示在指定区域的上方
                pos.Y = y - intHeight;
            }
            if (x + intWidth > ScreenWidth)
            {
                // 若列表过右则向左移动
                pos.X = ScreenWidth - intWidth;
            }
            return pos;
        }

        public void Show(System.Windows.Forms.Control ctl, int x, int y, int height)
        {
            this.OwnerControl = ctl;
            this.CompositionRect = new System.Drawing.Rectangle(x, y, 0, height);
            this.UpdateComposition();
            this.Show();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if ( this.Visible )
            {
                UpdateComposition();
            }
        }

        /// <summary>
        /// 显示弹出式列表项目
        /// </summary>
        /// <param name="x">弹出的列表的左上角在屏幕上的X坐标</param>
        /// <param name="y">弹出的列表的左上角在屏幕上的Y坐标</param>
        /// <param name="owner">弹出列表的窗体</param>
        /// <returns>用户选择的项目,-1表示没有选中</returns>
        public int ShowPopupList(int x, int y, System.Windows.Forms.Form owner)
        {
            this.Location = new System.Drawing.Point(x, y);
            this.Show();
            if (owner != null)
                owner.Activate();
            return 0;
        }

        //private UserProcessState _UserProcessState = UserProcessState.Processing ;
        ///// <summary>
        ///// 用户选择状态
        ///// </summary>
        //public virtual UserProcessState UserProcessState
        //{
        //    get { return _UserProcessState; }
        //    set { _UserProcessState = value; }
        //}

        //private UserProcessState _DefaultUserProcessState = UserProcessState.Accept;
        ///// <summary>
        ///// 默认的选择状态
        ///// </summary>
        //public virtual UserProcessState DefaultUserProcessState
        //{
        //    get { return _DefaultUserProcessState; }
        //    set { _DefaultUserProcessState = value; }
        //}
        
        /// <summary>
        /// 等待用户的选择操作,本函数将不返回直到用户确定选择或取消选择
        /// </summary>
        /// <remarks>本函数向当前应用程序添加自定义的消息过滤器,并开始循环处理当前应用程序的消息,若用户确定或取消的选择操作则退出循环处理</remarks>
        /// <returns>用户是否选择的某个项目</returns>
        public virtual void WaitUserSelected()
        {
            //Win32.NativeMSG msg = new Win32.NativeMSG();
            // Process messages until exit condition recognised
            bolExitLoop = false;
            bolUserAccept = false;

            MessageFilter.ExcludePaintMessageFilter.Enabled = true;
            Win32Message.ClearMessage();
            //System.Windows.Forms.Application.DoEvents();
            MessageFilter.ExcludePaintMessageFilter.Enabled = false;

            System.Windows.Forms.Application.AddMessageFilter(this);
            //this.UserProcessState = WinForms.UserProcessState.Processing;
            UserProcessState state = UserProcessState.Processing ;
            WindowInformation info = new WindowInformation(this);
            while ( Win32Message.Wait())
            {
                //User32.GetMessage(ref msg, 0, 0, 0);
                //				Windows32.User32.GetMessage( ref msg , 0 , 0 , 0 );
                //				Windows32.User32.TranslateMessage( ref msg );
                //				Windows32.User32.DispatchMessage( ref msg );
                System.Windows.Forms.Application.DoEvents();
                //state = this.UserProcessState ;
                if (this.Visible == false)
                {
                    break;
                }
                if (state == UserProcessState.Accept
                    || state == UserProcessState.Cancel)
                {
                    break;
                }
                if (bolExitLoop)
                {
                    break;
                }
            }//while
            System.Windows.Forms.Application.RemoveMessageFilter(this);
            Win32Message.ClearMessage();
            //if (state == UserProcessState.Processing)
            //{
            //    state = this.DefaultUserProcessState;
            //}
            if (this.AutoClose)
            {
                this.Hide();
            }
            //return state == UserProcessState.Accept ;
        }

        /// <summary>
        /// 隐藏列表
        /// </summary>
        public void CloseList()
        {
            //if (this.UserProcessState == WinForms.UserProcessState.Processing)
            //{
            //    this.UserProcessState = this.DefaultUserProcessState;
            //}
            bolExitLoop = true;
            System.Windows.Forms.Application.RemoveMessageFilter(this);
            this.Hide();
            if ((int)msgBack.HWnd != 0)
            {
                User32.PostMessage(
                    msgBack.HWnd,
                    (int)msgBack.Msg, 
                    (uint)msgBack.WParam,
                    (uint)msgBack.LParam);
                msgBack.HWnd = System.IntPtr.Zero;
            }
        }

        /// <summary>
        /// 重载本窗口的消息处理函数,处理WM_ACTIVATEAPP,WM_MOUSEACTIVATE消息
        /// </summary>
        /// <param name="m">当前的Windows消息对象</param>
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            switch (m.Msg)
            {
                case (int)Msgs.WM_ACTIVATEAPP:
                    if (this.CanGetFocus == false)
                    {
                        bolExitLoop = true;
                    }
                    break;
                case (int)Msgs.WM_MOUSEACTIVATE:
                    if (this.CanGetFocus == false)
                    {
                        m.Result = (IntPtr)MouseActivateFlags.MA_NOACTIVATE;
                    }
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
		

        Message msgBack = new Message();


        #region IMessageFilter 成员
        /// <summary>
        /// 本函数实现 System.Windows.Forms.IMessageFilter接口的PreFilterMessage函数,用于对当前应用程序的消息进行预处理
        /// </summary>
        /// <remarks>本过滤器处理了鼠标按钮按下时间和键盘事件以及鼠标滚轮事件,
        /// 若鼠标在本列表边框外部按下则表示认为用户想要关闭列表,此时立即关闭本列表窗体
        /// 此外本函数还处理汉语拼音辅助定位列表元素
        /// 用户可用上下光标键或者上下翻页键来滚动列表,用空格或回车来进行选择</remarks>
        /// <param name="m">当前消息队列中的消息对象</param>
        /// <returns>true 当前消息其他程序不可处理(本消息被吃掉了) false本消息还可被其他程序处理</returns>
        public virtual bool PreFilterMessage(ref System.Windows.Forms.Message m)
        {
            bool eatMessage = false;
            if (this.IsDisposed || this.Disposing || this.Visible == false)
            {
                // 若对象被销毁了或列表不显示则删除本消息过滤器
                System.Windows.Forms.Application.RemoveMessageFilter(this);
                return false;
            }

            Win32Message msg = new Win32Message(m);
            // 获得窗体的绝对位置和大小
            System.Drawing.Rectangle BoundsRect = this.Bounds;

            // 鼠标在客户区的按键按下消息
            // Mouse was pressed in a window of this application
            if( msg.IsMouseDownMessage )

            if (msg.Msg == (int)Msgs.WM_LBUTTONDOWN ||
                msg.Msg == (int)Msgs.WM_MBUTTONDOWN ||
                msg.Msg == (int)Msgs.WM_RBUTTONDOWN ||
                msg.Msg == (int)Msgs.WM_XBUTTONDOWN)
            {

                System.Drawing.Point mousePos = msg.ScreenMousePosition ;
                bolExitLoop = !BoundsRect.Contains(mousePos);
                msgBack = m;
                eatMessage = bolExitLoop;
                if (bolExitLoop)
                {
                    if (IsChildWindow(m.HWnd))
                    {
                        bolExitLoop = false;
                    }
                    WindowInformation info = new WindowInformation(this.Handle);
                    if (info.Enabled == false)
                    {
                        // 窗体不可用，很可能正在显示对话框使得本窗体暂时不可用
                        bolExitLoop = false;
                    }
                    //else
                    //{
                    //    this.UserProcessState = WinForms.UserProcessState.Accept;
                    //}
                    return true;
                }
            }
            // 鼠标在非客户区的按键按下消息
            // Mouse was pressed in a window of this application
            if ((m.Msg == (int)Msgs.WM_NCLBUTTONDOWN) ||
                (m.Msg == (int)Msgs.WM_NCMBUTTONDOWN) ||
                (m.Msg == (int)Msgs.WM_NCRBUTTONDOWN) ||
                (m.Msg == (int)Msgs.WM_NCXBUTTONDOWN))
            {
                System.Drawing.Point mousePos = msg.MousePosition;
                bolExitLoop = !BoundsRect.Contains(mousePos);
                //eatMessage = true;
                msgBack = m;
                eatMessage = bolExitLoop;
                if (bolExitLoop)
                {
                    if (IsChildWindow(m.HWnd))
                    {
                        bolExitLoop = false;
                    }
                    WindowInformation info = new WindowInformation(this.Handle);
                    if (info.Enabled == false)
                    {
                        // 窗体不可用，很可能正在显示对话框使得本窗体暂时不可用
                        bolExitLoop = false;
                    }
                    //else
                    //{
                    //    this.UserProcessState = WinForms.UserProcessState.Accept;
                    //}
                    return true;
                }
            }
            if (this.RuntimeContentControl is IMessageFilter)
            {
                WindowInformation info = new WindowInformation(this.Handle);
                //if (info.Enabled)
                {
                    IMessageFilter mf = (IMessageFilter)this.RuntimeContentControl;
                    return mf.PreFilterMessage(ref m);
                }
            }
            return eatMessage;
        }


        [ System.Runtime.InteropServices.DllImport("User32.dll")]
        private static extern bool IsChild(IntPtr parentHwnd, IntPtr hwnd);


        private bool IsChildWindow(System.IntPtr hwnd)
        {
            if (IsChild(this.Handle, hwnd))
            {
                return true;
            }

            Form[] fs = this.OwnedForms;
            if (fs != null && fs.Length > 0 )
            {
                foreach (Form frm in fs)
                {
                    if (frm.Handle == hwnd)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        #endregion
    }

    /// <summary>
    /// 用户操作状态
    /// </summary>
    public enum UserProcessState
    {
        /// <summary>
        /// 正在处理
        /// </summary>
        Processing ,
        /// <summary>
        /// 用户确定操作
        /// </summary>
        Accept ,
        /// <summary>
        /// 用户取消操作
        /// </summary>
        Cancel ,
    }
}
