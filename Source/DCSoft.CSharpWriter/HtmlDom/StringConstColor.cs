/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;

namespace DCSoft.HtmlDom
{
	/// <summary>
	/// HTML颜色常量字符串
	/// </summary>
	public sealed class StringConstColor
	{
		public const string aliceblue             = "aliceblue"            ;//(#F0F8FF) 
		public const string antiquewhite          = "antiquewhite"         ;//(#FAEBD7) 
		public const string aqua                  = "aqua"                 ;//(#00FFFF) 
		public const string aquamarine            = "aquamarine"           ;//(#7FFFD4) 
		public const string azure                 = "azure"                ;//(#F0FFFF) 
		public const string beige                 = "beige"                ;//(#F5F5DC) 
		public const string bisque                = "bisque"               ;//(#FFE4C4) 
		public const string black                 = "black"                ;//(#000000) 
		public const string blanchedalmond        = "blanchedalmond"       ;//(#FFEBCD) 
		public const string blue                  = "blue"                 ;//(#0000FF) 
		public const string blueviolet            = "blueviolet"           ;//(#8A2BE2) 
		public const string brown                 = "brown"                ;//(#A52A2A) 
		public const string burlywood             = "burlywood"            ;//(#DEB887) 
		public const string cadetblue             = "cadetblue"            ;//(#5F9EA0) 
		public const string chartreuse            = "chartreuse"           ;//(#7FFF00) 
		public const string chocolate             = "chocolate"            ;//(#D2691E) 
		public const string coral                 = "coral"                ;//(#FF7F50) 
		public const string cornflowerblue        = "cornflowerblue"       ;//(#6495ED) 
		public const string cornsilk              = "cornsilk"             ;//(#FFF8DC) 
		public const string crimson               = "crimson"              ;//(#DC143C) 
		public const string cyan                  = "cyan"                 ;//(#00FFFF) 
		public const string darkblue              = "darkblue"             ;//(#00008B) 
		public const string darkcyan              = "darkcyan"             ;//(#008B8B) 
		public const string darkgoldenrod         = "darkgoldenrod"        ;//(#B8860B) 
		public const string darkgray              = "darkgray"             ;//(#A9A9A9) 
		public const string darkgreen             = "darkgreen"            ;//(#006400) 
		public const string darkkhaki             = "darkkhaki"            ;//(#BDB76B) 
		public const string darkmagenta           = "darkmagenta"          ;//(#8B008B) 
		public const string darkolivegreen        = "darkolivegreen"       ;//(#556B2F) 
		public const string darkorange            = "darkorange"           ;//(#FF8C00) 
		public const string darkorchid            = "darkorchid"           ;//(#9932CC) 
		public const string darkred               = "darkred"              ;//(#8B0000) 
		public const string darksalmon            = "darksalmon"           ;//(#E9967A) 
		public const string darkseagreen          = "darkseagreen"         ;//(#8FBC8B) 
		public const string darkslateblue         = "darkslateblue"        ;//(#483D8B) 
		public const string darkslategray         = "darkslategray"        ;//(#2F4F4F) 
		public const string darkturquoise         = "darkturquoise"        ;//(#00CED1) 
		public const string darkviolet            = "darkviolet"           ;//(#9400D3) 
		public const string deeppink              = "deeppink"             ;//(#FF1493) 
		public const string deepskyblue           = "deepskyblue"          ;//(#00BFFF) 
		public const string dimgray               = "dimgray"              ;//(#696969) 
		public const string dodgerblue            = "dodgerblue"           ;//(#1E90FF) 
		public const string firebrick             = "firebrick"            ;//(#B22222) 
		public const string floralwhite           = "floralwhite"          ;//(#FFFAF0) 
		public const string forestgreen           = "forestgreen"          ;//(#228B22) 
		public const string fuchsia               = "fuchsia"              ;//(#FF00FF) 
		public const string gainsboro             = "gainsboro"            ;//(#DCDCDC) 
		public const string ghostwhite            = "ghostwhite"           ;//(#F8F8FF) 
		public const string gold                  = "gold"                 ;//(#FFD700) 
		public const string goldenrod             = "goldenrod"            ;//(#DAA520) 
		public const string gray                  = "gray"                 ;//(#808080) 
		public const string green                 = "green"                ;//(#008000) 
		public const string greenyellow           = "greenyellow"          ;//(#ADFF2F) 
		public const string honeydew              = "honeydew"             ;//(#F0FFF0) 
		public const string hotpink               = "hotpink"              ;//(#FF69B4) 
		public const string indianred             = "indianred"            ;//(#CD5C5C) 
		public const string indigo                = "indigo"               ;//(#4B0082) 
		public const string ivory                 = "ivory"                ;//(#FFFFF0) 
		public const string khaki                 = "khaki"                ;//(#F0E68C) 
		public const string lavender              = "lavender"             ;//(#E6E6FA) 
		public const string lavenderblush         = "lavenderblush"        ;//(#FFF0F5) 
		public const string lawngreen             = "lawngreen"            ;//(#7CFC00) 
		public const string lemonchiffon          = "lemonchiffon"         ;//(#FFFACD) 
		public const string lightblue             = "lightblue"            ;//(#ADD8E6) 
		public const string lightcoral            = "lightcoral"           ;//(#F08080) 
		public const string lightcyan             = "lightcyan"            ;//(#E0FFFF) 
		public const string lightgoldenrodyellow  = "lightgoldenrodyellow" ;//(#FAFAD2)
		public const string lightgreen            = "lightgreen"           ;//(#90EE90) 
		public const string lightgrey             = "lightgrey"            ;//(#D3D3D3) 
		public const string lightpink             = "lightpink"            ;//(#FFB6C1) 
		public const string lightsalmon           = "lightsalmon"          ;//(#FFA07A) 
		public const string lightseagreen         = "lightseagreen"        ;//(#20B2AA) 
		public const string lightskyblue          = "lightskyblue"         ;//(#87CEFA) 
		public const string lightslategray        = "lightslategray"       ;//(#778899) 
		public const string lightsteelblue        = "lightsteelblue"       ;//(#B0C4DE) 
		public const string lightyellow           = "lightyellow"          ;//(#FFFFE0) 
		public const string lime                  = "lime"                 ;//(#00FF00) 
		public const string limegreen             = "limegreen"            ;//(#32CD32) 
		public const string linen                 = "linen"                ;//(#FAF0E6) 
		public const string magenta               = "magenta"              ;//(#FF00FF) 
		public const string maroon                = "maroon"               ;//(#800000) 
		public const string mediumaquamarine      = "mediumaquamarine"     ;//(#66CDAA) 
		public const string mediumblue            = "mediumblue"           ;//(#0000CD) 
		public const string mediumorchid          = "mediumorchid"         ;//(#BA55D3) 
		public const string mediumpurple          = "mediumpurple"         ;//(#9370DB) 
		public const string mediumseagreen        = "mediumseagreen"       ;//(#3CB371) 
		public const string mediumslateblue       = "mediumslateblue"      ;//(#7B68EE) 
		public const string mediumspringgreen     = "mediumspringgreen"    ;//(#00FA9A) 
		public const string mediumturquoise       = "mediumturquoise"      ;//(#48D1CC) 
		public const string mediumvioletred       = "mediumvioletred"      ;//(#C71585) 
		public const string midnightblue          = "midnightblue"         ;//(#191970) 
		public const string mintcream             = "mintcream"            ;//(#F5FFFA) 
		public const string mistyrose             = "mistyrose"            ;//(#FFE4E1) 
		public const string moccasin              = "moccasin"             ;//(#FFE4B5) 
		public const string navajowhite           = "navajowhite"          ;//(#FFDEAD) 
		public const string navy                  = "navy"                 ;//(#000080) 
		public const string oldlace               = "oldlace"              ;//(#FDF5E6) 
		public const string olive                 = "olive"                ;//(#808000) 
		public const string olivedrab             = "olivedrab"            ;//(#6B8E23) 
		public const string orange                = "orange"               ;//(#FFA500) 
		public const string orangered             = "orangered"            ;//(#FF4500) 
		public const string orchid                = "orchid"               ;//(#DA70D6) 
		public const string palegoldenrod         = "palegoldenrod"        ;//(#EEE8AA) 
		public const string palegreen             = "palegreen"            ;//(#98FB98) 
		public const string paleturquoise         = "paleturquoise"        ;//(#AFEEEE) 
		public const string palevioletred         = "palevioletred"        ;//(#DB7093) 
		public const string papayawhip            = "papayawhip"           ;//(#FFEFD5) 
		public const string peachpuff             = "peachpuff"            ;//(#FFDAB9) 
		public const string peru                  = "peru"                 ;//(#CD853F) 
		public const string pink                  = "pink"                 ;//(#FFC0CB) 
		public const string plum                  = "plum"                 ;//(#DDA0DD) 
		public const string powderblue            = "powderblue"           ;//(#B0E0E6) 
		public const string purple                = "purple"               ;//(#800080) 
		public const string red                   = "red"                  ;//(#FF0000) 
		public const string rosybrown             = "rosybrown"            ;//(#BC8F8F) 
		public const string royalblue             = "royalblue"            ;//(#4169E1) 
		public const string saddlebrown           = "saddlebrown"          ;//(#8B4513) 
		public const string salmon                = "salmon"               ;//(#FA8072) 
		public const string sandybrown            = "sandybrown"           ;//(#F4A460) 
		public const string seagreen              = "seagreen"             ;//(#2E8B57) 
		public const string seashell              = "seashell"             ;//(#FFF5EE) 
		public const string sienna                = "sienna"               ;//(#A0522D) 
		public const string silver                = "silver"               ;//(#C0C0C0) 
		public const string skyblue               = "skyblue"              ;//(#87CEEB) 
		public const string slateblue             = "slateblue"            ;//(#6A5ACD) 
		public const string slategray             = "slategray"            ;//(#708090) 
		public const string snow                  = "snow"                 ;//(#FFFAFA) 
		public const string springgreen           = "springgreen"          ;//(#00FF7F) 
		public const string steelblue             = "steelblue"            ;//(#4682B4) 
		public const string tan                   = "tan"                  ;//(#D2B48C) 
		public const string teal                  = "teal"                 ;//(#008080) 
		public const string thistle               = "thistle"              ;//(#D8BFD8) 
		public const string tomato                = "tomato"               ;//(#FF6347) 
		public const string turquoise             = "turquoise"            ;//(#40E0D0) 
		public const string violet                = "violet"               ;//(#EE82EE) 
		public const string wheat                 = "wheat"                ;//(#F5DEB3) 
		public const string white                 = "white"                ;//(#FFFFFF) 
		public const string whitesmoke            = "whitesmoke"           ;//(#F5F5F5) 
		public const string yellow                = "yellow"               ;//(#FFFF00) 
		public const string yellowgreen           = "yellowgreen"          ;//(#9ACD32) 
		public const string activeborder          = "activeborder"         ;
		public const string activecaption         = "activecaption"        ;
		public const string appworkspace          = "appworkspace"         ;
		public const string background            = "background"           ;
		public const string buttonface            = "buttonface"           ;
		public const string buttonhighlight       = "buttonhighlight"      ;
		public const string buttonshadow          = "buttonshadow"         ;
		public const string buttontext            = "buttontext"           ;
		public const string captiontext           = "captiontext"          ;
		public const string graytext              = "graytext"             ;
		public const string highlight             = "highlight"            ;
		public const string highlighttext         = "highlighttext"        ;
		public const string inactiveborder        = "inactiveborder"       ;
		public const string inactivecaption       = "inactivecaption"      ;
		public const string inactivecaptiontext   = "inactivecaptiontext"  ; 
		public const string infobackground        = "infobackground"       ;
		public const string infotext              = "infotext"             ;
		public const string menu                  = "menu"                 ;
		public const string menutext              = "menutext"             ;
		public const string scrollbar             = "scrollbar"            ;
		public const string threeddarkshadow      = "threeddarkshadow"     ;
		public const string threedface            = "threedface"           ;
		public const string threedhighlight       = "threedhighlight"      ;
		public const string threedlightshadow     = "threedlightshadow"    ;
		public const string threedshadow          = "threedshadow"         ;
		public const string window                = "window"               ;
		public const string windowframe           = "windowframe"          ;
		public const string windowtext            = "windowtext"           ;

		private StringConstColor()
		{
		}
	}//public sealed class HTMLColorConst
}