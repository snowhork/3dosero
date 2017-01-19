using UnityEngine;
using System.Collections;

public class Stone : MonoBehaviour
{
    private Osero _osero;
	StoneInfo stoneInfo;

	public StoneInfo Info { get { return stoneInfo; } }
    public Osero osero { get { return _osero; } }

    public void ChangeColor(Const.Color color) {
		Info.color = color;
		MaterialManager.Instance.SetMaterial (color, gameObject);
		ParticleManager.Instance.HitEffect (color, transform.position);
	}

    public void SetOsero(Osero osero)
    {
        this._osero = osero;
    }

    Stone() {
		stoneInfo = new StoneInfo ();
	}
}
