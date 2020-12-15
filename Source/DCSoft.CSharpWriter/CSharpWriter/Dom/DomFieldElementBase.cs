using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Drawing;
using DCSoft.Drawing;

namespace DCSoft.CSharpWriter.Dom
{
    /// <summary>
    /// 字段元素
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [Serializable]
    [System.Xml.Serialization.XmlType("XField")]
    [System.Diagnostics.DebuggerDisplay("Field")]
    public class DomFieldElementBase : DomContainerElement
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DomFieldElementBase()
        {
        }

        /// <summary>
        /// 初始化起始元素和结束元素
        /// </summary>
        protected virtual void CheckStartEndElement()
        {
            if (_StartElement == null)
            {
                DomFieldBorderElement border = new DomFieldBorderElement();
                border.Parent = this;
                border.Position = BorderElementPosition.Start;
                _StartElement = border;
            }
            _StartElement.StyleIndex = this.StyleIndex;
            if (_EndElement == null)
            {
                DomFieldBorderElement border = new DomFieldBorderElement();
                border.Parent = this;
                border.Position = BorderElementPosition.End;
                _EndElement = border;
            }
            _EndElement.StyleIndex = this.StyleIndex;
        }

        [System.ComponentModel.Browsable( false )]
        [System.Xml.Serialization.XmlIgnore()]
        public override DomDocument OwnerDocument
        {
            get
            {
                return base.OwnerDocument;
            }
            set
            {
                base.OwnerDocument = value;
                if (this.StartElement != null)
                {
                    this.StartElement.OwnerDocument = value;
                }
                if (this.EndElement != null)
                {
                    this.EndElement.OwnerDocument = value;
                }
            }
        }

        private DomElement _StartElement = null;
        /// <summary>
        /// 起始元素
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        [System.Xml.Serialization.XmlIgnore()]
        public DomElement StartElement
        {
            get
            {
                CheckStartEndElement();
                if (_StartElement != null)
                {
                    _StartElement.Parent = this;
                }
                return _StartElement; 
            }
            set
            {
                _StartElement = value; 
            }
        }

        private DomElement _EndElement = null;
        /// <summary>
        /// 结束元素
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        public DomElement EndElement
        {
            get
            {
                CheckStartEndElement();
                if (_EndElement != null)
                {
                    _EndElement.Parent = this;
                }
                return _EndElement;
            }
            set
            {
                _EndElement = value; 
            }
        }

        /// <summary>
        /// 文档域文本值
        /// </summary>
        [Browsable( false )]
        [System.Xml.Serialization.XmlElement]
        public string TextValue
        {
            get
            {
                return this.Text;
            }
            set
            {
            }
        }

        [NonSerialized]
        protected DomElementList _backgroundTextElements = null;

        /// <summary>
        /// 判断是否是背景文本元素
        /// </summary>
        /// <param name="element">文档元素对象</param>
        /// <returns>是否是背景文本元素</returns>
        public virtual bool IsBackgroundTextElement(DomElement element)
        {
            return _backgroundTextElements != null
                && _backgroundTextElements.Contains(element);
        }

        /// <summary>
        /// 重新计算文档元素内容大小
        /// </summary>
        /// <param name="args">参数</param>
        public override void RefreshSize(DocumentPaintEventArgs args)
        {
            base.RefreshSize(args);
            CheckStartEndElement();
            if (this.StartElement != null)
            {
                this.StartElement.RefreshSize(args);
            }
            if (this.EndElement != null)
            {
                this.EndElement.RefreshSize(args);
            }
            if (this._backgroundTextElements != null)
            {
                foreach (DomElement e in this._backgroundTextElements)
                {
                    e.RefreshSize(args);
                }
            }
        }

        /// <summary>
        /// 处理元素样式发生改变事件
        /// </summary>
        public override void OnStyleChanged()
        {
            base.OnStyleChanged();
            if (this.StartElement != null)
            {
                this.StartElement.StyleIndex = this.StyleIndex;
            }
            if (this.EndElement != null)
            {
                this.EndElement.StyleIndex = this.StyleIndex;
            }
            if (_backgroundTextElements != null)
            {
                foreach (DomElement element in _backgroundTextElements)
                {
                    element.StyleIndex = this.StyleIndex;
                }
            }
        }

        /// <summary>
        /// 修复文档DOM结构数据
        /// </summary>
        public override void FixDomState()
        {
            base.FixDomState();
            if (_backgroundTextElements != null)
            {
                foreach (DomElement element in _backgroundTextElements)
                {
                    element.Parent = this;
                    element.OwnerDocument = this.OwnerDocument ;
                    element.StyleIndex = this.StyleIndex;
                }
            }
        }

        /// <summary>
        /// 文档中第一个内容元素
        /// </summary>
        [Browsable( false )]
        public override DomElement FirstContentElement
        {
            get
            {
                if (this.StartElement == null)
                {
                    return base.FirstContentElement;
                }
                else
                {
                    return this.StartElement;
                }
            }
        }

        /// <summary>
        /// 文档中最后一个内容元素
        /// </summary>
        [Browsable( false )]
        public override DomElement LastContentElement
        {
            get
            {
                if (this.EndElement == null)
                {
                    return base.LastContentElement;
                }
                else
                {
                    return this.EndElement;
                }
            }
        }

        /// <summary>
        /// 获得输入焦点
        /// </summary>
        public override void Focus()
        {
            if (this.StartElement != null)
            {
                DomDocumentContentElement dce = this.DocumentContentElement;
                dce.SetSelection(this.StartElement.ViewIndex + 1, 0);
            }
            else
            {
                base.Focus();
            }
        }
        //private bool _Readonly = false;
        ///// <summary>
        ///// 字段值是否只读
        ///// </summary>
        //[System.ComponentModel.DefaultValue(false)]
        //public virtual bool Readonly
        //{
        //    get
        //    {
        //        return _Readonly;
        //    }
        //    set
        //    {
        //        _Readonly = value;
        //    }
        //}

        //private string _FormValue = null;
        ///// <summary>
        ///// 表单数值
        ///// </summary>
        //public string FormValue
        //{
        //    get { return _FormValue; }
        //    set { _FormValue = value; }
        //}



        /// <summary>
        /// 文档加载后的处理
        /// </summary>
        /// <param name="format"></param>
        public override void AfterLoad(FileFormat format)
        {
            base.AfterLoad(format);
            if (this.Elements.Count > 0)
            {
                if (this.StartElement != null)
                {
                    this.StartElement.StyleIndex = this.Elements[0].StyleIndex;
                }
                if (this.EndElement != null)
                {
                    this.EndElement.StyleIndex = this.Elements.LastElement.StyleIndex;
                }
            }
        }

        /// <summary>
        /// 添加文档内容
        /// </summary>
        /// <param name="content"></param>
        /// <param name="privateMode"></param>
        public override int AppendContent(DomElementList content, bool privateMode)
        {
            if (this.OwnerDocument.Printing)
            {
                // 若处于打印模式则只添加可见元素
                return base.AppendContent(content, privateMode);
            }
            else
            {
                int result = 0;
                if (this.StartElement != null)
                {
                    content.Add(this.StartElement);
                    result++;
                }
                result = result + base.AppendContent(content, privateMode);
                if (this.EndElement != null)
                {
                    content.Add(this.EndElement);
                    result++;
                }
                return result;
            }
        }
 
        /// <summary>
        /// 声明用户界面无效，需要重新绘制
        /// </summary>
        public override void InvalidateView()
        {
            if (this.Parent == null)
            {
                // 对象的父节点还没有,说明它还没插入到文档树状结构中
                // 此时对象时不可能显示,因此无需声明用户界面无效.
                return;
            }
            if ( this.OwnerDocument != null && this.OwnerDocument.EditorControl != null)
            {
                RectangleF rect = RectangleF.Empty;
                if (this.StartElement != null)
                {
                    rect = this.StartElement.AbsBounds;
                }
                if (this.EndElement != null)
                {
                    if (rect.IsEmpty)
                    {
                        rect = this.EndElement.AbsBounds;
                    }
                    else
                    {
                        rect = RectangleF.Union(rect, this.EndElement.AbsBounds);
                    }
                }
                DomElementList list = new DomElementList();
                this.AppendContent(list, false);
                foreach (DomElement element in list)
                {
                    if (rect.IsEmpty)
                    {
                        rect = element.AbsBounds;
                    }
                    else
                    {
                        rect = RectangleF.Union(rect, element.AbsBounds);
                    }
                }
                this.OwnerDocument.EditorControl.ViewInvalidate(
                    Rectangle.Ceiling( rect),
                    this.DocumentContentElement.ContentPartyStyle);
            }
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <param name="Deeply">复制品</param>
        /// <returns>复制品</returns>
        public override DomElement Clone(bool Deeply)
        {
            DomFieldElementBase field = ( DomFieldElementBase )  base.Clone(Deeply);
            if (this._StartElement != null)
            {
                field._StartElement = this._StartElement.Clone(Deeply);
                field._StartElement.Parent = this;
            }
            if (this._EndElement != null)
            {
                field._EndElement = this._EndElement.Clone(Deeply);
                field._EndElement.Parent = this;
            }
            field._backgroundTextElements = null;
            return field;
        }

    }
}
