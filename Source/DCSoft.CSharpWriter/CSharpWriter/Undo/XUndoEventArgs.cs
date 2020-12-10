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

namespace DCSoft.CSharpWriter.Undo
{
    public class XUndoEventArgs : EventArgs
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public XUndoEventArgs()
        {
        }

        private Dictionary<string, object> _Parameters = new Dictionary<string, object>();
        /// <summary>
        /// 参数
        /// </summary>
        public Dictionary<string, object> Parameters
        {
            get { return _Parameters; }
            set { _Parameters = value; }
        }

        private bool _Cancel = false;
        /// <summary>
        /// 取消操作
        /// </summary>
        public bool Cancel
        {
            get { return _Cancel; }
            set { _Cancel = value; }
        }
    }
}
