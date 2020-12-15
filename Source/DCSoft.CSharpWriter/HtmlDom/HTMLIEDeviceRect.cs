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
	/// 打印模板边框对象(Provides a container for LAYOUTRECT elements and other content in a print template.)
	/// </summary>
	public class HTMLIEDeviceRect : HTMLContainer
	{
		/// <summary>
		/// 对象标签名称,返回"ie:devicerect"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstTagName.IEDevicerect ;}
		}
		/// <summary>
		/// Sets or retrieves whether the document will be printed at the highest possible resolution.
		/// </summary>
		public string Media
		{
			get{ return base.GetAttribute( StringConstAttributeName.Media );}
			set{ base.SetAttribute( StringConstAttributeName.Media , value);}
		}
	}//public class HTMLIEDeviceRect : HTMLContainer
}