using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteMovement : MonoBehaviour
{
    public float scrollSpeed = 1f;
    public float resetPosition = -20f;
    public float startPosition = 20f;

    private void Update()
    {
        // Move the cloud to the left
        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

        // Check if the cloud has exited the view
        if (transform.position.x <= resetPosition)
        {
            // Move the cloud to the starting position
            transform.position = new Vector3(startPosition, transform.position.y, transform.position.z);
        }
    }
}
