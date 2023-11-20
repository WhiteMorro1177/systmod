using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice3
{
	internal class MainClass
	{
		static void Main(string[] args)
		{
			// create density generator

			var generator = new DensityGenerator(
				densities_config_pairs: new double[][] { // first -> m, second -> sigma
					new double[] { 10, 2 },
					new double[] { 10, 1 },
					new double[] { 10, 0.5 },
					new double[] { 12, 1 }
				}
			);

			generator.CreateCharts();
			generator.RestoreValues();

            Console.WriteLine(generator.ToString());



        }
	}
}
