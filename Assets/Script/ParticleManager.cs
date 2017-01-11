using UnityEngine;
using System.Collections;

public class ParticleManager : SingletonMonoBehaviour<ParticleManager> {
	[SerializeField] GameObject BlackHit;
	[SerializeField] GameObject RedHit;
	[SerializeField] GameObject BlueHit;
	[SerializeField] GameObject YellowHit;

	void SpawnParticle(GameObject particle, Vector3 position, float destroyTime= 5f) {
		var spawn_particle = Instantiate (particle);
		spawn_particle.transform.position = position;
		Destroy(spawn_particle, destroyTime);
	}

	public void HitEffect(Const.Color color, Vector3 position) {
		GameObject particle;
		switch (color) {
		case Const.Color.Black:
			particle = BlackHit;
			break;
		case Const.Color.Red:
			particle = RedHit;
			break;
		case Const.Color.Blue:
			particle = BlueHit;
			break;
		case Const.Color.Yellow:
			particle = YellowHit;
			break;
		default:
			particle = BlackHit;
			break;
		}
		SpawnParticle (particle, position);
	}
}
