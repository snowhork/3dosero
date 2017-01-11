using UnityEngine;
using System.Collections;

public class StoneRayReciever : RayReciever {

	const float changeStoneCnt = 2f;
	Const.Color rayonColor;	

	void Update() {
		base.Update ();
		if (rayon && raycnt >= changeStoneCnt && settable) {
			foreach (StoneInfo stoneInfo in info.SetStone (rayonColor)) {
				Osero.Instance.SetStone (stoneInfo.position, rayonColor);
			}
		}
	}

	StoneInfo info { get { return GetComponent<Stone> ().Info; } } 
	bool settable { get { return info.Settable(rayonColor); } }
	Const.Color stoneColor { get { return info.color; } }

	protected override void SwitchToRayOn(RayCaster caster) {
		base.SwitchToRayOn (caster);
		rayonColor = caster.GetComponent<RayController> ().PlayerColor;
		if (stoneColor != Const.Color.Black) {
			return;
		}
		if (settable) {
			ParticleManager.Instance.HitEffect (Const.Color.Yellow, transform.position);
			MaterialManager.Instance.SetMaterial (Const.Color.Yellow, gameObject);	
		} else {
			ParticleManager.Instance.HitEffect (Const.Color.Black, transform.position);
		}
	}

	protected override void SwitchToRayOff(RayCaster caster) {
		base.SwitchToRayOff (caster);
		MaterialManager.Instance.SetMaterial (stoneColor, gameObject);
	}

	public override void RayOn(RayCaster caster) {
		base.RayOn (caster);
	}

	public override void RayOff(RayCaster caster) {
		base.RayOff (caster);
	}
}
