using UnityEngine;

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