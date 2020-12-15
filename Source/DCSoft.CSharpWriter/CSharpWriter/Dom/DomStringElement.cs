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
using DCSoft.RTF;
using System.Xml.Serialization;
using System.ComponentModel ;

namespace DCSoft.CSharpWriter.Dom
{
	/// <summary>
	/// 文档字符串对象,这是XWriterLib内部使用
	/// </summary>
    /// <remarks>本对象只是在加载或保存文档时临时生成。</remarks>
    [System.Xml.Serialization.XmlType("XString")]
    public class DomStringElement : DomContainerElement
	{
		/// <summary>
		/// 初始化对象
		/// </summary>
		public DomStringElement()
		{
            base.Attributes = null;
		}

		private System.Text.StringBuilder myText 
            = new System.Text.StringBuilder();

        /// <summary>
        /// 对象文本
        /// </summary>
        public override string Text
        {
            get
            {
                return myText.ToString();
            }
            set
            {
                myText = new System.Text.StringBuilder(value);
                this.Elements.Clear();
            }
        }

        [Browsable( false )]
        [XmlIgnore()]
        public override DomElementList Elements
        {
            get
            {
                return base.Elements;
            }
            set
            {
                base.Elements = value;
            }
        }

        /// <summary>
        /// 无效属性
        /// </summary>
        [System.Xml.Serialization.XmlIgnore()]
        [System.ComponentModel.Browsable( false )]
        [System.ComponentModel.DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden )]
        public override DomElementList ElementsForSerialize
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        /// <summary>
        /// 对象是否有内容
        /// </summary>
        [Browsable( false )]
        public bool HasContent
        {
            get
            {
                return myText.Length > 0 || this.Elements.Count > 0 ;
            }
        }

		public string GetOutputText( bool includeSelectionOnly )
		{
            if (this.Elements.Count == 0)
            {
                return "";
            }
			string txt = "";
			if( includeSelectionOnly == false || this.Elements.Count == 0 )
			{
				txt = this.Text ;
			}
			else 
			{
				System.Text.StringBuilder myStr = new System.Text.StringBuilder();
                DomDocumentContentElement dce = this.Elements[0].DocumentContentElement;
                foreach( DomCharElement c in this.Elements )
				{
                    if ( dce.IsSelected(c))
                    {
                        myStr.Append(c.CharValue);
                    }
				}//foreach
				txt = myStr.ToString();
			}
			return txt ;
		}

		/// <summary>
		/// 输出对象数据到HTML文档
		/// </summary>
		/// <param name="writer">HTML文档书写器</param>
        public override void WriteHTML(DCSoft.CSharpWriter.Html.WriterHtmlDocumentWriter  writer)
		{
            DocumentContentStyle rs = this.RuntimeStyle;
            if ( rs.Superscript )
            {
                writer.WriteStartElement("sup");
            }
            else if ( rs.Subscript )
            {
                writer.WriteStartElement("sub");
            }
            string link = rs.Link;
            if (link != null && link.Trim().Length > 0)
            {
                writer.WriteStartElement("a");
                writer.WriteAttributeString("href", link);
            }
            else
            {
                writer.WriteStartElement("span");
            }
            writer.WriteStartStyle ();
            writer.WriteDocumentContentStyle(this.RuntimeStyle  , this );
            writer.WriteEndStyle();
             
			writer.WriteText(
                this.GetOutputText( writer.IncludeSelectionOndly ));

            writer.WriteEndElement();
            if (rs.Superscript || rs.Subscript )
            {
                writer.WriteEndElement();
            }
		}

		/// <summary>
		/// 判断对象能否合并一个字符元素对象
		/// </summary>
		/// <param name="c">字符元素对象</param>
		/// <returns>能否合并</returns>
		internal bool CanMerge( DomCharElement c )
		{
			if( c != null )
			{
                if (myText.Length == 0)
                {
                    return true;
                }
                return this.StyleIndex == c.StyleIndex;
			}
			return false;
		}

		/// <summary>
		/// 合并一个字符元素对象
		/// </summary>
		/// <param name="c">字符元素对象</param>
		internal void Merge( DomCharElement c )
		{
			if( myText.Length == 0 )
			{
                this.StyleIndex = c.StyleIndex;
			}
            this.OwnerDocument = c.OwnerDocument;
			myText.Append( c.ToString() );
			this.Elements.AddRaw( c );
		}

		/// <summary>
		/// 将对象拆分成若干个字符元素对象
		/// </summary>
		/// <returns>拆分所得的字符元素对象列表</returns>
		internal DomElementList SplitChars()
		{
			DomElementList list = new DomElementList();
			string txt = myText.ToString();
			foreach( char c in txt )
			{
                DomCharElement NewChar = this.OwnerDocument.CreateChar( c , this.StyleIndex);
				if( NewChar != null )
				{
                    NewChar.StyleIndex = this.StyleIndex;
					NewChar.Parent = this.Parent ;
					list.Add( NewChar );
				}
			}
			return list ;
		}

        public override void WriteRTF(DCSoft.CSharpWriter.RTF.RTFContentWriter writer)
        {
            writer.WriteStartString(
                this.GetOutputText(writer.IncludeSelectionOnly),
                this.RuntimeStyle);
            writer.WriteEndString();
		}

        public override string ToString()
        {
            if (this.myText.Length > 20)
            {
                return myText.ToString(0, 20);
            }
            else
            {
                return myText.ToString();
            }
        }

        ///// <summary>
        ///// 表示对象内容的纯文本数据
        ///// </summary>
        //[System.ComponentModel.Browsable(false)]
        //[System.Xml.Serialization.XmlIgnore()]
        //[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        //public override string Text
        //{
        //    get
        //    {
        //        return myText.ToString();
        //    }
        //    set
        //    {
        //    }
        //}

	}//public class XTextString : XTextElement
}