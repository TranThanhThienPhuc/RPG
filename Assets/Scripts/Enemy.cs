using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public int index;
    public Vector2[] target;
    public float idleTime;
    private float time;
    public GameObject prefabObj;

    void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, target[index], Time.deltaTime * speed);

        // At destination
        if (transform.position.magnitude == target[index].magnitude)
        {
            time += Time.deltaTime;
        }

        // Continue patrolling after idle
        if (time > idleTime)
        {
            index++;
            time = 0;
        }

        // Loop to start
        if (index >= target.Length)
        {
            index = 0;
        }

    }
}