using UnityEngine;
using System.Collections;

public class RayController : MonoBehaviour {

	public GameObject pre_stone;
	float time_count;
	bool first_flag = true;
	bool changed_turn = false;

	void reset_pre_stone(){
		if (!first_flag) {
			if (Osero.instance.get_stone_status (pre_stone.GetComponent<Position> ()).state == StoneStatus.Status.White) {
				pre_stone.GetComponent<Renderer> ().material.color = Color.gray;
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
		if (Physics.Raycast (ray, out hit) && !changed_turn) {
			Debug.DrawLine (ray.origin, hit.point, Color.black);
			GameObject stone = hit.collider.gameObject;
			print (hit.collider.gameObject.name);
			if (hit.collider.gameObject.tag == "Stone") {
				
				Position position = stone.GetComponent<Position> ();
				if (Osero.instance.get_stone_status (position).state == StoneStatus.Status.White) {
					if (first_flag) {
						pre_stone = stone;
						first_flag = false;
					}
					if (position.isequal (pre_stone.GetComponent<Position> ())) {
						time_count += Time.deltaTime;
					} else {
						reset_pre_stone ();
					}
					stone.GetComponent<Renderer> ().material.color = Color.yellow;

					if (time_count >= 1f) {
						Osero.instance.set_stone (Osero.instance.get_stone_status (position));
						StartCoroutine ("Wait");
						time_count = 0;
					}
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
