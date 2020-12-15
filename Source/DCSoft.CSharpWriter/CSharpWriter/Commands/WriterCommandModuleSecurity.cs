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
    /// 安全相关的命令对象模块
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [WriterCommandDescription("Security")]
    internal class WriterCommandModuleSecurity : CSWriterCommandModule
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public WriterCommandModuleSecurity()
        {
        }


        /// <summary>
        /// 清除用户留下的痕迹
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="args">参数</param>
        [WriterCommandDescription(StandardCommandNames.ClearUserTrace)]
        protected void ClearUserTrace(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                // 控件不是只读的而且处于管理员模式下该命令才有效
                args.Enabled = args.EditorControl != null
                    && args.EditorControl.IsAdministrator
                    && args.EditorControl.Readonly == false
                    && args.Document != null
                    && args.Document.Selection.Length != 0;
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                args.Result = false;
                if (args.Document.Selection.Length != 0)
                {
                    Dictionary<DomElement, int> newStyleIndexs = new Dictionary<DomElement, int>();

                    foreach (DomElement element in args.Document.Selection)
                    {
                        DocumentContentStyle style = element.Style;
                        if (style.CreatorIndex >= 0 || style.DeleterIndex >= 0)
                        {
                            style = (DocumentContentStyle)style.Clone();
                            style.DisableDefaultValue = false;
                            style.CreatorIndex = -1;
                            style.DeleterIndex = -1;
                            newStyleIndexs[element] = args.Document.ContentStyles.GetStyleIndex(style);
                        }
                    }
                     
                    if (newStyleIndexs.Count > 0)
                    {
                        args.Result = true;
                        args.Document.EditorSetElementStyle(newStyleIndexs, true);
                    }
                }
            }
        }



        /// <summary>
        /// 管理员模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(StandardCommandNames.AdministratorViewMode)]
        protected void AdministratorViewMode(object sender, WriterCommandEventArgs args)
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
                    args.Checked = args.EditorControl.IsAdministrator;
                }
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                bool admin = !args.EditorControl.IsAdministrator;
                if (args.Parameter is bool)
                {
                    admin = (bool)args.Parameter;
                }
                args.EditorControl.IsAdministrator = admin;
                args.EditorControl.Invalidate();
                args.RefreshLevel = UIStateRefreshLevel.All;
            }
        }


    }
}
