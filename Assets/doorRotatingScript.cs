using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class doorRotatingScript : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform door;
    [SerializeField] private float distanceToPlayer;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float interactionDistance = 4f;
    

    private Quaternion originalAngle;
    private Quaternion changedAngle;
    private bool isRotating;

    void Start()
    {
        // Store the original rotation of the door
        originalAngle = door.rotation;
        // Calculate the new rotation (rotated by 45 degrees on the Y axis)
        changedAngle = originalAngle * Quaternion.Euler(0f, -150f, 0f);
    }

    void Update()
    {
        UpdatePlayerDistance();
    }

    private void UpdatePlayerDistance()
    {
        // Calculate the distance between the player and the door
        distanceToPlayer = (player.position - door.position).magnitude;

        // Rotate smoothly towards the target angle if player is close
        if (distanceToPlayer < interactionDistance)
        {
            // Rotate towards the changed angle when player is in range
            door.rotation = Quaternion.Slerp(door.rotation, changedAngle, Time.deltaTime * rotationSpeed);
        }
        else
        {
            // Rotate back to the original angle when player is out of range
            door.rotation = Quaternion.Slerp(door.rotation, originalAngle, Time.deltaTime * rotationSpeed);
        }
    }

}
