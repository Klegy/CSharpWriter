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
	/// 连接文件元素
	/// </summary>
	public class HTMLLinkElement : HTMLElement
	{
		/// <summary>
		/// 对象标签名称,返回"link"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstTagName.Link ;}
		}
		/// <summary>
		/// Sets or retrieves the destination URL or anchor point. 
		/// </summary>
		public string Href
		{
			get{ return GetAttribute( StringConstAttributeName.Href );}
			set{ SetAttribute( StringConstAttributeName.Href , value);}
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
		/// Sets or retrieves the relationship between the object and the destination of the link.
		/// </summary>
		public string Rel
		{
			get{ return GetAttribute( StringConstAttributeName.Rel );}
			set{ SetAttribute( StringConstAttributeName.Rel , value);}
		}
		/// <summary>
		/// Sets or retrieves the relationship between the object and the destination of the link.
		/// </summary>
		public string Rev
		{
			get{ return GetAttribute( StringConstAttributeName.Rev );}
			set{ SetAttribute( StringConstAttributeName.Rev , value);}
		}
		/// <summary>
		/// Sets or retrieves the window or frame at which to target content. 
		/// </summary>
		public string Target
		{
			get{ return GetAttribute( StringConstAttributeName.Target );}
			set{ SetAttribute( StringConstAttributeName.Target , value);}
		}
		/// <summary>
		/// Sets or retrieves the MIME type of the object. 
		/// </summary>
		public string Type
		{
			get{ return GetAttribute( StringConstAttributeName.Type );}
			set{ SetAttribute( StringConstAttributeName.Type , value);}
		}
	}//public class HTMLLinkElement : HTMLElement
}