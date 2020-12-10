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

namespace DCSoft.CSharpWriter.Dom
{
    /// <summary>
    /// 影响视图的类型
    /// </summary>
    public enum ViewEffects
    {
        /// <summary>
        /// 没有影响
        /// </summary>
        None ,
        /// <summary>
        /// 只是影响到显示，重新绘制即可。
        /// </summary>
        Display ,
        /// <summary>
        /// 影响到文档的排版，需要重新排版重新绘制。
        /// </summary>
        Layout
    }
}
