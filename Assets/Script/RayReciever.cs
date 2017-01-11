using UnityEngine;
using System.Collections;

public abstract class RayReciever : MonoBehaviour {
	protected bool rayon = false;
	protected float raycnt = 0f;

	protected void Update() {
		if (rayon) {
			raycnt += Time.deltaTime;
		}
	}

	protected virtual void SwitchToRayOn(RayCaster caster) {
		raycnt = 0f;	
	}

	protected virtual void SwitchToRayOff(RayCaster caster) {
		raycnt = 0f;
	}


	public virtual void RayOn(RayCaster caster) {
		if (!rayon) {
			SwitchToRayOn (caster);
		}
		rayon = true;
	}

	public virtual void RayOff(RayCaster caster) {
		if (rayon) {
			SwitchToRayOff (caster);
		}
		rayon = false;
	}
}
