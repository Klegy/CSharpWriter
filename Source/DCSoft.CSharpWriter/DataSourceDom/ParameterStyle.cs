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
    /// 参数应用样式
    /// </summary>
    /// <remarks>
    /// 对数据源节点命令文本中执行参数替换的模式,当数据源节点的DataSourceStyle为SQL模式,该样式才有效.
    /// </remarks>
    [DocumentComment()]
    public enum ParameterStyle
    {
        /// <summary>
        /// 默认样式
        /// </summary>
        /// <remarks>
        /// 对数据库连接对象,若其参数样式为Default则使用数据源文档对象的参数样式.
        /// <br />对数据源文档对象,若参数样式为Default则使用宏替换样式.
        /// </remarks>
        Default ,
        /// <summary>
        /// 宏替换样式
        /// </summary>
        /// <remarks>
        /// 对参数采用字符串全局替换的方式.例如对"Select * From Table Where id = [%@MyName%]",参数值为"123",
        /// <br />则处理后的SQL语句为"select * from Table Where id = 123".
        /// </remarks>
        Macro ,
        /// <summary>
        /// 匿名参数样式
        /// </summary>
        /// <remarks>
        /// 对参数采用匿名SQL参数替换模式.例如对"Select * From Table Where id = [%@MyName%]",参数值为"123",
        /// <br />则处理后的SQL语句为"select * from Table Where id = ?",并附带一个值为"123"的参数对象.
        /// </remarks>
        Anonymous,
        /// <summary>
        /// SQLServer参数样式
        /// </summary>
        /// <remarks>
        /// 对参数采用MS SQL Server样式的参数替换模式.例如对"Select * From Table Where id = [%@MyName%]",参数值为"123",
        /// <br />则处理后的SQL语句为"select * from Table Where id = @MyName",并附带一个名为"MyName"的,值为"123"的参数对象.
        /// </remarks>
        SQLServerStyle,
        /// <summary>
        /// Oracle参数样式
        /// </summary>
        /// <remarks>
        /// 对参数采用Oracle样式的参数替换模式.例如对"Select * From Table Where id = [%@MyName%]",参数值为"123",
        /// <br />则处理后的SQL语句为"select * from Table Where id = :MyName",并附带一个名为"MyName"的,值为"123"的参数对象.
        /// </remarks>
        OracleStyle
    }
}
