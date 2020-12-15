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
	/// 表单区域对象
	/// </summary>
	public class HTMLFormElement : HTMLContainer
	{
		private HTMLElementList myElements = new HTMLElementList();
		/// <summary>
		/// 内部所有的表单对象
		/// </summary>
		public HTMLElementList Elements
		{
			get{ return myElements ;}
		}
		/// <summary>
		/// 内部方法,添加表单域元素
		/// </summary>
		/// <param name="e"></param>
		internal void AppendElement( IHTMLFormElement e )
		{
			if( ! myElements.Contains( (HTMLElement) e ))
			{
				myElements.Add( ( HTMLElement ) e );
				e.Form = this ;
			}
		}
		/// <summary>
		/// 对象标签名称,返回"form"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstTagName.Form ;}
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
		/// 数据发送方式,可以为 GET 或 POST
		/// </summary>
		public string Method
		{
			get{ return GetAttribute( StringConstAttributeName.Method );}
			set{ SetAttribute( StringConstAttributeName.Method , value);}
		}
		/// <summary>
		/// Sets or retrieves the URL to which the form content is sent for processing.
		/// </summary>
		public string Action
		{
			get{ return GetAttribute( StringConstAttributeName.Action );}
			set{ SetAttribute( StringConstAttributeName.Action , value);}
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
		/// Sets or retrieves the Multipurpose Internet Mail Extensions (MIME) encoding for the form.
		/// </summary>
		public string Enctype
		{
			get{ return GetAttribute( StringConstAttributeName.Enctype );}
			set{ SetAttribute( StringConstAttributeName.Enctype , value);}
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
		/// Sets or retrieves advisory information (a ToolTip) for the object. 
		/// </summary>
		public string Title
		{
			get{ return GetAttribute( StringConstAttributeName.Title );}
			set{ SetAttribute( StringConstAttributeName.Title , value);}
		}
	}//public class HTMLFormElement : HTMLContainer
}