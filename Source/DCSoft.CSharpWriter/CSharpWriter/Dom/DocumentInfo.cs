/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;

namespace DCSoft.CSharpWriter.Dom
{
	/// <summary>
	/// 文档设置信息对象
	/// </summary>
    [Serializable()]
	public class DocumentInfo : System.ICloneable
	{
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DocumentInfo()
        {
        }
        ///// <summary>
        ///// 初始化对象
        ///// </summary>
        ///// <param name="doc">文档对象</param>
        //internal DocumentSettings( TextDocument doc )
        //{
        //    myDocument = doc ;
        //}

        //private XTextDocument myDocument = null;
        ///// <summary>
        ///// 对象所属文档对象
        ///// </summary>
        //[System.ComponentModel.Browsable( false )]
        //[System.Xml.Serialization.XmlIgnore()]
        //public XTextDocument Document
        //{
        //    get
        //    { 
        //        return myDocument ;
        //    }
        //}

        [NonSerialized()]
        private string _RuntimeTitle = null;
        /// <summary>
        /// 实际使用的文档标题
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        [System.Xml.Serialization.XmlIgnore()]
        public string RuntimeTitle
        {
            get { return _RuntimeTitle; }
            set { _RuntimeTitle = value; }
        }

        private string _Version = null;
        /// <summary>
        /// 内容版本号
        /// </summary>
         [System.ComponentModel.DefaultValue(null)]
        public string Version
        {
            get 
            {
                return _Version; 
            }
            set
            {
                _Version = value; 
            }
        }

        private string _Title = null;
        /// <summary>
        /// 文档标题
        /// </summary>
        [System.ComponentModel.DefaultValue( null)]
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        private string _Description = null;
        /// <summary>
        /// 文档说明
        /// </summary>
        [System.ComponentModel.DefaultValue(null)]
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        private DateTime _CreationTime = DateTime.Now;
        /// <summary>
        /// 文档创建日期
        /// </summary>
        public DateTime CreationTime
        {
            get { return _CreationTime; }
            set { _CreationTime = value; }
        }

        private DateTime _LastModifiedTime = DateTime.Now;
        /// <summary>
        /// 最后修改日期
        /// </summary>
        public DateTime LastModifiedTime
        {
            get { return _LastModifiedTime; }
            set { _LastModifiedTime = value; }
        }

        private int _EditMinute = 0;
        /// <summary>
        /// 文档编辑的时间长度，单位分钟。
        /// </summary>
        [System.ComponentModel.DefaultValue(0)]
        public int EditMinute
        {
            get { return _EditMinute; }
            set { _EditMinute = value; }
        }

        private DateTime _LastPrintTime = new DateTime(1980, 1, 1);
        /// <summary>
        /// 最后一次打印的时间
        /// </summary>
        public DateTime LastPrintTime
        {
            get { return _LastPrintTime ; }
            set { _LastPrintTime = value; }
        }

        private string _Author = null;
        /// <summary>
        /// 作者
        /// </summary>
        [System.ComponentModel.DefaultValue(null)]
        public string Author
        {
            get { return _Author; }
            set { _Author = value; }
        }

        private string _Comment = null;
        /// <summary>
        /// 内容说明
        /// </summary>
        [System.ComponentModel.DefaultValue(null)]
        public string Comment
        {
            get { return _Comment; }
            set { _Comment = value; }
        }

        private string _Operator = "DCSoft.CSharpWriter Version:" + typeof( DocumentInfo ) .Assembly.GetName().Version ;
        /// <summary>
        /// 操作者
        /// </summary>
        [System.ComponentModel.DefaultValue(null)]
        public string Operator
        {
            get { return _Operator; }
            set { _Operator = value; }
        }

        private int _NumOfPage = 0;
        /// <summary>
        /// 文档总页数
        /// </summary>
        [System.ComponentModel.DefaultValue( 0 )]
        public int NumOfPage
        {
            get
            {
                return _NumOfPage; 
            }
            set
            {
                _NumOfPage = value; 
            }
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public DocumentInfo Clone()
        {
            return (DocumentInfo)this.MemberwiseClone();
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        object ICloneable.Clone()
        {
            return (DocumentInfo)this.MemberwiseClone();
        }

    }
}