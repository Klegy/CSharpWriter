/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
//using DCSoft.Common;

namespace DCSoft.Drawing
{
    /// <summary>
    /// 垂直对齐方式
    /// </summary>
    //[DocumentComment()]
    public enum VerticalAlignStyle
    {
        /// <summary>
        /// 对齐到顶端
        /// </summary>
        Top = 0 ,
        /// <summary>
        /// 垂直居中对齐
        /// </summary>
        Middle = 1 ,
        /// <summary>
        /// 对齐到底端
        /// </summary>
        Bottom = 2 ,
        /// <summary>
        /// 垂直两边对齐
        /// </summary>
        Justify = 3
    }
}
