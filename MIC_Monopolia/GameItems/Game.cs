using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameItems {
	public class Game {
		private Player[] players;
		
		private const int GAME_IS_NOT_BEGIN = -1;
		private int currentPlayerIndex = GAME_IS_NOT_BEGIN;
		public int CurrentPlayerIndex {
			get {
				if (currentPlayerIndex == GAME_IS_NOT_BEGIN) {
					throw new Exception("The game is not started");
				}
				return currentPlayerIndex;
			}
		}
		
		private int cellCount = 0;
		
		public Game(Player[] gamePlayers, int gameCellCount) {
			players = gamePlayers;
			for (int i = 0; i < players.Length; i++) {
				players[i].Position = 0;
			}
			
			cellCount = gameCellCount;
		}
		
		public void NextMove(int value) {
			if (++currentPlayerIndex >= players.Length) {
				currentPlayerIndex = 0;
			}
			players[currentPlayerIndex].Position += value;
			if (players[currentPlayerIndex].Position >= cellCount) {
				players[currentPlayerIndex].Position %= cellCount;
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
		
		public List<int> SamePositionsOfCurrentPlayer {
			get {
				List<int> pos = new List<int>();
				for (int i = 0; i < players.Length; i++) {
					if (i == CurrentPlayerIndex) {
						continue;
					}
					if (players[i].Position == players[CurrentPlayerIndex].Position) {
						pos.Add(i);
					}
				}
				return pos;
			}
		}
	}
}