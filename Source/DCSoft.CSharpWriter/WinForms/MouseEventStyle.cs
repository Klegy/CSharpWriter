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

namespace DCSoft.WinForms
{
    /// <summary>
    /// 鼠标事件样式
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    public enum MouseEventStyle
    {
        /// <summary>
        /// 鼠标按键按下事件
        /// </summary>
        MouseDown ,
        /// <summary>
        /// 鼠标移动事件
        /// </summary>
        MouseMove ,
        /// <summary>
        /// 鼠标按键松开事件
        /// </summary>
        MouseUp ,
        /// <summary>
        /// 鼠标单击事件
        /// </summary>
        MouseClick ,
        /// <summary>
        /// 鼠标双击事件
        /// </summary>
        MouseDoubleClick 
    }
}
