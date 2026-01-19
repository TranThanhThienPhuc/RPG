using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public List<string> lines;
    public int lineIndex;
    private int conversationIndex;

    [Header("References")]
    //public string dialogue;
    //public string title;
    public Dialogue data;
    private InputAction interactAction;

    void Start()
    {
        interactAction = InputSystem.actions.FindAction("Interact");
    }

    // Update is called once per frame
    void Update()
    {
        if (lines.Count <= 0) return;
        if (lineIndex >= lines.Count) FinishConversation();
        if (interactAction.WasPressedThisFrame()) NextLine();
    }

    void NextLine()
    {
        lineIndex++;

        if (lineIndex >= lines.Count)
        {
            if (data != null) NextConversation();
            else FinishConversation();
        }
    }

    void NextConversation()
    {
        lineIndex = 0;
        conversationIndex++;

        if (conversationIndex >= data.conversations.Length)
        {
            FinishConversation();
            return;
        }
    }

    void FinishConversation()
    {
        data = null;
    }   
}
