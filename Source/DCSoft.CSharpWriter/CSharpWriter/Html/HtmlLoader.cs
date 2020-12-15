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
using DCSoft.HtmlDom;
using DCSoft.CSharpWriter.Dom;
using System.Drawing ;
using System.Drawing.Drawing2D;
using DCSoft.Drawing;
using DCSoft.CSharpWriter.Data;
using DCSoft.Common;

namespace DCSoft.CSharpWriter.Html
{
    internal class HtmlLoader
    {
        private DomDocument _DomDocument = null;
        /// <summary>
        /// 文本文档对象
        /// </summary>
        public DomDocument DomDocument
        {
            get { return _DomDocument; }
            set { _DomDocument = value; }
        }

        private HTMLDocument _HtmlDocument = null;

        public HTMLDocument HtmlDocument
        {
            get { return _HtmlDocument; }
            set { _HtmlDocument = value; }
        }

        /// <summary>
        /// 根据HTML文档填充文本文档
        /// </summary>
        /// <param name="htmlDoc">HTML文档</param>
        /// <param name="doc">文本文档</param>
        public void Load(HTMLDocument htmlDoc, DomDocument doc)
        {
            this.DomDocument = doc;
            this.HtmlDocument = htmlDoc;
            doc.Clear();
            doc.FileName = htmlDoc.Location;
            doc.Info.Title = htmlDoc.Title;
            _Styles = new Dictionary<string, HTMLStyle>();
            foreach (HTMLStyleElement element in htmlDoc.AllStyles)
            {
                foreach (HTMLNameStyleItem item in element.Items)
                {
                    _Styles[item.Name] = item;
                }
            }
            CreateElements(htmlDoc.Body, doc.Body, doc.DefaultStyle);
        }

        private void CreateElements(
            HTMLElement rootHtmlElement,
            DomElement rootDomElement ,
            DocumentContentStyle currentStyle )
        {
            if (rootHtmlElement.ChildNodes == null)
            {
                return;
            }
            foreach (HTMLElement element in rootHtmlElement.ChildNodes )
            {
                if (element is HTMLCommentElement)
                {
                    // 忽略注释
                    continue;
                }
                string tagName = element.FixTagName;
                switch (tagName)
                {
                    case "div":
                        {
                            CreateElements(element, rootDomElement, currentStyle);
                            DomElement p = this.DomDocument.CreateElement(typeof(DomParagraphFlagElement));
                            p.StyleIndex = this.DomDocument.ContentStyles.GetStyleIndex(currentStyle);
                            rootDomElement.Elements.Add(p);
                        }
                        break;
                    case "ul":// 原点式列表
                    case "ol":// 数值式列表
                        foreach (HTMLElement li in element.ChildNodes)
                        {
                            if (li.FixTagName == "li")
                            {
                                CreateElements(li, rootDomElement, currentStyle);
                                DomParagraphFlagElement flag = (DomParagraphFlagElement)this.DomDocument.CreateElement(typeof(DomParagraphFlagElement));
                                DocumentContentStyle s = (DocumentContentStyle)currentStyle.Clone();
                                if (tagName == "ul")
                                {
                                    s.BulletedList = true;
                                }
                                else
                                {
                                    s.NumberedList = true;
                                }
                                flag.StyleIndex = this.DomDocument.ContentStyles.GetStyleIndex(s);
                                rootDomElement.Elements.Add(flag);
                            }//if
                        }//foreach
                        break;
                    case "pre":
                        {
                            // 预览文本
                            DocumentContentStyle s = CreateStyle(element, currentStyle);
                            DomElementList list = this.DomDocument.CreateTextElements(element.InnerText, null, s);
                            rootDomElement.Elements.AddRange(list);
                            DomElement p = this.DomDocument.CreateElement(typeof(DomParagraphFlagElement));
                            p.StyleIndex = this.DomDocument.ContentStyles.GetStyleIndex(currentStyle);
                            rootDomElement.Elements.Add( p );
                            break;
                        }
                    
                    case "img":
                        {
                            // 图片
                            DocumentContentStyle s = CreateStyle(element, currentStyle);
                            s.BorderColor = Color.Black;
                            s.BorderLeft = true;
                            s.BorderTop = true;
                            s.BorderRight = true;
                            s.BorderBottom = true;
                            s.BorderWidth = 0;
                            if (element.HasAttribute("border"))
                            {
                                s.BorderWidth = ToInt32(element.GetAttribute("border"));
                            }
                            DomImageElement img = (DomImageElement)this.DomDocument.CreateElement(typeof(DomImageElement));

                            img.StyleIndex = this.DomDocument.ContentStyles.GetStyleIndex(s);
                            if (element.HasAttribute("width"))
                            {
                                img.Width =(float ) ToLength(element.GetAttribute("width"));
                            }
                            if (element.HasAttribute("height"))
                            {
                                img.Height = ( float ) ToLength(element.GetAttribute("height"));
                            }
                            if (element.HasAttribute("id"))
                            {
                                img.ID = element.GetAttribute("id");
                            }
                            if (element.HasAttribute("alt"))
                            {
                                img.Alt = element.GetAttribute("alt");
                            }
                            if (element.HasAttribute("title"))
                            {
                                img.Title = element.GetAttribute("title");
                            }
                            if( element.HasAttribute( "src"))
                            {
                                XImageValue v = new XImageValue();
                                string url = this.HtmlDocument.GetAbsoluteURL(element.GetAttribute("src"));
                                try
                                {
                                    string msg = string.Format(WriterStrings.Downloading_URL, url);
                                    if (this.DomDocument.EditorControl != null)
                                    {
                                        this.DomDocument.EditorControl.SetStatusText( msg );
                                    }
                                    if (this.DomDocument.Options.BehaviorOptions.DebugMode)
                                    {
                                        System.Diagnostics.Debug.Write(msg);
                                    }
                                    int len = v.Load(url);
                                    if (this.DomDocument.Options.BehaviorOptions.DebugMode)
                                    {
                                        System.Diagnostics.Debug.WriteLine(WriterUtils.FormatByteSize(len));
                                    }
                                }
                                catch (Exception ext)
                                {
                                    img.Alt = url + ":" + ext.Message ;
                                    if (this.DomDocument.Options.BehaviorOptions.DebugMode)
                                    {
                                        System.Diagnostics.Debug.WriteLine(WriterStrings.Fail);
                                    }
                                }
                                if (this.DomDocument.EditorControl != null)
                                {
                                    this.DomDocument.EditorControl.SetStatusText(null);
                                }
                                if (v.HasContent == false )
                                {
                                    img.Alt = url;
                                    if (img.Width == 0)
                                    {
                                        img.Width = 300;
                                    }
                                    if (img.Height == 0)
                                    {
                                        img.Height = 150;
                                    }
                                }
                                img.Image = v;
                            }
                            
                            if (img.Width == 0 || img.Height == 0)
                            {
                                img.UpdateSize();
                            }
                            rootDomElement.Elements.Add(img);
                        }
                        break;
                    case "#text":
                        {
                            // 纯文本片段
                            string text = DCSoft.Common.StringFormatHelper.NormalizeSpace( element.Text) ;
                            text = System.Web.HttpUtility.HtmlDecode(text);
                            if (string.IsNullOrEmpty(text) == false)
                            {
                                DomElementList cs =this._DomDocument.CreateTextElements(
                                    text,
                                    currentStyle ,
                                    currentStyle );
                                if (cs != null && cs.Count > 0)
                                {
                                    rootDomElement.Elements.AddRange(cs);
                                }
                            }
                        }
                        break;
                    case "p":
                        {
                            // 段落
                            DocumentContentStyle ps = CreateStyle(element, currentStyle);
                            CreateElements(element, rootDomElement, ps);
                            DomParagraphFlagElement flag = ( DomParagraphFlagElement ) _DomDocument.CreateElement(typeof(DomParagraphFlagElement));
                            flag.StyleIndex = _DomDocument.ContentStyles.GetStyleIndex(ps);
                            rootDomElement.Elements.Add(flag);
                        }
                        break;
                    case "br":
                        {
                            // 软回车
                            DomLineBreakElement lb = (DomLineBreakElement)_DomDocument.CreateElement(typeof(DomLineBreakElement));
                            rootDomElement.Elements.Add(lb);
                        }
                        break;
                    case "sup":
                        {
                            // 上标
                            DocumentContentStyle ss = ( DocumentContentStyle ) currentStyle.Clone();
                            ss.Superscript = true;
                            CreateElements(element, rootDomElement, ss);
                        }
                        break;
                    case "sub":
                        {
                            // 下标
                            DocumentContentStyle ss = (DocumentContentStyle)currentStyle.Clone();
                            ss.Subscript = true;
                            CreateElements(element, rootDomElement, ss);
                        }
                        break;
                    case "strong":
                    case "b":
                        {
                            // 粗体
                            DocumentContentStyle ss = (DocumentContentStyle)currentStyle.Clone();
                            ss.Bold = true;
                            CreateElements(element, rootDomElement, ss);
                        }
                        break;
                    case "i":
                        {
                            // 斜体
                            DocumentContentStyle ss = (DocumentContentStyle)currentStyle.Clone();
                            ss.Italic = true;
                            CreateElements(element, rootDomElement, ss);
                        }
                        break;
                    case "strike":
                        {
                            // 删除线
                            DocumentContentStyle ss = (DocumentContentStyle)currentStyle.Clone();
                            ss.Strikeout = true;
                            CreateElements(element, rootDomElement, ss);
                        }
                        break;
                    case "a":
                        {
                            // 超链接
                            DocumentContentStyle ss = (DocumentContentStyle)currentStyle.Clone();
                            ss.Color = Color.Blue;
                            CreateElements(element, rootDomElement, ss);
                        }
                        break;
                    case "font":
                        {
                            // 字体
                            DocumentContentStyle ss = CreateStyle(element, currentStyle);
                            if (element.HasAttribute("color"))
                            {
                                // 文字颜色
                                ss.Color = ToColor(element.GetAttribute("color"),Color.Black );
                            }
                            if (element.HasAttribute("face"))
                            {
                                // 字体名称
                                ss.FontName = GetFontName(element.GetAttribute("face"));
                            }
                            if (element.HasAttribute("size"))
                            {
                                // 文字大小
                                int size = ToInt32(element.GetAttribute("size"));
                                switch (size)
                                {
                                    case 1: ss.FontSize = 7; break;
                                    case 2: ss.FontSize = 10; break;
                                    case 3: ss.FontSize = 12; break;
                                    case 4: ss.FontSize = 14; break;
                                    case 5: ss.FontSize = 18; break;
                                    case 6: ss.FontSize = 24; break;
                                    case 7: ss.FontSize = 35; break;
                                }
                            }
                            CreateElements(element, rootDomElement, ss);
                        }
                        break;
                    case "h1":
                    case "h2":
                    case "h3":
                    case "h4":
                    case "h5":
                    case "h6":
                        {
                            // 标题
                            rootDomElement.Elements.Add(this.DomDocument.CreateElement(typeof(DomParagraphFlagElement)));
                            float fz = 9;
                            switch (tagName)
                            {
                                case "h1": fz = 24; break;
                                case "h2": fz = 18; break;
                                case "h3": fz = 13; break;
                                case "h4": fz = 12; break;
                                case "h5": fz = 10; break;
                                case "h6": fz = 8; break;
                            } 
                            DocumentContentStyle ss = CreateStyle(element, currentStyle);
                            if (XDependencyObject.HasPropertyValue(ss, "FontSize"))
                            {
                                ss.FontSize = fz;
                            }
                            CreateElements(element, rootDomElement, ss);
                            rootDomElement.Elements.Add(this.DomDocument.CreateElement(typeof(DomParagraphFlagElement)));
                        }
                        break;
                     
                    default:
                        {
                            DocumentContentStyle ds = CreateStyle(element, (DocumentContentStyle)currentStyle );
                            CreateElements(element, rootDomElement , ds );
                        }
                        break;
                }//switch
            }//foreach
        }

        private Dictionary<string, HTMLStyle> _Styles = new Dictionary<string,HTMLStyle>();
         
        /// <summary>
        /// 创建文档样式对象
        /// </summary>
        /// <param name="element">HTML文档元素</param>
        /// <param name="currentStyle">当前文档样式</param>
        /// <returns>创建的样式对象</returns>
        private DocumentContentStyle CreateStyle(HTMLElement element , DocumentContentStyle currentStyle )
        {
            DocumentContentStyle result = ( DocumentContentStyle ) currentStyle.Clone();
            string name = "#" + element.TagName;
            if (_Styles.ContainsKey(name))
            {
                // 获得元素全局样式
                ApplyHtmlStyle(_Styles[name], result);
            }

            string cn = element.ClassName;
            if (string.IsNullOrEmpty(cn) == false)
            {
                cn = "." + cn;
                if (_Styles.ContainsKey(cn))
                {
                    // 获得特定名称的样式
                    ApplyHtmlStyle(_Styles[cn], result);
                }
            }
            cn = element.FixTagName + "." + element.ClassName;
            if (string.IsNullOrEmpty(cn) == false)
            {
                if (_Styles.ContainsKey(cn))
                {
                    // 获得元素特定样式
                    ApplyHtmlStyle(_Styles[cn], result);
                }
            }
            // 解释元素的style属性
            string attr = element.GetAttribute("style");
            if (string.IsNullOrEmpty(attr) == false)
            {
                HTMLStyle es = new HTMLStyle();
                es.CSSString = attr;
                ApplyHtmlStyle(es, result);
            }
            return result;
        }

        private void ApplyHtmlStyle(HTMLStyle style, DocumentContentStyle cstyle)
        {
            foreach (HTMLAttribute attr in style.Attributes)
            {
                string styleName = attr.Name.Trim().ToLower();
                string styleValue = attr.Value  ;
                ParseStyleItem(cstyle, styleName, styleValue);
            }
        }
        private void ParseStyleItem(DocumentContentStyle cstyle, string styleName, string styleValue)
        {
            switch (styleName)
            {
                case "width":
                    {
                        float v = 0;
                        if (TryParseLength(styleValue, out v))
                        {
                            cstyle.WidthValue = v;
                        }
                    }
                    break;
                case "height":
                    {
                        float v = 0;
                        if (TryParseLength(styleValue, out v))
                        {
                            cstyle.HeightValue = v;
                        }
                    }
                    break;
                case "background":
                    {
                        // 解析背景设置
                        string[] items = SplitItems(styleValue);
                        if (items != null)
                        {
                            foreach (string item in items)
                            {
                                Color c = Color.Empty;
                                string url = GetAttributeUrl(item);
                                if (url != null)
                                {
                                    url = this.HtmlDocument.GetAbsoluteURL(url);
                                    XImageValue img = new XImageValue();
                                    if (img.Load(url) <= 0 )
                                    {
                                        cstyle.BackgroundImage = img;
                                    }
                                }
                                else if (TryParseColor(item, out c))
                                {
                                    cstyle.BackgroundColor = c;
                                }
                                else if (string.Equals(item, "repeat", StringComparison.CurrentCultureIgnoreCase))
                                {
                                    cstyle.BackgroundRepeat = true;
                                }
                                else if (string.Equals(item, "no-repeat", StringComparison.CurrentCultureIgnoreCase))
                                {
                                    cstyle.BackgroundRepeat = false;
                                }
                            }//foreach
                        }
                    }
                    break;
                case "background-color":
                    {
                        Color c = Color.Empty;
                        if (TryParseColor(styleValue, out c))
                        {
                            cstyle.BackgroundColor = c;
                        }
                    }
                    break;
                case "background-repeat":
                    {
                        string text = Trim(styleValue);
                        if (string.Equals(text, "repeat", StringComparison.CurrentCultureIgnoreCase))
                        {
                            cstyle.BackgroundRepeat = true;
                        }
                        else if (string.Equals(text, "no-repeat", StringComparison.CurrentCultureIgnoreCase))
                        {
                            cstyle.BackgroundRepeat = false;
                        }
                        else
                        {
                            cstyle.BackgroundRepeat = true;
                        }
                    }
                    break;
                case "border":
                    {
                        // 解析边框设置
                        string[] items = SplitItems(styleValue);
                        if (items != null)
                        {
                            foreach (string item in items)
                            {
                                DashStyle ds = DashStyle.Custom;
                                Color c = Color.Empty;
                                float w = 0;
                                if (TryParseBorderStyle(item, out ds))
                                {
                                    cstyle.BorderStyle = ds;
                                    if (ds == DashStyle.Custom)
                                    {
                                        cstyle.BorderStyle = DashStyle.Solid;
                                        cstyle.BorderWidth = 0;
                                        break;
                                    }
                                }
                                else if (TryParseColor(item, out c))
                                {
                                    cstyle.BorderColor = c;
                                }
                                else if (TryParseBorderWidth(item, ref w))
                                {
                                    cstyle.BorderWidth = w;
                                }
                            }//foreach
                        }
                    }
                    break;
                case "border-color":
                    {
                        Color c = Color.Empty;
                        if (TryParseColor(styleValue, out c))
                        {
                            cstyle.BorderColor = c;
                        }
                    }
                    break;
                case "border-width":
                    {
                        float w = 0;
                        if (TryParseBorderWidth(styleValue, ref w))
                        {
                            cstyle.BorderWidth = w;
                        }
                    }
                    break;
                case "border-style":
                    {
                        DashStyle bs = DashStyle.Solid;
                        if (TryParseBorderStyle(styleValue, out bs))
                        {
                            cstyle.BorderStyle = bs;
                            if (bs == DashStyle.Custom)
                            {
                                cstyle.BorderStyle = DashStyle.Solid;
                                cstyle.BorderWidth = 0;
                            }
                        }
                    }
                    break;
                case "color":
                    {
                        Color c = Color.Empty;
                        if (TryParseColor(styleValue, out c))
                        {
                            cstyle.Color = c;
                        }
                    }
                    break;
                case "font":
                    {
                        string[] items = SplitItems(styleValue);
                        if (items != null)
                        {
                            for (int iCount = 0; iCount < items.Length; iCount++)
                            {
                                string item = items[iCount];
                                float size = 0;
                                if (string.Equals(item, "italic", StringComparison.CurrentCultureIgnoreCase))
                                {
                                    cstyle.Italic = true;
                                }
                                else if (string.Equals(item, "oblique", StringComparison.CurrentCultureIgnoreCase))
                                {
                                    cstyle.Italic = true;
                                }
                                else if (string.Equals(item, "bold", StringComparison.CurrentCultureIgnoreCase))
                                {
                                    cstyle.Bold = true;
                                }
                                else if (string.Equals(item, "bolder", StringComparison.CurrentCultureIgnoreCase))
                                {
                                    cstyle.Bold = true;
                                }
                                else if (TryParseLength(item, out size))
                                {
                                    cstyle.FontSize = GraphicsUnitConvert.Convert(size, this.DomDocument.DocumentGraphicsUnit, GraphicsUnit.Point);
                                }
                                else
                                {
                                    string name2 = GetFontName(item);
                                    if (name2 != null)
                                    {
                                        cstyle.FontName = name2;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case "font-family":
                    {
                        string fn = GetFontName(styleValue);
                        if (fn != null)
                        {
                            cstyle.FontName = fn;
                        }
                    }
                    break;
                case "font-size":
                    {
                        float size = 0;
                        if (TryParseLength(styleValue, out size))
                        {
                            cstyle.FontSize = GraphicsUnitConvert.Convert(
                                size,
                                this.DomDocument.DocumentGraphicsUnit,
                                GraphicsUnit.Point);
                        }
                    }
                    break;
                case "font-style":
                    {
                        if (Contains(styleValue, "italic")
                            || Contains(styleValue, "oblique"))
                        {
                            cstyle.Italic = true;
                        }
                    }
                    break;
                case "font-weight":
                    if (Contains(styleValue, "bold"))
                    {
                        cstyle.Bold = true;
                    }
                    else if (Contains(styleValue, "700"))
                    {
                        cstyle.Bold = true;
                    }
                    break;
                case "line-height":
                    break;
                case "text-align":
                    if (Contains(styleValue, "left"))
                    {
                        cstyle.Align = DocumentContentAlignment.Left;
                    }
                    else if (Contains(styleValue, "right"))
                    {
                        cstyle.Align = DocumentContentAlignment.Right;
                    }
                    else if (Contains(styleValue, "center"))
                    {
                        cstyle.Align = DocumentContentAlignment.Center;
                    }
                    else if (Contains(styleValue, "justify"))
                    {
                        cstyle.Align = DocumentContentAlignment.Justify;
                    }
                    break;
                case "text-indent":
                    {
                        float v = 0;
                        if (TryParseLength(styleValue, out  v))
                        {
                            cstyle.FirstLineIndent = v;
                        }
                    }
                    break;
                case "padding":
                    {
                        // 读取内边距
                        float[] values = ParseLengths(styleValue);
                        if (values != null)
                        {
                            if (values.Length >= 4)
                            {
                                cstyle.PaddingTop = values[0];
                                cstyle.PaddingRight = values[1];
                                cstyle.PaddingBottom = values[2];
                                cstyle.PaddingLeft = values[3];
                            }
                            else if (values.Length == 1)
                            {
                                cstyle.PaddingLeft = values[0];
                                cstyle.PaddingTop = values[0];
                                cstyle.PaddingRight = values[0];
                                cstyle.PaddingBottom = values[0];
                            }
                            else if (values.Length == 2)
                            {
                                cstyle.PaddingTop = values[0];
                                cstyle.PaddingBottom = values[0];
                                cstyle.PaddingLeft = values[1];
                                cstyle.PaddingRight = values[1];
                            }
                            else if (values.Length == 3)
                            {
                                cstyle.PaddingTop = values[0];
                                cstyle.PaddingRight = values[1];
                                cstyle.PaddingLeft = values[1];
                                cstyle.PaddingBottom = values[2];
                            }
                        }
                    }
                    break;
                case "padding-left":
                    {
                        float v = 0;
                        if (TryParseLength(styleValue, out v))
                        {
                            cstyle.PaddingLeft = v;
                        }
                    }
                    break;
                case "padding-top":
                    {
                        float v = 0;
                        if (TryParseLength(styleValue, out v))
                        {
                            cstyle.PaddingTop = v;
                        }
                    }
                    break;
                case "padding-right":
                    {
                        float v = 0;
                        if (TryParseLength(styleValue, out v))
                        {
                            cstyle.PaddingRight = v;
                        }
                    }
                    break;
                case "padding-bottom":
                    {
                        float v = 0;
                        if (TryParseLength(styleValue, out v))
                        {
                            cstyle.PaddingBottom = v;
                        }
                    }
                    break;
            }//switch
        }

        private string Trim(string text)
        {
            if (text == null)
            {
                return null;
            }
            else
            {
                return text.Trim();
            }
        }

        private bool Contains(string text, string item)
        {
            if (text != null)
            {
                return text.IndexOf(item, StringComparison.CurrentCultureIgnoreCase) >= 0;
            }
            return false;
        }

        private string[] SplitItems(string text )
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }
            string[] items = text.Split(' ', '\r', '\n', '\t');
            for (int iCount = 0; iCount < items.Length; iCount++)
            {
                items[iCount] = items[iCount].Trim();
            }
            return items;
        }
        private string GetAttributeUrl(string text)
        {
            if (text != null)
            {
                int index = text.IndexOf("url(", StringComparison.CurrentCultureIgnoreCase);
                if (index >= 0)
                {
                    text = text.Substring(4);
                    index = text.IndexOf(")");
                    if (index >= 0)
                    {
                        text = text.Substring(0, index).Trim();
                    }
                    return text;
                }
            }
            return null;
        }

        private bool TryParseLength(string text, out float length)
        {
            length = 0;
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }
            text = text.Trim();
            if ("0123456789.-".IndexOf(text[0]) >= 0)
            {
                double v = GraphicsUnitConvert.ParseCSSLength(text, this.DomDocument.DocumentGraphicsUnit, double.NaN );
                if (double.IsNaN(v) == false)
                {
                    length = (float)v;
                    return true;
                }
            }
            return false;
        }

        private bool TryParseBorderWidth(string text, ref float width)
        {
            if ( string.IsNullOrEmpty( text ))
            {
                return false;
            }
            text = text.Trim().ToLower();
            if( text == "medium" || text == "thin" )
            {
                width = 1;
                return true;
            }
            if (text == "thick")
            {
                width = 2;
                return true;
            }
            float v = 0 ;
            if (TryParseLength(text, out v))
            {
                width = v;
                return true;
            }
            return false ;
        }

        private bool TryParseBorderStyle(string text, out DashStyle style)
        {
            if (text == null)
            {
                style = DashStyle.Custom;
                return false;
            }
            text = text.Trim();
            if (string.Equals(
                text,
                StringConstBorderStyle.dashed,
                StringComparison.CurrentCultureIgnoreCase))
            {
                style = DashStyle.Dash;
                return true;
            }
            if (string.Equals(
                text,
                StringConstBorderStyle.dotted,
                StringComparison.CurrentCultureIgnoreCase))
            {
                style = DashStyle.Dot;
                return true;
            }
            if (string.Equals(
               text,
                StringConstBorderStyle.solid,
                StringComparison.CurrentCultureIgnoreCase))
            {
                style = DashStyle.Solid;
                return true;
            }
            if (string.Equals(
                    text,
                    StringConstBorderStyle.none,
                    StringComparison.CurrentCultureIgnoreCase))
            {
                style = DashStyle.Custom;
                return true;
            }
            style = DashStyle.Custom;
            return false;
        }

        private static Dictionary<string, Color> htmlColors = null;

        private bool TryParseColor(string text, out Color color)
        {
            color = Color.Empty;
            if (htmlColors == null)
            {
                htmlColors = new Dictionary<string, Color>();
                foreach( string name in Enum.GetNames( typeof( KnownColor )))
                {
                    htmlColors[name.ToLower()] = Color.FromKnownColor((KnownColor)Enum.Parse(typeof(KnownColor), name));
                }
            }
            try
            {
                if (text != null)
                {
                    text = text.Trim();
                    if (text.StartsWith("#"))
                    {
                        color = ColorTranslator.FromHtml(text);
                        return true;
                    }
                    text = text.ToLower();
                    if (htmlColors.ContainsKey(text))
                    {
                        color = htmlColors[text];
                        return true;
                    }
                }
            }
            catch 
            {
            }
            return false;
        }

        private Color ToColor(string text, Color defaultValue)
        {
            Color result = defaultValue;
            if (TryParseColor(text, out result))
            {
                return result;
            }
            else
            {
                return defaultValue;
            }
        }

        private int ToInt32(string text)
        {
            return Int32.Parse(text);
        }

        private float ToSingle(string text)
        {
            return float.Parse(text);
        }

        private float[] ParseLengths(string text)
        {
            List<float> result = new List<float>();
            string[] items = SplitItems(text);
            if (items != null)
            {
                foreach (string item in items)
                {
                    double v = GraphicsUnitConvert.ParseCSSLength(item, this.DomDocument.DocumentGraphicsUnit, double.NaN);
                    if (double.IsNaN(v) == false)
                    {
                        result.Add((float)v);
                    }
                }
            }
            return result.ToArray();
        }

        private float ToLength(string text  )
        {
            return ( float ) DCSoft.Drawing.GraphicsUnitConvert.ParseCSSLength(text, _DomDocument.DocumentGraphicsUnit  , 0);
        }

        private static string[] _fontNames = null ;
        /// <summary>
        /// 解析出字体名称
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string GetFontName(string text)
        {
            if( _fontNames == null )
            {
                FontFamily[] fs = FontFamily.Families ;
                _fontNames = new string[ fs.Length ] ;
                for( int iCount = 0 ; iCount < fs.Length ;iCount ++ )
                {
                    _fontNames[ iCount ] = fs[ iCount ].Name ;
                }
            }
            if (text != null)
            {
                text = text.Trim();
                foreach( string name in _fontNames )
                {
                    if( string.Equals( name , text , StringComparison.CurrentCultureIgnoreCase ))
                    {
                        return name ;
                    }
                }
            }
            return null;
        }
    }
}
