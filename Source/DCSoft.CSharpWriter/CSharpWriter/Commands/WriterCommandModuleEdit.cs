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
    /// 编辑文档内容的功能模块
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [WriterCommandDescription(StandardCommandNames.ModuleEdit)]
    internal class WriterCommandModuleEdit : WriterCommandModule
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public WriterCommandModuleEdit()
        {
        }


        /// <summary>
        /// 执行退格删除操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(StandardCommandNames.Backspace, ShortcutKey = Keys.Back)]
        protected void Backspace(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                // 获得动作状态
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                // 执行动作
                args.EditorControl.DocumentControler.Backspace();
                args.RefreshLevel = UIStateRefreshLevel.All;
            }
        }


        /// <summary>
        /// 复制内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(
            StandardCommandNames.Copy,
            ShortcutKey = Keys.Control | Keys.C,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandCopy.bmp")]
        protected void Copy(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = (args.DocumentControler != null
                    && args.DocumentControler.CanCopy);
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                args.DocumentControler.Copy();
                args.RefreshLevel = UIStateRefreshLevel.All;
            }
        }

        /// <summary>
        /// 粘贴操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(
            StandardCommandNames.Cut,
            ShortcutKey = Keys.Control | Keys.X,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandCut.bmp")]
        protected void Cut(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = (args.DocumentControler != null
                    && args.DocumentControler.CanCut());
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                args.DocumentControler.Cut();
                args.RefreshLevel = UIStateRefreshLevel.All;
            }
        }

        ///// <summary>
        ///// 执行删除操作
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="args"></param>
        //[WriterCommandDescription(
        //    StandardCommandNames.DeleteSelectionOld,
        //    ShortcutKey = Keys.Delete,
        //    ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandDelete.bmp")]
        //protected void DeleteSelectionOld(object sender, WriterCommandEventArgs args)
        //{
        //    if (args.Mode == WriterCommandEventMode.QueryState)
        //    {
        //        args.Enabled = (args.DocumentControler != null
        //            && args.DocumentControler.CanDeleteSelection);
        //    }
        //    else if (args.Mode == WriterCommandEventMode.Invoke)
        //    {
        //        args.DocumentControler.DeleteSelectionOld();
        //        args.RefreshLevel = UIStateRefreshLevel.All;
        //    }
        //}

        /// <summary>
        /// 执行删除操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(
            StandardCommandNames.Delete,
            ShortcutKey = Keys.Delete,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandDelete.bmp")]
        protected void Delete(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = (args.DocumentControler != null
                    && args.DocumentControler.Snapshot.CanDeleteSelection);
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                args.DocumentControler.Delete();
                args.RefreshLevel = UIStateRefreshLevel.All;
            }
        }
         
        /// <summary>
        /// 设置修改、插入模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(StandardCommandNames.InsertMode, ShortcutKey = Keys.Insert)]
        protected void InsertMode(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = (args.Document != null);
                args.Checked = args.EditorControl.InsertMode;
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                if (args.Parameter is bool)
                {
                    args.EditorControl.InsertMode = (bool)args.Parameter;
                }
                else
                {
                    args.EditorControl.InsertMode = !args.EditorControl.InsertMode;
                }
                args.EditorControl.UpdateTextCaret();
            }
        }

        /// <summary>
        /// 粘贴操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(
            StandardCommandNames.Paste,
            ShortcutKey = Keys.Control | Keys.V,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandPaste.bmp")]
        protected void Paste(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = (args.DocumentControler != null && args.DocumentControler.CanPaste);
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                args.DocumentControler.Paste();
                args.RefreshLevel = UIStateRefreshLevel.All;
            }
        }

        /// <summary>
        /// 选择性粘贴操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(
            StandardCommandNames.SpecifyPaste,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandPaste.bmp")]
        protected void SpecifyPaste(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = (args.DocumentControler != null && args.DocumentControler.CanPaste);
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                string specifyFormat = args.Parameter as string;
                if (args.ShowUI)
                {
                    using (dlgSpecifyPaste dlg = new dlgSpecifyPaste())
                    {
                        dlg.Document = args.Document;
                        dlg.DataObject = System.Windows.Forms.Clipboard.GetDataObject();
                        dlg.ResultFormat = specifyFormat;
                        if (dlg.ShowDialog(args.EditorControl) == DialogResult.OK)
                        {
                            specifyFormat = dlg.ResultFormat;
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                if (string.IsNullOrEmpty(specifyFormat) == false)
                {
                    args.DocumentControler.Paste(specifyFormat);
                    args.RefreshLevel = UIStateRefreshLevel.All;
                }
            }
        }

        /// <summary>
        /// 重复执行操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(
            StandardCommandNames.Redo,
            ShortcutKey = Keys.Control | Keys.Y,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandRedo.bmp")]
        protected void Redo(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = false;
                if (args.Document != null)
                {
                    args.Enabled = args.DocumentControler.EditorControlReadonly == false
                        && args.Document.UndoList != null
                        && args.Document.UndoList.CanRedo;
                }
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                XUndoEventArgs e = new XUndoEventArgs();
                args.Document.UndoList.Redo(e);
                args.RefreshLevel = UIStateRefreshLevel.All;
            }
        }

        /// <summary>
        /// 重复执行操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(
            StandardCommandNames.Undo,
            ShortcutKey = Keys.Control | Keys.Z,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandUndo.bmp")]
        protected void Undo(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = false;
                if (args.Document != null)
                {
                    args.Enabled = args.DocumentControler.EditorControlReadonly == false
                        && args.Document.UndoList != null
                        && args.Document.UndoList.CanUndo;
                }
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                XUndoEventArgs e = new XUndoEventArgs();
                args.Document.UndoList.Undo(e);
                args.RefreshLevel = UIStateRefreshLevel.All;
            }
        }
          

        /// <summary>
        /// 编辑元素属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(StandardCommandNames.ElementProperties ,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandElementProperties.bmp")]
        protected void ElementProperties(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = args.DocumentControler != null
                    && args.DocumentControler.CanSetStyle;
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                args.Result = false;
                DomElement element = args.Document.CurrentElement;
                if (args.Document.Selection.ContentElements.Count == 1)
                {
                    element = args.Document.Selection.ContentElements[0];
                }

                while (element != null)
                {
                    ElementEditor editor = (ElementEditor)TypeDescriptor.GetEditor(element, typeof(ElementEditor));
                    if (editor != null)
                    {
                        if (editor.IsSupportMethod(ElementEditMethod.Edit))
                        {
                            // 调用指定的编辑器
                            ElementEditEventArgs ea = new ElementEditEventArgs();
                            ea.Document = args.Document;
                            ea.Element = element;
                            ea.Host = args.Host;
                            ea.LogUndo = true;
                            ea.Method = ElementEditMethod.Edit;
                            bool changed = false;
                            args.Document.BeginLogUndo();
                            if (editor.Edit(ea))
                            {
                                // 更新元素内容版本号
                                if (element is DomContainerElement)
                                {
                                    DomContainerElement c = (DomContainerElement)element;
                                    c.EditorInvalidateContent();
                                }
                                element.UpdateContentVersion();
                                element.InvalidateView();
                                changed = true;
                            }
                            args.Document.EndLogUndo();
                            if (changed)
                            {
                                args.Result = true;
                                args.Document.Modified = true;
                                //args.Document.OnSelectionChanged();
                                args.Document.OnDocumentContentChanged();
                                args.RefreshLevel = UIStateRefreshLevel.All;
                            }
                        }
                        return;
                    }
                    element = element.Parent;
                }//while

                element = args.Document.CurrentElement;
                if (args.Document.Selection.ContentElements.Count == 1)
                {
                    element = args.Document.Selection.ContentElements[0];
                }

                XTextElementProperties properties = null;
                while (element != null)
                {
                    properties = args.Host.CreateProperties(element.GetType());
                    if (properties != null)
                    {
                        break;
                    }
                    element = element.Parent;
                }//while
                if (properties != null)
                {
                    properties.Document = args.Document;
                    if (properties.ReadProperties(element))
                    {
                        args.SourceElement = element;
                        if (properties.PromptEditProperties(args))
                        {
                            args.Document.BeginLogUndo();
                            bool changed = false;
                            if( properties.ApplyToElement(args.Document, element, true) )
                            {
                                // 更新元素内容版本号
                                if (element is DomContainerElement)
                                {
                                    DomContainerElement c = (DomContainerElement)element;
                                    c.EditorInvalidateContent();
                                }
                                element.UpdateContentVersion();
                                element.InvalidateView();
                                changed = true;
                            }
                            args.Document.EndLogUndo();
                            if (changed)
                            {
                                args.Result = true;
                                args.Document.Modified = true;
                                //args.Document.OnSelectionChanged();
                                args.Document.OnDocumentContentChanged();
                                args.RefreshLevel = UIStateRefreshLevel.All;
                            }
                        }
                    }
                }
            }
        }



        /// <summary>
        /// 查找和替换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(StandardCommandNames.SearchReplace ,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandSearch.bmp",
            ShortcutKey=Keys.Control | Keys.F )]
        protected void SearchReplace(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = args.EditorControl != null && args.Document != null;
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                SearchReplaceCommandArgs cmdArgs = args.Parameter as SearchReplaceCommandArgs;
                if (args.ShowUI)
                {
                    // 显示查找、替换操作对话框
                    dlgSearch dlg = ( dlgSearch ) args.EditorControl.ToolWindows[ typeof( dlgSearch )];
                    if (dlg == null)
                    {
                        dlg = new dlgSearch();
                        args.EditorControl.ToolWindows.Add(dlg);
                        dlg.Owner = args.EditorControl.FindForm();
                        dlg.WriterControl = args.EditorControl;
                    }
                    if (dlg != null)
                    {
                        if (dlg.Visible == false)
                        {
                            if (cmdArgs == null)
                            {
                                cmdArgs = new SearchReplaceCommandArgs();
                            }
                            if (args.Document.Selection.Length != 0)
                            {
                                cmdArgs.SearchString = args.Document.Selection.Text;
                            }
                            dlg.CommandArgs = cmdArgs;

                            dlg.Show(args.EditorControl);
                            dlg.UpdateUIState();
                        }
                        else
                        {
                            if (cmdArgs != null)
                            {
                                dlg.CommandArgs = cmdArgs;
                                dlg.UpdateUIState();
                            }
                            else
                            {
                                if (args.Document.Selection.Length != 0)
                                {
                                    dlg.CommandArgs.SearchString = args.Document.Selection.Text;
                                    dlg.UpdateUIState();
                                }
                            }
                            dlg.Focus();
                        }
                    }
                }
                else
                {

                }
            }
        }
         

        /// <summary>
        /// 设置文档默认样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription( StandardCommandNames.SetDefaultStyle )]
        protected void SetDefaultStyle(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = false;
                if (args.Document != null && args.EditorControl.Readonly == false)
                {
                    args.Enabled = true;
                }
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                DocumentContentStyle style = null;
                if (args.Parameter is string)
                {
                    style = new DocumentContentStyle();
                    style.DisableDefaultValue = true;
                    string values = (string)args.Parameter;
                    AttributeString attrs = new AttributeString(values);
                    bool setFlag = false;
                    foreach (AttributeStringItem item in attrs)
                    {
                        XDependencyProperty p = XDependencyProperty.GetProperty(
                            typeof(DocumentContentStyle),
                            item.Name);
                        if (p != null)
                        {
                            Type pt = p.PropertyType;
                            try
                            {
                                if (pt.Equals(typeof(string)))
                                {
                                    style.SetValue(p, item.Value);
                                }
                                else
                                {
                                    TypeConverter tc = TypeDescriptor.GetConverter(pt);
                                    if (tc != null && tc.CanConvertFrom(typeof(string)))
                                    {
                                        object v = tc.ConvertFrom(item.Value);
                                        style.SetValue(p, v);
                                    }
                                    else
                                    {
                                        object v = Convert.ChangeType(item.Value, pt);
                                        style.SetValue(p, v);
                                    }
                                }
                                setFlag = true;
                            }
                            catch { }
                        }//p
                    }//foreach
                    if (setFlag == false )
                    {
                        // 输入的数据不对，退出处理
                        style = null;
                    }
                }//if
                else if (args.Parameter is DocumentContentStyle)
                {
                    style = (DocumentContentStyle)args.Parameter;
                }
                if (style != null)
                {
                    args.Document.EditorSetDefaultStyle( style , true );
                    args.RefreshLevel = UIStateRefreshLevel.All;
                }
            }//else
        }
    }
}