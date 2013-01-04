using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using UsefulControls;

namespace GameItems {
	public partial class ChanceForm : Form {
		private const string CHANCE_TASK_FILE_PATH = "chance.txt";
		private AppButton[] tasksButtons;
		
		public ChanceForm() {
			InitializeComponent();
			int countTasks = File.ReadAllLines(@CHANCE_TASK_FILE_PATH).Length;
			tasksButtons = new AppButton[countTasks];
			double qur = Math.Round(Math.Sqrt((double)countTasks), 0, MidpointRounding.AwayFromZero);
			mainTableLayoutPanel.ColumnCount = (int)qur;
			mainTableLayoutPanel.RowCount = (int)qur;
			
			for (int i = 0; i < tasksButtons.Length; i++) {
				tasksButtons[i] = new AppButton() {
					Dock = DockStyle.Fill,
					Margin = new Padding(10)
				};
			}
			
			foreach (AppButton b in tasksButtons) {
				mainTableLayoutPanel.Controls.Add(b);
			}
			
			this.Show();
		}
	}
}
