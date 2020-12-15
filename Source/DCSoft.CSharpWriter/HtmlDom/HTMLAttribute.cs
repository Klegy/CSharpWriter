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
	/// HTML元素属性对象
	/// </summary>
	public class HTMLAttribute
	{
		/// <summary>
		/// 属性名称
		/// </summary>
		public string Name ;
		/// <summary>
		/// 属性值
		/// </summary>
		public string Value ;
		/// <summary>
		/// 返回除去换行回车字符的属性值
		/// </summary>
		public string FixValue
		{
			get
			{
				if( Value != null && Value.IndexOf("\r\n") >= 0 )
					return Value.Replace("\r\n" , "");
				else
					return Value ;
			}
		}
		/// <summary>
		/// 本属性值是否表示为XSL类型(即又前缀 "xsl:" )
		/// </summary>
		public bool IsXSLAttribute
		{
			get
			{
				return Value != null && Value.StartsWith("xsl:");
			}
		}
		/// <summary>
		/// 若本属性值表示XSL类型,则返回XSL值的名称(即前缀 "xsl:" 后面的部分)
		/// </summary>
		public string XSLSource
		{
			get
			{
				if( Value != null && Value.StartsWith("xsl:"))
				{
					string strValue = Value.Substring( 4 );
					if( strValue.IndexOf("\r\n") >= 0 )
						return strValue.Replace("\r\n","");
					else
						return strValue ;
				}
				else
					return null;
			}
		}
	}//public class HTMLAttribute
}