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
    /// 表单视图模式类型
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    public enum FormViewMode
    {
        /// <summary>
        /// 不处于表单视图模式
        /// </summary>
        Disable ,
        /// <summary>
        /// 普通表单视图模式
        /// </summary>
        Normal ,
        /// <summary>
        /// 严格的表单视图模式
        /// </summary>
        Strict

    }
}
