using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameItems {
	public class Game {
		private Player[] players;
		private int currentPlayerIndex = -1;
		
		public Game(Player[] gamePlayers) {
			players = gamePlayers;
			currentPlayerIndex++;
		}
		
		public void NextMove(int value) {
			players[currentPlayerIndex++].Position += value;
		}
	}
}