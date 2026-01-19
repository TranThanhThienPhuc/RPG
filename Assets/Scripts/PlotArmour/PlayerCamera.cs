using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform player;

    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(player.position.x, -23.3f, 15.9f), Mathf.Clamp(player.position.y, -8.5f, 13.3f), -10);
    }
}