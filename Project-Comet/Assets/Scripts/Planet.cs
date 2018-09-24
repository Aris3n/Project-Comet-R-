using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

	public float maxOrbitRadius;
	public float orbitRadius;
	public float orbitRadiusSpeed;
	public Vector3 Axis = new Vector3(0,0,1);

	public void DisableEntryPoint () {
		GetComponent<CircleCollider2D>().enabled = false;
		orbitRadius = maxOrbitRadius;
	}

	public void GravityDecay(float orbitSpeedPercentage) {
		orbitRadius = orbitSpeedPercentage * maxOrbitRadius;
	}

	void OnBecameInvisible() {
         Destroy(gameObject);
     }
}
