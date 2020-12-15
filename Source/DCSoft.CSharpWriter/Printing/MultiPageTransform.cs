/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using DCSoft.WinForms;
using DCSoft.Drawing ;
namespace DCSoft.Printing
{
	/// <summary>
	/// ReportPageTransform 的摘要说明。
	/// </summary>
	public class MultiPageTransform : MultiRectangleTransform
	{
		public MultiPageTransform()
		{
		}

		protected PrintPageCollection  myPages = null;
		/// <summary>
		/// 页面集合
		/// </summary>
		public PrintPageCollection Pages
		{
			get
			{
				return myPages ;
			}
			set
			{
				myPages = value;
			}
		}
         
		/// <summary>
		/// 根据页面位置添加矩形区域转换关系
		/// </summary>
		/// <param name="myTransform">转换列表</param>
		/// <param name="ForPrint">是否为打印进行填充</param>
        public virtual void AddPage(PrintPage page, float pageTop, float zoomRate)
        {
            //XPageSettings pageSettings = page.PageSettings;

            System.Drawing.Rectangle rect = System.Drawing.Rectangle.Empty;

            int leftmargin = (int)(page.ViewLeftMargin * zoomRate);
            int topmargin = (int)(page.ViewTopMargin * zoomRate);
            int rightmargin = (int)(page.ViewRightMargin * zoomRate);
            int bottommargin = (int)(page.ViewBottomMargin * zoomRate);
            int pagewidth = (int)(page.ViewPaperWidth * zoomRate);
            int pageheight = (int)(page.ViewPaperHeight * zoomRate);

            int top = (int)pageTop + topmargin;

            SimpleRectangleTransform item = null;
            if (page.HeaderHeight > 0)
            {
                // 添加文档页眉视图映射
                item = new SimpleRectangleTransform();
                item.PageIndex = page.GlobalIndex;
                item.ContentStyle = PageContentPartyStyle.Header;
                item.PageObject = page;
                item.DocumentObject = page.Document;
                // 映射到文档视图
                item.DescRectF = new System.Drawing.RectangleF(
                    0,
                    0,
                    page.Width,
                    page.HeaderHeight - 1);
                top = SetSourceRect(
                     item,
                     zoomRate,
                     leftmargin + page.ClientLeftFix,
                     top);

                this.Add(item);
            }
            // 添加正文文档映射
            item = new SimpleRectangleTransform();
            item.PageIndex = page.GlobalIndex;
            item.ContentStyle = PageContentPartyStyle.Body;
            item.PageObject = page;
            item.DocumentObject = page.Document;
            // 映射到文档视图
            item.DescRectF = new System.Drawing.RectangleF(
                0,
                page.Top,
                page.Width,
                page.Height);

            top = SetSourceRect(
                item,
                zoomRate,
                leftmargin + page.ClientLeftFix,
                top);

            this.Add(item);

            if (page.FooterHeight > 0)
            {
                // 添加页脚文档视图映射
                item = new SimpleRectangleTransform();
                item.PageIndex = page.GlobalIndex;
                item.ContentStyle = PageContentPartyStyle.Footer;
                item.PageObject = page;
                item.DocumentObject = page.Document;
                // 映射到文档视图

                item.DescRectF = new System.Drawing.RectangleF(
                    0,
                    page.DocumentHeight - page.FooterHeight + 1,
                    page.Width,
                    page.FooterHeight - 1);
 

                SetSourceRect(item, zoomRate, leftmargin, top);
                rect = item.SourceRect;

                top = (int)(pageTop + pageheight - bottommargin - rect.Height);
                item.SourceRectF = new System.Drawing.RectangleF(
                    leftmargin + page.ClientLeftFix,
                    top,
                    rect.Width,
                    rect.Height);

                this.Add(item);
            }
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

		public void Refresh( float zoomRate , int pageSpacing )
		{
            int maxPageWidth = 0;
            foreach (PrintPage page in this.Pages)
            {
                if (maxPageWidth < page.Width)
                {
                    maxPageWidth = page.Width;
                }
            }
            maxPageWidth = ( int )( maxPageWidth * zoomRate );
//			float leftmargin = ( float ) ( myPages.LeftMargin * ZoomRate );
			//float pageheight = ( float ) (  myPages.PaperHeight * ZoomRate );

			mySourceOffsetBack = System.Drawing.Point.Empty ;
			this.Clear();
			//int iCount = 0 ;
            float topCount = pageSpacing;
			foreach( PrintPage page in this.Pages )
			{
                //page.ClientLeftFix = (int)(ZoomRate * (MaxPageWidth - page.Width) / 2.0);
				//float PageTop = ( pageheight + PageSpacing ) * iCount + PageSpacing ;
				//iCount ++ ;

				AddPage( page , topCount , zoomRate );

                topCount = topCount + page.PageSettings.ViewPaperHeight * zoomRate + pageSpacing;

			}//foreach( PrintPage page in myDocument.Pages )
			//this.OffsetSource( leftmargin , 0 , false );
		}


        private int intLimitedPageIndex = -1;
        /// <summary>
        /// 进行转换时限制为指定页序号
        /// </summary>
        public int LimitedPageIndex
        {
            get
            { 
                return intLimitedPageIndex;
            }
            set
            { 
                intLimitedPageIndex = value; 
            }
        }
        
		/// <summary>
		/// 是否使用绝对点坐标转换模式
		/// </summary>
		protected bool bolUseAbsTransformPoint = false;
		/// <summary>
        /// 是否使用绝对点坐标转换模式
		/// </summary>
		public bool UseAbsTransformPoint
		{
			get
            { 
                return bolUseAbsTransformPoint ;
            }
			set
            {
                bolUseAbsTransformPoint = value;
            }
		}

        public override System.Drawing.Point UnTransformPoint(int x, int y)
        {
            if (this.bolUseAbsTransformPoint)
            {
                System.Drawing.PointF p = AbsUnTransformPoint(x, y);
                return new System.Drawing.Point((int)p.X, (int)p.Y);
            }
            else
            {
                System.Drawing.Point p = System.Drawing.Point.Empty;
                for( int iCount = this.Count - 1 ; iCount >= 0 ; iCount -- ) 
                {
                    SimpleRectangleTransform item = this[ iCount ] ;
                    if (item.Enable && item.DescRect.Contains(x, y))
                    {
                        p = item.UnTransformPoint(x, y);
                        return p;
                    }
                }
                return p;
            }
        }
        public override System.Drawing.PointF UnTransformPointF(float x, float y)
        {
            if (this.bolUseAbsTransformPoint)
            {
                return AbsUnTransformPoint(x, y);
            }
            else
            {
                return base.UnTransformPointF(x, y);
            }
        }

        public override System.Drawing.Point TransformPoint(int x, int y)
		{
			if( this.bolUseAbsTransformPoint )
			{
                System.Drawing.PointF p = AbsTransformPoint(x, y);
                return new System.Drawing.Point((int)p.X, (int)p.Y);
			}
			else
			{
				return base.TransformPoint (x, y);
			}
		}

        public override System.Drawing.PointF TransformPointF(float x, float y)
        {
            if (this.bolUseAbsTransformPoint)
            {
                return AbsTransformPoint(x, y);
            }
            else
            {
                return base.TransformPointF(x, y);
            }
        }

		public override bool ContainsSourcePoint(int x, int y)
		{
			if( this.bolUseAbsTransformPoint )
				return true;
			else
				return base.ContainsSourcePoint (x, y);	
		}
        public override System.Drawing.Point TransformPoint(System.Drawing.Point p)
        {
            return base.TransformPoint(p);
        }


        public System.Drawing.PointF AbsUnTransformPoint(float x, float y)
        {
            SimpleRectangleTransform pre = null;
            SimpleRectangleTransform next = null;
            SimpleRectangleTransform cur = null;

            foreach (SimpleRectangleTransform item in this)
            {
                if (item.Enable == false)
                    continue;
                if (intLimitedPageIndex >= 0 && item.PageIndex != intLimitedPageIndex)
                    continue;
                if (item.DescRectF.Contains(x, y))
                    return item.UnTransformPointF(x, y);

                if (y >= item.DescRectF.Top && y < item.DescRectF.Bottom)
                {
                    cur = item;
                    break;
                }
                if (y < item.DescRectF.Top)
                {
                    if (next == null || item.DescRectF.Top < next.DescRectF.Top)
                        next = item;
                }
                if (y > item.DescRectF.Bottom)
                {
                    if (pre == null || item.DescRectF.Bottom > pre.DescRectF.Bottom)
                        pre = item;
                }
            }//foreach
            if (cur == null)
            {
                if (pre != null)
                    cur = pre;
                else
                    cur = next;
            }
            if (cur == null)
                return System.Drawing.PointF.Empty;
            System.Drawing.PointF p = new System.Drawing.PointF(x, y);
            p = RectangleCommon.MoveInto(p, cur.DescRectF);
            return cur.UnTransformPointF(p);
        }


		public System.Drawing.PointF AbsTransformPoint( float x , float y )
		{
			SimpleRectangleTransform pre = null;
			SimpleRectangleTransform next = null;
			SimpleRectangleTransform cur = null;
			
			foreach( SimpleRectangleTransform item in this )
			{
                if (item.Enable == false)
                {
                    continue;
                }
                if (item.SourceRectF.Contains(x, y))
                {
                    return item.TransformPointF(x, y);
                }

				if( y >= item.SourceRectF.Top && y <= item.SourceRectF.Bottom )
				{
					cur = item ;
					break;
				}
				if( y < item.SourceRectF.Top )
				{
                    if (next == null || item.SourceRectF.Top < next.SourceRectF.Top)
                    {
                        next = item;
                    }
				}
				if( y > item.SourceRectF.Bottom )
				{
                    if (pre == null || item.SourceRectF.Bottom > pre.SourceRectF.Bottom)
                    {
                        pre = item;
                    }
				}
			}//foreach
			if( cur == null )
			{
                if (pre != null)
                {
                    cur = pre;
                }
                else
                {
                    cur = next;
                }
			}
            if (cur == null)
            {
                return System.Drawing.PointF.Empty;
            }
			System.Drawing.PointF p = new System.Drawing.PointF( x , y );
			p =  RectangleCommon.MoveInto( p , cur.SourceRectF  );
			return cur.TransformPointF( p );
		}
	}//public class MultiPageTransform : MultiRectangleTransform
}