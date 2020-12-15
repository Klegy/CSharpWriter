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
	/// 对象参数对象,父标签只能为"applet"或"object"
	/// </summary>
	public class HTMLParamElement : HTMLElement
	{
		/// <summary>
		/// 对象标签名称,返回"param"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstTagName.Param ; }
		}
		/// <summary>
		/// 参数名称
		/// </summary>
		public string Name
		{
			get{ return GetAttribute( StringConstAttributeName.Name );}
			set{ SetAttribute( StringConstAttributeName.Name , value);}
		}
		/// <summary>
		/// 参数数值
		/// </summary>
		public override string Value
		{
			get{ return GetAttribute( StringConstAttributeName.Value );}
			set{ SetAttribute( StringConstAttributeName.Value , value);}
		}
	}//public class HTMLParamElement : HTMLElement
}