using UnityEngine;
using System.Collections;

public interface IPlayer
{
    Const.Color Color { get; }

    void SetGameManager(GameManager gameManager);
	void StartTurn ();
	void EndTurn();
}
