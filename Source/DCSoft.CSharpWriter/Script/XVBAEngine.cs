/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
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
    /// Script engine use VB.NET
    /// </summary>
    /// <remarks>
    /// This engine can dynamic VB.NET source code , and execute the method which define in the source code.
    /// this engine support MS.NET 1.1 , 2.0 or later.
    /// </remarks>
    [Serializable()]
    public class XVBAEngine : System.IDisposable
    {
        /// <summary>
        /// static initialize
        /// </summary>
        static XVBAEngine()
        {
            // set resource string cluture.
            ScriptStrings.Culture = System.Globalization.CultureInfo.CurrentUICulture;
        }

        /// <summary>
        /// Dynamic genereate collection instance specify object map , new instance has 
        /// fields member , those fields's name is key of map , field value is value of map.
        /// </summary>
        /// <param name="objs">object maps</param>
        /// <param name="BaseType">base type</param>
        /// <returns>dynamic genereate instance</returns>
        public static object CreateAllObject(System.Collections.Hashtable objs, System.Type BaseType)
        {
            if (objs == null)
                return null;
            System.Reflection.AssemblyName an = new System.Reflection.AssemblyName();
            an.Name = "XDesignerLib_AllObject_Assembly";
            System.Reflection.Emit.AssemblyBuilder ab = System.AppDomain.CurrentDomain.DefineDynamicAssembly(
                an,
                System.Reflection.Emit.AssemblyBuilderAccess.Run);
            System.Reflection.Emit.ModuleBuilder mb = ab.DefineDynamicModule("AllObjectModule");
            System.Reflection.Emit.TypeBuilder tb = null;
            if (BaseType == null)
                tb = mb.DefineType(
                    "AllObject" + System.Environment.TickCount,
                    System.Reflection.TypeAttributes.Public);
            else
                tb = mb.DefineType(
                    "AllObject" + System.Environment.TickCount,
                    System.Reflection.TypeAttributes.Public, BaseType);
            System.Collections.ArrayList Fields = new System.Collections.ArrayList();
            foreach (string key in objs.Keys)
            {
                if (key != null && key.Length != 0)
                {
                    bool bolFind = false;
                    foreach (System.Reflection.FieldInfo f in Fields)
                    {
                        if (string.Compare(f.Name, key, true) == 0)
                        {
                            bolFind = true;
                            break;
                        }
                    }
                    if (bolFind == false)
                        Fields.Add(tb.DefineField(
                            key, 
                            typeof(object),
                            System.Reflection.FieldAttributes.Public));
                }
            }//foreach
            System.Type t = tb.CreateType();
            object obj = System.Activator.CreateInstance(t);
            foreach (System.Reflection.FieldInfo field in Fields)
            {
                t.InvokeMember(
                    field.Name,
                    System.Reflection.BindingFlags.SetField,
                    null,
                    obj,
                    new object[] { objs[field.Name] });
            }//foreach
            return obj;
        }

        /// <summary>
        /// initialize instance
        /// </summary>
        public XVBAEngine()
        {
            Init();
        }

        /// <summary>
        /// initialize instance
        /// </summary>
        /// <param name="SourceCode">script source code</param>
        public XVBAEngine(string SourceCode)
        {
            Init();
            this.ScriptText = SourceCode;
        }

        /// <summary>
        /// initialize engine
        /// </summary>
        private void Init()
        {
            //this.mySourceImports.Add("System");
            ////this.mySourceImports.Add("System.Data");
            ////this.mySourceImports.Add("System.Windows.Forms");
            //this.mySourceImports.Add("Microsoft.VisualBasic");
            ////this.mySourceImports.Add("System.Drawing");

            this.myVBCompilerImports.Add("Microsoft.VisualBasic");

            //this.myCompilerParameters.ReferencedAssemblies.Add("mscorlib.dll");
            //this.myCompilerParameters.ReferencedAssemblies.Add("System.dll");
            //this.myCompilerParameters.ReferencedAssemblies.Add("System.Data.dll");
            //this.myCompilerParameters.ReferencedAssemblies.Add("System.Xml.dll");
            //this.myCompilerParameters.ReferencedAssemblies.Add("System.Drawing.dll");
            //this.myCompilerParameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            //this.myCompilerParameters.ReferencedAssemblies.Add("Microsoft.VisualBasic.dll");
        }

        //private string strAssemblyTitle = null;

        private XVBAOptions myOptions = new XVBAOptions();

        public XVBAOptions Options
        {
            get
            {
                if (myOptions == null)
                    myOptions = new XVBAOptions();
                return myOptions; 
            }
            set
            {
                myOptions = value; 
            }
        }

        private bool bolEnabled = true;
        /// <summary>
        /// enbale script engine
        /// </summary>
        public bool Enabled
        {
            get
            {
                return bolEnabled;
            }
            set
            {
                bolEnabled = value;
            }
        }

        private bool bolThrowException = false;
        /// <summary>
        /// whether throw exception when engine has error.
        /// </summary>
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

        private bool bolOutputDebug = true;
        /// <summary>
        /// whether script can ouput debug text at runtime.
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
       

        private StringCollection myReferencedAssemblies = new StringCollection();
        /// <summary>
        /// engine reference assemblies
        /// </summary>
        public StringCollection ReferencedAssemblies
        {
            get
            {
                return myReferencedAssemblies;
            }
        }
         
        /// <summary>
        /// namespace use in source code
        /// </summary>
        private StringCollection mySourceImports = new StringCollection();
        /// <summary>
        /// namespace use in source code
        /// </summary>
        public StringCollection SourceImports
        {
            get
            {
                return mySourceImports;
            }
        }

        /// <summary>
        /// namespace imported for VB compiler
        /// </summary>
        private StringCollection myVBCompilerImports = new StringCollection();
        /// <summary>
        /// namespace imported for VB compiler
        /// </summary>
        public StringCollection VBCompilerImports
        {
            get
            {
                return myVBCompilerImports;
            }
        }

        /// <summary>
        /// assembly generaged.
        /// </summary>
        [System.NonSerialized()]
        private System.Reflection.Assembly myAssembly = null;

        /// <summary>
        /// flag for script source code modified.
        /// </summary>
        private bool bolScriptModified = true;

        /// <summary>
        /// Native script source code.
        /// </summary>
        private string strScriptText = null;
        /// <summary>
        /// Native script source code.
        /// </summary>
        public string ScriptText
        {
            get
            {
                return strScriptText;
            }
            set
            {
                if (strScriptText != value)
                {
                    bolScriptModified = true;
                    strScriptText = value;
                }
            }
        }

        private string strRuntimeScriptText = null;
        /// <summary>
        /// current used script source code
        /// </summary>
        public string RuntimeScriptText
        {
            get
            {
                return strRuntimeScriptText;
            }
        }

        private string strRuntimeScriptTextWithCompilerErrorMessage = null;
        /// <summary>
        /// script in fact and with compiler error message , for user debug.
        /// </summary>
        public string RuntimeScriptTextWithCompilerErrorMessage
        {
            get
            {
                return strRuntimeScriptTextWithCompilerErrorMessage;
            }
        }

        private string strCompilerOutput = null;
        /// <summary>
        /// Compiler output.
        /// </summary>
        public string CompilerOutput
        {
            get
            {
                return strCompilerOutput;
            }
        }

        private int intScriptVersion = 0;
        /// <summary>
        /// Script version , every user modify script text , this version will increase.
        /// </summary>
        public int ScriptVersion
        {
            get
            {
                this.CheckReady();
                return intScriptVersion;
            }
        }

        [NonSerialized()]
        private XVBAScriptGlobalObjectList myGlobalObjects = new XVBAScriptGlobalObjectList();
        /// <summary>
        /// global object list use in script,etc. instance for document, window , event and so on.
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        [System.Xml.Serialization.XmlIgnore()]
        public XVBAScriptGlobalObjectList GlobalObjects
        {
            get
            {
                if (myGlobalObjects == null)
                {
                    myGlobalObjects = new XVBAScriptGlobalObjectList();
                }
                return myGlobalObjects; 
            }
            set
            {
                myGlobalObjects = value;
            }
        }

        [NonSerialized()]
        private static System.AppDomain myAppDomain = System.AppDomain.CurrentDomain;
        /// <summary>
        /// appdomain to contain script assembly.
        /// </summary>
        public static System.AppDomain AppDomain
        {
            get
            {
                return myAppDomain;
            }
            set
            {
                myAppDomain = value;
                if (myAssemblyBuffer != null)
                {
                    myAssemblyBuffer.AppDomain = myAppDomain;
                }
            }
        }

        [NonSerialized()]
        private static ScriptAssemblyBuffer myAssemblyBuffer = new ScriptAssemblyBuffer();
        /// <summary>
        /// assembly buffer
        /// </summary>
        public static ScriptAssemblyBuffer AssemblyBuffer
        {
            get
            {
                return myAssemblyBuffer; 
            }
            set
            {
                myAssemblyBuffer = value;
                if (myAssemblyBuffer != null)
                {
                    myAssemblyBuffer.AppDomain = myAppDomain;
                }
            }
        }
         

        /// <summary>
        /// Both information about script method
        /// </summary>
        [NonSerialized()]
        private ArrayList myScriptMethods = new ArrayList();

        private ArrayList ScriptMethods
        {
            get
            {
                if (myScriptMethods == null)
                    myScriptMethods = new ArrayList();
                return myScriptMethods;
            }
        }

        /// <summary>
        /// Script method information
        /// </summary>
        private class ScriptMethodInfo
        {
            /// <summary>
            /// assembly
            /// </summary>
            public System.Reflection.Assembly Assembly = null;
            /// <summary>
            /// VB Module name
            /// </summary>
            public string ModuleName = null;
            /// <summary>
            /// Method name
            /// </summary>
            public string MethodName = null;
            /// <summary>
            /// Method informaiton
            /// </summary>
            public System.Reflection.MethodInfo MethodObject = null;
            /// <summary>
            /// return type
            /// </summary>
            public System.Type ReturnType = null;
            /// <summary>
            /// delete to the method , use delegate can speed .
            /// </summary>
            public System.Delegate MethodDelegate = null;
            /// <summary>
            /// script text for this method
            /// </summary>
            public string ScriptText = null;
        }

        /// <summary>
        /// check script engine state
        /// </summary>
        /// <returns>whether engine is ready</returns>
        public virtual bool CheckReady()
        {
            if (bolClosedFlag)
            {
                return false;
            }
            if (this.Enabled == false)
            {
                return false;
            }
            if (bolScriptModified == false)
            {
                return myAssembly != null;
            }
            return Compile();
        }

        private CompilerErrorCollection myCompilerErrors = null;
        /// <summary>
        /// Compiler error list
        /// </summary>
        public CompilerErrorCollection CompilerErrors
        {
            get
            {
                return myCompilerErrors; 
            }
        }

        /// <summary>
        /// Compile script with debug mode
        /// </summary>
        /// <returns>result</returns>
        public bool DebugCompile()
        {
            myAssemblyBuffer.Clear();
            return Compile();
        }

        /// <summary>
        /// Compile script
        /// </summary>
        /// <returns>Compiler ok or not</returns>
        public virtual bool Compile()
        {
            strRuntimeScriptText = null;
            strCompilerOutput = null;
            myCompilerErrors = null;
            if (bolClosedFlag)
            {
                return false;
            }
            if (strScriptText == null || strScriptText.Trim().Length == 0)
            {
                return false;
            }
            intScriptVersion++;
            bool bolResult = false;
            this.ScriptMethods.Clear();
            myAssembly = null;
            bolScriptModified = false;

            // Generate integrated VB.NET source code

            string ModuleName = "mdlXVBAScriptEngine";
            string globalModuleName = "mdlXVBAScriptGlobalValues" ;
            string nsName = "NameSpaceXVBAScriptEngien";
            System.Text.StringBuilder mySource = new System.Text.StringBuilder();
            mySource.Append("Option Strict Off");
            foreach (string import in this.Options.ImportNamespaces)
            {
                mySource.Append("\r\nImports " + import);
            }
            mySource.Append("\r\nNamespace " + nsName);
            //System.Collections.Hashtable myGObjects = new Hashtable();
            // Generate source code for global objects
            if ( myGlobalObjects != null && myGlobalObjects.Count > 0)
            {
                myVBCompilerImports.Add( nsName );

                //mySource.Append("\r\n<Microsoft.VisualBasic.CompilerServices.StandardModule>");
                mySource.Append("\r\nModule " + globalModuleName);
                mySource.Append("\r\n");
                mySource.Append("\r\n    Public myGlobalValues As Object");
                foreach( XVBAScriptGlobalObject item in myGlobalObjects )
                {
                    if (System.Xml.XmlReader.IsName(item.Name) == false)
                    {
                        if (this.ThrowException)
                        {
                            throw new ArgumentException("Script global object name:" + item.Name);
                        }
                        else
                        {
                            if (this.OutputDebug)
                            {
                                System.Diagnostics.Debug.WriteLine("Script global object name:" + item.Name);
                            }
                            continue;
                        }
                    }
                    if (item.ValueType.Equals(typeof(object)) || item.ValueType.IsPublic == false )
                    {
                        mySource.Append("\r\n    Public ReadOnly Property " + item.Name + "() As Object ");
                        mySource.Append("\r\n      Get");
                        mySource.Append("\r\n         Return myGlobalValues(\"" + item.Name + "\") ");
                        mySource.Append("\r\n      End Get");
                        mySource.Append("\r\n    End Property");
                    }
                    else
                    {
                        string typeName = item.ValueType.FullName;
                        typeName = typeName.Replace("+", ".");
                        mySource.Append("\r\n    Public ReadOnly Property " + item.Name + "() As " + typeName);
                        mySource.Append("\r\n      Get");
                        mySource.Append("\r\n         Return CType( myGlobalValues(\"" + item.Name + "\") , " + typeName + ")");
                        mySource.Append("\r\n      End Get");
                        mySource.Append("\r\n    End Property");
                    }
                }
                mySource.Append("\r\nEnd Module");
            }

            mySource.Append("\r\nModule " + ModuleName);
            mySource.Append("\r\n");
            mySource.Append(this.strScriptText);
            mySource.Append("\r\nEnd Module");
            mySource.Append("\r\nEnd Namespace");
            strRuntimeScriptText = mySource.ToString();

            myAssembly = null;
            if (myAssemblyBuffer != null)
            {
                // Check assembly buffer
                myAssembly = myAssemblyBuffer.GetAssembly(strRuntimeScriptText);
            }
            if (myAssembly == null)
            {
                // Use dynamic compile

                DynamicCompiler compiler = new DynamicCompiler();
                compiler.Language = CompilerLanguage.VB;
                compiler.AppDomain = myAppDomain;
                compiler.PreserveAssemblyFile = false;
                compiler.OutputDebug = this.OutputDebug;
                compiler.ThrowException = this.ThrowException;
                compiler.SourceCode = strRuntimeScriptText;
                compiler.AutoLoadResultAssembly = true;
                // add referenced assemblies
                DotNetAssemblyInfoList refs = this.Options.ReferenceAssemblies.Clone();
                refs.AddByType(this.GetType());
                if (this.GlobalObjects.Count > 0)
                {
                    foreach (XVBAScriptGlobalObject item in this.GlobalObjects)
                    {
                        refs.AddByType(item.ValueType);
                    }
                }
                foreach (string asm in this.Options.InnerReferenceAssemblies)
                {
                    refs.AddByName(asm);
                }

				foreach( DotNetAssemblyInfo asm in refs )
				{
					if( asm.SourceStyle == AssemblySourceStyle.Standard )
					{
						compiler.ReferenceAssemblies.Add( System.IO.Path.GetFileName ( asm.FileName ));
					}
					else
					{
						compiler.ReferenceAssemblies.Add( asm.FileName );
					}
				}

                foreach (string ns in this.VBCompilerImports)
                {
                    compiler.CompilerImports.Add(ns);
                }
                if (compiler.Compile())
                {
                    myAssembly = compiler.ResultAssembly;
                    if (myAssemblyBuffer != null)
                    {
                        myAssemblyBuffer.AddAssembly(
                            strRuntimeScriptText, 
                            myAssembly, 
                            compiler.ResultAssemblyBinary);
                    }
                    bolResult = true;
                }
                else
                {
                    myCompilerErrors = compiler.CompilerErrors;
                    foreach (CompilerError error in myCompilerErrors )
                    {
                        if (error.IsWarning == false)
                        {
                            // report error
                            if (this.Errors != null)
                            {
                                ScriptError err = new ScriptError(this, ScriptErrorStyle.Compile, null, null);
                                err.Message = error.ErrorText;
                                err.ScriptText = error.ErrorText;
                                this.Errors.Add(err);
                            }
                            bolResult = false ;
                            break;
                        }
                    }
                }
                strRuntimeScriptTextWithCompilerErrorMessage = compiler.SourceCodeWithCompilerErrorMessage;
            }
            if (this.myAssembly != null)
            {
                // set global object instance
                Type ModuleType = myAssembly.GetType(nsName + "." + globalModuleName);
                if (ModuleName != null)
                {
                    System.Reflection.FieldInfo gf = ModuleType.GetField("myGlobalValues");
                    gf.SetValue(null, this.GlobalObjects );
                }

                // analyze script method
                ModuleType = myAssembly.GetType(nsName + "." + ModuleName);
                if (ModuleType != null)
                {
                    System.Reflection.MethodInfo[] ms = ModuleType.GetMethods(
                        System.Reflection.BindingFlags.Public
                        | System.Reflection.BindingFlags.NonPublic
                        | System.Reflection.BindingFlags.Static);
                    foreach (System.Reflection.MethodInfo m in ms)
                    {
                        // generate script method information
                        ScriptMethodInfo info = new ScriptMethodInfo();
                        info.Assembly = myAssembly;
                        info.MethodName = m.Name;
                        info.MethodObject = m;
                        info.ModuleName = ModuleType.Name;
                        info.ReturnType = m.ReturnType;
                        info.ScriptText = (string)myMethodSciptText[info.MethodName];
                        this.ScriptMethods.Add(info);
                        if (this.bolOutputDebug)
                        {
                            System.Diagnostics.Debug.WriteLine(
                                string.Format( ScriptStrings.AnalyseVBMethod_Name , m.Name ));
                        }
                    }//foreach
                    bolResult = true;
                }//if
            }
            return bolResult;
        }

        private System.Collections.Hashtable myMethodSciptText = new Hashtable();

        public void SetMethodScriptText(string methodName, string scriptText)
        {
            myMethodSciptText[methodName] = scriptText;
        }

        System.Reflection.Assembly myAppDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            System.AppDomain domain = ( System.AppDomain ) sender ;
            foreach (System.Reflection.Assembly asm in domain.GetAssemblies())
            {
                if (asm.FullName == args.Name)
                {
                    return asm;
                }
            }
            return null;
        }

        /// <summary>
        /// clear
        /// </summary>
        public void Clear()
        {
            this.strScriptText = "";
            bolClosedFlag = false;
            this.bolScriptModified = false;
            this.ScriptMethods.Clear();
            if (myGlobalObjects != null)
            {
                myGlobalObjects.Clear();
            }
            if (this.bolOutputDebug)
            {
                System.Diagnostics.Debug.WriteLine( ScriptStrings.ClearVBAEngine );
            }
        }

        private bool bolClosedFlag = false;
        /// <summary>
        /// 对象已经关闭了
        /// </summary>
        public bool IsClosed
        {
            get
            {
                return bolClosedFlag;
            }
        }

        /// <summary>
        /// Close
        /// </summary>
        public void Close()
        {
            bolClosedFlag = true;
            this.ScriptMethods.Clear();
            this.myAssembly = null;
            if (myGlobalObjects != null)
            {
                myGlobalObjects.Clear();
            }
            if (this.bolOutputDebug)
            {
                System.Diagnostics.Debug.WriteLine( ScriptStrings.CloseVBAEngine);
            }
        }

        /// <summary>
        /// get a string array which contains names of all script method
        /// </summary>
        public string[] ScriptMethodNames
        {
            get
            {
                if (CheckReady() == false)
                {
                    return null;
                }
                ArrayList names = new ArrayList();
                foreach (ScriptMethodInfo info in this.ScriptMethods)
                {
                    names.Add(info.MethodName);
                }
                return (string[])names.ToArray(typeof(string));
            }
        }

        /// <summary>
        /// Detect whether exist script method specify name
        /// </summary>
        /// <param name="MethodName">method name</param>
        /// <returns>whether exist script method</returns>
        public bool HasMethod(string MethodName)
        {
            if (CheckReady() == false)
            {
                return false;
            }
            if (this.ScriptMethods.Count > 0)
            {
                foreach (ScriptMethodInfo info in this.ScriptMethods)
                {
                    if (string.Compare(info.MethodName, MethodName, true) == 0)
                    {
                        return true;
                    }//if
                }//foreach
            }//if
            return false;
        }

        public System.Reflection.MethodInfo GetScriptMethod(string MethodName)
        {
            if (CheckReady() == false)
            {
                return null;
            }
            foreach (ScriptMethodInfo info in this.ScriptMethods)
            {
                if (string.Compare(info.MethodName, MethodName, true) == 0)
                {
                    return info.MethodObject;
                }
            }
            return null;
        }

        /// <summary>
        /// Execute script method safe and simple
        /// </summary>
        /// <param name="MethodName">Method name</param>
        public void ExecuteSub(string MethodName)
        {
            Execute(MethodName, null, false);
        }

        private ScriptErrorList myErrors = new ScriptErrorList();
        /// <summary>
        /// Script error list
        /// </summary>
        public ScriptErrorList Errors
        {
            get
            {
                return myErrors; 
            }
            set
            {
                myErrors = value; 
            }
        }

        private bool bolExecuting = false;
        /// <summary>
        /// the Engine is executing script method
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public bool Executing
        {
            get
            {
                return bolExecuting;
            }
        }

        /// <summary>
        /// Execute script method
        /// </summary>
        /// <param name="MethodName">method name</param>
        /// <param name="Parameters">parameters</param>
        /// <param name="ThrowException">whether throw exception when happend error</param>
        /// <returns>result of script method</returns>
        public object Execute(string MethodName, object[] Parameters, bool ThrowException)
        {
            if (CheckReady() == false)
            {
                return null;
            }
            if (ThrowException)
            {
                if (MethodName == null)
                {
                    throw new ArgumentNullException("MethodName");
                }
                MethodName = MethodName.Trim();
                if (MethodName.Length == 0)
                {
                    throw new ArgumentException("MethodName");
                }
                if (this.ScriptMethods.Count > 0)
                {
                    foreach (ScriptMethodInfo info in this.ScriptMethods)
                    {
                        if (string.Compare(info.MethodName, MethodName, true) == 0)
                        {
                            object result = null;
                            bolExecuting = true;
                            try
                            {
                                if (info.MethodDelegate != null)
                                {
                                    result = info.MethodDelegate.DynamicInvoke(Parameters);
                                }
                                else
                                {
                                    result = info.MethodObject.Invoke(null, Parameters);
                                }
                            }
                            finally
                            {
                                bolExecuting = false;
                            }
                            return result;
                        }//if
                    }//foreach
                }//if
            }
            else
            {
                if (MethodName == null)
                {
                    return null;
                }
                MethodName = MethodName.Trim();
                if (MethodName.Length == 0)
                {
                    return null;
                }
                if (this.ScriptMethods.Count > 0)
                {
                    foreach (ScriptMethodInfo info in this.ScriptMethods)
                    {
                        if (string.Compare(info.MethodName, MethodName, true) == 0)
                        {
                            ResolveEventHandler handler = new ResolveEventHandler(AppDomain_AssemblyResolve);
                            try
                            {
                                AppDomain.AssemblyResolve += handler;
                                bolExecuting = true;
                                object result = info.MethodObject.Invoke(null, Parameters);
                                bolExecuting = false;
                                return result;
                            }
                            catch (Exception ext)
                            {
                                //System.Diagnostics.Debugger.Break();
                                string msg = ext.Message ;
                                if (this.Errors != null)
                                {
                                    ScriptError err = new ScriptError(
                                        this,
                                        ScriptErrorStyle.Runtime,
                                        MethodName,
                                        null);

                                    if (ext is System.Reflection.TargetInvocationException
                                        && ext.InnerException != null)
                                    {
                                        err.Exception = ext.InnerException;
                                    }
                                    else
                                    {
                                        err.Exception = ext;
                                    }
                                    err.Message = err.Exception.Message;
                                    err.ScriptText = info.ScriptText;
                                    msg = err.Message;
                                    this.Errors.Add(err);
                                }
                                System.Diagnostics.Debug.WriteLine(
                                    string.Format(
                                        ScriptStrings.VBARuntimeError_Method_Message,
                                        MethodName,
                                        msg ));
                            }
                            finally
                            {
                                AppDomain.AssemblyResolve -= handler;
                                bolExecuting = false;
                            }
                            return null;
                        }//if
                    }//foreach
                }//if
            }//else
            return null;
        }

        private System.Reflection.Assembly AppDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            System.Reflection.AssemblyName name = new System.Reflection.AssemblyName(args.Name);
            foreach (System.Reflection.Assembly asm in AppDomain.GetAssemblies())
            {
                if ( string.Compare( asm.GetName().Name , name.Name , true ) == 0 )
                {
                    return asm;
                }
            }
            if ( string.Compare( name.Name , this.GetType().Assembly.GetName().Name , true ) ==0 )
            {
                return this.GetType().Assembly;
            }

            System.Diagnostics.Debug.WriteLine("Script AssemblyResolve Warring:" + args.Name);
            return null;
        }//public object Execute( string MethodName , object[] Parameters , bool ThrowException )

        /// <summary>
        /// 销毁对象
        /// </summary>
        public void Dispose()
        {
            this.Close();
        }
    }//public class XVBAScriptEngine

    
}
