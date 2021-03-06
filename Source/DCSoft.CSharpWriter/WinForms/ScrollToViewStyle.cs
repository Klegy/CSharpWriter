﻿/*****************************
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
    /// 滚动到视图的样式
    /// </summary>
    public enum ScrollToViewStyle
    {
        /// <summary>
        /// 正常模式
        /// </summary>
        Normal ,
        /// <summary>
        /// 滚动到顶端
        /// </summary>
        Top ,
        /// <summary>
        /// 滚动到中间
        /// </summary>
        Middle ,
        /// <summary>
        /// 滚动到低端
        /// </summary>
        Bottom
    }
}
