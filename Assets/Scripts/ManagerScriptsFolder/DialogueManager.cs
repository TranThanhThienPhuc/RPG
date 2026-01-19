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
    

    void Start()
    {
        interactAction = InputSystem.actions.FindAction("Interact");
    }

    // Update is called once per frame
    void Update()
    {
        if (lines.Count <= 0) return;
        //if (lineIndex >= lines.Count) FinishConversation();

        dialogue.text = lines[lineIndex];
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
            if (data != null) NextConversation();
        }
        else FinishConversation();
    }

    public void NextConversation()
    {
        lineIndex = 0;
        conversationIndex++;
        lines = data.conversations[conversationIndex].lines.ToList();
        if (conversationIndex >= data.conversations.Length)
        {
            FinishConversation();
            return;
        }
    }

    public void FinishConversation()
    {
        data = null;
        canvasManager.dialogueDisplay.SetActive(false);
    }   

}
