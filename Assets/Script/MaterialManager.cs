using UnityEngine;
using System.Collections;

public class MaterialManager : SingletonMonoBehaviour<MaterialManager> {
	[SerializeField] Material BlackMaterial;
	[SerializeField] Material RedMaterial;
	[SerializeField] Material BlueMaterial;
	[SerializeField] Material YellowMaterial;


	public void SetMaterial(Const.Color color, GameObject obj) {
		Material material;
		switch (color) {
		case Const.Color.Black:
			material = BlackMaterial;
			break;
		case Const.Color.Red:
			material = RedMaterial;
			break;
		case Const.Color.Blue:
			material = BlueMaterial;
			break;
		case Const.Color.Yellow:
			material = YellowMaterial;
			break;
		default:
			material = BlueMaterial;
			break;
		}
		obj.GetComponent<Renderer> ().material = material;
	}
}
