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
	/// 图片热点对象(Defines the shape, coordinates, and associated URL of one hyperlink region within a client-side image map.)
	/// </summary>
	public class HTMLAreaElement : HTMLElement
	{
		/// <summary>
		/// 标签名称,返回 "area"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstTagName.Area ; }
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
		/// Sets or retrieves the coordinates of the object.
		/// </summary>
		public string Coords
		{
			get{ return GetAttribute( StringConstAttributeName.Coords );}
			set{ SetAttribute( StringConstAttributeName.Coords , value);}
		}
		/// <summary>
		/// Sets or retrieves the destination URL or anchor point. 
		/// </summary>
		public string Href
		{
			get{ return GetAttribute(StringConstAttributeName.Href );}
			set{ SetAttribute( StringConstAttributeName.Href , value);}
		}
		/// <summary>
		/// Sets or retrieves the shape of the object.
		/// </summary>
		public string Shape
		{
			get{ return GetAttribute( StringConstAttributeName.Shape );}
			set{ SetAttribute( StringConstAttributeName.Shape , value);}
		}
		/// <summary>
		/// 目标框架,可以为 _blank , _media , _parent , _search , _self , _top 或框架名称,默认 _self
		/// </summary>
		public string Target
		{
			get{ return GetAttribute( StringConstAttributeName.Target );}
			set{ SetAttribute( StringConstAttributeName.Target , value);}
		}
		/// <summary>
		/// Sets or retrieves advisory information (a ToolTip) for the object. 
		/// </summary>
		public string Title
		{
			get{ return GetAttribute( StringConstAttributeName.Title );}
			set{ SetAttribute( StringConstAttributeName.Title , value);}
		}
	}//public class HTMLAreaElement : HTMLElement
}