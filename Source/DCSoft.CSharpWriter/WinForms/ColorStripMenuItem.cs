/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using System.Text;

namespace DCSoft.WinForms
{
    public class ColorStripMenuItem : System.Windows.Forms.ToolStripMenuItem
    {
        public static string CustomColorName = "Custom...";

        private static System.Collections.Hashtable myColorNames = new System.Collections.Hashtable();
        public static void AddColorName(System.Drawing.Color c, string name)
        {
            myColorNames[c.ToArgb()] = name;
        }

        private static System.Collections.Hashtable myImages = new System.Collections.Hashtable();
        public static System.Drawing.Bitmap GetBitmap(System.Drawing.Color color)
        {
            int size = 16;
            System.Drawing.Bitmap bmp = (System.Drawing.Bitmap)myImages[color];
            if (bmp != null)
            {
                return bmp;
            }
            bmp = new System.Drawing.Bitmap(size, size);
            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp))
            {
                using (System.Drawing.SolidBrush b = new System.Drawing.SolidBrush(color))
                {
                    g.FillRectangle(b, 3, 3, size - 6, size - 6);
                }
                g.DrawRectangle(System.Drawing.Pens.Black, 3, 3, size - 6, size - 6);

                //g.Clear(color);
                //g.DrawRectangle(System.Drawing.Pens.Black, 0, 0, 14, 14);
            }
            //bmp.MakeTransparent(System.Drawing.Color.Transparent);
            myImages[color] = bmp;
            return bmp;
        }

        /// <summary>
        /// 创建标准颜色菜单数组
        /// </summary>
        /// <returns>菜单对象数组</returns>
        public static ColorStripMenuItem[] CreateStandardItems( System.EventHandler handler )
        {
            System.Collections.ArrayList list = new System.Collections.ArrayList();
            list.Add(new ColorStripMenuItem(System.Drawing.Color.Black, "Black", handler)); // Black 0,0,0
            list.Add(new ColorStripMenuItem(System.Drawing.Color.DarkRed, "Bark Red", handler)); // Dark Red 128,0,0
            list.Add(new ColorStripMenuItem(System.Drawing.Color.DarkGreen, "Dark Green", handler)); // Dark Green 0,128,0
            list.Add(new ColorStripMenuItem(System.Drawing.Color.Olive, "Olive", handler)); // Pea Green 128,128,0
            list.Add(new ColorStripMenuItem(System.Drawing.Color.DarkBlue, "Dark Blue", handler)); // Dark Blue 0,0,128
            list.Add(new ColorStripMenuItem(System.Drawing.Color.Lavender, "Lavender", handler)); // Lavender ,128,0,128
            list.Add(new ColorStripMenuItem(System.Drawing.Color.SlateBlue, "Slate", handler)); // Slate 0,128,128
            list.Add(new ColorStripMenuItem(System.Drawing.Color.LightGray, "Light Gray", handler)); // Light Gray 192,192,192
            list.Add(new ColorStripMenuItem(System.Drawing.Color.DarkGray, "Dark Gray", handler)); // Dark Gray 128,128,128
            list.Add(new ColorStripMenuItem(System.Drawing.Color.Red, "Bright Red", handler)); // Bright Red 255,0,0
            list.Add(new ColorStripMenuItem(System.Drawing.Color.Green, "Bright Green", handler)); // Bright Green 0,255,0
            list.Add(new ColorStripMenuItem(System.Drawing.Color.Yellow, "Yellow", handler)); // Yellow 255,255,0
            list.Add(new ColorStripMenuItem(System.Drawing.Color.Blue, "Bright Blue", handler)); // Bright Blue 0,0,255
            list.Add(new ColorStripMenuItem(System.Drawing.Color.Magenta, "Magenta", handler)); // Magenta 255,0,255
            list.Add(new ColorStripMenuItem(System.Drawing.Color.Cyan, "Cyan", handler)); // Cyan 0,255,255
            list.Add(new ColorStripMenuItem(System.Drawing.Color.White, "White", handler)); //  1 - white 255,255,255
            return (ColorStripMenuItem[])list.ToArray(typeof(ColorStripMenuItem));
        }

        public static ColorStripMenuItem CreateCustomMenuItem(string text, System.Drawing.Color color, EventHandler handler)
        {
            ColorStripMenuItem item = new ColorStripMenuItem(color, text, handler);
            item.ShowColorDialog = true;
            return item;
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="color">颜色值</param>
        /// <param name="text">文本</param>
        /// <param name="handler">菜单点击事件处理委托</param>
        public ColorStripMenuItem(System.Drawing.Color color, string text, System.EventHandler handler)
        {
            intColor = color;
            string name = myColorNames[color.ToArgb()] as string ;
            if (name == null)
                this.Text = text;
            else
                this.Text = name;
            if (handler != null)
            {
                this.Click += handler;
            }
            this.Image = GetBitmap(intColor);
        }

        private System.Drawing.Color intColor = System.Drawing.Color.Transparent;
        /// <summary>
        /// 颜色值
        /// </summary>
        public System.Drawing.Color Color
        {
            get 
            {
                return intColor; 
            }
            set 
            {
                intColor = value;
                this.Image = GetBitmap(value); 
            }
        }

        private bool bolShowColorDialog = false;
        /// <summary>
        /// 是否显示颜色对话框
        /// </summary>
        public bool ShowColorDialog
        {
            get { return bolShowColorDialog; }
            set { bolShowColorDialog = value; }
        }

        protected override void OnClick(EventArgs e)
        {
            if (bolShowColorDialog)
            {
                using (System.Windows.Forms.ColorDialog dlg = new System.Windows.Forms.ColorDialog())
                {
                    dlg.Color = intColor;
                    dlg.FullOpen = true;
                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        intColor = dlg.Color;
                        base.OnClick(e);
                    }
                }
            }
            else
            {
                base.OnClick(e);
            }
        }
    }//public class ColorStripMenuItem : System.Windows.Forms.ToolStripMenuItem
}
