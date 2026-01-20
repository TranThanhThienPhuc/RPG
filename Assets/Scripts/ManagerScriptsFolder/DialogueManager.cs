using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using TMPro;
using System.Linq;
using Unity.VisualScripting;
public class DialogueManager : MonoBehaviour
{
    public List<string> lines;
    public int lineIndex;
    private int conversationIndex;

    [Header("References")]
    public TMP_Text dialogue;
    public TMP_Text title;
    public Dialogue data;
    public Dialogue incompletedDialogue;
    public Dialogue denyQuest;
    private InputAction interactAction;
    public CanvasManager canvasManager;
    public PlayerMovement player;
    

    void Start()
    {
        interactAction = InputSystem.actions.FindAction("Interact");
    }

    // Update is called once per frame
    void Update()
    {
        if (lines.Count <= 0) return;
        //if (lineIndex >= lines.Count) FinishConversation();

        Debug.Log(lineIndex + "|" + lines.Count.ToString());

        if (lines != null) dialogue.text = lines[lineIndex];
        
        if (interactAction.WasPressedThisFrame()) NextLine();
    }

    public void NewDialogue(Dialogue data)
    {
        this.data = data;
        title.text = data.conversations[conversationIndex].title;
        lines = data.conversations[conversationIndex].lines.ToList();
        Debug.Log(data.conversations[0].title);
    }
    public void NextLine()
    {
        lineIndex++;
        if (lineIndex >= lines.Count)
        {
            lineIndex = 0;
            if (data != null) NextConversation();
        }
    }

    public void NextConversation()
    {
        conversationIndex++;
        
        if (conversationIndex >= data.conversations.Length)
        {
            FinishConversation();
            return;
        }
        else lines = data.conversations[conversationIndex].lines.ToList();
    }

    public void FinishConversation()
    {
        canvasManager.player.isInteracting = false;
        lineIndex = 0;
        conversationIndex = 0;
        //data = null;
        player.QuestAdvance();
        gameObject.SetActive(false);
    }   

}
