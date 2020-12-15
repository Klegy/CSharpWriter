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
using System.Xml.Serialization;
using DCSoft.Drawing;
using DCSoft.Printing;

namespace DCSoft.CSharpWriter.Dom
{
    /// <summary>
    /// 文档节元素
    /// </summary>
    [System.Xml.Serialization.XmlType("XTextSection")]
    public class DomSectionElement : DomContainerElement
    {
        public DomSectionElement()
        {
            this.AppendChildElement(new XTextDocumentHeaderElement());
            this.AppendChildElement(new XTextDocumentBodyElement());
            this.AppendChildElement(new XTextDocumentFooterElement());
        }

        private string _Name = null;
        /// <summary>
        /// 名称
        /// </summary>
        [DefaultValue( null )]
        public string Name
        {
            get
            {
                return _Name; 
            }
            set
            {
                _Name = value; 
            }
        }

        [NonSerialized()]
        private bool _PageRefreshed = false;
        /// <summary>
        /// 文档已经执行的排版和分页操作
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        public bool PageRefreshed
        {
            get
            {
                return _PageRefreshed;
            }
            set
            {
                _PageRefreshed = value;
            }
        }

        private XPageSettings _PageSettings = new XPageSettings();
        /// <summary>
        /// 页面设置信息对象
        /// </summary>
        [System.ComponentModel.Category("Layout")]
        [System.ComponentModel.RefreshProperties(
            System.ComponentModel.RefreshProperties.All)]
        public XPageSettings PageSettings
        {
            get
            {
                if (_PageSettings == null)
                {
                    _PageSettings = new XPageSettings();
                    this.PageRefreshed = false;
                }
                return _PageSettings;
            }
            set
            {
                _PageSettings = value;
                if (_PageSettings == null)
                {
                    _PageSettings = new XPageSettings();
                }
                this.PageRefreshed = false;
            }
        }

        /// <summary>
        /// 页眉对象
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DefaultValue(null)]
        [System.Xml.Serialization.XmlIgnore()]
        public XTextDocumentHeaderElement Header
        {
            get
            {
                foreach (DomElement element in this.Elements)
                {
                    if (element is XTextDocumentHeaderElement)
                    {
                        return (XTextDocumentHeaderElement)element;
                    }
                }
                XTextDocumentHeaderElement header = new XTextDocumentHeaderElement();
                header.IsEmpty = true;
                //header.FixElements();
                this.AppendChildElement(header);
                //header.Elements.Add(new XTextParagraphFlagElement());
                return header;
            }
        }

        /// <summary>
        /// 文档正文对象
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DefaultValue(null)]
        [System.Xml.Serialization.XmlIgnore()]
        public XTextDocumentBodyElement Body
        {
            get
            {
                foreach (DomElement element in this.Elements)
                {
                    if (element is XTextDocumentBodyElement)
                    {
                        return (XTextDocumentBodyElement)element;
                    }
                }
                XTextDocumentBodyElement body = new XTextDocumentBodyElement();
                this.AppendChildElement(body);
                //body.FixElements();
                //body.Elements.Add(new XTextParagraphFlagElement());// this.CreateParagraphEOF());
                return body;
            }
        }

        /// <summary>
        /// 页脚对象
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DefaultValue(null)]
        [System.Xml.Serialization.XmlIgnore()]
        public XTextDocumentFooterElement Footer
        {
            get
            {
                foreach (DomElement element in this.Elements)
                {
                    if (element is XTextDocumentFooterElement)
                    {
                        return (XTextDocumentFooterElement)element;
                    }
                }
                XTextDocumentFooterElement footer = new XTextDocumentFooterElement();
                //footer.Elements.Add(this.CreateParagraphEOF());
                footer.IsEmpty = true;
                this.AppendChildElement(footer);
                //footer.Elements.Add(new XTextParagraphFlagElement());
                return footer;
            }
        }

        public override void AfterLoad(FileFormat format)
        {
            XTextDocumentBodyElement body = this.Body;
            //body.Elements.Clear();
            for (int iCount = this.Elements.Count - 1; iCount >= 0; iCount--)
            {
                DomElement element = this.Elements[iCount];
                if (element is DomDocumentContentElement)
                {
                }
                else
                {
                    this.Elements.RemoveAtRaw(iCount);
                    //body.AppendChildElement(element);
                    body.Elements.Insert(0, element);
                    element.Parent = body;
                    element.OwnerDocument = this.OwnerDocument ;
                }
            }//for
            base.AfterLoad(format);
        }

        internal void ClearContent()
        {
            this.Elements.Clear();
            DomDocumentContentElement ce = this.Body;
            ce.AppendChildElement(this.OwnerDocument.CreateParagraphEOF());
            ce.UpdateContentElements(true);
            ce.SetSelection(0, 0);

            ce = this.Header;
            ce.FixElements();
            DocumentContentStyle style = new DocumentContentStyle();

            ce.UpdateContentElements(true);
            ce.SetSelection(0, 0);

            ce = this.Footer;
            ce.FixElements();
            ce.UpdateContentElements(true);
            ce.SetSelection(0, 0);
        }

    }//public class XTextSectionElement : XTextContainerElement
}
