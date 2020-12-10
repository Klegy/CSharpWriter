/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using DCSoft.WinForms;
using DCSoft.Drawing;
using DCSoft.Printing;
using System.Drawing ;

namespace DCSoft.CSharpWriter.Dom
{
    internal class DocumentViewTransform : MultiPageTransform
    {
        public DocumentViewTransform()
        {
        }

        /// <summary>
        /// 根据页面位置添加矩形区域转换关系
        /// </summary>
        /// <param name="myTransform">转换列表</param>
        /// <param name="ForPrint">是否为打印进行填充</param>
        public override void AddPage(PrintPage page, float pageTop, float zoomRate)
        {
            //XPageSettings pageSettings = page.PageSettings;
            XPageSettings ps = page.PageSettings;

            System.Drawing.Rectangle rect = System.Drawing.Rectangle.Empty;

            int leftmargin = (int)(page.ViewLeftMargin * zoomRate);
            int topmargin = (int)(page.ViewTopMargin * zoomRate);
            int rightmargin = (int)(page.ViewRightMargin * zoomRate);
            int bottommargin = (int)(page.ViewBottomMargin * zoomRate);
            int pagewidth = (int)(page.ViewPaperWidth * zoomRate);
            int pageheight = (int)(page.ViewPaperHeight * zoomRate);

            int top = (int)pageTop + topmargin;

            SimpleRectangleTransform headerItem = null;
            SimpleRectangleTransform bodyItem = null;
            SimpleRectangleTransform footerItem = null;
            DomDocument document = (DomDocument)page.Document;

            // 添加文档页眉视图映射
            headerItem = new SimpleRectangleTransform();
            headerItem.Enable = (document.CurrentContentPartyStyle == PageContentPartyStyle.Header);
            headerItem.PageIndex = page.PageIndex ;
            headerItem.ContentStyle = PageContentPartyStyle.Header;
            headerItem.PageObject = page;
            headerItem.DocumentObject = page.Document;
            // 映射到文档视图
            //if (document.CurrentContentElement == document.Header)
            {
                headerItem.DescRectF = new System.Drawing.RectangleF(
                    0,
                    0,
                    page.Width,
                    // 如果当前编辑区域是页眉则设置页眉可视区域的高度为页眉内容高度和页眉标准高度的较大者
                    Math.Max(ps.ViewHeaderHeight - 1, document.Header.Height));
            }
            if (document.Header.Height > ps.ViewHeaderHeight)
            {
                top = top + (int) ((document.Header.Height - ps.ViewHeaderHeight)* zoomRate );
            }
            //else
            //{
            //    headerItem.DescRectF = new System.Drawing.RectangleF(
            //        0,
            //        0,
            //        page.Width,
            //        page.ViewTopMargin - ps.ViewHeaderDistance - 1);
            //}
            SetSourceRect(
                headerItem,
                zoomRate,
                leftmargin + page.ClientLeftFix,
                (int)(pageTop + ps.ViewHeaderDistance  * zoomRate));
            headerItem.PartialAreaSourceBounds = new Rectangle(
                headerItem.SourceRect.Left ,
                headerItem.SourceRect.Top ,
                headerItem.SourceRect.Width ,
                headerItem.SourceRect.Height );
                //(int)( headerItem.DescRectF.Height * zoomRate ));
            

            // 添加正文文档映射
            bodyItem = new SimpleRectangleTransform();
            bodyItem.Enable = (document.CurrentContentPartyStyle == PageContentPartyStyle.Body);
            bodyItem.PageIndex = page.PageIndex ;
            bodyItem.ContentStyle = PageContentPartyStyle.Body;
            bodyItem.PageObject = page;
            bodyItem.DocumentObject = page.Document;
            // 映射到文档视图
            bodyItem.DescRectF = new System.Drawing.RectangleF(
                0,
                page.Top,
                page.Width,
                page.Height);
            int spacing = 0;
            if (document.Header.Height > ps.ViewHeaderHeight - 10 )
            {
                spacing = 5;// 当页眉实际高度大于标准页眉高度，页眉内容突出了标准区域，此时为了美观，页眉和页身之间留点空隙
            }
            top = SetSourceRect(
                bodyItem,
                zoomRate,
                leftmargin + page.ClientLeftFix,
                headerItem.PartialAreaSourceBounds.Bottom + spacing );

            bodyItem.PartialAreaSourceBounds = new Rectangle(
                bodyItem.SourceRect.Left,
                bodyItem.SourceRect.Top,
                bodyItem.SourceRect.Width,
                (int)( document.GetStandartPapeViewHeight( ps ) * zoomRate));


            // 添加页脚文档视图映射
            footerItem = new SimpleRectangleTransform();
            footerItem.Enable = (document.CurrentContentPartyStyle == PageContentPartyStyle.Footer);
            footerItem.PageIndex = page.PageIndex;
            footerItem.ContentStyle = PageContentPartyStyle.Footer;
            footerItem.PageObject = page;
            footerItem.DocumentObject = page.Document;
            // 映射到文档视图
            //if (document.CurrentContentElement == document.Footer)
            {
                footerItem.DescRectF = new System.Drawing.RectangleF(
                    0,
                    0,
                    page.Width,
                    // 如果当前编辑区域是页脚则设置页脚可视区域的高度为页脚内容高度和页脚标准高度的较大者
                    document.Footer.Height );
            }
            //else
            //{
            //    footerItem.DescRectF = new System.Drawing.RectangleF(
            //        0,
            //        0,
            //        page.Width,
            //        page.ViewBottomMargin - ps.ViewFooterDistance - 1);
            //}
            SetSourceRect(
                footerItem,
                zoomRate,
                leftmargin + page.ClientLeftFix ,
                ( int ) ( pageTop + pageheight - ps.ViewFooterDistance * zoomRate - document.Footer.Height * zoomRate ));
                //bodyItem.PartialAreaSourceBounds.Bottom + 2);// 为了美观，页身和页脚之间留点空隙，由于页身实际高度经常小于页身标准高度，因此留的空隙小一些。

            footerItem.PartialAreaSourceBounds = new Rectangle(
                footerItem.SourceRect.Left,
                (int ) ( pageTop + pageheight - bottommargin ),
                footerItem.SourceRect.Width,
                ( int ) ( footerItem.SourceRect.Bottom - ( pageTop + pageheight - bottommargin)));// footerItem.SourceRect.Height );
                //(int)( footerItem.DescRectF.Height * zoomRate));
            

            switch (document.CurrentContentPartyStyle)
            {
                case PageContentPartyStyle.Header :
                    if (headerItem != null)
                    {
                        this.Add(headerItem);
                    }
                    this.Add(bodyItem);
                    if (footerItem != null)
                    {
                        this.Add(footerItem);
                    }
                    break;
                case PageContentPartyStyle.Body :
                    this.Add(bodyItem);
                    if (headerItem != null)
                    {
                        this.Add(headerItem);
                    }
                    if (footerItem != null)
                    {
                        this.Add(footerItem);
                    }
                    break;
                case PageContentPartyStyle.Footer :
                    if (footerItem != null)
                    {
                        this.Add(footerItem);
                    }
                    if (headerItem != null)
                    {
                        this.Add(headerItem);
                    }
                    this.Add(bodyItem);
                    break;
            }//switch
        }

        private int SetSourceRect(SimpleRectangleTransform item, float zoomRate, int left, int top)
        {
            System.Drawing.RectangleF rect = System.Drawing.Rectangle.Empty;
            rect.X = left;
            rect.Y = top;
            rect.Width = (int)(item.DescRectF.Width * zoomRate);
            rect.Height = (int)(item.DescRectF.Height * zoomRate);
            // 映射到控件客户区
            item.SourceRectF = rect;
            return (int)rect.Bottom;// ( int ) Math.Ceiling( top + rect.Height );
        }
    }
}
