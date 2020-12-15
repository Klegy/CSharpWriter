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

namespace DCSoft.CSharpWriter.Dom
{
    /// <summary>
    /// 内容引用状态
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [Serializable]
    public enum ContentReferenceState
    {
        /// <summary>
        /// 禁止引用
        /// </summary>
        Disable ,
        /// <summary>
        /// 每次都自动更新。
        /// </summary>
        AutoUpdate ,
        /// <summary>
        /// 只更新一次，成功后不再更新。
        /// </summary>
        OnceUpdate ,
        /// <summary>
        /// 加载成功，不再更新。
        /// </summary>
        Loaded
    }
}
