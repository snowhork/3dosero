using UnityEngine;
using System.Collections;

public class StoneStatus : MonoBehaviour {

	public enum Status {
		White,
		Red,
		Blue,
		None,
	}

	public int x;
	public int y;
	public int z;
	public Status state;
	// Use this for initialization
	public StoneStatus() {
		x = -1000;
		y = -1000;
		z = -1000;
		state = Status.White;
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool isequal(StoneStatus other) {
		return x == other.x && y == other.y && z == other.z;
	}

	public void set_red() {
		state = Status.Red;
	}
	public void set_blue() {
		state = Status.Blue;
	}
}
