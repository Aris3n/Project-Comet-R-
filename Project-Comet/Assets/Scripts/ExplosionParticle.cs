using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionParticle : MonoBehaviour {

	void Start () 
	{
		StartCoroutine(DeployParticle());
	}
	
	IEnumerator DeployParticle()
    {
        yield return new WaitForSeconds(0.25f);
        Destroy(this.gameObject);
    }
}
