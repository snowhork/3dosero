using UnityEngine;
using System.Collections;

public class RayController : MonoBehaviour {

	public GameObject pre_stone;
	float time_count;
	bool first_flag = true;
	bool changed_turn = false;

	public GameObject yellow_hit;
	public GameObject black_hit;

	void reset_pre_stone(){
		if (!first_flag) {
			if (Osero.instance.get_stone_status (pre_stone.GetComponent<Position> ()).state == StoneStatus.Status.White) {
				pre_stone.GetComponent<Renderer> ().material = Osero.instance.white_material;				
			}
			time_count = 0;
		}
	}

	// Use this for initialization
	void Start () {
		time_count = 0;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 rayposition = transform.localPosition;
		//rayposition.y += 0.5f;
		Ray ray = new Ray (rayposition, transform.forward);
		RaycastHit hit;


//		if (Osero.instance.ai.myturn) {
//			//print (Osero.instance.ai.max_position.x.ToString() + Osero.instance.ai.max_position.y + Osero.instance.ai.max_position.z);
//			if (Osero.instance.ai.myturn && Osero.instance.ai.max_position == null) {	
//				print (Osero.instance.ai.max_position.x.ToString() + Osero.instance.ai.max_position.y + Osero.instance.ai.max_position.z);
//				
//				Osero.instance.set_stone (Osero.instance.get_stone_status (Osero.instance.ai.max_position));
//				Osero.instance.ai.turn_end ();
//				StartCoroutine (Wait ());
//			}
//			return;
//		}

		if (Physics.Raycast (ray, out hit) && !changed_turn) {
			Debug.DrawLine (ray.origin, hit.point, Color.red);
			GameObject stone = hit.collider.gameObject;
			if (stone.tag == "Stone") {
				Position position = stone.GetComponent<Position> ();
				if (Osero.instance.get_stone_status (position).state == StoneStatus.Status.White) {
					if (first_flag) {
						pre_stone = stone;
						first_flag = false;
					}
					if (Osero.instance.set_stone (Osero.instance.get_stone_status (position), Osero.SetStoneMode.Settable)) {
						if (time_count == 0) {
							Destroy (Instantiate (yellow_hit, stone.transform.position, Quaternion.identity), 1f);
							stone.GetComponent<Renderer> ().material = Osero.instance.yellow_material;
						}
						if (time_count >= 1f) {
							Osero.instance.set_stone (Osero.instance.get_stone_status (position));
							StartCoroutine ("Wait");
							time_count = 0;
						}
					} else {
						if (time_count == 0) {
							Destroy (Instantiate (black_hit, stone.transform.position, Quaternion.identity), 1f);	
						}
					}
					if (position.isequal(pre_stone.GetComponent<Position>())) {
						time_count += Time.deltaTime;
					}
				} else {
					reset_pre_stone ();
				}
				pre_stone = stone;
			} else {
				reset_pre_stone ();
			}
		} else {
			reset_pre_stone();
		}
	}

	IEnumerator Wait() {
		changed_turn = true;
		yield return new WaitForSeconds (6f);
		changed_turn = false;
	}
}
