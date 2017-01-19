using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] Osero osero;
    [SerializeField] private IPlayer _player1;
    [SerializeField] private IPlayer _player2;

    private IPlayer RivalPlayer(IPlayer player)
    {
        if (player == _player1)
        {
            return _player2;
        }
        else
        {
            return _player1;
        }
    }

    public void ChangeTurn(IPlayer player)
    {
        osero.board.color = player.Color;
        RivalPlayer(player).StartTurn();
    }


    void Start () {
        _player1.SetGameManager(this);
        _player2.SetGameManager(this);
        osero.initialize ();

    }

}