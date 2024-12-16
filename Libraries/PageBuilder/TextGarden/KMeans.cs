using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Data;
using System.IO;
using System.Collections.Specialized;
using System.Collections;
using System.Xml;

using CategorizerLib.Common;

namespace CategorizerLib.TextGarden
{
	public class KMeans : IDisposable
	{
		#region Global Declarations

		private XmlDocument _result;
		private String _tempDir;
		private Boolean _manageTempDir = false;
		private KMeansConfig _config;
		private String _id;
		private Boolean _disposed = false;

		#endregion

		#region Properties

		public XmlDocument XmlResult
		{
			get
			{
				EnsureNotDisposed();
				return _result;
			}
		}

		public KMeansConfig KMeansConfig
		{
			get { return _config; }
			set { _config = value; }
		}

		#endregion

		#region Constructors

		private KMeans()
		{
			_id = Guid.NewGuid().ToString();
		}

		public KMeans(KMeansConfig Config)
			: this()
		{
			_config = Config;
		}

		public KMeans(KMeansConfig Config, String TempDir)
			: this(Config)
		{
			_tempDir = TempDir;
		}

		#endregion

		#region Private Methods

		private void Bow2KMeans()
		{

			/*
 * Bag-Of-Words K-Means [Sep/2/2004] [Sep 29 2004]
 * ===============================================
 * usage: BowKMeans
 * -i:Input-Bow-File (default:'')
 * -op:Output-BowPartition-File (default:'KMeans.BowPart')
 * -ot:Output-Txt-File (default:'KMeans.Txt')
 * -ox:Output-Xml-File (default:'KMeans.Xml')
 * -docs:Documents (default:-1)
 * -clusts:Clusters (default:10)
 * -rseed:RNG-Seed (default:1)
 * -ctrials:Clustering-Trials (default:1)
 * -ceps:Convergence-Epsilon (default:10)
 * -cutww:Cut-Word-Weight-Sum-Percentage (default:0.5)
 * -mnwfq:Minimal-Word-Frequency (default:5)
 * -sdnm:Save-Document-Names (default:'F')
 * ================================= 
*/
			ProcessStartInfo bowKMeans = new ProcessStartInfo();
			String file = Path.Combine(Config.ExecutablePath, @"BowKMeans.exe");
			bowKMeans.FileName = file;
			bowKMeans.WorkingDirectory = _tempDir;

			// FIXME:  This path will not work if the BOW temp dir is not the same as KMeans.

			bowKMeans.Arguments += "-i:" + Path.GetFileName(_config.BagofWords.ResultPath);
			bowKMeans.Arguments += " -ox:KMeansResult" + _id + ".xml";
			bowKMeans.Arguments += " -clusts:" + _config.Clusters.ToString();
			bowKMeans.Arguments += " -rseed:" + _config.RSeed.ToString();
			bowKMeans.Arguments += " -ctrials:" + _config.ClusteringTrials.ToString();
			bowKMeans.Arguments += " -ceps:" + _config.ConvergenceEpsilon.ToString();
			bowKMeans.Arguments += " -cutww:" + _config.CutWordWeightSumPercentage.ToString();
			bowKMeans.Arguments += " -mnwfq:" + _config.MinimumWordFreq.ToString();

			// bowKMeans.WindowStyle = ProcessWindowStyle.Hidden;
			// bowKMeans.CreateNoWindow = true;

			Process myProcess = Process.Start(bowKMeans);
			myProcess.WaitForExit();

			_result = new XmlDocument();
			_result.Load(Path.Combine(_tempDir, "KMeansResult" + _id + ".xml"));
		}	

		#endregion

		#region Public Methods

		public XmlDocument Run()
		{
			EnsureNotDisposed();
			EnsureParametersExist();
			EnsureTempDirExists();
			Bow2KMeans();
			return _result;
		}

		private void EnsureParametersExist()
		{
			if (_config == null)
			{
				throw new ArgumentNullException("Config");
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

		~KMeans()
		{
			this.Dispose(false);
		}

		#endregion
	}
}
