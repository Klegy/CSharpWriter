/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using System.Runtime.InteropServices;
using System.Drawing;

namespace DCSoft.WinForms.Native
{
    /// <summary>
    /// 可逆图形样式
    /// </summary>
    public enum ReversibleShapeStyle
    {
        /// <summary>
        /// 可逆的直线
        /// </summary>
        Line,
        /// <summary>
        /// 可逆矩形边框
        /// </summary>
        Rectangle,
        /// <summary>
        /// 椭圆形
        /// </summary>
        Ellipse,
        /// <summary>
        /// 可逆的填充的矩形
        /// </summary>
        FillRectangle,
        /// <summary>
        /// 自定义,触发事件
        /// </summary>
        Custom
    }

    /// <summary>
    /// 鼠标拖拽回调函数委托类型
    /// </summary>
    public delegate void CaptureMouseMoveEventHandler(object sender, CaptureMouseMoveEventArgs e);
    /// <summary>
    /// 鼠标拖拽事件消息对象
    /// </summary>
    public class CaptureMouseMoveEventArgs : System.EventArgs
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="sender">消息发送者</param>
        /// <param name="sp">开始点坐标</param>
        /// <param name="cp">当前点坐标</param>
        public CaptureMouseMoveEventArgs(
            MouseCapturer sender,
            System.Drawing.Point sp,
            System.Drawing.Point cp)
        {
            this.mySender = sender;
            this.myStartPosition = sp;
            this.myCurrentPosition = cp;
            //this.bolCancel = false ;
        }

        private object _Tag = null;
        /// <summary>
        /// 额外数据
        /// </summary>
        public object Tag
        {
            get { return _Tag; }
            set { _Tag = value; }
        }

        private MouseCapturer mySender = null;
        /// <summary>
        /// 消息发送者
        /// </summary>
        public MouseCapturer Sender
        {
            get
            {
                return mySender; 
            }
        }

        private System.Drawing.Point myStartPosition = System.Drawing.Point.Empty;
        /// <summary>
        /// 鼠标开始拖拽的点坐标
        /// </summary>
        public System.Drawing.Point StartPosition
        {
            get
            {
                return myStartPosition; 
            }
            set
            {
                myStartPosition = value; 
            }
        }

        private System.Drawing.Point myCurrentPosition = System.Drawing.Point.Empty;
        /// <summary>
        /// 鼠标当前点坐标
        /// </summary>
        public System.Drawing.Point CurrentPosition
        {
            get
            {
                return myCurrentPosition; 
            }
            set
            {
                myCurrentPosition = value; 
            }
        }

        /// <summary>
        /// 当前横向移动的距离
        /// </summary>
        public int DX
        {
            get
            {
                return myCurrentPosition.X - myStartPosition.X; 
            }
        }

        /// <summary>
        /// 当前纵向移动的距离
        /// </summary>
        public int DY
        {
            get
            {
                return myCurrentPosition.Y - myStartPosition.Y; 
            }
        }

        private bool bolResumeView = false;
        /// <summary>
        /// 绘图操作是否是恢复原始视图
        /// </summary>
        public bool ResumeView
        {
            get
            {
                return bolResumeView; 
            }
            set
            {
                bolResumeView = value; 
            }
        }
        //private bool bolCancel = false;
        /// <summary>
        /// 是否取消拖拽
        /// </summary>
        public bool Cancel
        {
            get
            {
                return mySender.CancelFlag; 
            }
            set
            {
                mySender.CancelFlag = value; 
            }
        }
    }//public class CaptureMouseMoveEventArgs

    /// <summary>
    /// 绘制矩形的鼠标拖拽对象
    /// </summary>
    /// <remarks>编写 袁永福</remarks>
    public class RectangleMouseCapturer : MouseCapturer
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public RectangleMouseCapturer()
        {
        }
        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="ctl">进行鼠标拖拽的控件对象</param>
        public RectangleMouseCapturer(System.Windows.Forms.Control ctl)
        {
            myBindControl = ctl;
        }
        protected int intDragStyle = 0;
        /// <summary>
        /// 拖拽类型
        /// </summary>
        public int DragStyle
        {
            get { return intDragStyle; }
            set { intDragStyle = value; }
        }
        protected System.Drawing.Rectangle mySourceRectangle = System.Drawing.Rectangle.Empty;
        /// <summary>
        /// 原始矩形
        /// </summary>
        public System.Drawing.Rectangle SourceRectangle
        {
            get { return mySourceRectangle; }
            set { mySourceRectangle = value; }
        }

        protected System.Drawing.Rectangle myDescRectangle = System.Drawing.Rectangle.Empty;
        /// <summary>
        /// 处理后的矩形
        /// </summary>
        public System.Drawing.Rectangle DescRectangle
        {
            get { return myDescRectangle; }
            set { myDescRectangle = value; }
        }

        protected bool bolCustomAction = false;
        /// <summary>
        /// 自定义动作样式
        /// </summary>
        public bool CustomAction
        {
            get { return bolCustomAction; }
            set { bolCustomAction = value; }
        }

        /// <summary>
        /// 更新矩形
        /// </summary>
        /// <param name="rect">原始矩形</param>
        /// <param name="dx">水平移动量</param>
        /// <param name="dy">垂直移动量</param>
        /// <returns>处理后的矩形</returns>
        public System.Drawing.Rectangle UpdateRectangle(System.Drawing.Rectangle rect, int dx, int dy)
        {
            // 中间
            if (intDragStyle == -1)
                rect.Offset(dx, dy);
            // 左边
            if (intDragStyle == 0 || intDragStyle == 7 || intDragStyle == 6)
            {
                rect.Offset(dx, 0);
                rect.Width = rect.Width - dx;
            }
            // 顶边
            if (intDragStyle == 0 || intDragStyle == 1 || intDragStyle == 2)
            {
                rect.Offset(0, dy);
                rect.Height = rect.Height - dy;
            }
            // 右边
            if (intDragStyle == 2 || intDragStyle == 3 || intDragStyle == 4)
            {
                rect.Width = rect.Width + dx;
            }
            // 底边
            if (intDragStyle == 4 || intDragStyle == 5 || intDragStyle == 6)
            {
                rect.Height = rect.Height + dy;
            }
            return rect;
        }

        /// <summary>
        /// 绘制可逆矩形
        /// </summary>
        protected override void OnDraw(bool ResumeView)
        {
            base.OnDraw(ResumeView);
            if (bolCustomAction)
                return;
            ReversibleDrawer drawer = null;
            if (myBindControl != null)
                drawer = ReversibleDrawer.FromHwnd(myBindControl.Handle);
            else
                drawer = ReversibleDrawer.FromScreen();
            drawer.DrawRectangle(myDescRectangle);
            drawer.Dispose();
        }
        /// <summary>
        /// 当前拖拽的矩形区域
        /// </summary>
        public System.Drawing.Rectangle CurrentRectangle
        {
            get
            {
                return MouseCapturer.GetRectangle(this.StartPosition, this.CurrentPosition);
            }
        }

        /// <summary>
        /// 处理鼠标移动事件
        /// </summary>
        protected override void OnMouseMove()
        {
            base.OnMouseMove();
            if (bolCustomAction)
                return;
            int dx = base.CurrentPosition.X - base.StartPosition.X;
            int dy = base.CurrentPosition.Y - base.StartPosition.Y;
            this.myDescRectangle = UpdateRectangle(this.mySourceRectangle, dx, dy);
        }
    }

    /// <summary>
    /// 捕获鼠标的模块
    /// </summary>
    /// <remarks>编写 袁永福</remarks>
    public class MouseCapturer
    {
        /// <summary>
        /// 无作为的初始化对象
        /// </summary>
        public MouseCapturer()
        {
        }
        /// <summary>
        /// 初始化对象并设置绑定的控件
        /// </summary>
        /// <param name="ctl">绑定的控件</param>
        public MouseCapturer(System.Windows.Forms.Control ctl)
        {
            myBindControl = ctl;
        }

        protected System.Windows.Forms.Control myBindControl = null;
        /// <summary>
        /// 对象绑定的控件,若该控件有效则鼠标光标是用控件客户区坐标,否则采用屏幕坐标
        /// </summary>
        public System.Windows.Forms.Control BindControl
        {
            get { return myBindControl; }
            set { myBindControl = value; }
        }
        private System.Drawing.Point myInitStartPosition = System.Drawing.Point.Empty;
        /// <summary>
        /// 初始化时的鼠标开始位置
        /// </summary>
        public System.Drawing.Point InitStartPosition
        {
            get { return this.myInitStartPosition; }
            set { this.myInitStartPosition = value; }
        }
        private System.Drawing.Point myStartPosition = System.Drawing.Point.Empty;
        /// <summary>
        /// 开始捕获时的鼠标光标的位置
        /// </summary>
        public System.Drawing.Point StartPosition
        {
            get
            {
                return myStartPosition; 
            }
            set
            {
                myStartPosition = value;
            }
        }
        private System.Drawing.Point myEndPosition = System.Drawing.Point.Empty;
        /// <summary>
        /// 结束捕获时鼠标光标位置
        /// </summary>
        public System.Drawing.Point EndPosition
        {
            get { return myEndPosition; }
        }
        private System.Drawing.Point myLastPosition = System.Drawing.Point.Empty;
        /// <summary>
        /// 上一次处理时鼠标光标位置
        /// </summary>
        public System.Drawing.Point LastPosition
        {
            get { return myLastPosition; }
        }
        private System.Drawing.Point myCurrentPosition = System.Drawing.Point.Empty;
        /// <summary>
        /// 当前鼠标光标的位置
        /// </summary>
        public System.Drawing.Point CurrentPosition
        {
            get { return myCurrentPosition; }
        }
        private System.Drawing.Size myMoveSize = System.Drawing.Size.Empty;
        /// <summary>
        /// 整个操作中鼠标移动的距离,属性的Width值表示光标横向移动的距离,Height值表示纵向移动距离
        /// </summary>
        public System.Drawing.Size MoveSize
        {
            get { return myMoveSize; }
            set { myMoveSize = value; }
        }
        /// <summary>
        /// 整个操作中鼠标横向移动距离
        /// </summary>
        public int DX
        {
            get 
            {
                return this.myEndPosition.X - this.myStartPosition.X; 
            }
        }
        /// <summary>
        /// 整个操作中鼠标纵向移动距离
        /// </summary>
        public int DY
        {
            get
            {
                return this.myEndPosition.Y - this.myStartPosition.Y; 
            }
        }
        /// <summary>
        /// 鼠标移动起点和终点组成的矩形区域
        /// </summary>
        public System.Drawing.Rectangle CaptureRectagle
        {
            get
            {
                // 根据起点坐标和终点坐标确定选择区域矩形
                System.Drawing.Rectangle rect = GetRectangle(myStartPosition, myEndPosition);
                //rect.Location = this.FixPointForControl(rect.Location);
                return rect;
            }
        }

        //internal static Rectangle GetRectangle(Point p1, Point p2)
        //{
        //    Rectangle rect = new Rectangle(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y), 0, 0);
        //    rect.Width = Math.Max(p1.X, p2.X) - rect.Left;
        //    rect.Height = Math.Max(p1.Y, p2.Y) - rect.Top;
        //    return rect;
        //}
        private System.Drawing.Rectangle myClipRectangle = System.Drawing.Rectangle.Empty;
        /// <summary>
        /// 鼠标光标的活动范围
        /// </summary>
        public System.Drawing.Rectangle ClipRectangle
        {
            get 
            {
                return myClipRectangle; 
            }
            set
            {
                myClipRectangle = value; 
            }
        }

        protected virtual CaptureMouseMoveEventArgs CreateArgs()
        {
            CaptureMouseMoveEventArgs e = new CaptureMouseMoveEventArgs(
                this,
                this.myStartPosition,
                this.myCurrentPosition);
            return e;
        }


        /// <summary>
        /// 鼠标捕获期间移动时的回调处理事件
        /// </summary>
        public event CaptureMouseMoveEventHandler MouseMove = null;

        /// <summary>
        /// 鼠标捕获期间移动时的回调处理
        /// </summary>
        protected virtual void OnMouseMove()
        {
            if (MouseMove != null)
                MouseMove(this, this.CreateArgs());
        }
        /// <summary>
        /// 鼠标捕获期间绘制可逆图形的回调处理事件
        /// </summary>
        public event CaptureMouseMoveEventHandler Draw = null;

        protected virtual void OnDraw(bool ResumeView)
        {
            if (this.ReversibleShape == ReversibleShapeStyle.Custom)
            {
                if (Draw != null)
                {
                    CaptureMouseMoveEventArgs args = this.CreateArgs();
                    args.ResumeView = ResumeView;
                    Draw(this, args);
                }
            }
            else
            {
                ReversibleDrawer drawer = null;
                if (myBindControl == null)
                    drawer = ReversibleDrawer.FromScreen();
                else
                    drawer = ReversibleDrawer.FromHwnd(myBindControl.Handle);

                drawer.PenStyle = PenStyle.PS_SOLID;
                drawer.PenColor = Color.White;
                drawer.LineWidth = 1;

                Rectangle rect = GetRectangle(this.StartPosition, this.CurrentPosition);
                switch (this.ReversibleShape)
                {
                    case ReversibleShapeStyle.Rectangle:
                        drawer.DrawRectangle(rect);
                        break;
                    case ReversibleShapeStyle.Ellipse:
                        drawer.DrawEllipse(rect);
                        break;
                    case ReversibleShapeStyle.Line:
                        drawer.DrawLine(this.StartPosition, this.CurrentPosition);
                        break;
                    case ReversibleShapeStyle.FillRectangle:
                        drawer.FillRectangle(rect);
                        break;
                }
                drawer.Dispose();
            }
        }



        /// <summary>
        /// 鼠标捕获期间绘制可逆图形的回调处理
        /// </summary>
        protected virtual void OnReversibleDrawCallback()
        {
            System.Drawing.Rectangle rect = GetRectangle( this.myStartPosition, this.myCurrentPosition);
            switch (intReversibleShape)
            {
                case ReversibleShapeStyle.Line:
                    System.Windows.Forms.ControlPaint.DrawReversibleLine(
                        this.myStartPosition,
                        this.myCurrentPosition, System.Drawing.Color.Black);
                    break;
                case ReversibleShapeStyle.Rectangle:
                    System.Windows.Forms.ControlPaint.DrawReversibleFrame(
                        rect,
                        System.Drawing.Color.SkyBlue,
                        System.Windows.Forms.FrameStyle.Dashed);
                    break;
                case ReversibleShapeStyle.FillRectangle:
                    System.Windows.Forms.ControlPaint.FillReversibleRectangle(
                        rect,
                        System.Drawing.Color.Black);
                    break;
                case ReversibleShapeStyle.Custom:
                    if (Draw != null)
                        Draw(this, null);
                    break;
            }
        }

        private ReversibleShapeStyle intReversibleShape = ReversibleShapeStyle.Custom;
        /// <summary>
        /// 可逆图形样式
        /// </summary>
        public ReversibleShapeStyle ReversibleShape
        {
            get
            {
                return intReversibleShape;
            }
            set
            {
                intReversibleShape = value;
            }
        }

        private object objTag = null;
        /// <summary>
        /// 对象额外数据
        /// </summary>
        public object Tag
        {
            get
            {
                return objTag;
            }
            set
            {
                objTag = value;
            }
        }
        /// <summary>
        /// 重新设置内部数据
        /// </summary>
        public void Reset()
        {
            if (this.myInitStartPosition.IsEmpty)
                this.myStartPosition = this.CurMousePosition;
            else
                this.myStartPosition = this.myInitStartPosition;

            this.myLastPosition = myStartPosition;
            this.myCurrentPosition = myStartPosition;
            this.myEndPosition = myStartPosition;
            this.myMoveSize = System.Drawing.Size.Empty;
        }
        private bool bolCancelFlag = false;
        public bool CancelFlag
        {
            get { return bolCancelFlag; }
            set { bolCancelFlag = value; }
        }

        /// <summary>
        /// 捕获鼠标移动
        /// </summary>
        /// <returns>是否完成操作</returns>
        public bool CaptureMouseMove()
        {
            return CaptureMouseMove(true);
        }

        /// <summary>
        /// 捕获鼠标移动
        /// </summary>
        /// <param name="needDragDetect">是否事先检测拖拽操作</param>
        /// <returns>是否成功的完成了操作</returns>
        public bool CaptureMouseMove(bool needDragDetect)
        {
            Reset();
            MSG msg = new MSG();
            int MinDragSize = System.Windows.Forms.SystemInformation.DragSize.Width;
            bool DragStartFlag = false;

            if (System.Windows.Forms.Control.MouseButtons == System.Windows.Forms.MouseButtons.None)
                return false;

            System.Drawing.Point curPoint = this.CurMousePosition;

            bolCancelFlag = false;
            //if (this.BindControl != null)
            //{
            //    POINT p = new POINT();
            //    p.x = System.Windows.Forms.Control.MousePosition.X;
            //    p.y = System.Windows.Forms.Control.MousePosition.Y;
            //    if( DragDetect( this.BindControl.Handle , p ) == false )
            //    {
            //        return false;
            //    }
            //}
            // 开始Windows消息处理
            bool startDraw = false;
            //return false;
            System.Windows.Forms.Cursor myCursorBack = null;
            if (this.BindControl != null)
            {
                myCursorBack = this.BindControl.Cursor;
                if (needDragDetect)
                {
                    if (DragDetect(this.BindControl.Handle) == false)
                    {
                        return false;
                    }
                }
                this.BindControl.Cursor = myCursorBack;
                this.BindControl.Capture = true;
                DragStartFlag = true;
            }
            else
            {
 
                System.Windows.Forms.Application.DoEvents();
                DragStartFlag = true;
            }

            int msgCount = 0;
            while (true)
            {
                //MsgWaitForMultipleObjects(1, 0, true, 20, 0xff);
                MsgWaitForMultipleObjectsEx(0, IntPtr.Zero, 250, 255, 4);
                if (PeekMessage(ref msg, 0, 0, 0, (int)PeekMessageFlags.PM_NOREMOVE) == false)
                {
                    continue;
                }
                msgCount++;
                //System.Console.WriteLine( msgCount + ":" + msg.message.ToString() + " " + msg.wParam.ToString("x") + " " + msg.lParam.ToString("x"));

                //GetMessage(ref msg, 0, 0, 0);
                ////System.Windows.Forms.Application.DoEvents();
                //TranslateMessage(ref msg);
                //DispatchMessage(ref msg);
                //System.Windows.Forms.Application.DoEvents();


                if (System.Windows.Forms.Control.MouseButtons
                    == System.Windows.Forms.MouseButtons.None)
                {
                    break;
                }
                if (bolCancelFlag)
                {
                    break;
                }

                // 若当前消息为鼠标按键松开消息则退出循环
                if (MouseMessageHelper.IsMouseUpMessage(msg.message ))
                {
                    //curPoint.X = MouseMessageHelper.GetX( msg.lParam );
                    //curPoint.Y = MouseMessageHelper.GetY(msg.lParam);
                    ///////curPoint = this.CurMousePosition2;
                    //GetMessage(ref msg, 0, 0, 0);
                    GetMessage(ref msg, 0, 0, 0);
                    break;
                }

                System.Windows.Forms.MouseButtons button = System.Windows.Forms.MouseButtons.Left;
                if (MouseMessageHelper.IsMouseMessage(msg.message ))
                {
                    button = MouseMessageHelper.GetMouseButtons(msg.wParam.ToInt32());
                }
                if (MouseMessageHelper.IsMouseMoveMessage(msg.message ))
                {
                    // 若为鼠标移动消息则进行处理
                    curPoint = this.CurMousePosition2;
                    System.Drawing.Point p = curPoint;// this.CurMousePosition2;// MouseMessageHelper.GetPoint(msg.lParam);

                    if (p.X != this.myCurrentPosition.X || p.Y != this.myCurrentPosition.Y)
                    {
                        if (startDraw)
                        {
                            this.OnDraw(true);
                        }
                        this.myCurrentPosition = p;
                        if (DragStartFlag == false)
                        {
                            if (System.Math.Abs(this.myCurrentPosition.X - this.myStartPosition.X) >= MinDragSize
                                || System.Math.Abs(this.myCurrentPosition.Y - this.myStartPosition.Y) >= MinDragSize)
                                DragStartFlag = true;
                        }
                        if (DragStartFlag)
                        {
                            this.myCurrentPosition = p;
                            this.OnDraw(false);
                            startDraw = true;
                            this.OnMouseMove();
                            this.myLastPosition = this.myCurrentPosition;
                        }
                    }
                    GetMessage(ref msg, 0, 0, 0);
                }
                else
                {
                    GetMessage(ref msg, 0, 0, 0);
                    if (msg.message == (int)Msgs.WM_LBUTTONUP
                        || msg.message == (int)Msgs.WM_RBUTTONUP)
                    {
                        // 鼠标松开事件
                        break;
                    }
                    if (msg.message !=  (int) Msgs.WM_PAINT )
                    {
                        // 不是重绘消息
                        ////System.Windows.Forms.Application.DoEvents();
                        TranslateMessage(ref msg);
                        DispatchMessage(ref msg);
                    }
                }

                if (button == System.Windows.Forms.MouseButtons.None)
                {
                    break;
                }
            }// while( User32.WaitMessage() )

            this.myCurrentPosition = curPoint;

            if (startDraw)
            {
                this.OnDraw(true);
            }
            this.myEndPosition = this.myCurrentPosition;
            this.myMoveSize = new System.Drawing.Size(
                myEndPosition.X - myStartPosition.X,
                myEndPosition.Y - myStartPosition.Y);
            if (this.BindControl != null && myCursorBack != null)
            {
                this.BindControl.Cursor = myCursorBack;
                this.BindControl.Capture = false;
            }
            if (this.bolCancelFlag)
                return false;
            if (myMoveSize.Width == 0 && myMoveSize.Height == 0)
                return false;
            return DragStartFlag;
        }

        public System.Drawing.Size CurrentMoveSize
        {
            get
            {
                return new System.Drawing.Size(
                    myCurrentPosition.X - myStartPosition.X,
                    myCurrentPosition.Y - myStartPosition.Y);
            }
        }

        private System.Drawing.Point GetMousePosition(System.Drawing.Point p)
        {
            if (myBindControl != null)
            {
                p = myBindControl.PointToClient(p);
            }
            return MoveInto(p, this.myClipRectangle);
        }
        private System.Drawing.Point CurMousePosition
        {
            get
            {
                return GetMousePosition(System.Windows.Forms.Control.MousePosition);
            }

        }

        private System.Drawing.Point CurMousePosition2
        {
            get
            {
                System.Drawing.Point p = System.Windows.Forms.Control.MousePosition;
                if (this.BindControl != null)
                {
                    p = this.BindControl.PointToClient(p);
                }
                return p;
            }

        }

        private System.Drawing.Point FixPointForControl(System.Drawing.Point p)
        {
            if (myBindControl != null)
                p = myBindControl.PointToClient(p);
            return p;
        }

        /// <summary>
        /// 检测鼠标是否开始执行了拖拽操作
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns>是否开始进行了鼠标拖拽操作</returns>
        public static bool DragDetect(IntPtr hwnd)
        {
            POINT p = new POINT();
            p.x = System.Windows.Forms.Control.MousePosition.X;
            p.y = System.Windows.Forms.Control.MousePosition.Y;
            return DragDetect(hwnd, p);
            //return false;
        }

        #region Win32API函数声明定义代码 **************************************

        /// <summary>
        /// 判断该Windows消息是否是鼠标移动消息
        /// </summary>
        /// <param name="intMessage">消息编码</param>
        /// <returns>判断结果</returns>
        private static bool isMouseMoveMessage(int intMessage)
        {
            if (intMessage == 0x0200 || intMessage == 0x00A0)
                return true;
            return false;
        }

        /// <summary>
        /// 判断该Windows消息是否是鼠标按键松开消息
        /// </summary>
        /// <param name="intMessage">消息编码</param>
        /// <returns>判断结果</returns>
        private static bool isMouseUpMessage(int intMessage)
        {
            // 鼠标在客户区的按钮松开消息
            if (intMessage == 0x0202
                || intMessage == 0x0208
                || intMessage == 0x0205
                || intMessage == 0x020C)
                return true;

            // 鼠标在非客户区的按键松开消息
            if (intMessage == 0x00A2
                || intMessage == 0x00A8
                || intMessage == 0x00A5
                || intMessage == 0x00AC)
                return true;
            return false;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool DragDetect(System.IntPtr hWnd, POINT pt);


        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern int MsgWaitForMultipleObjectsEx(int nCount, IntPtr pHandles, int dwMilliseconds, int dwWakeMask, int dwFlags);


        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int MsgWaitForMultipleObjects(int nCount, int pHandles, bool fWaitAll, int dwMilliseconds, int dwWakeMask);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern bool GetMessage(ref MSG msg, int hWnd, uint wFilterMin, uint wFilterMax);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern bool PeekMessage(ref MSG msg, int hWnd, uint wFilterMin, uint wFilterMax, uint wFlag);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern bool WaitMessage();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern bool TranslateMessage(ref MSG msg);

        [DllImport("user32.dll")]
        private static extern IntPtr DispatchMessage(ref MSG msg);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern bool IsWindowUnicode(HandleRef hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
        private static extern IntPtr DispatchMessageA([In] ref MSG msg);

        [StructLayout(LayoutKind.Sequential)]
        private struct MSG
        {
            public IntPtr  hwnd;
            public int message;
            public IntPtr  wParam;
            public IntPtr lParam;
            public uint time;
            public int pt_x;
            public int pt_y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int x;
            public int y;
        }
        private enum PeekMessageFlags
        {
            PM_NOREMOVE = 0,
            PM_REMOVE = 1,
            PM_NOYIELD = 2
        }

        #endregion



        internal static Rectangle GetRectangle(Point p1, Point p2)
        {
            Rectangle rect = Rectangle.Empty;
            rect.X = Math.Min(p1.X, p2.X);
            rect.Y = Math.Min(p1.Y, p2.Y);
            rect.Width = Math.Max(p1.X, p2.X) - rect.Left;
            rect.Height = Math.Max(p1.Y, p2.Y) - rect.Top;
            return rect;
        }

        public static System.Drawing.Point MoveInto(
            System.Drawing.Point p,
            System.Drawing.Rectangle Bounds)
        {
            if (!Bounds.IsEmpty)
            {
                if (p.X < Bounds.Left)
                    p.X = Bounds.Left;
                if (p.X >= Bounds.Right)
                    p.X = Bounds.Right;
                if (p.Y < Bounds.Top)
                    p.Y = Bounds.Top;
                if (p.Y >= Bounds.Bottom)
                    p.Y = Bounds.Bottom;
            }
            return p;
        }

    }//public class MouseCapturer
}