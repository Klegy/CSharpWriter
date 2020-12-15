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
using DCSoft.Script;
using DCSoft.CSharpWriter.Controls;

namespace DCSoft.CSharpWriter.Script
{
    /// <summary>
    /// 编辑器脚本使用的Window对象类型
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    public class WriterWindow : XVBAWindowObject
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public WriterWindow()
        {
        }

        private CSWriterControl _WriterControl = null;
        /// <summary>
        /// 文本编辑器控件
        /// </summary>
        public CSWriterControl WriterControl
        {
            get
            {
                return _WriterControl;
            }
            set
            {
                _WriterControl = value;
            }
        }

        /// <summary>
        /// 状态栏文本
        /// </summary>
        public object StatusText
        {
            get
            {
                if (this.WriterControl != null)
                {
                    return this.WriterControl.StatusText;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (this.WriterControl != null)
                {
                    this.WriterControl.SetStatusText(GetDisplayText(value));
                }
            }
        }
    }
}