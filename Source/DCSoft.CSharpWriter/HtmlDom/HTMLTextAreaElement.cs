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
	/// 文本区域对象
	/// </summary>
	public class HTMLTextAreaElement : HTMLElement, IHTMLFormElement
	{
		/// <summary>
		/// 对象标签名称,返回"textarea"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstTagName.TextArea ; }
		}
		private HTMLFormElement myForm = null;
		/// <summary>
		/// 本基本元素所属表单区域对象
		/// </summary>
		public HTMLFormElement Form
		{
			get{ return myForm ;}
			set{ myForm = value;}
		}

		private string strText = null;
		/// <summary>
		/// 文本内容
		/// </summary>
		public override string InnerText
		{
			get
			{
				return strText ;
			}
		}
		/// <summary>
		/// 读写名称
		/// </summary>
		public string Name
		{
			get{ return GetAttribute( StringConstAttributeName.Name );}
			set{ SetAttribute( StringConstAttributeName.Name ,value);}
		}
		/// <summary>
		/// 行数
		/// </summary>
		public string Rows
		{
			get{ return GetAttribute( StringConstAttributeName.Rows );}
			set{ SetAttribute( StringConstAttributeName.Rows , value);}
		}
		/// <summary>
		/// 列数
		/// </summary>
		public string Cols
		{
			get{ return GetAttribute( StringConstAttributeName.Cols );}
			set{ SetAttribute( StringConstAttributeName.Cols , value);}
		}
		/// <summary>
		/// 是否只读
		/// </summary>
		public bool Readonly
		{
			get{ return HasAttribute( StringConstAttributeName.Readonly );}
			set
			{
				if( value)
					SetAttribute( StringConstAttributeName.Readonly , "1");
				else
					RemoveAttribute( StringConstAttributeName.Readonly );
			}
		}
		/// <summary>
		/// 是否不可用
		/// </summary>
		public bool Disabled
		{
			get{ return HasAttribute( StringConstAttributeName.Disabled );}
			set
			{
				if( value)
					SetAttribute( StringConstAttributeName.Disabled  , "1");
				else
					RemoveAttribute( StringConstAttributeName.Disabled );
			}
		}
		/// <summary>
		/// 对象数值
		/// </summary>
		public override string Value
		{
			get{ return strText ; }
			set{ strText = value; }
		}
		/// <summary>
		/// 对象文本
		/// </summary>
		public override string Text
		{
			get{ return strText; }
			set{ strText = value;}
		}
		/// <summary>
		/// 内部方法
		/// </summary>
		protected override bool HasText
		{
			get{ return true; }
		}
		internal override bool InnerRead(HTMLTextReader myReader)
		{
			strText = myReader.ReadToEndTag( this.TagName );
			return true;
		}
		/// <summary>
		/// 内部的输出对象数据到XML书写器
		/// </summary>
		/// <param name="myWriter">XML书写器</param>
		/// <returns>操作是否成功</returns>
		protected override bool InnerWrite(System.Xml.XmlWriter myWriter)
		{
			if( myOwnerDocument.WriteOptions.TextAreaOutputXSL )
			{
				myWriter.WriteStartElement( StringConstXSLT.Value_of );
				myWriter.WriteAttributeString( StringConstXSLT.Value_of , this.Name );
				myWriter.WriteEndElement();
			}
			else
			{
				myWriter.WriteString( HTMLTextReader.isBlankString( strText ) ? " " : strText );
			}
			return true;
		}
	}
}