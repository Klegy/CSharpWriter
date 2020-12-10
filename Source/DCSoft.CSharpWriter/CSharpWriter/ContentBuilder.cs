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
using DCSoft.CSharpWriter.Dom ;

namespace DCSoft.CSharpWriter
{
    /// <summary>
    /// 文档内容创建者
    /// </summary>
    /// <remarks>编制 袁永福 2012-8-23</remarks>
    public class ContentBuilder
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="container">容器对象</param>
        public ContentBuilder(DomContainerElement container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            _Container = container;
            _Document = container.OwnerDocument;
        }

        private DomDocument _Document = null;
        /// <summary>
        /// 文档对象
        /// </summary>
        public DomDocument Document
        {
            get { return _Document; }
        }
        private DomContainerElement _Container = null;
        /// <summary>
        /// 容器对象
        /// </summary>
        public DomContainerElement Container
        {
            get { return _Container; }
        }

        private DocumentContentStyle _ContentStyle = new DocumentContentStyle();
        /// <summary>
        /// 文档内容样式
        /// </summary>
        public DocumentContentStyle ContentStyle
        {
            get { return _ContentStyle; }
        //    set { _ContentStyle = value; }
        }

        private DocumentContentStyle _ParagraphStyle = new DocumentContentStyle();
        /// <summary>
        /// 段落样式
        /// </summary>
        public DocumentContentStyle ParagraphStyle
        {
            get { return _ParagraphStyle; }
        //    set { _ParagraphStyle = value; }
        }

        private bool _EnableAddPermissionFlag = true;
        /// <summary>
        /// 新增的文档内容添加授权标志信息
        /// </summary>
        public bool EnableAddPermissionFlag
        {
            get { return _EnableAddPermissionFlag; }
            set { _EnableAddPermissionFlag = value; }
        }

        /// <summary>
        /// 清空内容
        /// </summary>
        public void Clear()
        {
            this.Container.Elements.Clear();

            if (this.Container is DomContentElement)
            {
                ((DomContentElement)this.Container).FixElements();
            }
        }

        /// <summary>
        /// 设置当前段落样式
        /// </summary>
        /// <param name="style"></param>
        public void SetParagraphStyle(DocumentContentStyle style)
        {
            this._ParagraphStyle = style;
            if (this.Container.Elements.LastElement is DomParagraphFlagElement)
            {
                this.Container.Elements.LastElement.Style = style;
            }
        }

        /// <summary>
        /// 添加文本内容
        /// </summary>
        /// <param name="text">文本内容</param>
        /// <param name="style">样式</param>
        /// <returns></returns>
        public DomElementList Append(string text, DocumentContentStyle style)
        {
            if (style == null)
            {
                style = this.ContentStyle;
            }
            DomElementList list = this.Document.CreateTextElements(
                text, 
                style , 
                style,
                this.EnableAddPermissionFlag 
                && this.Document.Options.SecurityOptions.EnablePermission );
            Append(list);
            return list;
        }

        /// <summary>
        /// 使用默认样式添加文本内容
        /// </summary>
        /// <param name="text">文本内容</param>
        /// <returns>产生的元素对象</returns>
        public DomElementList Append(string text)
        {
            return Append(text, null);
        }
        /// <summary>
        /// 添加段落
        /// </summary>
        /// <param name="style">段落样式</param>
        /// <returns>新增的段落标记元素</returns>
        public DomParagraphFlagElement AppendParagraphFlag(DocumentContentStyle style)
        {
            DomParagraphFlagElement flag = new DomParagraphFlagElement();
            flag.OwnerDocument = this.Document;
            flag.Parent = this.Container;
            DocumentContentStyle rs = style == null ? this.ParagraphStyle : style;
            if (this.EnableAddPermissionFlag)
            {
                rs = ( DocumentContentStyle ) rs.Clone();
                rs.CreatorIndex = this.Document.UserHistories.CurrentIndex;
                rs.DeleterIndex = -1;
            }
            flag.Style = style;
            this.Append(flag);
            return flag;
        }

        public DomParagraphFlagElement AppendParagraphFlag()
        {
            return AppendParagraphFlag(null);
        }

        /// <summary>
        /// 添加文档元素
        /// </summary>
        /// <param name="element">要添加的内容</param>
        public void Append(DomElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.Parent = this.Container;
            element.OwnerDocument = this.Document;
            if (this.Container is DomContentElement && this.Container.Elements.Count > 0)
            {
                this.Container.Elements.Insert(this.Container.Elements.Count - 1, element);
            }
            else
            {
                this.Container.Elements.Add(element);
            }
        }

        /// <summary>
        /// 添加多个文档元素
        /// </summary>
        /// <param name="elements"></param>
        public void Append(DomElementList elements)
        {
            if (elements == null)
            {
                throw new ArgumentNullException("elements");
            }
            if (this.Container is DomContentElement && this.Container.Elements.Count > 0)
            {
                this.Container.Elements.InsertRange(this.Container.Elements.Count - 1, elements);
            }
            else
            {
                this.Container.Elements.AddRange(elements);
            }
        }
    }
}
