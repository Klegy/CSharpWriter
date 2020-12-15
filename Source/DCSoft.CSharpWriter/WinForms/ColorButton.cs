/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using System.Windows.Forms;
using System.Drawing;

namespace DCSoft.WinForms
{
    [System.ComponentModel.ToolboxItem( true )]
    public class ColorButton : Button
    {
        Color centerColor = Color.Black ;

        public ColorButton()
        {
            MouseEnter += new EventHandler(OnMouseEnter);
            MouseLeave += new EventHandler(OnMouseLeave);
            MouseUp += new MouseEventHandler(OnMouseUp);
            Paint += new PaintEventHandler(ButtonPaint);
        }

        [System.ComponentModel.DefaultValue(typeof( Color ),"Black")]
        public Color CenterColor
        {
            get { return centerColor; }
            set { centerColor = value; }
        }

        void OnMouseEnter(object sender, EventArgs e)
        {
            Invalidate();
        }

        void OnMouseLeave(object sender, EventArgs e)
        {
            Invalidate();
        }

        void OnMouseUp(object sender, MouseEventArgs e)
        {
            Invalidate();
        }

        void ButtonPaint(Object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Rectangle r = ClientRectangle;

            byte border = 4;
            byte right_border = 15;

            Rectangle rc = new Rectangle(r.Left + border, r.Top + border,
                                         r.Width - border - right_border - 1, r.Height - border * 2 - 1);

            using (SolidBrush centerColorBrush = new SolidBrush(centerColor))
            {
                g.FillRectangle(centerColorBrush, rc);
            }

            using (Pen pen = new Pen(Color.Black))
            {
                g.DrawRectangle(pen, rc);

                //draw the arrow
                Point p1 = new Point(r.Width - 9, r.Height / 2 - 1);
                Point p2 = new Point(r.Width - 5, r.Height / 2 - 1);
                g.DrawLine(pen, p1, p2);

                p1 = new Point(r.Width - 8, r.Height / 2);
                p2 = new Point(r.Width - 6, r.Height / 2);
                g.DrawLine(pen, p1, p2);

                p1 = new Point(r.Width - 7, r.Height / 2);
                p2 = new Point(r.Width - 7, r.Height / 2 + 1);
                g.DrawLine(pen, p1, p2);
            }
            using( Pen pen = new Pen(SystemColors.ControlDark))
            {
                Point p1 = new Point(r.Width - 12, 4);
                Point p2 = new Point(r.Width - 12, r.Height - 5);
                g.DrawLine(pen, p1, p2);
            }
            using( Pen  pen = new Pen(SystemColors.ControlLightLight))
            {
                Point p1 = new Point(r.Width - 11, 4);
                Point p2 = new Point(r.Width - 11, r.Height - 5);
                g.DrawLine(pen, p1, p2);
            }
        }

        private bool _EnableTransparentColor = true;
        /// <summary>
        /// 是否允许透明色
        /// </summary>
        [System.ComponentModel.DefaultValue( true )]
        public bool EnableTransparentColor
        {
            get
            {
                return _EnableTransparentColor; 
            }
            set
            {
                _EnableTransparentColor = value; 
            }
        }

        protected override void OnClick(EventArgs e)
        {
            if( this.ContextMenuStrip == null )
            {
                ContextMenuStrip menu = new ContextMenuStrip();
                EventHandler handler = new EventHandler( this.HandleMenuEvent );
                menu.Items.Add( new ColorStripMenuItem( Color.Transparent , "Transparent" ,handler));
                ColorStripMenuItem[] items = ColorStripMenuItem.CreateStandardItems(handler);
                foreach (ColorStripMenuItem item in items)
                {
                    menu.Items.AddRange(items);
                }
                menu.Items.Add( ColorStripMenuItem.CreateCustomMenuItem("Custom..." , this.CenterColor ,handler ));
                this.ContextMenuStrip = menu ;
            }
            this.ContextMenuStrip.Show( this , 0 , this.Height );

            base.OnClick(e);
        }
         

        private void HandleMenuEvent(object sender, EventArgs args)
        {
            ColorStripMenuItem item = (ColorStripMenuItem)sender;
            this.CenterColor = item.Color;
            this.Invalidate();
            if (ValueChanged != null)
            {
                ValueChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 用户修改了颜色时的事件
        /// </summary>
        public event EventHandler ValueChanged = null;
    }

}