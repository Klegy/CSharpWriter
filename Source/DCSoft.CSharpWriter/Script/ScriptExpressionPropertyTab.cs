/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace DCSoft.Script
{
    /// <summary>
    /// script expression proerty tab, it can add a button in the toolbar of PropertyGrid control.
    /// </summary>
    public class ScriptExpressionPropertyTab : PropertyTab
    {

        private static System.Drawing.Design.UITypeEditor myEditor = null;
        /// <summary>
        /// Editor for script expression property value 
        /// </summary>
        public static System.Drawing.Design.UITypeEditor Editor
        {
            get
            {
                return myEditor;
            }
            set
            {
                myEditor = value;
            }
        }

        /// <summary>
        /// Initialize instance
        /// </summary>
        public ScriptExpressionPropertyTab()
        {
        }

        // Returns the properties of the specified component extended with 
        // a CategoryAttribute reflecting the name of the type of the property.
        public override System.ComponentModel.PropertyDescriptorCollection GetProperties(
            object component, System.Attribute[] attributes)
        {
            PropertyDescriptorCollection props;
            //TypeConverter tc = TypeDescriptor.GetConverter(component);
            //if (tc != null)
            //{
            //    props = tc.GetProperties(component);
            //    props.
            //}
            if (attributes == null)
                props = TypeDescriptor.GetProperties(component);
            else
                props = TypeDescriptor.GetProperties(component, attributes);

            PropertyDescriptor[] propArray = new PropertyDescriptor[props.Count];
            System.Collections.ArrayList list = new System.Collections.ArrayList();
            foreach (PropertyDescriptor p in props)
            {
                foreach (Attribute a in p.Attributes)
                {
                    if (a is ScriptExpressionIgnoreAttribute)
                    {
                        goto NextProperty;
                    }

                    if (a is System.ComponentModel.ReadOnlyAttribute)
                        goto NextProperty;

                    if (a is ObsoleteAttribute)
                        goto NextProperty;
                }
                list.Add(new ScriptExpressionPropertyDescriptor( p , attributes));
            NextProperty: ;
            }
            TypeConverter tc = TypeDescriptor.GetConverter(component);
            if (tc != null)
            {
                PropertyDescriptorCollection ps = tc.GetProperties(component);
                if (ps != null && ps.Count > 0)
                {
                    foreach (ScriptExpressionPropertyDescriptor p in list)
                    {
                        PropertyDescriptor p2 = ps.Find(p.Name, false);
                        if (p2 != null)
                        {
                            p.InnerDescription = p2.Description;
                            p.InnerDisplayName = p2.DisplayName;
                        }
                    }
                }
            }
            return new PropertyDescriptorCollection((PropertyDescriptor[])list.ToArray(
                typeof(PropertyDescriptor)));
        }

        public override System.ComponentModel.PropertyDescriptorCollection GetProperties(object component)
        {
            return this.GetProperties(component, null);
        }

        // Provides the name for the property tab.
        public override string TabName
        {
            get
            {
                return "Script";
            }
        }

        private static Bitmap myBmp = null;
        // Provides an image for the property tab.
        public override System.Drawing.Bitmap Bitmap
        {
            get
            {
                if (myBmp == null)
                {
                    System.IO.Stream stream = this.GetType().Assembly.GetManifestResourceStream(
                        "DCSoft.Script.ScriptExpressionPropertyTabBmp.bmp");
                    if (stream != null)
                    {
                        myBmp = new Bitmap(stream);
                    }
                }
                return myBmp;
            }
        }
    }
    /// <summary>
    /// Attribute for ignore expression.
    /// </summary>
    /// <remarks>
    /// When type's property has this attribute , Script expression
    /// property grid will not display the property.
    /// </remarks>
    [System.AttributeUsage( AttributeTargets.Property , AllowMultiple= false )]
    public class ScriptExpressionIgnoreAttribute : Attribute
    {
    }

    /// <summary>
    /// expression property descriptor
    /// </summary>
    public class ScriptExpressionPropertyDescriptor : PropertyDescriptor
    {
        /// <summary>
        /// Initialize instance
        /// </summary>
        /// <param name="property">base property</param>
        /// <param name="attrs">attributes</param>
        public ScriptExpressionPropertyDescriptor(
            PropertyDescriptor property , 
            Attribute[] attrs)
            : base( property.Name , attrs)
        {
            myProperty = property;
        }

        private PropertyDescriptor myProperty = null;
        
        public override bool CanResetValue(object component)
        {
            return true;
        }

        
       

        public override Type ComponentType
        {
            get
            {
                return myProperty.ComponentType;
            }
        }


        public override object GetEditor(Type editorBaseType)
        {
            if (editorBaseType.Name == "UITypeEditor")
            {
                return ScriptExpressionPropertyTab.Editor;
            }
            return myProperty.GetEditor( editorBaseType);
        }

        public override object GetValue(object component)
        {
            ScriptMemberExpressionContainer container 
                = ScriptMemberExpressionContainer.GetContainer(component);
            if (container == null)
                return null;
            else
                return container.GetScript( myProperty.Name );
        }


        public override void SetValue(object component, object Value)
        {
            ScriptMemberExpressionContainer container
                = ScriptMemberExpressionContainer.GetContainer(component);
            if (container != null)
                container.SetScript( myProperty.Name , (string)Value);
        }


        public override void ResetValue(object component)
        {
            ScriptMemberExpressionContainer container
                = ScriptMemberExpressionContainer.GetContainer(component);
            if (container != null)
                container.SetScript(myProperty.Name , null);
        }

        public override bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public override Type PropertyType
        {
            get
            {
                return typeof(string);
            }
        }


        public override bool ShouldSerializeValue(object component)
        {
            return true;
        }

        internal string InnerDescription = null;

        public override string Description
        {
            get
            {
                if (InnerDescription == null)
                    return "(" + ScriptStrings.ReturnType 
                        + " " + myProperty.PropertyType.Name + ") "
                        + myProperty.Description;
                else
                    return "(" + ScriptStrings.ReturnType
                        + " " + myProperty.PropertyType.Name + ") "
                        + InnerDescription;
            }
        }

        public override string Category
        {
            get
            {
                return myProperty.Category ;
            }
        }

        internal string InnerDisplayName = null;

        public override string DisplayName
        {
            get
            {
                if (InnerDisplayName == null)
                    return myProperty.DisplayName;
                else
                    return InnerDisplayName;
            }
        }

        public override bool IsBrowsable
        {
            get
            {
                return true;
            }
        }
    }
}
