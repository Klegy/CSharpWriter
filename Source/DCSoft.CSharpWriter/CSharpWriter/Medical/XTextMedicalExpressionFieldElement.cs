using System;
using System.Collections.Generic;
using System.Text;
using DCSoft.CSharpWriter.Dom ;
using System.Xml.Serialization ;
using System.ComponentModel ;
using System.Drawing;
using DCSoft.Drawing;

namespace DCSoft.CSharpWriter.Medical
{
    /// <summary>
    /// 医学表达式输入域对象
    /// </summary>
    /// <remarks>编写 袁永福</remarks>
    [Serializable]
    [XmlType("XMedicalExpressionField")]
    [System.Diagnostics.DebuggerDisplay("Medical expression:{Name}")]
    //[XTextElementDescriptor( PropertiesType= typeof( XTextMedicalExpressionFieldElementProperties ))]
    [Editor(
        typeof( XTextMedicalExpressionFieldElementEditor ) ,
        typeof( ElementEditor ))]
    public class XTextMedicalExpressionFieldElement : DomShapeInputFieldElement 
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public XTextMedicalExpressionFieldElement()
        {
        }

        /// <summary>
        /// 对象宽度
        /// </summary>
        [System.ComponentModel.Browsable(true)]
        [System.Xml.Serialization.XmlElement()]
        public override float Width
        {
            get
            {
                return base.Width;
            }
            set
            {
                base.Width = value;
            }
        }

        /// <summary>
        /// 对象高度
        /// </summary>
        [System.ComponentModel.Browsable(true)]
        [System.Xml.Serialization.XmlElement()]
        public override float Height
        {
            get
            {
                return base.Height;
            }
            set
            {
                base.Height = value;
            }
        }

        private MedicalExpressionStyle _ExpressionStyle = MedicalExpressionStyle.FourValues;
        /// <summary>
        /// 医学表达式类型
        /// </summary>
        [DefaultValue( MedicalExpressionStyle.FourValues )]
        public MedicalExpressionStyle ExpressionStyle
        {
            get
            { 
                return _ExpressionStyle; 
            }
            set
            {
                _ExpressionStyle = value;
            }
        }
           

        private MedicalExpressionRender _ExpressionRender = null;

        /// <summary>
        /// 内置的表达式呈现器
        /// </summary>
        internal MedicalExpressionRender ExpressionRender
        {
            get
            {
                CheckShapeState(false);
                return _ExpressionRender; 
            }
        }

        /// <summary>
        /// 检查医学表达式元素状态
        /// </summary>
        /// <param name="updateSize">是否根据需要计算元素大小</param>
        public override void CheckShapeState(bool updateSize)
        {
            if (_ExpressionRender == null)
            {
                _ExpressionRender = new MedicalExpressionRender();
                _ExpressionRender.SourceVersion = this.ContentVersion - 1;
            }
            if ( _ExpressionRender.SourceVersion  != this.ContentVersion
                || _ExpressionRender.Style != this.ExpressionStyle)
            {
                _ExpressionRender.SourceVersion = this.ContentVersion;
                _ExpressionRender.Style = this.ExpressionStyle;
                string text = this.Text;
                if (text != null && text.Length > 0)
                {
                    string[] items = text.Split(',', ';', '，');
                    _ExpressionRender.Value1 = null;
                    _ExpressionRender.Value2 = null;
                    _ExpressionRender.Value3 = null;
                    _ExpressionRender.Value4 = null;
                    if (items.Length > 0)
                    {
                        _ExpressionRender.Value1 = items[0];
                    }
                    if (items.Length > 1)
                    {
                        _ExpressionRender.Value2 = items[1];
                    }
                    if (items.Length > 2)
                    {
                        _ExpressionRender.Value3 = items[2];
                    }
                    if (items.Length > 3)
                    {
                        _ExpressionRender.Value4 = items[3];
                    }
                }
                this.SizeInvalid = true;
            }
            if (updateSize)
            {
                DocumentContentStyle rs = this.RuntimeStyle ;
                _ExpressionRender.Font = rs.Font;
                _ExpressionRender.ForeColor = rs.Color;
            }
            if ( updateSize && this.SizeInvalid )
            {
                using (System.Drawing.Graphics g = this.OwnerDocument.CreateGraphics())
                {
                    InnerRefreshRenderSize(g);
                }
            }
        }

        private void InnerRefreshRenderSize(Graphics g)
        {
            DocumentContentStyle rs = this.RuntimeStyle;
            _ExpressionRender.Font = rs.Font;
            _ExpressionRender.ForeColor = rs.Color;
            SizeF size = _ExpressionRender.GetPreferredSize(g);
            base.SizeInvalid = false;
            this.Width = Math.Max( size.Width , this.Width );
            this.Height = Math.Max( size.Height , this.Height );
        }
          
        public override void DrawContent(DocumentPaintEventArgs args)
        {
            CheckShapeState(true);
            _ExpressionRender.Render(args.Graphics, this.AbsBounds);
        }         
    }
}
