using UnityEngine;
using System.Collections;

public class Position : MonoBehaviour {

	public int x;
	public int y;
	public int z;
	
	public Position(int x = -100, int y = -100, int z = -100) {
		this.x = x;
		this.y = y;
		this.z = z;
	}
	public void set_position(int x, int y, int z) {
		this.x = x;
		this.y = y;
		this.z = z;
	}
	public bool isequal(Position other) {
		return x == other.x && y == other.y && z == other.z;
	}

}
