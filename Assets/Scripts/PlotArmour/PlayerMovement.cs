using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

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
    public Quests activeQuest;
    public CanvasManager canvasManager;
    public int questStage;

    public InputAction fetch;
    public int ItemRetrieved;
    public bool canFetch;

    private List<string> ItemName;
    private List<int> ItemAmountRequired;


    private void Start()
    {
        fetch = InputSystem.actions.FindAction("Fetch");
    }
    void Update()
    {
        if (freeze == true) return;
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        if (direction.x != 0)
            spriteRenderer.flipX = direction.x < 0;

        ani.SetBool("Moving", direction.sqrMagnitude > 0);

        if (Input.GetKeyDown(KeyCode.F) && interactable == true) Interact();
        if (fetch.WasPressedThisFrame() && canFetch) Fetch();
    }

    void QuestLogic()
    {
        if (activeQuest == null) return;

        if (activeQuest.elements[questStage].data = null) Debug.LogError("No data to retrieve");

        switch (activeQuest.elements[questStage].stage)
        {
            case Quests.QuestStage.Dialogue:
                Dialogue dialogue = activeQuest.elements[questStage].data as Dialogue;
                freeze = true;
                break;
            case Quests.QuestStage.Fetch:
                ToFetch dataFetch = activeQuest.elements[questStage].data as ToFetch;
                ItemName = new();
                ItemAmountRequired = new();
                foreach (ToFetch.Target target in dataFetch.FetchList)
                {
                    ItemName.Add(target.type.itemName);
                    ItemAmountRequired.Add(target.amount);
                }
                canvasManager.SetObjective(dataFetch.FetchList[0].ToString(), dataFetch.FetchList[2].ToString());
                break;

        }
    }

    public void QuestStart(Quests newQuest)
    {
        print("Quest started");
        activeQuest = newQuest;
        questStage = 0;
        QuestLogic();
    }

    public void QuestAdvance()
    {
        ItemName = null;
        ItemAmountRequired = null;
        canvasManager.ClearObjective();
        questStage++;
        if (questStage < activeQuest.elements.Length) QuestLogic();
        else QuestComplete();
    }

    public void QuestComplete()
    {
        canvasManager.StartCoroutine(canvasManager.QuestComplete());
        questgiver.NextQuest();
        activeQuest = null;
    }
    
    void Interact()
    {
        freeze = true;
        isInteracting = true;
        questgiver.GiveQuest();
        Debug.Log("Started dialogue");
    }

    void Fetch()
    {
        if (ItemRetrieved <= ItemAmountRequired.Count - 1) ItemRetrieved++;
        else QuestAdvance();
        
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