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

namespace DCSoft.Barcode
{
    internal class Code93BarcodeEncoder : BarcodeEncoder
    {
        public Code93BarcodeEncoder(string txt)
        {
            this.Raw_Data = txt;
        }

        private const string Chars = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ-. $/+%!@#$";
        private static string[] Patterns = 
		{
			"bsssbsbss" , // 0
			"bsbssbsss" , // 1
			"bsbsssbss" , // 2
			"bsbssssbs" , // 3
			"bssbsbsss" , // 4
			"bssbssbss" , // 5
			"bssbsssbs" , // 6
			"bsbsbssss" , // 7
			"bsssbssbs" , // 8
			"bssssbsbs" , // 9
			"bbsbsbsss" , // A
			"bbsbssbss" , // B
			"bbsbsssbs" , // C
			"bbssbsbss" , // D
			"bbssbssbs" , // E
			"bbsssbsbs" , // F
			"bsbbsbsss" , // G
			"bsbbssbss" , // H
			"bsbbsssbs" , // I
			"bssbbsbss" , // J
			"bsssbbsbs" , // K
			"bsbsbbsss" , // L
			"bsbssbbss" , // M
			"bsbsssbbs" , // N
			"bssbsbbss" , // O
			"bsssbsbbs" , // P
			"bbsbbsbss" , // Q
			"bbsbbssbs" , // R
			"bbsbsbbss" , // S
			"bbsbssbbs" , // T
			"bbssbsbbs" , // U
			"bbssbbsbs" , // V
			"bsbbsbbss" , // W
			"bsbbssbbs" , // X
			"bssbbsbbs" , // Y
			"bssbbbsbs" , // Z
			"bssbsbbbs" , // -
			"bbbsbsbss" , // .
			"bbbsbssbs" , // ' '
			"bbbssbsbs" , // $
			"bsbbsbbbs" , // /
			"bsbbbsbbs" , // +
			"bbsbsbbbs" , // %
			"bssbssbbs" , // SHIFT1
			"bbbsbbsbs" , // SHIFT2
			"bbbsbsbbs" , // SHIFT3
			"bssbbssbs" , // SHIFT4
			"bsbsbbbbs" , // START
			"bsbsbbbbsb" // STOP
		};

        public override string Encoded_Value
        {
            get
            {
                this.ErrorMessage = null;
                System.Text.StringBuilder output = new StringBuilder();
                //System.Text.StringBuilder myCode = new System.Text.StringBuilder();
                string bCode = this.Raw_Data;
                //			if (extended)
                //				bCode = GetCode39Ex(code);
                //			if (generateChecksum)
                //				bCode += GetChecksum(bCode);

                bCode = bCode.ToUpper();
                // 添加开始标记
                FillCharacter(Patterns[47], output);
                // 添加正文
                foreach (char c in bCode)
                {
                    int index = Chars.IndexOf(c);
                    if (index < 0)
                    {
                        this.ErrorMessage = BarcodeStrings.Code93InvaliData;
                        return null;
                    }
                    FillCharacter(Patterns[index], output);
                }
                // 计算第一个校验字符
                int sum = 0;
                int pow = bCode.Length;
                foreach (char c in bCode)
                {
                    sum = sum + Chars.IndexOf(c) * pow;
                    pow--;
                }
                sum = sum % 47;
                FillCharacter(Patterns[sum], output);

                // 计算第二个校验字符
                pow = bCode.Length + 1;
                foreach (char c in bCode)
                {
                    sum = sum + Chars.IndexOf(c) * pow;
                    pow--;
                }
                sum = sum % 47;
                FillCharacter(Patterns[sum], output);
                // 添加结束标记
                FillCharacter(Patterns[48], output);
                return output.ToString();
            }
        }

        private void FillCharacter(string Pattern, System.Text.StringBuilder output)
        {
            bool print = true;
            foreach (char c in Pattern)
            {
                if (c == 'b')
                {
                    output.Append("1");
                }
                else
                {
                    output.Append("0");
                }
                print = !print;
            }
        }

    }
}
