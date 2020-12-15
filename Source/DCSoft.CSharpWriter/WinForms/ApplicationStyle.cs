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
    public enum ApplicationStyle
    {
        /// <summary>
        /// 控件运行在WinForm环境下
        /// </summary>
        WinForm,
        /// <summary>
        /// WPF环境下
        /// </summary>
        WPF,
        /// <summary>
        /// 运行在IE浏览器宿主中
        /// </summary>
        IEHost ,
        /// <summary>
        /// 运行在IE中的智能客户端
        /// </summary>
        SmartClient,
        /// <summary>
        /// 未知
        /// </summary>
        UnKnow
    }
}
