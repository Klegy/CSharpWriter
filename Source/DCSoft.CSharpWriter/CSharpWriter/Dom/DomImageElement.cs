/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using System.Drawing;
using DCSoft.Drawing;

using System.ComponentModel ;
using System.Xml.Serialization ;
using DCSoft.CSharpWriter.Controls;
using DCSoft.CSharpWriter.Data;

namespace DCSoft.CSharpWriter.Dom
{
	/// <summary>
	/// 图片对象
	/// </summary>
    /// <remarks>编制袁永福</remarks>
    [Serializable()]
    [System.Xml.Serialization.XmlType("XImage")]
    //[XTextElementDescriptor(
    //    PropertiesType = typeof(DCSoft.CSharpWriter.Commands.XTextImageElementProperties))]
    [Editor(
        typeof( DCSoft.CSharpWriter.Commands.XTextImageElementEditor ) ,
        typeof( ElementEditor ))]
    public class DomImageElement : DomObjectElement, System.IDisposable
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DomImageElement()
        {
            base.WidthHeightRate = 0;
        }

        private string _Title = null;
        /// <summary>
        /// 标题
        /// </summary>
        [System.ComponentModel.DefaultValue( null )]
        public string Title
        {
            get
            {
                return _Title; 
            }
            set
            {
                _Title = value; 
            }
        }

        private string _Alt = null;
        /// <summary>
        /// 缺乏图片时显示的文本
        /// </summary>
        [System.ComponentModel.DefaultValue(null)]
        public string Alt
        {
            get
            {
                return _Alt; 
            }
            set
            {
                _Alt = value; 
            }
        }

        private bool _KeepWidthHeightRate = true;
        /// <summary>
        /// 保持宽度、高度比例。若本属性值为true，
        /// 则用户鼠标拖拽改变图片大小时会保持图片的宽度高度比例，
        /// 否则用户可以随意改变图片的宽度和高度。
        /// </summary>
        [System.ComponentModel.DefaultValue( true )]
        public bool KeepWidthHeightRate
        {
            get 
            {
                return _KeepWidthHeightRate; 
            }
            set
            {
                _KeepWidthHeightRate = value;
                UpdateWidthHeightRate();
            }
        }

        private string _Source = null;
        /// <summary>
        /// 图片来源
        /// </summary>
        [DefaultValue( null )]
        public string Source
        {
            get
            {
                return _Source; 
            }
            set
            {
                _Source = value; 
            }
        }

        private bool _SaveContentInFile = true;
        /// <summary>
        /// 保存图片数据到文件中
        /// </summary>
        [DefaultValue( true )]
        public bool SaveContentInFile
        {
            get 
            {
                return _SaveContentInFile; 
            }
            set
            {
                _SaveContentInFile = value; 
            }
        }

        private void UpdateWidthHeightRate()
        {
            if (_KeepWidthHeightRate
                && myImage != null
                && myImage.Value != null
                && myImage.Height > 0)
            {
                this.WidthHeightRate = (double)myImage.Width / (double)myImage.Height;
            }
            else
            {
                this.WidthHeightRate = 0;
            }
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


        /// <summary>
        /// 设置/获得对象在设计器中的大小
        /// </summary>
        [Browsable( false )]
        [XmlIgnore ]
        public override SizeF EditorSize
        {
            get
            {
                return base.EditorSize;
            }
            set
            {
                base.EditorSize = value;
                this.DisposePreviewImage();
            }
        }

        /// <summary>
        /// 内部的图像数据对象
        /// </summary>
        private XImageValue myImage = new XImageValue();
        /// <summary>
        /// 内部的图像数据对象
        /// </summary>
        public XImageValue Image
        {
            get
            {
                if (myImage == null)
                {
                    myImage = new XImageValue();
                }
                return myImage;
            }
            set
            {
                myImage = value;
                UpdateWidthHeightRate();
            }
        }

        

        /// <summary>
        /// 图像对象
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        [System.ComponentModel.DesignerSerializationVisibility(
            System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public System.Drawing.Image ImageValue
        {
            get
            {
                return myImage.Value;
            }
            set
            {
                myImage.Value = value;
            }
        }

        /// <summary>
        /// 图像数据
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        [System.ComponentModel.DesignerSerializationVisibility(
            System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public byte[] ImageData
        {
            get
            {
                return myImage.ImageData;
            }
            set
            {
                myImage.ImageData = value;
            }
        }

        /// <summary>
        /// 加载图片数据
        /// </summary>
        /// <param name="img">图片对象</param>
        /// <param name="setSize">是否设置大小</param>
        /// <returns>操作是否成功</returns>
        public bool LoadImage(System.Drawing.Image img, bool setSize)
        {
            if (img != null)
            {
                myImage.Value = img;
                if (setSize)
                {
                    UpdateSize();
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 从指定的资源地址加载图片数据
        /// </summary>
        /// <param name="strUrl">图片资源地址</param>
        /// <param name="setSize">是否设置对象大小</param>
        /// <returns>操作是否成功</returns>
        public bool LoadImage(string strUrl, bool setSize)
        {
            if (myImage.Load(strUrl) > 0 )
            {
                if (setSize)
                {
                    UpdateSize();
                }
                return true;
            }
            return false;
        }

        public override void OnMouseEnter(EventArgs args)
        {
            base.OnMouseEnter(args);
            if (this.OwnerDocument != null && this.OwnerDocument.EditorControl != null)
            {
                if (string.IsNullOrEmpty(this.Title))
                {
                    this.OwnerDocument.EditorControl.ToolTips.Remove(this);
                }
                else
                {
                    this.OwnerDocument.EditorControl.ToolTips.Add(
                        this,
                        this.Title,
                        ToolTipStyle.ToolTip,
                        ToolTipLevel.Normal);
                }
            }
        }

        public override void AfterLoad(FileFormat format)
        {
            base.AfterLoad(format);
            if ( this.Image == null || this.Image.HasContent == false)
            {
                //if (this.OwnerDocument.Options.BehaviorOptions.DesignMode == false)
                {
                    UpdateImageContent();
                }
            }
        }

        /// <summary>
        /// 更新内容
        /// </summary>
        public void UpdateImageContent()
        {
            if (string.IsNullOrEmpty(this.Source) == false)
            {
                DCSoft.CSharpWriter.Data.IFileSystem fs = this.OwnerDocument.AppHost.FileSystems.Default;
                VFileInfo info = fs.GetFileInfo( this.OwnerDocument.AppHost.Services , this.Source);
                if ( info.Exists )
                {
                    System.IO.Stream stream = fs.Open( this.OwnerDocument.AppHost.Services , this.Source);
                    if (stream != null)
                    {
                        using (stream)
                        {
                            XImageValue img = new XImageValue( stream );
                            this.Image = img;
                        }
                    }
                }
            }   
        }

        /// <summary>
        /// 根据图片内容更新元素的大小
        /// </summary>
        public void UpdateSize()
        {
            if (this.Image.HasContent)
            {
                System.Drawing.Size size = myImage.Size;
                //base.WidthHeightRate = ( double )size.Width / ( double ) size.Height ;
                size = GraphicsUnitConvert.Convert(
                    myImage.Value.Size,
                    System.Drawing.GraphicsUnit.Pixel,
                    this.OwnerDocument.DocumentGraphicsUnit);
                UpdateWidthHeightRate();
                this.EditorSize = new SizeF(size.Width, size.Height);
            }
            else
            {
                this.EditorSize = new SizeF(100, 100);
            }
        }
         
        /// <summary>
        /// 销毁对象
        /// </summary>
        public override void Dispose()
        {
            if (myImage != null)
            {
                myImage.Dispose();
            }
            DisposePreviewImage();
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <param name="Deeply">是否深度复制</param>
        /// <returns>复制品</returns>
        public override DomElement Clone(bool Deeply)
        {
            DomImageElement img = ( DomImageElement ) base.Clone(Deeply);
            if (this._PreviewImage != null)
            {
                img._PreviewImage = ( Image ) this._PreviewImage.Clone();
            }
            if (this.myImage != null)
            {
                img.myImage = this.myImage.Clone();
            }
            return img;
        }

        [NonSerialized]
        private System.Drawing.Image _PreviewImage = null;
        private int _PreviewImageContentVerion = 0;
        /// <summary>
        /// 预览使用的图片对象
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public override System.Drawing.Image PreviewImage
        {
            get
            {
                if (_PreviewImageContentVerion != this.Image.ContentVersion)
                {
                    this.DisposePreviewImage();
                    _PreviewImageContentVerion = this.Image.ContentVersion;
                }

                if (_PreviewImage == null)
                {
                    // 判断是否需要创建预览图片
                    bool needPreviewImage = false;
                    
                    {
                        if (this.Image.HasContent)
                        {
                            SizeF size = GraphicsUnitConvert.Convert(
                                new SizeF(this.Image.Width, this.Image.Height),
                                GraphicsUnit.Pixel,
                                this.OwnerDocument.DocumentGraphicsUnit);
                            if (size.Width > this.Width * 3 || size.Height > this.Height * 3)
                            {
                                // 显示比较大的图片，则创建预览图片
                                needPreviewImage = true;
                            }
                        }
                    }
                    if (needPreviewImage)
                    {
                        // 创建预览图片
                        _PreviewImage = CreateContentImage();
                    }
                }
                return _PreviewImage;
            }
        }

        /// <summary>
        /// 销毁预览图片
        /// </summary>
        private void DisposePreviewImage()
        {
            if (_PreviewImage != null)
            {
                _PreviewImage.Dispose();
                _PreviewImage = null;
            }
        }

        /// <summary>
        /// 输出对象数据到HTML文档
        /// </summary>
        /// <param name="writer">HTML文档书写器</param>
        public override void WriteHTML(DCSoft.CSharpWriter.Html.WriterHtmlDocumentWriter writer)
        {
            if (this.Image.HasContent)
            {
                writer.WriteImageElement(this);
            }
        }

        /// <summary>
        /// 绘制图片文档内容
        /// </summary>
        /// <param name="args"></param>
        public override void DrawContent(DocumentPaintEventArgs args)
        {
            //try
            //{
            System.Drawing.RectangleF bounds = this.AbsBounds;
            float rate = 1;
            if (this.OwnerLine.AdditionHeight < 0)
            {
                float newHeight = bounds.Height + this.OwnerLine.AdditionHeight;
                rate = newHeight / bounds.Height ;
                bounds.Height = newHeight ;
            }
            if (args.RenderStyle == DocumentRenderStyle.Paint
                && this.PreviewImage != null)
            {
                Image img = this.PreviewImage ;
                args.Graphics.DrawImage(
                    img ,
                    bounds ,
                    new RectangleF(
                        0 , 
                        0 , 
                        img.Width ,
                        img.Height * rate ),
                        GraphicsUnit.Pixel );
                    //bounds.X,
                    //bounds.Y,
                    //bounds.Width ,
                    //bounds.Height );
            }
            else
            {
                if (this.Image.HasContent)
                {
                    Image img = this.Image.Value;
                    args.Graphics.DrawImage(
                        img,
                        bounds,
                        new RectangleF(
                            0,
                            0,
                            img.Width,
                            img.Height * rate),
                            GraphicsUnit.Pixel);
                }
                else
                {
                    bool draw = true;
                    if (args.RenderStyle == DocumentRenderStyle.Print
                        && this.OwnerDocument.Options.ViewOptions.PrintImageAltText == false )
                    {
                        draw = false;
                    }
                    if (draw)
                    {
                        using (System.Drawing.StringFormat f
                            = new System.Drawing.StringFormat())
                        {
                            f.Alignment = System.Drawing.StringAlignment.Center;
                            f.LineAlignment = System.Drawing.StringAlignment.Center;
                            string text = WriterStrings.NoImage;
                            if (string.IsNullOrEmpty(this.Alt) == false)
                            {
                                text = this.Alt;
                            }
                            args.Graphics.DrawString(
                                text ,
                                System.Windows.Forms.Control.DefaultFont,
                                System.Drawing.Brushes.Red,
                                bounds,
                                f);
                        }//using
                    }//if
                }
            }
            //}
            //catch (Exception ext) 
            //{
            //    System.Console.WriteLine(ext.ToString());
            //}

        }
         
        /// <summary>
        /// 创建预览用的图片
        /// </summary>
        /// <returns>创建的图片对象</returns>
        public override Image CreateContentImage()
        {
            if (this.Image.HasContent)
            {
                System.Drawing.SizeF size = new System.Drawing.SizeF(this.Width, this.Height);
                size = GraphicsUnitConvert.Convert(
                    size,
                    this.OwnerDocument.DocumentGraphicsUnit,
                    GraphicsUnit.Pixel);
                return this.Image.GetThumbnailImage((int)size.Width, (int)size.Height).Value ;
            }
            else
            {
                return base.CreateContentImage();
            }
        }


        /// <summary>
        /// 输出对象到RTF文档中
        /// </summary>
        /// <param name="writer">RTF文档书写器</param>
        public override void WriteRTF(DCSoft.CSharpWriter.RTF.RTFContentWriter writer)
        {
            System.Drawing.SizeF size = new System.Drawing.SizeF(this.Width, this.Height);
            size = GraphicsUnitConvert.Convert(
                size,
                this.OwnerDocument.DocumentGraphicsUnit,
                GraphicsUnit.Pixel);
            
                writer.WriteImage(
                    this.Image.Value,
                    (int)size.Width,
                    (int)size.Height,
                    this.Image.ImageData,
                    this.RuntimeStyle);
           
        }

        /// <summary>
        /// 返回表示对象的字符串
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString()
        {
            if (this.Image == null)
            {
                return "[IMG Null]";
            }
            else
            {
                return "[IMG" + this.Image.ToString() + "]";
            }
        }

        /// <summary>
        /// 返回表示对象内容的字符串
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToPlaintString()
        {
            return "";
        }

        /// <summary>
        /// 表示对象内容的纯文本数据
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        [System.ComponentModel.DesignerSerializationVisibility(
            System.ComponentModel.DesignerSerializationVisibility.Hidden)]
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
    }
}