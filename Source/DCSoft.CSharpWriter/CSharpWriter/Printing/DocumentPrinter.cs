/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using System.Text;
using DCSoft.Printing;
using System.Drawing.Printing;
using DCSoft.Common;
using DCSoft.CSharpWriter.Dom;
using System.Collections.Generic;
using DCSoft.CSharpWriter.Controls;
using System.Drawing;

namespace DCSoft.CSharpWriter.Printing
{
    public class DocumentPrinter
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DocumentPrinter()
        {
        }
        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="doc">要打印的文档对象</param>
        public DocumentPrinter(DomDocument doc)
        {
            myDocuments = new DomDocumentList(doc);
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="documents">要打印的文档集合</param>
        public DocumentPrinter(DomDocumentList documents)
        {
            myDocuments = documents;
        }


        private bool bolAsyncPrint = true;
        /// <summary>
        /// 异步打印
        /// </summary>
        /// <remarks>
        /// 本属性默认为true，执行异步打印，程序向系统提交打印任务后立即返回；
        /// 若该属性为false，则执行同步打印，程序向系统提交打印任务后等待打印任务完全完成后才返回。
        /// </remarks>
        [System.ComponentModel.DefaultValue(true)]
        public bool AsyncPrint
        {
            get
            {
                return bolAsyncPrint;
            }
            set
            {
                bolAsyncPrint = value;
            }
        }

        /// <summary>
        /// 要打印的文档
        /// </summary>
        public DomDocument Document
        {
            get
            {
                if (myDocuments == null)
                    return null;
                else
                    return (DomDocument)myDocuments.FirstDocument;
            }
            set
            {
                myDocuments = new DomDocumentList(value);
            }
        }
        private DomDocumentList myDocuments = new DomDocumentList();
        /// <summary>
        /// 要打印的文档集合
        /// </summary>
        public DomDocumentList Documents
        {
            get
            {
                return myDocuments;
            }
            set
            {
                myDocuments = value;
            }
        }

        private bool _CleanMode = false;
        /// <summary>
        /// 整洁打印模式
        /// </summary>
        public bool CleanMode
        {
            get
            {
                return _CleanMode;
            }
            set
            {
                _CleanMode = value;
            }
        }
         

        private PrintPage myCurrentPage = null;
        /// <summary>
        /// 当前页对象
        /// </summary>
        public PrintPage CurrentPage
        {
            get
            {
                return myCurrentPage;
            }
            set
            {
                myCurrentPage = value;
            }
        }

        private PrintRange intPrintRange = PrintRange.AllPages;
        public PrintRange PrintRange
        {
            get
            {
                return intPrintRange;
            }
            set
            {
                intPrintRange = value;
            }
        }

        private int intFromPage = 0;
        /// <summary>
        /// 获取或设置要打印的第一页的从0开始计算的页码。 
        /// </summary>
        public int FromPage
        {
            get
            {
                return intFromPage;
            }
            set
            {
                intFromPage = value;
                //CheckData();
            }
        }

        private int intToPage = 0;
        /// <summary>
        /// 获取或设置要打印的最后一页的从0开始计算的页码。
        /// </summary>
        public int ToPage
        {
            get
            {
                return intToPage;
            }
            set
            {
                intToPage = value;
                //CheckData();
            }
        }

        private void CheckData()
        {
            if (intFromPage < 0)
            {
                intFromPage = -intFromPage;
            }
            if (intToPage < 0)
            {
                intToPage = -intToPage;
            }
            if (intFromPage > intToPage)
            {
                int temp = intFromPage;
                intFromPage = intToPage;
                intToPage = temp;
            }
        }

        private string strPrinterName = null;
        /// <summary>
        /// 指定默认打印机的名称
        /// </summary>
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

        private CSWriterControl _WriterControl = null;
        /// <summary>
        /// 编辑器控件对象
        /// </summary>
        public CSWriterControl WriterControl
        {
            get { return _WriterControl; }
            set { _WriterControl = value; }
        }

        /// <summary>
        /// 打印报表文档
        /// </summary>
        /// <param name="Prompt">是否显示打印机选择对话框</param>
        /// <returns>是否进行了打印</returns>
        public bool PrintDocument(bool Prompt)
        {
            return PrintDocument(Prompt, -1);
        }

        private void HandleQueryPageSettings(
            object Sender,
            System.Drawing.Printing.QueryPageSettingsEventArgs args)
        {
            XPrintDocument doc = (XPrintDocument)Sender;
            DomDocument doc2 = (DomDocument)doc.CurrentPrintingPage.Document;
            //if (doc2.MainDocument != null)
            //    doc2 = doc2.MainDocument;
            //doc2.RawPageIndex = doc2.Pages.IndexOf(doc.CurrentPrintPage);
            //doc2.ScriptRunner.ExecutePropertyScriptBlock(doc2, ReportConsts.ScriptOnQueryPageSettings);
        }
        private void HandlePaintPage(
            object Sender,
            EventArgs args)
        {
            XPrintDocument doc = (XPrintDocument)Sender;
            DomDocument doc2 = (DomDocument)doc.CurrentPrintingPage.Document;
            doc2.PageIndex = doc.CurrentPrintingPage.GlobalIndex;
            doc2._Printing = true;
            if (this.WriterControl != null)
            {
                this.WriterControl.SetStatusText(
                    string.Format(WriterStrings.PrintPage_PageIndex, doc2.PageIndex + 1));
            }
            //doc2.Settings.LastPrintTime = DateTime.Now;
            //doc2.SetDocumentStateWithRaiseScript(DocumentState.Printing);
        }

        /// <summary>
        /// 打印报表文档
        /// </summary>
        /// <param name="Prompt">是否显示打印机选择对话框</param>
        /// <param name="SpecialPageIndex">从0开始计算的要打印的指定序号的报表页,若该参数超出范围则打印文档的所有页</param>
        /// <returns>是否进行了打印</returns>
        public bool PrintDocument(bool Prompt, int SpecialPageIndex)
        {
            if (this.Document == null)
                return false;

            //// 设置文档中内嵌的授权信息
            //ReportUtil.SetRedistributeLicenceInfo(this.Documents);

            //if (this.Documents.Count > 1)
            //{
            //    if ( DCSoft.MyLicense.MyLicenseManager.ValidateLicense(ReportConsts.LicenceID_MultiDocument, false , false ) == false)
            //    {
            //        throw new Exception(XReportStrings.LicenceNotSupportMultiDocument);
            //    }
            //}
            
            this.CheckData();

            using (WriterPrintDocument printDoc = new WriterPrintDocument())
            {
                printDoc.CleanMode = this.CleanMode;
                printDoc.AsyncPrint = this.AsyncPrint;
                printDoc.Documents = this.Documents;
                
                printDoc.WriterControl = this.WriterControl;
                printDoc.CurrentDocumentPage = this.CurrentPage;
                if (string.IsNullOrEmpty(this.PrinterName) == false)
                {
                    printDoc.PrinterName = this.PrinterName;
                }
                if (string.IsNullOrEmpty(this.PaperSourceName) == false)
                {
                    printDoc.PaperSourceName = this.PaperSourceName;
                }
                if (Prompt)
                {
                    // 显示打印机选择对话框
                    if (printDoc.Prompt(SpecialPageIndex ) == false)
                    {
                        return false;
                    }
                }
                return printDoc.PrintDocument(SpecialPageIndex);
            }
        }

        //    using (XPrintDocument doc = new XPrintDocument())
        //    {
        //        doc.CurrentDocumentPage = this.CurrentPage;
        //        doc.PageDrawer = new DocumentPageDrawer();
                
        //        doc.QueryPageSettings +=
        //            new System.Drawing.Printing.QueryPageSettingsEventHandler(
        //                HandleQueryPageSettings);
        //        doc.PaintPage += new EventHandler(HandlePaintPage);
        //        doc.PrinterSettings = new System.Drawing.Printing.PrinterSettings();
        //        doc.PrinterSettings.PrintRange = this.PrintRange;
        //        doc.PrinterSettings.FromPage = this.FromPage;
        //        doc.PrinterSettings.ToPage = this.ToPage;
        //        doc.PreparePrintJob = !this.AsyncPrint;
        //        // 调用标准的打印机控制器，不显示打印进度对话框。
        //        doc.PrintController = new System.Drawing.Printing.StandardPrintController();
        //        if (strPrinterName != null
        //            && strPrinterName.Trim().Length > 0)
        //        {
        //            doc.PrinterName = strPrinterName;
        //        }
        //        if (strPaperSourceName != null
        //            && strPaperSourceName.Trim().Length > 0)
        //        {
        //            doc.PaperSourceName = strPaperSourceName;
        //        }
        //        if (Prompt)
        //        {
        //            using (System.Windows.Forms.PrintDialog dlg
        //                       = new System.Windows.Forms.PrintDialog())
        //            {
        //                string printerName = this.strPrinterName;
        //                if (printerName == null || printerName.Trim().Length == 0)
        //                {
        //                    printerName = this.Document.PageSettings.PrinterName;
        //                }
        //                if (printerName != null && printerName.Trim().Length > 0)
        //                {
        //                    doc.PrinterSettings.PrinterName = printerName;
        //                }
        //                dlg.PrinterSettings = doc.PrinterSettings;
        //                if (SpecialPageIndex < 0)
        //                {
        //                    dlg.AllowCurrentPage = true;
        //                    dlg.AllowSelection = false;
        //                    dlg.AllowSomePages = true;
        //                }
        //                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //                {
        //                    doc.PrinterName = null;
        //                    doc.PaperSourceName = null;
        //                    XPageSettings settings = new XPageSettings();
        //                    settings.StdPageSettings =
        //                        dlg.PrinterSettings.DefaultPageSettings;
        //                    //doc.PrinterSettings.
        //                }
        //                else
        //                {
        //                    return false;
        //                }
        //            }
        //        }

        //        //myReportDocument.Runtime
        //        //doc.Document = this;

        //        foreach (XTextDocument doc2 in this.Documents)
        //        {
        //            //doc2.DesignMode = false;
        //            //doc2.SetDocumentStateWithRaiseScript(DocumentState.Printing);
        //        }
        //        Dictionary<XTextDocument, DocumentOptions> optionsBacks = new Dictionary<XTextDocument, DocumentOptions>();
        //        if (this.CleanMode)
        //        {
        //            foreach (XTextDocument doc2 in this.Documents)
        //            {
        //                // 设置整洁打印模式
        //                if (this.Document.Options.SecurityOptions.ShowLogicDeletedContent
        //                    || this.Document.Options.SecurityOptions.ShowPermissionMark)
        //                {
        //                    DocumentOptions optionsBack = this.Document.Options;
        //                    optionsBacks[doc2] = optionsBack;
        //                    this.Document.Options = optionsBack.Clone();
        //                    this.Document.Options.SecurityOptions.ShowLogicDeletedContent = false;
        //                    this.Document.Options.SecurityOptions.ShowPermissionMark = false;
        //                    if (optionsBack.SecurityOptions.ShowLogicDeletedContent)
        //                    {
        //                        // 若该文档是显示被逻辑删除的内容则需要隐藏被逻辑删除的内容，并重新分页。
        //                        // 重新排版
        //                        doc2.ExecuteLayout();
        //                        // 重新分页
        //                        doc2.RefreshPages();
        //                    }
        //                }
        //            }//foreach
        //        }

        //        doc.Pages = this.Documents.AllPages;
        //        doc.JumpPrint = this.myJumpPrint;
        //        doc.CurrentDocumentPage = this.CurrentPage;
        //        if (this.Document.Info.Title != null)
        //        {
        //            doc.DocumentName = this.Document.Info.Title;
        //        }
        //        else
        //        {
        //            doc.DocumentName = "DCSoft.CSharpWriter document";
        //        }
        //        //this.Pages.PrinterSettings
        //        //    = new System.Drawing.Printing.PrinterSettings();

        //        //this.SetDocumentStateWithRaiseScript(DocumentState.Printing);
        //        //this.State = DocumentState.Printing;
        //        //doc.AutoDetectPageSize = true;
        //        foreach (XTextDocument document in this.Documents)
        //        {
        //            document._Printing = false;
        //        }
        //        try
        //        {
        //            if (SpecialPageIndex >= 0
        //                && SpecialPageIndex < doc.Pages.Count)
        //            {
        //                doc.PrintSpecialPage(SpecialPageIndex);
        //            }
        //            else
        //            {
        //                doc.Print();
        //            }
        //            if (this.WriterControl != null)
        //            {
        //                this.WriterControl.SetStatusText(WriterStrings.PrintComplete);
        //            }
        //        }
        //        finally
        //        {
        //            foreach (XTextDocument document in this.Documents)
        //            {
        //                if (document._Printing)
        //                {
        //                    document._Printing = false;
        //                    // 更新文档最后一次打印的时间
        //                    document.Info.LastPrintTime = DateTime.Now;
        //                }
        //            }
        //            if (optionsBacks.Count > 0)
        //            {
        //                foreach (XTextDocument doc2 in optionsBacks.Keys)
        //                {
        //                    doc2.Options = optionsBacks[doc2];
        //                    if (doc2.Options.SecurityOptions.ShowLogicDeletedContent)
        //                    {
        //                        // 恢复设置,重新分页
        //                        doc2.ExecuteLayout();
        //                        doc2.RefreshPages();
        //                    }
        //                }
        //            }
        //        }
        //        if (this.AsyncPrint == false)
        //        {
        //            return doc.WaitForExit(System.Environment.UserInteractive);
        //        }
        //        if (doc.PrintedPageCount > 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }//using

        //}//public bool PrintDocument( bool Prompt , int SpecialPageIndex )
    }
}
