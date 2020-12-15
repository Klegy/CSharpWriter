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
	/// 列表元素
	/// </summary>
	public class HTMLSelectElement : HTMLContainer , IHTMLFormElement
	{
		/// <summary>
		/// 对象标签名称,返回"select"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstTagName.Select ; }
		}
		/// <summary>
		/// 对象所属表单域对象
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
		/// 列表项目集合
		/// </summary>
		public HTMLElementList Options
		{
			get{ return this.myChildNodes ;}
		}
		/// <summary>
		/// XSL数据源名
		/// </summary>
		public string XSLSource
		{
			get{ return GetAttribute( StringConstAttributeName.XSLSource );}
			set{ SetAttribute( StringConstAttributeName.XSLSource , value);}
		}
		/// <summary>
		/// 列表项目的value属性的XSL数据源名
		/// </summary>
		public string ValueXSLSource
		{
			get{ return GetAttribute( StringConstAttributeName.ValueXSLSource);}
			set{ SetAttribute( StringConstAttributeName.ValueXSLSource , value);}
		}
		/// <summary>
		/// 列表项目的文本的XSL数据源名
		/// </summary>
		public string TextXSLSource
		{
			get{ return GetAttribute( StringConstAttributeName.TextXSLSource );}
			set{ SetAttribute( StringConstAttributeName.TextXSLSource , value);}
		}
		/// <summary>
		/// 对象名称
		/// </summary>
		public string Name
		{
			get{ return GetAttribute( StringConstAttributeName.Name );}
			set{ SetAttribute( StringConstAttributeName.Name , value);}
		}
		/// <summary>
		/// 对象是否不可用
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
		/// Sets or retrieves the Boolean value indicating whether multiple items can be selected from a list.
		/// </summary>
		public bool Multiple
		{
			get{ return HasAttribute( StringConstAttributeName.Multiple  );}
			set
			{
				if( value)
					SetAttribute( StringConstAttributeName.Multiple  , "1");
				else
					RemoveAttribute( StringConstAttributeName.Multiple );
			}
		}
		/// <summary>
		/// Sets or retrieves the number of rows in the list box. 
		/// </summary>
		public string Size
		{
			get{ return GetAttribute( StringConstAttributeName.Size );}
			set{ SetAttribute( StringConstAttributeName.Size , value);}
		}
		/// <summary>
		/// Sets or retrieves the index of the selected option in a select object. 
		/// </summary>
		public int SelectedIndex
		{
			get
			{
				foreach( HTMLOptionElement op in myChildNodes )
					if( op.Selected )
						return myChildNodes.IndexOf( op );
				return -1 ;
			}
			set
			{
				foreach( HTMLOptionElement op in myChildNodes)
					op.Selected = false;
				( ( HTMLOptionElement ) myChildNodes[ value]).Selected = true;
			}
		}
		/// <summary>
		/// Sets or retrieves the value which is returned to the server when the form control is submitted.
		/// </summary>
		public override string Value
		{
			get
			{
				foreach( HTMLOptionElement o in myChildNodes)
				{
					if( o.Selected )
						return o.Value ;
				}
				return null;
			}
			set
			{
				foreach( HTMLOptionElement o in myChildNodes)
				{
					o.Selected = ( o.Value == value );
				}
			}
		}

		/// <summary>
		/// 向XML书写器输出对象数据
		/// </summary>
		/// <remarks>若设置了OwnerDocument.WriteOptions.SelectOutputXSL
		/// 则输出XSLT代码</remarks>
		/// <param name="myWriter">XML书写器</param>
		/// <returns>操作是否成功</returns>
		public override bool Write(System.Xml.XmlWriter myWriter)
		{
			if( myOwnerDocument.WriteOptions.SelectOutputXSL )
			{
				string vSource = myOwnerDocument.WriteOptions.FixXSLSource( this.XSLSource );
				if( vSource != null && vSource.Length > 0 )
				{
					string vValueSource = myOwnerDocument.WriteOptions.FixXSLSource( this.ValueXSLSource );
					string vTextSource = myOwnerDocument.WriteOptions.FixXSLSource( this.TextXSLSource ) ;
					if( vValueSource == null || vValueSource.Length == 0 )
						vValueSource = "value";
					if( vTextSource == null || vTextSource.Length == 0 )
						vTextSource = "text";
					myWriter.WriteStartElement( this.TagName );							//<select >
					this.WriteAllAttributes( myWriter );
					myWriter.WriteStartElement( StringConstXSLT.Variable );				//	<xsl:variable>
					myWriter.WriteAttributeString( StringConstXSLT.Name , "selectvalue"); //		name="selectvalue"
					myWriter.WriteStartElement( StringConstXSLT.Value_of );				//		<xsl:value-of
					myWriter.WriteAttributeString( StringConstXSLT.Select , this.Name );	//			select="name"
					myWriter.WriteEndElement();											//		</xsl:value-of>
					myWriter.WriteEndElement();											//	</xsl:variable>
					myWriter.WriteStartElement( StringConstXSLT.For_each );				//	<xsl:for-each>
					myWriter.WriteAttributeString( StringConstXSLT.Select , vSource );	//			select="*"
					myWriter.WriteStartElement( StringConstTagName.Option ) ;					//		<option>
					myWriter.WriteStartElement( StringConstXSLT.Attribute );				//			<xsl:attribute>
					myWriter.WriteAttributeString( StringConstAttributeName.Name , StringConstAttributeName.Value );	//				name="value"
					myWriter.WriteStartElement( StringConstXSLT.Value_of );				//				<xsl:value-of 
					myWriter.WriteAttributeString( StringConstXSLT.Select , vValueSource );//					select="value"
					myWriter.WriteEndElement();											//				</xsl:value-of>
					myWriter.WriteEndElement();											//			</xsl:attribute>
					myWriter.WriteStartElement( StringConstXSLT.IF );						//			<xsl:if 
					myWriter.WriteAttributeString( StringConstXSLT.Test , "$selectvalue=" + vValueSource); // test="$selectvalue=value"
					myWriter.WriteStartElement( StringConstXSLT.Attribute );				//				<xsl:attribute>
					myWriter.WriteAttributeString( StringConstXSLT.Name , StringConstAttributeName.Selected );//				name="selected"
					myWriter.WriteString("1");
					myWriter.WriteEndElement();											//				</xsl:attribute>
					myWriter.WriteEndElement();											//			</xsl:if>
					myWriter.WriteStartElement( StringConstXSLT.Value_of );				//			<xsl:value-of>
					myWriter.WriteAttributeString( StringConstXSLT.Select , vTextSource );//				select="text"
					myWriter.WriteEndElement();											//			</xsl:value-of>
					myWriter.WriteEndElement();											//		</option>
					myWriter.WriteEndElement();											//	</xsl:for-each>
					myWriter.WriteEndElement();											//</select>
					return true;
				}
				else
					return base.Write( myWriter );
			}
			else
			{
				return base.Write( myWriter );
			}
		}

		/// <summary>
		/// Select 元素只能接收 Option 子元素
		/// </summary>
		/// <param name="strName"></param>
		/// <returns></returns>
		internal override bool CheckChildTagName(string strName)
		{
			return strName == StringConstTagName.Option ;
		}
	}//public class HTMLSelectElement : HTMLContainer , IHTMLFormElement
}