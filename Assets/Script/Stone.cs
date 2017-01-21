using UnityEngine;
using System.Collections;

public class Stone : MonoBehaviour
{
    public StoneInfo Info { get; private set; }
    public Osero Osero { get; private set; }

    public void ChangeColor(Const.Color color) {
		Info.color = color;
		MaterialManager.Instance.SetMaterial (color, gameObject);
		ParticleManager.Instance.HitEffect (color, transform.position);
	}

    public void SetOsero(Osero osero)
    {
        this.Osero = osero;
    }

    Stone() {
		Info = new StoneInfo ();
	}
}
