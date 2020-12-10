/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime;
using System.Runtime.InteropServices;

namespace DCSoft.WinForms.Native
{
    /// <summary>
    /// Windows系统消息对象
    /// </summary>
    public class Win32Message
    {
        /// <summary>
        /// 清空指定窗体消息队列中的所有的消息
        /// </summary>
        /// <returns>删除的消息个数</returns>
        public static int ClearMessage( IntPtr hwnd )
        {
            int result = 0;
            while (true)
            {
                NativeMSG msg = new NativeMSG();
                //if (NativeGetMessage(ref msg, hwnd, 0, 0))
                //{
                //    result++;
                //}
                //else
                //{
                //    break;
                //}
                if (NativePeekMessage(ref msg, hwnd, 0, 0, 0xffffff))
                {
                    if (msg.message == 0)
                    {
                        break;
                    }
                    result++;
                }
                else
                {
                    break;
                }
            }//while
            return result;
        }

        /// <summary>
        /// 清空应用程序消息队列中的所有的消息
        /// </summary>
        /// <returns>删除的消息个数</returns>
        public static int ClearMessage()
        {
            return ClearMessage(IntPtr.Zero);
        }

        /// <summary>
        /// 等待一个消息
        /// </summary>
        /// <returns>是否等到的一个消息</returns>
        public static bool Wait()
        {
            return NativeWaitMessage();
        }

        /// <summary>
        /// 从消息队列中获得一个消息对象,并从消息队列中删除这个消息
        /// </summary>
        /// <returns>获得的消息对象</returns>
        public static Win32Message GetMessage()
        {
            NativeMSG msg = new NativeMSG();
            if (NativeGetMessage(ref msg, IntPtr.Zero, 0, 0))
            {
                Win32Message msg2 = new Win32Message(msg);
                return msg2;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 从消息队列中获得一个消息，但不从消息队列中删除该消息
        /// </summary>
        /// <returns>获得的消息对象</returns>
        public static Win32Message Peek()
        {
            NativeMSG msg = new NativeMSG();
            if (NativePeekMessage(ref msg, IntPtr.Zero, 0, 0, 0))
            {
                return new Win32Message(msg);
            }
            else
            {
                return null;
            }
        }

        private Win32Message(NativeMSG msg)
        {
            _HWnd = new IntPtr(msg.hwnd);
            _LParam = new IntPtr( msg.lParam );
            _Msg = msg.message;
            _WParam = new IntPtr( msg.wParam );
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="msg"></param>
        public Win32Message(Message msg)
        {
            _HWnd = msg.HWnd;
            _LParam = msg.LParam;
            _Msg = msg.Msg;
            _Result = msg.Result;
            _WParam = msg.WParam;
        }

        private IntPtr _HWnd = IntPtr.Zero;

        public IntPtr HWnd
        {
            get { return _HWnd; }
            set { _HWnd = value; }
        }

        private int _Msg = 0;

        public int Msg
        {
            get { return _Msg; }
            set { _Msg = value; }
        }

        private IntPtr _LParam = IntPtr.Zero;

        public IntPtr LParam
        {
            get { return _LParam; }
            set { _LParam = value; }
        }

        private IntPtr _WParam = IntPtr.Zero;

        public IntPtr WParam
        {
            get { return _WParam; }
            set { _WParam = value; }
        }

        private IntPtr _Result = IntPtr.Zero;

        public IntPtr Result
        {
            get { return _Result; }
            set { _Result = value; }
        }

        public uint SendTo(IntPtr hwnd)
        {
            return NativeSendMessage(hwnd, this.Msg, (uint)this.WParam, (uint)this.LParam);
        }

        public uint SendTo()
        {
            return NativeSendMessage(this.HWnd, this.Msg, (uint)this.WParam, (uint)this.LParam);
        }

        public bool PostTo(IntPtr hwnd)
        {
            return NativePostMessage(hwnd, Msg, (uint)this.WParam, (uint)this.LParam);
        }

        public bool PostTo()
        {
            return NativePostMessage(this.HWnd, this.Msg, (uint)this.WParam, (uint)this.LParam);
        }

        /// <summary>
        /// 判断是否是鼠标按键按下消息
        /// </summary>
        public bool IsMouseDownMessage
        {
            get
            {
                // 鼠标在客户区的按钮按下消息
                if ((_Msg == (int)Msgs.WM_LBUTTONDOWN) ||
                    (_Msg == (int)Msgs.WM_MBUTTONDOWN) ||
                    (_Msg == (int)Msgs.WM_RBUTTONDOWN) ||
                    (_Msg == (int)Msgs.WM_XBUTTONDOWN))
                {
                    return true;
                }
                // 鼠标在非客户区的按键按下消息
                if ((_Msg == (int)Msgs.WM_NCLBUTTONDOWN) ||
                    (_Msg == (int)Msgs.WM_NCMBUTTONDOWN) ||
                    (_Msg == (int)Msgs.WM_NCRBUTTONDOWN) ||
                    (_Msg == (int)Msgs.WM_NCXBUTTONDOWN))
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 是否是鼠标移动消息
        /// </summary>
        public bool IsMouseMoveMessage
        {
            get
            {
                if (_Msg == (int)Msgs.WM_MOUSEMOVE ||
                    _Msg == (int)Msgs.WM_NCMOUSEMOVE)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 是否是鼠标按键松开消息
        /// </summary>
        public bool IsMouseUpMessage
        {
            get
            {
                // 鼠标在客户区的按钮松开消息
                if ((_Msg == (int)Msgs.WM_LBUTTONUP) ||
                    (_Msg == (int)Msgs.WM_MBUTTONUP) ||
                    (_Msg == (int)Msgs.WM_RBUTTONUP) ||
                    (_Msg == (int)Msgs.WM_XBUTTONUP))
                {
                    return true;
                }
                // 鼠标在非客户区的按键松开消息
                if ((_Msg == (int)Msgs.WM_NCLBUTTONUP) ||
                    (_Msg == (int)Msgs.WM_NCMBUTTONUP) ||
                    (_Msg == (int)Msgs.WM_NCRBUTTONUP) ||
                    (_Msg == (int)Msgs.WM_NCXBUTTONUP))
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 是否是键盘按键消息
        /// </summary>
        public bool IsKeyMessage
        {
            get
            {
                if (_Msg == (int)Msgs.WM_KEYDOWN ||
                    _Msg == (int)Msgs.WM_KEYUP ||
                    _Msg == (int)Msgs.WM_CHAR)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 鼠标光标在屏幕中的位置
        /// </summary>
        public System.Drawing.Point ScreenMousePosition
        {
            get
            {
                NativePOINT pt = new NativePOINT();
                pt.x = (short)( this.LParam.ToInt32() & 0x0000FFFFU);
                pt.y = (short)((this.LParam.ToInt32() & 0xFFFF0000U) >> 16);
                if (this.HWnd != IntPtr.Zero)
                {
                    ClientToScreen( this.HWnd , ref pt);
                }
                return new System.Drawing.Point(pt.x, pt.y);
            }
        }

        /// <summary>
        /// 鼠标光标的位置
        /// </summary>
        public System.Drawing.Point MousePosition
        {
            get
            {
                NativePOINT pt = new NativePOINT();
                pt.x = (short)(this.LParam.ToInt32() & 0x0000FFFFU);
                pt.y = (short)((this.LParam.ToInt32() & 0xFFFF0000U) >> 16);
                return new System.Drawing.Point(pt.x, pt.y);
            }
        }


        #region 声明引入Windows平台调用函数 ****************************************************

        [DllImport("user32",EntryPoint="PostQuitMessage")]
        public static extern void PostQuitMessage(int nExitCode);

        [DllImport("user32")] //ANSI
        public static extern int GetMessageA(out NativeMSG msg, int hwnd, int minFilter, int maxFilter);

        [DllImport("user32")] //ANSI
        public static extern bool PeekMessageA(out NativeMSG lpMsg, int hWnd, uint wMsgFilterMin, uint wMsgFilterMax, PeekMessageFlags wRemoveMsg);

        [DllImport("user32", CharSet = CharSet.Ansi)] //ANSI
        public static extern int DispatchMessageA(ref NativeMSG msg);


        [DllImport("User32.dll", EntryPoint="WaitMessage", CharSet = CharSet.Auto)]
        private static extern bool NativeWaitMessage();

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool TranslateMessage(ref NativeMSG msg);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool DispatchMessage(ref NativeMSG msg);

        [DllImport("User32.dll", EntryPoint="PostMessage", CharSet = CharSet.Auto)]
        private static extern bool NativePostMessage(IntPtr hWnd, int Msg, uint wParam, uint lParam);

        [DllImport("User32.dll", EntryPoint="SendMessage", CharSet = CharSet.Auto)]
        private static extern uint NativeSendMessage(IntPtr hWnd, int Msg, uint wParam, uint lParam);

        [DllImport("User32.dll", EntryPoint="GetMessage", CharSet = CharSet.Auto)]
        private static extern bool NativeGetMessage(ref NativeMSG msg, IntPtr hWnd, uint wFilterMin, uint wFilterMax);

        [DllImport("User32.dll", EntryPoint="PeekMessage", CharSet = CharSet.Auto)]
        private static extern bool NativePeekMessage(ref NativeMSG msg, IntPtr hWnd, uint wFilterMin, uint wFilterMax, uint wFlag);


        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern bool ClientToScreen(IntPtr hWnd, ref NativePOINT pt);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern bool ScreenToClient(IntPtr hWnd, ref NativePOINT pt);


        #endregion

    }


    [StructLayout(LayoutKind.Sequential)]
    public struct NativeMSG
    {
        public int hwnd;
        public int message;
        public int wParam;
        public int lParam;
        public int time;
        public int pt_x;
        public int pt_y;
    }


    public enum Msgs
    {
        WM_NULL = 0x0000,
        WM_CREATE = 0x0001,
        WM_DESTROY = 0x0002,
        WM_MOVE = 0x0003,
        WM_SIZE = 0x0005,
        WM_ACTIVATE = 0x0006,
        WM_SETFOCUS = 0x0007,
        WM_KILLFOCUS = 0x0008,
        WM_ENABLE = 0x000A,
        WM_SETREDRAW = 0x000B,
        WM_SETTEXT = 0x000C,
        WM_GETTEXT = 0x000D,
        WM_GETTEXTLENGTH = 0x000E,
        WM_PAINT = 0x000F,
        WM_CLOSE = 0x0010,
        WM_QUERYENDSESSION = 0x0011,
        WM_QUIT = 0x0012,
        WM_QUERYOPEN = 0x0013,
        WM_ERASEBKGND = 0x0014,
        WM_SYSCOLORCHANGE = 0x0015,
        WM_ENDSESSION = 0x0016,
        WM_SHOWWINDOW = 0x0018,
        WM_WININICHANGE = 0x001A,
        WM_SETTINGCHANGE = 0x001A,
        WM_DEVMODECHANGE = 0x001B,
        WM_ACTIVATEAPP = 0x001C,
        WM_FONTCHANGE = 0x001D,
        WM_TIMECHANGE = 0x001E,
        WM_CANCELMODE = 0x001F,
        WM_SETCURSOR = 0x0020,
        WM_MOUSEACTIVATE = 0x0021,
        WM_CHILDACTIVATE = 0x0022,
        WM_QUEUESYNC = 0x0023,
        WM_GETMINMAXINFO = 0x0024,
        WM_PAINTICON = 0x0026,
        WM_ICONERASEBKGND = 0x0027,
        WM_NEXTDLGCTL = 0x0028,
        WM_SPOOLERSTATUS = 0x002A,
        WM_DRAWITEM = 0x002B,
        WM_MEASUREITEM = 0x002C,
        WM_DELETEITEM = 0x002D,
        WM_VKEYTOITEM = 0x002E,
        WM_CHARTOITEM = 0x002F,
        WM_SETFONT = 0x0030,
        WM_GETFONT = 0x0031,
        WM_SETHOTKEY = 0x0032,
        WM_GETHOTKEY = 0x0033,
        WM_QUERYDRAGICON = 0x0037,
        WM_COMPAREITEM = 0x0039,
        WM_GETOBJECT = 0x003D,
        WM_COMPACTING = 0x0041,
        WM_COMMNOTIFY = 0x0044,
        WM_WINDOWPOSCHANGING = 0x0046,
        WM_WINDOWPOSCHANGED = 0x0047,
        WM_POWER = 0x0048,
        WM_COPYDATA = 0x004A,
        WM_CANCELJOURNAL = 0x004B,
        WM_NOTIFY = 0x004E,
        WM_INPUTLANGCHANGEREQUEST = 0x0050,
        WM_INPUTLANGCHANGE = 0x0051,
        WM_TCARD = 0x0052,
        WM_HELP = 0x0053,
        WM_USERCHANGED = 0x0054,
        WM_NOTIFYFORMAT = 0x0055,
        WM_CONTEXTMENU = 0x007B,
        WM_STYLECHANGING = 0x007C,
        WM_STYLECHANGED = 0x007D,
        WM_DISPLAYCHANGE = 0x007E,
        WM_GETICON = 0x007F,
        WM_SETICON = 0x0080,
        WM_NCCREATE = 0x0081,
        WM_NCDESTROY = 0x0082,
        WM_NCCALCSIZE = 0x0083,
        WM_NCHITTEST = 0x0084,
        WM_NCPAINT = 0x0085,
        WM_NCACTIVATE = 0x0086,
        WM_GETDLGCODE = 0x0087,
        WM_SYNCPAINT = 0x0088,
        WM_NCMOUSEMOVE = 0x00A0,
        WM_MOUSEWHEEL = 0x020A,
        WM_NCLBUTTONDOWN = 0x00A1,
        WM_NCLBUTTONUP = 0x00A2,
        WM_NCLBUTTONDBLCLK = 0x00A3,
        WM_NCRBUTTONDOWN = 0x00A4,
        WM_NCRBUTTONUP = 0x00A5,
        WM_NCRBUTTONDBLCLK = 0x00A6,
        WM_NCMBUTTONDOWN = 0x00A7,
        WM_NCMBUTTONUP = 0x00A8,
        WM_NCMBUTTONDBLCLK = 0x00A9,
        WM_NCXBUTTONDOWN = 0x00AB,
        WM_NCXBUTTONUP = 0x00AC,
        WM_KEYDOWN = 0x0100,
        WM_KEYUP = 0x0101,
        WM_CHAR = 0x0102,
        WM_DEADCHAR = 0x0103,
        WM_SYSKEYDOWN = 0x0104,
        WM_SYSKEYUP = 0x0105,
        WM_SYSCHAR = 0x0106,
        WM_SYSDEADCHAR = 0x0107,
        WM_KEYLAST = 0x0108,
        WM_IME_STARTCOMPOSITION = 0x010D,
        WM_IME_ENDCOMPOSITION = 0x010E,
        WM_IME_COMPOSITION = 0x010F,
        WM_IME_KEYLAST = 0x010F,
        WM_INITDIALOG = 0x0110,
        WM_COMMAND = 0x0111,
        WM_SYSCOMMAND = 0x0112,
        WM_TIMER = 0x0113,
        WM_HSCROLL = 0x0114,
        WM_VSCROLL = 0x0115,
        WM_INITMENU = 0x0116,
        WM_INITMENUPOPUP = 0x0117,
        WM_MENUSELECT = 0x011F,
        WM_MENUCHAR = 0x0120,
        WM_ENTERIDLE = 0x0121,
        WM_MENURBUTTONUP = 0x0122,
        WM_MENUDRAG = 0x0123,
        WM_MENUGETOBJECT = 0x0124,
        WM_UNINITMENUPOPUP = 0x0125,
        WM_MENUCOMMAND = 0x0126,
        WM_CTLCOLORMSGBOX = 0x0132,
        WM_CTLCOLOREDIT = 0x0133,
        WM_CTLCOLORLISTBOX = 0x0134,
        WM_CTLCOLORBTN = 0x0135,
        WM_CTLCOLORDLG = 0x0136,
        WM_CTLCOLORSCROLLBAR = 0x0137,
        WM_CTLCOLORSTATIC = 0x0138,
        WM_MOUSEMOVE = 0x0200,
        WM_LBUTTONDOWN = 0x0201,
        WM_LBUTTONUP = 0x0202,
        WM_LBUTTONDBLCLK = 0x0203,
        WM_RBUTTONDOWN = 0x0204,
        WM_RBUTTONUP = 0x0205,
        WM_RBUTTONDBLCLK = 0x0206,
        WM_MBUTTONDOWN = 0x0207,
        WM_MBUTTONUP = 0x0208,
        WM_MBUTTONDBLCLK = 0x0209,
        WM_XBUTTONDOWN = 0x020B,
        WM_XBUTTONUP = 0x020C,
        WM_XBUTTONDBLCLK = 0x020D,
        WM_PARENTNOTIFY = 0x0210,
        WM_ENTERMENULOOP = 0x0211,
        WM_EXITMENULOOP = 0x0212,
        WM_NEXTMENU = 0x0213,
        WM_SIZING = 0x0214,
        WM_CAPTURECHANGED = 0x0215,
        WM_MOVING = 0x0216,
        WM_DEVICECHANGE = 0x0219,
        WM_MDICREATE = 0x0220,
        WM_MDIDESTROY = 0x0221,
        WM_MDIACTIVATE = 0x0222,
        WM_MDIRESTORE = 0x0223,
        WM_MDINEXT = 0x0224,
        WM_MDIMAXIMIZE = 0x0225,
        WM_MDITILE = 0x0226,
        WM_MDICASCADE = 0x0227,
        WM_MDIICONARRANGE = 0x0228,
        WM_MDIGETACTIVE = 0x0229,
        WM_MDISETMENU = 0x0230,
        WM_ENTERSIZEMOVE = 0x0231,
        WM_EXITSIZEMOVE = 0x0232,
        WM_DROPFILES = 0x0233,
        WM_MDIREFRESHMENU = 0x0234,
        WM_IME_SETCONTEXT = 0x0281,
        WM_IME_NOTIFY = 0x0282,
        WM_IME_CONTROL = 0x0283,
        WM_IME_COMPOSITIONFULL = 0x0284,
        WM_IME_SELECT = 0x0285,
        WM_IME_CHAR = 0x0286,
        WM_IME_REQUEST = 0x0288,
        WM_IME_KEYDOWN = 0x0290,
        WM_IME_KEYUP = 0x0291,
        WM_MOUSEHOVER = 0x02A1,
        WM_MOUSELEAVE = 0x02A3,
        WM_CUT = 0x0300,
        WM_COPY = 0x0301,
        WM_PASTE = 0x0302,
        WM_CLEAR = 0x0303,
        WM_UNDO = 0x0304,
        WM_RENDERFORMAT = 0x0305,
        WM_RENDERALLFORMATS = 0x0306,
        WM_DESTROYCLIPBOARD = 0x0307,
        WM_DRAWCLIPBOARD = 0x0308,
        WM_PAINTCLIPBOARD = 0x0309,
        WM_VSCROLLCLIPBOARD = 0x030A,
        WM_SIZECLIPBOARD = 0x030B,
        WM_ASKCBFORMATNAME = 0x030C,
        WM_CHANGECBCHAIN = 0x030D,
        WM_HSCROLLCLIPBOARD = 0x030E,
        WM_QUERYNEWPALETTE = 0x030F,
        WM_PALETTEISCHANGING = 0x0310,
        WM_PALETTECHANGED = 0x0311,
        WM_HOTKEY = 0x0312,
        WM_PRINT = 0x0317,
        WM_PRINTCLIENT = 0x0318,
        WM_HANDHELDFIRST = 0x0358,
        WM_HANDHELDLAST = 0x035F,
        WM_AFXFIRST = 0x0360,
        WM_AFXLAST = 0x037F,
        WM_PENWINFIRST = 0x0380,
        WM_PENWINLAST = 0x038F,
        WM_APP = 0x8000,
        WM_USER = 0x0400,
        WM_MOUSEFIRST = 0x200,
        WM_MOUSELAST = 0x20A
    }

}
