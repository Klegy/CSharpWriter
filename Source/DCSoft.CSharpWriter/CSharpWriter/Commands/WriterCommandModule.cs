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

namespace DCSoft.CSharpWriter.Commands
{
    /// <summary>
    /// 功能模块对象
    /// </summary>
    public class WriterCommandModule : IDisposable 
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public WriterCommandModule()
        {
        }

        private string _Name = null;
        /// <summary>
        /// 模块名称
        /// </summary>
        public virtual string Name
        {
            get
            {
                return _Name; 
            }
            set
            {
                if (_Name == null)
                {
                    WriterCommandDescriptionAttribute attr = (WriterCommandDescriptionAttribute)
                        Attribute.GetCustomAttribute(
                        this.GetType(), typeof(WriterCommandDescriptionAttribute), true);
                    if (attr != null)
                    {
                        _Name = attr.Name;
                    }
                }
                _Name = value; 
            }
        }

        private bool _Enabled = true;
        /// <summary>
        /// 模块是否可用
        /// </summary>
        public bool Enabled
        {
            get
            {
                return _Enabled; 
            }
            set
            {
                _Enabled = value; 
            }
        }

        /// <summary>
        /// 启动模块
        /// </summary>
        /// <param name="args">事件参数</param>
        /// <returns>操作是否成功</returns>
        public virtual bool Start(WriterCommandEventArgs args)
        {
            return true;
        }

        private WriterCommandList _Actions = null;
        /// <summary>
        /// 本模块包含的动作对象列表
        /// </summary>
        public virtual WriterCommandList Actions
        {
            get
            {
                if (_Actions == null)
                {
                    _Actions = CreateCommands();
                }
                return _Actions;
            }
        }

        /// <summary>
        /// 关闭模块
        /// </summary>
        /// <param name="args"></param>
        public virtual void Close(WriterCommandEventArgs args)
        {
        }

        /// <summary>
        /// 销毁对象
        /// </summary>
        public virtual void Dispose()
        {
        }

        protected WriterCommandList CreateCommands()
        {
            WriterCommandModuleDescriptor md = WriterCommandModuleDescriptor.Create(
                this.GetType(),
                false);
            WriterCommandList list = new WriterCommandList();
            foreach (WriterCommandDescriptor d in md.Commands )
            {
                WriterCommandDelegate cmd = new WriterCommandDelegate();
                cmd.Name = d.CommandName;
                cmd.ShortcutKey = d.ShortcutKey;
                cmd.ToolbarImage = d.Image;
                cmd.Description = d.Description;
                cmd.Handler = (WriterCommandEventHandler)Delegate.CreateDelegate(
                    typeof(WriterCommandEventHandler),
                    this,
                    d.Method);
                list.Add(cmd);
            }
            return list;
        }

        public override string ToString()
        {
            return "Module:" + this.Name;
        }
    }

    /// <summary>
    /// 功能模块列表
    /// </summary>
    public class WriterActionModuleList : List<WriterCommandModule>
    {
    }
}
