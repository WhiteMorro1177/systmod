using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Practice3
{
	public partial class ChartPreview : Form
	{
		private double[] Data;

		private string ImagePath;
		private ChartImageFormat Format;

		public ChartPreview(string name, double[] data)
		{
			Data = data;
			StartPosition = FormStartPosition.CenterScreen;
			InitializeComponent();

			main_chart.Series.Clear();
			main_chart.Series.Add("densities");
			main_chart.Series["densities"].ChartType = SeriesChartType.Spline;
			main_chart.Series["densities"].BorderWidth = 3;

			for (int i = 0; i < Data.Length; i++)
				main_chart.Series["densities"].Points.Add(Data[i]);


			string saves_directory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\saves";

			Format = ChartImageFormat.Png;
			ImagePath = saves_directory + $"\\{name.Replace(" ", "_")}.{Format.ToString().ToLower()}";

		}

		public ChartPreview()
		{
			InitializeComponent();
		}

		public void Save()
		{
			try
			{
				main_chart.SaveImage(ImagePath, Format);
				Console.WriteLine($"Diagram saved in {ImagePath}");
			}
			catch (Exception ex) { Console.WriteLine(ex.StackTrace); }
		}
	}
}
