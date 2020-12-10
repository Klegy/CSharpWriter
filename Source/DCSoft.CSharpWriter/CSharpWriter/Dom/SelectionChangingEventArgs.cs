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
using DCSoft.CSharpWriter.Controls ;

namespace DCSoft.CSharpWriter.Dom
{
    /// <summary>
    /// 选择区域正在发生改变事件委托类型
    /// </summary>
    /// <param name="sender">参数</param>
    /// <param name="args">参数</param>
    public delegate void SelectionChangingEventHandler(
            object sender ,
            SelectionChangingEventArgs args );

    /// <summary>
    /// 选择区域正在发生改变事件参数类型
    /// </summary>
    /// <remarks> 编制 袁永福</remarks>
    public class SelectionChangingEventArgs : EventArgs 
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public SelectionChangingEventArgs()
        {
        }
        ///// <summary>
        ///// 初始化对象
        ///// </summary>
        ///// <param name="oldSelectionIndex">旧的选择区域开始位置</param>
        ///// <param name="oldSelectionLength">旧的选择区域长度</param>
        ///// <param name="newSelectionIndex">新的选择区域开始位置</param>
        ///// <param name="newSelectionLength">新的选择区域长度</param>
        //public SelectionChangingEventArgs(
        //    int oldSelectionIndex, 
        //    int oldSelectionLength,
        //    int newSelectionIndex,
        //    int newSelectionLength)
        //{
        //    _OldSelectionIndex = oldSelectionIndex;
        //    _OldSelectionLength = oldSelectionLength;
        //    _NewSelectionIndex = newSelectionIndex;
        //    _NewSelectionLength = newSelectionLength;
        //}

        private DomDocument _Documnent = null;
        /// <summary>
        /// 文档对象
        /// </summary>
        public DomDocument Documnent
        {
            get { return _Documnent; }
            set { _Documnent = value; }
        }

        private bool _OldLineEndFlag = false;
        /// <summary>
        /// 旧的行尾标记
        /// </summary>
        public bool OldLineEndFlag
        {
            get { return _OldLineEndFlag; }
            set { _OldLineEndFlag = value; }
        }

        private int _OldSelectionIndex = 0;
        /// <summary>
        /// 旧的选择区域开始位置
        /// </summary>
        public int OldSelectionIndex
        {
            get { return _OldSelectionIndex; }
            set { _OldSelectionIndex = value; }
        }

        private int _OldSelectionLength = 0;
        /// <summary>
        /// 旧的选择区域长度
        /// </summary>
        public int OldSelectionLength
        {
            get { return _OldSelectionLength; }
            set { _OldSelectionLength = value; }
        }

        //private int _OldNativeSelectionIndex = 0;
        ///// <summary>
        ///// 旧的原始选择区域位置
        ///// </summary>
        //public int OldNativeSelectionIndex
        //{
        //    get { return _OldNativeSelectionIndex; }
        //    set { _OldNativeSelectionIndex = value; }
        //}
        //private int _OldNativeSelectionLength = 0;
        ///// <summary>
        ///// 旧的原始选择区域长度
        ///// </summary>
        //public int OldNativeSelectionLength
        //{
        //    get { return _OldNativeSelectionLength; }
        //    set { _OldNativeSelectionLength = value; }
        //}

        private bool _NewLineEndFlag = false;
        /// <summary>
        /// 新的行尾标记
        /// </summary>
        public bool NewLineEndFlag
        {
            get { return _NewLineEndFlag; }
            set { _NewLineEndFlag = value; }
        }

        private int _NewSelectionIndex = 0;
        /// <summary>
        /// 新的选择区域开始位置
        /// </summary>
        public int NewSelectionIndex
        {
            get { return _NewSelectionIndex; }
            set { _NewSelectionIndex = value; }
        }

        private int _NewSelectionLength = 0;
        /// <summary>
        /// 新的选择区域长度
        /// </summary>
        public int NewSelectionLength
        {
            get { return _NewSelectionLength; }
            set { _NewSelectionLength = value; }
        }

        //private int _NewNativeSelectionIndex = 0;
        ///// <summary>
        ///// 新的原始选择区域位置
        ///// </summary>
        //public int NewNativeSelectionIndex
        //{
        //    get { return _NewNativeSelectionIndex; }
        //    set { _NewNativeSelectionIndex = value; }
        //}
        //private int _NewNativeSelectionLength = 0;
        ///// <summary>
        ///// 新的原始选择区域长度
        ///// </summary>
        //public int NewNativeSelectionLength
        //{
        //    get { return _NewNativeSelectionLength; }
        //    set { _NewNativeSelectionLength = value; }
        //}


        private bool _Cancel = false;
        /// <summary>
        /// 用户取消操作
        /// </summary>
        public bool Cancel
        {
            get { return _Cancel; }
            set { _Cancel = value; }
        }
    }
}
