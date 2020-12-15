/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using DCSoft.CSharpWriter.Html;

namespace DCSoft.CSharpWriter.Dom
{
	/// <summary>
	/// 段落列表对象
	/// </summary>
	public class DomParagraphList : DomContainerElement
	{
		/// <summary>
		/// 初始化对象
		/// </summary>
		public DomParagraphList()
		{
		}

		/// <summary>
		/// 段落列表样式
		/// </summary>
		protected ParagraphListStyle intListStyle = ParagraphListStyle.None ;
		/// <summary>
		/// 段落列表样式
		/// </summary>
		public ParagraphListStyle ListStyle
		{
			get{ return intListStyle ;}
			set{ intListStyle = value;}
		}

		public override void WriteHTML(WriterHtmlDocumentWriter writer)
		{
			if( intListStyle == ParagraphListStyle.BulletedList )
			{
				writer.WriteStartElement("ul");
			}
			else
			{
				writer.WriteStartElement("ol");
			}
			DomElementList list = WriterUtils.MergeElements( this.Elements , false );
			if( list != null && list.Count > 0 )
			{
				foreach( DomElement element in list )
				{
					if( writer.IncludeSelectionOndly == false
                        || element.HasSelection )
					{
						element.WriteHTML( writer );
					}
				}
			}
			writer.WriteEndElement();
		}
	}
}
