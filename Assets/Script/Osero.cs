using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Osero : MonoBehaviour {
	
	[SerializeField] GameObject stone_pref;
	[SerializeField] GameObject frame_pref;

	Ai ai = new Ai();

	Position widthNum = new Position (2, 2, 2);
	public Stone[,,] Stones;
    private Board _board;
    public Board board { get { return _board; }}

	const float stone_scaling = 0.85f;
	const float stone_start_scaling = 0.35f;
	const float frame_width = 0.04f;
	float width = 2.2f;

	public Const.Color BoardColor {
		get {
			return board.color;
		}
	}

	void InstantiateFrame(Stone stone) {
		Vector3[] directions = new Vector3[3] { 
			new Vector3 (width, frame_width, frame_width),
			new Vector3 (frame_width, width, frame_width),
			new Vector3 (frame_width, frame_width, width),
		};
		for (int i = 0; i < 3; i++) {
			var frame = Instantiate (frame_pref);
			frame.transform.position = stone.transform.position;
			frame.transform.rotation = Quaternion.identity;
			frame.transform.localScale = directions [i];
		}		
	}

	public void SetStone(Position p, Const.Color color) {
		var stone = Stones [p.x, p.y, p.z];
	    stone.SetOsero(this);
		stone.ChangeColor (color);		
	}

	void set_stone_scale(Stone stone) {
		float[] abs_position = new float[3] { Mathf.Abs (stone.transform.position.x), Mathf.Abs (stone.transform.position.y), Mathf.Abs (stone.transform.position.z) };
		float max_position = -width * board.MaxWidth;
		for (int i = 0; i < 3; i++) {
			if (max_position < abs_position [i])
				max_position = abs_position [i];
		}
		float max_depth = (max_position - width / 2) / width;
		stone.transform.localScale = Vector3.one*(stone_start_scaling + max_depth * stone_scaling);
	}

	public void initialize() {
		_board = new Board (widthNum);
		Stones = board.WidthArray<Stone> ();
		Vector3 start_position = Vector3.one * (-width * board.MaxWidth + width / 2f);

		foreach (Position p in board.StonesIterator) {
			var stone = Instantiate (stone_pref).GetComponent<Stone>();
			Stones [p.x, p.y, p.z] = stone;

			stone.transform.position = start_position + new Vector3 (p.x, p.y, p.z) * width;
			stone.transform.rotation = Quaternion.identity;
			set_stone_scale (stone);
			InstantiateFrame (stone);

			MaterialManager.Instance.SetMaterial (Const.Color.Black, stone.gameObject);
			var info = stone.Info;
			info.initialize (p);
			board.SetStoneInfo (p, info);

		}
		SetStone (new Position (1, 1, 1), Const.Color.Red);
		SetStone (new Position (2, 1, 2), Const.Color.Red);
		SetStone (new Position (1, 2, 1), Const.Color.Red);
		SetStone (new Position (2, 2, 2), Const.Color.Red);
		SetStone (new Position (1, 1, 2), Const.Color.Blue);
		SetStone (new Position (2, 1, 1), Const.Color.Blue);
		SetStone (new Position (1, 2, 2), Const.Color.Blue);
		SetStone (new Position (2, 2, 1), Const.Color.Blue);
	}




//	public void Changeturn(Const.Color color) {
//		board.color = color;
//		AiTurn ();
//		board.color = Const.antiColor(color);
//
//	}

	public int CountStone(Const.Color color) {
		return board.CountStone (color);
	}

	bool exists_set_stone() {
		
//		for (int x = Osero.instance.wall_x_min; x < Osero.instance.wall_x_max; x++)
//			for (int y = Osero.instance.wall_y_min; y < Osero.instance.wall_y_max; y++)
//				for (int z = Osero.instance.wall_z_min; z < Osero.instance.wall_z_max; z++) {
//
//					Position position = new Position (x, y, z);
//					if (Osero.instance.set_stone (Osero.instance.get_stone_status (position), Osero.SetStoneMode.Settable)) {
//						return true;
//					}
//				}
		return false;
	}

	public void AiTurn() {
		Position position = ai.decision (board);
		SetStone (position, Const.Color.Blue);
	}


}
