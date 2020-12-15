using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Drawing ;
using DCSoft.Drawing ;

namespace DCSoft.CSharpWriter.Dom
{
    /// <summary>
    /// 输入域边界元素
    /// </summary>
    [Serializable]
    public class DomFieldBorderElement : DomElement 
    {
        /// <summary>
        /// 边框元素的标准像素宽度
        /// </summary>
        public static float StandardPixelWidth = 4.0f;

        /// <summary>
        /// 初始化对象
        /// </summary>
        public DomFieldBorderElement()
        {
        }

        private BorderElementPosition _Position = BorderElementPosition.Start;
        /// <summary>
        /// 位置
        /// </summary>
        public BorderElementPosition Position
        {
            get
            {
                return _Position; 
            }
            set
            {
                _Position = value; 
            }
        }

        [XmlIgnore]
        [Browsable( false )]
        public DomFieldElementBase OwnerField
        {
            get
            {
                return (DomFieldElementBase)this.Parent;
            }
        }

        /// <summary>
        /// 本元素的影子元素就是其所处的输入域容器对象
        /// </summary>
        [Browsable( false )]
        public override DomElement ShadowElement
        {
            get
            {
                return this.Parent;
            }
        }

        /// <summary>
        /// 对象无文本
        /// </summary>
        [XmlIgnore]
        [Browsable( false )]
        public override string Text
        {
            get
            {
                return "";
            }
            set
            {
            }
        }

        /// <summary>
        /// 标准高度
        /// </summary>
        internal float StandardHeight
        {
            get
            {
                if (this.OwnerDocument == null)
                {
                    return GraphicsUnitConvert.Convert(16, GraphicsUnit.Pixel, GraphicsUnit.Document);
                }
                else
                {
                    return GraphicsUnitConvert.Convert(16, GraphicsUnit.Pixel, this.OwnerDocument.DocumentGraphicsUnit);
                }
            }
        }

        public override string ToPlaintString()
        {
            return "";
        }

        /// <summary>
        /// 返回表示对象的文本
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "FieldBorder";
        }
    }

    /// <summary>
    /// 边框元素位置
    /// </summary>
    public enum BorderElementPosition
    {
        /// <summary>
        /// 开始位置
        /// </summary>
        Start ,
        /// <summary>
        /// 结束位置
        /// </summary>
        End
    }
}
