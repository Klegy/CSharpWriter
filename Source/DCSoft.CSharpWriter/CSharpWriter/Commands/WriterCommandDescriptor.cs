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
using System.Reflection ;

namespace DCSoft.CSharpWriter.Commands
{
    /// <summary>
    /// 编辑器命令描述信息对象
    /// </summary>
    [Serializable()]
    public class WriterCommandDescriptor : System.IComparable 
    {
        /// <summary>
        /// 根据指定的模块类型和方法创建命令描述对象
        /// </summary>
        /// <param name="moduleType">模块类型</param>
        /// <param name="method">方法对象</param>
        /// <param name="throwException">若操作失败是否抛出异常</param>
        /// <returns>命令描述对象</returns>
        public static WriterCommandDescriptor Create(Type moduleType, MethodInfo method, bool throwException )
        {
            if( moduleType == null )
            {
                if( throwException  )
                {
                    throw new ArgumentNullException("moduleType");
                }
            }
            if (method == null)
            {
                if (throwException)
                {
                    throw new ArgumentNullException("method");
                }
                else
                {
                    return null;
                }
            }
            WriterCommandDescriptionAttribute attr = (WriterCommandDescriptionAttribute)
                    Attribute.GetCustomAttribute(
                    method,
                    typeof(WriterCommandDescriptionAttribute),
                    false);
            System.Reflection.ParameterInfo[] ps = method.GetParameters();
            if ( attr == null 
                || method.ReturnType.Equals( typeof( void )) == false 
                || ps == null
                || ps.Length != 2)
            {
                if (throwException)
                {
                    throw new ArgumentException(moduleType.FullName + "#" + method.Name);
                }
                else
                {
                    return null;
                }
            }
            if (ps[0].ParameterType.Equals(typeof(object))
                && ps[1].ParameterType.Equals(typeof(WriterCommandEventArgs)))
            {
                string name = attr.Name;
                //System.EventHandler h = new EventHandler( objInstance , m.Name );// m.MethodHandle );
                if (name == null || name.Trim().Length == 0)
                {
                    name = method.Name;
                }
                WriterCommandDescriptor descriptor = new WriterCommandDescriptor();
                descriptor.CommandName = name;
                descriptor.ContainerType = moduleType;
                descriptor.Method = method;
                descriptor.ShortcutKey = attr.ShortcutKey;
                descriptor.ImageResource = attr.ImageResource;
                descriptor.Description = attr.Description;
                string resource = attr.ImageResource;
                if (resource == null || resource.Trim().Length == 0)
                {
                    resource = moduleType.Namespace + "." + name + ".bmp";
                }
                if (resource != null)
                {
                    descriptor.Image = CommandUtils.GetResourceImage(moduleType.Assembly, resource.Trim());
                }
                //if (descriptor.Image == null)
                //{
                //    descriptor.Image = CommandUtils.NullImage;
                //}
                if (descriptor.Description == null || descriptor.Description.Trim().Length == 0)
                {
                    // 获得说明
                    DescriptionAttribute da = (DescriptionAttribute)Attribute.GetCustomAttribute(
                        method,
                        typeof(DescriptionAttribute),
                        true);
                    if (da != null)
                    {
                        descriptor.Description = da.Description;
                    }
                }
                return descriptor;
            }
            else
            {
                if (throwException)
                {
                    throw new ArgumentException(moduleType.FullName + "#" + method.Name);
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 根据指定的命令对象类型创建命令描述对象
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="throwException">若操作失败是否抛出异常</param>
        /// <returns>创建的命令描述对象</returns>
        public static WriterCommandDescriptor Create(Type commandType , bool throwException )
        {
            if (commandType == null)
            {
                if (throwException)
                {
                    throw new ArgumentNullException("commandType");
                }
                else
                {
                    return null;
                }
            }
            if (commandType.IsSubclassOf(typeof(WriterCommand)) == false)
            {
                if (throwException)
                {
                    throw new ArgumentException(commandType.FullName);
                }
                else
                {
                    return null;
                }
            }
            WriterCommandDescriptionAttribute attr = (WriterCommandDescriptionAttribute)Attribute.GetCustomAttribute(
                    commandType, typeof(WriterCommandDescriptionAttribute), false);
            //if (attr == null)
            //{
            //    if (throwException)
            //    {
            //        throw new ArgumentException(commandType.FullName);
            //    }
            //    else
            //    {
            //        return null;
            //    }
            //}
            WriterCommandDescriptor descriptor = new WriterCommandDescriptor();
            descriptor.ContainerType = commandType;
            descriptor.CommandName = commandType.Name;
            if (attr != null)
            {
                descriptor.CommandName = attr.Name;
                descriptor.Description = attr.Description;
                descriptor.ImageResource = attr.ImageResource;
                descriptor.ShortcutKey = attr.ShortcutKey;
                string src = attr.ImageResource;
                if (src != null && src.Trim().Length > 0)
                {
                    descriptor.Image = CommandUtils.GetResourceImage(commandType.Assembly, src.Trim());
                }
            }
            //if (descriptor.Image == null)
            //{
            //    descriptor.Image = CommandUtils.NullImage;
            //}
            if (descriptor.Description == null || descriptor.Description.Trim().Length == 0)
            {
                DescriptionAttribute da = (DescriptionAttribute)Attribute.GetCustomAttribute(
                    commandType,
                    typeof(DescriptionAttribute),
                    true);
                if (da != null)
                {
                    descriptor.Description = da.Description;
                }
            }
            return descriptor;
        }
        /// <summary>
        /// 初始化对象
        /// </summary>
        public WriterCommandDescriptor()
        {
        }

        private string _CommandName = null;
        /// <summary>
        /// 命令名称
        /// </summary>
        public string CommandName
        {
            get { return _CommandName; }
            set { _CommandName = value; }
        }

        private string _ModuleName = null;
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName
        {
            get { return _ModuleName; }
            set { _ModuleName = value; }
        }

        private string _ImageResource = null;
        /// <summary>
        /// 命令使用的图标资源名
        /// </summary>
        public string ImageResource
        {
            get { return _ImageResource; }
            set { _ImageResource = value; }
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

        private string _Description = null;
        /// <summary>
        /// 命令说明
        /// </summary>
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        private System.Drawing.Image _Image = null;
        /// <summary>
        /// 图标对象
        /// </summary>
        public System.Drawing.Image Image
        {
            get { return _Image; }
            set { _Image = value; }
        }

        private Type _ContainerType = null;
        /// <summary>
        /// 容器对象
        /// </summary>
        public Type ContainerType
        {
            get { return _ContainerType; }
            set { _ContainerType = value; }
        }

        private System.Reflection.MethodInfo _Method = null;
        /// <summary>
        /// 对应的方法对象
        /// </summary>
        public System.Reflection.MethodInfo Method
        {
            get { return _Method; }
            set { _Method = value; }
        }

        public override string ToString()
        {
            return "Command:" + this.CommandName;
        }

        #region IComparable 成员

        int IComparable.CompareTo(object obj)
        {
            if (obj is WriterCommandDescriptor)
            {
                return string.Compare(this.CommandName, ((WriterCommandDescriptor)obj).CommandName, true);
            }
            return 0;
        }

        #endregion
    }
}