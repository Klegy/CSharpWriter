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
    public class ValueValidateResultList : List<ValueValidateResult>
    {
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            foreach (ValueValidateResult item in this)
            {
                if (str.Length > 0)
                {
                    str.Append(Environment.NewLine);
                }
                str.Append(item.Message);
            }
            return str.ToString();
        }
    }

    /// <summary>
    /// 数值校验结果信息对象
    /// </summary>
    public class ValueValidateResult
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public ValueValidateResult()
        {
        }

        private DomElement _Element = null;
        /// <summary>
        /// 文档元素对象
        /// </summary>
        public DomElement Element
        {
            get
            {
                return _Element; 
            }
            set
            {
                _Element = value; 
            }
        }

        private string _Message = null;
        /// <summary>
        /// 信息
        /// </summary>
        public string Message
        {
            get
            {
                return _Message; 
            }
            set
            {
                _Message = value; 
            }
        }
    }
}
