using System;
using System.Collections.Generic;
using System.Text;

namespace DCSoft.CSharpWriter.Dom
{
    /// <summary>
    /// 文本块
    /// </summary>
    [Serializable()]
    public class DomBlockElement : DomElement
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DomBlockElement()
        {
        }

        private string _Text = null;
        /// <summary>
        /// 文本值
        /// </summary>
        [System.ComponentModel.DefaultValue( null )]
        public override string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }
    }
}
