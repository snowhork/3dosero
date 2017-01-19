using UnityEngine;
using System.Collections;
using System;

public class Ai {		
	public Position decision(Board board) {
		int maxstones = 0;
		Position maxPosition = null;
		foreach (Position p in board.StonesIterator) {
			var getStones = board.SetStone (p, Const.Color.Blue);
			if (getStones.Count >= maxstones) {
				maxstones = getStones.Count;
				maxPosition = p;
			}
		}
		Debug.Log (maxstones);
		return maxPosition;
	}
}


