using UnityEngine;
using System.Collections;

public class RayCaster : MonoBehaviour {
	protected RayReciever reciever = null;

	void RayOn(RayReciever reciever) {
		if (reciever == null) {
			return;
		}
		reciever.RayOn (this);
	}

	void RayOff(RayReciever reciever) {
		if (reciever == null) {
			return;
		}
		reciever.RayOff (this);

	}

	protected RaycastHit RayCast(Ray ray) {
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			Debug.DrawLine (ray.origin, hit.point, Color.red);
			var reciever = hit.collider.GetComponent (typeof(RayReciever)) as RayReciever;

			if (this.reciever == reciever) {
				return hit;
			}

			RayOff (this.reciever);
			RayOn (reciever);
			this.reciever = reciever;	
		} else {
			RayOff (this.reciever);
			this.reciever = null;
		}
		return hit;
	}
}
