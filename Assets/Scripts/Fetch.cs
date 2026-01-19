using UnityEngine;

public class Fetch : MonoBehaviour
{
    public bool collectable;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            Collect();
        }
    }

    public void Collect()
    {

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Herbs"))
        {
            collectable = true;
        }
    }
}
