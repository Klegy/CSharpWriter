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

namespace DCSoft.CSharpWriter.Commands
{
    /// <summary>
    /// 插入字符串的命令参数对象
    /// </summary>
    /// <remarks>编写 袁永福</remarks>
    [Serializable]
    public class InsertStringCommandParameter
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public InsertStringCommandParameter()
        {
        }

        private string _Text = null;
        /// <summary>
        /// 要插入的文本
        /// </summary>
        public string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }

        //private bool _DeleteSelection = true;
        ///// <summary>
        ///// 插入文本时是否删除选中的文档内容
        ///// </summary>
        //public bool DeleteSelection
        //{
        //    get { return _DeleteSelection; }
        //    set { _DeleteSelection = value; }
        //}
    }
}
