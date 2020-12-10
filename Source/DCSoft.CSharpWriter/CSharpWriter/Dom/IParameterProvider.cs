/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace DCSoft.CSharpWriter.Dom
//{
//    public interface IParameterProvider
//    {
//        /// <summary>
//        /// 获得变量值
//        /// </summary>
//        /// <param name="name">变量名</param>
//        /// <returns>变量值</returns>
//        string GetParameterValue(string name);

//        /// <summary>
//        /// 设置变量值
//        /// </summary>
//        /// <param name="name">变量名</param>
//        /// <param name="Value">新变量值</param>
//        /// <returns>操作是否成功</returns>
//        bool SetParameterValue( string name , string Value );

//        /// <summary>
//        /// 获得系统支持的参数名
//        /// </summary>
//        /// <returns>参数名</returns>
//        string[] GetParameterNames();

//    }

//    /// <summary>
//    /// 文档参数列表
//    /// </summary>
//    [Serializable]
//    public class DocumentParameterList : List<DocumentParameter>
//    {
//        /// <summary>
//        /// 初始化对象
//        /// </summary>
//        public DocumentParameterList()
//        {
//        }

//        /// <summary>
//        /// 获得指定名称的参数对象
//        /// </summary>
//        /// <param name="name">参数名</param>
//        /// <returns>获得的参数对象</returns>
//        public DocumentParameter this[string name]
//        {
//            get
//            {
//                foreach (DocumentParameter p in this)
//                {
//                    if (p.Name == name)
//                    {
//                        return p;
//                    }
//                }
//                return null;
//            }
//        }
//    }

//    /// <summary>
//    /// 文档参数对象
//    /// </summary>
//    [Serializable()]
//    public class DocumentParameter : System.ICloneable
//    {
//        /// <summary>
//        /// 初始化对象
//        /// </summary>
//        public DocumentParameter()
//        {
//        }

//        private string _Name = null;
//        /// <summary>
//        /// 参数名
//        /// </summary>
//        [System.ComponentModel.DefaultValue( null)]
//        public string Name
//        {
//            get { return _Name; }
//            set { _Name = value; }
//        }

//        private string _Value = null;
//        /// <summary>
//        /// 参数值
//        /// </summary>
//        [System.ComponentModel.DefaultValue( null)]
//        public string Value
//        {
//            get { return _Value; }
//            set { _Value = value; }
//        }

//        /// <summary>
//        /// 复制对象
//        /// </summary>
//        /// <returns>复制品</returns>
//        object ICloneable.Clone()
//        {
//            return (DocumentParameter)this.MemberwiseClone();
//        }

//        /// <summary>
//        /// 复制对象
//        /// </summary>
//        /// <returns>复制品</returns>
//        public DocumentParameter Clone()
//        {
//            return (DocumentParameter)this.MemberwiseClone();
//        }


//    }
//}
