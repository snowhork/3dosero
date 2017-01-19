using UnityEngine;
using System.Collections;
using System.ComponentModel.Design;

public class Player : RayCaster,IPlayer
{
    private GameManager _gameManager;

    private Const.Color _color;
    public Const.Color Color { get { return _color; } }

    public void SetGameManager(GameManager gameManager)
    {
        _gameManager = gameManager;
    }


	void Update () {
		Vector3 rayPosition = transform.localPosition;
		var ray = new Ray (rayPosition, transform.forward);
		RayCast (ray);
	}

	public void TurnEnd() {
//		Osero.Instance.Changeturn (Const.antiColor(color));
		//color = Const.antiColor (color);
	}

    public void StartTurn()
    {

    }

    public void EndTurn()
    {
         _gameManager.ChangeTurn(this);
    }

}
