using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Animator ani;
    [HideInInspector] public Vector2 direction;
    [SerializeField] SpriteRenderer spriteRenderer;
    public bool interactable;
    public bool freeze;
    public bool isInteracting;
    public NPC questgiver;

    void Update()
    {
        if (freeze == true) return;
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        if (direction.x != 0)
            spriteRenderer.flipX = direction.x < 0;

        ani.SetBool("Moving", direction.sqrMagnitude > 0);

        if (Input.GetKeyDown(KeyCode.F) && interactable == true) Interact();
    }
    
    void Interact()
    {
        freeze = true;
        isInteracting = true;
        questgiver.GiveQuest();
        Debug.Log("Started dialogue");
    }
    private void FixedUpdate()
    {
        transform.Translate(Time.deltaTime * speed * direction.normalized);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            interactable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            interactable = false;
        }
    }
}