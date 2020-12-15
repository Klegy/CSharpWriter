/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
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
    /// 工具类型的编辑器命令容器
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [WriterCommandDescription("Tools")]
    internal class WriterCommandModuleTools : CSWriterCommandModule
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public WriterCommandModuleTools()
        {
        }
         

        /// <summary>
        /// 编辑VBA脚本代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(StandardCommandNames.EditVBAScript,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandEditVBAScript.bmp")]
        protected void EditVBAScript(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = false;
                if (args.Document != null)
                {
                    args.Enabled = true;
                }
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                args.Result = false;
                string script = args.Document.ScriptText;
                if (args.Parameter is string)
                {
                    if (string.IsNullOrEmpty((string)args.Parameter) == false)
                    {
                        script = (string)args.Parameter;
                    }
                }

                if (args.ShowUI)
                {
                    using (DCSoft.CSharpWriter.Script.frmScriptEdtior frm = new Script.frmScriptEdtior())
                    {
                        frm.Document = args.Document;
                        frm.ScriptText = script;
                        if (frm.ShowDialog(args.EditorControl) == DialogResult.OK)
                        {
                            script = frm.ScriptText;
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                if (args.DocumentControler.EditorControlReadonly == false)
                {
                    if (args.Document.ScriptText != script)
                    {
                        // 记录撤销信息
                        if (args.Document.BeginLogUndo())
                        {
                            args.Document.UndoList.AddProperty(
                                "ScriptText",
                                args.Document.ScriptText,
                                script,
                                args.Document);
                            args.Document.EndLogUndo();
                        }
                        args.Document.ScriptText = script;
                        args.Document.UpdateContentVersion();
                        args.Document.Modified = true;
                        args.Result = true;
                        args.Document.OnDocumentContentChanged();
                    }
                }
            }
        }

         
        /// <summary>
        /// 显示文档字数统计信息
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="args">参数</param>
        [WriterCommandDescription(StandardCommandNames.WordCount,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandWordCount.bmp")]
        protected void WordCount(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = args.Document != null;
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                if (args.Document != null)
                {
                    DomElementList list = new DomElementList();
                    if (args.Document.Selection.Length != 0)
                    {
                        // 计算被选择区域
                        list = args.Document.Selection.ContentElements.Clone();
                    }
                    else
                    {
                        // 计算整个文档
                        foreach (DomDocumentContentElement ce in args.Document.Elements)
                        {
                            if (ce.IsEmpty == false)
                            {
                                list.AddRange(ce.Content);
                            }
                        }
                    }
                    WordCountResult result = new WordCountResult(args.Document, list);
                    args.Result = result;
                    if (args.ShowUI)
                    {
                        using (dlgWordCount dlg = new dlgWordCount())
                        {
                            dlg.CountResult = result;
                            dlg.ShowDialog(args.EditorControl);
                        }
                    }
                    args.RefreshLevel = UIStateRefreshLevel.None;
                }
            }
        }


    }
}
