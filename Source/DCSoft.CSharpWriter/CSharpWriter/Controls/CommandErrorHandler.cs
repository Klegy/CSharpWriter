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
using DCSoft.CSharpWriter.Dom ;

namespace DCSoft.CSharpWriter.Controls
{
    /// <summary>
    /// 命令执行错误处理委托类型
    /// </summary>
    /// <param name="sender">参数</param>
    /// <param name="args">参数</param>
    public delegate void CommandEventHandler( object sender , CommandErrorEventArgs args );
    public class CommandErrorEventArgs : EventArgs 
    {
        public CommandErrorEventArgs()
        {
        }

        private CSWriterControl _WriterControl = null;
        /// <summary>
        /// 编辑器控件对象
        /// </summary>
        public CSWriterControl WriterControl
        {
            get { return _WriterControl; }
            set { _WriterControl = value; }
        }

        private DomDocument _Document = null;
        /// <summary>
        /// 正在处理的文档对象
        /// </summary>
        public DomDocument Document
        {
            get { return _Document; }
            set { _Document = value; }
        }

        private string _CommandName = null;
        /// <summary>
        /// 命令名称
        /// </summary>
        public string CommandName
        {
            get { return _CommandName; }
            set { _CommandName = value; }
        }

        private object _CommmandParameter = null;
        /// <summary>
        /// 命令参数
        /// </summary>
        public object CommmandParameter
        {
            get { return _CommmandParameter; }
            set { _CommmandParameter = value; }
        }

        private Exception _Exception = null;
        /// <summary>
        /// 异常对象
        /// </summary>
        public Exception Exception
        {
            get { return _Exception; }
            set { _Exception = value; }
        }
    }
}
