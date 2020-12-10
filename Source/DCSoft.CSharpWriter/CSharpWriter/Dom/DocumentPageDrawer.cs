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
using DCSoft.Printing;
using DCSoft.Drawing;
using DCSoft.WinForms;
using DCSoft.WinForms.Native;

namespace DCSoft.CSharpWriter.Dom
{
    internal class DocumentPageDrawer : PageContentDrawer
    {

        /// <summary>
        /// 打印指定页面
        /// </summary>
        /// <param name="myPage">页面对象</param>
        /// <param name="g">绘图操作对象</param>
        /// <param name="MainClipRect">主剪切矩形</param>
        /// <param name="UseMargin">是否启用页边距</param>
        public override void DrawPage(
            PrintPage myPage,
            System.Drawing.Graphics g,
            System.Drawing.Rectangle MainClipRect,
            bool UseMargin)
        {
            //XPageSettings pageSettings = myPage.PageSettings;
            int LeftMargin = 0;
            int TopMargin = 0;
            int RightMargin = 0;
            int BottomMargin = 0;
            if (UseMargin)
            {
                LeftMargin = (int)myPage.ViewLeftMargin;
                TopMargin = (int)myPage.ViewTopMargin;
                RightMargin = (int)myPage.ViewRightMargin;
                BottomMargin = (int)myPage.ViewBottomMargin;
            }

            this.OnBeforeDrawPage(myPage, g);
            IntPtr hdc = g.GetHdc();
            DeviceCapsClass dcc = new DeviceCapsClass(hdc);
            g.ReleaseHdc();

            DomDocument document = (DomDocument)this.Document;
            XPageSettings ps = myPage.PageSettings;
            if (ps == null)
            {
                ps = document.PageSettings;
            }
            g.PageUnit = document.DocumentGraphicsUnit;
            System.Drawing.Rectangle ClipRect = System.Drawing.Rectangle.Empty;
            if (this.PageHeadText != null)
            {
                // 绘制标题文本
                g.DrawString(
                    this.PageHeadText,
                    System.Windows.Forms.Control.DefaultFont,
                    System.Drawing.Brushes.Red,
                    20,
                    20,
                    System.Drawing.StringFormat.GenericDefault);
            }
            float printableAreaOffsetX = (float)GraphicsUnitConvert.Convert(
                this.PrintableAreaOffset.X / 100.0,
                System.Drawing.GraphicsUnit.Inch,
                document.DocumentGraphicsUnit);
            float printableAreaOffsetY = (float)GraphicsUnitConvert.Convert(
                this.PrintableAreaOffset.Y / 100.0,
                System.Drawing.GraphicsUnit.Inch,
                document.DocumentGraphicsUnit);

            float headerHeight = Math.Max(ps.ViewHeaderHeight, document.Header.Height);
            int headerHeightFix = 0;
            if (document.Header.Height > ps.ViewHeaderHeight - 10 )
            {
                headerHeightFix =( int ) ( document.Header.Height - ( ps.ViewHeaderHeight - 10 ));
            }
            if (this.DrawHead)
            {
                // 绘制页眉
                g.ResetTransform();
                g.ResetClip();
                ClipRect = new System.Drawing.Rectangle(
                    0,
                    0,
                    myPage.Width,
                    (int)headerHeight );

                g.ScaleTransform(this.XZoomRate, this.YZoomRate);
                g.TranslateTransform(
                    LeftMargin - printableAreaOffsetX,
                    ps.ViewHeaderDistance - printableAreaOffsetY );

                g.SetClip(new System.Drawing.Rectangle(
                    ClipRect.Left,
                    ClipRect.Top,
                    ClipRect.Width + 1,
                    ClipRect.Height + 1));

                PageDocumentPaintEventArgs args = new PageDocumentPaintEventArgs(
                    g,
                    ClipRect,
                    document,
                    myPage,
                    PageContentPartyStyle.Header);
                args.RenderMode = ContentRenderMode.Print;
                args.ContentBounds = ClipRect;
                document.DrawContent(args);
                //DesignPaintEventArgs e = new DesignPaintEventArgs( g , ClipRect );
                //myDocument.RefreshView( e );
                g.ResetClip();
                g.ResetTransform();
            }

            // 绘制页面正文
            ClipRect = new System.Drawing.Rectangle(
                0,
                myPage.Top,
                myPage.Width,
                myPage.Height);

            if (!MainClipRect.IsEmpty)
            {
                ClipRect = System.Drawing.Rectangle.Intersect(ClipRect, MainClipRect);
            }
            if (!ClipRect.IsEmpty)
            {
                g.ScaleTransform(this.XZoomRate, this.YZoomRate);
                g.TranslateTransform(
                    LeftMargin - printableAreaOffsetX,
                    TopMargin - myPage.Top + headerHeightFix - printableAreaOffsetY);

                //System.Drawing.Drawing2D.GraphicsPath clipPath = new System.Drawing.Drawing2D.GraphicsPath();
                //clipPath.AddRectangle( ClipRect );
                //g.SetClip( clipPath );

                //g.TranslateTransform( myPages.LeftMargin , myPages.TopMargin - myPage.Top + myPages.HeadHeight );

                System.Drawing.RectangleF rect = DrawerUtil.FixClipBounds(
                    g,
                    ClipRect.Left,
                    ClipRect.Top,
                    ClipRect.Width,
                    ClipRect.Height);

                rect.Offset(-4, -4);
                rect.Width = rect.Width + 8;
                rect.Height = rect.Height + 8;
                g.SetClip(rect);

                //				System.Drawing.RectangleF rect2 = g.ClipBounds ;
                //				if( rect.Top < rect2.Top )
                //				{
                //					float dy = rect2.Top - rect.Top ;
                //					rect.Y = rect.Y - dy * 2 ;
                //					rect.Height = rect.Height + dy * 4 ; 
                //				}
                //				g.SetClip( rect );

                PageDocumentPaintEventArgs args = new PageDocumentPaintEventArgs(
                    g,
                    ClipRect,
                    document,
                    myPage,
                    PageContentPartyStyle.Body);
                args.RenderMode = ContentRenderMode.Print;
                args.ContentBounds = ClipRect;
                document.DrawContent(args);

                //myDocument.DrawDocument( g , ClipRect );
                //DesignPaintEventArgs e = new DesignPaintEventArgs( g , ClipRect );
                //myDocument.RefreshView( e );
            }

            if (this.DrawFooter)
            {
                // 绘制页脚
                g.ResetClip();
                g.ResetTransform();
                int documentHeight = myPage.DocumentHeight;
                float footerHeight = Math.Max(document.Footer.Height, ps.ViewFooterHeight);
                ClipRect = new System.Drawing.Rectangle(
                    0,
                    0,
                    myPage.Width,
                    (int)footerHeight );
                int dy = 0;

                    dy = (int)(myPage.ViewPaperHeight
                        - ps.ViewFooterDistance - document.Footer.Height );
               
                g.ScaleTransform(this.XZoomRate, this.YZoomRate);
                g.TranslateTransform(
                    LeftMargin - printableAreaOffsetX,
                    dy - printableAreaOffsetY);

                g.SetClip(new System.Drawing.Rectangle(
                    ClipRect.Left,
                    ClipRect.Top,
                    ClipRect.Width + 1,
                    ClipRect.Height + 1));

                PageDocumentPaintEventArgs args = new PageDocumentPaintEventArgs(
                    g,
                    ClipRect,
                    document,
                    myPage,
                    PageContentPartyStyle.Footer);
                args.RenderMode = ContentRenderMode.Print;
                args.ContentBounds = ClipRect;
                document.DrawContent(args);
                //DesignPaintEventArgs e = new DesignPaintEventArgs( g , ClipRect );
                //myDocument.RefreshView( e );
            }//if( this.bolDrawFooter )
        }//public void DrawPage()
    }
}
