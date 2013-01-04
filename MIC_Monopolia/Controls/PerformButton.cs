using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UsefulControls;

namespace Controls {
	public class PerformButton : AppButton {
		private const int ERROR = -1;
		private int index = -1;
		public int PlayerIndex {
			get {
				if (index == ERROR) {
					throw new Exception("PlayerIndex wasn't established");
				}
				return index;
			}
			set {
				index = value;
			}
		}
	}
}