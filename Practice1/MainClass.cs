using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practice1
{
	internal class MainClass
	{
		static void Main(string[] args)
		{
			bool is_exit = false;
			while (!is_exit)
			{
				Console.Write("\n1 - count 'pi'\n2 - count integral\n0 - exit\nEnter: ");
				int user_choose;
				while (!int.TryParse(Console.ReadLine(), out user_choose))
					Console.Write("\nIncorrect value! Again: ");

				switch (user_choose)
				{
					case 0: is_exit = true; break;
					case 1: CallPiCalculations(); break;
					case 2: CallIntegralCalculations(); break;
					default: break;
				}
			}
		}

		private static void CallPiCalculations()
		{
			// parse input values
			Console.Write("x0 y0 r: ");
			var input = new List<int>();
			Console.ReadLine().Split(' ').ToList().ForEach(item => input.Add(int.Parse(item)));

			var start_time = DateTime.Now;

			// count "pi" for 10^4
			double pi = Counter.CountPi(input[0], input[1], input[2], 4);
			Console.WriteLine($"'pi' with power '4' = {pi}");

			var series = new List<List<double>>();
			var tasks = new List<Task>();

			for (int i = 1; i <= 5; i++)
			{
				var task = Task.Factory.StartNew(() =>
				{
					var result = Counter.CountOneSeries(input[0], input[1], input[2]);
					series.Add(result);
				});
				tasks.Add(task);
			}

			// wait for task completing
			Task.WaitAll(tasks.ToArray());
			Print("Series", series);

			Console.WriteLine($"Calculations was complete in {(DateTime.Now - start_time).TotalSeconds} seconds");

			// calculate eps for series
			var eps = new List<List<double>>();
			Counter.CalculateEps(series, eps, Math.PI);
			Print("Eps", eps);


			// calculate eps_s for series[i]
			var eps_s = new List<double>();
			Counter.CalculateEps(series, eps_s, Math.PI);
			Print("Eps_s", eps_s);
		}
		private static void CallIntegralCalculations()
		{
			// parse input values
			Console.Write("a b: ");
			var input = new List<int>();
			Console.ReadLine().Split(' ').ToList().ForEach(item => input.Add(int.Parse(item)));

			var start_time = DateTime.Now;

			// count integral for 10^4
			double integral_value = Counter.CountIntegral(input[0], input[1], 4);
			Console.WriteLine($"integral value for 10^4 = {integral_value}");

			var series = new List<List<double>>();
			var tasks = new List<Task>();

			for (int i = 1; i <= 3; i++)
			{
				var task = Task.Factory.StartNew(() =>
				{
					List<double> result = Counter.CountOneSeries(input[0], input[1]);
					series.Add(result);
				});
				tasks.Add(task);
			}

			// wait for task completing
			Task.WaitAll(tasks.ToArray());
			Print("Series", series);

			Console.WriteLine($"Calculations was complete in {(DateTime.Now - start_time).TotalSeconds} seconds");

			// calculate eps for series
			var eps = new List<List<double>>();
			Counter.CalculateEps(series, eps, Counter.IntegralFunction(input[0], input[1]));
			Print("Eps", eps);


			// calculate eps_s for series[i]
			var eps_s = new List<double>();
			Counter.CalculateEps(series, eps_s, Counter.IntegralFunction(input[0], input[1]));
			Print("Eps_s", eps_s);
		}


		static void Print(string title, List<List<double>> to_print)
		{
			Console.WriteLine(title + ":\n{");
			to_print.ForEach(inner_list =>
			{
				Console.Write("\t[");
				inner_list.ForEach(item => Console.Write(item.ToString() + ", "));
				Console.WriteLine("],");
			});
			Console.WriteLine("}");
		}
		static void Print(string title, List<double> to_print)
		{
			Console.Write(title + ": [");
			to_print.ForEach(item => Console.Write(item + ", "));
			Console.WriteLine("]");
		}
	}
}
