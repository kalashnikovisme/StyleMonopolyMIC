using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GameItems {
	public class Dice : PictureBox {
		private int number = 0;
		public int Number {
			get {
				if (number == 0) {
					throw new Exception("Dice wasn't rolled");
				}
				return number;
			}
			set {
				if ((value < 1) || (value > 6)) {
					throw new Exception("Dice will be assigned an incorrect value");
				}
				number = value;
			}
		}		
	}
}