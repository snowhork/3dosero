using UnityEngine;
using System.Collections;

public class Ai : MonoBehaviour {

	public bool myturn;
	public bool deciding;
	public Position max_position;
	// Use this for initialization
	void Start () {
		myturn = false;
		deciding = false;
		max_position = null;
	}
	
	// Update is called once per frame
	void Update () {
		if (myturn) {
			print (myturn);
			if (!deciding) {
				print (deciding);
//				StartCoroutine (decision ());
				deciding = true;
			}
		}
	}

	public void turn_end() {
		max_position = null;
		deciding = false;
		myturn = false;
	}


//	IEnumerator decision() {
//		Gamenode max_game_node = null;
//		Position max_pos = null;
//		bool first_flag = true;
//		for (int x = 0; x < Osero.Instance.WidthX; x++)
//			for (int y = 0; y < Osero.Instance.WidthY; y++)
//				for (int z = 0; z < Osero.Instance.WidthZ; z++) {
//					yield return new WaitForSeconds (0.02f);
//
//					Position position = new Position (x, y, z);
//					if (Osero.Instance.set_stone (Osero.Instance.get_stone_status (position), Osero.SetStoneMode.Settable)) {
//						yield return new WaitForSeconds (0.02f);
//						//print (x.ToString() + y + z);
//						Gamenode gamenode = new Gamenode (Osero.Instance.stone_statuses);
//						Osero.Instance.set_stone (gamenode.stone_statuses [x, y, z], Osero.SetStoneMode.Virtual, gamenode.stone_statuses);
//											
//						int score = gamenode.rescore ();						
//						if (first_flag) {
//							max_game_node = gamenode;
//							max_pos = position;
//							first_flag = false;
//						}
//						else if (score > max_game_node.score) {
//							max_game_node = gamenode;
//							max_pos = position;
//						}
//					}
//				}
//		print ("max:" + max_game_node.score);
//		this.max_position = max_pos;
//		
//	}
}
