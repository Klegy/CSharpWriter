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
	/// 超链接对象
	/// </summary>
	public class HTMLAElement : HTMLContainer 
	{
		/// <summary>
		/// 对象标签名称,返回"a"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstTagName.A ;}
		}
		/// <summary>
		/// 对象是否可用
		/// </summary>
		public bool Disabled
		{
			get{ return HasAttribute( StringConstAttributeName.Disabled );}
			set
			{
				if( value)
					SetAttribute( StringConstAttributeName.Disabled , "1");
				else
					RemoveAttribute( StringConstAttributeName.Disabled );
			}
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
		/// Sets or retrieves the list of HTTP methods supported by the object.
		/// </summary>
		public string Methods
		{
			get{ return GetAttribute( StringConstAttributeName.Methods );}
			set{ SetAttribute( StringConstAttributeName.Methods , value);}
		}
		/// <summary>
		/// 书签名称
		/// </summary>
		public string Name
		{
			get{ return GetAttribute( StringConstAttributeName.Name );}
			set{ SetAttribute( StringConstAttributeName.Name , value);}
		}
		/// <summary>
		/// 目标框架,可以为 _blank , _media , _parent , _search , _self , _top 或框架名称,默认 _self
		/// </summary>
		public string Target
		{
			get{ return GetAttribute( StringConstAttributeName.Target) ;}
			set{ SetAttribute( StringConstAttributeName.Target , value);}
		}
		/// <summary>
		/// Sets or retrieves advisory information (a ToolTip) for the object. 
		/// </summary>
		public string Title
		{
			get{ return GetAttribute( StringConstAttributeName.Title );}
			set{ SetAttribute( StringConstAttributeName.Title ,value);}
		}
		/// <summary>
		/// Sets or retrieves a URN for a target document. 
		/// </summary>
		public string Urn
		{
			get{ return GetAttribute( StringConstAttributeName.Urn );}
			set{ SetAttribute( StringConstAttributeName.Urn , value);}
		}
		/// <summary>
		/// Sets or retrieves the MIME type of the object.
		/// </summary>
		public string Type
		{
			get{ return GetAttribute( StringConstAttributeName.Type );}
			set{ SetAttribute( StringConstAttributeName.Type , value);}
		}
		/// <summary>
		/// Sets or retrieves the shape of the object.
		/// </summary>
		public string Shape
		{
			get{ return GetAttribute( StringConstAttributeName.Shape );}
			set{ SetAttribute( StringConstAttributeName.Shape , value);}
		}
	}//public class HTMLAElement : HTMLContainer 
}