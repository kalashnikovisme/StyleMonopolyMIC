using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameItems {
	public class Game {
		private Player[] players;
		private int currentPlayerIndex = -1;
		private int cellCount = 0;
		
		public Game(Player[] gamePlayers, int gameCellCount) {
			players = gamePlayers;
			for (int i = 0; i < players.Length; i++) {
				players[i].Position = 0;
			}
			currentPlayerIndex++;
			
			cellCount = gameCellCount;
		}
		
		public void NextMove(int value) {
			players[currentPlayerIndex].Position += value;
			if (players[currentPlayerIndex].Position >= cellCount) {
				players[currentPlayerIndex].Position %= cellCount;
			}
			currentPlayerIndex++;
			if (currentPlayerIndex >= players.Length) {
				currentPlayerIndex = 0;
			}
		}
		
		private List<int> positionPlayers;
		public int[] PlayersPositions {
			get {
				positionPlayers = new List<int>();
				foreach (Player p in players) {
					positionPlayers.Add(p.Position);
				}
				return positionPlayers.ToArray<int>();
			}
		}
	}
}