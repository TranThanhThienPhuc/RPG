using UnityEngine;
using UnityEngine.Events;

public class NPC : MonoBehaviour
{
    public Quest[] questOrder;
    public DialogueManager dialogueManager;
    public int questIndex;
    public int questStage;
    public bool incomplete;
    public PlayerMovement player;


    [System.Serializable]
    public struct Quest
    {
        public Quests theQuest;
        public UnityEvent completedTask;
    }
    public void GiveQuest()
    {
        print("d");
        if (questOrder == null)
        {
            print("There is no quest");
            return;
        }
        dialogueManager.NewDialogue(questOrder[questIndex].theQuest.elements[questStage].data as Dialogue);
        player.QuestStart(questOrder[questIndex].theQuest);
    }

    public void NextQuest()
    {
        questIndex++;
    }
}
