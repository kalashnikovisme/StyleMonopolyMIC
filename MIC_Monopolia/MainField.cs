using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GameItems;

namespace MIC_Monopolia {
	public partial class MainField : Form {
		private const int SQUARE_SIDES_COUNT = 4;
		private const int PERCENT_100 = 100;
		private const int ERROR_INT = -1;
		
		private Cell[] playCells;
		
		public MainField(int playCellsCount) {
			playCells = new Cell[playCellsCount];
			
			InitializeComponent();
			this.Font = new Font("PF Beausans Pro Light", 12F);
			
			createField();
		}

		private void createField() {
			fieldTableLayoutPanel.ColumnStyles.Insert(0, new ColumnStyle(SizeType.Percent, 80));
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
			
			inputCells();			
		}
	
		private int calculateFieldSide() {
			if ((playCells.Length % SQUARE_SIDES_COUNT) != 0) {
				return ERROR_INT;
			}
			return playCells.Length / SQUARE_SIDES_COUNT;
		}
		
		/// <summary>
		/// Input 4 queues of cells
		/// </summary>
		private void inputCells() {
			for (int i = 0; i < playCells.Length; i++) {
				playCells[i] = new Cell(i);
				playCells[i].Click += new EventHandler(MainField_Click);
			}
			for (int i = 0; i < calculateFieldSide(); i++) {
				spaceTableLayoutPanel.Controls.Add(playCells[i], i, 0);
			}
			for (int i = 1; i < calculateFieldSide(); i++) {
				spaceTableLayoutPanel.Controls.Add(playCells[calculateFieldSide() + i], calculateFieldSide(), i);
			}
			for (int i = 2; i < calculateFieldSide() + 1; i++) {
				spaceTableLayoutPanel.Controls.Add(playCells[(calculateFieldSide() * 2) + i], calculateFieldSide() - i, calculateFieldSide() - 1);
			}
			for (int i = 2; i < calculateFieldSide(); i++) {
				spaceTableLayoutPanel.Controls.Add(playCells[(calculateFieldSide() * 3) + i], 0, calculateFieldSide() - i);
			}
		}

		private void MainField_Click(object sender, EventArgs e) {
			
		}

		private void MainField_Load(object sender, EventArgs e) {
			
		}
	}
}