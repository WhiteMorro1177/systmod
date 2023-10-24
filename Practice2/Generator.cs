using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.DataVisualization;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;


namespace Practice2
{
	internal class Generator
	{
		// configuration values
		readonly int a = 22695477, b = 1;
		readonly double m = 2 ^ 32;

		// start value
		public int X0 { get; private set; }

		// interval borders
		readonly int A = 0, B = 10;

		// storage
		public readonly List<double[]> generated_sequences;
		private readonly List<double> sequences_M_deflections;
		private readonly List<double> sequences_D_deflections;
		private readonly List<double[]> sequences_periods;

		private List<double[]> frequencies;
		private readonly List<double> pirson_criterias;

		// statistics
		private readonly double M, D;

		// default constructor
		public Generator(int start_value = 1)
		{
			X0 = start_value;

			generated_sequences = new List<double[]>();
			sequences_M_deflections = new List<double>();
			sequences_D_deflections = new List<double>();
			sequences_periods = new List<double[]>();

			frequencies = new List<double[]>();
			pirson_criterias = new List<double>();

			M = (A + B) / 2;
			D = Math.Pow((B - A), 2) / 12;
		}


		/// <summary>
		/// Function calculate one item based on some start value
		/// </summary>
		/// <param name="previous">Value that used in new value generation formula</param>
		/// <returns>Double value of calculated item</returns>
		private double Generate(double previous) => ((a * previous) + b) % m;

		/// <summary>
		/// Function generate sequence based on recurrent formula
		/// </summary>
		/// <param name="length">Iterations count</param>
		public void GenerateRandomSequence(int length, bool useRandomInstance = false)
		{
			// generate an array
			var random_array = new double[length];
			if (!useRandomInstance)
			{
				random_array[0] = Generate(X0);
				for (int i = 1; i < random_array.Length; i++)
					random_array[i] = A + ((B - A) * Generate(random_array[i - 1])) / m;

			}
			else
			{
				var r = new Random();
				for (int i = 0; i < length; i++)
					random_array[i] = r.Next(A, B + 1);
			}
			
			generated_sequences.Add(random_array);
			Console.WriteLine($"Calculations for sequence[{length}] ended");

			// calculate deflections
			CalculateDeflections(random_array);
			RandomSequencePeriod(random_array);

		}


		/// <summary>
		/// Calculate deflections of random sequence
		/// </summary>
		/// <param name="target">Sequence from random generator</param>
		private void CalculateDeflections(double[] target)
		{
			int length = target.Length;

			double mean_M = (target.Sum()) / length;
			double M_deflection = Math.Abs((M - mean_M) / M) * 100;
			sequences_M_deflections.Add(M_deflection);

			double mean_D = ((target.Sum(x => Math.Pow(x, 2)) / length) - Math.Pow(mean_M, 2)) * (length / (length - 1));
			double D_deflection = Math.Abs((D - mean_D) / D) * 100;
			sequences_D_deflections.Add(D_deflection);
		}

		/// <summary>
		/// Calculate period for random sequence
		/// </summary>
		/// <param name="sequence">Double array of generated sequence</param>
		/// <returns>Double array with length 3</returns>
		private void RandomSequencePeriod(double[] sequence)
		{
			var result = new double[] { -1, -1, -1 };
			int N = sequence.Length;

			for (int i = 0; i < N; i++)
			{
				double element = sequence[i];
				for (int j = i; j < N; j++)
				{
					if (element == sequence[j])
					{
						result[0] = j - i;
						result[1] = i;
						result[2] = j;
						sequences_periods.Add(result);
						return;
					}
				}
			}

			sequences_periods.Add(result);
		}

		/// <summary>
		/// Calculate mean frequencies for sequence
		/// </summary>
		/// <param name="sequence">Random generated sequence</param>
		/// <param name="left_border"></param>
		/// <param name="right_border"></param>
		/// <param name="interval_count"></param>
		/// <returns></returns>
		private void GetMeanFrequencies(double[] sequence, double left_border, double right_border, int interval_count)
		{
			double[] tmp_frequencies = new double[interval_count];
			for (int i = 0; i < interval_count; i++) tmp_frequencies[i] = 0;

			double d_y = (right_border - left_border) / interval_count;

			for (int i = 0; i < sequence.Length; i++)
			{
				double y_c = sequence[i];
				int fn = (int)Math.Floor(y_c / d_y);
				fn = (fn == 10) ? fn - 1 : fn;
				tmp_frequencies[fn] += 1;
			}

			for (int i = 0; i < interval_count; i++)
			{
				tmp_frequencies[i] /= (sequence.Length * d_y);
			}

			frequencies.Add(tmp_frequencies);
		}

		public void CreateHistogram(string name, double[] sequence, int index)
		{
			GetMeanFrequencies(sequence, sequence.Min(), sequence.Max(), 10);
			CalculatePirsonCriteria(sequence);
			ChartPreview histogram_preview = new ChartPreview(name, frequencies[index]);
			histogram_preview.ShowDialog();
			histogram_preview.Save();
		}

		/// <summary>
		/// Calculate Pirson criteria for each sequence
		/// </summary>
		/// <param name="sequence">generated sequence</param>
		private void CalculatePirsonCriteria(double[] sequence)
		{
			int interval_count = sequence.Length;

			double criteria = 0;
			for (int i = 0; i < interval_count; i++)
			{
				double criteria_iteration = Math.Pow(1 / (interval_count - sequence[i]), 2) / sequence[i];
				criteria += criteria_iteration;
			}
			pirson_criterias.Add(Math.Sqrt(criteria));
		}

		/// <summary>
		/// Clear all generator data
		/// </summary>
		public void Clear()
		{
			generated_sequences.Clear();
			sequences_M_deflections.Clear();
			sequences_D_deflections.Clear();
			sequences_periods.Clear();
			frequencies.Clear();
			pirson_criterias.Clear();
		}

		/// <summary>
		/// Function print statistics for current generator | work with double values
		/// </summary>
		public void PrintStatistics()
		{
			/*
			Console.WriteLine("History of calculations:\n[");
			generated_sequences
				.ForEach(inner_array =>
				{
					Console.Write("\t[ ");
					inner_array.ToList().ForEach(item => Console.Write(item + ", "));
					Console.WriteLine("]");
				});
			*/

			Console.WriteLine("Deflections:\n");
			Console.Write("Sequence_M_deflection: [ ");
			sequences_M_deflections.ForEach(item => Console.Write(item + ", "));
			Console.WriteLine("]");

			Console.Write("Sequence_D_deflection: [ ");
			sequences_D_deflections.ForEach(item => Console.Write(item + ", "));
			Console.WriteLine("]");

			Console.WriteLine("Frequencies:\n[");
			frequencies
				.ForEach(inner_array =>
				{
					Console.Write("\t[ ");
					inner_array.ToList().ForEach(item => Console.Write(Math.Round(item, 4) + ", "));
					Console.WriteLine("]");
				});

			Console.Write("Pirson criterias:\n[");
			pirson_criterias.ForEach(item => Console.Write(item + ", "));
			Console.WriteLine("]");
		}
	}
}
