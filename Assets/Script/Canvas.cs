using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Canvas : MonoBehaviour
{

    [SerializeField] private Osero osero;
    [SerializeField] Text RedNum;
    [SerializeField] Text WhiteNum;
    [SerializeField] Text BlueNum;
    [SerializeField] Text Turn;

	void Update () {
		RedNum.text = "Red: " + osero.CountStone(Const.Color.Red);
		BlueNum.text = "Blue: " + osero.CountStone(Const.Color.Blue);
		WhiteNum.text = "Black: " + osero.CountStone(Const.Color.Black);

		switch (osero.BoardColor) {
		case Const.Color.Red:
			Turn.text = "Red TURN";
			break;
		case Const.Color.Blue:
			Turn.text = "Blue TURN";
			break;
		}
	}
}
