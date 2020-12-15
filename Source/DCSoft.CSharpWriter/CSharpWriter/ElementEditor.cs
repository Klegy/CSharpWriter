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
using DCSoft.CSharpWriter.Dom;

namespace DCSoft.CSharpWriter
{
    /// <summary>
    /// 文档元素编辑器
    /// </summary>
    public abstract class ElementEditor
    {
        /// <summary>
        /// 判断指定类型的编辑操作是否支持
        /// </summary>
        /// <param name="method">编辑操作的方法</param>
        /// <returns>是否支持</returns>
        public abstract bool IsSupportMethod(ElementEditMethod method);
        /// <summary>
        /// 编辑文档元素内容
        /// </summary>
        /// <param name="args">参数</param>
        /// <returns>操作是否修改了文档元素的内容</returns>
        public abstract bool Edit(ElementEditEventArgs args );
    }

    /// <summary>
    /// 编辑文档元素事件参数
    /// </summary>
    public class ElementEditEventArgs : EventArgs
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public ElementEditEventArgs()
        {
        }

        private DomDocument _Document = null;
        /// <summary>
        /// 文档对象
        /// </summary>
        public DomDocument Document
        {
            get { return _Document; }
            set { _Document = value; }
        }

        private WriterAppHost _Host = null;
        /// <summary>
        /// 编辑器宿主对象
        /// </summary>
        public WriterAppHost Host
        {
            get { return _Host; }
            set { _Host = value; }
        }

        private DomElement _Element = null;
        /// <summary>
        /// 正在编辑的文档对象
        /// </summary>
        public DomElement Element
        {
            get { return _Element; }
            set { _Element = value; }
        }

        private bool _LogUndo = true;
        /// <summary>
        /// 是否记录重做、撤销操作信息。
        /// </summary>
        public bool LogUndo
        {
            get { return _LogUndo; }
            set { _LogUndo = value; }
        }

        private ElementEditMethod _Method = ElementEditMethod.Insert;
        /// <summary>
        /// 操作模式
        /// </summary>
        public ElementEditMethod Method
        {
            get { return _Method; }
            set { _Method = value; }
        }

        private System.Windows.Forms.IWin32Window _ParentWindow = null;

        public System.Windows.Forms.IWin32Window ParentWindow
        {
            get { return _ParentWindow; }
            set { _ParentWindow = value; }
        }
    }

    /// <summary>
    /// 元素编辑操作模式
    /// </summary>
    public enum ElementEditMethod
    {
        /// <summary>
        /// 新增元素
        /// </summary>
        Insert ,
        /// <summary>
        /// 编辑元素
        /// </summary>
        Edit
    }
}
