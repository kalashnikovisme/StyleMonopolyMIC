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

namespace MIC_Monopolia {
	public partial class MainField : Form {
		private const int SQUARE_SIDES_COUNT = 4;
		private const int PERCENT_100 = 100;
		private const int ERROR_INT = -1;
		private const int LEFT_MOST_COLUMN = 0;
		private const int CHIPS_COUNT = 10;
		private const int POINTS_COLUMN_INDEX = 2;
		private const string IMAGE_CHIPS_PATH = "chips/";


		private Cell[] cells;
		private Chip[] chips;
		private ImprovedLabel[] namePlayersDisTextBox;
		private OpacityLabel[] pointsPlayersLabel;
		private TableLayoutPanel cubesPanel;
		private Dice[] dices;

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
			pointsPlayersLabel = new OpacityLabel[playersCount];
			chips = new Chip[CHIPS_COUNT];
			cubesPanel = new TableLayoutPanel();

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
			spaceTableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;

			controlTableLayoutPanel.RowStyles.Insert(0, new RowStyle(SizeType.Percent, 70));
			controlTableLayoutPanel.RowStyles.Insert(1, new RowStyle(SizeType.Absolute, 150));

			statisticTableLayoutPanel.RowCount = chips.Length;
			int chipSidePercent = PERCENT_100 / statisticTableLayoutPanel.RowCount;
			for (int i = 0; i < statisticTableLayoutPanel.RowCount; i++) {
				statisticTableLayoutPanel.RowStyles.Insert(i, new RowStyle(SizeType.Percent, chipSidePercent));
			}
			statisticTableLayoutPanel.ColumnStyles.Insert(0, new ColumnStyle(SizeType.Absolute, percents(statisticTableLayoutPanel.Height, chipSidePercent)));
			statisticTableLayoutPanel.ColumnStyles.Insert(1, new ColumnStyle(SizeType.Percent, 80));
			statisticTableLayoutPanel.ColumnStyles.Insert(POINTS_COLUMN_INDEX, new ColumnStyle(SizeType.Percent, 20));

			controlTableLayoutPanel.Controls.Add(cubesPanel, 0, 1);

			createDicesPanel();
			initializeNamePlayersDisTextBox();
			initializePointPlayersLabel();
			initilizeCells();
			initilizePlayers();
			initilizeChips();
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

		private void rollDicesButton_Click(object sender, EventArgs e) {
			Random r = new Random();
			for (int i = 0; i < 2; i++) {
				int rand = r.Next(1, 6);
				dices[i].BackgroundImage = Image.FromFile(@"" + rand.ToString() + ".jpg");
				dices[i].Number = rand;
			}
			play();
		}

		private void initializePointPlayersLabel() {
			for (int i = 0; i < pointsPlayersLabel.Length; i++) {
				pointsPlayersLabel[i] = new OpacityLabel() {
					Dock = DockStyle.Fill,
					TextAlign = ContentAlignment.MiddleCenter,
					Font = new Font("PF Beausans Pro Light", 12F, FontStyle.Bold),
					Text = "0"
				};
			}
			for (int i = 0; i < pointsPlayersLabel.Length; i++) {
				statisticTableLayoutPanel.Controls.Add(pointsPlayersLabel[i], POINTS_COLUMN_INDEX, i);
			}
		}

		private int percents(int value, int per) {
			return (value * per) / PERCENT_100;
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

		private int calculateFieldSide() {
			if ((cells.Length % SQUARE_SIDES_COUNT) != 0) {
				return ERROR_INT;
			}
			return cells.Length / SQUARE_SIDES_COUNT;
		}

		/// <summary>
		/// Input 4 queues of cells
		/// </summary>
		private void initilizeCells() {
			for (int i = 0; i < cells.Length; i++) {
				cells[i] = new Cell(i);
				cells[i].Click += new EventHandler(MainField_Click);
			}
			for (int i = 0; i < calculateFieldSide(); i++) {
				spaceTableLayoutPanel.Controls.Add(cells[i], i, 0);
			}
			for (int i = 1; i < calculateFieldSide(); i++) {
				spaceTableLayoutPanel.Controls.Add(cells[calculateFieldSide() + i], calculateFieldSide(), i);
			}
			for (int i = 2; i < calculateFieldSide() + 1; i++) {
				spaceTableLayoutPanel.Controls.Add(cells[(calculateFieldSide() * 2) + i], calculateFieldSide() - i, calculateFieldSide() - 1);
			}
			for (int i = 2; i < calculateFieldSide(); i++) {
				spaceTableLayoutPanel.Controls.Add(cells[(calculateFieldSide() * 3) + i], 0, calculateFieldSide() - i);
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
			for (int i = 0; i < CHIPS_COUNT; i++) {
				chips[i] = new Chip() {
					Image = Image.FromFile(IMAGE_CHIPS_PATH + i.ToString() + ".png")
				};
			}
			for (int i = players.Length; i < CHIPS_COUNT; i++) {
				chips[i].Visible = false;
			}
			for (int i = 0; i < chips.Length; i++) {
				statisticTableLayoutPanel.Controls.Add(chips[i], 0, i);
			}
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

			viewDatas();
		}
		
		private void viewDatas() {
			for (int i = 0; i < game.PlayersPositions.Length; i++) {
				pointsPlayersLabel[i].Text = game.PlayersPositions[i].ToString();
			}
			
			int currentPlayerIndex = game.CurrentPlayerIndex;
			int currentPosition = game.PlayersPositions[currentPlayerIndex];
			adjustSizeOfChips(currentPlayerIndex, currentPosition);
			cells[currentPosition].Controls.Add(chips[currentPlayerIndex]);
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
				MessageBox.Show("2");
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
					MessageBox.Show("3");
					chips[game.SamePositionsOfCurrentPlayer[2]].Location = new Point(quarterCellSize);
				}
			}
		}
		
		#endregion
		
		#region Animation
		
		
		
		#endregion
	}
}