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
using System.ComponentModel;

namespace DCSoft.CSharpWriter.Commands
{
    public class WriterCommandModuleDescriptor 
    {
        private static List<WriterCommandModuleDescriptor> _Instances 
            = new List<WriterCommandModuleDescriptor>();

        public static List<WriterCommandModuleDescriptor> Instances
        {
            get { return _Instances; }
            set { _Instances = value; }
        }


        /// <summary>
        /// 使用特定对象来初始化对象
        /// </summary>
        /// <param name="moduleType"></param>
        public static WriterCommandModuleDescriptor Create(Type moduleType , bool throwException )
        {
            if (moduleType == null)
            {
                if (throwException)
                {
                    throw new ArgumentNullException("moduleType");
                }
                else
                {
                    return null ;
                }
            }
            if (moduleType.IsSubclassOf(typeof(CSWriterCommandModule)) == false)
            {
                if (throwException)
                {
                    throw new ArgumentException(moduleType.FullName);
                }
                else
                {
                    return null;
                }
            }
            WriterCommandModuleDescriptor descriptor = new WriterCommandModuleDescriptor();
            descriptor.ModuleType = moduleType;
            descriptor.Name = moduleType.Name;
            WriterCommandDescriptionAttribute attr = (WriterCommandDescriptionAttribute)
                Attribute.GetCustomAttribute(
                    descriptor.ModuleType,
                    typeof(WriterCommandDescriptionAttribute),
                    false);
            if (attr != null)
            {
                descriptor.Name = attr.Name;
                descriptor.Description = attr.Description;
                descriptor.ImageResource = attr.ImageResource;
                string src = attr.ImageResource;
                if (src != null && src.Trim().Length > 0)
                {
                    descriptor.Image = CommandUtils.GetResourceImage(moduleType.Assembly, src.Trim());
                }
            }
            if (descriptor.Description == null || descriptor.Description.Trim().Length == 0)
            {
                // 获得说明
                DescriptionAttribute da = (DescriptionAttribute)Attribute.GetCustomAttribute(
                    descriptor.ModuleType,
                    typeof(DescriptionAttribute),
                    true);
                if (da != null)
                {
                    descriptor.Description = da.Description;
                }
            }
            

            // 分析成员方法，创建方法命令描述对象
            System.Reflection.MethodInfo[] ms = moduleType.GetMethods(
                System.Reflection.BindingFlags.Instance
                | System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.NonPublic);
            foreach (System.Reflection.MethodInfo m in ms)
            {
                //System.Console.WriteLine( m.DeclaringType.Name + "*" + m.Name);

                if (m.DeclaringType.Equals(typeof(System.Windows.Forms.Form)))
                {
                    continue;
                }
                if (m.DeclaringType.Equals(typeof(System.Windows.Forms.Control)))
                {
                    continue;
                }
                if (m.DeclaringType.Equals(typeof(object)))
                {
                    continue;
                }

                WriterCommandDescriptor cmd = WriterCommandDescriptor.Create(moduleType, m, false);
                if (cmd != null)
                {
                    descriptor.Commands.Add(cmd);
                }
            }//foreach
            descriptor.Commands.Sort();
            return descriptor;
        }



        /// <summary>
        /// 初始化对象
        /// </summary>
        public WriterCommandModuleDescriptor()
        {
        }

        private string _Name = null;
        /// <summary>
        /// 模块名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _Description = null;

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        private string _ImageResource = null;

        public string ImageResource
        {
            get { return _ImageResource; }
            set { _ImageResource = value; }
        }

        private System.Drawing.Image _Image = null;
        /// <summary>
        /// 图标
        /// </summary>
        public System.Drawing.Image Image
        {
            get { return _Image; }
            set { _Image = value; }
        }

        private Type _ModuleType = null;
        /// <summary>
        /// 模块对象类型
        /// </summary>
        public Type ModuleType
        {
            get { return _ModuleType; }
            set { _ModuleType = value; }
        }

        private List<WriterCommandDescriptor> _Commands = new List<WriterCommandDescriptor>();

        public List<WriterCommandDescriptor> Commands
        {
            get { return _Commands; }
            set { _Commands = value; }
        }

        public override string ToString()
        {
            return "Module:" + this.Name;
        }
    }

    public class WriterCommandModuleDescriptorList : List<WriterCommandModuleDescriptor>
    {
        //private static Dictionary<System.Reflection.Assembly, WriterCommandModuleDescriptorList> _buffer
        //    = new Dictionary<System.Reflection.Assembly, WriterCommandModuleDescriptorList>();

        //public static WriterCommandModuleDescriptorList GetModuleDescriptors(
        //    System.Reflection.Assembly assembly ,
        //    bool buffered )
        //{
        //    if (assembly == null)
        //    {
        //        throw new ArgumentNullException("assembly");
        //    }
        //    if (buffered)
        //    {
        //        if (_buffer.ContainsKey(assembly))
        //        {
        //            return _buffer[assembly];
        //        }
        //    }
        //    WriterCommandModuleDescriptorList list = new WriterCommandModuleDescriptorList();
        //    foreach (Type t in assembly.GetTypes())
        //    {
        //        if (t.IsSubclassOf(typeof(WriterCommandModule)) == false)
        //        {
        //            continue;
        //        }
        //        WriterCommandModuleDescriptor descriptor = new WriterCommandModuleDescriptor();
        //        descriptor.ModuleType = t;
        //        descriptor.Name = t.Name;
        //        WriterCommandDescriptionAttribute attr = (WriterCommandDescriptionAttribute)
        //            Attribute.GetCustomAttribute(
        //                descriptor.ModuleType ,
        //                typeof(WriterCommandDescriptionAttribute),
        //                false);
        //        if (attr != null)
        //        {
        //            descriptor.Name = attr.Name;
        //            descriptor.Description = attr.Description;
        //            descriptor.ImageResource = attr.ImageResource;
        //            string src = attr.ImageResource;
        //            if (src != null && src.Trim().Length > 0)
        //            {
        //                descriptor.Image = System.Drawing.ToolboxBitmapAttribute.GetImageFromResource(
        //                    t,
        //                    src.Trim(),
        //                    false);
        //            }
        //        }
        //        descriptor.Commands = WriterCommandDescriptionAttribute.CreateDescriptors(descriptor.ModuleType);
        //        list.Add(descriptor);
        //    }//foreach
        //    if (buffered)
        //    {
        //        _buffer[assembly] = list;
        //    }
        //    return list;
        //}

        //public WriterCommandDescriptor GetDescriptor(string commandName)
        //{
        //    foreach (WriterCommandModuleDescriptor module in this)
        //    {
        //        foreach (WriterCommandDescriptor cmd in module.Commands)
        //        {
        //            if (string.Compare(cmd.CommandName, commandName, true) == 0)
        //            {
        //                return cmd;
        //            }
        //        }
        //    }
        //    return null;
        //}

    }
}
