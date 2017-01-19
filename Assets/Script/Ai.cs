using UnityEngine;
using System.Collections;
using System;

public class Ai : MonoBehaviour, IPlayer {
    private GameManager _gameManager;

    public Const.Color Color { get; set; }

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

    public void StartTurn()
    {
         _gameManager.osero.SetStone(decision(_gameManager.osero.Board), Color);
        EndTurn();
    }

    public void EndTurn()
    {
        _gameManager.ChangeTurn(this);
    }

    public void SetGameManager(GameManager gameManager)
    {
        _gameManager = gameManager;
    }
}


