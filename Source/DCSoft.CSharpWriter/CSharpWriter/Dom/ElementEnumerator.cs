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

namespace DCSoft.CSharpWriter.Dom
{
    /// <summary>
    /// 遍历文档元素事件委托类型 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public delegate void ElementEnumerateEventHandler(
        object sender ,
        ElementEnumerateEventArgs args );

    /// <summary>
    /// 遍历元素事件参数
    /// </summary>
    public class ElementEnumerateEventArgs : EventArgs 
    {
        private bool _Cancel = false;
        /// <summary>
        /// 取消操作
        /// </summary>
        public bool Cancel
        {
            get { return _Cancel; }
            set { _Cancel = value; }
        }

        private bool _CancelChild = false;
        /// <summary>
        /// 取消遍历子孙元素
        /// </summary>
        public bool CancelChild
        {
            get { return _CancelChild; }
            set { _CancelChild = value; }
        }

        internal DomContainerElement _Parent = null;
        /// <summary>
        /// 父节点
        /// </summary>
        public DomContainerElement Parent
        {
            get { return _Parent; }
            //set { _Parent = value; }
        }

        internal DomElement _Element = null;
        /// <summary>
        /// 当前处理的元素
        /// </summary>
        public DomElement Element
        {
            get { return _Element; }
            //set { _Element = value; }
        }
    }
}
