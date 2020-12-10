/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using System.Collections.Generic;
using System.Text;

namespace DCSoft.CSharpWriter
{
    /// <summary>
    /// 可访问标记
    /// </summary>
    [Flags]
    public enum AccessFlags
    {
        /// <summary>
        /// 元素只读
        /// </summary>
        Readonly = 1 ,
        /// <summary>
        /// 能修改内容
        /// </summary>
        Modify = 2,
        /// <summary>
        /// 能删除
        /// </summary>
        Delete = 3 ,
        /// <summary>
        /// 所有的权限
        /// </summary>
        All = 0xffffff
    }
}
