using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Osero : SingletonMonoBehaviour<Osero> {

	public enum SetStoneMode {
		Real,
		Virtual,
		Settable,
	};
		
	[SerializeField] GameObject stone_pref;
	public Material white_material;
	public Material blue_material;
	public Material red_material;
	public Material yellow_material;

	public GameObject red_hit;
	public GameObject blue_hit;
	public GameObject blue_sprays;
	public GameObject red_sprays;

	public GameObject frame_pref;
	public Ai ai;

	Position widthNum = new Position (2, 2, 2);
	[SerializeField] int black_stones_num = 0;
	public int BlackStonesNum { get { return black_stones_num; } }
	[SerializeField] int red_stones_num = 0;
	public int RedStonesNum { get { return red_stones_num; } }
	[SerializeField] int blue_stones_num = 0;
	public int BlueStonesNum { get { return blue_stones_num; } }

	public StoneStatus.Status mycolor = StoneStatus.Status.Red;

	public Stone[,,] Stones;
	Board board;
	public StoneStatus[,,] stone_statuses;

	const float stone_scaling = 0.85f;
	const float stone_start_scaling = 0.35f;
	const float frame_width = 0.04f;
	float width = 2.2f;

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

	void initialize() {
		board = new Board (widthNum);
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


	void Start () {
		initialize ();
	
	}

	public bool set_stone(StoneStatus stone_status, SetStoneMode mode = SetStoneMode.Real, StoneStatus[,,] virtual_statuses = null) {
		bool find_enemy_stone = false;

		Position position = stone_status.position;

		if (!(stone_status.state == StoneStatus.Status.White)) {
			return false;
		}

		for (int dx = -1; dx <= 1; dx++) {
			for (int dy = -1; dy <= 1; dy++) {
				for (int dz = -1; dz <= 1; dz++) {
					int x = position.x;
					int y = position.y;
					int z = position.z;
					if (dx == 0 && dy == 0 && dz == 0) {
						continue;
					}
					while (true) {
						x += dx; y += dy; z += dz;
//						if (x < 0 || x >= WidthX ||
//							y < 0 || y >= WidthY || 
//							z < 0 || z >= WidthZ) {
//							break;
//						}
						StoneStatus next_stonestatus = null;
						switch (mode) {
						case SetStoneMode.Real:
							next_stonestatus = stone_statuses [x, y, z];
							break;
						case SetStoneMode.Virtual:
							next_stonestatus = virtual_statuses [x, y, z];							
							break;
						case SetStoneMode.Settable:
							next_stonestatus = stone_statuses [x, y, z];							
							break;
						}
						if (next_stonestatus.state == StoneStatus.Status.White) {
							break;
						}

						if (next_stonestatus.state == mycolor) {
							while (true) {
								x -= dx; y -= dy; z -= dz;
								if (position.isequal(x,y,z)) {
									break;
								}
								switch (mode) {
								case SetStoneMode.Real:
									get_stone (stone_statuses [x, y, z], mycolor, mode);
									GameObject sprays = null;
									switch (mycolor) {
									case StoneStatus.Status.Red:
										sprays = red_sprays;
										break;
									case StoneStatus.Status.Blue:
										sprays = blue_sprays;
										break;
									}
									Destroy (
										Instantiate (sprays, 
											GameObject.Find (get_stone_name (position)).transform.position,
											Quaternion.LookRotation (new Vector3 (dx, dy, dz))),
										2f);
									break;
								case SetStoneMode.Virtual:
									get_stone (virtual_statuses [x, y, z], mycolor, mode, virtual_statuses);
									break;
								}
								find_enemy_stone = true;
							}
							break;
						}
					}
				}
			}
		}
		if (find_enemy_stone) {
			switch (mode) {
			case SetStoneMode.Real:
				get_stone (stone_statuses [position.x, position.y, position.z], mycolor);
				Changeturn ();
				break;
			case SetStoneMode.Virtual:				
				get_stone (virtual_statuses [position.x, position.y, position.z], mycolor, mode, virtual_statuses);
				break;
			}

		}
		return find_enemy_stone;
	}

	public StoneStatus get_stone_status(Position position) {
		return stone_statuses [position.x, position.y, position.z];
	}

	void down_stones_num(StoneStatus.Status state) {
		switch (state) {
		case StoneStatus.Status.White:
			black_stones_num--;	
			break;
		case StoneStatus.Status.Red:
			red_stones_num--;
			break;
		case StoneStatus.Status.Blue:
			blue_stones_num--;
			break;
		}
	}

	void up_stones_num(StoneStatus.Status state) {
		switch (state) {
		case StoneStatus.Status.White:
			black_stones_num++;	
			break;
		case StoneStatus.Status.Red:
			red_stones_num++;
			break;
		case StoneStatus.Status.Blue:
			blue_stones_num++;
			break;
		}
	}
		
	string get_stone_name(int x, int y, int z) {
		return "stone" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",";
	}

	string get_stone_name(Position position) {
		return get_stone_name (position.x, position.y, position.z);
	}

//	string get_frame_name(Position position, int i) {
//		return "frame" + position.x + "," + position.y + "," + position.z + "," + i;
//	}

	void get_stone(StoneStatus status, StoneStatus.Status color, SetStoneMode mode = SetStoneMode.Real, StoneStatus[,,] virtual_status = null) {
		StoneStatus.Status oldcolor = status.state;
		status.set_state (color);		

		switch (mode) {
		case SetStoneMode.Real:
			Change_stone_material (color, status.position);
			down_stones_num (oldcolor);				
			up_stones_num (color);
			break;
		}
			
	}

	void Change_stone_material (StoneStatus.Status color, Position position) {
		GameObject stone = GameObject.Find (get_stone_name (position));
		switch (color) {
		case StoneStatus.Status.Red:
			Destroy(Instantiate (red_hit, stone.transform.position, Quaternion.identity), 3f);
			stone.GetComponent<Renderer> ().material = red_material;
			break;
		case StoneStatus.Status.White:
			stone.GetComponent<Renderer> ().material = white_material;
			break;
		case StoneStatus.Status.Blue:
			Destroy (Instantiate (blue_hit, stone.transform.position, Quaternion.identity),3f);
			stone.GetComponent<Renderer> ().material = blue_material;
			break;
		}
	
		/*for (int i = 0; i < 3; i++) {
			GameObject frame = GameObject.Find (get_frame_name (position, i));
			switch (color) {
			case StoneStatus.Status.Red:
				frame.GetComponent<Renderer> ().material = red_material;
				break;
			case StoneStatus.Status.White:
				frame.GetComponent<Renderer> ().material = white_material;
				break;
			case StoneStatus.Status.Blue:
				frame.GetComponent<Renderer> ().material = blue_material;
				break;
			}
		}*/

	}	

	void Changeturn() {
		switch (mycolor) {
		case StoneStatus.Status.Red:
			mycolor = StoneStatus.Status.Blue;
			if (exists_set_stone ()) {
				//ai.myturn = true;
			} else {
				mycolor = StoneStatus.Status.Red;
			}
			break;
		case StoneStatus.Status.Blue:
			mycolor = StoneStatus.Status.Red;
			if (exists_set_stone ()) {
				
			} else {
				mycolor = StoneStatus.Status.Blue;
			}
			break;
		}
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
}
