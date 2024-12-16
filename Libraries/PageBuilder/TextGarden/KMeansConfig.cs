using System;
using System.Collections.Generic;
using System.Text;

namespace CategorizerLib.TextGarden
{
	public class KMeansConfig
	{
		private int _clusters = 10;
		private int _rseed = 1;
		private int _clusteringTrials = 1;
		private int _convergenceEpsilon = 10;
		private int _minimumWordFreq = 5;
		private double _cutWordWeightSumPercentage = 0.5;
		private BagOfWords _bagOfWords;

		private KMeansConfig() { }

		public KMeansConfig(BagOfWords BagOfWords)
			: this()
		{
			_bagOfWords = BagOfWords;
		}

		public KMeansConfig(BagOfWords BagOfWords, int Clusters)
			: this(BagOfWords)
		{
			_clusters = Clusters;
		}

		public KMeansConfig(BagOfWords BagOfWords, int Clusters, int ClusteringTrials, int RSeed, int ConvergenceEplison,
			int MinimumWordFreq, double CutWordWeightSumPercentage)
			: this(BagOfWords, Clusters)
		{
			_rseed = RSeed;
			_clusteringTrials = ClusteringTrials;
			_convergenceEpsilon = ConvergenceEplison;
			_minimumWordFreq = MinimumWordFreq;
			_cutWordWeightSumPercentage = CutWordWeightSumPercentage;
		}

		public BagOfWords BagofWords
		{
			get { return _bagOfWords; }
			set { _bagOfWords = value; }
		}

		/// <summary>
		/// Determines the final number of clusters. (Default: 10)
		/// </summary>
		public int Clusters
		{
			get { return _clusters; }
			set { _clusters = value; }
		}

		/// <summary>
		/// Determines the value of random-number-generator seed, where value 0 means nondeterministic value. (Default: 1)
		/// </summary>
		public int RSeed
		{
			get { return _rseed; }
			set { _rseed = value; }
		}

		/// <summary>
		/// Determines the number of different runs/trials of K-Means algorithm in a search for the best solution. (Default: 1)
		/// </summary>
		public int ClusteringTrials
		{
			get { return _clusteringTrials; }
			set { _clusteringTrials = value; }
		}

		/// <summary>
		/// Determines convergence epsilon value which influences the stopping criterium for the K-Means algorithm. (Default: 10)
		/// </summary>
		public int ConvergenceEpsilon
		{
			get { return _convergenceEpsilon; }
			set { _convergenceEpsilon = value; }
		}

		/// <summary>
		/// Determines the minimal document-frequency of the words which are used for the document representation. (Default: 5)
		/// </summary>
		public int MinimumWordFreq
		{
			get { return _minimumWordFreq; }
			set { _minimumWordFreq = value; }
		}

		/// <summary>
		/// Determines the percentage of the sum of the weights for the best words in the centroids which appear in the textual output file. (Default: 0.5)
		/// </summary>
		public double CutWordWeightSumPercentage
		{
			get { return _cutWordWeightSumPercentage; }
			set { _cutWordWeightSumPercentage = value; }
		}
	}
}
