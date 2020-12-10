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
using DCSoft.WinForms;
using System.Windows.Forms;
using DCSoft.CSharpWriter.Dom;
using DCSoft.CSharpWriter.Controls;

namespace DCSoft.CSharpWriter.Commands
{
    public class WriterCommandContainer
    {

        /// <summary>
        /// 命令容器对象
        /// </summary>
        public WriterCommandContainer()
        {
        }

        private WriterActionModuleList _Modules = new WriterActionModuleList();
        /// <summary>
        /// 模块列表
        /// </summary>
        public WriterActionModuleList Modules
        {
            get
            {
                return _Modules;
            }
        }

        private WriterCommandList _Commands = new WriterCommandList();
        /// <summary>
        /// 动作列表
        /// </summary>
        public WriterCommandList Commands
        {
            get
            {
                return _Commands;
            }
        }

        /// <summary>
        /// 获得指定名称的动作对象
        /// </summary>
        /// <param name="commandName"></param>
        /// <returns></returns>
        public WriterCommand GetCommand(string commandName)
        {
            foreach (WriterCommand act in this.Commands)
            {
                if (string.Compare(act.Name, commandName, true) == 0)
                {
                    return act;
                }
            }
            foreach (WriterCommandModule module in this.Modules)
            {
                foreach (WriterCommand act in module.Actions)
                {
                    if (string.Compare(act.Name, commandName, true) == 0)
                    {
                        return act;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 根据键盘事件来获得被激活的动作对象
        /// </summary>
        /// <param name="editControl"></param>
        /// <param name="document"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public WriterCommand Active(
            CSWriterControl editControl,
            DomDocument document,
            System.Windows.Forms.KeyEventArgs args)
        {
            WriterCommandEventArgs e = new WriterCommandEventArgs(
                editControl,
                document,
                WriterCommandEventMode.QueryState);
            e.AltKey = args.Alt;
            e.ShiftKey = args.Shift;
            e.CtlKey = args.Control;
            e.KeyCode = args.KeyCode;

            foreach (WriterCommand cmd in this.Commands)
            {
                if (cmd.ShortcutKey != System.Windows.Forms.Keys.None)
                {
                    // 检查快捷键
                    KeyState sk = new KeyState(cmd.ShortcutKey);
                    if (sk.Alt == args.Alt
                        && sk.Shift == args.Shift
                        && sk.Control == args.Control
                        && sk.Key == args.KeyCode)
                    {
                        e.Enabled = true;
                        e.Actived = true;
                        cmd.Invoke(e);
                        if (e.Actived && e.Enabled)
                        {
                            return cmd;
                        }
                    }
                }
                e.Actived = false;
                cmd.Invoke(e);
                if (e.Enabled && e.Actived)
                {
                    return cmd;
                }
            }

            foreach (WriterCommandModule module in this.Modules)
            {
                foreach (WriterCommand act in module.Actions)
                {
                    if (act.ShortcutKey != System.Windows.Forms.Keys.None)
                    {
                        // 检查快捷键
                        KeyState sk = new KeyState(act.ShortcutKey);
                        if (sk.Alt == args.Alt
                            && sk.Shift == args.Shift
                            && sk.Control == args.Control
                            && sk.Key == args.KeyCode)
                        {
                            e.Enabled = true;
                            e.Actived = true;
                            act.Invoke(e);
                            if (e.Enabled && e.Actived)
                            {
                                return act;
                            }
                        }
                    }
                    e.Actived = false;
                    e.Enabled = true;
                    act.Invoke(e);
                    if (e.Enabled && e.Actived)
                    {
                        return act;
                    }
                }
            }

            return null;
        }
    }
}
