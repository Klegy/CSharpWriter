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

namespace DCSoft.CSharpWriter.Dom
{
    /// <summary>
    /// 文档内容统计信息对象
    /// </summary>
    public class WordCountResult
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public WordCountResult()
        {
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="content">要统计的文档内容</param>
        public WordCountResult( DomDocument document , System.Collections.IEnumerable content)
        {
            if (document == null)
            {
                throw new ArgumentNullException("document");
            }
            if (content == null)
            {
                throw new ArgumentNullException("content");
            }
            this._Pages = document.Pages.Count;

            bool wordFlag = false;
            DomContentLine line = null;
            //XTextParagraphElement p = null;
            foreach (DomElement element in content)
            {
                if (line != element.OwnerLine)
                {
                    line = element.OwnerLine;
                    _Lines++;
                }
                if (element is DomParagraphFlagElement)
                {
                    _Paragraphs++;
                }
                if (element is DomCharElement)
                {
                    char c = ((DomCharElement)element).CharValue;
                    _Chars++;
                    if (char.IsWhiteSpace(c) == false)
                    {
                        _CharsNoWhitespace++;
                    }
                    if ((c >= 'a' && c <= 'z')
                        || (c >= 'A' && c <= 'Z'))
                    {
                        if (wordFlag == false)
                        {
                            _Words++;
                        }
                        wordFlag = true;
                    }
                    else
                    {
                        if (char.IsWhiteSpace(c) == false)
                        {
                            _Words++;
                        }
                        wordFlag = false;
                    }
                    continue;
                }//foreach
                wordFlag = false;
                if (element is DomImageElement)
                {
                    _Images++;
                }
            }//foreach
        }

        private int _Pages = 0;
        /// <summary>
        /// 页数
        /// </summary>
        public int Pages
        {
            get { return _Pages; }
            set { _Pages = value; }
        }

        private int _Paragraphs = 0;
        /// <summary>
        /// 段落数
        /// </summary>
        public int Paragraphs
        {
            get { return _Paragraphs; }
            set { _Paragraphs = value; }
        }

        private int _Words = 0;
        /// <summary>
        /// 单词数
        /// </summary>
        public int Words
        {
            get { return _Words; }
            set { _Words = value; }
        }

        private int _CharsNoWhitespace = 0;
        /// <summary>
        /// 不含空格的字符数
        /// </summary>
        public int CharsNoWhitespace
        {
            get { return _CharsNoWhitespace; }
            set { _CharsNoWhitespace = value; }
        }

        private int _Chars = 0;
        /// <summary>
        /// 含空格的字符数
        /// </summary>
        public int Chars
        {
            get { return _Chars; }
            set { _Chars = value; }
        }

        private int _Lines = 0;
        /// <summary>
        /// 文本行数
        /// </summary>
        public int Lines
        {
            get { return _Lines; }
            set { _Lines = value; }
        }

        private int _Images = 0;
        /// <summary>
        /// 图片个数
        /// </summary>
        public int Images
        {
            get { return _Images; }
            set { _Images = value; }
        }
    }
}
