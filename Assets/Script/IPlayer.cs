using UnityEngine;
using System.Collections;

public interface IPlayer
{
    Const.Color Color { get; set; }

    void SetGameManager(GameManager gameManager);
	void StartTurn ();
	void EndTurn();
}
