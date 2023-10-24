using System;
using System.Collections.Generic;
using System.Threading;

namespace Practice1
{
	internal class Counter
	{
		/// <summary>
		/// Вычисление одного значения числа "Пи" с помощью рандомайзера
		/// </summary>
		/// <param name="x_center">Координата X центра окружности</param>
		/// <param name="y_center">Координата Y центра окружности</param>
		/// <param name="radius">Радиус окружности</param>
		/// <param name="experiments_count_power">Степень количества экспериментов (10^param)</param>
		/// <returns>Возвращает значение числа "Пи"</returns>
		public static double CountPi(int x_center, int y_center, int radius, int experiments_count_power)
		{
			double all_calculations_count = Math.Pow(10.0, experiments_count_power);
			Random random = new Random();

			int[] x = new int[] { x_center - radius, x_center + radius };
			int[] y = new int[] { y_center - radius, y_center + radius };

			int positive_calculations_count = 0;
			for (int i = 0; i < all_calculations_count; i++)
			{
				// generate x
				double point = random.NextDouble();
				double x_point = ((x[1] - x[0]) * point) + x[0];
				// generate y
				point = random.NextDouble();
				double y_point = ((y[1] - y[0]) * point) + y[0];

				// check borders
				if ((Math.Pow(x_point - x_center, 2) + Math.Pow(y_point - y_center, 2)) < Math.Pow(radius, 2))
					positive_calculations_count++;
			}

			return 4 * (positive_calculations_count / all_calculations_count);
		}

		public static double CountIntegral(int a, int b, int experiments_number_count)
		{
			Random random = new Random();
			int x_min = a, x_max = b;
			double y_min = 0, y_max = TargetIntegralFunction(b);
			double exp_count = Math.Pow(10, experiments_number_count);
			int m = 0;

			for (int i = 0; i < exp_count; i++)
			{
				double rnd_point = random.NextDouble();
				double x = (x_max - x_min) * rnd_point + x_min;
				rnd_point = random.NextDouble();
				double y = (y_max - y_min) * rnd_point + y_min;

				if (TargetIntegralFunction(x) > y) m++;
			}

			double S = (m / exp_count) * (b - a) * TargetIntegralFunction(b);
			return S;
		}
		private static double TargetIntegralFunction(double x) => Math.Pow(x, 3) + 1;

		public static double IntegralFunction(double a, double b)
		{
			double S = 0, N = 100;

			double h = (b - a) / N;
			double x = a + h;

			while (x < b)
			{
				S += 4 * TargetIntegralFunction(x);
				x += h;

				if (x >= b) break;

				S += 2 * TargetIntegralFunction(x);
				x += h;
			}

			S = (h / 3) * (S + TargetIntegralFunction(a) + TargetIntegralFunction(b));

			return S;
		}

		/// <summary>
		/// Получение одной серии вычислений числа пи
		/// </summary>
		/// <param name="x_center">X-координата центра окружности</param>
		/// <param name="y_center">Y-координата центра окружности</param>
		/// <param name="radius">Радиус окружности вокруг точки (X, Y)</param>
		/// <returns>Возвращает список длинной 5 чисел с плавающей точкой - результатов расчётов числа пи</returns>
		public static List<double> CountOneSeries(int x_center, int y_center, int radius)
		{
			List<double> tmp = new List<double>() { 0, 0, 0, 0, 0 };

			for (int i = 8; i >= 4; i--) Count(i);

			void Count(int iterator)
			{
				new Thread(() =>
				{
					Thread.CurrentThread.Name = iterator.ToString();
					double another_pi = CountPi(x_center, y_center, radius, int.Parse(Thread.CurrentThread.Name));
					tmp[int.Parse(Thread.CurrentThread.Name) - 4] = another_pi;
				}).Start();
			}

			while (tmp.Contains(0)) Thread.Sleep(2000);
			return tmp;
		}

		/// <summary>
		/// Получение одной серии вычислений для интеграла
		/// </summary>
		/// <param name="a">Нижний предел интегрирования</param>
		/// <param name="b">Верхний предел интегрирования</param>
		/// <returns>Возвращает список длинной 4 чисел с плавающей точкой - результатов расчётов интеграла</returns>
		public static List<double> CountOneSeries(int a, int b)
		{
			List<double> tmp = new List<double>() { 0, 0, 0, 0 };

			for (int i = 7; i >= 4; i--) Count(i);

			void Count(int iterator)
			{
				new Thread(() =>
				{
					Thread.CurrentThread.Name = iterator.ToString();
					double another_pi = CountIntegral(a, b, int.Parse(Thread.CurrentThread.Name));
					tmp[int.Parse(Thread.CurrentThread.Name) - 4] = another_pi;
				}).Start();
			}

			while (tmp.Contains(0)) Thread.Sleep(2000);
			return tmp;
		}


		public static void CalculateEps(List<List<double>> series, List<List<double>> eps, double true_value)
		{
			foreach (List<double> inner_list in series)
			{
				var eps_for_one_series = new List<double>();
				foreach (double item in inner_list)
				{
					double one_eps = AvgFunc(item, true_value);
					eps_for_one_series.Add(one_eps > 0 ? one_eps : -one_eps);
				}
				eps.Add(eps_for_one_series);
			}
		}
		public static void CalculateEps(List<List<double>> series, List<double> eps_s, double true_value)
		{
			for (int i = 0; i < series[0].Count; i++)
			{
				double seria_sum = 0;
				foreach (var inner_list in series) seria_sum += inner_list[i];

				seria_sum /= series.Count;

				double one_eps_s = AvgFunc(seria_sum, true_value);
				eps_s.Add(one_eps_s > 0 ? one_eps_s : -one_eps_s);
			}
		}
		private static double AvgFunc(double parameter, double true_value) => (parameter - true_value) / true_value;
	}
}
