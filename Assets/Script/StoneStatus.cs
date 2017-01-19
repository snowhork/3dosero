using UnityEngine;
using System.Collections;

public class StoneStatus : MonoBehaviour {

	public enum Status {
		White,
		Red,
		Blue,
		None,
	}

	public Position position;	
	public Status state;

	public StoneStatus(int x, int y, int z) {
		position = new Position (x, y, z);
		state = Status.None;
	}

	public Color get_color() {
		switch (state) {
		case Status.White:
			return Color.white;
		case Status.Red:
			return Color.red;
		case Status.Blue:
			return Color.blue;
		}
		return Color.white;
	}
	public static Color get_color(Status color) {
		switch (color) {
		case Status.White:
			return Color.white;
		case Status.Red:
			return Color.red;
		case Status.Blue:
			return Color.blue;
		}
		return Color.white;
	}

	public void set_red() {
		state = Status.Red;
	}
	public void set_blue() {
		state = Status.Blue;
	}
	public void set_state(Status state) {
		this.state = state;
	}
	public void set_position(int x, int y, int z) {
		position.set_position (x, y, z);
	}
}
