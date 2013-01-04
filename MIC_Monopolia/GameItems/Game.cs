using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameItems {
	public class Game {
		private Player[] players;
		
		private const int MONEY = 0;
		private const int PEOPLE = 1;
		private const int FAMOUS = 2;
		
		private const string CHANCE = "Шанс";
		
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
		
		public bool AllPlayersHaveMoved = false;
		
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
				AllPlayersHaveMoved = true;
			}
			players[currentPlayerIndex].Position += value;
			if (players[currentPlayerIndex].Position >= cellCount) {
				players[currentPlayerIndex].Position %= cellCount;
			}
			
		}
		
		public void CheckCell(string taskCell) {
			if (taskCell == CHANCE) {
				ChanceForm chance = new ChanceForm();
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

		public int[] Money {
			get {
				List<int> money = new List<int>();
				foreach (Player p in players) {
					money.Add(p.Money);
				}
				return money.ToArray<int>();
			}
		}
		
		public int[] People {
			get {
				List<int> people = new List<int>();
				foreach (Player p in players) {
					people.Add(p.People);
				}
				return people.ToArray<int>();
			}
		}

		public int[] Famous {
			get {
				List<int> famous= new List<int>();
				foreach (Player p in players) {
					famous.Add(p.Famous);
				}
				return famous.ToArray<int>();
			}
		}
		
		private List<int> getSamePositionsOfPlayer(int playerIndex) {
			List<int> pos = new List<int>();
			for (int i = 0; i < players.Length; i++) {
				if (i == playerIndex) {
					continue;
				}
				if (players[i].Position == players[playerIndex].Position) {
					pos.Add(i);
				}
			}
			return pos;
		}
		
		public List<int> SamePositionsOfCurrentPlayer {
			get {
				return getSamePositionsOfPlayer(CurrentPlayerIndex);
			}
		}
		
		public List<int> GetSamePositionsOfPlayer(int playerIndex) {
			return getSamePositionsOfPlayer(playerIndex);
		} 
		
		public void SetPointsToPlayer(int playerIndex, int cellIndex) {
			int[] points = Rules.Points(cellIndex);
			if (points.Length == 0) {
				return;
			}
			players[playerIndex].Money += points[MONEY];
			players[playerIndex].People += points[PEOPLE];
			players[playerIndex].Famous += points[FAMOUS];
		}
	}
}