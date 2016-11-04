
using UnityEngine;
using System.Collections;

public class Osero : MonoBehaviour {

	public static Osero instance;
	public GameObject stone;
	public Material white_material;
	public Material blue_material;
	public Material red_material;
	public GameObject frame;

	const int width_num = 3;
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

	const float stone_scaling = 0.8f;
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
		for (int x = 0; x < width * 2; x++) {
			for (int y = 0; y < width * 2; y++) {
				for (int z = 0; z < width * 2; z++) {
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
					new_stone.GetComponent<Position> ().set_position(x,y,z);
					new_stone.name = get_stone_name (new Position(x,y,z));
					new_stone.transform.localScale = Vector3.one*(stone_start_scaling + max_depth * stone_scaling);
					new_stone.GetComponent<Renderer> ().material = white_material;

					stone_statuses [x, y, z].set_state (StoneStatus.Status.White);

					GameObject x_frame = (GameObject)Instantiate (frame, world_position + new Vector3(width/2, 0, 0), Quaternion.identity);
					x_frame.transform.localScale = new Vector3 (width, frame_width, frame_width);
					GameObject y_frame = (GameObject)Instantiate (frame, world_position + new Vector3(0, width/2, 0), Quaternion.identity);
					y_frame.transform.localScale = new Vector3 (frame_width, width, frame_width);
					GameObject z_frame = (GameObject)Instantiate (frame, world_position + new Vector3(0, 0, width/2), Quaternion.identity);
					z_frame.transform.localScale = new Vector3 (frame_width, frame_width, width);
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

	public void set_stone(StoneStatus stone_status) {
		bool find_enemy_stone = false;

		Position position = stone_status.position;

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
						StoneStatus next_stonestatus = stone_statuses [x, y, z];

						if (next_stonestatus.state == StoneStatus.Status.White) {
							break;
						}

						if (next_stonestatus.state == mycolor) {
							while (true) {
								x -= dx; y -= dy; z -= dz;
								if (position.isequal(x,y,z)) {
									break;
								}
								get_stone (stone_statuses [x, y, z], mycolor);
								find_enemy_stone = true;
							}
							break;
						}
					}
				}
			}
		}
		if (find_enemy_stone) {
			get_stone (stone_statuses [position.x, position.y, position.z], mycolor);
			Changeturn ();				
		}
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
		return x.ToString() + "," + y.ToString() + "," + z.ToString() + ",";
	}

	string get_stone_name(Position position) {
		return position.x.ToString() + "," + position.y.ToString() + "," + position.z.ToString() + ",";
	}

	void get_stone(StoneStatus status, StoneStatus.Status color) {
		down_stones_num (status.state);
		status.set_state (color);
		up_stones_num (color);
		Change_stone_material (color, status.position);		
	}

	void Change_stone_material (StoneStatus.Status color, Position position) {
		GameObject stone = GameObject.Find (get_stone_name (position));
		switch (color) {
		case StoneStatus.Status.Red:
			stone.GetComponent<Renderer> ().material = red_material;
			return;
		case StoneStatus.Status.White:
			stone.GetComponent<Renderer> ().material = white_material;
			break;
		case StoneStatus.Status.Blue:
			stone.GetComponent<Renderer> ().material = blue_material;
			break;
		}
	}	

	void Changeturn() {
		switch (mycolor) {
		case StoneStatus.Status.Red:
			mycolor = StoneStatus.Status.Blue;
			break;
		case StoneStatus.Status.Blue:
			mycolor = StoneStatus.Status.Red;
			break;
		}
	}
}
