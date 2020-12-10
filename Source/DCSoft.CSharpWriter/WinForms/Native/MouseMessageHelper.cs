/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using System.Text;
using System.Runtime.InteropServices;

namespace DCSoft.WinForms.Native
{
    /// <summary>
    /// 专门处理鼠标消息的帮助类
    /// </summary>
    /// <remarks>编写 袁永福</remarks>
    public class MouseMessageHelper
    {
        
        public const int WM_CAPTURECHANGED = 0x0215;

        public const int WM_MOUSEMOVE = 0x0200;
        public const int WM_NCMOUSEMOVE = 0x00A0;
        
        public const int WM_LBUTTONDOWN = 0x0201;
        public const int WM_LBUTTONUP = 0x0202;
        public const int WM_LBUTTONDBLCLK = 0x0203;
        
        public const int WM_RBUTTONDOWN = 0x0204;
        public const int WM_RBUTTONUP = 0x0205;
        public const int WM_RBUTTONDBLCLK = 0x0206;
        
        public const int WM_MBUTTONDOWN = 0x0207;
        public const int WM_MBUTTONUP = 0x0208;
        public const int WM_MBUTTONDBLCLK = 0x0209;
        
        public const int WM_XBUTTONDOWN = 0x020B;
        public const int WM_XBUTTONUP = 0x020C;
        public const int WM_XBUTTONDBLCLK = 0x020D;
        
        public const int WM_MOUSEHOVER = 0x02A1;
        public const int WM_MOUSELEAVE = 0x02A3;
        public const int WM_MOUSEWHEEL = 0x020A;
        
        public const int WM_NCLBUTTONDOWN = 0x00A1;
        public const int WM_NCLBUTTONUP = 0x00A2;
        public const int WM_NCLBUTTONDBLCLK = 0x00A3;

        public const int WM_NCRBUTTONDOWN = 0x00A4;
        public const int WM_NCRBUTTONUP = 0x00A5;
        public const int WM_NCRBUTTONDBLCLK = 0x00A6;
        
        public const int WM_NCMBUTTONDOWN = 0x00A7;
        public const int WM_NCMBUTTONUP = 0x00A8;
        public const int WM_NCMBUTTONDBLCLK = 0x00A9;
        
        public const int WM_NCXBUTTONDOWN = 0x00AB;
        public const int WM_NCXBUTTONUP = 0x00AC;

        public static bool IsMouseMoveMessage(int Message)
        {
            if (Message == WM_MOUSEMOVE) return true;
            if (Message == WM_NCMOUSEMOVE) return true;
            return false;
        }
        /// <summary>
        /// 判断windows消息是不是鼠标按键松开类型的消息
        /// </summary>
        /// <param name="Message">消息类型</param>
        /// <returns>是否鼠标按键松开消息</returns>
        public static bool IsMouseUpMessage(int Message)
        {
            if (Message == WM_LBUTTONUP) return true;
            if (Message == WM_RBUTTONUP) return true;
            if (Message == WM_MBUTTONUP) return true;
            if (Message == WM_XBUTTONUP) return true;
            if (Message == WM_NCLBUTTONUP) return true;
            if (Message == WM_NCRBUTTONUP) return true;
            if (Message == WM_NCMBUTTONUP) return true;
            if (Message == WM_NCXBUTTONUP) return true;
            return false;
        }
        /// <summary>
        /// 判断windows消息是不是鼠标按键按下类型的消息
        /// </summary>
        /// <param name="Message">消息类型</param>
        /// <returns>是否鼠标按键按下消息</returns>
        public static bool IsMouseDownMessage(int Message)
        {
            if (Message == WM_LBUTTONDOWN) return true;
            if (Message == WM_RBUTTONDOWN) return true;
            if (Message == WM_MBUTTONDOWN) return true;
            if (Message == WM_XBUTTONDOWN) return true;
            if (Message == WM_NCLBUTTONDOWN) return true;
            if (Message == WM_NCRBUTTONDOWN) return true;
            if (Message == WM_NCMBUTTONDOWN) return true;
            if (Message == WM_NCXBUTTONDOWN) return true;
            return false;
        }
        /// <summary>
        /// 判断windows消息是不是鼠标类型的消息
        /// </summary>
        /// <param name="Message">消息类型</param>
        /// <returns>是否鼠标消息</returns>
        public static bool IsMouseMessage(int Message)
        {
            if (Message == WM_MOUSEWHEEL) return true;
            if (Message == WM_CAPTURECHANGED) return true;
            if (Message == WM_MOUSEMOVE) return true;
            if (Message == WM_NCMOUSEMOVE) return true;
            if (Message == WM_LBUTTONDOWN) return true;
            if (Message == WM_LBUTTONUP) return true;
            if (Message == WM_LBUTTONDBLCLK) return true;
            if (Message == WM_RBUTTONDOWN) return true;
            if (Message == WM_RBUTTONUP) return true;
            if (Message == WM_RBUTTONDBLCLK) return true;
            if (Message == WM_MBUTTONDOWN) return true;
            if (Message == WM_MBUTTONUP) return true;
            if (Message == WM_MBUTTONDBLCLK) return true;
            if (Message == WM_XBUTTONDOWN) return true;
            if (Message == WM_XBUTTONUP) return true;
            if (Message == WM_XBUTTONDBLCLK) return true;
            if (Message == WM_MOUSEHOVER) return true;
            if (Message == WM_MOUSELEAVE) return true;
            if (Message == WM_MOUSEWHEEL) return true;
            if (Message == WM_NCLBUTTONDOWN) return true;
            if (Message == WM_NCLBUTTONUP) return true;
            if (Message == WM_NCLBUTTONDBLCLK) return true;
            if (Message == WM_NCRBUTTONDOWN) return true;
            if (Message == WM_NCRBUTTONUP) return true;
            if (Message == WM_NCRBUTTONDBLCLK) return true;
            if (Message == WM_NCMBUTTONDOWN) return true;
            if (Message == WM_NCMBUTTONUP) return true;
            if (Message == WM_NCMBUTTONDBLCLK) return true;
            if (Message == WM_NCXBUTTONDOWN) return true;
            if (Message == WM_NCXBUTTONUP) return true;
            return false;
        }

        public static System.Windows.Forms.MouseButtons GetMouseButtons(int wParam )
        {
            System.Windows.Forms.MouseButtons btn = System.Windows.Forms.MouseButtons.None;
            if (GetStyle(wParam, 0x1))
            {
                btn = btn | System.Windows.Forms.MouseButtons.Left;
            }
            if (GetStyle(wParam, 0x2))
            {
                btn = btn | System.Windows.Forms.MouseButtons.Right;
            }
            if (GetStyle(wParam, 0x10))
            {
                btn = btn | System.Windows.Forms.MouseButtons.Middle;
            }
            if (GetStyle(wParam, 0x20))
            {
                btn = btn | System.Windows.Forms.MouseButtons.XButton1;
            }
            if (GetStyle(wParam, 0x40))
            {
                btn = btn | System.Windows.Forms.MouseButtons.XButton2;
            }
            return btn;
        }

        public static int GetX( int lParam)
        {
            return lParam & 0xff;
        }

        public static int GetY( int lParam)
        {
            return lParam >> 0x10;
        }

        public static System.Drawing.Point GetPoint( int lParam)
        {
            int x = lParam & 0xff;
            int y = lParam >> 0x10 ;
            return new System.Drawing.Point(x, y);
        }

        /// <summary>
        /// 将点坐标转换为Windows消息参数值
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static uint GetLParam(System.Drawing.Point p)
        {
            return (uint) (( p.Y << 0x10) + p.X);
        }

        /// <summary>
        /// 鼠标双击判断时间长度，单位毫秒
        /// </summary>
        public static int DoubleClickTime
        {
            get
            {
                return GetDoubleClickTime();
            }
            set
            {
                SetDoubleClickTime(value);
            }
        }

        private static bool GetStyle(int intValue, int MaskFlag)
        {
            return (intValue & MaskFlag) == MaskFlag;
        }

        public static System.Drawing.Point GetScreenMousePosition(System.Windows.Forms.Message msg)
        {
            if (msg.Msg == WM_MOUSEMOVE
                || msg.Msg == WM_LBUTTONDBLCLK
                || msg.Msg == WM_LBUTTONDOWN
                || msg.Msg == WM_LBUTTONUP
                || msg.Msg == WM_RBUTTONDBLCLK
                || msg.Msg == WM_RBUTTONDOWN
                || msg.Msg == WM_RBUTTONUP)
            {
                APIPOINT p = new APIPOINT();
                p.x = GetX(msg.LParam.ToInt32());
                p.y = GetY(msg.LParam.ToInt32());
                ClientToScreen(msg.HWnd, ref p);
                return new System.Drawing.Point(p.x, p.y);
            }
            else
            {
                return GetPoint(msg.LParam.ToInt32());
            }
        }

        public static System.Drawing.Point ClientToScreen(IntPtr hwnd, System.Drawing.Point p)
        {
            APIPOINT p2 = new APIPOINT();
            p2.x = p.X;
            p2.y = p.Y;
            if (ClientToScreen(hwnd, ref p2))
            {
                return new System.Drawing.Point(p2.x, p2.y);
            }
            else
            {
                return System.Drawing.Point.Empty;
            }
        }

        public static System.Drawing.Point ScreenToClient(IntPtr hwnd, System.Drawing.Point p)
        {
            APIPOINT p2 = new APIPOINT();
            p2.x = p.X;
            p2.y = p.Y;
            if (ScreenToClient(hwnd, ref p2))
            {
                return new System.Drawing.Point(p2.x, p2.y);
            }
            else
            {
                return System.Drawing.Point.Empty;
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool ClientToScreen(IntPtr hwnd, ref APIPOINT pt);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool ScreenToClient(IntPtr hwnd, ref APIPOINT pt);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool DragDetect(System.IntPtr hWnd, APIPOINT pt);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int GetCapture();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int GetDoubleClickTime();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SetDoubleClickTime(int Interval);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool SwapMouseButton(bool fSwap);


        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SetCapture( IntPtr hWnd );


        [StructLayout(LayoutKind.Sequential)]
        private struct APIPOINT
        {
            public int x;
            public int y;
        }


    }
}
