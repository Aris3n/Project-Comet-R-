using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    public bool isClockwiseOrbit = true;

    public void EnterOrbit(Planet _planet)
    {
        _planet.DisableEntryPoint();
        transform.position = (transform.position - _planet.transform.position).normalized *
        _planet.orbitRadius + _planet.transform.position;
        transform.up = _planet.transform.position - transform.position;
        RotateShipToOrbit();
    }

    public void SetRotationDirection(float planetXPosition)
    {
        if (transform.position.x > planetXPosition)
            isClockwiseOrbit = true;
        else
            isClockwiseOrbit = false;
    }

    private void RotateShipToOrbit()
    {
        transform.GetChild(0).localRotation = Quaternion.identity;
        if (isClockwiseOrbit)
            transform.GetChild(0).localRotation = Quaternion.Euler(0, 0, 90);
        else
            transform.GetChild(0).localRotation = Quaternion.Euler(0, 0, -90);
    }

    public void ExitOrbit()
    {
        Quaternion originalRotation = transform.rotation;
        if (isClockwiseOrbit)
        {
            transform.rotation = originalRotation * Quaternion.AngleAxis(90, Vector3.forward);
        }
        else
        {
            transform.rotation = originalRotation * Quaternion.AngleAxis(-90, Vector3.forward);
        }
        transform.GetChild(0).localRotation = Quaternion.identity;
    }

    public void Fly(float _speed)
    {
        transform.position += transform.up * Time.deltaTime * _speed;
    }

    public void Orbit(Planet _planet, float _speed)
    {
        if (isClockwiseOrbit)
        {
            transform.RotateAround(_planet.transform.position, -Vector3.forward, _speed * Time.deltaTime);
        }
        else
        {
            transform.RotateAround(_planet.transform.position, Vector3.forward, _speed * Time.deltaTime);
        }
        Vector3 desiredPosition = (transform.position - _planet.transform.position).normalized * _planet.orbitRadius + _planet.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * _planet.orbitRadiusSpeed);
        _planet.GravityDecay(_speed);
    }
}