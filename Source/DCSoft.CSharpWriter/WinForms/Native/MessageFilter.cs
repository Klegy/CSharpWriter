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

namespace DCSoft.WinForms.Native
{
    /// <summary>
    /// 平台消息过滤器
    /// </summary>
    /// <remarks>编写 袁永福</remarks>
    public class MessageFilter : System.Windows.Forms.IMessageFilter
    {
        private static MessageFilter _ExcludePaintMessageFilter = null;
        /// <summary>
        /// 忽略用户界面绘制消息的消息过滤器
        /// </summary>
        public static MessageFilter ExcludePaintMessageFilter
        {
            get
            {
                if (_ExcludePaintMessageFilter == null)
                {
                    _ExcludePaintMessageFilter = new MessageFilter();
                    _ExcludePaintMessageFilter._ExcludeMessages.Add((int)Msgs.WM_PAINT);
                    _ExcludePaintMessageFilter._ExcludeMessages.Add((int)Msgs.WM_PAINTCLIPBOARD);
                    _ExcludePaintMessageFilter._ExcludeMessages.Add((int)Msgs.WM_PAINTICON);
                    _ExcludePaintMessageFilter._ExcludeMessages.Add((int)Msgs.WM_PALETTECHANGED);
                    _ExcludePaintMessageFilter._ExcludeMessages.Add((int)Msgs.WM_PALETTEISCHANGING);
                    _ExcludePaintMessageFilter._ExcludeMessages.Add((int)Msgs.WM_PARENTNOTIFY);
                    _ExcludePaintMessageFilter._ExcludeMessages.Add((int)Msgs.WM_NCPAINT);
                    _ExcludePaintMessageFilter._ExcludeMessages.Add((int)Msgs.WM_SYNCPAINT);
                }
                return _ExcludePaintMessageFilter; 
            }
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        public MessageFilter()
        {
        }

        private bool _Enabled = false;
        /// <summary>
        /// 对象是否可用
        /// </summary>
        public bool Enabled
        {
            get
            {
                return _Enabled; 
            }
            set
            {
                if (_Enabled != value)
                {
                    _Enabled = value;
                    if (_Enabled)
                    {
                        Application.AddMessageFilter(this);
                    }
                    else
                    {
                        Application.RemoveMessageFilter(this);
                    }
                }
            }
        }

        private List<IntPtr> _ExcludeHwnds = new List<IntPtr>();
        /// <summary>
        /// 忽略处理的窗体的句柄列表
        /// </summary>
        public List<IntPtr> ExcludeHwnds
        {
            get { return _ExcludeHwnds; }
            set { _ExcludeHwnds = value; }
        }

        private List<int> _ExcludeMessages = new List<int>();
        /// <summary>
        /// 忽略处理的消息编号列表
        /// </summary>
        public List<int> ExcludeMessages
        {
            get { return _ExcludeMessages; }
            set { _ExcludeMessages = value; }
        }

        bool IMessageFilter.PreFilterMessage(ref System.Windows.Forms.Message m)
        {
            if (_ExcludeHwnds != null && _ExcludeHwnds.Count > 0)
            {
                foreach (IntPtr hwnd in _ExcludeHwnds)
                {
                    if (m.HWnd == hwnd)
                    {
                        return false;
                    }
                }
            }
            if (_ExcludeMessages != null && _ExcludeMessages.Count > 0 )
            {
                foreach (int msg in _ExcludeMessages)
                {
                    if (msg == m.Msg)
                    {
                        return false;
                    }
                }
            }
            return true ;
        }
    }
}
