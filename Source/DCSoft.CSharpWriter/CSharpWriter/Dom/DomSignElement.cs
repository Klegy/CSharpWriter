using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing ;

namespace DCSoft.CSharpWriter.Dom
{
    /// <summary>
    /// 锁定内容的元素
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [Serializable]
    [System.Xml.Serialization.XmlType("XTextLock")]
    public class DomSignElement : DomElement
    {
        private static Bitmap _StandardIcon = null;
        /// <summary>
        /// 元素使用的标准图标
        /// </summary>
        public static Bitmap StandardIcon
        {
            get
            {
                if (_StandardIcon == null)
                {
                    _StandardIcon = WriterResources.lockflag;
                    _StandardIcon.MakeTransparent();
                }
                return _StandardIcon; 
            }
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        public DomSignElement()
        {
        }
    }
}