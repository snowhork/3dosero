using UnityEngine;
using System.Collections;

public class Gamenode : MonoBehaviour {

	public StoneStatus[,,] stone_statuses;
	public int score;
	public Gamenode(StoneStatus[,,] stone_statuses) {
		this.stone_statuses = new StoneStatus[Osero.width_num*2, Osero.width_num*2, Osero.width_num*2];
		score = 0;
		for (int x = 0; x < Osero.width_num * 2; x++) {
			for (int y = 0; y < Osero.width_num * 2; y++) {
				for (int z = 0; z < Osero.width_num * 2; z++) {
					this.stone_statuses [x, y, z] = new StoneStatus (x, y, z);
					this.stone_statuses [x, y, z].set_state (stone_statuses [x, y, z].state);
					score += evaluate (x, y, z, this.stone_statuses [x, y, z].state);
				}
			}
		}
	}

	public int rescore() {
		int score = 0;

		for (int x = 0; x < Osero.width_num * 2; x++) {
			for (int y = 0; y < Osero.width_num * 2; y++) {
				for (int z = 0; z < Osero.width_num * 2; z++) {
					score += evaluate (x, y, z, this.stone_statuses [x, y, z].state);
				}
			}
		}
		this.score = score;
		return score;
	}

	int evaluate(int x, int y, int z, StoneStatus.Status color) {
		int sumi = 0;
		int score = 0;
		if (x == Osero.instance.wall_x_min || x == Osero.instance.wall_x_max - 1) {
			sumi++;
		}
		if (y == Osero.instance.wall_y_min || y == Osero.instance.wall_y_max - 1) {
			sumi++;
		}
		if (z == Osero.instance.wall_z_min || z == Osero.instance.wall_z_max - 1) {
			sumi++;
		}
		switch (sumi) {
		case 0:
			score = 1;
			break;
		case 1:
			score = 2;
			break;
		case 2:
			score = 4;
			break;
		case 3:
			score = 8;
			break;
		}
		switch (color) {
		case StoneStatus.Status.Red:
			score *= -1;
			break;
		case StoneStatus.Status.Blue:
			score *= 1;
			break;
		case StoneStatus.Status.White:
			score *= 0;
			break;
		case StoneStatus.Status.None:
			score *= 0;
			break;
		}
		return score;
	}
}
