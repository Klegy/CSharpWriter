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
using DCSoft.CSharpWriter.Dom ;
using DCSoft.Script ;
using DCSoft.CSharpWriter.Controls;

namespace DCSoft.CSharpWriter.Script
{
    /// <summary>
    /// 脚本使用的全局对象列表
    /// </summary>
    /// <remarks>编写 袁永福</remarks>
    public class ScriptGlobalObjectContainer : DCSoft.Script.XVBAScriptGlobalObjectList 
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public ScriptGlobalObjectContainer()
        {
            this.Window = new WriterWindow();
        }

        /// <summary>
        /// 编辑器控件对象
        /// </summary>
        public CSWriterControl WriterControl
        {
            get
            {
                return (CSWriterControl)base["WriterControl"];
            }
            set
            {
                base.SetValue("WriterControl", value, typeof(CSWriterControl));
            }
        }

        /// <summary>
        /// 窗体对象
        /// </summary>
        public WriterWindow Window
        {
            get
            {
                return (WriterWindow)base["Window"];
            }
            set
            {
                base.SetValue("Window", value, typeof(WriterWindow));
            }
        }

        /// <summary>
        /// 文档对象
        /// </summary>
        public DomDocument Document
        {
            get
            {
                return (DomDocument)base["Document"];
            }
            set
            {
                base.SetValue("Document" ,  value , typeof( DomDocument )) ;
            }
        }

        /// <summary>
        /// 当前处理的文档元素对象
        /// </summary>
        public DomElement CurrentElement
        {
            get
            {
                return (DomElement)base["CurrentElement"];
            }
            set
            {
                base.SetValue("CurrentElement", value, typeof(DomElement));
            }
        }

        /// <summary>
        /// 上下文对象
        /// </summary>
        public System.Collections.Hashtable Session
        {
            get
            {
                return (System.Collections.Hashtable)base["Session"];
            }
            set
            {
                base.SetValue("Session", value, typeof(System.Collections.Hashtable));
            }
        }

        /// <summary>
        /// 服务器对象
        /// </summary>
        public object Server
        {
            get
            {
                return base["Server"];
            }
            set
            {
                base["Server"] = value;
            }
        }
    }
}
