using System;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Practice2
{
	public partial class ChartPreview : Form
	{
		private double[] Data;

		private string ImagePath;
		private ChartImageFormat Format;

		public ChartPreview(string name, double[] data)
		{
			Data = data;
			InitializeComponent();

			main_histogram.Series.Clear();
			main_histogram.Series.Add("frequencies");

			for (int i = 0; i < Data.Length; i++)
				main_histogram.Series["frequencies"].Points.Add(Data[i]);


			string saves_directory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\saves";

			Format = ChartImageFormat.Png;
			ImagePath = saves_directory + $"\\{name}.{Format.ToString().ToLower()}";
			
		}

		public ChartPreview()
		{
			InitializeComponent();
		}

		public void Save()
		{
			try
			{
				main_histogram.SaveImage(ImagePath, Format);
				Console.WriteLine($"Diagram saved in {ImagePath}");
			}
			catch (Exception ex) { Console.WriteLine(ex.StackTrace); }
		}
	}
}
