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
    /// 内容元素类型
    /// </summary>
    [Flags]
    [System.ComponentModel.Editor(
        typeof( DCSoft.CSharpWriter.Commands.dlgElementTypeEditor.ElementTypeEditor ) ,
        typeof( System.Drawing.Design.UITypeEditor ))]
    public enum ElementType
    {
        /// <summary>
        /// 无效类型
        /// </summary>
        None = 0 ,
        /// <summary>
        /// 文本元素
        /// </summary>
        Text = 1,
        /// <summary>
        /// 字段元素
        /// </summary>
        Field = 2,
        /// <summary>
        /// 输入框元素
        /// </summary>
        InputField = 4,
        /// <summary>
        /// 表格元素
        /// </summary>
        Table = 8,
        /// <summary>
        /// 表格行
        /// </summary>
        TableRow = 16 ,
        /// <summary>
        /// 表格列
        /// </summary>
        TableColumn = 32 ,
        /// <summary>
        /// 表格单元格
        /// </summary>
        TableCell = 64 ,
        /// <summary>
        /// 嵌入的对象
        /// </summary>
        Object = 128,
        /// <summary>
        /// 换行
        /// </summary>
        LineBreak = 256 ,
        /// <summary>
        /// 分页符号
        /// </summary>
        PageBreak = 512 ,
        /// <summary>
        /// 段落标记
        /// </summary>
        ParagraphFlag = 1024,
        /// <summary>
        /// 复选框
        /// </summary>
        CheckBox = 2048,
        /// <summary>
        /// 图片元素
        /// </summary>
        Image =2048,
        /// <summary>
        /// 文档对象
        /// </summary>
        Document = 5096 ,
        /// <summary>
        /// 所有类型
        /// </summary>
        All = 0xffffff
    }
}
