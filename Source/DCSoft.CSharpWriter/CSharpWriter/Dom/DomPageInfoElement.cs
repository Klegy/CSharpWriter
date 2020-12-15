using System;
using System.Collections.Generic;
using System.Text;
using DCSoft.CSharpWriter.RTF;

namespace DCSoft.CSharpWriter.Dom
{
    /// <summary>
    /// 页码文档元素对象
    /// </summary>
    [Serializable]
    [System.Xml.Serialization.XmlType("XPageInfo")]
    [System.Diagnostics.DebuggerDisplay("PageInfo:{ContentType}")]
    public class DomPageInfoElement : DomElement
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DomPageInfoElement()
        {
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="type">类型</param>
        public DomPageInfoElement(PageInfoContentType type)
        {
            _ContentType = type;
        }

        private PageInfoContentType _ContentType = PageInfoContentType.PageIndex;
        /// <summary>
        /// 内容样式
        /// </summary>
        [System.ComponentModel.DefaultValue( PageInfoContentType.PageIndex )]
        public PageInfoContentType ContentType
        {
            get
            {
                return _ContentType; 
            }
            set 
            {
                _ContentType = value; 
            }
        }

        /// <summary>
        /// 获得文本宽度
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public int TextWidth
        {
            get
            {
                int result = 0;
                DomDocument document = this.OwnerDocument;
                switch (this.ContentType)
                {
                    case PageInfoContentType.NumOfGlobalPages:
                        if (document == null)
                        {
                            result = 1;
                        }
                        else if (document.GlobalPages != null
                            && document.GlobalPages.Count > 0)
                        {
                            result = document.GlobalPages.Count.ToString().Length;
                        }
                        else if (document.Info != null)
                        {
                            result = document.Info.NumOfPage.ToString().Length ;
                        }
                        else
                        {
                            result = 1;
                        }
                        break;
                    case PageInfoContentType.NumOfPages:
                        if (document == null)
                        {
                            result = 1;
                        }
                        else if (document.Pages != null
                            && document.Pages.Count > 0)
                        {
                            result = document.Pages.Count.ToString().Length;
                        }
                        else if (document.Info != null)
                        {
                            result = document.Info.NumOfPage.ToString().Length;
                        }
                        else
                        {
                            result = 1;
                        }
                        break;
                    case PageInfoContentType.PageIndex:
                        if (document == null 
                            || document.Pages == null 
                            || document.Pages.Count == 0 )
                        {
                            result = 1;
                        }
                        else
                        {
                            result = (int)(Math.Ceiling(Math.Log10(document.Pages.Count)));
                        }
                        break;
                }
                return Math.Max(result, 2);
            }
        }

        /// <summary>
        /// 内容文本
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public string ContentText
        {
            get
            {
                DomDocument document = this.OwnerDocument;
                switch (this.ContentType)
                {
                    case PageInfoContentType.NumOfGlobalPages :
                        if (document == null)
                        {
                            return "0";
                        }
                        else if (document.GlobalPages != null
                            && document.GlobalPages.Count > 0)
                        {
                            return document.GlobalPages.Count.ToString();
                        }
                        else if (document.Info != null)
                        {
                            return document.Info.NumOfPage.ToString();
                        }
                        else
                        {
                            return "0";
                        }
                    case PageInfoContentType.NumOfPages :
                        if (document == null)
                        {
                            return "0";
                        }
                        else if (document.Pages != null
                            && document.Pages.Count > 0)
                        {
                            return document.Pages.Count.ToString();
                        }
                        else if (document.Info != null)
                        {
                            return document.Info.NumOfPage.ToString();
                        }
                        else
                        {
                            return "0";
                        }
                    case PageInfoContentType.PageIndex :
                        if (document == null)
                        {
                            return "1";
                        }
                        else
                        {
                            return document.PageIndex.ToString();
                        }
                }
                return "0";
            }
        }

        public override string ToPlaintString()
        {
            if (this.OwnerDocument != null)
            {
                return this.OwnerDocument.PageIndex.ToString();
            }
            else
            {
                return "";
            }
        }

        public override string ToDebugString()
        {
            return "PageIndex:" + this.ToPlaintString();
        }

        public override void WriteRTF(RTFContentWriter writer)
        {
            int level = writer.GroupLevel;
            writer.WriteStartGroup();
            writer.WriteKeyword("field");
            writer.WriteStartGroup();
            writer.WriteKeyword("fldinst", true);
            writer.WriteStartGroup();
            if (this.ContentType == PageInfoContentType.PageIndex)
            {
                writer.Writer.WriteRaw("PAGE");
            }
            else
            {
                writer.Writer.WriteRaw("NUMPAGES");
            }
            writer.WriteEndGroup();
            writer.WriteEndGroup();
            
            writer.WriteStartGroup();
            writer.WriteKeyword("fldrslt");
            writer.WriteStartGroup();
            writer.WriteStartString(this.ContentText, this.RuntimeStyle);
            writer.WriteEndString();
            writer.WriteEndGroup();
            writer.WriteEndGroup();
            writer.WriteEndGroup();
            level = writer.GroupLevel - level;
        }
    }

    /// <summary>
    /// 页码信息类型
    /// </summary>
    public enum PageInfoContentType
    {
        /// <summary>
        /// 从1开始计算的页码
        /// </summary>
        PageIndex ,
        /// <summary>
        /// 文档总页数
        /// </summary>
        NumOfPages,
        /// <summary>
        /// 所有的文档的总页数
        /// </summary>
        NumOfGlobalPages
    }
}
