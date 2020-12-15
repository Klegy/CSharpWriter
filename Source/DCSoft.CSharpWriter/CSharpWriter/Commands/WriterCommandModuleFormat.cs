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
    /// 文档内容格式命令模块
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [WriterCommandDescription( "Format")]
    internal class WriterCommandModuleFormat : CSWriterCommandModule
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public WriterCommandModuleFormat()
        {
        }


        /// <summary>
        /// 设置段落格式
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="args">参数</param>
        [WriterCommandDescription(StandardCommandNames.ParagraphFormat)]
        protected void ParagraphFormat(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = args.DocumentControler != null
                    && args.DocumentControler.Snapshot.CanModifySelection;
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                args.Result = false;
                ParagraphFormatCommandParameter parameter = args.Parameter
                    as ParagraphFormatCommandParameter;
                if (parameter == null)
                {
                    parameter = new ParagraphFormatCommandParameter();
                    parameter.Read(args.Document.CurrentParagraphStyle);
                }
                if (args.ShowUI)
                {
                    using (dlgParagraphFormatcs dlg = new dlgParagraphFormatcs())
                    {
                        dlg.CommandParameter = parameter;
                        if (dlg.ShowDialog(args.EditorControl) != DialogResult.OK)
                        {
                            // 用户取消操作
                            return;
                        }
                    }
                }//if
                DocumentContentStyle ns = new DocumentContentStyle();
                ns.DisableDefaultValue = true;
                parameter.Save(ns);
                args.Document.BeginLogUndo();
                DomElementList list = args.Document.Selection.SetParagraphStyle(ns);
                if (list != null && list.Count > 0)
                {
                    args.Result = true;
                }
                args.Document.EndLogUndo();
                args.Document.OnSelectionChanged();
                args.Document.OnDocumentContentChanged();
                args.RefreshLevel = UIStateRefreshLevel.All;
            }
        }

        /// <summary>
        /// 边框和背景样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(StandardCommandNames.BorderBackgroundFormat)]
        protected void BorderBackgroundFormat(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = false;
                if (args.DocumentControler.Snapshot.CanModifySelection)
                {
                    if (Math.Abs(args.Document.Selection.Length) == 1)
                    {
                        DomElement element = args.Document.Selection.ContentElements[0];
                        if (element is DomObjectElement)
                        {
                            args.Enabled = true;
                            return;
                        }
                    }
                    if (args.Document.Selection.Mode == ContentRangeMode.Cell)
                    {
                        args.Enabled = true;
                    }
                    

                }
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                DomElement simpleElement = null;
                if (Math.Abs(args.Document.Selection.Length) == 1)
                {
                    if (args.Document.Selection.ContentElements[0] is DomObjectElement)
                    {
                        simpleElement = args.Document.Selection.ContentElements[0];
                    }
                }
                BorderBackgroundCommandParameter parameter = args.Parameter
                    as BorderBackgroundCommandParameter;
                if (parameter == null && args.ShowUI == false)
                {
                    // 操作无意义
                    return;
                }
                 
                if (args.ShowUI)
                {
                    using (dlgDocumentBorderBackground dlg = new dlgDocumentBorderBackground())
                    {
                        dlg.CommandParameter = parameter;
                        if (simpleElement != null)
                        {
                            dlg.CompleMode = false;
                        }
                        if (dlg.ShowDialog(args.EditorControl) == DialogResult.OK)
                        {
                        }
                        else
                        {
                            // 用户取消操作
                            return;
                        }
                    }
                }
                args.Result = false;
                
                    SetElementBorderBackgroundFormat(parameter, args, simpleElement);
                 
            }
        }
          
        /// <summary>
        /// 设置表格单元格的边框和背景样式
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <param name="args">参数</param>
        private void SetElementBorderBackgroundFormat(
            BorderBackgroundCommandParameter parameter,
            WriterCommandEventArgs args,
            DomElement element)
        {
            DocumentContentStyle rs = (DocumentContentStyle)element.Style.Clone();
            bool modified = false;
            bool globalModified = false;
            if (rs.BorderLeft != parameter.LeftBorder)
            {
                rs.BorderLeft = parameter.LeftBorder;
                modified = true;
            }
            if (rs.BorderTop != parameter.TopBorder)
            {
                rs.BorderTop = parameter.TopBorder;
                modified = true;
            }
            if (rs.BorderRight != parameter.RightBorder)
            {
                rs.BorderRight = parameter.RightBorder;
                modified = true;
            }
            if (rs.BorderBottom != parameter.BottomBorder)
            {
                rs.BorderBottom = parameter.BottomBorder;
                modified = true;
            }
            if (rs.BorderColor != parameter.BorderColor)
            {
                rs.BorderColor = parameter.BorderColor;
                modified = true;
            }
            if (rs.BorderStyle != parameter.BorderStyle)
            {
                rs.BorderStyle = parameter.BorderStyle;
                modified = true;
            }
            if (rs.BorderWidth != parameter.BorderWidth)
            {
                rs.BorderWidth = parameter.BorderWidth;
                modified = true;
            }
            if (rs.BackgroundColor != parameter.BackgroundColor)
            {
                rs.BackgroundColor = parameter.BackgroundColor;
                modified = true;
            }
            args.Document.BeginLogUndo();
            if (modified)
            {
                globalModified = true;
                int newStyleIndex = args.Document.ContentStyles.GetStyleIndex(rs);
                if (newStyleIndex != element.StyleIndex)
                {
                    if (args.Document.CanLogUndo)
                    {
                        args.Document.UndoList.AddStyleIndex(element, element.StyleIndex, newStyleIndex);
                    }
                    element.StyleIndex = newStyleIndex;
                    if (element.ShadowElement != null)
                    {
                        if (args.Document.CanLogUndo)
                        {
                            args.Document.UndoList.AddStyleIndex(element.ShadowElement, element.ShadowElement.StyleIndex, newStyleIndex);
                        }
                        element.ShadowElement.StyleIndex = newStyleIndex;
                        element.ShadowElement.InvalidateView();
                    }
                    element.InvalidateView();
                    element.UpdateContentVersion();
                }
            }
            args.Document.EndLogUndo();
            if (globalModified)
            {
                args.Result = true;
                args.Document.Modified = true;
                //args.Document.UpdateContentVersion();
                args.Document.OnDocumentContentChanged();
            }
        }
          
        /// <summary>
        /// 设置背景颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(StandardCommandNames.BackColor,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandBackColor.bmp")]
        protected void BackColor(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = args.DocumentControler != null
                    && args.DocumentControler.Snapshot.CanModifySelection;
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                Color color = System.Drawing.Color.Transparent;
                if (args.Parameter is Color)
                {
                    color = (Color)args.Parameter;
                }
                if (args.ShowUI)
                {
                    using (ColorDialog dlg = new ColorDialog())
                    {
                        dlg.Color = color;
                        if (dlg.ShowDialog(args.EditorControl) == DialogResult.OK)
                        {
                            color = dlg.Color;
                        }
                        else
                        {
                            return;
                        }
                    }//using
                }
                args.Parameter = color;
                SetStyleProperty(sender, args, StandardCommandNames.BackColor);
            }
        }

        /// <summary>
        /// 设置文本颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(StandardCommandNames.Color,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandColor.bmp")]
        protected void ColorFunction(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = args.DocumentControler != null
                     && args.DocumentControler.Snapshot.CanModifySelection;
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                DocumentContentStyle cs = GetCurrentStyle(args.Document);
                Color color = Color.Black;
                if (args.Parameter is Color)
                {
                    color = (Color)args.Parameter;
                }
                else
                {
                    color = cs.Color;
                }
                if (args.ShowUI)
                {
                    using (ColorDialog dlg = new ColorDialog())
                    {
                        dlg.Color = color;
                        if (dlg.ShowDialog(args.EditorControl) == DialogResult.OK)
                        {
                            color = dlg.Color;
                        }
                        else
                        {
                            return;
                        }
                    }//using
                }
                args.Parameter = color;
                SetStyleProperty(sender, args, StandardCommandNames.Color);
            }
        }

        /// <summary>
        /// 设置字体名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(StandardCommandNames.FontName)]
        protected void FontName(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.InitalizeUIElement)
            {
                if (args.UIElement is ToolStripComboBox)
                {
                    ToolStripComboBox cbo = (ToolStripComboBox)args.UIElement;
                    if (cbo.Items.Count == 0)
                    {
                        foreach (FontFamily f in FontFamily.Families)
                        {
                            cbo.Items.Add(f.Name);
                        }
                    }
                }
                else if (args.UIElement is ComboBox)
                {
                    ComboBox cbo = (ComboBox)args.UIElement;
                    if (cbo.Items.Count == 0)
                    {
                        foreach (FontFamily f in FontFamily.Families)
                        {
                            cbo.Items.Add(f.Name);
                        }
                    }
                }
            }
            else if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = args.DocumentControler != null
                             && args.DocumentControler.Snapshot.CanModifySelection;
                args.Parameter = GetCurrentStyle(args.Document).FontName;
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                SetStyleProperty(sender, args, StandardCommandNames.FontName);
                args.RefreshLevel = UIStateRefreshLevel.Current;
                if (args.EditorControl != null)
                {
                    args.EditorControl.Focus();
                }
            }
        }

        /// <summary>
        /// 设置字体大小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(StandardCommandNames.FontSize)]
        protected void FontSize(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.InitalizeUIElement)
            {
                if (args.UIElement is ToolStripComboBox)
                {
                    ToolStripComboBox cbo = (ToolStripComboBox)args.UIElement;
                    if (cbo.Items.Count == 0)
                    {
                        foreach (FontSizeInfo info in FontSizeInfo.StandSizes)
                        {
                            cbo.Items.Add(info.Name);
                        }
                    }
                }
                if (args.UIElement is ComboBox)
                {
                    ComboBox cbo = (ComboBox)args.UIElement;
                    if (cbo.Items.Count == 0)
                    {
                        foreach (FontSizeInfo info in FontSizeInfo.StandSizes)
                        {
                            cbo.Items.Add(info.Name);
                        }
                    }
                }
            }
            else if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = args.DocumentControler != null
                            && args.DocumentControler.Snapshot.CanModifySelection;
                args.Parameter = FontSizeInfo.GetStandSizeName(GetCurrentStyle(args.Document).FontSize);
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                SetStyleProperty(sender, args, StandardCommandNames.FontSize);
                if (args.EditorControl != null)
                {
                    args.EditorControl.Focus();
                }
            }
        }

        /// <summary>
        /// 设置字体样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(StandardCommandNames.Font,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandFont.bmp")]
        protected void Font(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = args.DocumentControler != null
                                 && args.DocumentControler.Snapshot.CanModifySelection;
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                //MessageBox.Show(Convert.ToString(args.Parameter));
                //System.Diagnostics.Debugger.Break();
                DocumentContentStyle cs = GetCurrentStyle(args.Document);
                XFontValue font = new XFontValue();
                if (args.Parameter == null)
                {
                    font = cs.Font;
                    //MessageBox.Show("1:" + font.Size);
                }
                else if (args.Parameter is XFontValue)
                {
                    font = ((XFontValue)args.Parameter).Clone();
                    //MessageBox.Show("2:" + font.Size);
                }
                else if (args.Parameter is System.Drawing.Font)
                {
                    font = new XFontValue((System.Drawing.Font)args.Parameter);
                    //MessageBox.Show("3:" + font.Size);
                }
                else
                {
                    // 未知参数
                    font = cs.Font;
                }
                if (args.ShowUI)
                {
                    using (FontDialog dlg = new FontDialog())
                    {
                        dlg.Font = font.Value;
                        if (dlg.ShowDialog(args.EditorControl) == DialogResult.OK)
                        {
                            font = new XFontValue(dlg.Font);
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                args.Parameter = font;
                SetStyleProperty(sender, args, StandardCommandNames.Font);
            }
        }

        /// <summary>
        /// 设置下划线样式
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="args">参数</param>
        [WriterCommandDescription(
            StandardCommandNames.Underline,
            ShortcutKey = Keys.Control | Keys.U,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandUnderline.bmp")]
        protected void Underline(object sender, WriterCommandEventArgs args)
        {
            SetStyleProperty(sender, args, StandardCommandNames.Underline);
        }


        /// <summary>
        /// 设置斜体模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(
            StandardCommandNames.Italic,
            ShortcutKey = Keys.Control | Keys.I,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandItalic.bmp")]
        protected void Italic(object sender, WriterCommandEventArgs args)
        {
            SetStyleProperty(sender, args, StandardCommandNames.Italic);
        }

        /// <summary>
        /// 设置或取消粗体样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(
            StandardCommandNames.Bold,
            ShortcutKey = Keys.Control | Keys.B,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandBold.bmp")]
        protected void Bold(object sender, WriterCommandEventArgs args)
        {
            SetStyleProperty(sender, args, StandardCommandNames.Bold);
        }

        /// <summary>
        /// 段落左对齐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(
            StandardCommandNames.AlignLeft,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandAlignLeft.bmp")]
        protected void AlignLeft(object sender, WriterCommandEventArgs args)
        {
            SetParagraphStyleProperty(sender, args, StandardCommandNames.AlignLeft);
        }

        /// <summary>
        /// 段落左对齐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(
            StandardCommandNames.AlignCenter,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandAlignCenter.bmp")]
        protected void AlignCenter(object sender, WriterCommandEventArgs args)
        {
            SetParagraphStyleProperty(sender, args, StandardCommandNames.AlignCenter);
        }

        /// <summary>
        /// 段落左对齐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(
            StandardCommandNames.AlignRight,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandAlignRight.bmp")]
        protected void AlignRight(object sender, WriterCommandEventArgs args)
        {
            SetParagraphStyleProperty(sender, args, StandardCommandNames.AlignRight);
        }

        /// <summary>
        /// 段落左对齐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(
            StandardCommandNames.AlignJustify,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandAlignJustify.bmp")]
        protected void AlignJustify(object sender, WriterCommandEventArgs args)
        {
            SetParagraphStyleProperty(sender, args, StandardCommandNames.AlignJustify);
        }

        /// <summary>
        /// 设置段落的左边框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(
            StandardCommandNames.BorderLeft,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandBorderLeft.bmp")]
        protected void BorderLeft(object sender, WriterCommandEventArgs args)
        {
            SetParagraphStyleProperty(sender, args, StandardCommandNames.BorderLeft);
        }

        /// <summary>
        /// 段落左对齐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(
            StandardCommandNames.BorderTop,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandBorderTop.bmp")]
        protected void BorderTop(object sender, WriterCommandEventArgs args)
        {
            SetParagraphStyleProperty(sender, args, StandardCommandNames.BorderTop);
        }

        /// <summary>
        /// 段落左对齐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(
            StandardCommandNames.BorderRight,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandBorderRight.bmp")]
        protected void BorderRight(object sender, WriterCommandEventArgs args)
        {
            SetParagraphStyleProperty(sender, args, StandardCommandNames.BorderRight);
        }

        /// <summary>
        /// 段落左对齐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(
            StandardCommandNames.BorderBottom,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandBorderBottom.bmp")]
        protected void BorderBottom(object sender, WriterCommandEventArgs args)
        {
            SetParagraphStyleProperty(sender, args, StandardCommandNames.BorderBottom);
        }

        /// <summary>
        /// 设置/取消删除线样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(
            StandardCommandNames.Strikeout)]
        protected void Strikeout(object sender, WriterCommandEventArgs args)
        {
            SetStyleProperty(sender, args, StandardCommandNames.Strikeout);
        }

        /// <summary>
        /// 设置段落的数字列表样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(StandardCommandNames.NumberedList,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandNumberedList.bmp")]
        protected void NumberedList(object sender, WriterCommandEventArgs args)
        {
            SetParagraphStyleProperty(sender, args, StandardCommandNames.NumberedList);
        }

        /// <summary>
        /// 设置段落的原点列表样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(StandardCommandNames.BulletedList,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandBulletedList.bmp")]
        protected void BulletedList(object sender, WriterCommandEventArgs args)
        {
            SetParagraphStyleProperty(sender, args, StandardCommandNames.BulletedList);
        }

        /// <summary>
        /// 段落首行缩进
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(StandardCommandNames.FirstLineIndent,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandFirstLineIndent.bmp")]
        protected void FirstLineIndent(object sender, WriterCommandEventArgs args)
        {
            SetParagraphStyleProperty(sender, args, StandardCommandNames.FirstLineIndent);
        }

        /// <summary>
        /// 段落首行缩进
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(StandardCommandNames.Subscript,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandSubscript.bmp")]
        protected void Subscript(object sender, WriterCommandEventArgs args)
        {
            SetStyleProperty(sender, args, StandardCommandNames.Subscript);
        }

        /// <summary>
        /// 段落左对齐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(StandardCommandNames.Superscript,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandSuperscript.bmp")]
        protected void Superscript(object sender, WriterCommandEventArgs args)
        {
            SetStyleProperty(sender, args, StandardCommandNames.Superscript);
        }



        internal static DocumentContentStyle GetCurrentStyle(DomDocument document)
        {
            return document.EditorCurrentStyle;

            //DocumentContentStyle style = document.CurrentStyle;
            //if (document.Selection.Length != 0)
            //{
            //    document._UserSpecifyStyle = null;
            //}
            //if (document._UserSpecifyStyle != null)
            //{
            //    // 设置为用户指定的样式
            //    style = document._UserSpecifyStyle;
            //}
            //return style;
        }

        private void SetStyleProperty(
            object sender,
            WriterCommandEventArgs args,
            string commandName)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                DocumentContentStyle style = GetCurrentStyle(args.Document);
                args.Enabled = args.DocumentControler != null
                    && args.DocumentControler.Snapshot.CanModifySelection;
                switch (commandName)
                {
                    case StandardCommandNames.Bold:
                        args.Checked = style.Bold;
                        break;
                    case StandardCommandNames.BorderBottom:
                        args.Checked = style.BorderBottom;
                        break;
                    case StandardCommandNames.BorderLeft:
                        args.Checked = style.BorderLeft;
                        break;
                    case StandardCommandNames.BorderRight:
                        args.Checked = style.BorderRight;
                        break;
                    case StandardCommandNames.BorderTop:
                        args.Checked = style.BorderTop;
                        break;
                    case StandardCommandNames.Italic:
                        args.Checked = style.Italic;
                        break;
                    case StandardCommandNames.Strikeout:
                        args.Checked = style.Strikeout;
                        break;
                    case StandardCommandNames.Subscript:
                        args.Checked = style.Subscript;
                        break;
                    case StandardCommandNames.Superscript:
                        args.Checked = style.Superscript;
                        break;
                    case StandardCommandNames.Underline:
                        args.Checked = style.Underline;
                        break;
                    default:
                        args.Enabled = false;
                        return;
                }
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                DocumentContentStyle cs = GetCurrentStyle(args.Document);
                DocumentContentStyle ns = args.Document.CreateDocumentContentStyle();
                ns.DisableDefaultValue = true;
                switch (commandName)
                {
                    case StandardCommandNames.Bold:
                        if (args.Parameter is bool)
                        {
                            ns.Bold = (bool)args.Parameter;
                        }
                        else
                        {
                            ns.Bold = !cs.Bold;
                        }
                        break;
                    case StandardCommandNames.BorderBottom:
                        if (args.Parameter is bool)
                        {
                            ns.BorderBottom = (bool)args.Parameter;
                        }
                        else
                        {
                            ns.BorderBottom = !cs.BorderBottom;
                        }
                        break;
                    case StandardCommandNames.BorderLeft:
                        if (args.Parameter is bool)
                        {
                            ns.BorderLeft = (bool)args.Parameter;
                        }
                        else
                        {
                            ns.BorderLeft = !cs.BorderLeft;
                        }
                        break;
                    case StandardCommandNames.BorderRight:
                        if (args.Parameter is bool)
                        {
                            ns.BorderRight = (bool)args.Parameter;
                        }
                        else
                        {
                            ns.BorderRight = !cs.BorderRight;
                        }
                        break;
                    case StandardCommandNames.BorderTop:
                        if (args.Parameter is bool)
                        {
                            ns.BorderTop = (bool)args.Parameter;
                        }
                        else
                        {
                            ns.BorderTop = !cs.BorderTop;
                        }
                        break;
                    case StandardCommandNames.Italic:
                        if (args.Parameter is bool)
                        {
                            ns.Italic = (bool)args.Parameter;
                        }
                        else
                        {
                            ns.Italic = !cs.Italic;
                        }
                        break;
                    case StandardCommandNames.Strikeout:
                        if (args.Parameter is bool)
                        {
                            ns.Strikeout = (bool)args.Parameter;
                        }
                        else
                        {
                            ns.Strikeout = !cs.Strikeout;
                        }
                        break;
                    case StandardCommandNames.Subscript:
                        if (args.Parameter is bool)
                        {
                            ns.Subscript = (bool)args.Parameter;
                        }
                        else
                        {
                            ns.Subscript = !cs.Subscript;
                        }
                        ns.Superscript = false;
                        break;
                    case StandardCommandNames.Superscript:
                        ns.Subscript = false;
                        if (args.Parameter is bool)
                        {
                            ns.Superscript = (bool)args.Parameter;
                        }
                        else
                        {
                            ns.Superscript = !cs.Superscript;
                        }
                        break;
                    case StandardCommandNames.Underline:
                        if (args.Parameter is bool)
                        {
                            ns.Underline = (bool)args.Parameter;
                        }
                        else
                        {
                            ns.Underline = !cs.Underline;
                        }
                        break;
                    case StandardCommandNames.Color:
                        if (args.Parameter is Color)
                        {
                            ns.Color = (Color)args.Parameter;
                        }
                        break;
                    case StandardCommandNames.BackColor:
                        if (args.Parameter is Color)
                        {
                            ns.BackgroundColor = (Color)args.Parameter;
                        }
                        break;
                    case StandardCommandNames.Font:
                        if (args.Parameter is Font)
                        {
                            ns.Font = new XFontValue((Font)args.Parameter);
                        }
                        else if (args.Parameter is XFontValue)
                        {
                            ns.Font = ((XFontValue)args.Parameter).Clone();
                        }
                        break;
                    case StandardCommandNames.FontName:
                        if (args.Parameter is string)
                        {
                            ns.FontName = (string)args.Parameter;
                            args.Document.EditorCurrentStyle.FontName = ns.FontName;
                            //if (args.EditorControl != null)
                            //{
                            //    args.EditorControl.Focus();
                            //}
                        }
                        break;
                    case StandardCommandNames.FontSize:
                        if (args.Parameter is string)
                        {
                            ns.FontSize = FontSizeInfo.GetFontSize((string)args.Parameter, args.Document.DefaultStyle.FontSize);
                        }
                        else if (args.Parameter is float
                            || args.Parameter is double
                            || args.Parameter is int)
                        {
                            ns.FontSize = Convert.ToSingle(args.Parameter);
                        }
                        args.Document.EditorCurrentStyle.FontSize = ns.FontSize;
                        //if (args.EditorControl != null)
                        //{
                        //    args.EditorControl.Focus();
                        //}
                        break;

                    default:
                        throw new NotSupportedException(commandName);
                }//switch
                XDependencyObject.MergeValues(ns, args.Document.EditorCurrentStyle, true);
                if (args.Document.Selection.Length != 0)
                {
                    args.Document.BeginLogUndo();
                    args.Document.Selection.SetElementStyle(ns);
                    args.Document.EndLogUndo();
                    args.Document.OnSelectionChanged();
                    args.Document.OnDocumentContentChanged();
                }
                //args.Document.CurrentStyle.Underline = v;
            }
        }

        /// <summary>
        /// 设置段落样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <param name="commandName"></param>
        private void SetParagraphStyleProperty(
            object sender,
            WriterCommandEventArgs args,
            string commandName)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                DocumentContentStyle style = args.Document.CurrentParagraphStyle;
                args.Enabled = args.DocumentControler != null
                    && args.DocumentControler.Snapshot.CanModifyParagraphs;
                switch (commandName)
                {
                    case StandardCommandNames.AlignLeft:
                        args.Checked = (style.Align == DocumentContentAlignment.Left);
                        break;
                    case StandardCommandNames.AlignCenter:
                        args.Checked = (style.Align == DocumentContentAlignment.Center);
                        break;
                    case StandardCommandNames.AlignRight:
                        args.Checked = (style.Align == DocumentContentAlignment.Right);
                        break;
                    case StandardCommandNames.AlignJustify:
                        args.Checked = (style.Align == DocumentContentAlignment.Justify);
                        break;
                    case StandardCommandNames.BorderBottom:
                        args.Checked = style.BorderBottom;
                        break;
                    case StandardCommandNames.BorderLeft:
                        args.Checked = style.BorderLeft;
                        break;
                    case StandardCommandNames.BorderRight:
                        args.Checked = style.BorderRight;
                        break;
                    case StandardCommandNames.BorderTop:
                        args.Checked = style.BorderTop;
                        break;
                    case StandardCommandNames.BulletedList:
                        args.Checked = style.BulletedList;
                        break;
                    case StandardCommandNames.NumberedList:
                        args.Checked = style.NumberedList;
                        break;
                    case StandardCommandNames.FirstLineIndent:
                        args.Checked = style.FirstLineIndent > 1.0f
                            && style.NumberedList == false
                            && style.BulletedList == false;
                        break;
                    default:
                        args.Enabled = false;
                        return;
                }
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                DocumentContentStyle cs = args.Document.CurrentParagraphStyle;
                DocumentContentStyle ns = args.Document.CreateDocumentContentStyle();
                ns.DisableDefaultValue = true;
                switch (commandName)
                {
                    case StandardCommandNames.AlignCenter:
                        ns.Align = DocumentContentAlignment.Center;
                        args.RefreshLevel = UIStateRefreshLevel.All;
                        break;
                    case StandardCommandNames.AlignJustify:
                        ns.Align = DocumentContentAlignment.Justify;
                        args.RefreshLevel = UIStateRefreshLevel.All;
                        break;
                    case StandardCommandNames.AlignLeft:
                        ns.Align = DocumentContentAlignment.Left;
                        args.RefreshLevel = UIStateRefreshLevel.All;
                        break;
                    case StandardCommandNames.AlignRight:
                        ns.Align = DocumentContentAlignment.Right;
                        args.RefreshLevel = UIStateRefreshLevel.All;
                        break;
                    case StandardCommandNames.BorderBottom:
                        if (args.Parameter is bool)
                        {
                            // 用户指定设置
                            ns.BorderBottom = (bool)args.Parameter;
                        }
                        else
                        {
                            ns.BorderBottom = !cs.BorderBottom;
                        }
                        break;
                    case StandardCommandNames.BorderLeft:
                        if (args.Parameter is bool)
                        {
                            // 用户指定设置
                            ns.BorderLeft = (bool)args.Parameter;
                        }
                        else
                        {
                            ns.BorderLeft = !cs.BorderLeft;
                        }
                        break;
                    case StandardCommandNames.BorderRight:
                        if (args.Parameter is bool)
                        {
                            ns.BorderRight = (bool)args.Parameter;
                        }
                        else
                        {
                            ns.BorderRight = !cs.BorderRight;
                        }
                        break;
                    case StandardCommandNames.BorderTop:
                        if (args.Parameter is bool)
                        {
                            ns.BorderTop = (bool)args.Parameter;
                        }
                        else
                        {
                            ns.BorderTop = !cs.BorderTop;
                        }
                        break;
                    case StandardCommandNames.BulletedList:
                        ns.NumberedList = false;
                        if (args.Parameter is bool)
                        {
                            // 用户指定设置
                            ns.BulletedList = (bool)args.Parameter;
                        }
                        else
                        {
                            ns.BulletedList = !cs.BulletedList;
                        }
                        if (ns.NumberedList || ns.BulletedList)
                        {
                            ns.FirstLineIndent = -100;
                            ns.LeftIndent = 100;
                        }
                        else
                        {
                            ns.FirstLineIndent = 0;
                            ns.LeftIndent = 0;
                        }
                        args.RefreshLevel = UIStateRefreshLevel.All;
                        break;
                    case StandardCommandNames.NumberedList:
                        if (args.Parameter is bool)
                        {
                            ns.NumberedList = (bool)args.Parameter;
                        }
                        else
                        {
                            ns.NumberedList = !cs.NumberedList;
                        }
                        ns.BulletedList = false;
                        if (ns.NumberedList || ns.BulletedList)
                        {
                            ns.FirstLineIndent = -100;
                            ns.LeftIndent = 100;
                        }
                        else
                        {
                            ns.FirstLineIndent = 0;
                            ns.LeftIndent = 0;
                        }
                        args.RefreshLevel = UIStateRefreshLevel.All;
                        break;
                    case StandardCommandNames.FirstLineIndent:
                        bool bolSet = false;

                        if (args.Parameter is bool)
                        {
                            // 用户指定首行缩进了
                            bolSet = (bool)args.Parameter;
                        }
                        else
                        {
                            if (cs.FirstLineIndent > 1f)
                            {
                                // 已经是首行缩进，则取消首行缩进
                                bolSet = false;
                            }
                            else
                            {
                                bolSet = true;
                            }
                        }
                        if (bolSet)
                        {
                            // 设置段落首行缩进
                            ns.FirstLineIndent = 100;
                            ns.LeftIndent = 0;
                            ns.NumberedList = false;
                            ns.BulletedList = false;
                        }
                        else
                        {
                            // 取消设置
                            ns.FirstLineIndent = 0;
                            ns.LeftIndent = 0;
                            ns.NumberedList = false;
                            ns.BulletedList = false;
                        }
                        args.RefreshLevel = UIStateRefreshLevel.All;
                        break;
                    default:
                        throw new NotSupportedException(commandName);
                }//switch
                args.Document.BeginLogUndo();
                args.Document.Selection.SetParagraphStyle(ns);
                args.Document.EndLogUndo();
                //args.Document.CurrentStyle.Underline = v;
                args.Document.OnSelectionChanged();
                args.Document.OnDocumentContentChanged();
            }
        }

    }
}
