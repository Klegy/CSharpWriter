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
	public class HTMLTRElement : HTMLContainer
	{
		/// <summary>
		/// 对象标签名称,返回"tr"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstTagName.Tr ; }
		}
		/// <summary>
		/// 表格行对象下属节点只能是 td 
		/// </summary>
		/// <param name="strName"></param>
		/// <returns></returns>
		internal override bool CheckChildTagName(string strName)
		{
			return strName == StringConstTagName.Td ;
		}
		/// <summary>
		/// XML数据源名称
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
		/// 纵向对齐方式,可以为middle,baseline,bottom,top,默认 middle
		/// </summary>
		public string VAlign
		{
			get{ return GetAttribute( StringConstAttributeName.VAlign );}
			set{ SetAttribute( StringConstAttributeName.VAlign , value);}
		}
		
		internal override bool MeetEndTag(HTMLTextReader myReader, string vTagName)
		{
			if( vTagName == StringConstTagName.Table 
				|| vTagName == StringConstTagName.TBody )
			{
				myReader.MovePreTo('<');
				return true;
			}
			return base.MeetEndTag( myReader , vTagName );
		}

		/// <summary>
		/// 输出对象数据到XML书写器中
		/// </summary>
		/// <remarks>
		/// 若设置了OwnerDocument.WriteOptions.TROutputXSL且存在 xslsource 属性则 td 标签外部
		/// 还套上[xsl:for-each select="xslsource属性值"]标签
		/// </remarks>
		/// <param name="myWriter">XML书写器</param>
		/// <returns>操作是否成功</returns>
		public override bool Write(System.Xml.XmlWriter myWriter)
		{
			if( myOwnerDocument.WriteOptions.TROutputXSL )
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
	}//public class HTMLTRElement : HTMLContainer
}