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
using System.Collections;

namespace DCSoft.Barcode
{
    internal class Code128BarcodeEncoder : BarcodeEncoder
    {

        public enum Code128Style
        {
            DYNAMIC,
            A,
            B,
            C
        };

        private System.Collections.ArrayList FormattedData = new System.Collections.ArrayList();
        private System.Collections.ArrayList EncodedData = new System.Collections.ArrayList();
        //private DataRow StartCharacter = null;
        private Code128Style type = Code128Style.DYNAMIC;

        /// <summary>
        /// Encodes data in Code128 format.
        /// </summary>
        /// <param name="input">Data to encode.</param>
        public Code128BarcodeEncoder(string input)
        {
            Raw_Data = input;
        }//Code128

        /// <summary>
        /// Encodes data in Code128 format.
        /// </summary>
        /// <param name="input">Data to encode.</param>
        /// <param name="type">Type of encoding to lock to. (Code 128A, Code 128B, Code 128C)</param>
        public Code128BarcodeEncoder(string input, Code128Style type)
        {
            this.type = type;
            Raw_Data = input;
        }//Code128

        private class Code128Value
        {
            /// <summary>
            /// 静态构造函数
            /// </summary>
            static Code128Value()
            {
                ArrayList list = new ArrayList();
                list.Add(new Code128Value("0", " ", " ", "00", "11011001100"));
                list.Add(new Code128Value("1", "!", "!", "01", "11001101100"));
                list.Add(new Code128Value("2", "\"", "\"", "02", "11001100110"));
                list.Add(new Code128Value("3", "#", "#", "03", "10010011000"));
                list.Add(new Code128Value("4", "$", "$", "04", "10010001100"));
                list.Add(new Code128Value("5", "%", "%", "05", "10001001100"));
                list.Add(new Code128Value("6", "&", "&", "06", "10011001000"));
                list.Add(new Code128Value("7", "'", "'", "07", "10011000100"));
                list.Add(new Code128Value("8", "(", "(", "08", "10001100100"));
                list.Add(new Code128Value("9", ")", ")", "09", "11001001000"));
                list.Add(new Code128Value("10", "*", "*", "10", "11001000100"));
                list.Add(new Code128Value("11", "+", "+", "11", "11000100100"));
                list.Add(new Code128Value("12", ",", ",", "12", "10110011100"));
                list.Add(new Code128Value("13", "-", "-", "13", "10011011100"));
                list.Add(new Code128Value("14", ".", ".", "14", "10011001110"));
                list.Add(new Code128Value("15", "/", "/", "15", "10111001100"));
                list.Add(new Code128Value("16", "0", "0", "16", "10011101100"));
                list.Add(new Code128Value("17", "1", "1", "17", "10011100110"));
                list.Add(new Code128Value("18", "2", "2", "18", "11001110010"));
                list.Add(new Code128Value("19", "3", "3", "19", "11001011100"));
                list.Add(new Code128Value("20", "4", "4", "20", "11001001110"));
                list.Add(new Code128Value("21", "5", "5", "21", "11011100100"));
                list.Add(new Code128Value("22", "6", "6", "22", "11001110100"));
                list.Add(new Code128Value("23", "7", "7", "23", "11101101110"));
                list.Add(new Code128Value("24", "8", "8", "24", "11101001100"));
                list.Add(new Code128Value("25", "9", "9", "25", "11100101100"));
                list.Add(new Code128Value("26", ":", ":", "26", "11100100110"));
                list.Add(new Code128Value("27", ";", ";", "27", "11101100100"));
                list.Add(new Code128Value("28", "<", "<", "28", "11100110100"));
                list.Add(new Code128Value("29", "=", "=", "29", "11100110010"));
                list.Add(new Code128Value("30", ">", ">", "30", "11011011000"));
                list.Add(new Code128Value("31", "?", "?", "31", "11011000110"));
                list.Add(new Code128Value("32", "@", "@", "32", "11000110110"));
                list.Add(new Code128Value("33", "A", "A", "33", "10100011000"));
                list.Add(new Code128Value("34", "B", "B", "34", "10001011000"));
                list.Add(new Code128Value("35", "C", "C", "35", "10001000110"));
                list.Add(new Code128Value("36", "D", "D", "36", "10110001000"));
                list.Add(new Code128Value("37", "E", "E", "37", "10001101000"));
                list.Add(new Code128Value("38", "F", "F", "38", "10001100010"));
                list.Add(new Code128Value("39", "G", "G", "39", "11010001000"));
                list.Add(new Code128Value("40", "H", "H", "40", "11000101000"));
                list.Add(new Code128Value("41", "I", "I", "41", "11000100010"));
                list.Add(new Code128Value("42", "J", "J", "42", "10110111000"));
                list.Add(new Code128Value("43", "K", "K", "43", "10110001110"));
                list.Add(new Code128Value("44", "L", "L", "44", "10001101110"));
                list.Add(new Code128Value("45", "M", "M", "45", "10111011000"));
                list.Add(new Code128Value("46", "N", "N", "46", "10111000110"));
                list.Add(new Code128Value("47", "O", "O", "47", "10001110110"));
                list.Add(new Code128Value("48", "P", "P", "48", "11101110110"));
                list.Add(new Code128Value("49", "Q", "Q", "49", "11010001110"));
                list.Add(new Code128Value("50", "R", "R", "50", "11000101110"));
                list.Add(new Code128Value("51", "S", "S", "51", "11011101000"));
                list.Add(new Code128Value("52", "T", "T", "52", "11011100010"));
                list.Add(new Code128Value("53", "U", "U", "53", "11011101110"));
                list.Add(new Code128Value("54", "V", "V", "54", "11101011000"));
                list.Add(new Code128Value("55", "W", "W", "55", "11101000110"));
                list.Add(new Code128Value("56", "X", "X", "56", "11100010110"));
                list.Add(new Code128Value("57", "Y", "Y", "57", "11101101000"));
                list.Add(new Code128Value("58", "Z", "Z", "58", "11101100010"));
                list.Add(new Code128Value("59", "[", "[", "59", "11100011010"));
                list.Add(new Code128Value("60", @"\", @"\", "60", "11101111010"));
                list.Add(new Code128Value("61", "]", "]", "61", "11001000010"));
                list.Add(new Code128Value("62", "^", "^", "62", "11110001010"));
                list.Add(new Code128Value("63", "_", "_", "63", "10100110000"));
                list.Add(new Code128Value("64", "\0", "`", "64", "10100001100"));
                list.Add(new Code128Value("65", Convert.ToChar(1).ToString(), "a", "65", "10010110000"));
                list.Add(new Code128Value("66", Convert.ToChar(2).ToString(), "b", "66", "10010000110"));
                list.Add(new Code128Value("67", Convert.ToChar(3).ToString(), "c", "67", "10000101100"));
                list.Add(new Code128Value("68", Convert.ToChar(4).ToString(), "d", "68", "10000100110"));
                list.Add(new Code128Value("69", Convert.ToChar(5).ToString(), "e", "69", "10110010000"));
                list.Add(new Code128Value("70", Convert.ToChar(6).ToString(), "f", "70", "10110000100"));
                list.Add(new Code128Value("71", Convert.ToChar(7).ToString(), "g", "71", "10011010000"));
                list.Add(new Code128Value("72", Convert.ToChar(8).ToString(), "h", "72", "10011000010"));
                list.Add(new Code128Value("73", Convert.ToChar(9).ToString(), "i", "73", "10000110100"));
                list.Add(new Code128Value("74", Convert.ToChar(10).ToString(), "j", "74", "10000110010"));
                list.Add(new Code128Value("75", Convert.ToChar(11).ToString(), "k", "75", "11000010010"));
                list.Add(new Code128Value("76", Convert.ToChar(12).ToString(), "l", "76", "11001010000"));
                list.Add(new Code128Value("77", Convert.ToChar(13).ToString(), "m", "77", "11110111010"));
                list.Add(new Code128Value("78", Convert.ToChar(14).ToString(), "n", "78", "11000010100"));
                list.Add(new Code128Value("79", Convert.ToChar(15).ToString(), "o", "79", "10001111010"));
                list.Add(new Code128Value("80", Convert.ToChar(16).ToString(), "p", "80", "10100111100"));
                list.Add(new Code128Value("81", Convert.ToChar(17).ToString(), "q", "81", "10010111100"));
                list.Add(new Code128Value("82", Convert.ToChar(18).ToString(), "r", "82", "10010011110"));
                list.Add(new Code128Value("83", Convert.ToChar(19).ToString(), "s", "83", "10111100100"));
                list.Add(new Code128Value("84", Convert.ToChar(20).ToString(), "t", "84", "10011110100"));
                list.Add(new Code128Value("85", Convert.ToChar(21).ToString(), "u", "85", "10011110010"));
                list.Add(new Code128Value("86", Convert.ToChar(22).ToString(), "v", "86", "11110100100"));
                list.Add(new Code128Value("87", Convert.ToChar(23).ToString(), "w", "87", "11110010100"));
                list.Add(new Code128Value("88", Convert.ToChar(24).ToString(), "x", "88", "11110010010"));
                list.Add(new Code128Value("89", Convert.ToChar(25).ToString(), "y", "89", "11011011110"));
                list.Add(new Code128Value("90", Convert.ToChar(26).ToString(), "z", "90", "11011110110"));
                list.Add(new Code128Value("91", Convert.ToChar(27).ToString(), "{", "91", "11110110110"));
                list.Add(new Code128Value("92", Convert.ToChar(28).ToString(), "|", "92", "10101111000"));
                list.Add(new Code128Value("93", Convert.ToChar(29).ToString(), ")", "93", "10100011110"));
                list.Add(new Code128Value("94", Convert.ToChar(30).ToString(), "~", "94", "10001011110"));

                list.Add(new Code128Value("95", Convert.ToChar(31).ToString(), Convert.ToChar(127).ToString(), "95", "10111101000"));
                list.Add(new Code128Value("96", Convert.ToChar(202).ToString()/*FNC3*/, Convert.ToChar(202).ToString()/*FNC3*/, "96", "10111100010"));
                list.Add(new Code128Value("97", Convert.ToChar(201).ToString()/*FNC2*/, Convert.ToChar(201).ToString()/*FNC2*/, "97", "11110101000"));
                list.Add(new Code128Value("98", "SHIFT", "SHIFT", "98", "11110100010"));
                list.Add(new Code128Value("99", "CODE_C", "CODE_C", "99", "10111011110"));
                list.Add(new Code128Value("100", "CODE_B", Convert.ToChar(203).ToString()/*FNC4*/, "CODE_B", "10111101110"));
                list.Add(new Code128Value("101", Convert.ToChar(203).ToString()/*FNC4*/, "CODE_A", "CODE_A", "11101011110"));
                list.Add(new Code128Value("102", Convert.ToChar(200).ToString()/*FNC1*/, Convert.ToChar(200).ToString()/*FNC1*/, Convert.ToChar(200).ToString()/*FNC1*/, "11110101110"));
                list.Add(new Code128Value("103", "START_A", "START_A", "START_A", "11010000100"));
                list.Add(new Code128Value("104", "START_B", "START_B", "START_B", "11010010000"));
                list.Add(new Code128Value("105", "START_C", "START_C", "START_C", "11010011100"));
                list.Add(new Code128Value("", "STOP", "STOP", "STOP", "11000111010"));
                Instances = (Code128Value[])list.ToArray(typeof(Code128Value));
            }

            public static readonly Code128Value[] Instances = null;

            public Code128Value(string v, string a, string b, string c, string encoding)
            {
                this.Value = v;
                this.A = a;
                this.B = b;
                this.C = c;
                this.Encoding = encoding;
            }

            public string Value = null;
            public string A = null;
            public string B = null;
            public string C = null;
            public string Encoding = null;
        }


        private string GetEncoding()
        {
            this.ErrorMessage = null;
            System.Text.StringBuilder result = new StringBuilder();
            int totalSum = 104;
            if (this.type == Code128Style.A)
            {
                // 起始位
                result.Append("11010000100");
                totalSum = 103;
                // 添加内容
                for (int iCount = 0; iCount < Raw_Data.Length; iCount++)
                {
                    string s = Raw_Data[iCount].ToString();
                    bool find = false;
                    foreach (Code128Value v in Code128Value.Instances)
                    {
                        if (v.A == s)
                        {
                            result.Append(v.Encoding);
                            totalSum = totalSum + Convert.ToInt32(v.Value) * (iCount + 1);
                            find = true;
                            break;
                        }
                    }
                    if (find == false)
                    {
                        this.ErrorMessage = BarcodeStrings.Code128InvaliData;
                        return null;
                    }
                }
                // 校验位
                result.Append(Code128Value.Instances[totalSum % 103].Encoding);
                // 结束位
                result.Append("1100011101011");
            }
            else if (this.type == Code128Style.B)
            {
                // 起始位
                result.Append("11010010000");
                totalSum = 104;
                // 添加内容
                for (int iCount = 0; iCount < Raw_Data.Length; iCount++)
                {
                    string s = Raw_Data[iCount].ToString();
                    bool find = false;
                    foreach (Code128Value v in Code128Value.Instances)
                    {
                        if (v.B == s)
                        {
                            result.Append(v.Encoding);
                            totalSum = totalSum + Convert.ToInt32(v.Value) * (iCount + 1);
                            find = true;
                            break;
                        }
                    }
                    if (find == false)
                    {
                        this.ErrorMessage = BarcodeStrings.Code128InvaliData;
                        return null;
                    }
                }
                // 校验位
                result.Append(Code128Value.Instances[totalSum % 103].Encoding);
                // 结束位
                result.Append("1100011101011");
            }
            else if (this.type == Code128Style.C)
            {
                // 起始位
                result.Append("11010011100");
                totalSum = 105;
                // 修正文本
                string txt = Raw_Data;
                if ((txt.Length % 2) != 0)
                    txt = "0" + txt;
                // 添加内容
                for (int iCount = 0; iCount < txt.Length; iCount += 2)
                {
                    string s = txt.Substring(iCount, 2);
                    bool find = false;
                    foreach (Code128Value v in Code128Value.Instances)
                    {
                        if (v.C == s)
                        {
                            result.Append(v.Encoding);
                            totalSum = totalSum + Convert.ToInt32(v.Value) * (1 + (iCount - 1) / 2);
                            find = true;
                            break;
                        }
                    }
                    if (find == false)
                    {
                        this.ErrorMessage = BarcodeStrings.Code128InvaliData;
                        return null;
                    }
                }
                // 校验位
                result.Append(Code128Value.Instances[totalSum % 103].Encoding);
                // 结束位
                result.Append("1100011101011");
            }
            else
            {
                this.ErrorMessage = BarcodeStrings.Code128InvaliData;
                return null;
            }
            return result.ToString();
        }


        public override string Encoded_Value
        {
            get
            {
                base.ErrorMessage = null;
                return GetEncoding();
            }
        }

    }//class
}
