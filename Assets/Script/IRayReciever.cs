using UnityEngine;
using System.Collections;

public interface IRayReciever {
	void RayOn (RayCaster caster);
	void RayOff (RayCaster caster);
}
