using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoneInfo {

	Position stone_position;
	Const.Color stone_color;
	Board stone_board;

	public Position position {
		get {
			return stone_position;
		}
		set {
			stone_position = value;
		}
	}
	public Const.Color color {
		get {
			return stone_color;
		}
		set {
			stone_color = value;
		}
	}
	public Board board {
		get {
			return stone_board;
		}
		set {
			stone_board = value;
		}
	}

    public StoneInfo() {
        stone_position = new Position ();
    }

    public bool Settable(Const.Color color) {
		return board.Settable (position, color);
	}

	public List<StoneInfo> SetStone(Const.Color color) {
		return board.SetStone (position, color);
	}

	public void initialize(Position p) {
		stone_position.set_position (p);
		color = Const.Color.Black;
	}

	public void set_position(int x, int y, int z) {
		stone_position.set_position (x, y, z);
	}
}
