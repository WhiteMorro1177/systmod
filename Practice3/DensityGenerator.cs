using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Practice3
{
	internal class DensityGenerator
	{
		private bool isInit = false;

		private double[][] densities_config_pairs;
		private double[] densities;

		private static Random random;


		public DensityGenerator() { }
		public DensityGenerator(double[][] densities_config_pairs)
		{
			this.densities_config_pairs = densities_config_pairs;
			random = new Random();
			isInit = true;
		}

		private double DensityFormula(int x, double m, double sigma) => Math.Pow(Math.E, (-Math.Pow(x - m, 2)) / (2 * sigma * sigma)) / (sigma * Math.Sqrt(2 * Math.PI));
		// private double ReverseDensityFormula(int density, double m, double sigma) => m + sigma * Math.Sqrt(-2 * Math.Log(1 - density) * Math.Cos(2 * Math.PI * density));

		// refactor for input data
		public void CreateCharts()
		{
			if (isInit)
			{
				foreach (var pair in densities_config_pairs)
				{
					densities = GetDensities(pair[0], pair[1]);
					var density_chart = new ChartPreview($"Chart for M = {pair[0]}, Sigma = {pair[1]}", densities);
					density_chart.ShowDialog();
					density_chart.Save();
				}
			}
			else
			{
				Console.WriteLine("Initialize the Generator!");
			}
		}

		public void RestoreValues()
		{
			if (isInit)
			{
				var values = new List<double>();
				for (int i = 0; i < 1000; i++)
				{
					double a = DensityFormula(random.Next(0, 20), 10, 2);
					double b = Find(a, densities);
					values.Add(b);
				}

				var density_chart = new ChartPreview($"Values chart for M = 10, Sigma = 2", values.ToArray());
				density_chart.ShowDialog();
				density_chart.Save();
			}
			else
			{
				Console.WriteLine("Initialize the Generator!");
			}
		}

		public static double Find(double a, double[] arr)
		{
			//Выполняем бинарный поиск.
			int index = Array.BinarySearch(arr, a);
			index = (index < 0) ? ~index : index;
			return (double)(arr.Length - index - 1) / (arr.Length - 1);
		}

		// TODO: check this
		private double[] GetRandomByReverseFormula(double m, double sigma, int amount)
		{
			var densities = new List<double>();
			Random rnd = new Random();
			for (int i = 0; i < 20; i++)
			{
				densities.Add(rnd.Next(20));
			}

			return densities.ToArray();
		}

		private double[] GetDensities(double m, double sigma)
		{
			var densities = new List<double>();
			for (int i = 0; i < 20; i++)
			{
				densities.Add(DensityFormula(i, m, sigma));
			}

			return densities.ToArray();
		}

		private double[] GetMeanFrequencies(double[] sequence, double left_border, double right_border, int interval_count)
		{
			double[] tmp_frequencies = new double[interval_count];
			for (int i = 0; i < interval_count; i++) tmp_frequencies[i] = 0;

			double d_y = (right_border - left_border) / interval_count;

			for (int i = 0; i < sequence.Length; i++)
			{
				double y_c = sequence[i];
				int fn = (int)Math.Floor(y_c / d_y);
				fn = (fn >= interval_count) ? 9 : fn;
				fn = (fn <= 0) ? 0 : fn;
				tmp_frequencies[fn] += 1;
			}

			for (int i = 0; i < interval_count; i++)
			{
				tmp_frequencies[i] /= (sequence.Length * d_y);
			}

			return tmp_frequencies;
		}

		public override string ToString()
		{
			return "Yet not implemented";
		}
	}
}
