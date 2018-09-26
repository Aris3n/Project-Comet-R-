using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float[] movementXCoordinates;
    private Vector3 destinationCoordinates;
    [SerializeField]
    private GameObject particle;
    private void Start()
    {
        float startXPostion = movementXCoordinates[Random.Range(0, movementXCoordinates.Length)];
        transform.position = new Vector3(startXPostion, transform.position.y, transform.position.z);
        UpdateDestination();
    }
    private void Update()
    {
        if (transform.position == destinationCoordinates)
            UpdateDestination();
        else
            transform.position = Vector3.MoveTowards(transform.position, destinationCoordinates, moveSpeed * Time.deltaTime);
    }
    private void UpdateDestination()
    {
        destinationCoordinates = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (particle != null)
            Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
