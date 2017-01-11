using UnityEngine;
using System.Collections;

public class Stone : MonoBehaviour {

	StoneInfo stoneInfo;
	public StoneInfo Info { get { return stoneInfo; } }

	public void ChangeColor(Const.Color color) {
		Info.color = color;
		MaterialManager.Instance.SetMaterial (color, gameObject);
		ParticleManager.Instance.HitEffect (color, transform.position);
	}

	Stone() {
		stoneInfo = new StoneInfo ();
	}
}
