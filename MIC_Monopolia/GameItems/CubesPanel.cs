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
			this.ColumnStyles.Insert(0, new ColumnStyle(SizeType.Percent, 30));
			this.ColumnStyles.Insert(1, new ColumnStyle(SizeType.Percent, 40));
			this.ColumnStyles.Insert(2, new ColumnStyle(SizeType.Percent, 30));
			for (int i = 0; i < cubes.Length; i++) {
				cubes[i] = new PictureBox() {
					BackgroundImage = Image.FromFile(@"1.jpg"),
					Dock = DockStyle.Fill,
					BackgroundImageLayout = ImageLayout.Zoom
				};
			}
			this.RowCount = 1;
			this.ColumnCount = 3;
			this.Controls.Add(cubes[0], 0, 0);
			this.Controls.Add(cubes[1], 2, 0);
			
			AppButton button = new AppButton() {
				Text = "Бросить кубики",
				Font = new Font("PF Beausans Pro Light", 15F)
			};
			this.Controls.Add(button, 1, 0);
			this.AutoSize = true;
		}
	}
}