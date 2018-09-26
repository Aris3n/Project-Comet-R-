using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{

    public float maxOrbitRadius;
    public float orbitRadius;
    public float orbitRadiusSpeed;
    [SerializeField]
    private float maxOrbitSpeed;
    [SerializeField]
    private float orbitSpeedDecayRate;

    public float GetMaxOrbitSpeed()
    {
        return maxOrbitSpeed;
    }
    public float GetOrbitSpeedDecayRate()
    {
        return orbitSpeedDecayRate;
    }

    public float GetOrbitSpeedPercantage(float orbitSpeed)
    {
        return orbitSpeed / maxOrbitSpeed;
    }

    public void SetMaxOrbitSpeed(float speed)
    {
        maxOrbitSpeed = speed;
    }

    public void SetMaxOrbitSpeedDecayRate(float rate) {
        orbitSpeedDecayRate = rate;
    }

    public void DisableEntryPoint()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        orbitRadius = maxOrbitRadius;
    }

    public void GravityDecay(float orbitSpeed)
    {
        orbitRadius = GetOrbitSpeedPercantage(orbitSpeed) * maxOrbitRadius;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
