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
	/// 标签对象
	/// </summary>
	public class HTMLLabelElement : HTMLContainer
	{
		/// <summary>
		/// 对象标签名称,返回"label"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstTagName.Label ;}
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
		/// Sets or retrieves the object to which the given label object is assigned. 
		/// </summary>
		public string HTMLFor
		{
			get{ return GetAttribute( StringConstAttributeName.HTMLFor );}
			set{ SetAttribute( StringConstAttributeName.HTMLFor , value);}
		}
		/// <summary>
		/// Sets or retrieves advisory information (a ToolTip) for the object. 
		/// </summary>
		public string Title
		{
			get{ return GetAttribute( StringConstAttributeName.Title );}
			set{ SetAttribute( StringConstAttributeName.Title , value);}
		}
	}//public class HTMLLabelElement : HTMLContainer
}