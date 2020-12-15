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
using System.Windows.Forms;

namespace DCSoft.WinForms
{
    /// <summary>
    /// 窗体对象列表
    /// </summary>
    public class FormList : List< System.Windows.Forms.Form>
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public FormList()
        {
        }

        /// <summary>
        /// 获得指定类型的窗体对象
        /// </summary>
        /// <param name="type">窗体对象类型</param>
        /// <returns>获得的窗体对象</returns>
        public Form this[Type type]
        {
            get
            {
                foreach (Form frm in this)
                {
                    if (type.IsInstanceOfType(frm))
                    {
                        if (frm.IsDisposed)
                        {
                            this.Remove(frm);
                            return null;
                        }
                        return frm;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// 销毁所有的窗体对象
        /// </summary>
        public void DisposeAllForm()
        {
            foreach (Form frm in this)
            {
                if (frm != null && frm.IsDisposed == false)
                {
                    frm.Dispose();
                }
            }
            this.Clear();
        }
    }
}
