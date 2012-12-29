using System;
using System.Collections.Generic;
using System.Linq;

namespace UsefulClasses {
	abstract public class RealObject {
		private string name = "";
		public string Name {
			get {
				return name;
			}
			set {
				name = value;
			}
		}

		private string info = "";
		public string Info {
			get {
				return info;
			}
			set {
				info = value;
			}
		}

		private System.Drawing.Image image = null;
		public System.Drawing.Image Image {
			get {
				return image;
			}
			set {
				image = value;
			}
		}		
	}
}