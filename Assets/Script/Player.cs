using UnityEngine;
using System.Collections;
using System.ComponentModel.Design;

public class Player : RayCaster, IPlayer
{
    private GameManager _gameManager;
    private bool _isTurn;
    public Const.Color Color { get; set; }

    public void SetGameManager(GameManager gameManager)
    {
        _gameManager = gameManager;
    }


    private void Update ()
	{
	    if (!_isTurn)
	    {
	        return;
	    }
	    var rayPosition = transform.localPosition;
	    var ray = new Ray(rayPosition, transform.forward);
	    RayCast(ray);
	}

    public void StartTurn()
    {
        _isTurn = true;
    }

    public void EndTurn()
    {
        _isTurn = false;
        _gameManager.ChangeTurn(this);
    }

}
