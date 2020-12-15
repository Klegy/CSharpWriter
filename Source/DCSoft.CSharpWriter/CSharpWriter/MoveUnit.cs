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

namespace DCSoft.CSharpWriter
{
    /// <summary>
    /// 在文档中移动的单位
    /// </summary>
    public enum MoveUnit
    {
        Character ,
        Word ,
        Line ,
        Paragraph ,
        Cell ,

    }

    public enum MoveTarget
    {
        /// <summary>
        /// 无意义
        /// </summary>
        None ,
        /// <summary>
        /// 文档开头
        /// </summary>
        DocumentHome ,
        /// <summary>
        /// 单元格的开头
        /// </summary>
        CellHome,
        /// <summary>
        /// 段落开头
        /// </summary>
        ParagraphHome ,
        /// <summary>
        /// 上一行
        /// </summary>
        PreLine ,
        /// <summary>
        /// 行首
        /// </summary>
        Home ,
        /// <summary>
        /// 行尾
        /// </summary>
        End ,
        /// <summary>
        /// 下一行
        /// </summary>
        NextLine,
        /// <summary>
        /// 段落尾
        /// </summary>
        ParagraphEnd ,
        /// <summary>
        /// 单元格结尾
        /// </summary>
        CellEnd ,
        /// <summary>
        /// 文档尾
        /// </summary>
        DocumentEnd 
        
    }
}
