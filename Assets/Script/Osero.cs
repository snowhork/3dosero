
using UnityEngine;
using System.Collections;

public class Osero : MonoBehaviour {

	public static Osero instance;
	public GameObject stone;
	public GameObject frame;
	const float stone_scaling = 0.6f;
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

	const float stone_start_scaling = 0.25f;
	const float frame_width = 0.01f;
	float width = 2.2f;

	// Use this for initialization
	void Start () {
		if (instance == null) {
			instance = this;
		}
		Vector3 start_position = Vector3.one * (-width * width_num + width / 2f);
		for (int x = wall_x_min; x < wall_x_max; x++)
			for (int y = wall_y_min; y < wall_y_max; y++)
				for (int z = wall_z_min; z < wall_z_max; z++) {
					Vector3 position = start_position + new Vector3 (x, y, z) * width;

					float[] abs_position = new float[3] { Mathf.Abs (position.x), Mathf.Abs (position.y), Mathf.Abs (position.z) };

					float max_position = -width * width_num;
					for (int i = 0; i < 3; i++) {
						if (max_position < abs_position [i])
							max_position = abs_position [i];
					}
					float max_depth = (max_position - width / 2) / width;
					
					GameObject new_stone = (GameObject)Instantiate (stone, position, Quaternion.identity);
					new_stone.name = get_stone_name (x, y, z);
					new_stone.transform.localScale = Vector3.one*(stone_start_scaling + max_depth * stone_scaling);
					StoneStatus stonestatus = new_stone.GetComponent<StoneStatus> ();
					stonestatus.x = x;
					stonestatus.y = y;
					stonestatus.z = z;

					GameObject x_frame = (GameObject)Instantiate (frame, position + new Vector3(width/2, 0, 0), Quaternion.identity);
					x_frame.transform.localScale = new Vector3 (width, frame_width, frame_width);
					GameObject y_frame = (GameObject)Instantiate (frame, position + new Vector3(0, width/2, 0), Quaternion.identity);
					y_frame.transform.localScale = new Vector3 (frame_width, width, frame_width);
					GameObject z_frame = (GameObject)Instantiate (frame, position + new Vector3(0, 0, width/2), Quaternion.identity);
					z_frame.transform.localScale = new Vector3 (frame_width, frame_width, width);
				}
		white_stones_num =
		(wall_x_max - wall_x_min) * 
		(wall_y_max - wall_y_min) * 
		(wall_z_max - wall_z_min);
		get_red_stone( GameObject.Find (get_stone_name (2,2,2)));
		get_red_stone( GameObject.Find (get_stone_name (3,2,3)));
		get_red_stone( GameObject.Find (get_stone_name (2,3,2)));
		get_red_stone( GameObject.Find (get_stone_name (3,3,3)));
		get_blue_stone( GameObject.Find (get_stone_name (2,2,3)));
		get_blue_stone( GameObject.Find (get_stone_name (3,2,2)));
		get_blue_stone( GameObject.Find (get_stone_name (2,3,3)));
		get_blue_stone( GameObject.Find (get_stone_name (3,3,2)));
			
	//	get_red_stone( GameObject.Find (Osero.get_stone_name (2,2,2)));
	//	get_blue_stone( GameObject.Find (Osero.get_stone_name (2,2,3)));
		
	//	get_red_stone( GameObject.Find (Osero.get_stone_name (3,3,3)));
	//	get_blue_stone( GameObject.Find (Osero.get_stone_name (3,3,2)));

		//get_blue_stone( GameObject.Find (Osero.get_stone_name (3,3,3)));
	
	}
	void down_stones_num(GameObject stone) {
		StoneStatus stonestatus = stone.GetComponent<StoneStatus> ();
		switch (stonestatus.state) {
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

	string get_stone_name(int x, int y, int z) {
		return x.ToString() + "," + y.ToString() + "," + z.ToString() + ",";
	}
	void get_red_stone(GameObject stone) {
		down_stones_num (stone);
		stone.GetComponent<Renderer> ().material.color = Color.red;
		StoneStatus stonestatus = stone.GetComponent<StoneStatus> ();
		red_stones_num++;
		stonestatus.set_red ();
	}
	void get_blue_stone(GameObject stone) {
		down_stones_num (stone);
		stone.GetComponent<Renderer> ().material.color = Color.blue;
		StoneStatus stonestatus = stone.GetComponent<StoneStatus> ();
		blue_stones_num++;
		stonestatus.set_blue ();
	}
	public void set_stone(GameObject stone) {
		bool find_enemy_stone = false;

		StoneStatus stonestatus = stone.GetComponent<StoneStatus> ();

		for (int dx = -1; dx <= 1; dx++) {
			for (int dy = -1; dy <= 1; dy++) {
				for (int dz = -1; dz <= 1; dz++) {
					int x = stonestatus.x;
					int y = stonestatus.y;
					int z = stonestatus.z;
					if (dx == 0 && dy == 0 && dz == 0) {
						continue;
					}
					while (true) {
						x += dx;
						y += dy;
						z += dz;
						if (x < wall_x_min || x >= wall_x_max ||
							y < wall_y_min || y >= wall_y_max || 
							z < wall_z_min || z >= wall_z_max) {
							break;
						}

						GameObject next_stone = GameObject.Find (get_stone_name (x, y, z));
						StoneStatus next_stonestatus = next_stone.GetComponent<StoneStatus> ();

						if (next_stonestatus.state == StoneStatus.Status.White) {
							break;
						}

						if (next_stonestatus.state == mycolor) {
							while (true) {
								x -= dx;
								y -= dy;
								z -= dz;
								if (x == stonestatus.x &&
									y == stonestatus.y &&
									z == stonestatus.z) {
									break;
								}
								GameObject before_stone = GameObject.Find (get_stone_name (x, y, z));
								find_enemy_stone = true;
								switch (mycolor) {
								case StoneStatus.Status.Red:
									get_red_stone (before_stone);
									break;
								case StoneStatus.Status.Blue:
									get_blue_stone (before_stone);
									break;
								}
							}
							break;
						}
					}
				}
			}
		}
		if (find_enemy_stone) {
			switch (mycolor) {
			case StoneStatus.Status.Red:
				get_red_stone (stone);
				break;
			case StoneStatus.Status.Blue:
				get_blue_stone (stone);
				break;
			}
			Changeturn ();	
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




