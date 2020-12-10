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

namespace DCSoft.CSharpWriter.Dom
{
    /// <summary>
    /// 文档内容发生改变事件委托类型
    /// </summary>
    /// <param name="sender">发起者</param>
    /// <param name="args">事件参数</param>
    /// <remarks>编写 袁永福</remarks>
    public delegate void ContentChangedEventHandler(
            object sender ,
            ContentChangedEventArgs args );

    /// <summary>
    /// 文档内容发生改变事件参数
    /// </summary>
    /// <remarks>编写 袁永福</remarks>
    public class ContentChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public ContentChangedEventArgs()
        {
        }

        private bool _UndoRedoCause = false;
        /// <summary>
        /// 由于进行重做/撤销操作而引发了该事件
        /// </summary>
        public bool UndoRedoCause
        {
            get
            {
                return _UndoRedoCause; 
            }
            set
            {
                _UndoRedoCause = value; 
            }
        }

        private DomDocument _Document = null;
        /// <summary>
        /// 文档对象
        /// </summary>
        public DomDocument Document
        {
            get
            {
                return _Document; 
            }
            set
            {
                _Document = value; 
            }
        }

        private bool _LoadingDocument = false;
        /// <summary>
        /// 正在加载文档
        /// </summary>
        public bool LoadingDocument
        {
            get
            {
                return _LoadingDocument; 
            }
            set
            {
                _LoadingDocument = value; 
            }
        }

        private object _Tag = null;
        /// <summary>
        /// 额外的数据
        /// </summary>
        public object Tag
        {
            get
            {
                return _Tag; 
            }
            set
            {
                _Tag = value; 
            }
        }

        private DomElement _Element = null;
        /// <summary>
        /// 容器元素对象
        /// </summary>
        public DomElement Element
        {
            get
            {
                return _Element; 
            }
            set
            {
                _Element = value; 
            }
        }

        private int _ElementIndex = 0;
        /// <summary>
        /// 发生操作时的元素位置序号
        /// </summary>
        public int ElementIndex
        {
            get
            {
                return _ElementIndex; 
            }
            set
            {
                _ElementIndex = value; 
            }
        }

        private DomElementList _DeletedElements = null;
        /// <summary>
        /// 正要删除的元素列表
        /// </summary>
        public DomElementList DeletedElements
        {
            get
            {
                return _DeletedElements; 
            }
            set
            {
                _DeletedElements = value; 
            }
        }

        private DomElementList _InsertedElements = null;
        /// <summary>
        /// 准备新增的元素列表
        /// </summary>
        public DomElementList InsertedElements
        {
            get { return _InsertedElements; }
            set { _InsertedElements = value; }
        }

        private bool _CancelBubble = false;
        /// <summary>
        /// 取消事件向上层元素冒泡传递
        /// </summary>
        public bool CancelBubble
        {
            get { return _CancelBubble; }
            set { _CancelBubble = value; }
        }
    }
}
