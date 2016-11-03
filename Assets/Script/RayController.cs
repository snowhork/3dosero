using UnityEngine;
using System.Collections;

public class RayController : MonoBehaviour {

	public GameObject pre_stone;
	float time_count;
	bool first_flag = true;
	bool changed_turn = false;

	void reset_pre_stone(){
		if (!first_flag) {
			if (pre_stone.GetComponent<StoneStatus> ().state == StoneStatus.Status.White) {
				pre_stone.GetComponent<Renderer> ().material.color = Color.white;
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
			if (hit.collider.gameObject.tag == "Stone" && stone.GetComponent<StoneStatus> ().state == StoneStatus.Status.White) {

				if (first_flag) {
					pre_stone = stone;
					first_flag = false;
				}
					
				StoneStatus stonestatus = stone.GetComponent<StoneStatus> ();
				stone.GetComponent<Renderer> ().material.color = Color.yellow;

				if (stonestatus.isequal (pre_stone.GetComponent<StoneStatus> ())) {
					time_count += Time.deltaTime;
				} else {
					reset_pre_stone ();

				}

				if (time_count >= 1f) {
					Osero.instance.set_stone (stone);
					StartCoroutine("Wait");
					time_count = 0;
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
