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
		private const int PERCENT_100 = 100;
		public int ChosenIndex = -1;
		
		public ChanceForm() {
			InitializeComponent();
			string[] tasks = File.ReadAllLines(@CHANCE_TASK_FILE_PATH, System.Text.Encoding.Default);
			tasksButtons = new AppButton[tasks.Length];
			double qur = Math.Round(Math.Sqrt((double)tasks.Length), 0, MidpointRounding.AwayFromZero);
			int sideTableCount = (int)qur;
			mainTableLayoutPanel.ColumnCount = sideTableCount;
			mainTableLayoutPanel.RowCount = sideTableCount;
			for (int i = 0; i < sideTableCount; i++) {
				mainTableLayoutPanel.ColumnStyles.Insert(i, new ColumnStyle(SizeType.Percent, PERCENT_100 / sideTableCount));
				
				mainTableLayoutPanel.RowStyles.Insert(i, new RowStyle(SizeType.Percent, PERCENT_100 / sideTableCount));
			}
			
			for (int i = 0; i < tasksButtons.Length; i++) {
				tasksButtons[i] = new AppButton() {
					Font = new Font("PF Beausans Pro Light", 12F),
					Text = (i + 1).ToString(),
					Size = new Size(50, 50),
					Dock = DockStyle.Fill,
					Margin = new Padding(10),
					Index = i
				};
				tasksButtons[i].Click += new EventHandler(ChanceForm_Click);
			}
			
			foreach (AppButton b in tasksButtons) {
				mainTableLayoutPanel.Controls.Add(b);
			}
			
			this.Show();
		}

		private void ChanceForm_Click(object sender, EventArgs e) {
			int index = ((AppButton)sender).Index;
			Random r = new Random();
			ChosenIndex = r.Next(0, tasksButtons.Length - 1);
			tasksButtons[index].Text = File.ReadAllLines(@CHANCE_TASK_FILE_PATH, System.Text.Encoding.Default)[ChosenIndex].Split('\t')[0];
			tasksButtons[index].Click -= new EventHandler(ChanceForm_Click);
			tasksButtons[index].Click += new EventHandler(ChanceFormButtonHide_Click);
		}
		
		private void ChanceFormButtonHide_Click(object sender, EventArgs e) {
			int index = ((AppButton)sender).Index;
			tasksButtons[index].Text = (index + 1).ToString();
			this.Close();
		}
	}
}