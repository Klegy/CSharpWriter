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
	/// Object对象
	/// </summary>
	public class HTMLObjectElement : HTMLContainer
	{
		/// <summary>
		/// 对象标签名称,返回"object"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstTagName.Object ; }
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
		/// Sets or retrieves the width of the border to draw around the object. 
		/// </summary>
		public string Border
		{
			get{ return GetAttribute( StringConstAttributeName.Border );}
			set{ SetAttribute( StringConstAttributeName.Border , value);}
		}
		/// <summary>
		/// Sets or retrieves the class identifier for the object.
		/// </summary>
		public string ClassID
		{
			get{ return GetAttribute( StringConstAttributeName.ClassID );}
			set{ SetAttribute( StringConstAttributeName.ClassID , value);}
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
		/// Sets or retrieves the Internet media type for the code associated with the object. 
		/// </summary>
		public string CodeType
		{
			get{ return GetAttribute( StringConstAttributeName.CodeType );}
			set{ SetAttribute( StringConstAttributeName.CodeType , value);}
		}
		/// <summary>
		/// Sets or retrieves the URL that references the data of the object.
		/// </summary>
		public string Data
		{
			get{ return GetAttribute( StringConstAttributeName.Data );}
			set{ SetAttribute( StringConstAttributeName.Data , value);}
		}
		/// <summary>
		/// Sets or retrieves the height of the object. 
		/// </summary>
		public string Height
		{
			get{ return GetAttribute( StringConstAttributeName.Height );}
			set{ SetAttribute( StringConstAttributeName.Height , value);}
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
		/// Sets or retrieves advisory information (a ToolTip) for the object. 
		/// </summary>
		public string Title
		{
			get{ return GetAttribute( StringConstAttributeName.Title );}
			set{ SetAttribute( StringConstAttributeName.Title , value);}
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
		/// Sets or retrieves the width of the object.
		/// </summary>
		public string Width
		{
			get{ return GetAttribute( StringConstAttributeName.Width );}
			set{ SetAttribute( StringConstAttributeName.Width ,value);}
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
		/// 检查子标签内容,子标签只能为 param
		/// </summary>
		/// <param name="strName"></param>
		/// <returns></returns>
		internal override bool CheckChildTagName(string strName)
		{
			return strName == StringConstTagName.Param ;
		}
	}//public class HTMLObjectElement : HTMLContainer
}