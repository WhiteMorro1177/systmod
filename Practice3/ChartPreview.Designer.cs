namespace Practice3
{
	partial class ChartPreview
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
			this.main_chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
			((System.ComponentModel.ISupportInitialize)(this.main_chart)).BeginInit();
			this.SuspendLayout();
			// 
			// main_chart
			// 
			chartArea1.Name = "ChartArea1";
			this.main_chart.ChartAreas.Add(chartArea1);
			legend1.Name = "Legend1";
			this.main_chart.Legends.Add(legend1);
			this.main_chart.Location = new System.Drawing.Point(12, 12);
			this.main_chart.Name = "main_chart";
			this.main_chart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Berry;
			series1.BorderWidth = 3;
			series1.ChartArea = "ChartArea1";
			series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
			series1.Legend = "Legend1";
			series1.Name = "Series1";
			series1.YValuesPerPoint = 2;
			this.main_chart.Series.Add(series1);
			this.main_chart.Size = new System.Drawing.Size(555, 426);
			this.main_chart.TabIndex = 0;
			this.main_chart.Text = "chart1";
			// 
			// ChartPreview
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(579, 450);
			this.Controls.Add(this.main_chart);
			this.Name = "ChartPreview";
			this.Text = "ChartPreview";
			((System.ComponentModel.ISupportInitialize)(this.main_chart)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataVisualization.Charting.Chart main_chart;
	}
}