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
    /// 执行ReplaceElements函数使用的参数
    /// </summary>
    public class ReplaceElementsArgs
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public ReplaceElementsArgs()
        {
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="container">要替换子元素的容器元素对象</param>
        /// <param name="startIndex">替换区域的开始序号</param>
        /// <param name="deleteLength">要删除的子元素的个数</param>
        /// <param name="newElements">要插入的新元素列表</param>
        /// <param name="logUndo">是否记录撤销操作信息</param>
        /// <param name="updateContent">是否更新文档视图</param>
        /// <param name="raiseEvent">是否触发事件</param>
        public ReplaceElementsArgs(
            DomContainerElement container,
            int startIndex,
            int deleteLength,
            DomElementList newElements,
            bool logUndo,
            bool updateContent,
            bool raiseEvent)
        {
            _Container = container;
            _StartIndex = startIndex;
            _DeleteLength = deleteLength;
            _NewElements = newElements;
            _LogUndo = logUndo;
            _UpdateContent = updateContent;
            _RaiseEvent = raiseEvent;
        }


        private DomContainerElement _Container = null;
        /// <summary>
        /// 容器元素对象
        /// </summary>
        public DomContainerElement Container
        {
            get { return _Container; }
            set { _Container = value; }
        }

        private int _StartIndex = 0;
        /// <summary>
        /// 操作的开始区域序号
        /// </summary>
        public int StartIndex
        {
            get { return _StartIndex; }
            set { _StartIndex = value; }
        }
        private int _DeleteLength = 0;
        /// <summary>
        /// 要删除的区域的长度
        /// </summary>
        public int DeleteLength
        {
            get { return _DeleteLength; }
            set { _DeleteLength = value; }
        }

        private DomElementList _NewElements = null;
        /// <summary>
        /// 新插入的元素对象列表
        /// </summary>
        public DomElementList NewElements
        {
            get { return _NewElements; }
            set { _NewElements = value; }
        }
        private bool _LogUndo = true;
        /// <summary>
        /// 是否记录撤销操作信息
        /// </summary>
        public bool LogUndo
        {
            get { return _LogUndo; }
            set { _LogUndo = value; }
        }
        private bool _UpdateContent = true;
        /// <summary>
        /// 是否更新文档视图
        /// </summary>
        public bool UpdateContent
        {
            get { return _UpdateContent; }
            set { _UpdateContent = value; }
        }
        private bool _RaiseEvent = true;
        /// <summary>
        /// 是否触发相关事件
        /// </summary>
        public bool RaiseEvent
        {
            get { return _RaiseEvent; }
            set { _RaiseEvent = value; }
        }

        private bool _DisablePermission = false;
        /// <summary>
        /// 禁止权限控制
        /// </summary>
        public bool DisablePermission
        {
            get { return _DisablePermission; }
            set { _DisablePermission = value; }
        }

        private DomAccessFlags _AccessFlags = DomAccessFlags.Normal ;
        /// <summary>
        /// 访问行为标记
        /// </summary>
        public DomAccessFlags AccessFlags
        {
            get { return _AccessFlags; }
            set { _AccessFlags = value; }
        }

        private bool _ChangeSelection = true;
        /// <summary>
        /// 是否修改文档文档的选择状态
        /// </summary>
        public bool ChangeSelection
        {
            get { return _ChangeSelection; }
            set { _ChangeSelection = value; }
        }
    }
}
