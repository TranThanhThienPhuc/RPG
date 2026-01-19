using UnityEngine;
using UnityEngine.Events;

public class NPC : MonoBehaviour
{
    public Quest[] questOrder;
    public DialogueManager dialogueMan;
    public int questIndex;


    [System.Serializable]
    public struct Quest
    {
        public Quests theQuest;
        public UnityEvent completedTask;
    }
    public void GiveQuest()
    {
        if (questOrder == null)
        {
            print("There is no quest");
            return;
        }
        dialogueMan.NewDialogue(questOrder[questIndex].theQuest.elements[questIndex].data as Dialogue);
    }
    void NextQuest()
    {
        questIndex++;
    }
}
