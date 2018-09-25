using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour {
	
	void Start () {

	}
	
	void FixedUpdate () {

	}

	public void EnterOrbit (Planet _planet) {
		_planet.DisableEntryPoint();
		transform.position = (transform.position - _planet.transform.position).normalized * 
		_planet.orbitRadius + _planet.transform.position;
 		transform.up = _planet.transform.position - transform.position;
	}
	public void ExitOrbit (bool _isClockwise) {
		Quaternion originalRotation = transform.rotation;
		if (_isClockwise) { 
			transform.rotation = originalRotation * Quaternion.AngleAxis(90, Vector3.forward);
		} else {
			transform.rotation = originalRotation * Quaternion.AngleAxis(-90, Vector3.forward);
		}
	}

	public void Fly (float _speed) {
			transform.position += transform.up * Time.deltaTime * _speed;
	}

	public void Orbit (Planet _planet, float _speed, bool _isClockwise) {
		if (_isClockwise) {
			transform.RotateAround (_planet.transform.position, -Vector3.forward, _speed * Time.deltaTime);
		} else {
			transform.RotateAround (_planet.transform.position, Vector3.forward, _speed * Time.deltaTime);
		}
    	Vector3 desiredPosition = (transform.position - _planet.transform.position).normalized * _planet.orbitRadius + _planet.transform.position;
    	transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * _planet.orbitRadiusSpeed);
	}
}
