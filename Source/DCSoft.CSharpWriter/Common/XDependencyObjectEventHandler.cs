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
using System.ComponentModel;


namespace DCSoft.Common
{

    /// <summary>
    /// 对象事件
    /// </summary>
    /// <param name="sender">发起者</param>
    /// <param name="args">参数</param>
    public delegate void XDependencyObjectEventHandler(
            object sender,
            XDependencyObjectEventArgs args);

    /// <summary>
    /// 对象事件参数类型
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    public class XDependencyObjectEventArgs : EventArgs
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="instance">对象实例</param>
        /// <param name="property">属性</param>
        /// <param name="Value">数值</param>
        /// <param name="newValue">新数值</param>
        internal XDependencyObjectEventArgs(
            XDependencyObject instance,
            XDependencyProperty property,
            object Value,
            object newValue)
        {
            _instance = instance;
            _Property = property;
            _Value = Value;
            _NewValue = newValue;
        }

        private XDependencyObject _instance = null;
        /// <summary>
        /// 对象实例
        /// </summary>
        public XDependencyObject Instance
        {
            get { return _instance; }
        }

        private XDependencyProperty _Property = null;
        /// <summary>
        /// 操作的属性
        /// </summary>
        public XDependencyProperty Property
        {
            get { return _Property; }
        }

        private object _Value = null;
        /// <summary>
        /// 属性值
        /// </summary>
        public object Value
        {
            get { return _Value; }
        }

        private object _NewValue = null;
        /// <summary>
        /// 新属性值
        /// </summary>
        public object NewValue
        {
            get { return _NewValue; }
            set { _NewValue = value; }
        }

        private bool _Cancel = false;
        /// <summary>
        /// 是否取消操作
        /// </summary>
        public bool Cancel
        {
            get { return _Cancel; }
            set { _Cancel = value; }
        }
    }
}
