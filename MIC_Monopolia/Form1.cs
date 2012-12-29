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
		private const int CELL_X = 10;
		private const int CELL_Y = 10;
		private const int PERCENT_100 = 100;
		
		public MainField() {
			InitializeComponent();
			this.Font = new Font("PF Beausans Pro Light", 12F);
			fieldTableLayoutPanel.ColumnStyles.Insert(0, new ColumnStyle(SizeType.Percent, 80));
			fieldTableLayoutPanel.RowCount = 1;
			playSpaceTableLayoutPanel.ColumnCount = CELL_X;
			playSpaceTableLayoutPanel.RowCount = CELL_Y;
			for (int i = 0; i < playSpaceTableLayoutPanel.ColumnCount; i++) {
				playSpaceTableLayoutPanel.ColumnStyles.Insert(i, new ColumnStyle(SizeType.Percent, PERCENT_100 / playSpaceTableLayoutPanel.ColumnCount));
			}
			for (int i = 0; i < playSpaceTableLayoutPanel.RowCount; i++) {
				playSpaceTableLayoutPanel.RowStyles.Insert(i, new RowStyle(SizeType.Percent, PERCENT_100 / playSpaceTableLayoutPanel.RowCount));
			}
			
			playSpaceTableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
		}

		private void MainField_Load(object sender, EventArgs e) {

		}
	}
}