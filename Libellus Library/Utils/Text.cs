using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibellusLibrary.Utils
{
	public static class Text
	{
		public static string ASCII8ToString(byte[] ASCIIData)
		{
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
			var e = Encoding.GetEncoding("437");
			return e.GetString(ASCIIData);
		}

		public static byte[] StringtoASCII8(string text)
		{
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
			var e = Encoding.GetEncoding("437");
			return e.GetBytes(text);
		}
	}
}
