using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Xml;
using System.IO;
using System.Reflection;

using CategorizerLib.Common;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace CategorizerLib.TextGarden
{
	public class BagOfWords : IDisposable
	{

		#region Global Declarations

		private String _tempDir;
		private Boolean _manageTempDir = false;
		private DataTable _dataSource;
		private String[] _dataColumns;
		private String _keyColumn;
		private String _id;
		private Boolean _disposed = false;

		#endregion

		#region Constructors

		private BagOfWords()
		{
			_id = Guid.NewGuid().ToString();
		}

		public BagOfWords(DataTable DataSource)
			: this()
		{
			_dataSource = DataSource;
		}

		public BagOfWords(DataTable DataSource, String TempDir)
			: this(DataSource)
		{
			_tempDir = TempDir;
		}

		public BagOfWords(DataTable DataSource, String keyColumn, String[] dataColumns)
			: this(DataSource)
		{
			_keyColumn = keyColumn;
			_dataColumns = dataColumns;
		}

		public BagOfWords(DataTable DataSource, String keyColumn, String[] dataColumns, String TempDir)
			: this(DataSource, keyColumn, dataColumns)
		{
			_tempDir = TempDir;
		}

		#endregion

		#region Properties

		public string ResultPath
		{
			get
			{
				EnsureNotDisposed();
				return Path.Combine(_tempDir, @"BowResult" + _id + ".bow");
			}
		}

		#endregion

		#region Private Methods

		private void Content2Txt()
		{
			DataColumn[] dataColumns = GetDataColumns();

			StreamWriter sw = new StreamWriter(Path.Combine(_tempDir, "BowRawData" + _id + ".txt"), false);

			int i = 0;

			foreach (DataRow row in _dataSource.Rows)
			{
				String content = String.Empty;
				String rowId = String.Empty;

				if (_keyColumn == null)
				{
					rowId = Convert.ToString(i++);
				}
				else
				{
					rowId = row[_dataSource.Columns[_keyColumn]].ToString();
				}

				foreach (DataColumn col in dataColumns)
				{
					content += " " + row[col].ToString().Normalize();
				}

				content = RemoveHtmlTags(content);
				content = ConvertToAscii(content);
				content = RemovePunctuation(content);
				content = RemoveStopWords(content);
				content = TrimString(content);


				if (content.Length > 100)
				{
					// Write the entries as a file with lines
					sw.WriteLine("{0} {1}", rowId, content);
				}
				else
				{
					// TODO: Update the database.
				}
			}
			sw.Close();
		}

		private void Txt2Bow()
		{

			/*
 * Text To Bag-Of-Words [Sep 30 2004]
 * ==================================
 * usage: Txt2Bow
 * -idir:Input-Directory (default:'')
 * -imtx:Input-Matrix-File (default:'')
 * -itab:Input-Tab-File (default:'')
 * -itsc:Input-Transaction-File (default:'')
 * -ispr:Input-Sparse-File (default:'')
 * -icpd:Input-CompactDocuments-File (default:'')
 * -itbs:Input-TextBase-File (default:'')
 * -ilndoc:Input-LineDocuments-File (default:'')
 * -inlndoc:Input-Named-LineDocuments-File (default:'')
 * -ir21578:Input-Reuters21578-Path (default:'')
 * -o:Bow-Output-File (.bow) (default:'')
 * -ostat:Statistics-Output-File (.txt) (default:'')
 * -docs:Documents (default:-1)
 * -recurse:Recurse-Directories (default:'F')
 * ====================
 */

			ProcessStartInfo txt2bow = new ProcessStartInfo();
			txt2bow.FileName = Path.Combine(Config.ExecutablePath, @"Txt2Bow.exe");
			txt2bow.WorkingDirectory = _tempDir;

			//txt2bow.Arguments = @"-ilndoc:BowRawData" + _id + ".txt -o:BowResult" + _id + ".bow";
			txt2bow.Arguments = @"-inlndoc:BowRawData" + _id + ".txt -o:BowResult" + _id + ".bow";

			// txt2bow.WindowStyle = ProcessWindowStyle.Hidden;
			// txt2bow.CreateNoWindow = true;

			Process myProcess = Process.Start(txt2bow);
			myProcess.WaitForExit();
		}

		private DataColumn[] GetDataColumns()
		{
			ArrayList al = new ArrayList();
			if (_dataColumns == null)
			{
				for (int i = 0; i < _dataSource.Columns.Count; i++)
				{
					al.Add(_dataSource.Columns[i]);
				}
			}
			else
			{
				foreach (String name in _dataColumns)
				{
					al.Add(_dataSource.Columns[name]);
				}
			}
			al.TrimToSize();
			return (DataColumn[])al.ToArray(typeof(DataColumn));
		}

		private void EnsureParametersExist()
		{
			if (_dataSource == null)
			{
				throw new ArgumentNullException("DataSource");
			}
		}

		private void EnsureTempDirExists()
		{
			if (_tempDir == null)
			{
				_tempDir = Util.GetTempDirectory();
				_manageTempDir = true;
			}
		}

		private String RemoveHtmlTags(String Input)
		{
			Input = System.Web.HttpContext.Current.Server.HtmlDecode(Input);
			return Regex.Replace(Input, @"<(.|\n)*?>", " ");
		}

		private String ConvertToAscii(String Input)
		{
			byte[] bytes = System.Text.UTF8Encoding.UTF8.GetBytes(Input);
			return System.Text.ASCIIEncoding.ASCII.GetString(bytes);
		}

		private String RemovePunctuation(String Input)
		{
			Input = Input.Replace("\r", " ");
			Input = Input.Replace("\n", " ");
			Input = Regex.Replace(Input, @"[^a-zA-Z0-9-\s\']+", " ");
			return Input;
		}

		private String RemoveStopWords(String Input)
		{
			String output = String.Empty;
			Boolean allowed;

			StringTokenizer st = new StringTokenizer(Input, new char[] { ' ' });

			foreach (String token in st)
			{
				String aToken = token.ToLower();
				allowed = true;

				foreach (String stopWord in Config.StopWordsArray)
				{
					String aStopWord = stopWord.ToLower();

					if (aToken == aStopWord)
					{
						allowed = false;
						break;
					}
				}
				if (allowed)
				{
					if (!String.IsNullOrEmpty(output))
						output += " ";

					output += aToken;
				}
			}
			return output;
		}

		private String TrimString(String Input)
	{
			Input = Regex.Replace(Input, "\\s+", " ");
			Input = Input.Trim();
			return Input;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Converts the DataSource to a .bow file.
		/// </summary>
		/// <returns>A path to the created .bow file</returns>
		public String Run()
		{
			EnsureNotDisposed();
			EnsureParametersExist();
			EnsureTempDirExists();
			Content2Txt();
			Txt2Bow();
			return this.ResultPath;
		}

		#endregion

		#region IDisposable Members

		private void EnsureNotDisposed()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException(this.ToString());
			}
		}

		protected void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				try
				{
					if (disposing)
					{
						// Clean Up Managed Resources Here;
					}

					// Clean Up Unmanaged Resources Here;
					if (_manageTempDir && Directory.Exists(_tempDir))
						Directory.Delete(_tempDir, true);

					this._disposed = true;
				}
				finally
				{
					GC.SuppressFinalize(this);
				}
			}
		}

		public void Dispose()
		{
			this.Dispose(true);
		}

		~BagOfWords()
		{
			this.Dispose(false);
		}

		#endregion

	}
}
