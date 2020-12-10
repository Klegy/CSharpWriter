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
using DCSoft.CSharpWriter.Dom;
using DCSoft.CSharpWriter.Controls;
using System.Windows.Forms;
using DCSoft.Drawing;
using DCSoft.CSharpWriter.Undo;
using System.Drawing;
using DCSoft.Common;
using DCSoft.WinForms.Design;
using DCSoft.CSharpWriter.Data;
using System.ComponentModel;

namespace DCSoft.CSharpWriter.Commands
{
    /// <summary>
    /// 插入内容编辑器命令容器
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [WriterCommandDescription("Insert")]
    internal class WriterCommandModuleInsert : WriterCommandModule
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public WriterCommandModuleInsert()
        {
        }
         
        /// <summary>
        /// 调用文档元素编辑器
        /// </summary>
        /// <param name="args">编辑器命令参数对象</param>
        /// <param name="element">文档元素对象</param>
        /// <param name="method">编辑使用的方法</param>
        /// <returns>操作是否成功</returns>
        internal static bool CallElementEdtior(WriterCommandEventArgs args, DomElement element, ElementEditMethod method)
        {
            ElementEditor editor = (ElementEditor)TypeDescriptor.GetEditor(element, typeof(ElementEditor));
            if (editor != null)
            {
                ElementEditEventArgs ea = new ElementEditEventArgs();
                ea.Document = args.Document;
                ea.Host = args.Host;
                ea.LogUndo = (method == ElementEditMethod.Edit);
                ea.ParentWindow = args.EditorControl;
                ea.Element = element;
                ea.Method = method;
                return editor.Edit(ea);
            }
            return false;
        }

        /// <summary>
        /// 插入文件内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(StandardCommandNames.InsertFileContent)]
        protected void InsertFileContent(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = args.DocumentControler != null
                    && args.Document != null
                    && args.DocumentControler.CanInsertElementAtCurrentPosition(
                    typeof(DomElement));
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                args.Result = false;
                DomDocument document = null;
                string fileName = null;
                if (args.Parameter is string)
                {
                    fileName = (string)args.Parameter;
                }
                else if (args.Parameter is DomDocument)
                {
                    document = (DomDocument)args.Parameter;
                }
                if (document == null)
                {
                    IFileSystem fs = args.Host.FileSystems.Docuemnt;
                    if (fs != null)
                    {
                        if (args.ShowUI)
                        {
                            // 浏览文件
                            fileName = fs.BrowseOpen(args.Host.Services, fileName);
                        }
                        if (string.IsNullOrEmpty(fileName))
                        {
                            return;
                        }
                        VFileInfo info = fs.GetFileInfo(args.Host.Services, fileName);
                        if (info.Exists == false)
                        {
                            // 文件不存在
                            return;
                        }
                        //打开文件
                        if (args.Host.Debuger != null)
                        {
                            args.Host.Debuger.DebugLoadingFile(fileName);
                        }
                        System.IO.Stream stream = fs.Open(args.Host.Services, fileName);
                        if (stream != null)
                        {
                            FileFormat format = WriterUtils.ParseFileFormat(info.Format);
                            using (stream)
                            {
                                // 读取文件，加载文档对象
                                document = new DomDocument();
                                document.Options = args.Document.Options;
                                
                                document.ServerObject = args.Document.ServerObject;
                                document.Load(stream, format);
                                if (args.Host.Debuger != null)
                                {
                                    args.Host.Debuger.DebugLoadFileComplete((int)stream.Length);
                                }
                            }
                        }
                    }
                }
                if (document != null
                    && document.Body != null
                    && document.Body.Elements.Count > 0)
                {
                    // 导入文档内容
                    DomElementList list = document.Body.Elements;
                    args.Document.ImportElements(list);
                    args.DocumentControler.InsertElements(list);
                    args.Result = list;
                }
            }
        }
         
        /// <summary>
        /// 向文档的当前位置插入XML内容。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(StandardCommandNames.InsertXML)]
        protected void InsertXML(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = args.DocumentControler != null
                    && args.Document != null
                    && args.DocumentControler.CanInsertElementAtCurrentPosition(
                    typeof(DomElement));
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                args.Result = false;

                DomDocument document = null;
                if (args.Parameter is string)
                {
                    System.IO.StringReader reader = new System.IO.StringReader(
                        (string)args.Parameter);
                    document = DocumentLoader.LoadXmlFileWithCreateDocument(
                        reader,
                        args.Document);
                    reader.Close();
                }
                else if (args.Parameter is System.IO.Stream)
                {
                    document = DocumentLoader.LoadXmlFileWithCreateDocument(
                        (System.IO.Stream)args.Parameter,
                        args.Document);
                }
                else if (args.Parameter is System.IO.TextReader)
                {
                    document = DocumentLoader.LoadXmlFileWithCreateDocument(
                        (System.IO.TextReader)args.Parameter,
                        args.Document);
                }
                else if (args.Parameter is System.Xml.XmlReader)
                {
                    document = DocumentLoader.LoadXmlFileWithCreateDocument(
                        (System.Xml.XmlReader)args.Parameter,
                        args.Document);
                }
                if (document != null
                    && document.Body != null
                    && document.Body.Elements.Count > 0)
                {
                    DomElementList list = document.Body.Elements;
                    args.Document.ImportElements(list);
                    args.DocumentControler.InsertElements(list);
                    args.Result = list;
                }
            }
        }

        /// <summary>
        /// 插入RTF文本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(StandardCommandNames.InsertRTF)]
        protected void InsertRTF(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = args.DocumentControler != null
                    && args.DocumentControler.CanInsertElementAtCurrentPosition(typeof(DomElement));
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                args.Result = false;
                InsertRTFCommandParameter parameter = null;
                if (args.Parameter is InsertRTFCommandParameter)
                {
                    parameter = (InsertRTFCommandParameter)args.Parameter;
                }
                else
                {
                    parameter = new InsertRTFCommandParameter();
                    if (args.Parameter != null)
                    {
                        parameter.RTFText = Convert.ToString(args.Parameter);
                    }
                }
                if (args.ShowUI)
                {
                    using (dlgInputRTF dlg = new dlgInputRTF())
                    {
                        dlg.InputRTFText = parameter.RTFText;
                        if (dlg.ShowDialog(args.EditorControl) == DialogResult.OK)
                        {
                            parameter.RTFText = dlg.InputRTFText;
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                if (string.IsNullOrEmpty(parameter.RTFText) == false)
                {
                    args.Result = args.DocumentControler.InsertRTF(parameter.RTFText);
                }
            }
        }

        /// <summary>
        /// 插入纯文本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(StandardCommandNames.InsertString)]
        protected void InsertString(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = args.DocumentControler != null
                    && args.DocumentControler.CanInsertElementAtCurrentPosition(
                    typeof(DomCharElement));
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                args.Result = 0;
                InsertStringCommandParameter parameter = null;
                if (args.Parameter is InsertStringCommandParameter)
                {
                    parameter = (InsertStringCommandParameter)args.Parameter;
                }
                else
                {
                    parameter = new InsertStringCommandParameter();
                    if (args.Parameter != null)
                    {
                        parameter.Text = Convert.ToString(args.Parameter);
                    }
                }
                if (args.ShowUI)
                {
                    using (dlgInputString dlg = new dlgInputString())
                    {
                        dlg.InputText = parameter.Text;
                        if (dlg.ShowDialog(args.EditorControl) == DialogResult.OK)
                        {
                            parameter.Text = dlg.InputText;
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                if (string.IsNullOrEmpty(parameter.Text) == false)
                {
                    args.Result = args.DocumentControler.InsertString(parameter.Text);
                }
            }
        }


        /// <summary>
        /// 插入软回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(StandardCommandNames.InsertLineBreak,
            ShortcutKey = Keys.Shift | Keys.Enter)]
        protected void InsertLineBreak(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = (args.DocumentControler != null
                    && args.DocumentControler.CanInsertElementAtCurrentPosition(typeof(DomLineBreakElement)));
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                args.Result = args.DocumentControler.InsertLineBreak();
                args.RefreshLevel = UIStateRefreshLevel.All;

            }
        }

    }
}
