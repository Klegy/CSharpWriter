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
	/// 表单基本元素
	/// </summary>
	public class HTMLInputElement : HTMLElement , IHTMLFormElement
	{
		/// <summary>
		/// 对象标签名称,返回"input"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstTagName.Input ; }
		}
		/// <summary>
		/// 本基本元素所属表单区域对象
		/// </summary>
		protected HTMLFormElement myForm = null;
		/// <summary>
		/// 本基本元素所属表单区域对象
		/// </summary>
		public HTMLFormElement Form
		{
			get{ return myForm ;}
			set{ myForm = value;}
		}
		/// <summary>
		/// 类型 可以为 button,checkbox,file,hidden,image,password,radio,reset,submit,text
		/// </summary>
		public virtual string Type
		{
			get{ return this.GetAttribute(StringConstAttributeName.Type );}
			set{ SetAttribute( StringConstAttributeName.Type , value);}
		}
		/// <summary>
		/// 表单域值
		/// </summary>
		public override string Value
		{
			get{ return GetAttribute( StringConstAttributeName.Value );  }
			set{ SetAttribute( StringConstAttributeName.Value , value ); }
		}

		/// <summary>
		/// 对象是否可用
		/// </summary>
		public bool Disabled
		{
			get{ return GetBoolAttribute( StringConstAttributeName.Disabled );}
			set{ SetBoolAttribute( StringConstAttributeName.Disabled , value);}
		}

		/// <summary>
		/// 对象内部的纯文本
		/// </summary>
		public override string InnerText
		{
			get
			{
				string vType = this.Type ;
				if( vType == StringConstInputType.Text )
					return this.Value ;
				else
					return null;
			}
		}
		/// <summary>
		/// 对象大小
		/// </summary>
		public string Size
		{
			get{ return GetAttribute( StringConstAttributeName.Size );}
			set{ SetAttribute( StringConstAttributeName.Size , value );}
		}
		/// <summary>
		/// 对象名称
		/// </summary>
		public string Name
		{
			get{ return GetAttribute(StringConstAttributeName.Name );}
			set{ SetAttribute( StringConstAttributeName.Name , value);}
		}

		
		/// <summary>
		/// 对于 image 类型,表示其图片来源
		/// </summary>
		public string Src
		{
			get{ return GetAttribute( StringConstAttributeName.Src );}
			set{ SetAttribute( StringConstAttributeName.Src , value);}
		}
		/// <summary>
		/// 对checkbox,radio,对象是否选中
		/// </summary>
		public bool Checked
		{
			get{ return GetBoolAttribute( StringConstAttributeName.Checked );}
			set{ SetBoolAttribute( StringConstAttributeName.Checked , value);}
		}

		/// <summary>
		/// 输出对象数据到一个XML书写器中
		/// </summary>
		/// <remarks >
		/// 当设置了OwnerDocument.WriteOptions.InputOutputXSL则进行XSLT代码输出
		/// 对于text,hidden类型输出
		/// ＜xsl:attribute name="value"＞＜xsl:value-of select="对象名称"/＞ ＜/xsl:attribute＞
		/// 对于 radio,checkbox类型输出
		/// ＜xsl:if test="对象名称='value值'＞＜xsl:attribute name="checked"＞1＜/xsl:attribute＞＜/xsl:if＞
		/// </remarks>
		/// <param name="myWriter">XML书写器</param>
		/// <returns>操作是否成功</returns>
		public override bool Write(System.Xml.XmlWriter myWriter)
		{
			string vType = this.Type ;
			if( vType != StringConstInputType.CheckBox && vType != StringConstInputType.Radio )
				this.RemoveAttribute( StringConstAttributeName.Checked ) ;
			if( vType != StringConstInputType.Image )
				this.RemoveAttribute( StringConstAttributeName.Src );
			if( myOwnerDocument.WriteOptions.InputOutputXSL )
			{
				if( vType == StringConstInputType.Text || vType == StringConstInputType.Hidden )
					this.RemoveAttribute( StringConstAttributeName.Value );
				else if( vType == StringConstInputType.Radio || vType == StringConstInputType.CheckBox)
					this.RemoveAttribute( StringConstAttributeName.Checked );
			}
			return base.Write (myWriter);
		}
		/// <summary>
		/// 输出对象数据到XML书写器中
		/// </summary>
		/// <param name="myWriter">XML书写器</param>
		/// <returns>操作是否成功</returns>
		protected override bool InnerWrite(System.Xml.XmlWriter myWriter)
		{
			if( myOwnerDocument.WriteOptions.InputOutputXSL )
			{
				string vType = this.Type ;
				if( vType == StringConstInputType.Text || vType == StringConstInputType.Hidden )
				{
					myWriter.WriteStartElement( StringConstXSLT.Attribute );
					myWriter.WriteAttributeString( StringConstXSLT.Name , StringConstAttributeName.Value );
					myWriter.WriteStartElement( StringConstXSLT.Value_of );
					myWriter.WriteAttributeString( StringConstXSLT.Select , this.Name );
					myWriter.WriteEndElement();
					myWriter.WriteEndElement();
				}
				else if( vType == StringConstInputType.Radio || vType == StringConstInputType.CheckBox)
				{
					myWriter.WriteStartElement( StringConstXSLT.IF );
					myWriter.WriteAttributeString( StringConstXSLT.Test , this.Name + "='" + this.Value + "'");
					myWriter.WriteStartElement( StringConstXSLT.Attribute );
					myWriter.WriteAttributeString( StringConstXSLT.Name , StringConstAttributeName.Checked );
					myWriter.WriteString("1");
					myWriter.WriteEndElement();
					myWriter.WriteEndElement();
				}
			}
			return true;
		}

	}//public class InputElement : HTMLElement
}