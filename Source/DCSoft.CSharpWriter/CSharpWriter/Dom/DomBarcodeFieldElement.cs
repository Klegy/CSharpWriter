using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel ;
using System.Xml.Serialization ;
using DCSoft.Barcode;
using System.Drawing;

namespace DCSoft.CSharpWriter.Dom
{
    /// <summary>
    /// 条形码输入域对象
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [XmlType("XBarcodeField")]
    [Serializable]
    [System.Diagnostics.DebuggerDisplay("Barcode:{Name}")]
    [Editor(
        typeof( DCSoft.CSharpWriter.Commands.XTextBarcodeFieldElementEditor ) ,
        typeof( ElementEditor ))]
    //[XTextElementDescriptor(
    //    PropertiesType = typeof(DCSoft.CSharpWriter.Commands.XTextBarcodeFieldElementProperties))]
    public class DomBarcodeFieldElement : DomShapeInputFieldElement
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DomBarcodeFieldElement()
        {
            this.Height = 125f;
        }

        ///// <summary>
        ///// 对象宽度
        ///// </summary>
        //[System.ComponentModel.Browsable(true)]
        //[System.Xml.Serialization.XmlElement()]
        //public override float Width
        //{
        //    get
        //    {
        //        return base.Width;
        //    }
        //    set
        //    {
        //        base.Width = value;
        //    }
        //}

        private string _InitalizeText = null;
        /// <summary>
        /// 初始化使用的文本
        /// </summary>
        [DefaultValue( null )]
        [XmlIgnore ()]
        [Browsable( false )]
        [EditorBrowsable(  EditorBrowsableState.Advanced )]
        public string InitalizeText
        {
            get
            {
                return _InitalizeText; 
            }
            set
            {
                _InitalizeText = value; 
            }
        }


        /// <summary>
        /// 对象高度
        /// </summary>
        [System.ComponentModel.Browsable(true)]
        [System.Xml.Serialization.XmlElement()]
        [DefaultValue( 125f)]
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

        private BarcodeStyle _BarcodeStyle = BarcodeStyle.Code128C;
        /// <summary>
        /// 条码样式
        /// </summary>
        [DefaultValue( BarcodeStyle.Code128C )]
        public BarcodeStyle BarcodeStyle
        {
            get
            {
                return _BarcodeStyle; 
            }
            set
            {
                _BarcodeStyle = value; 
            }
        }

        private System.Drawing.StringAlignment intTextAlignment = StringAlignment.Center;
        /// <summary>
        /// 文本对齐方式
        /// </summary>
        [DefaultValue( StringAlignment.Center )]
        public System.Drawing.StringAlignment TextAlignment
        {
            get
            {
                return intTextAlignment;
            }
            set
            {
                intTextAlignment = value;
            }
        }

        private bool bolShowText = true;
        /// <summary>
        /// 是否绘制文本
        /// </summary>
        [DefaultValue( true )]
        public bool ShowText
        {
            get
            {
                return bolShowText;
            }
            set
            {
                bolShowText = value;
            }
        }

        private float fMinBarWidth = 7f;
        /// <summary>
        /// 条码细条宽度
        /// </summary>
        [DefaultValue(7f)]
        public float MinBarWidth
        {
            get
            {
                return fMinBarWidth;
            }
            set
            {
                fMinBarWidth = value;
            }
        }

        private BarcodeDrawer _Drawer = null;
        /// <summary>
        /// 绘制图形内容
        /// </summary>
        /// <param name="args">参数</param>
        public override void DrawContent(DocumentPaintEventArgs args)
        {
            CheckShapeState(true);
            RectangleF rect  = this.AbsBounds ;
            _Drawer.Draw(
                args.Graphics ,
                new Rectangle( 
                    (int)rect.Left ,
                    (int)rect.Top ,
                    (int)rect.Width ,
                    (int)rect.Height ));
        }

        /// <summary>
        /// 检查图形状态
        /// </summary>
        /// <param name="updateSize">是否更新元素大小</param>
        public override void CheckShapeState(bool updateSize)
        {
            if (_Drawer == null)
            {
                _Drawer = new BarcodeDrawer();
                _Drawer.SourceContentVersion = this.ContentVersion - 1;
                _Drawer.MinBarWidth = this.OwnerDocument.PixelToDocumentUnit(3);
            }
            if (_Drawer.SourceContentVersion != this.ContentVersion
                || _Drawer.Style != this.BarcodeStyle)
            {
                _Drawer.Text = this.Text;
                _Drawer.Style = this.BarcodeStyle;
                _Drawer.ShowText = this.ShowText;
               
                _Drawer.SourceContentVersion = this.ContentVersion;
                this.SizeInvalid = true ;
            }
            if (updateSize)
            {
                _Drawer.ShowText = this.ShowText;
                _Drawer.MinBarWidth = this.MinBarWidth;
                _Drawer.Font = this.Style.Font.Value;
            }
            if (updateSize && this.SizeInvalid )
            {
                //using (Graphics g = this.OwnerDocument.CreateGraphics())
                {
                    if (_Drawer.Encode())
                    {
                        this.Width = _Drawer.Width;
                        if (this.Height == 0)
                        {
                            this.Height = this.OwnerDocument.PixelToDocumentUnit(40);
                        }
                    }
                }
                this.SizeInvalid = false;
            }
            if (this.Width <= 0)
            {
                // 条码宽度为0，内容可能为空，需要设置宽度值。
                this.Width = 100;
            }
        }
    }
}