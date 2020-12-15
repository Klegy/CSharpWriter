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
    ///// <summary>
    ///// 数据过滤器
    ///// </summary>
    //public abstract class DataFilter 
    //{
    //    /// <summary>
    //    /// 过滤要插入文档中的纯文本内容
    //    /// </summary>
    //    /// <param name="text">纯文本字符串</param>
    //    /// <returns>过滤后的字符串</returns>
    //    public virtual string FilterInputText(string text)
    //    {
    //        return text;
    //    }

    //    /// <summary>
    //    /// 过滤要插入文档中的RTF文档
    //    /// </summary>
    //    /// <param name="rtf"></param>
    //    /// <returns></returns>
    //    public virtual string FilterInputRTF(string rtf  )
    //    {
    //        return rtf;
    //    }

    //    /// <summary>
    //    /// 过滤要插入文档中的文档元素
    //    /// </summary>
    //    /// <param name="sourceElements"></param>
    //    /// <returns></returns>
    //    public virtual XTextElementList FilterInputElements(XTextElementList sourceElements )
    //    {
    //        return sourceElements;
    //    }
    //}

    /// <summary>
    /// 数据过滤器事件委托类型
    /// </summary>
    /// <param name="sender">发起者</param>
    /// <param name="args">事件参数</param>
    public delegate void FilterValueEventHandler( object sender , FilterValueEventArgs args );

    /// <summary>
    /// 数据过滤器事件参数
    /// </summary>
    public class FilterValueEventArgs : EventArgs
    {
        ///// <summary>
        ///// 初始化对象
        ///// </summary>
        //public FilterValueEventArgs()
        //{
        //}

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="source">数据来源</param>
        /// <param name="type">数据类型</param>
        /// <param name="Value">要处理的数据</param>
        public FilterValueEventArgs(InputValueSource source, InputValueType type , object Value)
        {
            this._Source = source;
            this._Type = type;
            this._Value = Value;
        }

        private InputValueSource _Source = InputValueSource.Clipboard;
        /// <summary>
        /// 数据来源
        /// </summary>
        public InputValueSource Source
        {
            get { return _Source; }
            set { _Source = value; }
        }

        private string _SourceName = null;
        /// <summary>
        /// 数据来源名称
        /// </summary>
        public string SourceName
        {
            get { return _SourceName; }
            set { _SourceName = value; }
        }

        private InputValueType _Type = InputValueType.Dom;
        /// <summary>
        /// 数据类型
        /// </summary>
        public InputValueType Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        private object _Value = null;
        /// <summary>
        /// 数据内容
        /// </summary>
        public object Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        private bool _Cancel = false;
        /// <summary>
        /// 取消相关的数据操作
        /// </summary>
        public bool Cancel
        {
            get { return _Cancel; }
            set { _Cancel = value; }
        }
    }

    public enum InputValueSource
    {
        /// <summary>
        /// 数据来自系统剪切板
        /// </summary>
        Clipboard ,
        /// <summary>
        /// 数据来用户界面的用户输入
        /// </summary>
        UI ,
        /// <summary>
        /// 未知
        /// </summary>
        Unknow
    }

    public enum InputValueType
    {
        /// <summary>
        /// 纯文本数据
        /// </summary>
        Text ,
        /// <summary>
        /// RTF文档数据
        /// </summary>
        RTF ,
        /// <summary>
        /// 图片数据
        /// </summary>
        Image ,
        /// <summary>
        /// 文件名
        /// </summary>
        FileName,
        /// <summary>
        /// 文档DOM数据
        /// </summary>
        Dom 
    }
     
}
