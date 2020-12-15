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
using DCSoft.CSharpWriter ;
using DCSoft.CSharpWriter.Commands ;

namespace DCSoft.CSharpWriter.Dom
{
    /// <summary>
    /// 文档内容查找和替换操作器
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    public class ContentSearchReplacer
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public ContentSearchReplacer()
        {
        }

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
                if (_Document != null)
                {
                    _Content = _Document.Content;
                }
            }
        }
        private DomContent _Content = null;
        /// <summary>
        /// 文档内容对象
        /// </summary>
        public DomContent Content
        {
            get { return _Content; }
            set { _Content = value; }
        }

        /// <summary>
        /// 全局替换
        /// </summary>
        /// <param name="args">参数</param>
        /// <returns>替换的次数</returns>
        public int ReplaceAll(SearchReplaceCommandArgs args)
        {
            int result = 0;
            List<int> indexs = new List<int>();
            SearchReplaceCommandArgs args2 = args.Clone();
            args2.Backward = false ;
            int currentIndex = this.Content.Count - 1 ;
            this.Document.BeginLogUndo();
            DocumentControler controler = this.Document.DocumentControler;
            Dictionary<DomContentElement, int> startIndexs = new Dictionary<DomContentElement, int>();
            while (true)
            {
                int index = Search(args2, false, currentIndex );
                if (index >= 0)
                {
                    DomSelection mySelection = new DomSelection(this.Document.CurrentContentElement);
                    mySelection.Refresh(index, args.SearchString.Length);
                    DomContainerElement container = null;
                    int elementIndex = 0 ;
                    this.Content.GetPositonInfo(index, out container, out elementIndex, false);
                    DomContentElement contentElement = container.ContentElement;
                    int pi = contentElement.PrivateContent.IndexOf( this.Content[index] );
                    if (startIndexs.ContainsKey(contentElement))
                    {
                        startIndexs[contentElement] = Math.Min(startIndexs[contentElement], pi);
                    }
                    else
                    {
                        startIndexs[contentElement] = pi;
                    }
                    indexs.Add(index);
                    if (string.IsNullOrEmpty(args.ReplaceString))
                    {
                        this.Content.DeleteSelection(true, false, true , mySelection );
                    }
                    else
                    {
                        DomElementList newElements = this.Document.CreateTextElements(
                            args.ReplaceString ,
                            (DocumentContentStyle)this.Document.CurrentParagraphStyle,
                            (DocumentContentStyle)this.Document.EditorCurrentStyle.Clone());
                        ReplaceElementsArgs args3 = new ReplaceElementsArgs(
                            container,
                            index,
                            0,
                            newElements,
                            true,
                            false ,
                            true);
                        int repResult = this.Document.ReplaceElements(args3);
                    }
                    result++;
                }
                else
                {
                    break;
                }
                currentIndex = index + args2.SearchString.Length;
            }//while
            this.Document.EndLogUndo();
            if (startIndexs.Count > 0)
            {
                bool refreshPage = false;
                foreach (DomContentElement ce in startIndexs.Keys)
                {
                    ce.UpdateContentElements(true);
                    ce.UpdateContentVersion();
                    ce._NeedRefreshPage = false;
                    ce.RefreshPrivateContent(startIndexs[ce]);
                    if (ce._NeedRefreshPage)
                    {
                        refreshPage = true;
                    }
                }
                if (refreshPage)
                {
                    this.Document.RefreshPages();
                    if (this.Document.EditorControl != null)
                    {
                        this.Document.EditorControl.UpdatePages();
                        this.Document.EditorControl.UpdateTextCaret();
                        this.Document.EditorControl.Invalidate();
                    }
                }
            }
            return startIndexs.Count;
        }

        /// <summary>
        /// 替换
        /// </summary>
        /// <param name="args">参数</param>
        /// <returns>操作的元素在文档中的序号</returns>
        public int Replace(SearchReplaceCommandArgs args )
        {
            int index = Search(args, true , -1 );
            if (index >= 0)
            {
                // 找到内容了，执行替换操作
                this.Document.BeginLogUndo();

                bool insertModeBack = false;
                if (this.Document.EditorControl != null)
                {
                    insertModeBack = this.Document.EditorControl.InsertMode;
                    this.Document.EditorControl.InsertMode = false;
                }
                int selectionStart = this.Content.Selection.AbsStartIndex;
                if (string.IsNullOrEmpty(args.ReplaceString))
                {
                    this.Document.DocumentControler.Delete();
                }
                else
                {
                    this.Document.DocumentControler.InsertString(args.ReplaceString );
                }
                this.Content.SetSelection(selectionStart, args.ReplaceString.Length);
                if (this.Document.EditorControl != null)
                {
                    this.Document.EditorControl.InsertMode = insertModeBack;
                }
                this.Document.EndLogUndo();
            }
            return index;
        }

        /// <summary>
        /// 执行查找
        /// </summary>
        /// <param name="args">参数</param>
        /// <param name="setSelection">找到后是否设置文档选择状态</param>
        /// <returns>找到的元素在文档中的序号</returns>
        public int Search(SearchReplaceCommandArgs args, bool setSelection , int startIndex )
        {
            if (args == null)
            {
                throw new ArgumentNullException("args");
            }
            if (string.IsNullOrEmpty(args.SearchString))
            {
                return -1;
            }
            DomContent content = this.Content;
            int searchStringLength = args.SearchString.Length;
            int newStartIndex = -1;
            if (args.Backward)
            {
                // 向后查找
                if (startIndex < 0)
                {
                    startIndex = content.Selection.AbsEndIndex;
                }
                int contentLength = content.Count;
                for (int iCount = startIndex; iCount < contentLength; iCount++)
                {
                    DomElement element = content[iCount];
                    if (element is DomCharElement)
                    {
                        char c = ((DomCharElement)element).CharValue;
                        if (EqualsChar(c, args.SearchString[0], args.IgnoreCase))
                        {
                            if (searchStringLength == 1)
                            {
                                newStartIndex = iCount;
                                break;
                            }
                            else if (contentLength - iCount >= searchStringLength)
                            {
                                for (int iCount2 = 1; iCount2 < searchStringLength; iCount2++)
                                {
                                    DomElement element2 = content[iCount + iCount2];
                                    if (element2 is DomCharElement)
                                    {
                                        if (EqualsChar(
                                            ((DomCharElement)element2).CharValue,
                                            args.SearchString[iCount2],
                                            args.IgnoreCase))
                                        {
                                            if (iCount2 == searchStringLength - 1)
                                            {
                                                newStartIndex = iCount;
                                                goto EndSetNewStartIndex;
                                            }
                                        }
                                        else
                                        {
                                            // 判断失败
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        // 不是字符元素，判断失败
                                        break;
                                    }
                                }//for
                            }//else if
                        }
                    }//if
                }//for
            }//if
            else
            {
                // 向前查找
                if (startIndex < 0)
                {
                    startIndex = content.Selection.AbsStartIndex;
                }
                char lastChar = args.SearchString[args.SearchString.Length - 1];
                for (int iCount = startIndex; iCount >= 0; iCount--)
                {
                    DomElement element = content[iCount];
                    if (element is DomCharElement)
                    {
                        char c = ((DomCharElement)element).CharValue;
                        if (EqualsChar(c, lastChar, args.IgnoreCase))
                        {
                            if (searchStringLength == 1)
                            {
                                newStartIndex = iCount;
                                break;
                            }
                            else if (iCount >= searchStringLength - 1 )
                            {
                                for (int iCount2 = searchStringLength - 2; iCount2 >= 0; iCount2--)
                                {
                                    DomElement element2 = content[iCount - searchStringLength + iCount2 + 1];
                                    if (element2 is DomCharElement)
                                    {
                                        if (EqualsChar(
                                            ((DomCharElement)element2).CharValue,
                                            args.SearchString[iCount2],
                                            args.IgnoreCase))
                                        {
                                            if (iCount2 == 0)
                                            {
                                                newStartIndex = iCount - searchStringLength + 1 ;
                                                goto EndSetNewStartIndex;
                                            }
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }//for
            }
        EndSetNewStartIndex: ;
            if (setSelection)
            {
                if (newStartIndex >= 0)
                {
                    content.SetSelection(newStartIndex, args.SearchString.Length);
                }
            }
            return newStartIndex;
        }

        private bool EqualsChar(char c1, char c2, bool ignoreCase)
        {
            if (c1 == c2)
            {
                return true;
            }
            if (ignoreCase)
            {
                if (char.IsLower(c1))
                {
                    return c1 == char.ToLower(c2);
                }
                else if (char.IsUpper(c1))
                {
                    return c1 == char.ToUpper(c2);
                }
            }
            return false;
        }
    }
}
