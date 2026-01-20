using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Quests.Stages stage;
    public PlayerMovement player;
    public NPC questGiver;

    public void ReceiveQuest(Quests.Stages stages)
    {
        this.stage = stages;
    }

}
