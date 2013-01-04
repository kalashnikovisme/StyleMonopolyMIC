using System;
using System.Windows.Forms;
using System.Drawing;

namespace GameItems {
	public class Cell : Panel {
		private const int ERROR_INT = -1;
		
		/// <summary>
		/// Cell PlayerIndex in the Game
		/// </summary>
		private int index = ERROR_INT;
		public int Index {
			get {
				if (index == ERROR_INT) {
					throw new Exception("PlayerIndex of PlayCell object is not initialized");
				}
				return index;
			}
			set {
				index = value;
				label.Text = index.ToString();
			}
		}
		
		private string task = "";
		public string Task {
			get {
				if (task == "") {
					throw new Exception("Not given the task of the cell");
				}
				return task;
			}
			set {
				task = value;
			}
		}
		
		private Label label;
		
		public Cell() {
			label = new Label() {
				Location = new Point(0, 0),
				Size = new Size(40, 40),
				BackColor = Color.FromArgb(0)
			};
			//this.Controls.Add(label);
			
			this.Dock = DockStyle.Fill;
			this.BackColor = Color.White;
		}
	}
}