using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MIC_Monopolia {
	public partial class MainField : Form {
		private int cellsCount = 0;
		private const int SQUARE_SIDES_COUNT = 4;
		private const int PERCENT_100 = 100;
		private const int ERROR_INT = -1;
		
		public MainField(int toCellsCount) {
			cellsCount = toCellsCount;
			
			InitializeComponent();
			this.Font = new Font("PF Beausans Pro Light", 12F);
			
			createField();
		}

		private void createField() {
			fieldTableLayoutPanel.ColumnStyles.Insert(0, new ColumnStyle(SizeType.Percent, 80));
			fieldTableLayoutPanel.RowCount = 1;
			playSpaceTableLayoutPanel.ColumnCount = calculateFieldSide();
			playSpaceTableLayoutPanel.RowCount = calculateFieldSide();
			for (int i = 0; i < playSpaceTableLayoutPanel.ColumnCount; i++) {
				playSpaceTableLayoutPanel.ColumnStyles.Insert(i, new ColumnStyle(SizeType.Percent, PERCENT_100 / playSpaceTableLayoutPanel.ColumnCount));
			}
			for (int i = 0; i < playSpaceTableLayoutPanel.RowCount; i++) {
				playSpaceTableLayoutPanel.RowStyles.Insert(i, new RowStyle(SizeType.Percent, PERCENT_100 / playSpaceTableLayoutPanel.RowCount));
			}
			playSpaceTableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
			
			inputCells();			
		}
		
		private int calculateFieldSide() {
			if ((cellsCount % SQUARE_SIDES_COUNT) != 0) {
				return ERROR_INT;
			}
			return cellsCount / SQUARE_SIDES_COUNT;
		}
		
		private void inputCells() {
			
		}

		private void MainField_Load(object sender, EventArgs e) {

		}
	}
}