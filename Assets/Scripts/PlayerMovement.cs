using UnityEngine;

public class PlayerMovement : UnityEngine.MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Animator ani;
    [HideInInspector] public Vector2 direction;
    [SerializeField] SpriteRenderer spriteRenderer;

    void Update()
    {
        direction.x = UnityEngine.Input.GetAxisRaw("Horizontal");
        direction.y = UnityEngine.Input.GetAxisRaw("Vertical");

        if (direction.x != 0)
            spriteRenderer.flipX = direction.x < 0;

        ani.SetBool("Moving", direction.sqrMagnitude > 0);
    }

    private void FixedUpdate()
    {
        transform.Translate(Time.deltaTime * speed * direction.normalized);
    }
}