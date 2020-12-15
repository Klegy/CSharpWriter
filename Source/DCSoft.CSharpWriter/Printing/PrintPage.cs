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
namespace DCSoft.Printing
{
	/// <summary>
	/// 打印页对象
	/// </summary>
    [Serializable()]
	public class PrintPage
	{
        ///// <summary>
        ///// 无作为的初始化对象
        ///// </summary>
        //public PrintPage()
        //{
        //}

        public PrintPage( 
            IPageDocument document ,
            XPageSettings pageSettings, 
            PrintPageCollection pages , 
            int headerHeight ,
            int footerHeight )
        {
            myDocument = document;
            myPageSettings = pageSettings;
            myOwnerPages = pages;
            intHeaderHeight = headerHeight;
            intFooterHeight = footerHeight;
            intWidth = (int)myPageSettings.ViewClientWidth ;
            // 对标准页高缩小点，避免由于某个页高正好等于标准页高时该页最下面
            // 的线条无法显示和打印。（通融才能从容）
            intHeight = this.ViewStandardHeight - 10 ;
        }

        private XPageSettings myPageSettings = null;
        /// <summary>
        /// 页面设置对象
        /// </summary>
        [System.Xml.Serialization.XmlIgnore()]
        public XPageSettings PageSettings
        {
            get
            {
                return myPageSettings; 
            }
            set
            {
                myPageSettings = value; 
            }
        }

        private int intDocumentHeight = 0;
        /// <summary>
        /// 文档高度
        /// </summary>
        public int DocumentHeight
        {
            get
            {
                return intDocumentHeight; 
            }
            set
            {
                intDocumentHeight = value; 
            }
        }

        private IPageDocument myDocument = null;
        /// <summary>
        /// 页面所属文档对象
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
            }
        }
 
		private PrintPageCollection myOwnerPages = null;
        /// <summary>
        /// 对象所属页面集合
        /// </summary>
        public PrintPageCollection OwnerPages
        {
            get
            {
                return myOwnerPages; 
            }
            set
            {
                myOwnerPages = value;
                if (myOwnerPages != null)
                {
                    if (intHeight < myOwnerPages.MinPageHeight)
                    {
                        intHeight = myOwnerPages.MinPageHeight;
                    }
                }
            }
        }

        private System.Drawing.Point _ClientLocation = System.Drawing.Point.Empty;
        /// <summary>
        /// 在控件客户区域中的位置
        /// </summary>
        public System.Drawing.Point ClientLocation
        {
            get
            {
                return _ClientLocation; 
            }
            set
            {
                _ClientLocation = value; 
            }
        }

        private int intHeaderHeight = 0;
        /// <summary>
        /// 页眉高度
        /// </summary>
        public int HeaderHeight
        {
            get 
            {
                return intHeaderHeight; 
            }
            set
            {
                intHeaderHeight = value; 
            }
        }


        private int intFooterHeight = 0;
        /// <summary>
        /// 页脚高度
        /// </summary>
        public int FooterHeight
        {
            get
            {
                return intFooterHeight; 
            }
            set
            {
                intFooterHeight = value; 
            }
        }

        protected int intLeft = 0;
        public int Left
        {
            get
            {
                return intLeft; 
            }
            set
            {
                intLeft = value; 
            }
        }
        
        /// <summary>
        /// 获得打印页的顶端位置
        /// </summary>
        public int Top
        {
            get
            {
                int intTop = myOwnerPages.Top;
                foreach (PrintPage myPage in myOwnerPages)
                {
                    if (myPage == this)
                        break;
                    intTop += myPage.Height;
                }
                return intTop;
            }
        }


        private int intWidth = 0;
        /// <summary>
        /// 页面对象的宽度
        /// </summary>
        public int Width
        {
            get
            {
                return intWidth; 
            }
            set
            {
                intWidth = value; 
            }
        }
        
        private int intHeight = 0;
        /// <summary>
        /// 页高
        /// </summary>
        public int Height
        {
            get
            {
                return intHeight; 
            }
            set
            {
                intHeight = value;
                FixHeight(); 
            }
        }
        /// <summary>
        /// 标准页高
        /// </summary>
        public int ViewStandardHeight
        {
            get
            {
                return ( int ) myPageSettings.ViewClientHeight
                    - intHeaderHeight
                    - intFooterHeight ; 
            }
        }

        public int Right
        {
            get
            {
                return intLeft + intWidth; 
            }
        }
        
        /// <summary>
        /// 设置,返回页面对象的底线
        /// </summary>
        public int Bottom
        {
            get
            {
                return this.Top + intHeight; 
            }
            set
            {
                intHeight = value - this.Top; 
                FixHeight(); 
            }
        }
        
        private void FixHeight()
        {
            if (intHeight < myOwnerPages.MinPageHeight)
            {
                intHeight = myOwnerPages.MinPageHeight;
            }
            if (intHeight > this.ViewStandardHeight)
            {
                intHeight = this.ViewStandardHeight;
            }
        }

        /// <summary>
        /// 从0开始计算的全局页码
        /// </summary>
        private int intGlobalIndex = 0;
        /// <summary>
        /// 从0开始计算的全局页码
        /// </summary>
        public int GlobalIndex
        {
            get 
            {
                return intGlobalIndex; 
            }
            set
            {
                intGlobalIndex = value; 
            }
        }

        ///// <summary>
        ///// 从0开始的页号
        ///// </summary>
        //public int Index
        //{
        //    get
        //    {
        //        return myOwnerPages.IndexOf(this); 
        //    }
        //}

        public float ViewLeftMargin
        {
            get
            {
               return myPageSettings.ViewLeftMargin;
            }
        }
        public float ViewTopMargin
        {
            get
            {
                return myPageSettings.ViewTopMargin;
            }
        }

        public float ViewRightMargin
        {
            get
            {
                return myPageSettings.ViewRightMargin;
            }
        }

        public float ViewBottomMargin
        {
            get
            {
                return myPageSettings.ViewBottomMargin;
            }
        }

        public float ViewPaperWidth
        {
            get
            {
                return myPageSettings.ViewPaperWidth;
            }
        }
        public float ViewPaperHeight
        {
            get
            {
                return myPageSettings.ViewPaperHeight;
            }
        }


//		/// <summary>
//		/// 根据页面位置添加矩形区域转换关系
//		/// </summary>
//		/// <param name="myTransform">转换列表</param>
//		/// <param name="ForPrint">是否为打印进行填充</param>
//		public void FillTransform( MultiRectangleTransform myTransform , bool ForPrint )
//		{
//			System.Drawing.Rectangle rect = System.Drawing.Rectangle.Empty ;
//			if( ForPrint )
//				rect = new System.Drawing.Rectangle(
//					myOwnerPages.LeftMargin ,
//					myOwnerPages.TopMargin + myOwnerPages.HeadHeight , 
//					this.Width , this.Height );
//			else
//				rect = this.BodyViewBounds ;
//
//			myTransform.Add( 
//				rect ,
//				new System.Drawing.Rectangle( 0 , this.Top , this.Width , this.Height )).Tag = this;
//
//			if( myOwnerPages.HeadHeight > 0 )
//			{
//				if( ForPrint )
//					rect = new System.Drawing.Rectangle(
//						myOwnerPages.LeftMargin , 
//						myOwnerPages.TopMargin ,
//						this.Width , 
//						this.myOwnerPages.HeadHeight );
//				else
//					rect = this.HeadViewBounds ;
//
//				myTransform.Add( 
//					rect , 
//					new System.Drawing.Rectangle( 0 , 0 , this.Width , myOwnerPages.HeadHeight )).Tag = this ;
//			}
//			if( myOwnerPages.TailHeight > 0 )
//			{
//				if( ForPrint )
//					rect = new System.Drawing.Rectangle(
//						myOwnerPages.LeftMargin ,
//						myOwnerPages.PaperHeight - myOwnerPages.BottomMargin - myOwnerPages.TailHeight  ,
//						this.Width ,
//						myOwnerPages.TailHeight );
//				else
//					rect = this.TailViewBounds ;
//
//				myTransform.Add(
//					rect , 
//					new System.Drawing.Rectangle( 0 , myOwnerPages.DocumentHeight - myOwnerPages.TailHeight , this.Width , myOwnerPages.TailHeight )).Tag = this ;
//			}
//		}

//		/// <summary>
//		/// 页面在控件视图区中的内容边框
//		/// </summary>
//		public System.Drawing.Rectangle ContentViewBounds
//		{
//			get
//			{
//				return new System.Drawing.Rectangle( 
//					this.intViewLeft + myOwnerPages.LeftMargin ,
//					this.intViewTop + myOwnerPages.TopMargin ,
//					intWidth ,
//					this.intHeight );
//			}
//		}
//		 
//		public System.Drawing.Rectangle HeadViewBounds
//		{
//			get
//			{
//				if( myOwnerPages.HeadHeight == 0 )
//					return System.Drawing.Rectangle.Empty ;
//				return new System.Drawing.Rectangle( 
//					this.intViewLeft + myOwnerPages.LeftMargin , 
//					this.intViewTop + myOwnerPages.TopMargin ,
//					this.intWidth , 
//					this.myOwnerPages.HeadHeight );
//			}
//		}
//		public System.Drawing.Rectangle BodyViewBounds
//		{
//			get
//			{
//				return new System.Drawing.Rectangle( 
//					this.intViewLeft + myOwnerPages.LeftMargin ,
//					this.intViewTop + myOwnerPages.TopMargin + this.myOwnerPages.HeadHeight ,
//					this.intWidth , 
//					this.intHeight  );
//			}
//		}
//		public System.Drawing.Rectangle TailViewBounds
//		{
//			get
//			{
//				if( myOwnerPages.FooterHeight  == 0 )
//					return System.Drawing.Rectangle.Empty ;
//				else
//					return new System.Drawing.Rectangle(
//						this.intViewLeft + myOwnerPages.LeftMargin , 
//						this.intViewTop + myOwnerPages.PaperHeight - myOwnerPages.BottomMargin - myOwnerPages.FooterHeight ,
//						this.intWidth , 
//						this.myOwnerPages.FooterHeight  ) ;
//			}
//		}
		/// <summary>
		/// 页面对象在未分割的文档视图中的边框
		/// </summary>
		/// <returns></returns>
		public System.Drawing.Rectangle Bounds 
		{
			get
			{
				return new System.Drawing.Rectangle( 0 , this.Top , intWidth , intHeight );
			}
		}

        private System.Drawing.Printing.Margins myClientMargins = new System.Drawing.Printing.Margins();

        public System.Drawing.Printing.Margins ClientMargins
        {
            get
            {
                return myClientMargins; 
            }
            set
            { 
                myClientMargins = value; 
            }
        }


		protected System.Drawing.Rectangle myClientBounds = System.Drawing.Rectangle.Empty ;
		public System.Drawing.Rectangle ClientBounds
		{
			get
            { 
                return myClientBounds ;
            }
			set
            {
                myClientBounds = value;
            }
		}

        private int intClientLeftFix = 0;

        public int ClientLeftFix
        {
            get 
            {
                return intClientLeftFix;
            }
            set
            { 
                intClientLeftFix = value; 
            }
        }


        private bool bolForNewPage = false;
        /// <summary>
        /// 由于报表元素设置了强制分页而导致了分页
        /// </summary>
        public bool ForNewPage
        {
            get
            {
                return bolForNewPage;
            }
            set
            {
                bolForNewPage = value;
            }
        }

//		protected int intViewLeft = 0 ;
//		public int ViewLeft
//		{
//			get{ return intViewLeft;}
//			set{ intViewLeft = value;}
//		}
//
//		protected int intViewTop = 0 ;
//		public int ViewTop 
//		{
//			get{ return intViewTop ;}
//			set{ intViewTop = value;}
//		}
//		public int ViewBottom
//		{
//			get{ return intViewTop + this.intHeight ;}
//		}
//		public System.Drawing.Rectangle ViewBounds
//		{
//			get
//			{
//				return new System.Drawing.Rectangle(
//					intViewLeft , 
//					intViewTop ,
//					myOwnerPages.PaperWidth ,
//					myOwnerPages.PaperHeight );
//			}
//		}
		
		

		/// <summary>
		/// 获得从1开始的页号
		/// </summary>
		public int PageIndex
		{
            get
            {
                if (myOwnerPages == null)
                {
                    return -1;
                }
                else
                {
                    return myOwnerPages.IndexOf(this) + 1;
                }
            }
		}

		
//		public int ContentTop
//		{
//			get
//			{
//				int intTop = 0 ;
//				foreach( PrintPage page in myOwnerPages )
//				{
//					if( page == this )
//						break;
//					intTop += page.ContentHeight ;
//				}
//				return intTop ;
//			}
//		}
		

	}
}