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
using System.ComponentModel ;
using System.Drawing ;

namespace DCSoft.WinForms.VirtualControls
{
    internal class VUtils
    {
        private static TypeConverter cc = TypeDescriptor.GetConverter(typeof(Color));

        public static Color StringToColor(string v , Color defaultColor )
        {
            if (string.IsNullOrEmpty(v))
            {
                return defaultColor;
            }
            else
            {
                try
                {
                    return (Color)cc.ConvertFromString(v);
                }
                catch
                {
                    return defaultColor;
                }
            }
        }

        public static string ColorToString(Color c)
        {
            if (c.IsNamedColor)
            {
                return c.Name;
            }
            else
            {
                return cc.ConvertToString(c);
            }
        }
    }

}
