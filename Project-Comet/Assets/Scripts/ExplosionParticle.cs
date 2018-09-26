using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionParticle : MonoBehaviour
{
    [SerializeField]
    private float activeTime;
    void Start()
    {
        StartCoroutine(DeployParticle());
    }

    IEnumerator DeployParticle()
    {
        yield return new WaitForSeconds(activeTime);
        Destroy(this.gameObject);
    }
}
