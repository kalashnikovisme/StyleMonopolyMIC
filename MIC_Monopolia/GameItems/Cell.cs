using System;
using System.Windows.Forms;
using System.Drawing;

namespace GameItems {
	public class Cell : Panel {
		private const int ERROR_INT = -1;
		
		/// <summary>
		/// Cell Index in the Game
		/// </summary>
		private int index = ERROR_INT;
		public int Index {
			get {
				if (index == ERROR_INT) {
					throw new Exception("Index of PlayCell object is not initialized");
				}
				return index;
			}
			set {
				index = value;
				label.Text = index.ToString();
			}
		}
		
		private Label label;
		
		public Cell(int cellIndex) {
			label = new Label() {
				Location = new Point(0, 0),
				Size = new Size(15, 15),
				BackColor = Color.FromArgb(0)
			};
			//this.Controls.Add(label);
			
			Index = cellIndex;
			
			this.Dock = DockStyle.Fill;
			this.BackColor = Color.White;
		}
	}
}