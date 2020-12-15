using System;
using System.Collections.Generic;
using System.Text;

namespace DCSoft.CSharpWriter.Dom
{
    /// <summary>
    /// 变量域对象
    /// </summary>
    [Serializable()]
    [System.Xml.Serialization.XmlType("XTextParameter")]
    public class DomParameterElement :DomFieldElementBase 
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DomParameterElement()
        {
        }

        private string _Name = null;
        /// <summary>
        /// 对象名称
        /// </summary>
        [System.ComponentModel.DefaultValue(null)]
        public string Name
        {
            get
            {
                return _Name; 
            }
            set
            {
                _Name = value; 
            }
        }

        private string _ParameterName = null;
        /// <summary>
        /// 参数名
        /// </summary>
        [System.ComponentModel.DefaultValue( null)]
        public string ParameterName
        {
            get
            {
                return _ParameterName; 
            }
            set
            {
                _ParameterName = value; 
            }
        }

        private ParameterValueUpdateMode _ValueUpdateMode = ParameterValueUpdateMode.Fixed;
        /// <summary>
        /// 参数值更新方式
        /// </summary>
        [System.ComponentModel.DefaultValue( ParameterValueUpdateMode.Fixed )]
        public ParameterValueUpdateMode ValueUpdateMode
        {
            get
            {
                return _ValueUpdateMode; 
            }
            set
            {
                _ValueUpdateMode = value; 
            }
        }

        ///// <summary>
        ///// 文档加载后的处理
        ///// </summary>
        ///// <param name="format">文档参数</param>
        //public override void AfterLoad(FileFormat format)
        //{
        //    if ( this.ValueUpdateMode == ParameterValueUpdateMode.UpdateWhenAfterLoad )
        //    {
        //        // 自动更新参数内容
        //        if (this.OwnerDocument != null)
        //        {
        //            string txt = ((IParameterProvider)this.OwnerDocument).GetParameterValue(this.ParameterName );
        //            this.SetInnerTextFast(txt);
        //        }
        //    }
        //    base.AfterLoad(format);
        //}
    }

    /// <summary>
    /// 参数值更新方式
    /// </summary>
    public enum ParameterValueUpdateMode
    {
        /// <summary>
        /// 固定值
        /// </summary>
        Fixed ,
        /// <summary>
        /// 加载文档后更新内容
        /// </summary>
        UpdateWhenAfterLoad,
        /// <summary>
        /// 动态更新
        /// </summary>
        DynamicUpdate
    }
}