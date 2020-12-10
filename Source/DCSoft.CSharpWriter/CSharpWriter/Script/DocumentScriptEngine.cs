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

namespace DCSoft.CSharpWriter.Script
{
    /// <summary>
    /// 文档脚本引擎
    /// </summary>
    /// <remarks>编写 袁永福</remarks>
    public class DocumentScriptEngine : DCSoft.Script.XVBAEngine 
    {

        /// <summary>
        /// 文档内容发生改变事件脚本方法名称
        /// </summary>
        public const string Document_DocumentContentChanged = "Document_DocumentContentChanged";
        /// <summary>
        /// 文档加载事件脚本方法名称
        /// </summary>
        public const string Document_DocumentLoad = "Document_DocumentLoad";
        /// <summary>
        /// 文档选择状态发生改变事件脚本方法名称
        /// </summary>
        public const string Document_SelectionChanged = "Document_SelectionChanged";


        /// <summary>
        /// 初始化对象
        /// </summary>
        public DocumentScriptEngine()
        {
            this.DocumentGlobalObjects = new ScriptGlobalObjectContainer();
        }

        /// <summary>
        /// 文档对象
        /// </summary>
        public DomDocument Document
        {
            get { return this.DocumentGlobalObjects.Document; }
            set { this.DocumentGlobalObjects.Document = value; }
        }
         
        /// <summary>
        /// 当前元素对象
        /// </summary>
        public DomElement CurrentElement
        {
            get { return this.DocumentGlobalObjects.CurrentElement; }
            set { this.DocumentGlobalObjects.CurrentElement = value; }
        }

        [NonSerialized()]
        private static System.Collections.Hashtable _ScriptSession = null;
        /// <summary>
        /// 脚本中使用的全局上下文对象
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        public static System.Collections.Hashtable ScriptSession
        {
            get
            {
                if (_ScriptSession == null)
                {
                    _ScriptSession = new System.Collections.Hashtable();
                }
                return _ScriptSession;
            }
        }

        /// <summary>
        /// 脚本全局对象列表
        /// </summary>
        public ScriptGlobalObjectContainer DocumentGlobalObjects
        {
            get
            {
                return (ScriptGlobalObjectContainer)base.GlobalObjects; 
            }
            set
            {
                base.GlobalObjects = value;
            }
        }

        /// <summary>
        /// 检查脚本引擎的状态
        /// </summary>
        /// <returns>脚本引擎是否成功启动</returns>
        public override bool CheckReady()
        {
            if (this.Enabled == false || this.IsClosed )
            {
                return false;
            }
            if (this.Document == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.ScriptText))
            {
                this.ScriptText = this.Document.ScriptText;
            }
            if (this.DocumentGlobalObjects.Window == null)
            {
                this.DocumentGlobalObjects.Window = new WriterWindow();
            }
            this.DocumentGlobalObjects.Window.ParentWindow = this.Document.EditorControl;
            this.DocumentGlobalObjects.Window.WriterControl = this.Document.EditorControl;
            this.DocumentGlobalObjects.Server = this.Document.ServerObject;
            return base.CheckReady();
        }

        /// <summary>
        /// 编译脚本
        /// </summary>
        /// <returns></returns>
        public override bool Compile()
        {
            if (this.Document != null)
            {
                this.DocumentGlobalObjects.Session = ScriptSession;
                this.DocumentGlobalObjects.Window.Engine = this;
                this.DocumentGlobalObjects.Window.ParentWindow = this.Document.EditorControl;
                this.DocumentGlobalObjects.WriterControl = this.Document.EditorControl;
                return base.Compile();
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 启动脚本引擎
        /// </summary>
        /// <returns>启动是否成功</returns>
        public bool Start()
        {
            this.ScriptText = this.Document.ScriptText;
            return this.Compile();
        }

        /// <summary>
        /// 针对一个文档元素执行脚本方法
        /// </summary>
        /// <param name="element">文档元素名称</param>
        /// <param name="eventName">方法名称</param>
        public void ExecuteSub(DomElement element, string subName )
        {
            if (CheckReady())
            {
                this.CurrentElement = element;
                base.ExecuteSub(subName);
            }
        }


    }
}