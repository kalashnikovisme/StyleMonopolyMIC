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
		private const string LEADER_TASK_FILE_PATH = "leader.txt";
		private const string INFORMATION_TASK_FILE_PATH = "information.txt";
		private const string LAW_TASK_FILE_PATH = "law.txt";
		private const string DIALOGUE_CULTURES_TASK_FILE_PATH = "dialogue_cultures.txt";
		private const string GOOD_TASK_FILE_PATH = "good.txt";
		private const string IT_TASK_FILE_PATH = "it.txt";
		private const string CORPORATE_TASK_FILE_PATH = "corporate.txt";
		private AppButton[] tasksButtons;
		private const int PERCENT_100 = 100;
		private const int ERROR = -1;
		public int ChosenIndex = ERROR;

		private Chip currentPlayerChip;

		private const string CHANCE = "Шанс";

		private const string LEADER = "Лидер";
		private const string INFORMATION = "Inформация";
		private const string LAW = "Право";
		private const string DIALOGUE_CULTURES = "Диалог культур";
		private const string GOOD = "Добро";
		private const string IT = "Информационные технологии";
		private const string CORPORATE = "Корпоратив";

		public ChanceForm(int currentPlayer, string type) {
			InitializeComponent();
			currentPlayerChip = new Chip() {
				BackgroundImage = Image.FromFile("chips/" + currentPlayer.ToString() + ".png")
			};
			if (type == CHANCE) {
				readAllTasks(CHANCE_TASK_FILE_PATH);
				double qur = Math.Round(Math.Sqrt((double)tasksButtons.Length), 0, MidpointRounding.AwayFromZero);
				int sideTableCount = (int)qur;
				mainTableLayoutPanel.ColumnCount = sideTableCount;
				mainTableLayoutPanel.RowCount = sideTableCount;
				for (int i = 0; i < sideTableCount; i++) {
					mainTableLayoutPanel.ColumnStyles.Insert(i, new ColumnStyle(SizeType.Percent, PERCENT_100 / sideTableCount));

					mainTableLayoutPanel.RowStyles.Insert(i, new RowStyle(SizeType.Percent, PERCENT_100 / sideTableCount));
				}
			} else {
				if (type == LEADER) {
					readAllTasks(LEADER_TASK_FILE_PATH);
				}
				if (type == INFORMATION) {
					readAllTasks(INFORMATION_TASK_FILE_PATH);
				}
				if (type == LAW) {
					readAllTasks(LAW_TASK_FILE_PATH);
				}
				if (type == DIALOGUE_CULTURES) {
					readAllTasks(DIALOGUE_CULTURES_TASK_FILE_PATH);
				}
				if (type == GOOD) {
					readAllTasks(GOOD_TASK_FILE_PATH);
				}
				if (type == IT) {
					readAllTasks(IT_TASK_FILE_PATH);
				}
				if (type == CORPORATE) {
					readAllTasks(CORPORATE_TASK_FILE_PATH);
				}
				mainTableLayoutPanel.ColumnCount = 1;
				mainTableLayoutPanel.RowCount = tasksButtons.Length;
				for (int i = 0; i < mainTableLayoutPanel.RowCount; i++) {
					mainTableLayoutPanel.RowStyles.Insert(i, new RowStyle(SizeType.Percent, PERCENT_100 / mainTableLayoutPanel.RowCount));
				}
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
			mainTableLayoutPanel.Controls.Add(currentPlayerChip);
			this.Show();
			this.FormClosing += ChanceForm_FormClosing;
		}

		private void readAllTasks(string filePath) {
			string[] tasks = File.ReadAllLines(@filePath, System.Text.Encoding.Default);
			tasksButtons = new AppButton[tasks.Length + 1];
		}

		private void ChanceForm_FormClosing(object sender, FormClosingEventArgs e) {
			if (ChosenIndex == ERROR) {
				ChosenIndex = 0;
			}
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