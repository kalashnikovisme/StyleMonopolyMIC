using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using UsefulControls;

namespace GameItems {
	public class CubesPanel : TableLayoutPanel {
		private PictureBox[] cubes = new PictureBox[2];
		
		public CubesPanel() {
			this.Dock = DockStyle.Fill;
			this.ColumnStyles.Insert(0, new ColumnStyle(SizeType.Percent, 50));
			this.ColumnStyles.Insert(1, new ColumnStyle(SizeType.Percent, 50));
			for (int i = 0; i < cubes.Length; i++) {
				cubes[i] = new PictureBox() {
					Image = Image.FromFile(@"1.jpg"),
					Dock = DockStyle.Fill,
					BackgroundImageLayout = ImageLayout.Zoom
				};
			}
			this.RowCount = 2;
			this.ColumnCount = 2;
			for (int i = 0; i < cubes.Length; i++) {
				this.Controls.Add(cubes[i]);
			}
			AppButton button = new AppButton() {
				Text = "Бросить кубики"
			};
			this.Controls.Add(button);
			this.SetColumnSpan(button, 2);
			this.AutoSize = true;
		}
	}
}