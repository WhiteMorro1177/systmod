using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Practice2
{
	internal class MainClass
	{
		static void Main(string[] args)
		{
			// initialize generator
			Generator random_sequence_generator = new Generator();

			// generate sequences
			for (int i = 2; i <= 5; i++) random_sequence_generator.GenerateRandomSequence((int)Math.Pow(10, i));

			// create histograms
			for (int i = 0; i < random_sequence_generator.generated_sequences.Count; i++)
			{
				double[] sequence = random_sequence_generator.generated_sequences[i];
				random_sequence_generator.CreateHistogram($"custom_generator_{i}", sequence, i);
			}

			random_sequence_generator.PrintStatistics();


			Console.WriteLine("\n\tCalculations for default generator\n");
			random_sequence_generator.Clear();
			// original Random instance
			// generate sequences
			for (int i = 2; i <= 5; i++) random_sequence_generator.GenerateRandomSequence((int)Math.Pow(10, i), true);

			// create histograms
			for (int i = 0; i < random_sequence_generator.generated_sequences.Count; i++)
			{
				double[] sequence = random_sequence_generator.generated_sequences[i];
				random_sequence_generator.CreateHistogram($"default_generator_{i}", sequence, i);
			}

			random_sequence_generator.PrintStatistics();
		}
	}
}
