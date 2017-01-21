using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] Osero _osero;
    [SerializeField] Player player;
    [SerializeField] Ai ai;

    public Osero osero
    {
        get { return _osero; }
    }

    private IPlayer _player1;
    private IPlayer _player2;

    private IPlayer RivalPlayer(IPlayer player)
    {
        return player == _player1 ? _player2 : _player1;
    }

    public void ChangeTurn(IPlayer player)
    {
        osero.Board.color = player.Color;
        RivalPlayer(player).StartTurn();
    }


    void Start()
    {
        _player1 = player;
        _player2 = ai;
        _player1.Color = Const.Color.Red;
        _player2.Color = Const.Color.Blue;

        _player1.SetGameManager(this);
        _player2.SetGameManager(this);

        osero.Initialize();
        _player1.StartTurn();
    }
}