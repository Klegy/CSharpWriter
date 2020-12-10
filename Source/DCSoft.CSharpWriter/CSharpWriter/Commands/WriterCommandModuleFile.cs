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
using System.Windows.Forms;
using DCSoft.CSharpWriter.Dom;
using DCSoft.Printing;
using DCSoft.CSharpWriter.Printing;
using DCSoft.CSharpWriter.Data;
using DCSoft.Drawing;
using System.Web;
 

namespace DCSoft.CSharpWriter.Commands
{
    /// <summary>
    /// 文件功能模块
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [WriterCommandDescription( StandardCommandNames.ModuleFile )]
    internal class WriterCommandModuleFile : WriterCommandModule
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public WriterCommandModuleFile()
        {
        }
         

        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(StandardCommandNames.FileOpen,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandFileOpen.bmp")]
        protected void FileOpen(object sender, WriterCommandEventArgs args)
        {
            InnerFileOpen(sender, args);
        }

        private void InnerFileOpen( object sender , WriterCommandEventArgs args  )
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = args.Document != null && args.EditorControl != null ;
            }
            if (args.Mode == WriterCommandEventMode.Invoke)
            {
                IFileSystem fs = args.Host.FileSystems.Docuemnt ;
                args.Result = false;
                if (args.Document.Modified)
                {
                    if (QuerySave(args) == false)
                    {
                        return;
                    }
                }
                 

                if (args.Parameter is System.IO.Stream)
                {
                    // 用户参数为一个文件流对象，则以XML的格式从这个流中加载数据
                    System.IO.Stream stream = (System.IO.Stream)args.Parameter;
                     args.EditorControl.LoadDocument(stream, FileFormat.XML);
                     args.Document.FileName = null;
                    args.Result = true;
                    args.Document.OnSelectionChanged();
                    args.Document.OnDocumentContentChanged();
                    return;
                }

                string fileName = null;
                if (args.Parameter is string)
                {
                    // 用户指定文件名了
                    fileName = (string)args.Parameter;
                    if (fileName.StartsWith("rawxml:"))
                    {
                        // 认为是原生态的XML字符串
                        fileName = fileName.Substring("rawxml:".Length);
                        System.IO.StringReader myStr = new System.IO.StringReader(fileName);
                         args.EditorControl.LoadDocument(myStr, FileFormat.XML);
                         args.Document.FileName = null;
                        args.Result = true;
                        args.Document.OnSelectionChanged();
                        args.Document.OnDocumentContentChanged();
                        return;
                    }
                }
                if (args.ShowUI)
                {
                    fileName = fs.BrowseOpen(args.Host.Services, fileName);
                    if (string.IsNullOrEmpty(fileName))
                    {
                        // 用户取消操作
                        return;
                    }
                }
                VFileInfo info = fs.GetFileInfo(args.Host.Services, fileName);
                if ( info.Exists )
                {
                    FileFormat format = WriterUtils.ParseFileFormat(info.Format);
                   System.IO.Stream stream = fs.Open( args.Host.Services , fileName );
                    if (args.Host.Debuger != null)
                    {
                        args.Host.Debuger.DebugLoadingFile(fileName);
                    }
                    if (stream != null)
                    {
                        int length = 0;
                        using (stream)
                        {
                            //args.Document.FileName = fileName;
                            args.EditorControl.Document.BaseUrl = System.IO.Path.GetDirectoryName(fileName) + "\\";
                            args.Document.FileName = fileName;
                            args.EditorControl.LoadDocument(stream, format);
                            args.Document.FileName = fileName;
                            length = (int)stream.Length;
                        }
                        if (args.Host.Debuger != null)
                        {
                            args.Host.Debuger.DebugLoadFileComplete(length);
                        }
                        args.Document.OnSelectionChanged();
                        args.Document.OnDocumentContentChanged();
                        args.Document.FileName = fileName;
                        args.Result = true;
                        args.RefreshLevel = UIStateRefreshLevel.All;
                    }
                    else
                    {
                        args.Result = false;
                        args.RefreshLevel = UIStateRefreshLevel.None;
                        return;
                    }
                    
                }
                else
                {
                    if (args.ShowUI)
                    {
                        MessageBox.Show(
                            args.EditorControl,
                            string.Format(WriterStrings.FileNotExist_FileName, fileName),
                            WriterStrings.SystemAlert,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    args.Result = false;
                }
            }
        }


        [WriterCommandDescription(StandardCommandNames.FileSave,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandSave.bmp")]
        protected void FileSave(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = args.Document != null;
            }
            if (args.Mode == WriterCommandEventMode.Invoke)
            {
                args.Result = SaveDocument(false, args);
            }
        }

        [WriterCommandDescription(StandardCommandNames.FileSaveAs,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandSave.bmp")]
        protected void FileSaveAs(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = args.Document != null;
            }
            if (args.Mode == WriterCommandEventMode.Invoke)
            {
                args.Result= SaveDocument(true, args);
            }
        }

        [WriterCommandDescription(StandardCommandNames.FileNew ,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandFileNew.bmp")]
        protected void FileNew(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.Invoke)
            {
                if (QuerySave(args))
                {
                    args.EditorControl.ClearContent();
                    //args.EditorControl.RefreshDocument();
                    args.Document.FileName = null;
                }
            }
        }



        /// <summary>
        /// 显示页面设置对话框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription( StandardCommandNames.FilePageSettings ,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandPageSettings.bmp")]
        protected void FilePageSettings(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = false;
                if (args.Document != null )
                {
                    args.Enabled = args.DocumentControler.EditorControlReadonly == false;
                }
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                args.Result = false;
                using ( dlgPageSetup dlg = new dlgPageSetup())
                {
                    dlg.PageSettings = args.Document.PageSettings.Clone();
                    if (dlg.ShowDialog( args.EditorControl) == DialogResult.OK)
                    {
                        XPageSettings ps = dlg.PageSettings ;
                        if (args.Document.BeginLogUndo())
                        {
                            args.Document.UndoList.AddProperty(
                                "PageSettings",
                                args.Document.PageSettings,
                                ps, 
                                args.Document);
                            args.Document.EndLogUndo();
                        }
                        args.Document.PageSettings = ps;
                        args.Document.UpdateContentVersion();
                        if (args.EditorControl != null)
                        {
                            args.EditorControl.RefreshDocument();
                            args.EditorControl.Invalidate();
                        }
                        args.Result = true;
                    }
                }
            }
        }

        /// <summary>
        /// 打印文档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription( StandardCommandNames.FilePrint , 
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandPrint.bmp")]
        protected void FilePrint(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = args.Document != null && args.Document.Options.BehaviorOptions.Printable ;
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                args.Result = false;
                if (args.Document.Options.BehaviorOptions.Printable == false)
                {
                    // 文档禁止打印
                    return;
                }
                DocumentPrinter printer = new DocumentPrinter( args.Document);
                if (args.EditorControl != null)
                {
                    printer.CurrentPage = args.EditorControl.CurrentPage;
                }
                printer.PrintRange = System.Drawing.Printing.PrintRange.AllPages;
                InnerPrint(args, printer, true );
            }
        }

        /// <summary>
        /// 整洁打印文档,不支持续打和打印当前页。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(StandardCommandNames.FileCleanPrint,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandPrint.bmp")]
        protected void FileCleanPrint(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = args.Document != null && args.Document.Options.BehaviorOptions.Printable;
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                args.Result = false;
                if (args.Document.Options.BehaviorOptions.Printable == false)
                {
                    // 文档禁止打印
                    return;
                }
                DocumentPrinter printer = new DocumentPrinter(args.Document);
                printer.CleanMode = true;
                if (args.EditorControl != null)
                {
                    printer.CurrentPage = args.EditorControl.CurrentPage;
                }
                printer.PrintRange = System.Drawing.Printing.PrintRange.AllPages;
                InnerPrint(args, printer, true);
            }
        }

        private void InnerPrint(
            WriterCommandEventArgs args,
            DocumentPrinter printer ,
            bool refreshDocument )
        {
            System.Windows.Forms.Cursor cur = null;
            int piBack = -1;
            if (args.EditorControl != null)
            {
                printer.WriterControl = args.EditorControl;
                cur = args.EditorControl.Cursor;
                args.EditorControl.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                if (refreshDocument)
                {
                    // 冻结用户界面
                    args.EditorControl.FreezeUI();
                }
            }
            try
            {
                args.Result = printer.PrintDocument(args.ShowUI);
            }
            finally
            {
                if (args.EditorControl != null)
                {
                    if (refreshDocument)
                    {
                        args.EditorControl.RefreshDocument();
                        if (piBack >= 0 )
                        {
                        }
                        args.EditorControl.ReleaseFreezeUI();
                    }
                    args.EditorControl.Cursor = cur;
                }
            }
        }

        /// <summary>
        /// 打印当前页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(StandardCommandNames.FilePrintCurrentPage,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandPrint.bmp")]
        protected void PrintCurrentPage(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = ( args.Document != null 
                    && args.EditorControl != null 
                    && args.Document.Options.BehaviorOptions.Printable  );
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                args.Result = false;
                if (args.Document.Options.BehaviorOptions.Printable == false)
                {
                    // 文档禁止打印
                    return;
                }
                DocumentPrinter printer = new DocumentPrinter(args.Document);
                if (args.EditorControl != null)
                {
                     printer.CurrentPage = args.EditorControl.CurrentPage;
                }
                 
                printer.PrintRange = System.Drawing.Printing.PrintRange.CurrentPage ;

                InnerPrint(args, printer, false );
            }
        }


        /// <summary>
        /// 若文档内容修改则询问用户是否保存。
        /// </summary>
        /// <param name="args">参数</param>
        /// <returns>用户确认保存</returns>
        public virtual bool QuerySave(WriterCommandEventArgs args)
        {
            if (args.ShowUI == false )
            {
                return true;
            }
            if (args.Document.Modified)
            {
                switch (MessageBox.Show(
                    args.EditorControl,
                    string.Format(WriterStrings.PromptSaveFile_Name, args.Document.FileName),
                    WriterStrings.SystemAlert,
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question))
                {
                    case DialogResult.Yes :
                        return SaveDocument(false, args);
                    case  DialogResult.No :
                        return true;
                    case  DialogResult.Cancel :
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="newFileName">使用新文件名</param>
        /// <param name="args">事件参数</param>
        /// <returns>操作是否成功</returns>
        public virtual bool SaveDocument(bool newFileName , WriterCommandEventArgs args )
        {
            string fileName = args.Document.FileName;
            IFileSystem fs = args.Host.FileSystems.Docuemnt ;
            if (args.Parameter is string)
            {
                if (newFileName == false)
                {
                    fileName = (string)args.Parameter;
                }
            }
            else if (args.Parameter is System.IO.Stream)
            {
                System.IO.Stream stream2 = (System.IO.Stream)args.Parameter;
                args.Document.Save(stream2, FileFormat.XML);
                return true;
            }
            else if (args.Parameter is System.IO.TextWriter)
            {
                System.IO.TextWriter writer = (System.IO.TextWriter)args.Parameter;
                args.Document.Save(writer, FileFormat.XML);
                return true;
            }
            if (args.ShowUI)
            {
                if (fileName == null || fileName.Trim().Length == 0 || newFileName)
                {
                    fileName = fs.BrowseSave(args.Host.Services, fileName);
                    if (string.IsNullOrEmpty(fileName))
                    {
                        return false;
                    }
                }
            }//if
            
            if (fileName == null || fileName.Trim().Length == 0)
            {
                return false;
            }

            VFileInfo info = fs.GetFileInfo(args.Host.Services, fileName);
            FileFormat format = WriterUtils.ParseFileFormat(info.Format);
            System.IO.Stream stream = fs.Save(args.Host.Services, fileName);
            if (stream != null)
            {
                using (stream)
                {
                    args.Document.Save(stream, format);
                }
                return true;
            }
            else
            {
                // 未能打开文件，保存失败。
                return false;
            }
        }

        /// <summary>
        /// 设置文档的默认字体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription( StandardCommandNames.DocumentDefaultFont )]
        protected void DocumentDefaultFont(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = false;
                if (args.Document != null)
                {
                    if (args.EditorControl != null 
                        && args.EditorControl.Readonly == false
                        && args.EditorControl.AutoSetDocumentDefaultFont == false)
                    {
                        args.Enabled = true;
                    }
                }
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                XFontValue font = new XFontValue();
                if (args.Parameter is XFontValue)
                {
                    font = (XFontValue)args.Parameter;
                }
                else if (args.Parameter is System.Drawing.Font)
                {
                    font = new XFontValue((System.Drawing.Font)args.Parameter);
                }
                else if (args.Parameter is string)
                {
                    font.Parse((string)args.Parameter);
                }
                else
                {
                    font = args.Document.DefaultStyle.Font;
                }
                System.Drawing.Color c = args.Document.DefaultStyle.Color ;
                if (args.ShowUI)
                {
                    using (FontDialog dlg = new FontDialog())
                    {
                        dlg.ShowColor = true;
                        dlg.Font = font.Value;
                        dlg.Color = c;
                        if (dlg.ShowDialog(args.EditorControl) == DialogResult.OK)
                        {
                            font = new XFontValue(dlg.Font);
                            c = dlg.Color;
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                if (args.Document.BeginLogUndo())
                {
                    DCSoft.CSharpWriter.Dom.Undo.XTextUndoSetDefaultFont undo = 
                        new Dom.Undo.XTextUndoSetDefaultFont(
                            args.EditorControl,
                            args.Document.DefaultStyle.Font,
                            args.Document.DefaultStyle.Color,
                            font,
                            c);
                    args.Document.UndoList.Add(undo);
                    args.Document.EndLogUndo();
                }
                args.EditorControl.EditorSetDefaultFont(font, c, true);
            }
        }

        /// <summary>
        /// 查看和编辑文档选项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription( StandardCommandNames.DocumentOptions )]
        protected void DocumentOptions(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled =  args.EditorControl != null;
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                if (args.ShowUI)
                {
                    using (dlgDocumentOptions dlg = new dlgDocumentOptions())
                    {
                        dlg.DocumentOptions = args.EditorControl.DocumentOptions.Clone();
                        if (dlg.ShowDialog(args.EditorControl) == DialogResult.OK)
                        {
                            args.EditorControl.DocumentOptions = dlg.DocumentOptions;
                            args.Document.HighlightManager.UpdateHighlightInfos();
                            args.EditorControl.Invalidate();
                            args.RefreshLevel = UIStateRefreshLevel.All;
                        }//if
                    }//using
                }//if
            }//else if
        }

        /// <summary>
        /// 获得文件XML代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(StandardCommandNames.ViewXMLSource)]
        protected void ViewXMLSource(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState )
            {
                args.Enabled = args.Document != null;
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                System.IO.StringWriter writer = new System.IO.StringWriter();
                DocumentSaver.SaveXmlFile( writer, args.Document);

                string xml = writer.ToString();
                if (args.ShowUI)
                {
                    using (frmViewXML frm = new frmViewXML())
                    {
                        frm.XMLSource = xml;
                        frm.ShowDialog(args.EditorControl);
                    }
                }
                args.Result = xml;
                args.RefreshLevel = UIStateRefreshLevel.None;
            }
        }

        ///// <summary>
        ///// 获得应用系统中使用的知识库对象
        ///// </summary>
        ///// <param name="host"></param>
        ///// <returns></returns>
        //private KBLibrary GetKBLibrary(WriterAppHost host)
        //{
        //    if (host == null)
        //    {
        //        return null;
        //    }
        //    IListSourceProvider lsp = (IListSourceProvider)host.Services.GetService(typeof(IListSourceProvider));
        //    if (lsp is DefaultDataProvider)
        //    {
        //        DefaultDataProvider dp = (DefaultDataProvider)lsp;
        //        if (dp.KBLibrary != null)
        //        {
        //            return dp.KBLibrary;
        //        }
        //    }
        //    return (KBLibrary)host.Services.GetService(typeof(KBLibrary));
        //}
          

    }
}
