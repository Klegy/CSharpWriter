/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
//using System;
//using System.Collections.Generic ;
//using System.Drawing ;

//namespace DCSoft.CSharpWriter.Dom
//{
//    /// <summary>
//    /// 实填充画刷对象列表
//    /// </summary>
//    public class SolidBrushBuffer
//    {
//        private static Dictionary<int , SolidBrush > brushBuffer = new Dictionary<int,SolidBrush>();
//        /// <summary>
//        /// 清空缓冲区
//        /// </summary>
//        public static void Clear()
//        {
//            foreach (SolidBrush b in brushBuffer.Values)
//            {
//                b.Dispose();
//            }
//            brushBuffer.Clear();
//            foreach (Pen p in penBuffer.Values)
//            {
//                p.Dispose();
//            }
//            penBuffer.Clear();
//        }
//        /// <summary>
//        /// 获得指定颜色的画刷对象
//        /// </summary>
//        /// <param name="color">画刷颜色</param>
//        /// <returns>画刷对象</returns>
//        public static System.Drawing.SolidBrush GetBrush( System.Drawing.Color color )
//        {
//            int rgb = color.ToArgb();
//            if( brushBuffer.ContainsKey( rgb ))
//            {
//                return brushBuffer[ rgb ] ;
//            }
//            else
//            {
//                SolidBrush b = new SolidBrush( color );
//                brushBuffer[ rgb ] = b ;
//                return b ;
//            }
//        }

//        private static Dictionary<int, Pen> penBuffer = new Dictionary<int, Pen>();
//        /// <summary>
//        /// 获得指定颜色的画笔对象
//        /// </summary>
//        /// <param name="color">画笔颜色</param>
//        /// <returns>画笔对象</returns>
//        public static Pen GetPen(Color color)
//        {
//            int rgb = color.ToArgb();
//            if (penBuffer.ContainsKey(rgb))
//            {
//                return penBuffer[rgb];
//            }
//            else
//            {
//                Pen p = new Pen(color);
//                penBuffer[rgb] = p;
//                return p;
//            }
//        }

//        /// <summary>
//        /// 对象不可实例化
//        /// </summary>
//        private SolidBrushBuffer()
//        {
//        }
//    }
//}