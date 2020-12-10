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
    internal class WriterUtils
    {
       

        public static FileFormat ParseFileFormat(string format)
        {
            if (string.IsNullOrEmpty(format))
            {
                return FileFormat.XML;
            }
            try
            {
                FileFormat f =  (FileFormat ) Enum.Parse(typeof(FileFormat), format);
                return f;
            }
            catch
            {
                return FileFormat.XML;
            }
        }

        public static FileFormat GetFormat(string fileName)
        {
            string ext = System.IO.Path.GetExtension(fileName);
            if (string.Compare(ext, ".xml", true) == 0)
            {
                return FileFormat.XML ;
            }
            else if (string.Compare(ext, ".rtf", true) == 0)
            {
                return FileFormat.RTF ;
            }
            
            else if (string.Compare(ext, ".txt", true) == 0)
            {
                return FileFormat.Text ;
            }
            return FileFormat.XML;
        }

        public static string FormatByteSize(int size)
        {
            return DCSoft.Common.FileHelper.FormatByteSize(size);
        }

        /// <summary>
        /// 删除元素列表中自动创建的段落标记元素
        /// </summary>
        /// <param name="elements">文档元素列表</param>
        /// <returns>操作是否改变了列表内容</returns>
        public static bool RemoveAutoCreateParagraphFlag(DomElementList elements)
        {
            if (elements != null && elements.Count > 0)
            {
                if (elements.LastElement is DomParagraphFlagElement)
                {
                    DomParagraphFlagElement p = (DomParagraphFlagElement)elements.LastElement;
                    if (p.AutoCreate)
                    {
                        elements.RemoveAt(elements.Count - 1);
                        return true;
                    }
                }
            }
            return false;
        }
         
        public static string CombinUrl(string baseUrl, string url)
        {
            if (string.IsNullOrEmpty(baseUrl))
            {
                return url;
            }
            else
            {
                Uri uri = new Uri(new Uri(baseUrl), url);
                return uri.AbsoluteUri ;
            }
        }

        public static string GetBaseURL(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }

            string result = url;
            int index = url.LastIndexOfAny(new char[] { '\\', '/' });
            if (index > 0)
            {
                result = url.Substring(0, index);
            }
            return result;
        }
          

        /// <summary>
        /// 遍历子孙元素
        /// </summary>
        /// <param name="elements">要遍历的元素列表</param>
        /// <param name="handler">遍历过程的委托对象</param>
        public static void Enumerate(
            DomElementList elements,
            ElementEnumerateEventHandler handler )
        {
            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }
            ElementEnumerateEventArgs args = new ElementEnumerateEventArgs();
            InnerEnumerate(elements, handler, args, true );
        }
         
        private static void InnerEnumerate(
           DomElementList elements,
           ElementEnumerateEventHandler handler ,
           ElementEnumerateEventArgs args ,
            bool deeply )
        {
            foreach (DomElement element in elements)
            {
                args._Element = element;
                args.CancelChild = false;
                handler(null, args);
                if (args.Cancel)
                {
                    break;
                }
                if (args.CancelChild == false && deeply )
                {
                    if (element is DomContainerElement)
                    {
                        InnerEnumerate(((DomContainerElement)element).Elements, handler, args , deeply );
                        if (args.Cancel)
                        {
                            break;
                        }
                    }
                }
                args.CancelChild = false;
            }
        }



        /// <summary>
        /// 将一个多行字符串转化为若干个单行字符串组成的数组
        /// </summary>
        /// <param name="txt">原始字符串</param>
        /// <returns>转化所得的单行字符串数组</returns>
        public static string[] GetLines(string txt)
        {
            if (txt == null)
                return null;
            System.Collections.ArrayList list = new System.Collections.ArrayList();
            System.IO.StringReader reader = new System.IO.StringReader(txt);
            string strLine = reader.ReadLine();
            while (strLine != null)
            {
                list.Add(strLine);
                strLine = reader.ReadLine();
                if (strLine == null)
                {
                    break;
                }
            }//while
            //if( txt.EndsWith("\r") || txt.EndsWith("\n"))
            //    list.Add("");
            return (string[])list.ToArray(typeof(string));
        }

        private static int intTabStep = 0;

        /// <summary>
        /// 计算指定位置处的制表符的宽度
        /// </summary>
        /// <remarks>
        /// 本函数根据作为参数传入的制表符开始的位置,计算制表符的宽度,以保证制表符的右端位置在某个制表位上
        /// 制表位的位置恒定为标准制表符的宽度的整数倍(在此处标准制表符为4个下划线的宽度)
        ///    在下面的标尺上
        ///   0___1___2___3___4___5___6___7___8___9___10
        ///    制表符的位置随意,但制表符右端必须在某个数字下面
        ///    若制表符恰好在制表位上则返回标准制表符宽度
        /// 由于有这样的限制,因此制表符的宽度不固定,而根据其位置而改变,本函数就是计算其宽度
        /// </remarks>
        /// <param name="Pos">制表符开始的位置</param>
        /// <returns>制表符的宽度</returns>
        public static float GetTabWidth(float Pos, float TabStep)
        {
            float iWidth = TabStep * (int)System.Math.Ceiling((double)Pos / TabStep) - Pos;
            if (iWidth == 0)
                iWidth = TabStep;
            return iWidth;
        }


        /// <summary>
        /// 默认的字体，目前设置字体大小为12
        /// </summary>
        public static System.Drawing.Font DefaultFont = new System.Drawing.Font(
            System.Windows.Forms.Control.DefaultFont.Name,
            (float)12);
         

        /// <summary>
        /// 获得两个文档元素共同所在的最低层次的父文档元素
        /// </summary>
        /// <param name="element1">文档元素1</param>
        /// <param name="element2">文档元素2</param>
        /// <returns>共同的父元素</returns>
        public static DomContainerElement GetSameParent(DomElement element1, DomElement element2)
        {
            if (element1 == null)
            {
                throw new ArgumentNullException("element1");
            }
            if (element2 == null)
            {
                throw new ArgumentNullException("element2");
            }
            if (element1.Parent == element2.Parent)
            {
                // 大多数情况下，两个元素在同一个容器元素中，因此返回其父元素。
                return element1.Parent;
            }
            DomElement parent1 = element1;
            while (parent1 != null )
            {
                DomElement parent2 = element2;
                while (parent2 != null)
                {
                    if (parent1 == parent2)
                    {
                        return parent1 as DomContainerElement;
                    }
                    parent2 = parent2.Parent;
                }//while
                parent1 = parent1.Parent;
            }//while
            return null;
        }

        /// <summary>
        /// 获得指定元素的父节点对象列表，在该列表中，近亲在前，远亲在后。
        /// </summary>
        /// <remarks>
        /// 本方法和GetParentList的不同就是当元素是父元素的第一个文档内容元素则跳过这个父元素
        /// </remarks>
        /// <param name="element">文档元素对象</param>
        /// <returns>父节点对象列表</returns>
        public static DomElementList GetParentList2(DomElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            DomElementList result = new DomElementList();
            DomContainerElement parent = element.Parent;
            while (parent != null)
            {
                if (parent.FirstContentElement != element)
                {
                    result.Add(parent);
                }
                parent = parent.Parent;
            }
            return result;
        }

        /// <summary>
        /// 获得指定元素的父节点对象列表，在该列表中，近亲在前，远亲在后。
        /// </summary>
        /// <param name="element">文档元素对象</param>
        /// <returns>父节点对象列表</returns>
        public static DomElementList GetParentList(DomElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            DomElementList result = new DomElementList();
            DomContainerElement parent = element.Parent;
            while (parent != null)
            {
                result.Add(parent);
                parent = parent.Parent;
            }
            return result;
        }


        public static DomElementList MergeParagraphs(
            DomElementList sourceElements,
            bool includeSelectionOnly)
        {
            if (sourceElements == null || sourceElements.Count == 0)
            {
                return null;
                //throw new ArgumentNullException("sourceElements");
            }
            DomElementList list = MergeElements(sourceElements, includeSelectionOnly);
            DomElementList result = new DomElementList();
            DomDocument document = sourceElements[0].OwnerDocument;
            DomParagraphElement p = new DomParagraphElement();
            p.OwnerDocument = document;
            result.Add(p);
            foreach (DomElement element in list)
            {
                DomParagraphElement paragraph = (DomParagraphElement)result[result.Count - 1];
                if (element is DomParagraphFlagElement)
                {
                    paragraph.StyleIndex = element.StyleIndex;
                    paragraph.Elements.AddRaw(element);

                    paragraph = new DomParagraphElement();
                    paragraph.OwnerDocument = document;
                    result.Add(paragraph);
                }
                else
                {
                    paragraph.Elements.AddRaw(element);
                }
            }
            DomParagraphElement lastP = (DomParagraphElement)result[result.Count - 1];
            if (lastP.Elements.Count == 0)
            {
                // 删除最后一个没有任何内容的段落对象
                result.RemoveAt(result.Count - 1);
            }
            return result;
        }

        /// <summary>
        /// 对字符元素进行合并操作
        /// </summary>
        /// <param name="sourceList">元素列表</param>
        /// <returns>合并后的元素列表</returns>
        public static DomElementList MergeElements(
            DomElementList sourceList,
            bool includeSelectionOnly)
        {
            DomElementList result = new DomElementList();
            if (sourceList.Count == 0)
            {
                return result;
            }
            DomStringElement myString = null;
            //XTextParagraphList plist = null;
            DomDocumentContentElement ce = sourceList[0].DocumentContentElement;
            foreach (DomElement element in sourceList)
            {
                if (includeSelectionOnly)
                {
                    if (element.HasSelection == false)
                    {
                        continue;
                    }
                }
                if (element is DomCharElement)
                {
                    DomCharElement c = (DomCharElement)element;
                    if (myString != null && myString.CanMerge(c))
                    {
                        myString.Merge(c);
                    }
                    else
                    {
                        myString = new DomStringElement();
                        myString.OwnerDocument = c.OwnerDocument;
                        result.Add(myString);
                        myString.Merge(c);
                    }
                    continue;
                }

                myString = null;
                result.Add(element);
            }
            return result;
        }

        public static bool SplitElements(DomElementList SourceList , bool deeply )
        {
            bool result = false;
            DomElementList tempList = new DomElementList();
            tempList.AddRange(SourceList);
            foreach (DomElement element in tempList)
            {
                if (element is DomStringElement)
                {
                    // 将文本元素拆分成多个字符元素
                    int index = SourceList.IndexOf(element);
                    DomElementList cs = ((DomStringElement)element).SplitChars();
                    SourceList.RemoveAt(index);
                    for (int iCount = 0; iCount < cs.Count; iCount++)
                    {
                        SourceList.Insert(index + iCount, cs[iCount]);
                    }
                    result = true;
                }
                else if( element is DomContainerElement )
                {
                    SplitElements(((DomContainerElement)element).Elements, deeply);
                    //SourceList.Add(element);
                }
            }
            return result;
        }


        //public static bool SplitElements(XTextElementList SourceList)
        //{
        //    return SplitElements(SourceList, false);
        //    //bool result = false;
        //    //XTextElementList tempList = new XTextElementList();
        //    //tempList.AddRange(SourceList);
        //    //foreach (XTextElement element in tempList)
        //    //{
        //    //    if (element is XTextStringElement)
        //    //    {
        //    //        // 将文本元素拆分成多个字符元素
        //    //        int index = SourceList.IndexOf(element);
        //    //        XTextElementList cs = ((XTextStringElement)element).SplitChars();
        //    //        SourceList.RemoveAt(index);
        //    //        for (int iCount = 0; iCount < cs.Count; iCount++)
        //    //        {
        //    //            SourceList.Insert(index + iCount, cs[iCount]);
        //    //        }
        //    //        result = true;
        //    //    }
        //    //    else
        //    //    {
        //    //        //SourceList.Add(element);
        //    //    }
        //    //}
        //    //return result;
        //}

        /// <summary>
        /// 获得两个元素节点最近的共同的祖先元素
        /// </summary>
        /// <param name="element1">文档元素1</param>
        /// <param name="element2">文档元素2</param>
        /// <returns>共同的祖先元素</returns>
        public static DomElement GetRootElement(DomElement element1, DomElement element2)
        {
            if (element1 == null)
            {
                throw new ArgumentNullException("element1");
            }
            if (element2 == null)
            {
                throw new ArgumentNullException("element2");
            }

            if (element1 == element2)
            {
                return element1;
            }
            DomElementList parents = new DomElementList();
            DomElement parent = element1;
            while (parent != null)
            {
                parents.Add(parent);
                parent = parent.Parent;
            }

            parent = element2;
            while (parent != null)
            {
                if (parents.Contains(parent))
                {
                    return parent;
                }
                parent = parent.Parent;
            }
            return null;
        }

        public static ElementType GetElementType(Type elementType)
        {
            if (elementType == null)
            {
                throw new ArgumentNullException("elementType");
            }
            if (elementType.Equals(typeof(DomCharElement))
                || elementType.IsSubclassOf(typeof(DomCharElement)))
            {
                return ElementType.Text;
            }
            
            if (elementType.Equals(typeof(DomDocument))
                || elementType.IsSubclassOf(typeof(DomDocument)))
            {
                return ElementType.Document;
            }
              
            if( elementType.Equals( typeof( DomLineBreakElement ))
                || elementType.IsSubclassOf( typeof( DomLineBreakElement )))
            {
                return ElementType.LineBreak ;
            }
            if( elementType.Equals( typeof( DomPageBreakElement ))
                || elementType.IsSubclassOf( typeof( DomPageBreakElement )))
            {
                return ElementType.PageBreak ;
            }
            if( elementType.Equals( typeof( DomParagraphFlagElement ))
                || elementType.IsSubclassOf( typeof( DomParagraphFlagElement )))
            {
                return ElementType.ParagraphFlag ;
            }
            
            return ElementType.None;
        }

        //public static FileFormat GetFormat(string fileName)
        //{
        //    string ext = System.IO.Path.GetExtension(fileName);
        //    if (string.Compare(ext, ".rtf", true) == 0)
        //        return FileFormat.RTF;
        //    else if (string.Compare(ext, ".xml", true) == 0)
        //        return FileFormat.XML;
        //    else if (string.Compare(ext, ".txt", true) == 0)
        //        return FileFormat.Text;
        //    else if (string.Compare(ext, ".htm", true) == 0
        //        || string.Compare(ext, ".html", true) == 0)
        //        return FileFormat.Html;
        //    return FileFormat.XML;
        //}
    }
}
