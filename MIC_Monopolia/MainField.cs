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
		
		private Cell[] cells;
		private Chip[] chips;		
		private	ImprovedLabel[] namePlayersDisTextBox;
		private CubesPanel cubesPanel;
		
		private Player[] players;
		
		private Color[] orderColor = new Color[] { Color.Red, 
			Color.Blue, Color.Green, Color.Yellow, Color.Black, Color.Brown, Color.Coral, Color.Orange, 
			Color.Purple, Color.Gray 
		}; 
		
		public MainField(int playCellsCount, int playersCount) {
			cells = new Cell[playCellsCount];
			namePlayersDisTextBox = new ImprovedLabel[playersCount];
			chips = new Chip[CHIPS_COUNT];
			cubesPanel = new CubesPanel();
			
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
			for (int i = 1; i < statisticTableLayoutPanel.ColumnCount; i++) {
				statisticTableLayoutPanel.ColumnStyles.Insert(i, new ColumnStyle(SizeType.AutoSize));
			}

			controlTableLayoutPanel.Controls.Add(cubesPanel, 0, 1);

			initializeNamePlayersDisTextBox();
			initilizeCells();
			initilizePlayers();
			initilizeChips();
		}
	
		private int percents(int value, int per) {
			return (value * per) / PERCENT_100;
		}
	
		private void initializeNamePlayersDisTextBox() {
			for (int i = 0; i < namePlayersDisTextBox.Length; i++) {
				namePlayersDisTextBox[i] = new ImprovedLabel() {
					Text = "Введите название команды",
					Dock = DockStyle.Fill,
					Control = ImprovedLabel.OBJ.TextBox
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
				chips[i] = new Chip(orderColor[i]);
			}
			for (int i = players.Length - 1; i < CHIPS_COUNT; i++) {
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
	}
}