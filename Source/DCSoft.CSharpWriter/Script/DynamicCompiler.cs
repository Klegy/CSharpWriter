/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using System.Text;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Specialized;
using System.Collections;

namespace DCSoft.Script
{
    /// <summary>
    /// dynamic source code compiler
    /// </summary>
    /// <remarks>
    /// This type can dynamic compile program source code at runtime 
    /// and genereate .net assembly, this type support VB.NET and C#.
    /// </remarks>
    [Serializable()]
    public class DynamicCompiler
    {
        /// <summary>
        /// initialize instance
        /// </summary>
        public DynamicCompiler()
        {
        }

        /// <summary>
        /// initialize instance
        /// </summary>
        /// <param name="language">programming language type</param>
        /// <param name="sourceCode">source code text</param>
        public DynamicCompiler(CompilerLanguage language, string sourceCode)
        {
            intLanguage = language;
            strSourceCode = sourceCode;
        }

        private CompilerLanguage intLanguage = CompilerLanguage.CSharp;
        /// <summary>
        /// programming language
        /// </summary>
        [System.ComponentModel.DefaultValue(CompilerLanguage.CSharp)]
        public CompilerLanguage Language
        {
            get
            {
                return intLanguage;
            }
            set
            {
                intLanguage = value;
            }
        }

        /// <summary>
        /// namespaces import to VB compiler
        /// </summary>
        private MyStringList myCompilerImports = new MyStringList();
        /// <summary>
        /// namespaces import to VB compiler
        /// </summary>
        public MyStringList CompilerImports
        {
            get
            {
                return myCompilerImports;
            }
        }

        private MyStringList myReferenceAssemblies = new MyStringList();
        /// <summary>
        /// reference assemblys for compile
        /// </summary>
        [System.Xml.Serialization.XmlArrayItem("Name", typeof(string))]
        public MyStringList ReferenceAssemblies
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


        private bool bolAutoLoadResultAssembly = true;
        /// <summary>
        /// Whether load result assebly automatic
        /// </summary>
        [System.ComponentModel.DefaultValue( true )]
        public bool AutoLoadResultAssembly
        {
            get
            {
                return bolAutoLoadResultAssembly; 
            }
            set
            {
                bolAutoLoadResultAssembly = value; 
            }
        }

        private string strSourceCode = null;
        /// <summary>
        /// programming source code text
        /// </summary>
        [System.ComponentModel.DefaultValue(null)]
        public string SourceCode
        {
            get
            {
                return strSourceCode;
            }
            set
            {
                strSourceCode = value;
            }
        }


        [NonSerialized()]
        private AppDomain myAppDomain = AppDomain.CurrentDomain;
        /// <summary>
        /// domain for compile
        /// </summary>
        public AppDomain AppDomain
        {
            get
            {
                return myAppDomain; 
            }
            set
            {
                myAppDomain = value; 
            }
        }

        [NonSerialized()]
        private System.Reflection.Assembly myResultAssembly = null;
        /// <summary>
        /// the result assembly of compile
        /// </summary>
        public System.Reflection.Assembly ResultAssembly
        {
            get
            {
                return myResultAssembly;
            }
        }

        private byte[] bsResultAssemblyBinary = null;
        /// <summary>
        /// the binary of result assembly
        /// </summary>
        public byte[] ResultAssemblyBinary
        {
            get
            {
                return bsResultAssemblyBinary; 
            }
            set
            {
                bsResultAssemblyBinary = value; 
            }
        }

        private CompilerErrorCollection myCompilerErrors = null;
        /// <summary>
        /// compile error list
        /// </summary>
        public CompilerErrorCollection CompilerErrors
        {
            get
            {
                return myCompilerErrors;
            }
        }

        private string strCompilerOutput = null;
        /// <summary>
        /// compiler output text
        /// </summary>
        public string CompilerOutput
        {
            get
            {
                return strCompilerOutput;
            }
        }

        private CompilerParameters _myCompilerParameters = null ;//new CompilerParameters();
        /// <summary>
        /// compile parameters
        /// </summary>
        public CompilerParameters CompilerParameters
        {
            get
            {
                if (_myCompilerParameters == null)
                    _myCompilerParameters = new CompilerParameters();
                return _myCompilerParameters;
            }
        }


        private string strAssemblyFileName = null;
        /// <summary>
        /// result assembly file name
        /// </summary>
        public string AssemblyFileName
        {
            get
            {
                return strAssemblyFileName; 
            }
            set
            {
                strAssemblyFileName = value; 
            }
        }

        private string strRuntimeAssemblyFileName = null;
        /// <summary>
        /// result assembly file name in fact.
        /// </summary>
        public string RuntimeAssemblyFileName
        {
            get
            {
                return strRuntimeAssemblyFileName; 
            }
        }

        private bool bolPreserveAssemblyFile = false ;
        /// <summary>
        /// Whether preserve result assembly file
        /// </summary>
        [System.ComponentModel.DefaultValue(false)]
        public bool PreserveAssemblyFile
        {
            get
            {
                return bolPreserveAssemblyFile; 
            }
            set
            {
                bolPreserveAssemblyFile = value; 
            }
        }

        private bool bolOutputDebug = true;
        /// <summary>
        /// whether output debug text
        /// </summary>
        public bool OutputDebug
        {
            get
            {
                return bolOutputDebug; 
            }
            set
            {
                bolOutputDebug = value; 
            }
        }

        private bool bolHasError = false;
        /// <summary>
        /// has compile error.
        /// </summary>
        public bool HasError
        {
            get
            {
                return bolHasError; 
            }
            set
            {
                bolHasError = value; 
            }
        }

        public string CompilerErrorMessage
        {
            get
            {
                StringBuilder str = new StringBuilder();
                if (this.CompilerErrors != null)
                {
                    foreach (CompilerError err in this.CompilerErrors)
                    {
                        str.AppendLine(err.ErrorText);
                    }
                }
                return str.ToString();
            }
        }

        /// <summary>
        /// source code embed line number and compile error , help user to debug.
        /// </summary>
        /// <remarks>
        /// This property return source code like the following
        /// 01: dim strValue as String
        /// 02: strValue = This.Text
        /// 03:if strValue.lengt h <> 40 then
        ///   Error:Must be end of expression
        ///   Error:"lengt" is not member of "String".
        /// 04:   return nothing
        /// 05:end if
        /// </remarks>
        public string SourceCodeWithCompilerErrorMessage
        {
            get
            {
                System.IO.StringReader reader = new System.IO.StringReader( strSourceCode );
                string strLine = reader.ReadLine();
                ArrayList list = new ArrayList();
                while (strLine != null)
                {
                    list.Add(strLine);
                    strLine = reader.ReadLine();
                }
                reader.Close();
                string format = new string('0', (int)(Math.Ceiling(Math.Log10(list.Count + 1))));
                System.Text.StringBuilder myStr = new StringBuilder();
                System.Collections.ArrayList errors = new ArrayList(myCompilerErrors);
                for (int iCount = 1; iCount <= list.Count; iCount++)
                {
                    if (myStr.Length > 0)
                    {
                        myStr.Append(Environment.NewLine);
                    }
                    myStr.Append(iCount.ToString(format)).Append(":").Append((string)list[iCount - 1]);
                    if (myCompilerErrors != null && myCompilerErrors.Count > 0)
                    {
                        for( int iCount2 = errors.Count - 1; iCount2 >= 0 ; iCount2 -- )
                        {
                            CompilerError err = (CompilerError)errors[iCount2];
                            if (err.Line == iCount)
                            {
                                myStr.Append(Environment.NewLine);
                                if (err.IsWarning)
                                {
                                    myStr.Append("  " + ScriptStrings.Warring + ":");
                                }
                                else
                                {
                                    myStr.Append("  " + ScriptStrings.Error + ":");
                                }
                                myStr.Append(err.ErrorText);
                                errors.RemoveAt(iCount2);
                            }
                        }
                    }
                }
                if (errors.Count > 0)
                {
                    foreach (CompilerError err in errors)
                    {
                        if (myStr.Length > 0)
                        {
                            myStr.Append(Environment.NewLine);
                        }
                        if (err.IsWarning)
                        {
                            myStr.Append("   " + ScriptStrings.Warring + ":");
                        }
                        else
                        {
                            myStr.Append("   " + ScriptStrings.Error + ":");
                        }
                        myStr.Append(err.ErrorText);
                    }
                }
                return myStr.ToString();
            }
        }

        private bool bolThrowException = false;
        /// <summary>
        /// throw exception when happend error.
        /// </summary>
        [System.ComponentModel.DefaultValue( false )]
        public bool ThrowException
        {
            get
            {
                return bolThrowException; 
            }
            set
            {
                bolThrowException = value; 
            }
        }

        /// <summary>
        /// compile source code
        /// </summary>
        /// <returns>whether compile is ok</returns>
        public bool Compile()
        {
            bool bolResult = false;
            
            bolHasError = false;
            strCompilerOutput = null;
            myCompilerErrors = new CompilerErrorCollection();
            myResultAssembly = null;
            this.bsResultAssemblyBinary = null;

            // add referenced assemblys information
            this.CompilerParameters.ReferencedAssemblies.Clear();
            foreach (string asm in this.ReferenceAssemblies)
            {
                this.CompilerParameters.ReferencedAssemblies.Add(asm);
            }

            // check domain
            ResolveEventHandler ResolveHandler = new ResolveEventHandler(myAppDomain_AssemblyResolve);
            if (myAppDomain != null)
            {
                myAppDomain.AssemblyResolve += ResolveHandler;
            }
            try
            {
                // compile
                this.CompilerParameters.GenerateExecutable = false;
                this.CompilerParameters.GenerateInMemory = false;
                this.CompilerParameters.IncludeDebugInformation = false;
                strRuntimeAssemblyFileName = this.AssemblyFileName;
                if (strRuntimeAssemblyFileName == null 
                    || strRuntimeAssemblyFileName.Trim().Length == 0)
                {
                    strRuntimeAssemblyFileName = System.IO.Path.GetTempFileName() + ".dll";
                }
                this.CompilerParameters.OutputAssembly = strRuntimeAssemblyFileName;

                if (this.CompilerImports != null && this.CompilerImports.Count > 0)
                {
                    System.Text.StringBuilder opt = new System.Text.StringBuilder();
                    foreach (string import in this.CompilerImports)
                    {
                        if (opt.Length > 0)
                        {
                            opt.Append(",");
                        }
                        opt.Append(import.Trim());
                    }
                    opt.Insert(0, " /imports:");
                    this.CompilerParameters.CompilerOptions = opt.ToString();
                }//if

                if (this.bolOutputDebug)
                {
                    System.Diagnostics.Debug.WriteLine(
                        ScriptStrings.StartDynamicCompile + "\r\n" + strSourceCode);
                    foreach (string dll in this.CompilerParameters.ReferencedAssemblies)
                    {
                        System.Diagnostics.Debug.WriteLine( ScriptStrings.Reference + ":" + dll);
                    }
                }
                CompilerResults result = null;
                if (this.Language == CompilerLanguage.VB)
                {
                    Microsoft.VisualBasic.VBCodeProvider provider
                        = new Microsoft.VisualBasic.VBCodeProvider();
 
                    result = provider.CompileAssemblyFromSource(
                        this.CompilerParameters,
                        strSourceCode );
 
                    provider.Dispose();
                }
                else if (this.Language == CompilerLanguage.CSharp)
                {
                    Microsoft.CSharp.CSharpCodeProvider provider
                        = new Microsoft.CSharp.CSharpCodeProvider();
 
                    result = provider.CompileAssemblyFromSource(
                        this.CompilerParameters,
                        strSourceCode);
 
                    provider.Dispose();

                }
                else
                {
                    throw new Exception( string.Format( 
                        ScriptStrings.NotSupportLanguage_Language , this.Language ));
                }
                System.Text.StringBuilder myOutput = new System.Text.StringBuilder();
                foreach (string line in result.Output)
                {
                    myOutput.Append("\r\n" + line);
                }
                this.strCompilerOutput = myOutput.ToString();
                if (this.OutputDebug)
                {
                    if (this.strCompilerOutput.Length > 0)
                    {
                        System.Diagnostics.Debug.WriteLine(
                            ScriptStrings.DymamicCompilerResult + strCompilerOutput);
                    }
                }
                myCompilerErrors = result.Errors;
                if (result.Errors.HasErrors)
                {
                    bolResult = false;
                    bolHasError = true;
                }
                else
                {
                    if (this.AutoLoadResultAssembly)
                    {
                        if (this.CompilerParameters.GenerateInMemory)
                        {
                            this.myResultAssembly = result.CompiledAssembly;
                        }
                        else
                        {
                            bsResultAssemblyBinary = null;
                            using (System.IO.FileStream stream = new System.IO.FileStream(
                                this.CompilerParameters.OutputAssembly,
                                System.IO.FileMode.Open,
                                System.IO.FileAccess.Read))
                            {
                                bsResultAssemblyBinary = new byte[stream.Length];
                                stream.Read(
                                    bsResultAssemblyBinary , 
                                    0,
                                    bsResultAssemblyBinary.Length);
                            }
                            this.myResultAssembly = myAppDomain.Load( 
                                bsResultAssemblyBinary , 
                                null,
                                this.CompilerParameters.Evidence);
                        }
                    }
                    bolResult = true;
                }
                if (this.PreserveAssemblyFile == false)
                {
                    if (System.IO.File.Exists(strRuntimeAssemblyFileName))
                    {
                        System.IO.File.Delete(strRuntimeAssemblyFileName);
                    }
                }
            }//if
            catch (Exception ext)
            {
                System.Diagnostics.Debug.WriteLine( 
                    string.Format( ScriptStrings.CompileError_Message , ext.Message ));
                if (bolThrowException)
                {
                    throw ext;
                }
            }
            if (myAppDomain != null)
            {
                myAppDomain.AssemblyResolve -= ResolveHandler;
            }
            return bolResult;
        }

        private System.Reflection.Assembly myAppDomain_AssemblyResolve(
            object sender, 
            ResolveEventArgs args)
        {
            System.AppDomain domain = (System.AppDomain)sender;
            foreach (System.Reflection.Assembly asm in domain.GetAssemblies())
            {
                if (asm.FullName == args.Name)
                {
                    return asm;
                }
            }
            return null;
        }
    }//public class DynamicCompiler

    /// <summary>
    /// programming language type
    /// </summary>
    public enum CompilerLanguage
    {
        /// <summary>
        /// C#
        /// </summary>
        CSharp,
        /// <summary>
        /// VB.NET
        /// </summary>
        VB
    }
}