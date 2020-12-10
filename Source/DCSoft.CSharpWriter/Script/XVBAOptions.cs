/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using System.Text;
using System.Collections.Specialized;

namespace DCSoft.Script
{
    /// <summary>
    /// VB.NET script engine's options
    /// </summary>
    [System.ComponentModel.Editor(
        "DCSoft.Script.ScriptOptionsEditor" , 
        typeof( System.Drawing.Design.UITypeEditor ))]
    [System.Serializable()]
    public class XVBAOptions : System.ICloneable
    {
        public XVBAOptions()
        {
            myImportNamespaces.Add("System");
            myImportNamespaces.Add("Microsoft.VisualBasic");

            myReferenceAssemblies.AddStandard("mscorlib");
            myReferenceAssemblies.AddStandard("System");
            myReferenceAssemblies.AddStandard("System.Data");
            myReferenceAssemblies.AddStandard("System.Xml");
            myReferenceAssemblies.AddStandard("System.Drawing");
            myReferenceAssemblies.AddStandard("System.Windows.Forms");
            myReferenceAssemblies.AddStandard("Microsoft.VisualBasic");
        }

        private MyStringList myImportNamespaces = new MyStringList();
        /// <summary>
        /// namespace imported in source code
        /// </summary>
        [System.Xml.Serialization.XmlArrayItem("Namespace" , typeof( string ))]
        public MyStringList ImportNamespaces
        {
            get
            {
                return myImportNamespaces; 
            }
            set
            {
                myImportNamespaces = value; 
            }
        }

        private DotNetAssemblyInfoList myReferenceAssemblies = new DotNetAssemblyInfoList();
        /// <summary>
        /// reference assemblies
        /// </summary>
        [System.Xml.Serialization.XmlArrayItem("DotNetAssemblyInfo" ,typeof( DotNetAssemblyInfo ))]
        public DotNetAssemblyInfoList ReferenceAssemblies
        {
            get
            {
                return myReferenceAssemblies; 
            }
            set
            {
                myReferenceAssemblies = value; 
            }
        }

        [NonSerialized()]
        private MyStringList myInnerReferenceAssemblies = new MyStringList();
        /// <summary>
        /// custom reference assemblies
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        [System.Xml.Serialization.XmlIgnore()]
        public MyStringList InnerReferenceAssemblies
        {
            get
            {
                if (myInnerReferenceAssemblies == null)
                    myInnerReferenceAssemblies = new MyStringList();
                return myInnerReferenceAssemblies; 
            }
            set
            {
                myInnerReferenceAssemblies = value; 
            }
        }

        object System.ICloneable.Clone()
        {
            XVBAOptions opt = new XVBAOptions();
            if (this.myImportNamespaces != null)
            {
                opt.myImportNamespaces = myImportNamespaces.Clone();
            }
            if (this.myReferenceAssemblies != null)
            {
                opt.myReferenceAssemblies = this.myReferenceAssemblies.Clone();
            }
            if (this.myInnerReferenceAssemblies != null)
            {
                opt.myInnerReferenceAssemblies = this.myInnerReferenceAssemblies.Clone();
            }
            return opt;
        }
        public XVBAOptions Clone()
        {
            return (XVBAOptions)((ICloneable)this).Clone();
        }

        public override string ToString()
        {
            return string.Format( 
                ScriptStrings.VBAOptionString_RefCount_NSCount , 
                this.myReferenceAssemblies.Count ,
                this.myImportNamespaces.Count) ;
        }

        /// <summary>
        /// get assembly's file name
        /// </summary>
        /// <param name="SourceType">object type</param>
        public static string GetReferenceAssemblyByType(Type SourceType)
        {
            if (SourceType == null)
            {
                throw new ArgumentNullException("SourceType");
            }
            System.Uri uri = new Uri(SourceType.Assembly.CodeBase);
            string path = null;
            if (uri.Scheme == System.Uri.UriSchemeFile)
            {
                path = uri.LocalPath;
            }
            else
            {
                path = SourceType.Assembly.Location;
            }
            return path;
        }
    }
}
