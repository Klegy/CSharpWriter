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

namespace DCSoft.CSharpWriter.Script
{
    /// <summary>
    /// 表达式信息列表
    /// </summary>
    [Serializable]
    public class DomExpressionList : List<DomExpression>
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DomExpressionList()
        {
        }

        /// <summary>
        /// 获得指定名称的表达式对象
        /// </summary>
        /// <param name="name">表达式名称</param>
        /// <returns>表达式对象</returns>
        public DomExpression this[string name]
        {
            get
            {
                foreach (DomExpression item in this)
                {
                    if (item.Name == name)
                    {
                        return item;
                    }
                }
                return null;
            }
        }
    }
}
