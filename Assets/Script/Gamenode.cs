using UnityEngine;
using System.Collections;

public class Gamenode : MonoBehaviour {

	public StoneStatus[,,] stone_statuses;
	public int score;
	public Gamenode(StoneStatus[,,] stone_statuses) {
//		this.stone_statuses = new StoneStatus[Osero.Instance.WidthX*2, Osero.Instance.WidthY*2, Osero.Instance.WidthZ*2];
		score = 0;
//		foreach (Position p in Osero.Instance.StonesIterator) {
//			this.stone_statuses [p.x, p.y, p.z] = new StoneStatus (p.x, p.y, p.z);
//			this.stone_statuses [p.x, p.y, p.z].set_state (stone_statuses [p.x, p.y, p.z].state);
//			score += evaluate (p.x, p.y, p.z, this.stone_statuses [p.x, p.y, p.z].state);
//		}
	}

	public int rescore() {
		int score = 0;
//		foreach (Position p in Osero.Instance.StonesIterator) {
//			score += evaluate (p.x, p.y, p.z, this.stone_statuses [p.x, p.y, p.z].state);
//		}
//		this.score = score;
		return score;
	}

	int evaluate(int x, int y, int z, StoneStatus.Status color) {
		int sumi = 0;
		int score = 0;
//		if (x == Osero.instance.wall_x_min || x == Osero.instance.wall_x_max - 1) {
//			sumi++;
//		}
//		if (y == Osero.instance.wall_y_min || y == Osero.instance.wall_y_max - 1) {
//			sumi++;
//		}
//		if (z == Osero.instance.wall_z_min || z == Osero.instance.wall_z_max - 1) {
//			sumi++;
//		}
//		switch (sumi) {
//		case 0:
//			score = 1;
//			break;
//		case 1:
//			score = 2;
//			break;
//		case 2:
//			score = 4;
//			break;
//		case 3:
//			score = 8;
//			break;
//		}
//		switch (color) {
//		case StoneStatus.Status.Red:
//			score *= -1;
//			break;
//		case StoneStatus.Status.Blue:
//			score *= 1;
//			break;
//		case StoneStatus.Status.White:
//			score *= 0;
//			break;
//		case StoneStatus.Status.None:
//			score *= 0;
//			break;
//		}
//		return score;
		return 0;
	}
}
