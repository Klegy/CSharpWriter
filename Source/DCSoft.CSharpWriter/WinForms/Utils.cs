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
using DCSoft.WinForms.Native;
using System.Windows.Forms;

namespace DCSoft.WinForms
{
    public static class Utils
    {
        public static ApplicationStyle GetApplicationStyle(System.Windows.Forms.Control ctl)
        {
            int layer = 0;
            //string txt = "";
            Control parent = ctl;
            while (parent != null)
            {
                //txt = txt + Environment.NewLine + ctl.GetType().FullName;
                if (parent is System.Windows.Forms.Form)
                {
                    return ApplicationStyle.WinForm;
                }
                else if (parent.GetType().FullName == "System.Windows.Forms.Integration.WinFormsAdapter")
                {
                    return ApplicationStyle.WPF;
                }

                parent = parent.Parent;
                layer++;
            }
            WindowInformation info = new WindowInformation(ctl.Handle);
            while (info != null)
            {
                string cn = info.ClassName;
                if( cn != null && 
                    cn.IndexOf(
                        "Internet Explorer" , 
                        StringComparison.CurrentCultureIgnoreCase ) >= 0)
                {
                    // 运行在IE浏览器中
                    return ApplicationStyle.IEHost;
                }
                info = info.Parent;
            }
            if (layer == 1)
            {
                return ApplicationStyle.SmartClient;
            }
            //System.Windows.Forms.MessageBox.Show(txt);
            return ApplicationStyle.UnKnow;
        }
    }
}
