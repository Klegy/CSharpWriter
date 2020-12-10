/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing ;

namespace DCSoft.Drawing
{
    /// <summary>
    /// 一些图形对象的缓存区，使用它能避免频繁的创建和销毁图形对象
    /// </summary>
    public static class GraphicsObjectBuffer
    {
        private static Dictionary<Color, SolidBrush> brushes = new Dictionary<Color, SolidBrush>();
        /// <summary>
        /// 获得指定颜色的纯色画刷对象
        /// </summary>
        /// <param name="color">指定的颜色</param>
        /// <returns>画刷对象</returns>
        public static SolidBrush GetSolidBrush(Color color)
        {
            if (brushes.ContainsKey(color))
            {
                return brushes[color];
            }
            else
            {
                SolidBrush b = new SolidBrush(color);
                brushes[color] = b;
                return b;
            }
        }

        private static Dictionary<Color, Pen> pens = new Dictionary<Color, Pen>();
        /// <summary>
        /// 获得指定颜色的画笔对象
        /// </summary>
        /// <param name="color">指定的颜色</param>
        /// <returns>画笔对象</returns>
        public static Pen GetPen(Color color)
        {
            if (pens.ContainsKey(color))
            {
                return pens[color];
            }
            else
            {
                Pen p = new Pen(color);
                pens[color] = p;
                return p;
            }
        }

        /// <summary>
        /// 清空数据，释放所有资源
        /// </summary>
        public static void Clear()
        {
            foreach (SolidBrush b in brushes.Values)
            {
                b.Dispose();
            }
            brushes.Clear();

            foreach (Pen p in pens.Values)
            {
                p.Dispose();
            }
            pens.Clear();
        }

    }
}
