/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
//using DCSoft.Dom ;
using DCSoft.WinForms;
using System.Drawing.Printing;

namespace DCSoft.Printing
{
	/// <summary>
	/// 打印报表的打印文档对象
	/// </summary>
	/// <remarks>本打印文档对象专门用于实现报表文档的打印输出</remarks>
	[System.ComponentModel.Browsable(false)]
	[System.ComponentModel.ToolboxItem( false )]
    public class XPrintDocument : System.Drawing.Printing.PrintDocument
	{
		/// <summary>
		/// 初始化对象
		/// </summary>
		public XPrintDocument()
		{
		}


        private bool bolPreparePrintJob = false ;
        /// <summary>
        /// 打印文档时是否准备好打印任务对象
        /// </summary>
        [System.ComponentModel.DefaultValue(false)]
        public bool PreparePrintJob
        {
            get
            {
                return bolPreparePrintJob;
            }
            set
            {
                bolPreparePrintJob = value;
            }
        }

        private PrintJob myPrintJob = null;
        /// <summary>
        /// 打印任务
        /// </summary>
        public PrintJob PrintJob
        {
            get
            {
                return myPrintJob;
            }
        }

        private PrintPageCollection myPages = new PrintPageCollection();
        /// <summary>
        /// 需要打印的文档页集合
        /// </summary>
        [System.ComponentModel.Browsable( false )]
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

        private System.Collections.IEnumerator myPageEnumerator = null;

        private PrintPage myCurrentDocumentPage = null;
        /// <summary>
        /// 当前文档页对象
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public PrintPage CurrentDocumentPage
        {
            get
			{
				return myCurrentDocumentPage; 
			}
            set
			{
				myCurrentDocumentPage = value; 
			}
        }
         
        /// <summary>
        /// 当前打印的页
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public PrintPage CurrentPrintingPage
        {
            get
            {
                if (myPageEnumerator == null)
                    return null;
                else
                    return (PrintPage)myPageEnumerator.Current;

                //if (intCurrentPrintPageIndex >= 0 && intCurrentPrintPageIndex < myPages.Count)
                //    return myPages[intCurrentPrintPageIndex];
                //else
                //    return null;
            }
        }

        private JumpPrintInfo myJumpPrint = new JumpPrintInfo();
        /// <summary>
        /// 续打信息
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public JumpPrintInfo JumpPrint
        {
            get
            {
                if (myJumpPrint == null)
                    myJumpPrint = new JumpPrintInfo();
                return myJumpPrint; 
            }
            set
			{
				myJumpPrint = value; 
			}
        }
         
        private string strPrinterName = null;
        /// <summary>
        /// 指定使用的打印机名称
        /// </summary>
        [System.ComponentModel.DefaultValue(null)]
        public string PrinterName
        {
            get
			{
				return strPrinterName; 
			}
            set
			{
				strPrinterName = value; 
			}
        }


        private string strPaperSourceName = null;
        /// <summary>
        /// 指定默认打印纸张来源
        /// </summary>
        [System.ComponentModel.DefaultValue(null)]
        [System.ComponentModel.Category("Behavior")]
        public string PaperSourceName
        {
            get
            {
                return strPaperSourceName;
            }
            set
            {
                strPaperSourceName = value;
            }
        }

        //private bool bolPrintSuccess = false;
        ///// <summary>
        ///// 打印是否成功
        ///// </summary>
        //[System.ComponentModel.Browsable( false )]
        //public bool PrintSuccess
        //{
        //    get
        //    {
        //        return bolPrintSuccess; 
        //    }
        //}

        protected override void OnEndPrint(PrintEventArgs e)
        {
            base.OnEndPrint(e);
            if (this.PreparePrintJob )
            {
                this.myPrintJob = null;
                PrinterInformation info = new PrinterInformation(this.PrinterSettings.PrinterName);
                foreach (PrintJob job in info.Jobs)
                {
                    if (job.Document == this.DocumentName)
                    {
                        this.myPrintJob = job;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 等待打印任务完成
        /// </summary>
        /// <param name="showUI">是否显示等待对话框</param>
        /// <returns>打印操作是否成功</returns>
        public bool WaitForExit( bool showUI )
        {
            if (myPrintJob == null)
            {
                return true;
            }
            else
            {
                if (showUI && System.Environment.UserInteractive)
                {
                    using (dlgWaitPrintJob dlg = new dlgWaitPrintJob())
                    {
                        dlg.PrintJob = myPrintJob;
                        if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            return true;
                        }
                        return false;
                    }
                }
                else
                {
                    return myPrintJob.WaitForExit(null);
                }
            }
        }
         

        ///// <summary>
        ///// 打印指定页
        ///// </summary>
        //private int intSpecialPageIndex = - 1;
		
		/// <summary>
		/// 打印指定页
		/// </summary>
		/// <param name="vPageIndex">指定页号</param>
		public void PrintSpecialPage( int vPageIndex )
		{
 
            this.PrinterSettings.PrintRange = PrintRange.CurrentPage;
 
            this.myCurrentDocumentPage = myPages[vPageIndex];
			this.Print();
		}


		/// <summary>
		/// 打印第一页标记
		/// </summary>
		protected bool bolFirstPage = true;
		/// <summary>
		/// 已重载:开始打印文档
		/// </summary>
		/// <param name="e">事件参数</param>
		protected override void OnBeginPrint(System.Drawing.Printing.PrintEventArgs e)
		{
			if( CheckContent() )
			{
                // 指定打印机
                string printerName = this.PrinterName;
                if (printerName == null || printerName.Trim().Length == 0)
                {
                    if (this.CurrentDocumentPage != null)
                    {
                        printerName = this.CurrentDocumentPage.PageSettings.PrinterName;
                    }
                }
                if (printerName != null && printerName.Trim().Length > 0)
                {
                    printerName = printerName.Trim();
                    foreach (string name in PrinterSettings.InstalledPrinters)
                    {
                        if (string.Compare(printerName, name, true) == 0)
                        {
                            //this.PrinterSettings.PrinterName = name;
                            base.DefaultPageSettings.PrinterSettings.PrinterName = name;
                            base.PrinterSettings.PrinterName = name;
                            break;
                        }
                    }
                }

                // 指定纸张设置
                string paperSource = this.PaperSourceName;
                if (paperSource != null && paperSource.Trim().Length > 0)
                {
                    foreach (System.Drawing.Printing.PaperSource source
                        in base.DefaultPageSettings.PrinterSettings.PaperSources)
                    {
                        if ( source.SourceName != null 
                            && string.Compare(
                            source.SourceName.Trim() ,
                            paperSource.Trim() , 
                            true) == 0)
                        {
                            base.DefaultPageSettings.PaperSource = source;
                            break;
                        }
                    }
                    //e.PageSettings.PaperSource.SourceName = paperSource;
                }
                

                int startIndex = 0;
                if (this.JumpPrint.Enabled 
                    && this.JumpPrint.Page != null)
                {
                    startIndex = myPages.IndexOf(this.JumpPrint.Page);
                }
                System.Collections.ArrayList pages = new System.Collections.ArrayList();
                switch (this.PrinterSettings.PrintRange)
                {
                    case PrintRange.Selection :
                        
                    case PrintRange.AllPages :
                        for (int iCount = startIndex; iCount < myPages.Count; iCount++)
                        {
                            pages.Add(myPages[iCount]);
                        }
                        break;
 
                    case PrintRange.CurrentPage :
                        pages.Add(this.CurrentDocumentPage);
                        break;
                     case PrintRange.SomePages :
                        int endIndex = Math.Min( myPages.Count-1 , this.PrinterSettings.ToPage );
                        for (int iCount = Math.Max(startIndex, this.PrinterSettings.FromPage);
                            iCount <= endIndex;
                            iCount++)
                        {
                            pages.Add( myPages[ iCount ] );
                        }
                        break;
                }
                if (pages.Count == 0)
                {
                    // 没有打印页，立即取消打印操作
                    e.Cancel = true;
                    return;
                }
                else
                {
                    myPageEnumerator = pages.GetEnumerator();
                    myPageEnumerator.MoveNext();
                }
			}
			else
            {
				//intCurrentPrintPageIndex = 0 ;
            }
            
            if (this.PreparePrintJob )
            {
                this.myPrintJob = null;
                this.DocumentName = this.DocumentName + "$" + System.Environment.TickCount;
            }

			base.OnBeginPrint (e);
		}

		/// <summary>
		/// 已重载:查询页面设置
		/// </summary>
		/// <param name="e">事件参数</param>
		protected override void OnQueryPageSettings(System.Drawing.Printing.QueryPageSettingsEventArgs e)
		{
            base.OnQueryPageSettings(e);
            if (CheckContent())
			{
                //string printerNameBack = this.CurrentPrintPage.PageSettings.PrinterName;

                //this.CurrentPrintPage.PageSettings.PrinterName = 
                //    e.PageSettings.PrinterSettings.PrinterName;

                XPageSettings ps = this.CurrentPrintingPage.PageSettings.Clone() ;
                ps.PrinterName = e.PageSettings.PrinterSettings.PrinterName;

                // 指定打印机
                string printerName = ps.PrinterName;
                if (printerName != null && printerName.Trim().Length > 0)
                {
                    printerName = printerName.Trim();
                    foreach (string name in PrinterSettings.InstalledPrinters)
                    {
                        if (string.Compare(printerName, name, true) == 0)
                        {
                            //this.PrinterSettings.PrinterName = name;
                            e.PageSettings.PrinterSettings.PrinterName = name;
                            base.PrinterSettings.PrinterName = name;
                            break;
                        }
                    }
                }
                
                // 指定纸张设置
                string paperSource = ps.PaperSource;
                if (paperSource != null && paperSource.Trim().Length > 0)
                {
					foreach( System.Drawing.Printing.PaperSource source 
                        in e.PageSettings.PrinterSettings.PaperSources )
					{
						if( string.Compare( source.SourceName , paperSource , true ) == 0 )
						{
							e.PageSettings.PaperSource = source ;
							break;
						}
					}
                    //e.PageSettings.PaperSource.SourceName = paperSource;
                }

                //if (ps.StickToPageSize)
                //{
                if (ps.PaperKind == PaperKind.Custom)
                {
                    System.Drawing.Printing.PaperSize newSize = new PaperSize("Custom", ps.PaperWidth, ps.PaperHeight);
                    //System.Drawing.Printing.PaperSize newSize = new PaperSize( mySize.PaperName, ps.PaperWidth, ps.PaperHeight);
                    newSize.Width = ps.PaperWidth;
                    newSize.Height = ps.PaperHeight;
                    //newSize.RawKind = mySize.RawKind;
                    e.PageSettings.PaperSize = newSize;
                }
                //if (ps.PaperKind != PaperKind.Custom)
                else
                {
                    foreach (System.Drawing.Printing.PaperSize mySize
                        in e.PageSettings.PrinterSettings.PaperSizes)
                    {
                        if (ps.PaperKind == mySize.Kind)
                        {
                            e.PageSettings.PaperSize = mySize;
                            break;
                        }
                    }
                }
                //}
                e.PageSettings.Margins = ps.Margins;
                e.PageSettings.Landscape = ps.Landscape;
			}
		}

        private int intPrintedPageCount = 0;
        /// <summary>
        /// 实际累计打印的总页数
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public int PrintedPageCount
        {
            get
            {
                return intPrintedPageCount; 
            }
            set
            {
                intPrintedPageCount = value; 
            }
        }
        

		/// <summary>
		/// 已重载:打印一页内容
		/// </summary>
		/// <param name="e">事件参数</param>
		protected override void OnPrintPage(System.Drawing.Printing.PrintPageEventArgs e)
		{
            base.OnPrintPage(e);
            if (e.Cancel)
            {
                return;
            }
            if (myPageEnumerator != null)
            {
                PrintPage myPage = (PrintPage)myPageEnumerator.Current;

                //myPage.Document.PageIndex = myPage.GlobalIndex;
                //myDocument.PageIndex = intCurrentPageIndex ;
                System.Drawing.Rectangle ClipRect = new System.Drawing.Rectangle(
                    myPage.Left,
                    myPage.Top,
                    myPage.Width,
                    myPage.Height);
                bool bolJumpPrint = false;
                if (this.JumpPrint.Enabled 
                    && this.JumpPrint.Page == myPage )
                {
                    //if( this.JumpPrint.Position > myPage.Top && this.JumpPrint.Position < myPage.Bottom )
                    {
                        int dy = this.JumpPrint.Position;// -myPage.Top;
                        ClipRect.Offset(0, dy);
                        ClipRect.Height = ClipRect.Height - dy;
                        bolJumpPrint = true;
                    }
                }
                if (bolJumpPrint)
                {
                    OnPaintPage(myPage, e.Graphics, ClipRect, false, false , e );
                }
                else
                {
                    OnPaintPage(myPage, e.Graphics, ClipRect, true, true , e );
                }

                e.HasMorePages = myPageEnumerator.MoveNext();
                intPrintedPageCount++;
            }
		}

        private bool bolAutoFitPageSize = false;
        /// <summary>
        /// 自动适应纸张大小
        /// </summary>
        [System.ComponentModel.DefaultValue(false)]
        public bool AutoFitPageSize
        {
            get
            {
                return bolAutoFitPageSize;
            }
            set
            {
                bolAutoFitPageSize = value;
            }
        }

        //private bool bolAutoDetectPageSize = false;
        ///// <summary>
        ///// 自动检测纸张大小,若实际的纸张大小和预计的纸张大小不同则进行缩放
        ///// </summary>
        //[System.ComponentModel.DefaultValue( false )]
        //public bool AutoDetectPageSize
        //{
        //    get
        //    {
        //        return bolAutoDetectPageSize; 
        //    }
        //    set
        //    {
        //        bolAutoDetectPageSize = value; 
        //    }
        //}


        private PageContentDrawer _PageDrawer = new PageContentDrawer();
        /// <summary>
        /// 页面内容绘制器
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public PageContentDrawer PageDrawer
        {
            get
            {
                return _PageDrawer;
            }
            set
            {
                _PageDrawer = value;
            }
        }

		/// <summary>
		/// 打印指定页面
		/// </summary>
		/// <param name="myPage">页面对象</param>
		/// <param name="g">绘图操作对象</param>
		/// <param name="MainClipRect">主剪切矩形</param>
		/// <param name="PrintHead">是否打印页眉</param>
		/// <param name="PrintTail">是否打印页脚</param>
		protected void OnPaintPage(
			PrintPage myPage ,
			System.Drawing.Graphics g ,
			System.Drawing.Rectangle MainClipRect ,
			bool DrawHead , 
			bool DrawFooter ,
            PrintPageEventArgs args )
		{
            if (PaintPage != null)
            {
                PaintPage(this, new EventArgs());
            }
            this.PageDrawer.Document = myPage.Document;
            this.PageDrawer.Pages = this.Pages;
            this.PageDrawer.DrawFooter = DrawFooter;
            this.PageDrawer.DrawHead = DrawHead;
            if ( myPage.PageSettings.AutoFitPageSize)
            {
                //// 计算实际的打印区域大小
                //IntPtr hdc = g.GetHdc();
                //Win32.DeviceCapsClass dcc = new DCSoft.Win32.DeviceCapsClass(hdc);
                //g.ReleaseHdc(hdc);

                //float width = ( float )DCSoft.Drawing.GraphicsUnitConvert.Convert(dcc.HORZSIZE * 1.0 , System.Drawing.GraphicsUnit.Millimeter, System.Drawing.GraphicsUnit.Inch) * 100.0f ;// args.PageSettings.PaperSize.Width;// -args.PageSettings.Margins.Left - args.PageSettings.Margins.Right;
                //float height = ( float) DCSoft.Drawing.GraphicsUnitConvert.Convert(dcc.VERTSIZE * 1.0 , System.Drawing.GraphicsUnit.Millimeter, System.Drawing.GraphicsUnit.Inch) * 100.0f ;// args.PageSettings.PaperSize.Height;// -args.PageSettings.Margins.Top - args.PageSettings.Margins.Bottom;
                float width = args.PageSettings.Bounds.Width;
                float height = args.PageSettings.Bounds.Height;

                //if( args.PageSettings.Landscape )
                //{
                //    float temp = width ;
                //    width = height ;
                //    height = temp ;
                //}
                // 计算预计的打印区域大小
                float width2 = myPage.PageSettings.PaperWidth;// -myPage.PageSettings.LeftMargin - myPage.PageSettings.RightMargin;
                float height2 = myPage.PageSettings.PaperHeight;// -myPage.PageSettings.TopMargin - myPage.PageSettings.BottomMargin;
                if (myPage.PageSettings.Landscape)
                {
                    float temp = width2;
                    width2 = height2;
                    height2 = temp;
                }
                if (Math.Abs((width - width2) / width2) > 0.04
                    || Math.Abs((height - height2) / height2) > 0.04)
                {
                    // 预计的打印区域和实际的打印区域出现较大的差别,则进行自动缩放
                    if (width2 > 0 && height2 > 0)
                    {
                        float rate = Math.Min(width / width2, height / height2);
                        //if (rate < 1)
                        //    rate = rate * 0.98f;
                        this.PageDrawer.XZoomRate = rate;
                        this.PageDrawer.YZoomRate = rate;

                        //drawer.XZoomRate = width / width2;
                        //if (drawer.XZoomRate < 1)
                        //    drawer.XZoomRate *= 0.98f;
                        //drawer.YZoomRate = height / height2;
                        //if (drawer.YZoomRate < 1)
                        //    drawer.YZoomRate *= 0.98f;
                    }
                }
            }
            this.PageDrawer.PrintableAreaOffset = args.PageSettings.PrintableArea.Location;
            this.PageDrawer.DrawPage(myPage, g, MainClipRect, true);
		}

        /// <summary>
        /// 绘制一页文档前触发的事件
        /// </summary>
        public event EventHandler PaintPage = null;

        private bool CheckContent()
        {
            return myPages != null && myPages.Count > 0;
        }
	}//public class DesignPrintDocument : System.Drawing.Printing.PrintDocument

    /// <summary>
    /// 续打信息
    /// </summary>
    public class JumpPrintInfo
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public JumpPrintInfo()
        {
        }

        private bool bolEnabled = false;
        /// <summary>
        /// 是否允许续打
        /// </summary>
        public bool Enabled
        {
            get
            {
                return bolEnabled;
            }
            set
            {
                bolEnabled = value;
            }
        }

        private PrintPage myPage = null;
        /// <summary>
        /// 发生续打的页面
        /// </summary>
        public PrintPage Page
        {
            get
            {
                return myPage;
            }
            set
            {
                myPage = value;
            }
        }
        private int intNativePosition = 0;
        /// <summary>
        /// 原始续打位置
        /// </summary>
        public int NativePosition
        {
            get
            {
                return intNativePosition;
            }
            set
            {
                intNativePosition = value;
                intPosition = value;
            }
        }
        private int intPosition = 0;
        /// <summary>
        /// 实际使用的续打位置离续打页面顶端的距离
        /// </summary>
        public int Position
        {
            get
            {
                return intPosition;
            }
            set
            {
                intPosition = value;
            }
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public JumpPrintInfo Clone()
        {
            return (JumpPrintInfo)this.MemberwiseClone();
        }

        /// <summary>
        /// 比较两个对象的数据
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool EqualsValue(JumpPrintInfo info)
        {
            if (info == null)
            {
                return false;
            }
            if (info == this)
            {
                return true;
            }
            return info.bolEnabled == this.bolEnabled
                && info.intNativePosition == this.intNativePosition
                && info.intPosition == this.intPosition
                && info.myPage == this.myPage;
        }
    }//public class JumpPrintInfo
}