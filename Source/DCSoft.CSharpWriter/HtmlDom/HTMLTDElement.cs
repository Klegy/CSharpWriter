/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System ;
namespace DCSoft.HtmlDom
{
	/// <summary>
	/// 表格行对象
	/// </summary>
	public class HTMLTDElement : HTMLContainer
	{
		/// <summary>
		/// 对象标签名称,返回"td"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstTagName.Td ; }
		}
		/// <summary>
		/// XSL数据源名称
		/// </summary>
		public string XSLSource
		{
			get{ return GetAttribute( StringConstAttributeName.XSLSource );}
			set{ SetAttribute( StringConstAttributeName.XSLSource , value);}
		}
		/// <summary>
		/// 对齐方式,可以为 center ,justify, left, right ,默认 left
		/// </summary>
		public string Align
		{
			get{ return GetAttribute( StringConstAttributeName.Align );}
			set{ SetAttribute( StringConstAttributeName.Align , value);}
		}
		/// <summary>
		/// Sets or retrieves the background picture tiled behind the text and graphics in the object. 
		/// </summary>
		public string BackGround
		{
			get{ return GetAttribute( StringConstAttributeName.BackGround );}
			set{ SetAttribute( StringConstAttributeName.BackGround , value);}
		}
		/// <summary>
		/// Deprecated. Sets or retrieves the background color behind the object. 
		/// </summary>
		public string BgColor
		{
			get{ return GetAttribute( StringConstAttributeName.BgColor );}
			set{ SetAttribute( StringConstAttributeName.BgColor , value);}
		}
		/// <summary>
		/// Sets or retrieves the border color of the object.
		/// </summary>
		public string BorderColor
		{
			get{ return GetAttribute( StringConstAttributeName.BorderColor );}
			set{ SetAttribute( StringConstAttributeName.BorderColor , value);}
		}
		/// <summary>
		/// Sets or retrieves the number columns in the table that the object should span. 
		/// </summary>
		public string ColSpan
		{
			get{ return GetAttribute( StringConstAttributeName.ColSpan );}
			set{ SetAttribute( StringConstAttributeName.ColSpan , value);}
		}
		/// <summary>
		/// Sets or retrieves whether the browser automatically performs wordwrap.
		/// </summary>
		public string NoWrap
		{
			get{ return GetAttribute( StringConstAttributeName.NoWrap );}
			set{ SetAttribute( StringConstAttributeName.NoWrap , value);}
		}
		/// <summary>
		/// Sets or retrieves how many rows in a table the cell should span. 
		/// </summary>
		public string RowSpan
		{
			get{ return GetAttribute( StringConstAttributeName.RowSpan );}
			set{ SetAttribute( StringConstAttributeName.RowSpan , value);}
		}
		/// <summary>
		/// Sets or retrieves the height of the object. 
		/// </summary>
		public string Height
		{
			get{ return GetAttribute( StringConstAttributeName.Height );}
			set{ SetAttribute( StringConstAttributeName.Height , value);}
		}
		/// <summary>
		/// Sets or retrieves how text and other content are vertically aligned within the object that contains them. 
		/// </summary>
		public string VAlign
		{
			get{ return GetAttribute( StringConstAttributeName.VAlign );}
			set{ SetAttribute( StringConstAttributeName.VAlign , value);}
		}
		/// <summary>
		/// Sets or retrieves the width of the object.
		/// </summary>
		public string Width
		{
			get{ return GetAttribute( StringConstAttributeName.Width );}
			set{ SetAttribute( StringConstAttributeName.Width ,value);}
		}

		/// <summary>
		/// 向XML书写器输出对象数据
		/// </summary>
		/// <remarks>若设置了OwnerDocument.WriteOptions.TDOutputXSL且 xslsource 属性不为空
		/// 则在 td 标签外还套上[xsl:for-each select="xslsource属性值"] 标签
		/// </remarks>
		/// <param name="myWriter">XML书写器</param>
		/// <returns>操作是否成功</returns>
		public override bool Write(System.Xml.XmlWriter myWriter)
		{
			if( myOwnerDocument.WriteOptions.TDOutputXSL )
			{
				string vSource = this.XSLSource ;
				if( vSource != null && vSource.Length > 0 )
				{
					myWriter.WriteStartElement( StringConstXSLT.For_each);
					myWriter.WriteAttributeString( StringConstXSLT.Select , myOwnerDocument.WriteOptions.FixXSLSource( vSource ));
					bool bolResult = base.Write( myWriter );
					myWriter.WriteEndElement();
					return bolResult ;
				}
				else
					return base.Write( myWriter );
			}
			else
				return base.Write( myWriter );
		}

		/// <summary>
		/// 遇到结束标签时的处理
		/// </summary>
		/// <param name="myReader"></param>
		/// <param name="vTagName"></param>
		/// <returns></returns>
		internal override bool MeetEndTag(HTMLTextReader myReader, string vTagName)
		{
			if( vTagName == StringConstTagName.Tr 
				|| vTagName == StringConstTagName.Table
				|| vTagName == StringConstTagName.TBody )
			{
				myReader.MovePreTo('<');
				return true;
			}
			return base.MeetEndTag( myReader , vTagName );
		}
	}//public class HTMLTDElement : HTMLContainer
}