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

namespace DCSoft.CSharpWriter.Dom
{
    /// <summary>
    /// 描述文档元素类型的属性
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [AttributeUsage(AttributeTargets.Class )]
    public class ElementDescriptorAttribute : Attribute
    {
        public ElementDescriptorAttribute()
        {
        }

        private Type _PropertiesType = null;
        /// <summary>
        /// 元素属性包对象类型
        /// </summary>
        public Type PropertiesType
        {
            get { return _PropertiesType; }
            set { _PropertiesType = value; }
        }

        public static ElementDescriptorAttribute GetDescriptor(Type elementType)
        {
            if (elementType == null)
            {
                throw new ArgumentNullException("elementType");
            }
            if (elementType.Equals(typeof( DomElement)) == false
                && elementType.IsSubclassOf(typeof(DomElement)) == false)
            {
                throw new ArgumentOutOfRangeException(elementType.FullName);
            }
            ElementDescriptorAttribute attr = (ElementDescriptorAttribute)Attribute.GetCustomAttribute(elementType, typeof(ElementDescriptorAttribute), true);
            return attr;
        }
    }
}
