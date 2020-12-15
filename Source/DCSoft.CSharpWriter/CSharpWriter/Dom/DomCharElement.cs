/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using DCSoft.Drawing;

namespace DCSoft.CSharpWriter.Dom
{
	/// <summary>
	/// 字符对象
	/// </summary>
    /// <remarks>编写 袁永福</remarks>
    [Serializable()]
    public class DomCharElement : DomElement
	{
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DomCharElement()
        {
        }
		 
		private char _CharValue = char.MinValue ;
		/// <summary>
		/// 字符数据
		/// </summary>
		public char CharValue
		{
			get
            {
                return _CharValue ;
            }
			set
            {
                _CharValue = value;
            }
		}

        /// <summary>
        /// 所用字体的高度
        /// </summary>
        internal float _FontHeight = 0;

        //[System.ComponentModel.Browsable( false )]
        //public override bool UseFontStyle
        //{
        //    get
        //    {
        //        return true;
        //    }
        //}

        /// <summary>
        /// 处理文档用户界面事件
        /// </summary>
        /// <param name="args">事件参数</param>
        public override void HandleDocumentEvent(DocumentEventArgs args)
        {
            if (args.Style == DocumentEventStyles.MouseDown)
            {
                string link = this.Style.Link;
                if (link != null && link.Length > 0)
                {
                    if (link.StartsWith("#"))
                    {
                        string name = link.Substring(1);
                         
                    }
                    else
                    {
                        this.OwnerDocument.Content.MoveSelectStart(this);
                    }
                    this.OwnerDocument.OnLinkClick(this, link);
                    args.CancelBubble = true;
                }
            }
            else if (args.Style == DocumentEventStyles.MouseMove)
            {
                string link = this.Style.Link;
                if (link != null && link.Length > 0)
                {
                    args.Cursor = System.Windows.Forms.Cursors.Hand;
                    args.Tooltip = link;
                }
            }
            else
            {
                base.HandleDocumentEvent(args);
            }
        }
          

        internal void SetWidthForTab()
        {
            if (_CharValue == '\t')
            {
                this.Width = WriterUtils.GetTabWidth( 
                    this.Left - this.OwnerDocument.Left ,
                    this.OwnerDocument.DefaultStyle.TabWidth );
            }
        }

        public override string ToPlaintString()
        {
            return _CharValue.ToString();
        }

		public override string ToString()
		{
			return _CharValue.ToString();
		}

	}//public class XTextChar : XTextElement , IFontable

    /// <summary>
    /// 文本标记样式
    /// </summary>
    public enum TextScriptStyle
    {
        /// <summary>
        /// 无样式
        /// </summary>
        None,
        /// <summary>
        /// 上标
        /// </summary>
        Superscript,
        /// <summary>
        /// 下标
        /// </summary>
        Subscript
    }

}