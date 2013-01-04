using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GameItems;
using UsefulControls;
using Controls;

namespace MIC_Monopolia {
	public partial class MainField : Form {
		private const int SQUARE_SIDES_COUNT = 4;
		private const int PERCENT_100 = 100;
		private const int ERROR_INT = -1;
		private const int LEFT_MOST_COLUMN = 0;
		private const int DEFAULT_COUNT = 10;
		private const int MONEY_COLUMN_INDEX = 2;
		private const int PEOPLE_COLUMN_INDEX = 3;
		private const int FAMOUS_COLUMN_INDEX = 4;
		private const string IMAGE_CHIPS_PATH = "chips/";


		private Cell[] cells;
		private Chip[] chips;
		private Chip[] staticCloneChips;
		private ImprovedLabel[] namePlayersDisTextBox;
		private OpacityLabel[] moneyPlayersLabel;
		private OpacityLabel[] peoplePlayersLabel;
		private OpacityLabel[] famousPlayersLabel;
		private TableLayoutPanel cubesPanel;
		private Dice[] dices;
		
		private TableLayoutPanel taskTableLayoutPanel;
		private Chip[] tasksStaticCloneChips;
		private OpacityLabel[] tasksLabels;
		private OpacityLabel[] positionLabels;
		private PerformButton[] tasksPerformButtons;
		
		private Player[] players;
		private Game game;
		private bool isGame = false;

		private Color[] orderColor = new Color[] { Color.Red, 
			Color.Blue, Color.Green, Color.Yellow, Color.Black, Color.Brown, Color.Coral, Color.Orange, 
			Color.Purple, Color.Gray 
		};

		#region Create Field

		public MainField(int playCellsCount, int playersCount) {
			cells = new Cell[playCellsCount];
			namePlayersDisTextBox = new ImprovedLabel[playersCount];
			moneyPlayersLabel = new OpacityLabel[playersCount];
			peoplePlayersLabel = new OpacityLabel[playersCount];
			famousPlayersLabel = new OpacityLabel[playersCount];
			chips = new Chip[DEFAULT_COUNT];
			staticCloneChips = new Chip[DEFAULT_COUNT];
			tasksStaticCloneChips = new Chip[DEFAULT_COUNT];
			tasksLabels = new OpacityLabel[playersCount];
			positionLabels = new OpacityLabel[playersCount];
			tasksPerformButtons = new PerformButton[playersCount];
			cubesPanel = new TableLayoutPanel();
			taskTableLayoutPanel = new TableLayoutPanel();
			players = new Player[playersCount];

			InitializeComponent();
			this.Font = new Font("PF Beausans Pro Light", 12F);

			createField();
		}

		private void createField() {
			fieldTableLayoutPanel.ColumnStyles.Insert(0, new ColumnStyle(SizeType.Percent, 60));
			fieldTableLayoutPanel.ColumnStyles.Insert(1, new ColumnStyle(SizeType.Percent, 40));
			fieldTableLayoutPanel.RowCount = 1;
			spaceTableLayoutPanel.ColumnCount = calculateFieldSide();
			spaceTableLayoutPanel.RowCount = calculateFieldSide();
			for (int i = 0; i < spaceTableLayoutPanel.ColumnCount; i++) {
				spaceTableLayoutPanel.ColumnStyles.Insert(i, new ColumnStyle(SizeType.Percent, PERCENT_100 / spaceTableLayoutPanel.ColumnCount));
			}
			for (int i = 0; i < spaceTableLayoutPanel.RowCount; i++) {
				spaceTableLayoutPanel.RowStyles.Insert(i, new RowStyle(SizeType.Percent, PERCENT_100 / spaceTableLayoutPanel.RowCount));
			}
			controlTableLayoutPanel.RowStyles.Insert(0, new RowStyle(SizeType.Percent, 70));
			controlTableLayoutPanel.RowStyles.Insert(1, new RowStyle(SizeType.Absolute, 150));

			statisticTableLayoutPanel.RowCount = chips.Length;
			int chipSidePercent = PERCENT_100 / statisticTableLayoutPanel.RowCount;
			for (int i = 0; i < statisticTableLayoutPanel.RowCount; i++) {
				statisticTableLayoutPanel.RowStyles.Insert(i, new RowStyle(SizeType.Percent, chipSidePercent));
			}
			statisticTableLayoutPanel.ColumnCount = FAMOUS_COLUMN_INDEX + 1;
			statisticTableLayoutPanel.ColumnStyles.Insert(0, new ColumnStyle(SizeType.Absolute, percents(statisticTableLayoutPanel.Height, chipSidePercent)));
			statisticTableLayoutPanel.ColumnStyles.Insert(1, new ColumnStyle(SizeType.Percent, 80));
			statisticTableLayoutPanel.ColumnStyles.Insert(MONEY_COLUMN_INDEX, new ColumnStyle(SizeType.Percent, 20));
			statisticTableLayoutPanel.ColumnStyles.Insert(PEOPLE_COLUMN_INDEX, new ColumnStyle(SizeType.Percent, 20));
			statisticTableLayoutPanel.ColumnStyles.Insert(FAMOUS_COLUMN_INDEX, new ColumnStyle(SizeType.Percent, 20));
			controlTableLayoutPanel.Controls.Add(cubesPanel, 0, 1);

			createDicesPanel();
			initializeNamePlayersDisTextBox();
			initializePointPlayersLabel();
			initilizeCells();
			initilizePlayers();
			initilizeChips();
			initializeTaskField();
		}

		private void initializeTaskField() {
			taskTableLayoutPanel = new TableLayoutPanel() {
				Dock = DockStyle.Fill
			};
			spaceTableLayoutPanel.Controls.Add(taskTableLayoutPanel, 1, 1);
			spaceTableLayoutPanel.SetColumnSpan(taskTableLayoutPanel, calculateFieldSide() - 2);
			spaceTableLayoutPanel.SetRowSpan(taskTableLayoutPanel, calculateFieldSide() - 2);
			
			taskTableLayoutPanel.ColumnCount = 4;
			taskTableLayoutPanel.ColumnStyles.Insert(0, new ColumnStyle(SizeType.AutoSize));
			taskTableLayoutPanel.ColumnStyles.Insert(1, new ColumnStyle(SizeType.Percent, 70));
			taskTableLayoutPanel.ColumnStyles.Insert(2, new ColumnStyle(SizeType.AutoSize));
			taskTableLayoutPanel.ColumnStyles.Insert(3, new ColumnStyle(SizeType.AutoSize));
			int chipSidePercent = PERCENT_100 / statisticTableLayoutPanel.RowCount;
			taskTableLayoutPanel.RowCount = chips.Length;
			for (int i = 0; i < taskTableLayoutPanel.RowCount; i++) {
				taskTableLayoutPanel.RowStyles.Insert(i, new RowStyle(SizeType.Percent, chipSidePercent));
			}
			for (int i = 0; i < tasksLabels.Length; i++) {
				tasksLabels[i] = new OpacityLabel() {
					Text = "Задание",
					Dock = DockStyle.Fill
				};
				tasksPerformButtons[i] = new PerformButton() {
					Text = "+", 
					Dock = DockStyle.Fill,
					PlayerIndex = i
				};
				tasksPerformButtons[i].Click += new EventHandler(tasksPerformButton_Click);
				positionLabels[i] = new OpacityLabel() {
					Text = "0",
					Dock = DockStyle.Fill	
				};
			}
			for (int i = 0; i < tasksLabels.Length; i++) {
				taskTableLayoutPanel.Controls.Add(tasksStaticCloneChips[i], 0, i);
				taskTableLayoutPanel.Controls.Add(tasksLabels[i], 1, i);
				taskTableLayoutPanel.Controls.Add(positionLabels[i], 2, i);
				taskTableLayoutPanel.Controls.Add(tasksPerformButtons[i], 3, i);
			}
		}

		private void createDicesPanel() {
			dices = new Dice[2];
			int[] pair = new int[2];
			cubesPanel.Dock = DockStyle.Fill;
			cubesPanel.ColumnStyles.Insert(0, new ColumnStyle(SizeType.Percent, 30));
			cubesPanel.ColumnStyles.Insert(1, new ColumnStyle(SizeType.Percent, 40));
			cubesPanel.ColumnStyles.Insert(2, new ColumnStyle(SizeType.Percent, 30));
			for (int i = 0; i < dices.Length; i++) {
				dices[i] = new Dice() {
					BackgroundImage = Image.FromFile(@"1.jpg"),
					Dock = DockStyle.Fill,
					BackgroundImageLayout = ImageLayout.Zoom
				};
			}
			cubesPanel.RowCount = 1;
			cubesPanel.ColumnCount = 3;
			cubesPanel.Controls.Add(dices[0], 0, 0);
			cubesPanel.Controls.Add(dices[1], 2, 0);

			AppButton rollDicesButton = new AppButton() {
				Text = "Бросить кубики",
				Font = new Font("PF Beausans Pro Light", 15F)
			};
			rollDicesButton.Click += new EventHandler(rollDicesButton_Click);
			cubesPanel.Controls.Add(rollDicesButton, 1, 0);
			cubesPanel.AutoSize = true;
		}

		private void initializePointPlayersLabel() {
			for (int i = 0; i < moneyPlayersLabel.Length; i++) {
				moneyPlayersLabel[i] = new OpacityLabel() {
					Dock = DockStyle.Fill,
					TextAlign = ContentAlignment.MiddleCenter,
					Font = new Font("PF Beausans Pro Light", 12F, FontStyle.Bold),
					Text = "0"
				};
				peoplePlayersLabel[i] = new OpacityLabel() {
					Dock = DockStyle.Fill,
					TextAlign = ContentAlignment.MiddleCenter,
					Font = new Font("PF Beausans Pro Light", 12F, FontStyle.Bold),
					Text = "0"
				};
				famousPlayersLabel[i] = new OpacityLabel() {
					Dock = DockStyle.Fill,
					TextAlign = ContentAlignment.MiddleCenter,
					Font = new Font("PF Beausans Pro Light", 12F, FontStyle.Bold),
					Text = "0"
				};
			}
			for (int i = 0; i < moneyPlayersLabel.Length; i++) {
				statisticTableLayoutPanel.Controls.Add(moneyPlayersLabel[i], MONEY_COLUMN_INDEX, i);
				statisticTableLayoutPanel.Controls.Add(peoplePlayersLabel[i], PEOPLE_COLUMN_INDEX, i);
				statisticTableLayoutPanel.Controls.Add(famousPlayersLabel[i], FAMOUS_COLUMN_INDEX, i);
			}
		}

		private void initializeNamePlayersDisTextBox() {
			for (int i = 0; i < namePlayersDisTextBox.Length; i++) {
				namePlayersDisTextBox[i] = new ImprovedLabel() {
					Text = "Введите название команды",
					Dock = DockStyle.Fill,
					Control = ImprovedLabel.OBJ.TextBox,
					BorderStyle = BorderStyle.None
				};
			}
			for (int i = 0; i < namePlayersDisTextBox.Length; i++) {
				statisticTableLayoutPanel.Controls.Add(namePlayersDisTextBox[i].Label, 1, i);
				statisticTableLayoutPanel.Controls.Add(namePlayersDisTextBox[i].TextBox, 1, i);
			}
		}

		/// <summary>
		/// Input 4 queues of cells
		/// </summary>
		private void initilizeCells() {
			for (int i = 0; i < cells.Length; i++) {
				cells[i] = new Cell() {
					Index = i,
					Task = Rules.AllRules[i]
				};
				cells[i].Click += new EventHandler(MainField_Click);
			}
			for (int i = 0; i < calculateFieldSide(); i++) {
				spaceTableLayoutPanel.Controls.Add(cells[i], i, 0);
			}
			for (int i = 1; i < calculateFieldSide(); i++) {
				spaceTableLayoutPanel.Controls.Add(cells[calculateFieldSide() + i - 1], calculateFieldSide(), i);
			}
			for (int i = 2; i < calculateFieldSide() + 1; i++) {
				spaceTableLayoutPanel.Controls.Add(cells[(calculateFieldSide() * 2) + i - 3], calculateFieldSide() - i, calculateFieldSide() - 1);
			}
			for (int i = 2; i < calculateFieldSide(); i++) {
				spaceTableLayoutPanel.Controls.Add(cells[(calculateFieldSide() * 3) + i - 4], 0, calculateFieldSide() - i);
			}
		}

		private void initilizePlayers() {
			for (int i = 0; i < players.Length; i++) {
				players[i] = new Player(namePlayersDisTextBox[i].Text);
			}
		}

		/// <summary>
		/// Chips count is always 10. It need to save size of chip. All other chips are invisible.
		/// </summary>
		private void initilizeChips() {
			for (int i = 0; i < DEFAULT_COUNT; i++) {
				chips[i] = new Chip() {
					Image = Image.FromFile(IMAGE_CHIPS_PATH + i.ToString() + ".png")
				};
				staticCloneChips[i] = new Chip() {
					Image = Image.FromFile(IMAGE_CHIPS_PATH + i.ToString() + ".png")
				};
				tasksStaticCloneChips[i] = new Chip() {
					Image = Image.FromFile(IMAGE_CHIPS_PATH + i.ToString() + ".png")
				};
			}
			for (int i = players.Length; i < DEFAULT_COUNT; i++) {
				chips[i].Visible = false;
				staticCloneChips[i].Visible = false;
				tasksStaticCloneChips[i].Visible = false;
			}
			for (int i = 0; i < chips.Length; i++) {
				statisticTableLayoutPanel.Controls.Add(staticCloneChips[i], 0, i);
			}
		}
		
		private void rollDicesButton_Click(object sender, EventArgs e) {
			Random r = new Random();
			for (int i = 0; i < 2; i++) {
				int rand = r.Next(1, 6);
				dices[i].BackgroundImage = Image.FromFile(@"" + rand.ToString() + ".jpg");
				dices[i].Number = rand;
			}
			play();
		}
		
		private int percents(int value, int per) {
			return (value * per) / PERCENT_100;
		}

		private int calculateFieldSide() {
			if ((cells.Length % SQUARE_SIDES_COUNT) != 0) {
				return ERROR_INT;
			}
			return (cells.Length / SQUARE_SIDES_COUNT) + 1;
		}

		private void MainField_Click(object sender, EventArgs e) {

		}

		private void MainField_Load(object sender, EventArgs e) {

		}

		#endregion
		
		#region Play
		
		private bool beginPlayCondition() {
			foreach (ImprovedLabel m in namePlayersDisTextBox) {
				if (m.Control == ImprovedLabel.OBJ.TextBox) {
					MessageBox.Show("Не все названия команд заполнены!");
					return false;
				}
			}
			return true;
		}

		private int sumPointsOfDices() {
			int sum = 0;
			foreach (Dice d in dices) {
				sum += d.Number;
			}
			return sum;
		}

		private void play() {
			if (beginPlayCondition() == false) {
				return;
			}
			if (isGame == false) {
				game = new Game(players, cells.Length);
				isGame = true;
			}
			game.NextMove(sumPointsOfDices());
			game.CheckCell(cells[game.PlayersPositions[game.CurrentPlayerIndex]].Task);
			viewDatas();
		}
		
		private void viewDatas() {
			int currentPlayerIndex = game.CurrentPlayerIndex;
			int currentPosition = game.PlayersPositions[currentPlayerIndex];
			adjustSizeOfChips(currentPlayerIndex, currentPosition);
			cells[currentPosition].Controls.Add(chips[currentPlayerIndex]);
			tasksLabels[currentPlayerIndex].Text = cells[currentPosition].Task;
			positionLabels[currentPlayerIndex].Text = currentPosition.ToString();
			for (int i = 0; i < players.Length; i++) {
				moneyPlayersLabel[i].Text = game.Money[i].ToString();
				peoplePlayersLabel[i].Text = game.People[i].ToString();
				famousPlayersLabel[i].Text = game.Famous[i].ToString();	
			}
		}

		private void tasksPerformButton_Click(object sender, EventArgs e) {
			int playerIndex = ((PerformButton)sender).PlayerIndex;
			game.SetPointsToPlayer(playerIndex, game.PlayersPositions[playerIndex]);
			viewDatas();
		}
		
		private void adjustSizeOfChips(int player, int position) {
			for (int i = 0; i < players.Length; i++) {
				if (game.GetSamePositionsOfPlayer(i).Count == 0) {
					chips[i].Dock = DockStyle.Fill;
				}
			}
			if (game.SamePositionsOfCurrentPlayer.Count == 1) {
				Size halfCellSize = new Size(cells[position].Width / 2, cells[position].Height);
				chips[player].Size = halfCellSize;
				chips[game.SamePositionsOfCurrentPlayer[0]].Size = halfCellSize;
				
				chips[player].Dock = DockStyle.Left;
				chips[game.SamePositionsOfCurrentPlayer[0]].Dock = DockStyle.Right;
				return;
			}
			if ((game.SamePositionsOfCurrentPlayer.Count == 2) || (game.SamePositionsOfCurrentPlayer.Count == 3)) {
				Size quarterCellSize = new Size(cells[position].Width / 2, cells[position].Height / 2);				
				chips[player].Dock = DockStyle.None;
				chips[player].Size = quarterCellSize;
				foreach (int i in game.SamePositionsOfCurrentPlayer) {
					chips[i].Dock = DockStyle.None;
					chips[i].Size = quarterCellSize;
				}
				chips[player].Location = new Point(0, 0);
				chips[game.SamePositionsOfCurrentPlayer[0]].Location = new Point(quarterCellSize.Width, 0);
				chips[game.SamePositionsOfCurrentPlayer[1]].Location = new Point(0, quarterCellSize.Height);
				if (game.SamePositionsOfCurrentPlayer.Count == 3) {
					chips[game.SamePositionsOfCurrentPlayer[2]].Location = new Point(quarterCellSize);
				}
			}

			//if ((game.AllPlayersHaveMoved) && 
			//    ((game.SamePositionsOfCurrentPlayer.Count >= 4) || (game.SamePositionsOfCurrentPlayer.Count <= 9))) {
			//    Size titheCellSize = new Size(cells[position].Width / 2, cells[position].Height / 5);
			//    chips[player].Dock = DockStyle.None;
			//    chips[player].Size = titheCellSize;
			//    foreach (int i in game.SamePositionsOfCurrentPlayer) {
			//        chips[i].Dock = DockStyle.None;
			//        chips[i].Size = titheCellSize;
			//    }
			//    chips[player].Location = new Point(0, 0);
			//    chips[game.SamePositionsOfCurrentPlayer[0]].Location = new Point(titheCellSize.Width, 0);
			//    chips[game.SamePositionsOfCurrentPlayer[1]].Location = new Point(0, titheCellSize.Height);
			//    chips[game.SamePositionsOfCurrentPlayer[2]].Location = new Point(titheCellSize);
			//    chips[game.SamePositionsOfCurrentPlayer[3]].Location = new Point(0, titheCellSize.Height * 2);
			//    if (game.SamePositionsOfCurrentPlayer.Count > 4) {
			//        chips[game.SamePositionsOfCurrentPlayer[4]].Location = new Point(titheCellSize.Width, titheCellSize.Height * 2);
			//    }
			//    if (game.SamePositionsOfCurrentPlayer.Count > 5) {
			//        chips[game.SamePositionsOfCurrentPlayer[5]].Location = new Point(0, titheCellSize.Height * 3);
			//    }
			//    if (game.SamePositionsOfCurrentPlayer.Count > 6) {
			//        chips[game.SamePositionsOfCurrentPlayer[6]].Location = new Point(titheCellSize.Width, titheCellSize.Height * 3);
			//    }
			//    if (game.SamePositionsOfCurrentPlayer.Count > 7) {
			//        chips[game.SamePositionsOfCurrentPlayer[7]].Location = new Point(0, titheCellSize.Height * 4);
			//    }
			//    if (game.SamePositionsOfCurrentPlayer.Count > 8) {
			//        chips[game.SamePositionsOfCurrentPlayer[8]].Location = new Point(titheCellSize.Width, titheCellSize.Height * 4);
			//    }
			//    if (game.SamePositionsOfCurrentPlayer.Count > 9) {
			//        chips[game.SamePositionsOfCurrentPlayer[9]].Location = new Point(0, titheCellSize.Height * 5);
			//    }
			//    chips[game.SamePositionsOfCurrentPlayer[3]].Location = new Point(0, titheCellSize.Height * 2);
				
			//    if (game.SamePositionsOfCurrentPlayer.Count == 3) {
			//        chips[game.SamePositionsOfCurrentPlayer[2]].Location = new Point(titheCellSize);
			//    }
			//}
		}
		
		#endregion
		
		#region Animation
		
		
		
		#endregion
	}
}