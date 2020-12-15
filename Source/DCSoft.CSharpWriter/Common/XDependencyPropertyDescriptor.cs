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
namespace DCSoft.Common
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    public class XDependencyPropertyDescriptor : PropertyDescriptor
    {
        public XDependencyPropertyDescriptor(XDependencyProperty property , Attribute[] attrs )
            :base( property.Name , attrs)
        {
            if (property == null)
            {
                throw new ArgumentNullException("property");
            }
            _Property = property;
        }

        private XDependencyProperty _Property = null;
        public override bool CanResetValue(object component)
        {
            return true;
        }

        public override Type ComponentType
        {
            get
            {
                return _Property.OwnerType;
            }
        }

        public override object GetValue(object component)
        {
            XDependencyObject obj = component as XDependencyObject;
            if (obj == null)
            {
                return null;
            }
            else
            {
                return obj.GetValue(this._Property);
            }
        }

        public override bool IsReadOnly
        {
            get
            {
                return this._Property.IsReadOnly;   
            }
        }

        public override Type PropertyType
        {
            get 
            {
                return this._Property.PropertyType;
            }
        }

        public override void ResetValue(object component)
        {
            XDependencyObject obj = component as XDependencyObject;
            if (obj != null)
            {
                obj.SetValue(this._Property, this._Property.DefaultValue);
            }
        }

        public override void SetValue(object component, object value)
        {
            XDependencyObject obj = component as XDependencyObject;
            if (obj != null)
            {
                obj.SetValue(this._Property, value);
            }
        }

        public override bool ShouldSerializeValue(object component)
        {
            XDependencyObject obj = component as XDependencyObject;
            if (obj != null)
            {
                object v = obj.GetValue(this._Property);
                if (this._Property.EqualsDefaultValue(v))
                {
                    return false;
                }
            }
            return true;
        }

        public override string Category
        {
            get
            {
                return this._Property.Category;
            }
        }

        public override TypeConverter Converter
        {
            get
            {
                return this._Property.TypeConverter;
            }
        }

        public override string Description
        {
            get
            {
                return this._Property.Description;
            }
        }

        public override bool DesignTimeOnly
        {
            get
            {
                return this._Property.DesignTimeOnly;
            }
        }

        public override string DisplayName
        {
            get
            {
                return this._Property.DisplayName ;
            }
        }

        public override bool IsBrowsable
        {
            get
            {
                return this._Property.IsBrowsable;
            }
        }

        public override string Name
        {
            get
            {
                return this._Property.Name;
            }
        }

        public override bool IsLocalizable
        {
            get
            {
                return this._Property.IsLocalizable;
            }
        }

        public override object GetEditor(Type editorBaseType)
        {
            if (this._Property.Editor != null
                && editorBaseType.IsInstanceOfType(this._Property.Editor))
            {
                return this._Property.Editor;
            }
            else
            {
                return null;
            }
        }
    }

}
