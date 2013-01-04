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

		private string mainFilePath = "";

		private Chip currentPlayerChip;

		private const string CHANCE = "Шанс";

		private const string LEADER = "Лидер";
		private const string INFORMATION = "Inформация";
		private const string LAW = "Право";
		private const string DIALOGUE_CULTURES = "Диалог культур";
		private const string GOOD = "Добро";
		private const string IT = "Информационные технологии";
		private const string CORPORATE = "Корпоратив";

		private TableLayoutPanel answerTableLayuotPanel = null;
		private AppButton right;
		private AppButton wrong;

		public ChanceForm(int currentPlayer, string type) {
			InitializeComponent();
			currentPlayerChip = new Chip() {
				BackgroundImage = Image.FromFile("chips/" + currentPlayer.ToString() + ".png")
			};
			if (type == CHANCE) {
				mainFilePath = CHANCE_TASK_FILE_PATH;
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
					mainFilePath = LEADER_TASK_FILE_PATH;
					readAllTasks(LEADER_TASK_FILE_PATH);
				}
				if (type == INFORMATION) {
					mainFilePath = INFORMATION_TASK_FILE_PATH;
					readAllTasks(INFORMATION_TASK_FILE_PATH);
				}
				if (type == LAW) {
					mainFilePath = LAW_TASK_FILE_PATH;
					readAllTasks(LAW_TASK_FILE_PATH);
				}
				if (type == DIALOGUE_CULTURES) {
					mainFilePath = DIALOGUE_CULTURES_TASK_FILE_PATH;
					readAllTasks(DIALOGUE_CULTURES_TASK_FILE_PATH);
				}
				if (type == GOOD) {
					mainFilePath = GOOD_TASK_FILE_PATH;
					readAllTasks(GOOD_TASK_FILE_PATH);
				}
				if (type == IT) {
					mainFilePath = IT_TASK_FILE_PATH;
					readAllTasks(IT_TASK_FILE_PATH);
				}
				if (type == CORPORATE) {
					mainFilePath = CORPORATE_TASK_FILE_PATH;
					readAllTasks(CORPORATE_TASK_FILE_PATH);
				}
				mainTableLayoutPanel.ColumnCount = 1;
				mainTableLayoutPanel.RowCount = tasksButtons.Length + 1;
				for (int i = 0; i < mainTableLayoutPanel.RowCount; i++) {
					mainTableLayoutPanel.RowStyles.Insert(i, new RowStyle(SizeType.Percent, PERCENT_100 / mainTableLayoutPanel.RowCount));
				}
				initializeAnswerPanel();
			}
			
			for (int i = 0; i < tasksButtons.Length; i++) {
				tasksButtons[i] = new AppButton() {
					Font = new Font("PF Beausans Pro Light", 12F),
					Text = (i + 1).ToString(),
					Size = new Size(50, 50),
					Dock = DockStyle.Fill,
					Index = i
				};
				tasksButtons[i].Click += new EventHandler(ChanceForm_Click);
			}

			foreach (AppButton b in tasksButtons) {
				mainTableLayoutPanel.Controls.Add(b);
			}
			if (type != CHANCE) {
				mainTableLayoutPanel.Controls.Add(answerTableLayuotPanel);
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
			tasksButtons[index].Text = File.ReadAllLines(@mainFilePath, System.Text.Encoding.Default)[ChosenIndex].Split('\t')[0];
			if (answerTableLayuotPanel != null) {
				answerTableLayuotPanel.Enabled = true;
				return;
			}
			
			tasksButtons[index].Click -= new EventHandler(ChanceForm_Click);
			tasksButtons[index].Click += new EventHandler(ChanceFormButtonHide_Click);
		}
		
		private void ChanceFormButtonHide_Click(object sender, EventArgs e) {
			int index = ((AppButton)sender).Index;
			tasksButtons[index].Text = (index + 1).ToString();
			this.Close();
		}

		public event AnswerEventHandler Answer;
		public delegate void AnswerEventHandler(bool answer);

		private void initializeAnswerPanel() {
			answerTableLayuotPanel = new TableLayoutPanel() {
				RowCount = 1,
				ColumnCount = 2,
				Dock = DockStyle.Fill,
				Enabled = false
			};
			for (int i = 0; i < answerTableLayuotPanel.ColumnCount; i++) {
				answerTableLayuotPanel.ColumnStyles.Insert(i, new ColumnStyle(SizeType.Percent, PERCENT_100 / answerTableLayuotPanel.ColumnCount));
			}
			right = initButtons("Верно!");
			wrong = initButtons("Неверно!");
			right.Click += right_Click;
			wrong.Click += wrong_Click;
			answerTableLayuotPanel.Controls.Add(right);
			answerTableLayuotPanel.Controls.Add(wrong);
		}

		private void wrong_Click(object sender, EventArgs e) {
			Answer(false);
			this.Close();
		}

		private void right_Click(object sender, EventArgs e) {
			Answer(true);
			this.Close();
		}

		private AppButton initButtons(string text) {
			AppButton app = new AppButton() {
				Text = text
			};
			return app;
		}
	}
}