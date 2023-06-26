using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{

    [SerializeField] public Transform target;        // Reference to the player's transform
    public Vector2 offset;
    public float smoothSpeed = 0.5f;

    private Vector3 velocity = Vector3.zero;

    void Start()
    {

    }

    private void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {

        Vector3 targetPosition = target.position + (Vector3)offset + new Vector3(0, 0, -1);

        // Smoothly move the camera towards the target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothSpeed);
    }


}

