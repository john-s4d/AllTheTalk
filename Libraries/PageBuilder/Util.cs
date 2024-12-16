using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;


namespace CategorizerLib.Common
{
	public static class Util
	{
		public static String GetTempDirectory()
		{
			string path = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
			Directory.CreateDirectory(path);
			return path;
		}
	}

	public class StringTokenizer : IEnumerable, IEnumerator
	{
		string[] tokensizedStrs;
		int index = -1;

		public StringTokenizer(string target, char[] tokens)
		{
			tokensizedStrs = target.Split(tokens);
		}
		public IEnumerator GetEnumerator()
		{
			return this;
		}
		object IEnumerator.Current
		{
			get
			{
				return tokensizedStrs[index];
			}
		}
		bool IEnumerator.MoveNext()
		{
			if (++index >= tokensizedStrs.Length)
				return false;
			else
				return true;
		}
		void IEnumerator.Reset()
		{
			index = -1;
		}
	}	
}
