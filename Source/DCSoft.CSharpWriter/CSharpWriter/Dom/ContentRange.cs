/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using DCSoft.CSharpWriter.Dom.Undo ;
using DCSoft.Drawing;
using System.Drawing;
using DCSoft.Common;
using System.Collections;
using System.Collections.Generic;
using DCSoft.CSharpWriter.RTF;

namespace DCSoft.CSharpWriter.Dom
{
    /// <summary>
	/// 文档区域对象
	/// </summary>
    /// <remarks>编写 袁永福</remarks>
    [System.Diagnostics.DebuggerDisplay("Index={StartIndex},Length={Length}")]
    [System.Diagnostics.DebuggerTypeProxy(typeof(DCSoft.Common.ListDebugView))]
    public class ContentRange : System.Collections.IEnumerable
    {

        /// <summary>
        /// 判断两个区域的设置是否相等
        /// </summary>
        /// <param name="range1">区域1</param>
        /// <param name="range2">区域2</param>
        /// <returns>两者是否等价</returns>
        public static bool Compare(ContentRange range1, ContentRange range2)
        {
            if (range1 == range2)
            {
                return true;
            }
            else
            {
                if (range1 == null || range2 == null )
                {
                    // 此时两个对象必然一个为空另外一个不为空，因此不可能相等
                    return false;
                }
                if (range1._Document != range2._Document
                    || range1._Elements != range2._Elements
                    || range1._EndElement != range2._EndElement
                    || range1._Length != range2._Length
                    || range1._StartElement != range2._StartElement
                    || range1._StartIndex != range2._StartIndex
                    || range1._StateInvalidate != range2._StateInvalidate)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        public ContentRange()
        {
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="document">文档对象</param>
        /// <param name="elements">元素对象列表</param>
        /// <param name="startIndex">区域的开始序号</param>
        /// <param name="length">区域的长度</param>
        public ContentRange(DomDocument document, DomElementList elements, int startIndex, int length)
        {
            _Document = document;
            _Elements = elements;
            _StartIndex = startIndex;
            _Length = length;
            _StateInvalidate = true;
        }

        public ContentRange(DomDocumentContentElement element, int startIndex, int length)
        {
            Refresh(element, startIndex, length);
        }

        public ContentRange(DomDocumentContentElement element, DomElement startElement , DomElement endElement )
        {
            Refresh(element, startElement , endElement );
        }

        public void Refresh(DomDocumentContentElement element, DomElement startElement, DomElement endElement)
        {
            _Document = element.OwnerDocument;
            _Elements = element.Content;
            _StartElement = startElement;
            _EndElement = endElement;
            _StateInvalidate = true;
            _ContentVersion++;
            if (_Document != null)
            {
                // 清空用户指定的样式
                //_Document._UserSpecifyStyle = null;
            }
        }

        public void Refresh(DomDocumentContentElement element, int startIndex, int length)
        {
            _Document = element.OwnerDocument;
            _Elements = element.Content;
            _StartIndex = startIndex;
            _Length = length;
            _StateInvalidate = true;
            _ContentVersion++;
            if (_Document != null)
            {
                // 清空用户指定的样式
                //_Document._UserSpecifyStyle = null;
            }
        }


        /// <summary>
        /// 设置状态无效
        /// </summary>
        public void Invalidate()
        {
            _StateInvalidate = true;
            _ContentVersion++;
        }

        private int _ContentVersion = 0;
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
                _ContentVersion++; 
            }
        }

        private DomElementList _Elements = null;
        /// <summary>
        /// 参与的元素列表
        /// </summary>
        public DomElementList Elements
        {
            get
            {
                return _Elements; 
            }
            set
            {
                _Elements = value;
                _StateInvalidate = true;
                _ContentVersion++; 
            }
        }

        private int _StartIndex = 0;
        /// <summary>
        /// 区域开始序号
        /// </summary>
        public int StartIndex
        {
            get
            {
                return _StartIndex; 
            }
            set
            {
                _StartIndex = value;
                _StateInvalidate = true;
                _ContentVersion++; 
            }
        }

        private int _Length = 0;
        /// <summary>
        /// 区域长度
        /// </summary>
        public int Length
        {
            get
            {
                return _Length; 
            }
            set
            {
                _Length = value;
                _StateInvalidate = true; 
                _ContentVersion++; 
            }
        }

        private DomElement _StartElement = null;
        /// <summary>
        /// 区域起始元素
        /// </summary>
        public DomElement StartElement
        {
            get
            {
                return _StartElement; 
            }
            set 
            {
                _StartElement = value;
                _StateInvalidate = true;
                _ContentVersion++;
            }
        }

        private DomElement _EndElement = null;
        /// <summary>
        /// 区域结束元素
        /// </summary>
        public DomElement EndElement
        {
            get
            {
                return _EndElement; 
            }
            set
            {
                _EndElement = value;
                _StateInvalidate = true;
                _ContentVersion++;
            }
        }

        private bool _StateInvalidate = true;

        private void CheckState()
        {
            if (_StateInvalidate)
            {
                CheckState(true);
            }
        }

        internal bool CheckState(bool throwException)
        {
            if (_Elements == null)
            {
                if (throwException)
                {
                    throw new ArgumentNullException("Elements");
                }
                else
                {
                    return false;
                }
            }
            if ((_StartElement == null) != (_EndElement == null))
            {
                if (throwException)
                {
                    throw new ArgumentException("StartElement vs EndElement");
                }
                else
                {
                    return false;
                }
            }
            if (_StartElement != null && _EndElement != null)
            {
                _StartIndex = Math.Min(_Elements.IndexOf(_StartElement), _Elements.IndexOf(_EndElement));
                if (_StartIndex < 0)
                {
                    if (throwException)
                    {
                        throw new IndexOutOfRangeException(_StartIndex.ToString());
                    }
                    else
                    {
                        return false;
                    }
                }
                _Length = Math.Abs(_Elements.IndexOf(_StartElement) - _Elements.IndexOf(_EndElement)) + 1;
            }
            else
            {
                if (_Length < 0)
                {
                    // 修正区域长度
                    _StartIndex = _StartIndex + _Length;
                    _Length = -_Length;
                }
                if (_StartIndex < 0 || _StartIndex >= _Elements.Count)
                {
                    if (throwException)
                    {
                        throw new ArgumentOutOfRangeException("StartIndex");
                    }
                    else
                    {
                        return false;
                    }
                }
                if (_Length < 0 || _StartIndex + _Length > _Elements.Count)
                {
                    if (throwException)
                    {
                        throw new ArgumentOutOfRangeException("Length");
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            _StateInvalidate = false;
            return true;
        }

        /// <summary>
        /// 获得区域中指定序号的元素
        /// </summary>
        /// <param name="index">序号</param>
        /// <returns>指定的元素</returns>
        public DomElement this[int index]
        {
            get
            {
                CheckState();
                return _Elements[_StartIndex + index];
            }
        }

        public DomElement SafeGet(int index)
        {
            CheckState();
            return _Elements.SafeGet(_StartIndex + index);
        }

        /// <summary>
        /// 区域中第一个元素
        /// </summary>
        public DomElement FirstElement
        {
            get
            {
                CheckState();
                return _Elements[_StartIndex];
            }
        }

        /// <summary>
        /// 区域中最后一个元素
        /// </summary>
        public DomElement LastElement
        {
            get
            {
                CheckState();
                return _Elements[_StartIndex + _Length];
            }
        }

        /// <summary>
        /// 获得区域包含的元素列表
        /// </summary>
        /// <returns>创建的元素列表</returns>
        public DomElementList GetElements()
        {
            CheckState();
            DomElementList result = new DomElementList();
            for (int iCount = 0; iCount < _Length; iCount++)
            {
                result.Add(this._Elements[_StartIndex + iCount]);
            }
            return result;
        }

        /// <summary>
        /// 判断指定的元素是否在区域中
        /// </summary>
        /// <param name="element">元素对象</param>
        /// <returns>是否在区域中</returns>
        public bool Contains(DomElement element)
        {
            CheckState();
            for (int iCount = 0; iCount < _Length; iCount++)
            {
                if (_Elements[iCount + _StartIndex] == element)
                {
                    return true;
                }
            }
            return false;
        }

        public bool Contains(int viewIndex)
        {
            CheckState();
            if (_Length == 0)
            {
                return false;// _StartIndex == viewIndex;
            }
            else
            {
                return viewIndex >= _StartIndex && viewIndex < _StartIndex + _Length;
            }
        }

        /// <summary>
        /// 获得纯文本内容
        /// </summary>
        public string Text
        {
            get
            {
                System.Text.StringBuilder myStr = new System.Text.StringBuilder();
                foreach (DomElement element in this)
                {
                    myStr.Append(element.ToPlaintString());
                }
                return myStr.ToString();
            }
        }

        /// <summary>
        /// 获得表示内容的RTF文本
        /// </summary>
        public string RTFText
        {
            get
            {
                CheckState();
                if( _Length == 0 )
                {
                    return "";
                }
                DomElementList list = this.Document.CreateParagraphs(this);
                if (list == null || list.Count == 0)
                {
                    return "";
                }
                System.IO.StringWriter writer = new System.IO.StringWriter();
                RTFContentWriter w = new RTFContentWriter();
                w.Open(writer);
                w.Document = this.Document;
                w.CollectionDocumentsStyle();
                w.WriteStartDocument();
                foreach (DomElement element in list)
                {
                    element.WriteRTF(w);
                }
                w.WriteEndDocument();
                w.Close();
                return writer.ToString();
            }
        }


        //private DocumentContentStyle _Style = null;
        /// <summary>
        /// 当前文档样式
        /// </summary>
        public DocumentContentStyle Style
        {
            get
            {
                CheckState();
                if ( this.Length > 0 )
                {
                    List<int> rsi = new List<int>();
                    foreach (DomElement element in this)
                    {
                        if (rsi.Contains(element.StyleIndex) == false)
                        {
                            rsi.Add(element.StyleIndex);
                        }
                    }
                    DocumentContentStyle result = new DocumentContentStyle();
                    foreach (int index in rsi)
                    {
                        DocumentContentStyle st = ( DocumentContentStyle) this.Document.ContentStyles.GetStyle(index);
                        XDependencyObject.MergeValues(st, result, true);
                    }
                    return result;
                }
                else
                {
                    DomElement element = this.SafeGet( this.StartIndex );
                    if (element == null)
                    {
                        return null;
                    }
                    else
                    {
                        return element.RuntimeStyle;
                    }
                }
            }
        }

        private DomDocumentContentElement Content
        {
            get
            {
                CheckState();
                DomElement element = this[this.StartIndex];
                return element.DocumentContentElement;
            }
        }

        /// <summary>
        /// 设置段落样式
        /// </summary>
        /// <param name="newStyle"></param>
        /// <returns></returns>
        public DomElementList SetParagraphStyle(DocumentContentStyle newStyle)
        {
            CheckState();
            Dictionary<DomElement, int> styleIndexs
                = new Dictionary<DomElement, int>();
            foreach (DomParagraphFlagElement p in this.ParagraphsEOFs)
            {
                DocumentContentStyle rs = (DocumentContentStyle)p.RuntimeStyle.Clone();
                if (XDependencyObject.MergeValues(newStyle, rs, true) > 0)
                {
                    int newStyleIndex = this.Document.ContentStyles.GetStyleIndex(rs);
                    if (newStyleIndex != p.StyleIndex)
                    {
                        styleIndexs[ p ] = newStyleIndex ;
                    }
                }
            }//foreach
            if (styleIndexs.Count > 0)
            {
                this.Document.EditorSetParagraphStyle(styleIndexs, true);
            }
            return this.ParagraphsEOFs;
        }

        /// <summary>
        /// 设置文档元素的样式
        /// </summary>
        /// <param name="newStyle">新样式</param>
        /// <returns>是否修改了文档内容</returns>
        public bool SetElementStyle(DocumentContentStyle newStyle)
        {
            Dictionary<DomElement, int> newStyleIndexs
                = new Dictionary<DomElement, int>();
            foreach (DomElement element in this)
            {
                DocumentContentStyle rs = (DocumentContentStyle)element.RuntimeStyle.Clone();
                if (XDependencyObject.MergeValues(newStyle, rs, true) > 0)
                {
                    int styleIndex = this.Document.ContentStyles.GetStyleIndex(rs);
                    if (styleIndex != element.StyleIndex)
                    {
                        newStyleIndexs[element] = styleIndex;
                    }
                }
            }//foreach
            if (newStyleIndexs.Count > 0)
            {
                this.Document.EditorSetElementStyle(newStyleIndexs, true);
                return true;
            }
            return false ;
        }

        /// <summary>
        /// 获得区间包含的段落对象列表
        /// </summary>
        public DomElementList ParagraphsEOFs
        {
            get
            {
                DomElementList list = new DomElementList();
                if ( this.Length > 0 )
                {
                    foreach (DomElement element in this)
                    {
                        if (element is DomParagraphFlagElement)
                        {
                            list.Add(element);
                        }
                    }
                    if (!(this.LastElement is DomParagraphFlagElement))
                    {
                        list.Add(this.LastElement.OwnerParagraphEOF);
                    }
                }
                else
                {
                    DomElement element = this.Elements.SafeGet(this.StartIndex);
                    list.Add(element.OwnerParagraphEOF);
                }
                return list;
            }
        }

        /// <summary>
        /// 获得元素的枚举器
        /// </summary>
        /// <returns>枚举器</returns>
        public System.Collections.IEnumerator GetEnumerator()
        {
            CheckState();
            return new RangeEnumerator(this);
        }

        private class RangeEnumerator : System.Collections.IEnumerator
        {
            public RangeEnumerator(ContentRange r)
            {
                range = r;
                position = range.StartIndex - 1 ;
                contentVersion = range._ContentVersion;
            }

            private int contentVersion = 0;
            private ContentRange range = null;

            private int position = -1;
             
            public object Current
            {
                get
                {
                    if (contentVersion != range._ContentVersion)
                    {
                        throw new InvalidOperationException(" 列表内容已被修改 ");
                    }
                    return range.Elements[position];
                }
            }

            public bool MoveNext()
            {
                if (contentVersion != range._ContentVersion)
                {
                    throw new InvalidOperationException(" 列表内容已被修改 ");
                }
                if (position < range.StartIndex + range.Length)
                {
                    position++;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public void Reset()
            {
                position = range.StartIndex;
                contentVersion = range._ContentVersion;
            }
        }

        /// <summary>
        /// 返回表示对象数据的字符串
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString()
        {
            return "Index=" + this.StartIndex + ",Length=" + this.Length;
        }
    }
}