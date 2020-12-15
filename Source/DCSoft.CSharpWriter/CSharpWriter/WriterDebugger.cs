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
    /// 编辑器调试器
    /// </summary>
    public class WriterDebugger
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public WriterDebugger()
        {
        }

        private bool _Enabled = true;
        /// <summary>
        /// 处于允许状态
        /// </summary>
        public bool Enabled
        {
            get { return _Enabled; }
            set { _Enabled = value; }
        }

        public void WriteLine(string text)
        {
            if (this.Enabled)
            {
                System.Diagnostics.Debug.WriteLine(text);
            }
        }

        public void Write(string text)
        {
            if (this.Enabled)
            {
                System.Diagnostics.Debug.Write(text);
            }
        }


        public void DebugLoadingFile(string fileName)
        {
            if (this.Enabled)
            {
                System.Diagnostics.Debug.WriteLine(string.Format(
                    WriterStrings.Loading_FileName, fileName));
            }
        }

        public void DebugLoadFileComplete(int size)
        {
            if (this.Enabled )
            {
                System.Diagnostics.Debug.WriteLine(string.Format(
                    WriterStrings.LoadComplete_Size,
                    WriterUtils.FormatByteSize(size)));
            }
        }

    }
}
