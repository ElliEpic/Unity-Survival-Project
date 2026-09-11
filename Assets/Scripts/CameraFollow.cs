using UnityEngine;
// Script for making the camera follow the player
public class CameraFollow : MonoBehaviour
{
    public Transform player;

    void LateUpdate()
    {
        transform.position = new Vector3(
            player.position.x,
            player.position.y,
            transform.position.z
        );
    }
}

// Connecting my script to the camera in unity