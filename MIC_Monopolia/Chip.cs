using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace MIC_Monopolia {
	public class Chip : PictureBox {
		private Color color;
		public Chip(Color chipColor) {
			color = chipColor;
			this.Paint += new PaintEventHandler(Chip_Paint);
			this.Dock = DockStyle.Fill;
		}

		private void Chip_Paint(object sender, PaintEventArgs e) {
			Graphics g = e.Graphics;

			Rectangle pict = new Rectangle(0, 0, this.Width, this.Height);
			//System.Drawing.Drawing2D.LinearGradientBrush empty = new System.Drawing.Drawing2D.LinearGradientBrush(pict, color, Color.Black, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
			SolidBrush brush = new SolidBrush(color);
			g.FillEllipse(brush, pict);
		}
	}
}