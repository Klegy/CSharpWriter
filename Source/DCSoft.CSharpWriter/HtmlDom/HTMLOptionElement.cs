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
	/// 下拉列表的列表项目
	/// </summary>
	public class HTMLOptionElement : HTMLElement
	{
		/// <summary>
		/// 对象标签名称,返回"option"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstTagName.Option ; }
		}
	
		private string strText ;
		/// <summary>
		/// 内部使用的方法
		/// </summary>
		protected override bool HasText
		{
			get{ return true;}
		}
		/// <summary>
		/// 对象文本
		/// </summary>
		public override string Text
		{
			get{ return strText == null ? "" : strText ;}
			set{ strText = value;}
		}
		/// <summary>
		/// 对象数值
		/// </summary>
		public override string Value
		{
			get{ return GetAttribute(StringConstAttributeName.Value );}
			set{ SetAttribute( StringConstAttributeName.Value , value);}
		}
		/// <summary>
		/// 对象是否被选中
		/// </summary>
		public bool Selected
		{
			get{ return HasAttribute( StringConstAttributeName.Selected );}
			set{ SetBoolAttribute( StringConstAttributeName.Selected , value);}
		}
		/// <summary>
		/// 输出对象数据到XML书写器
		/// </summary>
		/// <remarks>
		/// 若设置了OwnerDocument.WriteOptions.OptionOutputXSL则输出
		/// ＜option value="数值" ＞
		///		＜xsl:if test="下拉列表名称='对象数值'"＞
		///			＜xsl:attribute name="selected"＞1＜/xsl:attribute＞
		///		＜/xsl:if＞
		///		＜xsl:text＞对象文本＜/xsl:text＞
		/// ＜/option＞
		/// </remarks>
		/// <param name="myWriter">XML书写器</param>
		/// <returns>操作是否成功</returns>
		public override bool Write(System.Xml.XmlWriter myWriter)
		{
			if( myOwnerDocument.WriteOptions.OptionOutputXSL )
			{
				HTMLSelectElement select = ( HTMLSelectElement ) myParent ;
				this.RemoveAttribute( StringConstAttributeName.Selected );
				myWriter.WriteStartElement( this.TagName );
				this.WriteAllAttributes( myWriter );
				myWriter.WriteStartElement( StringConstXSLT.IF );
				myWriter.WriteAttributeString( StringConstXSLT.Test , select.Name + "='" + this.Value + "'");
				myWriter.WriteStartElement( StringConstXSLT.Attribute );
				myWriter.WriteAttributeString( StringConstXSLT.Name , StringConstAttributeName.Selected );
				myWriter.WriteString("1");
				myWriter.WriteEndElement();
				myWriter.WriteEndElement();
				myWriter.WriteElementString ( StringConstXSLT.Text , this.strText );
				myWriter.WriteEndElement();
				return true;
			}
			else
				return base.Write (myWriter);
		}
	}//public class HTMLOptionElement : HTMLElement
}