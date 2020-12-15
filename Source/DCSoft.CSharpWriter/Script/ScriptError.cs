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

namespace DCSoft.Script
{
    /// <summary>
    /// script error
    /// </summary>
    public enum ScriptErrorStyle
    {
        /// <summary>
        /// compile error
        /// </summary>
        Compile ,
        /// <summary>
        /// runtime error
        /// </summary>
        Runtime 
    }
    /// <summary>
    /// Script error information
    /// </summary>
    [Serializable()]
    public class ScriptError
    {
        /// <summary>
        /// Initialize instance
        /// </summary>
        public ScriptError()
        {
        }

        /// <summary>
        /// Initialize instance
        /// </summary>
        /// <param name="engine">script engine</param>
        /// <param name="style">error type</param>
        /// <param name="methodName">Name of script method</param>
        /// <param name="ext">Exception instance</param>
        public ScriptError( XVBAEngine engine, ScriptErrorStyle style , string methodName, Exception ext)
        {
            myEngine = engine;
            intStyle = style;
            strMethodName = methodName;
            myException = ext;
            if (ext != null)
            {
                strMessage = ext.Message;
            }
        }

        private ScriptErrorStyle intStyle = ScriptErrorStyle.Compile;
        /// <summary>
        /// script error style
        /// </summary>
        public ScriptErrorStyle Style
        {
            get
            {
                return intStyle; 
            }
            set
            {
                intStyle = value; 
            }
        }

        private XVBAEngine myEngine = null;
        /// <summary>
        /// script engine
        /// </summary>
        [System.Xml.Serialization.XmlIgnore()]
        public XVBAEngine Engine
        {
            get
            {
                return myEngine; 
            }
            set
            {
                myEngine = value; 
            }
        }

        private DateTime dtmTime = DateTime.Now;
        /// <summary>
        /// time when happend error
        /// </summary>
        [System.Xml.Serialization.XmlIgnore()]
        public DateTime Time
        {
            get
            {
                return dtmTime; 
            }
            set
            {
                dtmTime = value; 
            }
        }

        private string strMethodName = null;
        /// <summary>
        /// Name of script method
        /// </summary>
        public string MethodName
        {
            get
            {
                return strMethodName; 
            }
            set
            {
                strMethodName = value; 
            }
        }

        private string strMessage = null;
        /// <summary>
        /// Message
        /// </summary>
        public string Message
        {
            get
            {
                return strMessage; 
            }
            set
            {
                strMessage = value; 
            }
        }

        [NonSerialized()]
        private Exception myException = null;
        /// <summary>
        /// Exception about error
        /// </summary>
        [System.Xml.Serialization.XmlIgnore()]
        public Exception Exception
        {
            get
            {
                return myException; 
            }
            set
            {
                myException = value; 
            }
        }

        private string strScriptText = null;
        /// <summary>
        /// Script source code about error
        /// </summary>
        public string ScriptText
        {
            get
            {
                return strScriptText; 
            }
            set
            {
                strScriptText = value; 
            }
        }
    }

    /// <summary>
    /// Script error list
    /// </summary>
    [Serializable()]
    public class ScriptErrorList : System.Collections.CollectionBase
    {
        /// <summary>
        /// get error information
        /// </summary>
        /// <param name="index">index</param>
        /// <returns>error information</returns>
        public ScriptError this[int index]
        {
            get
            {
                return (ScriptError)this.List[index];
            }
        }
        /// <summary>
        /// add new item
        /// </summary>
        /// <param name="err">item</param>
        /// <returns>index of item</returns>
        public int Add(ScriptError err)
        {
            return this.List.Add(err);
        }
    }
}
