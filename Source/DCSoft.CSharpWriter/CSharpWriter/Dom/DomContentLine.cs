/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using DCSoft.Printing ;
using DCSoft.Drawing;
using System.Collections;
using System.Collections.Generic;

namespace DCSoft.CSharpWriter.Dom
{
	/// <summary>
	/// 文本行对象
	/// </summary>
	public class DomContentLine : DomElementList
	{
        /// <summary>
        /// 对象所属的文档对象
        /// </summary>
        internal DomDocument _OwnerDocument = null;
        /// <summary>
        /// 对象所属的文档对象
        /// </summary>
        public virtual DomDocument OwnerDocument
        {
            get
            {
                return _OwnerDocument;
            }
            //set
            //{
            //    myOwnerDocument = value;
            //}
        }

        internal DomContentElement _OwnerContentElement = null;
        /// <summary>
        /// 对象所属的文档区域对象
        /// </summary>
        public DomContentElement OwnerContentElement
        {
            get
            {
                return _OwnerContentElement;
            }
            //set
            //{
            //    _DocumentContentElement = value;
            //}
        }

		/// <summary>
		/// 对象所在的文档页对象
		/// </summary>
		internal PrintPage _OwnerPage = null;
		/// <summary>
		/// 对象所在的文档页对象
		/// </summary>
		public PrintPage OwnerPage
		{
			get
            {
                return _OwnerPage ;
            }
            //set
            //{
            //    myOwnerPage = value;
            //}
		}


        /// <summary>
        /// 行状态无效标记
        /// </summary>
        internal bool InvalidateState = false;

        private int _GlobalIndex = 0;
        /// <summary>
        /// 在整个文档中的从1开始的行号
        /// </summary>
        public int GlobalIndex
        {
            get
            {
                return _GlobalIndex; 
            }
            set
            {
                _GlobalIndex = value; 
            }
        }

        private int _IndexInPage = 0;
        /// <summary>
        /// 在所在文档页中的从1开始的行号
        /// </summary>
        public int IndexInPage
        {
            get { return _IndexInPage; }
            set { _IndexInPage = value; }
        }
        ///// <summary>
        ///// 行号
        ///// </summary>
        //public int Index
        //{
        //    get
        //    {
        //        if (this.OwnerContentElement == null)
        //        {
        //            return 0;
        //        }
        //        else
        //        {
        //            return this.OwnerContentElement.PrivateLines.IndexOf(this);
        //        }
        //    }
        //}

        ///// <summary>
        ///// 文本行在所在页中的序号
        ///// </summary>
        //public int IndexInPage
        //{
        //    get
        //    {
        //        if( this.OwnerContentElement != null )
        //        {
        //            int iCount = 0 ;
        //            foreach( XTextLine line in this.OwnerContentElement.PrivateLines )
        //            {
        //                if( line.OwnerPage == _OwnerPage )
        //                {
        //                    if (line == this)
        //                    {
        //                        break;
        //                    }
        //                    iCount ++ ;
        //                }
        //            }
        //            return iCount ;
        //        }
        //        return -1 ;
        //    }
        //}

        ///// <summary>
        ///// 获得文本行所在的段落对象
        ///// </summary>
        //public XTextParagraph OwnerParagraph
        //{
        //    get
        //    {
        //        if( this.Count > 0 )
        //        {
        //            return ( XTextParagraph ) this[0].OwnerParagraph ;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}

        private ParagraphListStyle _ParagraphListStyle = ParagraphListStyle.None;
        /// <summary>
        /// 段落列表样式
        /// </summary>
        public ParagraphListStyle ParagraphListStyle
        {
            get
            {
                return _ParagraphListStyle; 
            }
            set
            {
                _ParagraphListStyle = value; 
            }
        }

        private int _ParagraphStyleIndex = -1;
        /// <summary>
        /// 段落样式编号
        /// </summary>
        public int ParagraphStyleIndex
        {
            get
            {
                return _ParagraphStyleIndex; 
            }
            set 
            {
                _ParagraphStyleIndex = value; 
            }
        }

        ///// <summary>
        ///// 段落样式对象
        ///// </summary>
        //public DocumentContentStyle RuntimeStyle
        //{
        //    get
        //    {
        //        return this.OwnerDocument.ContentStyles.GetRuntimeStyle(this.ParagraphStyleIndex);
        //    }
        //}

        private VerticalAlignStyle _VerticalAlign = VerticalAlignStyle.Bottom;
        /// <summary>
        /// 垂直对齐方式
        /// </summary>
        public VerticalAlignStyle VerticalAlign
        {
            get
            {
                return _VerticalAlign;
            }
            set
            {
                _VerticalAlign = value;
            }
        }

        private DocumentContentAlignment intAlign = DocumentContentAlignment.Left;
        /// <summary>
        /// 内容的水平对齐方式
        /// </summary>
        public DocumentContentAlignment Align
        {
            get
            {
                return intAlign;
            }
            set
            {
                intAlign = value;
            }
        }

		/// <summary>
		/// 左内边距
		/// </summary>
		private float _PaddingLeft = 0 ;
		/// <summary>
		/// 左内边距
		/// </summary>
        public float PaddingLeft
		{
			get
            {
                return _PaddingLeft ;
            }
			set
            {
                _PaddingLeft = value;
            }
		}

        private float _PaddingRight = 0f;
        /// <summary>
        /// 右内边距
        /// </summary>
        public float PaddingRight
        {
            get
            {
                return _PaddingRight; 
            }
            set
            {
                _PaddingRight = value; 
            }
        }

		/// <summary>
		/// 对象左端位置
		/// </summary>
		private float _Left = 0 ;
		/// <summary>
		/// 对象左端位置
		/// </summary>
		public float Left
		{
			get
            {
                return _Left ;
            }
			set
            {
                _Left = value;
            }
		}

        /// <summary>
        /// 文本行的绝对左端位置
        /// </summary>
        public float AbsLeft
        {
            get
            {
                if (this._OwnerContentElement == null)
                {
                    return _Left + this._OwnerDocument.Left;
                }
                else
                {
                    return _Left + _OwnerContentElement.AbsLeft;
                }
            }
        }

		/// <summary>
		/// 对象顶端位置
		/// </summary>
		private float _Top = 0 ;
		/// <summary>
		/// 对象顶端位置
		/// </summary>
        public float Top
		{
			get
            {
                return _Top ;
            }
			set
            {
                _Top = value;
            }
		}

        public float AbsTop
        {
            get
            {
                if (this._OwnerContentElement == null)
                {
                    return _Top + this._OwnerDocument.Top ;
                }
                else
                {
                    return _Top + _OwnerContentElement.AbsTop ;
                } 
            }
        }

		/// <summary>
		/// 对象宽度
		/// </summary>
		private float _Width = 0;
		/// <summary>
		/// 对象宽度
		/// </summary>
        public float Width
		{
			get{ return _Width ;}
			set{ _Width = value;}
		}
		/// <summary>
		/// 对象高度
		/// </summary>
		private float _Height = 0 ;
		/// <summary>
		/// 对象高度
		/// </summary>
        public float Height
		{
			get
            {
                return _Height ;
            }
			set
            {
                _Height = value;
            }
		}

        /// <summary>
        /// 文档行的显示高度
        /// </summary>
        public float ViewHeight
        {
            get
            {
                if (this._AdditionHeight >= 0)
                {
                    return this._Height;
                }
                else
                {
                    return this._Height + this._AdditionHeight;
                }
            }
        }

        private float _AdditionHeight = 0;
        /// <summary>
        /// 额外的行高，这是由于段落行间距设置而引起的
        /// </summary>
        public float AdditionHeight
        {
            get
            {
                return _AdditionHeight; 
            }
            set
            {
                _AdditionHeight = value; 
            }
        }

		/// <summary>
		/// 对象底端位置
		/// </summary>
        public float Bottom
        {
            get
            {
                return _Top + _Height + _AdditionHeight ; 
            }
        }

        public float AbsBottom
        {
            get
            {
                return this.AbsTop + _Height + _AdditionHeight ;
            }
        }

        private float _BeforeSpacing = 0;
        /// <summary>
        /// 文本行前额外增加的间距
        /// </summary>
        public float BeforeSpacing
        {
            get
            {
                return _BeforeSpacing; 
            }
            set
            {
                _BeforeSpacing = value; 
            }
        }

        ///// <summary>
        ///// 行间距
        ///// </summary>
        //private float _LineSpacing = 0 ;
        ///// <summary>
        ///// 行间距
        ///// </summary>
        //public float LineSpacing
        //{
        //    get{ return _LineSpacing ;}
        //    set{ _LineSpacing = value;}
        //}
		/// <summary>
		/// 字符间距
		/// </summary>
		private float _Spacing = 0 ;
		/// <summary>
		/// 字符间距
		/// </summary>
		public float Spacing
		{
			get{ return _Spacing ;}
			set{ _Spacing = value;}
		}
		 
		/// <summary>
		/// 对象边框
		/// </summary>
		public System.Drawing.RectangleF Bounds
		{
			get
            {
                return new System.Drawing.RectangleF(
                    _Left ,
                    _Top ,
                    _Width ,
                    _Height );
            }
		}

        /// <summary>
        /// 采用绝对坐标下的对象边框
        /// </summary>
        public System.Drawing.RectangleF AbsBounds
        {
            get
            {
                return new System.Drawing.RectangleF(
                    this.AbsLeft,
                    this.AbsTop,
                    _Width,
                    _Height);
            }
        }
	 
		public float ContentWidth
		{
			get
			{
                if (this.Count == 0)
                {
                    return 0;
                }
                float WidthCount = 0;
                foreach (DomElement element in this)
				{
					WidthCount = WidthCount + element.Width + this._Spacing ;
				}
				return WidthCount - this._Spacing ;
			}
		}
		/// <summary>
		/// 本文本行行尾是不是行结束类型的元素
		/// </summary>
		public bool HasLineEndElement
		{
			get
			{
				if( this.Count > 0 )
				{
					DomElement element = this.LastElement ;
                    if (element is DomEOFElement)
                    {
                        return true;
                    }
					return this.OwnerDocument.DocumentControler.IsNewLine( this.LastElement );
				}
				return false;
			}
		}

		public static float LineHeighFix = 0 ;
 
        private float _ContentHeight = -1;

        /// <summary>
        /// 更新行内容高度
        /// </summary>
        public void UpdateContentHeight()
        {
            _ContentHeight = -1;
        }

        internal static float ContentTopFix = 3.125f;

        /// <summary>
        /// 内容高度
        /// </summary>
        public float ContentHeight
        {
            get
            {
                if (_ContentHeight < 0)
                {
                    _ContentHeight = 0;
                    foreach (DomElement element in this)
                    {
                        //if (element is XTextCharElement)
                        //{
                        //    XTextCharElement chr = (XTextCharElement)element;
                        //    _ContentHeight = Math.Max(_ContentHeight, chr._FontHeight);
                        //}
                        ////else if (element is XTextFieldBorderElement)
                        ////{
                        ////    // 字段边界元素不参与计算
                        ////}
                        //else
                        {
                            _ContentHeight = Math.Max(element.Height, _ContentHeight);
                        }
                    }
                     
                    _ContentHeight += ContentTopFix;
                }
                return _ContentHeight;
            }
        }

        /// <summary>
        /// 获得本行中最大字体高度
        /// </summary>
        public float MaxFontHeight
        {
            get
            {
                float result = 50;
                foreach (DomElement element in this)
                {
                    DocumentContentStyle rs = element.RuntimeStyle;
                    if (result < rs.DefaultLineHeight)
                    {
                        result = rs.DefaultLineHeight;
                    }
                    //if (element is XTextCharElement)
                    //{
                    //    XTextCharElement c = (XTextCharElement)element;
                    //    if (result < c._FontHeight)
                    //    {
                    //        result = c._FontHeight;
                    //    }
                    //}
                }
                return result;
            }
        }
         

		/// <summary>
		/// 刷新状态
		/// </summary>
		/// <returns>操作是否成功</returns>
		public bool RefreshState()
		{
            if (this.Count == 0)
            {
                return false;
            }
            DocumentControler controler = null ;
            foreach (DomElement element in this)
            {
                if (element.OwnerDocument != null)
                {
                    controler = element.OwnerDocument.DocumentControler;
                    break;
                }
            }
            //XTextParagraphFlagElement pe = this.LastElement as XTextParagraphFlagElement ;
            //if( pe != null )
            //{
            //    pe.RefreshSize2( this.SafeGet( this.Count - 2 ));
            //}
			DomLineBreakElement lb = this.LastElement as DomLineBreakElement ;
			if( lb != null )
			{
				lb.RefreshSize2( this.SafeGet( this.Count - 2 ));
			}


//			if( this.Count == 1 )
//			{
//				XTextElement element = this[ 0 ];
//				this.intHeight = element.Height + LineHeighFix + this.LineSpacing ;
//				element.Left = this.intMargin ;
//				element.Top = 0 ;
//				return true ;
//			}
            this._ContentHeight = -1;
			float contentWidth = 0 ;
			float lineHeight = 10 ;
            foreach (DomElement element in this)
            {
                 
                contentWidth = contentWidth + this.Spacing + element.Width;
                element.WidthFix = 0;
                float eh = element.Height;
                //if (element is XTextCharElement)
                //{
                //    XTextCharElement chr = (XTextCharElement)element;
                //    eh = Math.Max(eh, chr._FontHeight);
                //}
                if (eh > lineHeight)
                {
                    lineHeight = eh;
                }
                element.OwnerLine = this;
            }//foreach
			contentWidth -= this.Spacing ;
            
			this.Height = ( float ) Math.Ceiling( lineHeight + LineHeighFix );

//			if( EOF != null )
//				EOF.Height = this.intHeight ;
 
			
			bool CanFix = true;
			if( this.Count == 1 )
			{
				CanFix = false;
			}
			else
			{
                if (this.HasLineEndElement)// this.OwnerDocument.IsNewLine( this.LastElement ) || this.LastElement is XTextEOF )
                {
                    CanFix = false;
                }
                else if (this.OwnerContentElement.PrivateLines.LastLine == this)
                {
                    CanFix = false;
                }
			}
			DocumentContentAlignment align = this.Align ;
			
			// 元素空白平均修正量
			float fix = 0 ;
			// 总空白量
			float Blank = this.Width - this.PaddingLeft - contentWidth ;
            if (Blank < 0)
            {
                return false;
            }
			if( CanFix )
			{
				// 计算由于元素分组而损失的空白区域个数
				// 文档中连续的英文和数字字符之间没有修正空白,制表符和书签也拒绝修正空白
				// 由于存在拒绝修正空白,导致空白分摊的元素减少,此处就修正这种空白分摊元素
				// 减少而带来的影响
				int GroupFix = 0 ;
				for( int iCount = 0 ; iCount < this.Count ; iCount ++ )
				{
					 
			
					DomCharElement c = this[ iCount ] as DomCharElement ;
					if( c != null )
					{
                        if (c.CharValue == '\t')
                        {
                            GroupFix++;
                        }
                        else if (iCount < this.Count - 1)
                        {
                            if (controler.IsEnglishLetterOrDigit(c.CharValue))
                            {
                                DomCharElement c2 = this[iCount + 1] as DomCharElement;
                                if (c2 != null
                                    && controler.IsEnglishLetterOrDigit(c2.CharValue))
                                {
                                    GroupFix++;
                                }
                            }
                        }		
					}
				}
                if (GroupFix > this.Count - 2)
                {
                    GroupFix = 0;
                }
                if (this.Count > 1)
                {
                    fix = (int)Math.Ceiling(Blank / (this.Count - 1.0 - GroupFix));
                }
                if (fix < 0)
                {
                    fix = 0;
                }

				align = DocumentContentAlignment.Justify ;
			}
			else
			{
				fix = 0 ;
			}
			float leftCount = 0 ;
            if (align == DocumentContentAlignment.Left)
            {
                leftCount = this.PaddingLeft;
            }
            else if (align == DocumentContentAlignment.Center)
            {
                leftCount = this.PaddingLeft + (this.Width - this.PaddingLeft - contentWidth) / 2;
            }
            else if (align == DocumentContentAlignment.Right)
            {
                leftCount = this.PaddingLeft + (this.Width - this.PaddingLeft - contentWidth);
            }
            else
            {
                leftCount = this.PaddingLeft;
            }
            //foreach( XTextElement element in this )
            for( int iCount = 0 ; iCount < this.Count ; iCount ++ )
			{
				DomElement element = this[ iCount ] ;
				element.Left = leftCount ;
                float ctf = ContentTopFix;
                //if (this.AdditionHeight < 0)
                //{
                //    ctf = ctf + this.AdditionHeight;
                //}
                switch (this.VerticalAlign)
                {
                    case VerticalAlignStyle.Top :
                        element.Top = ContentTopFix ;
                        break;
                    case VerticalAlignStyle.Middle :
                        element.Top =  ContentTopFix + ( lineHeight - element.Height) / 2.0f;
                        break;
                    case  VerticalAlignStyle.Bottom :
                        element.Top = ctf +lineHeight - element.Height;
                        break;
                    case VerticalAlignStyle.Justify :
                        element.Top = ContentTopFix +(lineHeight - element.Height) / 2.0f;
                        break;
                    default :
                        element.Top = ctf + lineHeight - element.Height;
                        break;
                }//switch
               
				//bool IsTab = false ;
				bool FixFlag = true ;
				// TAB制表符不接收修正空隙
				if( element is DomCharElement )
				{
					DomCharElement c = ( DomCharElement ) element ;
                    if (c.CharValue == '\t')
                    {
                        FixFlag = false;
                    }
                    else if (iCount < this.Count - 1)
                    {
                        if (controler.IsEnglishLetterOrDigit(c.CharValue))
                        {
                            DomCharElement c2 = this[iCount + 1] as DomCharElement;
                            if (c2 != null
                                && controler.IsEnglishLetterOrDigit(c2.CharValue))
                            {
                                FixFlag = false;
                            }
                        }
                    }
				}
			 
//				// 出现拒绝修正情况,重新计算平均修正空隙
//				if( CanFix && FixFlag == false )
//				{
//					if( iCount < this.Count - 2 )
//					{
//						Blank = ( this.intWidth - LeftCount - element.Width ) ;
//						for( int iCount2 = iCount + 1 ; iCount2 < this.Count ; iCount2 ++ )
//						{
//							Blank -= this[ iCount ].Width ;
//						}
//						if( Blank > 0 )
//						{
//							fix = Blank / ( this.Count - iCount - 1.0 );
//						}
//						else
//							fix = 0 ;
//					}
//				}
//				else if( element is XTextNull )
//				{
//					// 若元素是没有实际意义的占位符合则不接收修正空隙
//					FixFlag = false;
//				}
                if (CanFix && FixFlag)
                {
                    if (Blank < fix)
                    {
                        fix = Blank;
                    }
                    element.WidthFix = fix + _Spacing;
                    Blank -= fix;
                    if (Blank <= 0)
                    {
                        fix = 0;
                        Blank = 0;
                    }
                }
                else
                {
                    element.WidthFix = _Spacing;
                }
                leftCount = leftCount + element.Width + element.WidthFix;
			}
			//this.Height += this.LineSpacing  ;
			return true;
		}
 
        //private ParagraphListStyle _L

		//private float _LeftCount = 0 ;
		internal bool AddElement( DomElement element )
		{
             
			if( this.Count == 0 )
			{
				//intLeftCount = 0 ;
				this.List.Add( element );
                 
                {
                    DomParagraphFlagElement p = element.OwnerParagraphEOF;
                    if (p != null)
                    {
                        DocumentContentStyle rs = p.RuntimeStyle;
                        if (element == p.ParagraphFirstContentElement )
                        {
                            if (rs.NumberedList || rs.BulletedList)
                            {
                                this.PaddingLeft = rs.LeftIndent;
                                if (rs.NumberedList)
                                {
                                    this.ParagraphListStyle = ParagraphListStyle.NumberedList;
                                }
                                else if (rs.BulletedList)
                                {
                                    this.ParagraphListStyle = ParagraphListStyle.BulletedList;
                                }
                                this.ParagraphStyleIndex = p.ListIndex;
                            }
                            this.PaddingLeft = rs.LeftIndent + rs.FirstLineIndent;
                        }
                        else
                        {
                            this.PaddingLeft = rs.LeftIndent;
                        }
                    }
                    else
                    {
                        this.PaddingLeft = 0;
                    }
                }
				element.Left = this.PaddingLeft ;
                if (this.ParagraphListStyle == Dom.ParagraphListStyle.BulletedList
                    || this.ParagraphListStyle == Dom.ParagraphListStyle.NumberedList)
                {
                    DomParagraphListItemElement item = new DomParagraphListItemElement();
                    DocumentContentStyle rs = element.RuntimeStyle;
                    float size = rs.Font.GetHeight(element.OwnerDocument.DocumentGraphicsUnit);
                    item.Width = size;
                    item.Height = size;
                    if (this.ParagraphListStyle == Dom.ParagraphListStyle.NumberedList)
                    {
                        item.Width = item.Width * 2;
                    }
                    item.OwnerDocument = element.OwnerDocument;
                    item.StyleIndex = element.StyleIndex;
                    this.Insert(0, item);
                     // float size = rs.Font.GetHeight( 
                }
				//intLeftCount = intLeftCount + element.Width + this.intSpacing ;
				//element.OwnerLine = this ;
				return true;
			}
			float WidthCount = this.ContentWidth ;
            if ((element is DomParagraphFlagElement) == false)
            {
                // 在排版中段落元素不计宽度，可以无条件的添加到文档行中。
                if (WidthCount + element.Width + this._Spacing > this.Width - this.PaddingLeft)
                {
                    return false;
                }
            }
			DomElement PreElement = ( DomElement ) this.List[ this.Count - 1 ];
			element.Left = PreElement.Left + PreElement.Width + this._Spacing ;
			this.List.Add( element );
			//intLeftCount = intLeftCount + element.Width + this.intSpacing ;
			//element.OwnerLine = this ;
			return true;
		}
         
		/// <summary>
		/// 删除并返回最后一个元素,并保证列表中有内容
		/// </summary>
		/// <returns>返回的最后一个元素</returns>
		internal DomElement PopupLastElement()
		{
			if( this.Count > 1 )
			{
				DomElement e = this[ this.Count - 1 ];
				this.List.RemoveAt( this.Count - 1 );
				return e;
			}
			return null;
		}

//		/// <summary>
//		/// 比较两个文本行状态是否一致
//		/// </summary>
//		/// <param name="list">元素列表</param>
//		/// <returns>两个对象的元素清单是否一致</returns>
//		internal bool EqualsState( XTextElementList list )
//		{
//			if( list != null && list.Count == this.Count )
//			{
//				for( int iCount = 0 ; iCount < this.Count ; iCount ++ )
//				{
//					XTextElement element = this[ iCount ] ;
//					if( list[ iCount ] != element )
//						return false;
//					if( element.SizeInvalid )
//						return false;
//				}
//				return true ;
//			}
//			return false;
//		}

		private string strLineBack = null;
		private System.Collections.ArrayList ElementsBack = null;
		private System.Drawing.RectangleF[] BoundsBack = null;
		 
		public override string ToString()
		{
			System.Text.StringBuilder str = new System.Text.StringBuilder();
			foreach( DomElement e in this )
			{
				str.Append( e.ToString());
			}
			return str.ToString();
		}

	}
}