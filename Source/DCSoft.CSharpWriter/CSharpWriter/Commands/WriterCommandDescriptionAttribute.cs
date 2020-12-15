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
using System.Drawing ;
using System.ComponentModel;

namespace DCSoft.CSharpWriter.Commands
{
    /// <summary>
    /// 动作方法声明特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class , AllowMultiple=false )]
    public class WriterCommandDescriptionAttribute : Attribute 
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public WriterCommandDescriptionAttribute()
        {
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="name">动作名称</param>
        public WriterCommandDescriptionAttribute(string name)
        {
            _Name = name;
        }

        private string _Name = null;
        /// <summary>
        /// 动作名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private System.Windows.Forms.Keys _ShortcutKey = System.Windows.Forms.Keys.None;
        /// <summary>
        /// 快捷键
        /// </summary>
        public System.Windows.Forms.Keys ShortcutKey
        {
            get { return _ShortcutKey; }
            set { _ShortcutKey = value; }
        }

        private string _ImageResource = null;
        /// <summary>
        /// 动作图标来源
        /// </summary>
        public string ImageResource
        {
            get { return _ImageResource; }
            set { _ImageResource = value; }
        }

        private string _Description = null;
        /// <summary>
        /// 动作说明
        /// </summary>
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }


        //public System.Drawing.Image GetResourceImage(System.Reflection.Assembly asm)
        //{
        //    if (asm == null)
        //    {
        //        return null;
        //    }
        //    if ( this.ImageResource != null && this.ImageResource.Trim().Length > 0)
        //    {
        //        System.IO.Stream stream = asm.GetManifestResourceStream(this.ImageResource);
        //        if (stream != null)
        //        {
        //            byte[] bs = new byte[stream.Length];
        //            stream.Read(bs, 0, bs.Length);
        //            System.IO.MemoryStream ms = new System.IO.MemoryStream(bs);
        //            System.Drawing.Image img2 = System.Drawing.Image.FromStream(ms);
        //            if (img2 is System.Drawing.Bitmap)
        //            {
        //                System.Drawing.Bitmap bmp = (System.Drawing.Bitmap)img2;
        //                bmp.MakeTransparent(bmp.GetPixel(0, bmp.Height - 1));
        //            }
        //            return img2;
        //        }
        //    }
        //    return null;
        //}

        //public static WriterCommandDescriptor CreateDescriptor(Type commandType)
        //{
        //    if (commandType == null)
        //    {
        //        throw new ArgumentNullException("commandType");
        //    }
        //    if (commandType.IsSubclassOf(typeof(WriterCommandBase)) == false)
        //    {
        //        throw new ArgumentException(commandType.FullName);
        //    }
        //    WriterCommandDescriptor descriptor = new WriterCommandDescriptor();
        //    descriptor.ContainerType = commandType;
        //    WriterCommandDescriptionAttribute attr = (WriterCommandDescriptionAttribute)Attribute.GetCustomAttribute(
        //            commandType , typeof(WriterCommandDescriptionAttribute), false);
        //    if( attr != null )
        //    {
        //        descriptor.CommandName = attr.Name ;
        //        descriptor.Description  = attr.Description ;
        //        descriptor.ImageResource = attr.ImageResource ;
        //        descriptor.ShortcutKey = attr.ShortcutKey ;
        //        string src = attr.ImageResource ;
        //        if( src != null && src.Trim().Length > 0 )
        //        {
        //            descriptor.Image = ToolboxBitmapAttribute.GetImageFromResource(
        //                commandType ,
        //                src.Trim() ,
        //                false );
        //        }
        //    }
        //    return descriptor ;
        //}

        //public static List<WriterCommandModuleDescriptor> CreateModuleDescriptors(System.Reflection.Assembly assembly)
        //{
        //    if (assembly == null)
        //    {
        //        throw new ArgumentNullException("assembly");
        //    }
        //    List<WriterCommandModuleDescriptor> list = new List<WriterCommandModuleDescriptor>();
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
        //                descriptor.Image = ToolboxBitmapAttribute.GetImageFromResource(
        //                    t,
        //                    src.Trim(),
        //                    false);
        //            }
        //        }
        //        if (descriptor.Description == null && descriptor.Description.Trim().Length == 0)
        //        {
        //            // 获得说明
        //            DescriptionAttribute da = (DescriptionAttribute)Attribute.GetCustomAttribute(
        //                descriptor.ModuleType,
        //                typeof(DescriptionAttribute),
        //                true);
        //            if (da != null)
        //            {
        //                descriptor.Description = da.Description;
        //            }
        //        }
        //        descriptor.Commands = CreateDescriptors(descriptor.ModuleType);
        //        list.Add(descriptor);
        //    }//foreach
        //    return list;
        //}

        //public static List<WriterCommandDescriptor> CreateDescriptors(Type commandModuleType)
        //{
        //    if (commandModuleType == null)
        //    {
        //        throw new ArgumentNullException("commandModuleType");
        //    }

        //    List<WriterCommandDescriptor> list = new List<WriterCommandDescriptor>();

        //    System.Reflection.MethodInfo[] ms = commandModuleType.GetMethods(
        //        System.Reflection.BindingFlags.Instance
        //        | System.Reflection.BindingFlags.Public
        //        | System.Reflection.BindingFlags.NonPublic);
        //    foreach (System.Reflection.MethodInfo m in ms)
        //    {
        //        //System.Console.WriteLine( m.DeclaringType.Name + "*" + m.Name);

        //        if (m.DeclaringType.Equals(typeof(System.Windows.Forms.Form)))
        //            continue;
        //        if (m.DeclaringType.Equals(typeof(System.Windows.Forms.Control)))
        //            continue;
        //        //System.Console.WriteLine( m.Name );
        //        WriterCommandDescriptionAttribute attr = (WriterCommandDescriptionAttribute)
        //            Attribute.GetCustomAttribute(
        //            m, typeof(WriterCommandDescriptionAttribute), false);
        //        if (attr == null)
        //        {
        //            continue;
        //        }
        //        //System.Console.WriteLine( "+++++++" + m.Name);
        //        if (m.ReturnType.Equals(typeof(void)) == false)
        //        {
        //            continue;
        //        }
        //        System.Reflection.ParameterInfo[] ps = m.GetParameters();
        //        if (ps == null || ps.Length != 2)
        //        {
        //            continue;
        //        }
        //        if (ps[0].ParameterType.Equals(typeof(object))
        //            || ps[1].ParameterType.Equals(typeof(WriterCommandEventArgs)))
        //        {

        //            string name = attr.Name;
        //            //System.EventHandler h = new EventHandler( objInstance , m.Name );// m.MethodHandle );
        //            if (name == null || name.Trim().Length == 0)
        //            {
        //                name = m.Name;
        //            }
        //            WriterCommandDescriptor descriptor = new WriterCommandDescriptor();
        //            descriptor.CommandName = name;
        //            descriptor.ContainerType = commandModuleType;
        //            descriptor.Method = m;
        //            descriptor.ShortcutKey = attr.ShortcutKey;
        //            descriptor.ImageResource = attr.ImageResource;
        //            descriptor.Description = attr.Description;
        //            string resource = attr.ImageResource;
        //            if (resource == null || resource.Trim().Length == 0)
        //            {
        //                resource = commandModuleType.Namespace + "." + name + ".bmp";
        //            }
        //            if (resource != null)
        //            {
        //                descriptor.Image = ToolboxBitmapAttribute.GetImageFromResource(commandModuleType, resource.Trim(), false);
        //            }
        //            if (descriptor.Description == null || descriptor.Description.Trim().Length == 0)
        //            {
        //                // 获得说明
        //                DescriptionAttribute da = (DescriptionAttribute)Attribute.GetCustomAttribute(
        //                    m,
        //                    typeof(DescriptionAttribute),
        //                    true);
        //                if (da != null)
        //                {
        //                    descriptor.Description = da.Description;
        //                }
        //            }
        //            list.Add(descriptor);
        //        }
        //    }//foreach

        //    return list;
        //}

        ///// <summary>
        ///// 根据对象方法创建动作列表
        ///// </summary>
        ///// <param name="objInstance">对象实例</param>
        ///// <returns>创建的动作列表</returns>
        //public static WriterCommandList CreateCommands(Type ActionContainerType, object objInstance)
        //{
        //    if (ActionContainerType == null)
        //    {
        //        throw new ArgumentNullException("ActionContainerType");
        //    }

        //    WriterCommandList list = new WriterCommandList();

        //    System.Reflection.MethodInfo[] ms = ActionContainerType.GetMethods(
        //        System.Reflection.BindingFlags.Instance
        //        | System.Reflection.BindingFlags.Public
        //        | System.Reflection.BindingFlags.NonPublic);
        //    foreach (System.Reflection.MethodInfo m in ms)
        //    {
        //        //System.Console.WriteLine( m.DeclaringType.Name + "*" + m.Name);

        //        if (m.DeclaringType.Equals(typeof(System.Windows.Forms.Form)))
        //            continue;
        //        if (m.DeclaringType.Equals(typeof(System.Windows.Forms.Control)))
        //            continue;
        //        //System.Console.WriteLine( m.Name );
        //        WriterCommandDescriptionAttribute attr = (WriterCommandDescriptionAttribute)Attribute.GetCustomAttribute(
        //            m, typeof(WriterCommandDescriptionAttribute), false);
        //        if (attr == null)
        //        {
        //            continue;
        //        }
        //        //System.Console.WriteLine( "+++++++" + m.Name);
        //        if (m.ReturnType.Equals(typeof(void)) == false)
        //        {
        //            continue;
        //        }
        //        System.Reflection.ParameterInfo[] ps = m.GetParameters();
        //        if (ps == null || ps.Length != 2)
        //        {
        //            continue;
        //        }
        //        if (ps[0].ParameterType.Equals(typeof(object))
        //            || ps[1].ParameterType.Equals(typeof(WriterCommandDescriptionAttribute)))
        //        {

        //            System.Delegate handler = null;
        //            if (objInstance != null)
        //            {
        //                handler = System.Delegate.CreateDelegate(typeof(WriterCommandEventHandler), objInstance, m);
        //            }
        //            string name = attr.CommandName;
        //            //System.EventHandler h = new EventHandler( objInstance , m.Name );// m.MethodHandle );
        //            if (name == null || name.Trim().Length == 0)
        //            {
        //                name = m.Name;
        //            }
        //            WriterCommandDelegate act = new WriterCommandDelegate(name, (WriterCommandEventHandler)handler);
        //            //act.ForUIDesigner = attr.ForUIDesigner ;
        //            act.ShortcutKey = attr.ShortcutKey;
        //            string resource = attr.ImageResource;
        //            if (resource == null || resource.Length == 0)
        //            {
        //                resource = ActionContainerType.Namespace + "." + name + ".bmp";
        //            }
        //            if (resource != null)
        //            {
        //                System.IO.Stream stream = ActionContainerType.Assembly.GetManifestResourceStream(resource);
        //                if (stream != null)
        //                {
        //                    System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
        //                    if (img is System.Drawing.Bitmap)
        //                    {
        //                        System.Drawing.Bitmap bmp = (System.Drawing.Bitmap)img;
        //                        //bmp.MakeTransparent();
        //                        if (bmp.Width > 1 && bmp.Height > 1)
        //                        {
        //                            System.Drawing.Color c = bmp.GetPixel(0, bmp.Height - 1);
        //                            bmp.MakeTransparent(c);
        //                        }
        //                    }
        //                    act.ToolbarImage = img;
        //                    stream.Close();
        //                }
        //            }
        //            list.Add(act);
        //        }
        //    }//foreach

        //    return list;
        //}//public static DesignerActionList CreateActions( object objInstance )
    }
}
