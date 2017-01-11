using UnityEngine;
using System.Collections;

public class RayController : RayCaster {

	float time_count;
	Const.Color color = Const.Color.Red;

	public Const.Color PlayerColor { get { return color;} }

	void Start () {
		time_count = 0;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 rayPosition = transform.localPosition;
		//rayposition.y += 0.5f;
		var ray = new Ray (rayPosition, transform.forward);
		RayCast (ray);
	}



//			if (stone.tag == "Stone") {
//				Position position = stone.GetComponent<Position> ();
//				if (Osero.instance.get_stone_status (position).state == StoneStatus.Status.White) {
//					if (first_flag) {
//						pre_stone = stone;
//						first_flag = false;
//					}
//					if (Osero.instance.set_stone (Osero.instance.get_stone_status (position), Osero.SetStoneMode.Settable)) {
//						if (time_count == 0) {
//							Destroy (Instantiate (yellow_hit, stone.transform.position, Quaternion.identity), 1f);
//							stone.GetComponent<Renderer> ().material = Osero.instance.yellow_material;
//						}
//						if (time_count >= 1f) {
//							Osero.instance.set_stone (Osero.instance.get_stone_status (position));
//							StartCoroutine ("Wait");
//							time_count = 0;
//						}
//					} else {
//						if (time_count == 0) {
//							Destroy (Instantiate (black_hit, stone.transform.position, Quaternion.identity), 1f);	
//						}
//					}
//					if (position.isequal(pre_stone.GetComponent<Position>())) {
//						time_count += Time.deltaTime;
//					}
//				} else {
//					reset_pre_stone ();
//				}
//				pre_stone = stone;
//			} else {
//				reset_pre_stone ();
//			}
//		} else {
//			reset_pre_stone();
//		}
//	}
//
//	IEnumerator Wait() {
//		changed_turn = true;
//		yield return new WaitForSeconds (6f);
//		changed_turn = false;
//	}
}
