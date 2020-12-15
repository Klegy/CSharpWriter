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
	/// 图片热点对象
	/// </summary>
	public class HTMLMapElement : HTMLContainer
	{
		/// <summary>
		/// 对象标签名称,返回"map"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstTagName.Map ; }
		}
		/// <summary>
		/// Sets or retrieves the name of the object.
		/// </summary>
		public string Name
		{
			get{ return GetAttribute( StringConstAttributeName.Name );}
			set{ SetAttribute( StringConstAttributeName.Name , value);}
		}
		/// <summary>
		/// Sets or retrieves advisory information (a ToolTip) for the object. 
		/// </summary>
		public string Title
		{
			get{ return GetAttribute( StringConstAttributeName.Title );}
			set{ SetAttribute( StringConstAttributeName.Title , value);}
		}
	}//public class HTMLMapElement : HTMLContainer
}