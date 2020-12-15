/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
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
using System.ComponentModel;

namespace DCSoft.CSharpWriter.Printing
{
    /// <summary>
    /// 打印文档对象
    /// </summary>
    public class WriterPrintDocument : DCSoft.Printing.XPrintDocument
    {

        /// <summary>
        /// 初始化对象
        /// </summary>
        public WriterPrintDocument()
        {
            this.PageDrawer = new DocumentPageDrawer();
            this.PaintPage += new EventHandler(HandlePaintPage);
            this.PrinterSettings = new System.Drawing.Printing.PrinterSettings();
            // 调用标准的打印机控制器，不显示打印进度对话框。
            this.PrintController = new System.Drawing.Printing.StandardPrintController();
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

        /// <summary>
        /// 显示打印机选中对话框
        /// </summary>
        /// <param name="specialPageIndex">指定的页码</param>
        /// <param name="ownerForm">主窗体</param>
        /// <returns>用户是否成功的设置</returns>
        public bool Prompt( int specialPageIndex )
        {
            using (System.Windows.Forms.PrintDialog dlg
                           = new System.Windows.Forms.PrintDialog())
            {
                dlg.UseEXDialog = true;
                string printerName = this.PrinterSettings.PrinterName;
                if (printerName == null || printerName.Trim().Length == 0)
                {
                    printerName = this.RuntimeMainDocument.PageSettings.PrinterName;
                }
                if (printerName != null && printerName.Trim().Length > 0)
                {
                    this.PrinterSettings.PrinterName = printerName;
                }
                dlg.PrinterSettings = this.PrinterSettings;
                if (specialPageIndex < 0)
                {
                    dlg.AllowCurrentPage = true;
                    dlg.AllowSelection = false;
                    dlg.AllowSomePages = true;
                }
                if (dlg.ShowDialog( this.WriterControl ) == System.Windows.Forms.DialogResult.OK)
                {
                    this.PrinterName = null;
                    this.PaperSourceName = null;
                    XPageSettings settings = new XPageSettings();
                    settings.StdPageSettings =
                        dlg.PrinterSettings.DefaultPageSettings;
                    //doc.PrinterSettings.
                    return true;
                }
                else
                {
                    // 用户取消操作
                    return false;
                }
            }
        }

        /// <summary>
        /// 打印文档
        /// </summary>
        /// <param name="Prompt">是否显示打印机选中对话框</param>
        /// <param name="SpecialPageIndex">指定的要打印的页码序号</param>
        /// <returns>操作是否成功</returns>
        public bool PrintDocument( int SpecialPageIndex)
        {
            DomDocument mainDocument = null;
            if (this.Documents.Count > 0)
            {
                mainDocument = this.Documents[0];
            }
            else
            {
                return false;
            }

            //myReportDocument.Runtime
            //this.Document = this;

            foreach (DomDocument doc2 in this.Documents)
            {
                //doc2.DesignMode = false;
                //doc2.SetDocumentStateWithRaiseScript(DocumentState.Printing);
            }
            //if (this.CleanMode)
            //{
            //    foreach (XTextDocument doc2 in this.Documents)
            //    {
            //        // 设置整洁打印模式
            //        if (doc2.Options.SecurityOptions.ShowLogicDeletedContent
            //            || doc2.Options.SecurityOptions.ShowPermissionMark)
            //        {
            //            DocumentOptions optionsBack = doc2.Options;
            //            optionsBacks[doc2] = optionsBack;
            //            doc2.Options = optionsBack.Clone();
            //            doc2.Options.SecurityOptions.ShowLogicDeletedContent = false;
            //            doc2.Options.SecurityOptions.ShowPermissionMark = false;
            //            doc2._Printing = true;
            //            bool refreshLayout = false;
            //            if (optionsBack.SecurityOptions.ShowLogicDeletedContent)
            //            {
            //                refreshLayout = true;
            //            }
            //            else
            //            {
            //                doc2.Enumerate(delegate(object sender , ElementEnumerateEventArgs args) 
            //                    {
            //                        if (args.Element is XTextInputFieldElementBase )
            //                        {
            //                            // 存在字段元素，由于字段元素的
            //                            XTextInputFieldElementBase field = (XTextInputFieldElementBase)args.Element;
            //                            if (field.Elements == null && field.Elements.Count == 0
            //                                && string.IsNullOrEmpty(field.BackgroundText) == false)
            //                            {
            //                                // 由于显示了背景文本，因此文档需要重新排版
            //                                refreshLayout = true;
            //                                args.Cancel = true;
            //                            }
            //                        }
            //                    });
            //            }
            //            if ( refreshLayout )
            //            {
            //                int pageIndex = doc2.Pages.IndexOf(this.CurrentDocumentPage);
            //                int pageIndex2 = -1;
            //                if (this.JumpPrint != null && this.JumpPrint.Page != null)
            //                {
            //                    pageIndex2 = doc2.Pages.IndexOf(this.JumpPrint.Page);
            //                }
            //                // 若该文档是显示被逻辑删除的内容则需要隐藏被逻辑删除的内容，并重新分页。
            //                // 重新排版
            //                doc2.ExecuteLayout();
            //                // 重新分页
            //                doc2.RefreshPages();
            //                // 修正当前打印的页
            //                if (pageIndex >= 0 && pageIndex < doc2.Pages.Count)
            //                {
            //                    this.CurrentDocumentPage = doc2.Pages[pageIndex];
            //                }
            //                else
            //                {
            //                    this.CurrentDocumentPage = null;
            //                }
            //                // 修正断点续打的页面
            //                if (pageIndex2 >= 0 && pageIndex2 < doc2.Pages.Count)
            //                {
            //                    this.JumpPrint.Page = doc2.Pages[pageIndex2];
            //                }
            //                else
            //                {
            //                    this.JumpPrint.Page = null;
            //                    this.JumpPrint.Enabled = false;
            //                }
            //            }//if
            //        }
            //    }//foreach
            //}
            Dictionary<DomDocument, MyDocumentOptionBack> optionsBacks =
                new Dictionary<DomDocument, MyDocumentOptionBack>();

            // 需要刷新排版的文档元素对象
            List<DomDocument> refreshLayoutDocuments = new List<DomDocument>();
            
            // 进一步判断需要刷新内容的文档对象
            foreach (DomDocument document in this.Documents)
            {
                document._Printing = true;

                bool refreshLayout = false;
                // 判断文档内容是否需要重新排版

                document.Enumerate(delegate(object sender, ElementEnumerateEventArgs args)
                    {
                        
                        if (this.CleanMode && document.Options.SecurityOptions.ShowLogicDeletedContent)
                        {
                            if (args.Element.Style.DeleterIndex >= 0)
                            {
                                // 处于整洁打印模式，而且文档中
                                // 存在被逻辑删除的内容，文档需要重新排版
                                refreshLayout = true;
                                args.Cancel = true;
                            }
                        }
                    });

                if (this.CleanMode)
                {
                    MyDocumentOptionBack optionBack = new MyDocumentOptionBack();
                    optionBack.ReadFrom(document);
                    optionsBacks[document] = optionBack;

                    // 设置整洁打印模式，需要调整文档的配置信息
                    if (document.Options.SecurityOptions.ShowLogicDeletedContent
                        || document.Options.SecurityOptions.ShowPermissionMark)
                    {
                        document.Options.SecurityOptions.ShowLogicDeletedContent = false;
                        document.Options.SecurityOptions.ShowPermissionMark = false;
                        document._Printing = true;
                    }
                }
                if (refreshLayout)
                {

                    refreshLayoutDocuments.Add(document);
                    // 保存状态
                    if (optionsBacks.ContainsKey(document) == false)
                    {
                        MyDocumentOptionBack optionBack = new MyDocumentOptionBack();
                        optionBack.ReadFrom(document);
                        optionsBacks[document] = optionBack;
                    }
                    document.PageViewMode = PageViewMode.Page;

                    int pageIndex = document.Pages.IndexOf(this.CurrentDocumentPage);
                    int pageIndex2 = -1;
                    if (this.JumpPrint != null && this.JumpPrint.Page != null)
                    {
                        pageIndex2 = document.Pages.IndexOf(this.JumpPrint.Page);
                    }
                    // 若该文档是显示被逻辑删除的内容则需要隐藏被逻辑删除的内容，并重新分页。
                    // 重新排版
                    document.ExecuteLayout();
                    // 重新分页
                    document.RefreshPages();
                    // 修正当前打印的页
                    if (pageIndex >= 0 && pageIndex < document.Pages.Count)
                    {
                        this.CurrentDocumentPage = document.Pages[pageIndex];
                    }
                    else
                    {
                        this.CurrentDocumentPage = null;
                    }
                    // 修正断点续打的页面
                    if (pageIndex2 >= 0 && pageIndex2 < document.Pages.Count)
                    {
                        this.JumpPrint.Page = document.Pages[pageIndex2];
                    }
                    else
                    {
                        this.JumpPrint.Page = null;
                        this.JumpPrint.Enabled = false;
                    }
                }//if

            }//foreach

            this.Pages = this.Documents.AllPages;
            if (this.Documents.Count > 0)
            {
                if (mainDocument.Info.Title != null)
                {
                    this.DocumentName = mainDocument.Info.Title;
                }
                else
                {
                    this.DocumentName = "DCSoft.CSharpWriter document";
                }
            }
            //this.Pages.PrinterSettings
            //    = new System.Drawing.Printing.PrinterSettings();

            //this.SetDocumentStateWithRaiseScript(DocumentState.Printing);
            //this.State = DocumentState.Printing;
            //this.AutoDetectPageSize = true;
            foreach (DomDocument document in this.Documents)
            {
                document._Printing = false;
            }
            try
            {
                if (SpecialPageIndex >= 0
                    && SpecialPageIndex < this.Pages.Count)
                {
                    this.PrintSpecialPage(SpecialPageIndex);
                }
                else
                {
                    this.Print();
                }
                if (this.WriterControl != null)
                {
                    this.WriterControl.SetStatusText(WriterStrings.PrintComplete);
                }
            }
            finally
            {
                // 恢复文档设置
                foreach (DomDocument document in this.Documents)
                {
                    if (document._Printing)
                    {
                        document._Printing = false;
                        // 更新文档最后一次打印的时间
                        document.Info.LastPrintTime = DateTime.Now;
                    }
                }
                if (optionsBacks.Count > 0)
                {
                    foreach (DomDocument doc2 in optionsBacks.Keys)
                    {
                        optionsBacks[doc2].WriteTo(doc2);
                    }
                }
                if (refreshLayoutDocuments.Count > 0)
                {
                    foreach (DomDocument document in refreshLayoutDocuments)
                    {
                        document.ExecuteLayout();
                        document.RefreshPages();
                    }
                }
            }//finally
            if (this.AsyncPrint == false)
            {
                return this.WaitForExit(System.Environment.UserInteractive);
            }
            if (this.PrintedPageCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
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

        private void HandlePaintPage(
            object Sender,
            EventArgs args)
        {
            XPrintDocument doc = (XPrintDocument)Sender;
            DomDocument doc2 = (DomDocument)doc.CurrentPrintingPage.Document;
            doc2.PageIndex = doc.Pages.IndexOf(doc.CurrentPrintingPage) + 1;
            doc2._Printing = true;
            if (this.WriterControl != null)
            {
                this.WriterControl.SetStatusText(
                    string.Format(
                        WriterStrings.PrintPage_PageIndex,
                        doc2.PageIndex + 1));
            }
            //doc2.Settings.LastPrintTime = DateTime.Now;
            //doc2.SetDocumentStateWithRaiseScript(DocumentState.Printing);
        }

        protected override void OnQueryPageSettings(System.Drawing.Printing.QueryPageSettingsEventArgs e)
        {
            base.OnQueryPageSettings(e);

            DomDocument doc2 = (DomDocument)this.CurrentPrintingPage.Document;
            //if (doc2.MainDocument != null)
            //    doc2 = doc2.MainDocument;
            //doc2.RawPageIndex = doc2.Pages.IndexOf(this.CurrentPrintPage);
            //doc2.ScriptRunner.ExecutePropertyScriptBlock(doc2, ReportConsts.ScriptOnQueryPageSettings);
        }

        private CSWriterControl _WriterControl = null;
        /// <summary>
        /// 编辑器控件
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public CSWriterControl WriterControl
        {
            get
            {
                return _WriterControl; 
            }
            set
            {
                _WriterControl = value; 
            }
        }

        private DomDocumentList _Documents = new Dom.DomDocumentList();
        /// <summary>
        /// 文档对象
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public DomDocumentList Documents
        {
            get
            {
                return _Documents; 
            }
            set
            {
                _Documents = value; 
            }
        }

        /// <summary>
        /// 主文档对象
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public DomDocument RuntimeMainDocument
        {
            get
            {
                if (_Documents == null || _Documents.Count == 0)
                {
                    return null;
                }
                else
                {
                    return _Documents[0];
                }
            }
        }

        private class MyDocumentOptionBack
        {
            private DocumentOptions _Options = null;
            private PageViewMode _PageViewMode = PageViewMode.Page;

            public void ReadFrom(DomDocument document)
            {
                _Options = document.Options.Clone();
                _PageViewMode = document.PageViewMode;
            }
            public void WriteTo(DomDocument document)
            {
                document.Options = _Options;
                document.PageViewMode = this._PageViewMode;
            }
        }

    }
}
