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
using System.Drawing;
using System.Reflection;

namespace DCSoft.CSharpWriter.Commands
{
    internal class CommandUtils
    {
        private static Image _NullImage = null;
        public static System.Drawing.Image NullImage
        {
            get
            {
                if (_NullImage == null)
                {
                    _NullImage = GetResourceImage(typeof(CommandUtils).Assembly, "DCSoft.CSharpWriter.Commands.Images.Null.bmp");
                }
                return _NullImage;
            }
        }
        public static System.Drawing.Image GetResourceImage(System.Reflection.Assembly asm , string resourceName )
        {
            if (asm == null)
            {
                return null;
            }
            if (resourceName != null && resourceName.Trim().Length > 0)
            {
                System.IO.Stream stream = asm.GetManifestResourceStream(resourceName);
                if (stream != null)
                {
                    byte[] bs = new byte[stream.Length];
                    stream.Read(bs, 0, bs.Length);
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(bs);
                    System.Drawing.Image img2 = System.Drawing.Image.FromStream(ms);
                    if (img2 is System.Drawing.Bitmap)
                    {
                        System.Drawing.Bitmap bmp = (System.Drawing.Bitmap)img2;
                        bmp.MakeTransparent(bmp.GetPixel(0, bmp.Height - 1));
                    }
                    return img2;
                }
            }
            return null;
        }
    }
}
