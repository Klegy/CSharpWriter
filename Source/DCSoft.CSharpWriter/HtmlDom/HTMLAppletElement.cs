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
	/// Applet 对象
	/// </summary>
	public class HTMLAppletElement : HTMLContainer
	{
		/// <summary>
		/// 对象标签名称, 返回"applet"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstTagName.Applet ; }
		}
		/// <summary>
		/// Sets or retrieves how the object is aligned with adjacent text. 
		/// </summary>
		public string Align
		{
			get{ return GetAttribute( StringConstAttributeName.Align );}
			set{ SetAttribute( StringConstAttributeName.Align , value);}
		}
		/// <summary>
		/// Sets or retrieves a text alternative to the graphic. 
		/// </summary>
		public string Alt
		{
			get{ return GetAttribute( StringConstAttributeName.Alt );}
			set{ SetAttribute( StringConstAttributeName.Alt , value);}
		}
		/// <summary>
		/// Sets or retrieves the URL of the file containing the compiled Java class.
		/// </summary>
		public string Code
		{
			get{ return GetAttribute( StringConstAttributeName.Code );}
			set{ SetAttribute( StringConstAttributeName.Code , value);}
		}
		/// <summary>
		/// Sets or retrieves the URL of the component.
		/// </summary>
		public string CodeBase
		{
			get{ return GetAttribute( StringConstAttributeName.CodeBase );}
			set{ SetAttribute( StringConstAttributeName.CodeBase , value);}
		}
		/// <summary>
		/// Sets or retrieves the horizontal margin for the object. 
		/// </summary>
		public string HSpace
		{
			get{ return GetAttribute( StringConstAttributeName.HSpace );}
			set{ SetAttribute( StringConstAttributeName.HSpace , value);}
		}
		/// <summary>
		/// Sets or retrieves the language in which the current script is written. 
		/// </summary>
		public string Language
		{
			get{ return GetAttribute( StringConstAttributeName.Language );}
			set{ SetAttribute( StringConstAttributeName.Language , value);}
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
		/// Sets or retrieves a URL to be loaded by the object. 
		/// </summary>
		public string Src
		{
			get{ return GetAttribute( StringConstAttributeName.Src );}
			set{ SetAttribute( StringConstAttributeName.Src , value);}
		}
		/// <summary>
		/// Sets or retrieves advisory information (a ToolTip) for the object. 
		/// </summary>
		public string Title
		{
			get{ return GetAttribute( StringConstAttributeName.Title );}
			set{ SetAttribute( StringConstAttributeName.Title , value);}
		}
		/// <summary>
		/// Sets or retrieves the vertical margin for the object. 
		/// </summary>
		public string VSpace
		{
			get{ return GetAttribute( StringConstAttributeName.VSpace );}
			set{ SetAttribute( StringConstAttributeName.VSpace , value);}
		}
		/// <summary>
		/// 内部方法,子元素只能是 param 类型
		/// </summary>
		/// <param name="strName">标签名称</param>
		/// <returns>对象是否可以包含标签</returns>
		internal override bool CheckChildTagName(string strName)
		{
			return strName == StringConstTagName.Param ;
		}
	}//public class HTMLAppletElement : HTMLContainer
}