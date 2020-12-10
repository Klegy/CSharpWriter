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
using System.Windows.Forms ;
using DCSoft.Printing;
using DCSoft.CSharpWriter.Printing;
using DCSoft.Common;

namespace DCSoft.CSharpWriter.Commands
{
    /// <summary>
    /// 只读的浏览文档内容的功能模块
    /// </summary>
    /// <remarks>
    /// 该模块中的功能用于滚动浏览文档内容，不会修改文档内容。编制，袁永福。
    /// </remarks>
    [WriterCommandDescription( StandardCommandNames.ModuleBrowse )]
    internal class WriterCommandModuleBrowse : WriterCommandModule 
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public WriterCommandModuleBrowse()
        {
        }

        /// <summary>
        /// 显示关于对话框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(StandardCommandNames.AboutControl,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandAboutControl.bmp")]
        protected void AboutControl(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = args.ShowUI;
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                if (args.ShowUI)
                {
                    using (DCSoft.CSharpWriter.Controls.dlgAbout dlg 
                        = new DCSoft.CSharpWriter.Controls.dlgAbout())
                    {
                        dlg.ShowDialog(args.EditorControl);
                    }
                }
            }
        }

        /// <summary>
        /// 移动位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription( StandardCommandNames.MoveTo )]
        protected void MoveTo(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = args.Document != null;
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                MoveTarget target = MoveTarget.None;
                if (args.Parameter is MoveTarget)
                {
                    target = (MoveTarget)args.Parameter;
                }
                else if (args.Parameter is string)
                {
                    try
                    {
                        target = (MoveTarget)Enum.Parse(
                            typeof(MoveTarget),
                            (string)args.Parameter,
                            true);
                    }
                    catch
                    {
                    }
                }
                if (target != MoveTarget.None)
                {
                    args.Document.Content.AutoClearSelection = true;
                    args.Document.Content.MoveTo(target);
                }
            }
        }

        /// <summary>
        /// 向上移动一页
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="args">参数</param>
        [WriterCommandDescription(
            StandardCommandNames.MovePageDown ,
            ShortcutKey = Keys.PageDown )]
        protected void MovePageDown(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = (args.Document != null && args.EditorControl != null);
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                args.Document.Content.AutoClearSelection = true;
                args.Document.Content.MoveStep(
                    args.Document.PageSettings.ViewClientHeight);
                args.Document.EditorControl.UpdateTextCaret();
                //args.RefreshLevel = UIStateRefreshLevel.All;
            }
        }

        /// <summary>
        /// 向上移动一页
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="args">参数</param>
        [WriterCommandDescription( StandardCommandNames.MovePageUp , ShortcutKey = Keys.PageUp)]
        protected void MovePageUp(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = (args.Document != null && args.EditorControl != null);
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                args.Document.Content.AutoClearSelection = true;
                args.Document.Content.MoveStep(
                    - args.Document.PageSettings.ViewClientHeight);
                args.Document.EditorControl.UpdateTextCaret();
                //args.RefreshLevel = UIStateRefreshLevel.All;
            }
        }

        /// <summary>
        /// 向上移动一列
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="args">参数</param>
        [WriterCommandDescription(StandardCommandNames.MoveUp , ShortcutKey = Keys.Up)]
        protected void MoveUp(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = (args.Document != null && args.EditorControl != null);
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                args.Document.Content.AutoClearSelection = true;
                args.Document.Content.MoveUpOneLine();
                args.Document.EditorControl.UpdateTextCaret();
                //args.RefreshLevel = UIStateRefreshLevel.All;
            }
        }

        /// <summary>
        /// 向右移动一列
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="args">参数</param>
        [WriterCommandDescription( StandardCommandNames.MoveRight , ShortcutKey = Keys.Right)]
        protected void MoveRight(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = (args.Document != null && args.EditorControl != null);
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                args.Document.Content.AutoClearSelection = true;
                args.Document.Content.MoveRight();
                args.Document.EditorControl.UpdateTextCaret();
                //args.RefreshLevel = UIStateRefreshLevel.All;
            }
        }


        /// <summary>
        /// 向左移动一列
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="args">参数</param>
        [WriterCommandDescription( StandardCommandNames.MoveLeft , ShortcutKey = Keys.Left )]
        protected void MoveLeft(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = (args.Document != null && args.EditorControl != null);
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                args.Document.Content.AutoClearSelection = true;
                args.Document.Content.MoveLeft();
                args.Document.EditorControl.UpdateTextCaret();
                //args.RefreshLevel = UIStateRefreshLevel.All;
            }
        }

        /// <summary>
        /// 向下移动一行
        /// </summary>
        /// <param name="send"></param>
        /// <param name="args"></param>
        [WriterCommandDescription( StandardCommandNames.MoveDown , ShortcutKey = Keys.Down)]
        protected void MoveDown(object send, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = (args.Document != null && args.EditorControl != null);
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                args.Document.Content.AutoClearSelection = true;
                args.Document.Content.MoveDownOneLine();
                args.Document.EditorControl.UpdateTextCaret();
                //args.RefreshLevel = UIStateRefreshLevel.All;
            }
        }
        /// <summary>
        /// 移动到行首
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(StandardCommandNames.MoveHome ,ShortcutKey=Keys.Home )]
        protected void MoveHome(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = (args.Document != null && args.EditorControl != null);
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                args.Document.Content.AutoClearSelection = true;
                args.Document.Content.MoveHome();
                args.EditorControl.UpdateTextCaret();
                //args.RefreshLevel = UIStateRefreshLevel.All;
            }
        }

        /// <summary>
        /// 移动到行尾
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription( StandardCommandNames.MoveEnd ,ShortcutKey=Keys.End)]
        protected void MoveEnd(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = (args.Document != null && args.EditorControl != null);
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                args.Document.Content.AutoClearSelection = true;
                args.Document.Content.MoveEnd();
                args.EditorControl.UpdateTextCaret();
                //args.RefreshLevel = UIStateRefreshLevel.All;
            }
        }


        /// <summary>
        /// 向上移动一页
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="args">参数</param>
        [WriterCommandDescription( StandardCommandNames.ShiftMovePageDown ,
            ShortcutKey = Keys.Shift | Keys.PageDown)]
        protected void ShiftMovePageDown(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = (args.Document != null && args.EditorControl != null);
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                args.Document.Content.AutoClearSelection = false ;
                args.Document.Content.MoveStep(
                    args.Document.PageSettings.ViewClientHeight);
                args.Document.EditorControl.UpdateTextCaret();
                //args.RefreshLevel = UIStateRefreshLevel.All;
            }
        }

        /// <summary>
        /// 向上移动一页
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="args">参数</param>
        [WriterCommandDescription( StandardCommandNames.ShiftMovePageUp ,
            ShortcutKey = Keys.Shift | Keys.PageUp)]
        protected void ShiftMovePageUp(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = (args.Document != null && args.EditorControl != null);
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                args.Document.Content.AutoClearSelection = false ;
                args.Document.Content.MoveStep(
                    -args.Document.PageSettings.ViewClientHeight);
                args.Document.EditorControl.UpdateTextCaret();
                //args.RefreshLevel = UIStateRefreshLevel.All;
            }
        }

        /// <summary>
        /// 向上移动一列
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="args">参数</param>
        [WriterCommandDescription( StandardCommandNames.ShiftMoveUp , ShortcutKey = Keys.Shift | Keys.Up)]
        protected void ShiftMoveUp(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = (args.Document != null && args.EditorControl != null);
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                args.Document.Content.AutoClearSelection = false ;
                args.Document.Content.MoveUpOneLine();
                args.Document.EditorControl.UpdateTextCaret();
                //args.RefreshLevel = UIStateRefreshLevel.All;
            }
        }

        /// <summary>
        /// 向右移动一列
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="args">参数</param>
        [WriterCommandDescription( StandardCommandNames.ShiftMoveRight ,
            ShortcutKey = Keys.Shift | Keys.Right)]
        protected void ShiftMoveRight(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = (args.Document != null && args.EditorControl != null);
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                args.Document.Content.AutoClearSelection = false ;
                args.Document.Content.MoveRight();
                args.Document.EditorControl.UpdateTextCaret();
                //args.RefreshLevel = UIStateRefreshLevel.All;
            }
        }


        /// <summary>
        /// 向左移动一列
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="args">参数</param>
        [WriterCommandDescription( StandardCommandNames.ShiftMoveLeft ,
            ShortcutKey = Keys.Shift | Keys.Left)]
        protected void ShiftMoveLeft(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = (args.Document != null && args.EditorControl != null);
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                args.Document.Content.AutoClearSelection = false;
                args.Document.Content.MoveLeft();
                args.Document.EditorControl.UpdateTextCaret();
                //args.RefreshLevel = UIStateRefreshLevel.All;
            }
        }

        /// <summary>
        /// 向下移动一行
        /// </summary>
        /// <param name="send"></param>
        /// <param name="args"></param>
        [WriterCommandDescription( StandardCommandNames.ShiftMoveDown ,
            ShortcutKey = Keys.Shift | Keys.Down)]
        protected void ShiftMoveDown(object send, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = (args.Document != null && args.EditorControl != null);
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                args.Document.Content.AutoClearSelection = false ;
                args.Document.Content.MoveDownOneLine();
                args.Document.EditorControl.UpdateTextCaret();
                //args.RefreshLevel = UIStateRefreshLevel.All;
            }
        }
        /// <summary>
        /// 移动到行首
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(StandardCommandNames.ShiftMoveHome ,
            ShortcutKey = Keys.Shift | Keys.Home)]
        protected void ShiftMoveHome(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = (args.Document != null && args.EditorControl != null);
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                args.Document.Content.AutoClearSelection = false ;
                args.Document.Content.MoveHome();
                args.EditorControl.UpdateTextCaret();
                //args.RefreshLevel = UIStateRefreshLevel.All;
            }
        }

        /// <summary>
        /// 移动到行尾
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(StandardCommandNames.ShiftMoveEnd ,
            ShortcutKey = Keys.Shift | Keys.End)]
        protected void ShiftMoveEnd(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = (args.Document != null && args.EditorControl != null);
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                args.Document.Content.AutoClearSelection = false ;
                args.Document.Content.MoveEnd();
                args.EditorControl.UpdateTextCaret();
                //args.RefreshLevel = UIStateRefreshLevel.All;
            }
        }

        [WriterCommandDescription( StandardCommandNames.CtlMoveUp ,
            ShortcutKey=Keys.Control | Keys.Up)]
        protected void CtlMoveUp( object sender , WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = (args.EditorControl != null);
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                if (args.EditorControl != null)
                {
                    System.Drawing.Point p = args.EditorControl.AutoScrollPosition;
                    int height = args.EditorControl.Transform.UnTransformSize(
                        1,
                        (int)args.Document.DefaultStyle.DefaultLineHeight).Height;
                    p = new System.Drawing.Point(-p.X, -height - p.Y);
                    args.EditorControl.AutoScrollPosition = p;
                    args.EditorControl.InnerOnScroll();
                    //args.RefreshLevel = UIStateRefreshLevel.All;
                }
            }
        }
        
        /// <summary>
        /// 按住Ctl键向下移动一行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription( StandardCommandNames.CtlMoveDown ,
            ShortcutKey=Keys.Control | Keys.Down )]
        protected void CtlMoveDown( object sender , WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = (args.EditorControl != null);
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                if (args.EditorControl != null)
                {
                    System.Drawing.Point p = args.EditorControl.AutoScrollPosition;
                    int height = (int)args.EditorControl.Transform.UnTransformSize(
                        1,
                        (int)args.Document.DefaultStyle.DefaultLineHeight).Height;
                    p = new System.Drawing.Point(-p.X, height - p.Y);
                    args.EditorControl.SetAutoScrollPosition(p);
                    args.EditorControl.InnerOnScroll();
                    //args.RefreshLevel = UIStateRefreshLevel.All;
                }
            }
        }

        /// <summary>
        /// 全选文档内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription( StandardCommandNames.SelectAll ,
            ShortcutKey = Keys.Control | Keys.A)]
        protected void SelectAll(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = (args.Document != null );
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                args.Document.Content.SelectAll();
                args.EditorControl.UpdateTextCaret();
                //args.RefreshLevel = UIStateRefreshLevel.All;
            }
        }
         

        /// <summary>
        /// 编辑器是否处于设计时模式
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="args">参数</param>
        [WriterCommandDescription(StandardCommandNames.DesignMode,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandDesignMode.bmp")]
        protected void DesignMode(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                if (args.EditorControl == null)
                {
                    args.Enabled = false;
                }
                else
                {
                    args.Enabled = true;
                    args.Checked = args.EditorControl.DocumentOptions.BehaviorOptions.DesignMode;
                }
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                bool v = !args.EditorControl.DocumentOptions.BehaviorOptions.DesignMode;
                if (args.Parameter is bool)
                {
                    v = (bool)args.Parameter;
                }
                else if (args.Parameter is string)
                {
                    if (bool.TryParse((string)args.Parameter, out v) == false)
                    {
                        v = !args.EditorControl.DocumentOptions.BehaviorOptions.DesignMode;
                    }
                }
                args.EditorControl.DocumentOptions.BehaviorOptions.DesignMode = v;
                args.Document.Options.BehaviorOptions.DesignMode = v;
                args.RefreshLevel = UIStateRefreshLevel.Current ;
            }
        }

        /// <summary>
        /// 编辑器是否处于调试模式
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="args">参数</param>
        [WriterCommandDescription( StandardCommandNames.DebugMode )]
        protected void DebugMode(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                if (args.EditorControl == null)
                {
                    args.Enabled = false;
                }
                else
                {
                    args.Enabled = true;
                    args.Checked = args.EditorControl.DocumentOptions.BehaviorOptions.DebugMode;
                }
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                bool v = ! args.EditorControl.DocumentOptions.BehaviorOptions.DebugMode;
                if (args.Parameter is bool)
                {
                    v = (bool)args.Parameter;
                }
                else if (args.Parameter is string)
                {
                    if (bool.TryParse((string)args.Parameter, out v) == false )
                    {
                        v = !args.EditorControl.DocumentOptions.BehaviorOptions.DebugMode;
                    }
                }
                args.EditorControl.DocumentOptions.BehaviorOptions.DebugMode = v;
                args.Document.Options.BehaviorOptions.DebugMode = v;
                args.RefreshLevel = UIStateRefreshLevel.Current;
            }
        }

        /// <summary>
        /// 页面视图方式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(StandardCommandNames.ComplexViewMode)]
        protected void ComplexViewMode(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                if (args.Document == null )
                {
                    args.Enabled = false;
                }
                else
                {
                    args.Enabled = true;
                    DCSoft.CSharpWriter.Security.DocumentSecurityOptions opt = args.Document.Options.SecurityOptions;
                    args.Checked = opt.ShowLogicDeletedContent == true 
                        && opt.ShowPermissionMark == true 
                        && opt.ShowPermissionTip == true ;
                    
                }
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                DCSoft.CSharpWriter.Security.DocumentSecurityOptions opt = args.Document.Options.SecurityOptions;
                opt.ShowLogicDeletedContent = true ;
                opt.ShowPermissionMark = true;
                opt.ShowPermissionTip = true;
                if (args.EditorControl != null)
                {
                    opt = args.EditorControl.DocumentOptions.SecurityOptions;
                    opt.ShowLogicDeletedContent = true;
                    opt.ShowPermissionMark = true;
                    opt.ShowPermissionTip = true;
                }
                args.EditorControl.RefreshDocument();
                args.RefreshLevel = UIStateRefreshLevel.All;
            }
        }

        /// <summary>
        /// 页面视图方式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(StandardCommandNames.CleanViewMode)]
        protected void CleanViewMode(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                if (args.Document == null )
                {
                    args.Enabled = false;
                }
                else
                {
                    args.Enabled = true;
                    DCSoft.CSharpWriter.Security.DocumentSecurityOptions opt = args.Document.Options.SecurityOptions ;
                    args.Checked = opt.ShowLogicDeletedContent == false
                        && opt.ShowPermissionMark == false 
                        && opt.ShowPermissionTip == false ;
                }
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                DCSoft.CSharpWriter.Security.DocumentSecurityOptions opt = args.Document.Options.SecurityOptions;
                opt.ShowLogicDeletedContent = false;
                opt.ShowPermissionMark = false;
                opt.ShowPermissionTip = false;
                if (args.EditorControl != null)
                {
                    opt = args.EditorControl.DocumentOptions.SecurityOptions;
                    opt.ShowLogicDeletedContent = false ;
                    opt.ShowPermissionMark = false ;
                    opt.ShowPermissionTip = false ;
                }
                args.EditorControl.RefreshDocument();
                args.RefreshLevel = UIStateRefreshLevel.All;
            }
        }
         
        /// <summary>
        /// 页面视图方式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription( StandardCommandNames.PageViewMode )]
        protected void PageViewModeFunction(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                if (args.EditorControl == null)
                {
                    args.Enabled = false;
                }
                else
                {
                    args.Enabled = true;
                    args.Checked = args.EditorControl.ViewMode == PageViewMode.Page;
                }
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
              
                args.EditorControl.ViewMode = PageViewMode.Page;
                args.EditorControl.RefreshDocument();
                args.EditorControl.Invalidate();
                args.RefreshLevel = UIStateRefreshLevel.All;
            }
        }

        /// <summary>
        /// 页面视图方式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(StandardCommandNames.NormalViewMode)]
        protected void NormalViewMode(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                if (args.EditorControl == null)
                {
                    args.Enabled = false;
                }
                else
                {
                    args.Enabled = true;
                    args.Checked = args.EditorControl.ViewMode == PageViewMode.Normal ;
                }
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
              
                args.EditorControl.ViewMode = PageViewMode.Normal ;
                args.EditorControl.RefreshDocument();
                args.EditorControl.Invalidate();
                args.RefreshLevel = UIStateRefreshLevel.All;
            }
        }

        /// <summary>
        /// 替换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription( StandardCommandNames.Replace)]
        protected void Replace(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = args.Document != null;
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
            }
        }


        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription( StandardCommandNames.Search )]
        protected void SearchString(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = args.Document != null;
                string text = Convert.ToString(args.Parameter);
                if (text == null || text.Trim().Length == 0)
                {
                    args.Enabled = false;
                }
            }
            else
            {
                string text = Convert.ToString(args.Parameter);
                if (text == null || text.Trim().Length == 0)
                {
                    return;
                }
                //int index = args.Document.Selection.StartIndex;
                //if (args.Document.Selection.Count > 0)
                //    index += args.Document.Selection.Count;
                //if (this.txtSearch.Modified)
                //{
                //    index = 0;
                //    txtSearch.Modified = false;
                //}
                //int FindIndex = myEditor.Document.Content.SearchString(index, this.txtSearch.Text, true, true);
                //myEditor.ScrollToCaret();
                //if (FindIndex == -1 && index > 0)
                //{
                //    FindIndex = myEditor.Document.Content.SearchString(0, this.txtSearch.Text, true, true);
                //    myEditor.ScrollToCaret();
                //    //myEditor.Document.Content.MoveSelectStart( 0 );
                //}
                //return FindIndex;
            }
        }

        /// <summary>
        /// 显示调试输出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription( StandardCommandNames.DebugOutputWindow,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandDebugOutputWindow.bmp")]
        protected void DebugOutputWindow(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = args.EditorControl != null;
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                frmDebugOutput frm = (frmDebugOutput)args.EditorControl.ToolWindows[typeof(frmDebugOutput)];
                if (frm == null)
                {
                    frm = new frmDebugOutput();
                    frm.Owner = args.EditorControl.FindForm();
                    args.EditorControl.ToolWindows.Add(frm);
                }
                if (frm.Visible)
                {
                    frm.Activate();
                }
                else
                {
                    frm.Show(args.EditorControl);
                }
            }
        }

        

    }
}