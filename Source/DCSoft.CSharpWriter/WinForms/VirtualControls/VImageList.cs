/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing ;
using DCSoft.Drawing ;
using System.ComponentModel;
using System.Xml.Serialization ;
using System.Windows.Forms ;

namespace DCSoft.WinForms.VirtualControls
{
    /// <summary>
    /// 虚拟图片列表
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [Serializable]
    public class VImageList
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public VImageList()
        {
        }

        private VImageListItemCollection _Images = new VImageListItemCollection();
        /// <summary>
        /// 图片列表
        /// </summary>
        [XmlArrayItem( "Image" , typeof( VImageListItem ))]
        public VImageListItemCollection Images
        {
            get { return _Images; }
            set { _Images = value; }
        }

        private Size _ImageSize = Size.Empty;
        /// <summary>
        /// 图片大小
        /// </summary>
        public Size ImageSize
        {
            get { return _ImageSize; }
            set { _ImageSize = value; }
        }

        private Color _TransparentColor = Color.Empty;
        /// <summary>
        /// 透明色
        /// </summary>
        [XmlIgnore]
        public Color TransparentColor
        {
            get { return _TransparentColor; }
            set { _TransparentColor = value; }
        }

        [Browsable(false)]
        [XmlElement]
        public string TransparentColorValue
        {
            get
            {
                return ColorTranslator.ToHtml(this.TransparentColor);
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _TransparentColor = Color.Empty;
                }
                else
                {
                    this.TransparentColor = ColorTranslator.FromHtml(value);
                }
            }
        }

        public ImageList CreateImageListControl()
        {
            ImageList list = new ImageList();
            list.TransparentColor = this.TransparentColor;
            if (this.Images != null)
            {
                foreach (VImageListItem item in this.Images)
                {
                    XImageValue img = new XImageValue();
                    if (item.Content != null && item.Content.Length > 0)
                    {
                        img.ImageData = item.Content;
                    }
                    else if (string.IsNullOrEmpty(item.Source) == false)
                    {
                        img.Load(item.Source);
                    }
                    if (img.Value == null)
                    {
                        img.Value = VResources.Blank16_16;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(item.Key))
                        {
                            list.Images.Add(img.Value);
                        }
                        else
                        {
                            list.Images.Add(item.Key, img.Value);
                        }
                    }
                }//foreach
            }//if
            return list;
        }


        public void ReadFrom(ImageList list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            this.ImageSize = list.ImageSize;
            this.TransparentColor = list.TransparentColor;
            this.Images = new VImageListItemCollection();
            foreach (Image img in list.Images)
            {
                VImageListItem newItem = new VImageListItem();
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                ms.Close();
                newItem.Content = ms.ToArray();
                this.Images.Add(newItem);
            }
        }
    }

    /// <summary>
    /// 虚拟图片列表项目集合
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [Serializable]
    public class VImageListItemCollection : List<VImageListItem>
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public VImageListItemCollection()
        {
        }
    }

    /// <summary>
    /// 虚拟图片列表项目
    /// </summary>
    ///<remarks>编制 袁永福</remarks>
    [Serializable ]
    public class VImageListItem
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public VImageListItem()
        {
        }

        private string _Key = null;
        /// <summary>
        /// 关键字
        /// </summary>
        [DefaultValue( null )]
        public string Key
        {
            get { return _Key; }
            set { _Key = value; }
        }

        private byte[] _Content = null;
        /// <summary>
        /// 图像二进制数据
        /// </summary>
        [DefaultValue( null )]
        [System.Xml.Serialization.XmlElement( DataType ="base64Binary ")]
        public byte[] Content
        {
            get { return _Content; }
            set { _Content = value; }
        }

        private string _Source = null;
        /// <summary>
        /// 图像数据来源
        /// </summary>
        [DefaultValue( null )]
        public string Source
        {
            get { return _Source; }
            set { _Source = value; }
        }
    }
}
