/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using System.Text;
using DCSoft.Common;
namespace DCSoft.DataSourceDom
{
    /// <summary>
    /// 数据源数据类型
    /// </summary>
    [DocumentComment()]
    public enum DataSourceValueType
    {
        /// <summary>
        /// 纯文本
        /// </summary>
        Text,
        /// <summary>
        /// 布尔类型值
        /// </summary>
        Boolean,
        /// <summary>
        /// 数值
        /// </summary>
        Numeric,
        /// <summary>
        /// 日期
        /// </summary>
        Date,
        /// <summary>
        /// 时间
        /// </summary>
        Time,
        /// <summary>
        /// 日期时间
        /// </summary>
        DateTime,
        /// <summary>
        /// 二进制数据
        /// </summary>
        Binary,
        /// <summary>
        /// 对象类型
        /// </summary>
        Object
    }
}