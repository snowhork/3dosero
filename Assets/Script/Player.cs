using UnityEngine;
using System.Collections;

public class Player : RayCaster {	
	Const.Color color = Const.Color.Red;

	public Const.Color PlayerColor { get { return color;} }

	void Update () {
		Vector3 rayPosition = transform.localPosition;
		var ray = new Ray (rayPosition, transform.forward);
		RayCast (ray);
	}

	public void TurnEnd() {
		Osero.Instance.Changeturn (Const.antiColor(color));		
		color = Const.antiColor (color);
	}
}
