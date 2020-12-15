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

namespace DCSoft.Common
{
    /// <summary>
    /// 文档注释输出属性
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [System.AttributeUsage( AttributeTargets.All , AllowMultiple= false )]
    public class DocumentCommentAttribute : System.Attribute
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DocumentCommentAttribute()
        {
        }
        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="level">输出等级</param>
        public DocumentCommentAttribute(int level)
        {
            intLevel = level;
        }

        private int intLevel = 5 ;
        /// <summary>
        /// 输出等级
        /// </summary>
        public int Level
        {
            get { return intLevel; }
            set { intLevel = value; }
        }
    }
}
