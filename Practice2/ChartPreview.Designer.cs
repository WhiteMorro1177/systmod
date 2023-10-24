namespace Practice2
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
			this.main_histogram = new System.Windows.Forms.DataVisualization.Charting.Chart();
			((System.ComponentModel.ISupportInitialize)(this.main_histogram)).BeginInit();
			this.SuspendLayout();
			// 
			// main_histogram
			// 
			chartArea1.Name = "ChartArea1";
			this.main_histogram.ChartAreas.Add(chartArea1);
			legend1.Name = "Legend1";
			this.main_histogram.Legends.Add(legend1);
			this.main_histogram.Location = new System.Drawing.Point(12, 12);
			this.main_histogram.Name = "main_histogram";
			series1.ChartArea = "ChartArea1";
			series1.Legend = "Legend1";
			series1.Name = "Series1";
			this.main_histogram.Series.Add(series1);
			this.main_histogram.Size = new System.Drawing.Size(795, 545);
			this.main_histogram.TabIndex = 0;
			this.main_histogram.Text = "chart1";
			// 
			// ChartPreview
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(819, 569);
			this.Controls.Add(this.main_histogram);
			this.Name = "ChartPreview";
			this.Text = "Chart";
			((System.ComponentModel.ISupportInitialize)(this.main_histogram)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataVisualization.Charting.Chart main_histogram;
	}
}