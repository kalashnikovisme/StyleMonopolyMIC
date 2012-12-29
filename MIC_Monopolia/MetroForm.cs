using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UsefulForms {
	public partial class MetroForm : Form {
		public MetroForm() {
			InitializeComponent();
			this.FormBorderStyle = FormBorderStyle.None;
			this.WindowState = FormWindowState.Maximized;
			this.KeyDown += new KeyEventHandler(MetroForm_KeyDown);
		}

		protected void MetroForm_KeyDown(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Escape) {
				this.Close();
			}
		}
	}
}