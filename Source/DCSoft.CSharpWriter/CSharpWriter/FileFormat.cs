/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;

namespace DCSoft.CSharpWriter
{
	/// <summary>
	/// 文档格式样式
	/// </summary>
	public enum FileFormat
	{
        /// <summary>
        /// 无效值
        /// </summary>
        None,
		/// <summary>
		/// 标准的XML序列化格式
		/// </summary>
		XML ,
		/// <summary>
		/// RTF文档
		/// </summary>
		RTF ,
		/// <summary>
		/// 纯文本
		/// </summary>
		Text,
        /// <summary>
        /// HTML文档
        /// </summary>
         
	}
}