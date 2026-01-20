using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [Header("Display")]
    public GameObject dialogueDisplay;
    public TMP_Text interactDisplay;
    public TMP_Text objectiveDisplay;
    public TMP_Text levelDisplay;
    public TMP_Text exp;
    public TMP_Text questCompleteDisplay;

    [Header("Reference")]
    public PlayerMovement player;
    [HideInInspector] public DialogueManager dialogue;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.interactable == true) interactDisplay.text = "F";
        else interactDisplay.text = string.Empty;

        if (player.isInteracting) StartDialogue();
        else player.freeze = false;

            levelDisplay.text = "Level: " + LevelManager.level;
        exp.text = "Exp: " + LevelManager.currentExp + "/" + LevelManager.expRequired;
    }

    void StartDialogue()
    {
        dialogueDisplay.SetActive(true);
    }

    public void SetObjective(string itemName, string itemAmount)
    {
        objectiveDisplay.text = "Current Objective:\n" + "Gather " + itemAmount + " " + itemName;
    }

    public void ClearObjective()
    {
        objectiveDisplay.text = string.Empty;
    }
    
    public IEnumerator QuestComplete()
    {
        questCompleteDisplay.text = "Quest Completed";
        yield return new WaitForSeconds(2f);
        questCompleteDisplay.text = string.Empty;
    }
}
