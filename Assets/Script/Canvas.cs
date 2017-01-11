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
		red_num.text = "Red: " + Osero.Instance.RedStonesNum;
		blue_num.text = "Blue: " + Osero.Instance.BlueStonesNum;
		white_num.text = "Bluck: " + Osero.Instance.BlackStonesNum;

		switch (Osero.Instance.mycolor) {
		case StoneStatus.Status.Red:
			turn.text = "Red TURN";
			break;
		case StoneStatus.Status.Blue:
			turn.text = "Blue TURN";
			break;
		}
	}
}
