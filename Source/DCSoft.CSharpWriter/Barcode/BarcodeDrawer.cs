/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Collections.Generic;

namespace DCSoft.Barcode
{
    /// <summary>
    /// 条码绘制对象
    /// </summary>
    /// <remarks>编写 袁永福</remarks>
    public class BarcodeDrawer
    {
        private static Dictionary<BarcodeStyle, string> _SampleValues = null;
        /// <summary>
        /// 各种条码的演示文本内容
        /// </summary>
        public static Dictionary<BarcodeStyle, string> SampleValues
        {
            get
            {
                if (_SampleValues == null)
                {
                    _SampleValues = new Dictionary<BarcodeStyle, string>();
                    _SampleValues[BarcodeStyle.BOOKLAND] = "9787121143137";
                    _SampleValues[BarcodeStyle.Codabar] = "A123456B";
                    _SampleValues[BarcodeStyle.CODE11] = "1234-5678";
                    //_SampleValues[BarcodeStyle.Code128] = "01234ABCDE";
                    _SampleValues[BarcodeStyle.Code128A] = "01234ABCDE";
                    _SampleValues[BarcodeStyle.Code128B] = "01234ABCDE";
                    _SampleValues[BarcodeStyle.Code128C] = "0123456789";
                    _SampleValues[BarcodeStyle.Code39] = "01234ABCDE";
                    _SampleValues[BarcodeStyle.Code39Extended] = "01234ABCDE";
                    _SampleValues[BarcodeStyle.Code93] = "01234ABCDE";
                    _SampleValues[BarcodeStyle.EAN13] = "012345678912";
                    _SampleValues[BarcodeStyle.EAN8] = "1234567";
                    _SampleValues[BarcodeStyle.I2of5] = "123456789";
                    _SampleValues[BarcodeStyle.Interleaved2of5] = "123456";
                    _SampleValues[BarcodeStyle.ISBN] = "9787121143137";
                    _SampleValues[BarcodeStyle.JAN13] = "491234567890";
                    _SampleValues[BarcodeStyle.LOGMARS] = "01234ABCDE";
                    _SampleValues[BarcodeStyle.Modified_Plessey] = "0123456789";
                    _SampleValues[BarcodeStyle.MSI_2Mod10] = "0123456789";
                    _SampleValues[BarcodeStyle.MSI_Mod10] = "0123456789";
                    _SampleValues[BarcodeStyle.MSI_Mod11] = "0123456789";
                    _SampleValues[BarcodeStyle.MSI_Mod11_Mod10] = "0123456789";
                    _SampleValues[BarcodeStyle.PostNet] = "123456789";
                    _SampleValues[BarcodeStyle.Standard2of5] = "123456789";
                    _SampleValues[BarcodeStyle.SUPP2] = "12";
                    _SampleValues[BarcodeStyle.SUPP5] = "12345";
                    _SampleValues[BarcodeStyle.UCC12] = "012345678912";
                    _SampleValues[BarcodeStyle.UCC13] = "0123456789123";
                    _SampleValues[BarcodeStyle.UNSPECIFIED] = "000000000";
                    _SampleValues[BarcodeStyle.UPCA] = "012345678912";
                    _SampleValues[BarcodeStyle.UPCE] = "01234567";
                    _SampleValues[BarcodeStyle.USD8] = "1234-5678";
                }
                return _SampleValues;
            }
        }
        /// <summary>
        /// 获得指定条码样式的演示文本
        /// </summary>
        /// <param name="style">条码样式</param>
        /// <returns>演示文本</returns>
        public static string GetSampleValue(BarcodeStyle style)
        {
            if (SampleValues.ContainsKey(style))
            {
                return SampleValues[style];
            }
            else
            {
                return "0000000000";
            }
        }
        
        private int _SourceContentVersion = 0;
        /// <summary>
        /// 内容来源版本号
        /// </summary>
        public int SourceContentVersion
        {
            get { return _SourceContentVersion; }
            set { _SourceContentVersion = value; }
        }

        private bool bEncoded = false;

        #region Constructors
        /// <summary>
        /// Default constructor.  Does not populate the raw data.  MUST be done via the RawData property before encoding.
        /// </summary>
        public BarcodeDrawer()
        {
            //constructor
        }//Barcode

        /// <summary>
        /// Constructor. Populates the raw data. No whitespace will be added before or after the barcode.
        /// </summary>
        /// <param name="data">String to be encoded.</param>
        public BarcodeDrawer(string data)
        {
            //constructor
            this.strText = data;
        }//Barcode

        public BarcodeDrawer(string data, BarcodeStyle style)
        {
            this.strText = data;
            this.intStyle = style;
        }

        public BarcodeDrawer(BarcodeStyle style)
        {
            this.intStyle = style;
        }

        #endregion

        #region Properties

        private string strText = "";
        /// <summary>
        /// Gets or sets the raw data to encode.
        /// </summary>
        public string Text
        {
            get
            {
                return strText;
            }
            set
            {
                strText = value;
                bEncoded = false;
            }
        }//EncodedValue

        private string strEncodedValue = "";
        /// <summary>
        /// Gets the encoded value.
        /// </summary>
        public string EncodedValue
        {
            get
            {
                return strEncodedValue;
            }
        }//EncodedValue

        private string strErrorMessage = null;
        /// <summary>
        /// 检测到的错误信息
        /// </summary>
        public string ErrorMessage
        {
            get
            {
                return strErrorMessage;
            }
        }

        private string _Country_Assigning_Manufacturer_Code = "N/A";
        /// <summary>
        /// Gets the Country that assigned the Manufacturer Code.
        /// </summary>
        public string Country_Assigning_Manufacturer_Code
        {
            get
            {
                return _Country_Assigning_Manufacturer_Code;
            }
        }//Country_Assigning_Manufacturer_Code

        private BarcodeStyle intStyle = BarcodeStyle.Code39;
        /// <summary>
        /// Gets or sets the Encoded Type (ex. UPC-A, EAN-13 ... etc)
        /// </summary>
        public BarcodeStyle Style
        {
            set
            {
                intStyle = value;
            }
            get
            {
                return intStyle;
            }
        }//EncodedType


        private Color _ForeColor = Color.Black;
        /// <summary>
        /// Gets or sets the color of the bars. (Default is black)
        /// </summary>
        public Color ForeColor
        {
            get
            {
                return this._ForeColor;
            }
            set
            {
                this._ForeColor = value;
            }
        }//ForeColor


        private float fMinBarWidth = 2;
        /// <summary>
        /// 条码细条宽度
        /// </summary>
        public float MinBarWidth
        {
            get
            {
                return fMinBarWidth;
            }
            set
            {
                fMinBarWidth = value;
                bEncoded = false;
            }
        }

        private bool bolShowText = true;
        /// <summary>
        /// 是否绘制文本
        /// </summary>
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

        private Font myFont = System.Windows.Forms.Control.DefaultFont;
        /// <summary>
        /// 绘制文字使用的字体
        /// </summary>
        public Font Font
        {
            get
            {
                return myFont;
            }
            set
            {
                myFont = value;
            }
        }

        private System.Drawing.StringAlignment intTextAlignment = StringAlignment.Center;
        /// <summary>
        /// 文本对齐方式
        /// </summary>
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

        private float fWidth = 0;
        /// <summary>
        /// 条码区域宽度
        /// </summary>
        public float Width
        {
            get
            {
                return fWidth;
            }
        }


        #endregion

        #region Functions

        /// <summary>
        /// Encodes the raw data into binary form representing bars and spaces.
        /// </summary>
        public bool Encode()
        {
            this.strErrorMessage = null;
            //make sure there is something to encode
            if (strText == null || strText.Trim().Length == 0)
            {
                this.strErrorMessage = BarcodeStrings.TextMustNotNull;
                this.strEncodedValue = "";
                return false;
            }
            //throw new ArgumentNullException("Text");

            this.strEncodedValue = "";
            this._Country_Assigning_Manufacturer_Code = "N/A";

            BarcodeEncoder ibarcode;
            switch (this.intStyle)
            {
                case BarcodeStyle.UCC12:
                case BarcodeStyle.UPCA: //Encode_UPCA();
                    ibarcode = new UPCABarcodeEncoder(strText);
                    break;
                case BarcodeStyle.UCC13:
                case BarcodeStyle.EAN13: //Encode_EAN13();
                    ibarcode = new EAN13BarcodeEncoder(strText);
                    break;
                case BarcodeStyle.Interleaved2of5: //Encode_Interleaved2of5();
                    ibarcode = new Interleaved2of5BarcodeEncoder(strText);
                    break;
                case BarcodeStyle.I2of5:
                case BarcodeStyle.Standard2of5: //Encode_Standard2of5();
                    ibarcode = new Standard2of5BarcodeEncoder(strText);
                    break;
                case BarcodeStyle.LOGMARS:
                case BarcodeStyle.Code39: //Encode_Code39();
                    ibarcode = new Code39BarcodeEncoder(strText);
                    break;
                case BarcodeStyle.Code39Extended:
                    ibarcode = new Code39BarcodeEncoder(strText, true);
                    break;
                case BarcodeStyle.Code93:
                    ibarcode = new Code93BarcodeEncoder(strText);
                    break;
                case BarcodeStyle.Codabar: //Encode_Codabar();
                    ibarcode = new CodabarBarcodeEncoder(strText);
                    break;
                case BarcodeStyle.PostNet: //Encode_PostNet();
                    ibarcode = new PostnetBarcodeEncoder(strText);
                    break;
                case BarcodeStyle.ISBN:
                case BarcodeStyle.BOOKLAND: //Encode_ISBN_Bookland();
                    ibarcode = new ISBNBarcodeEncoder(strText);
                    break;
                case BarcodeStyle.JAN13: //Encode_JAN13();
                    ibarcode = new JAN13BarcodeEncoder(strText);
                    break;
                case BarcodeStyle.SUPP2: //Encode_UPCSupplemental_2();
                    ibarcode = new UPCSupplement2BarcodeEncoder(strText);
                    break;
                case BarcodeStyle.MSI_Mod10:
                case BarcodeStyle.MSI_2Mod10:
                case BarcodeStyle.MSI_Mod11:
                case BarcodeStyle.MSI_Mod11_Mod10:
                case BarcodeStyle.Modified_Plessey: //Encode_MSI();
                    ibarcode = new MSIBarcodeEncoder(strText, intStyle);
                    break;
                case BarcodeStyle.SUPP5: //Encode_UPCSupplemental_5();
                    ibarcode = new UPCSupplement5BarcodeEncoder(strText);
                    break;
                case BarcodeStyle.UPCE: //Encode_UPCE();
                    ibarcode = new UPCEBarcodeEncoder(strText);
                    break;
                case BarcodeStyle.EAN8: //Encode_EAN8();
                    ibarcode = new EAN8BarcodeEncoder(strText);
                    break;
                case BarcodeStyle.USD8:
                case BarcodeStyle.CODE11: //Encode_Code11();
                    ibarcode = new Code11BarcodeEncoder(strText);
                    break;
                ////case BarcodeStyle.Code128: //Encode_Code128();
                ////    ibarcode = new Code128BarcodeEncoder(strText);
                ////    break;
                case BarcodeStyle.Code128A:
                    ibarcode = new Code128BarcodeEncoder(strText, Code128BarcodeEncoder.Code128Style.A);
                    break;
                case BarcodeStyle.Code128B:
                    ibarcode = new Code128BarcodeEncoder(strText, Code128BarcodeEncoder.Code128Style.B);
                    break;
                case BarcodeStyle.Code128C:
                    ibarcode = new Code128BarcodeEncoder(strText, Code128BarcodeEncoder.Code128Style.C);
                    break;
                default:
                    this.strErrorMessage = BarcodeStrings.InvaliBarcodeStyle;
                    return false;
            }//switch

            ibarcode.ErrorMessage = null;
            this.strEncodedValue = ibarcode.Encoded_Value;
            this.strErrorMessage = ibarcode.ErrorMessage;

            bEncoded = true;

            if (strEncodedValue != null)
            {
                switch (this.intStyle)
                {
                    case BarcodeStyle.UPCA:
                    case BarcodeStyle.EAN13:
                    case BarcodeStyle.EAN8:
                    case BarcodeStyle.Standard2of5:
                    case BarcodeStyle.I2of5:
                    case BarcodeStyle.Interleaved2of5:
                    case BarcodeStyle.CODE11:
                    case BarcodeStyle.Code39:
                    case BarcodeStyle.Code39Extended:
                    case BarcodeStyle.Code93:
                    //////case BarcodeStyle.Code128:
                    case BarcodeStyle.Code128A:
                    case BarcodeStyle.Code128B:
                    case BarcodeStyle.Code128C:
                    case BarcodeStyle.LOGMARS:
                    case BarcodeStyle.Codabar:
                    case BarcodeStyle.BOOKLAND:
                    case BarcodeStyle.ISBN:
                    case BarcodeStyle.SUPP2:
                    case BarcodeStyle.SUPP5:
                    case BarcodeStyle.JAN13:
                    case BarcodeStyle.MSI_Mod10:
                    case BarcodeStyle.MSI_2Mod10:
                    case BarcodeStyle.MSI_Mod11:
                    case BarcodeStyle.MSI_Mod11_Mod10 :
                    case BarcodeStyle.Modified_Plessey :
                    case BarcodeStyle.UPCE:
                    case BarcodeStyle.USD8:
                    case BarcodeStyle.UCC12:
                    case BarcodeStyle.UCC13:
                        {
                            fWidth = strEncodedValue.Length * fMinBarWidth;
                            break;
                        }//case
                    case BarcodeStyle.PostNet:
                        {
                            fWidth = strEncodedValue.Length * fMinBarWidth * 2;
                            break;
                        }//case
                    default:
                        fWidth = 0;
                        return false;
                }//switch
                return true;
            }
            return false;
        }//Encode

        /// <summary>
        /// Gets a bitmap representation of the encoded data.
        /// </summary>
        /// <param name="DrawColor">Color to draw the bars.</param>
        /// <param name="BackColor">Color to draw the spaces.</param>
        /// <returns>Bitmap of encoded value.</returns>
        public void Draw(Graphics g, Rectangle bounds)
        {
            if (bEncoded == false)
            {
                this.Encode();
            }

            if (strEncodedValue == null || strEncodedValue.Trim().Length == 0)
            {
                if (strErrorMessage != null)
                {
                    using (SolidBrush b = new SolidBrush(this.ForeColor))
                    {
                        using (StringFormat f = new StringFormat())
                        {
                            f.Alignment = StringAlignment.Center;
                            f.LineAlignment = StringAlignment.Center;
                            g.DrawString(
                                this.strErrorMessage,
                                this.myFont,
                                b,
                                new RectangleF(
                                    bounds.Left,
                                    bounds.Top,
                                    bounds.Width,
                                    bounds.Height),
                                f);
                        }
                    }
                }
                return;
            }

            float textHeight = 0;
            if (bolShowText)
            {
                textHeight = myFont.GetHeight(g);
            }

            switch (this.intStyle)
            {
                case BarcodeStyle.UPCA:
                case BarcodeStyle.EAN13:
                case BarcodeStyle.EAN8:
                case BarcodeStyle.Standard2of5:
                case BarcodeStyle.I2of5:
                case BarcodeStyle.Interleaved2of5:
                case BarcodeStyle.CODE11:
                case BarcodeStyle.Code39:
                case BarcodeStyle.Code39Extended:
                case BarcodeStyle.Code93:
                //////case BarcodeStyle.Code128:
                case BarcodeStyle.Code128A:
                case BarcodeStyle.Code128B:
                case BarcodeStyle.Code128C:
                case BarcodeStyle.LOGMARS:
                case BarcodeStyle.Codabar:
                case BarcodeStyle.BOOKLAND:
                case BarcodeStyle.ISBN:
                case BarcodeStyle.SUPP2:
                case BarcodeStyle.SUPP5:
                case BarcodeStyle.JAN13:
                case BarcodeStyle.MSI_Mod10:
                case BarcodeStyle.MSI_2Mod10:
                case BarcodeStyle.MSI_Mod11:
                case BarcodeStyle.MSI_Mod11_Mod10 :
                case BarcodeStyle.Modified_Plessey :
                case BarcodeStyle.UPCE:
                case BarcodeStyle.USD8:
                case BarcodeStyle.UCC12:
                case BarcodeStyle.UCC13:
                    {
                        int width = strEncodedValue.Length * 2;
                        using (SolidBrush foreBrush = new SolidBrush(this.ForeColor))
                        {
                            SolidBrush brush = foreBrush;

                            float pos = bounds.Left;
                            for (int iCount = 0; iCount < strEncodedValue.Length; iCount++)
                            {
                                if (strEncodedValue[iCount] == '1')
                                {
                                    g.FillRectangle(
                                        brush,
                                        pos,
                                        bounds.Top,
                                        fMinBarWidth,
                                        bounds.Height - textHeight);
                                }
                                pos = pos + fMinBarWidth;
                            }
                        }//using
                        break;
                    }//case
                case BarcodeStyle.PostNet:
                    {
                        //int w = strEncodedValue.Length * 3;
                        using (SolidBrush foreBrush = new SolidBrush(this.ForeColor))
                        {
                            for (int iCount = 0; iCount < strEncodedValue.Length; iCount++)
                            {
                                if (strEncodedValue[iCount] == '1')
                                {
                                    g.FillRectangle(
                                        foreBrush,
                                        new RectangleF(
                                            bounds.Left + iCount * fMinBarWidth * 2,
                                            bounds.Top,
                                            fMinBarWidth,
                                            bounds.Height - textHeight));
                                }
                                else
                                {
                                    g.FillRectangle(
                                        foreBrush,
                                        new RectangleF(
                                            bounds.Left + iCount * fMinBarWidth * 2,
                                            bounds.Top,
                                            fMinBarWidth,
                                            (bounds.Height - textHeight) / 2));
                                }
                            }
                        }
                        break;
                    }//case
                default: return;
            }//switch

            if (bolShowText)
            {
                using (SolidBrush foreBrush = new SolidBrush(this.ForeColor))
                {
                    // 绘制文本
                    using (StringFormat format = new StringFormat())
                    {
                        format.FormatFlags = StringFormatFlags.NoWrap;
                        format.Alignment = intTextAlignment;
                        g.DrawString(
                            this.Text,
                            myFont,
                            foreBrush,
                            new RectangleF(
                                bounds.Left,
                                bounds.Bottom - textHeight,
                                fWidth,
                                textHeight),
                            format);
                    }
                }
            }//if
        }//Generate_Image



        /// <summary>
        /// Gets a bitmap representation of the encoded data.
        /// </summary>
        /// <param name="DrawColor">Color to draw the bars.</param>
        /// <param name="BackColor">Color to draw the spaces.</param>
        /// <returns>Bitmap of encoded value.</returns>
        public Bitmap GenerateImage(Color BackColor)
        {
            if (strEncodedValue == null || strEncodedValue.Trim().Length == 0)
            {
                return null;
            }
            //if (strEncodedValue == "") throw new Exception("EGENERATE_IMAGE-1: Must be encoded first.");
            Bitmap b = null;

            switch (this.intStyle)
            {
                case BarcodeStyle.UPCA:
                case BarcodeStyle.EAN13:
                case BarcodeStyle.EAN8:
                case BarcodeStyle.Standard2of5:
                case BarcodeStyle.I2of5:
                case BarcodeStyle.Interleaved2of5:
                case BarcodeStyle.CODE11:
                case BarcodeStyle.Code39:
                case BarcodeStyle.Code39Extended:
                case BarcodeStyle.Code93:
                //////case BarcodeStyle.Code128:
                case BarcodeStyle.Code128A:
                case BarcodeStyle.Code128B:
                case BarcodeStyle.Code128C:
                case BarcodeStyle.LOGMARS:
                case BarcodeStyle.Codabar:
                case BarcodeStyle.BOOKLAND:
                case BarcodeStyle.ISBN:
                case BarcodeStyle.SUPP2:
                case BarcodeStyle.SUPP5:
                case BarcodeStyle.JAN13:
                case BarcodeStyle.MSI_Mod10:
                case BarcodeStyle.MSI_2Mod10:
                case BarcodeStyle.MSI_Mod11:
                case BarcodeStyle.Modified_Plessey :
                case BarcodeStyle.UPCE:
                case BarcodeStyle.USD8:
                case BarcodeStyle.UCC12:
                case BarcodeStyle.UCC13:
                    {
                        b = new Bitmap(strEncodedValue.Length * 2, strEncodedValue.Length);

                        int pos = 0;

                        using (Graphics g = Graphics.FromImage(b))
                        {
                            using (Pen forePen = new Pen(this.ForeColor, 2), backPen = new Pen(BackColor, 2))
                            {
                                Pen p = null;
                                while ((pos * 2 + 1) < b.Width)
                                {
                                    if (pos < strEncodedValue.Length)
                                    {
                                        if (strEncodedValue[pos] == '1')
                                            p = forePen;
                                        if (strEncodedValue[pos] == '0')
                                            p = backPen;
                                    }//if

                                    //lines are 2px wide so draw the appropriate color line vertically
                                    g.DrawLine(p, new Point(pos * 2 + 1, 0), new Point(pos * 2 + 1, b.Height));

                                    pos++;
                                }//while
                            }
                        }//using
                        break;
                    }//case
                case BarcodeStyle.PostNet:
                    {
                        b = new Bitmap(strEncodedValue.Length * 4, 20);

                        //draw image
                        for (int y = b.Height - 1; y > 0; y--)
                        {
                            int x = 0;
                            if (y < b.Height / 2)
                            {
                                //top
                                while (x < b.Width)
                                {
                                    if (strEncodedValue[x / 4] == '1')
                                    {
                                        //draw bar
                                        b.SetPixel(x, y, this.ForeColor);
                                        b.SetPixel(x + 1, y, this.ForeColor);
                                        b.SetPixel(x + 2, y, BackColor);
                                        b.SetPixel(x + 3, y, BackColor);
                                    }//if
                                    else
                                    {
                                        //draw space
                                        b.SetPixel(x, y, BackColor);
                                        b.SetPixel(x + 1, y, BackColor);
                                        b.SetPixel(x + 2, y, BackColor);
                                        b.SetPixel(x + 3, y, BackColor);
                                    }//else
                                    x += 4;
                                }//while
                            }//if
                            else
                            {
                                //bottom
                                while (x < b.Width)
                                {
                                    b.SetPixel(x, y, this.ForeColor);
                                    b.SetPixel(x + 1, y, this.ForeColor);
                                    b.SetPixel(x + 2, y, BackColor);
                                    b.SetPixel(x + 3, y, BackColor);
                                    x += 4;
                                }//while
                            }//else

                        }//for

                        break;
                    }//case
                default: return null;
            }//switch
            bEncoded = true;

            return b;
        }//Generate_Image


        #region Label Generation
        public Image Generate_Labels(Image img)
        {
            if (bEncoded)
            {
                switch (this.intStyle)
                {
                    case BarcodeStyle.EAN13:
                    case BarcodeStyle.EAN8:
                    case BarcodeStyle.Standard2of5:
                    case BarcodeStyle.I2of5:
                    case BarcodeStyle.Interleaved2of5:
                    case BarcodeStyle.CODE11:
                    case BarcodeStyle.Code39:
                    case BarcodeStyle.Code39Extended:
                    case BarcodeStyle.Code93:
                    //////case BarcodeStyle.Code128:
                    case BarcodeStyle.Code128A:
                    case BarcodeStyle.Code128B:
                    case BarcodeStyle.Code128C:
                    case BarcodeStyle.LOGMARS:
                    case BarcodeStyle.Codabar:
                    case BarcodeStyle.BOOKLAND:
                    case BarcodeStyle.ISBN:
                    case BarcodeStyle.SUPP2:
                    case BarcodeStyle.SUPP5:
                    case BarcodeStyle.JAN13:
                    case BarcodeStyle.MSI_Mod10:
                    case BarcodeStyle.MSI_2Mod10:
                    case BarcodeStyle.MSI_Mod11:
                    case BarcodeStyle.UPCE:
                    case BarcodeStyle.USD8:
                    case BarcodeStyle.UCC13: return Label_Generic(img);
                    case BarcodeStyle.UCC12:
                    case BarcodeStyle.UPCA: return Label_UPCA(img);
                    default: throw new Exception("EGENERATE_LABELS-1: Invalid type.");
                }//switch
            }//if
            else
                throw new Exception("EGENERATE_LABELS-2: Encode the image first.");
        }//Generate_Labels
        private Image Label_UPCA(Image img)
        {
            System.Drawing.Font font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);

            Graphics g = Graphics.FromImage(img);

            g.DrawImage(img, (float)0, (float)0);

            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;

            //draw boxes
            g.FillRectangle(new SolidBrush(Color.White), new Rectangle(img.Width / 16, img.Height - 12, (int)(img.Width * 0.42), img.Height / 2));
            g.FillRectangle(new SolidBrush(Color.White), new Rectangle((int)(img.Width * 0.52), img.Height - 12, (int)(img.Width * 0.42), img.Height / 2));

            string drawstring1 = "";
            string drawstring2 = "";
            foreach (char c in strText.Substring(1, 5))
            {
                drawstring1 += c.ToString() + "  ";
            }//foreach
            foreach (char c in strText.Substring(6, 5))
            {
                drawstring2 += c.ToString() + "  ";
            }//foreach

            drawstring1 = drawstring1.Substring(0, drawstring1.Length - 1);
            drawstring2 = drawstring2.Substring(0, drawstring2.Length - 1);
            g.DrawString(drawstring1, font, new SolidBrush(this.ForeColor), new Rectangle(img.Width / 14, img.Height - 13, (int)(img.Width * 0.50), img.Height / 2));
            g.DrawString(drawstring2, font, new SolidBrush(this.ForeColor), new Rectangle((int)(img.Width * 0.55), img.Height - 13, (int)(img.Width * 0.50), img.Height / 2));
            g.Save();
            g.Dispose();

            Bitmap borderincluded = new Bitmap((int)(img.Width * 1.12), (int)(img.Height * 1.12));
            for (int y = 0; y < borderincluded.Height; y++)
                for (int x = 0; x < borderincluded.Width; x++)
                    borderincluded.SetPixel(x, y, Color.White);

            Graphics g2 = Graphics.FromImage((Image)borderincluded);

            g2.SmoothingMode = SmoothingMode.HighQuality;
            g2.InterpolationMode = InterpolationMode.NearestNeighbor;
            g2.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g2.CompositingQuality = CompositingQuality.HighQuality;

            g2.DrawImage((Image)img, (float)((float)img.Width * 0.06), (float)((float)img.Height * 0.06), (float)img.Width, (float)img.Height);

            //UPC-A check digit and number system chars are a little smaller than the other numbers
            font = new Font("MS Sans Serif", 9, FontStyle.Regular);

            //draw the number system digit
            g2.DrawString(strText[0].ToString(), font, new SolidBrush(this.ForeColor), new Rectangle(-1, img.Height + (int)(img.Height * 0.07) - 13, (int)(img.Width * 0.35), img.Height / 2));

            //draw the check digit
            g2.DrawString(strText[strText.Length - 1].ToString(), font, new SolidBrush(this.ForeColor), new Rectangle((int)(borderincluded.Width * 0.96), img.Height + (int)(img.Height * 0.07) - 13, (int)(img.Width * 0.35), img.Height / 2));

            g2.Save();
            g2.Dispose();

            //this.Encoded_Image = (Image)borderincluded;

            return (Image)borderincluded;
        }//Label_UPCA
        private Image Label_Generic(Image img)
        {
            System.Drawing.Font font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);

            using (Graphics g = Graphics.FromImage(img))
            {
                g.DrawImage(img, (float)0, (float)0);

                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;

                //color a white box at the bottom of the barcode to hold the string of data
                g.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, img.Height - 16, img.Width, 16));

                //draw datastring under the barcode image
                StringFormat f = new StringFormat();
                f.Alignment = StringAlignment.Center;
                g.DrawString(this.strText, font, new SolidBrush(this.ForeColor), (float)(img.Width / 2), img.Height - 16, f);

                g.Save();
            }//using
            return img;
        }//Label_Generic
        #endregion
        #endregion

        public override string ToString()
        {
            string txt = this.Style + ":" + this.Text;
            if (this.strErrorMessage != null && this.strErrorMessage.Trim().Length > 0)
            {
                txt = txt + "#" + strErrorMessage;
            }
            return txt;
        }

    }//Barcode Class
}
