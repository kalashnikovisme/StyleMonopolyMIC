using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GameItems {
	public class Chip : PictureBox {
		public Chip() {
			this.Dock = DockStyle.Fill;
			this.BackgroundImageLayout = ImageLayout.Zoom;
		}
		
		public System.Drawing.Image Image {
			get {
				return BackgroundImage;
			}
			set {
				BackgroundImage = value;
			}
		}
	}
}