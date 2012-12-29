using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace UsefulControls {
	/// <summary>
	/// Автор: Антон Белов (с) 2012
	/// Редактировал: Павел Калашников. Последняя редакция 30.12.2012
	/// </summary>
	public class ImprovedLabel : Control {
		public System.Windows.Forms.Label Label;
		public System.Windows.Forms.TextBox Textbox;

		/// <summary>
		/// Added 09/08/2012
		/// </summary>
		public enum OBJ { Label, Textbox };
		private OBJ control = OBJ.Label;
		public OBJ Control {
			get {
				return control;
			}
			set {
				control = value;
				if (control == OBJ.Label) {
					ToLabel();
				}
				if (control == OBJ.Textbox) {
					ToTextbox();
				}
			}
		}

		public ImprovedLabel() {
			Label = new System.Windows.Forms.Label();
			Textbox = new System.Windows.Forms.TextBox();

			Label.AutoEllipsis = true;
			Label.AutoSize = true;
			Label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			Label.MouseClick += new System.Windows.Forms.MouseEventHandler(lab_MouseClick);
			// 
			// Textbox
			// 
			Textbox.Visible = false;
			Textbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(box_KeyPress);

			Label.SizeChanged += new EventHandler(lab_SizeChanged);
		}

		void lab_SizeChanged(object sender, EventArgs e) {
			Textbox.Size = Label.Size;
		}

		public ImprovedLabel(int x, int y) {
			Label = new System.Windows.Forms.Label();
			Textbox = new System.Windows.Forms.TextBox();
			// 
			// Label
			// 
			Label.AutoEllipsis = true;
			Label.AutoSize = true;
			Label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			Label.Location = new System.Drawing.Point(x, y);
			Label.MinimumSize = new System.Drawing.Size(10, 10);
			Label.Name = "linkLabel1";
			Label.Size = new System.Drawing.Size(57, 15);
			Label.TabIndex = 0;
			Label.TabStop = true;
			Label.Text = "default text";
			Label.MouseClick += new System.Windows.Forms.MouseEventHandler(lab_MouseClick);
			// 
			// Textbox
			// 
			Textbox.Location = new System.Drawing.Point(x, y);
			Textbox.Name = "NameTextBox";
			Textbox.Size = new System.Drawing.Size(118, 20);
			Textbox.TabIndex = 1;
			Textbox.Text = "default text";
			Textbox.Visible = false;
			Textbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(box_KeyPress);
		}

		/// <summary>
		/// Changed 09/08/2012
		/// </summary>
		private void ToLabel() {
			Label.Visible = false;
			Textbox.Visible = true;
			Label.Enabled = false;
			Textbox.Enabled = true;
			Textbox.Focus();
		}

		/// <summary>
		/// Changed 09/08/2012
		/// </summary>
		private void lab_MouseClick(object sender, MouseEventArgs e) {
			ToLabel();
		}

		/// <summary>
		/// Changed 09/08/2012
		/// </summary>
		private void ToTextbox() {
			Label.Text = Textbox.Text;
			Textbox.Visible = false;
			Label.Visible = true;
			Textbox.Enabled = false;
			Label.Enabled = true;
		}

		/// <summary>
		/// Changed 09/08/2012
		/// </summary>
		private void box_KeyPress(object sender, KeyPressEventArgs e) {
			if (e.KeyChar == '\r') {
				ToTextbox();
			}
		}

		/// <summary>
		/// Переменная, отвечающая за доступность элемента
		/// </summary>
		private bool enabledValue = true;

		/// <summary>
		/// Свойство, доступность элемента на форме. Аналог.
		/// </summary>
		public bool Enabled {
			get {
				return enabledValue;
			}
			set {
				enabledValue = value;
				Label.Enabled = value;
				Textbox.Enabled = value;
			}
		}

		/// <summary>
		/// Переменная, отвечающая за расположение
		/// </summary>
		private Point locationValue = new Point(0, 0);

		/// <summary>
		/// Свойство, отвечающее за расположение. Аналог.
		/// </summary>
		public Point Location {
			get {
				return locationValue;
			}
			set {
				locationValue = value;
				Label.Location = value;
				Textbox.Location = value;
			}
		}

		/// <summary>
		/// Переменная, отвечающая за размер
		/// </summary>
		private Size sizeValue = new Size(0, 0);

		/// <summary>
		/// Свойство, отвечающее за размер. Аналог.
		/// </summary>
		public Size Size {
			get {
				return sizeValue;
			}
			set {
				sizeValue = value;
				Label.Size = value;
				Textbox.Size = value;
			}
		}

		private string textValue = "";

		public string Text {
			get {
				return textValue;
			}
			set {
				textValue = value;
				Label.Text = value;
				Textbox.Text = value;
			}
		}

		private Font fontValue = new System.Drawing.Font("PF BeauSans Pro SemiBold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));

		public Font Font {
			get {
				return fontValue;
			}
			set {
				fontValue = value;
				Label.Font = value;
				Textbox.Font = value;
			}
		}

		private Color backColorValue = Color.Empty;

		public Color BackColor {
			get {
				return backColorValue;
			}
			set {
				backColorValue = value;
				Label.BackColor = value;
			}
		}

		public bool Visible {
			get {
				return Label.Visible;
			}
			set {
				Label.Visible = value;
				Textbox.Visible = value;
			}
		}

		public bool AutoSize {
			get {
				return Label.AutoSize;
			}
			set {
				Label.AutoSize = value;
			}
		}

		/// <summary>
		/// Added 08/08/2012
		/// </summary>
		public Size MaximumSize {
			get {
				return Label.MaximumSize;
			}
			set {
				Label.MaximumSize = value;
				Textbox.MaximumSize = value;
			}
		}

		/// <summary>
		/// Added 08/08/2012
		/// </summary>
		public BorderStyle BorderStyle {
			get {
				return Label.BorderStyle;
			}
			set {
				Label.BorderStyle = value;
			}
		}

		/// <summary>
		/// Added 08/08/2012
		/// </summary>
		public bool Multiline {
			get {
				return Textbox.Multiline;
			}
			set {
				Textbox.Multiline = value;
			}
		}

		/// <summary>
		/// Added 14/08/2012
		/// </summary>
		/// <param name="newEvent"></param>
		public void SaveText(EventHandler newEvent) {
			Textbox.TextChanged += newEvent;
		}

		/// <summary>
		/// Added 30/12/2012
		/// </summary>
		public DockStyle Dock {
			get {
				return Label.Dock;
			}
			set {
				Label.Dock = value;
				Textbox.Dock = value;
			}
		}
	}
}