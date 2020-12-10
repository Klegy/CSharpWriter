/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;

using System.Drawing;
using DCSoft.Drawing;
using System.Xml.Serialization;

namespace DCSoft.CSharpWriter.Dom
{
	/// <summary>
	/// 段落列表样式
	/// </summary>
	public enum ParagraphListStyle
	{
		/// <summary>
		/// 无样式
		/// </summary>
		None ,
		/// <summary>
		/// 圆头列表
		/// </summary>
		BulletedList ,
		/// <summary>
		/// 带数字的列表
		/// </summary>
		NumberedList 
	}


	/// <summary>
	/// 段落对象
	/// </summary>
    /// <remarks >本对象只在加载或保存文档是临时生成。</remarks>
	[Serializable()]
    [System.Diagnostics.DebuggerDisplay("Para :{ PreviewString }")]
    [System.Xml.Serialization.XmlType("XParagraph")]
	public class DomParagraphElement : DomContainerElement
	{
		/// <summary>
		/// 初始化对象
		/// </summary>
		public DomParagraphElement(  )
		{
            //myEOFElement = new XTextParagraphEOF();
            //myEOFElement.Parent = this;
            //this.Elements.EOFElement = myEOFElement;
            //myElements.CanDeleteEOF = true;
		}

        //internal void SetEOF( XTextParagraphEOF eof )
        //{
        //    myEOFElement = eof ;
        //    myEOFElement.Parent = this;
        //    this.Elements.EOFElement = eof ;
        //    //myEOFElement.myOwnerParagraph = this ;
        //}

        //public XTextParagraphEOF PEOF
        //{
        //    get
        //    {
        //        return ( XTextParagraphEOF ) myEOFElement ;
        //    }
        //}

        //public override bool AcceptChildElement(Type elementType)
        //{
        //    if( elementType == typeof( XTextParagraphElement )
        //        || elementType.IsSubclassOf( typeof( XTextParagraphElement )))
        //        return false ;
        //    else
        //        return true ;
        //}

//        /// <summary>
//        /// 段落首行行首边距
//        /// </summary>
//        public int FirstLineIndent
//        {
//            get
//            {
//                return this.PEOF.FirstLineIndent ; 
//            }
//            set
//            {
//                this.PEOF.FirstLineIndent = value;
//            }
//        }

//        /// <summary>
//        /// 整个段落的左缩进量
//        /// </summary>
//        public int LeftIndent
//        {
//            get{ return this.PEOF.LeftIndent ; }
//            set{ this.PEOF.LeftIndent = value;}
//        }

//        /// <summary>
//        /// 段落列表样式
//        /// </summary>
//        public ParagraphListStyle ListStyle
//        {
//            get{ return this.PEOF.ListStyle ;}
//            set{ this.PEOF.ListStyle = value;}
//        }

        ///// <summary>
        ///// 段落在列表中的序号
        ///// </summary>
        //private int intListIndex = 0;
        ///// <summary>
        ///// 段落在列表中的序号
        ///// </summary>
        //[System.ComponentModel.Browsable(false)]
        //public int ListIndex
        //{
        //    get { return intListIndex; }
        //    set { intListIndex = value; }
        //}

        ///// <summary>
        ///// 段落在列表中的序号
        ///// </summary>
        //[System.ComponentModel.Browsable(false)]
        //public int ListIndex
        //{
        //    get
        //    {
               
        //        if (this.EOFElement == null)
        //            return 0;
        //        else
        //            return ((XTextParagraphFlagElement)this.EOFElement).ListIndex;
        //    }
        //}


//        /// <summary>
//        /// 内容对齐方式
//        /// </summary>
//        public TextAlignment Align
//        {
//            get{ return this.PEOF.Align ;}
//            set{ this.PEOF.Align = value;}
//        }
//
//		/// <summary>
//		/// 复制对象
//		/// </summary>
//		/// <param name="Deeply">是否深入复制子元素</param>
//		/// <returns>复制品</returns>
//		public override XTextElement Clone(bool Deeply)
//		{
//			XTextParagraph p = new XTextParagraph();
//			p.intAlign = this.intAlign ;
//			p.myOwnerDocument = this.myOwnerDocument ;
//			p.myParent = this.myParent ;
//			p.intListStyle = this.intListStyle ;
//			p.intFirstLineIndent = this.intFirstLineIndent ;
//			p.intLeftIndent = this.intLeftIndent ;
//			if( Deeply )
//			{
//				foreach( XTextElement element in myElements )
//				{
//					XTextElement e2 = element.Clone( Deeply );
//					e2.OwnerDocument = this.OwnerDocument ;
//					e2.Parent = p ;
//					p.Elements.Add( e2 );
//				}
//			}
//			return p ;
//		}

        //[System.ComponentModel.Browsable(false)]
        //[System.Xml.Serialization.XmlArrayItem("String", typeof(XTextStringElement))]
        //[System.Xml.Serialization.XmlArrayItem("Char", typeof(XTextCharElement))]
        //[System.Xml.Serialization.XmlArrayItem("Image", typeof(XTextImageElement))]
        //[System.Xml.Serialization.XmlArrayItem("LineBreak", typeof(XTextLineBreakElement))]
        //[System.Xml.Serialization.XmlArrayItem("Object", typeof(XTextObjectElement))]
        //[System.Xml.Serialization.XmlArrayItem("Bookmark", typeof(XTextBookmark))]
        //[System.Xml.Serialization.XmlArrayItem("PFlag", typeof(XTextParagraphFlagElement))]
        //public override XTextElementList Elements
        //{
        //    get
        //    {
        //        return base.Elements;
        //    }
        //    set
        //    {
        //        base.Elements = value;
        //    }
        //}

		/// <summary>
		/// 修正分隔元素
		/// </summary>
		/// <param name="SplitElement"></param>
		/// <returns></returns>
		internal DomElement FixSplitElement( DomElement SplitElement )
		{
            if (this.Elements.Contains(SplitElement))
            {
                return SplitElement;
            }
			DomElement element = SplitElement ;
			while( true )
			{
                if (element.Parent == this || element == null)
                {
                    break;
                }
				element = element.Parent ;
			}//while
            if (element != null && element.Parent == this)
            {
                return element;
            }
            else
            {
                return null;
            }
		}

        internal DomParagraphElement CreateMergedParagrahp( bool includeSelectionOnly )
        {
            DomParagraphElement p = new DomParagraphElement();
            p.OwnerDocument = this.OwnerDocument;
            p.StyleIndex = this.StyleIndex;
            DomStringElement myStr = new DomStringElement();
            myStr.OwnerDocument = this.OwnerDocument;
            //p.Elements.AddRaw( myStr );
            foreach (DomElement element in this.Elements)
            {
                if (includeSelectionOnly)
                {
                    if (this.OwnerDocument.IsSelected(element) == false)
                    {
                        continue;
                    }
                }
                if (element is DomCharElement)
                {
                    DomCharElement c = (DomCharElement)element;
                    if (myStr.CanMerge(c))
                    {
                        myStr.Merge(c);
                    }
                    else
                    {
                        if (myStr.HasContent)
                        {
                            p.Elements.AddRaw(myStr);
                            myStr = new DomStringElement();
                        }
                        myStr.OwnerDocument = this.OwnerDocument;
                        myStr.Merge(c);
                    }
                }
                else
                {
                    if (myStr.HasContent)
                    {
                        p.Elements.AddRaw(myStr);
                        myStr = new DomStringElement();
                    }
                    if ( ! (element is DomParagraphFlagElement))
                    {
                        p.Elements.AddRaw(element);
                    }
                }
            }
            if (myStr.HasContent)
            {
                p.Elements.AddRaw(myStr);
            }
            if (p.Elements.EOFElement != null)
            {
                p.Elements.EOFElement = null;
            }
            return p;
        }

        //public override void AfterLoad(DocumentFormatStyle format)
        //{
        //    base.AfterLoad(format);
        //    if (format == DocumentFormatStyle.XML)
        //    {
        //        WriterUtils.SplitElements(this.Elements);
        //    }
        //}
//
//		/// <summary>
//		/// 删除子元素操作
//		/// </summary>
//		/// <param name="element">要删除的子元素</param>
//		/// <returns>是否删除元素</returns>
//		public override bool RemoveChild(XTextElement element)
//		{
//			if( element == this.myEOFElement )
//			{
//				if( this.NextElement is XTextParagraph )
//				{
//					XTextParagraph p = ( XTextParagraph ) this.NextElement ;
//					int pindex = myParent.Elements.IndexOf( p );
//					XTextElementList list = new XTextElementList();
//					list.AddRange( p.Elements );
//					list.RemoveAt( list.Count - 1 );
//					int index = myElements.Count - 1 ;
//					myElements.AddRange( list );
//					myParent.RemoveChild( p );
//					if( myOwnerDocument.CanLogUndo )
//					{
//						myOwnerDocument.UndoList.AddRemoveElement( myParent , pindex , p );
//						myOwnerDocument.UndoList.AddInsertElements( this , index , list );
//					}
//					myOwnerDocument.bolModifyParagraph = true ;
//					return true ;
//				}
//				return false;
//			}
//			myElements.Remove( element );
//			return true ;
//		}
 

        /// <summary>
        /// 输出RTF文档
        /// </summary>
        /// <param name="writer">RTF文档书写器</param>
        public override void WriteRTF(DCSoft.CSharpWriter.RTF.RTFContentWriter writer)
        {
            writer.WriteStartParagraph(this.RuntimeStyle);
             
            foreach (DomElement element in this.Elements)
            {
                element.WriteRTF(writer);
            }
            writer.WriteEndParagraph();
        }
	}

}