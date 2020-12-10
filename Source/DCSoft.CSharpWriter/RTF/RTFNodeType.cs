/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
/*
 * 
 *   DCSoft RTF DOM v1.0
 *   Author : Yuan yong fu.
 *   Email  : yyf9989@hotmail.com
 *   blog site:http://www.cnblogs.com/xdesigner.
 * 
 */


using System;

namespace DCSoft.RTF
{
	/// <summary>
	/// RTF node type
	/// </summary>
	public enum RTFNodeType
	{
		/// <summary>
		/// root
		/// </summary>
		Root ,
		/// <summary>
		/// keyword, etc /marginl
		/// </summary>
		Keyword ,
		/// <summary>
		/// external keyword node , etc. /*/keyword
		/// </summary>
		ExtKeyword ,
		/// <summary>
		/// control
		/// </summary>
		Control ,
		/// <summary>
		/// plain text
		/// </summary>
		Text ,
		/// <summary>
		/// group , etc . { }
		/// </summary>
		Group ,
		/// <summary>
		/// nothing
		/// </summary>
		None
	}
}