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
    /// 基于委托的动态命令对象
    /// </summary>
    public class WriterCommandDelegate : WriterCommand
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public WriterCommandDelegate()
        {
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="name">动作名称</param>
        /// <param name="handler">执行动作的委托对象</param>
        public WriterCommandDelegate(string name, WriterCommandEventHandler handler)
        {
            base.strName = name;
            this.Handler = handler;
        }

        public WriterCommandEventHandler Handler = null;

        public override void Invoke(DCSoft.CSharpWriter.Commands.WriterCommandEventArgs args)
        {
            if (Handler != null)
            {
                Handler( this , args);
            }
        }
    }
}
