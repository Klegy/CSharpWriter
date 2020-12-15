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

namespace DCSoft.CSharpWriter.Script
{
    /// <summary>
    /// 表达式事件委托类型
    /// </summary>
    /// <param name="sender">参数</param>
    /// <param name="args">参数</param>
    public delegate void DomExpressionEventHandler(
        object sender,
        DomExpressionEventArgs args);

    /// <summary>
    /// 表达式事件参数
    /// </summary>
    public class DomExpressionEventArgs : EventArgs
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DomExpressionEventArgs()
        {
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="document">文档对象</param>
        /// <param name="element">当前元素对象</param>
        /// <param name="exp">表达式对象</param>
        public DomExpressionEventArgs(
            DomDocument document,
            DomElement element,
            DomExpression exp)
        {
            _Document = document;
            _Element = element;
            _Expression = exp;
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

        private DomElement _Element = null;
        /// <summary>
        /// 文档元素对象
        /// </summary>
        public DomElement Element
        {
            get { return _Element; }
            set { _Element = value; }
        }

        private DomExpression _Expression = null;
        /// <summary>
        /// 表达式对象
        /// </summary>
        public DomExpression Expression
        {
            get { return _Expression; }
            set { _Expression = value; }
        }

        private object _Result = null;
        /// <summary>
        /// 表达式执行的结果
        /// </summary>
        public object Result
        {
            get { return _Result; }
            set { _Result = value; }
        }
    }
}
