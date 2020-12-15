/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using System.Collections.Generic;
using System.Text;

namespace DCSoft.CSharpWriter.Controls
{
    /// <summary>
    /// 元素值编辑操作结果
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    public enum ElementValueEditResult
    {
        /// <summary>
        /// 没执行任何编辑操作.
        /// </summary>
        None ,
        /// <summary>
        /// 试图执行编辑器操作，但用户取消了。
        /// </summary>
        UserCancel ,
        /// <summary>
        /// 成功执行了编辑器操作.
        /// </summary>
        UserAccept 
    }
}
