using System;
using System.Collections.Generic;
using System.Text;

namespace CategorizerLib
{
	public class Config
	{
		private static String _dbConnectString = "Server=localhost;Database=feedrake;Uid=feedrake;Pwd=fr#21";
		private static String _executablePath = @"C:\Program Files\TextGarden\";
		private static String _stopWords = "I,a,about,an,are,as,at,be,by,com,de,en,for,from,how,in,is,la,of,on,or,that,the,this,to,was,what,when,where,who,will,with,und,the,www,-";
		private static String[] _stopWordsArray;

		public static String DbConnectString
		{
			get { return _dbConnectString; }
			set { _dbConnectString = value; }
		}

		public static String ExecutablePath
		{
			get { return _executablePath; }
			set { _executablePath = value; }
		}

		public static String StopWords
		{
			get { return _stopWords; }
			set { _stopWords = value; }
		}

		public static String[] StopWordsArray
		{
			get
			{
				if (_stopWordsArray == null)
					_stopWordsArray = _stopWords.Split(new char[] { ',' });
				return _stopWordsArray;
			}
		}
	}
}
