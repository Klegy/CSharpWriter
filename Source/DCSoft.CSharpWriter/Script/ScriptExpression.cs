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
using System.Reflection;
using System.ComponentModel;
namespace DCSoft.Script
{
    /// <summary>
    /// script expression container
    /// </summary>
    [Serializable()]
    public class ScriptMemberExpressionContainer : System.Collections.CollectionBase , ICloneable
    {
        private static System.Collections.Hashtable myPropertyTable = new System.Collections.Hashtable();
        public static ScriptMemberExpressionContainer GetContainer(object component)
        {
            Type t = component.GetType();
            if (myPropertyTable.ContainsKey(t))
            {
                return (ScriptMemberExpressionContainer)((PropertyInfo)myPropertyTable[t]).GetValue(component, null);
            }
            else
            {
                foreach (PropertyInfo p2 in t.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (p2.PropertyType.Equals(typeof(ScriptMemberExpressionContainer)))
                    {
                        myPropertyTable[t] = p2;
                        return (ScriptMemberExpressionContainer)p2.GetValue(component, null);
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Initialize instance
        /// </summary>
        public ScriptMemberExpressionContainer()
        { 
        }

        public bool IsEmpty
        {
            get
            {
                foreach (ScriptMemberExpression item in this)
                {
                    if (item.HasContent)
                        return false;
                }
                return true;
            }
        }
        public ScriptMemberExpression this[int index]
        {
            get
            {
                return (ScriptMemberExpression)this.List[index];
            }
        }
        public ScriptMemberExpression this[string name]
        {
            get
            {
                foreach (ScriptMemberExpression item in this)
                {
                    if (string.Compare(item.Name, name, true) == 0)
                        return item;
                }
                return null;
            }
        }

        public int Add(ScriptMemberExpression item)
        {
            return this.List.Add(item);
        }

        public void Remove(ScriptMemberExpression item)
        {
            this.List.Remove(item);
        }
        public string GetScript(string name)
        {
            ScriptMemberExpression item = this[name];
            if (item == null)
                return null;
            else
                return item.Script;
        }
        public void SetScript(string name, string script)
        {
            if (script == null || script.Trim().Length == 0)
            {
                ScriptMemberExpression item = this[name];
                if (item != null)
                    this.List.Remove(item);
            }
            else
            {
                ScriptMemberExpression item = this[name];
                if (item == null)
                {
                    item = new ScriptMemberExpression();
                    item.Name = name;
                    this.List.Add(item);
                }
                item.Script = script;
            }
        }
        /// <summary>
        /// Clone instance
        /// </summary>
        /// <returns>instance</returns>
        object ICloneable.Clone()
        {
            ScriptMemberExpressionContainer list = new ScriptMemberExpressionContainer();
            foreach (ScriptMemberExpression item in this)
            {
                list.List.Add(item.Clone());
            }
            return list;
        }

        /// <summary>
        /// Clone instance
        /// </summary>
        /// <returns>instance</returns>
        public ScriptMemberExpressionContainer Clone()
        {
            return (ScriptMemberExpressionContainer)((ICloneable)this).Clone();
        }
    }

    /// <summary>
    /// script expression
    /// </summary>
    [Serializable()]
    [System.Xml.Serialization.XmlType()]
    public class ScriptMemberExpression : ICloneable
    {
        /// <summary>
        /// Initialize instance
        /// </summary>
        public ScriptMemberExpression()
        {
        }

        private System.Reflection.MemberInfo myBindMember = null;
        /// <summary>
        /// member which expression bind
        /// </summary>
        internal System.Reflection.MemberInfo BindMember
        {
            get
            {
                return myBindMember; 
            }
            set
            {
                myBindMember = value; 
            }
        }

        private string strName = null;
        /// <summary>
        /// Name
        /// </summary>
        [System.ComponentModel.DefaultValue( null )]
        public string Name
        {
            get
            {
                return strName; 
            }
            set
            {
                strName = value; 
            }
        }

        private string strScriptMethodName = null;
        /// <summary>
        /// Name of script method
        /// </summary>
        internal string ScriptMethodName
        {
            get
            {
                return strScriptMethodName; 
            }
            set
            {
                strScriptMethodName = value; 
            }
        }

        private string strScript = null;
        /// <summary>
        /// script source code
        /// </summary>
        [System.ComponentModel.DefaultValue( null )]
        public string Script
        {
            get
            {
                return strScript; 
            }
            set
            {
                strScript = value; 
            }
        }

        public bool HasContent
        {
            get
            {
                return strScript != null && strScript.Trim().Length > 0;
            }
        }

        /// <summary>
        /// Clone instance
        /// </summary>
        /// <returns>instance</returns>
        object ICloneable.Clone()
        {
            ScriptMemberExpression item = new ScriptMemberExpression();
            item.strName = this.strName;
            item.strScript = this.strScript;
            return item;
        }

        /// <summary>
        /// Clone instance
        /// </summary>
        /// <returns>instance</returns>
        public ScriptMemberExpression Clone()
        {
            return (ScriptMemberExpression)((ICloneable)this).Clone();
        }
    }
}
