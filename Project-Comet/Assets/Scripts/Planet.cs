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
    [SerializeField]
    private ParticleSystem gravityParticle;
    [SerializeField]
    private float maxGravityParticleSize;
    [SerializeField]
    private float maxGravityParticleLifeTime;

    private Renderer renderer;
    [SerializeField]
    private Color[] colors;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.material.color = colors[Random.Range(0, colors.Length)];
        gravityParticle.startSize = maxGravityParticleSize;
        gravityParticle.startLifetime = maxGravityParticleLifeTime;
    }

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

    public void SetMaxOrbitSpeedDecayRate(float rate)
    {
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
        gravityParticle.startSize = GetOrbitSpeedPercantage(orbitSpeed) * maxGravityParticleSize;
        gravityParticle.startLifetime = GetOrbitSpeedPercantage(orbitSpeed) * maxGravityParticleLifeTime;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
