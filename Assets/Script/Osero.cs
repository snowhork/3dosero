
using UnityEngine;
using System.Collections;

public class Osero : MonoBehaviour {

	public enum SetStoneMode {
		Real,
		Virtual,
		Settable,
	};

	public static Osero instance;
	public GameObject stone;
	public Material white_material;
	public Material blue_material;
	public Material red_material;
	public Material yellow_material;

	public GameObject red_hit;
	public GameObject blue_hit;
	public GameObject blue_sprays;
	public GameObject red_sprays;

	public GameObject frame;
	public Ai ai;

	public const int width_num = 3;
	public int wall_x_min = 1;
	public int wall_x_max = width_num*2 - 1;
	public int wall_y_min = 1;
	public int wall_y_max = width_num*2 - 1;
	public int wall_z_min = 1;
	public int wall_z_max = width_num*2 - 1;
	public int white_stones_num = 0;
	public int red_stones_num = 0;
	public int blue_stones_num = 0;

	public StoneStatus.Status mycolor = StoneStatus.Status.Red;
	public StoneStatus[,,] stone_statuses;

	const float stone_scaling = 0.85f;
	const float stone_start_scaling = 0.35f;
	const float frame_width = 0.04f;
	float width = 2.2f;

	void Start () {
		if (instance == null) {
			instance = this;
		}
		else {
			return;
		}
		stone_statuses = new StoneStatus[width_num*2, width_num*2, width_num*2];
		for (int x = 0; x < width_num * 2; x++) {
			for (int y = 0; y < width_num * 2; y++) {
				for (int z = 0; z < width_num * 2; z++) {
					stone_statuses [x, y, z] = new StoneStatus (x, y, z);
				}
			}
		}

		Vector3 start_position = Vector3.one * (-width * width_num + width / 2f);
		for (int x = wall_x_min; x < wall_x_max; x++)
			for (int y = wall_y_min; y < wall_y_max; y++)
				for (int z = wall_z_min; z < wall_z_max; z++) {
					Vector3 world_position = start_position + new Vector3 (x, y, z) * width;

					float[] abs_position = new float[3] { Mathf.Abs (world_position.x), Mathf.Abs (world_position.y), Mathf.Abs (world_position.z) };
					float max_position = -width * width_num;
					for (int i = 0; i < 3; i++) {
						if (max_position < abs_position [i])
							max_position = abs_position [i];
					}
					float max_depth = (max_position - width / 2) / width;

					GameObject new_stone = (GameObject)Instantiate (stone, world_position, Quaternion.identity);
					Vector3[] directions = new Vector3[3] { 
						new Vector3 (width, frame_width, frame_width),
						new Vector3 (frame_width, width, frame_width),
						new Vector3 (frame_width, frame_width, width),
					};
					for (int i = 0; i < 3; i++) {
						GameObject new_frame = (GameObject)Instantiate (frame, 
							world_position, Quaternion.identity);
						//new_frame.transform.localScale = new Vector3 (frame_width, frame_width, width/4);
						new_frame.transform.localScale = directions [i];
						//new_frame.transform.rotation = Quaternion.Euler (directions [i] * 90);
						new_frame.name = get_frame_name (new Position (x, y, z), i);
					}

					new_stone.GetComponent<Position> ().set_position(x,y,z);
					new_stone.name = get_stone_name (new Position(x,y,z));
					new_stone.transform.localScale = Vector3.one*(stone_start_scaling + max_depth * stone_scaling);
					new_stone.GetComponent<Renderer> ().material = white_material;

					stone_statuses [x, y, z].set_state (StoneStatus.Status.White);

				}
		white_stones_num =
		(wall_x_max - wall_x_min) * 
		(wall_y_max - wall_y_min) * 
		(wall_z_max - wall_z_min);
		get_stone (stone_statuses [2, 2, 2], StoneStatus.Status.Red);
		get_stone (stone_statuses [3, 2, 3], StoneStatus.Status.Red);
		get_stone (stone_statuses [2, 3, 2], StoneStatus.Status.Red);
		get_stone (stone_statuses [3, 3, 3], StoneStatus.Status.Red);

		get_stone (stone_statuses [2, 2, 3], StoneStatus.Status.Blue);
		get_stone (stone_statuses [3, 2, 2], StoneStatus.Status.Blue);
		get_stone (stone_statuses [2, 3, 3], StoneStatus.Status.Blue);
		get_stone (stone_statuses [3, 3, 2], StoneStatus.Status.Blue);
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
						if (x < wall_x_min || x >= wall_x_max ||
							y < wall_y_min || y >= wall_y_max || 
							z < wall_z_min || z >= wall_z_max) {
							break;
						}
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
			white_stones_num--;	
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
			white_stones_num++;	
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

	string get_frame_name(Position position, int i) {
		return "frame" + position.x + "," + position.y + "," + position.z + "," + i;
	}

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
		
		for (int x = Osero.instance.wall_x_min; x < Osero.instance.wall_x_max; x++)
			for (int y = Osero.instance.wall_y_min; y < Osero.instance.wall_y_max; y++)
				for (int z = Osero.instance.wall_z_min; z < Osero.instance.wall_z_max; z++) {

					Position position = new Position (x, y, z);
					if (Osero.instance.set_stone (Osero.instance.get_stone_status (position), Osero.SetStoneMode.Settable)) {
						return true;
					}
				}
		return false;
	}
}
