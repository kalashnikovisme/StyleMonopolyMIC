using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace UsefulControls {
	public class AppButton : Button {
		public AppButton() {
			this.Dock = DockStyle.Fill;
			this.SizeChanged += new EventHandler(AppButton_SizeChanged);
		}

		private void AppButton_SizeChanged(object sender, EventArgs e) {
			resizeFont();
		}

		public enum ControlIndent { None, Small, Middle, Big, VeryBig };

		private ControlIndent indent = ControlIndent.None;
		public ControlIndent Indent {
			get {
				return indent;
			}
			set {
				indent = value;
				if (indent == ControlIndent.None) {
					this.Margin = new Padding(0);
					return;
				}
				if (indent == ControlIndent.Small) {
					this.Margin = new Padding(ConstControls.CONTROL_INDENT_SMALL);
					return;
				}
				if (indent == ControlIndent.Middle) {
					this.Margin = new Padding(ConstControls.CONTROL_INDENT_MIDDLE);
					return;
				}
				if (indent == ControlIndent.Big) {
					this.Margin = new Padding(ConstControls.CONTROL_INDENT_BIG);
				}
				if (indent == ControlIndent.VeryBig) {
					this.Margin = new Padding(ConstControls.CONTROL_INDENT_VERY_BIG);
				}
			}
		}


		private void resizeFont() {
			const int minFontSize = 7;
			//определение подходящего шрифта по ширине control'a
			float neededSizeX = (this.Width - this.Font.Size * 2) / this.Text.Length;
			int tempSizeXInPoint = minFontSize;
			while (true) {
				Graphics g = this.CreateGraphics();
				Font tempFont = new Font(this.Font.FontFamily, tempSizeXInPoint);
				int tempSize = (int)g.MeasureString(this.Text, tempFont).Width / this.Text.Length;
				if (neededSizeX - tempSize < 0.5) {
					break;
				}
				tempSizeXInPoint++;
				#region forDebug
				if (tempSizeXInPoint == 300) {
					tempSizeXInPoint = 9;
					break;
				}
				#endregion
			}
			//определение подходящего шрифта по высоте control'a
			int tempSizeYInPoint = minFontSize;
			while (true) {
				Graphics g = this.CreateGraphics();
				Font tempFont = new Font(this.Font.FontFamily, tempSizeYInPoint);
				if (this.Height - g.MeasureString(this.Text, tempFont).Height < 5) {
					break;
				}
				tempSizeYInPoint++;
				#region forDebug
				if (tempSizeYInPoint == 300) {
					tempSizeYInPoint = 9;
					break;
				}
				#endregion
			}
			//выбор наиболее подходящего из двух найденых шрифтов
			int resSize;
			Font resFont;
			if (tempSizeYInPoint > tempSizeXInPoint) // выбираем минимальный размер шрифта, т.к. главное что бы отобразилось все содержимое
			{
				resSize = tempSizeXInPoint;//размер определенный по ширине подошел
				resFont = new Font(this.Font.FontFamily, resSize);
				int numberOfLines = (int)((this.Height - 10) / resFont.GetHeight());//есть ли возможность разбить содержимое на несколько строк 
				if (numberOfLines > 1) {
					int tempResSize = resSize;
					while (true) {
						Graphics g = this.CreateGraphics();
						Font tempFont = new Font(this.Font.FontFamily, tempResSize);
						numberOfLines = (int)((this.Height - 10) / tempFont.GetHeight());
						if ((this.Width - tempResSize * 2) * numberOfLines - g.MeasureString(this.Text, tempFont).Width < 100) {
							break;
						}
						tempResSize++;
						#region forDebug
						if (tempResSize == 300) {
							tempResSize = 9;
							break;
						}
						#endregion

					}
					//подходящий размер шрифта определен, теперь проверяем не слишком ли он мал
					if (tempResSize < minFontSize + 2) {
						resFont = new Font(this.Font.FontFamily, minFontSize);
					} else if (tempResSize < 25) {
						resFont = new Font(this.Font.FontFamily, tempResSize - 1);
					} else // -1,-2 расходуется на погрешности отступов от краев control'a до текста
					{
						resFont = new Font(this.Font.FontFamily, tempResSize - 2);
					}
				}

			} else {
				resSize = tempSizeYInPoint; //размер определенный по высоте подошел
				resFont = new Font(this.Font.FontFamily, resSize);
			}
			this.Font = resFont;
		}

	}
}