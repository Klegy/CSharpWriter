/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
//using DCSoft.WinForms;
using DCSoft.Drawing;
using DCSoft.WinForms.Native ;

namespace DCSoft.Printing
{
    /// <summary>
    /// 文档页面绘制对象
    /// </summary>
    /// <remarks>
    /// 本对象用于使用页面的方式绘制文档的内容。
    /// </remarks>
    public class PageContentDrawer
    {
        #region 静态成员

        /// <summary>
        /// 创建文档指定页的位图
        /// </summary>
        /// <param name="doc">文档对象</param>
        /// <param name="pages">页面集合</param>
        /// <param name="PageIndex">指定页的序号</param>
        /// <param name="DrawBorder">是否绘制页面边框</param>
        /// <returns>生成的BMP位图文档对象</returns>
        public static System.Drawing.Bitmap GetPageBmp(
            IPageDocument doc,
            PrintPageCollection pages,
            int PageIndex,
            bool DrawBorder,
            PageContentDrawer drawer )
        {
            drawer.Document = doc;
            drawer.Pages = pages;
            drawer.BackColor = System.Drawing.Color.White;
            if (DrawBorder)
                drawer.BorderColor = System.Drawing.Color.Black;
            else
                drawer.BorderColor = System.Drawing.Color.Transparent;
            System.Drawing.Bitmap bmp = drawer.GetPageBmp(pages[PageIndex], true);
            return bmp;
        }

        /// <summary>
        /// 创建文档指定页的位图
        /// </summary>
        /// <param name="doc">文档对象</param>
        /// <param name="pages">页面集合</param>
        /// <param name="PageIndex">指定页的序号</param>
        /// <param name="DrawBorder">是否绘制页面边框</param>
        /// <returns>生成的BMP位图文档对象</returns>
        public static byte[] GetPageMetafile(
            IPageDocument doc,
            PrintPageCollection pages,
            int PageIndex,
            bool DrawBorder,
            PageContentDrawer drawer )
        {
            drawer.Document = doc;
            drawer.Pages = pages;
            drawer.BackColor = System.Drawing.Color.White;
            if (DrawBorder)
                drawer.BorderColor = System.Drawing.Color.Black;
            else
                drawer.BorderColor = System.Drawing.Color.Transparent;
            return drawer.GetMetafile(pages[PageIndex], true);
        }

        #endregion

        /// <summary>
        /// 初始化对象
        /// </summary>
        public PageContentDrawer()
        {
        }
        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="doc">文档对象</param>
        /// <param name="pages">页面集合</param>
        public PageContentDrawer(IPageDocument doc, PrintPageCollection pages)
        {
            this.myDocument = doc;
            this.myPages = pages;
        }

        private System.Drawing.Image myPageBackgroundImage = null;
        /// <summary>
        /// 页面背景图片
        /// </summary>
        public System.Drawing.Image PageBackgroundImage
        {
            get
            {
                return myPageBackgroundImage;
            }
            set
            {
                myPageBackgroundImage = value;
            }
        }

        private IPageDocument myDocument = null;
        /// <summary>
        /// 文档对象
        /// </summary>
        public IPageDocument Document
        {
            get
            {
                return myDocument;
            }
            set
            {
                myDocument = value;
                if (value != null)
                    myPages = value.Pages;
            }
        }

        private float fXZoomRate = 1.0f;
        /// <summary>
        /// X轴缩放比例
        /// </summary>
        public float XZoomRate
        {
            get
            {
                return fXZoomRate;
            }
            set
            {
                fXZoomRate = value;
            }
        }

        private float fYZoomRate = 1.0f;
        /// <summary>
        /// X轴缩放比例
        /// </summary>
        public float YZoomRate
        {
            get
            {
                return fYZoomRate;
            }
            set
            {
                fYZoomRate = value;
            }
        }

        private string strPageHeadText = null;
        /// <summary>
        /// 页面头额外显示的文字
        /// </summary>
        public string PageHeadText
        {
            get
            {
                return strPageHeadText;
            }
            set
            {
                strPageHeadText = value;
            }
        }

        protected PrintPageCollection myPages = null;
        /// <summary>
        /// 分页集合对象
        /// </summary>
        public PrintPageCollection Pages
        {
            get
            {
                return myPages;
            }
            set
            {
                myPages = value;
            }
        }

        /// <summary>
        /// 是否绘制页眉
        /// </summary>
        protected bool bolDrawHead = true;
        /// <summary>
        /// 是否绘制页眉
        /// </summary>
        public bool DrawHead
        {
            get
            {
                return bolDrawHead;
            }
            set
            {
                bolDrawHead = value;
            }
        }

        /// <summary>
        /// 是否绘制页脚
        /// </summary>
        protected bool bolDrawFooter = true;
        /// <summary>
        /// 是否绘制页脚
        /// </summary>
        public bool DrawFooter
        {
            get
            {
                return bolDrawFooter;
            }
            set
            {
                bolDrawFooter = value;
            }
        }

        protected System.Drawing.Color intBackColor = System.Drawing.Color.White;
        /// <summary>
        /// 页面背景颜色
        /// </summary>
        public System.Drawing.Color BackColor
        {
            get
            {
                return intBackColor;
            }
            set
            {
                intBackColor = value;
            }
        }

        protected System.Drawing.Color intBorderColor = System.Drawing.Color.Transparent;
        /// <summary>
        /// 边框颜色
        /// </summary>
        public System.Drawing.Color BorderColor
        {
            get
            {
                return intBorderColor;
            }
            set
            {
                intBorderColor = value;
            }
        }

        private System.Drawing.PointF myPrintableAreaOffset
            = System.Drawing.PointF.Empty;
        /// <summary>
        /// 可打印区域偏移量
        /// </summary>
        public System.Drawing.PointF PrintableAreaOffset
        {
            get
            {
                return myPrintableAreaOffset;
            }
            set
            {
                myPrintableAreaOffset = value;
            }
        }

        /// <summary>
        /// 创建指定页的图元数据
        /// </summary>
        /// <param name="page">页面对象</param>
        /// <param name="DrawMargin">是否绘制边距线</param>
        /// <returns>包含图元数据的字节数组</returns>
        public byte[] GetMetafile(PrintPage page, bool DrawMargin)
        {
            XPageSettings pageSettings = page.PageSettings;
            System.Drawing.Imaging.Metafile meta = null;
            using (DeviceContexts dc = DeviceContexts.CreateCompatibleDC(IntPtr.Zero))
            {
                System.IO.MemoryStream stream = new System.IO.MemoryStream();
                meta = new System.Drawing.Imaging.Metafile(
                    stream,
                    dc.HDC,
                    new System.Drawing.Rectangle(
                    0,
                    0,
                    (int)page.PageSettings.ViewPaperWidth,
                    (int)page.PageSettings.ViewPaperHeight),
                    //System.Drawing.Imaging.MetafileFrameUnit.Document );
                    PrintUtil.ConvertUnit(myDocument.DocumentGraphicsUnit));
                using (System.Drawing.Graphics g2 = System.Drawing.Graphics.FromImage(meta))
                {
                    if (intBackColor.A != 0)
                        g2.Clear(this.intBackColor);

                    g2.PageUnit = myDocument.DocumentGraphicsUnit;

                    PageFrameDrawer drawer = new PageFrameDrawer();
                    drawer.DrawMargin = DrawMargin;
                    drawer.BackColor = System.Drawing.Color.Transparent;
                    drawer.BorderColor = this.intBorderColor;
                    drawer.BorderWidth = 1;
                    drawer.LeftMargin = (int)page.ViewLeftMargin;
                    drawer.TopMargin = (int)page.ViewTopMargin;
                    drawer.RightMargin = (int)page.ViewRightMargin;
                    drawer.BottomMargin = (int)page.ViewBottomMargin;

                    drawer.Bounds = new System.Drawing.Rectangle(
                        0,
                        0,
                        (int)pageSettings.ViewPaperWidth,
                        (int)pageSettings.ViewPaperHeight);
                    drawer.BackgroundImage = this.PageBackgroundImage;
                    g2.ScaleTransform(this.XZoomRate, this.YZoomRate);
                    drawer.DrawPageFrame(g2, System.Drawing.Rectangle.Empty);

                    DrawPage(page, g2, page.Bounds, true);
                }
                meta.Dispose();
                dc.Dispose();
                return stream.ToArray();
            }
        }

        /// <summary>
        /// 获得指定页的BMP图片对象
        /// </summary>
        /// <param name="PageIndex">页号</param>
        /// <param name="DrawMargin">是否绘制页边距线</param>
        /// <returns>创建的BMP图片对象</returns>
        public System.Drawing.Bitmap GetPageBmp(int PageIndex, bool DrawMargin)
        {
            return GetPageBmp(this.myPages[PageIndex], DrawMargin);
        }
        /// <summary>
        /// 创建指定页的BMP图片对象
        /// </summary>
        /// <param name="page">页面对象</param>
        /// <param name="DrawMargin">是否绘制页边距线</param>
        /// <returns>创建的BMP图片对象</returns>
        public System.Drawing.Bitmap GetPageBmp(PrintPage page, bool DrawMargin)
        {
            XPageSettings pageSettings = page.PageSettings;
            double rate = GraphicsUnitConvert.GetRate(
                myDocument.DocumentGraphicsUnit,
                System.Drawing.GraphicsUnit.Pixel);

            int width = (int)Math.Ceiling(pageSettings.ViewPaperWidth / rate);
            int height = (int)Math.Ceiling(pageSettings.ViewPaperHeight / rate);
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(width, height);
            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp))
            {
                if (intBackColor.A != 0)
                    g.Clear(this.intBackColor);

                g.PageUnit = myDocument.DocumentGraphicsUnit;

                PageFrameDrawer drawer = new PageFrameDrawer();
                drawer.DrawMargin = DrawMargin;
                drawer.BackColor = System.Drawing.Color.Transparent;
                drawer.BorderColor = this.intBorderColor;
                drawer.BorderWidth = 1;
                drawer.LeftMargin = (int)page.ViewLeftMargin;
                drawer.TopMargin = (int)page.ViewTopMargin;
                drawer.RightMargin = (int)page.ViewRightMargin;
                drawer.BottomMargin = (int)page.ViewBottomMargin;

                drawer.Bounds = new System.Drawing.Rectangle(
                    0,
                    0,
                    (int)page.ViewPaperWidth,
                    (int)page.ViewPaperHeight);
                drawer.BackgroundImage = this.PageBackgroundImage;
                drawer.DrawPageFrame(g, System.Drawing.Rectangle.Empty);

                DrawPage(page, g, page.Bounds, true);
            }
            return bmp;
        }

        /// <summary>
        /// 开始输出一个页面前执行的过程
        /// </summary>
        /// <param name="page">页面对象</param>
        /// <param name="g">图形绘制对象</param>
        protected virtual void OnBeforeDrawPage(
            PrintPage page,
            System.Drawing.Graphics g)
        {
            //myDocument.PageIndex = page.GlobalIndex;
        }

        /// <summary>
        /// 打印指定页面
        /// </summary>
        /// <param name="myPage">页面对象</param>
        /// <param name="g">绘图操作对象</param>
        /// <param name="MainClipRect">主剪切矩形</param>
        /// <param name="UseMargin">是否启用页边距</param>
        public virtual void DrawPage(
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

            g.PageUnit = myDocument.DocumentGraphicsUnit;
            System.Drawing.Rectangle ClipRect = System.Drawing.Rectangle.Empty;
            if (this.strPageHeadText != null)
            {
                // 绘制标题文本
                g.DrawString(
                    strPageHeadText,
                    System.Windows.Forms.Control.DefaultFont,
                    System.Drawing.Brushes.Red,
                    20,
                    20,
                    System.Drawing.StringFormat.GenericDefault);
            }

            float printableAreaOffsetX = (float)GraphicsUnitConvert.Convert(
                myPrintableAreaOffset.X / 100.0,
                System.Drawing.GraphicsUnit.Inch,
                myDocument.DocumentGraphicsUnit);
            float printableAreaOffsetY = (float)GraphicsUnitConvert.Convert(
                myPrintableAreaOffset.Y / 100.0,
                System.Drawing.GraphicsUnit.Inch,
                myDocument.DocumentGraphicsUnit);

            if (this.bolDrawHead)
            {
                // 绘制页眉
                if (myPage.HeaderHeight > 0)
                {
                    g.ResetTransform();
                    g.ResetClip();

                    ClipRect = new System.Drawing.Rectangle(
                        0,
                        0,
                        myPage.Width,
                        myPage.HeaderHeight);

                    g.ScaleTransform(this.XZoomRate, this.YZoomRate);
                    g.TranslateTransform(
                        LeftMargin - printableAreaOffsetX,
                        TopMargin - printableAreaOffsetY);

                    g.SetClip(new System.Drawing.Rectangle(
                        ClipRect.Left,
                        ClipRect.Top,
                        ClipRect.Width + 1,
                        ClipRect.Height + 1));

                    PageDocumentPaintEventArgs args = new PageDocumentPaintEventArgs(
                        g,
                        ClipRect,
                        myDocument,
                        myPage,
                        PageContentPartyStyle.Header);
                    args.ContentBounds = ClipRect;
                    args.PageIndex = myPage.GlobalIndex;
                    args.NumberOfPages = this.Pages.Count;
                    args.ContentBounds = ClipRect;
                    myDocument.DrawContent(args);
                    //DesignPaintEventArgs e = new DesignPaintEventArgs( g , ClipRect );
                    //myDocument.RefreshView( e );
                }
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
                    TopMargin - myPage.Top + myPage.HeaderHeight - printableAreaOffsetY);

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
                    myDocument,
                    myPage,
                    PageContentPartyStyle.Body);
                args.PageIndex = myPage.GlobalIndex;
                args.NumberOfPages = this.Pages.Count;
                args.ContentBounds = ClipRect;
                myDocument.DrawContent(args);

                //myDocument.DrawDocument( g , ClipRect );
                //DesignPaintEventArgs e = new DesignPaintEventArgs( g , ClipRect );
                //myDocument.RefreshView( e );
            }

            if (this.bolDrawFooter)
            {
                // 绘制页脚
                if (myPage.FooterHeight > 0)
                {
                    g.ResetClip();
                    g.ResetTransform();
                    int documentHeight = myPage.DocumentHeight;

                    ClipRect = new System.Drawing.Rectangle(
                        0,
                        documentHeight - myPage.FooterHeight,
                        myPage.Width,
                        myPage.FooterHeight);

                    int dy = 0;

                    if (UseMargin)
                    {
                        dy = (int)(myPage.ViewPaperHeight
                            - myPage.ViewBottomMargin);
                    }
                    else
                    {
                        dy = (int)(myPage.ViewPaperHeight
                            - myPage.ViewBottomMargin
                            - myPage.ViewTopMargin);
                    }


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
                        myDocument,
                        myPage,
                        PageContentPartyStyle.Footer);
                    args.ContentBounds = ClipRect;
                    args.PageIndex = myPage.GlobalIndex;
                    args.NumberOfPages = this.Pages.Count;
                    myDocument.DrawContent(args);
                    //DesignPaintEventArgs e = new DesignPaintEventArgs( g , ClipRect );
                    //myDocument.RefreshView( e );
                }
            }//if( this.bolDrawFooter )
        }//public void DrawPage()
    }//public class DocumentPageDrawer
}