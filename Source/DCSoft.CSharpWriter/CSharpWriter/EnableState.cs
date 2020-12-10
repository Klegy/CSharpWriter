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
    /// 可用状态
    /// </summary>
    public enum EnableState
    {
        /// <summary>
        /// 默认状态
        /// </summary>
        Default ,
        /// <summary>
        /// 可用状态
        /// </summary>
        Enabled ,
        /// <summary>
        /// 无效状态
        /// </summary>
        Disabled
    }
}
