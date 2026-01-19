using UnityEngine;
using UnityEngine.Events;

public class NPC : MonoBehaviour
{
    public Quest[] questOrder;
    public Dialogue incompletedDialogue;
    public Dialogue denyQuest;
    public int questIndex;


    [System.Serializable]
    public struct Quest
    {
        public Quests theQuest;
        public UnityEvent completedTask;
    }
    void GiveQuest()
    {

    }
    void NextQuest()
    {
        questIndex++;
    }
}
