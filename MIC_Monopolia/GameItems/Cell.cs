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
			}
		}
		
		public Cell(int cellIndex) {
			Index = cellIndex;
		
			this.Dock = DockStyle.Fill;
			this.BackColor = Color.White;
		}
	}
}