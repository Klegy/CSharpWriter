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
using System.IO ;
using System.Xml ;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Web;
using System.Drawing.Imaging;

namespace DCSoft.Common
{
    /// <summary>
    /// HTML文档对象
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [Serializable()]
    public class HtmlDocumentWriter 
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public HtmlDocumentWriter()
        {
            _Writer = new XmlTextWriter(_StrWriter);
        }

        private StringWriter _StrWriter = new StringWriter();
        private XmlTextWriter _Writer = null;

        public virtual void WriteStartDocument()
        {
            _Writer.WriteStartDocument();
        }

        public virtual void WriteEndDocument()
        {
            _Writer.WriteEndDocument();
        }

        public virtual void WriteStartElement(string name)
        {
            _Writer.WriteStartElement(name);
        }

        public virtual void WriteEndElement()
        {
            _Writer.WriteEndElement();
        }

        public virtual void WriteFullEndElement()
        {
            _Writer.WriteFullEndElement();
        }

        public void WriteString(string text)
        {
            _Writer.WriteString( text );
        }


        private bool bolUseSpacingAsBlank = false;
        /// <summary>
        /// 使用空白区域代替空格字符
        /// </summary>
        public bool UseSpacingAsBlank
        {
            get
            {
                return bolUseSpacingAsBlank;
            }
            set
            {
                bolUseSpacingAsBlank = value;
            }
        }

        /// <summary>
        /// 网格单元宽度
        /// </summary>
        private float intGridWidth = 7.5f;
        /// <summary>
        /// 网格单元宽度
        /// </summary>
        public float GridWidth
        {
            get
            {
                return intGridWidth;
            }
            set
            {
                intGridWidth = value;
            }
        }

        /// <summary>
        /// 网格单元高度
        /// </summary>
        private float intGridHeight = 18;
        /// <summary>
        /// 网格单元高度
        /// </summary>
        public float GridHeight
        {
            get
            {
                return intGridHeight;
            }
            set
            {
                intGridHeight = value;
            }
        }


        ///// <summary>
        ///// 将采用指定单位的长度数值转换为HTML Point 单位的数值
        ///// </summary>
        ///// <param name="Value">旧数值</param>
        ///// <param name="unit">旧数值使用的单位</param>
        ///// <returns>新数值字符串</returns>
        //public static string ToPointValue(double Value, System.Drawing.GraphicsUnit unit)
        //{
        //    Value = GraphicsUnitConvert.Convert(Value, unit, System.Drawing.GraphicsUnit.Point);
        //    return Value.ToString("#.0") + "pt";
        //}


        /// <summary>
        /// 输出纯文本数据
        /// </summary>
        /// <param name="strText">文本内容</param>
        public void WriteText(string strText)
        {
            if (strText != null)
            {
                strText = System.Web.HttpUtility.HtmlEncode(strText);
                if (this.UseSpacingAsBlank)
                {
                    string[] items = SplitForBlank(strText);
                    foreach (string item in items)
                    {
                        if (item[0] == ' ')
                        {
                            _Writer.WriteStartElement("span");
                            int len = item.Length;
                            if (len > 4)
                            {
                                len = len - 2;
                            }
                            _Writer.WriteAttributeString("style", "width:" + Convert.ToString( this.GridWidth * len ));
                            _Writer.WriteFullEndElement();
                        }
                        else
                        {
                            string item2 = null;
                            if (this.WritingExcelHtml )
                            {
                                item2 = item.Replace("\r\n", "<br style='mso-data-placement:same-cell' />");
                            }
                            else
                            {
                                item2 = item.Replace("\r\n", "<br />");
                            }
                            _Writer.WriteString(item2);
                        }
                    }
                }
                else
                {
                    strText = strText.Replace(" ", "&nbsp;");
                    if (this.WritingExcelHtml)
                    {
                        strText = strText.Replace("\r\n", "<br style='mso-data-placement:same-cell'/>");
                    }
                    else
                    {
                        strText = strText.Replace("\r\n", "<br />");
                    }
                    if (strText.Length == 0)
                    {
                        _Writer.WriteString(" ");
                    }
                    else
                    {
                        _Writer.WriteRaw(strText);
                    }
                }
            }
            else
            {
                _Writer.WriteString(" ");
            }
        }

        private string[] SplitForBlank(string txt)
        {
            System.Collections.ArrayList list = new System.Collections.ArrayList();
            System.Text.StringBuilder myStr = new System.Text.StringBuilder();
            bool lastflag = false;
            foreach (char c in txt)
            {
                bool flag = (c == ' ');
                if (flag != lastflag)
                {
                    if (myStr.Length > 0)
                    {
                        list.Add(myStr.ToString());
                        myStr = new System.Text.StringBuilder();
                    }
                }
                lastflag = flag;
                myStr.Append(c);
            }//foreach
            if (myStr.Length > 0)
            {
                list.Add(myStr.ToString());
            }
            return (string[])list.ToArray(typeof(string));
        }

        public void WriteElementString(string name, string text)
        {
            _Writer.WriteElementString(name, text);
        }

        public void WriteRaw(string text)
        {
            _Writer.WriteRaw(text);
        }

        public void WriteAttributeString(string name, string Value)
        {
            if (name == null || name.Trim().Length == 0 )
            {
                throw new ArgumentNullException("name");
            }
            _Writer.WriteAttributeString(name.Trim(), Value);
        }

        /// <summary>
        /// 输出空格
        /// </summary>
        /// <param name="Length">空格个数</param>
        public virtual void WriteBlank(int Length)
        {
            if (Length > 0)
            {
                if (this.Indent)
                {
                    _Writer.WriteStartElement("span");
                }
                for (int iCount = 0; iCount < Length; iCount++)
                {
                    _Writer.WriteRaw("&nbsp;");
                }
                if (this.Indent)
                {
                    _Writer.WriteEndElement();
                }
            }
        }

        private string strTitle = "";
        /// <summary>
        /// 文档标题
        /// </summary>
        public string Title
        {
            get
            {
                return strTitle; 
            }
            set
            {
                strTitle = value; 
            }
        }

        private bool _Indent = false;
        /// <summary>
        /// 启用HTML代码缩进
        /// </summary>
        public virtual bool Indent
        {
            get
            {
                return _Indent; 
            }
            set
            {
                _Indent = value; 
            }
        }

        private bool _WritingExcelHtml = false;
        /// <summary>
        /// 是否添加EXCEL使用的HTML标记
        /// </summary>
        public bool WritingExcelHtml
        {
            get
            {
                return _WritingExcelHtml; 
            }
            set
            {
                _WritingExcelHtml = value; 
            }
        }
        /// <summary>
        /// 输出文本使用的编码格式
        /// </summary>
        [NonSerialized]
        private System.Text.Encoding myContentEncoding = System.Text.Encoding.Default ;
        /// <summary>
        /// 输出文本使用的编码格式
        /// </summary>
        public System.Text.Encoding ContentEncoding
        {
            get
            {
                if (myContentEncoding == null)
                {
                    myContentEncoding = System.Text.Encoding.Default;
                }
                return myContentEncoding; 
            }
            set
            {
                myContentEncoding = value; 
            }
        }

        private XWebBrowsersStyle _BrowserStyle = XWebBrowsersStyle.InternetExplorer;
        /// <summary>
        /// 目标浏览器样式
        /// </summary>
        public XWebBrowsersStyle BrowserStyle
        {
            get
            {
                return _BrowserStyle; 
            }
            set
            {
                _BrowserStyle = value; 
            }
        }

        /// <summary>
        /// 启动的HTML对象尺寸模型
        /// </summary>
        public XHtmlBoxModelStyle BoxModel
        {
            get
            {
                if (this.BrowserStyle == XWebBrowsersStyle.InternetExplorer &&
                    this.DocumentSchema == XHtmlDocumentSchema.None)
                {
                    return XHtmlBoxModelStyle.InternetExplorer;
                }
                else
                {
                    return XHtmlBoxModelStyle.Standard;
                }
            }
        }

        private XHtmlDocumentSchema _DocumentSchema = XHtmlDocumentSchema.None;

        /// <summary>
        /// 文档验证规则
        /// </summary>
        public XHtmlDocumentSchema DocumentSchema
        {
            get
            {
                return _DocumentSchema;
            }
            set
            {
                _DocumentSchema = value;
                strDocumentSchemaString = null;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private string strDocumentSchemaString = null;

        /// <summary>
        /// W3C标准声明部分
        /// </summary>
        public string DocumentSchemaString
        {
            get
            {
                if (this.strDocumentSchemaString == null)
                {
                    switch (this.DocumentSchema)
                    {
                        case XHtmlDocumentSchema.HTML4_0_Strict:
                            this.strDocumentSchemaString = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01//EN\" \"http://www.w3.org/TR/html4/strict.dtd\">" + Environment.NewLine + "<html>";
                            break;
                        case XHtmlDocumentSchema.HTML4_0_Transitional:
                            this.strDocumentSchemaString =
                                "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\" \"http://www.w3.org/TR/html4/loose.dtd\">" + Environment.NewLine + "<html>";
                            break;
                        case XHtmlDocumentSchema.XHTML1_0_Strict:
                            this.strDocumentSchemaString = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">" + Environment.NewLine + "<html xmlns=\"http://www.w3.org/1999/xhtml\">";
                            break;
                        case XHtmlDocumentSchema.XHTML1_0_Transitional:
                            this.strDocumentSchemaString = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">" + Environment.NewLine + "<html xmlns=\"http://www.w3.org/1999/xhtml\">";
                            break;
                        case XHtmlDocumentSchema.XHTML1_1:
                            this.strDocumentSchemaString = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.1//EN\" \"http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd\">" + Environment.NewLine + "<html xmlns=\"http://www.w3.org/1999/xhtml\">";
                            break;
                        case XHtmlDocumentSchema.None:
                            this.strDocumentSchemaString = "<html>";
                            break;
                        default:
                            this.strDocumentSchemaString = "<html>";
                            break;
                    }
                }
                return this.strDocumentSchemaString;
            }
        }

        public string ContentHtml
        {
            get
            {
                this._Writer.Flush();
                string html = this._StrWriter.ToString();
                html = RemoveXMLDeclear(html);
                return html;
            }
        }

        /// <summary>
        /// 重新设置对象,开始输出新的HTML文档
        /// </summary>
        public void Reset()
        {
            this._StrWriter = new StringWriter();
            this._Writer = new XmlTextWriter(this._StrWriter);
            if (this.Indent)
            {
                _Writer.Formatting = System.Xml.Formatting.Indented;
                _Writer.Indentation = 3;
                _Writer.IndentChar = ' ';
            }
            else
            {
                _Writer.Formatting = System.Xml.Formatting.None;
            }
        }

        private Dictionary<string, string> _StyleValues = new Dictionary<string, string>();

        private string _StyleClassNameFormat = "xs_{0}";
        /// <summary>
        /// css样式名称格式化字符串
        /// </summary>
        public string StyleClassNameFormat
        {
            get
            {
                if (_StyleClassNameFormat == null || _StyleClassNameFormat.Length == 0 )
                {
                    return "xs_{0}";
                }
                else
                {
                    return _StyleClassNameFormat;
                }
            }
            set
            {
                _StyleClassNameFormat = value; 
            }
        }

        private bool _UseClassAttribute = false;
        /// <summary>
        /// 使用HTML元素的class属性，也就是说在一个style标签下集中输出所有的CSS样式字符串，然后HTML内容标签中使用class属性来引用CSS样式。
        /// </summary>
        public virtual bool UseClassAttribute
        {
            get
            {
                return _UseClassAttribute; 
            }
            set 
            {
                _UseClassAttribute = value; 
            }
        }

        /// <summary>
        /// 当前样式值
        /// </summary>
        private Dictionary<string, string> _CurrentStyleValues = null;

        /// <summary>
        /// 内部使用的HTML样式列表
        /// </summary>
        public string DocumentStyleString
        {
            get
            {
                if (_StyleValues == null || _StyleValues.Count == 0)
                {
                    return "";
                }
                System.Text.StringBuilder myStr = new System.Text.StringBuilder();
                foreach (string strName in _StyleValues.Keys )
                {
                    myStr.Append(System.Environment.NewLine);
                    myStr.Append("   ." + strName + "{" + _StyleValues[strName] + "}");
                }
                myStr.Append(System.Environment.NewLine);
                return myStr.ToString();
            }
        }


        public void WriteStartStyle()
        {
            _CurrentStyleValues = new Dictionary<string, string>();
        }

        public void WriteStyleItem(string name, string Value)
        {
            if (_CurrentStyleValues == null)
            {
                throw new InvalidOperationException("Can not set style");
            }
            else
            {
                _CurrentStyleValues[name] = Value;
            }
        }

        [NonSerialized]
        private Font _DefaultFont = null;
        /// <summary>
        /// 默认字体
        /// </summary>
        public Font DefaultFont
        {
            get
            {
                if (_DefaultFont == null)
                {
                    _DefaultFont = System.Windows.Forms.Control.DefaultFont;
                }
                return _DefaultFont; 
            }
            set
            {
                _DefaultFont = value; 
            }
        }

        /// <summary>
        /// 输出字体样式字符串
        /// </summary>
        /// <param name="font">字体对象</param>
        public void WriteFontStyle(Font font)
        {
            if (font == null)
            {
                throw new ArgumentNullException("font");
            }
            if (font.Name != this.DefaultFont.Name)
            {
                WriteStyleItem("font-family", font.Name);
            }
            if (font.Size != this.DefaultFont.Size)
            {
                WriteStyleItem("font-size", font.Size.ToString() + " pt");
            }
            if (font.Italic != this.DefaultFont.Italic && font.Italic)
            {
                WriteStyleItem("font-style", "italic");
            }
            if (font.Bold != this.DefaultFont.Italic && font.Bold)
            {
                WriteStyleItem("font-weight", "bold");
            }
            if (font.Underline != this.DefaultFont.Underline && font.Underline)
            {
                WriteStyleItem("text-decoration", "underline");
            }
            else if (font.Strikeout != this.DefaultFont.Strikeout && font.Strikeout)
            {
                WriteStyleItem("text-decoration", "line-through");
            }
        }

        /// <summary>
        /// 输出内边距样式
        /// </summary>
        /// <param name="leftPadding">左边距</param>
        /// <param name="topPadding">顶边距</param>
        /// <param name="rightPadding">右边距</param>
        /// <param name="bottomPadding">底边距</param>
        public void WritePaddingStyle(
            int leftPadding,
            int topPadding,
            int rightPadding,
            int bottomPadding)
        {
            if (leftPadding != 0)
            {
                WriteStyleItem("padding-left", leftPadding.ToString());
            }
            if (topPadding != 0)
            {
                WriteStyleItem("padding-top", topPadding.ToString());
            }
            if (rightPadding != 0)
            {
                WriteStyleItem("padding-right", rightPadding.ToString());
            }
            if (bottomPadding != 0)
            {
                WriteStyleItem("padding-bottom", bottomPadding.ToString());
            }
        }

        /// <summary>
        /// 输出外边距样式
        /// </summary>
        /// <param name="leftMargin">左边距</param>
        /// <param name="topMargin">顶边距</param>
        /// <param name="rightMargin">右边距</param>
        /// <param name="bottomMargin">底边距</param>
        public void WriteMarginStyle(
            int leftMargin,
            int topMargin,
            int rightMargin,
            int bottomMargin)
        {
            if (leftMargin != 0)
            {
                WriteStyleItem("margin-left", leftMargin.ToString());
            }
            if (topMargin != 0)
            {
                WriteStyleItem("margin-top", topMargin.ToString());
            }
            if (rightMargin != 0)
            {
                WriteStyleItem("margin-right", rightMargin.ToString());
            }
            if (bottomMargin != 0)
            {
                WriteStyleItem("margin-bottom", bottomMargin.ToString());
            }
        }

        /// <summary>
        /// 输出边框线样式
        /// </summary>
        /// <param name="leftBorder">是否显示左边框线</param>
        /// <param name="topBorder">是否显示上边框线</param>
        /// <param name="rightBorder">是否显示右边框线</param>
        /// <param name="bottomBorder">是否显示下边框线</param>
        /// <param name="borderColor">边框线颜色</param>
        /// <param name="borderWidth">边框线粗细</param>
        /// <param name="borderStyle">边框线样式</param>
        public void WriteBorderStyle(
            bool leftBorder,
            bool topBorder,
            bool rightBorder,
            bool bottomBorder,
            Color borderColor,
            int borderWidth,
            DashStyle borderStyle)
        {
            if (leftBorder == false
                && topBorder == false
                && rightBorder == false
                && bottomBorder == false)
            {
                // 无边框线
                return;
            }
            if (borderWidth <= 0)
            {
                // 无边框线
                return;
            }
            if (borderColor.A == 0)
            {
                // 无边框线
                return;
            }
            string borderStyleString = "solid";
            switch (borderStyle)
            {
                case DashStyle.Solid:
                    borderStyleString = "solid";
                    break;
                case DashStyle.Dash:
                    borderStyleString = "dashed";
                    break;
                case DashStyle.DashDot:
                    borderStyleString = "dotted";
                    break;
                case DashStyle.DashDotDot:
                    borderStyleString = "dotted";
                    break;
                case DashStyle.Dot:
                    borderStyleString = "dotted";
                    break;
            }
            borderStyleString = borderWidth + " " + borderStyleString + " " + ColorToString(borderColor);
            if (leftBorder && topBorder && rightBorder && bottomBorder)
            {
                // 显示完整的边框线
                WriteStyleItem("border", borderStyleString);
            }
            else
            {
                // 显示各自的边框线
                if (leftBorder)
                {
                    WriteStyleItem("border-left", borderStyleString);
                }
                if (topBorder)
                {
                    WriteStyleItem("border-top", borderStyleString);
                }
                if (rightBorder)
                {
                    WriteStyleItem("border-right", borderStyleString);
                }
                if (bottomBorder)
                {
                    WriteStyleItem("border-bottom", borderStyleString);
                }
            }
        }

        /// <summary>
        /// 输出背景图片样式
        /// </summary>
        /// <param name="src"></param>
        public void WriteBackgroundImageStyle(string src)
        {
            WriteStyleItem("background-image", "url("
                + System.Web.HttpUtility.HtmlAttributeEncode( src) + ")");
        }

        /// <summary>
        /// 输出背景图片位置样式
        /// </summary>
        /// <param name="position">背景图片位置样式</param>
        public void WriteBackgroundPositionStyle(ContentAlignment position )
        {
            string strItem = null;
            switch ( position )
            {
                case ContentAlignment.TopLeft:
                    strItem = "top left";
                    break;
                case ContentAlignment.TopCenter:
                    strItem = "top center";
                    break;
                case ContentAlignment.TopRight:
                    strItem = "top right";
                    break;
                case ContentAlignment.MiddleLeft:
                    strItem = "center left";
                    break;
                case ContentAlignment.MiddleCenter:
                    strItem = "center center";
                    break;
                case ContentAlignment.MiddleRight:
                    strItem = "center right";
                    break;
                case ContentAlignment.BottomLeft:
                    strItem = "bottom left";
                    break;
                case ContentAlignment.BottomCenter:
                    strItem = "bottom center";
                    break;
                case ContentAlignment.BottomRight:
                    strItem = "bottom right";
                    break;
            }
            WriteStyleItem("background-position", strItem);
        }


        /// <summary>
        /// 输出绝对坐标位置信息
        /// </summary>
        /// <param name="Left">左端位置</param>
        /// <param name="Top">顶端位置</param>
        public void WriteAbsolutePositionStyle(int Left, int Top)
        {
            this.WriteStyleItem("position", "absolute");
            this.WriteStyleItem("left", Left.ToString());
            this.WriteStyleItem("top", Top.ToString());
        }

        /// <summary>
        /// 输出文本对齐方式信息
        /// </summary>
        /// <param name="align">对齐方式</param>
        public void StyleWriteAlign(System.Drawing.StringAlignment align)
        {
            if (align == System.Drawing.StringAlignment.Far)
            {
                WriteStyleItem("text-align", "right");
            }
            else if (align == System.Drawing.StringAlignment.Center)
            {
                WriteStyleItem("text-align", "center");
            }
            else
            {
                WriteStyleItem("text-align", "left");
            }
        }


        /// <summary>
        /// 设置鼠标光标样式
        /// </summary>
        /// <param name="Cursor">鼠标光标对象</param>
        public void WriteCursorStyle(System.Windows.Forms.Cursor Cursor)
        {
            WriteStyleItem("cursor", GetCursorName(Cursor));
        }

        /// <summary>
        /// 获得鼠标光标的名称
        /// </summary>
        /// <param name="cursor">鼠标光标对象</param>
        /// <returns>鼠标光标样式名称</returns>
        private string GetCursorName(System.Windows.Forms.Cursor cursor)
        {
            if (cursor.Equals(System.Windows.Forms.Cursors.Default))
                return "default";
            if (cursor.Equals(System.Windows.Forms.Cursors.Cross))
                return "crosshair";
            if (cursor.Equals(System.Windows.Forms.Cursors.IBeam))
                return "text";
            if (cursor.Equals(System.Windows.Forms.Cursors.Arrow))
                return "point";
            if (cursor.Equals(System.Windows.Forms.Cursors.WaitCursor))
                return "wait";
            if (cursor.Equals(System.Windows.Forms.Cursors.AppStarting))
                return "progress";
            if (cursor.Equals(System.Windows.Forms.Cursors.Hand))
                return "hand";
            if (cursor.Equals(System.Windows.Forms.Cursors.Help))
                return "help";
            if (cursor.Equals(System.Windows.Forms.Cursors.SizeAll))
                return "move";
            if (cursor.Equals(System.Windows.Forms.Cursors.SizeNS))
                return "n-resize";
            if (cursor.Equals(System.Windows.Forms.Cursors.SizeWE))
                return "e-resize";
            if (cursor.Equals(System.Windows.Forms.Cursors.SizeNESW))
                return "ne-resize";
            if (cursor.Equals(System.Windows.Forms.Cursors.SizeNWSE))
                return "se-resize";
            return "default";
        }

        public string ColorToString(Color color)
        {
            return ColorTranslator.ToHtml(color);
        }

        /// <summary>
        /// 结束输出STYLE样式
        /// </summary>
        public void WriteEndStyle()
        {
            if (_CurrentStyleValues == null)
            {
                throw new InvalidOperationException("StyleString is null");
            }
            else
            {
                StringBuilder str = new StringBuilder();
                foreach (string name in _CurrentStyleValues.Keys)
                {
                    if (str.Length > 0)
                    {
                        str.Append(";");
                    }
                    str.Append(name + ":" + HttpUtility.HtmlAttributeEncode(_CurrentStyleValues[name]));
                }
                string txt = str.ToString();
                _CurrentStyleValues = null;

                if (this.UseClassAttribute)
                {
                    string name = null;
                    foreach (string key in this._StyleValues.Keys)
                    {
                        if (this._StyleValues[key] == txt)
                        {
                            name = key;
                            break;
                        }
                    }//foreach
                    if (name == null)
                    {
                        name = string.Format(this.StyleClassNameFormat, _StyleValues.Count);
                        _StyleValues[name] = txt;
                    }
                    this.WriteAttributeString("class", name);
                }
                else
                {
                    this.WriteAttributeString("style", txt);   
                }
            }
        }

        public HtmlAttachFile AddImage(System.Drawing.Image img, string fileName)
        {
            if (img == null)
            {
                throw new ArgumentNullException("img");
            }
            ImageFormat format = null;
            
            foreach (HtmlAttachFile item in this.AttachFiles)
            {
                if (item.Value == img 
                    && item.Name == fileName )
                {
                    return item;
                }
            }//foreach

            HtmlAttachFile file = new HtmlAttachFile();
            file.Value = img;
            format = ImageHelper.GetFormatByFileName(fileName);
            file.ContentType = ImageHelper.GetContentType(fileName);
            if (format != null)
            {
                // 用户指定文件格式
                MemoryStream ms = new MemoryStream();
                img.Save(ms, format);
                ms.Close();
                file = new HtmlAttachFile();
                if( string.IsNullOrEmpty( fileName ))
                {
                    // 创建文件名
                    fileName = "image" + this.AttachFiles.Count + "." + ImageHelper.GetFileExtersion( format );
                }
                file.Name = fileName;
                file.Content = ms.ToArray();
            }
            else
            {
                // 用户没有指定文件格式
                format = ImageFormat.Png;
                MemoryStream ms = new MemoryStream();
                img.Save(ms, ImageFormat.Png );
                ms.Close();
                byte[] bs1 = ms.ToArray();
                ms = new MemoryStream();
                img.Save(ms, ImageFormat.Jpeg);
                ms.Close();
                byte[] bs2 = ms.ToArray();
                if (bs1.Length > bs2.Length)
                {
                    file.Content = bs2;
                    file.ContentType = "image/jpeg";
                    if (string.IsNullOrEmpty(fileName))
                    {
                        fileName = "image" + this.AttachFiles.Count + ".jpg";
                    }
                    file.Name = fileName;
                }
                else
                {
                    file.Content = bs1;
                    file.ContentType = "image/png";
                    if (string.IsNullOrEmpty(fileName))
                    {
                        fileName = "image" + this.AttachFiles.Count + ".png";
                    }
                    file.Name = fileName;
                }
            }
            this.AttachFiles.Add(file);
            return file;
        }

        private HtmlAttachFileList _AttachFiles = new HtmlAttachFileList();
        /// <summary>
        /// 附加的文件
        /// </summary>
        public HtmlAttachFileList AttachFiles
        {
            get
            {
                return _AttachFiles; 
            }
            set
            {
                _AttachFiles = value; 
            }
        }

        /// <summary>
        /// 完整的HTML文档HTML代码
        /// </summary>
        public string DocumentHtml
        {
            get
            {
                System.IO.StringWriter myStr = new System.IO.StringWriter();
                System.Xml.XmlTextWriter w = new System.Xml.XmlTextWriter(myStr);
                if (this.Indent)
                {
                    w.Formatting = System.Xml.Formatting.Indented;
                    w.IndentChar = ' ';
                    w.Indentation = 3;
                }
                else
                {
                    w.Formatting = System.Xml.Formatting.None;
                }
                w.WriteStartDocument();
                w.WriteStartElement("html");
                if (this.WritingExcelHtml)
                {
                    w.WriteAttributeString("xmlns:o", "urn:schemas-microsoft-com:office:office");
                    w.WriteAttributeString("xmlns:x", "urn:schemas-microsoft-com:office:excel");
                    w.WriteAttributeString("xmlns:v", "urn:schemas-microsoft-com:vml");
                }
                w.WriteStartElement("head");

                string title = this.Title;
                if (title != null && title.Length > 0)
                {
                    w.WriteElementString("title", title);
                }
                w.WriteStartElement("meta");
                w.WriteAttributeString("http-equiv", "Content-Type");
                w.WriteAttributeString("content", "text/html;charset=" + this.ContentEncoding.BodyName);
                w.WriteEndElement();

                if (this.WritingExcelHtml)
                {
                    GetExcelXML(title).WriteContentTo(w);
                }
                //			w.WriteElementString("style" , @"
                //table{font-size:9pt}
                //");
                w.WriteEndElement();
                string styleHtml = this.DocumentStyleString;
                if (string.IsNullOrEmpty(styleHtml) == false)
                {
                    w.WriteStartElement("style");
                    w.WriteRaw(Environment.NewLine);
                    w.WriteString(styleHtml);
                    w.WriteRaw(Environment.NewLine);
                    w.WriteEndElement();
                }
                w.WriteStartElement("body");
                w.WriteAttributeString("style", "font-size:" + this.DefaultFont.Size + "pt");
                w.WriteRaw(System.Environment.NewLine);
                w.WriteRaw(this.ContentHtml);
                w.WriteEndElement();
                w.WriteEndElement();
                w.WriteEndDocument();
                w.Close();

                return RemoveXMLDeclear(myStr.ToString());
            }
        }

        private const string _MimeBoundaryTag = "----=_NextPart_000_00"; //
            

        /// <summary>
        /// 以MHT格式保存文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        public void SaveMHT(string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName, false, this.ContentEncoding))
            {
                // 输出文件头
                writer.WriteLine("From: DCSoft.HtmlDocument");
                writer.WriteLine("Subject:" + this.Title);
                writer.WriteLine("Data:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                writer.WriteLine("MIME-Version: 1.0");
                writer.WriteLine("Content-Type: multipart/related;");
                writer.WriteLine(Convert.ToChar(9) + "type=\"text/html\";");
                writer.WriteLine(Convert.ToChar(9) + "boundary=\"" + _MimeBoundaryTag + "\"");
                writer.WriteLine("X-MimeOLE: Produced By Sinosoft SkyReports Engine");
                writer.WriteLine();
                writer.WriteLine("This is a multi-part message in MIME format.");

                // 输出文件尾

            }
        }

        public void Save(TextWriter writer)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            writer.Write(this.DocumentHtml);
        }

        public void Save(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            byte[] bs = this.ContentEncoding.GetBytes(this.DocumentHtml);
            stream.Write(bs, 0, bs.Length);
        }

        public void Save(string fileName, string attachFilePath)
        {
            Dictionary<HtmlAttachFile, string> runtimeFileNames = new Dictionary<HtmlAttachFile, string>();
            string html = this.DocumentHtml;
            // 附件文件目录和HTML文件的相对路径
            string relationPath = null;
            if ( string.IsNullOrEmpty( attachFilePath ))
            {
                relationPath = "";
            }
            else if (Path.IsPathRooted(attachFilePath))
            {
                relationPath = null;
                string filePath = Path.GetDirectoryName(fileName);
                if (attachFilePath.StartsWith(filePath, StringComparison.InvariantCulture))
                {
                    relationPath = attachFilePath.Substring(filePath.Length);
                    if( relationPath.StartsWith("\\"))
                    {
                        relationPath = relationPath.Substring(1);
                    }
                }
            }
            else
            {
                relationPath = attachFilePath;
            }

            string basePath = System.IO.Path.GetDirectoryName(fileName);
            if (string.IsNullOrEmpty(attachFilePath) == false)
            {
                if (this.AttachFiles != null && this.AttachFiles.Count > 0)
                {
                    // 输出附属文件
                    foreach (HtmlAttachFile file in this.AttachFiles)
                    {
                        string runtimeFileName = file.Name;
                        if (Path.IsPathRooted(runtimeFileName) == false)
                        {
                            runtimeFileName = Path.Combine(attachFilePath, runtimeFileName);
                        }
                        runtimeFileNames[file] = runtimeFileName;
                        if (relationPath != null)
                        {
                            string relFileName = Path.Combine(relationPath, file.Name);
                            relFileName = relFileName.Replace('\\', '/');
                            html = html.Replace(file.ReferenceCode, relFileName);
                        }
                        else
                        {
                            html = html.Replace(file.ReferenceCode, runtimeFileName);
                        }
                        if (file.Content != null && file.Content.Length > 0)
                        {
                            using (FileStream stream = new FileStream(
                                runtimeFileName,
                                FileMode.Create,
                                FileAccess.Write))
                            {
                                stream.Write(file.Content, 0, file.Content.Length);
                            }
                        }
                    }//foreach
                }
            }//if
            // 输出HTML文件
            using (StreamWriter writer = new StreamWriter(fileName, false, this.ContentEncoding))
            {
                writer.Write(html);
            }
        }

        #region 内部的私有成员 **************************************************

        private static System.Xml.XmlDocument ExcelXMLDoc = null;
        private static System.Xml.XmlDocument GetExcelXML(string strTitle)
        {
            if (ExcelXMLDoc == null)
            {
                ExcelXMLDoc = new System.Xml.XmlDocument();
                ExcelXMLDoc.LoadXml(@"<xml xmlns:o='urn:schemas-microsoft-com:office:office'
xmlns:x='urn:schemas-microsoft-com:office:excel'>
						<o:DocumentProperties>
							<o:Author>XReport</o:Author>
							<o:LastAuthor>ys2006</o:LastAuthor>
							<o:Created></o:Created>
							<o:LastSaved></o:LastSaved>
							<o:Company>DCSoft</o:Company>
							<o:Version>9.2812</o:Version>
						</o:DocumentProperties>
						<x:ExcelWorkbook>
							<x:ExcelWorksheets>
								<x:ExcelWorksheet>
									<x:Name id='nameelement' ></x:Name>
									<x:WorksheetOptions>
										<x:DefaultRowHeight>285</x:DefaultRowHeight>
										<x:Print>
											<x:ValidPrinterInfo />
											<x:PaperSizeIndex>9</x:PaperSizeIndex>
											<x:HorizontalResolution>100</x:HorizontalResolution>
											<x:VerticalResolution>100</x:VerticalResolution>
										</x:Print>
										<x:Selected />
										<x:ProtectContents>False</x:ProtectContents>
										<x:ProtectObjects>False</x:ProtectObjects>
										<x:ProtectScenarios>False</x:ProtectScenarios>
									</x:WorksheetOptions>
								</x:ExcelWorksheet>
							</x:ExcelWorksheets>
							<x:WindowHeight>13275</x:WindowHeight>
							<x:WindowWidth>18180</x:WindowWidth>
							<x:WindowTopX>480</x:WindowTopX>
							<x:WindowTopY>15</x:WindowTopY>
							<x:ProtectStructure>False</x:ProtectStructure>
							<x:ProtectWindows>False</x:ProtectWindows>
						</x:ExcelWorkbook>
					</xml>");
            }
            ExcelXMLDoc.SelectSingleNode("//*[@id='nameelement']").InnerText = strTitle;
            return ExcelXMLDoc;
        }

        /// <summary>
        /// 删除XML字符串的	XML声明头
        /// </summary>
        /// <param name="strXML">XML字符串</param>
        /// <returns>去掉XML声明头的XML字符串</returns>
        private string RemoveXMLDeclear(string strXML)
        {
            if (strXML != null)
            {
                int Index = strXML.IndexOf("?>");
                if (Index > 0)
                    Index += 2;
                else
                    Index = 0;
                for (int iCount = Index; iCount < strXML.Length; iCount++)
                {
                    if (!char.IsWhiteSpace(strXML, iCount))
                    {
                        return strXML.Substring(iCount);
                    }
                }
                return strXML;
            }
            return null;
        }

        #endregion

    }

    /// <summary>
    /// HTML附件文件列表
    /// </summary>
    [Serializable()]
    public class HtmlAttachFileList : List<HtmlAttachFile>
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public HtmlAttachFileList()
        {
        }
        /// <summary>
        /// 获得指定名称的文件对象
        /// </summary>
        /// <param name="name">文件名</param>
        /// <returns>文件对象</returns>
        public HtmlAttachFile this[string name]
        {
            get
            {
                foreach (HtmlAttachFile file in this)
                {
                    if (file.Name == name)
                    {
                        return file;
                    }
                }
                return null;
            }
        }
    }

    /// <summary>
    /// HTML文档附件文件
    /// </summary>
    [Serializable()]
    public class HtmlAttachFile
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public HtmlAttachFile()
        {
        }

        private string _ReferenceCode = Guid.NewGuid().ToString("N");
        /// <summary>
        /// 内部的引用名称
        /// </summary>
        public string ReferenceCode
        {
            get
            {
                return _ReferenceCode; 
            }
            set
            {
                _ReferenceCode = value; 
            }
        }

        private string _Name = null;
        /// <summary>
        /// 文件名
        /// </summary>
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

        private string _ContentType = null;
        /// <summary>
        /// 数据内容样式
        /// </summary>
        public string ContentType
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

        private byte[] _Content = null;
        /// <summary>
        /// 文件内容
        /// </summary>
        [System.Xml.Serialization.XmlElement(DataType="base64Binary")]
        public byte[] Content
        {
            get
            {
                return _Content; 
            }
            set
            {
                _Content = value; 
            }
        }

        [NonSerialized]
        private object _Value = null;
        /// <summary>
        /// 相关的对象数据
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        [System.Xml.Serialization.XmlIgnore()]
        public object Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
    }


    /// <summary>
    /// 使用的HTML对象尺寸模型样式
    /// </summary>
    public enum XHtmlBoxModelStyle
    {
        /// <summary>
        /// 微软InternetExplore样式的对象尺寸模型,此时对象的width和height属性包括内边距.
        /// </summary>
        InternetExplorer,
        /// <summary>
        /// 使用W3C指定的标准对象尺寸模型,此时对象的width和height属性为客户区尺寸.
        /// </summary>
        Standard
    }


    /// <summary>
    /// 文档验证规则(DTD)
    /// </summary>
    public enum XHtmlDocumentSchema
    {
        /// <summary>
        /// 没有文档类型
        /// </summary>
        None,
        /// <summary>
        /// HTML 4.01 Transitional
        /// </summary>
        HTML4_0_Transitional,
        /// <summary>
        /// HTML 4.01 Strict
        /// </summary>
        HTML4_0_Strict,
        /// <summary>
        /// XHTML 1.0 Transitional
        /// </summary>
        XHTML1_0_Transitional,
        /// <summary>
        /// XHTML 1.0 Strict
        /// </summary>
        XHTML1_0_Strict,
        /// <summary>
        /// XHTML 1.1
        /// </summary>
        XHTML1_1,
    }

    /// <summary>
    /// 
    /// </summary>
    public enum XWebBrowsersStyle
    {
        /// <summary>
        /// 自动检测
        /// </summary>
        AutoDetect,
        /// <summary>
        /// 标准浏览器
        /// </summary>
        Standard,
        /// <summary>
        /// FirFox2.0及其以上版本
        /// </summary>
        FireFox,
        /// <summary>
        /// 苹果MAC的Safari浏览器
        /// </summary>
        AppleMAC_Safari,
        /// <summary>
        /// 谷歌Chrome浏览器
        /// </summary>
        Chrome,
        /// <summary>
        /// 微软IE浏览器
        /// </summary>
        InternetExplorer,
        /// <summary>
        /// 微软IE7浏览器
        /// </summary>
        InternetExplorer7,
        /// <summary>
        /// 微软IE8浏览器
        /// </summary>
        InternetExplorer8,
        /// <summary>
        /// Netscape Navigator浏览器
        /// </summary>
        NetscapeNavigator
    }
    
}
