using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float[] movementRanges;
    private Vector3 destinationPosition;

    private void Start()
    {
        float startXPostion = movementRanges[Random.Range(0, movementRanges.Length)];
        transform.position = new Vector3(startXPostion, transform.position.y, transform.position.z);
        UpdateDestinationPostion();
    }

    private void Update()
    {
        if (transform.position == destinationPosition)
        {
			UpdateDestinationPostion();
        } else {
			float step = moveSpeed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, destinationPosition, step);
		}
    }

    private void UpdateDestinationPostion() {
		destinationPosition = new Vector3 (-transform.position.x, transform.position.y, transform.position.z);
	}

	private void OnTriggerEnter2D(Collider2D collider)
    {
        Destroy(this.gameObject);
    }

	private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
