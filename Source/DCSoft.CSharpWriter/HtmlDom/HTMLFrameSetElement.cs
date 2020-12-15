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
	/// 框架集合对象(Specifies a frameset, which is used to organize multiple frames and nested framesets.)
	/// </summary>
	public class HTMLFrameSetElement : HTMLContainer
	{
		/// <summary>
		/// 对象标签名称,返回"frameset"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstTagName.FrameSet ; }
		}
		/// <summary>
		/// Sets or retrieves the space between the frames, including the 3-D border. 
		/// </summary>
		public string Border
		{
			get{ return GetAttribute( StringConstAttributeName.Border );}
			set{ SetAttribute( StringConstAttributeName.Border , value);}
		}
		/// <summary>
		/// Sets or retrieves the border color of the object. 
		/// </summary>
		public string BorderColor
		{
			get{ return GetAttribute( StringConstAttributeName.BorderColor );}
			set{ SetAttribute( StringConstAttributeName.BorderColor , value);}
		}
		/// <summary>
		/// Sets or retrieves the frame widths of the object.
		/// </summary>
		public string Cols
		{
			get{ return GetAttribute( StringConstAttributeName.Cols );}
			set{ SetAttribute( StringConstAttributeName.Cols , value);}
		}
		/// <summary>
		/// Sets or retrieves whether to display a border for the frame.
		/// </summary>
		public string FrameBorder
		{
			get{ return GetAttribute( StringConstAttributeName.FrameBorder );}
			set{ SetAttribute( StringConstAttributeName.FrameBorder , value);}
		}
		/// <summary>
		/// Sets or retrieves the amount of additional space between the frames.
		/// </summary>
		public string FrameSpacing
		{
			get{ return GetAttribute( StringConstAttributeName.FrameSpacing );}
			set{ SetAttribute( StringConstAttributeName.FrameSpacing , value);}
		}
		/// <summary>
		/// Sets or retrieves the value indicating whether the object visibly indicates that it has focus.
		/// </summary>
		public string HideFocus
		{
			get{ return GetAttribute( StringConstAttributeName.HideFocus );}
			set{ SetAttribute( StringConstAttributeName.HideFocus , value);}
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
		/// Sets or retrieves the frame heights of the object.
		/// </summary>
		public string Rows
		{
			get{ return GetAttribute( StringConstAttributeName.Rows );}
			set{ SetAttribute( StringConstAttributeName.Rows , value);}
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
		/// Sets or retrieves the width of the object.
		/// </summary>
		public string Width
		{
			get{ return GetAttribute( StringConstAttributeName.Width );}
			set{ SetAttribute( StringConstAttributeName.Width , value);}
		}
		/// <summary>
		/// 内部方法,本对象子标签名称只能为 frame
		/// </summary>
		/// <param name="strName"></param>
		/// <returns></returns>
		internal override bool CheckChildTagName(string strName)
		{
			return strName == StringConstTagName.Frame ;
		}
	}//public class HTMLFrameSetElement : HTMLContainer
}