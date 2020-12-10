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
    /// 文档高亮度显示区域管理器
    /// </summary>
    public class HighlightManager
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public HighlightManager()
        {
        }
        private DomDocument _Document = null;
        /// <summary>
        /// 操作的文档对象
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


        /// <summary>
        /// 鼠标悬停处的高亮度显示区域对象
        /// </summary>
        [NonSerialized]
        private HighlightInfo _HoverHighlightInfo = null;

        public HighlightInfo HoverHighlightInfo
        {
            get { return _HoverHighlightInfo; }
            set { _HoverHighlightInfo = value; }
        }

        /// <summary>
        /// 高亮度显示的区域
        /// </summary>
        [NonSerialized()]
        private HighlightInfoList _HighlightRanges = null;
        /// <summary>
        /// 用户设置的高亮度显示的区域列表
        /// </summary>
        public HighlightInfoList HighlightRanges
        {
            get
            {
                return _HighlightRanges;
            }
            set
            {
                _HighlightRanges = value;
            }
        }

        /// <summary>
        /// 用户设置的高亮度显示区域
        /// </summary>
        public HighlightInfo HighlightRange
        {
            get
            {
                if (this._HighlightRanges == null || this._HighlightRanges.Count == 0)
                {
                    return null;
                }
                else
                {
                    return this._HighlightRanges[0];
                }
            }
            set
            {
                if (this._HighlightRanges == null)
                {
                    this._HighlightRanges = new HighlightInfoList();
                }
                this._HighlightRanges.Clear();
                if (value != null)
                {
                    this._HighlightRanges.Add(value);
                }
            }
        }

        /// <summary>
        /// 内部自动设置的高亮度显示区域列表
        /// </summary>
        [NonSerialized]
        private HighlightInfoList _InnerHighlightInfos = null;

        /// <summary>
        /// 更新高亮度显示区域信息
        /// </summary>
        internal void UpdateHighlightInfos()
        {
            _InnerHighlightInfos = null;
            if (this.HighlightRanges != null)
            {
                // 让用户指定的高亮度区域状态无效
                foreach (HighlightInfo info in this.HighlightRanges)
                {
                    if (info.Range != null)
                    {
                        info.Range.Invalidate();
                    }
                }
            }
            _HoverHighlightInfo = null;
        }

        ///// <summary>
        ///// 清空系统内置高亮度显示区域
        ///// </summary>
        //internal void ClearInnerHighlightInfos()
        //{
        //    if (_InnerHighlightInfos != null && this.EditorControl != null )
        //    {
        //        foreach (HighlightInfo info in _InnerHighlightInfos)
        //        {
        //            if (info.Range != null)
        //            {
        //                this.InvalidateView(info.Range);
        //            }
        //        }
        //    }
        //    _InnerHighlightInfos = null;
        //}

        /// <summary>
        /// 删除指定元素相关的高亮度区域信息
        /// </summary>
        /// <param name="element">文档元素对象</param>
        public virtual void Remove(DomElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            if (_InnerHighlightInfos != null)
            {
                // 删除内置高亮度区域列表
                Remove(_InnerHighlightInfos, element);
            }
            if (_HighlightRanges != null)
            {
                // 删除用户高亮度区域列表
                Remove(_HighlightRanges, element);
            }
            if (_InvalidateHighlightInfoElements != null
                && _InvalidateHighlightInfoElements.Count > 0)
            {
                for (int iCount = _InvalidateHighlightInfoElements.Count - 1; iCount >= 0; iCount--)
                {
                    DomElement e2 = _InvalidateHighlightInfoElements[iCount];
                    if (e2 == element || e2.IsParentOrSupParent(element))
                    {
                        _InvalidateHighlightInfoElements.RemoveAt(iCount);
                    }
                }
            }
        }

        private void Remove(HighlightInfoList list, DomElement element)
        {
            for (int iCount = list.Count - 1; iCount >= 0; iCount--)
            {
                HighlightInfo info = list[iCount];
                bool delete = false;
                if (info.OwnerElement == element)
                {
                    delete = true;
                }
                else if (info.OwnerElement != null
                    && info.OwnerElement.IsParentOrSupParent(element))
                {
                    delete = true;
                }
                if (delete)
                {
                    if (info.Range != null)
                    {
                        this.Document.InvalidateView(info.Range);
                    }
                    list.RemoveAt(iCount);
                }
            }//for
        }

        /// <summary>
        /// 声明指定的元素相关的高亮度显示区域无效,需要重新设置
        /// </summary>
        /// <param name="element">文档元素对象</param>
        /// <param name="deleteElement">由于删除元素操作而执行本方法，
        /// 此时不需要将元素添加到高亮度区域状态未知的元素列表</param>
        public void InvalidateHighlightInfo(DomElement element)
        {
            if (element is DomCharElement)
            {
                // 字符元素不能设置为高亮度区域，因此不处理，提高效率
                return;
            }
            if (_InnerHighlightInfos != null)
            {
                if (_InvalidateHighlightInfoElements == null)
                {
                    _InvalidateHighlightInfoElements = new DomElementList();
                }
                if (_InvalidateHighlightInfoElements.Contains(element) == false)
                {
                    _InvalidateHighlightInfoElements.Add(element);
                    if (_InnerHighlightInfos != null)
                    {
                        for (int iCount = _InnerHighlightInfos.Count - 1; iCount >= 0; iCount--)
                        {
                            HighlightInfo info = _InnerHighlightInfos[iCount];
                            bool delete = false;
                            if (info.OwnerElement == element)
                            {
                                delete = true;
                            }
                            else if (info.OwnerElement != null
                                && info.OwnerElement.IsParentOrSupParent(element))
                            {
                                delete = true;
                            }
                            if (delete)
                            {
                                if (info.Range != null)
                                {
                                    this.Document.InvalidateView(info.Range);
                                    if (info.OwnerElement != null
                                        && _InvalidateHighlightInfoElements.Contains(info.OwnerElement) == false)
                                    {
                                        _InvalidateHighlightInfoElements.Add(info.OwnerElement);
                                    }
                                }
                                _InnerHighlightInfos.RemoveAt(iCount);
                            }
                        }//for
                    }//if
                }//if
            }
        }

        [NonSerialized]
        private DomElementList _InvalidateHighlightInfoElements = new DomElementList();

        /// <summary>
        /// 内部自动设置的高亮度显示区域列表
        /// </summary>
        internal HighlightInfoList InnerHighlightInfos
        {
            get
            {
                if (_InnerHighlightInfos == null)
                {
                    _InnerHighlightInfos = new HighlightInfoList();
                    this.Document.Enumerate(delegate(object sender, ElementEnumerateEventArgs args)
                    {
                        if (args.Element.Visible)
                        {
                            HighlightInfoList list = args.Element.GetHighlightInfos();
                            if (list != null && list.Count > 0)
                            {
                                foreach (HighlightInfo info in list)
                                {
                                    if (info.OwnerElement == null
                                        || _InnerHighlightInfos.ContainsOwnerElement(info.OwnerElement) == false)
                                    {
                                        if ( info.Range != null && info.Range.CheckState(false))
                                        {
                                            _InnerHighlightInfos.Add(info);
                                        }
                                    }
                                }//foreach
                            }//if
                        }
                        else
                        {
                            args.CancelChild = true;
                        }
                    },
                        false);
                }
                else if (_InvalidateHighlightInfoElements != null
                    && _InvalidateHighlightInfoElements.Count > 0)
                {
                    // 为声明了无效高亮度区域的元素添加高亮度区域
                    foreach (DomElement element in _InvalidateHighlightInfoElements)
                    {
                        bool ignore = false;
                        DomElement parent = element;
                        while (parent != null)
                        {
                            if (parent.Visible == false)
                            {
                                ignore = true;
                            }
                            parent = parent.Parent;
                        }
                        if (ignore == false)
                        {
                            HighlightInfoList list = element.GetHighlightInfos();
                            if (list != null && list.Count > 0)
                            {
                                foreach (HighlightInfo info in list)
                                {
                                    if (info.OwnerElement == null
                                        || _InnerHighlightInfos.ContainsOwnerElement(info.OwnerElement) == false)
                                    {
                                        if (info.Range != null && info.Range.CheckState(false))
                                        {
                                            _InnerHighlightInfos.Add(info);
                                        }
                                    }//if
                                }//foreach
                            }//if
                        }
                    }//foreach
                    _InvalidateHighlightInfoElements = null;
                    _InnerHighlightInfos.SortInfo();
                }
                return _InnerHighlightInfos;
            }
        }

        /// <summary>
        /// 获得指定元素所在的高亮度显示区域
        /// </summary>
        /// <param name="element">指定的文档元素对象</param>
        /// <returns>获得的高亮度显示区域</returns>
        public virtual HighlightInfo this[DomElement element]
        {
            get
            {
                if (element == null)
                {
                    return null;
                }
                // 首先搜索鼠标悬浮高亮度显示区域,该区域优先级最高而且最容易被命中
                if (_HoverHighlightInfo != null)
                {
                    if (_HoverHighlightInfo.Contains(element))
                    {
                        return _HoverHighlightInfo;
                    }
                }

                // 搜索用户设置的高亮度显示区域，用户设置的优先处理
                if (this.HighlightRanges != null && this.HighlightRanges.Count > 0)
                {
                    HighlightInfo info = this.HighlightRanges[element];
                    if (info != null)
                    {
                        return info;
                    }
                }

                // 搜索内部自动生成的区域
                if (this.InnerHighlightInfos.Count > 0)
                {
                    HighlightInfo info = this.InnerHighlightInfos[element];
                    if (info != null)
                    {
                        return info;
                    }
                }
                return null;
            }
        }
    }
}
