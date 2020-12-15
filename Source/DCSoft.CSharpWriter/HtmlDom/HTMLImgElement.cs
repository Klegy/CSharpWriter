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
	/// 图片对象(Embeds an image or a video clip in the document. )
	/// </summary>
	public class HTMLImgElement : HTMLElement
	{
		/// <summary>
		/// 对象标签名称,返回"img"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstTagName.Img ;}
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
		/// Sets or retrieves a text alternative to the graphic. 
		/// </summary>
		public string Alt
		{
			get{ return GetAttribute( StringConstAttributeName.Alt );}
			set{ SetAttribute( StringConstAttributeName.Alt , value);}
		}
		/// <summary>
		/// 图片和文本的对齐方式,可以为 absbottom,absmiddle,baseline,bottom,left,middle,right,texttop,top.默认为 left
		/// </summary>
		public string Align
		{
			get{ return GetAttribute( StringConstAttributeName.Align );}
			set{ SetAttribute( StringConstAttributeName.Align , value);}
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
		/// 宽度
		/// </summary>
		public string Width
		{
			get{ return GetAttribute( StringConstAttributeName.Width );}
			set{ SetAttribute( StringConstAttributeName.Width , value);}
		}
		/// <summary>
		/// 高度
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
		/// Sets or retrieves the number of times a sound or video clip will loop when activated. 
		/// </summary>
		public string Loop
		{
			get{ return GetAttribute( StringConstAttributeName.Loop );}
			set{ SetAttribute( StringConstAttributeName.Loop , value);}
		}
		/// <summary>
		/// Sets or retrieves a lower resolution image to display. 
		/// </summary>
		public string LowSrc
		{
			get{ return GetAttribute( StringConstAttributeName.LowSrc );}
			set{ SetAttribute( StringConstAttributeName.LowSrc , value);}
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
			set{ SetAttribute( StringConstAttributeName.Title ,value);}
		}
		/// <summary>
		/// Sets or retrieves the vertical margin for the object. 
		/// </summary>
		public string VSpace
		{
			get{ return GetAttribute( StringConstAttributeName.VSpace );}
			set{ SetAttribute( StringConstAttributeName.VSpace , value);}
		}
	}//public class HTMLImgElement : HTMLElement
}