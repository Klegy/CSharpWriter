/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;

namespace DCSoft.CSharpWriter.Undo
{
	/// <summary>
	/// 可撤销和重做的接口类型
	/// </summary>
	public interface IUndoable
	{
		/// <summary>
		/// 撤销操作
		/// </summary>
        /// <param name="args">事件参数</param>
        void Undo(XUndoEventArgs args);
		/// <summary>
		/// 重新操作
		/// </summary>
        /// <param name="args">事件参数</param>
		void Redo( XUndoEventArgs args );
        /// <summary>
		/// 对象是否在一个批处理中
		/// </summary>
		bool InGroup { get ; set ; }
	}
}