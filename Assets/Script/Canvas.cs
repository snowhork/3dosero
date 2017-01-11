using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Canvas : MonoBehaviour {

	public Text red_num;
	public Text white_num;
	public Text blue_num;
	public Text turn;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		red_num.text = "Red: " + Osero.Instance.CountStone(Const.Color.Red);
		blue_num.text = "Blue: " + Osero.Instance.CountStone(Const.Color.Blue);
		white_num.text = "Black: " + Osero.Instance.CountStone(Const.Color.Black);

		switch (Osero.Instance.BoardColor) {
		case Const.Color.Red:
			turn.text = "Red TURN";
			break;
		case Const.Color.Blue:
			turn.text = "Blue TURN";
			break;
		}
	}
}
