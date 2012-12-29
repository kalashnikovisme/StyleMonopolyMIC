using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UsefulClasses;

namespace MIC_Monopolia {
	public class Player : RealObject {
		private int points = 0;
		public int Points {
			get {
				return points;
			}
			set {
				points = value;
			}
		}
		
		public Player(string playerName) {
			this.Name = playerName;
		}
	}
}